using System.Runtime.InteropServices;

namespace MySharpDivert.Native
{
	[StructLayout(LayoutKind.Explicit)]
	public struct UnionData
	{
		[FieldOffset(0)]
		public WinDivertDataNetwork Network;

		[FieldOffset(0)]
		public WinDivertDataFlow Flow;

		[FieldOffset(0)]
		public WinDivertDataSocket Socket;

		[FieldOffset(0)]
		public WinDivertDataReflect Reflect;
	}
}
