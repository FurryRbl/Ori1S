using System;
using UnityEngine;

// Token: 0x02000460 RID: 1120
public class DisableEmitterOnKill : MonoBehaviour, IKillReciever
{
	// Token: 0x06001ED6 RID: 7894 RVA: 0x00087CEC File Offset: 0x00085EEC
	private void Awake()
	{
		this.m_emitter = base.GetComponent<ParticleEmitter>();
		this.m_particleSystem = base.GetComponent<ParticleSystem>();
		if (this.m_emitter != null)
		{
			this.m_doEmit = this.m_emitter.emit;
		}
		if (this.m_particleSystem != null)
		{
			this.m_doEmit = this.m_particleSystem.enableEmission;
		}
	}

	// Token: 0x06001ED7 RID: 7895 RVA: 0x00087D58 File Offset: 0x00085F58
	private void OnPoolSpawned()
	{
		if (this.m_emitter)
		{
			this.m_emitter.emit = this.m_doEmit;
		}
		if (this.m_particleSystem)
		{
			this.m_particleSystem.enableEmission = this.m_doEmit;
		}
	}

	// Token: 0x06001ED8 RID: 7896 RVA: 0x00087DA8 File Offset: 0x00085FA8
	public void OnKill()
	{
		if (this.m_emitter)
		{
			this.m_emitter.emit = false;
		}
		if (this.m_particleSystem)
		{
			this.m_particleSystem.enableEmission = false;
		}
	}

	// Token: 0x04001AAB RID: 6827
	private ParticleEmitter m_emitter;

	// Token: 0x04001AAC RID: 6828
	private ParticleSystem m_particleSystem;

	// Token: 0x04001AAD RID: 6829
	private bool m_doEmit;
}
