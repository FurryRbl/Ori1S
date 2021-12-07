using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Control Message Protocol for Internet Protocol version 6 (ICMPv6) statistical data for the local computer.</summary>
	// Token: 0x02000365 RID: 869
	public abstract class IcmpV6Statistics
	{
		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages received because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages received.</returns>
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001EB3 RID: 7859
		public abstract long DestinationUnreachableMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages sent because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages sent.</returns>
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001EB4 RID: 7860
		public abstract long DestinationUnreachableMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Reply messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages received.</returns>
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001EB5 RID: 7861
		public abstract long EchoRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Reply messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages sent.</returns>
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001EB6 RID: 7862
		public abstract long EchoRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Request messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages received.</returns>
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001EB7 RID: 7863
		public abstract long EchoRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Request messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages sent.</returns>
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001EB8 RID: 7864
		public abstract long EchoRequestsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) error messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP error messages received.</returns>
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001EB9 RID: 7865
		public abstract long ErrorsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) error messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP error messages sent.</returns>
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001EBA RID: 7866
		public abstract long ErrorsSent { get; }

		/// <summary>Gets the number of Internet Group management Protocol (IGMP) Group Membership Query messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Query messages received.</returns>
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001EBB RID: 7867
		public abstract long MembershipQueriesReceived { get; }

		/// <summary>Gets the number of Internet Group management Protocol (IGMP) Group Membership Query messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Query messages sent.</returns>
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001EBC RID: 7868
		public abstract long MembershipQueriesSent { get; }

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Reduction messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Reduction messages received.</returns>
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001EBD RID: 7869
		public abstract long MembershipReductionsReceived { get; }

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Reduction messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Reduction messages sent.</returns>
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001EBE RID: 7870
		public abstract long MembershipReductionsSent { get; }

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Report messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Report messages received.</returns>
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001EBF RID: 7871
		public abstract long MembershipReportsReceived { get; }

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Report messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Report messages sent.</returns>
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001EC0 RID: 7872
		public abstract long MembershipReportsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv6 messages received.</returns>
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001EC1 RID: 7873
		public abstract long MessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv6 messages sent.</returns>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001EC2 RID: 7874
		public abstract long MessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Advertisement messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Neighbor Advertisement messages received.</returns>
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001EC3 RID: 7875
		public abstract long NeighborAdvertisementsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Advertisement messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Neighbor Advertisement messages sent.</returns>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001EC4 RID: 7876
		public abstract long NeighborAdvertisementsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Solicitation messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Neighbor Solicitation messages received.</returns>
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001EC5 RID: 7877
		public abstract long NeighborSolicitsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Solicitation messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Neighbor Solicitation messages sent.</returns>
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001EC6 RID: 7878
		public abstract long NeighborSolicitsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Packet Too Big messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Packet Too Big messages received.</returns>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001EC7 RID: 7879
		public abstract long PacketTooBigMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Packet Too Big messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Packet Too Big messages sent.</returns>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001EC8 RID: 7880
		public abstract long PacketTooBigMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Parameter Problem messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages received.</returns>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001EC9 RID: 7881
		public abstract long ParameterProblemsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Parameter Problem messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages sent.</returns>
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001ECA RID: 7882
		public abstract long ParameterProblemsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Redirect messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages received.</returns>
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001ECB RID: 7883
		public abstract long RedirectsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Redirect messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages sent.</returns>
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001ECC RID: 7884
		public abstract long RedirectsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Advertisement messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Advertisement messages received.</returns>
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001ECD RID: 7885
		public abstract long RouterAdvertisementsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Advertisement messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Advertisement messages sent.</returns>
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001ECE RID: 7886
		public abstract long RouterAdvertisementsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Solicitation messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Solicitation messages received.</returns>
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001ECF RID: 7887
		public abstract long RouterSolicitsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Solicitation messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Solicitation messages sent.</returns>
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001ED0 RID: 7888
		public abstract long RouterSolicitsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Time Exceeded messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages received.</returns>
		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001ED1 RID: 7889
		public abstract long TimeExceededMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Time Exceeded messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages sent.</returns>
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001ED2 RID: 7890
		public abstract long TimeExceededMessagesSent { get; }
	}
}
