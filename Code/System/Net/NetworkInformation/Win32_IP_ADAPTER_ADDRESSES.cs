using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003CD RID: 973
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class Win32_IP_ADAPTER_ADDRESSES
	{
		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060021A0 RID: 8608 RVA: 0x0006286C File Offset: 0x00060A6C
		public bool DdnsEnabled
		{
			get
			{
				return (this.Flags & 1U) != 0U;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x0006287C File Offset: 0x00060A7C
		public bool IsReceiveOnly
		{
			get
			{
				return (this.Flags & 8U) != 0U;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x0006288C File Offset: 0x00060A8C
		public bool NoMulticast
		{
			get
			{
				return (this.Flags & 16U) != 0U;
			}
		}

		// Token: 0x04001480 RID: 5248
		private const int MAX_ADAPTER_ADDRESS_LENGTH = 8;

		// Token: 0x04001481 RID: 5249
		private const int IP_ADAPTER_DDNS_ENABLED = 1;

		// Token: 0x04001482 RID: 5250
		private const int IP_ADAPTER_RECEIVE_ONLY = 8;

		// Token: 0x04001483 RID: 5251
		private const int IP_ADAPTER_NO_MULTICAST = 16;

		// Token: 0x04001484 RID: 5252
		public AlignmentUnion Alignment;

		// Token: 0x04001485 RID: 5253
		public IntPtr Next;

		// Token: 0x04001486 RID: 5254
		[MarshalAs(UnmanagedType.LPStr)]
		public string AdapterName;

		// Token: 0x04001487 RID: 5255
		public IntPtr FirstUnicastAddress;

		// Token: 0x04001488 RID: 5256
		public IntPtr FirstAnycastAddress;

		// Token: 0x04001489 RID: 5257
		public IntPtr FirstMulticastAddress;

		// Token: 0x0400148A RID: 5258
		public IntPtr FirstDnsServerAddress;

		// Token: 0x0400148B RID: 5259
		public string DnsSuffix;

		// Token: 0x0400148C RID: 5260
		public string Description;

		// Token: 0x0400148D RID: 5261
		public string FriendlyName;

		// Token: 0x0400148E RID: 5262
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] PhysicalAddress;

		// Token: 0x0400148F RID: 5263
		public uint PhysicalAddressLength;

		// Token: 0x04001490 RID: 5264
		public uint Flags;

		// Token: 0x04001491 RID: 5265
		public uint Mtu;

		// Token: 0x04001492 RID: 5266
		public NetworkInterfaceType IfType;

		// Token: 0x04001493 RID: 5267
		public OperationalStatus OperStatus;

		// Token: 0x04001494 RID: 5268
		public int Ipv6IfIndex;

		// Token: 0x04001495 RID: 5269
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		public uint[] ZoneIndices;
	}
}
