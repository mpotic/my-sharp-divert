using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	[StructLayout(LayoutKind.Sequential)]
	public struct WinDivertAddress
	{
		public long Timestamp;

		public ulong Layer;

		public ulong Event;

		public ulong Sniffed;

		public ulong Outbound;

		public ulong Loopback;

		public ulong Impostor;

		public ulong IPv6;

		public ulong IPChecksum;

		public ulong TCPChecksum;

		public ulong UDPChecksum;

		public UnionData Union;
	}
}
