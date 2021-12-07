using System;
using UnityEngine;

// Token: 0x0200021E RID: 542
[AddComponentMenu("Colorful/Photo Filter")]
[ExecuteInEditMode]
public class CC_PhotoFilter : CC_Base
{
	// Token: 0x060012A6 RID: 4774 RVA: 0x000551F4 File Offset: 0x000533F4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetColor("_rgb", this.color);
		base.material.SetFloat("_density", this.density);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400100E RID: 4110
	public Color color = new Color(1f, 0.5f, 0.2f, 1f);

	// Token: 0x0400100F RID: 4111
	public float density = 0.35f;
}
