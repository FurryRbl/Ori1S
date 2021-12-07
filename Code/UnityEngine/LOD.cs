using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200004D RID: 77
	[UsedByNativeCode]
	public struct LOD
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x00004714 File Offset: 0x00002914
		public LOD(float screenRelativeTransitionHeight, Renderer[] renderers)
		{
			this.screenRelativeTransitionHeight = screenRelativeTransitionHeight;
			this.fadeTransitionWidth = 0f;
			this.renderers = renderers;
		}

		// Token: 0x040000B4 RID: 180
		public float screenRelativeTransitionHeight;

		// Token: 0x040000B5 RID: 181
		public float fadeTransitionWidth;

		// Token: 0x040000B6 RID: 182
		public Renderer[] renderers;
	}
}
