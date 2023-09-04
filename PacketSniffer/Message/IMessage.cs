using MySharpDivert;
using MySharpDivert.Native;
using PacketSniffer.Enums;

namespace PacketSniffer
{
	internal interface IMessage
	{
		public WinDivertAddress Address { get; }

		public HeadersData TcpIpHeaderData { get; }

		public byte[] TcpIpHeader { get; }

		public byte[] Header { get; }

		public byte[] Payload { get; }

		public byte[] Packet { get; }

		public FunctionCode FuncCode { get; }

		public SenderCode Sender { get; }

		public bool PopulateMessage(IPacketResponse receiveResponse);

		string ToStringWithCustomTitle(string title);
	}
}
