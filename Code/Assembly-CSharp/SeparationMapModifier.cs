using System;

// Token: 0x02000813 RID: 2067
[UberShaderCategory(UberShaderCategory.Effects)]
[UberShaderOrder(UberShaderOrder.SeperationMap)]
[CustomShaderModifier("Separation Map Modifier")]
public class SeparationMapModifier : UberShaderModifier
{
	// Token: 0x06002F6D RID: 12141 RVA: 0x000C863E File Offset: 0x000C683E
	public override void SetProperties()
	{
		this.SepartationMapTexture.Set("_SeparationMapTex", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A6D RID: 10861
	public UberShaderTexture SepartationMapTexture = new UberShaderTexture();
}
