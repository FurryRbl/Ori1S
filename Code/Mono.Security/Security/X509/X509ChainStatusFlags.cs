using System;

namespace Mono.Security.X509
{
	// Token: 0x02000048 RID: 72
	[Flags]
	[Serializable]
	public enum X509ChainStatusFlags
	{
		// Token: 0x04000189 RID: 393
		InvalidBasicConstraints = 1024,
		// Token: 0x0400018A RID: 394
		NoError = 0,
		// Token: 0x0400018B RID: 395
		NotSignatureValid = 8,
		// Token: 0x0400018C RID: 396
		NotTimeNested = 2,
		// Token: 0x0400018D RID: 397
		NotTimeValid = 1,
		// Token: 0x0400018E RID: 398
		PartialChain = 65536,
		// Token: 0x0400018F RID: 399
		UntrustedRoot = 32
	}
}
