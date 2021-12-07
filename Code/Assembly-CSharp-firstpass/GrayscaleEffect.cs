using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
[AddComponentMenu("Image Effects/Grayscale")]
[ExecuteInEditMode]
public class GrayscaleEffect : ImageEffectBase
{
	// Token: 0x06000024 RID: 36 RVA: 0x00002CE8 File Offset: 0x00000EE8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		base.material.SetFloat("_RampOffset", this.rampOffset);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000020 RID: 32
	public Texture textureRamp;

	// Token: 0x04000021 RID: 33
	public float rampOffset;
}
