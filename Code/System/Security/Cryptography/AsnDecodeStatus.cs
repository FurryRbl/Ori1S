using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000431 RID: 1073
	internal enum AsnDecodeStatus
	{
		// Token: 0x040017BA RID: 6074
		NotDecoded = -1,
		// Token: 0x040017BB RID: 6075
		Ok,
		// Token: 0x040017BC RID: 6076
		BadAsn,
		// Token: 0x040017BD RID: 6077
		BadTag,
		// Token: 0x040017BE RID: 6078
		BadLength,
		// Token: 0x040017BF RID: 6079
		InformationNotAvailable
	}
}
