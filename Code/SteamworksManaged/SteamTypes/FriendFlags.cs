using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x0200004C RID: 76
	[Flags]
	public enum FriendFlags
	{
		// Token: 0x0400015F RID: 351
		None = 0,
		// Token: 0x04000160 RID: 352
		Blocked = 1,
		// Token: 0x04000161 RID: 353
		FriendshipRequested = 2,
		// Token: 0x04000162 RID: 354
		Immediate = 4,
		// Token: 0x04000163 RID: 355
		ClanMember = 8,
		// Token: 0x04000164 RID: 356
		OnGameServer = 16,
		// Token: 0x04000165 RID: 357
		RequestingFriendship = 128,
		// Token: 0x04000166 RID: 358
		RequestingInfo = 256,
		// Token: 0x04000167 RID: 359
		Ignored = 512,
		// Token: 0x04000168 RID: 360
		IgnoredFriend = 1024,
		// Token: 0x04000169 RID: 361
		Suggested = 2048,
		// Token: 0x0400016A RID: 362
		All = 65535
	}
}
