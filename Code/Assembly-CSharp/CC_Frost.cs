using System;
using UnityEngine;

// Token: 0x02000219 RID: 537
[AddComponentMenu("Colorful/Frost")]
[ExecuteInEditMode]
public class CC_Frost : CC_Base
{
	// Token: 0x0600129C RID: 4764 RVA: 0x00054C84 File Offset: 0x00052E84
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_scale", this.scale);
		base.material.SetFloat("_enableVignette", (!this.enableVignette) ? 0f : 1f);
		base.material.SetFloat("_sharpness", this.sharpness * 0.01f);
		base.material.SetFloat("_darkness", this.darkness * 0.02f);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000FEC RID: 4076
	public float scale = 1.2f;

	// Token: 0x04000FED RID: 4077
	public float sharpness = 40f;

	// Token: 0x04000FEE RID: 4078
	public float darkness = 35f;

	// Token: 0x04000FEF RID: 4079
	public bool enableVignette = true;
}
