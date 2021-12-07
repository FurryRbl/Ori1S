using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000370 RID: 880
	internal class IPAddressInformationImpl : IPAddressInformation
	{
		// Token: 0x06001F3F RID: 7999 RVA: 0x0005DC24 File Offset: 0x0005BE24
		public IPAddressInformationImpl(IPAddress address, bool isDnsEligible, bool isTransient)
		{
			this.address = address;
			this.is_dns_eligible = isDnsEligible;
			this.is_transient = isTransient;
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x0005DC44 File Offset: 0x0005BE44
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x0005DC4C File Offset: 0x0005BE4C
		public override bool IsDnsEligible
		{
			get
			{
				return this.is_dns_eligible;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x0005DC54 File Offset: 0x0005BE54
		[MonoTODO("Always false on Linux")]
		public override bool IsTransient
		{
			get
			{
				return this.is_transient;
			}
		}

		// Token: 0x04001312 RID: 4882
		private IPAddress address;

		// Token: 0x04001313 RID: 4883
		private bool is_dns_eligible;

		// Token: 0x04001314 RID: 4884
		private bool is_transient;
	}
}
