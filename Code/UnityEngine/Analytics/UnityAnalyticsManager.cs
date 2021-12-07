using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Analytics
{
	// Token: 0x02000259 RID: 601
	internal class UnityAnalyticsManager
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x0600240A RID: 9226
		public static extern string unityAdsId { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x0600240B RID: 9227
		public static extern bool unityAdsTrackingEnabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600240C RID: 9228
		public static extern string deviceUniqueIdentifier { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
