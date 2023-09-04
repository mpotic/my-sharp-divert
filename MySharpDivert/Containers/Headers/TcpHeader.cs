using System.Buffers.Binary;

namespace MySharpDivert
{
	public class TcpHeader
	{
		internal TcpHeader(Native.TcpHeader header)
		{
			SrcPort = BinaryPrimitives.ReverseEndianness(header.srcPort);
			DstPort = BinaryPrimitives.ReverseEndianness(header.dstPort);
			SeqNum = BinaryPrimitives.ReverseEndianness(header.seqNum);
			AckNum = BinaryPrimitives.ReverseEndianness(header.ackNum);
			HeaderSize = (byte)((header.reservedAndHdrLength >> 4) & 15);
			Reserved = (byte)(header.reservedAndHdrLength & 15);
			FlagsAndReserved = header.flagsAndReserved;
			Window = BinaryPrimitives.ReverseEndianness(header.window);
			Checksum = BinaryPrimitives.ReverseEndianness(header.checksum);
			UrgPtr = header.urgPtr;
		}

		private ushort SrcPort { get; set; }

		private ushort DstPort { get; set; }

		private uint SeqNum { get; set; }

		private uint AckNum { get; set; }

		private byte HeaderSize {get; set;} // 4 bits.

		private byte Reserved { get; set; } // 4 bits;

		private byte FlagsAndReserved { get; set; }

		public ushort Window { get; set; }

		public ushort Checksum { get; set; }

		public ushort UrgPtr { get; set; }

		public override string ToString()
		{
			string retVal = "";
			retVal += "- - - - - - - - - TCP Header - - - - - - - - -\n";
			retVal += $"Source port: {SrcPort}\n";
			retVal += $"Destination port: {DstPort}\n";
			retVal += $"Checksum: {Checksum}\n";

			return retVal;
		}
	}
}
