using System;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x020000BC RID: 188
	public interface IApps
	{
		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06000555 RID: 1365
		// (remove) Token: 0x06000556 RID: 1366
		event CallbackEvent<DlcInstalled> DlcInstalled;

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06000557 RID: 1367
		// (remove) Token: 0x06000558 RID: 1368
		event CallbackEvent<RegisterActivationCodeResponse> RegisterActivationCodeResponse;

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06000559 RID: 1369
		// (remove) Token: 0x0600055A RID: 1370
		event CallbackEvent<AppProofOfPurchaseKeyResponse> AppProofOfPurchaseKeyResponse;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x0600055B RID: 1371
		// (remove) Token: 0x0600055C RID: 1372
		event CallbackEvent<NewLaunchQueryParameters> NewLaunchQueryParameters;

		// Token: 0x0600055D RID: 1373
		bool IsSubscribed();

		// Token: 0x0600055E RID: 1374
		bool IsLowViolence();

		// Token: 0x0600055F RID: 1375
		bool IsCybercafe();

		// Token: 0x06000560 RID: 1376
		bool IsVACBanned();

		// Token: 0x06000561 RID: 1377
		string GetCurrentGameLanguage();

		// Token: 0x06000562 RID: 1378
		string GetAvailableGameLanguages();

		// Token: 0x06000563 RID: 1379
		bool IsSubscribedApp(AppID appID);

		// Token: 0x06000564 RID: 1380
		bool IsDlcInstalled(AppID appID);

		// Token: 0x06000565 RID: 1381
		uint GetEarliestPurchaseUnixTime(AppID appID);

		// Token: 0x06000566 RID: 1382
		bool IsSubscribedFromFreeWeekend();

		// Token: 0x06000567 RID: 1383
		int GetDLCCount();

		// Token: 0x06000568 RID: 1384
		bool GetDLCDataByIndex(int iDLC, out AppID pAppID, out bool pbAvailable, out string pchName);

		// Token: 0x06000569 RID: 1385
		AppsGetDLCDataByIndexResult GetDLCDataByIndex(int iDLC);

		// Token: 0x0600056A RID: 1386
		void InstallDLC(AppID appID);

		// Token: 0x0600056B RID: 1387
		void UninstallDLC(AppID appID);

		// Token: 0x0600056C RID: 1388
		void RequestAppProofOfPurchaseKey(AppID appID);

		// Token: 0x0600056D RID: 1389
		bool GetCurrentBetaName(out string pchName);

		// Token: 0x0600056E RID: 1390
		AppsGetCurrentBetaNameResult GetCurrentBetaName();

		// Token: 0x0600056F RID: 1391
		bool MarkContentCorrupt(bool bMissingFilesOnly);

		// Token: 0x06000570 RID: 1392
		uint GetInstalledDepots(AppID appID, out DepotID pDepotID, uint maxDepots);

		// Token: 0x06000571 RID: 1393
		SteamID GetAppOwner();

		// Token: 0x06000572 RID: 1394
		string GetLaunchQueryParam(string key);
	}
}
