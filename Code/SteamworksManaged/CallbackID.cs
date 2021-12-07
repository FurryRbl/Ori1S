using System;

namespace ManagedSteam
{
	// Token: 0x0200001D RID: 29
	internal enum CallbackID
	{
		// Token: 0x0400007E RID: 126
		CloudPublishFileProgress,
		// Token: 0x0400007F RID: 127
		CloudPublishedFileUpdated,
		// Token: 0x04000080 RID: 128
		UserStatsReceived,
		// Token: 0x04000081 RID: 129
		UserStatsStored,
		// Token: 0x04000082 RID: 130
		UserAchievementStored,
		// Token: 0x04000083 RID: 131
		UserStatsUnloaded,
		// Token: 0x04000084 RID: 132
		UserAchievementIconFetched,
		// Token: 0x04000085 RID: 133
		PersonaStateChange,
		// Token: 0x04000086 RID: 134
		GameOverlayActivated,
		// Token: 0x04000087 RID: 135
		GameServerChangeRequested,
		// Token: 0x04000088 RID: 136
		GameLobbyJoinRequested,
		// Token: 0x04000089 RID: 137
		AvatarImageLoaded,
		// Token: 0x0400008A RID: 138
		GameRichPresenceJoinRequested,
		// Token: 0x0400008B RID: 139
		FriendRichPresenceUpdate,
		// Token: 0x0400008C RID: 140
		GameConnectedClanChatMsg,
		// Token: 0x0400008D RID: 141
		GameConnectedChatJoin,
		// Token: 0x0400008E RID: 142
		GameConnectedChatLeave,
		// Token: 0x0400008F RID: 143
		GameConnectedFriendChatMsg,
		// Token: 0x04000090 RID: 144
		FavoritesListChanged,
		// Token: 0x04000091 RID: 145
		LobbyInvite,
		// Token: 0x04000092 RID: 146
		LobbyDataUpdate,
		// Token: 0x04000093 RID: 147
		LobbyChatUpdate,
		// Token: 0x04000094 RID: 148
		LobbyChatMsg,
		// Token: 0x04000095 RID: 149
		LobbyGameCreated,
		// Token: 0x04000096 RID: 150
		LobbyKicked,
		// Token: 0x04000097 RID: 151
		SteamServersConnected,
		// Token: 0x04000098 RID: 152
		SteamServerConnectFailure,
		// Token: 0x04000099 RID: 153
		SteamServersDisconnected,
		// Token: 0x0400009A RID: 154
		ClientGameServerDeny,
		// Token: 0x0400009B RID: 155
		IPCFailure,
		// Token: 0x0400009C RID: 156
		ValidateAuthTicketResponse,
		// Token: 0x0400009D RID: 157
		MicroTxnAuthorizationResponse,
		// Token: 0x0400009E RID: 158
		GetAuthSessionTicketResponse,
		// Token: 0x0400009F RID: 159
		GameWebCallback,
		// Token: 0x040000A0 RID: 160
		GSClientApprove,
		// Token: 0x040000A1 RID: 161
		GSClientDeny,
		// Token: 0x040000A2 RID: 162
		GSClientKick,
		// Token: 0x040000A3 RID: 163
		GSClientAchievementStatus,
		// Token: 0x040000A4 RID: 164
		GSPolicyResponse,
		// Token: 0x040000A5 RID: 165
		GSGameplayStats,
		// Token: 0x040000A6 RID: 166
		GSClientGroupStatus,
		// Token: 0x040000A7 RID: 167
		GSStatsUnloaded,
		// Token: 0x040000A8 RID: 168
		P2PSessionConnectFail,
		// Token: 0x040000A9 RID: 169
		P2PSessionRequest,
		// Token: 0x040000AA RID: 170
		SocketStatusCallback,
		// Token: 0x040000AB RID: 171
		IPCountry,
		// Token: 0x040000AC RID: 172
		LowBatteryPower,
		// Token: 0x040000AD RID: 173
		SteamShutdown,
		// Token: 0x040000AE RID: 174
		CheckFileSignature,
		// Token: 0x040000AF RID: 175
		GamepadTextInputDismissed,
		// Token: 0x040000B0 RID: 176
		DlcInstalled,
		// Token: 0x040000B1 RID: 177
		RegisterActivationCodeResponse,
		// Token: 0x040000B2 RID: 178
		AppProofOfPurchaseKeyResponse,
		// Token: 0x040000B3 RID: 179
		NewLaunchQueryParameters,
		// Token: 0x040000B4 RID: 180
		HTTPRequestHeadersReceived,
		// Token: 0x040000B5 RID: 181
		HTTPRequestDataReceived,
		// Token: 0x040000B6 RID: 182
		ScreenshotReady,
		// Token: 0x040000B7 RID: 183
		ScreenshotRequested,
		// Token: 0x040000B8 RID: 184
		UGCQueryCompleted
	}
}
