using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	/// <summary>
	/// Represents a UDP header.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct UdpHeader
	{
		internal ushort SrcPort;

		internal ushort DstPort;

		internal ushort Length;

		internal ushort Checksum;
	}
}
