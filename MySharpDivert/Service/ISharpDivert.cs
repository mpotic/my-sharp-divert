using MySharpDivert.Native;
using MySharpDivert.Native.Enums;

namespace MySharpDivert
{
	internal interface ISharpDivert
	{
		/// <summary>
		/// Opens a connection with specified filter, making a handle.
		/// </summary>
		IResponse Open(string filter);

		/// <summary>
		/// Receives a single packet.
		/// </summary>
		PacketResponse ReceivePacket();

		IResponse SendPacket(byte[] packet, WinDivertAddress address);
		
		IResponse ShutdownHandle();

		IResponse CloseHandle();

		IPacketResponse CalculateChecksum(byte[] packet, WinDivertAddress address, ChecksumCalcFlags flags);
	}
}
