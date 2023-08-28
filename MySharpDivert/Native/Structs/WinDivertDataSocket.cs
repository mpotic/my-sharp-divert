using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	[StructLayout(LayoutKind.Sequential)]
	public struct WinDivertDataSocket
	{
		public ulong Endpoint;

		public ulong ParentEndpoint;

		public uint ProcessId;

		public uint LocalAddr_0;

		public uint LocalAddr_1;

		public uint LocalAddr_2;

		public uint LocalAddr_3;

		public uint RemoteAddr_0;

		public uint RemoteAddr_1;

		public uint RemoteAddr_2;

		public uint RemoteAddr_3;

		public ushort LocalPort;

		public ushort RemotePort;

		public byte Protocol;
	}
}
