using System;
using System.Collections;
using System.Runtime.InteropServices;
using MySharpDivert.Native;

namespace MySharpDivert
{
	internal class SharpDivert : ISharpDivert
	{
		private IntPtr handle;

		private SharpDivertHelper helper = new SharpDivertHelper();

		public IResponse Open(string filter)
		{
			IResponse response;

			try
			{
				handle = WinDivertImports.WinDivertOpen(filter, WinDivertLayer.Network, (short)0, (ulong)0);
			}
			catch (Exception e)
			{
				response = new Response(false, e.Message);

				return response;
			}

			if (handle == IntPtr.Zero || handle == new IntPtr(-1))
			{
				response = new Response(false, helper.GetLastErrorMessage());

				return response;
			}

			response = new Response(true);

			return response;
		}

		public IReceiveResponse ReceivePacket()
		{
			IReceiveResponse response;
			bool isSuccessful;
			uint allowedPacketLen = 8192;
			IntPtr packetBuffer = Marshal.AllocHGlobal((int)allowedPacketLen);
			uint receivedPacketLength = 0;
			WinDivertAddress address = new WinDivertAddress();

			try
			{
				isSuccessful = WinDivertImports.WinDivertRecv(
					handle,
					packetBuffer,
					allowedPacketLen,
					ref receivedPacketLength,
					ref address
				);
			}
			catch (Exception e)
			{
				response = new ReceiveResponse(false, e.Message);

				return response;
			}

			if (!isSuccessful)
			{
				response = new ReceiveResponse(isSuccessful, helper.GetLastErrorMessage());

				return response;
			}

			byte[] packet = helper.GetPacket(packetBuffer, receivedPacketLength);
			response = new ReceiveResponse(packet);
			Marshal.FreeHGlobal(packetBuffer);

			return response;
		}

		public void CloseHandle()
		{
			if (handle != IntPtr.Zero && handle != new IntPtr(-1))
			{
				WinDivertImports.WinDivertClose(handle);
			}
		}

		public IResponse SendPacket(byte[] packet, WinDivertAddress address)
		{
			IResponse response;
			bool isSuccessful;
			IntPtr packetPtr = Marshal.AllocHGlobal(packet.Length);
			Marshal.Copy(packet, 0, packetPtr, packet.Length);
			uint writeLength = 0;

			try
			{
				isSuccessful = WinDivertImports.WinDivertSend(
					handle,
					packetPtr,
					(uint)packet.Length,
					ref address,
					ref writeLength
				);
			}
			catch (Exception e)
			{
				response = new Response(false, e.Message);

				return response;
			}

			if (!isSuccessful)
			{
				response = new Response(isSuccessful, helper.GetLastErrorMessage());

				return response;
			}

			response = new Response(isSuccessful);
			Marshal.FreeHGlobal(packetPtr);

			return response;
		}

		~SharpDivert()
		{
			CloseHandle();
		}
	}
}
