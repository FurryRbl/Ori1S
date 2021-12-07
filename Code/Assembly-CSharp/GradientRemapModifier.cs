using System;
using UnityEngine;

// Token: 0x02000804 RID: 2052
[UberShaderCategory(UberShaderCategory.Lighting)]
[CustomShaderModifier("Gradient Remap Modifier")]
[UberShaderOrder(UberShaderOrder.GradientRemap)]
public class GradientRemapModifier : UberShaderModifier
{
	// Token: 0x06002F3A RID: 12090 RVA: 0x000C7E94 File Offset: 0x000C6094
	public override void SetProperties()
	{
		this.GradientSettings.Set("_GradientSettings", base.AttachedToShaderBlock);
		this.BaseColorA.Set("_BaseColorA", base.AttachedToShaderBlock);
		this.RemapColorA.Set("_RemapColorA", base.AttachedToShaderBlock);
		this.BaseColorB.Set("_BaseColorB", base.AttachedToShaderBlock);
		this.RemapColorB.Set("_RemapColorB", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A4D RID: 10829
	public UberShaderVector GradientSettings = new UberShaderVector(1f, 0f, 1f, 0f);

	// Token: 0x04002A4E RID: 10830
	public UberShaderColor BaseColorA = new UberShaderColor(Color.clear);

	// Token: 0x04002A4F RID: 10831
	public UberShaderColor RemapColorA = new UberShaderColor(Color.clear);

	// Token: 0x04002A50 RID: 10832
	public UberShaderColor BaseColorB = new UberShaderColor(Color.clear);

	// Token: 0x04002A51 RID: 10833
	public UberShaderColor RemapColorB = new UberShaderColor(Color.clear);
}
