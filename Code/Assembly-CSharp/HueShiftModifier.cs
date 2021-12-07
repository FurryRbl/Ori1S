using System;

// Token: 0x02000805 RID: 2053
[CustomShaderModifier("Hue Shift")]
[UberShaderOrder(UberShaderOrder.HueShift)]
[UberShaderCategory(UberShaderCategory.Lighting)]
public class HueShiftModifier : UberShaderModifier
{
	// Token: 0x06002F3C RID: 12092 RVA: 0x000C7F27 File Offset: 0x000C6127
	public override void SetProperties()
	{
		this.HueShift.Set("_HueShift", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A52 RID: 10834
	public UberShaderFloat HueShift = new UberShaderFloat(0f);
}
