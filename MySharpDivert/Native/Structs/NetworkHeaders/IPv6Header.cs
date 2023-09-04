using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	/// <summary>
	/// Represents an IPV6 header.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct IPv6Header
	{
		internal ushort bitvector;

		internal ushort flowLabel;

		internal ushort length;

		internal byte nextHdr;

		internal byte hopLimit;

		internal uint _srcAddrA;
		internal uint _srcAddrB;
		internal uint _srcAddrC;
		internal uint _srcAddrD;

		internal uint _dstAddrA;
		internal uint _dstAddrB;
		internal uint _dstAddrC;
		internal uint _dstAddrD;
	}
}
