using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003CE RID: 974
	[StructLayout(LayoutKind.Sequential)]
	internal class Win32_IP_ADAPTER_INFO
	{
		// Token: 0x04001496 RID: 5270
		private const int MAX_ADAPTER_NAME_LENGTH = 256;

		// Token: 0x04001497 RID: 5271
		private const int MAX_ADAPTER_DESCRIPTION_LENGTH = 128;

		// Token: 0x04001498 RID: 5272
		private const int MAX_ADAPTER_ADDRESS_LENGTH = 8;

		// Token: 0x04001499 RID: 5273
		public IntPtr Next;

		// Token: 0x0400149A RID: 5274
		public int ComboIndex;

		// Token: 0x0400149B RID: 5275
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string AdapterName;

		// Token: 0x0400149C RID: 5276
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		public string Description;

		// Token: 0x0400149D RID: 5277
		public uint AddressLength;

		// Token: 0x0400149E RID: 5278
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] Address;

		// Token: 0x0400149F RID: 5279
		public uint Index;

		// Token: 0x040014A0 RID: 5280
		public uint Type;

		// Token: 0x040014A1 RID: 5281
		public uint DhcpEnabled;

		// Token: 0x040014A2 RID: 5282
		public IntPtr CurrentIpAddress;

		// Token: 0x040014A3 RID: 5283
		public Win32_IP_ADDR_STRING IpAddressList;

		// Token: 0x040014A4 RID: 5284
		public Win32_IP_ADDR_STRING GatewayList;

		// Token: 0x040014A5 RID: 5285
		public Win32_IP_ADDR_STRING DhcpServer;

		// Token: 0x040014A6 RID: 5286
		public bool HaveWins;

		// Token: 0x040014A7 RID: 5287
		public Win32_IP_ADDR_STRING PrimaryWinsServer;

		// Token: 0x040014A8 RID: 5288
		public Win32_IP_ADDR_STRING SecondaryWinsServer;

		// Token: 0x040014A9 RID: 5289
		public long LeaseObtained;

		// Token: 0x040014AA RID: 5290
		public long LeaseExpires;
	}
}
