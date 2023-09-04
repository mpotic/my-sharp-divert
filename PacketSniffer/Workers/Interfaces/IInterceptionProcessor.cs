namespace PacketSniffer
{
	internal interface IInterceptionProcessor
	{
		void ProcessInterceptedMessage(IMessage message);
	}
}
