using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000136 RID: 310
	public enum ChatRoomEnterResponse
	{
		// Token: 0x04000551 RID: 1361
		Success = 1,
		// Token: 0x04000552 RID: 1362
		DoesntExist,
		// Token: 0x04000553 RID: 1363
		NotAllowed,
		// Token: 0x04000554 RID: 1364
		Full,
		// Token: 0x04000555 RID: 1365
		Error,
		// Token: 0x04000556 RID: 1366
		Banned,
		// Token: 0x04000557 RID: 1367
		Limited,
		// Token: 0x04000558 RID: 1368
		ClanDisabled,
		// Token: 0x04000559 RID: 1369
		CommunityBan,
		// Token: 0x0400055A RID: 1370
		MemberBlockedYou,
		// Token: 0x0400055B RID: 1371
		YouBlockedMember,
		// Token: 0x0400055C RID: 1372
		NoRankingDataLobby,
		// Token: 0x0400055D RID: 1373
		NoRankingDataUser,
		// Token: 0x0400055E RID: 1374
		RankOutOfRange
	}
}
