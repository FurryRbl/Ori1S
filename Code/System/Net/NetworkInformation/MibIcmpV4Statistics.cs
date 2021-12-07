using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000361 RID: 865
	internal class MibIcmpV4Statistics : IcmpV4Statistics
	{
		// Token: 0x06001E7B RID: 7803 RVA: 0x0005CE3C File Offset: 0x0005B03C
		public MibIcmpV4Statistics(System.Collections.Specialized.StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x0005CE4C File Offset: 0x0005B04C
		private long Get(string name)
		{
			return (this.dic[name] == null) ? 0L : long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x0005CE88 File Offset: 0x0005B088
		public override long AddressMaskRepliesReceived
		{
			get
			{
				return this.Get("InAddrMaskReps");
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x0005CE98 File Offset: 0x0005B098
		public override long AddressMaskRepliesSent
		{
			get
			{
				return this.Get("OutAddrMaskReps");
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x0005CEA8 File Offset: 0x0005B0A8
		public override long AddressMaskRequestsReceived
		{
			get
			{
				return this.Get("InAddrMasks");
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0005CEB8 File Offset: 0x0005B0B8
		public override long AddressMaskRequestsSent
		{
			get
			{
				return this.Get("OutAddrMasks");
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x0005CEC8 File Offset: 0x0005B0C8
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return this.Get("InDestUnreachs");
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x0005CED8 File Offset: 0x0005B0D8
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return this.Get("OutDestUnreachs");
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x0005CEE8 File Offset: 0x0005B0E8
		public override long EchoRepliesReceived
		{
			get
			{
				return this.Get("InEchoReps");
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001E84 RID: 7812 RVA: 0x0005CEF8 File Offset: 0x0005B0F8
		public override long EchoRepliesSent
		{
			get
			{
				return this.Get("OutEchoReps");
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x0005CF08 File Offset: 0x0005B108
		public override long EchoRequestsReceived
		{
			get
			{
				return this.Get("InEchos");
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x0005CF18 File Offset: 0x0005B118
		public override long EchoRequestsSent
		{
			get
			{
				return this.Get("OutEchos");
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0005CF28 File Offset: 0x0005B128
		public override long ErrorsReceived
		{
			get
			{
				return this.Get("InErrors");
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x0005CF38 File Offset: 0x0005B138
		public override long ErrorsSent
		{
			get
			{
				return this.Get("OutErrors");
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0005CF48 File Offset: 0x0005B148
		public override long MessagesReceived
		{
			get
			{
				return this.Get("InMsgs");
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x0005CF58 File Offset: 0x0005B158
		public override long MessagesSent
		{
			get
			{
				return this.Get("OutMsgs");
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x0005CF68 File Offset: 0x0005B168
		public override long ParameterProblemsReceived
		{
			get
			{
				return this.Get("InParmProbs");
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0005CF78 File Offset: 0x0005B178
		public override long ParameterProblemsSent
		{
			get
			{
				return this.Get("OutParmProbs");
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x0005CF88 File Offset: 0x0005B188
		public override long RedirectsReceived
		{
			get
			{
				return this.Get("InRedirects");
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x0005CF98 File Offset: 0x0005B198
		public override long RedirectsSent
		{
			get
			{
				return this.Get("OutRedirects");
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x0005CFA8 File Offset: 0x0005B1A8
		public override long SourceQuenchesReceived
		{
			get
			{
				return this.Get("InSrcQuenchs");
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x0005CFB8 File Offset: 0x0005B1B8
		public override long SourceQuenchesSent
		{
			get
			{
				return this.Get("OutSrcQuenchs");
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x0005CFC8 File Offset: 0x0005B1C8
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return this.Get("InTimeExcds");
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x0005CFD8 File Offset: 0x0005B1D8
		public override long TimeExceededMessagesSent
		{
			get
			{
				return this.Get("OutTimeExcds");
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x0005CFE8 File Offset: 0x0005B1E8
		public override long TimestampRepliesReceived
		{
			get
			{
				return this.Get("InTimestampReps");
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x0005CFF8 File Offset: 0x0005B1F8
		public override long TimestampRepliesSent
		{
			get
			{
				return this.Get("OutTimestampReps");
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x0005D008 File Offset: 0x0005B208
		public override long TimestampRequestsReceived
		{
			get
			{
				return this.Get("InTimestamps");
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0005D018 File Offset: 0x0005B218
		public override long TimestampRequestsSent
		{
			get
			{
				return this.Get("OutTimestamps");
			}
		}

		// Token: 0x040012E3 RID: 4835
		private System.Collections.Specialized.StringDictionary dic;
	}
}
