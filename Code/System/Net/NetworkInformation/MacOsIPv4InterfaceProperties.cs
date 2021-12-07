using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000386 RID: 902
	internal sealed class MacOsIPv4InterfaceProperties : UnixIPv4InterfaceProperties
	{
		// Token: 0x0600201B RID: 8219 RVA: 0x0005F7B4 File Offset: 0x0005D9B4
		public MacOsIPv4InterfaceProperties(MacOsNetworkInterface iface) : base(iface)
		{
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x0005F7C0 File Offset: 0x0005D9C0
		public override bool IsForwardingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x0005F7C4 File Offset: 0x0005D9C4
		public override int Mtu
		{
			get
			{
				return 0;
			}
		}
	}
}
