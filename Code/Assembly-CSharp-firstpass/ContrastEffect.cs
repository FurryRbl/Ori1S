using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Contrast")]
public class ContrastEffect : ImageEffectBase
{
	// Token: 0x0600000C RID: 12 RVA: 0x0000239C File Offset: 0x0000059C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Brightness", this.Brightness);
		base.material.SetFloat("_Contrast", this.Contrast);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000006 RID: 6
	public float Brightness;

	// Token: 0x04000007 RID: 7
	public float Contrast;
}
