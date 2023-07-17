﻿using System.Threading.Tasks;

namespace PacketSniffer
{
	internal interface IOptions
	{
		void Begin();

		void InterceptAndForward();

		void StopIntercepting();
	}
}