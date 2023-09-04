namespace PacketSniffer.Workers.ModificationCommands
{
	internal interface IBaseModifier
	{
		void RecalculateLengthInHeader(string payload, ref byte[] header);
		
		void ExtractSignatureAndDataFromPayload(byte[] header, byte[] payload, out string plainPayload, out string signature);
	}
}
