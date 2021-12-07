using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003AD RID: 941
	internal class Win32NetworkInterface2 : NetworkInterface
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x00060CD8 File Offset: 0x0005EED8
		private Win32NetworkInterface2(Win32_IP_ADAPTER_ADDRESSES addr)
		{
			this.addr = addr;
			this.mib4 = default(Win32_MIB_IFROW);
			this.mib4.Index = addr.Alignment.IfIndex;
			if (Win32NetworkInterface2.GetIfEntry(ref this.mib4) != 0)
			{
				this.mib4.Index = -1;
			}
			this.mib6 = default(Win32_MIB_IFROW);
			this.mib6.Index = addr.Ipv6IfIndex;
			if (Win32NetworkInterface2.GetIfEntry(ref this.mib6) != 0)
			{
				this.mib6.Index = -1;
			}
			this.ip4stats = new Win32IPv4InterfaceStatistics(this.mib4);
			this.ip_if_props = new Win32IPInterfaceProperties2(addr, this.mib4, this.mib6);
		}

		// Token: 0x060020D0 RID: 8400
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetAdaptersInfo(byte[] info, ref int size);

		// Token: 0x060020D1 RID: 8401
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetAdaptersAddresses(uint family, uint flags, IntPtr reserved, byte[] info, ref int size);

		// Token: 0x060020D2 RID: 8402
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetIfEntry(ref Win32_MIB_IFROW row);

		// Token: 0x060020D3 RID: 8403 RVA: 0x00060D98 File Offset: 0x0005EF98
		public static NetworkInterface[] ImplGetAllNetworkInterfaces()
		{
			Win32_IP_ADAPTER_ADDRESSES[] adaptersAddresses = Win32NetworkInterface2.GetAdaptersAddresses();
			NetworkInterface[] array = new NetworkInterface[adaptersAddresses.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Win32NetworkInterface2(adaptersAddresses[i]);
			}
			return array;
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x00060DD4 File Offset: 0x0005EFD4
		public static Win32_IP_ADAPTER_INFO GetAdapterInfoByIndex(int index)
		{
			foreach (Win32_IP_ADAPTER_INFO win32_IP_ADAPTER_INFO in Win32NetworkInterface2.GetAdaptersInfo())
			{
				if ((ulong)win32_IP_ADAPTER_INFO.Index == (ulong)((long)index))
				{
					return win32_IP_ADAPTER_INFO;
				}
			}
			return null;
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x00060E10 File Offset: 0x0005F010
		private unsafe static Win32_IP_ADAPTER_INFO[] GetAdaptersInfo()
		{
			byte[] array = null;
			int num = 0;
			Win32NetworkInterface2.GetAdaptersInfo(array, ref num);
			array = new byte[num];
			int adaptersInfo = Win32NetworkInterface2.GetAdaptersInfo(array, ref num);
			if (adaptersInfo != 0)
			{
				throw new NetworkInformationException(adaptersInfo);
			}
			List<Win32_IP_ADAPTER_INFO> list = new List<Win32_IP_ADAPTER_INFO>();
			fixed (byte* value = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
			{
				IntPtr intPtr = (IntPtr)((void*)value);
				while (intPtr != IntPtr.Zero)
				{
					Win32_IP_ADAPTER_INFO win32_IP_ADAPTER_INFO = new Win32_IP_ADAPTER_INFO();
					Marshal.PtrToStructure(intPtr, win32_IP_ADAPTER_INFO);
					list.Add(win32_IP_ADAPTER_INFO);
					intPtr = win32_IP_ADAPTER_INFO.Next;
				}
			}
			return list.ToArray();
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x00060EB8 File Offset: 0x0005F0B8
		private unsafe static Win32_IP_ADAPTER_ADDRESSES[] GetAdaptersAddresses()
		{
			byte[] array = null;
			int num = 0;
			Win32NetworkInterface2.GetAdaptersAddresses(0U, 0U, IntPtr.Zero, array, ref num);
			array = new byte[num];
			int adaptersAddresses = Win32NetworkInterface2.GetAdaptersAddresses(0U, 0U, IntPtr.Zero, array, ref num);
			if (adaptersAddresses != 0)
			{
				throw new NetworkInformationException(adaptersAddresses);
			}
			List<Win32_IP_ADAPTER_ADDRESSES> list = new List<Win32_IP_ADAPTER_ADDRESSES>();
			fixed (byte* value = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
			{
				IntPtr intPtr = (IntPtr)((void*)value);
				while (intPtr != IntPtr.Zero)
				{
					Win32_IP_ADAPTER_ADDRESSES win32_IP_ADAPTER_ADDRESSES = new Win32_IP_ADAPTER_ADDRESSES();
					Marshal.PtrToStructure(intPtr, win32_IP_ADAPTER_ADDRESSES);
					list.Add(win32_IP_ADAPTER_ADDRESSES);
					intPtr = win32_IP_ADAPTER_ADDRESSES.Next;
				}
			}
			return list.ToArray();
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x00060F6C File Offset: 0x0005F16C
		public override IPInterfaceProperties GetIPProperties()
		{
			return this.ip_if_props;
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x00060F74 File Offset: 0x0005F174
		public override IPv4InterfaceStatistics GetIPv4Statistics()
		{
			return this.ip4stats;
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x00060F7C File Offset: 0x0005F17C
		public override PhysicalAddress GetPhysicalAddress()
		{
			byte[] array = new byte[this.addr.PhysicalAddressLength];
			Array.Copy(this.addr.PhysicalAddress, 0, array, 0, array.Length);
			return new PhysicalAddress(array);
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x00060FB8 File Offset: 0x0005F1B8
		public override bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			if (networkInterfaceComponent != NetworkInterfaceComponent.IPv4)
			{
				return networkInterfaceComponent == NetworkInterfaceComponent.IPv6 && this.mib6.Index >= 0;
			}
			return this.mib4.Index >= 0;
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x00061000 File Offset: 0x0005F200
		public override string Description
		{
			get
			{
				return this.addr.Description;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x00061010 File Offset: 0x0005F210
		public override string Id
		{
			get
			{
				return this.addr.AdapterName;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x00061020 File Offset: 0x0005F220
		public override bool IsReceiveOnly
		{
			get
			{
				return this.addr.IsReceiveOnly;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x00061030 File Offset: 0x0005F230
		public override string Name
		{
			get
			{
				return this.addr.FriendlyName;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x00061040 File Offset: 0x0005F240
		public override NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				return this.addr.IfType;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x00061050 File Offset: 0x0005F250
		public override OperationalStatus OperationalStatus
		{
			get
			{
				return this.addr.OperStatus;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x00061060 File Offset: 0x0005F260
		public override long Speed
		{
			get
			{
				return (long)((ulong)((this.mib6.Index < 0) ? this.mib4.Speed : this.mib6.Speed));
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x00061090 File Offset: 0x0005F290
		public override bool SupportsMulticast
		{
			get
			{
				return !this.addr.NoMulticast;
			}
		}

		// Token: 0x040013F2 RID: 5106
		private Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x040013F3 RID: 5107
		private Win32_MIB_IFROW mib4;

		// Token: 0x040013F4 RID: 5108
		private Win32_MIB_IFROW mib6;

		// Token: 0x040013F5 RID: 5109
		private Win32IPv4InterfaceStatistics ip4stats;

		// Token: 0x040013F6 RID: 5110
		private IPInterfaceProperties ip_if_props;
	}
}
