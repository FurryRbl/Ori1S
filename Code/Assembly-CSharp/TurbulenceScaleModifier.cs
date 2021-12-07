using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007E2 RID: 2018
[UberShaderCategory(UberShaderCategory.Turbulence)]
[CustomShaderModifier("Turbulence Scale")]
[UberShaderOrder(UberShaderOrder.TurbulenceScale)]
public class TurbulenceScaleModifier : TurbulenceModifier, IAnimationVertex
{
	// Token: 0x06002E49 RID: 11849 RVA: 0x000C4B18 File Offset: 0x000C2D18
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.LocalSettings.X *= speed;
		this.LocalSettings.Y *= strength;
	}

	// Token: 0x06002E4A RID: 11850 RVA: 0x000C4B4C File Offset: 0x000C2D4C
	public override IEnumerable<string> GetBaseVertexTextureNames()
	{
		yield return "_TurbulenceTexture";
		yield break;
	}

	// Token: 0x06002E4B RID: 11851 RVA: 0x000C4B68 File Offset: 0x000C2D68
	public override IEnumerable<string> GetKeywordsForShader()
	{
		if (base.HasCageMesh)
		{
			yield return "_CustomMesh";
		}
		yield break;
	}

	// Token: 0x06002E4C RID: 11852 RVA: 0x000C4B8C File Offset: 0x000C2D8C
	public override void Randomize()
	{
		Vector4 b = new Vector4(0f, 0f, base.RangeRandom(0.1f, 0.2f), 0f);
		this.LocalSettings.VectorValue += b;
	}

	// Token: 0x06002E4D RID: 11853 RVA: 0x000C4BD6 File Offset: 0x000C2DD6
	public override bool RequiresVertexColor()
	{
		return true;
	}

	// Token: 0x06002E4E RID: 11854 RVA: 0x000C4BDC File Offset: 0x000C2DDC
	public override void SetProperties()
	{
		this.LocalSettings.Set("_TurbulenceLocalScaleSettings", base.AttachedToShaderBlock);
		this.TurbulenceMask.Set("_TurbulenceScaleAnimMask", base.AttachedToShaderBlock);
		this.ScalePivot.Set("_TurbulenceScaleOffset", base.AttachedToShaderBlock);
		this.SquishScale.Set("_TurbulenceSquishScale", base.AttachedToShaderBlock);
		this.ScalePivot.Mode = UberShaderVector.ScalingMode.PivotOnXy;
		this.TurbulenceMask.IsVertexTexture = true;
	}

	// Token: 0x040029AB RID: 10667
	[UberShaderVectorDisplay("Local Speed", "Local Scale", "Local Offset", "Bias")]
	public UberShaderVector LocalSettings = new UberShaderVector(1f, 1f, 0f, 0.01f);

	// Token: 0x040029AC RID: 10668
	public UberShaderTexture TurbulenceMask = new UberShaderTexture();

	// Token: 0x040029AD RID: 10669
	[UberShaderVectorDisplay("Pivot", "UV Waviness", ShowAsVector2 = true)]
	public UberShaderVector ScalePivot = new UberShaderVector();

	// Token: 0x040029AE RID: 10670
	[UberShaderVectorDisplay("Scale XY", "", ShowAsVector2 = true)]
	public UberShaderVector SquishScale = new UberShaderVector(1f, 1f, 0f, 0f);
}
