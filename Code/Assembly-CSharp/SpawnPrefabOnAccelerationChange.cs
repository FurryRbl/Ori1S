using System;
using UnityEngine;

// Token: 0x020006DA RID: 1754
public class SpawnPrefabOnAccelerationChange : MonoBehaviour
{
	// Token: 0x060029F5 RID: 10741 RVA: 0x000B4B7C File Offset: 0x000B2D7C
	private void Awake()
	{
	}

	// Token: 0x060029F6 RID: 10742 RVA: 0x000B4B7E File Offset: 0x000B2D7E
	private void Start()
	{
		this.m_previousPosition = base.transform.position;
		this.m_previousSpeed = 0f;
		this.m_lastInstanciationTime = -100000f;
	}

	// Token: 0x060029F7 RID: 10743 RVA: 0x000B4BA8 File Offset: 0x000B2DA8
	private void FixedUpdate()
	{
		Vector3 position = base.transform.position;
		float magnitude = (position - this.m_previousPosition).magnitude;
		float num = magnitude - this.m_previousSpeed;
		this.m_previousPosition = position;
		this.m_previousSpeed = magnitude;
		if (num > this.MinimalAccelerationToSpawn && Time.time - this.m_lastInstanciationTime > this.CooldownTime)
		{
			InstantiateUtility.Instantiate(this.PrefabToSpawn, base.transform.position, Quaternion.identity);
			this.m_lastInstanciationTime = Time.time;
		}
	}

	// Token: 0x0400257F RID: 9599
	public GameObject PrefabToSpawn;

	// Token: 0x04002580 RID: 9600
	public float MinimalAccelerationToSpawn = 2f;

	// Token: 0x04002581 RID: 9601
	public float CooldownTime = 1f;

	// Token: 0x04002582 RID: 9602
	public float EmissionRatioMultiplier = 1f;

	// Token: 0x04002583 RID: 9603
	private Vector3 m_previousPosition;

	// Token: 0x04002584 RID: 9604
	private float m_lastInstanciationTime;

	// Token: 0x04002585 RID: 9605
	private float m_previousSpeed;
}
