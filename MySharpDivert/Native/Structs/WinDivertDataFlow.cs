using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	[StructLayout(LayoutKind.Sequential)]
	public struct WinDivertDataFlow
	{
		public ulong Endpoint;

		public ulong ParentEndpoint;

		public uint ProcessId;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]

		public uint[] LocalAddr;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]

		public uint[] RemoteAddr;

		public ushort LocalPort;

		public ushort RemotePort;

		public byte Protocol;
	}
}
