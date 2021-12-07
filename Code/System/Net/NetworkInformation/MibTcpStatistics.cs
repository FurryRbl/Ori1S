using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003BF RID: 959
	internal class MibTcpStatistics : TcpStatistics
	{
		// Token: 0x0600213B RID: 8507 RVA: 0x000620C0 File Offset: 0x000602C0
		public MibTcpStatistics(System.Collections.Specialized.StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000620D0 File Offset: 0x000602D0
		private long Get(string name)
		{
			return (this.dic[name] == null) ? 0L : long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x0006210C File Offset: 0x0006030C
		public override long ConnectionsAccepted
		{
			get
			{
				return this.Get("PassiveOpens");
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x0006211C File Offset: 0x0006031C
		public override long ConnectionsInitiated
		{
			get
			{
				return this.Get("ActiveOpens");
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x0006212C File Offset: 0x0006032C
		public override long CumulativeConnections
		{
			get
			{
				return this.Get("NumConns");
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x0006213C File Offset: 0x0006033C
		public override long CurrentConnections
		{
			get
			{
				return this.Get("CurrEstab");
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x0006214C File Offset: 0x0006034C
		public override long ErrorsReceived
		{
			get
			{
				return this.Get("InErrs");
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x0006215C File Offset: 0x0006035C
		public override long FailedConnectionAttempts
		{
			get
			{
				return this.Get("AttemptFails");
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x0006216C File Offset: 0x0006036C
		public override long MaximumConnections
		{
			get
			{
				return this.Get("MaxConn");
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x0006217C File Offset: 0x0006037C
		public override long MaximumTransmissionTimeout
		{
			get
			{
				return this.Get("RtoMax");
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x0006218C File Offset: 0x0006038C
		public override long MinimumTransmissionTimeout
		{
			get
			{
				return this.Get("RtoMin");
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x0006219C File Offset: 0x0006039C
		public override long ResetConnections
		{
			get
			{
				return this.Get("EstabResets");
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x000621AC File Offset: 0x000603AC
		public override long ResetsSent
		{
			get
			{
				return this.Get("OutRsts");
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x000621BC File Offset: 0x000603BC
		public override long SegmentsReceived
		{
			get
			{
				return this.Get("InSegs");
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x000621CC File Offset: 0x000603CC
		public override long SegmentsResent
		{
			get
			{
				return this.Get("RetransSegs");
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x000621DC File Offset: 0x000603DC
		public override long SegmentsSent
		{
			get
			{
				return this.Get("OutSegs");
			}
		}

		// Token: 0x04001452 RID: 5202
		private System.Collections.Specialized.StringDictionary dic;
	}
}
