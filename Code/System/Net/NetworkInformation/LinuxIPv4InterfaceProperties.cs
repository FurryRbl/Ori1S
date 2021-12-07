using System;
using System.IO;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000385 RID: 901
	internal sealed class LinuxIPv4InterfaceProperties : UnixIPv4InterfaceProperties
	{
		// Token: 0x06002018 RID: 8216 RVA: 0x0005F6F4 File Offset: 0x0005D8F4
		public LinuxIPv4InterfaceProperties(LinuxNetworkInterface iface) : base(iface)
		{
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x0005F700 File Offset: 0x0005D900
		public override bool IsForwardingEnabled
		{
			get
			{
				string path = "/proc/sys/net/ipv4/conf/" + this.iface.Name + "/forwarding";
				if (File.Exists(path))
				{
					string a = NetworkInterface.ReadLine(path);
					return a != "0";
				}
				return false;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x0005F748 File Offset: 0x0005D948
		public override int Mtu
		{
			get
			{
				string path = (this.iface as LinuxNetworkInterface).IfacePath + "mtu";
				int result = 0;
				if (File.Exists(path))
				{
					string s = NetworkInterface.ReadLine(path);
					try
					{
						result = int.Parse(s);
					}
					catch
					{
					}
				}
				return result;
			}
		}
	}
}
