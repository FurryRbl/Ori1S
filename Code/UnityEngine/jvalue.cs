using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020000D3 RID: 211
	[StructLayout(LayoutKind.Explicit)]
	public struct jvalue
	{
		// Token: 0x04000273 RID: 627
		[FieldOffset(0)]
		public bool z;

		// Token: 0x04000274 RID: 628
		[FieldOffset(0)]
		public byte b;

		// Token: 0x04000275 RID: 629
		[FieldOffset(0)]
		public char c;

		// Token: 0x04000276 RID: 630
		[FieldOffset(0)]
		public short s;

		// Token: 0x04000277 RID: 631
		[FieldOffset(0)]
		public int i;

		// Token: 0x04000278 RID: 632
		[FieldOffset(0)]
		public long j;

		// Token: 0x04000279 RID: 633
		[FieldOffset(0)]
		public float f;

		// Token: 0x0400027A RID: 634
		[FieldOffset(0)]
		public double d;

		// Token: 0x0400027B RID: 635
		[FieldOffset(0)]
		public IntPtr l;
	}
}
