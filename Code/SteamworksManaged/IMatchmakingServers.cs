using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x02000053 RID: 83
	public interface IMatchmakingServers
	{
		// Token: 0x0600021C RID: 540
		ServerListRequestHandle RequestInternetServerList(AppID appID, MatchMakingKeyValuePair[] filters, MatchmakingServerListResponse requestServersResponse);

		// Token: 0x0600021D RID: 541
		ServerListRequestHandle RequestLANServerList(AppID appID, MatchmakingServerListResponse requestServersResponse);

		// Token: 0x0600021E RID: 542
		ServerListRequestHandle RequestFriendsServerList(AppID appID, MatchMakingKeyValuePair[] filters, MatchmakingServerListResponse requestServersResponse);

		// Token: 0x0600021F RID: 543
		ServerListRequestHandle RequestFavoritesServerList(AppID appID, MatchMakingKeyValuePair[] filters, MatchmakingServerListResponse requestServersResponse);

		// Token: 0x06000220 RID: 544
		ServerListRequestHandle RequestHistoryServerList(AppID appID, MatchMakingKeyValuePair[] filters, MatchmakingServerListResponse requestServersResponse);

		// Token: 0x06000221 RID: 545
		ServerListRequestHandle RequestSpectatorServerList(AppID appID, MatchMakingKeyValuePair[] filters, MatchmakingServerListResponse requestServersResponse);

		// Token: 0x06000222 RID: 546
		void ReleaseRequest(ServerListRequestHandle serverListRequest);

		// Token: 0x06000223 RID: 547
		GameServerItem GetServerDetails(ServerListRequestHandle request, int server);

		// Token: 0x06000224 RID: 548
		void CancelQuery(ServerListRequestHandle request);

		// Token: 0x06000225 RID: 549
		void RefreshQuery(ServerListRequestHandle request);

		// Token: 0x06000226 RID: 550
		bool IsRefreshing(ServerListRequestHandle request);

		// Token: 0x06000227 RID: 551
		int GetServerCount(ServerListRequestHandle request);

		// Token: 0x06000228 RID: 552
		void RefreshServer(ServerListRequestHandle request, int server);

		// Token: 0x06000229 RID: 553
		ServerQueryHandle PingServer(uint ip, ushort port, MatchmakingPingResponse requestServersResponse);

		// Token: 0x0600022A RID: 554
		ServerQueryHandle PlayerDetails(uint ip, ushort port, MatchmakingPlayersResponse requestServersResponse);

		// Token: 0x0600022B RID: 555
		ServerQueryHandle ServerRules(uint ip, ushort port, MatchmakingRulesResponse requestServersResponse);

		// Token: 0x0600022C RID: 556
		void CancelServerQuery(ServerQueryHandle serverQuery);
	}
}
