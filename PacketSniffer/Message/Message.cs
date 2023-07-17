using MySharpDivert;
using MySharpDivert.Native;
using PacketSniffer.Enums;
using System.Text;

namespace PacketSniffer
{
	internal class Message : IMessage
    {
		public Message(IReceiveResponse receiveResponse)
		{
			string packet = Encoding.UTF8.GetString(receiveResponse.Packet);
		}

		public FunctionCode FunctionCode { get; set; }
	
		public WinDivertAddress Address { get; set; }
		
		public byte[] Payload { get; set; }
		
		public byte[] Header { get; set; }
	}
}
