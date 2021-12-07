using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class ChargeFlameBurst : MonoBehaviour, IPooled, ISuspendable
{
	// Token: 0x06000464 RID: 1124 RVA: 0x00011DD4 File Offset: 0x0000FFD4
	public void OnPoolSpawned()
	{
		this.m_suspended = false;
		this.m_simultaneousEnemies = 0;
		this.m_time = 0f;
		this.m_waitDelay = 0f;
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00011E08 File Offset: 0x00010008
	public static void IgnoreOnLastInstance(IAttackable attackable)
	{
		if (ChargeFlameBurst.m_lastInstance)
		{
			ChargeFlameBurst.m_lastInstance.m_damageAttackables.Add(attackable);
		}
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x00011E35 File Offset: 0x00010035
	public void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00011E3D File Offset: 0x0001003D
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00011E45 File Offset: 0x00010045
	public void OnEnable()
	{
		ChargeFlameBurst.m_lastInstance = this;
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x00011E4D File Offset: 0x0001004D
	public void OnDisable()
	{
		this.m_damageAttackables.Clear();
		if (ChargeFlameBurst.m_lastInstance == this)
		{
			ChargeFlameBurst.m_lastInstance = null;
		}
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00011E70 File Offset: 0x00010070
	public void Start()
	{
		this.DealDamage();
		this.m_time = 0f;
		this.m_simultaneousEnemies = 0;
		this.m_waitDelay = 0f;
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00011E98 File Offset: 0x00010098
	public void DealDamage()
	{
		Vector3 position = base.transform.position;
		foreach (IAttackable attackable in Targets.Attackables.ToArray())
		{
			if (!InstantiateUtility.IsDestroyed(attackable as Component))
			{
				if (!this.m_damageAttackables.Contains(attackable))
				{
					if (attackable.CanBeChargeFlamed())
					{
						Vector3 position2 = attackable.Position;
						Vector3 vector = position2 - position;
						if (vector.magnitude <= this.BurstRadius)
						{
							this.m_damageAttackables.Add(attackable);
							GameObject gameObject = ((Component)attackable).gameObject;
							Damage damage = new Damage(this.DamageAmount, vector.normalized * 3f, position, DamageType.ChargeFlame, base.gameObject);
							damage.DealToComponents(gameObject);
							bool flag = attackable.IsDead();
							if (!flag)
							{
								GameObject gameObject2 = (GameObject)InstantiateUtility.Instantiate(this.BurstImpactEffectPrefab, position2, Quaternion.identity);
								gameObject2.transform.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(vector.normalized));
								gameObject2.GetComponent<FollowPositionRotation>().SetTarget(gameObject.transform);
							}
							if (flag && attackable is IChargeFlameAttackable && ((IChargeFlameAttackable)attackable).CountsTowardsPowerOfLightAchievement())
							{
								this.m_simultaneousEnemies++;
							}
						}
					}
				}
			}
		}
		if (this.m_simultaneousEnemies >= 4)
		{
			AchievementsController.AwardAchievement(Characters.Sein.Abilities.ChargeFlame.KillEnemiesSimultaneouslyAchievement);
		}
		this.m_waitDelay = 0.1f;
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00012050 File Offset: 0x00010250
	public void FixedUpdate()
	{
		if (this.m_suspended)
		{
			return;
		}
		this.m_time += Time.deltaTime;
		this.m_waitDelay -= Time.deltaTime;
		if (this.m_time < this.DealDamageDuration && this.m_waitDelay <= 0f)
		{
			this.DealDamage();
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x0600046D RID: 1133 RVA: 0x000120B4 File Offset: 0x000102B4
	// (set) Token: 0x0600046E RID: 1134 RVA: 0x000120BC File Offset: 0x000102BC
	public bool IsSuspended
	{
		get
		{
			return this.m_suspended;
		}
		set
		{
			this.m_suspended = value;
		}
	}

	// Token: 0x04000397 RID: 919
	public float BurstRadius = 5f;

	// Token: 0x04000398 RID: 920
	public float DamageAmount = 10f;

	// Token: 0x04000399 RID: 921
	public GameObject BurstImpactEffectPrefab;

	// Token: 0x0400039A RID: 922
	public float DealDamageDuration = 0.5f;

	// Token: 0x0400039B RID: 923
	private float m_time;

	// Token: 0x0400039C RID: 924
	private float m_waitDelay;

	// Token: 0x0400039D RID: 925
	private readonly HashSet<IAttackable> m_damageAttackables = new HashSet<IAttackable>();

	// Token: 0x0400039E RID: 926
	private int m_simultaneousEnemies;

	// Token: 0x0400039F RID: 927
	private static ChargeFlameBurst m_lastInstance;

	// Token: 0x040003A0 RID: 928
	private bool m_suspended;
}
