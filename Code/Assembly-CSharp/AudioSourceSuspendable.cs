using System;
using UnityEngine;

// Token: 0x0200073D RID: 1853
public class AudioSourceSuspendable : Suspendable
{
	// Token: 0x06002B81 RID: 11137 RVA: 0x000BAF1D File Offset: 0x000B911D
	public new void Awake()
	{
		base.Awake();
		this.m_audioSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x06002B82 RID: 11138 RVA: 0x000BAF34 File Offset: 0x000B9134
	public void Suspend()
	{
		this.m_wasPlaying = this.m_audioSource.isPlaying;
		if (this.m_wasPlaying)
		{
			this.m_audioSource.Pause();
		}
		this.m_isSuspended = true;
	}

	// Token: 0x06002B83 RID: 11139 RVA: 0x000BAF6F File Offset: 0x000B916F
	public void Resume()
	{
		if (this.m_wasPlaying)
		{
			this.m_audioSource.Play();
		}
		this.m_isSuspended = false;
	}

	// Token: 0x170006EF RID: 1775
	// (get) Token: 0x06002B85 RID: 11141 RVA: 0x000BAFA7 File Offset: 0x000B91A7
	// (set) Token: 0x06002B84 RID: 11140 RVA: 0x000BAF8E File Offset: 0x000B918E
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

	// Token: 0x0400274F RID: 10063
	private AudioSource m_audioSource;

	// Token: 0x04002750 RID: 10064
	private bool m_wasPlaying;

	// Token: 0x04002751 RID: 10065
	private bool m_isSuspended;
}
