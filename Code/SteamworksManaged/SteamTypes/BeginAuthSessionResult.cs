using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000113 RID: 275
	public enum BeginAuthSessionResult
	{
		// Token: 0x040004E2 RID: 1250
		OK,
		// Token: 0x040004E3 RID: 1251
		InvalidTicket,
		// Token: 0x040004E4 RID: 1252
		DuplicateRequest,
		// Token: 0x040004E5 RID: 1253
		InvalidVersion,
		// Token: 0x040004E6 RID: 1254
		GameMismatch,
		// Token: 0x040004E7 RID: 1255
		ExpiredTicket
	}
}
