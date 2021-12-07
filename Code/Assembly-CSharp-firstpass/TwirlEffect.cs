using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Twirl")]
public class TwirlEffect : ImageEffectBase
{
	// Token: 0x06000042 RID: 66 RVA: 0x00003978 File Offset: 0x00001B78
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		ImageEffects.RenderDistortion(base.material, source, destination, this.angle, this.center, this.radius);
	}

	// Token: 0x04000048 RID: 72
	public Vector2 radius = new Vector2(0.3f, 0.3f);

	// Token: 0x04000049 RID: 73
	public float angle = 50f;

	// Token: 0x0400004A RID: 74
	public Vector2 center = new Vector2(0.5f, 0.5f);
}
