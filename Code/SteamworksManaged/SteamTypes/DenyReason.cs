using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000135 RID: 309
	public enum DenyReason
	{
		// Token: 0x04000540 RID: 1344
		Invalid,
		// Token: 0x04000541 RID: 1345
		InvalidVersion,
		// Token: 0x04000542 RID: 1346
		Generic,
		// Token: 0x04000543 RID: 1347
		NotLoggedOn,
		// Token: 0x04000544 RID: 1348
		NoLicense,
		// Token: 0x04000545 RID: 1349
		Cheater,
		// Token: 0x04000546 RID: 1350
		LoggedInElseWhere,
		// Token: 0x04000547 RID: 1351
		UnknownText,
		// Token: 0x04000548 RID: 1352
		IncompatibleAnticheat,
		// Token: 0x04000549 RID: 1353
		MemoryCorruption,
		// Token: 0x0400054A RID: 1354
		IncompatibleSoftware,
		// Token: 0x0400054B RID: 1355
		SteamConnectionLost,
		// Token: 0x0400054C RID: 1356
		SteamConnectionError,
		// Token: 0x0400054D RID: 1357
		SteamResponseTimedOut,
		// Token: 0x0400054E RID: 1358
		SteamValidationStalled,
		// Token: 0x0400054F RID: 1359
		SteamOwnerLeftGuestUser
	}
}
