using System;
using System.Collections.Generic;

// Token: 0x02000801 RID: 2049
[UberShaderOrder(UberShaderOrder.MaskSpiritLight)]
[UberShaderCategory(UberShaderCategory.Masking)]
[CustomShaderModifier("Spirit Light Mask")]
public class SpiritLightMaskModifier : UberShaderModifier
{
	// Token: 0x06002F32 RID: 12082 RVA: 0x000C7C10 File Offset: 0x000C5E10
	public override IEnumerable<string> GetKeywordsForShader()
	{
		List<string> list = new List<string>();
		if (this.SpiritLightMaskSettingsB.VectorValue.z < 1E-06f)
		{
			list.Add("OPTIMIZE_FOR_SPIRIT_VESSEL_LIGHT_ONLY");
		}
		return list;
	}

	// Token: 0x06002F33 RID: 12083 RVA: 0x000C7C4C File Offset: 0x000C5E4C
	public override bool DoesChangeShape()
	{
		return false;
	}

	// Token: 0x06002F34 RID: 12084 RVA: 0x000C7C50 File Offset: 0x000C5E50
	public override void SetProperties()
	{
		this.SpiritLighMaskFalloffGradient.Set("_SpiritLightFalloffGradientTexture", base.AttachedToShaderBlock);
		this.SpiritLightMaskSettings.Set("_SpiritLightMaskSettings", base.AttachedToShaderBlock);
		this.SpiritLightMaskSettingsB.Set("_SpiritLightMaskSettingsB", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A42 RID: 10818
	public UberShaderTexture SpiritLighMaskFalloffGradient = new UberShaderTexture();

	// Token: 0x04002A43 RID: 10819
	[UberShaderVectorDisplay("Hardness", "Radius Modulation", "", "Inversion")]
	public UberShaderVector SpiritLightMaskSettings = new UberShaderVector(1f, 1f, 0f, 0f);

	// Token: 0x04002A44 RID: 10820
	[UberShaderVectorDisplay("Affect Opacity", "Affect Dissolve", "Affected by Normal Lights", "Override Inversion")]
	public UberShaderVector SpiritLightMaskSettingsB = new UberShaderVector(1f, 0f, 1f, 0f);
}
