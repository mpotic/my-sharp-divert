namespace MySharpDivert
{
	/// <summary>
	/// Unfinished.
	/// </summary>
	public class IPv6Header
	{
		internal IPv6Header(Native.IPv6Header header)
		{
			Version = (byte)(header.bitvector & 0b_1111_0000_0000_0000);
		}

		public byte Version; // 8 bits.

		public byte TrafficClass; // 4 bits.

		public ushort FlowLabel; // 20 bits.

		public ushort Length;

		public byte NextHdr;

		public byte HopLimit;

		private uint[] SrcAddress; // 16 bytes. In big endian format (network format).

		private uint[] DstAddress; // 16 bytes. In big endian format (network format).
	}
}
