using System;
using UnityEngine;

// Token: 0x0200021A RID: 538
[AddComponentMenu("Colorful/Grayscale")]
[ExecuteInEditMode]
public class CC_Grayscale : CC_Base
{
	// Token: 0x0600129E RID: 4766 RVA: 0x00054D58 File Offset: 0x00052F58
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_rLum", this.redLuminance);
		base.material.SetFloat("_gLum", this.greenLuminance);
		base.material.SetFloat("_bLum", this.blueLuminance);
		base.material.SetFloat("_amount", this.amount);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000FF0 RID: 4080
	public float redLuminance = 0.3f;

	// Token: 0x04000FF1 RID: 4081
	public float greenLuminance = 0.59f;

	// Token: 0x04000FF2 RID: 4082
	public float blueLuminance = 0.11f;

	// Token: 0x04000FF3 RID: 4083
	public float amount = 1f;
}
