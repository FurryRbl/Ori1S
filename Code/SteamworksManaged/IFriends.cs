using System;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x02000020 RID: 32
	public interface IFriends
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060000E9 RID: 233
		// (remove) Token: 0x060000EA RID: 234
		event CallbackEvent<PersonaStateChange> PersonaStateChange;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060000EB RID: 235
		// (remove) Token: 0x060000EC RID: 236
		event CallbackEvent<GameOverlayActivated> GameOverlayActivated;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060000ED RID: 237
		// (remove) Token: 0x060000EE RID: 238
		event CallbackEvent<GameServerChangeRequested> GameServerChangeRequested;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060000EF RID: 239
		// (remove) Token: 0x060000F0 RID: 240
		event CallbackEvent<GameLobbyJoinRequested> GameLobbyJoinRequested;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060000F1 RID: 241
		// (remove) Token: 0x060000F2 RID: 242
		event CallbackEvent<AvatarImageLoaded> AvatarImageLoaded;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060000F3 RID: 243
		// (remove) Token: 0x060000F4 RID: 244
		event ResultEvent<ClanOfficerListResponse> ClanOfficerListResponseResult;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060000F5 RID: 245
		// (remove) Token: 0x060000F6 RID: 246
		event CallbackEvent<FriendRichPresenceUpdate> FriendRichPresenceUpdate;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060000F7 RID: 247
		// (remove) Token: 0x060000F8 RID: 248
		event CallbackEvent<GameRichPresenceJoinRequested> GameRichPresenceJoinRequested;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060000F9 RID: 249
		// (remove) Token: 0x060000FA RID: 250
		event CallbackEvent<GameConnectedClanChatMsg> GameConnectedClanChatMsg;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060000FB RID: 251
		// (remove) Token: 0x060000FC RID: 252
		event CallbackEvent<GameConnectedChatJoin> GameConnectedChatJoin;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060000FD RID: 253
		// (remove) Token: 0x060000FE RID: 254
		event CallbackEvent<GameConnectedChatLeave> GameConnectedChatLeave;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060000FF RID: 255
		// (remove) Token: 0x06000100 RID: 256
		event ResultEvent<DownloadClanActivityCountsResult> DownloadClanActivityCountsResultResult;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000101 RID: 257
		// (remove) Token: 0x06000102 RID: 258
		event ResultEvent<JoinClanChatRoomCompletionResult> JoinClanChatRoomCompletionResultResult;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000103 RID: 259
		// (remove) Token: 0x06000104 RID: 260
		event CallbackEvent<GameConnectedFriendChatMsg> GameConnectedFriendChatMsg;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000105 RID: 261
		// (remove) Token: 0x06000106 RID: 262
		event ResultEvent<FriendsGetFollowerCount> FriendsGetFollowerCount;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000107 RID: 263
		// (remove) Token: 0x06000108 RID: 264
		event ResultEvent<FriendsIsFollowing> FriendsIsFollowing;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000109 RID: 265
		// (remove) Token: 0x0600010A RID: 266
		event ResultEvent<FriendsEnumerateFollowingList> FriendsEnumerateFollowingList;

		// Token: 0x0600010B RID: 267
		string GetPersonaName();

		// Token: 0x0600010C RID: 268
		void SetPersonaName(string personaName);

		// Token: 0x0600010D RID: 269
		PersonaState GetPersonaState();

		// Token: 0x0600010E RID: 270
		int GetFriendCount(FriendFlags friendFlags);

		// Token: 0x0600010F RID: 271
		SteamID GetFriendByIndex(int friendIndex, FriendFlags friendFlags);

		// Token: 0x06000110 RID: 272
		FriendRelationship GetFriendRelationship(SteamID steamIDFriend);

		// Token: 0x06000111 RID: 273
		PersonaState GetFriendPersonaState(SteamID steamIDFriend);

		// Token: 0x06000112 RID: 274
		string GetFriendPersonaName(SteamID steamIDFriend);

		// Token: 0x06000113 RID: 275
		bool GetFriendGamePlayed(SteamID steamIDFriend, out FriendGameInfo friendGameInfo);

		// Token: 0x06000114 RID: 276
		FriendsGetFriendGamePlayedResult GetFriendGamePlayed(SteamID steamIDFriend);

		// Token: 0x06000115 RID: 277
		string GetFriendPersonaNameHistory(SteamID steamIDFriend, int personaName);

		// Token: 0x06000116 RID: 278
		string GetPlayerNickname(SteamID steamIDPlayer);

		// Token: 0x06000117 RID: 279
		bool HasFriend(SteamID steamIDFriend, FriendFlags friendFlags);

		// Token: 0x06000118 RID: 280
		int GetClanCount();

		// Token: 0x06000119 RID: 281
		SteamID GetClanByIndex(int clan);

		// Token: 0x0600011A RID: 282
		string GetClanName(SteamID steamIDClan);

		// Token: 0x0600011B RID: 283
		string GetClanTag(SteamID steamIDClan);

		// Token: 0x0600011C RID: 284
		bool GetClanActivityCounts(SteamID steamIDClan, out int online, out int inGame, out int chatting);

		// Token: 0x0600011D RID: 285
		FriendsGetClanActivityCountsResult GetClanActivityCounts(SteamID steamIDClan);

		// Token: 0x0600011E RID: 286
		void DownloadClanActivityCounts(SteamID[] clanIDs);

		// Token: 0x0600011F RID: 287
		int GetFriendCountFromSource(SteamID steamIDSource);

		// Token: 0x06000120 RID: 288
		SteamID GetFriendFromSourceByIndex(SteamID steamIDSource, int friendIndex);

		// Token: 0x06000121 RID: 289
		bool IsUserInSource(SteamID steamIDUser, SteamID steamIDSource);

		// Token: 0x06000122 RID: 290
		void SetInGameVoiceSpeaking(SteamID steamIDUser, bool speaking);

		// Token: 0x06000123 RID: 291
		void ActivateGameOverlay(OverlayDialog dialogType);

		// Token: 0x06000124 RID: 292
		void ActivateGameOverlayToUser(OverlayDialogToUser dialogType, SteamID steamID);

		// Token: 0x06000125 RID: 293
		void ActivateGameOverlayToWebPage(string url);

		// Token: 0x06000126 RID: 294
		void ActivateGameOverlayToStore(AppID appID, OverlayToStoreFlag flag);

		// Token: 0x06000127 RID: 295
		void SetPlayedWith(SteamID steamIDUserPlayedWith);

		// Token: 0x06000128 RID: 296
		void ActivateGameOverlayInviteDialog(SteamID steamIDLobby);

		// Token: 0x06000129 RID: 297
		ImageHandle GetSmallFriendAvatar(SteamID steamIDFriend);

		// Token: 0x0600012A RID: 298
		ImageHandle GetMediumFriendAvatar(SteamID steamIDFriend);

		// Token: 0x0600012B RID: 299
		ImageHandle GetLargeFriendAvatar(SteamID steamIDFriend);

		// Token: 0x0600012C RID: 300
		bool RequestUserInformation(SteamID steamIDUser, bool requireNameOnly);

		// Token: 0x0600012D RID: 301
		void RequestClanOfficerList(SteamID steamIDClan);

		// Token: 0x0600012E RID: 302
		SteamID GetClanOwner(SteamID steamIDClan);

		// Token: 0x0600012F RID: 303
		int GetClanOfficerCount(SteamID steamIDClan);

		// Token: 0x06000130 RID: 304
		SteamID GetClanOfficerByIndex(SteamID steamIDClan, int officer);

		// Token: 0x06000131 RID: 305
		UserRestriction GetUserRestrictions();

		// Token: 0x06000132 RID: 306
		bool SetRichPresence(string key, string value);

		// Token: 0x06000133 RID: 307
		void ClearRichPresence();

		// Token: 0x06000134 RID: 308
		string GetFriendRichPresence(SteamID steamIDFriend, string key);

		// Token: 0x06000135 RID: 309
		int GetFriendRichPresenceKeyCount(SteamID steamIDFriend);

		// Token: 0x06000136 RID: 310
		string GetFriendRichPresenceKeyByIndex(SteamID steamIDFriend, int key);

		// Token: 0x06000137 RID: 311
		void RequestFriendRichPresence(SteamID steamIDFriend);

		// Token: 0x06000138 RID: 312
		bool InviteUserToGame(SteamID steamIDFriend, string connectString);

		// Token: 0x06000139 RID: 313
		int GetCoplayFriendCount();

		// Token: 0x0600013A RID: 314
		SteamID GetCoplayFriend(int coplayFriend);

		// Token: 0x0600013B RID: 315
		int GetFriendCoplayTime(SteamID steamIDFriend);

		// Token: 0x0600013C RID: 316
		AppID GetFriendCoplayGame(SteamID steamIDFriend);

		// Token: 0x0600013D RID: 317
		void JoinClanChatRoom(SteamID steamIDClan);

		// Token: 0x0600013E RID: 318
		bool LeaveClanChatRoom(SteamID steamIDClan);

		// Token: 0x0600013F RID: 319
		int GetClanChatMemberCount(SteamID steamIDClan);

		// Token: 0x06000140 RID: 320
		SteamID GetChatMemberByIndex(SteamID steamIDClan, int user);

		// Token: 0x06000141 RID: 321
		bool SendClanChatMessage(SteamID steamIDClanChat, string text);

		// Token: 0x06000142 RID: 322
		int GetClanChatMessage(SteamID steamIDClanChat, int message, int maxMessageSize, out string text, out ChatEntryType chatEntryType, out SteamID sender);

		// Token: 0x06000143 RID: 323
		FriendsGetClanChatMessageResult GetClanChatMessage(SteamID steamIDClanChat, int message, int maxMessageSize);

		// Token: 0x06000144 RID: 324
		bool IsClanChatAdmin(SteamID steamIDClanChat, SteamID steamIDUser);

		// Token: 0x06000145 RID: 325
		bool IsClanChatWindowOpenInSteam(SteamID steamIDClanChat);

		// Token: 0x06000146 RID: 326
		bool OpenClanChatWindowInSteam(SteamID steamIDClanChat);

		// Token: 0x06000147 RID: 327
		bool CloseClanChatWindowInSteam(SteamID steamIDClanChat);

		// Token: 0x06000148 RID: 328
		bool SetListenForFriendsMessages(bool interceptEnabled);

		// Token: 0x06000149 RID: 329
		bool ReplyToFriendMessage(SteamID steamIDFriend, string message);

		// Token: 0x0600014A RID: 330
		int GetFriendMessage(SteamID steamIDFriend, int messageID, int maxMessageSize, out string text, out ChatEntryType chatEntryType);

		// Token: 0x0600014B RID: 331
		FriendsGetFriendMessageResult GetFriendMessage(SteamID steamIDFriend, int messageID, int maxMessageSize);

		// Token: 0x0600014C RID: 332
		void GetFollowerCount(SteamID steamID);

		// Token: 0x0600014D RID: 333
		void IsFollowing(SteamID steamID);

		// Token: 0x0600014E RID: 334
		void EnumerateFollowingList(uint startIndex);
	}
}
