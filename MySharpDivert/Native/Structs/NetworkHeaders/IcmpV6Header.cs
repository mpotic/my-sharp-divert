using System.Runtime.InteropServices;

namespace MySharpDivert.Native.Structs.IPStructs
{
	/// <summary>
	/// Represents an IPv6 Icmp header.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct IcmpV6Header
	{
		/// <summary>
		/// Gets or sets the ICMP type.
		/// </summary>
		public byte Type;

		/// <summary>
		/// Gets or sets the ICMP subtype.
		/// </summary>
		public byte Code;

		/// <summary>
		/// Gets or sets the checksum.
		/// </summary>
		public ushort Checksum;

		/// <summary>
		/// Gets or sets the body.
		/// </summary>
		public uint Body;
	}
}
