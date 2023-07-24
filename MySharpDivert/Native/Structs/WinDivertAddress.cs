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

		public override string ToString()
		{
			return $"WinDivertAddress:\n" +
			   $"  Timestamp: {Timestamp}\n" +
			   $"  Layer: {Layer}\n" +
			   $"  Event: {Event}\n" +
			   $"  Sniffed: {Sniffed}\n" +
			   $"  Outbound: {Outbound}\n" +
			   $"  Loopback: {Loopback}\n" +
			   $"  Impostor: {Impostor}\n" +
			   $"  IPv6: {IPv6}\n" +
			   $"  IPChecksum: {IPChecksum}\n" +
			   $"  TCPChecksum: {TCPChecksum}\n" +
			   $"  UDPChecksum: {UDPChecksum}\n";
		}
	}
}
