using System;
using UnityEngine;

// Token: 0x02000958 RID: 2392
public class ClearTrailOverCertainDistance : MonoBehaviour, IPooled
{
	// Token: 0x060034AC RID: 13484 RVA: 0x000DCF73 File Offset: 0x000DB173
	public void Awake()
	{
		this.m_trailRenderer = base.GetComponent<TrailRenderer>();
		this.m_time = this.m_trailRenderer.time;
	}

	// Token: 0x060034AD RID: 13485 RVA: 0x000DCF94 File Offset: 0x000DB194
	public void Update()
	{
		Vector3 position = base.transform.position;
		this.m_trailRenderer.time = this.m_time;
		if (Vector3.Distance(this.m_lastPosition, position) > this.Distance)
		{
			this.m_trailRenderer.time = 0f;
		}
		this.m_lastPosition = position;
	}

	// Token: 0x060034AE RID: 13486 RVA: 0x000DCFEC File Offset: 0x000DB1EC
	public void OnPoolSpawned()
	{
		this.m_lastPosition = Vector3.zero;
		this.m_trailRenderer.time = this.m_time;
	}

	// Token: 0x04002F7B RID: 12155
	public float Distance;

	// Token: 0x04002F7C RID: 12156
	private Vector3 m_lastPosition;

	// Token: 0x04002F7D RID: 12157
	private TrailRenderer m_trailRenderer;

	// Token: 0x04002F7E RID: 12158
	private float m_time;
}
