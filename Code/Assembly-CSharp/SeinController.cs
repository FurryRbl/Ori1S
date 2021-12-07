using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200007D RID: 125
public class SeinController : SaveSerialize, IDamageReciever, ISeinReceiver, ISuspendable, ICanActivateStompers
{
	// Token: 0x14000014 RID: 20
	// (add) Token: 0x06000537 RID: 1335 RVA: 0x000148D5 File Offset: 0x00012AD5
	// (remove) Token: 0x06000538 RID: 1336 RVA: 0x000148EE File Offset: 0x00012AEE
	public event Action OnTriggeredAnimationFinished = delegate()
	{
	};

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x06000539 RID: 1337 RVA: 0x00014908 File Offset: 0x00012B08
	public bool InputLocked
	{
		get
		{
			return (this.Sein.Abilities.Lever && this.Sein.Abilities.Lever.InputLocked) || GameController.Instance.LockInput || GameController.Instance.LockInputByAction;
		}
	}

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x0600053A RID: 1338 RVA: 0x00014967 File Offset: 0x00012B67
	public bool CanMove
	{
		get
		{
			return !this.InputLocked && !this.IsPlayingAnimation;
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x0600053B RID: 1339 RVA: 0x00014980 File Offset: 0x00012B80
	// (set) Token: 0x0600053C RID: 1340 RVA: 0x0001499C File Offset: 0x00012B9C
	public bool FaceLeft
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement.SpriteMirror.FaceLeft;
		}
		set
		{
			this.Sein.PlatformBehaviour.LeftRightMovement.SpriteMirror.FaceLeft = value;
		}
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x0600053D RID: 1341 RVA: 0x000149B9 File Offset: 0x00012BB9
	public Transform Transform
	{
		get
		{
			return this.m_transform;
		}
	}

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x0600053E RID: 1342 RVA: 0x000149C4 File Offset: 0x00012BC4
	public bool IsCrouching
	{
		get
		{
			return this.Sein.Abilities.Crouch && this.Sein.Abilities.Crouch.IsCrouching;
		}
	}

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x0600053F RID: 1343 RVA: 0x00014A04 File Offset: 0x00012C04
	private bool IsGrabbingBlock
	{
		get
		{
			return this.Sein.Abilities.GrabBlock && this.Sein.Abilities.GrabBlock.IsGrabbing;
		}
	}

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x06000540 RID: 1344 RVA: 0x00014A44 File Offset: 0x00012C44
	public bool IsGrabbingWall
	{
		get
		{
			return this.Sein.Abilities.GrabWall && this.Sein.Abilities.GrabWall.IsGrabbing;
		}
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x06000541 RID: 1345 RVA: 0x00014A84 File Offset: 0x00012C84
	public bool IsGrabbingLever
	{
		get
		{
			return this.Sein.Abilities.Lever && this.Sein.Abilities.Lever.IsUsingLever;
		}
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x06000542 RID: 1346 RVA: 0x00014AC4 File Offset: 0x00012CC4
	public bool IsGliding
	{
		get
		{
			return this.Sein.Abilities.Glide && this.Sein.Abilities.Glide.IsGliding;
		}
	}

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x06000543 RID: 1347 RVA: 0x00014B04 File Offset: 0x00012D04
	public bool IsPushPulling
	{
		get
		{
			return this.Sein.Abilities.GrabBlock && this.Sein.Abilities.GrabBlock.IsGrabbing;
		}
	}

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x06000544 RID: 1348 RVA: 0x00014B44 File Offset: 0x00012D44
	public bool IsSwimming
	{
		get
		{
			return this.Sein.Abilities.Swimming && this.Sein.Abilities.Swimming.IsSwimming;
		}
	}

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x06000545 RID: 1349 RVA: 0x00014B84 File Offset: 0x00012D84
	public bool IsBashing
	{
		get
		{
			return this.Sein.Abilities.Bash && this.Sein.Abilities.Bash.IsBashing;
		}
	}

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x06000546 RID: 1350 RVA: 0x00014BC4 File Offset: 0x00012DC4
	public bool IsAimingGrenade
	{
		get
		{
			return this.Sein.Abilities.Grenade && this.Sein.Abilities.Grenade.IsAiming;
		}
	}

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x06000547 RID: 1351 RVA: 0x00014C03 File Offset: 0x00012E03
	public bool IsInsideSoulFlame
	{
		get
		{
			return this.Sein.SoulFlame.InsideCheckpointMarker;
		}
	}

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x06000548 RID: 1352 RVA: 0x00014C18 File Offset: 0x00012E18
	public bool IsCarrying
	{
		get
		{
			return this.Sein.Abilities.Carry && (this.Sein.Abilities.Carry.IsCarrying || this.Sein.Abilities.Carry.IsPickingUp);
		}
	}

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x06000549 RID: 1353 RVA: 0x00014C74 File Offset: 0x00012E74
	public bool IsStomping
	{
		get
		{
			return this.Sein.Abilities.Stomp && this.Sein.Abilities.Stomp.IsStomping;
		}
	}

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x0600054A RID: 1354 RVA: 0x00014CB3 File Offset: 0x00012EB3
	public bool IsCharging
	{
		get
		{
			return this.Sein.Abilities.ChargeFlame && this.Sein.Abilities.ChargeFlame.IsCharging;
		}
	}

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x0600054B RID: 1355 RVA: 0x00014CE6 File Offset: 0x00012EE6
	public bool IsChargingJump
	{
		get
		{
			return this.Sein.Abilities.ChargeJumpCharging && this.Sein.Abilities.ChargeJumpCharging.IsCharging;
		}
	}

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x0600054C RID: 1356 RVA: 0x00014D19 File Offset: 0x00012F19
	// (set) Token: 0x0600054D RID: 1357 RVA: 0x00014D21 File Offset: 0x00012F21
	public bool IsSuspended { get; set; }

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x0600054E RID: 1358 RVA: 0x00014D2A File Offset: 0x00012F2A
	public Component[] Suspendables
	{
		get
		{
			return this.m_suspendables;
		}
	}

	// Token: 0x1700015B RID: 347
	// (get) Token: 0x0600054F RID: 1359 RVA: 0x00014D32 File Offset: 0x00012F32
	public bool AnimationHasMetaData
	{
		get
		{
			return this.IsPlayingAnimation && this.Sein.Animation.Animator.CurrentAnimation.AnimationMetaData != null;
		}
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x06000550 RID: 1360 RVA: 0x00014D61 File Offset: 0x00012F61
	public bool IsDashing
	{
		get
		{
			return this.Sein.Abilities.Dash && this.Sein.Abilities.Dash.IsDashingOrChangeDashing;
		}
	}

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x06000551 RID: 1361 RVA: 0x00014D94 File Offset: 0x00012F94
	public bool IsStandingOnEdge
	{
		get
		{
			return this.Sein.Abilities.StandingOnEdge && this.Sein.Abilities.StandingOnEdge.StandingOnEdge;
		}
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x00014DC8 File Offset: 0x00012FC8
	public void EnterPlayingAnimation()
	{
		this.IsPlayingAnimation = true;
		if (this.Sein.PlatformBehaviour.PlatformMovement)
		{
			Vector2 localSpeed = this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeed;
			localSpeed.x = 0f;
			if (localSpeed.y > 0f)
			{
				localSpeed.y = 0f;
			}
			this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeed = localSpeed;
		}
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x00014E4B File Offset: 0x0001304B
	public bool CanActivateSwitch(GameObject theSwitch)
	{
		return true;
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x00014E4E File Offset: 0x0001304E
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x00014E58 File Offset: 0x00013058
	public void HandleControllerInput()
	{
		if (this.Sein.PlatformBehaviour.LeftRightMovement == null)
		{
			return;
		}
		if (!this.IgnoreControllerInput)
		{
			if (this.CanMove && !this.LockMovementInput)
			{
				this.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput = (float)this.Sein.Input.NormalizedHorizontal;
				if (this.Sein.Abilities.Run.Active && this.Sein.PlatformBehaviour.PlatformMovement.IsOnGround)
				{
					float num = this.Sein.Controller.InputCurve.Evaluate(Mathf.Abs(this.Sein.Input.Horizontal)) * Mathf.Sign(this.Sein.Input.Horizontal);
					this.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput = 0f;
					if (this.Sein.Controller.CanMove)
					{
					}
					if (num == 0f)
					{
						this.m_horizontalInputDelay = 0.06666667f;
					}
					if (Mathf.Abs(num) > this.Sein.Controller.InputSettings.JogThreshold)
					{
						this.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput = num;
					}
					this.m_horizontalInputDelay = Mathf.Max(0f, this.m_horizontalInputDelay - Time.deltaTime);
					if (this.m_horizontalInputDelay == 0f)
					{
						this.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput = num;
					}
				}
				else
				{
					this.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput = (float)Core.Input.NormalizedHorizontal;
				}
			}
			else
			{
				this.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput = 0f;
			}
		}
		this.OnHorizontalInputPostCalculate();
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x00015044 File Offset: 0x00013244
	[UberBuildMethod]
	private void ProvideComponents()
	{
		this.m_suspendables = base.gameObject.FindComponentsInChildren<ISuspendable>();
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x00015058 File Offset: 0x00013258
	public override void Awake()
	{
		this.m_transform = base.transform;
		this.ProvideComponents();
		SuspensionManager.Register(this);
		UI.Cameras.Current.ChangeTargetToCurrentCharacter();
		base.Awake();
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x00015090 File Offset: 0x00013290
	public override void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		base.OnDestroy();
		PlatformMovementPortalVisitor component = this.Sein.GetComponent<PlatformMovementPortalVisitor>();
		if (component)
		{
			PlatformMovementPortalVisitor platformMovementPortalVisitor = component;
			platformMovementPortalVisitor.OnGoThroughPortalAction = (Action)Delegate.Remove(platformMovementPortalVisitor.OnGoThroughPortalAction, new Action(this.OnGoThroughPortal));
		}
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x000150E2 File Offset: 0x000132E2
	public void OnGoThroughPortal()
	{
		this.Sein.ResetAirLimits();
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x000150F0 File Offset: 0x000132F0
	public void Start()
	{
		this.Sein.PlatformBehaviour.PlatformMovement.PlaceOnGround(0.5f, 0f);
		UI.Cameras.Current.MoveCameraToTargetInstantly(true);
		PlatformMovementPortalVisitor component = this.Sein.GetComponent<PlatformMovementPortalVisitor>();
		if (component)
		{
			PlatformMovementPortalVisitor platformMovementPortalVisitor = component;
			platformMovementPortalVisitor.OnGoThroughPortalAction = (Action)Delegate.Combine(platformMovementPortalVisitor.OnGoThroughPortalAction, new Action(this.OnGoThroughPortal));
		}
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00015160 File Offset: 0x00013360
	public void HandleJumping()
	{
		if (!this.IgnoreControllerInput && !this.LockMovementInput && this.CanMove && Core.Input.Jump.OnPressed)
		{
			this.PerformJump();
		}
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x000151A4 File Offset: 0x000133A4
	public void PerformJump()
	{
		if (CharacterState.IsActive(this.Sein.Abilities.WallChargeJump) && this.Sein.Abilities.GrabWall && this.Sein.Abilities.WallChargeJump.CanChargeJump)
		{
			this.Sein.Abilities.WallChargeJump.PerformChargeJump();
		}
		else if (CharacterState.IsActive(this.Sein.Abilities.WallJump) && this.Sein.Abilities.WallJump.CanPerformWallJump)
		{
			this.Sein.Abilities.WallJump.PerformWallJump();
		}
		else if (!this.IsGrabbingBlock)
		{
			if (CharacterState.IsActive(this.Sein.Abilities.ChargeJump) && this.Sein.Abilities.ChargeJump.CanChargeJump)
			{
				this.Sein.Abilities.ChargeJump.PerformChargeJump();
			}
			else if (CharacterState.IsActive(this.Sein.Abilities.Jump) && this.Sein.Abilities.Jump.CanJump)
			{
				this.Sein.Abilities.Jump.PerformJump();
			}
			else if (CharacterState.IsActive(this.Sein.Abilities.DoubleJump) && this.Sein.Abilities.DoubleJump.CanDoubleJump)
			{
				if (this.Sein.Controller.IsGliding)
				{
					this.Sein.Abilities.Glide.Exit();
				}
				this.Sein.Abilities.DoubleJump.PerformDoubleJump();
			}
		}
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x00015386 File Offset: 0x00013586
	public bool RayTest(GameObject target)
	{
		return this.RayTest(target, Vector2.zero, Vector2.zero);
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0001539C File Offset: 0x0001359C
	public bool RayTest(GameObject target, Vector2 startOffset, Vector2 endOffset)
	{
		Vector3 vector = this.m_transform.position + startOffset;
		Vector3 a = target.transform.position + endOffset;
		Vector3 vector2 = a - vector;
		Rigidbody component = target.GetComponent<Rigidbody>();
		RaycastHit raycastHit;
		return !Physics.Raycast(vector, vector2.normalized, out raycastHit, vector2.magnitude, this.RayTestLayerMask) || !(raycastHit.collider.gameObject != target) || (component && !(component != raycastHit.collider.attachedRigidbody)) || raycastHit.collider.isTrigger;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0001545C File Offset: 0x0001365C
	public bool RayTest(Vector3 position, Vector3 delta, out RaycastHit hitInfo)
	{
		float magnitude = delta.magnitude;
		return Physics.Raycast(position, delta / magnitude, out hitInfo, magnitude);
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x00015480 File Offset: 0x00013680
	public void StopAnimation()
	{
		this.IsPlayingAnimation = false;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0001548C File Offset: 0x0001368C
	public void PlayAnimation(TextureAnimationWithTransitions animation)
	{
		Characters.Sein.Controller.EnterPlayingAnimation();
		if (animation.Animation.Loop)
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(animation, 200, new Func<bool>(this.ShouldAnimationKeepPlaying), false);
		}
		else
		{
			this.Sein.PlatformBehaviour.Visuals.Animation.Play(animation, 200, new Func<bool>(this.ShouldAnimationKeepPlaying));
			this.Sein.PlatformBehaviour.Visuals.Animation.Animator.OnAnimationEndEvent += this.OnAnimationEndEvent;
		}
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x00015544 File Offset: 0x00013744
	private void OnAnimationEndEvent(TextureAnimation textureAnimation)
	{
		this.Sein.PlatformBehaviour.Visuals.Animation.Animator.OnAnimationEndEvent -= this.OnAnimationEndEvent;
		if (this.IsPlayingAnimation)
		{
			this.IsPlayingAnimation = false;
			this.OnTriggeredAnimationFinished();
		}
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x00015599 File Offset: 0x00013799
	public bool ShouldAnimationKeepPlaying()
	{
		return this.IsPlayingAnimation;
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x000155A4 File Offset: 0x000137A4
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.IsPlayingAnimation)
		{
			TextureAnimation currentAnimation = this.Sein.Animation.Animator.CurrentAnimation;
			if (currentAnimation)
			{
				AnimationMetaData animationMetaData = currentAnimation.AnimationMetaData;
				if (animationMetaData)
				{
					Vector3 deltaPositionAtTime = animationMetaData.CameraData.GetDeltaPositionAtTime(this.Sein.Animation.Animator.CurrentAnimationTime);
					Vector3 a = Vector3.Scale(deltaPositionAtTime, this.Sein.PlatformBehaviour.Visuals.Sprite.transform.lossyScale);
					if (this.FaceLeft)
					{
						a.x *= -1f;
					}
					this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeed = a / Time.deltaTime;
				}
				else
				{
					this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeed = Vector2.zero;
				}
			}
		}
		this.HandleControllerInput();
		this.HandleJumping();
		this.UpdateOriActiveState();
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x000156B8 File Offset: 0x000138B8
	public void HandleOffscreenIssue()
	{
		if (Scenes.Manager.PositionInsideSceneStillLoading(this.Sein.Position))
		{
			this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeed = Vector2.zero;
			this.Sein.Mortality.DamageReciever.MakeInvincible(0.1f);
		}
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00015714 File Offset: 0x00013914
	public void UpdateOriActiveState()
	{
		if (Characters.Ori && Characters.Ori.gameObject.activeSelf != this.Sein.PlayerAbilities.SpiritFlame.HasAbility)
		{
			Characters.Ori.gameObject.SetActive(this.Sein.PlayerAbilities.SpiritFlame.HasAbility);
		}
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x0001577D File Offset: 0x0001397D
	public void UpdateMovementStuff()
	{
		this.Sein.Controller.HandleJumping();
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0001578F File Offset: 0x0001398F
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_horizontalInputDelay);
		if (ar.Reading)
		{
			this.IsPlayingAnimation = false;
			this.LockMovementInput = false;
		}
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x000157B6 File Offset: 0x000139B6
	public void OnRecieveDamage(Damage damage)
	{
		this.Sein.Mortality.DamageReciever.OnRecieveDamage(damage);
	}

	// Token: 0x0400041B RID: 1051
	public SeinAnimationSpeedSettings AnimationSpeedSettings;

	// Token: 0x0400041C RID: 1052
	public bool IgnoreControllerInput;

	// Token: 0x0400041D RID: 1053
	public bool LockMovementInput;

	// Token: 0x0400041E RID: 1054
	public AnimationCurve InputCurve;

	// Token: 0x0400041F RID: 1055
	public SeinInputSettings InputSettings;

	// Token: 0x04000420 RID: 1056
	public LayerMask RayTestLayerMask;

	// Token: 0x04000421 RID: 1057
	public SeinCharacter Sein;

	// Token: 0x04000422 RID: 1058
	public bool IsPlayingAnimation;

	// Token: 0x04000423 RID: 1059
	public Action OnHorizontalInputPostCalculate = delegate()
	{
	};

	// Token: 0x04000424 RID: 1060
	private Transform m_transform;

	// Token: 0x04000425 RID: 1061
	public Transform GetItemTransform;

	// Token: 0x04000426 RID: 1062
	[SerializeField]
	[HideInInspector]
	private Component[] m_suspendables;

	// Token: 0x04000427 RID: 1063
	private float m_horizontalInputDelay;
}
