using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class GrenadeBurst : MonoBehaviour, IPooled, ISuspendable
{
	// Token: 0x060003FB RID: 1019 RVA: 0x00010D3F File Offset: 0x0000EF3F
	public void OnPoolSpawned()
	{
		this.m_suspended = false;
		this.m_time = 0f;
		this.m_waitDelay = 0f;
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00010D60 File Offset: 0x0000EF60
	public static void IgnoreOnLastInstance(IAttackable attackable)
	{
		if (GrenadeBurst.m_lastInstance)
		{
			GrenadeBurst.m_lastInstance.m_damageAttackables.Add(attackable);
		}
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00010D8D File Offset: 0x0000EF8D
	public void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00010D95 File Offset: 0x0000EF95
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x00010D9D File Offset: 0x0000EF9D
	public void OnEnable()
	{
		GrenadeBurst.m_lastInstance = this;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x00010DA5 File Offset: 0x0000EFA5
	public void OnDisable()
	{
		this.m_damageAttackables.Clear();
		if (GrenadeBurst.m_lastInstance == this)
		{
			GrenadeBurst.m_lastInstance = null;
		}
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00010DC8 File Offset: 0x0000EFC8
	public void Start()
	{
		this.DealDamage();
		this.m_time = 0f;
		this.m_waitDelay = 0f;
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x00010DE8 File Offset: 0x0000EFE8
	public void DealDamage()
	{
		Vector3 position = base.transform.position;
		foreach (IAttackable attackable in Targets.Attackables.ToArray())
		{
			if (!InstantiateUtility.IsDestroyed(attackable as Component))
			{
				if (!this.m_damageAttackables.Contains(attackable))
				{
					if (attackable.CanBeGrenaded())
					{
						Vector3 position2 = attackable.Position;
						Vector3 vector = position2 - position;
						if (vector.magnitude <= this.BurstRadius)
						{
							this.m_damageAttackables.Add(attackable);
							GameObject gameObject = ((Component)attackable).gameObject;
							Damage damage = new Damage(this.DamageAmount, vector.normalized * 3f, position, DamageType.Grenade, base.gameObject);
							damage.DealToComponents(gameObject);
							if (!attackable.IsDead())
							{
								GameObject gameObject2 = (GameObject)InstantiateUtility.Instantiate(this.BurstImpactEffectPrefab, position2, Quaternion.identity);
								gameObject2.transform.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(vector.normalized));
								gameObject2.GetComponent<FollowPositionRotation>().SetTarget(gameObject.transform);
							}
						}
					}
				}
			}
		}
		this.m_waitDelay = 0.1f;
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x00010F4C File Offset: 0x0000F14C
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

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x06000404 RID: 1028 RVA: 0x00010FB0 File Offset: 0x0000F1B0
	// (set) Token: 0x06000405 RID: 1029 RVA: 0x00010FB8 File Offset: 0x0000F1B8
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

	// Token: 0x04000346 RID: 838
	public float BurstRadius = 5f;

	// Token: 0x04000347 RID: 839
	public float DamageAmount = 10f;

	// Token: 0x04000348 RID: 840
	public GameObject BurstImpactEffectPrefab;

	// Token: 0x04000349 RID: 841
	public float DealDamageDuration = 0.5f;

	// Token: 0x0400034A RID: 842
	private float m_time;

	// Token: 0x0400034B RID: 843
	private float m_waitDelay;

	// Token: 0x0400034C RID: 844
	private readonly HashSet<IAttackable> m_damageAttackables = new HashSet<IAttackable>();

	// Token: 0x0400034D RID: 845
	private static GrenadeBurst m_lastInstance;

	// Token: 0x0400034E RID: 846
	private bool m_suspended;
}
