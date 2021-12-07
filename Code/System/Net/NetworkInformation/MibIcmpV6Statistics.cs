using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000366 RID: 870
	internal class MibIcmpV6Statistics : IcmpV6Statistics
	{
		// Token: 0x06001ED3 RID: 7891 RVA: 0x0005D200 File Offset: 0x0005B400
		public MibIcmpV6Statistics(System.Collections.Specialized.StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0005D210 File Offset: 0x0005B410
		private long Get(string name)
		{
			return (this.dic[name] == null) ? 0L : long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x0005D24C File Offset: 0x0005B44C
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return this.Get("InDestUnreachs");
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x0005D25C File Offset: 0x0005B45C
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return this.Get("OutDestUnreachs");
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x0005D26C File Offset: 0x0005B46C
		public override long EchoRepliesReceived
		{
			get
			{
				return this.Get("InEchoReplies");
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x0005D27C File Offset: 0x0005B47C
		public override long EchoRepliesSent
		{
			get
			{
				return this.Get("OutEchoReplies");
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x0005D28C File Offset: 0x0005B48C
		public override long EchoRequestsReceived
		{
			get
			{
				return this.Get("InEchos");
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x0005D29C File Offset: 0x0005B49C
		public override long EchoRequestsSent
		{
			get
			{
				return this.Get("OutEchos");
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x0005D2AC File Offset: 0x0005B4AC
		public override long ErrorsReceived
		{
			get
			{
				return this.Get("InErrors");
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x0005D2BC File Offset: 0x0005B4BC
		public override long ErrorsSent
		{
			get
			{
				return this.Get("OutErrors");
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x0005D2CC File Offset: 0x0005B4CC
		public override long MembershipQueriesReceived
		{
			get
			{
				return this.Get("InGroupMembQueries");
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x0005D2DC File Offset: 0x0005B4DC
		public override long MembershipQueriesSent
		{
			get
			{
				return this.Get("OutGroupMembQueries");
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x0005D2EC File Offset: 0x0005B4EC
		public override long MembershipReductionsReceived
		{
			get
			{
				return this.Get("InGroupMembReductiions");
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x0005D2FC File Offset: 0x0005B4FC
		public override long MembershipReductionsSent
		{
			get
			{
				return this.Get("OutGroupMembReductiions");
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x0005D30C File Offset: 0x0005B50C
		public override long MembershipReportsReceived
		{
			get
			{
				return this.Get("InGroupMembRespons");
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x0005D31C File Offset: 0x0005B51C
		public override long MembershipReportsSent
		{
			get
			{
				return this.Get("OutGroupMembRespons");
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x0005D32C File Offset: 0x0005B52C
		public override long MessagesReceived
		{
			get
			{
				return this.Get("InMsgs");
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x0005D33C File Offset: 0x0005B53C
		public override long MessagesSent
		{
			get
			{
				return this.Get("OutMsgs");
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x0005D34C File Offset: 0x0005B54C
		public override long NeighborAdvertisementsReceived
		{
			get
			{
				return this.Get("InNeighborAdvertisements");
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x0005D35C File Offset: 0x0005B55C
		public override long NeighborAdvertisementsSent
		{
			get
			{
				return this.Get("OutNeighborAdvertisements");
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x0005D36C File Offset: 0x0005B56C
		public override long NeighborSolicitsReceived
		{
			get
			{
				return this.Get("InNeighborSolicits");
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x0005D37C File Offset: 0x0005B57C
		public override long NeighborSolicitsSent
		{
			get
			{
				return this.Get("OutNeighborSolicits");
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x0005D38C File Offset: 0x0005B58C
		public override long PacketTooBigMessagesReceived
		{
			get
			{
				return this.Get("InPktTooBigs");
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x0005D39C File Offset: 0x0005B59C
		public override long PacketTooBigMessagesSent
		{
			get
			{
				return this.Get("OutPktTooBigs");
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x0005D3AC File Offset: 0x0005B5AC
		public override long ParameterProblemsReceived
		{
			get
			{
				return this.Get("InParmProblems");
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x0005D3BC File Offset: 0x0005B5BC
		public override long ParameterProblemsSent
		{
			get
			{
				return this.Get("OutParmProblems");
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x0005D3CC File Offset: 0x0005B5CC
		public override long RedirectsReceived
		{
			get
			{
				return this.Get("InRedirects");
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0005D3DC File Offset: 0x0005B5DC
		public override long RedirectsSent
		{
			get
			{
				return this.Get("OutRedirects");
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x0005D3EC File Offset: 0x0005B5EC
		public override long RouterAdvertisementsReceived
		{
			get
			{
				return this.Get("InRouterAdvertisements");
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x0005D3FC File Offset: 0x0005B5FC
		public override long RouterAdvertisementsSent
		{
			get
			{
				return this.Get("OutRouterAdvertisements");
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x0005D40C File Offset: 0x0005B60C
		public override long RouterSolicitsReceived
		{
			get
			{
				return this.Get("InRouterSolicits");
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0005D41C File Offset: 0x0005B61C
		public override long RouterSolicitsSent
		{
			get
			{
				return this.Get("OutRouterSolicits");
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x0005D42C File Offset: 0x0005B62C
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return this.Get("InTimeExcds");
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x0005D43C File Offset: 0x0005B63C
		public override long TimeExceededMessagesSent
		{
			get
			{
				return this.Get("OutTimeExcds");
			}
		}

		// Token: 0x040012F5 RID: 4853
		private System.Collections.Specialized.StringDictionary dic;
	}
}
