using System;
using UnityEngine;

// Token: 0x02000216 RID: 534
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Channel Mixer")]
public class CC_ChannelMixer : CC_Base
{
	// Token: 0x06001296 RID: 4758 RVA: 0x00054A34 File Offset: 0x00052C34
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetVector("_red", new Vector4(this.redR * 0.01f, this.greenR * 0.01f, this.blueR * 0.01f));
		base.material.SetVector("_green", new Vector4(this.redG * 0.01f, this.greenG * 0.01f, this.blueG * 0.01f));
		base.material.SetVector("_blue", new Vector4(this.redB * 0.01f, this.greenB * 0.01f, this.blueB * 0.01f));
		base.material.SetVector("_constant", new Vector4(this.constantR * 0.01f, this.constantG * 0.01f, this.constantB * 0.01f));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000FDC RID: 4060
	public float redR = 100f;

	// Token: 0x04000FDD RID: 4061
	public float redG;

	// Token: 0x04000FDE RID: 4062
	public float redB;

	// Token: 0x04000FDF RID: 4063
	public float greenR;

	// Token: 0x04000FE0 RID: 4064
	public float greenG = 100f;

	// Token: 0x04000FE1 RID: 4065
	public float greenB;

	// Token: 0x04000FE2 RID: 4066
	public float blueR;

	// Token: 0x04000FE3 RID: 4067
	public float blueG;

	// Token: 0x04000FE4 RID: 4068
	public float blueB = 100f;

	// Token: 0x04000FE5 RID: 4069
	public float constantR;

	// Token: 0x04000FE6 RID: 4070
	public float constantG;

	// Token: 0x04000FE7 RID: 4071
	public float constantB;
}
