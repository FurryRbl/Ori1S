using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000414 RID: 1044
public class SeinDashAttack : CharacterState, ISeinReceiver
{
	// Token: 0x06001CB1 RID: 7345 RVA: 0x0007D270 File Offset: 0x0007B470
	// Note: this type is marked as 'beforefieldinit'.
	static SeinDashAttack()
	{
		SeinDashAttack.OnDashEvent = delegate()
		{
		};
		SeinDashAttack.OnWallDashEvent = delegate()
		{
		};
	}

	// Token: 0x14000031 RID: 49
	// (add) Token: 0x06001CB2 RID: 7346 RVA: 0x0007D2C7 File Offset: 0x0007B4C7
	// (remove) Token: 0x06001CB3 RID: 7347 RVA: 0x0007D2DE File Offset: 0x0007B4DE
	public static event Action OnDashEvent;

	// Token: 0x14000032 RID: 50
	// (add) Token: 0x06001CB4 RID: 7348 RVA: 0x0007D2F5 File Offset: 0x0007B4F5
	// (remove) Token: 0x06001CB5 RID: 7349 RVA: 0x0007D30C File Offset: 0x0007B50C
	public static event Action OnWallDashEvent;

	// Token: 0x170004D2 RID: 1234
	// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0007D323 File Offset: 0x0007B523
	public bool HasEnoughEnergy
	{
		get
		{
			return this.m_sein.Energy.CanAfford(this.EnergyCost);
		}
	}

