using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x0200015C RID: 348
	public struct MatchMakingGetLobbyGameServerResult
	{
		// Token: 0x04000634 RID: 1588
		public bool Result;

		// Token: 0x04000635 RID: 1589
		public uint GameServerIP;

		// Token: 0x04000636 RID: 1590
		public ushort GameServerPort;

		// Token: 0x04000637 RID: 1591
		public SteamID SteamIDGameServer;
	}
}
