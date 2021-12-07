using System;
using UnityEngine;

// Token: 0x0200073E RID: 1854
public class ParticleSuspender : Suspendable, IPooled
{
	// Token: 0x06002B87 RID: 11143 RVA: 0x000BAFB7 File Offset: 0x000B91B7
	public void OnPoolSpawned()
	{
		this.m_wereParticlesPlaying = false;
		this.m_isSuspended = false;
	}

	// Token: 0x06002B88 RID: 11144 RVA: 0x000BAFC8 File Offset: 0x000B91C8
	public new void Awake()
	{
		base.Awake();
		this.m_legacyParticleEmitter = base.GetComponent<ParticleEmitter>();
		this.m_shurikenParticleSystem = base.GetComponent<ParticleSystem>();
		if (!(this.m_legacyParticleEmitter == null) || this.m_shurikenParticleSystem == null)
		{
		}
	}

	// Token: 0x06002B89 RID: 11145 RVA: 0x000BB018 File Offset: 0x000B9218
	public void Suspend()
	{
		if (this.m_legacyParticleEmitter)
		{
			this.m_wereParticlesPlaying = this.m_legacyParticleEmitter.enabled;
			if (this.m_wereParticlesPlaying)
			{
				this.m_legacyParticleEmitter.enabled = false;
			}
		}
		else if (this.m_shurikenParticleSystem)
		{
			this.m_wereParticlesPlaying = this.m_shurikenParticleSystem.isPlaying;
			if (this.m_wereParticlesPlaying)
			{
				this.m_shurikenParticleSystem.Pause(true);
			}
		}
		this.m_isSuspended = true;
	}

	// Token: 0x06002B8A RID: 11146 RVA: 0x000BB0A4 File Offset: 0x000B92A4
	public void Resume()
	{
		if (this.m_legacyParticleEmitter)
		{
			if (this.m_wereParticlesPlaying)
			{
				this.m_legacyParticleEmitter.enabled = true;
			}
		}
		else if (this.m_shurikenParticleSystem && this.m_wereParticlesPlaying)
		{
			this.m_shurikenParticleSystem.Play(true);
		}
		this.m_isSuspended = false;
	}

	// Token: 0x170006F0 RID: 1776
	// (get) Token: 0x06002B8C RID: 11148 RVA: 0x000BB124 File Offset: 0x000B9324
	// (set) Token: 0x06002B8B RID: 11147 RVA: 0x000BB10B File Offset: 0x000B930B
	public override bool IsSuspended
	{
		get
		{
			return this.m_isSuspended;
		}
		set
		{
			if (value)
			{
				this.Suspend();
			}
			else
			{
				this.Resume();
			}
		}
	}

	// Token: 0x04002752 RID: 10066
	private ParticleEmitter m_legacyParticleEmitter;

	// Token: 0x04002753 RID: 10067
	private ParticleSystem m_shurikenParticleSystem;

	// Token: 0x04002754 RID: 10068
	private bool m_wereParticlesPlaying;

	// Token: 0x04002755 RID: 10069
	private bool m_isSuspended;
}
