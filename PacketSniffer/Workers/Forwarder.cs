using MySharpDivert;

namespace PacketSniffer.Workers
{
	internal class Forwarder : IInterceptionProcessor
	{
		ISharpDivertApi sharpDivert;

		public Forwarder(ISharpDivertApi sharpDivert)
		{
			this.sharpDivert = sharpDivert;
		}

		public void ProcessInterceptedMessage(IMessage message)
		{
			sharpDivert.SendSinglePacket(message.Packet, message.Address);
		}
	}
}
