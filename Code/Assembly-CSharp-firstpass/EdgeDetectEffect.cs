using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
[AddComponentMenu("Image Effects/Edge Detection (Color)")]
[ExecuteInEditMode]
public class EdgeDetectEffect : ImageEffectBase
{
	// Token: 0x06000018 RID: 24 RVA: 0x00002844 File Offset: 0x00000A44
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Treshold", this.threshold * this.threshold);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000015 RID: 21
	public float threshold = 0.2f;
}
