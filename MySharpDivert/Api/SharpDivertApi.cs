using MySharpDivert.Native;

namespace MySharpDivert
{
    public class SharpDivertApi : ISharpDivertApi
	{
		private ISharpDivert divertHandler = new SharpDivert();

		public IResponse Open(string filter)
		{
			IResponse response = divertHandler.Open(filter);

			return response;
		}

		public IReceiveResponse ReceiveSinglePacket()
		{
			IReceiveResponse receiveResponse = divertHandler.ReceivePacket();

			return receiveResponse;
		}

		public IResponse SharpDivertSend(byte[] packet, WinDivertAddress address)
		{
			IResponse response = divertHandler.SendPacket(packet, address);

			return response;
		}

		public IResponse CloseHandle()
		{
			IResponse response = divertHandler.CloseHandle();

			return response;
		}

		public IResponse ShutdownHandle()
		{
			IResponse response = divertHandler.ShutdownHandle();

			return response;
		}
	}
}
