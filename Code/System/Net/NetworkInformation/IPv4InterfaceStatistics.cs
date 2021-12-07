using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides statistical data for a network interface on the local computer.</summary>
	// Token: 0x02000389 RID: 905
	public abstract class IPv4InterfaceStatistics
	{
		/// <summary>Gets the number of bytes that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of bytes that were received on the interface.</returns>
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002029 RID: 8233
		public abstract long BytesReceived { get; }

		/// <summary>Gets the number of bytes that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of bytes that were transmitted on the interface.</returns>
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x0600202A RID: 8234
		public abstract long BytesSent { get; }

		/// <summary>Gets the number of incoming packets that were discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of discarded incoming packets.</returns>
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600202B RID: 8235
		public abstract long IncomingPacketsDiscarded { get; }

		/// <summary>Gets the number of incoming packets with errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets with errors.</returns>
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x0600202C RID: 8236
		public abstract long IncomingPacketsWithErrors { get; }

		/// <summary>Gets the number of incoming packets with an unknown protocol.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets with an unknown protocol.</returns>
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x0600202D RID: 8237
		public abstract long IncomingUnknownProtocolPackets { get; }

		/// <summary>Gets the number of non-unicast packets that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of non-unicast packets that were received on the interface.</returns>
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x0600202E RID: 8238
		public abstract long NonUnicastPacketsReceived { get; }

		/// <summary>Gets the number of non-unicast packets that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of non-unicast packets that were sent on the interface.</returns>
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600202F RID: 8239
		public abstract long NonUnicastPacketsSent { get; }

		/// <summary>Gets the number of outgoing packets that were discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of discarded outgoing packets.</returns>
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002030 RID: 8240
		public abstract long OutgoingPacketsDiscarded { get; }

		/// <summary>Gets the number of outgoing packets with errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of outgoing packets with errors.</returns>
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002031 RID: 8241
		public abstract long OutgoingPacketsWithErrors { get; }

		/// <summary>Gets the length of the output queue.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packets in the output queue.</returns>
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002032 RID: 8242
		[MonoTODO("Not implemented for Linux")]
		public abstract long OutputQueueLength { get; }

		/// <summary>Gets the number of unicast packets that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of unicast packets that were received on the interface.</returns>
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002033 RID: 8243
		public abstract long UnicastPacketsReceived { get; }

		/// <summary>Gets the number of unicast packets that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of unicast packets that were sent on the interface.</returns>
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002034 RID: 8244
		public abstract long UnicastPacketsSent { get; }
	}
}
