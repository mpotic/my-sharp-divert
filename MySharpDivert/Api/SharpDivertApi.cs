using MySharpDivert.Native;
using System;

namespace MySharpDivert
{
	public class SharpDivertApi : ISharpDivertApi
	{
		private ISharpDivert handler = new SharpDivert();

		public IResponse Open(string filter)
		{
			IResponse response = handler.Open(filter);

			return response;
		}

		public IReceiveResponse ReceiveSinglePacket()
		{
			IReceiveResponse receiveResponse = handler.ReceivePacket();

			return receiveResponse;
		}

		public IResponse SharpDivertSend(byte[] packet, WinDivertAddress address)
		{
			IResponse response = handler.SendPacket(packet, address);

			return response;
		}
	}
}
