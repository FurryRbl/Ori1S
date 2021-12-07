using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Connect
{
	// Token: 0x0200025B RID: 603
	internal class UnityAnalyticsSettings
	{
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002413 RID: 9235
		// (set) Token: 0x06002414 RID: 9236
		public static extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002415 RID: 9237
		// (set) Token: 0x06002416 RID: 9238
		public static extern bool initializeOnStartup { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002417 RID: 9239
		// (set) Token: 0x06002418 RID: 9240
		public static extern bool testMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002419 RID: 9241
		// (set) Token: 0x0600241A RID: 9242
		public static extern string testEventUrl { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x0600241B RID: 9243
		// (set) Token: 0x0600241C RID: 9244
		public static extern string testConfigUrl { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
