using System;
using System.Collections.Generic;

// Token: 0x020007D8 RID: 2008
[CustomShaderModifier("Position animator")]
[UberShaderOrder(UberShaderOrder.PositionAnimator)]
[UberShaderCategory(UberShaderCategory.Animation)]
public class TranslateModifier : UberShaderModifier, IAnimatedGraphic
{
	// Token: 0x06002E10 RID: 11792 RVA: 0x000C4200 File Offset: 0x000C2400
	public override IEnumerable<string> GetBaseVertexTextureNames()
	{
		yield return "_TranslateXCurve";
		yield return "_TranslateYCurve";
		yield break;
	}

	// Token: 0x06002E11 RID: 11793 RVA: 0x000C421C File Offset: 0x000C241C
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.TranslateXCurve.CurveScale *= strength;
		this.TranslateYCurve.CurveScale *= strength;
		this.TranslateXCurve.TimeScale *= speed;
		this.TranslateYCurve.TimeScale *= speed;
	}

	// Token: 0x06002E12 RID: 11794 RVA: 0x000C4278 File Offset: 0x000C2478
	public override void SetProperties()
	{
		this.TranslateXCurve.Set("_TranslateXCurve", base.AttachedToShaderBlock);
		this.TranslateYCurve.Set("_TranslateYCurve", base.AttachedToShaderBlock);
		this.Offset.Mode = UberShaderVector.ScalingMode.Offset;
		this.Offset.Set("_TransOffset", base.AttachedToShaderBlock);
	}

	// Token: 0x06002E13 RID: 11795 RVA: 0x000C42D4 File Offset: 0x000C24D4
	public override IEnumerable<string> GetKeywordsForShader()
	{
		bool simpleX = this.TranslateXCurve.IsSimple;
		bool simpleY = this.TranslateYCurve.IsSimple;
		if (simpleX)
		{
			yield return "X_IS_SIMPLE";
		}
		if (simpleY)
		{
			yield return "Y_IS_SIMPLE";
		}
		yield break;
	}

	// Token: 0x04002991 RID: 10641
	public UberShaderCurve TranslateXCurve = new UberShaderCurve(0f);

	// Token: 0x04002992 RID: 10642
	public UberShaderCurve TranslateYCurve = new UberShaderCurve(0f);

	// Token: 0x04002993 RID: 10643
	[UberShaderVectorDisplay("Offset", "Unused", ShowAsVector2 = true)]
	public UberShaderVector Offset = new UberShaderVector(0f, 0f, 0f, 0f);
}
