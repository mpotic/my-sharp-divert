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
			if (!Encoding.UTF8.GetString(receiveResponse.Packet).Contains("//"))
			{
				Console.WriteLine("Unknown message structure.");
				return;
			}

			Packet = new byte[receiveResponse.Packet.Length];
			Array.Copy(receiveResponse.Packet, Packet, receiveResponse.Packet.Length);
			
			string strPacket = Encoding.UTF8.GetString(receiveResponse.Packet);
            string strHeader = strPacket.Split("//")[0];
			string strPayload = strPacket.Split("//")[1];

			Header = Encoding.UTF8.GetBytes(strHeader);
			Payload = Encoding.UTF8.GetBytes(strPayload);
			Address = receiveResponse.Address;

			if (Enum.TryParse(strHeader.Split(';')[2], out FunctionCode funCode))
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

		public byte[] Packet { get; set; }

		public override string ToString()
		{
			string stringRepresentation =
				"--------------------- MESSAGE ---------------------\n" +
				$"Function code: 0x{FuncCode.ToString("X")} ({FuncCode})\n" +
				$"Header: {Encoding.UTF8.GetString(Header)}\n" +
				$"Payload: {Encoding.UTF8.GetString(Payload)}\n" +
				"---------------------------------------------------";

			return stringRepresentation;
		}
	}
}
