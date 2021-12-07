using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000413 RID: 1043
public class SeinChargeJumpCharging : CharacterState, ISeinReceiver
{
	// Token: 0x170004CA RID: 1226
	// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x0007CBFE File Offset: 0x0007ADFE
	public PlayerAbilities PlayerAbilities
	{
		get
		{
			return this.Sein.PlayerAbilities;
		}
	}

	// Token: 0x170004CB RID: 1227
	// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x0007CC0B File Offset: 0x0007AE0B
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x170004CC RID: 1228
	// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x0007CC1D File Offset: 0x0007AE1D
	public SeinChargeJump ChargeJump
	{
		get
		{
			return this.Sein.Abilities.ChargeJump;
		}
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x0007CC2F File Offset: 0x0007AE2F
	public override void UpdateCharacterState()
	{
		if (this.Sein.IsSuspended)
		{
			return;
		}
		this.UpdateState();
	}

	// Token: 0x06001CA5 RID: 7333 RVA: 0x0007CC48 File Offset: 0x0007AE48
	public void EndCharge()
	{
		this.ChangeState(SeinChargeJumpCharging.State.Normal);
		if (this.ChargeJumpCompleteEffectToSpawn)
		{
			InstantiateUtility.Instantiate(this.ChargeJumpCompleteEffectToSpawn, this.Sein.transform.position, this.Sein.transform.rotation);
		}
	}

	// Token: 0x06001CA6 RID: 7334 RVA: 0x0007CC98 File Offset: 0x0007AE98
	public override void OnExit()
	{
		if (this.CurrentState != SeinChargeJumpCharging.State.Normal)
		{
			this.EndCharge();
		}
		base.OnExit();
	}

	// Token: 0x06001CA7 RID: 7335 RVA: 0x0007CCB4 File Offset: 0x0007AEB4
	public void ChangeState(SeinChargeJumpCharging.State state)
	{
		switch (this.CurrentState)
		{
		case SeinChargeJumpCharging.State.Charging:
			if (this.m_chargingEffect)
			{
				this.m_chargingEffect.GetComponent<TransparencyAnimator>().AnimatorDriver.ContinueBackwards();
				this.m_chargingEffect = null;
			}
			if (this.m_lastChargingSound && state == SeinChargeJumpCharging.State.Normal)
			{
				this.m_lastChargingSound.FadeOut(0.2f, true);
				UberPoolManager.Instance.RemoveOnDestroyed(this.m_lastChargingSound.gameObject);
				this.m_lastChargingSound = null;
			}
			break;
		case SeinChargeJumpCharging.State.Charged:
			if (this.ChargedSound)
			{
				this.ChargedSound.Stop();
			}
			if (this.UnChargeSound && state == SeinChargeJumpCharging.State.Normal)
			{
				Sound.Play(this.UnChargeSound.GetSound(null), base.transform.position, null);
			}
			if (this.m_chargedEffect)
			{
				this.m_chargedEffect.GetComponent<TransparencyAnimator>().AnimatorDriver.ContinueBackwards();
				this.m_chargedEffect = null;
			}
			break;
		}
		this.CurrentState = state;
		this.m_stateCurrentTime = 0f;
		switch (this.CurrentState)
		{
		case SeinChargeJumpCharging.State.Charging:
			if (this.ChargingEffectToSpawn)
			{
				this.m_chargingEffect = (GameObject)InstantiateUtility.Instantiate(this.ChargingEffectToSpawn, this.Sein.transform.position, this.Sein.transform.rotation);
				this.m_chargingEffect.transform.parent = this.Sein.transform;
				this.m_chargingEffect.transform.localPosition = Vector3.zero;
				this.m_chargingEffect.transform.localRotation = Quaternion.identity;
			}
			this.m_lastChargingSound = Sound.Play(this.ChargeSound.GetSound(null), this.PlatformMovement.Position, delegate()
			{
				this.m_lastChargingSound = null;
			});
			break;
		case SeinChargeJumpCharging.State.Charged:
			if (this.ChargedEffectToSpawn)
			{
				this.m_chargedEffect = (GameObject)InstantiateUtility.Instantiate(this.ChargedEffectToSpawn, this.Sein.transform.position, this.Sein.transform.rotation);
				this.m_chargedEffect.transform.parent = this.Sein.transform;
				this.m_chargedEffect.transform.localPosition = Vector3.zero;
				this.m_chargedEffect.transform.localRotation = Quaternion.identity;
			}
			if (this.ChargedSound)
			{
				this.ChargedSound.Play();
			}
			break;
		}
	}

	// Token: 0x170004CD RID: 1229
	// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x0007CF78 File Offset: 0x0007B178
	private bool IsWallCharging
	{
		get
		{
			return this.Sein.Controller.IsGrabbingWall && this.Sein.Abilities.GrabWall.IsGrabbingAway && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities) && this.PlayerAbilities.ChargeJump.HasAbility;
		}
	}

