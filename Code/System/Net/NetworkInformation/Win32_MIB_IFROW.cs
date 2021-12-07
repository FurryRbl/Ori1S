using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003CF RID: 975
	internal struct Win32_MIB_IFROW
	{
		// Token: 0x040014AB RID: 5291
		private const int MAX_INTERFACE_NAME_LEN = 256;

		// Token: 0x040014AC RID: 5292
		private const int MAXLEN_PHYSADDR = 8;

		// Token: 0x040014AD RID: 5293
		private const int MAXLEN_IFDESCR = 256;

		// Token: 0x040014AE RID: 5294
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		public char[] Name;

		// Token: 0x040014AF RID: 5295
		public int Index;

		// Token: 0x040014B0 RID: 5296
		public NetworkInterfaceType Type;

		// Token: 0x040014B1 RID: 5297
		public int Mtu;

		// Token: 0x040014B2 RID: 5298
		public uint Speed;

		// Token: 0x040014B3 RID: 5299
		public int PhysAddrLen;

		// Token: 0x040014B4 RID: 5300
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] PhysAddr;

		// Token: 0x040014B5 RID: 5301
		public uint AdminStatus;

		// Token: 0x040014B6 RID: 5302
		public uint OperStatus;

		// Token: 0x040014B7 RID: 5303
		public uint LastChange;

		// Token: 0x040014B8 RID: 5304
		public int InOctets;

		// Token: 0x040014B9 RID: 5305
		public int InUcastPkts;

		// Token: 0x040014BA RID: 5306
		public int InNUcastPkts;

		// Token: 0x040014BB RID: 5307
		public int InDiscards;

		// Token: 0x040014BC RID: 5308
		public int InErrors;

		// Token: 0x040014BD RID: 5309
		public int InUnknownProtos;

		// Token: 0x040014BE RID: 5310
		public int OutOctets;

		// Token: 0x040014BF RID: 5311
		public int OutUcastPkts;

		// Token: 0x040014C0 RID: 5312
		public int OutNUcastPkts;

		// Token: 0x040014C1 RID: 5313
		public int OutDiscards;

		// Token: 0x040014C2 RID: 5314
		public int OutErrors;

		// Token: 0x040014C3 RID: 5315
		public int OutQLen;

		// Token: 0x040014C4 RID: 5316
		public int DescrLen;

		// Token: 0x040014C5 RID: 5317
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public byte[] Descr;
	}
}
