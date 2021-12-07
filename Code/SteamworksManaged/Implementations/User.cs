using System;
using System.Collections.Generic;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x02000137 RID: 311
	internal class User : SteamService, IUser
	{
		// Token: 0x06000A5F RID: 2655 RVA: 0x0000C658 File Offset: 0x0000A858
		public User()
		{
			this.steamServersConnected = new List<SteamServersConnected>();
			this.steamServerConnectFailure = new List<SteamServerConnectFailure>();
			this.steamServersDisconnected = new List<SteamServersDisconnected>();
			this.clientGameServerDeny = new List<ClientGameServerDeny>();
			this.ipcFailure = new List<IPCFailure>();
			this.validateAuthTicketResponse = new List<ValidateAuthTicketResponse>();
			this.microTxnAuthorizationResponse = new List<MicroTxnAuthorizationResponse>();
			this.encryptedAppTicketResponse = new List<SteamService.Result<EncryptedAppTicketResponse>>();
			this.getAuthSessionTicketResponse = new List<GetAuthSessionTicketResponse>();
			this.gameWebCallback = new List<GameWebCallback>();
			SteamService.Callbacks[CallbackID.SteamServersConnected] = delegate(IntPtr data, int dataSize)
			{
				this.steamServersConnected.Add(ManagedSteam.CallbackStructures.SteamServersConnected.Create(data, dataSize));
			};
			SteamService.Callbacks[CallbackID.SteamServerConnectFailure] = delegate(IntPtr data, int dataSize)
			{
				this.steamServerConnectFailure.Add(ManagedSteam.CallbackStructures.SteamServerConnectFailure.Create(data, dataSize));
			};
			SteamService.Callbacks[CallbackID.SteamServersDisconnected] = delegate(IntPtr data, int dataSize)
			{
				this.steamServersDisconnected.Add(ManagedSteam.CallbackStructures.SteamServersDisconnected.Create(data, dataSize));
			};
			SteamService.Callbacks[CallbackID.ClientGameServerDeny] = delegate(IntPtr data, int dataSize)
			{
				this.clientGameServerDeny.Add(ManagedSteam.CallbackStructures.ClientGameServerDeny.Create(data, dataSize));
			};
			SteamService.Callbacks[CallbackID.IPCFailure] = delegate(IntPtr data, int dataSize)
			{
				this.ipcFailure.Add(ManagedSteam.CallbackStructures.IPCFailure.Create(data, dataSize));
			};
			SteamService.Callbacks[CallbackID.ValidateAuthTicketResponse] = delegate(IntPtr data, int dataSize)
			{
				this.validateAuthTicketResponse.Add(ManagedSteam.CallbackStructures.ValidateAuthTicketResponse.Create(data, dataSize));
			};
			SteamService.Callbacks[CallbackID.MicroTxnAuthorizationResponse] = delegate(IntPtr data, int dataSize)
			{
				this.microTxnAuthorizationResponse.Add(ManagedSteam.CallbackStructures.MicroTxnAuthorizationResponse.Create(data, dataSize));
			};
			SteamService.Results[ResultID.EncryptedAppTicketResponse] = delegate(IntPtr data, int dataSize, bool flag)
			{
				this.encryptedAppTicketResponse.Add(new SteamService.Result<EncryptedAppTicketResponse>(ManagedSteam.CallbackStructures.EncryptedAppTicketResponse.Create(data, dataSize), flag));
			};
			SteamService.Callbacks[CallbackID.GetAuthSessionTicketResponse] = delegate(IntPtr data, int dataSize)
			{
				this.getAuthSessionTicketResponse.Add(ManagedSteam.CallbackStructures.GetAuthSessionTicketResponse.Create(data, dataSize));
			};
			SteamService.Callbacks[CallbackID.GameWebCallback] = delegate(IntPtr data, int dataSize)
			{
				this.gameWebCallback.Add(ManagedSteam.CallbackStructures.GameWebCallback.Create(data, dataSize));
			};
		}

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x06000A60 RID: 2656 RVA: 0x0000C828 File Offset: 0x0000AA28
		// (remove) Token: 0x06000A61 RID: 2657 RVA: 0x0000C860 File Offset: 0x0000AA60
		public event CallbackEvent<SteamServersConnected> SteamServersConnected;

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x06000A62 RID: 2658 RVA: 0x0000C898 File Offset: 0x0000AA98
		// (remove) Token: 0x06000A63 RID: 2659 RVA: 0x0000C8D0 File Offset: 0x0000AAD0
		public event CallbackEvent<SteamServerConnectFailure> SteamServerConnectFailure;

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x06000A64 RID: 2660 RVA: 0x0000C908 File Offset: 0x0000AB08
		// (remove) Token: 0x06000A65 RID: 2661 RVA: 0x0000C940 File Offset: 0x0000AB40
		public event CallbackEvent<SteamServersDisconnected> SteamServersDisconnected;

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06000A66 RID: 2662 RVA: 0x0000C978 File Offset: 0x0000AB78
		// (remove) Token: 0x06000A67 RID: 2663 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		public event CallbackEvent<ClientGameServerDeny> ClientGameServerDeny;

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x06000A68 RID: 2664 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		// (remove) Token: 0x06000A69 RID: 2665 RVA: 0x0000CA20 File Offset: 0x0000AC20
		public event CallbackEvent<IPCFailure> IPCFailure;

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x06000A6A RID: 2666 RVA: 0x0000CA58 File Offset: 0x0000AC58
		// (remove) Token: 0x06000A6B RID: 2667 RVA: 0x0000CA90 File Offset: 0x0000AC90
		public event CallbackEvent<ValidateAuthTicketResponse> ValidateAuthTicketResponse;

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x06000A6C RID: 2668 RVA: 0x0000CAC8 File Offset: 0x0000ACC8
		// (remove) Token: 0x06000A6D RID: 2669 RVA: 0x0000CB00 File Offset: 0x0000AD00
		public event CallbackEvent<MicroTxnAuthorizationResponse> MicroTxnAuthorizationResponse;

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x06000A6E RID: 2670 RVA: 0x0000CB38 File Offset: 0x0000AD38
		// (remove) Token: 0x06000A6F RID: 2671 RVA: 0x0000CB70 File Offset: 0x0000AD70
		public event ResultEvent<EncryptedAppTicketResponse> EncryptedAppTicketResponse;

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x06000A70 RID: 2672 RVA: 0x0000CBA8 File Offset: 0x0000ADA8
		// (remove) Token: 0x06000A71 RID: 2673 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		public event CallbackEvent<GetAuthSessionTicketResponse> GetAuthSessionTicketResponse;

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x06000A72 RID: 2674 RVA: 0x0000CC18 File Offset: 0x0000AE18
		// (remove) Token: 0x06000A73 RID: 2675 RVA: 0x0000CC50 File Offset: 0x0000AE50
		public event CallbackEvent<GameWebCallback> GameWebCallback;

		// Token: 0x06000A74 RID: 2676 RVA: 0x0000CC85 File Offset: 0x0000AE85
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0000CC88 File Offset: 0x0000AE88
		internal override void ReleaseManagedResources()
		{
			this.steamServersConnected = null;
			this.SteamServersConnected = null;
			this.steamServerConnectFailure = null;
			this.SteamServerConnectFailure = null;
			this.steamServersDisconnected = null;
			this.SteamServersDisconnected = null;
			this.clientGameServerDeny = null;
			this.ClientGameServerDeny = null;
			this.ipcFailure = null;
			this.IPCFailure = null;
			this.validateAuthTicketResponse = null;
			this.ValidateAuthTicketResponse = null;
			this.microTxnAuthorizationResponse = null;
			this.MicroTxnAuthorizationResponse = null;
			this.encryptedAppTicketResponse = null;
			this.EncryptedAppTicketResponse = null;
			this.getAuthSessionTicketResponse = null;
			this.GetAuthSessionTicketResponse = null;
			this.gameWebCallback = null;
			this.GameWebCallback = null;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0000CD24 File Offset: 0x0000AF24
		internal override void InvokeEvents()
		{
			SteamService.InvokeEvents<SteamServersConnected>(this.steamServersConnected, this.SteamServersConnected);
			SteamService.InvokeEvents<SteamServerConnectFailure>(this.steamServerConnectFailure, this.SteamServerConnectFailure);
			SteamService.InvokeEvents<SteamServersDisconnected>(this.steamServersDisconnected, this.SteamServersDisconnected);
			SteamService.InvokeEvents<ClientGameServerDeny>(this.clientGameServerDeny, this.ClientGameServerDeny);
			SteamService.InvokeEvents<IPCFailure>(this.ipcFailure, this.IPCFailure);
			SteamService.InvokeEvents<ValidateAuthTicketResponse>(this.validateAuthTicketResponse, this.ValidateAuthTicketResponse);
			SteamService.InvokeEvents<MicroTxnAuthorizationResponse>(this.microTxnAuthorizationResponse, this.MicroTxnAuthorizationResponse);
			SteamService.InvokeEvents<EncryptedAppTicketResponse>(this.encryptedAppTicketResponse, this.EncryptedAppTicketResponse);
			SteamService.InvokeEvents<GetAuthSessionTicketResponse>(this.getAuthSessionTicketResponse, this.GetAuthSessionTicketResponse);
			SteamService.InvokeEvents<GameWebCallback>(this.gameWebCallback, this.GameWebCallback);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0000CDDB File Offset: 0x0000AFDB
		public bool IsLoggedOn()
		{
			base.CheckIfUsable();
			return NativeMethods.User_IsLoggedOn();
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public SteamID GetSteamID()
		{
			base.CheckIfUsable();
			return new SteamID(NativeMethods.User_GetSteamID());
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0000CDFA File Offset: 0x0000AFFA
		public int InitiateGameConnection(IntPtr authBlob, int maxAuthBlob, SteamID steamIDGameServer, uint serverIP, ushort serverPort, bool secure)
		{
			base.CheckIfUsable();
			return NativeMethods.User_InitiateGameConnection(authBlob, maxAuthBlob, steamIDGameServer.AsUInt64, serverIP, serverPort, secure);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0000CE16 File Offset: 0x0000B016
		public void TerminateGameConnection(uint serverIP, ushort serverPort)
		{
			base.CheckIfUsable();
			NativeMethods.User_TerminateGameConnection(serverIP, serverPort);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0000CE25 File Offset: 0x0000B025
		public void TrackAppUsageEvent(GameID gameID, int appUsageEvent, string extraInfo = "")
		{
			base.CheckIfUsable();
			NativeMethods.User_TrackAppUsageEvent(gameID.AsUInt64, appUsageEvent, extraInfo);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0000CE3C File Offset: 0x0000B03C
		public bool GetUserDataFolder(out string text)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(256))
			{
				bool flag = NativeMethods.User_GetUserDataFolder(nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize);
				text = NativeHelpers.ToStringAnsi(nativeBuffer.UnmanagedMemory);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0000CE98 File Offset: 0x0000B098
		public UserGetUserDataFolder GetUserDataFolder()
		{
			UserGetUserDataFolder result = default(UserGetUserDataFolder);
			result.Result = this.GetUserDataFolder(out result.Path);
			return result;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0000CEC2 File Offset: 0x0000B0C2
		public void StartVoiceRecording()
		{
			base.CheckIfUsable();
			NativeMethods.User_StartVoiceRecording();
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0000CECF File Offset: 0x0000B0CF
		public void StopVoiceRecording()
		{
			base.CheckIfUsable();
			NativeMethods.User_StopVoiceRecording();
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0000CEDC File Offset: 0x0000B0DC
		public VoiceResult GetAvailableVoice(out uint compressed, out uint uncompressed, uint uncompressedVoiceDesiredSampleRate)
		{
			base.CheckIfUsable();
			compressed = 0U;
			uncompressed = 0U;
			return (VoiceResult)NativeMethods.User_GetAvailableVoice(ref compressed, ref uncompressed, uncompressedVoiceDesiredSampleRate);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		public UserGetAvailableVoiceResult GetAvailableVoice(uint uncompressedVoiceDesiredsampleRate)
		{
			UserGetAvailableVoiceResult result = default(UserGetAvailableVoiceResult);
			result.Result = this.GetAvailableVoice(out result.Compressed, out result.UnCompressed, uncompressedVoiceDesiredsampleRate);
			return result;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0000CF28 File Offset: 0x0000B128
		public VoiceResult GetVoice(bool wantCompressed, IntPtr destBuffer, uint destBufferSize, out uint bytesWritten, bool wantUncompressed, IntPtr uncompressedDestBuffer, uint uncompressedDestBufferSize, out uint uncompressedBytesWritten, uint uncompressedVoiceDesiredSampleRate)
		{
			base.CheckIfUsable();
			bytesWritten = 0U;
			uncompressedBytesWritten = 0U;
			return (VoiceResult)NativeMethods.User_GetVoice(wantCompressed, destBuffer, destBufferSize, ref bytesWritten, wantUncompressed, uncompressedDestBuffer, uncompressedDestBufferSize, ref uncompressedBytesWritten, uncompressedVoiceDesiredSampleRate);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0000CF58 File Offset: 0x0000B158
		public UserGetVoiceResult GetVoice(bool wantCompressed, IntPtr destBuffer, uint destBufferSize, bool wantUncompressed, IntPtr uncompressedDestBuffer, uint uncompressedDestBufferSize, uint uncompressedVoiceDesiredSampleRate)
		{
			UserGetVoiceResult result = default(UserGetVoiceResult);
			result.Result = this.GetVoice(wantCompressed, destBuffer, destBufferSize, out result.BytesWritten, wantUncompressed, uncompressedDestBuffer, uncompressedDestBufferSize, out result.UnCompressedBytesWritten, uncompressedVoiceDesiredSampleRate);
			return result;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0000CF94 File Offset: 0x0000B194
		public VoiceResult DecompressVoice(IntPtr compressed, uint compressedSize, IntPtr destBuffer, uint destBufferSize, out uint bytesWritten, uint desiredSampleRate)
		{
			base.CheckIfUsable();
			bytesWritten = 0U;
			return (VoiceResult)NativeMethods.User_DecompressVoice(compressed, compressedSize, destBuffer, destBufferSize, ref bytesWritten, desiredSampleRate);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
		public UserDecompressVoiceResult DecompressVoice(IntPtr compressed, uint compressedSize, IntPtr destBuffer, uint destBufferSize, uint desiredSampleRate)
		{
			UserDecompressVoiceResult result = default(UserDecompressVoiceResult);
			result.Result = this.DecompressVoice(compressed, compressedSize, destBuffer, destBufferSize, out result.BytesWritten, desiredSampleRate);
			return result;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0000CFE1 File Offset: 0x0000B1E1
		public uint GetVoiceOptimalSampleRate()
		{
			base.CheckIfUsable();
			return NativeMethods.User_GetVoiceOptimalSampleRate();
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0000CFEE File Offset: 0x0000B1EE
		public AuthTicketHandle GetAuthSessionTicket(IntPtr ticket, int maxTicket, out uint ticketLength)
		{
			base.CheckIfUsable();
			ticketLength = 0U;
			return new AuthTicketHandle(NativeMethods.User_GetAuthSessionTicket(ticket, maxTicket, ref ticketLength));
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0000D008 File Offset: 0x0000B208
		public UserGetAuthSessionTicketResult GetAuthSessionTicket(IntPtr ticket, int maxTicket)
		{
			UserGetAuthSessionTicketResult result = default(UserGetAuthSessionTicketResult);
			result.Result = this.GetAuthSessionTicket(ticket, maxTicket, out result.TicketLength);
			return result;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0000D034 File Offset: 0x0000B234
		public BeginAuthSessionResult BeginAuthSession(IntPtr authTicket, int cbAuthTicket, SteamID steamID)
		{
			base.CheckIfUsable();
			return (BeginAuthSessionResult)NativeMethods.User_BeginAuthSession(authTicket, cbAuthTicket, steamID.AsUInt64);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0000D04A File Offset: 0x0000B24A
		public void EndAuthSession(SteamID steamID)
		{
			base.CheckIfUsable();
			NativeMethods.User_EndAuthSession(steamID.AsUInt64);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0000D05E File Offset: 0x0000B25E
		public void CancelAuthTicket(uint authTicket)
		{
			base.CheckIfUsable();
			NativeMethods.User_CancelAuthTicket(authTicket);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0000D06C File Offset: 0x0000B26C
		public UserHasLicenseForAppResult UserHasLicenseForApp(SteamID steamID, AppID appID)
		{
			base.CheckIfUsable();
			return (UserHasLicenseForAppResult)NativeMethods.User_UserHasLicenseForApp(steamID.AsUInt64, appID.AsUInt32);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0000D087 File Offset: 0x0000B287
		public bool IsBehindNAT()
		{
			base.CheckIfUsable();
			return NativeMethods.User_IsBehindNAT();
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0000D094 File Offset: 0x0000B294
		public void AdvertiseGame(SteamID steamIDGameServer, uint serverIP, ushort serverPort)
		{
			base.CheckIfUsable();
			NativeMethods.User_AdvertiseGame(steamIDGameServer.AsUInt64, serverIP, serverPort);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0000D0AA File Offset: 0x0000B2AA
		public void RequestEncryptedAppTicket(IntPtr dataToInclude, int cbDataToInclude)
		{
			base.CheckIfUsable();
			NativeMethods.User_RequestEncryptedAppTicket(dataToInclude, cbDataToInclude);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0000D0B9 File Offset: 0x0000B2B9
		public bool GetEncryptedAppTicket(IntPtr ticket, int maxTicket, out uint ticketLength)
		{
			base.CheckIfUsable();
			ticketLength = 0U;
			return NativeMethods.User_GetEncryptedAppTicket(ticket, maxTicket, ref ticketLength);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		public UserGetEncryptedAppTicketResult GetEncryptedAppTicket(IntPtr ticket, int maxTicket)
		{
			UserGetEncryptedAppTicketResult result = default(UserGetEncryptedAppTicketResult);
			result.Result = this.GetEncryptedAppTicket(ticket, maxTicket, out result.TicketLength);
			return result;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0000D0F8 File Offset: 0x0000B2F8
		public int GetGameBadgeLevel(int nSeries, bool bFoil)
		{
			base.CheckIfUsable();
			return NativeMethods.User_GetGameBadgeLevel(nSeries, bFoil);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0000D107 File Offset: 0x0000B307
		public int GetPlayerSteamLevel()
		{
			base.CheckIfUsable();
			return NativeMethods.User_GetPlayerSteamLevel();
		}

		// Token: 0x0400055F RID: 1375
		private List<SteamServersConnected> steamServersConnected;

		// Token: 0x04000560 RID: 1376
		private List<SteamServerConnectFailure> steamServerConnectFailure;

		// Token: 0x04000561 RID: 1377
		private List<SteamServersDisconnected> steamServersDisconnected;

		// Token: 0x04000562 RID: 1378
		private List<ClientGameServerDeny> clientGameServerDeny;

		// Token: 0x04000563 RID: 1379
		private List<IPCFailure> ipcFailure;

		// Token: 0x04000564 RID: 1380
		private List<ValidateAuthTicketResponse> validateAuthTicketResponse;

		// Token: 0x04000565 RID: 1381
		private List<MicroTxnAuthorizationResponse> microTxnAuthorizationResponse;

		// Token: 0x04000566 RID: 1382
		private List<SteamService.Result<EncryptedAppTicketResponse>> encryptedAppTicketResponse;

		// Token: 0x04000567 RID: 1383
		private List<GetAuthSessionTicketResponse> getAuthSessionTicketResponse;

		// Token: 0x04000568 RID: 1384
		private List<GameWebCallback> gameWebCallback;
	}
}
