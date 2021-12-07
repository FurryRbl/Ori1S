using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007E5 RID: 2021
[UberShaderCategory(UberShaderCategory.Turbulence)]
[CustomShaderModifier("Turbulence Translation")]
[UberShaderOrder(UberShaderOrder.TurbulenceTranslation)]
public class TurbulenceTranslationModifier : TurbulenceModifier
{
	// Token: 0x06002E60 RID: 11872 RVA: 0x000C4E3C File Offset: 0x000C303C
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.LocalSettings.X *= speed;
		this.LocalSettings.Y *= strength;
	}

	// Token: 0x06002E61 RID: 11873 RVA: 0x000C4E6F File Offset: 0x000C306F
	private float RangeRandom(float mag)
	{
		return (UnityEngine.Random.value - 0.5f) * mag;
	}

	// Token: 0x06002E62 RID: 11874 RVA: 0x000C4E80 File Offset: 0x000C3080
	public override IEnumerable<string> GetBaseVertexTextureNames()
	{
		yield return "_TurbulenceTexture";
		yield break;
	}

	// Token: 0x06002E63 RID: 11875 RVA: 0x000C4E9C File Offset: 0x000C309C
	public override IEnumerable<string> GetKeywordsForShader()
	{
		if (base.HasCageMesh)
		{
			yield return "_CustomMesh";
		}
		yield break;
	}

	// Token: 0x06002E64 RID: 11876 RVA: 0x000C4EBF File Offset: 0x000C30BF
	public override bool RequiresVertexColor()
	{
		return true;
	}

	// Token: 0x06002E65 RID: 11877 RVA: 0x000C4EC4 File Offset: 0x000C30C4
	public override void Randomize()
	{
		Vector4 b = new Vector4(0f, 0f, this.RangeRandom(0.25f), 0f);
		this.LocalSettings.VectorValue += b;
	}

	// Token: 0x06002E66 RID: 11878 RVA: 0x000C4F0C File Offset: 0x000C310C
	public override void SetProperties()
	{
		this.LocalSettings.Set("_TurbulenceLocalTranslationSettings", base.AttachedToShaderBlock);
		this.TurbulenceMask.Set("_TurbulenceTranslationAnimMask", base.AttachedToShaderBlock);
		this.ScaleVarSettings.Set("_TurbulenceTranslationScaleVar", base.AttachedToShaderBlock);
		this.BiasSettings.Set("_TurbulenceTranslationBiasSettings", base.AttachedToShaderBlock);
		this.TurbulenceMask.IsVertexTexture = true;
	}

	// Token: 0x040029B4 RID: 10676
	[UberShaderVectorDisplay("Local Speed", "Local Scale", "Local Offset", "")]
	public UberShaderVector LocalSettings = new UberShaderVector(1f, 1f, 0f, 0.01f);

	// Token: 0x040029B5 RID: 10677
	public UberShaderTexture TurbulenceMask = new UberShaderTexture();

	// Token: 0x040029B6 RID: 10678
	[UberShaderVectorDisplay("Scale", "UV Waviness", ShowAsVector2 = true)]
	public UberShaderVector ScaleVarSettings = new UberShaderVector(1f, 1f, 0f, 0f);

	// Token: 0x040029B7 RID: 10679
	[UberShaderVectorDisplay("Bias", "", ShowAsVector2 = true)]
	public UberShaderVector BiasSettings = new UberShaderVector(0f, 0f, 0f, 0f);
}