	// Token: 0x06001CB7 RID: 7351 RVA: 0x0007D33B File Offset: 0x0007B53B
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.ReturnToNormal();
		}
	}

	// Token: 0x06001CB8 RID: 7352 RVA: 0x0007D34E File Offset: 0x0007B54E
	public override void OnExit()
	{
		this.ReturnToNormal();
		base.OnExit();
	}

	// Token: 0x06001CB9 RID: 7353 RVA: 0x0007D35C File Offset: 0x0007B55C
	public void OnDisable()
	{
		this.Exit();
	}

	// Token: 0x06001CBA RID: 7354 RVA: 0x0007D364 File Offset: 0x0007B564
	public void ReturnToNormal()
	{
		if (this.CurrentState != SeinDashAttack.State.Normal)
		{
			if (this.CurrentState == SeinDashAttack.State.Dashing)
			{
				this.m_sein.PlatformBehaviour.PlatformMovement.LocalSpeedX = (float)((!this.m_faceLeft) ? 1 : -1) * this.DashSpeedOverTime.Evaluate((float)this.DashSpeedOverTime.length);
			}
			if (this.CurrentState == SeinDashAttack.State.ChargeDashing)
			{
				this.m_sein.PlatformBehaviour.PlatformMovement.LocalSpeedX = (float)((!this.m_faceLeft) ? 1 : -1) * this.ChargeDashSpeedOverTime.Evaluate((float)this.ChargeDashSpeedOverTime.length);
			}
			UI.Cameras.Current.ChaseTarget.CameraSpeedMultiplier.x = 1f;
			if (this.CurrentState == SeinDashAttack.State.ChargeDashing)
			{
				this.RestoreEnergy();
			}
			this.ChangeState(SeinDashAttack.State.Normal);
		}
	}

	// Token: 0x06001CBB RID: 7355 RVA: 0x0007D446 File Offset: 0x0007B646
	public void SpendEnergy()
	{
		this.m_sein.Energy.Spend(this.EnergyCost);
	}

	// Token: 0x06001CBC RID: 7356 RVA: 0x0007D45E File Offset: 0x0007B65E
	public void RestoreEnergy()
	{
		this.m_sein.Energy.Gain(this.EnergyCost);
	}

	// Token: 0x06001CBD RID: 7357 RVA: 0x0007D476 File Offset: 0x0007B676
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
		sein.Abilities.Dash = this;
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x0007D48B File Offset: 0x0007B68B
	public override void UpdateCharacterState()
	{
		this.UpdateState();
	}

	// Token: 0x170004D3 RID: 1235
	// (get) Token: 0x06001CBF RID: 7359 RVA: 0x0007D493 File Offset: 0x0007B693
	public bool IsDashingOrChangeDashing
	{
		get
		{
			if (this.CurrentState == SeinDashAttack.State.Dashing)
			{
				return this.m_stateCurrentTime < this.DashTime;
			}
			return this.CurrentState == SeinDashAttack.State.ChargeDashing && this.m_stateCurrentTime < this.ChargeDashTime;
		}
	}

	// Token: 0x06001CC0 RID: 7360 RVA: 0x0007D4CC File Offset: 0x0007B6CC
	public void ChangeState(SeinDashAttack.State state)
	{
		this.CurrentState = state;
		this.m_stateCurrentTime = 0f;
		this.m_attackablesIgnore.Clear();
	}

	// Token: 0x170004D4 RID: 1236
	// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x0007D4EC File Offset: 0x0007B6EC
	public IChargeDashAttackable FindClosestAttackable
	{
		get
		{
			IChargeDashAttackable result = null;
			float num = float.MaxValue;
			foreach (IAttackable attackable in Targets.Attackables)
			{
				if (attackable as Component && attackable.CanBeChargeDashed() && attackable is IChargeDashAttackable)
				{
					IChargeDashAttackable chargeDashAttackable = (IChargeDashAttackable)attackable;
					if (UI.Cameras.Current.IsOnScreen(attackable.Position))
					{
						float magnitude = (attackable.Position - this.m_sein.Position).magnitude;
						if (magnitude < num && magnitude < this.ChargeDashTargetMaxDistance)
						{
							result = chargeDashAttackable;
							num = magnitude;
						}
					}
				}
			}
			return result;
		}
	}

	// Token: 0x06001CC2 RID: 7362 RVA: 0x0007D5CC File Offset: 0x0007B7CC
	public void AttackNearbyEnemies()
	{
		for (int i = 0; i < Targets.Attackables.Count; i++)
		{
			IAttackable attackable = Targets.Attackables[i];
			if (!InstantiateUtility.IsDestroyed(attackable as Component))
			{
				if (!this.m_attackablesIgnore.Contains(attackable))
				{
					if (attackable.CanBeChargeFlamed())
					{
						float magnitude = (attackable.Position - this.m_sein.PlatformBehaviour.PlatformMovement.HeadPosition).magnitude;
						if (magnitude <= 3f)
						{
							this.m_attackablesIgnore.Add(attackable);
							Vector3 v = (!this.m_chargeDashAtTarget) ? (((!this.m_faceLeft) ? Vector3.right : Vector3.left) * 3f) : (this.m_chargeDashDirection * 3f);
							Damage damage = new Damage((float)this.Damage, v, this.m_sein.Position, DamageType.ChargeFlame, base.gameObject);
							damage.DealToComponents(((Component)attackable).gameObject);
							this.m_hasHitAttackable = true;
							if (this.ExplosionEffect && Time.time - this.m_timeOfLastExplosionEffect > 0.1f)
							{
								this.m_timeOfLastExplosionEffect = Time.time;
								InstantiateUtility.Instantiate(this.ExplosionEffect, Vector3.Lerp(base.transform.position, attackable.Position, 0.5f), Quaternion.identity);
							}
							break;
						}
					}
				}
			}
		}
	}

	// Token: 0x06001CC3 RID: 7363 RVA: 0x0007D76C File Offset: 0x0007B96C
	private void PerformDash(TextureAnimationWithTransitions dashAnimation, SoundProvider dashSound)
	{
		this.m_sein.Mortality.DamageReciever.ResetInviciblity();
		this.m_hasDashed = true;
		this.m_isOnGround = this.m_sein.IsOnGround;
		this.m_lastDashTime = Time.time;
		this.m_lastPressTime = 0f;
		this.SpriteRotation = this.m_sein.PlatformBehaviour.PlatformMovement.GroundAngle;
		this.m_allowNoDecelerationForThisDash = true;
		if (this.m_chargeDashAtTarget)
		{
			this.m_faceLeft = (this.m_chargeDashDirection.x < 0f);
		}
		else if (this.m_sein.PlatformBehaviour.PlatformMovement.HasWallLeft)
		{
			this.m_faceLeft = false;
		}
		else if (this.m_sein.PlatformBehaviour.PlatformMovement.HasWallRight)
		{
			this.m_faceLeft = true;
		}
		else if (this.m_sein.Input.NormalizedHorizontal != 0)
		{
			this.m_faceLeft = (this.m_sein.Input.NormalizedHorizontal < 0);
		}
		else if (!Mathf.Approximately(this.m_sein.Speed.x, 0f))
		{
			this.m_faceLeft = (this.m_sein.Speed.x < 0f);
		}
		else
		{
			this.m_faceLeft = this.m_sein.FaceLeft;
			this.m_allowNoDecelerationForThisDash = false;
		}
		this.m_sein.FaceLeft = this.m_faceLeft;
		this.m_stopAnimation = false;
		if (dashSound)
		{
			Sound.Play(dashSound.GetSound(null), this.m_sein.Position, null);
		}
		this.m_sein.Animation.Play(dashAnimation, 154, new Func<bool>(this.KeepDashAnimationPlaying));
		if (SeinDashAttack.RainbowDashActivated)
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.DashFollowRainbowEffect, this.m_sein.Position, Quaternion.identity);
			gameObject.transform.parent = this.m_sein.Transform;
		}
		this.m_sein.PlatformBehaviour.PlatformMovement.LocalSpeedY = -this.DashDownwardSpeed;
	}

	// Token: 0x06001CC4 RID: 7364 RVA: 0x0007D9A0 File Offset: 0x0007BBA0
	public void PerformDash()
	{
		this.m_chargeDashAtTarget = false;
		SoundProvider dashSound = (!SeinDashAttack.RainbowDashActivated) ? this.DashSound : this.RainbowDashSound;
		bool isGliding = this.m_sein.Controller.IsGliding;
		this.PerformDash((!isGliding) ? this.DashAnimation : this.GlideDashAnimation, dashSound);
		this.ChangeState(SeinDashAttack.State.Dashing);
		this.UpdateDashing();
		SeinDashAttack.OnDashEvent();
	}

	// Token: 0x06001CC5 RID: 7365 RVA: 0x0007DA18 File Offset: 0x0007BC18
	public void PerformWallDash()
	{
		this.m_chargeDashAtTarget = false;
		SoundProvider dashSound = (!SeinDashAttack.RainbowDashActivated) ? this.DashSound : this.RainbowDashSound;
		this.PerformDash(this.DashAnimation, dashSound);
		this.ChangeState(SeinDashAttack.State.Dashing);
		this.UpdateDashing();
		SeinDashAttack.OnWallDashEvent();
	}

	// Token: 0x06001CC6 RID: 7366 RVA: 0x0007DA6C File Offset: 0x0007BC6C
	public void PerformDashIntoWall()
	{
		this.m_lastPressTime = 0f;
		this.m_lastDashTime = Time.time;
		this.m_sein.Animation.Play(this.DashIntoWallAnimation, 154, new Func<bool>(this.KeepDashIntoWallAnimationPlaying));
		Sound.Play(this.DashIntoWallSound.GetSound(null), this.m_sein.Position, null);
	}

	// Token: 0x06001CC7 RID: 7367 RVA: 0x0007DAD5 File Offset: 0x0007BCD5
	public bool KeepDashIntoWallAnimationPlaying()
	{
		return this.AgainstWall() && this.m_sein.IsOnGround;
	}

	// Token: 0x06001CC8 RID: 7368 RVA: 0x0007DAF0 File Offset: 0x0007BCF0
	public void PerformChargeDash()
	{
		this.m_hasHitAttackable = false;
		this.m_chargeJumpWasReleased = false;
		this.m_chargeDashAttackTarget = (this.FindClosestAttackable as IAttackable);
		if (this.m_chargeDashAttackTarget != null)
		{
			this.m_chargeDashAtTarget = true;
			this.m_chargeDashDirection = (this.m_chargeDashAttackTarget.Position - this.m_sein.Position).normalized;
			this.m_chargeDashAtTargetPosition = this.m_chargeDashAttackTarget.Position;
		}
		else
		{
			this.m_chargeDashAtTarget = false;
		}
		SoundProvider dashSound = (!SeinDashAttack.RainbowDashActivated) ? this.ChargeDashSound : this.RainbowDashSound;
		this.PerformDash(this.ChargeDashAnimation, dashSound);
		if (this.m_chargeDashAtTarget)
		{
			this.SpriteRotation = Mathf.Atan2(this.m_chargeDashDirection.y, this.m_chargeDashDirection.x) * 57.29578f - (float)((!this.m_faceLeft) ? 0 : 180);
		}
		this.ChangeState(SeinDashAttack.State.ChargeDashing);
		this.CompleteChargeEffect();
		this.UpdateChargeDashing();
	}

	// Token: 0x06001CC9 RID: 7369 RVA: 0x0007DBFA File Offset: 0x0007BDFA
	private bool HasChargeDashSkill()
	{
		return this.m_sein.PlayerAbilities.ChargeDash.HasAbility;
	}

	// Token: 0x06001CCA RID: 7370 RVA: 0x0007DC11 File Offset: 0x0007BE11
	private bool HasAirDashSkill()
	{
		return this.m_sein.PlayerAbilities.AirDash.HasAbility;
	}

	// Token: 0x06001CCB RID: 7371 RVA: 0x0007DC28 File Offset: 0x0007BE28
	private bool CanChargeDash()
	{
		return this.HasChargeDashSkill() && Core.Input.ChargeJump.Pressed && this.m_chargeJumpWasReleased;
	}

	// Token: 0x06001CCC RID: 7372 RVA: 0x0007DC4D File Offset: 0x0007BE4D
	public void CompleteChargeEffect()
	{
		if (this.m_sein.Abilities.ChargeJumpCharging)
		{
			this.m_sein.Abilities.ChargeJumpCharging.EndCharge();
		}
	}

	// Token: 0x06001CCD RID: 7373 RVA: 0x0007DC80 File Offset: 0x0007BE80
	private void UpdateTargetHighlight(IChargeDashAttackable target)
	{
		if (this.m_lastTarget == target)
		{
			return;
		}
		if (!InstantiateUtility.IsDestroyed(this.m_lastTarget as Component))
		{
			this.m_lastTarget.OnChargeDashDehighlight();
		}
		this.m_lastTarget = target;
		if (!InstantiateUtility.IsDestroyed(this.m_lastTarget as Component))
		{
			this.m_lastTarget.OnChargeDashHighlight();
		}
	}

	// Token: 0x06001CCE RID: 7374 RVA: 0x0007DCE4 File Offset: 0x0007BEE4
	public bool KeepDashAnimationPlaying()
	{
		return !this.m_stopAnimation && !this.m_sein.Abilities.WallSlide.IsOnWall && base.Active;
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x0007DD1F File Offset: 0x0007BF1F
	public bool KeepChargeDashAnimationPlaying()
	{
		return this.KeepDashAnimationPlaying();
	}

	// Token: 0x06001CD0 RID: 7376 RVA: 0x0007DD28 File Offset: 0x0007BF28
	public bool AgainstWall()
	{
		PlatformMovement platformMovement = this.m_sein.PlatformBehaviour.PlatformMovement;
		return (platformMovement.HasWallLeft && this.m_sein.FaceLeft) || (platformMovement.HasWallRight && !this.m_sein.FaceLeft);
	}

	// Token: 0x06001CD1 RID: 7377 RVA: 0x0007DD84 File Offset: 0x0007BF84
	public bool CanPerformNormalDash()
	{
		return (this.HasAirDashSkill() || this.m_sein.IsOnGround) && (!this.AgainstWall() && this.DashHasCooledDown) && !this.m_hasDashed;
	}

	// Token: 0x170004D5 RID: 1237
	// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x0007DDD0 File Offset: 0x0007BFD0
	private bool DashHasCooledDown
	{
		get
		{
			return Time.time - this.m_lastDashTime > 0.4f;
		}
	}

	// Token: 0x06001CD3 RID: 7379 RVA: 0x0007DDE5 File Offset: 0x0007BFE5
	public bool CanPerformDashIntoWall()
	{
		return this.m_sein.IsOnGround && this.AgainstWall() && this.DashHasCooledDown;
	}

	// Token: 0x06001CD4 RID: 7380 RVA: 0x0007DE0C File Offset: 0x0007C00C
	public bool CanWallDash()
	{
		PlatformMovement platformMovement = this.m_sein.PlatformBehaviour.PlatformMovement;
		bool flag = (platformMovement.HasWallLeft && this.m_sein.Input.Horizontal >= 0f) || (platformMovement.HasWallRight && this.m_sein.Input.Horizontal <= 0f);
		return flag && !this.m_sein.IsOnGround && this.m_sein.PlayerAbilities.AirDash.HasAbility;
	}

	// Token: 0x06001CD5 RID: 7381 RVA: 0x0007DEAC File Offset: 0x0007C0AC
	public void UpdateNormal()
	{
		float num = Time.time - this.m_lastPressTime;
		if (this.m_sein.IsOnGround)
		{
			this.m_hasDashed = false;
		}
		if (Core.Input.Glide.Pressed && this.m_timeWhenDashJumpHappened + 5f > Time.time)
		{
			this.m_timeWhenDashJumpHappened = 0f;
			PlatformMovement platformMovement = this.m_sein.PlatformBehaviour.PlatformMovement;
			float num2 = this.OffGroundSpeed - 2f;
			if (Mathf.Abs(platformMovement.LocalSpeedX) > num2)
			{
				platformMovement.LocalSpeedX = Mathf.Sign(platformMovement.LocalSpeedX) * num2;
			}
		}
		IChargeDashAttackable target;
		if (this.CanChargeDash())
		{
			IChargeDashAttackable findClosestAttackable = this.FindClosestAttackable;
			target = findClosestAttackable;
		}
		else
		{
			target = null;
		}
		this.UpdateTargetHighlight(target);
		if (Core.Input.RightShoulder.Pressed && num < 0.15f)
		{
			if (this.CanChargeDash())
			{
				if (this.HasEnoughEnergy)
				{
					this.SpendEnergy();
					this.PerformChargeDash();
				}
				else
				{
					this.ShowNotEnoughEnergy();
					this.m_lastPressTime = 0f;
				}
			}
			else if (this.CanPerformNormalDash())
			{
				this.PerformDash();
			}
			else if (this.CanWallDash())
			{
				this.PerformWallDash();
			}
			else if (this.CanPerformDashIntoWall())
			{
				this.PerformDashIntoWall();
			}
		}
	}

	// Token: 0x06001CD6 RID: 7382 RVA: 0x0007E004 File Offset: 0x0007C204
	private void ShowNotEnoughEnergy()
	{
		UI.SeinUI.ShakeEnergyOrbBar();
		if (this.NotEnoughEnergySound)
		{
			Sound.Play(this.NotEnoughEnergySound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x06001CD7 RID: 7383 RVA: 0x0007E04C File Offset: 0x0007C24C
	public void UpdateDashing()
	{
		PlatformMovement platformMovement = this.m_sein.PlatformBehaviour.PlatformMovement;
		UI.Cameras.Current.ChaseTarget.CameraSpeedMultiplier.x = Mathf.Clamp01(this.m_stateCurrentTime / this.DashTime);
		platformMovement.LocalSpeedX = (float)((!this.m_faceLeft) ? 1 : -1) * this.DashSpeedOverTime.Evaluate(this.m_stateCurrentTime);
		this.m_sein.FaceLeft = this.m_faceLeft;
		if (this.AgainstWall())
		{
			platformMovement.LocalSpeed = Vector2.zero;
		}
		this.SpriteRotation = Mathf.Lerp(this.SpriteRotation, this.m_sein.PlatformBehaviour.PlatformMovement.GroundAngle, 0.2f);
		if (this.m_sein.IsOnGround)
		{
			if (Core.Input.Horizontal > 0f && this.m_faceLeft)
			{
				this.StopDashing();
			}
			if (Core.Input.Horizontal < 0f && !this.m_faceLeft)
			{
				this.StopDashing();
			}
		}
		if (this.m_stateCurrentTime > this.DashTime)
		{
			if (platformMovement.IsOnGround && Core.Input.Horizontal == 0f)
			{
				platformMovement.LocalSpeedX = 0f;
			}
			this.ChangeState(SeinDashAttack.State.Normal);
		}
		if (Core.Input.Jump.OnPressed || Core.Input.Glide.OnPressed)
		{
			platformMovement.LocalSpeedX = ((!this.m_faceLeft) ? this.OffGroundSpeed : (-this.OffGroundSpeed));
			this.m_sein.PlatformBehaviour.AirNoDeceleration.NoDeceleration = this.m_allowNoDecelerationForThisDash;
			this.m_stopAnimation = true;
			this.ChangeState(SeinDashAttack.State.Normal);
			this.m_timeWhenDashJumpHappened = Time.time;
		}
		if (this.RaycastTest() && this.m_isOnGround)
		{
			this.StickOntoGround();
		}
		else
		{
			this.m_isOnGround = false;
		}
	}

	// Token: 0x06001CD8 RID: 7384 RVA: 0x0007E238 File Offset: 0x0007C438
	private void StickOntoGround()
	{
		PlatformMovement platformMovement = this.m_sein.PlatformBehaviour.PlatformMovement;
		Vector3 vector = platformMovement.Position;
		platformMovement.PlaceOnGround(0f, 8f);
		Vector3 vector2 = vector;
		platformMovement.PlaceOnGround(0.5f, 8f);
		Vector3 vector3 = vector;
		vector = vector2;
		if (vector3.y > vector2.y)
		{
			vector = vector3;
		}
		platformMovement.Position = vector;
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x0007E2A0 File Offset: 0x0007C4A0
	public void UpdateChargeDashing()
	{
		PlatformMovement platformMovement = this.m_sein.PlatformBehaviour.PlatformMovement;
		this.AttackNearbyEnemies();
		this.m_sein.Mortality.DamageReciever.MakeInvincibleToEnemies(1f);
		if (this.m_chargeDashAtTarget)
		{
			platformMovement.LocalSpeed = this.m_chargeDashDirection * this.ChargeDashSpeedOverTime.Evaluate(this.m_stateCurrentTime);
		}
		else
		{
			platformMovement.LocalSpeedX = (float)((!this.m_faceLeft) ? 1 : -1) * this.ChargeDashSpeedOverTime.Evaluate(this.m_stateCurrentTime);
		}
		if (this.m_hasHitAttackable)
		{
			platformMovement.LocalSpeed *= 0.33f;
		}
		this.m_sein.FaceLeft = this.m_faceLeft;
		this.SpriteRotation = Mathf.Lerp(this.SpriteRotation, this.m_sein.PlatformBehaviour.PlatformMovement.GroundAngle, 0.3f);
		if (this.AgainstWall())
		{
			platformMovement.LocalSpeed = Vector2.zero;
		}
		if (this.m_sein.IsOnGround)
		{
			if (Core.Input.Horizontal > 0f && this.m_faceLeft)
			{
				this.StopDashing();
			}
			if (Core.Input.Horizontal < 0f && !this.m_faceLeft)
			{
				this.StopDashing();
			}
		}
		if (this.m_stateCurrentTime > this.ChargeDashTime)
		{
			this.ChangeState(SeinDashAttack.State.Normal);
		}
		if (Core.Input.Jump.OnPressed || Core.Input.Glide.OnPressed)
		{
			platformMovement.LocalSpeedX = ((!this.m_faceLeft) ? this.OffGroundSpeed : (-this.OffGroundSpeed));
			this.m_sein.PlatformBehaviour.AirNoDeceleration.NoDeceleration = true;
			this.m_stopAnimation = true;
			this.ChangeState(SeinDashAttack.State.Normal);
		}
		if (this.RaycastTest() && this.m_isOnGround && !this.m_chargeDashAtTarget)
		{
			this.StickOntoGround();
		}
		else
		{
			this.m_isOnGround = false;
		}
	}

	// Token: 0x06001CDA RID: 7386 RVA: 0x0007E4B4 File Offset: 0x0007C6B4
	public void UpdateState()
	{
		UI.Cameras.Current.ChaseTarget.CameraSpeedMultiplier.x = 1f;
		if (Core.Input.RightShoulder.OnPressed)
		{
			this.m_lastPressTime = Time.time;
		}
		if (Core.Input.ChargeJump.Released)
		{
			this.m_chargeJumpWasReleased = true;
		}
		switch (this.CurrentState)
		{
		case SeinDashAttack.State.Normal:
			this.UpdateNormal();
			break;
		case SeinDashAttack.State.Dashing:
			this.UpdateDashing();
			break;
		case SeinDashAttack.State.ChargeDashing:
			this.UpdateChargeDashing();
			break;
		}
		this.m_stateCurrentTime += Time.deltaTime;
	}

	// Token: 0x06001CDB RID: 7387 RVA: 0x0007E55C File Offset: 0x0007C75C
	public void StopDashing()
	{
		PlatformMovement platformMovement = this.m_sein.PlatformBehaviour.PlatformMovement;
		platformMovement.LocalSpeed = Vector2.zero;
		this.ChangeState(SeinDashAttack.State.Normal);
		this.m_stopAnimation = true;
		this.m_chargeDashAtTarget = false;
	}

	// Token: 0x06001CDC RID: 7388 RVA: 0x0007E59C File Offset: 0x0007C79C
	private bool RaycastTest()
	{
		Vector3 a = Vector3.Cross(this.m_sein.PlatformBehaviour.PlatformMovement.GroundRayNormal, Vector3.forward);
		float num = this.m_sein.Speed.x * Time.deltaTime;
		Vector3 vector = this.m_sein.Position + a * num + Vector3.up;
		Vector3 vector2 = Vector3.down * (1.8f + Mathf.Abs(num));
		Debug.DrawRay(vector, vector2, Color.yellow, 0.5f);
		RaycastHit raycastHit;
		return this.m_sein.Controller.RayTest(vector, vector2, out raycastHit);
	}

	// Token: 0x06001CDD RID: 7389 RVA: 0x0007E645 File Offset: 0x0007C845
	public void ResetDashLimit()
	{
		this.m_hasDashed = false;
	}

	// Token: 0x040018E6 RID: 6374
	public AnimationCurve DashSpeedOverTime;

	// Token: 0x040018E7 RID: 6375
	public AnimationCurve ChargeDashSpeedOverTime;

	// Token: 0x040018E8 RID: 6376
	public float DashTime = 0.5f;

	// Token: 0x040018E9 RID: 6377
	public float ChargeDashTime = 0.5f;

	// Token: 0x040018EA RID: 6378
	public float ChargeTime = 0.2f;

	// Token: 0x040018EB RID: 6379
	public SoundProvider ChargeSound;

	// Token: 0x040018EC RID: 6380
	public SoundProvider DoneChargingSound;

	// Token: 0x040018ED RID: 6381
	public SoundSource ChargedSound;

	// Token: 0x040018EE RID: 6382
	public SoundProvider UnChargeSound;

	// Token: 0x040018EF RID: 6383
	public SoundProvider DashSound;

	// Token: 0x040018F0 RID: 6384
	public SoundProvider ChargeDashSound;

	// Token: 0x040018F1 RID: 6385
	public SoundProvider RainbowDashSound;

	// Token: 0x040018F2 RID: 6386
	public SoundProvider DashIntoWallSound;

	// Token: 0x040018F3 RID: 6387
	public GameObject ExplosionEffect;

	// Token: 0x040018F4 RID: 6388
	public SeinDashAttack.State CurrentState;

	// Token: 0x040018F5 RID: 6389
	public float DashDownwardSpeed = 10f;

	// Token: 0x040018F6 RID: 6390
	public float OffGroundSpeed = 15f;

	// Token: 0x040018F7 RID: 6391
	public int Damage = 50;

	// Token: 0x040018F8 RID: 6392
	public float EnergyCost = 1f;

	// Token: 0x040018F9 RID: 6393
	public SoundProvider NotEnoughEnergySound;

	// Token: 0x040018FA RID: 6394
	public TextureAnimationWithTransitions DashAnimation;

	// Token: 0x040018FB RID: 6395
	public TextureAnimationWithTransitions ChargeDashAnimation;

	// Token: 0x040018FC RID: 6396
	public TextureAnimationWithTransitions GlideDashAnimation;

	// Token: 0x040018FD RID: 6397
	public TextureAnimationWithTransitions DashIntoWallAnimation;

	// Token: 0x040018FE RID: 6398
	public GameObject DashStartEffect;

	// Token: 0x040018FF RID: 6399
	public GameObject DashFollowEffect;

	// Token: 0x04001900 RID: 6400
	public GameObject DashFollowRainbowEffect;

	// Token: 0x04001901 RID: 6401
	private SeinCharacter m_sein;

	// Token: 0x04001902 RID: 6402
	private bool m_faceLeft;

	// Token: 0x04001903 RID: 6403
	private float m_stateCurrentTime;

	// Token: 0x04001904 RID: 6404
	private HashSet<IAttackable> m_attackablesIgnore = new HashSet<IAttackable>();

	// Token: 0x04001905 RID: 6405
	private bool m_stopAnimation;

	// Token: 0x04001906 RID: 6406
	private float m_lastPressTime;

	// Token: 0x04001907 RID: 6407
	private float m_lastDashTime;

	// Token: 0x04001908 RID: 6408
	private bool m_isOnGround;

	// Token: 0x04001909 RID: 6409
	public static bool RainbowDashActivated = false;

	// Token: 0x0400190A RID: 6410
	private bool m_hasDashed;

	// Token: 0x0400190B RID: 6411
	public float ChargeDashTargetMaxDistance = 20f;

	// Token: 0x0400190C RID: 6412
	private float m_timeOfLastExplosionEffect;

	// Token: 0x0400190D RID: 6413
	private float m_timeWhenDashJumpHappened;

	// Token: 0x0400190E RID: 6414
	private bool m_allowNoDecelerationForThisDash;

	// Token: 0x0400190F RID: 6415
	private IAttackable m_chargeDashAttackTarget;

	// Token: 0x04001910 RID: 6416
	private bool m_hasHitAttackable;

	// Token: 0x04001911 RID: 6417
	private bool m_chargeJumpWasReleased = true;

	// Token: 0x04001912 RID: 6418
	private IChargeDashAttackable m_lastTarget;

	// Token: 0x04001913 RID: 6419
	public float SpriteRotation;

	// Token: 0x04001914 RID: 6420
	private Vector3 m_chargeDashDirection;

	// Token: 0x04001915 RID: 6421
	private bool m_chargeDashAtTarget;

	// Token: 0x04001916 RID: 6422
	private Vector3 m_chargeDashAtTargetPosition;

	// Token: 0x0200043E RID: 1086
	public enum State
	{
		// Token: 0x04001A04 RID: 6660
		Normal,
		// Token: 0x04001A05 RID: 6661
		Dashing,
		// Token: 0x04001A06 RID: 6662
		ChargeDashing
	}
}
