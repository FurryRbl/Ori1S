using System;
using UnityEngine;

// Token: 0x02000811 RID: 2065
[UberShaderOrder(UberShaderOrder.NightBerry)]
[UberShaderCategory(UberShaderCategory.Utility)]
[CustomShaderModifier("Night Berry")]
public class NightBerryModifier : UberShaderModifier
{
	// Token: 0x06002F67 RID: 12135 RVA: 0x000C85AB File Offset: 0x000C67AB
	public override void SetProperties()
	{
		this.HurtColor.Set("_HurtColor", base.AttachedToShaderBlock);
	}

	// Token: 0x04002A6A RID: 10858
	public UberShaderColor HurtColor = new UberShaderColor(new Color(0.5f, 0.25f, 0f, 0.5f));
}
