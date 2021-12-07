using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the protocols that the <see cref="T:System.Net.Sockets.Socket" /> class supports.</summary>
	// Token: 0x020003F3 RID: 1011
	public enum ProtocolType
	{
		/// <summary>Internet Protocol.</summary>
		// Token: 0x040015AB RID: 5547
		IP,
		/// <summary>Internet Control Message Protocol.</summary>
		// Token: 0x040015AC RID: 5548
		Icmp,
		/// <summary>Internet Group Management Protocol.</summary>
		// Token: 0x040015AD RID: 5549
		Igmp,
		/// <summary>Gateway To Gateway Protocol.</summary>
		// Token: 0x040015AE RID: 5550
		Ggp,
		/// <summary>Transmission Control Protocol.</summary>
		// Token: 0x040015AF RID: 5551
		Tcp = 6,
		/// <summary>PARC Universal Packet Protocol.</summary>
		// Token: 0x040015B0 RID: 5552
		Pup = 12,
		/// <summary>User Datagram Protocol.</summary>
		// Token: 0x040015B1 RID: 5553
		Udp = 17,
		/// <summary>Internet Datagram Protocol.</summary>
		// Token: 0x040015B2 RID: 5554
		Idp = 22,
		/// <summary>Internet Protocol version 6 (IPv6). </summary>
		// Token: 0x040015B3 RID: 5555
		IPv6 = 41,
		/// <summary>Net Disk Protocol (unofficial).</summary>
		// Token: 0x040015B4 RID: 5556
		ND = 77,
		/// <summary>Raw IP packet protocol.</summary>
		// Token: 0x040015B5 RID: 5557
		Raw = 255,
		/// <summary>Unspecified protocol.</summary>
		// Token: 0x040015B6 RID: 5558
		Unspecified = 0,
		/// <summary>Internet Packet Exchange Protocol.</summary>
		// Token: 0x040015B7 RID: 5559
		Ipx = 1000,
		/// <summary>Sequenced Packet Exchange protocol.</summary>
		// Token: 0x040015B8 RID: 5560
		Spx = 1256,
		/// <summary>Sequenced Packet Exchange version 2 protocol.</summary>
		// Token: 0x040015B9 RID: 5561
		SpxII,
		/// <summary>Unknown protocol.</summary>
		// Token: 0x040015BA RID: 5562
		Unknown = -1,
		/// <summary>Internet Protocol version 4.</summary>
		// Token: 0x040015BB RID: 5563
		IPv4 = 4,
		/// <summary>IPv6 Routing header.</summary>
		// Token: 0x040015BC RID: 5564
		IPv6RoutingHeader = 43,
		/// <summary>IPv6 Fragment header.</summary>
		// Token: 0x040015BD RID: 5565
		IPv6FragmentHeader,
		/// <summary>IPv6 Encapsulating Security Payload header.</summary>
		// Token: 0x040015BE RID: 5566
		IPSecEncapsulatingSecurityPayload = 50,
		/// <summary>IPv6 Authentication header. For details, see RFC 2292 section 2.2.1, available at http://www.ietf.org.</summary>
		// Token: 0x040015BF RID: 5567
		IPSecAuthenticationHeader,
		/// <summary>Internet Control Message Protocol for IPv6.</summary>
		// Token: 0x040015C0 RID: 5568
		IcmpV6 = 58,
		/// <summary>IPv6 No next header.</summary>
		// Token: 0x040015C1 RID: 5569
		IPv6NoNextHeader,
		/// <summary>IPv6 Destination Options header.</summary>
		// Token: 0x040015C2 RID: 5570
		IPv6DestinationOptions,
		/// <summary>IPv6 Hop by Hop Options header.</summary>
		// Token: 0x040015C3 RID: 5571
		IPv6HopByHopOptions = 0
	}
}
