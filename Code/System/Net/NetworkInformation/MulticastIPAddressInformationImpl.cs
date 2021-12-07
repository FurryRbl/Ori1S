using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003A0 RID: 928
	internal class MulticastIPAddressInformationImpl : MulticastIPAddressInformation
	{
		// Token: 0x0600207A RID: 8314 RVA: 0x0005FD44 File Offset: 0x0005DF44
		public MulticastIPAddressInformationImpl(IPAddress address, bool isDnsEligible, bool isTransient)
		{
			this.address = address;
			this.is_dns_eligible = isDnsEligible;
			this.is_transient = isTransient;
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x0005FD64 File Offset: 0x0005DF64
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x0005FD6C File Offset: 0x0005DF6C
		public override bool IsDnsEligible
		{
			get
			{
				return this.is_dns_eligible;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x0005FD74 File Offset: 0x0005DF74
		public override bool IsTransient
		{
			get
			{
				return this.is_transient;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x0005FD7C File Offset: 0x0005DF7C
		public override long AddressPreferredLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x0005FD80 File Offset: 0x0005DF80
		public override long AddressValidLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x0005FD84 File Offset: 0x0005DF84
		public override long DhcpLeaseLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x0005FD88 File Offset: 0x0005DF88
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return DuplicateAddressDetectionState.Invalid;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x0005FD8C File Offset: 0x0005DF8C
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return PrefixOrigin.Other;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x0005FD90 File Offset: 0x0005DF90
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return SuffixOrigin.Other;
			}
		}

		// Token: 0x040013C6 RID: 5062
		private IPAddress address;

		// Token: 0x040013C7 RID: 5063
		private bool is_dns_eligible;

		// Token: 0x040013C8 RID: 5064
		private bool is_transient;
	}
}
