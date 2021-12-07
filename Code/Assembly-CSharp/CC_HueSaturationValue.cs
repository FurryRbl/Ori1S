using System;
using UnityEngine;

// Token: 0x0200021B RID: 539
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Hue, Saturation, Value")]
public class CC_HueSaturationValue : CC_Base
{
	// Token: 0x060012A0 RID: 4768 RVA: 0x00054DD4 File Offset: 0x00052FD4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_hue", this.hue / 360f);
		base.material.SetFloat("_saturation", this.saturation * 0.01f);
		base.material.SetFloat("_value", this.value * 0.01f);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000FF4 RID: 4084
	public float hue;

	// Token: 0x04000FF5 RID: 4085
	public float saturation;

	// Token: 0x04000FF6 RID: 4086
	public float value;
}
