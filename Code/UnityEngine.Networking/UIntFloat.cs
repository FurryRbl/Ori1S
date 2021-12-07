using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Networking
{
	// Token: 0x02000036 RID: 54
	[StructLayout(LayoutKind.Explicit)]
	internal struct UIntFloat
	{
		// Token: 0x040000AC RID: 172
		[FieldOffset(0)]
		public float floatValue;

		// Token: 0x040000AD RID: 173
		[FieldOffset(0)]
		public uint intValue;

		// Token: 0x040000AE RID: 174
		[FieldOffset(0)]
		public double doubleValue;

		// Token: 0x040000AF RID: 175
		[FieldOffset(0)]
		public ulong longValue;
	}
}
