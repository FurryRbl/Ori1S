using System;
using UnityEngine;

// Token: 0x0200078B RID: 1931
[ExecuteInEditMode]
[CustomShaderBlock("Texture")]
[UberShaderOrder(UberShaderOrder.TexturedBlock)]
public class UberShaderBlockTextured : UberShaderBlock, IStrippable
{
	// Token: 0x06002CCB RID: 11467 RVA: 0x000BFF94 File Offset: 0x000BE194
	public override void SetProperties()
	{
		this.Color.Set("_Color", this);
		this.MainTexture.Set("_MainTex", this);
	}

	// Token: 0x06002CCC RID: 11468 RVA: 0x000BFFC3 File Offset: 0x000BE1C3
	public bool DoStrip()
	{
		return true;
	}

	// Token: 0x04002880 RID: 10368
	public UberShaderColor Color = new UberShaderColor(new Color(0.5f, 0.5f, 0.5f, 0.5f));

	// Token: 0x04002881 RID: 10369
	public UberShaderMainTexture MainTexture = new UberShaderMainTexture();

	// Token: 0x04002882 RID: 10370
	[HideInInspector]
	public bool EraseFromAlpha;

	// Token: 0x04002883 RID: 10371
	[HideInInspector]
	public bool DisableCustomMesh;

	// Token: 0x04002884 RID: 10372
	[HideInInspector]
	public bool CenteredQueue;
}
