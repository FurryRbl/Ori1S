using System;
using UnityEngine;

// Token: 0x02000817 RID: 2071
[ExecuteInEditMode]
[CustomShaderModifier("Water Debris")]
[UberShaderOrder(UberShaderOrder.WaterDebris)]
[UberShaderCategory(UberShaderCategory.Water)]
public class WaterDebrisModifier : UberShaderModifier
{
	// Token: 0x06002FA1 RID: 12193 RVA: 0x000C9D9F File Offset: 0x000C7F9F
	public override void SetProperties()
	{
		this.DistortStrength.Set("_DistortDebris", base.AttachedToShaderBlock);
	}

	// Token: 0x04002AC0 RID: 10944
	public UberShaderFloat DistortStrength = new UberShaderFloat(0f);

	// Token: 0x04002AC1 RID: 10945
	private UberWaterControl m_control;
}
