using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000388 RID: 904
	[StructLayout(LayoutKind.Sequential)]
	internal class Win32_IP_PER_ADAPTER_INFO
	{
		// Token: 0x04001374 RID: 4980
		public uint AutoconfigEnabled;

		// Token: 0x04001375 RID: 4981
		public uint AutoconfigActive;

		// Token: 0x04001376 RID: 4982
		public IntPtr CurrentDnsServer;

		// Token: 0x04001377 RID: 4983
		public Win32_IP_ADDR_STRING DnsServerList;
	}
}
