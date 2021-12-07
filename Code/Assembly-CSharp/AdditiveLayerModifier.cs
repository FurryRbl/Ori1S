using System;

// Token: 0x02000788 RID: 1928
[UberShaderCategory(UberShaderCategory.Lighting)]
[CustomShaderModifier("Additive Layer Modifier")]
[UberShaderOrder(UberShaderOrder.AdditiveLayer)]
public class AdditiveLayerModifier : UberShaderModifier
{
	// Token: 0x06002CC2 RID: 11458 RVA: 0x000BFD74 File Offset: 0x000BDF74
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.AdditiveLayerColor.A *= strength;
	}

	// Token: 0x06002CC3 RID: 11459 RVA: 0x000BFD8C File Offset: 0x000BDF8C
	public override void SetProperties()
	{
		this.AdditiveLayerTexture.Set("_AdditiveLayerTexture", base.AttachedToShaderBlock);
		this.AdditiveLayerColor.Set("_AdditiveLayerColor", base.AttachedToShaderBlock);
		this.AdditiveLayerMaskTexture.Set("_AdditiveLayerMaskTexture", base.AttachedToShaderBlock);
	}

	// Token: 0x04002875 RID: 10357
	public UberShaderTexture AdditiveLayerTexture = new UberShaderTexture();

	// Token: 0x04002876 RID: 10358
	public UberShaderTexture AdditiveLayerMaskTexture = new UberShaderTexture();

	// Token: 0x04002877 RID: 10359
	public UberShaderColor AdditiveLayerColor = new UberShaderColor();
}
