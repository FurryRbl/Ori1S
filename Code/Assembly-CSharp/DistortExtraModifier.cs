using System;

// Token: 0x02000799 RID: 1945
[UberShaderOrder(UberShaderOrder.DistortExtra)]
[UberShaderCategory(UberShaderCategory.Distortion)]
[CustomShaderModifier("Distort extra")]
public class DistortExtraModifier : DistortModifier
{
	// Token: 0x06002D32 RID: 11570 RVA: 0x000C170C File Offset: 0x000BF90C
	public override void SetProperties()
	{
		this.DistortTexture.Set("_DistortionExtraTex", base.AttachedToShaderBlock);
		this.DistortSettings.Set("_DistortionExtraSettings", base.AttachedToShaderBlock);
		this.DistortMaskTexture.Set("_DistortionExtraMaskTex", base.AttachedToShaderBlock);
	}
}
