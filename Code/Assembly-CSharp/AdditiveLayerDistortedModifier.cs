using System;

// Token: 0x02000789 RID: 1929
[UberShaderCategory(UberShaderCategory.Lighting)]
[UberShaderOrder(UberShaderOrder.AdditiveLayerDistorted)]
[CustomShaderModifier("Additive Layer distorted Modifier")]
public class AdditiveLayerDistortedModifier : UberShaderModifier
{
	// Token: 0x06002CC5 RID: 11461 RVA: 0x000BFE3A File Offset: 0x000BE03A
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.AdditiveLayerColor.A *= strength;
	}

	// Token: 0x06002CC6 RID: 11462 RVA: 0x000BFE50 File Offset: 0x000BE050
	public override void SetProperties()
	{
		this.AdditiveLayerTexture.Set("_AdditiveLayerDistortTexture", base.AttachedToShaderBlock);
		this.AdditiveLayerColor.Set("_AdditiveLayerDistortColor", base.AttachedToShaderBlock);
		this.AdditiveLayerMaskTexture.Set("_AdditiveLayerDistortMaskTexture", base.AttachedToShaderBlock);
		this.DistortTexture.Set("_AdditiveLayerDistortDistortTexture", base.AttachedToShaderBlock);
		this.DistortStrength.Set("_AdditiveLayerDistortStrength", base.AttachedToShaderBlock);
	}

	// Token: 0x04002878 RID: 10360
	public UberShaderTexture AdditiveLayerTexture = new UberShaderTexture();

	// Token: 0x04002879 RID: 10361
	public UberShaderTexture AdditiveLayerMaskTexture = new UberShaderTexture();

	// Token: 0x0400287A RID: 10362
	public UberShaderTexture DistortTexture = new UberShaderTexture();

	// Token: 0x0400287B RID: 10363
	public UberShaderColor AdditiveLayerColor = new UberShaderColor();

	// Token: 0x0400287C RID: 10364
	[UberShaderVectorDisplay("Strength", "")]
	public UberShaderVector DistortStrength = new UberShaderVector(0.1f, 0.1f, 0f, 0f);
}
