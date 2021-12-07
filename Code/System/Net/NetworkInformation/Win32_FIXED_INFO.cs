using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003CB RID: 971
	[StructLayout(LayoutKind.Sequential)]
	internal class Win32_FIXED_INFO
	{
		// Token: 0x0600219C RID: 8604
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetNetworkParams(byte[] bytes, ref int size);

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x000627E8 File Offset: 0x000609E8
		public static Win32_FIXED_INFO Instance
		{
			get
			{
				if (Win32_FIXED_INFO.fixed_info == null)
				{
					Win32_FIXED_INFO.fixed_info = Win32_FIXED_INFO.GetInstance();
				}
				return Win32_FIXED_INFO.fixed_info;
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x00062804 File Offset: 0x00060A04
		private unsafe static Win32_FIXED_INFO GetInstance()
		{
			int num = 0;
			Win32_FIXED_INFO.GetNetworkParams(null, ref num);
			byte[] array = new byte[num];
			Win32_FIXED_INFO.GetNetworkParams(array, ref num);
			Win32_FIXED_INFO win32_FIXED_INFO = new Win32_FIXED_INFO();
			fixed (byte* value = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
			{
				Marshal.PtrToStructure((IntPtr)((void*)value), win32_FIXED_INFO);
			}
			return win32_FIXED_INFO;
		}

		// Token: 0x04001470 RID: 5232
		private const int MAX_HOSTNAME_LEN = 128;

		// Token: 0x04001471 RID: 5233
		private const int MAX_DOMAIN_NAME_LEN = 128;

		// Token: 0x04001472 RID: 5234
		private const int MAX_SCOPE_ID_LEN = 256;

		// Token: 0x04001473 RID: 5235
		private static Win32_FIXED_INFO fixed_info;

		// Token: 0x04001474 RID: 5236
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		public string HostName;

		// Token: 0x04001475 RID: 5237
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		public string DomainName;

		// Token: 0x04001476 RID: 5238
		public IntPtr CurrentDnsServer;

		// Token: 0x04001477 RID: 5239
		public Win32_IP_ADDR_STRING DnsServerList;

		// Token: 0x04001478 RID: 5240
		public NetBiosNodeType NodeType;

		// Token: 0x04001479 RID: 5241
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string ScopeId;

		// Token: 0x0400147A RID: 5242
		public uint EnableRouting;

		// Token: 0x0400147B RID: 5243
		public uint EnableProxy;

		// Token: 0x0400147C RID: 5244
		public uint EnableDns;
	}
}
