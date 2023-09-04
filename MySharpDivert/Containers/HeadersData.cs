namespace MySharpDivert
{
	public class HeadersData : IHeadersData
	{
		public IcmpV4Header IcmpV4HeaderData { get; set; }

		public IcmpV6Header IcmpV6HeaderData { get; set; }

		public IPv4Header IPv4HeaderData { get; set; }

		public IPv6Header IPv6HeaderData { get; set; }

		public TcpHeader TcpHeaderData { get; set; }

		public UdpHeader UdpHeaderData { get; set; }

		public uint PacketDataLength { get; set; }
	}
}
