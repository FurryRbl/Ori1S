using System;
using UnityEngine;

// Token: 0x020007FE RID: 2046
[UberShaderCategory(UberShaderCategory.Masking)]
[CustomShaderModifier("Alpha Mask Fade")]
[UberShaderOrder(UberShaderOrder.AlphaMaskFade)]
public class AlphaMaskModifier : UberShaderModifier
{
	// Token: 0x06002F21 RID: 12065 RVA: 0x000C7968 File Offset: 0x000C5B68
	public override void SetProperties()
	{
		this.FadeMaskTexture.Set("_FadeMaskTexture", base.AttachedToShaderBlock);
		this.FadeRange.Set("_FadeRange", base.AttachedToShaderBlock);
		this.BackgroundTexture.Set("_BackgroundTexture", base.AttachedToShaderBlock);
		this.BackGroundColor.Set("_BackgroundColor", base.AttachedToShaderBlock);
		this.FadeAmount.Set("_FadeAmount", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A35 RID: 10805
	public UberShaderTexture FadeMaskTexture = new UberShaderTexture();

	// Token: 0x04002A36 RID: 10806
	public UberShaderFloat FadeRange = new UberShaderFloat();

	// Token: 0x04002A37 RID: 10807
	public UberShaderTexture BackgroundTexture = new UberShaderTexture();

	// Token: 0x04002A38 RID: 10808
	public UberShaderColor BackGroundColor = new UberShaderColor(new Color(0.5f, 0.5f, 0.5f, 0.5f));

	// Token: 0x04002A39 RID: 10809
	public UberShaderFloat FadeAmount = new UberShaderFloat();
}
