using System;

// Token: 0x02000812 RID: 2066
[UberShaderCategory(UberShaderCategory.Effects)]
[UberShaderOrder(UberShaderOrder.Outline)]
[CustomShaderModifier("Outline")]
public class OutlineModifier : UberShaderModifier
{
	// Token: 0x06002F69 RID: 12137 RVA: 0x000C85E8 File Offset: 0x000C67E8
	public override void SetProperties()
	{
		this.OutlineColor.Set("_OutlineColor", base.AttachedToShaderBlock);
		this.Size.Set("_OutlineSize", base.AttachedToShaderBlock);
	}

	// Token: 0x06002F6A RID: 12138 RVA: 0x000C8621 File Offset: 0x000C6821
	public override bool NeedsMipMap()
	{
		return true;
	}

	// Token: 0x06002F6B RID: 12139 RVA: 0x000C8624 File Offset: 0x000C6824
	public override float GetQuadExpandSize()
	{
		return 0.05f;
	}

	// Token: 0x04002A6B RID: 10859
	public UberShaderColor OutlineColor = new UberShaderColor();

	// Token: 0x04002A6C RID: 10860
	public UberShaderFloat Size = new UberShaderFloat(1f);
}
