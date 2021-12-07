using System;

namespace System.Reflection
{
	// Token: 0x020002AB RID: 683
	[Flags]
	internal enum PInfo
	{
		// Token: 0x04000CF9 RID: 3321
		Attributes = 1,
		// Token: 0x04000CFA RID: 3322
		GetMethod = 2,
		// Token: 0x04000CFB RID: 3323
		SetMethod = 4,
		// Token: 0x04000CFC RID: 3324
		ReflectedType = 8,
		// Token: 0x04000CFD RID: 3325
		DeclaringType = 16,
		// Token: 0x04000CFE RID: 3326
		Name = 32
	}
}
