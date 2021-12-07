using System;
using UnityEngine;

// Token: 0x0200021C RID: 540
[ExecuteInEditMode]
[AddComponentMenu("Colorful/LED")]
public class CC_Led : CC_Base
{
	// Token: 0x060012A2 RID: 4770 RVA: 0x00054E60 File Offset: 0x00053060
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_scale", this.scale);
		base.material.SetFloat("_brightness", this.brightness);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000FF7 RID: 4087
	public float scale = 80f;

	// Token: 0x04000FF8 RID: 4088
	public float brightness = 1f;
}
