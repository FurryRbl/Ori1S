using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000648 RID: 1608
[ExecuteInEditMode]
public class SpiritLightRadialVisualAffector : MonoBehaviour
{
	// Token: 0x1700063E RID: 1598
	// (get) Token: 0x06002754 RID: 10068 RVA: 0x000AB449 File Offset: 0x000A9649
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x1700063F RID: 1599
	// (get) Token: 0x06002755 RID: 10069 RVA: 0x000AB456 File Offset: 0x000A9656
	public float LightIntensityInThisFrame
	{
		get
		{
			return this.LightIntensity + this.LightIntensityTurbulence.TurbulenceValueInThisFrame;
		}
	}

	// Token: 0x17000640 RID: 1600
	// (get) Token: 0x06002756 RID: 10070 RVA: 0x000AB46A File Offset: 0x000A966A
	public float LightRadiusInThisFrame
	{
		get
		{
			return this.Radius + this.LightRadiusTurbulence.TurbulenceValueInThisFrame;
		}
	}

	// Token: 0x06002757 RID: 10071 RVA: 0x000AB47E File Offset: 0x000A967E
	public bool IsVisibleInCamera(GameplayCamera gameplayCamera)
	{
		return gameplayCamera.Controller.InsideFrustum(new Bounds(this.Position, new Vector3(this.Radius, this.Radius, this.Radius)));
	}

	// Token: 0x06002758 RID: 10072 RVA: 0x000AB4AD File Offset: 0x000A96AD
	public void OnEnable()
	{
		SpiritLightRadialVisualAffector.All.Add(this);
	}

	// Token: 0x06002759 RID: 10073 RVA: 0x000AB4BA File Offset: 0x000A96BA
	public void OnDisable()
	{
		SpiritLightRadialVisualAffector.All.Remove(this);
	}

	// Token: 0x040021EB RID: 8683
	public float LightIntensity = 1f;

	// Token: 0x040021EC RID: 8684
	public float Radius = 10f;

	// Token: 0x040021ED RID: 8685
	public SpiritLightType LightType;

	// Token: 0x040021EE RID: 8686
	public SpiritLightPriority LightPriority = SpiritLightPriority.Medium;

	// Token: 0x040021EF RID: 8687
	public SpiritLightTurbulence LightRadiusTurbulence;

	// Token: 0x040021F0 RID: 8688
	public SpiritLightTurbulence LightIntensityTurbulence;

	// Token: 0x040021F1 RID: 8689
	public static List<SpiritLightRadialVisualAffector> All = new List<SpiritLightRadialVisualAffector>();
}
