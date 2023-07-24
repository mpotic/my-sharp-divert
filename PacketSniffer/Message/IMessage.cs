using MySharpDivert.Native;
using PacketSniffer.Enums;
using System;

namespace PacketSniffer
{
	internal interface IMessage
	{
		FunctionCode FuncCode { get; set; }

		WinDivertAddress Address { get; set; }

		byte[] Payload { get; set; }

		byte[] Header { get; set; }
		
		byte[] Packet { get; }

		string ToString();
	}
}
