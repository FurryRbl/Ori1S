using System;
using UnityEngine;

// Token: 0x020001EC RID: 492
public class LimitedLifetime : Suspendable
{
	// Token: 0x060010E7 RID: 4327 RVA: 0x0004D46E File Offset: 0x0004B66E
	private void OnPoolSpawned()
	{
		this.m_lifeTimeRemaining = this.Lifetime;
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x0004D47C File Offset: 0x0004B67C
	public void Start()
	{
		this.m_lifeTimeRemaining = this.Lifetime;
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x0004D48A File Offset: 0x0004B68A
	public void SetRemainingLifetime(float lifetime)
	{
		this.Lifetime = lifetime;
		this.m_lifeTimeRemaining = lifetime;
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x0004D49C File Offset: 0x0004B69C
	private void FixedUpdate()
	{
		this.m_lifeTimeRemaining -= ((!this.IsSuspended) ? Time.deltaTime : 0f);
		if (this.m_lifeTimeRemaining < 0f)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x170002FC RID: 764
	// (get) Token: 0x060010EB RID: 4331 RVA: 0x0004D4EB File Offset: 0x0004B6EB
	// (set) Token: 0x060010EC RID: 4332 RVA: 0x0004D4F3 File Offset: 0x0004B6F3
	public override bool IsSuspended { get; set; }

	// Token: 0x04000EAA RID: 3754
	public float Lifetime;

	// Token: 0x04000EAB RID: 3755
	private float m_lifeTimeRemaining = 1f;
}
