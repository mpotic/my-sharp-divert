namespace PacketSniffer.Workers.ModificationCommands
{
	internal interface IPacketModifier
	{
		public byte[] Packet { get; }

		public void Prepare(IMessage message);

		public void RandomizeValues();
	}
}
