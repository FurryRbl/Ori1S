using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
[AddComponentMenu("Image Effects/Color Correction (Ramp)")]
[ExecuteInEditMode]
public class ColorCorrectionEffect : ImageEffectBase
{
	// Token: 0x0600000A RID: 10 RVA: 0x00002364 File Offset: 0x00000564
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000005 RID: 5
	public Texture textureRamp;
}
