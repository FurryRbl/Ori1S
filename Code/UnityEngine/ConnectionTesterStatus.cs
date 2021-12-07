using System;

namespace UnityEngine
{
	// Token: 0x0200006C RID: 108
	public enum ConnectionTesterStatus
	{
		// Token: 0x0400011A RID: 282
		Error = -2,
		// Token: 0x0400011B RID: 283
		Undetermined,
		// Token: 0x0400011C RID: 284
		[Obsolete("No longer returned, use newer connection tester enums instead.")]
		PrivateIPNoNATPunchthrough,
		// Token: 0x0400011D RID: 285
		[Obsolete("No longer returned, use newer connection tester enums instead.")]
		PrivateIPHasNATPunchThrough,
		// Token: 0x0400011E RID: 286
		PublicIPIsConnectable,
		// Token: 0x0400011F RID: 287
		PublicIPPortBlocked,
		// Token: 0x04000120 RID: 288
		PublicIPNoServerStarted,
		// Token: 0x04000121 RID: 289
		LimitedNATPunchthroughPortRestricted,
		// Token: 0x04000122 RID: 290
		LimitedNATPunchthroughSymmetric,
		// Token: 0x04000123 RID: 291
		NATpunchthroughFullCone,
		// Token: 0x04000124 RID: 292
		NATpunchthroughAddressRestrictedCone
	}
}
