using System;
using System.Linq;
using System.Text;

namespace PacketSniffer.Workers.ModificationCommands
{
	internal class WriteAnalogRequestModifier : IPacketModifier
	{
		private byte[] tcpIpHeader;

		private byte[] header;

		private byte[] payload;

		private int packetSize;

		IBaseModifier baseModifier = new BaseModifier();

		public byte[] Packet
		{
			get
			{
				byte[] slashes = Encoding.UTF8.GetBytes("//");
				byte[] packet = tcpIpHeader.Concat(header).Concat(slashes).Concat(payload).ToArray();
				Array.Resize(ref packet, packetSize);

				return packet;
			}
		}

		public void Prepare(IMessage message)
		{
			tcpIpHeader = new byte[message.TcpIpHeader.Length];
			header = new byte[message.Header.Length];
			payload = new byte[message.Payload.Length];

			Array.Copy(message.TcpIpHeader, tcpIpHeader, tcpIpHeader.Length);
			Array.Copy(message.Header, header, header.Length);
			Array.Copy(message.Payload, payload, payload.Length);

			packetSize = tcpIpHeader.Length + header.Length + payload.Length + 2; // 2 is for slashes separator.
		}

		public void RandomizeValues()
		{
			Random random = new Random();

			baseModifier.ExtractSignatureAndDataFromPayload(header, payload, out string payloadDataStr, out string signature);

			ushort[] values = new ushort[payloadDataStr.Count(x => x.Equals(',')) + 1];
			values = values.Select(x => (ushort)random.Next(0, 32768)).ToArray();
			int.TryParse(payloadDataStr.Split(';')[1], out int points);
			int newStartAddress = random.Next(0, 65536 - points);
			string newPayloadDataStr = payloadDataStr.Substring(0, payloadDataStr.LastIndexOf(';') + 1) + string.Join(',', values);

			baseModifier.RecalculateLengthInHeader(newPayloadDataStr, ref header);
			payload = Encoding.UTF8.GetBytes(newPayloadDataStr + signature);
		}
	}
}
