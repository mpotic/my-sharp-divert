using System;

namespace PacketSniffer
{
	internal class PacketQueue : IPacketQueue
	{
		public IMessage AddToQueue()
		{
			throw new NotImplementedException();
		}

		public IMessage GetFromQueue()
		{
			throw new NotImplementedException();
		}
	}
}
