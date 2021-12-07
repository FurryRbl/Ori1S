using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Experimental.Director
{
	// Token: 0x020000DA RID: 218
	public class DirectorPlayer : Behaviour
	{
		// Token: 0x06000E15 RID: 3605 RVA: 0x00011E44 File Offset: 0x00010044
		public void Play(Playable playable, object customData)
		{
			this.PlayInternal(playable, customData);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00011E50 File Offset: 0x00010050
		public void Play(Playable playable)
		{
			this.PlayInternal(playable, null);
		}

		// Token: 0x06000E17 RID: 3607
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void PlayInternal(Playable playable, object customData);

		// Token: 0x06000E18 RID: 3608
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		// Token: 0x06000E19 RID: 3609
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTime(double time);

		// Token: 0x06000E1A RID: 3610
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern double GetTime();

		// Token: 0x06000E1B RID: 3611
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTimeUpdateMode(DirectorUpdateMode mode);

		// Token: 0x06000E1C RID: 3612
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern DirectorUpdateMode GetTimeUpdateMode();
	}
}
