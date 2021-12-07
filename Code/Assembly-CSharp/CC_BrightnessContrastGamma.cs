using System;
using UnityEngine;

// Token: 0x02000215 RID: 533
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Brightness, Contrast, Gamma")]
public class CC_BrightnessContrastGamma : CC_Base
{
	// Token: 0x06001294 RID: 4756 RVA: 0x0005494C File Offset: 0x00052B4C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_rCoeff", this.redCoeff);
		base.material.SetFloat("_gCoeff", this.greenCoeff);
		base.material.SetFloat("_bCoeff", this.blueCoeff);
		base.material.SetFloat("_brightness", (this.brightness + 100f) * 0.01f);
		base.material.SetFloat("_contrast", (this.contrast + 100f) * 0.01f);
		base.material.SetFloat("_gamma", 1f / this.gamma);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000FD6 RID: 4054
	public float redCoeff = 0.5f;

	// Token: 0x04000FD7 RID: 4055
	public float greenCoeff = 0.5f;

	// Token: 0x04000FD8 RID: 4056
	public float blueCoeff = 0.5f;

	// Token: 0x04000FD9 RID: 4057
	public float brightness;

	// Token: 0x04000FDA RID: 4058
	public float contrast;

	// Token: 0x04000FDB RID: 4059
	public float gamma = 1f;
}
