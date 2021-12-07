using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x02000058 RID: 88
	internal class Friends : SteamService, IFriends
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x000055A8 File Offset: 0x000037A8
		internal Friends()
		{
			this.personaStateChange = new List<PersonaStateChange>();
			this.gameOverlayActivated = new List<GameOverlayActivated>();
			this.gameServerChangeRequested = new List<GameServerChangeRequested>();
			this.gameLobbyJoinRequested = new List<GameLobbyJoinRequested>();
			this.avatarImageLoaded = new List<AvatarImageLoaded>();
			this.clanOfficerListResponse = new List<SteamService.Result<ClanOfficerListResponse>>();
			this.friendRichPresenceUpdate = new List<FriendRichPresenceUpdate>();
			this.gameRichPresenceJoinRequested = new List<GameRichPresenceJoinRequested>();
			this.gameConnectedClanChatMsg = new List<GameConnectedClanChatMsg>();
			this.gameConnectedChatJoin = new List<GameConnectedChatJoin>();
			this.gameConnectedChatLeave = new List<GameConnectedChatLeave>();
			this.downloadClanActivityCountsResult = new List<SteamService.Result<DownloadClanActivityCountsResult>>();
			this.joinClanChatRoomCompletionResult = new List<SteamService.Result<JoinClanChatRoomCompletionResult>>();
			this.gameConnectedFriendChatMsg = new List<GameConnectedFriendChatMsg>();
			this.friendsGetFollowerCount = new List<SteamService.Result<FriendsGetFollowerCount>>();
			this.friendsIsFollowing = new List<SteamService.Result<FriendsIsFollowing>>();
			this.friendsEnumerateFollowingList = new List<SteamService.Result<FriendsEnumerateFollowingList>>();
			SteamService.Callbacks[CallbackID.PersonaStateChange] = delegate(IntPtr data, int size)
			{
				this.personaStateChange.Add(ManagedSteam.CallbackStructures.PersonaStateChange.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.GameOverlayActivated] = delegate(IntPtr data, int size)
			{
				this.gameOverlayActivated.Add(ManagedSteam.CallbackStructures.GameOverlayActivated.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.GameServerChangeRequested] = delegate(IntPtr data, int size)
			{
				this.gameServerChangeRequested.Add(ManagedSteam.CallbackStructures.GameServerChangeRequested.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.GameLobbyJoinRequested] = delegate(IntPtr data, int size)
			{
				this.gameLobbyJoinRequested.Add(ManagedSteam.CallbackStructures.GameLobbyJoinRequested.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.AvatarImageLoaded] = delegate(IntPtr data, int size)
			{
				this.avatarImageLoaded.Add(ManagedSteam.CallbackStructures.AvatarImageLoaded.Create(data, size));
			};
			SteamService.Results[ResultID.ClanOfficerListResponse] = delegate(IntPtr data, int size, bool flag)
			{
				this.clanOfficerListResponse.Add(new SteamService.Result<ClanOfficerListResponse>(ClanOfficerListResponse.Create(data, size), flag));
			};
			SteamService.Callbacks[CallbackID.FriendRichPresenceUpdate] = delegate(IntPtr data, int size)
			{
				this.friendRichPresenceUpdate.Add(ManagedSteam.CallbackStructures.FriendRichPresenceUpdate.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.GameRichPresenceJoinRequested] = delegate(IntPtr data, int size)
			{
				this.gameRichPresenceJoinRequested.Add(ManagedSteam.CallbackStructures.GameRichPresenceJoinRequested.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.GameConnectedClanChatMsg] = delegate(IntPtr data, int size)
			{
				this.gameConnectedClanChatMsg.Add(ManagedSteam.CallbackStructures.GameConnectedClanChatMsg.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.GameConnectedChatJoin] = delegate(IntPtr data, int size)
			{
				this.gameConnectedChatJoin.Add(ManagedSteam.CallbackStructures.GameConnectedChatJoin.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.GameConnectedChatLeave] = delegate(IntPtr data, int size)
			{
				this.gameConnectedChatLeave.Add(ManagedSteam.CallbackStructures.GameConnectedChatLeave.Create(data, size));
			};
			SteamService.Results[ResultID.DownloadClanActivityCountsResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.downloadClanActivityCountsResult.Add(new SteamService.Result<DownloadClanActivityCountsResult>(DownloadClanActivityCountsResult.Create(data, size), flag));
			};
			SteamService.Results[ResultID.JoinClanChatRoomCompletionResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.joinClanChatRoomCompletionResult.Add(new SteamService.Result<JoinClanChatRoomCompletionResult>(JoinClanChatRoomCompletionResult.Create(data, size), flag));
			};
			SteamService.Callbacks[CallbackID.GameConnectedFriendChatMsg] = delegate(IntPtr data, int size)
			{
				this.gameConnectedFriendChatMsg.Add(ManagedSteam.CallbackStructures.GameConnectedFriendChatMsg.Create(data, size));
			};
			SteamService.Results[ResultID.FriendsGetFollowerCount] = delegate(IntPtr data, int size, bool flag)
			{
				this.friendsGetFollowerCount.Add(new SteamService.Result<FriendsGetFollowerCount>(ManagedSteam.CallbackStructures.FriendsGetFollowerCount.Create(data, size), flag));
			};
			SteamService.Results[ResultID.FriendsIsFollowing] = delegate(IntPtr data, int size, bool flag)
			{
				this.friendsIsFollowing.Add(new SteamService.Result<FriendsIsFollowing>(ManagedSteam.CallbackStructures.FriendsIsFollowing.Create(data, size), flag));
			};
			SteamService.Results[ResultID.FriendsEnumerateFollowingList] = delegate(IntPtr data, int size, bool flag)
			{
				this.friendsEnumerateFollowingList.Add(new SteamService.Result<FriendsEnumerateFollowingList>(ManagedSteam.CallbackStructures.FriendsEnumerateFollowingList.Create(data, size), flag));
			};
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060002A1 RID: 673 RVA: 0x000058B8 File Offset: 0x00003AB8
		// (remove) Token: 0x060002A2 RID: 674 RVA: 0x000058F0 File Offset: 0x00003AF0
		public event CallbackEvent<PersonaStateChange> PersonaStateChange;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060002A3 RID: 675 RVA: 0x00005928 File Offset: 0x00003B28
		// (remove) Token: 0x060002A4 RID: 676 RVA: 0x00005960 File Offset: 0x00003B60
		public event CallbackEvent<GameOverlayActivated> GameOverlayActivated;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060002A5 RID: 677 RVA: 0x00005998 File Offset: 0x00003B98
		// (remove) Token: 0x060002A6 RID: 678 RVA: 0x000059D0 File Offset: 0x00003BD0
		public event CallbackEvent<GameServerChangeRequested> GameServerChangeRequested;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060002A7 RID: 679 RVA: 0x00005A08 File Offset: 0x00003C08
		// (remove) Token: 0x060002A8 RID: 680 RVA: 0x00005A40 File Offset: 0x00003C40
		public event CallbackEvent<GameLobbyJoinRequested> GameLobbyJoinRequested;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060002A9 RID: 681 RVA: 0x00005A78 File Offset: 0x00003C78
		// (remove) Token: 0x060002AA RID: 682 RVA: 0x00005AB0 File Offset: 0x00003CB0
		public event CallbackEvent<AvatarImageLoaded> AvatarImageLoaded;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060002AB RID: 683 RVA: 0x00005AE8 File Offset: 0x00003CE8
		// (remove) Token: 0x060002AC RID: 684 RVA: 0x00005B20 File Offset: 0x00003D20
		public event ResultEvent<ClanOfficerListResponse> ClanOfficerListResponseResult;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060002AD RID: 685 RVA: 0x00005B58 File Offset: 0x00003D58
		// (remove) Token: 0x060002AE RID: 686 RVA: 0x00005B90 File Offset: 0x00003D90
		public event CallbackEvent<FriendRichPresenceUpdate> FriendRichPresenceUpdate;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060002AF RID: 687 RVA: 0x00005BC8 File Offset: 0x00003DC8
		// (remove) Token: 0x060002B0 RID: 688 RVA: 0x00005C00 File Offset: 0x00003E00
		public event CallbackEvent<GameRichPresenceJoinRequested> GameRichPresenceJoinRequested;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060002B1 RID: 689 RVA: 0x00005C38 File Offset: 0x00003E38
		// (remove) Token: 0x060002B2 RID: 690 RVA: 0x00005C70 File Offset: 0x00003E70
		public event CallbackEvent<GameConnectedClanChatMsg> GameConnectedClanChatMsg;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060002B3 RID: 691 RVA: 0x00005CA8 File Offset: 0x00003EA8
		// (remove) Token: 0x060002B4 RID: 692 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public event CallbackEvent<GameConnectedChatJoin> GameConnectedChatJoin;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060002B5 RID: 693 RVA: 0x00005D18 File Offset: 0x00003F18
		// (remove) Token: 0x060002B6 RID: 694 RVA: 0x00005D50 File Offset: 0x00003F50
		public event CallbackEvent<GameConnectedChatLeave> GameConnectedChatLeave;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060002B7 RID: 695 RVA: 0x00005D88 File Offset: 0x00003F88
		// (remove) Token: 0x060002B8 RID: 696 RVA: 0x00005DC0 File Offset: 0x00003FC0
		public event ResultEvent<DownloadClanActivityCountsResult> DownloadClanActivityCountsResultResult;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060002B9 RID: 697 RVA: 0x00005DF8 File Offset: 0x00003FF8
		// (remove) Token: 0x060002BA RID: 698 RVA: 0x00005E30 File Offset: 0x00004030
		public event ResultEvent<JoinClanChatRoomCompletionResult> JoinClanChatRoomCompletionResultResult;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x060002BB RID: 699 RVA: 0x00005E68 File Offset: 0x00004068
		// (remove) Token: 0x060002BC RID: 700 RVA: 0x00005EA0 File Offset: 0x000040A0
		public event CallbackEvent<GameConnectedFriendChatMsg> GameConnectedFriendChatMsg;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x060002BD RID: 701 RVA: 0x00005ED8 File Offset: 0x000040D8
		// (remove) Token: 0x060002BE RID: 702 RVA: 0x00005F10 File Offset: 0x00004110
		public event ResultEvent<FriendsGetFollowerCount> FriendsGetFollowerCount;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x060002BF RID: 703 RVA: 0x00005F48 File Offset: 0x00004148
		// (remove) Token: 0x060002C0 RID: 704 RVA: 0x00005F80 File Offset: 0x00004180
		public event ResultEvent<FriendsIsFollowing> FriendsIsFollowing;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x060002C1 RID: 705 RVA: 0x00005FB8 File Offset: 0x000041B8
		// (remove) Token: 0x060002C2 RID: 706 RVA: 0x00005FF0 File Offset: 0x000041F0
		public event ResultEvent<FriendsEnumerateFollowingList> FriendsEnumerateFollowingList;

		// Token: 0x060002C3 RID: 707 RVA: 0x00006025 File Offset: 0x00004225
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00006028 File Offset: 0x00004228
		internal override void ReleaseManagedResources()
		{
			this.personaStateChange = null;
			this.PersonaStateChange = null;
			this.gameOverlayActivated = null;
			this.GameOverlayActivated = null;
			this.gameServerChangeRequested = null;
			this.GameServerChangeRequested = null;
			this.gameLobbyJoinRequested = null;
			this.GameLobbyJoinRequested = null;
			this.avatarImageLoaded = null;
			this.AvatarImageLoaded = null;
			this.clanOfficerListResponse = null;
			this.ClanOfficerListResponseResult = null;
			this.friendRichPresenceUpdate = null;
			this.FriendRichPresenceUpdate = null;
			this.gameRichPresenceJoinRequested = null;
			this.GameRichPresenceJoinRequested = null;
			this.gameConnectedClanChatMsg = null;
			this.GameConnectedClanChatMsg = null;
			this.gameConnectedChatJoin = null;
			this.GameConnectedChatJoin = null;
			this.gameConnectedChatLeave = null;
			this.GameConnectedChatLeave = null;
			this.downloadClanActivityCountsResult = null;
			this.DownloadClanActivityCountsResultResult = null;
			this.joinClanChatRoomCompletionResult = null;
			this.JoinClanChatRoomCompletionResultResult = null;
			this.gameConnectedFriendChatMsg = null;
			this.GameConnectedFriendChatMsg = null;
			this.friendsGetFollowerCount = null;
			this.FriendsGetFollowerCount = null;
			this.friendsIsFollowing = null;
			this.FriendsIsFollowing = null;
			this.friendsEnumerateFollowingList = null;
			this.FriendsEnumerateFollowingList = null;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00006124 File Offset: 0x00004324
		internal override void InvokeEvents()
		{
			SteamService.InvokeEvents<PersonaStateChange>(this.personaStateChange, this.PersonaStateChange);
			SteamService.InvokeEvents<GameOverlayActivated>(this.gameOverlayActivated, this.GameOverlayActivated);
			SteamService.InvokeEvents<GameServerChangeRequested>(this.gameServerChangeRequested, this.GameServerChangeRequested);
			SteamService.InvokeEvents<GameLobbyJoinRequested>(this.gameLobbyJoinRequested, this.GameLobbyJoinRequested);
			SteamService.InvokeEvents<AvatarImageLoaded>(this.avatarImageLoaded, this.AvatarImageLoaded);
			SteamService.InvokeEvents<ClanOfficerListResponse>(this.clanOfficerListResponse, this.ClanOfficerListResponseResult);
			SteamService.InvokeEvents<FriendRichPresenceUpdate>(this.friendRichPresenceUpdate, this.FriendRichPresenceUpdate);
			SteamService.InvokeEvents<GameRichPresenceJoinRequested>(this.gameRichPresenceJoinRequested, this.GameRichPresenceJoinRequested);
			SteamService.InvokeEvents<GameConnectedClanChatMsg>(this.gameConnectedClanChatMsg, this.GameConnectedClanChatMsg);
			SteamService.InvokeEvents<GameConnectedChatJoin>(this.gameConnectedChatJoin, this.GameConnectedChatJoin);
			SteamService.InvokeEvents<GameConnectedChatLeave>(this.gameConnectedChatLeave, this.GameConnectedChatLeave);
			SteamService.InvokeEvents<DownloadClanActivityCountsResult>(this.downloadClanActivityCountsResult, this.DownloadClanActivityCountsResultResult);
			SteamService.InvokeEvents<JoinClanChatRoomCompletionResult>(this.joinClanChatRoomCompletionResult, this.JoinClanChatRoomCompletionResultResult);
			SteamService.InvokeEvents<GameConnectedFriendChatMsg>(this.gameConnectedFriendChatMsg, this.GameConnectedFriendChatMsg);
			SteamService.InvokeEvents<FriendsGetFollowerCount>(this.friendsGetFollowerCount, this.FriendsGetFollowerCount);
			SteamService.InvokeEvents<FriendsIsFollowing>(this.friendsIsFollowing, this.FriendsIsFollowing);
			SteamService.InvokeEvents<FriendsEnumerateFollowingList>(this.friendsEnumerateFollowingList, this.FriendsEnumerateFollowingList);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00006252 File Offset: 0x00004452
		public string GetPersonaName()
		{
			base.CheckIfUsable();
			return NativeHelpers.ToStringUtf8(NativeMethods.Friends_GetPersonaName());
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00006264 File Offset: 0x00004464
		public void SetPersonaName(string personaName)
		{
			base.CheckIfUsable();
			using (NativeString nativeString = new NativeString(personaName))
			{
				NativeMethods.Friends_SetPersonaName(nativeString.ToNativeAsUtf8());
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000062A8 File Offset: 0x000044A8
		public PersonaState GetPersonaState()
		{
			base.CheckIfUsable();
			return (PersonaState)NativeMethods.Friends_GetPersonaState();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000062B5 File Offset: 0x000044B5
		public int GetFriendCount(FriendFlags friendFlags)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_GetFriendCount((int)friendFlags);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000062C3 File Offset: 0x000044C3
		public SteamID GetFriendByIndex(int friendIndex, FriendFlags friendFlags)
		{
			base.CheckIfUsable();
			return new SteamID(NativeMethods.Friends_GetFriendByIndex(friendIndex, (int)friendFlags));
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000062D7 File Offset: 0x000044D7
		public FriendRelationship GetFriendRelationship(SteamID steamIDFriend)
		{
			base.CheckIfUsable();
			return (FriendRelationship)NativeMethods.Friends_GetFriendRelationship(steamIDFriend.AsUInt64);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000062EB File Offset: 0x000044EB
		public PersonaState GetFriendPersonaState(SteamID steamIDFriend)
		{
			base.CheckIfUsable();
			return (PersonaState)NativeMethods.Friends_GetFriendPersonaState(steamIDFriend.AsUInt64);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00006300 File Offset: 0x00004500
		public string GetFriendPersonaName(SteamID steamIDFriend)
		{
			base.CheckIfUsable();
			IntPtr pointer = NativeMethods.Friends_GetFriendPersonaName(steamIDFriend.AsUInt64);
			return NativeHelpers.ToStringUtf8(pointer);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00006328 File Offset: 0x00004528
		public bool GetFriendGamePlayed(SteamID steamIDFriend, out FriendGameInfo friendGameInfo)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(Marshal.SizeOf(typeof(FriendGameInfo))))
			{
				if (nativeBuffer.UnmanagedSize != NativeMethods.Friends_GetFriendGameInfoSize())
				{
					Error.ThrowError(ErrorCodes.CallbackStructSizeMissmatch, new object[]
					{
						typeof(FriendGameInfo).Name
					});
				}
				bool flag = NativeMethods.Friends_GetFriendGamePlayed(steamIDFriend.AsUInt64, nativeBuffer.UnmanagedMemory);
				friendGameInfo = FriendGameInfo.Create(nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize);
				result = flag;
			}
			return result;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000063CC File Offset: 0x000045CC
		public FriendsGetFriendGamePlayedResult GetFriendGamePlayed(SteamID steamIDFriend)
		{
			FriendsGetFriendGamePlayedResult result = default(FriendsGetFriendGamePlayedResult);
			result.Result = this.GetFriendGamePlayed(steamIDFriend, out result.FriendGameInfo);
			return result;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000063F8 File Offset: 0x000045F8
		public string GetFriendPersonaNameHistory(SteamID steamIDFriend, int personaName)
		{
			base.CheckIfUsable();
			IntPtr pointer = NativeMethods.Friends_GetFriendPersonaNameHistory(steamIDFriend.AsUInt64, personaName);
			return NativeHelpers.ToStringUtf8(pointer);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00006420 File Offset: 0x00004620
		public string GetPlayerNickname(SteamID steamIDPlayer)
		{
			base.CheckIfUsable();
			IntPtr pointer = NativeMethods.Friends_GetPlayerNickname(steamIDPlayer.AsUInt64);
			return NativeHelpers.ToStringUtf8(pointer);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00006446 File Offset: 0x00004646
		public bool HasFriend(SteamID steamIDFriend, FriendFlags friendFlags)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_HasFriend(steamIDFriend.AsUInt64, (int)friendFlags);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000645B File Offset: 0x0000465B
		public int GetClanCount()
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_GetClanCount();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00006468 File Offset: 0x00004668
		public SteamID GetClanByIndex(int clan)
		{
			base.CheckIfUsable();
			return new SteamID(NativeMethods.Friends_GetClanByIndex(clan));
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000647B File Offset: 0x0000467B
		public string GetClanName(SteamID steamIDClan)
		{
			base.CheckIfUsable();
			return NativeHelpers.ToStringUtf8(NativeMethods.Friends_GetClanName(steamIDClan.AsUInt64));
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00006494 File Offset: 0x00004694
		public string GetClanTag(SteamID steamIDClan)
		{
			base.CheckIfUsable();
			return NativeHelpers.ToStringUtf8(NativeMethods.Friends_GetClanTag(steamIDClan.AsUInt64));
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000064AD File Offset: 0x000046AD
		public bool GetClanActivityCounts(SteamID steamIDClan, out int online, out int inGame, out int chatting)
		{
			base.CheckIfUsable();
			online = 0;
			inGame = 0;
			chatting = 0;
			return NativeMethods.Friends_GetClanActivityCounts(steamIDClan.AsUInt64, ref online, ref inGame, ref chatting);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000064D0 File Offset: 0x000046D0
		public FriendsGetClanActivityCountsResult GetClanActivityCounts(SteamID steamIDClan)
		{
			FriendsGetClanActivityCountsResult result = default(FriendsGetClanActivityCountsResult);
			result.Result = this.GetClanActivityCounts(steamIDClan, out result.Online, out result.InGame, out result.Chatting);
			return result;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000650C File Offset: 0x0000470C
		public void DownloadClanActivityCounts(SteamID[] clanIDs)
		{
			base.CheckIfUsable();
			byte[] managedData = NativeBuffer.ToBytes(clanIDs);
			using (NativeBuffer nativeBuffer = new NativeBuffer(managedData))
			{
				NativeMethods.Friends_DownloadClanActivityCounts(nativeBuffer.UnmanagedMemory, clanIDs.Length);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00006558 File Offset: 0x00004758
		public int GetFriendCountFromSource(SteamID steamIDSource)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_GetFriendCountFromSource(steamIDSource.AsUInt64);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000656C File Offset: 0x0000476C
		public SteamID GetFriendFromSourceByIndex(SteamID steamIDSource, int friendIndex)
		{
			base.CheckIfUsable();
			return new SteamID(NativeMethods.Friends_GetFriendFromSourceByIndex(steamIDSource.AsUInt64, friendIndex));
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00006586 File Offset: 0x00004786
		public bool IsUserInSource(SteamID steamIDUser, SteamID steamIDSource)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_IsUserInSource(steamIDUser.AsUInt64, steamIDSource.AsUInt64);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000065A1 File Offset: 0x000047A1
		public void SetInGameVoiceSpeaking(SteamID steamIDUser, bool speaking)
		{
			base.CheckIfUsable();
			NativeMethods.Friends_SetInGameVoiceSpeaking(steamIDUser.AsUInt64, speaking);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000065B6 File Offset: 0x000047B6
		public void ActivateGameOverlay(OverlayDialog dialogType)
		{
			base.CheckIfUsable();
			NativeMethods.Friends_ActivateGameOverlay((int)dialogType);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000065C4 File Offset: 0x000047C4
		public void ActivateGameOverlayToUser(OverlayDialogToUser dialogType, SteamID steamID)
		{
			base.CheckIfUsable();
			NativeMethods.Friends_ActivateGameOverlayToUser((int)dialogType, steamID.AsUInt64);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000065D9 File Offset: 0x000047D9
		public void ActivateGameOverlayToWebPage(string url)
		{
			base.CheckIfUsable();
			NativeMethods.Friends_ActivateGameOverlayToWebPage(url);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000065E7 File Offset: 0x000047E7
		public void ActivateGameOverlayToStore(AppID appID, OverlayToStoreFlag flag)
		{
			base.CheckIfUsable();
			NativeMethods.Friends_ActivateGameOverlayToStore(appID.AsUInt32, (int)flag);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000065FC File Offset: 0x000047FC
		public void SetPlayedWith(SteamID steamIDUserPlayedWith)
		{
			base.CheckIfUsable();
			NativeMethods.Friends_SetPlayedWith(steamIDUserPlayedWith.AsUInt64);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00006610 File Offset: 0x00004810
		public void ActivateGameOverlayInviteDialog(SteamID steamIDLobby)
		{
			base.CheckIfUsable();
			NativeMethods.Friends_ActivateGameOverlayInviteDialog(steamIDLobby.AsUInt64);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00006624 File Offset: 0x00004824
		public ImageHandle GetSmallFriendAvatar(SteamID steamIDFriend)
		{
			base.CheckIfUsable();
			int value = NativeMethods.Friends_GetSmallFriendAvatar(steamIDFriend.AsUInt64);
			return new ImageHandle(value);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000664C File Offset: 0x0000484C
		public ImageHandle GetMediumFriendAvatar(SteamID steamIDFriend)
		{
			base.CheckIfUsable();
			int value = NativeMethods.Friends_GetMediumFriendAvatar(steamIDFriend.AsUInt64);
			return new ImageHandle(value);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00006674 File Offset: 0x00004874
		public ImageHandle GetLargeFriendAvatar(SteamID steamIDFriend)
		{
			base.CheckIfUsable();
			int value = NativeMethods.Friends_GetLargeFriendAvatar(steamIDFriend.AsUInt64);
			return new ImageHandle(value);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000669A File Offset: 0x0000489A
		public bool RequestUserInformation(SteamID steamIDUser, bool requireNameOnly)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_RequestUserInformation(steamIDUser.AsUInt64, requireNameOnly);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000066AF File Offset: 0x000048AF
		public void RequestClanOfficerList(SteamID steamIDClan)
		{
			base.CheckIfUsable();
			NativeMethods.Friends_RequestClanOfficerList(steamIDClan.AsUInt64);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000066C4 File Offset: 0x000048C4
		public SteamID GetClanOwner(SteamID steamIDClan)
		{
			base.CheckIfUsable();
			ulong value = NativeMethods.Friends_GetClanOwner(steamIDClan.AsUInt64);
			return new SteamID(value);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000066EA File Offset: 0x000048EA
		public int GetClanOfficerCount(SteamID steamIDClan)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_GetClanOfficerCount(steamIDClan.AsUInt64);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00006700 File Offset: 0x00004900
		public SteamID GetClanOfficerByIndex(SteamID steamIDClan, int officer)
		{
			base.CheckIfUsable();
			ulong value = NativeMethods.Friends_GetClanOfficerByIndex(steamIDClan.AsUInt64, officer);
			return new SteamID(value);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00006727 File Offset: 0x00004927
		public UserRestriction GetUserRestrictions()
		{
			base.CheckIfUsable();
			return (UserRestriction)NativeMethods.Friends_GetUserRestrictions();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00006734 File Offset: 0x00004934
		public bool SetRichPresence(string key, string value)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeString nativeString = new NativeString(value))
			{
				result = NativeMethods.Friends_SetRichPresence(key, nativeString.ToNativeAsUtf8());
			}
			return result;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00006778 File Offset: 0x00004978
		public void ClearRichPresence()
		{
			base.CheckIfUsable();
			NativeMethods.Friends_ClearRichPresence();
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00006788 File Offset: 0x00004988
		public string GetFriendRichPresence(SteamID steamIDFriend, string key)
		{
			base.CheckIfUsable();
			IntPtr pointer = NativeMethods.Friends_GetFriendRichPresence(steamIDFriend.AsUInt64, key);
			return NativeHelpers.ToStringUtf8(pointer);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000067AF File Offset: 0x000049AF
		public int GetFriendRichPresenceKeyCount(SteamID steamIDFriend)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_GetFriendRichPresenceKeyCount(steamIDFriend.AsUInt64);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000067C4 File Offset: 0x000049C4
		public string GetFriendRichPresenceKeyByIndex(SteamID steamIDFriend, int key)
		{
			base.CheckIfUsable();
			IntPtr pointer = NativeMethods.Friends_GetFriendRichPresenceKeyByIndex(steamIDFriend.AsUInt64, key);
			return NativeHelpers.ToStringUtf8(pointer);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000067EB File Offset: 0x000049EB
		public void RequestFriendRichPresence(SteamID steamIDFriend)
		{
			base.CheckIfUsable();
			NativeMethods.Friends_RequestFriendRichPresence(steamIDFriend.AsUInt64);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000067FF File Offset: 0x000049FF
		public bool InviteUserToGame(SteamID steamIDFriend, string connectString)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_InviteUserToGame(steamIDFriend.AsUInt64, connectString);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00006814 File Offset: 0x00004A14
		public int GetCoplayFriendCount()
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_GetCoplayFriendCount();
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00006824 File Offset: 0x00004A24
		public SteamID GetCoplayFriend(int coplayFriend)
		{
			base.CheckIfUsable();
			ulong value = NativeMethods.Friends_GetCoplayFriend(coplayFriend);
			return new SteamID(value);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00006844 File Offset: 0x00004A44
		public int GetFriendCoplayTime(SteamID steamIDFriend)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_GetFriendCoplayTime(steamIDFriend.AsUInt64);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00006858 File Offset: 0x00004A58
		public AppID GetFriendCoplayGame(SteamID steamIDFriend)
		{
			base.CheckIfUsable();
			return new AppID(NativeMethods.Friends_GetFriendCoplayGame(steamIDFriend.AsUInt64));
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00006871 File Offset: 0x00004A71
		public void JoinClanChatRoom(SteamID steamIDClan)
		{
			base.CheckIfUsable();
			NativeMethods.Friends_JoinClanChatRoom(steamIDClan.AsUInt64);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00006885 File Offset: 0x00004A85
		public bool LeaveClanChatRoom(SteamID steamIDClan)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_LeaveClanChatRoom(steamIDClan.AsUInt64);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00006899 File Offset: 0x00004A99
		public int GetClanChatMemberCount(SteamID steamIDClan)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_GetClanChatMemberCount(steamIDClan.AsUInt64);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000068B0 File Offset: 0x00004AB0
		public SteamID GetChatMemberByIndex(SteamID steamIDClan, int user)
		{
			base.CheckIfUsable();
			ulong value = NativeMethods.Friends_GetChatMemberByIndex(steamIDClan.AsUInt64, user);
			return new SteamID(value);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000068D8 File Offset: 0x00004AD8
		public bool SendClanChatMessage(SteamID steamIDClanChat, string text)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeString nativeString = new NativeString(text))
			{
				result = NativeMethods.Friends_SendClanChatMessage(steamIDClanChat.AsUInt64, nativeString.ToNativeAsUtf8());
			}
			return result;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00006924 File Offset: 0x00004B24
		public int GetClanChatMessage(SteamID steamIDClanChat, int message, int maxMessageSize, out string text, out ChatEntryType chatEntryType, out SteamID sender)
		{
			base.CheckIfUsable();
			int num = 0;
			ulong value = 0UL;
			int result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(maxMessageSize))
			{
				int num2 = NativeMethods.Friends_GetClanChatMessage(steamIDClanChat.AsUInt64, message, nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize, ref num, ref value);
				chatEntryType = (ChatEntryType)num;
				sender = new SteamID(value);
				text = NativeHelpers.ToStringUtf8(nativeBuffer.UnmanagedMemory);
				result = num2;
			}
			return result;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000069A4 File Offset: 0x00004BA4
		public FriendsGetClanChatMessageResult GetClanChatMessage(SteamID steamIDClanChat, int message, int maxMessageSize)
		{
			FriendsGetClanChatMessageResult result = default(FriendsGetClanChatMessageResult);
			result.Result = this.GetClanChatMessage(steamIDClanChat, message, maxMessageSize, out result.Text, out result.ChatEntryType, out result.Sender);
			return result;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000069DF File Offset: 0x00004BDF
		public bool IsClanChatAdmin(SteamID steamIDClanChat, SteamID steamIDUser)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_IsClanChatAdmin(steamIDClanChat.AsUInt64, steamIDUser.AsUInt64);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000069FA File Offset: 0x00004BFA
		public bool IsClanChatWindowOpenInSteam(SteamID steamIDClanChat)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_IsClanChatWindowOpenInSteam(steamIDClanChat.AsUInt64);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00006A0E File Offset: 0x00004C0E
		public bool OpenClanChatWindowInSteam(SteamID steamIDClanChat)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_OpenClanChatWindowInSteam(steamIDClanChat.AsUInt64);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00006A22 File Offset: 0x00004C22
		public bool CloseClanChatWindowInSteam(SteamID steamIDClanChat)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_CloseClanChatWindowInSteam(steamIDClanChat.AsUInt64);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00006A36 File Offset: 0x00004C36
		public bool SetListenForFriendsMessages(bool interceptEnabled)
		{
			base.CheckIfUsable();
			return NativeMethods.Friends_SetListenForFriendsMessages(interceptEnabled);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00006A44 File Offset: 0x00004C44
		public bool ReplyToFriendMessage(SteamID steamIDFriend, string message)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeString nativeString = new NativeString(message))
			{
				result = NativeMethods.Friends_ReplyToFriendMessage(steamIDFriend.AsUInt64, nativeString.ToNativeAsUtf8());
			}
			return result;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00006A90 File Offset: 0x00004C90
		public int GetFriendMessage(SteamID steamIDFriend, int messageID, int maxMessageSize, out string text, out ChatEntryType chatEntryType)
		{
			base.CheckIfUsable();
			int num = 0;
			int result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(maxMessageSize))
			{
				int num2 = NativeMethods.Friends_GetFriendMessage(steamIDFriend.AsUInt64, messageID, nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize, ref num);
				chatEntryType = (ChatEntryType)num;
				text = NativeHelpers.ToStringUtf8(nativeBuffer.UnmanagedMemory);
				result = num2;
			}
			return result;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00006AFC File Offset: 0x00004CFC
		public FriendsGetFriendMessageResult GetFriendMessage(SteamID steamIDFriend, int messageID, int maxMessageSize)
		{
			FriendsGetFriendMessageResult result = default(FriendsGetFriendMessageResult);
			result.Result = this.GetFriendMessage(steamIDFriend, messageID, maxMessageSize, out result.Text, out result.ChatEntryType);
			return result;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00006B30 File Offset: 0x00004D30
		public void GetFollowerCount(SteamID steamID)
		{
			base.CheckIfUsable();
			NativeMethods.Friends_GetFollowerCount(steamID.AsUInt64);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00006B44 File Offset: 0x00004D44
		public void IsFollowing(SteamID steamID)
		{
			base.CheckIfUsable();
			NativeMethods.Friends_IsFollowing(steamID.AsUInt64);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00006B58 File Offset: 0x00004D58
		public void EnumerateFollowingList(uint startIndex)
		{
			base.CheckIfUsable();
			NativeMethods.Friends_EnumerateFollowingList(startIndex);
		}

		// Token: 0x04000192 RID: 402
		private List<PersonaStateChange> personaStateChange;

		// Token: 0x04000193 RID: 403
		private List<GameOverlayActivated> gameOverlayActivated;

		// Token: 0x04000194 RID: 404
		private List<GameServerChangeRequested> gameServerChangeRequested;

		// Token: 0x04000195 RID: 405
		private List<GameLobbyJoinRequested> gameLobbyJoinRequested;

		// Token: 0x04000196 RID: 406
		private List<AvatarImageLoaded> avatarImageLoaded;

		// Token: 0x04000197 RID: 407
		private List<SteamService.Result<ClanOfficerListResponse>> clanOfficerListResponse;

		// Token: 0x04000198 RID: 408
		private List<FriendRichPresenceUpdate> friendRichPresenceUpdate;

		// Token: 0x04000199 RID: 409
		private List<GameRichPresenceJoinRequested> gameRichPresenceJoinRequested;

		// Token: 0x0400019A RID: 410
		private List<GameConnectedClanChatMsg> gameConnectedClanChatMsg;

		// Token: 0x0400019B RID: 411
		private List<GameConnectedChatJoin> gameConnectedChatJoin;

		// Token: 0x0400019C RID: 412
		private List<GameConnectedChatLeave> gameConnectedChatLeave;

		// Token: 0x0400019D RID: 413
		private List<SteamService.Result<DownloadClanActivityCountsResult>> downloadClanActivityCountsResult;

		// Token: 0x0400019E RID: 414
		private List<SteamService.Result<JoinClanChatRoomCompletionResult>> joinClanChatRoomCompletionResult;

		// Token: 0x0400019F RID: 415
		private List<GameConnectedFriendChatMsg> gameConnectedFriendChatMsg;

		// Token: 0x040001A0 RID: 416
		private List<SteamService.Result<FriendsGetFollowerCount>> friendsGetFollowerCount;

		// Token: 0x040001A1 RID: 417
		private List<SteamService.Result<FriendsIsFollowing>> friendsIsFollowing;

		// Token: 0x040001A2 RID: 418
		private List<SteamService.Result<FriendsEnumerateFollowingList>> friendsEnumerateFollowingList;
	}
}
