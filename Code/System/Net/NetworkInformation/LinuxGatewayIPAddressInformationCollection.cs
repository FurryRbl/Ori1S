using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200035D RID: 861
	internal class LinuxGatewayIPAddressInformationCollection : GatewayIPAddressInformationCollection
	{
		// Token: 0x06001E58 RID: 7768 RVA: 0x0005CD7C File Offset: 0x0005AF7C
		private LinuxGatewayIPAddressInformationCollection(bool isReadOnly)
		{
			this.is_readonly = isReadOnly;
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0005CD8C File Offset: 0x0005AF8C
		public LinuxGatewayIPAddressInformationCollection(IPAddressCollection col)
		{
			foreach (IPAddress address in col)
			{
				this.Add(new GatewayIPAddressInformationImpl(address));
			}
			this.is_readonly = true;
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x0005CE0C File Offset: 0x0005B00C
		public override bool IsReadOnly
		{
			get
			{
				return this.is_readonly;
			}
		}

		// Token: 0x040012E0 RID: 4832
		public static readonly LinuxGatewayIPAddressInformationCollection Empty = new LinuxGatewayIPAddressInformationCollection(true);

		// Token: 0x040012E1 RID: 4833
		private bool is_readonly;
	}
}
