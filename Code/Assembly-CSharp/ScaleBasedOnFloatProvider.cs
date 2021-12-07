using System;
using UnityEngine;

// Token: 0x02000612 RID: 1554
public class ScaleBasedOnFloatProvider : MonoBehaviour
{
	// Token: 0x060026A9 RID: 9897 RVA: 0x000A9796 File Offset: 0x000A7996
	public void Awake()
	{
		this.m_transform = base.transform;
	}

	// Token: 0x060026AA RID: 9898 RVA: 0x000A97A4 File Offset: 0x000A79A4
	public void FixedUpdate()
	{
		Vector3 localScale = this.m_transform.localScale;
		if (this.ScaleX)
		{
			localScale.x = this.ScaleX.GetFloatValue() * this.ScalePerUnitX;
		}
		if (this.ScaleY)
		{
			localScale.y = this.ScaleY.GetFloatValue() * this.ScalePerUnitY;
		}
		this.m_transform.localScale = localScale;
	}

	// Token: 0x0400215D RID: 8541
	public FloatValueProvider ScaleX;

	// Token: 0x0400215E RID: 8542
	public FloatValueProvider ScaleY;

	// Token: 0x0400215F RID: 8543
	public float ScalePerUnitX;

	// Token: 0x04002160 RID: 8544
	public float ScalePerUnitY;

	// Token: 0x04002161 RID: 8545
	private Transform m_transform;
}
