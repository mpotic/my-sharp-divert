using MySharpDivert;
using MySharpDivert.Native;
using PacketSniffer.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PacketSniffer
{
	internal class Message : IMessage
	{
		public WinDivertAddress Address { get; private set; }

		public HeadersData TcpIpHeaderData { get; private set; }

		public FunctionCode FuncCode { get; private set; }

		public SenderCode Sender { get; private set; }

		public byte[] TcpIpHeader { get; private set; }

		public byte[] Header { get; private set; }

		public byte[] Payload { get; private set; }

		public string GetHeaderStr
		{
			get
			{
				return Encoding.UTF8.GetString(Header);
			}
		}

		public string GetPayloadStr
		{
			get
			{
				string payloadStr = Encoding.UTF8.GetString(Payload);
                payloadStr = payloadStr.Replace("\0", string.Empty);
                payloadStr = Regex.Replace(payloadStr, "[^0-9A-Za-z;,]", "?");

				return payloadStr;
			}
		}

		public byte[] Packet
		{
			get
			{
				byte[] slashes = Encoding.UTF8.GetBytes("//");
				byte[] packet = TcpIpHeader.Concat(Header).Concat(slashes).Concat(Payload).ToArray();

				return packet;
			}
		}

		string GetFunctionCodeStr
		{
			get
			{
				Dictionary<FunctionCode, string> mapping = new Dictionary<FunctionCode, string>
				{
					{ FunctionCode.ReadAnalogInputs, "reading analog input registers" },
					{ FunctionCode.ReadDiscreteInputs, "reading discrete input registers" },
					{ FunctionCode.ReadCoils, "read coil registers" },
					{ FunctionCode.ReadHolding, "read holding registers" },
					{ FunctionCode.WriteCoils, "write coil registers" },
					{ FunctionCode.WriteHolding, "write holding registers" }
				};

				if (mapping.TryGetValue(FuncCode, out string funCode))
				{
					return funCode;
				}

				return "Unknown function code...";
			}
		}

		string GetFunctionCodeHex
		{
			get
			{
				if (Enum.TryParse(Encoding.UTF8.GetString(Header).Split(';')[2], out FunctionCode parsedCode))
				{
					return parsedCode.ToString("X");
				}

				return string.Empty;
			}
		}

		string GetSenderStr
		{
			get
			{
				Dictionary<SenderCode, string> mapping = new Dictionary<SenderCode, string>
				{
					{ SenderCode.ProxyToSlave, "Proxy is sending a message for a slave to another proxy."},
					{ SenderCode.ProxyToMaster, "Proxy is sending a message for a master to another proxy."},
					{ SenderCode.Master, "Master is sending a slave IO request to a proxy."}
				};

				if (mapping.TryGetValue(Sender, out string sender))
				{
					return sender;
				}

				return "Unkown sender...";
			}
		}

		public bool PopulateMessage(IPacketResponse packetResponse)
		{
			int tcpIpHeaderLen = packetResponse.Packet.Length - (int)packetResponse.Headers.PacketDataLength;
			TcpIpHeader = new byte[tcpIpHeaderLen];
			Array.Copy(packetResponse.Packet, TcpIpHeader, tcpIpHeaderLen);

			byte[] packetMessage = new byte[packetResponse.Headers.PacketDataLength];
			Array.Copy(packetResponse.Packet, tcpIpHeaderLen, packetMessage, 0, (int)packetResponse.Headers.PacketDataLength);
			string packetMessageStr = Encoding.UTF8.GetString(packetMessage);
			
			if (!Regex.IsMatch(packetMessageStr, @"^\d*;[^;]*;[^/]*//[^;]*;\d*;[^;]*"))
			{
				Console.WriteLine("Unknown message contents.");

				return false;
			}

			Header = Encoding.UTF8.GetBytes(packetMessageStr.Split("//")[0]);
			int payloadSize = (int)packetResponse.Headers.PacketDataLength - Header.Length - 2;
			Payload = new byte[payloadSize];
			Array.Copy(packetMessage, Header.Length + 2, Payload, 0, payloadSize);
			TcpIpHeaderData = packetResponse.Headers;
			Address = packetResponse.Address;

			if (!Enum.TryParse(GetHeaderStr.Split(';')[2], out FunctionCode funCode) ||
				!Enum.TryParse(GetHeaderStr.Split(';')[1], out SenderCode senderCode))
			{
				Console.WriteLine("Unknown message contents.");

				return false;
			}

			FuncCode = funCode;
			Sender = senderCode;

			return true;
		}

		private void WriteMessageToFile()
		{
			string tcpIpHeader = "\tTCP/IPv4 header\n" + BitConverter.ToString(TcpIpHeader).Replace('-', ' ');
			string header = "\n\tCustom header\n" + BitConverter.ToString(Header).Replace('-', ' ');
			string payload = "\n\tPayload\n" + 
				BitConverter.ToString(Encoding.UTF8.GetBytes(GetPayloadStr.Remove('\0'))).Replace('-', ' ');
			string text = tcpIpHeader + header + payload;

			File.WriteAllText("LatestPacketInfo.txt", text);
		}

		public override string ToString()
		{
			string stringRepresentation =
				"--------------------------- MESSAGE ---------------------------\n" +
				$"Total length: {Packet.Length}\n" +
				$"Custom Header: {GetHeaderStr}\n" +
				$"Function code: {GetFunctionCodeHex} ({GetFunctionCodeStr})\n" +
				$"Sender info: {GetSenderStr}\n" +
				$"Payload: {GetPayloadStr}\n" +
				TcpIpHeaderData.IPv4HeaderData +
				TcpIpHeaderData.TcpHeaderData +
				"---------------------------------------------------------------";

			WriteMessageToFile();

			return stringRepresentation;
		}

		public string ToStringWithCustomTitle(string title)
		{
			string titleLine = $"--------------------------- {title} ---------------------------";

			string stringRepresentation =
				titleLine + "\n" +
				$"Total length: {Packet.Length}\n" +
				$"Custom Header: {GetHeaderStr}\n" +
				$"Function code: {GetFunctionCodeHex} ({GetFunctionCodeStr})\n" +
				$"Sender info: {GetSenderStr}\n" +
				$"Payload: {GetPayloadStr}\n" +
				TcpIpHeaderData.IPv4HeaderData +
				TcpIpHeaderData.TcpHeaderData +
				string.Join('-', new char[titleLine.Length - 1]);

			WriteMessageToFile();

			return stringRepresentation;
		}
	}
}
