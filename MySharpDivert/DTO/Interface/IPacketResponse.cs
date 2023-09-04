using MySharpDivert.Native;

namespace MySharpDivert
{
	public interface IPacketResponse : IResponse
	{
		HeadersData Headers { get; }

		byte[] Packet { get; }

		WinDivertAddress Address { get; }

		string ToString();
	}
}
