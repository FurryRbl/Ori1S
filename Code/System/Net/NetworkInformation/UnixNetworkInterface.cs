using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003AA RID: 938
	internal abstract class UnixNetworkInterface : NetworkInterface
	{
		// Token: 0x060020B1 RID: 8369 RVA: 0x000601AC File Offset: 0x0005E3AC
		internal UnixNetworkInterface(string name)
		{
			this.name = name;
			this.addresses = new List<IPAddress>();
		}

		// Token: 0x060020B2 RID: 8370
		[DllImport("libc")]
		private static extern int if_nametoindex(string ifname);

		// Token: 0x060020B3 RID: 8371 RVA: 0x000601C8 File Offset: 0x0005E3C8
		public static int IfNameToIndex(string ifname)
		{
			return UnixNetworkInterface.if_nametoindex(ifname);
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000601D0 File Offset: 0x0005E3D0
		internal void AddAddress(IPAddress address)
		{
			this.addresses.Add(address);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000601E0 File Offset: 0x0005E3E0
		internal void SetLinkLayerInfo(int index, byte[] macAddress, NetworkInterfaceType type)
		{
			this.index = index;
			this.macAddress = macAddress;
			this.type = type;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000601F8 File Offset: 0x0005E3F8
		public override PhysicalAddress GetPhysicalAddress()
		{
			if (this.macAddress != null)
			{
				return new PhysicalAddress(this.macAddress);
			}
			return PhysicalAddress.None;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00060218 File Offset: 0x0005E418
		public override bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			bool flag = networkInterfaceComponent == NetworkInterfaceComponent.IPv4;
			bool flag2 = !flag && networkInterfaceComponent == NetworkInterfaceComponent.IPv6;
			foreach (IPAddress ipaddress in this.addresses)
			{
				if (flag && ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					return true;
				}
				if (flag2 && ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x000602C8 File Offset: 0x0005E4C8
		public override string Description
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x000602D0 File Offset: 0x0005E4D0
		public override string Id
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x000602D8 File Offset: 0x0005E4D8
		public override bool IsReceiveOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x000602DC File Offset: 0x0005E4DC
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x000602E4 File Offset: 0x0005E4E4
		public override NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x000602EC File Offset: 0x0005E4EC
		[MonoTODO("Parse dmesg?")]
		public override long Speed
		{
			get
			{
				return 1000000L;
			}
		}

		// Token: 0x040013E0 RID: 5088
		protected IPv4InterfaceStatistics ipv4stats;

		// Token: 0x040013E1 RID: 5089
		protected IPInterfaceProperties ipproperties;

		// Token: 0x040013E2 RID: 5090
		private string name;

		// Token: 0x040013E3 RID: 5091
		private int index;

		// Token: 0x040013E4 RID: 5092
		protected List<IPAddress> addresses;

		// Token: 0x040013E5 RID: 5093
		private byte[] macAddress;

		// Token: 0x040013E6 RID: 5094
		private NetworkInterfaceType type;
	}
}
