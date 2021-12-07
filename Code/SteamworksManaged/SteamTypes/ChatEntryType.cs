using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000AE RID: 174
	[Flags]
	public enum ChatEntryType
	{
		// Token: 0x0400031E RID: 798
		Invalid = 0,
		// Token: 0x0400031F RID: 799
		ChatMsg = 1,
		// Token: 0x04000320 RID: 800
		Typing = 2,
		// Token: 0x04000321 RID: 801
		InviteGame = 3,
		// Token: 0x04000322 RID: 802
		Emote = 4,
		// Token: 0x04000323 RID: 803
		LeftConversation = 6
	}
}
