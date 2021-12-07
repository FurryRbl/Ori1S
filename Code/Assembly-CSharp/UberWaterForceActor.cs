using System;
using UnityEngine;

// Token: 0x020007A1 RID: 1953
[AddComponentMenu("Uber Water/Force Actor")]
[ExecuteInEditMode]
public class UberWaterForceActor : MonoBehaviour
{
	// Token: 0x06002D57 RID: 11607 RVA: 0x000C1DCC File Offset: 0x000BFFCC
	private void Awake()
	{
		this.m_control = UberWaterControl.GetNearestWaterControl(base.gameObject);
	}

	// Token: 0x06002D58 RID: 11608 RVA: 0x000C1DE0 File Offset: 0x000BFFE0
	private void FixedUpdate()
	{
		if (!this.m_control || this.Strength <= 0f || this.Radius <= 0f)
		{
			return;
		}
		this.m_control.Impact(base.transform.position, this.Strength, this.Radius, true, 1);
	}

	// Token: 0x06002D59 RID: 11609 RVA: 0x000C1E44 File Offset: 0x000C0044
	private void Update()
	{
		if (Application.isPlaying || !this.m_control || this.Strength <= 0f || this.Radius <= 0f)
		{
			return;
		}
		this.m_control.Impact(base.transform.position, this.Strength, this.Radius, true, 1);
	}

	// Token: 0x040028E4 RID: 10468
	private UberWaterControl m_control;

	// Token: 0x040028E5 RID: 10469
	public float Strength = 5f;

	// Token: 0x040028E6 RID: 10470
	public float Radius = 0.3f;
}
