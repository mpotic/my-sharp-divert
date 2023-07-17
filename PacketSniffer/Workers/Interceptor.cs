using MySharpDivert;
using PacketSniffer.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PacketSniffer
{
	internal class Interceptor : IInterceptor
	{
		ISharpDivertApi sharpDivert;

		WorkerStatus isIntercepting = WorkerStatus.Inactive;

		public Interceptor(ISharpDivertApi sharpDivert)
		{
			this.sharpDivert = sharpDivert;
		}

		public void InterceptAndForward()
		{
			Open();
			Console.WriteLine("Intercepting started...");
			StartIntercepting();
        }

		private void Open()
		{
			try
			{
				sharpDivert.Open("tcp.SrcPort == 21000 || tcp.SrcPort == 21001");
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);

				return;
			}
		}

		private bool StartIntercepting()
		{
			IReceiveResponse message;
			isIntercepting = WorkerStatus.Active;

			while (isIntercepting == WorkerStatus.Active)
			{
				try
				{
					message = sharpDivert.ReceiveSinglePacket();
					Console.WriteLine("Message received: " + message.ToString());
				}
				catch(Exception e)
				{
					Console.WriteLine(e.Message);
					isIntercepting = WorkerStatus.Inactive;

					return false;
				}
			}

			isIntercepting = WorkerStatus.Inactive;

			return true;
		}

		public void StopIntercepting()
		{
			isIntercepting = WorkerStatus.ShuttingDown;

			while (isIntercepting != WorkerStatus.Inactive)
			{
				Thread.Sleep(0);
			}

			Console.WriteLine("Stopped intercepting requests...");
		}

		public void InterceptAndAddToQueue()
		{
			throw new NotImplementedException();
		}
	}
}
