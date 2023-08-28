using System;
using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	[StructLayout(LayoutKind.Explicit)]
	public struct WinDivertAddress
	{
		[FieldOffset(0)]
		public Int64 Timestamp;

		[FieldOffset(8)]
		private readonly UInt64 _layerEventSniffedOutboundLoopbackImpostorIPv6IPChecksumTCPChecksumUDPChecksum;

		[FieldOffset(16)]
		public UnionData Union;

		// Bitfield accessors for the _layerEventSniffedOutboundLoopbackImpostorIPv6IPChecksumTCPChecksumUDPChecksum field.

		public Byte Layer
		{
			get { return (Byte)(_layerEventSniffedOutboundLoopbackImpostorIPv6IPChecksumTCPChecksumUDPChecksum & 0xFF); }
		}

		public Byte Event
		{
			get { return (Byte)((_layerEventSniffedOutboundLoopbackImpostorIPv6IPChecksumTCPChecksumUDPChecksum >> 8) & 0xFF); }
		}

		public Boolean Sniffed
		{
			get { return ((_layerEventSniffedOutboundLoopbackImpostorIPv6IPChecksumTCPChecksumUDPChecksum >> 16) & 0x1) == 1; }
		}

		public Boolean Outbound
		{
			get { return ((_layerEventSniffedOutboundLoopbackImpostorIPv6IPChecksumTCPChecksumUDPChecksum >> 17) & 0x1) == 1; }
		}

		public Boolean Loopback
		{
			get { return ((_layerEventSniffedOutboundLoopbackImpostorIPv6IPChecksumTCPChecksumUDPChecksum >> 18) & 0x1) == 1; }
		}

		public Boolean Impostor
		{
			get { return ((_layerEventSniffedOutboundLoopbackImpostorIPv6IPChecksumTCPChecksumUDPChecksum >> 19) & 0x1) == 1; }
		}

		public Boolean IPv6
		{
			get { return ((_layerEventSniffedOutboundLoopbackImpostorIPv6IPChecksumTCPChecksumUDPChecksum >> 20) & 0x1) == 1; }
		}

		public Boolean IPChecksum
		{
			get { return ((_layerEventSniffedOutboundLoopbackImpostorIPv6IPChecksumTCPChecksumUDPChecksum >> 21) & 0x1) == 1; }
		}

		public Boolean TCPChecksum
		{
			get { return ((_layerEventSniffedOutboundLoopbackImpostorIPv6IPChecksumTCPChecksumUDPChecksum >> 22) & 0x1) == 1; }
		}

		public Boolean UDPChecksum
		{
			get { return ((_layerEventSniffedOutboundLoopbackImpostorIPv6IPChecksumTCPChecksumUDPChecksum >> 23) & 0x1) == 1; }
		}
	}
	//[StructLayout(LayoutKind.Sequential)]
	//public struct WinDivertAddress
	//{
	//	public long Timestamp;

	//	public byte Layer;

	//	public byte Event;

	//	public bool Sniffed;

	//	public bool Outbound;

	//	public bool Loopback;

	//	public bool Impostor;

	//	public bool IPv6;

	//	public bool IPChecksum;

	//	public bool TCPChecksum;

	//	public bool UDPChecksum;

	//	public UnionData Union;

	//	public override string ToString()
	//	{
	//		return $"WinDivertAddress:\n" +
	//		   $"  Timestamp: {Timestamp}\n" +
	//		   $"  Layer: {Layer}\n" +
	//		   $"  Event: {Event}\n" +
	//		   $"  Sniffed: {Sniffed}\n" +
	//		   $"  Outbound: {Outbound}\n" +
	//		   $"  Loopback: {Loopback}\n" +
	//		   $"  Impostor: {Impostor}\n" +
	//		   $"  IPv6: {IPv6}\n" +
	//		   $"  IPChecksum: {IPChecksum}\n" +
	//		   $"  TCPChecksum: {TCPChecksum}\n" +
	//		   $"  UDPChecksum: {UDPChecksum}\n";
	//	}
	//}
}
