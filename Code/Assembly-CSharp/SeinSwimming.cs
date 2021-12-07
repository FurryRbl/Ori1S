using System;
using Core;
using Game;
using Sein.World;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class SeinSwimming : CharacterState, ISeinReceiver
{
	// Token: 0x06000798 RID: 1944 RVA: 0x0001FF70 File Offset: 0x0001E170
	public void ChangeState(SeinSwimming.State state)
	{
		SeinSwimming.State currentState = this.CurrentState;
		if (currentState == SeinSwimming.State.SwimMovingUnderwater)
		{
			if (this.UnderwaterSwimmingSoundProvider)
			{
				this.UnderwaterSwimmingSoundProvider.StopAndFadeOut(0.3f);
			}
		}
		this.CurrentState = state;
	}

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x06000799 RID: 1945 RVA: 0x0001FFBC File Offset: 0x0001E1BC
	public bool IsUpsideDown
	{
		get
		{
			return Vector3.Dot(MoonMath.Angle.VectorFromAngle(this.SwimAngle), (!this.m_sein.Controller.FaceLeft) ? Vector3.left : Vector3.right) > Mathf.Cos(0.87266463f);
		}
	}

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x0600079A RID: 1946 RVA: 0x0002000E File Offset: 0x0001E20E
	// (set) Token: 0x0600079B RID: 1947 RVA: 0x00020016 File Offset: 0x0001E216
	public float RemainingBreath { get; set; }

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x0600079C RID: 1948 RVA: 0x0002001F File Offset: 0x0001E21F
	public bool HasUnlimitedBreathingUnderwater
	{
		get
		{
			return this.m_sein.PlayerAbilities.WaterBreath.HasAbility;
		}
	}

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x0600079D RID: 1949 RVA: 0x00020036 File Offset: 0x0001E236
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.m_sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x0600079E RID: 1950 RVA: 0x00020048 File Offset: 0x0001E248
	public CharacterLeftRightMovement LeftRightMovement
	{
		get
		{
			return this.m_sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x0600079F RID: 1951 RVA: 0x0002005A File Offset: 0x0001E25A
	public CharacterGravity Gravity
	{
		get
		{
			return this.m_sein.PlatformBehaviour.Gravity;
		}
	}

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0002006C File Offset: 0x0001E26C
	public bool IsSwimming
	{
		get
		{
			return this.CurrentState != SeinSwimming.State.OutOfWater;
		}
	}

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0002007A File Offset: 0x0001E27A
	private float WaterSurfacePositionY
	{
		get
		{
			return this.m_currentWater.Bounds.yMax;
		}
	}

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0002008C File Offset: 0x0001E28C
	public Rect WaterSurfaceBound
	{
		get
		{
			Rect result = new Rect(this.m_currentWater.Bounds);
			result.yMin = result.yMax - 0.5f;
			result.yMax += ((!this.m_sein.PlatformBehaviour.PlatformMovement.IsOnGround) ? 0.5f : 0f);
			return result;
		}
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x000200F8 File Offset: 0x0001E2F8
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
		this.m_sein.Abilities.Swimming = this;
	}

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00020112 File Offset: 0x0001E312
	// (set) Token: 0x060007A5 RID: 1957 RVA: 0x0002011A File Offset: 0x0001E31A
	public bool IsSuspended { get; set; }

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00020123 File Offset: 0x0001E323
	public bool IsUnderwater
	{
		get
		{
			return this.CurrentState == SeinSwimming.State.SwimMovingUnderwater || this.CurrentState == SeinSwimming.State.SwimIdleUnderwater;
		}
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x00020140 File Offset: 0x0001E340
	public void HideBreathingUI()
	{
		for (int i = 0; i < this.m_breathingUIAnimators.Length; i++)
		{
			LegacyAnimator legacyAnimator = this.m_breathingUIAnimators[i];
			legacyAnimator.ContinueBackward();
		}
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00020178 File Offset: 0x0001E378
	public void ShowBreathingUI()
	{
		for (int i = 0; i < this.m_breathingUIAnimators.Length; i++)
		{
			LegacyAnimator legacyAnimator = this.m_breathingUIAnimators[i];
			legacyAnimator.ContinueForward();
		}
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x000201AD File Offset: 0x0001E3AD
	public override void Awake()
	{
		base.Awake();
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
		this.m_breathingUIAnimators = this.BreathingUI.GetComponentsInChildren<LegacyAnimator>();
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x000201DC File Offset: 0x0001E3DC
	public void RestoreBreath()
	{
		this.RemainingBreath = this.Breath;
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x000201EC File Offset: 0x0001E3EC
	public void UpdateDrowning()
	{
		if (!Sein.World.Events.WaterPurified && this.CurrentState != SeinSwimming.State.OutOfWater)
		{
			this.RemainingBreath = 0f;
			this.HideBreathingUI();
		}
		if (this.HasUnlimitedBreathingUnderwater && Sein.World.Events.WaterPurified)
		{
			return;
		}
		if (this.m_sein.Controller.IsBashing)
		{
			return;
		}
		if (this.RemainingBreath > 0f)
		{
			this.RemainingBreath -= Time.deltaTime;
		}
		if (this.RemainingBreath <= 0f)
		{
			this.RemainingBreath = 0f;
			if (this.m_drowningDelay < 0f)
			{
				Damage damage = new Damage(this.DrownDamage, Vector2.zero, base.transform.position, DamageType.Drowning, base.gameObject);
				damage.DealToComponents(Characters.Sein.Mortality.DamageReciever.gameObject);
				this.m_drowningDelay = this.DurationBetweenDrowningDamage;
			}
		}
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x000202E4 File Offset: 0x0001E4E4
	public void Start()
	{
		this.LeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent += this.ModifyHorizontalPlatformMovementSettings;
		this.Gravity.ModifyGravityPlatformMovementSettingsEvent += this.ModifyGravityPlatformMovementSettings;
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x00020320 File Offset: 0x0001E520
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.LeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent -= this.ModifyHorizontalPlatformMovementSettings;
		this.Gravity.ModifyGravityPlatformMovementSettingsEvent -= this.ModifyGravityPlatformMovementSettings;
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x00020378 File Offset: 0x0001E578
	public override void Serialize(Archive ar)
	{
		this.CurrentState = (SeinSwimming.State)ar.Serialize((int)this.CurrentState);
		ar.Serialize(ref this.m_drowningDelay);
		this.RemainingBreath = ar.Serialize(this.RemainingBreath);
		ar.Serialize(ref this.m_swimIdleTime);
		ar.Serialize(ref this.m_swimMovingTime);
		ar.Serialize(ref this.SwimAngle);
		ar.Serialize(ref this.SmoothAngleDelta);
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x000203E5 File Offset: 0x0001E5E5
	public void OnRestoreCheckpoint()
	{
		this.RestoreBreath();
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x000203F0 File Offset: 0x0001E5F0
	public void ModifyHorizontalPlatformMovementSettings(HorizontalPlatformMovementSettings settings)
	{
		switch (this.CurrentState)
		{
		case SeinSwimming.State.SwimmingOnSurface:
			settings.Air.ApplySpeedMultiplier(this.SwimmingOnSurfaceHorizontalSpeed);
			settings.Ground.ApplySpeedMultiplier(this.SwimmingOnSurfaceHorizontalSpeed);
			break;
		case SeinSwimming.State.SwimMovingUnderwater:
		case SeinSwimming.State.SwimIdleUnderwater:
			settings.Air.Acceleration = 0f;
			settings.Air.Decceleration = 0f;
			settings.Air.MaxSpeed = float.PositiveInfinity;
			settings.Ground.Acceleration = 0f;
			settings.Ground.Decceleration = 0f;
			settings.Ground.MaxSpeed = float.PositiveInfinity;
			break;
		}
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x000204B0 File Offset: 0x0001E6B0
	public void ModifyGravityPlatformMovementSettings(GravityPlatformMovementSettings settings)
	{
		if (this.CurrentState == SeinSwimming.State.SwimmingOnSurface)
		{
			settings.GravityStrength = 0f;
			settings.MaxFallSpeed = 0f;
		}
		if (this.CurrentState == SeinSwimming.State.SwimMovingUnderwater || this.CurrentState == SeinSwimming.State.SwimIdleUnderwater)
		{
			settings.GravityStrength = 0f;
		}
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00020504 File Offset: 0x0001E704
	public override void UpdateCharacterState()
	{
		if (this.m_drowningDelay >= 0f)
		{
			this.m_drowningDelay -= Time.deltaTime;
		}
		switch (this.CurrentState)
		{
		case SeinSwimming.State.OutOfWater:
			this.UpdateOutOfWaterState();
			break;
		case SeinSwimming.State.SwimmingOnSurface:
			this.UpdateSwimmingOnSurfaceState();
			break;
		case SeinSwimming.State.SwimMovingUnderwater:
			this.UpdateSwimMovingUnderwaterState();
			break;
		case SeinSwimming.State.SwimIdleUnderwater:
			this.UpdateSwimIdleUnderwaterState();
			break;
		}
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x00020584 File Offset: 0x0001E784
	public void GetOutOfWater()
	{
		Sound.Play(this.OutOfWaterSoundProvider.GetSound(null), this.m_sein.transform.position, null);
		InstantiateUtility.Instantiate(this.WaterSplashPrefab, this.m_sein.transform.position, Quaternion.identity);
		this.ChangeState(SeinSwimming.State.OutOfWater);
		this.RemainingBreath = this.Breath;
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x000205E8 File Offset: 0x0001E7E8
	public void SwimUnderwater()
	{
		this.ChangeState(SeinSwimming.State.SwimMovingUnderwater);
		this.SwimAngle = 270f;
		this.m_swimIdleTime = 0f;
		this.m_swimMovingTime = 0f;
		this.m_swimAccelerationTime = 0f;
		Sound.Play(this.InWaterSoundProvider.GetSound(null), this.m_sein.transform.position, null);
		if (this.m_sein.Abilities.Bash != null && this.m_sein.Abilities.Bash.IsBashing)
		{
			Sound.Play(this.BashIntoWaterSoundProvider.GetSound(null), this.m_sein.transform.position, null);
		}
		if (this.m_sein.Abilities.Stomp && this.m_sein.Abilities.Stomp.IsStomping)
		{
			Sound.Play(this.StompIntoWaterSoundProvider.GetSound(null), this.m_sein.transform.position, null);
		}
		InstantiateUtility.Instantiate(this.WaterSplashPrefab, this.m_sein.transform.position, Quaternion.identity);
		if (!this.HasUnlimitedBreathingUnderwater)
		{
			this.RemainingBreath = this.Breath;
			this.ShowBreathingUI();
		}
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00020738 File Offset: 0x0001E938
	public void RemoveUnderwaterSounds()
	{
		if (this.m_ambienceLayer != null)
		{
			Ambience.RemoveAmbienceLayer(this.m_ambienceLayer);
			this.m_ambienceLayer = null;
			this.UnderwaterMixerSnapshot.FadeOut();
		}
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x00020770 File Offset: 0x0001E970
	public void UpdateOutOfWaterState()
	{
		Vector3 headPosition = this.m_sein.PlatformBehaviour.PlatformMovement.HeadPosition;
		this.RemoveUnderwaterSounds();
		for (int i = 0; i < Zones.WaterZones.Count; i++)
		{
			WaterZone waterZone = Zones.WaterZones[i];
			if (waterZone.Bounds.Contains(headPosition))
			{
				this.m_currentWater = waterZone;
				this.m_sein.PlatformBehaviour.PlatformMovement.LocalSpeedX *= 0.5f;
				if (Mathf.Abs(this.PlatformMovement.LocalSpeedY) <= this.SkipSurfaceSpeedIn && this.WaterSurfaceBound.Contains(this.PlatformMovement.Position))
				{
					this.SwimOnSurface();
				}
				else if (this.PlatformMovement.LocalSpeedY < 0f)
				{
					this.SwimUnderwater();
					this.PlatformMovement.LocalSpeedY *= 0.8f;
				}
				else
				{
					this.m_currentWater = null;
				}
				return;
			}
		}
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0002087C File Offset: 0x0001EA7C
	public void SwimOnSurface()
	{
		this.PlatformMovement.PositionY = this.WaterSurfacePositionY;
		this.PlatformMovement.LocalSpeedY = 0f;
		this.ChangeState(SeinSwimming.State.SwimmingOnSurface);
		if (this.m_sein.Abilities.Carry && this.m_sein.Abilities.Carry.IsCarrying)
		{
			Damage damage = new Damage(1000f, (this.m_sein.transform.position - base.transform.position).normalized, base.transform.position, DamageType.Water, base.gameObject);
			this.m_sein.Mortality.DamageReciever.OnRecieveDamage(damage);
		}
		Sound.Play(this.OutOfWaterSoundProvider.GetSound(null), this.m_sein.transform.position, null);
		InstantiateUtility.Instantiate(this.WaterSplashPrefab, this.m_sein.transform.position, Quaternion.identity);
		this.RestoreBreath();
		this.HideBreathingUI();
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x00020995 File Offset: 0x0001EB95
	public void OnDisable()
	{
		this.RemoveUnderwaterSounds();
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x000209A0 File Offset: 0x0001EBA0
	public void UpdateSwimmingOnSurfaceState()
	{
		if (!Sein.World.Events.WaterPurified)
		{
			this.UpdateDrowning();
		}
		this.RemoveUnderwaterSounds();
		if (this.m_currentWater == null)
		{
			this.GetOutOfWater();
			return;
		}
		Vector2 point = this.m_sein.PlatformBehaviour.PlatformMovement.Position;
		if (this.WaterSurfaceBound.Contains(point))
		{
			this.PlatformMovement.Ground.IsOn = false;
			this.PlatformMovement.GroundNormal = Vector3.up;
			this.PlatformMovement.PositionY = this.WaterSurfacePositionY;
			this.PlatformMovement.LocalSpeedY = 0f;
			this.m_sein.PlatformBehaviour.Visuals.Animation.PlayLoop((this.m_sein.Input.NormalizedHorizontal != 0) ? this.Animations.SwimSurface.Moving : this.Animations.SwimSurface.Idle, 9, new Func<bool>(this.ShouldSwimSurfaceAnimationPlay), false);
			if (this.SurfaceSwimmingSoundProvider && !this.SurfaceSwimmingSoundProvider.IsPlaying && this.m_sein.Input.NormalizedHorizontal != 0)
			{
				this.SurfaceSwimmingSoundProvider.Play();
			}
			if (this.m_sein.Controller.CanMove && !this.m_sein.Controller.IsBashing)
			{
				if (this.m_sein.Input.Down.Pressed)
				{
					this.SwimUnderwater();
					this.PlatformMovement.LocalSpeedY = -this.DiveUnderwaterSpeed;
				}
				if (Core.Input.Jump.OnPressed)
				{
					this.SurfaceSwimJump();
				}
			}
			return;
		}
		this.GetOutOfWater();
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x00020B70 File Offset: 0x0001ED70
	public void HorizontalFlip()
	{
		this.m_swimMovingTime = 0f;
		this.m_boostAnimationRemainingTime = 0f;
		this.SwimAngle += 180f;
		this.m_sein.Controller.FaceLeft = !this.m_sein.Controller.FaceLeft;
		this.m_sein.PlatformBehaviour.Visuals.Animation.Play(this.Animations.SwimFlipHorizontalAnimation, 10, new Func<bool>(this.ShouldSwimUnderwaterAnimationPlay));
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x00020BFC File Offset: 0x0001EDFC
	public void VerticalFlip()
	{
		this.m_boostAnimationRemainingTime = 0f;
		this.m_swimMovingTime = 0f;
		this.m_sein.Controller.FaceLeft = !this.m_sein.Controller.FaceLeft;
		this.m_sein.PlatformBehaviour.Visuals.Animation.Play(this.Animations.SwimFlipVerticalAnimation, 10, new Func<bool>(this.ShouldSwimUnderwaterAnimationPlay));
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x00020C78 File Offset: 0x0001EE78
	public void HorizontalVerticalFlip()
	{
		this.m_swimMovingTime = 0f;
		this.m_boostAnimationRemainingTime = 0f;
		this.SwimAngle += 180f;
		this.m_sein.PlatformBehaviour.Visuals.Animation.Play(this.Animations.SwimFlipHorizontalVerticalAnimation, 10, new Func<bool>(this.ShouldSwimUnderwaterAnimationPlay));
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x00020CE4 File Offset: 0x0001EEE4
	public void OnBash(float angle)
	{
		if (this.IsUnderwater)
		{
			angle += 90f;
			this.SwimAngle = angle;
			this.m_sein.Controller.FaceLeft = (MoonMath.Angle.VectorFromAngle(angle).x < 0f);
			this.m_swimAccelerationTime = -this.BashTime;
			this.ChangeState(SeinSwimming.State.SwimIdleUnderwater);
		}
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x00020D45 File Offset: 0x0001EF45
	public void ApplySwimmingUnderwaterStuff()
	{
		if (this.m_ambienceLayer == null)
		{
			this.m_ambienceLayer = new Ambience.Layer(this.SwimmingUnderwaterAmbience, 0.7f, 0.7f, 5);
			Ambience.AddAmbienceLayer(this.m_ambienceLayer);
			this.UnderwaterMixerSnapshot.FadeIn();
		}
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x00020D84 File Offset: 0x0001EF84
	public void UpdateSwimMovingUnderwaterState()
	{
		this.UpdateDrowning();
		if (this.UnderwaterSwimmingSoundProvider && !this.UnderwaterSwimmingSoundProvider.IsPlaying)
		{
			this.UnderwaterSwimmingSoundProvider.Play();
		}
		this.m_sein.PlatformBehaviour.PlatformMovement.ForceKeepInAir = true;
		Vector2 vector = (!this.m_sein.Controller.CanMove) ? Vector2.zero : this.m_sein.Input.Axis;
		this.m_swimAccelerationTime += 2f * Time.deltaTime;
		Vector2 vector2 = Vector3.down * this.MaxFallSpeed;
		if (vector.magnitude > 0.3f)
		{
			this.m_swimIdleTime = 0f;
			vector.Normalize();
			float swimAngle = this.SwimAngle;
			Vector2 v = MoonMath.Angle.VectorFromAngle(this.SwimAngle);
			if (Vector3.Dot(-vector, v) > Mathf.Cos(1.0471976f))
			{
				if (this.IsUpsideDown)
				{
					this.HorizontalVerticalFlip();
				}
				else
				{
					this.HorizontalFlip();
				}
			}
			else
			{
				float target = MoonMath.Angle.AngleFromVector(vector);
				this.SwimAngle = Mathf.MoveTowardsAngle(this.SwimAngle, target, this.SwimAngleDeltaLimit * Time.deltaTime);
				vector = MoonMath.Angle.VectorFromAngle(this.SwimAngle);
				vector2 = vector * this.SwimSpeed;
				if (this.m_sein.Controller.CanMove && Core.Input.Jump.Pressed)
				{
					this.m_isBoosting = true;
					this.m_boostTime = Mathf.Min(this.m_boostTime, this.BoostPeakTime);
				}
				if (this.m_sein.Controller.CanMove && Core.Input.Jump.OnPressed && this.m_boostAnimationRemainingTime <= 0f && this.BoostSwimsoundProvider)
				{
					Sound.Play(this.BoostSwimsoundProvider.GetSound(null), base.transform.position, null);
					this.m_boostAnimationRemainingTime = 0.6666667f;
				}
				if (this.m_isBoosting)
				{
					this.m_boostTime += Time.deltaTime / this.BoostDuration;
					vector2 *= this.SwimSpeedBoostCurve.Evaluate(this.m_boostTime);
				}
				if (this.m_isBoosting && this.m_boostTime > this.BoostDuration)
				{
					this.m_isBoosting = false;
					this.m_boostTime = 0f;
				}
			}
			float b = MoonMath.Angle.AngleSubtract(this.SwimAngle, swimAngle) / Time.deltaTime;
			this.SmoothAngleDelta = Mathf.Lerp(this.SmoothAngleDelta, b, 0.1f);
		}
		else
		{
			if (this.m_swimAccelerationTime > 0f)
			{
				this.m_swimAccelerationTime = 0f;
			}
			if (this.m_isBoosting)
			{
				this.m_isBoosting = false;
				this.m_boostTime = 0f;
				this.m_boostAnimationRemainingTime = 0f;
			}
			if (this.m_swimIdleTime > 0.1f)
			{
				this.m_swimMovingTime = 0f;
				if (this.m_swimAccelerationTime > 0f)
				{
					this.m_swimAccelerationTime = 0f;
				}
				if (this.IsUpsideDown)
				{
					this.VerticalFlip();
				}
				bool faceLeft = this.m_sein.Controller.FaceLeft;
				float target2 = (float)((!faceLeft) ? 0 : 180);
				if (MoonMath.Angle.AngleSubtract(this.SwimAngle, target2) > 0f)
				{
					this.m_sein.PlatformBehaviour.Visuals.Animation.Play(faceLeft ? this.Animations.SwimMiddleToIdleClockwise : this.Animations.SwimMiddleToIdleAntiClockwise, 10, new Func<bool>(this.ShouldIdleUnderwaterAnimationPlay));
				}
				else
				{
					this.m_sein.PlatformBehaviour.Visuals.Animation.Play((!faceLeft) ? this.Animations.SwimMiddleToIdleClockwise : this.Animations.SwimMiddleToIdleAntiClockwise, 10, new Func<bool>(this.ShouldIdleUnderwaterAnimationPlay));
				}
				this.ChangeState(SeinSwimming.State.SwimIdleUnderwater);
			}
			this.m_swimIdleTime += Time.deltaTime;
		}
		this.PlatformMovement.LocalSpeed = Vector3.Lerp(this.PlatformMovement.LocalSpeed, vector2, this.AccelerationOverTime.Evaluate(this.m_swimAccelerationTime));
		if (this.IsUpsideDown && Math.Abs(this.SmoothAngleDelta) < 10f)
		{
			this.VerticalFlip();
		}
		this.ApplySwimmingUnderwaterStuff();
		if (this.m_boostAnimationRemainingTime > 0f)
		{
			this.m_boostAnimationRemainingTime -= Time.deltaTime;
			int min = Mathf.RoundToInt(this.Animations.AnimationFromBend.Evaluate(this.SmoothAngleDelta * (float)((!this.m_sein.Controller.FaceLeft) ? -1 : 1)) * (float)(this.Animations.SwimJumpLeft.Length - 1));
			int num = Mathf.Clamp(0, min, this.Animations.SwimJumpLeft.Length - 1);
			this.m_sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.Animations.SwimJumpLeft[num], 9, new Func<bool>(this.ShouldSwimUnderwaterAnimationPlay), true);
		}
		else
		{
			int min2 = Mathf.RoundToInt(this.Animations.AnimationFromBend.Evaluate(this.SmoothAngleDelta * (float)((!this.m_sein.Controller.FaceLeft) ? -1 : 1)) * (float)(this.Animations.SwimHorizontal.Length - 1));
			int num2 = Mathf.Clamp(0, min2, this.Animations.SwimHorizontal.Length - 1);
			this.m_sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.Animations.SwimHorizontal[num2], 9, new Func<bool>(this.ShouldSwimUnderwaterAnimationPlay), true);
		}
		this.HandleLeavingWater();
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x00021388 File Offset: 0x0001F588
	public void UpdateSwimIdleUnderwaterState()
	{
		this.UpdateDrowning();
		Vector2 vector = (!this.m_sein.Controller.CanMove) ? Vector2.zero : this.m_sein.Input.Axis;
		this.m_swimAccelerationTime += Time.deltaTime;
		if (vector.magnitude > 0.3f)
		{
			if (this.m_swimAccelerationTime > 0f)
			{
				this.m_swimAccelerationTime = 0f;
			}
			this.m_swimIdleTime = 0f;
			this.ChangeState(SeinSwimming.State.SwimMovingUnderwater);
		}
		else
		{
			float target = (float)((!this.m_sein.Controller.FaceLeft) ? 0 : 180);
			this.SwimAngle = Mathf.MoveTowardsAngle(this.SwimAngle, target, this.SwimAngleDeltaLimit * Time.deltaTime);
			this.m_sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.Animations.SwimIdle, 9, new Func<bool>(this.ShouldIdleUnderwaterAnimationPlay), true);
		}
		this.PlatformMovement.LocalSpeed = Vector3.Lerp(this.PlatformMovement.LocalSpeed, Vector3.down * this.MaxFallSpeed, this.AccelerationOverTime.Evaluate(this.m_swimAccelerationTime));
		this.ApplySwimmingUnderwaterStuff();
		this.HandleLeavingWater();
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x000214E8 File Offset: 0x0001F6E8
	public void HandleLeavingWater()
	{
		Vector3 position = this.m_sein.PlatformBehaviour.PlatformMovement.Position;
		for (int i = 0; i < Zones.WaterZones.Count; i++)
		{
			WaterZone waterZone = Zones.WaterZones[i];
			if (waterZone.Bounds.Contains(position))
			{
				this.m_currentWater = waterZone;
				return;
			}
		}
		if (this.RemainingBreath / this.Breath > 0.5f)
		{
			if (this.EmergeHighBreathSoundProvider)
			{
				Sound.Play(this.EmergeHighBreathSoundProvider.GetSound(null), base.transform.position, null);
			}
		}
		else if (this.RemainingBreath / this.Breath > 0.15f)
		{
			if (this.EmergeMedBreathSoundProvider)
			{
				Sound.Play(this.EmergeMedBreathSoundProvider.GetSound(null), base.transform.position, null);
			}
		}
		else if (this.EmergeLowBreathSoundProvider)
		{
			Sound.Play(this.EmergeLowBreathSoundProvider.GetSound(null), base.transform.position, null);
		}
		this.RestoreBreath();
		this.HideBreathingUI();
		if (this.m_currentWater.HasTopSurface && this.WaterSurfaceBound.Contains(this.PlatformMovement.Position))
		{
			this.SwimOnSurface();
		}
		else
		{
			this.GetOutOfWater();
		}
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00021658 File Offset: 0x0001F858
	public bool CanJump()
	{
		return this.CurrentState == SeinSwimming.State.SwimmingOnSurface || this.CurrentState == SeinSwimming.State.SwimMovingUnderwater;
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x00021674 File Offset: 0x0001F874
	public void SurfaceSwimJump()
	{
		this.PlatformMovement.LocalSpeedY = this.JumpOutOfWaterSpeed;
		if (this.m_sein.Input.NormalizedHorizontal == 0)
		{
			this.m_sein.PlatformBehaviour.Visuals.Animation.Play(this.Animations.JumpOutOfWater.Idle, 10, new Func<bool>(this.ShouldJumpOutOfWaterAnimationIdleKeepPlaying));
		}
		else
		{
			this.m_sein.PlatformBehaviour.Visuals.Animation.Play(this.Animations.JumpOutOfWater.Moving, 10, new Func<bool>(this.ShouldJumpOutOfWaterAnimationMovingKeepPlaying));
		}
		this.m_sein.ResetAirLimits();
		this.GetOutOfWater();
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0002172F File Offset: 0x0001F92F
	public bool ShouldSwimUnderwaterAnimationPlay()
	{
		return this.CurrentState == SeinSwimming.State.SwimMovingUnderwater;
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x0002173A File Offset: 0x0001F93A
	public bool ShouldIdleUnderwaterAnimationPlay()
	{
		return this.CurrentState == SeinSwimming.State.SwimIdleUnderwater;
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x00021745 File Offset: 0x0001F945
	public bool ShouldSwimSurfaceAnimationPlay()
	{
		return this.CurrentState == SeinSwimming.State.SwimmingOnSurface;
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x00021750 File Offset: 0x0001F950
	public bool ShouldJumpOutOfWaterAnimationIdleKeepPlaying()
	{
		return this.PlatformMovement.IsInAir && (!this.m_sein.Controller.CanMove || this.m_sein.Input.NormalizedHorizontal == 0) && (!this.IsSwimming || !this.PlatformMovement.Falling);
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x000217BC File Offset: 0x0001F9BC
	public bool ShouldJumpOutOfWaterAnimationMovingKeepPlaying()
	{
		return this.PlatformMovement.IsInAir && (!this.m_sein.Controller.CanMove || this.m_sein.Input.NormalizedHorizontal != 0) && (!this.IsSwimming || !this.PlatformMovement.Falling);
	}

	// Token: 0x040005F9 RID: 1529
	public SoundProvider SwimmingUnderwaterAmbience;

	// Token: 0x040005FA RID: 1530
	public MixerSnapshot UnderwaterMixerSnapshot;

	// Token: 0x040005FB RID: 1531
	public SeinSwimming.State CurrentState;

	// Token: 0x040005FC RID: 1532
	public SeinSwimming.SwimmingAnimations Animations;

	// Token: 0x040005FD RID: 1533
	public float Breath = 3f;

	// Token: 0x040005FE RID: 1534
	public GameObject BreathingUI;

	// Token: 0x040005FF RID: 1535
	public float DiveUnderwaterSpeed = 3f;

	// Token: 0x04000600 RID: 1536
	public float DurationBetweenDrowningDamage = 1f;

	// Token: 0x04000601 RID: 1537
	public SoundProvider InWaterSoundProvider;

	// Token: 0x04000602 RID: 1538
	public SoundProvider BashIntoWaterSoundProvider;

	// Token: 0x04000603 RID: 1539
	public SoundProvider StompIntoWaterSoundProvider;

	// Token: 0x04000604 RID: 1540
	public float JumpOutOfWaterSpeed = 20f;

	// Token: 0x04000605 RID: 1541
	public SoundProvider OutOfWaterSoundProvider;

	// Token: 0x04000606 RID: 1542
	public float SkipSurfaceSpeedIn = 20f;

	// Token: 0x04000607 RID: 1543
	public float SkipSurfaceSpeedOut = 10f;

	// Token: 0x04000608 RID: 1544
	public SoundSource SurfaceSwimmingSoundProvider;

	// Token: 0x04000609 RID: 1545
	public SoundSource UnderwaterSwimmingSoundProvider;

	// Token: 0x0400060A RID: 1546
	public SoundProvider EmergeHighBreathSoundProvider;

	// Token: 0x0400060B RID: 1547
	public SoundProvider EmergeMedBreathSoundProvider;

	// Token: 0x0400060C RID: 1548
	public SoundProvider EmergeLowBreathSoundProvider;

	// Token: 0x0400060D RID: 1549
	public SoundProvider BoostSwimsoundProvider;

	// Token: 0x0400060E RID: 1550
	public float SwimGravity = 13f;

	// Token: 0x0400060F RID: 1551
	public float SwimSpeed = 6f;

	// Token: 0x04000610 RID: 1552
	public AnimationCurve SwimSpeedBoostCurve;

	// Token: 0x04000611 RID: 1553
	public float BoostPeakTime = 0.2f;

	// Token: 0x04000612 RID: 1554
	private float m_boostTime;

	// Token: 0x04000613 RID: 1555
	public float BoostDuration;

	// Token: 0x04000614 RID: 1556
	private bool m_isBoosting;

	// Token: 0x04000615 RID: 1557
	public float SwimAngle;

	// Token: 0x04000616 RID: 1558
	public float SwimAngleDeltaLimit = 100f;

	// Token: 0x04000617 RID: 1559
	private float m_swimMovingTime;

	// Token: 0x04000618 RID: 1560
	private float m_swimIdleTime;

	// Token: 0x04000619 RID: 1561
	private float m_swimAccelerationTime;

	// Token: 0x0400061A RID: 1562
	public float SwimUpwardsGravity = 13f;

	// Token: 0x0400061B RID: 1563
	public HorizontalPlatformMovementSettings.SpeedMultiplierSet SwimmingOnSurfaceHorizontalSpeed;

	// Token: 0x0400061C RID: 1564
	public GameObject WaterSplashPrefab;

	// Token: 0x0400061D RID: 1565
	private WaterZone m_currentWater;

	// Token: 0x0400061E RID: 1566
	private float m_drowningDelay;

	// Token: 0x0400061F RID: 1567
	private SeinCharacter m_sein;

	// Token: 0x04000620 RID: 1568
	private LegacyAnimator[] m_breathingUIAnimators;

	// Token: 0x04000621 RID: 1569
	public float DrownDamage = 5f;

	// Token: 0x04000622 RID: 1570
	private Ambience.Layer m_ambienceLayer;

	// Token: 0x04000623 RID: 1571
	public bool CanHorizontalSwimJump;

	// Token: 0x04000624 RID: 1572
	public float Deceleration = 10f;

	// Token: 0x04000625 RID: 1573
	public float MaxFallSpeed = 4f;

	// Token: 0x04000626 RID: 1574
	public float BashTime = 1f;

	// Token: 0x04000627 RID: 1575
	public float SmoothAngleDelta;

	// Token: 0x04000628 RID: 1576
	public AnimationCurve AccelerationOverTime;

	// Token: 0x04000629 RID: 1577
	private float m_boostAnimationRemainingTime;

	// Token: 0x0400062A RID: 1578
	public bool CanJumpUnderwater;

	// Token: 0x0400062B RID: 1579
	public bool HoldAToSwimLoop;

	// Token: 0x0400062C RID: 1580
	public float SwimJumpSpeedDelta = 100f;

	// Token: 0x020000B4 RID: 180
	[Serializable]
	public class MovingAndIdleAnimationPair
	{
		// Token: 0x0400062F RID: 1583
		public TextureAnimationWithTransitions Idle;

		// Token: 0x04000630 RID: 1584
		public TextureAnimationWithTransitions Moving;
	}

	// Token: 0x020000B5 RID: 181
	public enum State
	{
		// Token: 0x04000632 RID: 1586
		OutOfWater,
		// Token: 0x04000633 RID: 1587
		SwimmingOnSurface,
		// Token: 0x04000634 RID: 1588
		SwimMovingUnderwater,
		// Token: 0x04000635 RID: 1589
		SwimIdleUnderwater
	}

	// Token: 0x020000B6 RID: 182
	[Serializable]
	public class SwimmingAnimations
	{
		// Token: 0x04000636 RID: 1590
		public SeinSwimming.MovingAndIdleAnimationPair JumpOutOfWater;

		// Token: 0x04000637 RID: 1591
		public SeinSwimming.MovingAndIdleAnimationPair SwimSurface;

		// Token: 0x04000638 RID: 1592
		public TextureAnimationWithTransitions[] SwimHorizontal;

		// Token: 0x04000639 RID: 1593
		public TextureAnimationWithTransitions[] SwimJumpLeft;

		// Token: 0x0400063A RID: 1594
		public AnimationCurve AnimationFromBend;

		// Token: 0x0400063B RID: 1595
		public TextureAnimationWithTransitions SwimIdle;

		// Token: 0x0400063C RID: 1596
		public TextureAnimationWithTransitions SwimMiddleToIdleClockwise;

		// Token: 0x0400063D RID: 1597
		public TextureAnimationWithTransitions SwimMiddleToIdleAntiClockwise;

		// Token: 0x0400063E RID: 1598
		public TextureAnimationWithTransitions SwimIdleToSwimMiddle;

		// Token: 0x0400063F RID: 1599
		public TextureAnimationWithTransitions SwimFlipHorizontalAnimation;

		// Token: 0x04000640 RID: 1600
		public TextureAnimationWithTransitions SwimFlipVerticalAnimation;

		// Token: 0x04000641 RID: 1601
		public TextureAnimationWithTransitions SwimFlipHorizontalVerticalAnimation;
	}
}
