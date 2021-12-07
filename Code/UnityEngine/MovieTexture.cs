using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000193 RID: 403
	public sealed class MovieTexture : Texture
	{
		// Token: 0x06001913 RID: 6419 RVA: 0x00018908 File Offset: 0x00016B08
		public void Play()
		{
			MovieTexture.INTERNAL_CALL_Play(this);
		}

		// Token: 0x06001914 RID: 6420
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Play(MovieTexture self);

		// Token: 0x06001915 RID: 6421 RVA: 0x00018910 File Offset: 0x00016B10
		public void Stop()
		{
			MovieTexture.INTERNAL_CALL_Stop(this);
		}

		// Token: 0x06001916 RID: 6422
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Stop(MovieTexture self);

		// Token: 0x06001917 RID: 6423 RVA: 0x00018918 File Offset: 0x00016B18
		public void Pause()
		{
			MovieTexture.INTERNAL_CALL_Pause(this);
		}

		// Token: 0x06001918 RID: 6424
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Pause(MovieTexture self);

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001919 RID: 6425
		public extern AudioClip audioClip { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600191A RID: 6426
		// (set) Token: 0x0600191B RID: 6427
		public extern bool loop { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x0600191C RID: 6428
		public extern bool isPlaying { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x0600191D RID: 6429
		public extern bool isReadyToPlay { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600191E RID: 6430
		public extern float duration { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
