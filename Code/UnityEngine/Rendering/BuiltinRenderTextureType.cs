using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020002BA RID: 698
	public enum BuiltinRenderTextureType
	{
		// Token: 0x04000B37 RID: 2871
		None,
		// Token: 0x04000B38 RID: 2872
		CurrentActive,
		// Token: 0x04000B39 RID: 2873
		CameraTarget,
		// Token: 0x04000B3A RID: 2874
		Depth,
		// Token: 0x04000B3B RID: 2875
		DepthNormals,
		// Token: 0x04000B3C RID: 2876
		PrepassNormalsSpec = 7,
		// Token: 0x04000B3D RID: 2877
		PrepassLight,
		// Token: 0x04000B3E RID: 2878
		PrepassLightSpec,
		// Token: 0x04000B3F RID: 2879
		GBuffer0,
		// Token: 0x04000B40 RID: 2880
		GBuffer1,
		// Token: 0x04000B41 RID: 2881
		GBuffer2,
		// Token: 0x04000B42 RID: 2882
		GBuffer3,
		// Token: 0x04000B43 RID: 2883
		Reflections
	}
}
