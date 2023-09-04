using MySharpDivert.Native;
using System;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;

namespace MySharpDivert
{
	internal class SharpDivertHelper
	{
		internal byte[] GetPacket(IntPtr packetPtr, uint packetLength)
		{
			byte[] packetData = new byte[packetLength];
			Marshal.Copy(packetPtr, packetData, 0, (int)packetLength);

			return packetData;
		}

		internal byte[] GetPacket(IntPtr packetPtr, IntPtr packetLength)
		{
			uint length = (uint)Marshal.ReadInt32(packetPtr);

			return GetPacket(packetPtr, length);
		}

		internal string GetLastErrorMessage()
		{
			var exceptionHandler = new Win32Exception(Marshal.GetLastWin32Error());
				
			return exceptionHandler.Message;
		}

		internal HeadersData GetHeadersFromPacket(IntPtr pPacket, uint packetLen)
		{
			HeadersData headersData = new HeadersData();

			IntPtr ppIpHdr, ppIpv6Hdr, ppIcmpHdr, ppIcmpv6Hdr, ppTcpHdr, ppUdpHdr, ppData, ppNext;
			byte protocol;
			uint pDataLen, pNextLen;

			bool result = WinDivertImports.WinDivertHelperParsePacket(pPacket, packetLen,
													 out ppIpHdr, out ppIpv6Hdr, out protocol,
													 out ppIcmpHdr, out ppIcmpv6Hdr,
													 out ppTcpHdr, out ppUdpHdr, out ppData,
													 out pDataLen, out ppNext, out pNextLen);

			if (!result)
			{
				return headersData;
			}

			if (ppIcmpHdr != IntPtr.Zero)
			{
				Native.IcmpV4Header _ICMPV4Header = Marshal.PtrToStructure<Native.IcmpV4Header>(ppIcmpHdr);
				headersData.IcmpV4HeaderData = new IcmpV4Header(_ICMPV4Header);
			}

			if (ppIcmpv6Hdr != IntPtr.Zero)
			{
				Native.IcmpV6Header _ICMPV6Header = Marshal.PtrToStructure<Native.IcmpV6Header>(ppIcmpv6Hdr);
				headersData.IcmpV6HeaderData = new IcmpV6Header(_ICMPV6Header);
			}

			if (ppIpHdr != IntPtr.Zero)
			{
				Native.IPv4Header _IPv4Header = Marshal.PtrToStructure<Native.IPv4Header>(ppIpHdr);
				headersData.IPv4HeaderData = new IPv4Header(_IPv4Header); 
			}

			if (ppIpv6Hdr != IntPtr.Zero)
			{
				Native.IPv6Header _IPv6Header  = Marshal.PtrToStructure<Native.IPv6Header>(ppIpv6Hdr);
				headersData.IPv6HeaderData = new IPv6Header(_IPv6Header);
			}

			if (ppTcpHdr != IntPtr.Zero)
			{
				Native.TcpHeader _TCPHeader = Marshal.PtrToStructure<Native.TcpHeader>(ppTcpHdr);
				headersData.TcpHeaderData = new TcpHeader(_TCPHeader);
			}

			if (ppUdpHdr != IntPtr.Zero)
			{
				Native.UdpHeader _UDPHeader = Marshal.PtrToStructure<Native.UdpHeader>(ppUdpHdr);
				headersData.UdpHeaderData = new UdpHeader(_UDPHeader);
			}

			headersData.PacketDataLength = pDataLen;

			return headersData;
		}
	}
}
