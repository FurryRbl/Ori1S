using System;
using UnityEngine;

// Token: 0x02000989 RID: 2441
public class ParticleSystemEmissionRateOverDistance : MonoBehaviour, IPooled
{
	// Token: 0x06003565 RID: 13669 RVA: 0x000DFE30 File Offset: 0x000DE030
	public void OnPoolSpawned()
	{
		this.m_lastPosition = Vector3.zero;
	}

	// Token: 0x06003566 RID: 13670 RVA: 0x000DFE3D File Offset: 0x000DE03D
	public void Awake()
	{
		this.m_particleSystem = base.GetComponent<ParticleSystem>();
		this.m_emissionRate = this.m_particleSystem.emissionRate;
		this.m_particleSystem.emissionRate = 0f;
	}

	// Token: 0x06003567 RID: 13671 RVA: 0x000DFE6C File Offset: 0x000DE06C
	private void Start()
	{
		this.m_lastPosition = base.transform.position;
	}

	// Token: 0x06003568 RID: 13672 RVA: 0x000DFE80 File Offset: 0x000DE080
	private void Update()
	{
		Vector3 position = base.transform.position;
		this.m_particleSystem.emissionRate = this.m_emissionRate * this.EmissionMultiplierOverDistance.Evaluate(Vector3.Distance(position, this.m_lastPosition));
		this.m_lastPosition = position;
	}

	// Token: 0x04002FFB RID: 12283
	public AnimationCurve EmissionMultiplierOverDistance;

	// Token: 0x04002FFC RID: 12284
	private ParticleSystem m_particleSystem;

	// Token: 0x04002FFD RID: 12285
	private Vector3 m_lastPosition;

	// Token: 0x04002FFE RID: 12286
	private float m_emissionRate;
}
