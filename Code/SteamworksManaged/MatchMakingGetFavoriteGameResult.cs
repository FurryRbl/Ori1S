using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x02000159 RID: 345
	public struct MatchMakingGetFavoriteGameResult
	{
		// Token: 0x04000627 RID: 1575
		public bool Result;

		// Token: 0x04000628 RID: 1576
		public AppID AppID;

		// Token: 0x04000629 RID: 1577
		public uint IP;

		// Token: 0x0400062A RID: 1578
		public ushort ConnPort;

		// Token: 0x0400062B RID: 1579
		public ushort Port;

		// Token: 0x0400062C RID: 1580
		public uint Flags;

		// Token: 0x0400062D RID: 1581
		public uint Time32LastPlayedOnServer;
	}
}
