using System;
using UnityEngine;

// Token: 0x0200080D RID: 2061
[UberShaderCategory(UberShaderCategory.Text)]
[CustomShaderModifier("Text shadow")]
[UberShaderOrder(UberShaderOrder.TextShadow)]
public class TextShadowModifier : UberShaderModifier
{
	// Token: 0x06002F5D RID: 12125 RVA: 0x000C843C File Offset: 0x000C663C
	public override void SetProperties()
	{
		this.ShadowColor.Set("_TxtShadowColor", base.AttachedToShaderBlock);
		this.ShadowOffset.Set("_TxtShadowOffset", base.AttachedToShaderBlock);
		this.ShadowOffset.Scale = 0.001f;
	}

	// Token: 0x04002A63 RID: 10851
	public UberShaderColor ShadowColor = new UberShaderColor(Color.black);

	// Token: 0x04002A64 RID: 10852
	[UberShaderVectorDisplay("Offset", "")]
	public UberShaderVector ShadowOffset = new UberShaderVector(1f, -1f, 0f, 0f);
}
