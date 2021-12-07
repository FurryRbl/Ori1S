using System;
using UnityEngine;

// Token: 0x0200080B RID: 2059
[UberShaderCategory(UberShaderCategory.Text)]
[UberShaderOrder(UberShaderOrder.TextGlow)]
[CustomShaderModifier("Text glow")]
public class TextGlowModifier : UberShaderModifier
{
	// Token: 0x06002F59 RID: 12121 RVA: 0x000C835C File Offset: 0x000C655C
	public override void SetProperties()
	{
		this.GlowSize.Set("_TxtGlowSize", base.AttachedToShaderBlock);
		this.GlowColor.Set("_TxtGlowColor", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A5F RID: 10847
	public UberShaderFloat GlowSize = new UberShaderFloat(0.01f);

	// Token: 0x04002A60 RID: 10848
	public UberShaderColor GlowColor = new UberShaderColor(Color.red);
}
