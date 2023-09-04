using MySharpDivert;
using PacketSniffer.Enums;
using PacketSniffer.Workers;
using System;
using System.Threading;

namespace PacketSniffer
{
	internal class Interceptor : IInterceptor
	{
		readonly ISharpDivertApi sharpDivert;

		WorkerStatus status = WorkerStatus.Inactive;

		IInterceptionProcessor processor;

		string exceptionMessage;

		public Interceptor(ISharpDivertApi sharpDivert)
		{
			this.sharpDivert = sharpDivert;
			processor = new Forwarder(sharpDivert);
		}

		public bool SetInterceptionMode(InterceptionMode mode)
		{
			try
			{
				processor = mode == InterceptionMode.Modify ? new Modifier(sharpDivert) : new Forwarder(sharpDivert);
			}
			catch (Exception ex)
			{
				exceptionMessage = ex.Message;

				return false;
			}

			return true;
		}

		public void PrepareInterception(string filter, InterceptionMode mode)
		{
			if (status != WorkerStatus.Inactive)
			{
				Console.WriteLine("Already intercepting!");

				return;
			}

			if (!SetInterceptionMode(mode))
			{
				Console.WriteLine("Failed to set interception mode! " + exceptionMessage);

				return;
			}

			if (!Open(filter))
			{
				Console.WriteLine("Failed to open! " + exceptionMessage);

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
				exceptionMessage = response.ErrorMessage;

				return false;
			}

			return true;
		}

		private bool StartIntercepting()
		{
			PacketResponse message;
			status = WorkerStatus.Active;

			while (status == WorkerStatus.Active)
			{
				try
				{
					message = sharpDivert.ReceiveSinglePacket();

					if (!message.IsSuccessful)  //Error with WinDivert => assume invalid packet and don't process it.
					{
						Console.WriteLine(message.ErrorMessage);

						continue;
					}

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

		private void ProcessPacket(PacketResponse packet)
		{
			try
			{
				IMessage message = new Message();
				if (!message.PopulateMessage(packet))
				{
					throw new Exception("Parsing failed.");
				}
				Console.WriteLine(message);
				processor.ProcessInterceptedMessage(message);
			}
			catch (Exception e)
			{
				if (packet.Packet != null)
				{
					sharpDivert.SendSinglePacket(packet.Packet, packet.Address);
				}

				Console.WriteLine(e.Message);
			}
		}

		public void StopIntercepting()
		{
			if (status != WorkerStatus.Active)
			{
				Console.WriteLine("Interception wasn't started!");

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
