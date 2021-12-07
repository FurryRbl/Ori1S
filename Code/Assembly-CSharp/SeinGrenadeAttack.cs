using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000412 RID: 1042
public class SeinGrenadeAttack : CharacterState, ISeinReceiver
{
	// Token: 0x170004BF RID: 1215
	// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0007B4E8 File Offset: 0x000796E8
	private bool IsGrabbingWall
	{
		get
		{
			return this.m_sein.Controller.IsGrabbingWall;
		}
	}

	// Token: 0x170004C0 RID: 1216
	// (get) Token: 0x06001C6C RID: 7276 RVA: 0x0007B4FA File Offset: 0x000796FA
	private bool IsInAir
	{
		get
		{
			return !this.m_isAiming;
		}
	}

	// Token: 0x06001C6D RID: 7277 RVA: 0x0007B505 File Offset: 0x00079705
	private void ResetAimToDefault()
	{
		this.SetAimVelocity(new Vector2(14f, 16f));
	}

	// Token: 0x06001C6E RID: 7278 RVA: 0x0007B51C File Offset: 0x0007971C
	private int PickAnimationIndex(int length)
	{
		float num = (!this.IsGrabbingWall) ? Mathf.InverseLerp(this.MinAimGroundAnimationAngle, this.MaxAimGroundAnimationAngle, this.m_animationAimAngle) : Mathf.InverseLerp(this.MinAimWallAnimationAngle, this.MaxAimWallAnimationAngle, this.m_animationAimAngle);
		return Mathf.Clamp(Mathf.FloorToInt(num * (float)length), 0, length - 1);
	}

	// Token: 0x06001C6F RID: 7279 RVA: 0x0007B57C File Offset: 0x0007977C
	private float IndexToAnimationAngle(int index, int length)
	{
		float t = (float)index / (float)length;
		return (!this.IsGrabbingWall) ? Mathf.Lerp(this.MinAimGroundAnimationAngle, this.MaxAimGroundAnimationAngle, t) : Mathf.Lerp(this.MinAimWallAnimationAngle, this.MaxAimWallAnimationAngle, t);
	}

	// Token: 0x06001C70 RID: 7280 RVA: 0x0007B5C4 File Offset: 0x000797C4
	private TextureAnimationWithTransitions PickAnimation(TextureAnimationWithTransitions[] animations)
	{
		int num = this.PickAnimationIndex(animations.Length);
		return animations[num];
	}

	// Token: 0x170004C1 RID: 1217
	// (get) Token: 0x06001C71 RID: 7281 RVA: 0x0007B5E0 File Offset: 0x000797E0
	private float EnergyCostFinal
	{
		get
		{
			return (!this.HasGrenadeEfficiencySkill()) ? this.EnergyCost : (this.EnergyCost / 2f);
		}
	}

	// Token: 0x06001C72 RID: 7282 RVA: 0x0007B60F File Offset: 0x0007980F
	private bool HasGrenadeEfficiencySkill()
	{
		return this.m_sein.PlayerAbilities.GrenadeEfficiency.HasAbility;
	}

	// Token: 0x170004C2 RID: 1218
	// (get) Token: 0x06001C73 RID: 7283 RVA: 0x0007B626 File Offset: 0x00079826
	private bool HasEnoughEnergy
	{
		get
		{
			return this.m_sein.Energy.CanAfford(this.EnergyCostFinal);
		}
	}

	// Token: 0x06001C74 RID: 7284 RVA: 0x0007B63E File Offset: 0x0007983E
	private void SpendEnergy()
	{
		this.m_sein.Energy.Spend(this.EnergyCostFinal);
	}

	// Token: 0x06001C75 RID: 7285 RVA: 0x0007B656 File Offset: 0x00079856
	private void RestoreEnergy()
	{
		this.m_sein.Energy.Gain(this.EnergyCostFinal);
	}

