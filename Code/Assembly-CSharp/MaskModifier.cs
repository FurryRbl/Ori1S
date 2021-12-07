using System;

// Token: 0x02000796 RID: 1942
[CustomShaderModifier("Mask")]
[UberShaderOrder(UberShaderOrder.Mask)]
[UberShaderCategory(UberShaderCategory.Masking)]
public class MaskModifier : UberShaderModifier
{
	// Token: 0x06002D23 RID: 11555 RVA: 0x000C1507 File Offset: 0x000BF707
	public override void Randomize()
	{
		base.RandomizeScrolling(this.MaskTexture);
	}

	// Token: 0x06002D24 RID: 11556 RVA: 0x000C1515 File Offset: 0x000BF715
	public override void ApplyMultipliers(float strength, float speed)
	{
		this.MaskTexture.SpeedupScroll(speed);
		this.MaskStrength.FloatValue *= strength;
	}

	// Token: 0x06002D25 RID: 11557 RVA: 0x000C1536 File Offset: 0x000BF736
	public override bool DoesChangeShape()
	{
		return true;
	}

	// Token: 0x06002D26 RID: 11558 RVA: 0x000C153C File Offset: 0x000BF73C
	public override void SetProperties()
	{
		this.MaskTexture.Set("_MaskTexture", base.AttachedToShaderBlock);
		this.MaskStrength.Set("_MaskStrength", base.AttachedToShaderBlock);
	}

	// Token: 0x040028C8 RID: 10440
	public UberShaderTexture MaskTexture = new UberShaderTexture();

	// Token: 0x040028C9 RID: 10441
	public UberShaderFloat MaskStrength = new UberShaderFloat(1f);
}
