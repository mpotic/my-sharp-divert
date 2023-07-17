using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	[StructLayout(LayoutKind.Sequential)]
	public struct WinDivertPacket
	{
		public ushort Timestamp;

		public ushort Layer;

		public ushort Priority;

		public ushort Flags;

		public uint IfIdx;

		public uint SubIfIdx;

		public uint Timestamp64;

		public uint IPv4HdrChecksum;

		public uint IPv6HdrChecksum;
	}
}
