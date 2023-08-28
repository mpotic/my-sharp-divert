using MySharpDivert;
using PacketSniffer.Enums;
using System;
using System.Threading;

namespace PacketSniffer
{
	internal class Interceptor : IInterceptor
	{
		ISharpDivertApi sharpDivert;

		WorkerStatus status = WorkerStatus.Inactive;

		string exceptionMessage;

		public Interceptor(ISharpDivertApi sharpDivert)
		{
			this.sharpDivert = sharpDivert;
		}

		public void InterceptAndForward(string filter)
		{
			if(status != WorkerStatus.Inactive)
			{
                Console.WriteLine("Already intercepting!");

                return;
			}

			if (!Open(filter))
			{
				return;
			}

			Console.WriteLine("Intercepting started.");

			if (StartIntercepting())
			{
				Console.WriteLine("Intercepting stopped.");
			}
			else
			{
				Console.WriteLine("Intercepting terminated! " + exceptionMessage);
			}
		}

		private bool Open(string userFilter)
		{
			string connectFilter = "tcp.PayloadLength > 0";
			string filter = $"({userFilter}) and {connectFilter}";
			
			IResponse response = sharpDivert.Open(filter);
			if (!response.IsSuccessful)
			{
				Console.WriteLine("Failed: " + response.ErrorMessage);

				return false;
			}

			return true;
		}

		private bool StartIntercepting()
		{
			IReceiveResponse message;
			status = WorkerStatus.Active;

			while (status == WorkerStatus.Active)
			{
				try
				{
					message = sharpDivert.ReceiveSinglePacket();
					ProcessPacket(message);
                }
				catch (Exception e)
				{
					status = WorkerStatus.Inactive;
					exceptionMessage = e.Message;

					return false;
				}
			}

			status = WorkerStatus.Inactive;

			return true;
		}

		private void ProcessPacket(IReceiveResponse packet)
		{
			IMessage message = new Message(packet);
			Console.WriteLine(message);
			sharpDivert.SharpDivertSend(message.Packet, message.Address);
		}

		public void StopIntercepting()
		{
			if(status != WorkerStatus.Active)
			{
                Console.WriteLine("Intercepting not started!");

                return;
			}

			status = WorkerStatus.ShuttingDown;
			sharpDivert.ShutdownHandle();
			sharpDivert.CloseHandle();

			while (status != WorkerStatus.Inactive)
			{
				Thread.Sleep(0);
			}
		}
	}
}
