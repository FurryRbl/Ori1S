using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Protocol (IP) statistical data.</summary>
	// Token: 0x02000379 RID: 889
	public abstract class IPGlobalStatistics
	{
		/// <summary>Gets the default time-to-live (TTL) value for Internet Protocol (IP) packets.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the TTL.</returns>
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001F98 RID: 8088
		public abstract int DefaultTtl { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that specifies whether Internet Protocol (IP) packet forwarding is enabled.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that specifies whether packet forwarding is enabled.</returns>
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001F99 RID: 8089
		public abstract bool ForwardingEnabled { get; }

		/// <summary>Gets the number of network interfaces.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value containing the number of network interfaces for the address family used to obtain this <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> instance.</returns>
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001F9A RID: 8090
		public abstract int NumberOfInterfaces { get; }

		/// <summary>Gets the number of Internet Protocol (IP) addresses assigned to the local computer.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of IP addresses assigned to the address family (Internet Protocol version 4 or Internet Protocol version 6) described by this object.</returns>
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001F9B RID: 8091
		public abstract int NumberOfIPAddresses { get; }

		/// <summary>Gets the number of routes in the Internet Protocol (IP) routing table.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of routes in the routing table.</returns>
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001F9C RID: 8092
		public abstract int NumberOfRoutes { get; }

		/// <summary>Gets the number of outbound Internet Protocol (IP) packets.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of outgoing packets.</returns>
		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001F9D RID: 8093
		public abstract long OutputPacketRequests { get; }

		/// <summary>Gets the number of routes that have been discarded from the routing table.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of valid routes that have been discarded.</returns>
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001F9E RID: 8094
		public abstract long OutputPacketRoutingDiscards { get; }

		/// <summary>Gets the number of transmitted Internet Protocol (IP) packets that have been discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of outgoing packets that have been discarded.</returns>
		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001F9F RID: 8095
		public abstract long OutputPacketsDiscarded { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets for which the local computer could not determine a route to the destination address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of packets that could not be sent because a route could not be found.</returns>
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001FA0 RID: 8096
		public abstract long OutputPacketsWithNoRoute { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets that could not be fragmented.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packets that required fragmentation but had the "Don't Fragment" bit set.</returns>
		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001FA1 RID: 8097
		public abstract long PacketFragmentFailures { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets that required reassembly.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packet reassemblies required.</returns>
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001FA2 RID: 8098
		public abstract long PacketReassembliesRequired { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets that were not successfully reassembled.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packets that could not be reassembled.</returns>
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001FA3 RID: 8099
		public abstract long PacketReassemblyFailures { get; }

		/// <summary>Gets the maximum amount of time within which all fragments of an Internet Protocol (IP) packet must arrive.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the maximum number of milliseconds within which all fragments of a packet must arrive to avoid being discarded.</returns>
		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001FA4 RID: 8100
		public abstract long PacketReassemblyTimeout { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets fragmented.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of fragmented packets.</returns>
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001FA5 RID: 8101
		public abstract long PacketsFragmented { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets reassembled.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of fragmented packets that have been successfully reassembled.</returns>
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001FA6 RID: 8102
		public abstract long PacketsReassembled { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets received.</returns>
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001FA7 RID: 8103
		public abstract long ReceivedPackets { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets delivered.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets delivered.</returns>
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001FA8 RID: 8104
		public abstract long ReceivedPacketsDelivered { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets that have been received and discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets that have been discarded.</returns>
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001FA9 RID: 8105
		public abstract long ReceivedPacketsDiscarded { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets forwarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of forwarded packets.</returns>
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001FAA RID: 8106
		public abstract long ReceivedPacketsForwarded { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets with address errors that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets received with errors in the address portion of the header.</returns>
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001FAB RID: 8107
		public abstract long ReceivedPacketsWithAddressErrors { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets with header errors that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets received and discarded due to errors in the header.</returns>
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001FAC RID: 8108
		public abstract long ReceivedPacketsWithHeadersErrors { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets received on the local machine with an unknown protocol in the header.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the total number of IP packets received with an unknown protocol.</returns>
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001FAD RID: 8109
		public abstract long ReceivedPacketsWithUnknownProtocol { get; }
	}
}
