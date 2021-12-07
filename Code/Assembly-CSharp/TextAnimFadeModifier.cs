using System;

// Token: 0x02000809 RID: 2057
[CustomShaderModifier("Text anim fade")]
[UberShaderOrder(UberShaderOrder.TextAnimFade)]
[UberShaderCategory(UberShaderCategory.Text)]
public class TextAnimFadeModifier : UberShaderModifier
{
	// Token: 0x06002F53 RID: 12115 RVA: 0x000C8274 File Offset: 0x000C6474
	public override bool RequiresNormals()
	{
		return true;
	}

	// Token: 0x06002F54 RID: 12116 RVA: 0x000C8278 File Offset: 0x000C6478
	public override void SetProperties()
	{
		this.TextFadeMask.Set("_TxtAnimMask", base.AttachedToShaderBlock);
		this.Strength.Set("_TxtAnimMaskStr", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A5B RID: 10843
	public UberShaderTexture TextFadeMask = new UberShaderTexture();

	// Token: 0x04002A5C RID: 10844
	public UberShaderFloat Strength = new UberShaderFloat(1f);
}
