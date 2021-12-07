using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003C0 RID: 960
	internal class Win32TcpStatistics : TcpStatistics
	{
		// Token: 0x0600214B RID: 8523 RVA: 0x000621EC File Offset: 0x000603EC
		public Win32TcpStatistics(Win32_MIB_TCPSTATS info)
		{
			this.info = info;
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x000621FC File Offset: 0x000603FC
		public override long ConnectionsAccepted
		{
			get
			{
				return (long)((ulong)this.info.PassiveOpens);
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x0006220C File Offset: 0x0006040C
		public override long ConnectionsInitiated
		{
			get
			{
				return (long)((ulong)this.info.ActiveOpens);
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x0006221C File Offset: 0x0006041C
		public override long CumulativeConnections
		{
			get
			{
				return (long)((ulong)this.info.NumConns);
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x0006222C File Offset: 0x0006042C
		public override long CurrentConnections
		{
			get
			{
				return (long)((ulong)this.info.CurrEstab);
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x0006223C File Offset: 0x0006043C
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.info.InErrs);
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x0006224C File Offset: 0x0006044C
		public override long FailedConnectionAttempts
		{
			get
			{
				return (long)((ulong)this.info.AttemptFails);
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x0006225C File Offset: 0x0006045C
		public override long MaximumConnections
		{
			get
			{
				return (long)((ulong)this.info.MaxConn);
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x0006226C File Offset: 0x0006046C
		public override long MaximumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.info.RtoMax);
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x0006227C File Offset: 0x0006047C
		public override long MinimumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.info.RtoMin);
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x0006228C File Offset: 0x0006048C
		public override long ResetConnections
		{
			get
			{
				return (long)((ulong)this.info.EstabResets);
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x0006229C File Offset: 0x0006049C
		public override long ResetsSent
		{
			get
			{
				return (long)((ulong)this.info.OutRsts);
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x000622AC File Offset: 0x000604AC
		public override long SegmentsReceived
		{
			get
			{
				return (long)((ulong)this.info.InSegs);
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x000622BC File Offset: 0x000604BC
		public override long SegmentsResent
		{
			get
			{
				return (long)((ulong)this.info.RetransSegs);
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x000622CC File Offset: 0x000604CC
		public override long SegmentsSent
		{
			get
			{
				return (long)((ulong)this.info.OutSegs);
			}
		}

		// Token: 0x04001453 RID: 5203
		private Win32_MIB_TCPSTATS info;
	}
}
