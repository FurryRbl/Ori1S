using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000380 RID: 896
public class StompPost : SaveSerialize, IDamageReciever, IAttackable, IStompAttackable, ISuspendable, IDynamicGraphicHierarchy
{
	// Token: 0x06001979 RID: 6521 RVA: 0x0006DB13 File Offset: 0x0006BD13
	public new void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
		this.m_transform = base.transform;
	}

	// Token: 0x0600197A RID: 6522 RVA: 0x0006DB2D File Offset: 0x0006BD2D
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600197B RID: 6523 RVA: 0x0006DB3B File Offset: 0x0006BD3B
	public void Start()
	{
		this.m_distanceStompedIntoGround = 0f;
		this.m_startLocalPosition = base.transform.localPosition;
	}

	// Token: 0x0600197C RID: 6524 RVA: 0x0006DB5C File Offset: 0x0006BD5C
	public void OnRecieveDamage(Damage damage)
	{
		if (damage.Type == DamageType.Stomp && Vector3.Dot(base.transform.rotation * Vector3.down, Characters.Sein.PlatformBehaviour.PlatformMovement.GravityDirection) > Mathf.Cos(0.17453292f) && !this.m_activated)
		{
			this.m_distanceStompedIntoGround = Mathf.Min(this.StompIntoGroundAmount, this.m_distanceStompedIntoGround + this.StompIntoGroundAmount / (float)this.NumberOfStomps);
			this.m_remainingRiseDelayTime = this.RisingDelay;
			if (Mathf.Approximately(this.m_distanceStompedIntoGround, this.StompIntoGroundAmount))
			{
				this.m_activated = true;
				if (this.AllTheWayInAction)
				{
					this.AllTheWayInAction.Perform(null);
				}
				if (this.AllTheWayInSound)
				{
					Sound.Play(this.AllTheWayInSound.GetSound(null), this.m_transform.position, null);
				}
			}
			else if (this.StompSound)
			{
				Sound.Play(this.StompSound.GetSound(null), this.m_transform.position, null);
			}
		}
	}

	// Token: 0x0600197D RID: 6525 RVA: 0x0006DC8C File Offset: 0x0006BE8C
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.m_remainingRiseDelayTime > 0f)
		{
			this.m_remainingRiseDelayTime -= Time.deltaTime;
			if (this.m_remainingRiseDelayTime < 0f)
			{
				this.m_remainingRiseDelayTime = 0f;
			}
		}
		if (!this.m_activated && this.m_remainingRiseDelayTime < 0f)
		{
			this.m_distanceStompedIntoGround -= Time.deltaTime * this.RiseSpeed;
			if (this.m_distanceStompedIntoGround < 0f)
			{
				this.m_distanceStompedIntoGround = 0f;
			}
		}
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.m_startLocalPosition + Vector3.down * this.m_distanceStompedIntoGround, 0.3f);
	}

	// Token: 0x0600197E RID: 6526 RVA: 0x0006DD6C File Offset: 0x0006BF6C
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_activated);
		ar.Serialize(ref this.m_distanceStompedIntoGround);
		ar.Serialize(ref this.m_remainingRiseDelayTime);
	}

	// Token: 0x1700045C RID: 1116
	// (get) Token: 0x0600197F RID: 6527 RVA: 0x0006DD9D File Offset: 0x0006BF9D
	// (set) Token: 0x06001980 RID: 6528 RVA: 0x0006DDA5 File Offset: 0x0006BFA5
	public bool IsSuspended { get; set; }

	// Token: 0x1700045D RID: 1117
	// (get) Token: 0x06001981 RID: 6529 RVA: 0x0006DDAE File Offset: 0x0006BFAE
	public Vector3 Position
	{
		get
		{
			return this.m_transform.position;
		}
	}

	// Token: 0x06001982 RID: 6530 RVA: 0x0006DDBB File Offset: 0x0006BFBB
	public bool CanBeChargeFlamed()
	{
		return false;
	}

	// Token: 0x06001983 RID: 6531 RVA: 0x0006DDBE File Offset: 0x0006BFBE
	public bool CanBeChargeDashed()
	{
		return false;
	}

	// Token: 0x06001984 RID: 6532 RVA: 0x0006DDC1 File Offset: 0x0006BFC1
	public bool CanBeGrenaded()
	{
		return false;
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x0006DDC4 File Offset: 0x0006BFC4
	public bool CanBeStomped()
	{
		return true;
	}

	// Token: 0x06001986 RID: 6534 RVA: 0x0006DDC7 File Offset: 0x0006BFC7
	public bool CanBeBashed()
	{
		return false;
	}

	// Token: 0x06001987 RID: 6535 RVA: 0x0006DDCA File Offset: 0x0006BFCA
	public bool CanBeSpiritFlamed()
	{
		return false;
	}

	// Token: 0x06001988 RID: 6536 RVA: 0x0006DDCD File Offset: 0x0006BFCD
	public bool IsStompBouncable()
	{
		return false;
	}

	// Token: 0x06001989 RID: 6537 RVA: 0x0006DDD0 File Offset: 0x0006BFD0
	public bool CanBeLevelUpBlasted()
	{
		return false;
	}

	// Token: 0x0600198A RID: 6538 RVA: 0x0006DDD3 File Offset: 0x0006BFD3
	public bool CountsTowardsSuperJumpAchievement()
	{
		return false;
	}

	// Token: 0x0600198B RID: 6539 RVA: 0x0006DDD6 File Offset: 0x0006BFD6
	public bool IsDead()
	{
		return false;
	}

	// Token: 0x040015E6 RID: 5606
	public int NumberOfStomps = 3;

	// Token: 0x040015E7 RID: 5607
	public float StompIntoGroundAmount = 0.1f;

	// Token: 0x040015E8 RID: 5608
	public float RisingDelay = 8f;

	// Token: 0x040015E9 RID: 5609
	public float RiseSpeed = 1f;

	// Token: 0x040015EA RID: 5610
	public SoundProvider StompSound;

	// Token: 0x040015EB RID: 5611
	public SoundProvider AllTheWayInSound;

	// Token: 0x040015EC RID: 5612
	public ActionMethod AllTheWayInAction;

	// Token: 0x040015ED RID: 5613
	private Vector3 m_startLocalPosition;

	// Token: 0x040015EE RID: 5614
	private Transform m_transform;

	// Token: 0x040015EF RID: 5615
	private float m_distanceStompedIntoGround;

	// Token: 0x040015F0 RID: 5616
	private float m_remainingRiseDelayTime;

	// Token: 0x040015F1 RID: 5617
	private bool m_activated;
}
