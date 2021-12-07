using System;
using UnityEngine;

// Token: 0x02000814 RID: 2068
[ExecuteInEditMode]
[CustomShaderModifier("UnderWaterLine")]
[UberShaderOrder(UberShaderOrder.UnderWaterLine)]
[UberShaderCategory(UberShaderCategory.Water)]
public class UnderWaterLineModifier : UberShaderModifier
{
	// Token: 0x06002F6F RID: 12143 RVA: 0x000C8674 File Offset: 0x000C6874
	public override void SetProperties()
	{
		this.WaterlineSettings.Set("_WaterlineSettingsUnder", base.AttachedToShaderBlock);
		this.WaterlineColor.Set("_WaterlineColorUnder", base.AttachedToShaderBlock);
		this.m_water = UberWaterControl.GetNearestWaterControl(base.gameObject);
	}

	// Token: 0x06002F70 RID: 12144 RVA: 0x000C86C0 File Offset: 0x000C68C0
	private void Update()
	{
		if (!Application.isPlaying)
		{
			this.SetProperties();
			if (this.m_water != null)
			{
				Vector4 vectorValue = this.WaterlineSettings.VectorValue;
				vectorValue.x = this.m_water.transform.position.y + 0.025f;
				this.WaterlineSettings.VectorValue = vectorValue;
			}
		}
	}

	// Token: 0x04002A6E RID: 10862
	public UberShaderVector WaterlineSettings = new UberShaderVector();

	// Token: 0x04002A6F RID: 10863
	public UberShaderColor WaterlineColor = new UberShaderColor();

	// Token: 0x04002A70 RID: 10864
	private UberWaterControl m_water;
}
