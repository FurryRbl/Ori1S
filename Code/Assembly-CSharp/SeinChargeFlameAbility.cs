using System;
using System.Collections.Generic;
using Core;
using fsm;
using Game;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class SeinChargeFlameAbility : CharacterState, ISeinReceiver
{
	// Token: 0x1700011A RID: 282
	// (get) Token: 0x06000472 RID: 1138 RVA: 0x000120FA File Offset: 0x000102FA
	public float ChargeDuration
	{
		get
		{
			return this.ChargeFlameSettings.ChargeDuration;
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06000473 RID: 1139 RVA: 0x00012108 File Offset: 0x00010308
	public bool HasEnoughEnergy
	{
		get
		{
			return this.m_sein.Energy.CanAfford(this.EnergyCost * ((!this.m_sein.PlayerAbilities.ChargeFlameEfficiency.HasAbility) ? 1f : 0.5f));
		}
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x00012158 File Offset: 0x00010358
	public void SpendEnergy()
	{
		this.m_sein.Energy.Spend(this.EnergyCost * ((!this.m_sein.PlayerAbilities.ChargeFlameEfficiency.HasAbility) ? 1f : 0.5f));
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x000121A8 File Offset: 0x000103A8
	public void RestoreEnergy()
	{
		this.m_sein.Energy.Gain(this.EnergyCost * ((!this.m_sein.PlayerAbilities.ChargeFlameEfficiency.HasAbility) ? 1f : 0.5f));
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x000121F8 File Offset: 0x000103F8
	public override void Awake()
	{
		base.Awake();
		this.State.Start = new State
		{
			UpdateStateEvent = new Action(this.UpdateStartState),
			OnEnterEvent = new Action(this.OnEnterStartState)
		};
		this.State.Precharging = new State
		{
			UpdateStateEvent = new Action(this.UpdatePrechargingState)
		};
		this.State.Charging = new State
		{
			UpdateStateEvent = new Action(this.UpdateChargingState)
		};
		this.State.Charged = new State
		{
			UpdateStateEvent = new Action(this.UpdateChargedState)
		};
		this.Logic.RegisterStates(new IState[]
		{
			this.State.Start,
			this.State.Precharging,
			this.State.Charging,
			this.State.Charged
		});
		this.Logic.ChangeState(this.State.Start);
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00012324 File Offset: 0x00010524
	public void OnRestoreCheckpoint()
	{
		if (this.m_chargeFlameChargeEffect)
		{
			InstantiateUtility.Destroy(this.m_chargeFlameChargeEffect);
		}
		if (this.CurrentChargingSound())
		{
			this.CurrentChargingSound().StopAndFadeOut(0.5f);
		}
		this.Logic.ChangeState(this.State.Start);
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x00012382 File Offset: 0x00010582
	public void OnEnterStartState()
	{
		if (this.m_chargeFlameChargeEffect)
		{
			InstantiateUtility.Destroy(this.m_chargeFlameChargeEffect);
		}
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x000123A0 File Offset: 0x000105A0
	public void UpdateStartState()
	{
		if (this.m_chargeFlameChargeEffect)
		{
			InstantiateUtility.Destroy(this.m_chargeFlameChargeEffect);
		}
		if (this.m_sein.Controller.IsBashing)
		{
			return;
		}
		if (this.ChargeFlameButton.OnPressed && !this.ChargeFlameButton.Used && this.m_sein.PlayerAbilities.ChargeFlame.HasAbility && !this.m_sein.Controller.InputLocked && !this.m_sein.Abilities.SpiritFlame.LockShootingSpiritFlame)
		{
			this.Logic.ChangeState(this.State.Precharging);
		}
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x00012460 File Offset: 0x00010660
	public void UpdatePrechargingState()
	{
		if (this.Logic.CurrentStateTime > 0.3f)
		{
			this.m_chargeFlameChargeEffect = (GameObject)InstantiateUtility.Instantiate(this.ChargeFlameSettings.ChargeFlameChargeEffectPrefab);
			this.m_chargeFlameChargeEffect.transform.position = Characters.Ori.transform.position;
			this.m_chargeFlameChargeEffect.transform.parent = Characters.Ori.transform;
			this.m_chargeFlameChargeEffect.GetComponentsInChildren<LegacyAnimator>(SeinChargeFlameAbility.s_legacyAnimatorList);
			for (int i = 0; i < SeinChargeFlameAbility.s_legacyAnimatorList.Count; i++)
			{
				LegacyAnimator legacyAnimator = SeinChargeFlameAbility.s_legacyAnimatorList[i];
				legacyAnimator.Speed = 1f / this.ChargeDuration;
			}
			SeinChargeFlameAbility.s_legacyAnimatorList.Clear();
			if (this.CurrentChargingSound())
			{
				this.CurrentChargingSound().Play();
			}
			this.Logic.ChangeState(this.State.Charging);
			return;
		}
		if (this.ChargeFlameButton.Released)
		{
			this.Logic.ChangeState(this.State.Start);
		}
		else if (this.m_sein.Abilities.SpiritFlame.LockShootingSpiritFlame)
		{
			this.Logic.ChangeState(this.State.Start);
		}
		else if (this.m_sein.Controller.InputLocked)
		{
			this.Logic.ChangeState(this.State.Start);
		}
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x000125E8 File Offset: 0x000107E8
	public void UpdateChargingState()
	{
		if (this.ChargeFlameButton.Released || this.m_sein.Controller.InputLocked || this.m_sein.Abilities.SpiritFlame.LockShootingSpiritFlame)
		{
			if (this.CurrentChargingSound())
			{
				this.CurrentChargingSound().StopAndFadeOut(0.5f);
			}
			this.Logic.ChangeState(this.State.Start);
			return;
		}
		if (this.Logic.CurrentStateTime >= this.ChargeDuration)
		{
			if (this.HasEnoughEnergy)
			{
				this.Logic.ChangeState(this.State.Charged);
				this.SpendEnergy();
			}
			else
			{
				if (this.CurrentChargingSound())
				{
					this.CurrentChargingSound().StopAndFadeOut(0.5f);
				}
				this.Logic.ChangeState(this.State.Start);
				UI.SeinUI.ShakeEnergyOrbBar();
				if (this.NotEnoughEnergySound)
				{
					Sound.Play(this.NotEnoughEnergySound.GetSound(null), base.transform.position, null);
				}
			}
		}
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x0001271C File Offset: 0x0001091C
	public void ReleaseChargeBurst()
	{
		if (this.CurrentChargingSound())
		{
			this.CurrentChargingSound().StopAndFadeOut(0.5f);
		}
		if (this.m_sein.PlayerAbilities.ChargeFlameBlast.HasAbility)
		{
			InstantiateUtility.Instantiate(this.ChargeFlameSettings.ChargeFlameBurstC, Characters.Ori.Position, Quaternion.identity);
		}
		else if (this.m_sein.PlayerAbilities.ChargeFlameBurn.HasAbility)
		{
			InstantiateUtility.Instantiate(this.ChargeFlameSettings.ChargeFlameBurstB, Characters.Ori.Position, Quaternion.identity);
		}
		else
		{
			InstantiateUtility.Instantiate(this.ChargeFlameSettings.ChargeFlameBurstA, Characters.Ori.Position, Quaternion.identity);
		}
		this.Logic.ChangeState(this.State.Start);
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x00012800 File Offset: 0x00010A00
	public void UpdateChargedState()
	{
		if (this.ChargeFlameButton.Released)
		{
			this.ReleaseChargeBurst();
		}
		else if (Core.Input.SoulFlame.OnPressed)
		{
			Core.Input.SoulFlame.Used = true;
			if (this.CurrentChargingSound())
			{
				this.CurrentChargingSound().StopAndFadeOut(0.5f);
			}
			this.Logic.ChangeState(this.State.Start);
			this.RestoreEnergy();
			UI.SeinUI.ShakeEnergyOrbBar();
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x0600047E RID: 1150 RVA: 0x00012888 File Offset: 0x00010A88
	public Core.Input.InputButtonProcessor ChargeFlameButton
	{
		get
		{
			return Core.Input.SpiritFlame;
		}
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001288F File Offset: 0x00010A8F
	public bool IsCharging
	{
		get
		{
			return this.Logic.CurrentState != this.State.Start;
		}
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x000128AC File Offset: 0x00010AAC
	public override void UpdateCharacterState()
	{
		this.Logic.UpdateState(Time.deltaTime);
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x000128BE File Offset: 0x00010ABE
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
		this.m_sein.Abilities.ChargeFlame = this;
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x000128D8 File Offset: 0x00010AD8
	public override void OnExit()
	{
		if (this.Logic.CurrentState == this.State.Precharging)
		{
			this.Logic.ChangeState(this.State.Start);
		}
		if (this.Logic.CurrentState == this.State.Charging)
		{
			if (this.CurrentChargingSound())
			{
				this.CurrentChargingSound().StopAndFadeOut(0.5f);
			}
			this.Logic.ChangeState(this.State.Start);
		}
		if (this.Logic.CurrentState == this.State.Charged)
		{
			this.ReleaseChargeBurst();
		}
		base.OnExit();
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00012990 File Offset: 0x00010B90
	private SoundSource CurrentChargingSound()
	{
		if (this.m_sein.PlayerAbilities.ChargeFlameBlast.HasAbility)
		{
			return this.ChargingSoundLevelC;
		}
		if (this.m_sein.PlayerAbilities.ChargeFlameBurn.HasAbility)
		{
			return this.ChargingSoundLevelB;
		}
		return this.ChargingSoundLevelA;
	}

	// Token: 0x040003A1 RID: 929
	public SoundSource ChargingSoundLevelA;

	// Token: 0x040003A2 RID: 930
	public SoundSource ChargingSoundLevelB;

	// Token: 0x040003A3 RID: 931
	public SoundSource ChargingSoundLevelC;

	// Token: 0x040003A4 RID: 932
	public AchievementAsset KillEnemiesSimultaneouslyAchievement;

	// Token: 0x040003A5 RID: 933
	public SoundProvider NotEnoughEnergySound;

	// Token: 0x040003A6 RID: 934
	public SeinChargeFlameAbility.ChargeFlameDefinitions ChargeFlameSettings;

	// Token: 0x040003A7 RID: 935
	public SeinChargeFlameAbility.States State = new SeinChargeFlameAbility.States();

	// Token: 0x040003A8 RID: 936
	private StateMachine Logic = new StateMachine();

	// Token: 0x040003A9 RID: 937
	private GameObject m_chargeFlameChargeEffect;

	// Token: 0x040003AA RID: 938
	public float EnergyCost = 1f;

	// Token: 0x040003AB RID: 939
	private static readonly List<LegacyAnimator> s_legacyAnimatorList = new List<LegacyAnimator>();

	// Token: 0x040003AC RID: 940
	private SeinCharacter m_sein;

	// Token: 0x02000072 RID: 114
	[Serializable]
	public class ChargeFlameDefinitions
	{
		// Token: 0x040003DE RID: 990
		public float ChargeDuration = 1f;

		// Token: 0x040003DF RID: 991
		public GameObject ChargeFlameBurstA;

		// Token: 0x040003E0 RID: 992
		public GameObject ChargeFlameBurstB;

		// Token: 0x040003E1 RID: 993
		public GameObject ChargeFlameBurstC;

		// Token: 0x040003E2 RID: 994
		public GameObject ChargeFlameChargeEffectPrefab;
	}

	// Token: 0x02000073 RID: 115
	public class States
	{
		// Token: 0x040003E3 RID: 995
		public State Start;

		// Token: 0x040003E4 RID: 996
		public State Precharging;

		// Token: 0x040003E5 RID: 997
		public State Charging;

		// Token: 0x040003E6 RID: 998
		public State Charged;
	}
}
