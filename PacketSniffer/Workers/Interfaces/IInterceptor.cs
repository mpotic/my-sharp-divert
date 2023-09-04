namespace PacketSniffer
{
	internal interface IInterceptor
	{
		void PrepareInterception(string filter, InterceptionMode mode);

		void StopIntercepting();
	}
}
