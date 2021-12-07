using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	// Token: 0x02000160 RID: 352
	public static class SoundComposition
	{
		// Token: 0x04000B6D RID: 2925
		public static SoundCompositionManager Manager;

		// Token: 0x020006B8 RID: 1720
		public static class SoundVolumes
		{
			// Token: 0x040024E9 RID: 9449
			public static Dictionary<AudioClip, float> Volumes = new Dictionary<AudioClip, float>();
		}
	}
}
