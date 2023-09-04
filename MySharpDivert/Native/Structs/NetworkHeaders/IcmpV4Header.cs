using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	/// <summary>
	/// Represents an IPv4 Icmp header.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct IcmpV4Header
	{
		internal byte type;

		internal byte code;

		internal ushort checksum;

		internal uint body;
	}
}
