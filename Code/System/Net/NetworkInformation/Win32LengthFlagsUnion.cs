using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003D1 RID: 977
	internal struct Win32LengthFlagsUnion
	{
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x000628A8 File Offset: 0x00060AA8
		public bool IsDnsEligible
		{
			get
			{
				return (this.Flags & 1U) != 0U;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x000628B8 File Offset: 0x00060AB8
		public bool IsTransient
		{
			get
			{
				return (this.Flags & 2U) != 0U;
			}
		}

		// Token: 0x040014CA RID: 5322
		private const int IP_ADAPTER_ADDRESS_DNS_ELIGIBLE = 1;

		// Token: 0x040014CB RID: 5323
		private const int IP_ADAPTER_ADDRESS_TRANSIENT = 2;

		// Token: 0x040014CC RID: 5324
		public uint Length;

		// Token: 0x040014CD RID: 5325
		public uint Flags;
	}
}
