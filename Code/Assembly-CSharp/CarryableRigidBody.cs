using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200035D RID: 861
public class CarryableRigidBody : SaveSerialize, IDamageReciever, ICarryable
{
	// Token: 0x06001883 RID: 6275 RVA: 0x000691A4 File Offset: 0x000673A4
	// Note: this type is marked as 'beforefieldinit'.
	static CarryableRigidBody()
	{
		CarryableRigidBody.OnAnyCarryablePickedUpAction = delegate()
		{
		};
	}

	// Token: 0x1400002B RID: 43
	// (add) Token: 0x06001884 RID: 6276 RVA: 0x000691D3 File Offset: 0x000673D3
	// (remove) Token: 0x06001885 RID: 6277 RVA: 0x000691EA File Offset: 0x000673EA
	public static event Action OnAnyCarryablePickedUpAction;

	// Token: 0x1400002C RID: 44
	// (add) Token: 0x06001886 RID: 6278 RVA: 0x00069201 File Offset: 0x00067401
	// (remove) Token: 0x06001887 RID: 6279 RVA: 0x0006921A File Offset: 0x0006741A
	public event Action OnDropEvent = delegate()
	{
	};

	// Token: 0x06001888 RID: 6280 RVA: 0x00069233 File Offset: 0x00067433
	public void Start()
	{
		this.RespawnPosition = this.m_transform.position;
	}

	// Token: 0x1700044F RID: 1103
	// (get) Token: 0x06001889 RID: 6281 RVA: 0x00069246 File Offset: 0x00067446
	// (set) Token: 0x0600188A RID: 6282 RVA: 0x00069253 File Offset: 0x00067453
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

	// Token: 0x0600188B RID: 6283 RVA: 0x00069261 File Offset: 0x00067461
	public new void Awake()
	{
		base.Awake();
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.m_transform = base.transform;
	}

	// Token: 0x0600188C RID: 6284 RVA: 0x00069281 File Offset: 0x00067481
	public void OnEnable()
	{
		Items.Carryables.Add(this);
	}

	// Token: 0x0600188D RID: 6285 RVA: 0x0006928E File Offset: 0x0006748E
	public void OnDisable()
	{
		Items.Carryables.Remove(this);
	}

