using System;
using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	internal class WinDivertImports
	{
		const string WIN_DIVERT_PATH = "WinDivert.dll";

		[DllImport(WIN_DIVERT_PATH, EntryPoint = "WinDivertOpen", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern IntPtr WinDivertOpen(
			[In()][MarshalAs(UnmanagedType.LPStr)] string filter,
			WinDivertLayer layer,
			short priority,
			ulong flags
		);

		[DllImport(WIN_DIVERT_PATH, EntryPoint = "WinDivertRecv", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool WinDivertRecv(
			[In()] IntPtr handle, 
			IntPtr pPacket,
			uint packetLen,
			ref uint readLen,
			[In()] ref WinDivertAddress pAddr
		);

		[DllImport(WIN_DIVERT_PATH, EntryPoint = "WinDivertSend", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool WinDivertSend(
			[In()] IntPtr handle,
			[In()] IntPtr pPacket,
			uint packetLen,
			[In()] ref WinDivertAddress pAddr,
			ref uint writeLen
		);

		[DllImport(WIN_DIVERT_PATH, EntryPoint = "WinDivertClose", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool WinDivertClose([In()] IntPtr handle);

		[DllImport(WIN_DIVERT_PATH, EntryPoint = "WinDivertHelperParsePacket", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool WinDivertHelperParsePacket(
			[In()] IntPtr pPacket,
			uint packetLen,
			ref IntPtr ppIpHdr,       // Pointer to IPv4Header*
			ref IntPtr ppIpv6Hdr,     // Pointer to IPv6Header*
			ref IntPtr ppIcmpHdr,     // Pointer to IcmpV4Header*
			ref IntPtr ppIcmpv6Hdr,   // Pointer to IcmpV6Header*
			ref IntPtr ppTcpHdr,      // Pointer to TcpHeader*
			ref IntPtr ppUdpHdr,      // Pointer to UdpHeader*
			ref IntPtr ppData,        // Pointer to byte*
			ref uint pDataLen
		);

		[DllImport(WIN_DIVERT_PATH, EntryPoint = "WinDivertHelperCalcChecksums", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern uint WinDivertHelperCalcChecksums(
			IntPtr pPacket,
			uint packetLen,
			[In()] ref WinDivertAddress pAddr,
			ulong flags
		);
	}
}
