using System.Runtime.InteropServices;

namespace MySharpDivert.Native.Structs.NetworkHeaders
{
	/// <summary>
	/// Represents a UDP header.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct UdpHeader
	{
		/// <summary>
		/// Gets or sets the source port.
		/// </summary>
		public ushort SrcPort;

		/// <summary>
		/// Gets or sets the destination port.
		/// </summary>
		public ushort DstPort;

		/// <summary>
		/// Gets or sets the length.
		/// </summary>
		public ushort Length;

		/// <summary>
		/// Gets or sets the checksum.
		/// </summary>
		public ushort Checksum;
	}
}
