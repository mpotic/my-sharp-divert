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
			IReceiveResponse receiveResponse = handler.ReceiveSinglePacket();

			return receiveResponse;
		}

		public IResponse SharpDivertSend()
		{
			throw new NotImplementedException();
		}

		public IResponse SharpDivertClose()
		{
			throw new NotImplementedException();
		}
	}
}
