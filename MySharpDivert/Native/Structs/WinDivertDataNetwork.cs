using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	[StructLayout(LayoutKind.Sequential)]
	public struct WinDivertDataNetwork
	{
		public uint IfIdx;

		public uint SubIfIdx;
	}
}
