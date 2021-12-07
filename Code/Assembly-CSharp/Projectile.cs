using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000362 RID: 866
public class Projectile : MonoBehaviour, IDamageReciever, IAttackable, IChargeFlameAttackable, IStompAttackable, IBashAttackable, IPooled, ISuspendable, IPortalVisitor, IReflectable
{
	// Token: 0x17000451 RID: 1105
	// (get) Token: 0x060018B4 RID: 6324 RVA: 0x00069E19 File Offset: 0x00068019
	// (set) Token: 0x060018B5 RID: 6325 RVA: 0x00069E21 File Offset: 0x00068021
	Vector3 IPortalVisitor.Speed
	{
		get
		{
			return this.Direction;
		}
		set
		{
			this.Direction = value;
		}
	}

	// Token: 0x17000452 RID: 1106
	// (get) Token: 0x060018B6 RID: 6326 RVA: 0x00069E2A File Offset: 0x0006802A
	// (set) Token: 0x060018B7 RID: 6327 RVA: 0x00069E32 File Offset: 0x00068032
	public Vector3 Direction { get; set; }

	// Token: 0x17000453 RID: 1107
	// (get) Token: 0x060018B8 RID: 6328 RVA: 0x00069E3B File Offset: 0x0006803B
	// (set) Token: 0x060018B9 RID: 6329 RVA: 0x00069E43 File Offset: 0x00068043
	public float Speed { get; set; }

	// Token: 0x17000454 RID: 1108
	// (get) Token: 0x060018BA RID: 6330 RVA: 0x00069E4C File Offset: 0x0006804C
	// (set) Token: 0x060018BB RID: 6331 RVA: 0x00069E54 File Offset: 0x00068054
	public GameObject LastReflector { get; set; }

	// Token: 0x17000455 RID: 1109
	// (get) Token: 0x060018BC RID: 6332 RVA: 0x00069E5D File Offset: 0x0006805D
	// (set) Token: 0x060018BD RID: 6333 RVA: 0x00069E65 File Offset: 0x00068065
	public Vector3 Displacement { get; set; }

	// Token: 0x17000456 RID: 1110
	// (get) Token: 0x060018BE RID: 6334 RVA: 0x00069E6E File Offset: 0x0006806E
	// (set) Token: 0x060018BF RID: 6335 RVA: 0x00069E76 File Offset: 0x00068076
	public bool IsSuspended { get; set; }

	// Token: 0x060018C0 RID: 6336 RVA: 0x00069E7F File Offset: 0x0006807F
	public void OnValidate()
	{
		this.m_onKillRecievers = base.GetComponentsInChildren(typeof(IKillReciever));
	}

	// Token: 0x060018C1 RID: 6337 RVA: 0x00069E98 File Offset: 0x00068098
	public void OnPoolSpawned()
	{
		this.HasBeenBashedByOri = false;
		this.CurrentTime = 0f;
		this.Gravity = this.m_originalGravity;
		this.Direction = Vector3.left;
		this.Speed = 0f;
		this.m_explode = false;
		this.m_explodeLater = false;
		this.m_lastLoop = null;
		this.LastReflector = null;
		this.Displacement = Vector3.zero;
		this.IsSuspended = false;
		this.Owner = null;
	}

