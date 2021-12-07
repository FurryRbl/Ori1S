using System;
using UnityEngine;

// Token: 0x0200080A RID: 2058
[CustomShaderModifier("Text anim glow")]
[UberShaderCategory(UberShaderCategory.Text)]
[UberShaderOrder(UberShaderOrder.TextAnimGlow)]
public class TextAnimGlowModifier : UberShaderModifier
{
	// Token: 0x06002F56 RID: 12118 RVA: 0x000C82E7 File Offset: 0x000C64E7
	public override bool RequiresNormals()
	{
		return true;
	}

	// Token: 0x06002F57 RID: 12119 RVA: 0x000C82EC File Offset: 0x000C64EC
	public override void SetProperties()
	{
		this.GlowSize.Set("_TxtAnimGlowSize", base.AttachedToShaderBlock);
		this.GlowColor.Set("_TxtAnimGlowColor", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A5D RID: 10845
	public UberShaderFloat GlowSize = new UberShaderFloat(0.01f);

	// Token: 0x04002A5E RID: 10846
	public UberShaderColor GlowColor = new UberShaderColor(Color.red);
}
