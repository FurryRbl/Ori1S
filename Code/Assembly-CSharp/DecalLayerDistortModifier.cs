using System;
using UnityEngine;

// Token: 0x02000802 RID: 2050
[UberShaderCategory(UberShaderCategory.Utility)]
[UberShaderOrder(UberShaderOrder.DecalLayerDistort)]
[CustomShaderModifier("Decal Layer Distort Modifier")]
public class DecalLayerDistortModifier : UberShaderModifier
{
	// Token: 0x06002F36 RID: 12086 RVA: 0x000C7D10 File Offset: 0x000C5F10
	public override void SetProperties()
	{
		this.DecalLayerTexture.Set("_DecalLayerDistortTexture", base.AttachedToShaderBlock);
		this.DecalLayerColor.Set("_DecalLayerDistortColor", base.AttachedToShaderBlock);
		this.DecalLayerMaskTexture.Set("_DecalLayerDistortMaskTexture", base.AttachedToShaderBlock);
		this.DistortTexture.Set("_DecalLayerDistortDistortTexture", base.AttachedToShaderBlock);
		this.DistortStrength.Set("_DecalLayerDistortStrength", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A45 RID: 10821
	public UberShaderTexture DecalLayerTexture = new UberShaderTexture();

	// Token: 0x04002A46 RID: 10822
	public UberShaderTexture DecalLayerMaskTexture = new UberShaderTexture();

	// Token: 0x04002A47 RID: 10823
	public UberShaderColor DecalLayerColor = new UberShaderColor(Color.white / 2f);

	// Token: 0x04002A48 RID: 10824
	public UberShaderTexture DistortTexture = new UberShaderTexture();

	// Token: 0x04002A49 RID: 10825
	[UberShaderVectorDisplay("Strength", "")]
	public UberShaderVector DistortStrength = new UberShaderVector(0.1f, 0.1f, 0f, 0f);
}