	// Token: 0x06001C76 RID: 7286 RVA: 0x0007B66E File Offset: 0x0007986E
	public void Start()
	{
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent += this.ModifyHorizontalPlatformMovementSettings;
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001C77 RID: 7287 RVA: 0x0007B6A0 File Offset: 0x000798A0
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent -= this.ModifyHorizontalPlatformMovementSettings;
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001C78 RID: 7288 RVA: 0x0007B6E0 File Offset: 0x000798E0
	public void OnRestoreCheckpoint()
	{
		this.CancelAiming();
	}

	// Token: 0x170004C3 RID: 1219
	// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0007B6E8 File Offset: 0x000798E8
	public CharacterLeftRightMovement CharacterLeftRightMovement
	{
		get
		{
			return this.m_sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x170004C4 RID: 1220
	// (get) Token: 0x06001C7A RID: 7290 RVA: 0x0007B6FA File Offset: 0x000798FA
	public CharacterGravity CharacterGravity
	{
		get
		{
			return this.m_sein.PlatformBehaviour.Gravity;
		}
	}

	// Token: 0x06001C7B RID: 7291 RVA: 0x0007B70C File Offset: 0x0007990C
	private void ModifyHorizontalPlatformMovementSettings(HorizontalPlatformMovementSettings settings)
	{
		if (this.m_isAiming)
		{
			settings.Ground.Acceleration = 0f;
			settings.Ground.MaxSpeed = 0f;
		}
	}

	// Token: 0x06001C7C RID: 7292 RVA: 0x0007B739 File Offset: 0x00079939
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
		sein.Abilities.Grenade = this;
	}

	// Token: 0x06001C7D RID: 7293 RVA: 0x0007B750 File Offset: 0x00079950
	public override void UpdateCharacterState()
	{
		if (this.m_sein.IsSuspended)
		{
			return;
		}
		if (this.m_sein.Controller.InputLocked)
		{
			return;
		}
		if (SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities))
		{
			return;
		}
		base.UpdateCharacterState();
		if (this.m_isAiming)
		{
			this.UpdateAiming();
		}
		else
		{
			this.UpdateNormal();
		}
	}

	// Token: 0x06001C7E RID: 7294 RVA: 0x0007B7B2 File Offset: 0x000799B2
	private bool HasGrenadeUpgrade()
	{
		return this.m_sein.PlayerAbilities.GrenadeUpgrade.HasAbility;
	}

	// Token: 0x170004C5 RID: 1221
	// (get) Token: 0x06001C7F RID: 7295 RVA: 0x0007B7C9 File Offset: 0x000799C9
	private Vector3 GrenadeSpawnPosition
	{
		get
		{
			return this.m_sein.Position;
		}
	}

	// Token: 0x06001C80 RID: 7296 RVA: 0x0007B7D8 File Offset: 0x000799D8
	private SpiritGrenade SpawnGrenade(Vector2 velocity)
	{
		this.RefreshListOfQuickSpiritGrenades();
		if (this.m_spiritGrenades.Count >= this.MaxSpamGrenades)
		{
			this.m_spiritGrenades[0].Explode();
			this.m_spiritGrenades.RemoveAt(0);
		}
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate((!this.HasGrenadeUpgrade()) ? this.Grenade : this.GrenadeUpgraded, this.GrenadeSpawnPosition, Quaternion.identity);
		SpiritGrenade component = gameObject.GetComponent<SpiritGrenade>();
		component.SetTrajectory(velocity);
		this.m_spiritGrenades.Add(component);
		if (this.m_autoTarget as Component != null)
		{
			component.Duration = this.TimeToTarget(velocity, this.m_autoTarget) + 0.2f;
			this.m_autoTarget = null;
		}
		return component;
	}

	// Token: 0x06001C81 RID: 7297 RVA: 0x0007B8A1 File Offset: 0x00079AA1
	private void RefreshListOfQuickSpiritGrenades()
	{
		this.m_spiritGrenades.RemoveAll((SpiritGrenade a) => a == null);
	}

	// Token: 0x170004C6 RID: 1222
	// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0007B8CC File Offset: 0x00079ACC
	public bool IsAiming
	{
		get
		{
			return this.m_isAiming;
		}
	}

	// Token: 0x170004C7 RID: 1223
	// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0007B8D4 File Offset: 0x00079AD4
	public bool CanAim
	{
		get
		{
			return !this.m_sein.PlatformBehaviour.PlatformMovement.MovingHorizontally && (this.m_sein.IsOnGround || this.IsGrabbingWall);
		}
	}

	// Token: 0x06001C84 RID: 7300 RVA: 0x0007B920 File Offset: 0x00079B20
	public void PlayAimAnimation()
	{
		this.m_sein.Animation.PlayLoop(this.PickAnimation((!this.IsGrabbingWall) ? this.AimingAnimations : this.WallAimingAnimations), 154, new Func<bool>(this.KeepPlayingAimAnimation), true);
	}

	// Token: 0x06001C85 RID: 7301 RVA: 0x0007B974 File Offset: 0x00079B74
	public void PlayThrowAnimation()
	{
		if (Mathf.Approximately(Mathf.Abs(this.m_rawAimOffset.x), this.QuickThrowSpeed.x) && Mathf.Approximately(this.m_rawAimOffset.y, this.QuickThrowSpeed.y))
		{
			this.m_sein.Animation.Play((!this.IsGrabbingWall) ? this.QuickThrow.IdleThrowAnimation : this.QuickThrow.WallThrowAnimation, 154, new Func<bool>(this.KeepPlayingThrowAnimation));
		}
		else
		{
			this.m_sein.Animation.Play(this.PickAnimation((!this.IsGrabbingWall) ? this.ThrowAnimations : this.WallThrowAnimations), 154, new Func<bool>(this.KeepPlayingThrowAnimation));
		}
	}

	// Token: 0x06001C86 RID: 7302 RVA: 0x0007BA58 File Offset: 0x00079C58
	public void PlayThrowSound()
	{
		Sound.Play(this.ThrowGrenadeSound.GetSound(null), base.transform.position, null);
	}

	// Token: 0x170004C8 RID: 1224
	// (get) Token: 0x06001C87 RID: 7303 RVA: 0x0007BA83 File Offset: 0x00079C83
	public float GrenadeGravity
	{
		get
		{
			return this.Trajectory.Gravity;
		}
	}

	// Token: 0x06001C88 RID: 7304 RVA: 0x0007BA90 File Offset: 0x00079C90
	public void UpdateAiming()
	{
		if (Core.Input.LeftShoulder.Released)
		{
			this.m_lockPressingInputTime = 0.64f;
			this.SpawnGrenade(this.m_rawAimOffset);
			this.PlayThrowAnimation();
			this.EndAiming();
			this.PlayThrowSound();
			return;
		}
		if (Core.Input.Jump.OnPressed || Core.Input.Cancel.OnPressed || !this.CanAim)
		{
			this.CancelAiming();
			return;
		}
		this.m_sein.Speed = Vector2.zero;
		Vector2 axis = Core.Input.Axis;
		Vector2 b = this.AimSpeed.Evaluate(axis.magnitude) * axis.normalized;
		if (b.magnitude > 0f)
		{
			this.m_autoAim = false;
		}
		this.m_rawAimOffset += b;
		if (this.m_autoAim)
		{
			this.AutoTarget();
		}
		else
		{
			this.m_autoTarget = null;
		}
		this.ClampAim();
		if (Core.Input.CursorMoved)
		{
			Vector2 v = UI.Cameras.Current.Camera.WorldToScreenPoint(base.transform.position);
			Vector2 b2 = UI.Cameras.System.GUICamera.ScreenToWorldPoint(v);
			this.m_rawAimOffset = (Core.Input.CursorPositionUI - b2) * this.CursorSpeedMultiplier + Vector2.up * this.CursorSpeedYOffset;
			this.m_autoAim = false;
			this.ClampAim();
		}
		this.m_aimOffset = Vector2.Lerp(this.m_rawAimOffset, this.m_aimOffset, 0.5f);
		if (!this.m_sein.Controller.IsGrabbingWall)
		{
			if (this.m_lockAimAnimationRemainingTime <= 0f)
			{
				bool faceLeft = this.m_faceLeft;
				this.m_faceLeft = (this.m_aimOffset.x < 0f);
				if (faceLeft != this.m_faceLeft)
				{
					this.m_lockAimAnimationRemainingTime = 0.17f;
					this.m_animationAimAngle = 90f;
					Sound.Play(this.TurnAroundAimingSound.GetSound(null), base.transform.position, null);
				}
			}
			this.m_sein.FaceLeft = this.m_faceLeft;
		}
		this.UpdateTrajectory();
		if (this.m_lockAimAnimationRemainingTime > 0f)
		{
			this.m_lockAimAnimationRemainingTime -= Time.deltaTime;
		}
		if (this.m_lockAimAnimationRemainingTime <= 0f)
		{
			Vector3 v2 = this.m_aimOffset.normalized;
			if (this.m_aimOffset.y > 0f)
			{
				float num = this.m_aimOffset.y / this.GrenadeGravity;
				float d = this.m_aimOffset.y * num + 0.5f * this.GrenadeGravity * num * num;
				v2 = (this.m_aimOffset.x * num * Vector3.right + d * Vector3.up).normalized;
			}
			v2.x = Mathf.Abs(v2.x);
			float target = MoonMath.Angle.AngleFromVector(v2);
			this.m_animationAimAngle = Mathf.MoveTowardsAngle(this.m_animationAimAngle, target, 90f * Time.deltaTime * 2f);
			this.PlayAimAnimation();
		}
		if (this.m_grenadeAiming)
		{
			SpriteAnimatorWithTransitions animator = this.m_sein.Animation.Animator;
			TextureAnimation currentAnimation = animator.CurrentAnimation;
			if (currentAnimation.AnimationMetaData)
			{
				this.PositionGrenadeAiming(currentAnimation.AnimationMetaData, (int)animator.TextureAnimator.Frame);
			}
			else if (this.IsGrabbingWall)
			{
				this.PositionGrenadeAiming(this.WallAimingMetaData, this.PickAnimationIndex(this.WallAimingAnimations.Length));
			}
			else
			{
				this.PositionGrenadeAiming(this.AimingMetaData, this.PickAnimationIndex(this.AimingAnimations.Length));
			}
		}
	}

	// Token: 0x06001C89 RID: 7305 RVA: 0x0007BE78 File Offset: 0x0007A078
	private void PositionGrenadeAiming(AnimationMetaData metaData, int frame)
	{
		AnimationMetaData.AnimationData animationData = metaData.FindData("#grenade");
		if (animationData != null)
		{
			Vector3 positionAtFrame = animationData.GetPositionAtFrame(frame);
			this.m_grenadeAiming.transform.position = this.m_sein.PlatformBehaviour.Visuals.Sprite.transform.TransformPoint(positionAtFrame);
		}
	}

	// Token: 0x06001C8A RID: 7306 RVA: 0x0007BED0 File Offset: 0x0007A0D0
	public void EndAiming()
	{
		this.m_lockAimAnimationRemainingTime = 0f;
		this.m_isAiming = false;
		if (this.m_sein.Abilities.GrabWall)
		{
			this.m_sein.Abilities.GrabWall.LockVerticalMovement = false;
		}
		if (this.m_grenadeAiming)
		{
			this.m_grenadeAiming.GetComponent<TransparencyAnimator>().AnimatorDriver.ContinueBackwards();
		}
		this.Trajectory.HideTrajectory();
		if (this.AimingSound)
		{
			this.AimingSound.Stop();
		}
	}

	// Token: 0x06001C8B RID: 7307 RVA: 0x0007BF6C File Offset: 0x0007A16C
	private void ClampAim()
	{
		this.m_rawAimOffset.x = Mathf.Clamp(this.m_rawAimOffset.x, -this.MaxAimDistance, this.MaxAimDistance);
		if (this.IsGrabbingWall)
		{
			this.m_rawAimOffset.x = ((!this.m_faceLeft) ? Mathf.Min(0f, this.m_rawAimOffset.x) : Mathf.Max(0f, this.m_rawAimOffset.x));
		}
		float num = (this.m_rawAimOffset.y <= 0f) ? this.MinAimDistanceDown : this.MinAimDistanceUp;
		float num2 = this.MinAimDistanceHorizontal / num;
		this.m_rawAimOffset.y = this.m_rawAimOffset.y * num2;
		if (this.m_rawAimOffset.magnitude < this.MinAimDistanceHorizontal)
		{
			this.m_rawAimOffset = this.m_rawAimOffset.normalized * this.MinAimDistanceHorizontal;
		}
		this.m_rawAimOffset.y = this.m_rawAimOffset.y / num2;
		this.m_rawAimOffset.y = Mathf.Clamp(this.m_rawAimOffset.y, (!this.IsGrabbingWall) ? this.MinAimVertical : this.MinAimVerticalWall, this.MaxAimVertical);
	}

	// Token: 0x06001C8C RID: 7308 RVA: 0x0007C0B6 File Offset: 0x0007A2B6
	public void UpdateTrajectory()
	{
		this.Trajectory.StartPosition = this.GrenadeSpawnPosition;
		this.Trajectory.InitialVelocity = this.m_aimOffset;
	}

	// Token: 0x06001C8D RID: 7309 RVA: 0x0007C0E0 File Offset: 0x0007A2E0
	public float TimeToTarget(Vector2 velocity, IAttackable target)
	{
		return Mathf.Abs(target.Position.x - this.GrenadeSpawnPosition.x) / Mathf.Abs(velocity.x);
	}

	// Token: 0x06001C8E RID: 7310 RVA: 0x0007C11C File Offset: 0x0007A31C
	public bool WillRayHitEnemy(Vector2 initialVelocity, IAttackable target)
	{
		Vector3 vector = this.GrenadeSpawnPosition;
		Vector3 a = initialVelocity;
		Vector3 vector2 = vector;
		float grenadeGravity = this.GrenadeGravity;
		float num = 0f;
		float num2 = this.TimeToTarget(initialVelocity, target);
		while (num < num2)
		{
			for (int i = 0; i < 2; i++)
			{
				vector += a * 0.01666667f;
				a += Vector3.down * grenadeGravity * 0.01666667f;
				num += 0.01666667f;
			}
			Vector3 vector3 = vector - vector2;
			RaycastHit raycastHit;
			if (Physics.SphereCast(vector2, 0.5f, vector3.normalized, out raycastHit, vector3.magnitude))
			{
				break;
			}
			vector2 = vector;
		}
		return Vector3.Distance(vector2, target.Position) <= 4f;
	}

	// Token: 0x06001C8F RID: 7311 RVA: 0x0007C1F8 File Offset: 0x0007A3F8
	public bool CompareAnimations(TextureAnimationWithTransitions current, TextureAnimationWithTransitions[] array)
	{
		foreach (TextureAnimationWithTransitions x in array)
		{
			if (x == current)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001C90 RID: 7312 RVA: 0x0007C230 File Offset: 0x0007A430
	public Func<bool> AnimationRule(SeinGrenadeAttack.FastThrowAnimationRule.AnimationRule rule)
	{
		if (rule == SeinGrenadeAttack.FastThrowAnimationRule.AnimationRule.InAir)
		{
			return new Func<bool>(this.KeepPlayingAirThrowAnimation);
		}
		if (rule != SeinGrenadeAttack.FastThrowAnimationRule.AnimationRule.OnGround)
		{
			return null;
		}
		return new Func<bool>(this.KeepPlayingGroundThrowAnimation);
	}

	// Token: 0x06001C91 RID: 7313 RVA: 0x0007C26C File Offset: 0x0007A46C
	public void PlayFastThrowAnimation()
	{
		TextureAnimation currentAnimation = this.m_sein.PlatformBehaviour.Visuals.Animation.Animator.CurrentAnimation;
		TextureAnimationWithTransitions currentTextureAnimationTransitions = this.m_sein.PlatformBehaviour.Visuals.Animation.Animator.CurrentTextureAnimationTransitions;
		foreach (SeinGrenadeAttack.FastThrowAnimationRule fastThrowAnimationRule in this.FastThrowAnimations)
		{
			if (fastThrowAnimationRule.Animations.Contains(currentAnimation))
			{
				this.m_sein.Animation.Play(fastThrowAnimationRule.ThrowAnimation, 10, this.AnimationRule(fastThrowAnimationRule.PlayRule));
				return;
			}
		}
		foreach (SeinGrenadeAttack.FastThrowAnimationRule fastThrowAnimationRule2 in this.FastThrowAnimations)
		{
			if (fastThrowAnimationRule2.AnimationsWithTransitions.Contains(currentTextureAnimationTransitions))
			{
				this.m_sein.Animation.Play(fastThrowAnimationRule2.ThrowAnimation, 10, this.AnimationRule(fastThrowAnimationRule2.PlayRule));
				break;
			}
		}
	}

	// Token: 0x06001C92 RID: 7314 RVA: 0x0007C3C0 File Offset: 0x0007A5C0
	public bool KeepPlayingAirThrowAnimation()
	{
		return this.m_sein.PlatformBehaviour.PlatformMovement.IsInAir;
	}

	// Token: 0x06001C93 RID: 7315 RVA: 0x0007C3D7 File Offset: 0x0007A5D7
	public bool KeepPlayingGroundThrowAnimation()
	{
		return this.m_sein.PlatformBehaviour.PlatformMovement.IsOnGround;
	}

	// Token: 0x06001C94 RID: 7316 RVA: 0x0007C3F0 File Offset: 0x0007A5F0
	public void UpdateNormal()
	{
		this.m_lockPressingInputTime -= Time.deltaTime;
		this.m_autoTarget = null;
		if (Core.Input.LeftShoulder.OnPressed && this.m_lockPressingInputTime <= 0f)
		{
			this.m_inputPressed = true;
		}
		if (Core.Input.LeftShoulder.Released)
		{
			this.m_inputPressed = false;
		}
		this.RefreshListOfQuickSpiritGrenades();
		if (Core.Input.LeftShoulder.Pressed && this.m_lockPressingInputTime <= 0f && this.HasEnoughEnergy && this.CanAim)
		{
			this.m_inputPressed = false;
			this.SpendEnergy();
			this.BeginAiming();
			this.UpdateTrajectory();
			this.Trajectory.ShowTrajectory();
		}
		if (this.m_inputPressed)
		{
			if (!this.HasEnoughEnergy)
			{
				this.m_inputPressed = false;
				UI.SeinUI.ShakeEnergyOrbBar();
				if (this.NotEnoughEnergySound)
				{
					Sound.Play(this.NotEnoughEnergySound.GetSound(null), base.transform.position, null);
				}
				this.m_sein.Animation.Play(this.PickAnimation((!this.IsGrabbingWall) ? this.NotEnoughEnergyThrowAnimations : this.NotEnoughEnergyWallThrowAnimations), 154, new Func<bool>(this.KeepPlayingNotEnoughEnergyAnimation));
				if (this.CanAim)
				{
					Vector3 b = (!this.IsGrabbingWall) ? new Vector2(-0.5f, 0.1f) : new Vector2(-0.8f, -0.13f);
					if (this.m_sein.FaceLeft)
					{
						b.x *= -1f;
					}
					InstantiateUtility.Instantiate(this.GrenadeFailEffect, this.m_sein.Position + b, Quaternion.identity);
				}
				this.m_lockPressingInputTime = 0.2f;
			}
			else if (!this.CanAim)
			{
				this.m_autoTarget = this.FindAutoAttackable;
				if (this.m_autoTarget != null)
				{
					this.m_inputPressed = false;
					this.m_lockPressingInputTime = 0.2f;
					SpiritGrenade spiritGrenade = this.SpawnGrenade(this.VelocityToAimAtTarget(this.m_autoTarget));
					spiritGrenade.Bashable = false;
					this.SpendEnergy();
					this.PlayFastThrowAnimation();
					this.PlayThrowSound();
					this.ResetAimToDefault();
				}
				else
				{
					this.m_inputPressed = false;
					this.m_lockPressingInputTime = 0.2f;
					Vector2 quickThrowSpeed = this.QuickThrowSpeed;
					if (this.m_sein.FaceLeft)
					{
						quickThrowSpeed.x *= -1f;
					}
					SpiritGrenade spiritGrenade2 = this.SpawnGrenade(quickThrowSpeed);
					spiritGrenade2.Bashable = false;
					this.SpendEnergy();
					this.PlayFastThrowAnimation();
					this.PlayThrowSound();
					this.ResetAimToDefault();
				}
				if (this.m_sein.Abilities.Glide)
				{
					this.m_sein.Abilities.Glide.LockGliding(0.2f);
					this.m_sein.Abilities.Glide.IsGliding = false;
				}
			}
		}
	}

	// Token: 0x06001C95 RID: 7317 RVA: 0x0007C6FB File Offset: 0x0007A8FB
	public bool KeepPlayingAimAnimation()
	{
		return this.m_isAiming;
	}

	// Token: 0x06001C96 RID: 7318 RVA: 0x0007C703 File Offset: 0x0007A903
	public bool KeepPlayingThrowAnimation()
	{
		return !this.m_sein.PlatformBehaviour.PlatformMovement.MovingHorizontally;
	}

	// Token: 0x06001C97 RID: 7319 RVA: 0x0007C71D File Offset: 0x0007A91D
	public bool KeepPlayingNotEnoughEnergyAnimation()
	{
		return this.m_sein.PlatformBehaviour.PlatformMovement.LocalSpeed == Vector2.zero;
	}

	// Token: 0x06001C98 RID: 7320 RVA: 0x0007C740 File Offset: 0x0007A940
	public void BeginAiming()
	{
		this.m_sein.PlatformBehaviour.PlatformMovement.LocalSpeed = Vector2.zero;
		if (this.IsGrabbingWall)
		{
			if (!this.m_lastAimWasOnWall)
			{
				this.ResetAimToDefault();
			}
			this.m_lastAimWasOnWall = true;
			this.m_animationAimAngle = this.IndexToAnimationAngle(8, this.WallAimingAnimations.Length);
			this.m_lockAimAnimationRemainingTime = 0.3667f;
		}
		else
		{
			if (this.m_lastAimWasOnWall)
			{
				this.ResetAimToDefault();
			}
			this.m_lastAimWasOnWall = false;
			this.m_animationAimAngle = this.IndexToAnimationAngle(8, this.AimingAnimations.Length);
			this.m_lockAimAnimationRemainingTime = 0.1f;
		}
		this.m_isAiming = true;
		this.m_faceLeft = this.m_sein.FaceLeft;
		this.m_rawAimOffset.x = Mathf.Abs(this.m_rawAimOffset.x) * (float)((!this.m_sein.FaceLeft) ? 1 : -1);
		if (this.IsGrabbingWall)
		{
			this.m_rawAimOffset.x = this.m_rawAimOffset.x * -1f;
		}
		this.ClampAim();
		this.m_aimOffset = this.m_rawAimOffset;
		this.m_autoAim = true;
		this.AutoTarget();
		if (this.m_sein.Abilities.GrabWall)
		{
			this.m_sein.Abilities.GrabWall.LockVerticalMovement = true;
		}
		this.m_grenadeAiming = (GameObject)InstantiateUtility.Instantiate(this.GrenadeAiming);
		Sound.Play(this.StartAimingSound.GetSound(null), base.transform.position, null);
		if (this.AimingSound)
		{
			this.AimingSound.Play();
		}
		this.PlayAimAnimation();
	}

	// Token: 0x170004C9 RID: 1225
	// (get) Token: 0x06001C99 RID: 7321 RVA: 0x0007C8FC File Offset: 0x0007AAFC
	public IAttackable FindAutoAttackable
	{
		get
		{
			IAttackable result = null;
			int num = 0;
			float num2 = float.MaxValue;
			foreach (IAttackable attackable in Targets.Attackables)
			{
				if (attackable as Component && attackable.CanBeGrenaded() && attackable is EntityTargetting && UI.Cameras.Current.IsOnScreen(attackable.Position))
				{
					Vector2 vector = attackable.Position - this.m_sein.Position;
					float magnitude = vector.magnitude;
					int num3 = (!this.m_sein.FaceLeft) ? 1 : -1;
					if (this.IsGrabbingWall)
					{
						num3 *= -1;
					}
					int num4 = (!(((EntityTargetting)attackable).Entity is Enemy)) ? 0 : 1;
					if (magnitude > this.AutoAim.MinDistance && magnitude < this.AutoAim.MaxDistance && num3 == (int)Mathf.Sign(vector.x) && (num < num4 || (num == num4 && magnitude < num2)))
					{
						Vector2 initialVelocity = this.VelocityToAimAtTarget(attackable);
						if (this.WillRayHitEnemy(initialVelocity, attackable))
						{
							result = attackable;
							num2 = magnitude;
							num = num4;
						}
					}
				}
			}
			return result;
		}
	}

	// Token: 0x06001C9A RID: 7322 RVA: 0x0007CA80 File Offset: 0x0007AC80
	public void AutoTarget()
	{
		this.m_autoTarget = this.FindAutoAttackable;
		if (this.m_autoTarget as Component != null)
		{
			this.SetAimVelocity(this.VelocityToAimAtTarget(this.m_autoTarget));
		}
	}

	// Token: 0x06001C9B RID: 7323 RVA: 0x0007CAB8 File Offset: 0x0007ACB8
	private void SetAimVelocity(Vector2 aim)
	{
		this.m_aimOffset = aim;
		this.m_rawAimOffset = aim;
	}

	// Token: 0x06001C9C RID: 7324 RVA: 0x0007CAD8 File Offset: 0x0007ACD8
	public Vector2 VelocityToAimAtTarget(IAttackable attackable)
	{
		Vector2 vector = attackable.Position - this.m_sein.Position;
		float num = (!this.IsInAir) ? (this.AutoAim.Speed + Mathf.Abs(vector.x) * this.AutoAim.SpeedPerXDistance + Mathf.Max(0f, vector.y) * this.AutoAim.SpeedPerYDistance) : this.AutoAim.InAirSpeed;
		float num2 = vector.magnitude / num;
		return new Vector2(vector.x / num2, vector.y / num2 + this.GrenadeGravity * num2 * 0.5f);
	}

	// Token: 0x06001C9D RID: 7325 RVA: 0x0007CB8F File Offset: 0x0007AD8F
	public override void OnExit()
	{
		base.OnExit();
		this.CancelAiming();
	}

	// Token: 0x06001C9E RID: 7326 RVA: 0x0007CBA0 File Offset: 0x0007ADA0
	public void CancelAiming()
	{
		if (this.m_isAiming)
		{
			this.RestoreEnergy();
			this.EndAiming();
			Sound.Play(this.StopAimingSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x0400189E RID: 6302
	public GameObject Grenade;

	// Token: 0x0400189F RID: 6303
	public GameObject GrenadeUpgraded;

	// Token: 0x040018A0 RID: 6304
	public GameObject GrenadeAiming;

	// Token: 0x040018A1 RID: 6305
	private GameObject m_grenadeAiming;

	// Token: 0x040018A2 RID: 6306
	public SeinGrenadeTrajectory Trajectory;

	// Token: 0x040018A3 RID: 6307
	public AnimationCurve AimSpeed;

	// Token: 0x040018A4 RID: 6308
	public float MaxAimDistance;

	// Token: 0x040018A5 RID: 6309
	public float MinAimDistanceUp;

	// Token: 0x040018A6 RID: 6310
	public float MinAimDistanceDown;

	// Token: 0x040018A7 RID: 6311
	public float MinAimDistanceHorizontal;

	// Token: 0x040018A8 RID: 6312
	public float MaxAimVertical = 50f;

	// Token: 0x040018A9 RID: 6313
	public float MinAimVertical = 2f;

	// Token: 0x040018AA RID: 6314
	public float MinAimVerticalWall = -30f;

	// Token: 0x040018AB RID: 6315
	public int MaxSpamGrenades = 3;

	// Token: 0x040018AC RID: 6316
	public float EnergyCost = 1f;

	// Token: 0x040018AD RID: 6317
	public SoundProvider NotEnoughEnergySound;

	// Token: 0x040018AE RID: 6318
	public SoundProvider TurnAroundAimingSound;

	// Token: 0x040018AF RID: 6319
	public SoundProvider ThrowGrenadeSound;

	// Token: 0x040018B0 RID: 6320
	public SoundProvider StopAimingSound;

	// Token: 0x040018B1 RID: 6321
	public SoundProvider StartAimingSound;

	// Token: 0x040018B2 RID: 6322
	public SoundSource AimingSound;

	// Token: 0x040018B3 RID: 6323
	public Vector2 QuickThrowSpeed = new Vector2(14f, 16f);

	// Token: 0x040018B4 RID: 6324
	public GameObject GrenadeFailEffect;

	// Token: 0x040018B5 RID: 6325
	public float AimAnimationAngleOffset = 5f;

	// Token: 0x040018B6 RID: 6326
	public float CursorSpeedMultiplier = 1f;

	// Token: 0x040018B7 RID: 6327
	public float CursorSpeedYOffset = 12f;

	// Token: 0x040018B8 RID: 6328
	private float m_lockPressingInputTime;

	// Token: 0x040018B9 RID: 6329
	private Vector2 m_rawAimOffset = new Vector2(14f, 16f);

	// Token: 0x040018BA RID: 6330
	private SeinCharacter m_sein;

	// Token: 0x040018BB RID: 6331
	private bool m_isAiming;

	// Token: 0x040018BC RID: 6332
	private Vector2 m_aimOffset;

	// Token: 0x040018BD RID: 6333
	private List<SpiritGrenade> m_spiritGrenades = new List<SpiritGrenade>();

	// Token: 0x040018BE RID: 6334
	private float m_animationAimAngle;

	// Token: 0x040018BF RID: 6335
	private bool m_lastAimWasOnWall;

	// Token: 0x040018C0 RID: 6336
	public TextureAnimationWithTransitions[] AimingAnimations;

	// Token: 0x040018C1 RID: 6337
	public TextureAnimationWithTransitions[] ThrowAnimations;

	// Token: 0x040018C2 RID: 6338
	public TextureAnimationWithTransitions[] WallAimingAnimations;

	// Token: 0x040018C3 RID: 6339
	public TextureAnimationWithTransitions[] WallThrowAnimations;

	// Token: 0x040018C4 RID: 6340
	public TextureAnimationWithTransitions[] NotEnoughEnergyThrowAnimations;

	// Token: 0x040018C5 RID: 6341
	public TextureAnimationWithTransitions[] NotEnoughEnergyWallThrowAnimations;

	// Token: 0x040018C6 RID: 6342
	public SeinGrenadeAttack.QuickThrowAnimations QuickThrow;

	// Token: 0x040018C7 RID: 6343
	public AnimationMetaData WallAimingMetaData;

	// Token: 0x040018C8 RID: 6344
	public AnimationMetaData AimingMetaData;

	// Token: 0x040018C9 RID: 6345
	private float m_lockAimAnimationRemainingTime;

	// Token: 0x040018CA RID: 6346
	private bool m_faceLeft;

	// Token: 0x040018CB RID: 6347
	public float MaxAimWallAnimationAngle = 85f;

	// Token: 0x040018CC RID: 6348
	public float MinAimWallAnimationAngle = -80f;

	// Token: 0x040018CD RID: 6349
	public float MaxAimGroundAnimationAngle = 90f;

	// Token: 0x040018CE RID: 6350
	public float MinAimGroundAnimationAngle = -30f;

	// Token: 0x040018CF RID: 6351
	private bool m_inputPressed;

	// Token: 0x040018D0 RID: 6352
	public List<SeinGrenadeAttack.FastThrowAnimationRule> FastThrowAnimations;

	// Token: 0x040018D1 RID: 6353
	private bool m_autoAim;

	// Token: 0x040018D2 RID: 6354
	private IAttackable m_autoTarget;

	// Token: 0x040018D3 RID: 6355
	public SeinGrenadeAttack.AutoAimSettings AutoAim;

	// Token: 0x0200044B RID: 1099
	[Serializable]
	public class QuickThrowAnimations
	{
		// Token: 0x04001A53 RID: 6739
		public TextureAnimationWithTransitions FallIdleThrowAnimation;

		// Token: 0x04001A54 RID: 6740
		public TextureAnimationWithTransitions FallThrowAnimation;

		// Token: 0x04001A55 RID: 6741
		public TextureAnimationWithTransitions RunThrowAnimation;

		// Token: 0x04001A56 RID: 6742
		public TextureAnimationWithTransitions JogThrowAnimation;

		// Token: 0x04001A57 RID: 6743
		public TextureAnimationWithTransitions WalkThrowAnimation;

		// Token: 0x04001A58 RID: 6744
		public TextureAnimationWithTransitions JumpThrowAnimation;

		// Token: 0x04001A59 RID: 6745
		public TextureAnimationWithTransitions JumpIdleThrowAnimation;

		// Token: 0x04001A5A RID: 6746
		public TextureAnimationWithTransitions IdleThrowAnimation;

		// Token: 0x04001A5B RID: 6747
		public TextureAnimationWithTransitions WallThrowAnimation;
	}

	// Token: 0x0200044C RID: 1100
	[Serializable]
	public class FastThrowAnimationRule
	{
		// Token: 0x04001A5C RID: 6748
		public TextureAnimationWithTransitions ThrowAnimation;

		// Token: 0x04001A5D RID: 6749
		public List<TextureAnimationWithTransitions> AnimationsWithTransitions;

		// Token: 0x04001A5E RID: 6750
		public List<TextureAnimation> Animations;

		// Token: 0x04001A5F RID: 6751
		public SeinGrenadeAttack.FastThrowAnimationRule.AnimationRule PlayRule;

		// Token: 0x0200044D RID: 1101
		public enum AnimationRule
		{
			// Token: 0x04001A61 RID: 6753
			InAir,
			// Token: 0x04001A62 RID: 6754
			OnGround
		}
	}

	// Token: 0x0200044E RID: 1102
	[Serializable]
	public class AutoAimSettings
	{
		// Token: 0x04001A63 RID: 6755
		public float MaxDistance = 30f;

		// Token: 0x04001A64 RID: 6756
		public float MinDistance = 2f;

		// Token: 0x04001A65 RID: 6757
		public float Speed = 5f;

		// Token: 0x04001A66 RID: 6758
		public float SpeedPerXDistance = 0.7f;

		// Token: 0x04001A67 RID: 6759
		public float SpeedPerYDistance = 2f;

		// Token: 0x04001A68 RID: 6760
		public float InAirSpeed = 30f;
	}
}
