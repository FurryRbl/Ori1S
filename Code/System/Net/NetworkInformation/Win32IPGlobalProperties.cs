using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000373 RID: 883
	internal class Win32IPGlobalProperties : IPGlobalProperties
	{
		// Token: 0x06001F70 RID: 8048 RVA: 0x0005E418 File Offset: 0x0005C618
		private unsafe void FillTcpTable(out List<Win32IPGlobalProperties.Win32_MIB_TCPROW> tab4, out List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> tab6)
		{
			tab4 = new List<Win32IPGlobalProperties.Win32_MIB_TCPROW>();
			int num = 0;
			Win32IPGlobalProperties.GetTcpTable(null, ref num, true);
			byte[] array = new byte[num];
			Win32IPGlobalProperties.GetTcpTable(array, ref num, true);
			int num2 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_TCPROW));
			fixed (byte* ptr = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
			{
				int num3 = Marshal.ReadInt32((IntPtr)((void*)ptr));
				for (int i = 0; i < num3; i++)
				{
					Win32IPGlobalProperties.Win32_MIB_TCPROW win32_MIB_TCPROW = new Win32IPGlobalProperties.Win32_MIB_TCPROW();
					Marshal.PtrToStructure((IntPtr)((void*)(ptr + i * num2 + 4)), win32_MIB_TCPROW);
					tab4.Add(win32_MIB_TCPROW);
				}
			}
			tab6 = new List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW>();
			if (Environment.OSVersion.Version.Major >= 6)
			{
				int num4 = 0;
				Win32IPGlobalProperties.GetTcp6Table(null, ref num4, true);
				byte[] array2 = new byte[num4];
				Win32IPGlobalProperties.GetTcp6Table(array2, ref num4, true);
				int num5 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_TCP6ROW));
				fixed (byte* ptr2 = ref (array2 != null && array2.Length != 0) ? ref array2[0] : ref *null)
				{
					int num6 = Marshal.ReadInt32((IntPtr)((void*)ptr2));
					for (int j = 0; j < num6; j++)
					{
						Win32IPGlobalProperties.Win32_MIB_TCP6ROW win32_MIB_TCP6ROW = new Win32IPGlobalProperties.Win32_MIB_TCP6ROW();
						Marshal.PtrToStructure((IntPtr)((void*)(ptr2 + j * num5 + 4)), win32_MIB_TCP6ROW);
						tab6.Add(win32_MIB_TCP6ROW);
					}
				}
			}
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0005E580 File Offset: 0x0005C780
		private bool IsListenerState(TcpState state)
		{
			switch (state)
			{
			case TcpState.Listen:
			case TcpState.SynSent:
			case TcpState.FinWait1:
			case TcpState.FinWait2:
			case TcpState.CloseWait:
				return true;
			}
			return false;
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x0005E5BC File Offset: 0x0005C7BC
		public override TcpConnectionInformation[] GetActiveTcpConnections()
		{
			List<Win32IPGlobalProperties.Win32_MIB_TCPROW> list = null;
			List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> list2 = null;
			this.FillTcpTable(out list, out list2);
			int count = list.Count;
			TcpConnectionInformation[] array = new TcpConnectionInformation[count + list2.Count];
			for (int i = 0; i < count; i++)
			{
				array[i] = list[i].TcpInfo;
			}
			for (int j = 0; j < list2.Count; j++)
			{
				array[count + j] = list2[j].TcpInfo;
			}
			return array;
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0005E644 File Offset: 0x0005C844
		public override IPEndPoint[] GetActiveTcpListeners()
		{
			List<Win32IPGlobalProperties.Win32_MIB_TCPROW> list = null;
			List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> list2 = null;
			this.FillTcpTable(out list, out list2);
			List<IPEndPoint> list3 = new List<IPEndPoint>();
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if (this.IsListenerState(list[i].State))
				{
					list3.Add(list[i].LocalEndPoint);
				}
				i++;
			}
			int j = 0;
			int count2 = list2.Count;
			while (j < count2)
			{
				if (this.IsListenerState(list2[j].State))
				{
					list3.Add(list2[j].LocalEndPoint);
				}
				j++;
			}
			return list3.ToArray();
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0005E6FC File Offset: 0x0005C8FC
		public unsafe override IPEndPoint[] GetActiveUdpListeners()
		{
			List<IPEndPoint> list = new List<IPEndPoint>();
			int num = 0;
			Win32IPGlobalProperties.GetUdpTable(null, ref num, true);
			byte[] array = new byte[num];
			Win32IPGlobalProperties.GetUdpTable(array, ref num, true);
			int num2 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_UDPROW));
			fixed (byte* ptr = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
			{
				int num3 = Marshal.ReadInt32((IntPtr)((void*)ptr));
				for (int i = 0; i < num3; i++)
				{
					Win32IPGlobalProperties.Win32_MIB_UDPROW win32_MIB_UDPROW = new Win32IPGlobalProperties.Win32_MIB_UDPROW();
					Marshal.PtrToStructure((IntPtr)((void*)(ptr + i * num2 + 4)), win32_MIB_UDPROW);
					list.Add(win32_MIB_UDPROW.LocalEndPoint);
				}
			}
			if (Environment.OSVersion.Version.Major >= 6)
			{
				int num4 = 0;
				Win32IPGlobalProperties.GetUdp6Table(null, ref num4, true);
				byte[] array2 = new byte[num4];
				Win32IPGlobalProperties.GetUdp6Table(array2, ref num4, true);
				int num5 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_UDP6ROW));
				fixed (byte* ptr2 = ref (array2 != null && array2.Length != 0) ? ref array2[0] : ref *null)
				{
					int num6 = Marshal.ReadInt32((IntPtr)((void*)ptr2));
					for (int j = 0; j < num6; j++)
					{
						Win32IPGlobalProperties.Win32_MIB_UDP6ROW win32_MIB_UDP6ROW = new Win32IPGlobalProperties.Win32_MIB_UDP6ROW();
						Marshal.PtrToStructure((IntPtr)((void*)(ptr2 + j * num5 + 4)), win32_MIB_UDP6ROW);
						list.Add(win32_MIB_UDP6ROW.LocalEndPoint);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0005E874 File Offset: 0x0005CA74
		public override IcmpV4Statistics GetIcmpV4Statistics()
		{
			if (!System.Net.Sockets.Socket.SupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIBICMPINFO info;
			Win32IPGlobalProperties.GetIcmpStatistics(out info, 2);
			return new Win32IcmpV4Statistics(info);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0005E8A0 File Offset: 0x0005CAA0
		public override IcmpV6Statistics GetIcmpV6Statistics()
		{
			if (!System.Net.Sockets.Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_ICMP_EX info;
			Win32IPGlobalProperties.GetIcmpStatisticsEx(out info, 23);
			return new Win32IcmpV6Statistics(info);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0005E8D0 File Offset: 0x0005CAD0
		public override IPGlobalStatistics GetIPv4GlobalStatistics()
		{
			if (!System.Net.Sockets.Socket.SupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_IPSTATS info;
			Win32IPGlobalProperties.GetIPStatisticsEx(out info, 2);
			return new Win32IPGlobalStatistics(info);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0005E8FC File Offset: 0x0005CAFC
		public override IPGlobalStatistics GetIPv6GlobalStatistics()
		{
			if (!System.Net.Sockets.Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_IPSTATS info;
			Win32IPGlobalProperties.GetIPStatisticsEx(out info, 23);
			return new Win32IPGlobalStatistics(info);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0005E92C File Offset: 0x0005CB2C
		public override TcpStatistics GetTcpIPv4Statistics()
		{
			if (!System.Net.Sockets.Socket.SupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_TCPSTATS info;
			Win32IPGlobalProperties.GetTcpStatisticsEx(out info, 2);
			return new Win32TcpStatistics(info);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0005E958 File Offset: 0x0005CB58
		public override TcpStatistics GetTcpIPv6Statistics()
		{
			if (!System.Net.Sockets.Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_TCPSTATS info;
			Win32IPGlobalProperties.GetTcpStatisticsEx(out info, 23);
			return new Win32TcpStatistics(info);
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0005E988 File Offset: 0x0005CB88
		public override UdpStatistics GetUdpIPv4Statistics()
		{
			if (!System.Net.Sockets.Socket.SupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_UDPSTATS info;
			Win32IPGlobalProperties.GetUdpStatisticsEx(out info, 2);
			return new Win32UdpStatistics(info);
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x0005E9B4 File Offset: 0x0005CBB4
		public override UdpStatistics GetUdpIPv6Statistics()
		{
			if (!System.Net.Sockets.Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_UDPSTATS info;
			Win32IPGlobalProperties.GetUdpStatisticsEx(out info, 23);
			return new Win32UdpStatistics(info);
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x0005E9E4 File Offset: 0x0005CBE4
		public override string DhcpScopeName
		{
			get
			{
				return Win32_FIXED_INFO.Instance.ScopeId;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x0005E9F0 File Offset: 0x0005CBF0
		public override string DomainName
		{
			get
			{
				return Win32_FIXED_INFO.Instance.DomainName;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x0005E9FC File Offset: 0x0005CBFC
		public override string HostName
		{
			get
			{
				return Win32_FIXED_INFO.Instance.HostName;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x0005EA08 File Offset: 0x0005CC08
		public override bool IsWinsProxy
		{
			get
			{
				return Win32_FIXED_INFO.Instance.EnableProxy != 0U;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x0005EA1C File Offset: 0x0005CC1C
		public override NetBiosNodeType NodeType
		{
			get
			{
				return Win32_FIXED_INFO.Instance.NodeType;
			}
		}

		// Token: 0x06001F82 RID: 8066
		[DllImport("Iphlpapi.dll")]
		private static extern int GetTcpTable(byte[] pTcpTable, ref int pdwSize, bool bOrder);

		// Token: 0x06001F83 RID: 8067
		[DllImport("Iphlpapi.dll")]
		private static extern int GetTcp6Table(byte[] TcpTable, ref int SizePointer, bool Order);

		// Token: 0x06001F84 RID: 8068
		[DllImport("Iphlpapi.dll")]
		private static extern int GetUdpTable(byte[] pUdpTable, ref int pdwSize, bool bOrder);

		// Token: 0x06001F85 RID: 8069
		[DllImport("Iphlpapi.dll")]
		private static extern int GetUdp6Table(byte[] Udp6Table, ref int SizePointer, bool Order);

		// Token: 0x06001F86 RID: 8070
		[DllImport("Iphlpapi.dll")]
		private static extern int GetTcpStatisticsEx(out Win32_MIB_TCPSTATS pStats, int dwFamily);

		// Token: 0x06001F87 RID: 8071
		[DllImport("Iphlpapi.dll")]
		private static extern int GetUdpStatisticsEx(out Win32_MIB_UDPSTATS pStats, int dwFamily);

		// Token: 0x06001F88 RID: 8072
		[DllImport("Iphlpapi.dll")]
		private static extern int GetIcmpStatistics(out Win32_MIBICMPINFO pStats, int dwFamily);

		// Token: 0x06001F89 RID: 8073
		[DllImport("Iphlpapi.dll")]
		private static extern int GetIcmpStatisticsEx(out Win32_MIB_ICMP_EX pStats, int dwFamily);

		// Token: 0x06001F8A RID: 8074
		[DllImport("Iphlpapi.dll")]
		private static extern int GetIPStatisticsEx(out Win32_MIB_IPSTATS pStats, int dwFamily);

		// Token: 0x0400131E RID: 4894
		public const int AF_INET = 2;

		// Token: 0x0400131F RID: 4895
		public const int AF_INET6 = 23;

		// Token: 0x02000374 RID: 884
		[StructLayout(LayoutKind.Explicit)]
		private struct Win32_IN6_ADDR
		{
			// Token: 0x04001320 RID: 4896
			[FieldOffset(0)]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] Bytes;
		}

		// Token: 0x02000375 RID: 885
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_TCPROW
		{
			// Token: 0x17000853 RID: 2131
			// (get) Token: 0x06001F8C RID: 8076 RVA: 0x0005EA30 File Offset: 0x0005CC30
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint((long)((ulong)this.LocalAddr), this.LocalPort);
				}
			}

			// Token: 0x17000854 RID: 2132
			// (get) Token: 0x06001F8D RID: 8077 RVA: 0x0005EA44 File Offset: 0x0005CC44
			public IPEndPoint RemoteEndPoint
			{
				get
				{
					return new IPEndPoint((long)((ulong)this.RemoteAddr), this.RemotePort);
				}
			}

			// Token: 0x17000855 RID: 2133
			// (get) Token: 0x06001F8E RID: 8078 RVA: 0x0005EA58 File Offset: 0x0005CC58
			public TcpConnectionInformation TcpInfo
			{
				get
				{
					return new TcpConnectionInformationImpl(this.LocalEndPoint, this.RemoteEndPoint, this.State);
				}
			}

			// Token: 0x04001321 RID: 4897
			public TcpState State;

			// Token: 0x04001322 RID: 4898
			public uint LocalAddr;

			// Token: 0x04001323 RID: 4899
			public int LocalPort;

			// Token: 0x04001324 RID: 4900
			public uint RemoteAddr;

			// Token: 0x04001325 RID: 4901
			public int RemotePort;
		}

		// Token: 0x02000376 RID: 886
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_TCP6ROW
		{
			// Token: 0x17000856 RID: 2134
			// (get) Token: 0x06001F90 RID: 8080 RVA: 0x0005EA7C File Offset: 0x0005CC7C
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint(new IPAddress(this.LocalAddr.Bytes, (long)((ulong)this.LocalScopeId)), this.LocalPort);
				}
			}

			// Token: 0x17000857 RID: 2135
			// (get) Token: 0x06001F91 RID: 8081 RVA: 0x0005EAAC File Offset: 0x0005CCAC
			public IPEndPoint RemoteEndPoint
			{
				get
				{
					return new IPEndPoint(new IPAddress(this.RemoteAddr.Bytes, (long)((ulong)this.RemoteScopeId)), this.RemotePort);
				}
			}

			// Token: 0x17000858 RID: 2136
			// (get) Token: 0x06001F92 RID: 8082 RVA: 0x0005EADC File Offset: 0x0005CCDC
			public TcpConnectionInformation TcpInfo
			{
				get
				{
					return new TcpConnectionInformationImpl(this.LocalEndPoint, this.RemoteEndPoint, this.State);
				}
			}

			// Token: 0x04001326 RID: 4902
			public TcpState State;

			// Token: 0x04001327 RID: 4903
			public Win32IPGlobalProperties.Win32_IN6_ADDR LocalAddr;

			// Token: 0x04001328 RID: 4904
			public uint LocalScopeId;

			// Token: 0x04001329 RID: 4905
			public int LocalPort;

			// Token: 0x0400132A RID: 4906
			public Win32IPGlobalProperties.Win32_IN6_ADDR RemoteAddr;

			// Token: 0x0400132B RID: 4907
			public uint RemoteScopeId;

			// Token: 0x0400132C RID: 4908
			public int RemotePort;
		}

		// Token: 0x02000377 RID: 887
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_UDPROW
		{
			// Token: 0x17000859 RID: 2137
			// (get) Token: 0x06001F94 RID: 8084 RVA: 0x0005EB00 File Offset: 0x0005CD00
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint((long)((ulong)this.LocalAddr), this.LocalPort);
				}
			}

			// Token: 0x0400132D RID: 4909
			public uint LocalAddr;

			// Token: 0x0400132E RID: 4910
			public int LocalPort;
		}

		// Token: 0x02000378 RID: 888
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_UDP6ROW
		{
			// Token: 0x1700085A RID: 2138
			// (get) Token: 0x06001F96 RID: 8086 RVA: 0x0005EB1C File Offset: 0x0005CD1C
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint(new IPAddress(this.LocalAddr.Bytes, (long)((ulong)this.LocalScopeId)), this.LocalPort);
				}
			}

			// Token: 0x0400132F RID: 4911
			public Win32IPGlobalProperties.Win32_IN6_ADDR LocalAddr;

			// Token: 0x04001330 RID: 4912
			public uint LocalScopeId;

			// Token: 0x04001331 RID: 4913
			public int LocalPort;
		}
	}
}
