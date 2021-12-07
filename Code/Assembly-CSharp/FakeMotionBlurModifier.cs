using System;
using UnityEngine;

// Token: 0x02000810 RID: 2064
[UberShaderCategory(UberShaderCategory.Effects)]
[CustomShaderModifier("Fake Motion Blur Modifier")]
[UberShaderOrder(UberShaderOrder.FakeMotionBlur)]
public class FakeMotionBlurModifier : UberShaderModifier
{
	// Token: 0x06002F64 RID: 12132 RVA: 0x000C84EC File Offset: 0x000C66EC
	public override void SetProperties()
	{
		this.FakeRotationMotionBlurMask.Set("_FakeRotationMotionBlurMaskTex", base.AttachedToShaderBlock);
		this.FakeMotionBlurSettings.Set("_FakeMotionBlurSettings", base.AttachedToShaderBlock);
		this.FakeMotionBlurSettings2.Set("_FakeMotionBlurSettings2", base.AttachedToShaderBlock);
	}

	// Token: 0x06002F65 RID: 12133 RVA: 0x000C853C File Offset: 0x000C673C
	public override float GetQuadExpandSize()
	{
		return Mathf.Max(this.FakeMotionBlurSettings.VectorValue.w, this.FakeMotionBlurSettings.VectorValue.w);
	}

	// Token: 0x04002A67 RID: 10855
	public UberShaderTexture FakeRotationMotionBlurMask = new UberShaderTexture();

	// Token: 0x04002A68 RID: 10856
	public UberShaderVector FakeMotionBlurSettings = new UberShaderVector();

	// Token: 0x04002A69 RID: 10857
	public UberShaderVector FakeMotionBlurSettings2 = new UberShaderVector();
}
