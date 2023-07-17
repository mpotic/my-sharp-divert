using MySharpDivert.Native;
using System.Text;

namespace MySharpDivert
{
	public class ReceiveResponse : Response, IReceiveResponse
    {
		public ReceiveResponse(bool isSuccessful) : base(isSuccessful)
		{
		}

		public ReceiveResponse(bool isSuccessful, string errorMessage) : base(isSuccessful, errorMessage)
		{
		}

		public ReceiveResponse(byte[] packet) : base(true)
		{
			Packet = packet;
		}

		public ReceiveResponse(byte[] packet, WinDivertAddress winDivertAddress) : this(packet)
		{
			Address = winDivertAddress;
		}

		public byte[] Packet { get; internal set; }

		public WinDivertAddress Address { get; internal set; }

		public override string ToString()
		{
			var result = "Packet: " + Encoding.UTF8.GetString(Packet) + "\n";

			return result;
		}
	}
}
