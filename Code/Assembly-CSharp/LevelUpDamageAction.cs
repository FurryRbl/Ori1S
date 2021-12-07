using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x020002F5 RID: 757
public class LevelUpDamageAction : ActionMethod, ISuspendable
{
	// Token: 0x060016BC RID: 5820 RVA: 0x000635B1 File Offset: 0x000617B1
	public override void Perform(IContext context)
	{
		this.m_active = true;
	}

	// Token: 0x060016BD RID: 5821 RVA: 0x000635BA File Offset: 0x000617BA
	public override void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
	}

	// Token: 0x060016BE RID: 5822 RVA: 0x000635C8 File Offset: 0x000617C8
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x000635D8 File Offset: 0x000617D8
	public void FixedUpdate()
	{
		if (!this.m_active)
		{
			return;
		}
		this.m_time += Time.deltaTime;
		this.m_delayTime -= Time.deltaTime;
		if (this.m_delayTime < 0f)
		{
			this.m_delayTime = 0.1f;
			float num = this.DistanceOverTime.Evaluate(this.m_time);
			List<IAttackable> attackables = Targets.Attackables;
			for (int i = 0; i < attackables.Count; i++)
			{
				IAttackable attackable = attackables[i];
				if (!InstantiateUtility.IsDestroyed(attackable as Component))
				{
					if (attackable.CanBeLevelUpBlasted())
					{
						if (!this.m_attackables.Contains(attackable))
						{
							if (Vector3.Distance(base.transform.position, attackable.Position) <= num)
							{
								this.m_attackables.Add(attackable);
								Damage damage = new Damage((float)this.Damage, (attackable.Position - base.transform.position).normalized, attackable.Position, DamageType.LevelUp, base.gameObject);
								damage.DealToComponents((attackable as Component).gameObject);
							}
						}
					}
				}
			}
		}
		if (this.m_time > this.Duration)
		{
			this.m_active = false;
			this.m_time = 0f;
			this.m_attackables.Clear();
		}
	}

	// Token: 0x170003FF RID: 1023
	// (get) Token: 0x060016C0 RID: 5824 RVA: 0x00063750 File Offset: 0x00061950
	// (set) Token: 0x060016C1 RID: 5825 RVA: 0x00063758 File Offset: 0x00061958
	public bool IsSuspended { get; set; }

	// Token: 0x0400139C RID: 5020
	private readonly HashSet<IAttackable> m_attackables = new HashSet<IAttackable>();

	// Token: 0x0400139D RID: 5021
	private bool m_active;

	// Token: 0x0400139E RID: 5022
	private float m_time;

	// Token: 0x0400139F RID: 5023
	public AnimationCurve DistanceOverTime;

	// Token: 0x040013A0 RID: 5024
	public float Duration;

	// Token: 0x040013A1 RID: 5025
	public int Damage;

	// Token: 0x040013A2 RID: 5026
	private float m_delayTime;
}
