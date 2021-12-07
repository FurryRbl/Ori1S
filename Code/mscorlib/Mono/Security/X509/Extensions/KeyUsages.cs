using System;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x020000E9 RID: 233
	[Flags]
	internal enum KeyUsages
	{
		// Token: 0x0400033C RID: 828
		digitalSignature = 128,
		// Token: 0x0400033D RID: 829
		nonRepudiation = 64,
		// Token: 0x0400033E RID: 830
		keyEncipherment = 32,
		// Token: 0x0400033F RID: 831
		dataEncipherment = 16,
		// Token: 0x04000340 RID: 832
		keyAgreement = 8,
		// Token: 0x04000341 RID: 833
		keyCertSign = 4,
		// Token: 0x04000342 RID: 834
		cRLSign = 2,
		// Token: 0x04000343 RID: 835
		encipherOnly = 1,
		// Token: 0x04000344 RID: 836
		decipherOnly = 2048,
		// Token: 0x04000345 RID: 837
		none = 0
	}
}
