using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000462 RID: 1122
	internal enum OpCode : ushort
	{
		// Token: 0x040018D2 RID: 6354
		False,
		// Token: 0x040018D3 RID: 6355
		True,
		// Token: 0x040018D4 RID: 6356
		Position,
		// Token: 0x040018D5 RID: 6357
		String,
		// Token: 0x040018D6 RID: 6358
		Reference,
		// Token: 0x040018D7 RID: 6359
		Character,
		// Token: 0x040018D8 RID: 6360
		Category,
		// Token: 0x040018D9 RID: 6361
		NotCategory,
		// Token: 0x040018DA RID: 6362
		Range,
		// Token: 0x040018DB RID: 6363
		Set,
		// Token: 0x040018DC RID: 6364
		In,
		// Token: 0x040018DD RID: 6365
		Open,
		// Token: 0x040018DE RID: 6366
		Close,
		// Token: 0x040018DF RID: 6367
		Balance,
		// Token: 0x040018E0 RID: 6368
		BalanceStart,
		// Token: 0x040018E1 RID: 6369
		IfDefined,
		// Token: 0x040018E2 RID: 6370
		Sub,
		// Token: 0x040018E3 RID: 6371
		Test,
		// Token: 0x040018E4 RID: 6372
		Branch,
		// Token: 0x040018E5 RID: 6373
		Jump,
		// Token: 0x040018E6 RID: 6374
		Repeat,
		// Token: 0x040018E7 RID: 6375
		Until,
		// Token: 0x040018E8 RID: 6376
		FastRepeat,
		// Token: 0x040018E9 RID: 6377
		Anchor,
		// Token: 0x040018EA RID: 6378
		Info
	}
}
