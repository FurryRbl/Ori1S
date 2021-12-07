using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000054 RID: 84
	[UsedByNativeCode]
	public struct GradientAlphaKey
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x0000494C File Offset: 0x00002B4C
		public GradientAlphaKey(float alpha, float time)
		{
			this.alpha = alpha;
			this.time = time;
		}

		// Token: 0x040000C6 RID: 198
		public float alpha;

		// Token: 0x040000C7 RID: 199
		public float time;
	}
}
