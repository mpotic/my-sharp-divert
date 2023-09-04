using System.Buffers.Binary;

namespace MySharpDivert
{
	public class IcmpV6Header
	{
		internal IcmpV6Header(Native.IcmpV6Header header)
		{
			Type = header.type;
			Code = header.code;
			Checksum = BinaryPrimitives.ReverseEndianness(header.checksum);
			Body = BinaryPrimitives.ReverseEndianness(header.body);
		}

		public byte Type { get; set; }

		public byte Code { get; set; }

		public ushort Checksum { get; set; }

		public uint Body { get; set; }
	}
}
