using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200038C RID: 908
	internal class MacOsIPv4InterfaceStatistics : IPv4InterfaceStatistics
	{
		// Token: 0x06002050 RID: 8272 RVA: 0x0005FAA4 File Offset: 0x0005DCA4
		public MacOsIPv4InterfaceStatistics(MacOsNetworkInterface parent)
		{
			this.macos = parent;
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x0005FAB4 File Offset: 0x0005DCB4
		public override long BytesReceived
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x0005FAB8 File Offset: 0x0005DCB8
		public override long BytesSent
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x0005FABC File Offset: 0x0005DCBC
		public override long IncomingPacketsDiscarded
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x0005FAC0 File Offset: 0x0005DCC0
		public override long IncomingPacketsWithErrors
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x0005FAC4 File Offset: 0x0005DCC4
		public override long IncomingUnknownProtocolPackets
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x0005FAC8 File Offset: 0x0005DCC8
		public override long NonUnicastPacketsReceived
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x0005FACC File Offset: 0x0005DCCC
		public override long NonUnicastPacketsSent
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x0005FAD0 File Offset: 0x0005DCD0
		public override long OutgoingPacketsDiscarded
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x0005FAD4 File Offset: 0x0005DCD4
		public override long OutgoingPacketsWithErrors
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x0005FAD8 File Offset: 0x0005DCD8
		public override long OutputQueueLength
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x0005FADC File Offset: 0x0005DCDC
		public override long UnicastPacketsReceived
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x0005FAE0 File Offset: 0x0005DCE0
		public override long UnicastPacketsSent
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x0400137A RID: 4986
		private MacOsNetworkInterface macos;
	}
}
