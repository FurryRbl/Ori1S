using System;
using UnityEngine;

// Token: 0x020003B3 RID: 947
public class LegacyParticleEmissionRateAnimator : LegacyAnimator
{
	// Token: 0x06001A66 RID: 6758 RVA: 0x00071A58 File Offset: 0x0006FC58
	private void FetchInitialEmitterValues()
	{
		this.m_emitter = base.GetComponent<ParticleEmitter>();
		if (this.m_emitter)
		{
			this.m_startMinEmission = this.m_emitter.minEmission;
			this.m_startMaxEmission = this.m_emitter.maxEmission;
		}
		this.m_particleSystem = base.GetComponent<ParticleSystem>();
		if (this.m_particleSystem)
		{
			this.m_startEmission = this.m_particleSystem.emissionRate;
		}
	}

	// Token: 0x06001A67 RID: 6759 RVA: 0x00071AD0 File Offset: 0x0006FCD0
	public override void Awake()
	{
		this.FetchInitialEmitterValues();
		base.Awake();
	}

	// Token: 0x06001A68 RID: 6760 RVA: 0x00071AE0 File Offset: 0x0006FCE0
	protected override void AnimateIt(float value)
	{
		if (this.m_emitter)
		{
			this.m_emitter.minEmission = value * this.m_startMinEmission;
			this.m_emitter.maxEmission = value * this.m_startMaxEmission;
		}
		if (this.m_particleSystem)
		{
			this.m_particleSystem.emissionRate = value * this.m_startEmission;
		}
	}

	// Token: 0x06001A69 RID: 6761 RVA: 0x00071B46 File Offset: 0x0006FD46
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(1f);
	}

	// Token: 0x040016D8 RID: 5848
	private ParticleEmitter m_emitter;

	// Token: 0x040016D9 RID: 5849
	private ParticleSystem m_particleSystem;

	// Token: 0x040016DA RID: 5850
	private float m_startMinEmission;

	// Token: 0x040016DB RID: 5851
	private float m_startMaxEmission;

	// Token: 0x040016DC RID: 5852
	private float m_startEmission;
}
