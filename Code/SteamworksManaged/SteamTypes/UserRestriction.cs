using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000004 RID: 4
	[Flags]
	public enum UserRestriction
	{
		// Token: 0x0400000D RID: 13
		None = 0,
		// Token: 0x0400000E RID: 14
		Unknown = 1,
		// Token: 0x0400000F RID: 15
		AnyChat = 2,
		// Token: 0x04000010 RID: 16
		VoiceChat = 4,
		// Token: 0x04000011 RID: 17
		GroupChat = 8,
		// Token: 0x04000012 RID: 18
		Rating = 16,
		// Token: 0x04000013 RID: 19
		GameInvites = 32,
		// Token: 0x04000014 RID: 20
		Trading = 64
	}
}
