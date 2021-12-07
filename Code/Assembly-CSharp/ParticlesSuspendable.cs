using System;
using UnityEngine;

// Token: 0x0200073F RID: 1855
public class ParticlesSuspendable : Suspendable
{
	// Token: 0x06002B8E RID: 11150 RVA: 0x000BB134 File Offset: 0x000B9334
	public new void Awake()
	{
		base.Awake();
		this.m_particleSystem = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x06002B8F RID: 11151 RVA: 0x000BB148 File Offset: 0x000B9348
	public void Suspend()
	{
		this.m_particleSystem.Pause();
		this.m_isSuspended = true;
	}

	// Token: 0x06002B90 RID: 11152 RVA: 0x000BB15C File Offset: 0x000B935C
	public void Resume()
	{
		this.m_particleSystem.Play();
		this.m_isSuspended = false;
	}

	// Token: 0x170006F1 RID: 1777
	// (get) Token: 0x06002B92 RID: 11154 RVA: 0x000BB189 File Offset: 0x000B9389
	// (set) Token: 0x06002B91 RID: 11153 RVA: 0x000BB170 File Offset: 0x000B9370
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

	// Token: 0x04002756 RID: 10070
	private ParticleSystem m_particleSystem;

	// Token: 0x04002757 RID: 10071
	private bool m_isSuspended;
}
