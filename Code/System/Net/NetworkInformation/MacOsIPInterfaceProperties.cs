using System;
using System.Collections.Generic;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000380 RID: 896
	internal class MacOsIPInterfaceProperties : UnixIPInterfaceProperties
	{
		// Token: 0x06001FFB RID: 8187 RVA: 0x0005F434 File Offset: 0x0005D634
		public MacOsIPInterfaceProperties(MacOsNetworkInterface iface, List<IPAddress> addresses) : base(iface, addresses)
		{
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0005F440 File Offset: 0x0005D640
		public override IPv4InterfaceProperties GetIPv4Properties()
		{
			if (this.ipv4iface_properties == null)
			{
				this.ipv4iface_properties = new MacOsIPv4InterfaceProperties(this.iface as MacOsNetworkInterface);
			}
			return this.ipv4iface_properties;
		}
	}
}
