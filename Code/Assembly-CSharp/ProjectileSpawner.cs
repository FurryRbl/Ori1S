using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x020005A0 RID: 1440
public class ProjectileSpawner : SaveSerialize, ISuspendable
{
	// Token: 0x1700060A RID: 1546
	// (get) Token: 0x060024DD RID: 9437 RVA: 0x000A0C57 File Offset: 0x0009EE57
	public Vector3 Position
	{
		get
		{
			return this.m_transform.position;
		}
	}

	// Token: 0x1700060B RID: 1547
	// (get) Token: 0x060024DE RID: 9438 RVA: 0x000A0C64 File Offset: 0x0009EE64
	// (set) Token: 0x060024DF RID: 9439 RVA: 0x000A0C6C File Offset: 0x0009EE6C
	public float TimeSinceLastShot { get; set; }

	// Token: 0x060024E0 RID: 9440 RVA: 0x000A0C75 File Offset: 0x0009EE75
	public override void Awake()
	{
		this.TimeSinceLastShot = float.MaxValue;
		base.Awake();
		SuspensionManager.Register(this);
	}

	// Token: 0x060024E1 RID: 9441 RVA: 0x000A0C8E File Offset: 0x0009EE8E
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060024E2 RID: 9442 RVA: 0x000A0C9C File Offset: 0x0009EE9C
	public void Start()
	{
		this.m_timedTrigger = base.GetComponent<TimedTrigger>();
		this.m_transform = base.transform;
	}

	// Token: 0x1700060C RID: 1548
	// (get) Token: 0x060024E3 RID: 9443 RVA: 0x000A0CB6 File Offset: 0x0009EEB6
	// (set) Token: 0x060024E4 RID: 9444 RVA: 0x000A0CD5 File Offset: 0x0009EED5
	private bool TimerPaused
	{
		get
		{
			return this.m_timedTrigger && this.m_timedTrigger.Paused;
		}
		set
		{
			if (this.m_timedTrigger)
			{
				this.m_timedTrigger.Paused = value;
			}
		}
	}

	// Token: 0x060024E5 RID: 9445 RVA: 0x000A0CF3 File Offset: 0x0009EEF3
	public void OnDisable()
	{
		this.TimerPaused = false;
	}

	// Token: 0x060024E6 RID: 9446 RVA: 0x000A0CFC File Offset: 0x0009EEFC
	public void OnTimedTrigger()
	{
		this.SpawnProjectile();
	}

	// Token: 0x060024E7 RID: 9447 RVA: 0x000A0D08 File Offset: 0x0009EF08
	public Projectile SpawnProjectile()
	{
		this.TimeSinceLastShot = 0f;
		GameObject gameObject = InstantiateUtility.Instantiate(this.Projectile) as GameObject;
		gameObject.transform.SetParentMaintainingLocalTransform(base.transform.root);
		this.m_lastProjectile = gameObject;
		gameObject.transform.position = base.transform.position;
		Projectile component = gameObject.GetComponent<Projectile>();
		component.Speed = this.Speed;
		component.Direction = this.Direction;
		if (this.Direction == Vector3.zero)
		{
			component.Direction = base.transform.up;
		}
		component.Gravity = this.Gravity;
		if (this.Owner)
		{
			component.Owner = this.Owner;
		}
		if (this.SpawnSound)
		{
			Sound.Play(this.SpawnSound, base.transform.position, null, this.SpawnSoundVolume, null);
		}
		return component;
	}

	// Token: 0x060024E8 RID: 9448 RVA: 0x000A0E04 File Offset: 0x0009F004
	public void AimAt(Transform target)
	{
		this.Direction = (target.position - this.m_transform.position).normalized;
	}

	// Token: 0x060024E9 RID: 9449 RVA: 0x000A0E35 File Offset: 0x0009F035
	public override void Serialize(Archive ar)
	{
	}

	// Token: 0x060024EA RID: 9450 RVA: 0x000A0E38 File Offset: 0x0009F038
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (InstantiateUtility.IsDestroyed(this.m_lastProjectile))
		{
			this.m_lastProjectile = null;
		}
		if (this.WaitForProjectileToBeDestroyed && !this.TimerPaused && this.m_lastProjectile != null)
		{
			this.TimerPaused = true;
		}
		if (this.WaitForProjectileToBeDestroyed && this.TimerPaused && this.m_lastProjectile == null)
		{
			this.TimerPaused = false;
		}
		this.TimeSinceLastShot += Time.deltaTime;
	}

	// Token: 0x1700060D RID: 1549
	// (get) Token: 0x060024EB RID: 9451 RVA: 0x000A0ED6 File Offset: 0x0009F0D6
	// (set) Token: 0x060024EC RID: 9452 RVA: 0x000A0EDE File Offset: 0x0009F0DE
	public bool IsSuspended { get; set; }

	// Token: 0x04001F32 RID: 7986
	public float Speed;

	// Token: 0x04001F33 RID: 7987
	public Vector3 Direction = Vector3.zero;

	// Token: 0x04001F34 RID: 7988
	public float Gravity;

	// Token: 0x04001F35 RID: 7989
	public GameObject Projectile;

	// Token: 0x04001F36 RID: 7990
	public List<Collider> CollidersToIgnore;

	// Token: 0x04001F37 RID: 7991
	public GameObject Owner;

	// Token: 0x04001F38 RID: 7992
	public bool WaitForProjectileToBeDestroyed;

	// Token: 0x04001F39 RID: 7993
	public AudioClip SpawnSound;

	// Token: 0x04001F3A RID: 7994
	public float SpawnSoundVolume = 0.3f;

	// Token: 0x04001F3B RID: 7995
	protected TimedTrigger m_timedTrigger;

	// Token: 0x04001F3C RID: 7996
	private GameObject m_lastProjectile;

	// Token: 0x04001F3D RID: 7997
	private Transform m_transform;
}
