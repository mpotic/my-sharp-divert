namespace MySharpDivert
{
	public interface IHeadersData
	{
		IcmpV4Header IcmpV4HeaderData { get; set; }

		IcmpV6Header IcmpV6HeaderData { get; set; }

		IPv4Header IPv4HeaderData { get; set; }

		IPv6Header IPv6HeaderData { get; set; }

		TcpHeader TcpHeaderData { get; set; }

		UdpHeader UdpHeaderData { get; set; }

		uint PacketDataLength { get; set; }
	}
}
