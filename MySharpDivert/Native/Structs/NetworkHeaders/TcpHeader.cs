using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	/// <summary>
	/// Represents a TCP header.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct TcpHeader
	{
		internal ushort srcPort;

		internal ushort dstPort;

		internal uint seqNum;

		internal uint ackNum;

		internal byte reservedAndHdrLength;

		internal byte flagsAndReserved;

		internal ushort window;

		internal ushort checksum;

		internal ushort urgPtr;
	}
}
