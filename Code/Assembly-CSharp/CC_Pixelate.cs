using System;
using UnityEngine;

// Token: 0x0200021F RID: 543
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Pixelate")]
public class CC_Pixelate : CC_Base
{
	// Token: 0x060012A8 RID: 4776 RVA: 0x00055250 File Offset: 0x00053450
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_scale", this.scale);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04001010 RID: 4112
	public float scale = 80f;
}
