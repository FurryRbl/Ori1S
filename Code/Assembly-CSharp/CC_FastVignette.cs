using System;
using UnityEngine;

// Token: 0x02000218 RID: 536
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Fast Vignette")]
public class CC_FastVignette : CC_Base
{
	// Token: 0x0600129A RID: 4762 RVA: 0x00054BF4 File Offset: 0x00052DF4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_sharpness", this.sharpness * 0.01f);
		base.material.SetFloat("_darkness", this.darkness * 0.02f);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000FEA RID: 4074
	public float sharpness = 10f;

	// Token: 0x04000FEB RID: 4075
	public float darkness = 30f;
}
