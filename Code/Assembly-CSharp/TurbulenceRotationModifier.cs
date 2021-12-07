using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007DF RID: 2015
[UberShaderCategory(UberShaderCategory.Turbulence)]
[UberShaderOrder(UberShaderOrder.TurbulenceRotation)]
[CustomShaderModifier("Turbulence Rotation")]
public class TurbulenceRotationModifier : TurbulenceModifier, IAnimationVertex
{
	// Token: 0x06002E32 RID: 11826 RVA: 0x000C4808 File Offset: 0x000C2A08
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.LocalSettings.X *= speed;
		this.LocalSettings.Y *= strength;
	}

	// Token: 0x06002E33 RID: 11827 RVA: 0x000C483C File Offset: 0x000C2A3C
	public override IEnumerable<string> GetBaseVertexTextureNames()
	{
		yield return "_TurbulenceTexture";
		yield break;
	}

	// Token: 0x06002E34 RID: 11828 RVA: 0x000C4858 File Offset: 0x000C2A58
	public override void Randomize()
	{
		Vector4 b = new Vector4(0f, 0f, base.RangeRandom(0.1f, 0.2f), 0f);
		this.LocalSettings.VectorValue += b;
	}

	// Token: 0x06002E35 RID: 11829 RVA: 0x000C48A4 File Offset: 0x000C2AA4
	public override IEnumerable<string> GetKeywordsForShader()
	{
		if (base.HasCageMesh)
		{
			yield return "_CustomMesh";
		}
		yield break;
	}

	// Token: 0x06002E36 RID: 11830 RVA: 0x000C48C7 File Offset: 0x000C2AC7
	public override bool RequiresVertexColor()
	{
		return true;
	}

	// Token: 0x06002E37 RID: 11831 RVA: 0x000C48CC File Offset: 0x000C2ACC
	public override void SetProperties()
	{
		this.LocalSettings.Set("_TurbulenceLocalRotationSettings", base.AttachedToShaderBlock);
		this.TurbulenceMask.Set("_TurbulenceRotationAnimMask", base.AttachedToShaderBlock);
		this.RotationPivot.Set("_TurbulenceRotationOffset", base.AttachedToShaderBlock);
		this.RotationPivotMask.Set("_TurbulenceRotationPivotMask", base.AttachedToShaderBlock);
		this.RotationPivot.Mode = UberShaderVector.ScalingMode.PivotOnXy;
		this.TurbulenceMask.IsVertexTexture = true;
	}

	// Token: 0x040029A2 RID: 10658
	[UberShaderVectorDisplay("Local Speed", "Local Scale", "Local Offset", "Bias")]
	public UberShaderVector LocalSettings = new UberShaderVector(1f, 1f, 0f, 0f);

	// Token: 0x040029A3 RID: 10659
	public UberShaderTexture TurbulenceMask = new UberShaderTexture();

	// Token: 0x040029A4 RID: 10660
	[UberShaderVectorDisplay("Pivot", "Uv Waviness", ShowAsVector2 = true)]
	public UberShaderVector RotationPivot = new UberShaderVector(0f, 0f, 0.1f, 0.1f);

	// Token: 0x040029A5 RID: 10661
	[UberShaderVectorDisplay("Pivot mask", "", ShowAsVector2 = true)]
	public UberShaderVector RotationPivotMask = new UberShaderVector(1f, 1f, 0f, 0f);
}