	// Token: 0x170004CE RID: 1230
	// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x0007CFD4 File Offset: 0x0007B1D4
	private bool IsGroundCharging
	{
		get
		{
			return (!PlayerInput.Instance.WasKeyboardUsedLast || !this.Sein.Controller.IsGrabbingWall) && Core.Input.ChargeJump.Pressed && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities);
		}
	}

	// Token: 0x06001CAA RID: 7338 RVA: 0x0007D020 File Offset: 0x0007B220
	public void UpdateState()
	{
		switch (this.CurrentState)
		{
		case SeinChargeJumpCharging.State.Normal:
			if (!this.PlayerAbilities.ChargeJump.HasAbility && (!this.PlayerAbilities.ChargeDash.HasAbility || !this.Sein.Abilities.Dash || !this.Sein.Abilities.Dash.HasEnoughEnergy))
			{
				return;
			}
			if (Characters.Sein.Controller.CanMove)
			{
				if (this.IsGroundCharging && Core.Input.ChargeJump.OnPressed)
				{
					this.ChangeState(SeinChargeJumpCharging.State.Charging);
				}
				else if (this.IsWallCharging)
				{
					this.m_wallChargeHeldTime += Time.deltaTime;
					if (this.m_wallChargeHeldTime > 0.2f)
					{
						this.ChangeState(SeinChargeJumpCharging.State.Charging);
					}
				}
				else
				{
					this.m_wallChargeHeldTime = 0f;
				}
			}
			else
			{
				this.m_wallChargeHeldTime = 0f;
			}
			break;
		case SeinChargeJumpCharging.State.Charging:
			if (this.m_stateCurrentTime > this.ChargingTime)
			{
				this.ChangeState(SeinChargeJumpCharging.State.Charged);
			}
			if (!this.IsGroundCharging && !this.IsWallCharging)
			{
				this.ChangeState(SeinChargeJumpCharging.State.Normal);
			}
			break;
		case SeinChargeJumpCharging.State.Charged:
			if (!this.IsGroundCharging && !this.IsWallCharging)
			{
				this.ChangeState(SeinChargeJumpCharging.State.Normal);
			}
			break;
		}
		this.m_stateCurrentTime += Time.deltaTime;
	}

	// Token: 0x170004CF RID: 1231
	// (get) Token: 0x06001CAB RID: 7339 RVA: 0x0007D1AB File Offset: 0x0007B3AB
	public bool IsCharging
	{
		get
		{
			return this.CurrentState == SeinChargeJumpCharging.State.Charging;
		}
	}

	// Token: 0x170004D0 RID: 1232
	// (get) Token: 0x06001CAC RID: 7340 RVA: 0x0007D1B6 File Offset: 0x0007B3B6
	public bool IsCharged
	{
		get
		{
			return this.CurrentState == SeinChargeJumpCharging.State.Charged;
		}
	}

	// Token: 0x170004D1 RID: 1233
	// (get) Token: 0x06001CAD RID: 7341 RVA: 0x0007D1C1 File Offset: 0x0007B3C1
	public float ChargingValue
	{
		get
		{
			return this.m_stateCurrentTime / this.ChargingTime;
		}
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x0007D1D0 File Offset: 0x0007B3D0
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.ChargeJumpCharging = this;
	}

	// Token: 0x040018D5 RID: 6357
	public SeinCharacter Sein;

	// Token: 0x040018D6 RID: 6358
	public SoundProvider ChargeSound;

	// Token: 0x040018D7 RID: 6359
	private SoundPlayer m_lastChargingSound;

	// Token: 0x040018D8 RID: 6360
	public SoundSource ChargedSound;

	// Token: 0x040018D9 RID: 6361
	public SoundProvider UnChargeSound;

	// Token: 0x040018DA RID: 6362
	public SeinChargeJumpCharging.State CurrentState;

	// Token: 0x040018DB RID: 6363
	private float m_stateCurrentTime;

	// Token: 0x040018DC RID: 6364
	private float m_wallChargeHeldTime;

	// Token: 0x040018DD RID: 6365
	public float ChargingTime;

	// Token: 0x040018DE RID: 6366
	public float UnchargingTime = 0.5f;

	// Token: 0x040018DF RID: 6367
	public GameObject ChargingEffectToSpawn;

	// Token: 0x040018E0 RID: 6368
	public GameObject UnchargingEffectToSpawn;

	// Token: 0x040018E1 RID: 6369
	public GameObject ChargedEffectToSpawn;

	// Token: 0x040018E2 RID: 6370
	public GameObject ChargeJumpCompleteEffectToSpawn;

	// Token: 0x040018E3 RID: 6371
	private GameObject m_chargingEffect;

	// Token: 0x040018E4 RID: 6372
	private GameObject m_unchargingEffect;

	// Token: 0x040018E5 RID: 6373
	private GameObject m_chargedEffect;

	// Token: 0x02000435 RID: 1077
	public enum State
	{
		// Token: 0x040019EB RID: 6635
		Normal,
		// Token: 0x040019EC RID: 6636
		Charging,
		// Token: 0x040019ED RID: 6637
		Charged
	}
}
