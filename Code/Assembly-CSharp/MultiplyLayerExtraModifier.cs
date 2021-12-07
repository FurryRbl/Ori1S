using System;
using UnityEngine;

// Token: 0x0200079B RID: 1947
[UberShaderCategory(UberShaderCategory.Lighting)]
[UberShaderOrder(UberShaderOrder.MultiplyLayerExtra)]
[CustomShaderModifier("Multiply Layer Extra Modifier")]
public class MultiplyLayerExtraModifier : UberShaderModifier
{
	// Token: 0x06002D37 RID: 11575 RVA: 0x000C1869 File Offset: 0x000BFA69
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.MultiplyLayerTexture.SpeedupScroll(speed);
		this.MultiplyColor.A *= strength;
	}

	// Token: 0x06002D38 RID: 11576 RVA: 0x000C188C File Offset: 0x000BFA8C
	public override void SetProperties()
	{
		this.MultiplyLayerTexture.Set("_MultiplyLayerExtraTexture", base.AttachedToShaderBlock);
		this.MultiplyColor.Set("_MultiplyLayerExtraColor", base.AttachedToShaderBlock);
		this.MultiplyLayerMaskTexture.Set("_MultiplyLayerExtraMaskTexture", base.AttachedToShaderBlock);
	}

	// Token: 0x040028D2 RID: 10450
	public UberShaderTexture MultiplyLayerTexture = new UberShaderTexture();

	// Token: 0x040028D3 RID: 10451
	public UberShaderTexture MultiplyLayerMaskTexture = new UberShaderTexture();

	// Token: 0x040028D4 RID: 10452
	public UberShaderMultiplyLayerColor MultiplyColor = new UberShaderMultiplyLayerColor(new Color(0.5f, 0.5f, 0.5f, 0.5f));
}
