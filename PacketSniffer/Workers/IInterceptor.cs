namespace PacketSniffer
{
	internal interface IInterceptor
	{
		void InterceptAndForward();

		void StopIntercepting();

		void InterceptAndAddToQueue();
	}
}
