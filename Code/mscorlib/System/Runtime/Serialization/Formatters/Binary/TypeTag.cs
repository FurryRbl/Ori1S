using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200051D RID: 1309
	internal enum TypeTag : byte
	{
		// Token: 0x040015B1 RID: 5553
		PrimitiveType,
		// Token: 0x040015B2 RID: 5554
		String,
		// Token: 0x040015B3 RID: 5555
		ObjectType,
		// Token: 0x040015B4 RID: 5556
		RuntimeType,
		// Token: 0x040015B5 RID: 5557
		GenericType,
		// Token: 0x040015B6 RID: 5558
		ArrayOfObject,
		// Token: 0x040015B7 RID: 5559
		ArrayOfString,
		// Token: 0x040015B8 RID: 5560
		ArrayOfPrimitiveType
	}
}