	// Token: 0x0600188E RID: 6286 RVA: 0x0006929C File Offset: 0x0006749C
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			bool flag = ar.Serialize(true);
			if (flag)
			{
				this.SetToCarryMode();
			}
			else
			{
				this.SetToDropMode();
			}
			base.gameObject.SetActive(true);
		}
		else
		{
			ar.Serialize(this.m_isCarried);
		}
		this.m_transform.localPosition = ar.Serialize(this.m_transform.localPosition);
		ar.Serialize(ref this.RespawnPosition);
	}

	// Token: 0x0600188F RID: 6287 RVA: 0x0006931C File Offset: 0x0006751C
	public bool CanBeCarried()
	{
		if (this.CanBeCarriedCondition && !this.CanBeCarriedCondition.Validate(null))
		{
			return false;
		}
		if (this.m_timeNotToShowMessage <= 0f && this.PickupMessage && (!UI.Hints.IsShowingHint || this.m_message))
		{
			if (this.m_message == null)
			{
				this.m_message = UI.Hints.Show(this.PickupMessage, HintLayer.Gameplay, 1f);
			}
			else
			{
				this.m_message.Visibility.ResetWaitDuration();
			}
		}
		return true;
	}

	// Token: 0x17000450 RID: 1104
	// (get) Token: 0x06001890 RID: 6288 RVA: 0x000693C4 File Offset: 0x000675C4
	public bool IsCarried
	{
		get
		{
			return this.m_isCarried;
		}
	}

	// Token: 0x06001891 RID: 6289 RVA: 0x000693CC File Offset: 0x000675CC
	public void Pickup()
	{
		if (this.OnPickupAction)
		{
			this.OnPickupAction.Perform(null);
		}
		CarryableRigidBody.OnAnyCarryablePickedUpAction();
		this.SetToCarryMode();
		if (this.CarryingSound)
		{
			this.CarryingSound.Play();
		}
		if (this.NotCarryingSound)
		{
			this.NotCarryingSound.StopAndFadeOut(0.2f);
		}
		if (this.OnPickUpSoundProvider)
		{
			Sound.Play(this.OnPickUpSoundProvider.GetSound(null), base.transform.position, null);
		}
		Characters.Sein.Abilities.Carry.OnPickup(this);
	}

	// Token: 0x06001892 RID: 6290 RVA: 0x00069484 File Offset: 0x00067684
	public void SetToCarryMode()
	{
		Characters.Sein.Abilities.Carry.OnSetToCarryMode(this);
		this.m_rigidbody.isKinematic = true;
		base.GetComponent<Collider>().isTrigger = true;
		this.m_transform.parent = Characters.Sein.PlatformBehaviour.Visuals.SpriteMirror.transform;
		this.m_transform.localPosition = Vector3.zero;
		this.m_isCarried = true;
		if (this.m_animationMetaDataDrivenTransform == null)
		{
			this.m_animationMetaDataDrivenTransform = base.gameObject.AddComponent<AnimationMetaDataDrivenTransform>();
		}
		this.m_animationMetaDataDrivenTransform.enabled = true;
		this.m_animationMetaDataDrivenTransform.SpriteAnimatorWithTransitions = Characters.Sein.PlatformBehaviour.Visuals.Sprite.GetComponent<SpriteAnimatorWithTransitions>();
		this.m_animationMetaDataDrivenTransform.ShouldFollowCameraPlane = false;
		this.m_animationMetaDataDrivenTransform.NodeName = "#nightBerry";
	}

	// Token: 0x06001893 RID: 6291 RVA: 0x00069568 File Offset: 0x00067768
	public void SetToDropMode()
	{
		if (!this.m_isCarried)
		{
			return;
		}
		Characters.Sein.Abilities.Carry.OnSetToDropMode();
		this.m_rigidbody.isKinematic = false;
		this.m_transform.parent = null;
		base.GetComponent<Collider>().isTrigger = false;
		this.m_isCarried = false;
		this.m_timeNotToShowMessage = 1f;
		if (this.m_animationMetaDataDrivenTransform)
		{
			this.m_animationMetaDataDrivenTransform.enabled = false;
		}
	}

	// Token: 0x06001894 RID: 6292 RVA: 0x000695E8 File Offset: 0x000677E8
	public void Drop()
	{
		if (this.OnDropAction)
		{
			this.OnDropAction.Perform(null);
		}
		this.SetToDropMode();
		this.OnDropEvent();
		if (Characters.Sein.PlatformBehaviour.PlatformMovement.IsOnGround)
		{
			if (this.OnDropSoundProvider && this.m_transform)
			{
				Sound.Play(this.OnDropSoundProvider.GetSound(null), this.m_transform.position, null);
			}
		}
		else if (this.OnPutDownSoundProvider && this.m_transform)
		{
			Sound.Play(this.OnPutDownSoundProvider.GetSound(null), this.m_transform.position, null);
		}
		if (this.CarryingSound)
		{
			this.CarryingSound.StopAndFadeOut(0.2f);
		}
		if (this.NotCarryingSound)
		{
			this.NotCarryingSound.Play();
		}
		Characters.Sein.Abilities.Carry.OnDrop();
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x0006970C File Offset: 0x0006790C
	public void ExplodeAndRespawn()
	{
		if (this.DeathEffect)
		{
			InstantiateUtility.Instantiate(this.DeathEffect, this.Position, Quaternion.identity);
		}
		this.Position = this.RespawnPosition;
		if (this.RespawnEffect)
		{
			InstantiateUtility.Instantiate(this.RespawnEffect, this.Position, Quaternion.identity);
		}
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x00069774 File Offset: 0x00067974
	public void FixedUpdate()
	{
		if (this.m_timeNotToShowMessage >= 0f)
		{
			this.m_timeNotToShowMessage -= Time.fixedDeltaTime;
		}
		if (this.m_rigidbody)
		{
			if (this.FreezeWhenOffscreen && !this.m_rigidbody.isKinematic && !UI.Cameras.Current.IsOnScreenPadded(this.m_transform.position, 5f))
			{
				this.m_rigidbody.isKinematic = true;
			}
			if (this.m_rigidbody.isKinematic && !this.m_isCarried && UI.Cameras.Current.CameraBoundingBox.Contains(this.m_transform.position))
			{
				this.m_rigidbody.isKinematic = false;
			}
			if (this.m_rigidbody.isKinematic)
			{
				this.m_rigidbody.WakeUp();
			}
		}
		this.m_transform.rotation = Quaternion.identity;
	}

	// Token: 0x06001897 RID: 6295 RVA: 0x00069870 File Offset: 0x00067A70
	public void OnCollisionEnter(Collision collision)
	{
		this.m_rigidbody.velocity *= 1f - this.DragOnCollision;
		if (this.OnHitGroundSoundProvider && this.m_timeLastSoundPlayed + 0.1f < Time.time)
		{
			Sound.Play(this.OnHitGroundSoundProvider.GetSound(null), this.m_transform.position, null);
		}
		this.m_timeLastSoundPlayed = Time.time;
		if (this.OnHitGroundAction)
		{
			this.OnHitGroundAction.Perform(null);
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001898 RID: 6296 RVA: 0x00069915 File Offset: 0x00067B15
	public void OnCollisionStay(Collision collision)
	{
		this.m_rigidbody.velocity *= 1f - this.DragOnCollision;
		this.m_timeLastSoundPlayed = Time.time;
	}

	// Token: 0x06001899 RID: 6297 RVA: 0x00069944 File Offset: 0x00067B44
	public void OnRecieveDamage(Damage damage)
	{
		if (this.DestroyOnDamage)
		{
			this.m_rigidbody.velocity *= 1f - this.DragOnCollision;
			if (this.OnHitGroundSoundProvider)
			{
				SoundDescriptor sound = this.OnHitGroundSoundProvider.GetSound(null);
				if (sound != null)
				{
					Sound.Play(sound, this.m_transform.position, null);
				}
			}
			if (this.OnHitGroundAction)
			{
				this.OnHitGroundAction.Perform(null);
				InstantiateUtility.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x0400150E RID: 5390
	public Varying2DSoundProvider OnPickUpSoundProvider;

	// Token: 0x0400150F RID: 5391
	public Varying2DSoundProvider OnPutDownSoundProvider;

	// Token: 0x04001510 RID: 5392
	public Varying2DSoundProvider OnDropSoundProvider;

	// Token: 0x04001511 RID: 5393
	public Varying2DSoundProvider OnHitGroundSoundProvider;

	// Token: 0x04001512 RID: 5394
	public ActionMethod OnPickupAction;

	// Token: 0x04001513 RID: 5395
	public ActionMethod OnDropAction;

	// Token: 0x04001514 RID: 5396
	public ActionMethod OnHitGroundAction;

	// Token: 0x04001515 RID: 5397
	public SoundSource CarryingSound;

	// Token: 0x04001516 RID: 5398
	public SoundSource NotCarryingSound;

	// Token: 0x04001517 RID: 5399
	public Vector3 RespawnPosition;

	// Token: 0x04001518 RID: 5400
	public float DragOnCollision;

	// Token: 0x04001519 RID: 5401
	public GameObject DeathEffect;

	// Token: 0x0400151A RID: 5402
	public GameObject RespawnEffect;

	// Token: 0x0400151B RID: 5403
	public MessageProvider PickupMessage;

	// Token: 0x0400151C RID: 5404
	private Rigidbody m_rigidbody;

	// Token: 0x0400151D RID: 5405
	private Transform m_transform;

	// Token: 0x0400151E RID: 5406
	private AnimationMetaDataDrivenTransform m_animationMetaDataDrivenTransform;

	// Token: 0x0400151F RID: 5407
	private MessageBox m_message;

	// Token: 0x04001520 RID: 5408
	private float m_timeNotToShowMessage;

	// Token: 0x04001521 RID: 5409
	private bool m_isCarried;

	// Token: 0x04001522 RID: 5410
	public Condition CanBeCarriedCondition;

	// Token: 0x04001523 RID: 5411
	public bool FreezeWhenOffscreen = true;

	// Token: 0x04001524 RID: 5412
	private float m_timeLastSoundPlayed;

	// Token: 0x04001525 RID: 5413
	public bool DestroyOnDamage = true;
}
