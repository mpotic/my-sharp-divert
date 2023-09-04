using System.Buffers.Binary;
using System.Net;

namespace MySharpDivert
{
	public class IPv4Header
	{    
		internal IPv4Header(Native.IPv4Header header) 
		{
			HeaderLength = (byte)(header.bitvector & 15);
			Version = (byte)((header.bitvector >> 4) & 15);
			TOS = header.TOS;
			Length = BinaryPrimitives.ReverseEndianness(header.Length);
			Id = BinaryPrimitives.ReverseEndianness(header.Id);
			FragOff0 = BinaryPrimitives.ReverseEndianness(header.FragOff0);
			TTL = header.TTL;
			Protocol = header.Protocol;
			Checksum = BinaryPrimitives.ReverseEndianness(header.Checksum);
			SrcAddr = new IPAddress(header.SrcAddr);
			DstAddr = new IPAddress(header.DstAddr);
		}

		public byte HeaderLength { get; set; }

		public byte Version { get; set; }

		public byte TOS { get; set; }

		public ushort Length { get; set; }

		public ushort Id { get; set; }

		public ushort FragOff0 { get; set; }

		public byte TTL { get; set; }

		public byte Protocol { get; set; }

		public ushort Checksum { get; set; }

		private IPAddress SrcAddr { get; set; }

		private IPAddress DstAddr { get; set; }

		public override string ToString()
		{
			string retVal = "";
			retVal += "- - - - - - - - - IPv4 Header - - - - - - - - -\n";
			retVal += $"Source address: {SrcAddr}\n";
			retVal += $"Destination address: {DstAddr}\n";
			retVal += $"Checksum: {Checksum}\n";

			return retVal;
		}
	}
}
