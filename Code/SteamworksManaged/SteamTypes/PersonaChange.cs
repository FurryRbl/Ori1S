using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000019 RID: 25
	[Flags]
	public enum PersonaChange
	{
		// Token: 0x04000063 RID: 99
		Name = 1,
		// Token: 0x04000064 RID: 100
		Status = 2,
		// Token: 0x04000065 RID: 101
		ComeOnline = 4,
		// Token: 0x04000066 RID: 102
		GoneOffline = 8,
		// Token: 0x04000067 RID: 103
		GamePlayed = 16,
		// Token: 0x04000068 RID: 104
		GameServer = 32,
		// Token: 0x04000069 RID: 105
		Avatar = 64,
		// Token: 0x0400006A RID: 106
		JoinedSource = 128,
		// Token: 0x0400006B RID: 107
		LeftSource = 256,
		// Token: 0x0400006C RID: 108
		RelationshipChanged = 512,
		// Token: 0x0400006D RID: 109
		NameFirstSet = 1024,
		// Token: 0x0400006E RID: 110
		FacebookInfo = 2048
	}
}
