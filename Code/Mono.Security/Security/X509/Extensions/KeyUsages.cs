using System;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x0200006D RID: 109
	[Flags]
	public enum KeyUsages
	{
		// Token: 0x040001D9 RID: 473
		digitalSignature = 128,
		// Token: 0x040001DA RID: 474
		nonRepudiation = 64,
		// Token: 0x040001DB RID: 475
		keyEncipherment = 32,
		// Token: 0x040001DC RID: 476
		dataEncipherment = 16,
		// Token: 0x040001DD RID: 477
		keyAgreement = 8,
		// Token: 0x040001DE RID: 478
		keyCertSign = 4,
		// Token: 0x040001DF RID: 479
		cRLSign = 2,
		// Token: 0x040001E0 RID: 480
		encipherOnly = 1,
		// Token: 0x040001E1 RID: 481
		decipherOnly = 2048,
		// Token: 0x040001E2 RID: 482
		none = 0
	}
}
