using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000464 RID: 1124
	internal enum Position : ushort
	{
		// Token: 0x040018F2 RID: 6386
		Any,
		// Token: 0x040018F3 RID: 6387
		Start,
		// Token: 0x040018F4 RID: 6388
		StartOfString,
		// Token: 0x040018F5 RID: 6389
		StartOfLine,
		// Token: 0x040018F6 RID: 6390
		StartOfScan,
		// Token: 0x040018F7 RID: 6391
		End,
		// Token: 0x040018F8 RID: 6392
		EndOfString,
		// Token: 0x040018F9 RID: 6393
		EndOfLine,
		// Token: 0x040018FA RID: 6394
		Boundary,
		// Token: 0x040018FB RID: 6395
		NonBoundary
	}
}
