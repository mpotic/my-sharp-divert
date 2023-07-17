using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	[StructLayout(LayoutKind.Sequential)]
	public struct WinDivertDataReflect
	{
		public long Timestamp;

		public uint ProcessId;

		public WinDivertLayer Layer;

		public ulong Flags;

		public short Priority;
	}
}
