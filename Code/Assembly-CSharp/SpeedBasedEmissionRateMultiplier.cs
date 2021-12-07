using System;
using UnityEngine;

// Token: 0x02000905 RID: 2309
public class SpeedBasedEmissionRateMultiplier : MonoBehaviour, IPooled
{
	// Token: 0x06003345 RID: 13125 RVA: 0x000D80A8 File Offset: 0x000D62A8
	public void OnPoolSpawned()
	{
		this.m_previousPosition = Vector3.zero;
		this.m_shouldHaltEmission = false;
		this.m_emissionRateBumpMultiplier = 1f;
		if (this.m_emitter)
		{
			this.m_emitter.minEmission = this.m_originalMinEmission;
			this.m_emitter.maxEmission = this.m_originalMaxEmission;
		}
		if (this.m_particleSystem)
		{
			this.m_particleSystem.emissionRate = this.m_origianlEmission;
		}
	}

	// Token: 0x06003346 RID: 13126 RVA: 0x000D8128 File Offset: 0x000D6328
	private void Awake()
	{
		this.m_emitter = base.GetComponent<ParticleEmitter>();
		this.m_particleSystem = base.GetComponent<ParticleSystem>();
		if (this.m_emitter)
		{
			this.m_originalMinEmission = this.m_emitter.minEmission;
			this.m_originalMaxEmission = this.m_emitter.maxEmission;
		}
		if (this.m_particleSystem)
		{
			this.m_origianlEmission = this.m_particleSystem.emissionRate;
		}
	}

	// Token: 0x06003347 RID: 13127 RVA: 0x000D81A0 File Offset: 0x000D63A0
	private void Start()
	{
		this.m_previousPosition = base.transform.position;
	}

	// Token: 0x06003348 RID: 13128 RVA: 0x000D81B4 File Offset: 0x000D63B4
	private void FixedUpdate()
	{
		Vector3 position = base.transform.position;
		float magnitude = (position - this.m_previousPosition).magnitude;
		this.m_previousPosition = position;
		if (magnitude > this.MaxAllowedSpeedOverAFrame)
		{
			this.HaltEmissionForAFrame();
			this.BoostEmissionRateForAFrame(this.EmissionBoostAfterUnhalt);
		}
		if (this.m_shouldHaltEmission)
		{
			if (this.m_emitter)
			{
				this.m_emitter.minEmission = 0f;
				this.m_emitter.maxEmission = 0f;
			}
			if (this.m_particleSystem)
			{
				this.m_particleSystem.emissionRate = 0f;
			}
			return;
		}
		float num = magnitude * this.SpeedToEmissionRelation;
		num = Mathf.Clamp(num, this.MinRatio, this.MaxRatio);
		num *= this.m_emissionRateBumpMultiplier;
		if (this.m_emitter)
		{
			this.m_emitter.minEmission = this.m_originalMinEmission * num;
			this.m_emitter.maxEmission = this.m_originalMaxEmission * num;
		}
		if (this.m_particleSystem)
		{
			this.m_particleSystem.emissionRate = num;
		}
	}

	// Token: 0x06003349 RID: 13129 RVA: 0x000D82DA File Offset: 0x000D64DA
	public void HaltEmission()
	{
		this.m_shouldHaltEmission = true;
	}

	// Token: 0x0600334A RID: 13130 RVA: 0x000D82E3 File Offset: 0x000D64E3
	public void HaltEmissionForAFrame()
	{
		this.HaltEmission();
		base.Invoke("UnhaltEmission", 0.04f);
	}

	// Token: 0x0600334B RID: 13131 RVA: 0x000D82FC File Offset: 0x000D64FC
	public void UnhaltEmission()
	{
		if (!this.m_shouldHaltEmission)
		{
			return;
		}
		this.m_shouldHaltEmission = false;
		this.m_previousPosition = base.transform.position;
	}

	// Token: 0x0600334C RID: 13132 RVA: 0x000D832D File Offset: 0x000D652D
	public void BoostEmissionRateForAFrame(float factor)
	{
		this.m_emissionRateBumpMultiplier = factor;
		base.Invoke("UnbumpEmissionRate", 0.04f);
	}

	// Token: 0x0600334D RID: 13133 RVA: 0x000D8346 File Offset: 0x000D6546
	private void UnbumpEmissionRate()
	{
		this.m_emissionRateBumpMultiplier = 1f;
	}

	// Token: 0x04002E3D RID: 11837
	public float SpeedToEmissionRelation = 10f;

	// Token: 0x04002E3E RID: 11838
	public float MinRatio = 1f;

	// Token: 0x04002E3F RID: 11839
	public float MaxRatio = 5f;

	// Token: 0x04002E40 RID: 11840
	public float MaxAllowedSpeedOverAFrame = 2f;

	// Token: 0x04002E41 RID: 11841
	public float EmissionBoostAfterUnhalt = 4f;

	// Token: 0x04002E42 RID: 11842
	private Vector3 m_previousPosition;

	// Token: 0x04002E43 RID: 11843
	private float m_originalMinEmission;

	// Token: 0x04002E44 RID: 11844
	private float m_originalMaxEmission;

	// Token: 0x04002E45 RID: 11845
	private float m_origianlEmission;

	// Token: 0x04002E46 RID: 11846
	private ParticleEmitter m_emitter;

	// Token: 0x04002E47 RID: 11847
	private ParticleSystem m_particleSystem;

	// Token: 0x04002E48 RID: 11848
	private bool m_shouldHaltEmission;

	// Token: 0x04002E49 RID: 11849
	private float m_emissionRateBumpMultiplier = 1f;
}
