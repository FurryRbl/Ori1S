using System;

namespace UnityEngine
{
	// Token: 0x020001A1 RID: 417
	public enum AnimationCullingType
	{
		// Token: 0x040004A4 RID: 1188
		AlwaysAnimate,
		// Token: 0x040004A5 RID: 1189
		BasedOnRenderers,
		// Token: 0x040004A6 RID: 1190
		[Obsolete("Enum member AnimatorCullingMode.BasedOnClipBounds has been deprecated. Use AnimationCullingType.AlwaysAnimate or AnimationCullingType.BasedOnRenderers instead")]
		BasedOnClipBounds,
		// Token: 0x040004A7 RID: 1191
		[Obsolete("Enum member AnimatorCullingMode.BasedOnUserBounds has been deprecated. Use AnimationCullingType.AlwaysAnimate or AnimationCullingType.BasedOnRenderers instead")]
		BasedOnUserBounds
	}
}
