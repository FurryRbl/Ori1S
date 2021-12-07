using System;
using UnityEngine;

// Token: 0x02000940 RID: 2368
public class OneShotEmitter : Suspendable
{
	// Token: 0x06003448 RID: 13384 RVA: 0x000DBC97 File Offset: 0x000D9E97
	private new void Awake()
	{
		base.Awake();
		this.m_emitter = base.GetComponent<ParticleEmitter>();
	}

	// Token: 0x06003449 RID: 13385 RVA: 0x000DBCAB File Offset: 0x000D9EAB
	private void OnPoolSpawned()
	{
		this.m_timeRemaining = 0.15f;
		this.m_emitter.emit = true;
	}

	// Token: 0x0600344A RID: 13386 RVA: 0x000DBCC4 File Offset: 0x000D9EC4
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_timeRemaining -= Time.deltaTime;
		if (this.m_timeRemaining <= 0f && this.m_emitter.emit)
		{
			this.m_emitter.emit = false;
		}
	}

	// Token: 0x1700083E RID: 2110
	// (get) Token: 0x0600344B RID: 13387 RVA: 0x000DBD1B File Offset: 0x000D9F1B
	// (set) Token: 0x0600344C RID: 13388 RVA: 0x000DBD23 File Offset: 0x000D9F23
	public override bool IsSuspended { get; set; }

	// Token: 0x04002F35 RID: 12085
	private float m_timeRemaining = 0.15f;

	// Token: 0x04002F36 RID: 12086
	private ParticleEmitter m_emitter;
}
