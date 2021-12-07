using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200037A RID: 890
	internal class MibIPGlobalStatistics : IPGlobalStatistics
	{
		// Token: 0x06001FAE RID: 8110 RVA: 0x0005EB54 File Offset: 0x0005CD54
		public MibIPGlobalStatistics(System.Collections.Specialized.StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0005EB64 File Offset: 0x0005CD64
		private long Get(string name)
		{
			return (this.dic[name] == null) ? 0L : long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x0005EBA0 File Offset: 0x0005CDA0
		public override int DefaultTtl
		{
			get
			{
				return (int)this.Get("DefaultTTL");
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x0005EBB0 File Offset: 0x0005CDB0
		public override bool ForwardingEnabled
		{
			get
			{
				return this.Get("Forwarding") != 0L;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x0005EBC4 File Offset: 0x0005CDC4
		public override int NumberOfInterfaces
		{
			get
			{
				return (int)this.Get("NumIf");
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x0005EBD4 File Offset: 0x0005CDD4
		public override int NumberOfIPAddresses
		{
			get
			{
				return (int)this.Get("NumAddr");
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0005EBE4 File Offset: 0x0005CDE4
		public override int NumberOfRoutes
		{
			get
			{
				return (int)this.Get("NumRoutes");
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x0005EBF4 File Offset: 0x0005CDF4
		public override long OutputPacketRequests
		{
			get
			{
				return this.Get("OutRequests");
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x0005EC04 File Offset: 0x0005CE04
		public override long OutputPacketRoutingDiscards
		{
			get
			{
				return this.Get("RoutingDiscards");
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x0005EC14 File Offset: 0x0005CE14
		public override long OutputPacketsDiscarded
		{
			get
			{
				return this.Get("OutDiscards");
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x0005EC24 File Offset: 0x0005CE24
		public override long OutputPacketsWithNoRoute
		{
			get
			{
				return this.Get("OutNoRoutes");
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x0005EC34 File Offset: 0x0005CE34
		public override long PacketFragmentFailures
		{
			get
			{
				return this.Get("FragFails");
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x0005EC44 File Offset: 0x0005CE44
		public override long PacketReassembliesRequired
		{
			get
			{
				return this.Get("ReasmReqds");
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x0005EC54 File Offset: 0x0005CE54
		public override long PacketReassemblyFailures
		{
			get
			{
				return this.Get("ReasmFails");
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x0005EC64 File Offset: 0x0005CE64
		public override long PacketReassemblyTimeout
		{
			get
			{
				return this.Get("ReasmTimeout");
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x0005EC74 File Offset: 0x0005CE74
		public override long PacketsFragmented
		{
			get
			{
				return this.Get("FragOks");
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x0005EC84 File Offset: 0x0005CE84
		public override long PacketsReassembled
		{
			get
			{
				return this.Get("ReasmOks");
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x0005EC94 File Offset: 0x0005CE94
		public override long ReceivedPackets
		{
			get
			{
				return this.Get("InReceives");
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x0005ECA4 File Offset: 0x0005CEA4
		public override long ReceivedPacketsDelivered
		{
			get
			{
				return this.Get("InDelivers");
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x0005ECB4 File Offset: 0x0005CEB4
		public override long ReceivedPacketsDiscarded
		{
			get
			{
				return this.Get("InDiscards");
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x0005ECC4 File Offset: 0x0005CEC4
		public override long ReceivedPacketsForwarded
		{
			get
			{
				return this.Get("ForwDatagrams");
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x0005ECD4 File Offset: 0x0005CED4
		public override long ReceivedPacketsWithAddressErrors
		{
			get
			{
				return this.Get("InAddrErrors");
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x0005ECE4 File Offset: 0x0005CEE4
		public override long ReceivedPacketsWithHeadersErrors
		{
			get
			{
				return this.Get("InHdrErrors");
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x0005ECF4 File Offset: 0x0005CEF4
		public override long ReceivedPacketsWithUnknownProtocol
		{
			get
			{
				return this.Get("InUnknownProtos");
			}
		}

		// Token: 0x04001332 RID: 4914
		private System.Collections.Specialized.StringDictionary dic;
	}
}
