using System;
using UnityEngine;

// Token: 0x02000217 RID: 535
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Double Vision")]
public class CC_DoubleVision : CC_Base
{
	// Token: 0x06001298 RID: 4760 RVA: 0x00054B68 File Offset: 0x00052D68
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetVector("_displace", new Vector2(this.displace.x / (float)Screen.width, this.displace.y / (float)Screen.height));
		base.material.SetFloat("_amount", this.amount);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000FE8 RID: 4072
	public Vector2 displace = new Vector2(0.7f, 0f);

	// Token: 0x04000FE9 RID: 4073
	public float amount = 1f;
}
