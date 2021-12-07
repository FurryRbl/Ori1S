using System;

// Token: 0x0200078A RID: 1930
[UberShaderOrder(UberShaderOrder.AdditiveLayerExtra)]
[UberShaderCategory(UberShaderCategory.Lighting)]
[CustomShaderModifier("Additive Layer Extra Modifier")]
public class AdditiveLayerExtraModifier : UberShaderModifier
{
	// Token: 0x06002CC8 RID: 11464 RVA: 0x000BFEF4 File Offset: 0x000BE0F4
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.AdditiveLayerColor.A *= strength;
	}

	// Token: 0x06002CC9 RID: 11465 RVA: 0x000BFF0C File Offset: 0x000BE10C
	public override void SetProperties()
	{
		this.AdditiveLayerTexture.Set("_AdditiveLayerExtraTexture", base.AttachedToShaderBlock);
		this.AdditiveLayerColor.Set("_AdditiveLayerExtraColor", base.AttachedToShaderBlock);
		this.AdditiveLayerMaskTexture.Set("_AdditiveLayerExtraMaskTexture", base.AttachedToShaderBlock);
	}

	// Token: 0x0400287D RID: 10365
	public UberShaderTexture AdditiveLayerTexture = new UberShaderTexture();

	// Token: 0x0400287E RID: 10366
	public UberShaderTexture AdditiveLayerMaskTexture = new UberShaderTexture();

	// Token: 0x0400287F RID: 10367
	public UberShaderColor AdditiveLayerColor = new UberShaderColor();
}
