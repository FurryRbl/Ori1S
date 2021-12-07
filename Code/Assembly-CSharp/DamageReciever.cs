using System;
using UnityEngine;

// Token: 0x0200008E RID: 142
public abstract class DamageReciever : SaveSerialize, IDamageReciever, IRespawnReciever, IPooled
{
	// Token: 0x060005EF RID: 1519 RVA: 0x0001773A File Offset: 0x0001593A
	public void OnValidate()
	{
		this.Health = this.MaxHealth;
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00017748 File Offset: 0x00015948
	public virtual void OnPoolSpawned()
	{
		this.Health = this.MaxHealth;
	}

	// Token: 0x17000176 RID: 374
	// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00017756 File Offset: 0x00015956
	public float NormalizedHealth
	{
		get
		{
			return this.Health / this.MaxHealth;
		}
	}

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x060005F2 RID: 1522
	public abstract GameObject DisableTarget { get; }

	// Token: 0x060005F3 RID: 1523 RVA: 0x00017765 File Offset: 0x00015965
	public new void Awake()
	{
		base.Awake();
		this.Health = this.MaxHealth;
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x0001777C File Offset: 0x0001597C
	public virtual void OnRecieveDamage(Damage damage)
	{
		if (!this.DisableTarget.activeSelf)
		{
			return;
		}
		if (this.IgnoreDamageCondition != null && this.IgnoreDamageCondition(damage))
		{
			return;
		}
		int num = Mathf.FloorToInt(this.Health) - Mathf.FloorToInt(this.Health - damage.Amount);
		this.Health -= damage.Amount;
		damage.SetAmount((float)num);
		if (this.DamageAction)
		{
			this.DamageAction.Perform(new DamageContext(damage));
		}
		if (this.NoHealthLeft)
		{
			this.OnDeathEvent.Call(damage);
			if (this.DeathAction)
			{
				this.DeathAction.Perform(new DamageContext(damage));
			}
			this.UpdateActive();
			if (this.DestroyWhenNoHealthLeft)
			{
				InstantiateUtility.Destroy(this.DisableTarget);
			}
		}
		else if (damage.Amount != 0f)
		{
			if (this.HurtAction)
			{
				this.HurtAction.Perform(new DamageContext(damage));
			}
			if (this.DamageAnimator)
			{
				this.DamageAnimator.Restart();
			}
		}
	}

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x060005F5 RID: 1525 RVA: 0x000178C6 File Offset: 0x00015AC6
	public bool NoHealthLeft
	{
		get
		{
			return this.Health <= 0f;
		}
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x000178D8 File Offset: 0x00015AD8
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Health);
		ar.Serialize(ref this.MaxHealth);
		if (ar.Reading)
		{
			this.UpdateActive();
		}
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x00017903 File Offset: 0x00015B03
	public void UpdateActive()
	{
		if (this.DisableWhenNoHealthLeft)
		{
			this.DisableTarget.SetActive(!this.NoHealthLeft);
		}
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x00017924 File Offset: 0x00015B24
	public void OnTimedRespawn()
	{
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x00017928 File Offset: 0x00015B28
	public void RegisterRespawnDelegate(Action onRespawn)
	{
		this.OnDeathEvent.Add(delegate(Damage a)
		{
			onRespawn();
		});
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x00017959 File Offset: 0x00015B59
	public void SetHealth(float health)
	{
		this.Health = health;
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x00017962 File Offset: 0x00015B62
	public void SetMaxHealth(float maxHealth)
	{
		this.MaxHealth = maxHealth;
	}

	// Token: 0x0400049F RID: 1183
	public ActionMethod DamageAction;

	// Token: 0x040004A0 RID: 1184
	public ActionMethod DeathAction;

	// Token: 0x040004A1 RID: 1185
	public ActionMethod HurtAction;

	// Token: 0x040004A2 RID: 1186
	public LegacyAnimator DamageAnimator;

	// Token: 0x040004A3 RID: 1187
	public float MaxHealth;

	// Token: 0x040004A4 RID: 1188
	[HideInInspector]
	public float Health;

	// Token: 0x040004A5 RID: 1189
	public bool DisableWhenNoHealthLeft = true;

	// Token: 0x040004A6 RID: 1190
	public bool DestroyWhenNoHealthLeft;

	// Token: 0x040004A7 RID: 1191
	public UberDelegate<Damage> OnDeathEvent = new UberDelegate<Damage>();

	// Token: 0x040004A8 RID: 1192
	public Func<Damage, bool> IgnoreDamageCondition;

	// Token: 0x040004A9 RID: 1193
	public bool BounceOnStomp = true;
}
