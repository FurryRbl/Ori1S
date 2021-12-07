using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200038B RID: 907
	internal class LinuxIPv4InterfaceStatistics : IPv4InterfaceStatistics
	{
		// Token: 0x06002042 RID: 8258 RVA: 0x0005F988 File Offset: 0x0005DB88
		public LinuxIPv4InterfaceStatistics(LinuxNetworkInterface parent)
		{
			this.linux = parent;
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x0005F998 File Offset: 0x0005DB98
		private long Read(string file)
		{
			long result;
			try
			{
				result = long.Parse(NetworkInterface.ReadLine(this.linux.IfacePath + file));
			}
			catch
			{
				result = 0L;
			}
			return result;
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x0005F9F8 File Offset: 0x0005DBF8
		public override long BytesReceived
		{
			get
			{
				return this.Read("statistics/rx_bytes");
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x0005FA08 File Offset: 0x0005DC08
		public override long BytesSent
		{
			get
			{
				return this.Read("statistics/tx_bytes");
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x0005FA18 File Offset: 0x0005DC18
		public override long IncomingPacketsDiscarded
		{
			get
			{
				return this.Read("statistics/rx_dropped");
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x0005FA28 File Offset: 0x0005DC28
		public override long IncomingPacketsWithErrors
		{
			get
			{
				return this.Read("statistics/rx_errors");
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002048 RID: 8264 RVA: 0x0005FA38 File Offset: 0x0005DC38
		public override long IncomingUnknownProtocolPackets
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x0005FA3C File Offset: 0x0005DC3C
		public override long NonUnicastPacketsReceived
		{
			get
			{
				return this.Read("statistics/multicast");
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x0005FA4C File Offset: 0x0005DC4C
		public override long NonUnicastPacketsSent
		{
			get
			{
				return this.Read("statistics/multicast");
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x0005FA5C File Offset: 0x0005DC5C
		public override long OutgoingPacketsDiscarded
		{
			get
			{
				return this.Read("statistics/tx_dropped");
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x0005FA6C File Offset: 0x0005DC6C
		public override long OutgoingPacketsWithErrors
		{
			get
			{
				return this.Read("statistics/tx_errors");
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x0005FA7C File Offset: 0x0005DC7C
		public override long OutputQueueLength
		{
			get
			{
				return 1024L;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x0005FA84 File Offset: 0x0005DC84
		public override long UnicastPacketsReceived
		{
			get
			{
				return this.Read("statistics/rx_packets");
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x0005FA94 File Offset: 0x0005DC94
		public override long UnicastPacketsSent
		{
			get
			{
				return this.Read("statistics/tx_packets");
			}
		}

		// Token: 0x04001379 RID: 4985
		private LinuxNetworkInterface linux;
	}
}
