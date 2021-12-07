using System;
using System.IO;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the network connectivity of the local computer.</summary>
	// Token: 0x02000371 RID: 881
	public abstract class IPGlobalProperties
	{
		/// <summary>Gets an object that provides information about the local computer's network connectivity and traffic statistics.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.IPGlobalProperties" /> object that contains information about the local computer.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Net.NetworkInformation.NetworkInformationPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Access="Read" />
		/// </PermissionSet>
		// Token: 0x06001F44 RID: 8004 RVA: 0x0005DC64 File Offset: 0x0005BE64
		public static IPGlobalProperties GetIPGlobalProperties()
		{
			PlatformID platform = Environment.OSVersion.Platform;
			if (platform != PlatformID.Unix)
			{
				return new Win32IPGlobalProperties();
			}
			if (Directory.Exists("/proc"))
			{
				MibIPGlobalProperties mibIPGlobalProperties = new MibIPGlobalProperties("/proc");
				if (File.Exists(mibIPGlobalProperties.StatisticsFile))
				{
					return mibIPGlobalProperties;
				}
			}
			if (Directory.Exists("/usr/compat/linux/proc"))
			{
				MibIPGlobalProperties mibIPGlobalProperties = new MibIPGlobalProperties("/usr/compat/linux/proc");
				if (File.Exists(mibIPGlobalProperties.StatisticsFile))
				{
					return mibIPGlobalProperties;
				}
			}
			throw new NotSupportedException("This platform is not supported");
		}

		/// <summary>Returns information about the Internet Protocol version 4 (IPV4) Transmission Control Protocol (TCP) connections on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.TcpConnectionInformation" /> array that contains objects that describe the active TCP connections, or an empty array if no active TCP connections are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function GetTcpTable failed. </exception>
		// Token: 0x06001F45 RID: 8005
		public abstract TcpConnectionInformation[] GetActiveTcpConnections();

		/// <summary>Returns endpoint information about the Internet Protocol version 4 (IPV4) Transmission Control Protocol (TCP) listeners on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.IPEndPoint" /> array that contains objects that describe the active TCP listeners, or an empty array, if no active TCP listeners are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function GetTcpTable failed. </exception>
		// Token: 0x06001F46 RID: 8006
		public abstract IPEndPoint[] GetActiveTcpListeners();

		/// <summary>Returns information about the Internet Protocol version 4 (IPv4) User Datagram Protocol (UDP) listeners on the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> array that contains objects that describe the UDP listeners, or an empty array if no UDP listeners are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetUdpTable failed. </exception>
		// Token: 0x06001F47 RID: 8007
		public abstract IPEndPoint[] GetActiveUdpListeners();

		/// <summary>Provides Internet Control Message Protocol (ICMP) version 4 statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IcmpV4Statistics" /> object that provides ICMP version 4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function GetIcmpStatistics failed. </exception>
		// Token: 0x06001F48 RID: 8008
		public abstract IcmpV4Statistics GetIcmpV4Statistics();

		/// <summary>Provides Internet Control Message Protocol (ICMP) version 6 statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IcmpV6Statistics" /> object that provides ICMP version 6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function GetIcmpStatisticsEx failed. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer's operating system is not Windows XP or later.</exception>
		// Token: 0x06001F49 RID: 8009
		public abstract IcmpV6Statistics GetIcmpV6Statistics();

		/// <summary>Provides Internet Protocol version 4 (IPv4) statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> object that provides IPv4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetIpStatistics failed.</exception>
		// Token: 0x06001F4A RID: 8010
		public abstract IPGlobalStatistics GetIPv4GlobalStatistics();

		/// <summary>Provides Internet Protocol version 6 (IPv6) statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> object that provides IPv6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetIpStatistics failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer is not running an operating system that supports IPv6. </exception>
		// Token: 0x06001F4B RID: 8011
		public abstract IPGlobalStatistics GetIPv6GlobalStatistics();

		/// <summary>Provides Transmission Control Protocol/Internet Protocol version 4 (TCP/IPv4) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.TcpStatistics" /> object that provides TCP/IPv4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetTcpStatistics failed.</exception>
		// Token: 0x06001F4C RID: 8012
		public abstract TcpStatistics GetTcpIPv4Statistics();

		/// <summary>Provides Transmission Control Protocol/Internet Protocol version 6 (TCP/IPv6) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.TcpStatistics" /> object that provides TCP/IPv6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetTcpStatistics failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer is not running an operating system that supports IPv6. </exception>
		// Token: 0x06001F4D RID: 8013
		public abstract TcpStatistics GetTcpIPv6Statistics();

		/// <summary>Provides User Datagram Protocol/Internet Protocol version 4 (UDP/IPv4) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.UdpStatistics" /> object that provides UDP/IPv4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetUdpStatistics failed.</exception>
		// Token: 0x06001F4E RID: 8014
		public abstract UdpStatistics GetUdpIPv4Statistics();

		/// <summary>Provides User Datagram Protocol/Internet Protocol version 6 (UDP/IPv6) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.UdpStatistics" /> object that provides UDP/IPv6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetUdpStatistics failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer is not running an operating system that supports IPv6. </exception>
		// Token: 0x06001F4F RID: 8015
		public abstract UdpStatistics GetUdpIPv6Statistics();

		/// <summary>Gets the Dynamic Host Configuration Protocol (DHCP) scope name.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the computer's DHCP scope name.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed. </exception>
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001F50 RID: 8016
		public abstract string DhcpScopeName { get; }

		/// <summary>Gets the domain in which the local computer is registered.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the computer's domain name. If the computer does not belong to a domain, returns <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed. </exception>
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001F51 RID: 8017
		public abstract string DomainName { get; }

		/// <summary>Gets the host name for the local computer.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the computer's NetBIOS name.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed. </exception>
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001F52 RID: 8018
		public abstract string HostName { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that specifies whether the local computer is acting as a Windows Internet Name Service (WINS) proxy.</summary>
		/// <returns>true if the local computer is a WINS proxy; otherwise, false.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed. </exception>
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001F53 RID: 8019
		public abstract bool IsWinsProxy { get; }

		/// <summary>Gets the Network Basic Input/Output System (NetBIOS) node type of the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetBiosNodeType" /> value.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed. </exception>
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001F54 RID: 8020
		public abstract NetBiosNodeType NodeType { get; }
	}
}
