using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200051C RID: 1308
	internal enum BinaryElement : byte
	{
		// Token: 0x04001599 RID: 5529
		Header,
		// Token: 0x0400159A RID: 5530
		RefTypeObject,
		// Token: 0x0400159B RID: 5531
		UntypedRuntimeObject,
		// Token: 0x0400159C RID: 5532
		UntypedExternalObject,
		// Token: 0x0400159D RID: 5533
		RuntimeObject,
		// Token: 0x0400159E RID: 5534
		ExternalObject,
		// Token: 0x0400159F RID: 5535
		String,
		// Token: 0x040015A0 RID: 5536
		GenericArray,
		// Token: 0x040015A1 RID: 5537
		BoxedPrimitiveTypeValue,
		// Token: 0x040015A2 RID: 5538
		ObjectReference,
		// Token: 0x040015A3 RID: 5539
		NullValue,
		// Token: 0x040015A4 RID: 5540
		End,
		// Token: 0x040015A5 RID: 5541
		Assembly,
		// Token: 0x040015A6 RID: 5542
		ArrayFiller8b,
		// Token: 0x040015A7 RID: 5543
		ArrayFiller32b,
		// Token: 0x040015A8 RID: 5544
		ArrayOfPrimitiveType,
		// Token: 0x040015A9 RID: 5545
		ArrayOfObject,
		// Token: 0x040015AA RID: 5546
		ArrayOfString,
		// Token: 0x040015AB RID: 5547
		Method,
		// Token: 0x040015AC RID: 5548
		_Unknown4,
		// Token: 0x040015AD RID: 5549
		_Unknown5,
		// Token: 0x040015AE RID: 5550
		MethodCall,
		// Token: 0x040015AF RID: 5551
		MethodResponse
	}
}
