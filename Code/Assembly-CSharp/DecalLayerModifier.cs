using System;
using UnityEngine;

// Token: 0x02000803 RID: 2051
[UberShaderOrder(UberShaderOrder.DecalLayer)]
[CustomShaderModifier("Decal Layer Modifier")]
[UberShaderCategory(UberShaderCategory.Utility)]
public class DecalLayerModifier : UberShaderModifier
{
	// Token: 0x06002F38 RID: 12088 RVA: 0x000C7DD0 File Offset: 0x000C5FD0
	public override void SetProperties()
	{
		this.DecalLayerTexture.Set("_DecalLayerTexture", base.AttachedToShaderBlock);
		this.DecalLayerColor.Set("_DecalLayerColor", base.AttachedToShaderBlock);
		this.DecalLayerMaskTexture.Set("_DecalLayerMaskTexture", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A4A RID: 10826
	public UberShaderTexture DecalLayerTexture = new UberShaderTexture();

	// Token: 0x04002A4B RID: 10827
	public UberShaderTexture DecalLayerMaskTexture = new UberShaderTexture();

	// Token: 0x04002A4C RID: 10828
	public UberShaderColor DecalLayerColor = new UberShaderColor(Color.white / 2f);
}
