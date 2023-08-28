using MySharpDivert.Native;

namespace MySharpDivert
{
	public interface IReceiveResponse : IResponse
	{
		byte[] Packet { get; }

		WinDivertAddress Address { get; }

		string ToString();
	}
}
