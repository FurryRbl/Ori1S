using System;
using Game;
using UnityEngine;

// Token: 0x0200008D RID: 141
public class EntityDamageReciever : DamageReciever, IDynamicGraphicHierarchy, IProjectileDetonatable
{
	// Token: 0x060005E3 RID: 1507 RVA: 0x0001733F File Offset: 0x0001553F
	public new void OnValidate()
	{
		this.Entity = base.transform.FindComponentUpwards<Entity>();
		this.Entity.DamageReciever = this;
		base.OnValidate();
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x00017364 File Offset: 0x00015564
	public new void Awake()
	{
		base.Awake();
		if (this.Entity == null)
		{
			this.OnValidate();
		}
	}

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00017383 File Offset: 0x00015583
	public override GameObject DisableTarget
	{
		get
		{
			return this.Entity.gameObject;
		}
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x00017390 File Offset: 0x00015590
	public override void OnPoolSpawned()
	{
		this.OnModifyDamage = delegate(Damage A_0)
		{
		};
		EntityDamageReciever.OnEntityDeathEvent = delegate(Entity A_0)
		{
		};
		base.OnPoolSpawned();
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x000173E8 File Offset: 0x000155E8
	public void OnTriggerEnter(Collider collider)
	{
		if (this.CanBeCrushed && collider.GetComponent<CrushPlayer>())
		{
			Damage damage = new Damage(10000f, Vector2.zero, this.Entity.Position, DamageType.Crush, base.gameObject);
			damage.DealToComponents(base.gameObject);
		}
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x00017440 File Offset: 0x00015640
	public override void OnRecieveDamage(Damage damage)
	{
		this.OnModifyDamage(damage);
		if (damage.Type == DamageType.Enemy)
		{
			return;
		}
		if (damage.Type == DamageType.Projectile)
		{
			damage.SetAmount(damage.Amount * 4f);
		}
		if (damage.Type == DamageType.Spikes || damage.Type == DamageType.Lava)
		{
			damage.SetAmount(1000f);
		}
		if (this.Entity.gameObject != base.gameObject)
		{
			damage.DealToComponents(this.Entity.gameObject);
		}
		base.OnRecieveDamage(damage);
		if (base.NoHealthLeft)
		{
			EntityDamageReciever.OnEntityDeathEvent(this.Entity);
			if (damage.Type == DamageType.Projectile && this.Entity is Enemy)
			{
				Projectile component = damage.Sender.GetComponent<Projectile>();
				if (component != null && component.HasBeenBashedByOri)
				{
					AchievementsLogic.Instance.OnProjectileKilledEnemy();
				}
				if (component != null && !component.HasBeenBashedByOri)
				{
					AchievementsLogic.Instance.OnEnemyKilledAnotherEnemy();
				}
			}
			if (damage.Type == DamageType.Crush || damage.Type == DamageType.Spikes || damage.Type == DamageType.Lava || damage.Type == DamageType.Laser)
			{
				Type type = this.Entity.GetType();
				if (type != typeof(DropSlugEnemy) && type != typeof(KamikazeSootEnemy) && !base.gameObject.name.ToLower().Contains("wall"))
				{
					AchievementsLogic.Instance.OnEnemyKilledItself();
				}
			}
			if (this.Entity is Enemy)
			{
				if (damage.Type == DamageType.ChargeFlame)
				{
					if (Characters.Sein && Characters.Sein.Abilities.Dash)
					{
						if (Characters.Sein.Abilities.Dash.CurrentState == SeinDashAttack.State.ChargeDashing)
						{
							AchievementsLogic.Instance.OnChargeDashKilledEnemy();
						}
						else
						{
							AchievementsLogic.Instance.OnChargeFlameKilledEnemy();
						}
					}
					else
					{
						AchievementsLogic.Instance.OnChargeFlameKilledEnemy();
					}
				}
				else if ((damage.Type == DamageType.Stomp && damage.Force.y < 0f) || damage.Type == DamageType.StompBlast)
				{
					AchievementsLogic.Instance.OnStompKilledEnemy();
				}
				else if (damage.Type == DamageType.SpiritFlameSplatter || damage.Type == DamageType.SpiritFlame)
				{
					AchievementsLogic.Instance.OnSpiritFlameKilledEnemy();
				}
				else if (damage.Type == DamageType.Grenade)
				{
					AchievementsLogic.Instance.OnGrenaedKilledEnemy();
				}
			}
		}
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x000176F3 File Offset: 0x000158F3
	public bool CanDetonateProjectiles()
	{
		return this.IgnoreDamageCondition == null || !this.IgnoreDamageCondition(null);
	}

	// Token: 0x04000497 RID: 1175
	public Entity Entity;

	// Token: 0x04000498 RID: 1176
	public EntityDamageReciever.ModifyDamageDelegate OnModifyDamage = delegate(Damage A_0)
	{
	};

	// Token: 0x04000499 RID: 1177
	public static Action<Entity> OnEntityDeathEvent = delegate(Entity A_0)
	{
	};

	// Token: 0x0400049A RID: 1178
	public bool CanBeCrushed = true;

	// Token: 0x02000537 RID: 1335
	// (Invoke) Token: 0x06002334 RID: 9012
	public delegate void ModifyDamageDelegate(Damage d);
}
