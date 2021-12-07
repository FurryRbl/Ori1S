using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200037E RID: 894
	internal abstract class UnixIPInterfaceProperties : IPInterfaceProperties
	{
		// Token: 0x06001FEA RID: 8170 RVA: 0x0005EE80 File Offset: 0x0005D080
		public UnixIPInterfaceProperties(UnixNetworkInterface iface, List<IPAddress> addresses)
		{
			this.iface = iface;
			this.addresses = addresses;
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0005EEB8 File Offset: 0x0005D0B8
		public override IPv6InterfaceProperties GetIPv6Properties()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0005EEC0 File Offset: 0x0005D0C0
		private void ParseRouteInfo(string iface)
		{
			try
			{
				this.gateways = new IPAddressCollection();
				using (StreamReader streamReader = new StreamReader("/proc/net/route"))
				{
					streamReader.ReadLine();
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						text = text.Trim();
						if (text.Length != 0)
						{
							string[] array = text.Split(new char[]
							{
								'\t'
							});
							if (array.Length >= 3)
							{
								string text2 = array[2].Trim();
								byte[] array2 = new byte[4];
								if (text2.Length == 8 && iface.Equals(array[0], StringComparison.OrdinalIgnoreCase))
								{
									for (int i = 0; i < 4; i++)
									{
										if (!byte.TryParse(text2.Substring(i * 2, 2), NumberStyles.HexNumber, null, out array2[3 - i]))
										{
										}
									}
									IPAddress ipaddress = new IPAddress(array2);
									if (!ipaddress.Equals(IPAddress.Any))
									{
										this.gateways.Add(ipaddress);
									}
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x0005F01C File Offset: 0x0005D21C
		private void ParseResolvConf()
		{
			try
			{
				DateTime lastWriteTime = File.GetLastWriteTime("/etc/resolv.conf");
				if (!(lastWriteTime <= this.last_parse))
				{
					this.last_parse = lastWriteTime;
					this.dns_suffix = string.Empty;
					this.dns_servers = new IPAddressCollection();
					using (StreamReader streamReader = new StreamReader("/etc/resolv.conf"))
					{
						string text;
						while ((text = streamReader.ReadLine()) != null)
						{
							text = text.Trim();
							if (text.Length != 0 && text[0] != '#')
							{
								System.Text.RegularExpressions.Match match = UnixIPInterfaceProperties.ns.Match(text);
								if (match.Success)
								{
									try
									{
										string text2 = match.Groups["address"].Value;
										text2 = text2.Trim();
										this.dns_servers.Add(IPAddress.Parse(text2));
									}
									catch
									{
									}
								}
								else
								{
									match = UnixIPInterfaceProperties.search.Match(text);
									if (match.Success)
									{
										string text2 = match.Groups["domain"].Value;
										string[] array = text2.Split(new char[]
										{
											','
										});
										this.dns_suffix = array[0].Trim();
									}
								}
							}
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
				this.dns_servers.SetReadOnly();
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x0005F1E0 File Offset: 0x0005D3E0
		public override IPAddressInformationCollection AnycastAddresses
		{
			get
			{
				List<IPAddress> list = new List<IPAddress>();
				return IPAddressInformationImplCollection.LinuxFromAnycast(list);
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x0005F1FC File Offset: 0x0005D3FC
		[MonoTODO("Always returns an empty collection.")]
		public override IPAddressCollection DhcpServerAddresses
		{
			get
			{
				IPAddressCollection ipaddressCollection = new IPAddressCollection();
				ipaddressCollection.SetReadOnly();
				return ipaddressCollection;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x0005F218 File Offset: 0x0005D418
		public override IPAddressCollection DnsAddresses
		{
			get
			{
				this.ParseResolvConf();
				return this.dns_servers;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x0005F228 File Offset: 0x0005D428
		public override string DnsSuffix
		{
			get
			{
				this.ParseResolvConf();
				return this.dns_suffix;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x0005F238 File Offset: 0x0005D438
		public override GatewayIPAddressInformationCollection GatewayAddresses
		{
			get
			{
				this.ParseRouteInfo(this.iface.Name.ToString());
				if (this.gateways.Count > 0)
				{
					return new LinuxGatewayIPAddressInformationCollection(this.gateways);
				}
				return LinuxGatewayIPAddressInformationCollection.Empty;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x0005F280 File Offset: 0x0005D480
		[MonoTODO("Always returns true")]
		public override bool IsDnsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x0005F284 File Offset: 0x0005D484
		[MonoTODO("Always returns false")]
		public override bool IsDynamicDnsEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x0005F288 File Offset: 0x0005D488
		public override MulticastIPAddressInformationCollection MulticastAddresses
		{
			get
			{
				List<IPAddress> list = new List<IPAddress>();
				foreach (IPAddress ipaddress in this.addresses)
				{
					byte[] addressBytes = ipaddress.GetAddressBytes();
					if (addressBytes[0] >= 224 && addressBytes[0] <= 239)
					{
						list.Add(ipaddress);
					}
				}
				return MulticastIPAddressInformationImplCollection.LinuxFromList(list);
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x0005F31C File Offset: 0x0005D51C
		public override UnicastIPAddressInformationCollection UnicastAddresses
		{
			get
			{
				List<IPAddress> list = new List<IPAddress>();
				foreach (IPAddress ipaddress in this.addresses)
				{
					System.Net.Sockets.AddressFamily addressFamily = ipaddress.AddressFamily;
					if (addressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
					{
						if (addressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
						{
							if (!ipaddress.IsIPv6Multicast)
							{
								list.Add(ipaddress);
							}
						}
					}
					else
					{
						byte b = ipaddress.GetAddressBytes()[0];
						if (b < 224 || b > 239)
						{
							list.Add(ipaddress);
						}
					}
				}
				return UnicastIPAddressInformationImplCollection.LinuxFromList(list);
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x0005F3F4 File Offset: 0x0005D5F4
		[MonoTODO("Always returns an empty collection.")]
		public override IPAddressCollection WinsServersAddresses
		{
			get
			{
				return new IPAddressCollection();
			}
		}

		// Token: 0x0400134B RID: 4939
		protected IPv4InterfaceProperties ipv4iface_properties;

		// Token: 0x0400134C RID: 4940
		protected UnixNetworkInterface iface;

		// Token: 0x0400134D RID: 4941
		private List<IPAddress> addresses;

		// Token: 0x0400134E RID: 4942
		private IPAddressCollection dns_servers;

		// Token: 0x0400134F RID: 4943
		private IPAddressCollection gateways;

		// Token: 0x04001350 RID: 4944
		private string dns_suffix;

		// Token: 0x04001351 RID: 4945
		private DateTime last_parse;

		// Token: 0x04001352 RID: 4946
		private static System.Text.RegularExpressions.Regex ns = new System.Text.RegularExpressions.Regex("\\s*nameserver\\s+(?<address>.*)");

		// Token: 0x04001353 RID: 4947
		private static System.Text.RegularExpressions.Regex search = new System.Text.RegularExpressions.Regex("\\s*search\\s+(?<domain>.*)");
	}
}
