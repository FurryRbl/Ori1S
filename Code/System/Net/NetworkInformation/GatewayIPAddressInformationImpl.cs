using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200035F RID: 863
	internal class GatewayIPAddressInformationImpl : GatewayIPAddressInformation
	{
		// Token: 0x06001E5E RID: 7774 RVA: 0x0005CE1C File Offset: 0x0005B01C
		public GatewayIPAddressInformationImpl(IPAddress address)
		{
			this.address = address;
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x0005CE2C File Offset: 0x0005B02C
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x040012E2 RID: 4834
		private IPAddress address;
	}
}
