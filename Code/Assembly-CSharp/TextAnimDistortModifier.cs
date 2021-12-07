using System;

// Token: 0x02000808 RID: 2056
[CustomShaderModifier("Text anim distort")]
[UberShaderOrder(UberShaderOrder.TextAnimDistort)]
[UberShaderCategory(UberShaderCategory.Text)]
public class TextAnimDistortModifier : UberShaderModifier
{
	// Token: 0x06002F50 RID: 12112 RVA: 0x000C8215 File Offset: 0x000C6415
	public override bool RequiresNormals()
	{
		return true;
	}

	// Token: 0x06002F51 RID: 12113 RVA: 0x000C8218 File Offset: 0x000C6418
	public override void SetProperties()
	{
		this.DistortTexture.Set("_TxtAnimDistort", base.AttachedToShaderBlock);
		this.Strength.Set("_TxtAnimDistStr", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A59 RID: 10841
	public UberShaderTexture DistortTexture = new UberShaderTexture();

	// Token: 0x04002A5A RID: 10842
	public UberShaderVector Strength = new UberShaderVector(0.01f, 0.01f, 0f, 0f);
}
