using System;
using System.Collections.Generic;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x0200012C RID: 300
	internal class Apps : SteamService, IApps
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x0000BF94 File Offset: 0x0000A194
		public Apps()
		{
			this.dlcInstalled = new List<DlcInstalled>();
			this.registerActivationCodeResponse = new List<RegisterActivationCodeResponse>();
			this.appProofOfPurchaseKeyResponse = new List<AppProofOfPurchaseKeyResponse>();
			this.newLaunchQueryParameters = new List<NewLaunchQueryParameters>();
			SteamService.Callbacks[CallbackID.DlcInstalled] = delegate(IntPtr data, int dataSize)
			{
				this.dlcInstalled.Add(ManagedSteam.CallbackStructures.DlcInstalled.Create(data, dataSize));
			};
			SteamService.Callbacks[CallbackID.RegisterActivationCodeResponse] = delegate(IntPtr data, int dataSize)
			{
				this.registerActivationCodeResponse.Add(ManagedSteam.CallbackStructures.RegisterActivationCodeResponse.Create(data, dataSize));
			};
			SteamService.Callbacks[CallbackID.AppProofOfPurchaseKeyResponse] = delegate(IntPtr data, int dataSize)
			{
				this.appProofOfPurchaseKeyResponse.Add(ManagedSteam.CallbackStructures.AppProofOfPurchaseKeyResponse.Create(data, dataSize));
			};
			SteamService.Callbacks[CallbackID.NewLaunchQueryParameters] = delegate(IntPtr data, int dataSize)
			{
				this.newLaunchQueryParameters.Add(ManagedSteam.CallbackStructures.NewLaunchQueryParameters.Create(data, dataSize));
			};
		}

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x06000A2E RID: 2606 RVA: 0x0000C050 File Offset: 0x0000A250
		// (remove) Token: 0x06000A2F RID: 2607 RVA: 0x0000C088 File Offset: 0x0000A288
		public event CallbackEvent<DlcInstalled> DlcInstalled;

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x06000A30 RID: 2608 RVA: 0x0000C0C0 File Offset: 0x0000A2C0
		// (remove) Token: 0x06000A31 RID: 2609 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		public event CallbackEvent<RegisterActivationCodeResponse> RegisterActivationCodeResponse;

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x06000A32 RID: 2610 RVA: 0x0000C130 File Offset: 0x0000A330
		// (remove) Token: 0x06000A33 RID: 2611 RVA: 0x0000C168 File Offset: 0x0000A368
		public event CallbackEvent<AppProofOfPurchaseKeyResponse> AppProofOfPurchaseKeyResponse;

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x06000A34 RID: 2612 RVA: 0x0000C1A0 File Offset: 0x0000A3A0
		// (remove) Token: 0x06000A35 RID: 2613 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		public event CallbackEvent<NewLaunchQueryParameters> NewLaunchQueryParameters;

		// Token: 0x06000A36 RID: 2614 RVA: 0x0000C20D File Offset: 0x0000A40D
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0000C20F File Offset: 0x0000A40F
		internal override void ReleaseManagedResources()
		{
			this.dlcInstalled = null;
			this.DlcInstalled = null;
			this.registerActivationCodeResponse = null;
			this.RegisterActivationCodeResponse = null;
			this.appProofOfPurchaseKeyResponse = null;
			this.AppProofOfPurchaseKeyResponse = null;
			this.newLaunchQueryParameters = null;
			this.NewLaunchQueryParameters = null;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0000C24C File Offset: 0x0000A44C
		internal override void InvokeEvents()
		{
			SteamService.InvokeEvents<DlcInstalled>(this.dlcInstalled, this.DlcInstalled);
			SteamService.InvokeEvents<RegisterActivationCodeResponse>(this.registerActivationCodeResponse, this.RegisterActivationCodeResponse);
			SteamService.InvokeEvents<AppProofOfPurchaseKeyResponse>(this.appProofOfPurchaseKeyResponse, this.AppProofOfPurchaseKeyResponse);
			SteamService.InvokeEvents<NewLaunchQueryParameters>(this.newLaunchQueryParameters, this.NewLaunchQueryParameters);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0000C29D File Offset: 0x0000A49D
		public bool IsSubscribed()
		{
			base.CheckIfUsable();
			return NativeMethods.Apps_IsSubscribed();
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0000C2AA File Offset: 0x0000A4AA
		public bool IsLowViolence()
		{
			base.CheckIfUsable();
			return NativeMethods.Apps_IsLowViolence();
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0000C2B7 File Offset: 0x0000A4B7
		public bool IsCybercafe()
		{
			base.CheckIfUsable();
			return NativeMethods.Apps_IsCybercafe();
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0000C2C4 File Offset: 0x0000A4C4
		public bool IsVACBanned()
		{
			base.CheckIfUsable();
			return NativeMethods.Apps_IsVACBanned();
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0000C2D1 File Offset: 0x0000A4D1
		public string GetCurrentGameLanguage()
		{
			base.CheckIfUsable();
			return NativeHelpers.ToStringAnsi(NativeMethods.Apps_GetCurrentGameLanguage());
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0000C2E3 File Offset: 0x0000A4E3
		public string GetAvailableGameLanguages()
		{
			base.CheckIfUsable();
			return NativeHelpers.ToStringAnsi(NativeMethods.Apps_GetAvailableGameLanguages());
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0000C2F5 File Offset: 0x0000A4F5
		public bool IsSubscribedApp(AppID appID)
		{
			base.CheckIfUsable();
			return NativeMethods.Apps_IsSubscribedApp(appID.AsUInt32);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0000C309 File Offset: 0x0000A509
		public bool IsDlcInstalled(AppID appID)
		{
			base.CheckIfUsable();
			return NativeMethods.Apps_IsDlcInstalled(appID.AsUInt32);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0000C31D File Offset: 0x0000A51D
		public uint GetEarliestPurchaseUnixTime(AppID appID)
		{
			base.CheckIfUsable();
			return NativeMethods.Apps_GetEarliestPurchaseUnixTime(appID.AsUInt32);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0000C331 File Offset: 0x0000A531
		public bool IsSubscribedFromFreeWeekend()
		{
			base.CheckIfUsable();
			return NativeMethods.Apps_IsSubscribedFromFreeWeekend();
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0000C33E File Offset: 0x0000A53E
		public int GetDLCCount()
		{
			base.CheckIfUsable();
			return NativeMethods.Apps_GetDLCCount();
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0000C34C File Offset: 0x0000A54C
		public bool GetDLCDataByIndex(int iDLC, out AppID pAppID, out bool pbAvailable, out string pchName)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(128))
			{
				uint value = 0U;
				pbAvailable = false;
				bool flag = NativeMethods.Apps_GetDLCDataByIndex(iDLC, ref value, ref pbAvailable, nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize);
				pAppID = new AppID(value);
				pchName = NativeHelpers.ToStringAnsi(nativeBuffer.UnmanagedMemory);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		public AppsGetDLCDataByIndexResult GetDLCDataByIndex(int iDLC)
		{
			AppsGetDLCDataByIndexResult result = default(AppsGetDLCDataByIndexResult);
			result.Result = this.GetDLCDataByIndex(iDLC, out result.AppID, out result.Available, out result.Name);
			return result;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0000C3F9 File Offset: 0x0000A5F9
		public void InstallDLC(AppID appID)
		{
			base.CheckIfUsable();
			NativeMethods.Apps_InstallDLC(appID.AsUInt32);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0000C40D File Offset: 0x0000A60D
		public void UninstallDLC(AppID appID)
		{
			base.CheckIfUsable();
			NativeMethods.Apps_UninstallDLC(appID.AsUInt32);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0000C421 File Offset: 0x0000A621
		public void RequestAppProofOfPurchaseKey(AppID appID)
		{
			base.CheckIfUsable();
			NativeMethods.Apps_RequestAppProofOfPurchaseKey(appID.AsUInt32);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0000C438 File Offset: 0x0000A638
		public bool GetCurrentBetaName(out string pchName)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(128))
			{
				bool flag = NativeMethods.Apps_GetCurrentBetaName(nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize);
				pchName = NativeHelpers.ToStringAnsi(nativeBuffer.UnmanagedMemory);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0000C494 File Offset: 0x0000A694
		public AppsGetCurrentBetaNameResult GetCurrentBetaName()
		{
			AppsGetCurrentBetaNameResult result = default(AppsGetCurrentBetaNameResult);
			result.Result = this.GetCurrentBetaName(out result.Name);
			return result;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0000C4BE File Offset: 0x0000A6BE
		public bool MarkContentCorrupt(bool bMissingFilesOnly)
		{
			base.CheckIfUsable();
			return NativeMethods.Apps_MarkContentCorrupt(bMissingFilesOnly);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0000C4CC File Offset: 0x0000A6CC
		public uint GetInstalledDepots(AppID appID, out DepotID pDepotID, uint maxDepots)
		{
			base.CheckIfUsable();
			uint value = 0U;
			uint result = NativeMethods.Apps_GetInstalledDepots(appID.AsUInt32, ref value, maxDepots);
			pDepotID = new DepotID(value);
			return result;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0000C4FE File Offset: 0x0000A6FE
		public SteamID GetAppOwner()
		{
			base.CheckIfUsable();
			return new SteamID(NativeMethods.Apps_GetAppOwner());
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0000C510 File Offset: 0x0000A710
		public string GetLaunchQueryParam(string key)
		{
			base.CheckIfUsable();
			return NativeHelpers.ToStringAnsi(NativeMethods.Apps_GetLaunchQueryParam(key));
		}

		// Token: 0x0400051F RID: 1311
		private List<DlcInstalled> dlcInstalled;

		// Token: 0x04000520 RID: 1312
		private List<RegisterActivationCodeResponse> registerActivationCodeResponse;

		// Token: 0x04000521 RID: 1313
		private List<AppProofOfPurchaseKeyResponse> appProofOfPurchaseKeyResponse;

		// Token: 0x04000522 RID: 1314
		private List<NewLaunchQueryParameters> newLaunchQueryParameters;
	}
}
