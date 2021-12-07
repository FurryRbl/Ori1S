using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x0200012B RID: 299
	[Flags]
	public enum ChatMemberStateChange
	{
		// Token: 0x0400051A RID: 1306
		Entered = 1,
		// Token: 0x0400051B RID: 1307
		Left = 2,
		// Token: 0x0400051C RID: 1308
		Disconnected = 4,
		// Token: 0x0400051D RID: 1309
		Kicked = 8,
		// Token: 0x0400051E RID: 1310
		Banned = 16
	}
}
