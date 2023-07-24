using MySharpDivert;
using MySharpDivert.Native;
using PacketSniffer.Enums;
using System;
using System.Linq;
using System.Text;

namespace PacketSniffer
{
	internal class Message : IMessage
	{
		public Message(IReceiveResponse receiveResponse)
		{
			Address = receiveResponse.Address;

			Header = new byte[20];
			Array.Copy(receiveResponse.Packet, Header, 20);

			int payloadLength = receiveResponse.Packet.Length - 20;
			Payload = new byte[payloadLength];
			Array.Copy(receiveResponse.Packet, 20, Payload, 0, payloadLength);

			if (Enum.TryParse(Encoding.UTF8.GetString(Payload).Split("//")[0].Split(';')[0], out FunctionCode funCode))
			{
				FuncCode = funCode;
			}
			else
			{
				FuncCode = FunctionCode.Unknown;
			}
		}

		public FunctionCode FuncCode { get; set; }

		public WinDivertAddress Address { get; set; }

		public byte[] Payload { get; set; }

		public byte[] Header { get; set; }

		public byte[] Packet
		{
			get
			{
				return Header.Concat(Payload).ToArray();
			}
		}

		public override string ToString()
		{
			string stringRepresentation = $"Function code: {FuncCode.ToString("X")} ({FuncCode})\n" +
				$"Header: 0x{BitConverter.ToString(Header)}\n" +
				$"Payload: {Encoding.UTF8.GetString(Payload)}";

			return stringRepresentation;
		}
	}
}
