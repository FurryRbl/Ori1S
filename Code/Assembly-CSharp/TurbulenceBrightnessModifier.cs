using System;
using UnityEngine;

// Token: 0x020007DC RID: 2012
[CustomShaderModifier("Turbulence Brightness")]
[UberShaderCategory(UberShaderCategory.Turbulence)]
[UberShaderOrder(UberShaderOrder.TurbulenceBrightness)]
public class TurbulenceBrightnessModifier : TurbulenceModifier
{
	// Token: 0x06002E28 RID: 11816 RVA: 0x000C4558 File Offset: 0x000C2758
	public override void Randomize()
	{
		Vector4 b = new Vector4(0f, 0f, base.RangeRandom(0.1f, 0.2f), 0f);
		this.LocalSettings.VectorValue += b;
	}

	// Token: 0x06002E29 RID: 11817 RVA: 0x000C45A4 File Offset: 0x000C27A4
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.LocalSettings.X *= speed;
		this.LocalSettings.Y *= strength;
	}

	// Token: 0x06002E2A RID: 11818 RVA: 0x000C45D8 File Offset: 0x000C27D8
	public override void SetProperties()
	{
		this.LocalSettings.Set("_TurbulenceLocalBrightSettings", base.AttachedToShaderBlock);
		this.ScaleVarSettings.Set("_TurbulenceBrightScaleVar", base.AttachedToShaderBlock);
	}

	// Token: 0x0400299C RID: 10652
	[UberShaderVectorDisplay("Local Speed", "Local Scale", "Local Offset", "Range")]
	public UberShaderVector LocalSettings = new UberShaderVector(1f, 1f, 0f, 1000f);

	// Token: 0x0400299D RID: 10653
	[UberShaderVectorDisplay("Uv Waviness", "", ShowAsVector2 = true)]
	public UberShaderVector ScaleVarSettings = new UberShaderVector(1f, 1f, 0f, 0f);
}
