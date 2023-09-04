using MySharpDivert.Native;
using MySharpDivert.Native.Enums;

namespace MySharpDivert
{
	public interface ISharpDivertApi
	{
		IResponse Open(string filter);

		PacketResponse ReceiveSinglePacket();

		IResponse SendSinglePacket(byte[] packet, WinDivertAddress address);

		IResponse CloseHandle();

		IResponse ShutdownHandle();

		IPacketResponse CalculateChecksum(byte[] packet, WinDivertAddress address, ChecksumCalcFlags flags);
	}
}
