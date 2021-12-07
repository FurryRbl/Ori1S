using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003CC RID: 972
	[StructLayout(LayoutKind.Explicit)]
	internal struct AlignmentUnion
	{
		// Token: 0x0400147D RID: 5245
		[FieldOffset(0)]
		public ulong Alignment;

		// Token: 0x0400147E RID: 5246
		[FieldOffset(0)]
		public int Length;

		// Token: 0x0400147F RID: 5247
		[FieldOffset(4)]
		public int IfIndex;
	}
}
