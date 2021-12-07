using System;

// Token: 0x02000787 RID: 1927
[UberShaderCategory(UberShaderCategory.Lighting)]
[UberShaderOrder(UberShaderOrder.Tint)]
[CustomShaderModifier("Tint")]
public class TintModifier : UberShaderModifier
{
	// Token: 0x06002CBF RID: 11455 RVA: 0x000BFD1E File Offset: 0x000BDF1E
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.Tint.A *= strength;
	}

	// Token: 0x06002CC0 RID: 11456 RVA: 0x000BFD33 File Offset: 0x000BDF33
	public override void SetProperties()
	{
		this.Tint.Set("_TintColor", base.AttachedToShaderBlock);
	}

	// Token: 0x04002874 RID: 10356
	public UberShaderColor Tint = new UberShaderColor();
}
