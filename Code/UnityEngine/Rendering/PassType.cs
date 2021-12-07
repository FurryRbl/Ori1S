using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020002BB RID: 699
	public enum PassType
	{
		// Token: 0x04000B45 RID: 2885
		Normal,
		// Token: 0x04000B46 RID: 2886
		Vertex,
		// Token: 0x04000B47 RID: 2887
		VertexLM,
		// Token: 0x04000B48 RID: 2888
		VertexLMRGBM,
		// Token: 0x04000B49 RID: 2889
		ForwardBase,
		// Token: 0x04000B4A RID: 2890
		ForwardAdd,
		// Token: 0x04000B4B RID: 2891
		LightPrePassBase,
		// Token: 0x04000B4C RID: 2892
		LightPrePassFinal,
		// Token: 0x04000B4D RID: 2893
		ShadowCaster,
		// Token: 0x04000B4E RID: 2894
		Deferred = 10,
		// Token: 0x04000B4F RID: 2895
		Meta
	}
}
