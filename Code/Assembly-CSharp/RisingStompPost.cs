using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class RisingStompPost : SaveSerialize, IDamageReciever, IAttackable, IStompAttackable, ISuspendable
{
	// Token: 0x06000C7C RID: 3196 RVA: 0x00038F33 File Offset: 0x00037133
	public new void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
		this._transform = base.transform;
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x00038F4D File Offset: 0x0003714D
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x00038F5C File Offset: 0x0003715C
	public void Start()
	{
		this._distanceStompedIntoGround = 0f;
		this._startLocalPosition = base.transform.localPosition;
		this.ConstantAction.Perform(null);
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x00038F94 File Offset: 0x00037194
	public void OnRecieveDamage(Damage damage)
	{
		if (damage.Type == DamageType.Stomp)
		{
			if (this.TwinPost != null)
			{
				this.TwinPost.OnTwinRecievedDamage(damage);
			}
			if (Vector3.Dot(base.transform.rotation * Vector3.down, Characters.Sein.PlatformBehaviour.PlatformMovement.GravityDirection) > Mathf.Cos(0.17453292f))
			{
				this._beingStomped = true;
				this._distanceStompedIntoGround = Mathf.Min(this.StompIntoGroundAmount, this._distanceStompedIntoGround + this.StompIntoGroundAmount / (float)this.MaxNumberOfStomps);
				this.StompedAction.Perform(null);
				if (this.StompSound)
				{
					Sound.Play(this.StompSound.GetSound(null), this._transform.position, null);
				}
			}
		}
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x00039070 File Offset: 0x00037270
	public void OnTwinRecievedDamage(Damage damage)
	{
		if (Vector3.Dot(base.transform.rotation * Vector3.down, Characters.Sein.PlatformBehaviour.PlatformMovement.GravityDirection) > Mathf.Cos(0.17453292f))
		{
			this._beingStomped = true;
			this._distanceStompedIntoGround = Mathf.Min(this.StompIntoGroundAmount, this._distanceStompedIntoGround + this.StompIntoGroundAmount / (float)this.MaxNumberOfStomps);
		}
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x000390E8 File Offset: 0x000372E8
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this._beingStomped)
		{
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this._startLocalPosition + Vector3.down * this._distanceStompedIntoGround, this.SpeedIntoGround);
			if (Math.Abs(this._startLocalPosition.y - base.transform.localPosition.y - this._distanceStompedIntoGround) < 0.001f)
			{
				this._beingStomped = false;
				this.ConstantAction.Perform(null);
			}
		}
		else
		{
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this._startLocalPosition, Time.deltaTime * this.RiseSpeed);
			this._distanceStompedIntoGround = this._startLocalPosition.y - base.transform.localPosition.y;
		}
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x000391E7 File Offset: 0x000373E7
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this._distanceStompedIntoGround);
		ar.Serialize(ref this._beingStomped);
	}

	// Token: 0x1700026E RID: 622
	// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00039201 File Offset: 0x00037401
	// (set) Token: 0x06000C84 RID: 3204 RVA: 0x00039209 File Offset: 0x00037409
	public bool IsSuspended { get; set; }

	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00039212 File Offset: 0x00037412
	public Vector3 Position
	{
		get
		{
			return this._transform.position;
		}
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x0003921F File Offset: 0x0003741F
	public bool CanBeChargeFlamed()
	{
		return false;
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x00039222 File Offset: 0x00037422
	public bool CanBeChargeDashed()
	{
		return false;
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x00039225 File Offset: 0x00037425
	public bool CanBeGrenaded()
	{
		return false;
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x00039228 File Offset: 0x00037428
	public bool CanBeStomped()
	{
		return true;
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x0003922B File Offset: 0x0003742B
	public bool CanBeBashed()
	{
		return false;
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x0003922E File Offset: 0x0003742E
	public bool CanBeSpiritFlamed()
	{
		return false;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x00039231 File Offset: 0x00037431
	public bool IsStompBouncable()
	{
		return false;
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x00039234 File Offset: 0x00037434
	public bool CanBeLevelUpBlasted()
	{
		return false;
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00039237 File Offset: 0x00037437
	public bool CountsTowardsSuperJumpAchievement()
	{
		return false;
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x0003923A File Offset: 0x0003743A
	public bool IsDead()
	{
		return false;
	}

	// Token: 0x04000A55 RID: 2645
	public int MaxNumberOfStomps = 3;

	// Token: 0x04000A56 RID: 2646
	public float StompIntoGroundAmount = 3f;

	// Token: 0x04000A57 RID: 2647
	public float RiseSpeed = 0.1f;

	// Token: 0x04000A58 RID: 2648
	public float SpeedIntoGround = 0.3f;

	// Token: 0x04000A59 RID: 2649
	public RisingStompPost TwinPost;

	// Token: 0x04000A5A RID: 2650
	public SoundProvider StompSound;

	// Token: 0x04000A5B RID: 2651
	public ActionMethod ConstantAction;

	// Token: 0x04000A5C RID: 2652
	public ActionMethod StompedAction;

	// Token: 0x04000A5D RID: 2653
	private Vector3 _startLocalPosition;

	// Token: 0x04000A5E RID: 2654
	private Transform _transform;

	// Token: 0x04000A5F RID: 2655
	private float _distanceStompedIntoGround;

	// Token: 0x04000A60 RID: 2656
	private bool _beingStomped;
}
