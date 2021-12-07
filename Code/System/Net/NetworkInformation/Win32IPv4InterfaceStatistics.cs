using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200038A RID: 906
	internal class Win32IPv4InterfaceStatistics : IPv4InterfaceStatistics
	{
		// Token: 0x06002035 RID: 8245 RVA: 0x0005F8B8 File Offset: 0x0005DAB8
		public Win32IPv4InterfaceStatistics(Win32_MIB_IFROW info)
		{
			this.info = info;
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x0005F8C8 File Offset: 0x0005DAC8
		public override long BytesReceived
		{
			get
			{
				return (long)this.info.InOctets;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x0005F8D8 File Offset: 0x0005DAD8
		public override long BytesSent
		{
			get
			{
				return (long)this.info.OutOctets;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x0005F8E8 File Offset: 0x0005DAE8
		public override long IncomingPacketsDiscarded
		{
			get
			{
				return (long)this.info.InDiscards;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x0005F8F8 File Offset: 0x0005DAF8
		public override long IncomingPacketsWithErrors
		{
			get
			{
				return (long)this.info.InErrors;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x0005F908 File Offset: 0x0005DB08
		public override long IncomingUnknownProtocolPackets
		{
			get
			{
				return (long)this.info.InUnknownProtos;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x0005F918 File Offset: 0x0005DB18
		public override long NonUnicastPacketsReceived
		{
			get
			{
				return (long)this.info.InNUcastPkts;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x0005F928 File Offset: 0x0005DB28
		public override long NonUnicastPacketsSent
		{
			get
			{
				return (long)this.info.OutNUcastPkts;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x0005F938 File Offset: 0x0005DB38
		public override long OutgoingPacketsDiscarded
		{
			get
			{
				return (long)this.info.OutDiscards;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x0600203E RID: 8254 RVA: 0x0005F948 File Offset: 0x0005DB48
		public override long OutgoingPacketsWithErrors
		{
			get
			{
				return (long)this.info.OutErrors;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x0005F958 File Offset: 0x0005DB58
		public override long OutputQueueLength
		{
			get
			{
				return (long)this.info.OutQLen;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x0005F968 File Offset: 0x0005DB68
		public override long UnicastPacketsReceived
		{
			get
			{
				return (long)this.info.InUcastPkts;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x0005F978 File Offset: 0x0005DB78
		public override long UnicastPacketsSent
		{
			get
			{
				return (long)this.info.OutUcastPkts;
			}
		}

		// Token: 0x04001378 RID: 4984
		private Win32_MIB_IFROW info;
	}
}
