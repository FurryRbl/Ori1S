using System;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x020000C0 RID: 192
	public interface IUser
	{
		// Token: 0x14000066 RID: 102
		// (add) Token: 0x0600057F RID: 1407
		// (remove) Token: 0x06000580 RID: 1408
		event CallbackEvent<SteamServersConnected> SteamServersConnected;

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06000581 RID: 1409
		// (remove) Token: 0x06000582 RID: 1410
		event CallbackEvent<SteamServerConnectFailure> SteamServerConnectFailure;

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06000583 RID: 1411
		// (remove) Token: 0x06000584 RID: 1412
		event CallbackEvent<SteamServersDisconnected> SteamServersDisconnected;

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06000585 RID: 1413
		// (remove) Token: 0x06000586 RID: 1414
		event CallbackEvent<ClientGameServerDeny> ClientGameServerDeny;

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06000587 RID: 1415
		// (remove) Token: 0x06000588 RID: 1416
		event CallbackEvent<IPCFailure> IPCFailure;

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06000589 RID: 1417
		// (remove) Token: 0x0600058A RID: 1418
		event CallbackEvent<ValidateAuthTicketResponse> ValidateAuthTicketResponse;

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x0600058B RID: 1419
		// (remove) Token: 0x0600058C RID: 1420
		event CallbackEvent<MicroTxnAuthorizationResponse> MicroTxnAuthorizationResponse;

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x0600058D RID: 1421
		// (remove) Token: 0x0600058E RID: 1422
		event ResultEvent<EncryptedAppTicketResponse> EncryptedAppTicketResponse;

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x0600058F RID: 1423
		// (remove) Token: 0x06000590 RID: 1424
		event CallbackEvent<GetAuthSessionTicketResponse> GetAuthSessionTicketResponse;

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06000591 RID: 1425
		// (remove) Token: 0x06000592 RID: 1426
		event CallbackEvent<GameWebCallback> GameWebCallback;

		// Token: 0x06000593 RID: 1427
		bool IsLoggedOn();

		// Token: 0x06000594 RID: 1428
		SteamID GetSteamID();

		// Token: 0x06000595 RID: 1429
		int InitiateGameConnection(IntPtr authBlob, int maxAuthBlob, SteamID steamIDGameServer, uint serverIP, ushort serverPort, bool secure);

		// Token: 0x06000596 RID: 1430
		void TerminateGameConnection(uint serverIP, ushort serverPort);

		// Token: 0x06000597 RID: 1431
		void TrackAppUsageEvent(GameID gameID, int appUsageEvent, string extraInfo = "");

		// Token: 0x06000598 RID: 1432
		bool GetUserDataFolder(out string text);

		// Token: 0x06000599 RID: 1433
		UserGetUserDataFolder GetUserDataFolder();

		// Token: 0x0600059A RID: 1434
		void StartVoiceRecording();

		// Token: 0x0600059B RID: 1435
		void StopVoiceRecording();

		// Token: 0x0600059C RID: 1436
		VoiceResult GetAvailableVoice(out uint compressed, out uint uncompressed, uint uncompressedVoiceDesiredSampleRate);

		// Token: 0x0600059D RID: 1437
		UserGetAvailableVoiceResult GetAvailableVoice(uint uncompressedVoiceDesiredSampleRate);

		// Token: 0x0600059E RID: 1438
		VoiceResult GetVoice(bool wantCompressed, IntPtr destBuffer, uint destBufferSize, out uint bytesWritten, bool wantUncompressed, IntPtr uncompressedDestBuffer, uint uncompressedDestBufferSize, out uint uncompressedBytesWritten, uint uncompressedVoiceDesiredSampleRate);

		// Token: 0x0600059F RID: 1439
		UserGetVoiceResult GetVoice(bool wantCompressed, IntPtr destBuffer, uint destBufferSize, bool wantUncompressed, IntPtr uncompressedDestBuffer, uint uncompressedDestBufferSize, uint uncompressedVoiceDesiredSampleRate);

		// Token: 0x060005A0 RID: 1440
		VoiceResult DecompressVoice(IntPtr compressed, uint compressedSize, IntPtr destBuffer, uint destBufferSize, out uint bytesWritten, uint desiredSampleRate);

		// Token: 0x060005A1 RID: 1441
		UserDecompressVoiceResult DecompressVoice(IntPtr compressed, uint compressedSize, IntPtr destBuffer, uint destBufferSize, uint desiredSampleRate);

		// Token: 0x060005A2 RID: 1442
		uint GetVoiceOptimalSampleRate();

		// Token: 0x060005A3 RID: 1443
		AuthTicketHandle GetAuthSessionTicket(IntPtr ticket, int maxTicket, out uint ticketLength);

		// Token: 0x060005A4 RID: 1444
		UserGetAuthSessionTicketResult GetAuthSessionTicket(IntPtr ticket, int maxTicket);

		// Token: 0x060005A5 RID: 1445
		BeginAuthSessionResult BeginAuthSession(IntPtr authTicket, int cbAuthTicket, SteamID steamID);

		// Token: 0x060005A6 RID: 1446
		void EndAuthSession(SteamID steamID);

		// Token: 0x060005A7 RID: 1447
		void CancelAuthTicket(uint authTicket);

		// Token: 0x060005A8 RID: 1448
		UserHasLicenseForAppResult UserHasLicenseForApp(SteamID steamID, AppID appID);

		// Token: 0x060005A9 RID: 1449
		bool IsBehindNAT();

		// Token: 0x060005AA RID: 1450
		void AdvertiseGame(SteamID steamIDGameServer, uint serverIP, ushort serverPort);

		// Token: 0x060005AB RID: 1451
		void RequestEncryptedAppTicket(IntPtr dataToInclude, int cbDataToInclude);

		// Token: 0x060005AC RID: 1452
		bool GetEncryptedAppTicket(IntPtr ticket, int maxTicket, out uint ticketLength);

		// Token: 0x060005AD RID: 1453
		UserGetEncryptedAppTicketResult GetEncryptedAppTicket(IntPtr ticket, int maxTicket);

		// Token: 0x060005AE RID: 1454
		int GetGameBadgeLevel(int nSeries, bool bFoil);

		// Token: 0x060005AF RID: 1455
		int GetPlayerSteamLevel();
	}
}
