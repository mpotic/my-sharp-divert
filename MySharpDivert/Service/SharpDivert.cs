using System;
using System.Runtime.InteropServices;
using MySharpDivert.Native;
using MySharpDivert.Native.Enums;

namespace MySharpDivert
{
	internal class SharpDivert : ISharpDivert
	{
		private IntPtr handle;

		private SharpDivertHelper helper = new SharpDivertHelper();

		public IResponse Open(string filter)
		{
			try
			{
				handle = WinDivertImports.WinDivertOpen(filter, WinDivertLayer.WINDIVERT_LAYER_NETWORK, (short)0, (ulong)0);
			}
			catch (Exception e)
			{
				return new Response(false, e.Message);
			}

			if (handle == IntPtr.Zero || handle == new IntPtr(-1))
			{
				return new Response(false, helper.GetLastErrorMessage());
			}

			return new Response(true);
		}

		public PacketResponse ReceivePacket()
		{
			PacketResponse response;
			bool isSuccessful;
			uint allowedPacketLen = 8192;
			IntPtr packetBuffer = Marshal.AllocHGlobal((int)allowedPacketLen);
			uint receivedPacketLength;
			WinDivertAddress address;

			try
			{
				isSuccessful = WinDivertImports.WinDivertRecv(
					handle,
					packetBuffer,
					allowedPacketLen,
					out receivedPacketLength,
					out address
				);

				byte[] packet = helper.GetPacket(packetBuffer, receivedPacketLength);
				HeadersData headers = helper.GetHeadersFromPacket(packetBuffer, receivedPacketLength);
				response = new PacketResponse(packet, headers, address);
			}
			catch (Exception e)
			{
				response = new PacketResponse(false, e.Message);

				return response;
			}

			if (!isSuccessful)
			{
				response = new PacketResponse(isSuccessful, helper.GetLastErrorMessage());

				return response;
			}

			Marshal.FreeHGlobal(packetBuffer);

			return response;
		}

		public IResponse SendPacket(byte[] packet, WinDivertAddress address)
		{
			if (packet == null || packet.Length < 0)
			{
				return new Response(false, "Packet can't be empty!");
			}

			IResponse response;
			bool isSuccessful;
			IntPtr packetPtr = Marshal.AllocHGlobal(packet.Length);
			Marshal.Copy(packet, 0, packetPtr, packet.Length);
			uint writeLength;

			try
			{
				isSuccessful = WinDivertImports.WinDivertSend(
					handle,
					packetPtr,
					(uint)packet.Length,
					out writeLength,
					ref address
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

		public IResponse ShutdownHandle()
		{
			if (handle == IntPtr.Zero || handle == new IntPtr(-1))
			{
				return new Response(false, "Handle is not open!");
			}

			bool shutdown = WinDivertImports.WinDivertShutdown(handle, WinDivertShutdown.WINDIVERT_SHUTDOWN_BOTH);
			if (!shutdown)
			{
				return new Response(false, helper.GetLastErrorMessage());
			}

			return new Response(true);
		}

		public IResponse CloseHandle()
		{
			if (handle == IntPtr.Zero || handle == new IntPtr(-1))
			{
				return new Response(false, "Handle is not open!");
			}

			bool close = WinDivertImports.WinDivertClose(handle);
			if (!close)
			{
				return new Response(false, helper.GetLastErrorMessage());
			}

			return new Response(true);
		}

		public IPacketResponse CalculateChecksum(byte[] packet, WinDivertAddress address, ChecksumCalcFlags flags)
		{
			IPacketResponse response;

			if (packet == null || packet.Length < 0)
			{
				return new PacketResponse(false, "Packet can't be empty!");
			}

			try
			{
				IntPtr packetPtr = Marshal.AllocHGlobal(packet.Length);
				Marshal.Copy(packet, 0, packetPtr, packet.Length);

				if (!WinDivertImports.WinDivertHelperCalcChecksums(packetPtr, (uint)packet.Length, out address, (ulong)flags))
				{
					return new PacketResponse(false, "SharpDivert couldn't calculate checksum! " + helper.GetLastErrorMessage());
				}

				packet = helper.GetPacket(packetPtr, (uint)packet.Length);
				HeadersData headers = helper.GetHeadersFromPacket(packetPtr, (uint)packet.Length);
				response = new PacketResponse(packet, headers, address);
				Marshal.FreeHGlobal(packetPtr);
			}
			catch (Exception e)
			{
				return new PacketResponse(false, "SharpDivert couldn't calculate checksum! " + e.Message);
			}

			return response;
		}

		~SharpDivert()
		{
			ShutdownHandle();
			CloseHandle();
		}
	}
}
