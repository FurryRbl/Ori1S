using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200051F RID: 1311
	internal enum MethodFlags
	{
		// Token: 0x040015BE RID: 5566
		NoArguments = 1,
		// Token: 0x040015BF RID: 5567
		PrimitiveArguments,
		// Token: 0x040015C0 RID: 5568
		ArgumentsInSimpleArray = 4,
		// Token: 0x040015C1 RID: 5569
		ArgumentsInMultiArray = 8,
		// Token: 0x040015C2 RID: 5570
		ExcludeLogicalCallContext = 16,
		// Token: 0x040015C3 RID: 5571
		IncludesLogicalCallContext = 64,
		// Token: 0x040015C4 RID: 5572
		IncludesSignature = 128,
		// Token: 0x040015C5 RID: 5573
		FormatMask = 15,
		// Token: 0x040015C6 RID: 5574
		GenericArguments = 32768,
		// Token: 0x040015C7 RID: 5575
		NeedsInfoArrayMask = 32972
	}
}
