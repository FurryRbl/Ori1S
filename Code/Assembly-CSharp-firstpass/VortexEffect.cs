using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Vortex")]
public class VortexEffect : ImageEffectBase
{
	// Token: 0x06000044 RID: 68 RVA: 0x000039E4 File Offset: 0x00001BE4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		ImageEffects.RenderDistortion(base.material, source, destination, this.angle, this.center, this.radius);
	}

	// Token: 0x0400004B RID: 75
	public Vector2 radius = new Vector2(0.4f, 0.4f);

	// Token: 0x0400004C RID: 76
	public float angle = 50f;

	// Token: 0x0400004D RID: 77
	public Vector2 center = new Vector2(0.5f, 0.5f);
}
