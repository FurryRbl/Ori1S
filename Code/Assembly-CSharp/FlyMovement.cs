using System;
using UnityEngine;

// Token: 0x020004F6 RID: 1270
public class FlyMovement : SaveSerialize, IDamageReciever, ISuspendable
{
	// Token: 0x170005E5 RID: 1509
	// (get) Token: 0x06002241 RID: 8769 RVA: 0x0009663B File Offset: 0x0009483B
	// (set) Token: 0x06002242 RID: 8770 RVA: 0x00096648 File Offset: 0x00094848
	public float Speed
	{
		get
		{
			return this.Velocity.magnitude;
		}
		set
		{
			this.Velocity = this.Velocity.normalized * this.Speed;
		}
	}

	// Token: 0x170005E6 RID: 1510
	// (get) Token: 0x06002243 RID: 8771 RVA: 0x00096666 File Offset: 0x00094866
	// (set) Token: 0x06002244 RID: 8772 RVA: 0x00096673 File Offset: 0x00094873
	public float Angle
	{
		get
		{
			return MoonMath.Angle.AngleFromVector(this.Velocity);
		}
		set
		{
			this.Velocity = this.Velocity.magnitude * MoonMath.Angle.VectorFromAngle(value);
		}
	}

	// Token: 0x170005E7 RID: 1511
	// (get) Token: 0x06002245 RID: 8773 RVA: 0x00096691 File Offset: 0x00094891
	// (set) Token: 0x06002246 RID: 8774 RVA: 0x000966A3 File Offset: 0x000948A3
	public Vector2 VelocityAsDelta
	{
		get
		{
			return this.Velocity * Time.deltaTime;
		}
		set
		{
			this.Velocity = ((Time.deltaTime != 0f) ? (value / Time.deltaTime) : Vector2.zero);
		}
	}

	// Token: 0x170005E8 RID: 1512
	// (get) Token: 0x06002247 RID: 8775 RVA: 0x000966CF File Offset: 0x000948CF
	public Rigidbody Rigidbody
	{
		get
		{
			return this.m_rigidbody;
		}
	}

	// Token: 0x06002248 RID: 8776 RVA: 0x000966D7 File Offset: 0x000948D7
	public override void Awake()
	{
		base.Awake();
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		SuspensionManager.Register(this);
	}

	// Token: 0x06002249 RID: 8777 RVA: 0x000966F1 File Offset: 0x000948F1
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600224A RID: 8778 RVA: 0x000966FF File Offset: 0x000948FF
	public void Start()
	{
	}

	// Token: 0x0600224B RID: 8779 RVA: 0x00096704 File Offset: 0x00094904
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			this.m_rigidbody.velocity = Vector3.zero;
		}
		else
		{
			this.Kickback.AdvanceTime();
			this.m_rigidbody.velocity = this.Velocity + ((!this.HasKickback) ? Vector2.zero : this.Kickback.KickbackVector);
		}
	}

	// Token: 0x0600224C RID: 8780 RVA: 0x00096778 File Offset: 0x00094978
	public void OnRecieveDamage(Damage damage)
	{
		if (this.HasKickback)
		{
			this.Kickback.ApplyKickback(damage.Force.magnitude, damage.Force);
		}
	}

	// Token: 0x170005E9 RID: 1513
	// (get) Token: 0x0600224D RID: 8781 RVA: 0x000967AF File Offset: 0x000949AF
	// (set) Token: 0x0600224E RID: 8782 RVA: 0x000967BC File Offset: 0x000949BC
	public float VelocityX
	{
		get
		{
			return this.Velocity.x;
		}
		set
		{
			Vector2 velocity = this.Velocity;
			velocity.x = value;
			this.Velocity = velocity;
		}
	}

	// Token: 0x170005EA RID: 1514
	// (get) Token: 0x0600224F RID: 8783 RVA: 0x000967DF File Offset: 0x000949DF
	// (set) Token: 0x06002250 RID: 8784 RVA: 0x000967EC File Offset: 0x000949EC
	public float VelocityY
	{
		get
		{
			return this.Velocity.y;
		}
		set
		{
			Vector2 velocity = this.Velocity;
			velocity.y = value;
			this.Velocity = velocity;
		}
	}

	// Token: 0x06002251 RID: 8785 RVA: 0x00096810 File Offset: 0x00094A10
	public override void Serialize(Archive ar)
	{
		this.Velocity = ar.Serialize(this.Velocity);
		this.m_rigidbody.velocity = ar.Serialize(this.m_rigidbody.velocity);
		base.transform.position = ar.Serialize(base.transform.position);
	}

	// Token: 0x170005EB RID: 1515
	// (get) Token: 0x06002252 RID: 8786 RVA: 0x00096867 File Offset: 0x00094A67
	// (set) Token: 0x06002253 RID: 8787 RVA: 0x0009686F File Offset: 0x00094A6F
	public bool IsSuspended { get; set; }

	// Token: 0x04001CC3 RID: 7363
	public Kickback Kickback;

	// Token: 0x04001CC4 RID: 7364
	public bool HasKickback = true;

	// Token: 0x04001CC5 RID: 7365
	public Vector2 Velocity;

	// Token: 0x04001CC6 RID: 7366
	private Rigidbody m_rigidbody;
}
