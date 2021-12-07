using System;

namespace ManagedSteam
{
	// Token: 0x02000088 RID: 136
	public enum OverlayDialog
	{
		// Token: 0x0400027A RID: 634
		None,
		// Token: 0x0400027B RID: 635
		Friends,
		// Token: 0x0400027C RID: 636
		Community,
		// Token: 0x0400027D RID: 637
		Players,
		// Token: 0x0400027E RID: 638
		Settings,
		// Token: 0x0400027F RID: 639
		OfficialGameGroup,
		// Token: 0x04000280 RID: 640
		Stats,
		// Token: 0x04000281 RID: 641
		Achievements,
		// Token: 0x04000282 RID: 642
		[Obsolete("ActivateGameOverlayInviteDialog should probably be used instead", true)]
		LobbyInvite
	}
}
