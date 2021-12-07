using System;
using UnityEngine;

// Token: 0x020007DE RID: 2014
[CustomShaderModifier("Turbulence Distortion")]
[UberShaderCategory(UberShaderCategory.Turbulence)]
[UberShaderOrder(UberShaderOrder.TurbulenceDistortion)]
public class TurbulenceDistortionModifier : TurbulenceModifier
{
	// Token: 0x06002E2D RID: 11821 RVA: 0x000C4698 File Offset: 0x000C2898
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.LocalSettings.X *= speed;
		this.LocalSettings.Y *= strength;
	}

	// Token: 0x06002E2E RID: 11822 RVA: 0x000C46CB File Offset: 0x000C28CB
	public override void Randomize()
	{
	}

	// Token: 0x06002E2F RID: 11823 RVA: 0x000C46D0 File Offset: 0x000C28D0
	public override float GetQuadExpandSize()
	{
		return this.LocalSettings.VectorValue.y * Mathf.Max(this.ScaleVarSettings.VectorValue.x, this.ScaleVarSettings.VectorValue.y) * 0.08f;
	}

	// Token: 0x06002E30 RID: 11824 RVA: 0x000C4724 File Offset: 0x000C2924
	public override void SetProperties()
	{
		this.LocalSettings.Set("_TurbulenceLocalDistortSettings", base.AttachedToShaderBlock);
		this.TurbulenceMask.Set("_TurbulenceDistortionMaskTex", base.AttachedToShaderBlock);
		this.ScaleVarSettings.Set("_TurbulenceDistortionScaleVar", base.AttachedToShaderBlock);
		this.BiasSettings.Set("_TurbulenceDistortionBias", base.AttachedToShaderBlock);
	}

	// Token: 0x0400299E RID: 10654
	[UberShaderVectorDisplay("Local Speed", "Local Scale", "Local Offset", "")]
	public UberShaderVector LocalSettings = new UberShaderVector(1f, 1f, 0f, 0f);

	// Token: 0x0400299F RID: 10655
	[UberShaderVectorDisplay("Strength", "Uv Waviness", ShowAsVector2 = true)]
	public UberShaderVector ScaleVarSettings = new UberShaderVector(1f, 1f, 0.1f, 0.1f);

	// Token: 0x040029A0 RID: 10656
	[UberShaderVectorDisplay("Bias", "", ShowAsVector2 = true)]
	public UberShaderVector BiasSettings = new UberShaderVector(0f, 0f, 0f, 0f);

	// Token: 0x040029A1 RID: 10657
	public UberShaderTexture TurbulenceMask = new UberShaderTexture();
}
