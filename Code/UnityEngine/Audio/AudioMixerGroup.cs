using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Audio
{
	// Token: 0x02000192 RID: 402
	public class AudioMixerGroup : Object
	{
		// Token: 0x06001910 RID: 6416 RVA: 0x000188F8 File Offset: 0x00016AF8
		internal AudioMixerGroup()
		{
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001911 RID: 6417
		public extern AudioMixer audioMixer { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
