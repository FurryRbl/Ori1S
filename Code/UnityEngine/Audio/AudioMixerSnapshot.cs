using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Audio
{
	// Token: 0x02000191 RID: 401
	public class AudioMixerSnapshot : Object
	{
		// Token: 0x0600190D RID: 6413 RVA: 0x000188F0 File Offset: 0x00016AF0
		internal AudioMixerSnapshot()
		{
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600190E RID: 6414
		public extern AudioMixer audioMixer { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600190F RID: 6415
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void TransitionTo(float timeToReach);
	}
}
