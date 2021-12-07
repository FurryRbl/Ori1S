using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Advertisements
{
	// Token: 0x02000257 RID: 599
	[RequiredByNativeCode]
	internal sealed class UnityAdsInternal
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060023E5 RID: 9189 RVA: 0x0002DA4C File Offset: 0x0002BC4C
		// (remove) Token: 0x060023E6 RID: 9190 RVA: 0x0002DA64 File Offset: 0x0002BC64
		public static event UnityAdsDelegate onCampaignsAvailable;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060023E7 RID: 9191 RVA: 0x0002DA7C File Offset: 0x0002BC7C
		// (remove) Token: 0x060023E8 RID: 9192 RVA: 0x0002DA94 File Offset: 0x0002BC94
		public static event UnityAdsDelegate onCampaignsFetchFailed;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060023E9 RID: 9193 RVA: 0x0002DAAC File Offset: 0x0002BCAC
		// (remove) Token: 0x060023EA RID: 9194 RVA: 0x0002DAC4 File Offset: 0x0002BCC4
		public static event UnityAdsDelegate onShow;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060023EB RID: 9195 RVA: 0x0002DADC File Offset: 0x0002BCDC
		// (remove) Token: 0x060023EC RID: 9196 RVA: 0x0002DAF4 File Offset: 0x0002BCF4
		public static event UnityAdsDelegate onHide;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060023ED RID: 9197 RVA: 0x0002DB0C File Offset: 0x0002BD0C
		// (remove) Token: 0x060023EE RID: 9198 RVA: 0x0002DB24 File Offset: 0x0002BD24
		public static event UnityAdsDelegate<string, bool> onVideoCompleted;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060023EF RID: 9199 RVA: 0x0002DB3C File Offset: 0x0002BD3C
		// (remove) Token: 0x060023F0 RID: 9200 RVA: 0x0002DB54 File Offset: 0x0002BD54
		public static event UnityAdsDelegate onVideoStarted;

		// Token: 0x060023F1 RID: 9201 RVA: 0x0002DB6C File Offset: 0x0002BD6C
		public static void RemoveAllEventHandlers()
		{
			UnityAdsInternal.onCampaignsAvailable = null;
			UnityAdsInternal.onCampaignsFetchFailed = null;
			UnityAdsInternal.onShow = null;
			UnityAdsInternal.onHide = null;
			UnityAdsInternal.onVideoCompleted = null;
			UnityAdsInternal.onVideoStarted = null;
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x0002DBA0 File Offset: 0x0002BDA0
		public static void CallUnityAdsCampaignsAvailable()
		{
			UnityAdsDelegate unityAdsDelegate = UnityAdsInternal.onCampaignsAvailable;
			if (unityAdsDelegate != null)
			{
				unityAdsDelegate();
			}
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x0002DBC0 File Offset: 0x0002BDC0
		public static void CallUnityAdsCampaignsFetchFailed()
		{
			UnityAdsDelegate unityAdsDelegate = UnityAdsInternal.onCampaignsFetchFailed;
			if (unityAdsDelegate != null)
			{
				unityAdsDelegate();
			}
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x0002DBE0 File Offset: 0x0002BDE0
		public static void CallUnityAdsShow()
		{
			UnityAdsDelegate unityAdsDelegate = UnityAdsInternal.onShow;
			if (unityAdsDelegate != null)
			{
				unityAdsDelegate();
			}
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x0002DC00 File Offset: 0x0002BE00
		public static void CallUnityAdsHide()
		{
			UnityAdsDelegate unityAdsDelegate = UnityAdsInternal.onHide;
			if (unityAdsDelegate != null)
			{
				unityAdsDelegate();
			}
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x0002DC20 File Offset: 0x0002BE20
		public static void CallUnityAdsVideoCompleted(string rewardItemKey, bool skipped)
		{
			UnityAdsDelegate<string, bool> unityAdsDelegate = UnityAdsInternal.onVideoCompleted;
			if (unityAdsDelegate != null)
			{
				unityAdsDelegate(rewardItemKey, skipped);
			}
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x0002DC44 File Offset: 0x0002BE44
		public static void CallUnityAdsVideoStarted()
		{
			UnityAdsDelegate unityAdsDelegate = UnityAdsInternal.onVideoStarted;
			if (unityAdsDelegate != null)
			{
				unityAdsDelegate();
			}
		}

		// Token: 0x060023F8 RID: 9208
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RegisterNative();

		// Token: 0x060023F9 RID: 9209
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Init(string gameId, bool testModeEnabled, bool debugModeEnabled, string unityVersion);

		// Token: 0x060023FA RID: 9210
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Show(string zoneId, string rewardItemKey, string options);

		// Token: 0x060023FB RID: 9211
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CanShowAds(string zoneId);

		// Token: 0x060023FC RID: 9212
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLogLevel(int logLevel);

		// Token: 0x060023FD RID: 9213
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCampaignDataURL(string url);
	}
}
