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
			if (!Open())
			{
				return;
			}

			Console.WriteLine("Intercepting started...");
			StartIntercepting();
		}

		private bool Open()
		{
			string portsFilter = "(tcp.SrcPort == 22000 or tcp.SrcPort == 22001)";
			string connectFilter = "tcp.PayloadLength > 0";
			string filter = portsFilter + " and " + connectFilter;
			
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
			isIntercepting = WorkerStatus.Active;

			while (isIntercepting == WorkerStatus.Active)
			{
				try
				{
					message = sharpDivert.ReceiveSinglePacket();
					ProcessPacket(message);
                }
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					isIntercepting = WorkerStatus.Inactive;

					return false;
				}
			}

			isIntercepting = WorkerStatus.Inactive;

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
