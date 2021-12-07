using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000657 RID: 1623
[ExecuteInEditMode]
public class SpiritLightCapsuleVisualAffector : MonoBehaviour
{
	// Token: 0x1700064C RID: 1612
	// (get) Token: 0x060027A5 RID: 10149 RVA: 0x000AC766 File Offset: 0x000AA966
	public Vector3 StartPointPosition
	{
		get
		{
			return this.StartPoint.position;
		}
	}

	// Token: 0x1700064D RID: 1613
	// (get) Token: 0x060027A6 RID: 10150 RVA: 0x000AC773 File Offset: 0x000AA973
	public Vector3 EndPointPosition
	{
		get
		{
			return this.EndPoint.position;
		}
	}

	// Token: 0x1700064E RID: 1614
	// (get) Token: 0x060027A7 RID: 10151 RVA: 0x000AC780 File Offset: 0x000AA980
	public float LightIntensityInThisFrame
	{
		get
		{
			return this.LightIntensity + this.LightIntensityTurbulence.TurbulenceValueInThisFrame;
		}
	}

	// Token: 0x1700064F RID: 1615
	// (get) Token: 0x060027A8 RID: 10152 RVA: 0x000AC794 File Offset: 0x000AA994
	public float LightCapsuleRadiusInThisFrame
	{
		get
		{
			return this.CapsuleRadius + this.LightRadiusTurbulence.TurbulenceValueInThisFrame;
		}
	}

	// Token: 0x060027A9 RID: 10153 RVA: 0x000AC7A8 File Offset: 0x000AA9A8
	public bool IsVisibleInCamera(GameplayCamera gameplayCamera)
	{
		CameraController controller = gameplayCamera.Controller;
		float num = Vector3.Distance(this.StartPointPosition, this.EndPointPosition);
		for (float num2 = 0f; num2 < num + Mathf.Epsilon; num2 += this.CapsuleRadius * 2f)
		{
			Vector3 center = Vector3.Lerp(this.StartPointPosition, this.EndPointPosition, num2 / num);
			if (controller.InsideFrustum(new Bounds(center, new Vector3(this.CapsuleRadius, this.CapsuleRadius, this.CapsuleRadius))))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060027AA RID: 10154 RVA: 0x000AC835 File Offset: 0x000AAA35
	public void OnEnable()
	{
		SpiritLightCapsuleVisualAffector.All.Add(this);
	}

	// Token: 0x060027AB RID: 10155 RVA: 0x000AC842 File Offset: 0x000AAA42
	public void OnDisable()
	{
		SpiritLightCapsuleVisualAffector.All.Remove(this);
	}

	// Token: 0x04002241 RID: 8769
	public Transform StartPoint;

	// Token: 0x04002242 RID: 8770
	public Transform EndPoint;

	// Token: 0x04002243 RID: 8771
	public float LightIntensity = 1f;

	// Token: 0x04002244 RID: 8772
	public float CapsuleRadius = 10f;

	// Token: 0x04002245 RID: 8773
	public SpiritLightTurbulence LightRadiusTurbulence;

	// Token: 0x04002246 RID: 8774
	public SpiritLightTurbulence LightIntensityTurbulence;

	// Token: 0x04002247 RID: 8775
	public static List<SpiritLightCapsuleVisualAffector> All = new List<SpiritLightCapsuleVisualAffector>();
}
