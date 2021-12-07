using System;
using UnityEngine;

// Token: 0x02000220 RID: 544
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Posterize")]
public class CC_Posterize : CC_Base
{
	// Token: 0x060012AA RID: 4778 RVA: 0x00055290 File Offset: 0x00053490
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_levels", (float)this.levels);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04001011 RID: 4113
	public int levels = 4;
}
