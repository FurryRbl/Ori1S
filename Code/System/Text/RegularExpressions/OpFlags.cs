using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000463 RID: 1123
	[Flags]
	internal enum OpFlags : ushort
	{
		// Token: 0x040018EC RID: 6380
		None = 0,
		// Token: 0x040018ED RID: 6381
		Negate = 256,
		// Token: 0x040018EE RID: 6382
		IgnoreCase = 512,
		// Token: 0x040018EF RID: 6383
		RightToLeft = 1024,
		// Token: 0x040018F0 RID: 6384
		Lazy = 2048
	}
}
