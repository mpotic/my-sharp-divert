using MySharpDivert.Native;
using System.Text;

namespace MySharpDivert
{
	public class PacketResponse : Response, IPacketResponse
    {
		public PacketResponse(bool isSuccessful, string errorMessage) : base(isSuccessful, errorMessage)
		{
		}

		public PacketResponse(byte[] packet, HeadersData headers, WinDivertAddress winDivertAddress) : base(true)
		{
			Packet = packet;
			Headers = headers;
			Address = winDivertAddress;
		}

		public HeadersData Headers { get; internal set; }

		public byte[] Packet { get; internal set; }

		public WinDivertAddress Address { get; internal set; }

		public override string ToString()
		{
			var result = "Packet: " + Encoding.UTF8.GetString(Packet) + "\n";

			return result;
		}
	}
}
