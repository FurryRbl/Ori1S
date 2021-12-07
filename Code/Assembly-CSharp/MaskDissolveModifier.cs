using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007FF RID: 2047
[UberShaderOrder(UberShaderOrder.MaskDissolve)]
[CustomShaderModifier("Mask Dissolve")]
[UberShaderCategory(UberShaderCategory.Masking)]
public class MaskDissolveModifier : UberShaderModifier
{
	// Token: 0x06002F23 RID: 12067 RVA: 0x000C7A3C File Offset: 0x000C5C3C
	public override IEnumerable<string> GetKeywordsForShader()
	{
		if (this.UseVertexColor)
		{
			yield return "MASK_DISSOLVE_VERTEX";
		}
		yield break;
	}

	// Token: 0x06002F24 RID: 12068 RVA: 0x000C7A5F File Offset: 0x000C5C5F
	public override bool RequiresVertexColor()
	{
		return true;
	}

	// Token: 0x06002F25 RID: 12069 RVA: 0x000C7A62 File Offset: 0x000C5C62
	public override void Randomize()
	{
		base.RandomizeScrolling(this.MaskTexture);
	}

	// Token: 0x06002F26 RID: 12070 RVA: 0x000C7A70 File Offset: 0x000C5C70
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.MaskTexture.SpeedupScroll(speed);
	}

	// Token: 0x06002F27 RID: 12071 RVA: 0x000C7A7E File Offset: 0x000C5C7E
	public override bool DoesChangeShape()
	{
		return true;
	}

	// Token: 0x06002F28 RID: 12072 RVA: 0x000C7A84 File Offset: 0x000C5C84
	public override void SetProperties()
	{
		this.MaskTexture.Set("_MaskDissolveTexture", base.AttachedToShaderBlock);
		this.DistortTexture.Set("_MaskDissolveDistort", base.AttachedToShaderBlock);
		this.DissolveColor.Set("_MaskDissolveColor", base.AttachedToShaderBlock);
		this.DistortStrength.Set("_MaskDissolveDistortStrength", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A3A RID: 10810
	public UberShaderTexture MaskTexture = new UberShaderTexture();

	// Token: 0x04002A3B RID: 10811
	public UberShaderTexture DistortTexture = new UberShaderTexture();

	// Token: 0x04002A3C RID: 10812
	[UberShaderVectorDisplay("Distort Strength X", "Distort Strength Y", "Hardness", "Color Fade")]
	public UberShaderVector DistortStrength = new UberShaderVector(1f, 1f, 1f, 1f);

	// Token: 0x04002A3D RID: 10813
	public bool UseVertexColor;

	// Token: 0x04002A3E RID: 10814
	public UberShaderColor DissolveColor = new UberShaderColor(Color.white);
}
