namespace PacketSniffer
{
	internal interface IPacketQueue
	{
		IMessage AddToQueue();

		IMessage GetFromQueue();
	}
}
