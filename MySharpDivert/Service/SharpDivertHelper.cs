using System;
using System.ComponentModel;
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
	}
}
