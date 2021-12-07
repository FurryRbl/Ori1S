using System;
using System.Collections.Generic;
using System.Net.NetworkInformation.MacOsStructs;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003AC RID: 940
	internal class MacOsNetworkInterface : UnixNetworkInterface
	{
		// Token: 0x060020C7 RID: 8391 RVA: 0x00060940 File Offset: 0x0005EB40
		private MacOsNetworkInterface(string name) : base(name)
		{
		}

		// Token: 0x060020C8 RID: 8392
		[DllImport("libc")]
		private static extern int getifaddrs(out IntPtr ifap);

		// Token: 0x060020C9 RID: 8393
		[DllImport("libc")]
		private static extern void freeifaddrs(IntPtr ifap);

		// Token: 0x060020CA RID: 8394 RVA: 0x0006094C File Offset: 0x0005EB4C
		public static NetworkInterface[] ImplGetAllNetworkInterfaces()
		{
			Dictionary<string, MacOsNetworkInterface> dictionary = new Dictionary<string, MacOsNetworkInterface>();
			IntPtr intPtr;
			if (MacOsNetworkInterface.getifaddrs(out intPtr) != 0)
			{
				throw new SystemException("getifaddrs() failed");
			}
			try
			{
				IntPtr intPtr2 = intPtr;
				while (intPtr2 != IntPtr.Zero)
				{
					ifaddrs ifaddrs = (ifaddrs)Marshal.PtrToStructure(intPtr2, typeof(ifaddrs));
					IPAddress ipaddress = IPAddress.None;
					string ifa_name = ifaddrs.ifa_name;
					int index = -1;
					byte[] array = null;
					NetworkInterfaceType networkInterfaceType = NetworkInterfaceType.Unknown;
					if (ifaddrs.ifa_addr != IntPtr.Zero)
					{
						sockaddr sockaddr = (sockaddr)Marshal.PtrToStructure(ifaddrs.ifa_addr, typeof(sockaddr));
						if (sockaddr.sa_family == 30)
						{
							sockaddr_in6 sockaddr_in = (sockaddr_in6)Marshal.PtrToStructure(ifaddrs.ifa_addr, typeof(sockaddr_in6));
							ipaddress = new IPAddress(sockaddr_in.sin6_addr.u6_addr8, (long)((ulong)sockaddr_in.sin6_scope_id));
						}
						else if (sockaddr.sa_family == 2)
						{
							ipaddress = new IPAddress((long)((ulong)((sockaddr_in)Marshal.PtrToStructure(ifaddrs.ifa_addr, typeof(sockaddr_in))).sin_addr));
						}
						else if (sockaddr.sa_family == 18)
						{
							sockaddr_dl sockaddr_dl = (sockaddr_dl)Marshal.PtrToStructure(ifaddrs.ifa_addr, typeof(sockaddr_dl));
							array = new byte[(int)sockaddr_dl.sdl_alen];
							Array.Copy(sockaddr_dl.sdl_data, (int)sockaddr_dl.sdl_nlen, array, 0, Math.Min(array.Length, sockaddr_dl.sdl_data.Length - (int)sockaddr_dl.sdl_nlen));
							index = (int)sockaddr_dl.sdl_index;
							int sdl_type = (int)sockaddr_dl.sdl_type;
							if (Enum.IsDefined(typeof(MacOsArpHardware), sdl_type))
							{
								MacOsArpHardware macOsArpHardware = (MacOsArpHardware)sdl_type;
								switch (macOsArpHardware)
								{
								case MacOsArpHardware.PPP:
									networkInterfaceType = NetworkInterfaceType.Ppp;
									break;
								case MacOsArpHardware.LOOPBACK:
									networkInterfaceType = NetworkInterfaceType.Loopback;
									array = null;
									break;
								default:
									if (macOsArpHardware != MacOsArpHardware.ETHER)
									{
										if (macOsArpHardware != MacOsArpHardware.FDDI)
										{
											if (macOsArpHardware == MacOsArpHardware.ATM)
											{
												networkInterfaceType = NetworkInterfaceType.Atm;
											}
										}
										else
										{
											networkInterfaceType = NetworkInterfaceType.Fddi;
										}
									}
									else
									{
										networkInterfaceType = NetworkInterfaceType.Ethernet;
									}
									break;
								case MacOsArpHardware.SLIP:
									networkInterfaceType = NetworkInterfaceType.Slip;
									break;
								}
							}
						}
					}
					MacOsNetworkInterface macOsNetworkInterface = null;
					if (!dictionary.TryGetValue(ifa_name, out macOsNetworkInterface))
					{
						macOsNetworkInterface = new MacOsNetworkInterface(ifa_name);
						dictionary.Add(ifa_name, macOsNetworkInterface);
					}
					if (!ipaddress.Equals(IPAddress.None))
					{
						macOsNetworkInterface.AddAddress(ipaddress);
					}
					if (array != null || networkInterfaceType == NetworkInterfaceType.Loopback)
					{
						macOsNetworkInterface.SetLinkLayerInfo(index, array, networkInterfaceType);
					}
					intPtr2 = ifaddrs.ifa_next;
				}
			}
			finally
			{
				MacOsNetworkInterface.freeifaddrs(intPtr);
			}
			NetworkInterface[] array2 = new NetworkInterface[dictionary.Count];
			int num = 0;
			foreach (NetworkInterface networkInterface in dictionary.Values)
			{
				array2[num] = networkInterface;
				num++;
			}
			return array2;
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x00060C88 File Offset: 0x0005EE88
		public override IPInterfaceProperties GetIPProperties()
		{
			if (this.ipproperties == null)
			{
				this.ipproperties = new MacOsIPInterfaceProperties(this, this.addresses);
			}
			return this.ipproperties;
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x00060CB0 File Offset: 0x0005EEB0
		public override IPv4InterfaceStatistics GetIPv4Statistics()
		{
			if (this.ipv4stats == null)
			{
				this.ipv4stats = new MacOsIPv4InterfaceStatistics(this);
			}
			return this.ipv4stats;
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x00060CD0 File Offset: 0x0005EED0
		public override OperationalStatus OperationalStatus
		{
			get
			{
				return OperationalStatus.Unknown;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x00060CD4 File Offset: 0x0005EED4
		public override bool SupportsMulticast
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040013EF RID: 5103
		private const int AF_INET = 2;

		// Token: 0x040013F0 RID: 5104
		private const int AF_INET6 = 30;

		// Token: 0x040013F1 RID: 5105
		private const int AF_LINK = 18;
	}
}
