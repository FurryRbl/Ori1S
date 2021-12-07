using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Advertisements
{
	// Token: 0x02000258 RID: 600
	internal class UnityAdsManager
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060023FF RID: 9215
		// (set) Token: 0x06002400 RID: 9216
		public static extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06002401 RID: 9217
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsPlatformEnabled(RuntimePlatform platform);

		// Token: 0x06002402 RID: 9218
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetPlatformEnabled(RuntimePlatform platform, bool value);

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002403 RID: 9219
		// (set) Token: 0x06002404 RID: 9220
		public static extern bool initializeOnStartup { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002405 RID: 9221
		// (set) Token: 0x06002406 RID: 9222
		public static extern bool testMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06002407 RID: 9223
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetGameId(RuntimePlatform platform);

		// Token: 0x06002408 RID: 9224
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGameId(RuntimePlatform platform, string gameId);
	}
}
