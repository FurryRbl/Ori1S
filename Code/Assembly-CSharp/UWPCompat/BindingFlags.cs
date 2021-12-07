using System;

namespace UWPCompat
{
	// Token: 0x02000199 RID: 409
	[Flags]
	public enum BindingFlags
	{
		// Token: 0x04000CED RID: 3309
		Default = 0,
		// Token: 0x04000CEE RID: 3310
		IgnoreCase = 1,
		// Token: 0x04000CEF RID: 3311
		DeclaredOnly = 2,
		// Token: 0x04000CF0 RID: 3312
		Instance = 4,
		// Token: 0x04000CF1 RID: 3313
		Static = 8,
		// Token: 0x04000CF2 RID: 3314
		Public = 16,
		// Token: 0x04000CF3 RID: 3315
		NonPublic = 32,
		// Token: 0x04000CF4 RID: 3316
		FlattenHierarchy = 64,
		// Token: 0x04000CF5 RID: 3317
		InvokeMethod = 128,
		// Token: 0x04000CF6 RID: 3318
		CreateInstance = 256,
		// Token: 0x04000CF7 RID: 3319
		GetField = 512,
		// Token: 0x04000CF8 RID: 3320
		SetField = 1024,
		// Token: 0x04000CF9 RID: 3321
		GetProperty = 2048,
		// Token: 0x04000CFA RID: 3322
		SetProperty = 4096,
		// Token: 0x04000CFB RID: 3323
		PutDispProperty = 8192,
		// Token: 0x04000CFC RID: 3324
		PutRefDispProperty = 16384,
		// Token: 0x04000CFD RID: 3325
		ExactBinding = 32768,
		// Token: 0x04000CFE RID: 3326
		SuppressChangeType = 65536,
		// Token: 0x04000CFF RID: 3327
		OptionalParamBinding = 131072,
		// Token: 0x04000D00 RID: 3328
		IgnoreReturn = 262144
	}
}
