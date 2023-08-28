namespace PacketSniffer
{
	internal interface IInterceptor
	{
		void InterceptAndForward(string filter);

		void StopIntercepting();
	}
}
