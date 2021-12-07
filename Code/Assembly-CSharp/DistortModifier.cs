using System;
using UnityEngine;

// Token: 0x02000798 RID: 1944
[UberShaderCategory(UberShaderCategory.Distortion)]
[UberShaderOrder(UberShaderOrder.Distort)]
[CustomShaderModifier("Distort")]
public class DistortModifier : UberShaderModifier, IAnimatedGraphic
{
	// Token: 0x06002D2D RID: 11565 RVA: 0x000C1630 File Offset: 0x000BF830
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.DistortSettings.X *= strength;
		this.DistortSettings.Y *= strength;
		this.DistortTexture.SpeedupScroll(speed);
	}

	// Token: 0x06002D2E RID: 11566 RVA: 0x000C1670 File Offset: 0x000BF870
	public override void SetProperties()
	{
		this.DistortTexture.Set("_DistortionTex", base.AttachedToShaderBlock);
		this.DistortMaskTexture.Set("_DistortionMaskTex", base.AttachedToShaderBlock);
		this.DistortSettings.Set("_DistortionSettings", base.AttachedToShaderBlock);
	}

	// Token: 0x06002D2F RID: 11567 RVA: 0x000C16C0 File Offset: 0x000BF8C0
	public override float GetQuadExpandSize()
	{
		return Mathf.Max(Mathf.Abs(this.DistortSettings.VectorValue.x), Mathf.Abs(this.DistortSettings.VectorValue.y));
	}

	// Token: 0x06002D30 RID: 11568 RVA: 0x000C1702 File Offset: 0x000BF902
	public override void Randomize()
	{
	}

	// Token: 0x040028CC RID: 10444
	public UberShaderTexture DistortTexture = new UberShaderTexture();

	// Token: 0x040028CD RID: 10445
	public UberShaderTexture DistortMaskTexture = new UberShaderTexture();

	// Token: 0x040028CE RID: 10446
	[UberShaderVectorDisplay("Distort strength (uv)", "Separation offsets (uv)")]
	public UberShaderVector DistortSettings = new UberShaderVector();
}
