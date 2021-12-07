using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000CC RID: 204
	public sealed class Time
	{
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000CEB RID: 3307
		public static extern float time { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000CEC RID: 3308
		public static extern float timeSinceLevelLoad { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000CED RID: 3309
		public static extern float deltaTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000CEE RID: 3310
		public static extern float fixedTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000CEF RID: 3311
		public static extern float unscaledTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000CF0 RID: 3312
		public static extern float unscaledDeltaTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000CF1 RID: 3313
		// (set) Token: 0x06000CF2 RID: 3314
		public static extern float fixedDeltaTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000CF3 RID: 3315
		// (set) Token: 0x06000CF4 RID: 3316
		public static extern float maximumDeltaTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000CF5 RID: 3317
		public static extern float smoothDeltaTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000CF6 RID: 3318
		// (set) Token: 0x06000CF7 RID: 3319
		public static extern float timeScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000CF8 RID: 3320
		public static extern int frameCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000CF9 RID: 3321
		public static extern int renderedFrameCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000CFA RID: 3322
		public static extern float realtimeSinceStartup { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000CFB RID: 3323
		// (set) Token: 0x06000CFC RID: 3324
		public static extern int captureFramerate { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
