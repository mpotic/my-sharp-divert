﻿using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace MySharpDivert.Native.Structs.IPStructs
{
	/// <summary>
	/// Represents an IPV4 header.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct IPv4Header
	{
		/// HdrLength : 4
		/// Version : 4
		private byte bitvector1;

		/// <summary>
		/// Gets or sets the type of service.
		/// </summary>
		public byte TOS;

		/// <summary>
		/// Gets or sets the header length.
		/// </summary>
		public ushort Length;

		/// <summary>
		/// Gets or sets the ID.
		/// </summary>
		public ushort Id;

		/// <summary>
		/// Private member for <see cref="FragOff"/>, <see cref="Mf"/>, <see cref="Df"/> and <see cref="Reserved"/>
		/// </summary>
		private ushort FragOff0;

		/// <summary>
		/// Gets or sets the TTL.
		/// </summary>
		public byte TTL;

		/// <summary>
		/// Gets or sets the protocol.
		/// </summary>
		public byte Protocol;

		/// <summary>
		/// Gets or sets the checksum.
		/// </summary>
		public ushort Checksum;

		/// <summary>
		/// Private member for <see cref="SrcAddr"/>
		/// </summary>
		private uint _srcAddr;

		/// <summary>
		/// Gets or sets the source IP address.
		/// </summary>
		/// <exception cref="ArgumentException">
		/// When setting, if the supplied address is a not a valid IPv4 address, the setter will throw.
		/// </exception>
		public IPAddress SrcAddr
		{
			get
			{
				return new IPAddress(unchecked((long)this._srcAddr));
			}

			set
			{
				Debug.Assert(value.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork, "Not a valid IPV4 address.");

				if (value.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
				{
					throw new ArgumentException("Not a valid IPV4 address.", nameof(SrcAddr));
				}

				this._srcAddr = (uint)BitConverter.ToInt32(value.GetAddressBytes(), 0);
			}
		}

		/// <summary>
		/// Private member for <see cref="DstAddr"/>
		/// </summary>
		private uint _dstAddr;

		/// <summary>
		/// Gets or sets the destination IP address.
		/// </summary>
		/// <exception cref="ArgumentException">
		/// When setting, if the supplied address is a not a valid IPv4 address, the setter will throw.
		/// </exception>
		public IPAddress DstAddr
		{
			get
			{
				return new IPAddress(unchecked((long)this._dstAddr));
			}

			set
			{
				Debug.Assert(value.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork, "Not a valid IPV4 address.");

				if (value.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
				{
					throw new ArgumentException("Not a valid IPV4 address.", nameof(DstAddr));
				}

				this._dstAddr = (uint)BitConverter.ToInt32(value.GetAddressBytes(), 0);
			}
		}

		/// <summary>
		/// Gets or sets the header length.
		/// </summary>
		public byte HdrLength
		{
			get
			{
				return ((byte)((this.bitvector1 & 15u)));
			}
			set
			{
				this.bitvector1 = ((byte)((value | this.bitvector1)));
			}
		}

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		public byte Version
		{
			get
			{
				return ((byte)(((this.bitvector1 & 240u)
							/ 16)));
			}
			set
			{
				this.bitvector1 = ((byte)(((value * 16)
							| this.bitvector1)));
			}
		}

		/// <summary>
		/// Gets or sets the fragment offset for the given ipv4 header.
		/// </summary>
		public ushort FragOff
		{
			get
			{
				return (ushort)((this.FragOff0) & 0xFF1F);
			}

			set
			{
				this.FragOff0 = (ushort)(((this.FragOff0) & 0x00E0) | ((value) & 0xFF1F));
			}
		}

		/// <summary>
		/// Gets or sets whether or not given ipv4 header has the more fragments flag set.
		/// </summary>
		public bool Mf
		{
			get
			{
				return (ushort)((this.FragOff0) & 0x0020) != 0;
			}

			set
			{
				this.FragOff0 = (ushort)(((this.FragOff0) & 0xFFDF) | (((value ? 1 : 0) & 0x0001) << 5));
			}
		}

		/// <summary>
		/// Gets or sets whether or not given ipv4 header has the don't fragment flag set.
		/// </summary>
		public bool Df
		{
			get
			{
				return (ushort)((this.FragOff0) & 0x0040) != 0;
			}

			set
			{
				this.FragOff0 = (ushort)(((this.FragOff0) & 0xFFBF) | (((value ? 1 : 0) & 0x0001) << 6));
			}
		}

		/// <summary>
		/// Gets or sets whether or not given ipv4 header has the reserved flag set.
		/// </summary>
		public bool Reserved
		{
			get
			{
				return (ushort)((this.FragOff0) & 0x0080) != 0;
			}

			set
			{
				this.FragOff0 = (ushort)(((this.FragOff0) & 0xFF7F) | (((value ? 1 : 0) & 0x0001) << 7));
			}
		}
	}
}
