using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000053 RID: 83
	[UsedByNativeCode]
	public struct GradientColorKey
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x0000493C File Offset: 0x00002B3C
		public GradientColorKey(Color col, float time)
		{
			this.color = col;
			this.time = time;
		}

		// Token: 0x040000C4 RID: 196
		public Color color;

		// Token: 0x040000C5 RID: 197
		public float time;
	}
}
