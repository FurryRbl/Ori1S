using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000520 RID: 1312
	internal enum ReturnTypeTag : byte
	{
		// Token: 0x040015C9 RID: 5577
		Null = 2,
		// Token: 0x040015CA RID: 5578
		PrimitiveType = 8,
		// Token: 0x040015CB RID: 5579
		ObjectType = 16,
		// Token: 0x040015CC RID: 5580
		Exception = 32
	}
}
