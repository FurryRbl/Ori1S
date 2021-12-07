using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Control Message Protocol for IPv4 (ICMPv4) statistical data for the local computer.</summary>
	// Token: 0x02000360 RID: 864
	public abstract class IcmpV4Statistics
	{
		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Reply messages that were received.</returns>
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001E61 RID: 7777
		public abstract long AddressMaskRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Reply messages that were sent.</returns>
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001E62 RID: 7778
		public abstract long AddressMaskRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Request messages that were received.</returns>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001E63 RID: 7779
		public abstract long AddressMaskRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Request messages that were sent.</returns>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001E64 RID: 7780
		public abstract long AddressMaskRequestsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were received because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages that were received.</returns>
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001E65 RID: 7781
		public abstract long DestinationUnreachableMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were sent because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages sent.</returns>
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001E66 RID: 7782
		public abstract long DestinationUnreachableMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages that were received.</returns>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001E67 RID: 7783
		public abstract long EchoRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages that were sent.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001E68 RID: 7784
		public abstract long EchoRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages that were received.</returns>
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001E69 RID: 7785
		public abstract long EchoRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages that were sent.</returns>
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001E6A RID: 7786
		public abstract long EchoRequestsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) error messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP error messages that were received.</returns>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001E6B RID: 7787
		public abstract long ErrorsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) error messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP error messages that were sent.</returns>
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001E6C RID: 7788
		public abstract long ErrorsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv4 messages that were received.</returns>
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001E6D RID: 7789
		public abstract long MessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv4 messages that were sent.</returns>
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001E6E RID: 7790
		public abstract long MessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Parameter Problem messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages that were received.</returns>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001E6F RID: 7791
		public abstract long ParameterProblemsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Parameter Problem messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages that were sent.</returns>
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001E70 RID: 7792
		public abstract long ParameterProblemsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Redirect messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages that were received.</returns>
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001E71 RID: 7793
		public abstract long RedirectsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Redirect messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages that were sent.</returns>
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001E72 RID: 7794
		public abstract long RedirectsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Source Quench messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Source Quench messages that were received.</returns>
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001E73 RID: 7795
		public abstract long SourceQuenchesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Source Quench messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Source Quench messages that were sent.</returns>
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001E74 RID: 7796
		public abstract long SourceQuenchesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Time Exceeded messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages that were received.</returns>
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001E75 RID: 7797
		public abstract long TimeExceededMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Time Exceeded messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages that were sent.</returns>
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001E76 RID: 7798
		public abstract long TimeExceededMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Reply messages that were received.</returns>
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001E77 RID: 7799
		public abstract long TimestampRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Reply messages that were sent.</returns>
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001E78 RID: 7800
		public abstract long TimestampRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Request messages that were received.</returns>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001E79 RID: 7801
		public abstract long TimestampRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Request messages that were sent.</returns>
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001E7A RID: 7802
		public abstract long TimestampRequestsSent { get; }
	}
}
