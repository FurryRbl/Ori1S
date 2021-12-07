using System;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x0200000F RID: 15
	public interface IMatchmaking
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600002B RID: 43
		// (remove) Token: 0x0600002C RID: 44
		event CallbackEvent<FavoritesListChanged> FavoritesListChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600002D RID: 45
		// (remove) Token: 0x0600002E RID: 46
		event CallbackEvent<LobbyInvite> LobbyInvite;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600002F RID: 47
		// (remove) Token: 0x06000030 RID: 48
		event ResultEvent<LobbyEnter> LobbyEnterResult;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000031 RID: 49
		// (remove) Token: 0x06000032 RID: 50
		event CallbackEvent<LobbyDataUpdate> LobbyDataUpdate;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000033 RID: 51
		// (remove) Token: 0x06000034 RID: 52
		event CallbackEvent<LobbyChatUpdate> LobbyChatUpdate;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000035 RID: 53
		// (remove) Token: 0x06000036 RID: 54
		event CallbackEvent<LobbyChatMsg> LobbyChatMsg;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000037 RID: 55
		// (remove) Token: 0x06000038 RID: 56
		event CallbackEvent<LobbyGameCreated> LobbyGameCreated;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000039 RID: 57
		// (remove) Token: 0x0600003A RID: 58
		event ResultEvent<LobbyMatchList> LobbyMatchListResult;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600003B RID: 59
		// (remove) Token: 0x0600003C RID: 60
		event CallbackEvent<LobbyKicked> LobbyKicked;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600003D RID: 61
		// (remove) Token: 0x0600003E RID: 62
		event ResultEvent<LobbyCreated> LobbyCreatedResult;

		// Token: 0x0600003F RID: 63
		int GetFavoriteGameCount();

		// Token: 0x06000040 RID: 64
		bool GetFavoriteGame(int game, out AppID appID, out uint ip, out ushort connPort, out ushort port, out uint flags, out uint time32LastPlayedOnServer);

		// Token: 0x06000041 RID: 65
		MatchMakingGetFavoriteGameResult GetFavoriteGame(int game);

		// Token: 0x06000042 RID: 66
		int AddFavoriteGame(AppID appID, uint ip, ushort connPort, ushort queryPort, uint flags, uint time32LastPlayedOnServer);

		// Token: 0x06000043 RID: 67
		bool RemoveFavoriteGame(AppID appID, uint ip, ushort connPort, ushort queryPort, uint flags);

		// Token: 0x06000044 RID: 68
		void RequestLobbyList();

		// Token: 0x06000045 RID: 69
		void AddRequestLobbyListStringFilter(string keyToMatch, string valueToMatch, LobbyComparison comparisonType);

		// Token: 0x06000046 RID: 70
		void AddRequestLobbyListNumericalFilter(string keyToMatch, int valueToMatch, LobbyComparison comparisonType);

		// Token: 0x06000047 RID: 71
		void AddRequestLobbyListNearValueFilter(string keyToMatch, int valueToBeCloseTo);

		// Token: 0x06000048 RID: 72
		void AddRequestLobbyListFilterSlotsAvailable(int slotsAvailable);

		// Token: 0x06000049 RID: 73
		void AddRequestLobbyListDistanceFilter(LobbyDistanceFilter lobbyDistanceFilter);

		// Token: 0x0600004A RID: 74
		void AddRequestLobbyListResultCountFilter(int maxResults);

		// Token: 0x0600004B RID: 75
		void AddRequestLobbyListCompatibleMembersFilter(SteamID steamIDLobby);

		// Token: 0x0600004C RID: 76
		SteamID GetLobbyByIndex(int lobby);

		// Token: 0x0600004D RID: 77
		void CreateLobby(LobbyType lobbyType, int maxMembers);

		// Token: 0x0600004E RID: 78
		void JoinLobby(SteamID steamIDLobby);

		// Token: 0x0600004F RID: 79
		void LeaveLobby(SteamID steamIDLobby);

		// Token: 0x06000050 RID: 80
		bool InviteUserToLobby(SteamID steamIDLobby, SteamID steamIDInvitee);

		// Token: 0x06000051 RID: 81
		int GetNumLobbyMembers(SteamID steamIDLobby);

		// Token: 0x06000052 RID: 82
		SteamID GetLobbyMemberByIndex(SteamID steamIDLobby, int member);

		// Token: 0x06000053 RID: 83
		string GetLobbyData(SteamID steamIDLobby, string key);

		// Token: 0x06000054 RID: 84
		bool SetLobbyData(SteamID steamIDLobby, string key, string value);

		// Token: 0x06000055 RID: 85
		int GetLobbyDataCount(SteamID steamIDLobby);

		// Token: 0x06000056 RID: 86
		bool GetLobbyDataByIndex(SteamID steamIDLobby, int lobbyData, out string key, out string value);

		// Token: 0x06000057 RID: 87
		MatchMakingGetLobbyDataByIndexResult GetLobbyDataByIndex(SteamID steamIDLobby, int lobbyData);

		// Token: 0x06000058 RID: 88
		bool DeleteLobbyData(SteamID steamIDLobby, string key);

		// Token: 0x06000059 RID: 89
		string GetLobbyMemberData(SteamID steamIDLobby, SteamID steamIDUser, string key);

		// Token: 0x0600005A RID: 90
		void SetLobbyMemberData(SteamID steamIDLobby, string key, string value);

		// Token: 0x0600005B RID: 91
		bool SendLobbyChatMsg(SteamID steamIDLobby, byte[] msgBody);

		// Token: 0x0600005C RID: 92
		int GetLobbyChatEntry(SteamID steamIDLobby, int chatID, out SteamID steamIDUser, byte[] data, out ChatEntryType chatEntryType);

		// Token: 0x0600005D RID: 93
		MatchmakingGetLobbyChatEntryResult GetLobbyChatEntry(SteamID steamIDLobby, int chatID, byte[] data);

		// Token: 0x0600005E RID: 94
		bool RequestLobbyData(SteamID steamIDLobby);

		// Token: 0x0600005F RID: 95
		void SetLobbyGameServer(SteamID steamIDLobby, uint gameServerIP, ushort gameServerPort, SteamID steamIDGameServer);

		// Token: 0x06000060 RID: 96
		bool GetLobbyGameServer(SteamID steamIDLobby, out uint gameServerIP, out ushort gameServerPort, out SteamID steamIDGameServer);

		// Token: 0x06000061 RID: 97
		MatchMakingGetLobbyGameServerResult GetLobbyGameServer(SteamID steamIDLobby);

		// Token: 0x06000062 RID: 98
		bool SetLobbyMemberLimit(SteamID steamIDLobby, int maxMembers);

		// Token: 0x06000063 RID: 99
		int GetLobbyMemberLimit(SteamID steamIDLobby);

		// Token: 0x06000064 RID: 100
		bool SetLobbyType(SteamID steamIDLobby, LobbyType lobbyType);

		// Token: 0x06000065 RID: 101
		bool SetLobbyJoinable(SteamID steamIDLobby, bool lobbyJoinable);

		// Token: 0x06000066 RID: 102
		SteamID GetLobbyOwner(SteamID steamIDLobby);

		// Token: 0x06000067 RID: 103
		bool SetLobbyOwner(SteamID steamIDLobby, SteamID steamIDNewOwner);

		// Token: 0x06000068 RID: 104
		bool SetLinkedLobby(SteamID steamIDLobby, SteamID steamIDLobbyDependent);
	}
}
