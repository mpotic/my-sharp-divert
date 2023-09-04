using MySharpDivert.Native;
using MySharpDivert.Native.Enums;

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

		public PacketResponse ReceiveSinglePacket()
		{
			PacketResponse receiveResponse = divertHandler.ReceivePacket();

			return receiveResponse;
		}

		public IResponse SendSinglePacket(byte[] packet, WinDivertAddress address)
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

		public IPacketResponse CalculateChecksum(byte[] packet, WinDivertAddress address, ChecksumCalcFlags flags)
		{
			IPacketResponse response = divertHandler.CalculateChecksum(packet, address, flags);

			return response;
		}
	}
}
