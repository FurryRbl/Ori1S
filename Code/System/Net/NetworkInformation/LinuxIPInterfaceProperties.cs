using System;
using System.Collections.Generic;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200037F RID: 895
	internal class LinuxIPInterfaceProperties : UnixIPInterfaceProperties
	{
		// Token: 0x06001FF9 RID: 8185 RVA: 0x0005F3FC File Offset: 0x0005D5FC
		public LinuxIPInterfaceProperties(LinuxNetworkInterface iface, List<IPAddress> addresses) : base(iface, addresses)
		{
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x0005F408 File Offset: 0x0005D608
		public override IPv4InterfaceProperties GetIPv4Properties()
		{
			if (this.ipv4iface_properties == null)
			{
				this.ipv4iface_properties = new LinuxIPv4InterfaceProperties(this.iface as LinuxNetworkInterface);
			}
			return this.ipv4iface_properties;
		}
	}
}
