using System;

// Token: 0x02000797 RID: 1943
[UberShaderOrder(UberShaderOrder.MaskExtra)]
[CustomShaderModifier("Mask Extra")]
[UberShaderCategory(UberShaderCategory.Masking)]
public class MaskExtraModifier : UberShaderModifier
{
	// Token: 0x06002D28 RID: 11560 RVA: 0x000C1598 File Offset: 0x000BF798
	public override void Randomize()
	{
		base.RandomizeScrolling(this.MaskTexture);
	}

	// Token: 0x06002D29 RID: 11561 RVA: 0x000C15A6 File Offset: 0x000BF7A6
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.MaskTexture.SpeedupScroll(speed);
		this.MaskStrength.FloatValue *= strength;
	}

	// Token: 0x06002D2A RID: 11562 RVA: 0x000C15C7 File Offset: 0x000BF7C7
	public override bool DoesChangeShape()
	{
		return true;
	}

	// Token: 0x06002D2B RID: 11563 RVA: 0x000C15CC File Offset: 0x000BF7CC
	public override void SetProperties()
	{
		this.MaskTexture.Set("_MaskTextureExtra", base.AttachedToShaderBlock);
		this.MaskStrength.Set("_MaskStrengthExtra", base.AttachedToShaderBlock);
	}

	// Token: 0x040028CA RID: 10442
	public UberShaderTexture MaskTexture = new UberShaderTexture();

	// Token: 0x040028CB RID: 10443
	public UberShaderFloat MaskStrength = new UberShaderFloat(1f);
}
