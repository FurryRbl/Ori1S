using System;
using UnityEngine;

// Token: 0x02000214 RID: 532
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Bleach Bypass")]
public class CC_BleachBypass : CC_Base
{
	// Token: 0x06001292 RID: 4754 RVA: 0x000548DC File Offset: 0x00052ADC
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_amount", this.amount);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000FD5 RID: 4053
	public float amount = 1f;
}
