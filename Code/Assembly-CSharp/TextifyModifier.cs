using System;

// Token: 0x0200080E RID: 2062
[UberShaderCategory(UberShaderCategory.Text)]
[CustomShaderModifier("Textify")]
[UberShaderOrder(UberShaderOrder.Textify)]
public class TextifyModifier : UberShaderModifier, IDynamicGraphic
{
	// Token: 0x06002F5F RID: 12127 RVA: 0x000C8498 File Offset: 0x000C6698
	public override float GetQuadExpandSize()
	{
		return 100f;
	}

	// Token: 0x06002F60 RID: 12128 RVA: 0x000C849F File Offset: 0x000C669F
	public override bool DoesChangeShape()
	{
		return true;
	}

	// Token: 0x06002F61 RID: 12129 RVA: 0x000C84A2 File Offset: 0x000C66A2
	public override void SetProperties()
	{
		this.TextTexture.Set("_TxtTexture", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A65 RID: 10853
	public UberShaderTexture TextTexture = new UberShaderTexture();
}
