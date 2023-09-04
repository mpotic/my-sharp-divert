using MySharpDivert.Native.Enums;
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
			[Out()][Optional] IntPtr pPacket,
			[In()] uint packetLen,
			[Out()][Optional] out uint readLen,
			[Out()][Optional] out WinDivertAddress pAddr
		);

		[DllImport(WIN_DIVERT_PATH, EntryPoint = "WinDivertSend", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool WinDivertSend(
			[In()] IntPtr handle,
			[In()] IntPtr pPacket,
			[In()] uint packetLen,
			[Out()][Optional] out uint sendLen,
			[In()] ref WinDivertAddress pAddr
		);

		[DllImport(WIN_DIVERT_PATH, EntryPoint = "WinDivertShutdown", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool WinDivertShutdown([In()] IntPtr handle, [In()] WinDivertShutdown how);

		[DllImport(WIN_DIVERT_PATH, EntryPoint = "WinDivertClose", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool WinDivertClose([In()] IntPtr handle);

		[DllImport(WIN_DIVERT_PATH, EntryPoint = "WinDivertHelperParsePacket", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool WinDivertHelperParsePacket(
			[In()] IntPtr pPacket,
			[In()] uint packetLen,
			[Optional][Out()] out IntPtr ppIpHdr,
			[Optional][Out()] out IntPtr ppIpv6Hdr,
			[Optional][Out()] out byte protocol,
			[Optional][Out()] out IntPtr ppIcmpHdr,
			[Optional][Out()] out IntPtr ppIcmpv6Hdr,
			[Optional][Out()] out IntPtr ppTcpHdr,
			[Optional][Out()] out IntPtr ppUdpHdr,
			[Optional][Out()] out IntPtr ppData,
			[Optional][Out()] out uint pDataLen,
			[Optional][Out()] out IntPtr ppNext,
			[Optional][Out()] out uint pNextLen
		);

		[DllImport(WIN_DIVERT_PATH, EntryPoint = "WinDivertHelperCalcChecksums", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool WinDivertHelperCalcChecksums(
			[In()][Out()] IntPtr pPacket,
			[In()] uint packetLen,
			[Optional][Out()] out WinDivertAddress pAddr,
			[In()] ulong flags
		);
	}
}
