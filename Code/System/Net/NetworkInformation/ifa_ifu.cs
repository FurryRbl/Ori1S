using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200038F RID: 911
	[StructLayout(LayoutKind.Explicit)]
	internal struct ifa_ifu
	{
		// Token: 0x0400137C RID: 4988
		[FieldOffset(0)]
		public IntPtr ifu_broadaddr;

		// Token: 0x0400137D RID: 4989
		[FieldOffset(0)]
		public IntPtr ifu_dstaddr;
	}
}
