using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	/// <summary>
	/// Represents an IPV4 header.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct IPv4Header
	{
		internal byte bitvector;

		internal byte TOS;

		internal ushort Length;

		internal ushort Id;

		internal ushort FragOff0;

		internal byte TTL;

		internal byte Protocol;

		internal ushort Checksum;

		internal uint SrcAddr;

		internal uint DstAddr;
	}
}
