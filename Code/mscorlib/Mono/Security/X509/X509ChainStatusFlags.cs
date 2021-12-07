using System;

namespace Mono.Security.X509
{
	// Token: 0x020000CC RID: 204
	[Flags]
	[Serializable]
	internal enum X509ChainStatusFlags
	{
		// Token: 0x04000308 RID: 776
		InvalidBasicConstraints = 1024,
		// Token: 0x04000309 RID: 777
		NoError = 0,
		// Token: 0x0400030A RID: 778
		NotSignatureValid = 8,
		// Token: 0x0400030B RID: 779
		NotTimeNested = 2,
		// Token: 0x0400030C RID: 780
		NotTimeValid = 1,
		// Token: 0x0400030D RID: 781
		PartialChain = 65536,
		// Token: 0x0400030E RID: 782
		UntrustedRoot = 32
	}
}
