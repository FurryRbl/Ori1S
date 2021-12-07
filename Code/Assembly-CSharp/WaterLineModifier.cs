using System;
using UnityEngine;

// Token: 0x02000818 RID: 2072
[UberShaderOrder(UberShaderOrder.WaterLine)]
[ExecuteInEditMode]
[CustomShaderModifier("WaterLine")]
[UberShaderCategory(UberShaderCategory.Water)]
public class WaterLineModifier : UberShaderModifier, IDynamicGraphic
{
	// Token: 0x06002FA3 RID: 12195 RVA: 0x000C9DD5 File Offset: 0x000C7FD5
	private void OnEnable()
	{
		this.m_frame = UnityEngine.Random.Range(0, 5);
		this.m_water = UberWaterControl.GetNearestWaterControl(base.gameObject);
	}

	// Token: 0x06002FA4 RID: 12196 RVA: 0x000C9DF8 File Offset: 0x000C7FF8
	public override void SetProperties()
	{
		this.WaterlineSettings.Set("_WaterlineSettings", base.AttachedToShaderBlock);
		this.WaterlineColor.Set("_WaterlineColor", base.AttachedToShaderBlock);
	}

	// Token: 0x06002FA5 RID: 12197 RVA: 0x000C9E34 File Offset: 0x000C8034
	private void Update()
	{
		if (this.m_water != null && this.m_frame % 5 == 0)
		{
			Vector3 position = this.m_water.transform.position;
			if (position != this.m_prevPos)
			{
				this.m_prevPos = position;
				if (base.Renderer.sharedMaterial.HasProperty("_WaterlineSettings"))
				{
					if (this.m_water.enabled)
					{
						Vector4 vector = base.Renderer.sharedMaterial.GetVector("_WaterlineSettings");
						vector.x = position.y - 0.1f;
						base.Renderer.sharedMaterial.SetVector("_WaterlineSettings", vector);
					}
					else
					{
						Vector4 vector2 = base.Renderer.sharedMaterial.GetVector("_WaterlineSettings");
						vector2.x = -100000f;
						base.Renderer.sharedMaterial.SetVector("_WaterlineSettings", vector2);
					}
				}
			}
		}
		this.m_frame++;
	}

	// Token: 0x06002FA6 RID: 12198 RVA: 0x000C9F3D File Offset: 0x000C813D
	public override bool DoStrip()
	{
		return !this.DynamicWater;
	}

	// Token: 0x04002AC2 RID: 10946
	public UberShaderVector WaterlineSettings = new UberShaderVector();

	// Token: 0x04002AC3 RID: 10947
	public UberShaderColor WaterlineColor = new UberShaderColor();

	// Token: 0x04002AC4 RID: 10948
	public bool DynamicWater;

	// Token: 0x04002AC5 RID: 10949
	private UberWaterControl m_water;

	// Token: 0x04002AC6 RID: 10950
	private int m_frame;

	// Token: 0x04002AC7 RID: 10951
	private Vector3 m_prevPos;
}
