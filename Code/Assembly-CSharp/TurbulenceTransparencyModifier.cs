using System;
using UnityEngine;

// Token: 0x020007E8 RID: 2024
[CustomShaderModifier("Turbulence Transparency")]
[UberShaderCategory(UberShaderCategory.Turbulence)]
[UberShaderOrder(UberShaderOrder.TurbulenceTransparency)]
public class TurbulenceTransparencyModifier : TurbulenceModifier
{
	// Token: 0x06002E78 RID: 11896 RVA: 0x000C5138 File Offset: 0x000C3338
	public override void Randomize()
	{
		Vector4 b = new Vector4(0f, 0f, base.RangeRandom(0.1f, 0.2f), 0f);
		this.LocalSettings.VectorValue += b;
	}

	// Token: 0x06002E79 RID: 11897 RVA: 0x000C5184 File Offset: 0x000C3384
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.LocalSettings.X *= speed;
		this.LocalSettings.Y *= strength;
	}

	// Token: 0x06002E7A RID: 11898 RVA: 0x000C51B8 File Offset: 0x000C33B8
	public override void SetProperties()
	{
		this.LocalSettings.Set("_TurbulenceLocalTranspSettings", base.AttachedToShaderBlock);
		this.ScaleVarSettings.Set("_TurbulenceTranspScaleVar", base.AttachedToShaderBlock);
	}

	// Token: 0x040029BD RID: 10685
	[UberShaderVectorDisplay("Local Speed", "Local Scale", "Local Offset", "Range")]
	public UberShaderVector LocalSettings = new UberShaderVector(1f, 1f, 0f, 1000f);

	// Token: 0x040029BE RID: 10686
	[UberShaderVectorDisplay("Uv Waviness", "", ShowAsVector2 = true)]
	public UberShaderVector ScaleVarSettings = new UberShaderVector(1f, 1f, 0f, 0f);
}
