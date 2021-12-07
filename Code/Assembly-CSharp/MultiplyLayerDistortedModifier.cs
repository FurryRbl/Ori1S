using System;
using UnityEngine;

// Token: 0x02000806 RID: 2054
[UberShaderCategory(UberShaderCategory.Lighting)]
[CustomShaderModifier("Multiply Layer Distort Modifier")]
[UberShaderOrder(UberShaderOrder.MultiplyLayerDistort)]
public class MultiplyLayerDistortedModifier : UberShaderModifier
{
	// Token: 0x06002F3E RID: 12094 RVA: 0x000C7FB7 File Offset: 0x000C61B7
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.MultiplyLayerTexture.SpeedupScroll(speed);
		this.MultiplyColor.A *= strength;
	}

	// Token: 0x06002F3F RID: 12095 RVA: 0x000C7FD8 File Offset: 0x000C61D8
	public override void SetProperties()
	{
		this.MultiplyLayerTexture.Set("_MultiplyLayerDistortTexture", base.AttachedToShaderBlock);
		this.MultiplyColor.Set("_MultiplyLayerDistortColor", base.AttachedToShaderBlock);
		this.MultiplyLayerMaskTexture.Set("_MultiplyLayerDistortMaskTexture", base.AttachedToShaderBlock);
		this.DistortTexture.Set("_MultiplyLayerDistortDistortTexture", base.AttachedToShaderBlock);
		this.DistortStrength.Set("_MultiplyLayerDistortStrength", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A53 RID: 10835
	public UberShaderTexture MultiplyLayerTexture = new UberShaderTexture();

	// Token: 0x04002A54 RID: 10836
	public UberShaderTexture MultiplyLayerMaskTexture = new UberShaderTexture();

	// Token: 0x04002A55 RID: 10837
	public UberShaderMultiplyLayerColor MultiplyColor = new UberShaderMultiplyLayerColor(new Color(0.5f, 0.5f, 0.5f, 0.5f));

	// Token: 0x04002A56 RID: 10838
	public UberShaderTexture DistortTexture = new UberShaderTexture();

	// Token: 0x04002A57 RID: 10839
	[UberShaderVectorDisplay("Strength", "")]
	public UberShaderVector DistortStrength = new UberShaderVector(0.1f, 0.1f, 0f, 0f);
}
