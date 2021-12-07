using System;
using UnityEngine;

// Token: 0x0200077F RID: 1919
[RequireComponent(typeof(ParticleSystem))]
public class ShurikenParticleAnimator : BaseAnimator
{
	// Token: 0x17000718 RID: 1816
	// (get) Token: 0x06002C8E RID: 11406 RVA: 0x000BF4D3 File Offset: 0x000BD6D3
	private ParticleSystem ParticleSystem
	{
		get
		{
			if (this.m_particleSystem == null)
			{
				this.m_particleSystem = base.GetComponent<ParticleSystem>();
			}
			return this.m_particleSystem;
		}
	}

	// Token: 0x06002C8F RID: 11407 RVA: 0x000BF4F8 File Offset: 0x000BD6F8
	public override void OnPoolSpawned()
	{
		base.OnPoolSpawned();
		this.m_startedPlayback = false;
	}

	// Token: 0x06002C90 RID: 11408 RVA: 0x000BF508 File Offset: 0x000BD708
	public override void CacheOriginals()
	{
		this.ParticleSystem.playOnAwake = false;
		this.ParticleSystem.Stop(true);
	}

	// Token: 0x06002C91 RID: 11409 RVA: 0x000BF52D File Offset: 0x000BD72D
	public override void SampleValue(float time, bool forceSample)
	{
		if (!this.m_startedPlayback && time >= 0f)
		{
			this.ParticleSystem.Play(true);
			this.m_startedPlayback = true;
		}
	}

	// Token: 0x06002C92 RID: 11410 RVA: 0x000BF558 File Offset: 0x000BD758
	public override void RestoreToOriginalState()
	{
		this.m_startedPlayback = false;
	}

	// Token: 0x17000719 RID: 1817
	// (get) Token: 0x06002C93 RID: 11411 RVA: 0x000BF561 File Offset: 0x000BD761
	public override float Duration
	{
		get
		{
			return this.ParticleSystem.duration;
		}
	}

	// Token: 0x1700071A RID: 1818
	// (get) Token: 0x06002C94 RID: 11412 RVA: 0x000BF56E File Offset: 0x000BD76E
	public override bool IsLooping
	{
		get
		{
			return this.ParticleSystem.loop;
		}
	}

	// Token: 0x04002852 RID: 10322
	private ParticleSystem m_particleSystem;

	// Token: 0x04002853 RID: 10323
	private bool m_startedPlayback;
}
