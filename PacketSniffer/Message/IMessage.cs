using MySharpDivert.Native;
using PacketSniffer.Enums;

namespace PacketSniffer
{
	internal interface IMessage
	{
		FunctionCode FunctionCode { get; set; }

		WinDivertAddress Address { get; set; }

		byte[] Payload { get; set; }

		byte[] Header { get; set; }

		string ToString();
	}
}
