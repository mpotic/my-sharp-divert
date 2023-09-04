using System;
using System.Text;

namespace PacketSniffer.Workers.ModificationCommands
{
	internal class BaseModifier : IBaseModifier
	{
		public void RecalculateLengthInHeader(string payload, ref byte[] header)
		{
			payload = payload.Replace("\0", string.Empty);

			// Create header.
			string headerStr = Encoding.UTF8.GetString(header);
			string newHeaderStr = headerStr.Substring(headerStr.IndexOf(';'));

			// Calculate the size of the message.
			int dataLength = (newHeaderStr.Length + payload.Length + 2); // Plus 2 is for the separator ("//").
			int length = dataLength + dataLength.ToString().Length;
			length += length.ToString().Length - dataLength.ToString().Length; // Add 1 if size increased by one digit.

			header = Encoding.UTF8.GetBytes(length.ToString() + newHeaderStr);
        }

		public void ExtractSignatureAndDataFromPayload(byte[] header, byte[] payload, out string plainPayload, out string signature)
		{
			int.TryParse(Encoding.UTF8.GetString(header).Split(';')[0], out int payloadLength);
			payloadLength -= Encoding.UTF8.GetString(header).Length - 2; // Minus 2 is for the separator ("//").
			if (payloadLength <= 0)
			{
				throw new Exception("Invalid payload length!");
			}

			string payloadStr = Encoding.UTF8.GetString(payload);
			plainPayload = payloadStr.Substring(0, payloadLength);
			signature = payloadStr.Substring(payloadLength);
		}
	}
}
