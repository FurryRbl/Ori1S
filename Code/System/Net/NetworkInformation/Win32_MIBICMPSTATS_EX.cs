using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200036A RID: 874
	internal struct Win32_MIBICMPSTATS_EX
	{
		// Token: 0x04001309 RID: 4873
		public uint Msgs;

		// Token: 0x0400130A RID: 4874
		public uint Errors;

		// Token: 0x0400130B RID: 4875
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public uint[] Counts;
	}
}
