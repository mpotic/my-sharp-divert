using MySharpDivert.Native;

namespace MySharpDivert
{
	public interface ISharpDivertApi
	{
		IResponse Open(string filter);

		IReceiveResponse ReceiveSinglePacket();

		IResponse SharpDivertSend(byte[] packet, WinDivertAddress address);

		IResponse CloseHandle();

		IResponse ShutdownHandle();
	}
}
