using System;
using UnityEngine;

// Token: 0x0200079C RID: 1948
[UberShaderCategory(UberShaderCategory.Lighting)]
[CustomShaderModifier("Multiply Layer Third Modifier")]
[UberShaderOrder(UberShaderOrder.MultiplyLayerThird)]
public class MultiplyLayerThirdModifier : UberShaderModifier
{
	// Token: 0x06002D3A RID: 11578 RVA: 0x000C1929 File Offset: 0x000BFB29
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.MultiplyLayerTexture.SpeedupScroll(speed);
		this.MultiplyColor.A *= strength;
	}

	// Token: 0x06002D3B RID: 11579 RVA: 0x000C194C File Offset: 0x000BFB4C
	public override void SetProperties()
	{
		this.MultiplyLayerTexture.Set("_MultiplyLayerThirdTexture", base.AttachedToShaderBlock);
		this.MultiplyColor.Set("_MultiplyLayerThirdColor", base.AttachedToShaderBlock);
		this.MultiplyLayerMaskTexture.Set("_MultiplyLayerThirdMaskTexture", base.AttachedToShaderBlock);
	}

	// Token: 0x040028D5 RID: 10453
	public UberShaderTexture MultiplyLayerTexture = new UberShaderTexture();

	// Token: 0x040028D6 RID: 10454
	public UberShaderTexture MultiplyLayerMaskTexture = new UberShaderTexture();

	// Token: 0x040028D7 RID: 10455
	public UberShaderMultiplyLayerColor MultiplyColor = new UberShaderMultiplyLayerColor(new Color(0.5f, 0.5f, 0.5f, 0.5f));
}
