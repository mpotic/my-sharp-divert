﻿using System.Runtime.InteropServices;

namespace MySharpDivert.Native.Structs.IPStructs
{
	/// <summary>
	/// Represents a TCP header.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct TcpHeader
	{
		/// <summary>
		/// Gets or sets the source port.
		/// </summary>
		public ushort SrcPort;

		/// <summary>
		/// Gets or sets the destination port.
		/// </summary>
		public ushort DstPort;

		/// <summary>
		/// Gets or sets the sequence number.
		/// </summary>
		public uint SeqNum;

		/// <summary>
		/// Gets or sets the acknowledgement number.
		/// </summary>
		public uint AckNum;

		/// Reserved1 : 4
		/// HdrLength : 4
		/// Fin : 1
		/// Syn : 1
		/// Rst : 1
		/// Psh : 1
		/// Ack : 1
		/// Urg : 1
		/// Reserved2 : 2
		private ushort bitvector1;

		/// <summary>
		/// Gets or sets the window.
		/// </summary>
		public ushort Window;

		/// <summary>
		/// Gets or sets the checksum.
		/// </summary>
		public ushort Checksum;

		/// <summary>
		/// Gets or sets the urgent pointer.
		/// </summary>
		public ushort UrgPtr;

		/// <summary>
		/// Gets or sets the first reserved bits.
		/// </summary>
		/// <remarks>
		/// Should always be zero.
		/// </remarks>
		public ushort Reserved1
		{
			get
			{
				return ((ushort)((this.bitvector1 & 15u)));
			}
			set
			{
				this.bitvector1 = ((ushort)((value | this.bitvector1)));
			}
		}

		/// <summary>
		/// Gets or sets the header length.
		/// </summary>
		public ushort HdrLength
		{
			get
			{
				return ((ushort)(((this.bitvector1 & 240u)
							/ 16)));
			}
			set
			{
				this.bitvector1 = ((ushort)(((value * 16)
							| this.bitvector1)));
			}
		}

		/// <summary>
		/// Gets or sets the fin flag.
		/// </summary>
		public uint Fin
		{
			get
			{
				return ((ushort)(((this.bitvector1 & 256u)
							/ 256)));
			}
			set
			{
				this.bitvector1 = ((ushort)(((value * 256)
							| this.bitvector1)));
			}
		}

		/// <summary>
		/// Gets or sets the syn flag.
		/// </summary>
		public uint Syn
		{
			get
			{
				return ((ushort)(((this.bitvector1 & 512u)
							/ 512)));
			}
			set
			{
				this.bitvector1 = ((ushort)(((value * 512)
							| this.bitvector1)));
			}
		}

		/// <summary>
		/// Gets or sets the reset flag.
		/// </summary>
		public uint Rst
		{
			get
			{
				return ((ushort)(((this.bitvector1 & 1024u)
							/ 1024)));
			}
			set
			{
				this.bitvector1 = ((ushort)(((value * 1024)
							| this.bitvector1)));
			}
		}

		/// <summary>
		/// Gets or sets the push flag.
		/// </summary>
		public uint Psh
		{
			get
			{
				return ((ushort)(((this.bitvector1 & 2048u)
							/ 2048)));
			}
			set
			{
				this.bitvector1 = ((ushort)(((value * 2048)
							| this.bitvector1)));
			}
		}

		/// <summary>
		/// Gets or sets the acknowledgement flag.
		/// </summary>
		public uint Ack
		{
			get
			{
				return ((ushort)(((this.bitvector1 & 4096u)
							/ 4096)));
			}
			set
			{
				this.bitvector1 = ((ushort)(((value * 4096)
							| this.bitvector1)));
			}
		}

		/// <summary>
		/// Gets or sets the urgent pointer flag.
		/// </summary>
		public uint Urg
		{
			get
			{
				return ((ushort)(((this.bitvector1 & 8192u)
							/ 8192)));
			}
			set
			{
				this.bitvector1 = ((ushort)(((value * 8192)
							| this.bitvector1)));
			}
		}

		/// <summary>
		/// Gets or sets the second reserved bits.
		/// </summary>
		public uint Reserved2
		{
			get
			{
				return ((ushort)(((this.bitvector1 & 49152u)
							/ 16384)));
			}
			set
			{
				this.bitvector1 = ((ushort)(((value * 16384)
							| this.bitvector1)));
			}
		}
	}
}
