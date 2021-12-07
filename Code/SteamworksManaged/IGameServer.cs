using System;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x020000F9 RID: 249
	public interface IGameServer
	{
		// Token: 0x1400008F RID: 143
		// (add) Token: 0x060006B7 RID: 1719
		// (remove) Token: 0x060006B8 RID: 1720
		event CallbackEvent<SteamServersConnected> SteamServersConnected;

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x060006B9 RID: 1721
		// (remove) Token: 0x060006BA RID: 1722
		event CallbackEvent<SteamServerConnectFailure> SteamServerConnectFailure;

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x060006BB RID: 1723
		// (remove) Token: 0x060006BC RID: 1724
		event CallbackEvent<SteamServersDisconnected> SteamServersDisconnected;

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x060006BD RID: 1725
		// (remove) Token: 0x060006BE RID: 1726
		event CallbackEvent<ValidateAuthTicketResponse> ValidateAuthTicketResponse;

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x060006BF RID: 1727
		// (remove) Token: 0x060006C0 RID: 1728
		event CallbackEvent<GSClientApprove> GSClientApprove;

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x060006C1 RID: 1729
		// (remove) Token: 0x060006C2 RID: 1730
		event CallbackEvent<GSClientDeny> GSClientDeny;

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x060006C3 RID: 1731
		// (remove) Token: 0x060006C4 RID: 1732
		event CallbackEvent<GSClientKick> GSClientKick;

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x060006C5 RID: 1733
		// (remove) Token: 0x060006C6 RID: 1734
		event CallbackEvent<GSClientAchievementStatus> GSClientAchievementStatus;

		// Token: 0x14000097 RID: 151
		// (add) Token: 0x060006C7 RID: 1735
		// (remove) Token: 0x060006C8 RID: 1736
		event CallbackEvent<GSPolicyResponse> GSPolicyResponse;

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x060006C9 RID: 1737
		// (remove) Token: 0x060006CA RID: 1738
		event CallbackEvent<GSGameplayStats> GSGameplayStats;

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x060006CB RID: 1739
		// (remove) Token: 0x060006CC RID: 1740
		event CallbackEvent<GSClientGroupStatus> GSClientGroupStatus;

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x060006CD RID: 1741
		// (remove) Token: 0x060006CE RID: 1742
		event ResultEvent<GSReputation> GSReputation;

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x060006CF RID: 1743
		// (remove) Token: 0x060006D0 RID: 1744
		event ResultEvent<AssociateWithClanResult> AssociateWithClanResult;

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x060006D1 RID: 1745
		// (remove) Token: 0x060006D2 RID: 1746
		event ResultEvent<ComputeNewPlayerCompatibilityResult> ComputeNewPlayerCompatibilityResult;

		// Token: 0x060006D3 RID: 1747
		bool InitGameServer(uint ip, ushort gamePort, ushort queryPort, uint flags, AppID gameAppId, string versionString);

		// Token: 0x060006D4 RID: 1748
		void SetProduct(string product);

		// Token: 0x060006D5 RID: 1749
		void SetGameDescription(string gameDescription);

		// Token: 0x060006D6 RID: 1750
		void SetModDir(string modDir);

		// Token: 0x060006D7 RID: 1751
		void SetDedicatedServer(bool dedicated);

		// Token: 0x060006D8 RID: 1752
		void LogOn(string accountName, string password);

		// Token: 0x060006D9 RID: 1753
		void LogOnAnonymous();

		// Token: 0x060006DA RID: 1754
		void LogOff();

		// Token: 0x060006DB RID: 1755
		bool LoggedOn();

		// Token: 0x060006DC RID: 1756
		bool Secure();

		// Token: 0x060006DD RID: 1757
		SteamID GetSteamID();

		// Token: 0x060006DE RID: 1758
		bool WasRestartRequested();

		// Token: 0x060006DF RID: 1759
		void SetMaxPlayerCount(int playersMax);

		// Token: 0x060006E0 RID: 1760
		void SetBotPlayerCount(int botPlayers);

		// Token: 0x060006E1 RID: 1761
		void SetServerName(string serverName);

		// Token: 0x060006E2 RID: 1762
		void SetMapName(string mapName);

		// Token: 0x060006E3 RID: 1763
		void SetPasswordProtected(bool passwordProtected);

		// Token: 0x060006E4 RID: 1764
		void SetSpectatorPort(ushort spectatorPort);

		// Token: 0x060006E5 RID: 1765
		void SetSpectatorServerName(string spectatorServerName);

		// Token: 0x060006E6 RID: 1766
		void ClearAllKeyValues();

		// Token: 0x060006E7 RID: 1767
		void SetKeyValue(string key, string value);

		// Token: 0x060006E8 RID: 1768
		void SetGameTags(string gameTags);

		// Token: 0x060006E9 RID: 1769
		void SetGameData(string gameData);

		// Token: 0x060006EA RID: 1770
		void SetRegion(string region);

		// Token: 0x060006EB RID: 1771
		bool SendUserConnectAndAuthenticate(uint ipClient, byte[] authenticationBlob, out SteamID steamIDUser);

		// Token: 0x060006EC RID: 1772
		GameServerSendUserConnectAndAuthenticateResult SendUserConnectAndAuthenticate(uint ipClient, byte[] authenticationBlob);

		// Token: 0x060006ED RID: 1773
		SteamID CreateUnauthenticatedUserConnection();

		// Token: 0x060006EE RID: 1774
		void SendUserDisconnect(SteamID steamIDUser);

		// Token: 0x060006EF RID: 1775
		bool UpdateUserData(SteamID steamIDUser, string playerName, uint score);

		// Token: 0x060006F0 RID: 1776
		AuthTicketHandle GetAuthSessionTicket(IntPtr ticket, int maxTicket, out uint ticketLength);

		// Token: 0x060006F1 RID: 1777
		GameServerGetAuthSessionTicketResult GetAuthSessionTicket(IntPtr ticket, int maxTicket);

		// Token: 0x060006F2 RID: 1778
		BeginAuthSessionResult BeginAuthSession(IntPtr authTicket, int cbAuthTicket, SteamID steamID);

		// Token: 0x060006F3 RID: 1779
		void EndAuthSession(SteamID steamID);

		// Token: 0x060006F4 RID: 1780
		void CancelAuthTicket(AuthTicketHandle authTicket);

		// Token: 0x060006F5 RID: 1781
		UserHasLicenseForAppResult UserHasLicenseForApp(SteamID steamID, AppID appID);

		// Token: 0x060006F6 RID: 1782
		bool RequestUserGroupStatus(SteamID steamIDUser, SteamID steamIDGroup);

		// Token: 0x060006F7 RID: 1783
		void GetGameplayStats();

		// Token: 0x060006F8 RID: 1784
		void GetServerReputation();

		// Token: 0x060006F9 RID: 1785
		uint GetPublicIP();

		// Token: 0x060006FA RID: 1786
		bool HandleIncomingPacket(byte[] packet, uint sourceIP, ushort sourcePort);

		// Token: 0x060006FB RID: 1787
		int GetNextOutgoingPacket(byte[] packet, out uint netAdr, out ushort port);

		// Token: 0x060006FC RID: 1788
		GameServerGetNextOutgoingPacketResult GetNextOutgoingPacket(byte[] data);

		// Token: 0x060006FD RID: 1789
		void EnableHeartbeats(bool active);

		// Token: 0x060006FE RID: 1790
		void SetHeartbeatInterval(int heartbeatInterval);

		// Token: 0x060006FF RID: 1791
		void ForceHeartbeat();

		// Token: 0x06000700 RID: 1792
		void AssociateWithClan(SteamID steamIDClan);

		// Token: 0x06000701 RID: 1793
		void ComputeNewPlayerCompatibility(SteamID steamIDNewPlayer);
	}
}