	// Token: 0x060018C2 RID: 6338 RVA: 0x00069F10 File Offset: 0x00068110
	public void Start()
	{
		if (this.RotateSpriteToDirection)
		{
			base.transform.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(this.Direction));
		}
		if (this.EnableCollisionGracePeriod)
		{
			this.m_collider.enabled = false;
		}
	}

	// Token: 0x060018C3 RID: 6339 RVA: 0x00069F6C File Offset: 0x0006816C
	public void Awake()
	{
		this.m_nullify = delegate()
		{
			this.m_lastLoop = null;
		};
		SuspensionManager.Register(this);
		this.Direction = Vector3.left;
		this.Speed = 0f;
		this.m_collider = base.GetComponent<Collider>();
		this.Rigidbody = base.GetComponent<Rigidbody>();
		DamageDealer component = base.GetComponent<DamageDealer>();
		if (component)
		{
			DamageDealer damageDealer = component;
			damageDealer.OnDamageDealtEvent = (Action<GameObject, Damage>)Delegate.Combine(damageDealer.OnDamageDealtEvent, new Action<GameObject, Damage>(this.OnDamageDealt));
		}
		if (this.ProjectileLoop)
		{
			this.m_lastLoop = Sound.Play(this.ProjectileLoop.GetSound(null), base.transform.position, this.m_nullify);
			if (this.m_lastLoop)
			{
				this.m_lastLoop.AttachTo = base.transform;
			}
		}
		this.m_originalGravity = this.Gravity;
	}

	// Token: 0x060018C4 RID: 6340 RVA: 0x0006A058 File Offset: 0x00068258
	public void OnEnable()
	{
		Targets.Attackables.Add(this);
		PortalVistor.All.Add(this);
		this.CurrentTime = 0f;
		this.Rigidbody.velocity = Vector3.zero;
	}

	// Token: 0x060018C5 RID: 6341 RVA: 0x0006A08B File Offset: 0x0006828B
	public void OnDisable()
	{
		Targets.Attackables.Remove(this);
		PortalVistor.All.Remove(this);
	}

	// Token: 0x060018C6 RID: 6342 RVA: 0x0006A0A4 File Offset: 0x000682A4
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060018C7 RID: 6343 RVA: 0x0006A0AC File Offset: 0x000682AC
	public virtual bool CanBeBashed()
	{
		return this.CanProjectileBeBashed;
	}

	// Token: 0x060018C8 RID: 6344 RVA: 0x0006A0B4 File Offset: 0x000682B4
	public bool CanBeSpiritFlamed()
	{
		return false;
	}

	// Token: 0x060018C9 RID: 6345 RVA: 0x0006A0B7 File Offset: 0x000682B7
	public bool IsStompBouncable()
	{
		return false;
	}

	// Token: 0x060018CA RID: 6346 RVA: 0x0006A0BA File Offset: 0x000682BA
	public bool CanBeLevelUpBlasted()
	{
		return false;
	}

	// Token: 0x060018CB RID: 6347 RVA: 0x0006A0BD File Offset: 0x000682BD
	public void OnEnterBash()
	{
		if (this.m_lastLoop)
		{
			this.m_lastLoop.FadeOut(0.3f, true);
		}
	}

	// Token: 0x060018CC RID: 6348 RVA: 0x0006A0E0 File Offset: 0x000682E0
	public void OnBashHighlight()
	{
	}

	// Token: 0x060018CD RID: 6349 RVA: 0x0006A0E2 File Offset: 0x000682E2
	public void OnBashDehighlight()
	{
	}

	// Token: 0x17000457 RID: 1111
	// (get) Token: 0x060018CE RID: 6350 RVA: 0x0006A0E4 File Offset: 0x000682E4
	public int BashPriority
	{
		get
		{
			return 40;
		}
	}

	// Token: 0x060018CF RID: 6351 RVA: 0x0006A0E8 File Offset: 0x000682E8
	public void OnRecieveDamage(Damage damage)
	{
		DamageType type = damage.Type;
		switch (type)
		{
		case DamageType.Bash:
			this.HasBeenBashedByOri = true;
			this.Direction = damage.Force.normalized;
			if (this.UseBashSpeed)
			{
				this.Speed = this.BashSpeed;
			}
			if (this.CancelGravityOnBash)
			{
				this.Gravity = 0f;
			}
			this.Owner = null;
			return;
		case DamageType.Grenade:
			break;
		default:
			if (type != DamageType.ChargeFlame)
			{
				return;
			}
			break;
		case DamageType.StompBlast:
			this.Direction = damage.Force.normalized;
			this.Owner = null;
			return;
		}
		this.Direction = damage.Force.normalized;
		this.Owner = null;
	}

	// Token: 0x17000458 RID: 1112
	// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0006A1C9 File Offset: 0x000683C9
	// (set) Token: 0x060018D1 RID: 6353 RVA: 0x0006A1D6 File Offset: 0x000683D6
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
		set
		{
			base.transform.position = value;
		}
	}

	// Token: 0x060018D2 RID: 6354 RVA: 0x0006A1E4 File Offset: 0x000683E4
	public bool CanBeStomped()
	{
		return true;
	}

	// Token: 0x060018D3 RID: 6355 RVA: 0x0006A1E7 File Offset: 0x000683E7
	public bool CountsTowardsSuperJumpAchievement()
	{
		return false;
	}

	// Token: 0x060018D4 RID: 6356 RVA: 0x0006A1EA File Offset: 0x000683EA
	public bool CanBeChargeFlamed()
	{
		return true;
	}

	// Token: 0x060018D5 RID: 6357 RVA: 0x0006A1ED File Offset: 0x000683ED
	public bool CanBeChargeDashed()
	{
		return false;
	}

	// Token: 0x060018D6 RID: 6358 RVA: 0x0006A1F0 File Offset: 0x000683F0
	public bool CanBeGrenaded()
	{
		return true;
	}

	// Token: 0x060018D7 RID: 6359 RVA: 0x0006A1F3 File Offset: 0x000683F3
	public bool CountsTowardsPowerOfLightAchievement()
	{
		return false;
	}

	// Token: 0x060018D8 RID: 6360 RVA: 0x0006A1F6 File Offset: 0x000683F6
	public bool IsDead()
	{
		return false;
	}

	// Token: 0x060018D9 RID: 6361 RVA: 0x0006A1F9 File Offset: 0x000683F9
	public void OnGoThroughPortal()
	{
	}

	// Token: 0x060018DA RID: 6362 RVA: 0x0006A1FB File Offset: 0x000683FB
	public void OnPortalOverlapEnter()
	{
	}

	// Token: 0x060018DB RID: 6363 RVA: 0x0006A1FD File Offset: 0x000683FD
	public void OnPortalOverlapExit()
	{
	}

	// Token: 0x060018DC RID: 6364 RVA: 0x0006A1FF File Offset: 0x000683FF
	public bool CanBeReflected(float maximumReflectableDamage)
	{
		return true;
	}

	// Token: 0x060018DD RID: 6365 RVA: 0x0006A202 File Offset: 0x00068402
	public void OnGrabbed()
	{
	}

	// Token: 0x060018DE RID: 6366 RVA: 0x0006A204 File Offset: 0x00068404
	public void OnReleased(float speed, Vector3 direction)
	{
	}

	// Token: 0x060018DF RID: 6367 RVA: 0x0006A208 File Offset: 0x00068408
	public void OnDamageDealt(GameObject go, Damage damage)
	{
		if (go == this.Owner)
		{
			return;
		}
		IProjectileDetonatable projectileDetonatable = go.FindComponent<IProjectileDetonatable>();
		if (projectileDetonatable != null && projectileDetonatable.CanDetonateProjectiles())
		{
			this.ExplodeProjectile();
		}
	}

	// Token: 0x060018E0 RID: 6368 RVA: 0x0006A248 File Offset: 0x00068448
	public virtual void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			this.Rigidbody.velocity = Vector3.zero;
			return;
		}
		if (this.EnableCollisionGracePeriod && this.CurrentTime > this.CollisionGracePeriod)
		{
			this.m_collider.enabled = true;
		}
		if (this.m_lastLoop == null && this.ProjectileLoop != null)
		{
			this.m_lastLoop = Sound.Play(this.ProjectileLoop.GetSound(null), base.transform.position, this.m_nullify);
			if (this.m_lastLoop)
			{
				this.m_lastLoop.AttachTo = base.transform;
			}
		}
		this.CurrentTime += Time.deltaTime;
		if (this.CurrentTime > this.MaximumLiveTime)
		{
			this.m_explode = true;
		}
		if (WaterZone.PositionInWater(this.Position))
		{
			this.m_explode = true;
		}
		if (this.Gravity > 0f)
		{
			this.SpeedVector += Vector3.down * this.Gravity * Time.fixedDeltaTime;
		}
		this.UpdateVelocity();
		if (this.RotateSpriteToDirection)
		{
			float num = base.transform.eulerAngles.z;
			num = Mathf.MoveTowardsAngle(num, MoonMath.Angle.AngleFromDirection(this.Direction), this.SpriteTurnSpeed * Time.deltaTime);
			base.transform.eulerAngles = new Vector3(0f, 0f, num);
		}
		if (this.m_explode)
		{
			this.ExplodeProjectile();
		}
		if (this.m_explodeLater)
		{
			this.m_explode = true;
			this.m_explodeLater = false;
		}
	}

	// Token: 0x060018E1 RID: 6369 RVA: 0x0006A410 File Offset: 0x00068610
	public void ExplodeProjectile()
	{
		if (this.m_lastLoop)
		{
			this.m_lastLoop.FadeOut(0.3f, true);
		}
		for (int i = 0; i < this.m_onKillRecievers.Length; i++)
		{
			if (this.m_onKillRecievers[i])
			{
				((IKillReciever)this.m_onKillRecievers[i]).OnKill();
			}
		}
		InstantiateUtility.Destroy(base.gameObject);
	}

	// Token: 0x060018E2 RID: 6370 RVA: 0x0006A486 File Offset: 0x00068686
	public void OnCollisionEnter(Collision collision)
	{
		this.m_explodeLater = true;
	}

	// Token: 0x060018E3 RID: 6371 RVA: 0x0006A48F File Offset: 0x0006868F
	public void OnCollisionStay(Collision collision)
	{
		this.m_explode = true;
	}

	// Token: 0x060018E4 RID: 6372 RVA: 0x0006A498 File Offset: 0x00068698
	public void UpdateVelocity()
	{
		Vector3 vector = -Vector3.ClampMagnitude(this.Displacement / Time.deltaTime, 10f);
		this.Displacement += vector * Time.deltaTime;
		this.Rigidbody.velocity = this.Direction * this.Speed + vector;
	}

	// Token: 0x17000459 RID: 1113
	// (get) Token: 0x060018E5 RID: 6373 RVA: 0x0006A503 File Offset: 0x00068703
	// (set) Token: 0x060018E6 RID: 6374 RVA: 0x0006A518 File Offset: 0x00068718
	public Vector3 SpeedVector
	{
		get
		{
			return this.Speed * this.Direction;
		}
		set
		{
			this.Speed = value.magnitude;
			this.Direction = value.normalized;
		}
	}

	// Token: 0x060018E7 RID: 6375 RVA: 0x0006A540 File Offset: 0x00068740
	public void UpdateSpeedAndDirection()
	{
		this.Direction = this.Rigidbody.velocity.normalized;
		this.Speed = this.Rigidbody.velocity.magnitude;
	}

	// Token: 0x0400153B RID: 5435
	public GameObject Owner;

	// Token: 0x0400153C RID: 5436
	public bool CanProjectileBeBashed = true;

	// Token: 0x0400153D RID: 5437
	public float CollisionGracePeriod = 0.5f;

	// Token: 0x0400153E RID: 5438
	public bool EnableCollisionGracePeriod;

	// Token: 0x0400153F RID: 5439
	public float Gravity;

	// Token: 0x04001540 RID: 5440
	public float MaximumLiveTime = 5f;

	// Token: 0x04001541 RID: 5441
	public SoundProvider ProjectileLoop;

	// Token: 0x04001542 RID: 5442
	public float BashSpeed = 20f;

	// Token: 0x04001543 RID: 5443
	public bool UseBashSpeed;

	// Token: 0x04001544 RID: 5444
	public bool CancelGravityOnBash;

	// Token: 0x04001545 RID: 5445
	public bool RotateSpriteToDirection;

	// Token: 0x04001546 RID: 5446
	public float SpriteTurnSpeed = 360f;

	// Token: 0x04001547 RID: 5447
	[NonSerialized]
	public bool HasBeenBashedByOri;

	// Token: 0x04001548 RID: 5448
	protected float CurrentTime;

	// Token: 0x04001549 RID: 5449
	private float m_originalGravity;

	// Token: 0x0400154A RID: 5450
	private SoundPlayer m_lastLoop;

	// Token: 0x0400154B RID: 5451
	private bool m_explode;

	// Token: 0x0400154C RID: 5452
	private bool m_explodeLater;

	// Token: 0x0400154D RID: 5453
	private Action m_nullify;

	// Token: 0x0400154E RID: 5454
	protected Rigidbody Rigidbody;

	// Token: 0x0400154F RID: 5455
	private Collider m_collider;

	// Token: 0x04001550 RID: 5456
	[SerializeField]
	[HideInInspector]
	private Component[] m_onKillRecievers;
}
