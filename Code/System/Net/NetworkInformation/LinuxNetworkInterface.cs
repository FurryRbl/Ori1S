using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003AB RID: 939
	internal class LinuxNetworkInterface : UnixNetworkInterface
	{
		// Token: 0x060020BE RID: 8382 RVA: 0x000602F4 File Offset: 0x0005E4F4
		private LinuxNetworkInterface(string name) : base(name)
		{
			this.iface_path = "/sys/class/net/" + name + "/";
			this.iface_operstate_path = this.iface_path + "operstate";
			this.iface_flags_path = this.iface_path + "flags";
		}

		// Token: 0x060020BF RID: 8383
		[DllImport("libc")]
		private static extern int getifaddrs(out IntPtr ifap);

		// Token: 0x060020C0 RID: 8384
		[DllImport("libc")]
		private static extern void freeifaddrs(IntPtr ifap);

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0006034C File Offset: 0x0005E54C
		internal string IfacePath
		{
			get
			{
				return this.iface_path;
			}
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x00060354 File Offset: 0x0005E554
		public static NetworkInterface[] ImplGetAllNetworkInterfaces()
		{
			Dictionary<string, LinuxNetworkInterface> dictionary = new Dictionary<string, LinuxNetworkInterface>();
			IntPtr intPtr;
			if (LinuxNetworkInterface.getifaddrs(out intPtr) != 0)
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
						sockaddr_in sockaddr_in = (sockaddr_in)Marshal.PtrToStructure(ifaddrs.ifa_addr, typeof(sockaddr_in));
						if (sockaddr_in.sin_family == 10)
						{
							sockaddr_in6 sockaddr_in2 = (sockaddr_in6)Marshal.PtrToStructure(ifaddrs.ifa_addr, typeof(sockaddr_in6));
							ipaddress = new IPAddress(sockaddr_in2.sin6_addr.u6_addr8, (long)((ulong)sockaddr_in2.sin6_scope_id));
						}
						else if (sockaddr_in.sin_family == 2)
						{
							ipaddress = new IPAddress((long)((ulong)sockaddr_in.sin_addr));
						}
						else if (sockaddr_in.sin_family == 17)
						{
							sockaddr_ll sockaddr_ll = (sockaddr_ll)Marshal.PtrToStructure(ifaddrs.ifa_addr, typeof(sockaddr_ll));
							if ((int)sockaddr_ll.sll_halen > sockaddr_ll.sll_addr.Length)
							{
								Console.Error.WriteLine("Got a bad hardware address length for an AF_PACKET {0} {1}", sockaddr_ll.sll_halen, sockaddr_ll.sll_addr.Length);
								intPtr2 = ifaddrs.ifa_next;
								continue;
							}
							array = new byte[(int)sockaddr_ll.sll_halen];
							Array.Copy(sockaddr_ll.sll_addr, 0, array, 0, array.Length);
							index = sockaddr_ll.sll_ifindex;
							int sll_hatype = (int)sockaddr_ll.sll_hatype;
							if (Enum.IsDefined(typeof(LinuxArpHardware), sll_hatype))
							{
								LinuxArpHardware linuxArpHardware = (LinuxArpHardware)sll_hatype;
								switch (linuxArpHardware)
								{
								case LinuxArpHardware.TUNNEL:
									break;
								case LinuxArpHardware.TUNNEL6:
									break;
								default:
									switch (linuxArpHardware)
									{
									case LinuxArpHardware.ETHER:
										break;
									case LinuxArpHardware.EETHER:
										break;
									default:
										if (linuxArpHardware == LinuxArpHardware.ATM)
										{
											networkInterfaceType = NetworkInterfaceType.Atm;
											goto IL_27A;
										}
										if (linuxArpHardware == LinuxArpHardware.SLIP)
										{
											networkInterfaceType = NetworkInterfaceType.Slip;
											goto IL_27A;
										}
										if (linuxArpHardware != LinuxArpHardware.PPP)
										{
											goto IL_27A;
										}
										networkInterfaceType = NetworkInterfaceType.Ppp;
										goto IL_27A;
									case LinuxArpHardware.PRONET:
										networkInterfaceType = NetworkInterfaceType.TokenRing;
										goto IL_27A;
									}
									networkInterfaceType = NetworkInterfaceType.Ethernet;
									goto IL_27A;
								case LinuxArpHardware.LOOPBACK:
									networkInterfaceType = NetworkInterfaceType.Loopback;
									array = null;
									goto IL_27A;
								case LinuxArpHardware.FDDI:
									networkInterfaceType = NetworkInterfaceType.Fddi;
									goto IL_27A;
								}
								networkInterfaceType = NetworkInterfaceType.Tunnel;
							}
						}
					}
					IL_27A:
					LinuxNetworkInterface linuxNetworkInterface = null;
					if (!dictionary.TryGetValue(ifa_name, out linuxNetworkInterface))
					{
						linuxNetworkInterface = new LinuxNetworkInterface(ifa_name);
						dictionary.Add(ifa_name, linuxNetworkInterface);
					}
					if (!ipaddress.Equals(IPAddress.None))
					{
						linuxNetworkInterface.AddAddress(ipaddress);
					}
					if (array != null || networkInterfaceType == NetworkInterfaceType.Loopback)
					{
						if (networkInterfaceType == NetworkInterfaceType.Ethernet && Directory.Exists(linuxNetworkInterface.IfacePath + "wireless"))
						{
							networkInterfaceType = NetworkInterfaceType.Wireless80211;
						}
						linuxNetworkInterface.SetLinkLayerInfo(index, array, networkInterfaceType);
					}
					intPtr2 = ifaddrs.ifa_next;
				}
			}
			finally
			{
				LinuxNetworkInterface.freeifaddrs(intPtr);
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

		// Token: 0x060020C3 RID: 8387 RVA: 0x00060710 File Offset: 0x0005E910
		public override IPInterfaceProperties GetIPProperties()
		{
			if (this.ipproperties == null)
			{
				this.ipproperties = new LinuxIPInterfaceProperties(this, this.addresses);
			}
			return this.ipproperties;
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x00060738 File Offset: 0x0005E938
		public override IPv4InterfaceStatistics GetIPv4Statistics()
		{
			if (this.ipv4stats == null)
			{
				this.ipv4stats = new LinuxIPv4InterfaceStatistics(this);
			}
			return this.ipv4stats;
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x00060758 File Offset: 0x0005E958
		public override OperationalStatus OperationalStatus
		{
			get
			{
				if (!Directory.Exists(this.iface_path))
				{
					return OperationalStatus.Unknown;
				}
				try
				{
					string text = NetworkInterface.ReadLine(this.iface_operstate_path);
					string text2 = text;
					switch (text2)
					{
					case "unknown":
						return OperationalStatus.Unknown;
					case "notpresent":
						return OperationalStatus.NotPresent;
					case "down":
						return OperationalStatus.Down;
					case "lowerlayerdown":
						return OperationalStatus.LowerLayerDown;
					case "testing":
						return OperationalStatus.Testing;
					case "dormant":
						return OperationalStatus.Dormant;
					case "up":
						return OperationalStatus.Up;
					}
				}
				catch
				{
				}
				return OperationalStatus.Unknown;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x00060894 File Offset: 0x0005EA94
		public override bool SupportsMulticast
		{
			get
			{
				if (!Directory.Exists(this.iface_path))
				{
					return false;
				}
				bool result;
				try
				{
					string text = NetworkInterface.ReadLine(this.iface_flags_path);
					if (text.Length > 2 && text[0] == '0' && text[1] == 'x')
					{
						text = text.Substring(2);
					}
					ulong num = ulong.Parse(text, NumberStyles.HexNumber);
					result = ((num & 4096UL) == 4096UL);
				}
				catch
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x040013E7 RID: 5095
		private const int AF_INET = 2;

		// Token: 0x040013E8 RID: 5096
		private const int AF_INET6 = 10;

		// Token: 0x040013E9 RID: 5097
		private const int AF_PACKET = 17;

		// Token: 0x040013EA RID: 5098
		private NetworkInterfaceType type;

		// Token: 0x040013EB RID: 5099
		private string iface_path;

		// Token: 0x040013EC RID: 5100
		private string iface_operstate_path;

		// Token: 0x040013ED RID: 5101
		private string iface_flags_path;
	}
}
