using System;

namespace UnityEngine.Networking
{
	// Token: 0x0200024D RID: 589
	public enum NetworkError
	{
		// Token: 0x0400095A RID: 2394
		Ok,
		// Token: 0x0400095B RID: 2395
		WrongHost,
		// Token: 0x0400095C RID: 2396
		WrongConnection,
		// Token: 0x0400095D RID: 2397
		WrongChannel,
		// Token: 0x0400095E RID: 2398
		NoResources,
		// Token: 0x0400095F RID: 2399
		BadMessage,
		// Token: 0x04000960 RID: 2400
		Timeout,
		// Token: 0x04000961 RID: 2401
		MessageToLong,
		// Token: 0x04000962 RID: 2402
		WrongOperation,
		// Token: 0x04000963 RID: 2403
		VersionMismatch,
		// Token: 0x04000964 RID: 2404
		CRCMismatch,
		// Token: 0x04000965 RID: 2405
		DNSFailure
	}
}
