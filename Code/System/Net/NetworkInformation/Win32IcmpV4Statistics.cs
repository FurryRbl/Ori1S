using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000362 RID: 866
	internal class Win32IcmpV4Statistics : IcmpV4Statistics
	{
		// Token: 0x06001E97 RID: 7831 RVA: 0x0005D028 File Offset: 0x0005B228
		public Win32IcmpV4Statistics(Win32_MIBICMPINFO info)
		{
			this.iin = info.InStats;
			this.iout = info.OutStats;
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x0005D058 File Offset: 0x0005B258
		public override long AddressMaskRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.AddrMaskReps);
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x0005D068 File Offset: 0x0005B268
		public override long AddressMaskRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.AddrMaskReps);
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x0005D078 File Offset: 0x0005B278
		public override long AddressMaskRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.AddrMasks);
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0005D088 File Offset: 0x0005B288
		public override long AddressMaskRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.AddrMasks);
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x0005D098 File Offset: 0x0005B298
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.DestUnreachs);
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x0005D0A8 File Offset: 0x0005B2A8
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.DestUnreachs);
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x0005D0B8 File Offset: 0x0005B2B8
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.EchoReps);
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x0005D0C8 File Offset: 0x0005B2C8
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.EchoReps);
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001EA0 RID: 7840 RVA: 0x0005D0D8 File Offset: 0x0005B2D8
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Echos);
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x0005D0E8 File Offset: 0x0005B2E8
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.Echos);
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x0005D0F8 File Offset: 0x0005B2F8
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Errors);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x0005D108 File Offset: 0x0005B308
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.iout.Errors);
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x0005D118 File Offset: 0x0005B318
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Msgs);
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x0005D128 File Offset: 0x0005B328
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Msgs);
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x0005D138 File Offset: 0x0005B338
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.iin.ParmProbs);
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x0005D148 File Offset: 0x0005B348
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.iout.ParmProbs);
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0005D158 File Offset: 0x0005B358
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Redirects);
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x0005D168 File Offset: 0x0005B368
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.iout.Redirects);
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x0005D178 File Offset: 0x0005B378
		public override long SourceQuenchesReceived
		{
			get
			{
				return (long)((ulong)this.iin.SrcQuenchs);
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x0005D188 File Offset: 0x0005B388
		public override long SourceQuenchesSent
		{
			get
			{
				return (long)((ulong)this.iout.SrcQuenchs);
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x0005D198 File Offset: 0x0005B398
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.TimeExcds);
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x0005D1A8 File Offset: 0x0005B3A8
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.TimeExcds);
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001EAE RID: 7854 RVA: 0x0005D1B8 File Offset: 0x0005B3B8
		public override long TimestampRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.TimestampReps);
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x0005D1C8 File Offset: 0x0005B3C8
		public override long TimestampRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.TimestampReps);
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x0005D1D8 File Offset: 0x0005B3D8
		public override long TimestampRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Timestamps);
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x0005D1E8 File Offset: 0x0005B3E8
		public override long TimestampRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.Timestamps);
			}
		}

		// Token: 0x040012E4 RID: 4836
		private Win32_MIBICMPSTATS iin;

		// Token: 0x040012E5 RID: 4837
		private Win32_MIBICMPSTATS iout;
	}
}
