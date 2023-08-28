using MySharpDivert.Native;

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
		IReceiveResponse ReceivePacket();

		IResponse SendPacket(byte[] packet, WinDivertAddress address);
		
		IResponse ShutdownHandle();

		IResponse CloseHandle();
	}
}
