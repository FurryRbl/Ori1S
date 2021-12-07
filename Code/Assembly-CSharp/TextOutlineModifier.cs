using System;
using UnityEngine;

// Token: 0x0200080C RID: 2060
[UberShaderOrder(UberShaderOrder.TextOutline)]
[UberShaderCategory(UberShaderCategory.Text)]
[CustomShaderModifier("Text outline")]
public class TextOutlineModifier : UberShaderModifier
{
	// Token: 0x06002F5B RID: 12123 RVA: 0x000C83CC File Offset: 0x000C65CC
	public override void SetProperties()
	{
		this.OutlineSize.Set("_TxtOutlineSize", base.AttachedToShaderBlock);
		this.OutlineColor.Set("_TxtOutlineColor", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A61 RID: 10849
	public UberShaderFloat OutlineSize = new UberShaderFloat(0.01f);

	// Token: 0x04002A62 RID: 10850
	public UberShaderColor OutlineColor = new UberShaderColor(Color.red);
}
