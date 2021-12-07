using System;
using Sein.World;
using UnityEngine;

// Token: 0x02000899 RID: 2201
public class WaterAreaMapCanvasOverlay : AreaMapCanvasOverlay
{
	// Token: 0x0600315C RID: 12636 RVA: 0x000D1F88 File Offset: 0x000D0188
	public void OnEnable()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (component)
		{
			component.material.SetColor(ShaderProperties.Color, (!Events.WaterPurified) ? this.DirtyWater : this.CleanWater);
		}
	}

	// Token: 0x04002CA3 RID: 11427
	public Color CleanWater;

	// Token: 0x04002CA4 RID: 11428
	public Color DirtyWater;
}
