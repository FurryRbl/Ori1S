using System;
using UnityEngine;

// Token: 0x020007D7 RID: 2007
[UberShaderCategory(UberShaderCategory.Animation)]
[CustomShaderModifier("Scale Animator")]
[UberShaderOrder(UberShaderOrder.ScaleAnimator)]
public class ScaleAnimationModifier : UberShaderModifier, IAnimatedGraphic
{
	// Token: 0x06002E0C RID: 11788 RVA: 0x000C40FC File Offset: 0x000C22FC
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.ScaleCurve.CurveScale *= strength;
		this.ScaleCurve.TimeScale *= speed;
	}

	// Token: 0x06002E0D RID: 11789 RVA: 0x000C4130 File Offset: 0x000C2330
	public override void SetProperties()
	{
		this.ScaleCurve.Set("_ScaleCurve", base.AttachedToShaderBlock);
		this.ScalePivot.Set("_ScaleAnimPivotScale", base.AttachedToShaderBlock);
		this.Mask.Set("_ScaleAnimMask", base.AttachedToShaderBlock);
		this.Mask.IsVertexTexture = true;
		this.ScalePivot.Mode = UberShaderVector.ScalingMode.PivotOnXy;
	}

	// Token: 0x06002E0E RID: 11790 RVA: 0x000C4197 File Offset: 0x000C2397
	public override void Randomize()
	{
		this.ScaleCurve.TimeOffset = UnityEngine.Random.value;
	}

	// Token: 0x0400298E RID: 10638
	public UberShaderCurve ScaleCurve = new UberShaderCurve();

	// Token: 0x0400298F RID: 10639
	public UberShaderTexture Mask = new UberShaderTexture();

	// Token: 0x04002990 RID: 10640
	[UberShaderVectorDisplay("Pivot", "Scale")]
	public UberShaderVector ScalePivot = new UberShaderVector(0f, 0f, 1f, 1f);
}
