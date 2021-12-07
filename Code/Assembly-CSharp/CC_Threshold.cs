using System;
using UnityEngine;

// Token: 0x02000222 RID: 546
[AddComponentMenu("Colorful/Threshold")]
[ExecuteInEditMode]
public class CC_Threshold : CC_Base
{
	// Token: 0x060012B2 RID: 4786 RVA: 0x00055468 File Offset: 0x00053668
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_threshold", this.threshold / 255f);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400101A RID: 4122
	public float threshold = 128f;
}
