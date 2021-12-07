using System;
using System.Collections.Generic;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x02000010 RID: 16
	internal class MatchMaking : SteamService, IMatchmaking
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00002744 File Offset: 0x00000944
		public MatchMaking()
		{
			this.favoritesListChanged = new List<FavoritesListChanged>();
			this.lobbyInvite = new List<LobbyInvite>();
			this.lobbyEnter = new List<SteamService.Result<LobbyEnter>>();
			this.lobbyDataUpdate = new List<LobbyDataUpdate>();
			this.lobbyChatUpdate = new List<LobbyChatUpdate>();
			this.lobbyChatMsg = new List<LobbyChatMsg>();
			this.lobbyGameCreated = new List<LobbyGameCreated>();
			this.lobbyMatchList = new List<SteamService.Result<LobbyMatchList>>();
			this.lobbyKicked = new List<LobbyKicked>();
			this.lobbyCreated = new List<SteamService.Result<LobbyCreated>>();
			SteamService.Callbacks[CallbackID.FavoritesListChanged] = delegate(IntPtr data, int size)
			{
				this.favoritesListChanged.Add(ManagedSteam.CallbackStructures.FavoritesListChanged.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.LobbyInvite] = delegate(IntPtr data, int size)
			{
				this.lobbyInvite.Add(ManagedSteam.CallbackStructures.LobbyInvite.Create(data, size));
			};
			SteamService.Results[ResultID.LobbyEnterResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.lobbyEnter.Add(new SteamService.Result<LobbyEnter>(LobbyEnter.Create(data, size), flag));
			};
			SteamService.Callbacks[CallbackID.LobbyDataUpdate] = delegate(IntPtr data, int size)
			{
				this.lobbyDataUpdate.Add(ManagedSteam.CallbackStructures.LobbyDataUpdate.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.LobbyChatUpdate] = delegate(IntPtr data, int size)
			{
				this.lobbyChatUpdate.Add(ManagedSteam.CallbackStructures.LobbyChatUpdate.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.LobbyChatMsg] = delegate(IntPtr data, int size)
			{
				this.lobbyChatMsg.Add(ManagedSteam.CallbackStructures.LobbyChatMsg.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.LobbyGameCreated] = delegate(IntPtr data, int size)
			{
				this.lobbyGameCreated.Add(ManagedSteam.CallbackStructures.LobbyGameCreated.Create(data, size));
			};
			SteamService.Results[ResultID.LobbyMatchList] = delegate(IntPtr data, int size, bool flag)
			{
				this.lobbyMatchList.Add(new SteamService.Result<LobbyMatchList>(LobbyMatchList.Create(data, size), flag));
			};
			SteamService.Callbacks[CallbackID.LobbyKicked] = delegate(IntPtr data, int size)
			{
				this.lobbyKicked.Add(ManagedSteam.CallbackStructures.LobbyKicked.Create(data, size));
			};
			SteamService.Results[ResultID.LobbyCreated] = delegate(IntPtr data, int size, bool flag)
			{
				this.lobbyCreated.Add(new SteamService.Result<LobbyCreated>(LobbyCreated.Create(data, size), flag));
			};
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600006A RID: 106 RVA: 0x00002914 File Offset: 0x00000B14
		// (remove) Token: 0x0600006B RID: 107 RVA: 0x0000294C File Offset: 0x00000B4C
		public event CallbackEvent<FavoritesListChanged> FavoritesListChanged;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600006C RID: 108 RVA: 0x00002984 File Offset: 0x00000B84
		// (remove) Token: 0x0600006D RID: 109 RVA: 0x000029BC File Offset: 0x00000BBC
		public event CallbackEvent<LobbyInvite> LobbyInvite;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600006E RID: 110 RVA: 0x000029F4 File Offset: 0x00000BF4
		// (remove) Token: 0x0600006F RID: 111 RVA: 0x00002A2C File Offset: 0x00000C2C
		public event ResultEvent<LobbyEnter> LobbyEnterResult;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000070 RID: 112 RVA: 0x00002A64 File Offset: 0x00000C64
		// (remove) Token: 0x06000071 RID: 113 RVA: 0x00002A9C File Offset: 0x00000C9C
		public event CallbackEvent<LobbyDataUpdate> LobbyDataUpdate;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000072 RID: 114 RVA: 0x00002AD4 File Offset: 0x00000CD4
		// (remove) Token: 0x06000073 RID: 115 RVA: 0x00002B0C File Offset: 0x00000D0C
		public event CallbackEvent<LobbyChatUpdate> LobbyChatUpdate;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000074 RID: 116 RVA: 0x00002B44 File Offset: 0x00000D44
		// (remove) Token: 0x06000075 RID: 117 RVA: 0x00002B7C File Offset: 0x00000D7C
		public event CallbackEvent<LobbyChatMsg> LobbyChatMsg;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000076 RID: 118 RVA: 0x00002BB4 File Offset: 0x00000DB4
		// (remove) Token: 0x06000077 RID: 119 RVA: 0x00002BEC File Offset: 0x00000DEC
		public event CallbackEvent<LobbyGameCreated> LobbyGameCreated;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000078 RID: 120 RVA: 0x00002C24 File Offset: 0x00000E24
		// (remove) Token: 0x06000079 RID: 121 RVA: 0x00002C5C File Offset: 0x00000E5C
		public event ResultEvent<LobbyMatchList> LobbyMatchListResult;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600007A RID: 122 RVA: 0x00002C94 File Offset: 0x00000E94
		// (remove) Token: 0x0600007B RID: 123 RVA: 0x00002CCC File Offset: 0x00000ECC
		public event CallbackEvent<LobbyKicked> LobbyKicked;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600007C RID: 124 RVA: 0x00002D04 File Offset: 0x00000F04
		// (remove) Token: 0x0600007D RID: 125 RVA: 0x00002D3C File Offset: 0x00000F3C
		public event ResultEvent<LobbyCreated> LobbyCreatedResult;

		// Token: 0x0600007E RID: 126 RVA: 0x00002D74 File Offset: 0x00000F74
		internal override void InvokeEvents()
		{
			SteamService.InvokeEvents<FavoritesListChanged>(this.favoritesListChanged, this.FavoritesListChanged);
			SteamService.InvokeEvents<LobbyInvite>(this.lobbyInvite, this.LobbyInvite);
			SteamService.InvokeEvents<LobbyEnter>(this.lobbyEnter, this.LobbyEnterResult);
			SteamService.InvokeEvents<LobbyDataUpdate>(this.lobbyDataUpdate, this.LobbyDataUpdate);
			SteamService.InvokeEvents<LobbyChatUpdate>(this.lobbyChatUpdate, this.LobbyChatUpdate);
			SteamService.InvokeEvents<LobbyChatMsg>(this.lobbyChatMsg, this.LobbyChatMsg);
			SteamService.InvokeEvents<LobbyGameCreated>(this.lobbyGameCreated, this.LobbyGameCreated);
			SteamService.InvokeEvents<LobbyMatchList>(this.lobbyMatchList, this.LobbyMatchListResult);
			SteamService.InvokeEvents<LobbyKicked>(this.lobbyKicked, this.LobbyKicked);
			SteamService.InvokeEvents<LobbyCreated>(this.lobbyCreated, this.LobbyCreatedResult);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002E2B File Offset: 0x0000102B
		internal override void ReleaseManagedResources()
		{
			this.lobbyMatchList = null;
			this.LobbyMatchListResult = null;
			this.lobbyCreated = null;
			this.LobbyCreatedResult = null;
			this.lobbyEnter = null;
			this.LobbyEnterResult = null;
			this.lobbyChatMsg = null;
			this.LobbyChatMsg = null;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002E65 File Offset: 0x00001065
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002E67 File Offset: 0x00001067
		public int GetFavoriteGameCount()
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_GetFavoriteGameCount();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002E74 File Offset: 0x00001074
		public bool GetFavoriteGame(int game, out AppID appID, out uint ip, out ushort connPort, out ushort port, out uint flags, out uint time32LastPlayedOnServer)
		{
			base.CheckIfUsable();
			ip = 0U;
			connPort = 0;
			port = 0;
			flags = 0U;
			time32LastPlayedOnServer = 0U;
			uint value = 0U;
			bool result = NativeMethods.MatchMaking_GetFavoriteGame(game, ref value, ref ip, ref connPort, ref port, ref flags, ref time32LastPlayedOnServer);
			appID = new AppID(value);
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002EBC File Offset: 0x000010BC
		public MatchMakingGetFavoriteGameResult GetFavoriteGame(int game)
		{
			MatchMakingGetFavoriteGameResult result = default(MatchMakingGetFavoriteGameResult);
			result.Result = this.GetFavoriteGame(game, out result.AppID, out result.IP, out result.ConnPort, out result.Port, out result.Flags, out result.Time32LastPlayedOnServer);
			return result;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002F0A File Offset: 0x0000110A
		public int AddFavoriteGame(AppID appID, uint ip, ushort connPort, ushort queryPort, uint flags, uint time32LastPlayedOnServer)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_AddFavoriteGame(appID.AsUInt32, ip, connPort, queryPort, flags, time32LastPlayedOnServer);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002F26 File Offset: 0x00001126
		public bool RemoveFavoriteGame(AppID appID, uint ip, ushort connPort, ushort queryPort, uint flags)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_RemoveFavoriteGame(appID.AsUInt32, ip, connPort, queryPort, flags);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002F40 File Offset: 0x00001140
		public void RequestLobbyList()
		{
			base.CheckIfUsable();
			NativeMethods.MatchMaking_RequestLobbyList();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002F4D File Offset: 0x0000114D
		public void AddRequestLobbyListStringFilter(string keyToMatch, string valueToMatch, LobbyComparison comparisonType)
		{
			base.CheckIfUsable();
			NativeMethods.MatchMaking_AddRequestLobbyListStringFilter(keyToMatch, valueToMatch, (int)comparisonType);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002F5D File Offset: 0x0000115D
		public void AddRequestLobbyListNumericalFilter(string keyToMatch, int valueToMatch, LobbyComparison comparisonType)
		{
			base.CheckIfUsable();
			NativeMethods.MatchMaking_AddRequestLobbyListNumericalFilter(keyToMatch, valueToMatch, (int)comparisonType);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002F6D File Offset: 0x0000116D
		public void AddRequestLobbyListNearValueFilter(string keyToMatch, int valueToBeCloseTo)
		{
			base.CheckIfUsable();
			NativeMethods.MatchMaking_AddRequestLobbyListNearValueFilter(keyToMatch, valueToBeCloseTo);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002F7C File Offset: 0x0000117C
		public void AddRequestLobbyListFilterSlotsAvailable(int slotsAvailable)
		{
			base.CheckIfUsable();
			NativeMethods.MatchMaking_AddRequestLobbyListFilterSlotsAvailable(slotsAvailable);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002F8A File Offset: 0x0000118A
		public void AddRequestLobbyListDistanceFilter(LobbyDistanceFilter lobbyDistanceFilter)
		{
			base.CheckIfUsable();
			NativeMethods.MatchMaking_AddRequestLobbyListDistanceFilter((int)lobbyDistanceFilter);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002F98 File Offset: 0x00001198
		public void AddRequestLobbyListResultCountFilter(int maxResults)
		{
			base.CheckIfUsable();
			NativeMethods.MatchMaking_AddRequestLobbyListResultCountFilter(maxResults);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002FA6 File Offset: 0x000011A6
		public void AddRequestLobbyListCompatibleMembersFilter(SteamID steamIDLobby)
		{
			base.CheckIfUsable();
			NativeMethods.MatchMaking_AddRequestLobbyListCompatibleMembersFilter(steamIDLobby.AsUInt64);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002FBA File Offset: 0x000011BA
		public SteamID GetLobbyByIndex(int lobby)
		{
			base.CheckIfUsable();
			return new SteamID(NativeMethods.MatchMaking_GetLobbyByIndex(lobby));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002FCD File Offset: 0x000011CD
		public void CreateLobby(LobbyType lobbyType, int maxMembers)
		{
			base.CheckIfUsable();
			NativeMethods.MatchMaking_CreateLobby((int)lobbyType, maxMembers);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002FDC File Offset: 0x000011DC
		public void JoinLobby(SteamID steamIDLobby)
		{
			base.CheckIfUsable();
			NativeMethods.MatchMaking_JoinLobby(steamIDLobby.AsUInt64);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002FF0 File Offset: 0x000011F0
		public void LeaveLobby(SteamID steamIDLobby)
		{
			base.CheckIfUsable();
			NativeMethods.MatchMaking_LeaveLobby(steamIDLobby.AsUInt64);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003004 File Offset: 0x00001204
		public bool InviteUserToLobby(SteamID steamIDLobby, SteamID steamIDInvitee)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_InviteUserToLobby(steamIDLobby.AsUInt64, steamIDInvitee.AsUInt64);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000301F File Offset: 0x0000121F
		public int GetNumLobbyMembers(SteamID steamIDLobby)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_GetNumLobbyMembers(steamIDLobby.AsUInt64);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003033 File Offset: 0x00001233
		public SteamID GetLobbyMemberByIndex(SteamID steamIDLobby, int member)
		{
			base.CheckIfUsable();
			return new SteamID(NativeMethods.MatchMaking_GetLobbyMemberByIndex(steamIDLobby.AsUInt64, member));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000304D File Offset: 0x0000124D
		public string GetLobbyData(SteamID steamIDLobby, string key)
		{
			base.CheckIfUsable();
			return NativeHelpers.ToStringAnsi(NativeMethods.MatchMaking_GetLobbyData(steamIDLobby.AsUInt64, key));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003067 File Offset: 0x00001267
		public bool SetLobbyData(SteamID steamIDLobby, string key, string value)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_SetLobbyData(steamIDLobby.AsUInt64, key, value);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000307D File Offset: 0x0000127D
		public int GetLobbyDataCount(SteamID steamIDLobby)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_GetLobbyDataCount(steamIDLobby.AsUInt64);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003094 File Offset: 0x00001294
		public bool GetLobbyDataByIndex(SteamID steamIDLobby, int lobbyData, out string key, out string value)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(255))
			{
				using (NativeBuffer nativeBuffer2 = new NativeBuffer(255))
				{
					bool flag = NativeMethods.MatchMaking_GetLobbyDataByIndex(steamIDLobby.AsUInt64, lobbyData, nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize, nativeBuffer2.UnmanagedMemory, nativeBuffer2.UnmanagedSize);
					key = NativeHelpers.ToStringAnsi(nativeBuffer.UnmanagedMemory);
					value = NativeHelpers.ToStringAnsi(nativeBuffer2.UnmanagedMemory);
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003134 File Offset: 0x00001334
		public MatchMakingGetLobbyDataByIndexResult GetLobbyDataByIndex(SteamID steamIDLobby, int lobbyData)
		{
			MatchMakingGetLobbyDataByIndexResult result = default(MatchMakingGetLobbyDataByIndexResult);
			result.Result = this.GetLobbyDataByIndex(steamIDLobby, lobbyData, out result.Key, out result.Value);
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003167 File Offset: 0x00001367
		public bool DeleteLobbyData(SteamID steamIDLobby, string key)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_DeleteLobbyData(steamIDLobby.AsUInt64, key);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000317C File Offset: 0x0000137C
		public string GetLobbyMemberData(SteamID steamIDLobby, SteamID steamIDUser, string key)
		{
			base.CheckIfUsable();
			return NativeHelpers.ToStringAnsi(NativeMethods.MatchMaking_GetLobbyMemberData(steamIDLobby.AsUInt64, steamIDUser.AsUInt64, key));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000319D File Offset: 0x0000139D
		public void SetLobbyMemberData(SteamID steamIDLobby, string key, string value)
		{
			base.CheckIfUsable();
			NativeMethods.MatchMaking_SetLobbyMemberData(steamIDLobby.AsUInt64, key, value);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000031B4 File Offset: 0x000013B4
		public bool SendLobbyChatMsg(SteamID steamIDLobby, byte[] msgBody)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(msgBody))
			{
				nativeBuffer.WriteToUnmanagedMemory();
				result = NativeMethods.MatchMaking_SendLobbyChatMsg(steamIDLobby.AsUInt64, nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize);
			}
			return result;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000320C File Offset: 0x0000140C
		public int GetLobbyChatEntry(SteamID steamIDLobby, int chatID, out SteamID steamIDUser, byte[] data, out ChatEntryType chatEntryType)
		{
			base.CheckIfUsable();
			ulong value = 0UL;
			int num = 0;
			int result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(data))
			{
				int num2 = NativeMethods.MatchMaking_GetLobbyChatEntry(steamIDLobby.AsUInt64, chatID, ref value, nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize, ref num);
				nativeBuffer.ReadFromUnmanagedMemory(num2);
				steamIDUser = new SteamID(value);
				chatEntryType = (ChatEntryType)num;
				result = num2;
			}
			return result;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003284 File Offset: 0x00001484
		public MatchmakingGetLobbyChatEntryResult GetLobbyChatEntry(SteamID steamIDLobby, int chatID, byte[] data)
		{
			MatchmakingGetLobbyChatEntryResult result = default(MatchmakingGetLobbyChatEntryResult);
			result.Result = this.GetLobbyChatEntry(steamIDLobby, chatID, out result.SteamIDUser, data, out result.ChatEntryType);
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000032B8 File Offset: 0x000014B8
		void IMatchmaking.SetLobbyGameServer(SteamID steamIDLobby, uint gameServerIP, ushort gameServerPort, SteamID steamIDGameServer)
		{
			base.CheckIfUsable();
			NativeMethods.MatchMaking_SetLobbyGameServer(steamIDLobby.AsUInt64, gameServerIP, gameServerPort, steamIDGameServer.AsUInt64);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000032D8 File Offset: 0x000014D8
		public bool GetLobbyGameServer(SteamID steamIDLobby, out uint gameServerIP, out ushort gameServerPort, out SteamID steamIDGameServer)
		{
			base.CheckIfUsable();
			gameServerIP = 0U;
			gameServerPort = 0;
			ulong value = 0UL;
			bool result = NativeMethods.MatchMaking_GetLobbyGameServer(steamIDLobby.AsUInt64, ref gameServerIP, ref gameServerPort, ref value);
			steamIDGameServer = new SteamID(value);
			return result;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003314 File Offset: 0x00001514
		public MatchMakingGetLobbyGameServerResult GetLobbyGameServer(SteamID steamIDLobby)
		{
			MatchMakingGetLobbyGameServerResult result = default(MatchMakingGetLobbyGameServerResult);
			result.Result = this.GetLobbyGameServer(steamIDLobby, out result.GameServerIP, out result.GameServerPort, out result.SteamIDGameServer);
			return result;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000334D File Offset: 0x0000154D
		public bool SetLobbyMemberLimit(SteamID steamIDLobby, int maxMembers)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_SetLobbyMemberLimit(steamIDLobby.AsUInt64, maxMembers);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003362 File Offset: 0x00001562
		public int GetLobbyMemberLimit(SteamID steamIDLobby)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_GetLobbyMemberLimit(steamIDLobby.AsUInt64);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003376 File Offset: 0x00001576
		public bool SetLobbyType(SteamID steamIDLobby, LobbyType lobbyType)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_SetLobbyType(steamIDLobby.AsUInt64, (int)lobbyType);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000338B File Offset: 0x0000158B
		public bool SetLobbyJoinable(SteamID steamIDLobby, bool lobbyJoinable)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_SetLobbyJoinable(steamIDLobby.AsUInt64, lobbyJoinable);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000033A0 File Offset: 0x000015A0
		public SteamID GetLobbyOwner(SteamID steamIDLobby)
		{
			base.CheckIfUsable();
			return new SteamID(NativeMethods.MatchMaking_GetLobbyOwner(steamIDLobby.AsUInt64));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000033B9 File Offset: 0x000015B9
		public bool SetLobbyOwner(SteamID steamIDLobby, SteamID steamIDNewOwner)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_SetLobbyOwner(steamIDLobby.AsUInt64, steamIDNewOwner.AsUInt64);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000033D4 File Offset: 0x000015D4
		public bool SetLinkedLobby(SteamID steamIDLobby, SteamID steamIDLobbyDependent)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_SetLobbyOwner(steamIDLobby.AsUInt64, steamIDLobbyDependent.AsUInt64);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000033EF File Offset: 0x000015EF
		public bool RequestLobbyData(SteamID steamIDLobby)
		{
			base.CheckIfUsable();
			return NativeMethods.MatchMaking_RequestLobbyData(steamIDLobby.AsUInt64);
		}

		// Token: 0x04000035 RID: 53
		private List<FavoritesListChanged> favoritesListChanged;

		// Token: 0x04000036 RID: 54
		private List<LobbyInvite> lobbyInvite;

		// Token: 0x04000037 RID: 55
		private List<SteamService.Result<LobbyEnter>> lobbyEnter;

		// Token: 0x04000038 RID: 56
		private List<LobbyDataUpdate> lobbyDataUpdate;

		// Token: 0x04000039 RID: 57
		private List<LobbyChatUpdate> lobbyChatUpdate;

		// Token: 0x0400003A RID: 58
		private List<LobbyChatMsg> lobbyChatMsg;

		// Token: 0x0400003B RID: 59
		private List<LobbyGameCreated> lobbyGameCreated;

		// Token: 0x0400003C RID: 60
		private List<SteamService.Result<LobbyMatchList>> lobbyMatchList;

		// Token: 0x0400003D RID: 61
		private List<LobbyKicked> lobbyKicked;

		// Token: 0x0400003E RID: 62
		private List<SteamService.Result<LobbyCreated>> lobbyCreated;
	}
}
