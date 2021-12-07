using System;
using UnityEngine;

// Token: 0x0200079A RID: 1946
[UberShaderCategory(UberShaderCategory.Lighting)]
[CustomShaderModifier("Multiply Layer Modifier")]
[UberShaderOrder(UberShaderOrder.MultiplyLayer)]
public class MultiplyLayerModifier : UberShaderModifier
{
	// Token: 0x06002D34 RID: 11572 RVA: 0x000C17A9 File Offset: 0x000BF9A9
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.MultiplyLayerTexture.SpeedupScroll(speed);
		this.MultiplyColor.A *= strength;
	}

	// Token: 0x06002D35 RID: 11573 RVA: 0x000C17CC File Offset: 0x000BF9CC
	public override void SetProperties()
	{
		this.MultiplyLayerTexture.Set("_MultiplyLayerTexture", base.AttachedToShaderBlock);
		this.MultiplyColor.Set("_MultiplyLayerColor", base.AttachedToShaderBlock);
		this.MultiplyLayerMaskTexture.Set("_MultiplyLayerMaskTexture", base.AttachedToShaderBlock);
	}

	// Token: 0x040028CF RID: 10447
	public UberShaderTexture MultiplyLayerTexture = new UberShaderTexture();

	// Token: 0x040028D0 RID: 10448
	public UberShaderTexture MultiplyLayerMaskTexture = new UberShaderTexture();

	// Token: 0x040028D1 RID: 10449
	public UberShaderMultiplyLayerColor MultiplyColor = new UberShaderMultiplyLayerColor(new Color(0.5f, 0.5f, 0.5f, 0.5f));
}
