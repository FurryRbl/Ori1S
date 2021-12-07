using System;
using UnityEngine;

// Token: 0x02000223 RID: 547
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Vibrance")]
public class CC_Vibrance : CC_Base
{
	// Token: 0x060012B4 RID: 4788 RVA: 0x000554A8 File Offset: 0x000536A8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_amount", this.amount * 0.02f);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400101B RID: 4123
	public float amount;
}
