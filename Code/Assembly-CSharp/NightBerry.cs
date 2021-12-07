using System;
using Core;
using Game;
using Sein.World;
using UnityEngine;

// Token: 0x0200028C RID: 652
public class NightBerry : SaveSerialize, ISuspendable
{
	// Token: 0x170003C9 RID: 969
	// (get) Token: 0x06001540 RID: 5440 RVA: 0x0005E9CB File Offset: 0x0005CBCB
	public float SafeFromDamageRadius
	{
		get
		{
			return this.OuterRadius * this.m_spiritRingRadiusMultiplier;
		}
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x0005E9DA File Offset: 0x0005CBDA
	public void SetRespawnPosition(Vector3 position)
	{
		this.m_carryable.RespawnPosition = position;
	}

	// Token: 0x170003CA RID: 970
	// (get) Token: 0x06001542 RID: 5442 RVA: 0x0005E9E8 File Offset: 0x0005CBE8
	// (set) Token: 0x06001543 RID: 5443 RVA: 0x0005E9F5 File Offset: 0x0005CBF5
	public Vector3 Position
	{
		get
		{
			return this.m_transform.position;
		}
		set
		{
			this.m_transform.position = value;
		}
	}

	// Token: 0x170003CB RID: 971
	// (get) Token: 0x06001544 RID: 5444 RVA: 0x0005EA03 File Offset: 0x0005CC03
	public bool IsCarried
	{
		get
		{
			return this.m_carryable.IsCarried;
		}
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x0005EA10 File Offset: 0x0005CC10
	public void SetToDropMode()
	{
		this.m_carryable.SetToDropMode();
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x0005EA20 File Offset: 0x0005CC20
	public new void Awake()
	{
		SuspensionManager.Register(this);
		this.m_carryable = base.GetComponent<CarryableRigidBody>();
		this.m_carryable.OnDropEvent += this.OnReleaseNightberry;
		if (Items.NightBerry)
		{
			InstantiateUtility.Destroy(base.gameObject);
			return;
		}
		Items.NightBerry = this;
		this.m_transform = base.transform;
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.m_collider = base.GetComponent<Collider>();
		Game.Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x0005EAB6 File Offset: 0x0005CCB6
	public void OnGameReset()
	{
		InstantiateUtility.Destroy(base.gameObject);
		Items.NightBerry = null;
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x0005EAC9 File Offset: 0x0005CCC9
	public new void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		this.m_carryable.OnDropEvent -= this.OnReleaseNightberry;
		Game.Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x0005EB04 File Offset: 0x0005CD04
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_isChasing);
		ar.Serialize(ref this.m_canChase);
		ar.Serialize(ref this.m_spiritRingRadiusMultiplier);
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x0005EB38 File Offset: 0x0005CD38
	public void OnReleaseNightberry()
	{
		Characters.Sein.PlatformBehaviour.Gravity.BaseSettings.GravityAngle = 0f;
		Characters.Sein.Controller.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x0005EB8C File Offset: 0x0005CD8C
	public void FixedUpdate()
	{
		if (Characters.Sein == null)
		{
			return;
		}
		if (this.IsSuspended)
		{
			return;
		}
		if (!this.IsCarried && !Sein.World.Events.GravityActivated)
		{
			Vector3 vector = Characters.Ori.Position - base.transform.position + Vector3.up;
			if (Mathf.Abs(vector.x) < 1f && Mathf.Abs(vector.y) < this.ActivateChaseRange)
			{
				this.m_canChase = true;
			}
			if (vector.magnitude > this.StartChaseRange && this.m_canChase)
			{
				if (this.OnChaseSound && !this.m_isChasing)
				{
					Sound.Play(this.OnChaseSound.GetSound(null), base.transform.position, null);
				}
				this.m_isChasing = true;
			}
			if (vector.magnitude < this.StopChaseRange && this.m_rigidbody.velocity.magnitude < this.StopChaseVelocity && base.transform.position.y > Characters.Sein.Position.y)
			{
				this.m_isChasing = false;
			}
			if (this.m_isChasing && !this.m_rigidbody.isKinematic)
			{
				this.m_collider.enabled = false;
				this.m_rigidbody.useGravity = false;
				this.m_rigidbody.velocity += vector.normalized * this.ForceOverDistance.Evaluate(vector.magnitude) * Time.deltaTime;
			}
			else
			{
				this.m_collider.enabled = true;
				this.m_rigidbody.useGravity = true;
			}
			if (!this.m_rigidbody.isKinematic)
			{
				float num = this.DragOverDistance.Evaluate(vector.magnitude);
				this.m_rigidbody.velocity *= 1f - num;
				this.m_rigidbody.WakeUp();
			}
		}
		if (this.m_carryable.IsCarried)
		{
			this.GrowSpiritRing();
		}
		else
		{
			this.ShrinkSpiritRing();
		}
		if (this.m_spiritRingSpeed <= 0f && this.GrowSound)
		{
			this.GrowSound.Stop();
		}
		if (this.m_spiritRingSpeed >= 0f && this.ShrinkSound)
		{
			this.ShrinkSound.Stop();
		}
		this.m_spiritRingRadiusMultiplier = Mathf.Clamp01(this.m_spiritRingRadiusMultiplier + this.m_spiritRingSpeed * Time.deltaTime);
		float num2 = this.SpiritRingRadius * this.m_spiritRingRadiusMultiplier;
		this.Ring.localScale = new Vector3(num2, num2, 1f);
		foreach (ParticleSystem particleSystem in this.Particles)
		{
			particleSystem.enableEmission = (this.m_spiritRingRadiusMultiplier > 0.9f);
		}
		Shader.SetGlobalVector("_NightberryPosition", new Vector4(this.m_transform.position.x, this.m_transform.position.y, this.InnerRadius * this.m_spiritRingRadiusMultiplier, this.OuterRadius * this.m_spiritRingRadiusMultiplier));
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x0005EF04 File Offset: 0x0005D104
	public void ShrinkSpiritRing()
	{
		if (this.ShrinkSound && this.m_spiritRingSpeed >= 0f)
		{
			this.ShrinkSound.Play();
		}
		this.m_spiritRingSpeed = -0.33333334f;
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x0005EF48 File Offset: 0x0005D148
	public void GrowSpiritRing()
	{
		if (this.GrowSound && this.m_spiritRingSpeed <= 0f)
		{
			this.GrowSound.Play();
		}
		this.m_spiritRingSpeed = 1f;
	}

	// Token: 0x170003CC RID: 972
	// (get) Token: 0x0600154E RID: 5454 RVA: 0x0005EF8B File Offset: 0x0005D18B
	// (set) Token: 0x0600154F RID: 5455 RVA: 0x0005EF93 File Offset: 0x0005D193
	public bool IsSuspended { get; set; }

	// Token: 0x0400126A RID: 4714
	public float OuterRadius;

	// Token: 0x0400126B RID: 4715
	public float InnerRadius;

	// Token: 0x0400126C RID: 4716
	public SoundSource ShrinkSound;

	// Token: 0x0400126D RID: 4717
	public SoundSource GrowSound;

	// Token: 0x0400126E RID: 4718
	public AnimationCurve ForceOverDistance;

	// Token: 0x0400126F RID: 4719
	public AnimationCurve DragOverDistance;

	// Token: 0x04001270 RID: 4720
	public float ActivateChaseRange = 3f;

	// Token: 0x04001271 RID: 4721
	public float StopChaseRange = 2f;

	// Token: 0x04001272 RID: 4722
	public float StartChaseRange = 5f;

	// Token: 0x04001273 RID: 4723
	public float StopChaseVelocity = 1f;

	// Token: 0x04001274 RID: 4724
	public Varying2DSoundProvider OnChaseSound;

	// Token: 0x04001275 RID: 4725
	public Transform Ring;

	// Token: 0x04001276 RID: 4726
	private bool m_isChasing;

	// Token: 0x04001277 RID: 4727
	private bool m_canChase;

	// Token: 0x04001278 RID: 4728
	private Transform m_transform;

	// Token: 0x04001279 RID: 4729
	private Rigidbody m_rigidbody;

	// Token: 0x0400127A RID: 4730
	private Collider m_collider;

	// Token: 0x0400127B RID: 4731
	private CarryableRigidBody m_carryable;

	// Token: 0x0400127C RID: 4732
	private float m_spiritRingSpeed;

	// Token: 0x0400127D RID: 4733
	public float SpiritRingRadius;

	// Token: 0x0400127E RID: 4734
	private float m_spiritRingRadiusMultiplier;

	// Token: 0x0400127F RID: 4735
	public ParticleSystem[] Particles;
}
