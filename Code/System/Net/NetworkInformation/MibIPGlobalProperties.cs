using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000372 RID: 882
	internal class MibIPGlobalProperties : IPGlobalProperties
	{
		// Token: 0x06001F55 RID: 8021 RVA: 0x0005DCF4 File Offset: 0x0005BEF4
		public MibIPGlobalProperties(string procDir)
		{
			this.StatisticsFile = Path.Combine(procDir, "net/snmp");
			this.StatisticsFileIPv6 = Path.Combine(procDir, "net/snmp6");
			this.TcpFile = Path.Combine(procDir, "net/tcp");
			this.Tcp6File = Path.Combine(procDir, "net/tcp6");
			this.UdpFile = Path.Combine(procDir, "net/udp");
			this.Udp6File = Path.Combine(procDir, "net/udp6");
		}

		// Token: 0x06001F57 RID: 8023
		[DllImport("libc")]
		private static extern int gethostname([MarshalAs(UnmanagedType.LPArray, SizeConst = 0, SizeParamIndex = 1)] byte[] name, int len);

		// Token: 0x06001F58 RID: 8024
		[DllImport("libc")]
		private static extern int getdomainname([MarshalAs(UnmanagedType.LPArray, SizeConst = 0, SizeParamIndex = 1)] byte[] name, int len);

		// Token: 0x06001F59 RID: 8025 RVA: 0x0005DD88 File Offset: 0x0005BF88
		private System.Collections.Specialized.StringDictionary GetProperties4(string item)
		{
			string statisticsFile = this.StatisticsFile;
			string text = item + ": ";
			System.Collections.Specialized.StringDictionary result;
			using (StreamReader streamReader = new StreamReader(statisticsFile, Encoding.ASCII))
			{
				string[] array = null;
				string[] array2 = null;
				string text2 = string.Empty;
				for (;;)
				{
					text2 = streamReader.ReadLine();
					if (!string.IsNullOrEmpty(text2))
					{
						if (text2.Length > text.Length && string.CompareOrdinal(text2, 0, text, 0, text.Length) == 0)
						{
							if (array != null)
							{
								break;
							}
							array = text2.Substring(text.Length).Split(new char[]
							{
								' '
							});
						}
					}
					if (streamReader.EndOfStream)
					{
						goto IL_E2;
					}
				}
				if (array2 != null)
				{
					throw this.CreateException(statisticsFile, string.Format("Found duplicate line for values for the same item '{0}'", item));
				}
				array2 = text2.Substring(text.Length).Split(new char[]
				{
					' '
				});
				IL_E2:
				if (array2 == null)
				{
					throw this.CreateException(statisticsFile, string.Format("No corresponding line was not found for '{0}'", item));
				}
				if (array.Length != array2.Length)
				{
					throw this.CreateException(statisticsFile, string.Format("The counts in the header line and the value line do not match for '{0}'", item));
				}
				System.Collections.Specialized.StringDictionary stringDictionary = new System.Collections.Specialized.StringDictionary();
				for (int i = 0; i < array.Length; i++)
				{
					stringDictionary[array[i]] = array2[i];
				}
				result = stringDictionary;
			}
			return result;
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x0005DF18 File Offset: 0x0005C118
		private System.Collections.Specialized.StringDictionary GetProperties6(string item)
		{
			if (!File.Exists(this.StatisticsFileIPv6))
			{
				throw new NetworkInformationException();
			}
			string statisticsFileIPv = this.StatisticsFileIPv6;
			System.Collections.Specialized.StringDictionary result;
			using (StreamReader streamReader = new StreamReader(statisticsFileIPv, Encoding.ASCII))
			{
				System.Collections.Specialized.StringDictionary stringDictionary = new System.Collections.Specialized.StringDictionary();
				string text = string.Empty;
				for (;;)
				{
					text = streamReader.ReadLine();
					if (!string.IsNullOrEmpty(text))
					{
						if (text.Length > item.Length && string.CompareOrdinal(text, 0, item, 0, item.Length) == 0)
						{
							int num = text.IndexOfAny(MibIPGlobalProperties.wsChars, item.Length);
							if (num < 0)
							{
								break;
							}
							stringDictionary[text.Substring(item.Length, num - item.Length)] = text.Substring(num + 1).Trim(MibIPGlobalProperties.wsChars);
						}
					}
					if (streamReader.EndOfStream)
					{
						goto Block_7;
					}
				}
				throw this.CreateException(statisticsFileIPv, null);
				Block_7:
				result = stringDictionary;
			}
			return result;
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x0005E03C File Offset: 0x0005C23C
		private Exception CreateException(string file, string msg)
		{
			return new InvalidOperationException(string.Format("Unsupported (unexpected) '{0}' file format. ", file) + msg);
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0005E054 File Offset: 0x0005C254
		private IPEndPoint[] GetLocalAddresses(List<string[]> list)
		{
			IPEndPoint[] array = new IPEndPoint[list.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.ToEndpoint(list[i][1]);
			}
			return array;
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0005E094 File Offset: 0x0005C294
		private IPEndPoint ToEndpoint(string s)
		{
			int num = s.IndexOf(':');
			int port = int.Parse(s.Substring(num + 1), NumberStyles.HexNumber);
			if (s.Length == 13)
			{
				return new IPEndPoint(long.Parse(s.Substring(0, num), NumberStyles.HexNumber), port);
			}
			byte[] array = new byte[16];
			int num2 = 0;
			while (num2 << 1 < num)
			{
				array[num2] = byte.Parse(s.Substring(num2 << 1, 2), NumberStyles.HexNumber);
				num2++;
			}
			return new IPEndPoint(new IPAddress(array), port);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0005E124 File Offset: 0x0005C324
		private void GetRows(string file, List<string[]> list)
		{
			if (!File.Exists(file))
			{
				return;
			}
			using (StreamReader streamReader = new StreamReader(file, Encoding.ASCII))
			{
				streamReader.ReadLine();
				while (!streamReader.EndOfStream)
				{
					string[] array = streamReader.ReadLine().Split(MibIPGlobalProperties.wsChars, StringSplitOptions.RemoveEmptyEntries);
					if (array.Length < 4)
					{
						throw this.CreateException(file, null);
					}
					list.Add(array);
				}
			}
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0005E1BC File Offset: 0x0005C3BC
		public override TcpConnectionInformation[] GetActiveTcpConnections()
		{
			List<string[]> list = new List<string[]>();
			this.GetRows(this.TcpFile, list);
			this.GetRows(this.Tcp6File, list);
			TcpConnectionInformation[] array = new TcpConnectionInformation[list.Count];
			for (int i = 0; i < array.Length; i++)
			{
				IPEndPoint local = this.ToEndpoint(list[i][1]);
				IPEndPoint remote = this.ToEndpoint(list[i][2]);
				TcpState state = (TcpState)int.Parse(list[i][3], NumberStyles.HexNumber);
				array[i] = new TcpConnectionInformationImpl(local, remote, state);
			}
			return array;
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x0005E250 File Offset: 0x0005C450
		public override IPEndPoint[] GetActiveTcpListeners()
		{
			List<string[]> list = new List<string[]>();
			this.GetRows(this.TcpFile, list);
			this.GetRows(this.Tcp6File, list);
			return this.GetLocalAddresses(list);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0005E284 File Offset: 0x0005C484
		public override IPEndPoint[] GetActiveUdpListeners()
		{
			List<string[]> list = new List<string[]>();
			this.GetRows(this.UdpFile, list);
			this.GetRows(this.Udp6File, list);
			return this.GetLocalAddresses(list);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0005E2B8 File Offset: 0x0005C4B8
		public override IcmpV4Statistics GetIcmpV4Statistics()
		{
			return new MibIcmpV4Statistics(this.GetProperties4("Icmp"));
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x0005E2CC File Offset: 0x0005C4CC
		public override IcmpV6Statistics GetIcmpV6Statistics()
		{
			return new MibIcmpV6Statistics(this.GetProperties6("Icmp6"));
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x0005E2E0 File Offset: 0x0005C4E0
		public override IPGlobalStatistics GetIPv4GlobalStatistics()
		{
			return new MibIPGlobalStatistics(this.GetProperties4("Ip"));
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x0005E2F4 File Offset: 0x0005C4F4
		public override IPGlobalStatistics GetIPv6GlobalStatistics()
		{
			return new MibIPGlobalStatistics(this.GetProperties6("Ip6"));
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0005E308 File Offset: 0x0005C508
		public override TcpStatistics GetTcpIPv4Statistics()
		{
			return new MibTcpStatistics(this.GetProperties4("Tcp"));
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x0005E31C File Offset: 0x0005C51C
		public override TcpStatistics GetTcpIPv6Statistics()
		{
			return new MibTcpStatistics(this.GetProperties4("Tcp"));
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0005E330 File Offset: 0x0005C530
		public override UdpStatistics GetUdpIPv4Statistics()
		{
			return new MibUdpStatistics(this.GetProperties4("Udp"));
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0005E344 File Offset: 0x0005C544
		public override UdpStatistics GetUdpIPv6Statistics()
		{
			return new MibUdpStatistics(this.GetProperties6("Udp6"));
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x0005E358 File Offset: 0x0005C558
		public override string DhcpScopeName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x0005E360 File Offset: 0x0005C560
		public override string DomainName
		{
			get
			{
				byte[] array = new byte[256];
				if (MibIPGlobalProperties.getdomainname(array, 256) != 0)
				{
					throw new NetworkInformationException();
				}
				int num = Array.IndexOf<byte>(array, 0);
				return Encoding.ASCII.GetString(array, 0, (num >= 0) ? num : 256);
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x0005E3B4 File Offset: 0x0005C5B4
		public override string HostName
		{
			get
			{
				byte[] array = new byte[256];
				if (MibIPGlobalProperties.gethostname(array, 256) != 0)
				{
					throw new NetworkInformationException();
				}
				int num = Array.IndexOf<byte>(array, 0);
				return Encoding.ASCII.GetString(array, 0, (num >= 0) ? num : 256);
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x0005E408 File Offset: 0x0005C608
		public override bool IsWinsProxy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x0005E40C File Offset: 0x0005C60C
		public override NetBiosNodeType NodeType
		{
			get
			{
				return NetBiosNodeType.Unknown;
			}
		}

		// Token: 0x04001315 RID: 4885
		public const string ProcDir = "/proc";

		// Token: 0x04001316 RID: 4886
		public const string CompatProcDir = "/usr/compat/linux/proc";

		// Token: 0x04001317 RID: 4887
		public readonly string StatisticsFile;

		// Token: 0x04001318 RID: 4888
		public readonly string StatisticsFileIPv6;

		// Token: 0x04001319 RID: 4889
		public readonly string TcpFile;

		// Token: 0x0400131A RID: 4890
		public readonly string Tcp6File;

		// Token: 0x0400131B RID: 4891
		public readonly string UdpFile;

		// Token: 0x0400131C RID: 4892
		public readonly string Udp6File;

		// Token: 0x0400131D RID: 4893
		private static readonly char[] wsChars = new char[]
		{
			' ',
			'\t'
		};
	}
}
