using System;
using UnityEngine;

// Token: 0x020005F6 RID: 1526
public class SelfDestruct : Suspendable, IPooled
{
	// Token: 0x0600263C RID: 9788 RVA: 0x000A7C8F File Offset: 0x000A5E8F
	public void OnPoolSpawned()
	{
		this.m_timeRemaining = this.WaitTime;
	}

	// Token: 0x0600263D RID: 9789 RVA: 0x000A7C9D File Offset: 0x000A5E9D
	public void Start()
	{
		this.m_timeRemaining = this.WaitTime;
	}

	// Token: 0x0600263E RID: 9790 RVA: 0x000A7CAC File Offset: 0x000A5EAC
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.m_timeRemaining > 0f)
		{
			this.m_timeRemaining -= Time.deltaTime;
			if (this.HalfTimeOnEasy && DifficultyController.Instance.Difficulty == DifficultyMode.Easy)
			{
				this.m_timeRemaining -= Time.deltaTime;
			}
			if (this.m_timeRemaining <= 0f)
			{
				this.m_timeRemaining = 0f;
				Damage damage = new Damage(9999f, Vector2.zero, base.transform.position, DamageType.Explosion, base.gameObject);
				damage.DealToComponents(this.Entity.DamageReciever.gameObject);
			}
		}
	}

	// Token: 0x17000618 RID: 1560
	// (get) Token: 0x0600263F RID: 9791 RVA: 0x000A7D66 File Offset: 0x000A5F66
	// (set) Token: 0x06002640 RID: 9792 RVA: 0x000A7D6E File Offset: 0x000A5F6E
	public override bool IsSuspended { get; set; }

	// Token: 0x040020DB RID: 8411
	public float WaitTime;

	// Token: 0x040020DC RID: 8412
	public Entity Entity;

	// Token: 0x040020DD RID: 8413
	public bool HalfTimeOnEasy;

	// Token: 0x040020DE RID: 8414
	private float m_timeRemaining;
}
