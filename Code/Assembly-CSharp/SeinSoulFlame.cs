using System;
using Core;
using Game;
using Sein.World;
using UnityEngine;

// Token: 0x020001F6 RID: 502
public class SeinSoulFlame : CharacterState, ISeinReceiver
{
	// Token: 0x06001166 RID: 4454 RVA: 0x0004F994 File Offset: 0x0004DB94
	// Note: this type is marked as 'beforefieldinit'.
	static SeinSoulFlame()
	{
		SeinSoulFlame.OnSoulFlameCast = delegate()
		{
		};
	}

	// Token: 0x14000028 RID: 40
	// (add) Token: 0x06001167 RID: 4455 RVA: 0x0004F9C3 File Offset: 0x0004DBC3
	// (remove) Token: 0x06001168 RID: 4456 RVA: 0x0004F9DA File Offset: 0x0004DBDA
	public static event Action OnSoulFlameCast;

	// Token: 0x1700031A RID: 794
	// (get) Token: 0x06001169 RID: 4457 RVA: 0x0004F9F1 File Offset: 0x0004DBF1
	public bool SoulFlameExists
	{
		get
		{
			return this.m_checkpointMarkerGameObject;
		}
	}

	// Token: 0x1700031B RID: 795
	// (get) Token: 0x0600116A RID: 4458 RVA: 0x0004F9FE File Offset: 0x0004DBFE
	public Vector3 SoulFlamePosition
	{
		get
		{
			return this.m_checkpointMarkerGameObject.transform.position;
		}
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x0004FA10 File Offset: 0x0004DC10
	public new void Awake()
	{
		base.Awake();
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
		Game.Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x0004FA54 File Offset: 0x0004DC54
	public void OnGameReset()
	{
		this.m_numberOfSoulFlamesCast = 0;
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x0004FA5D File Offset: 0x0004DC5D
	public void OnRestoreCheckpoint()
	{
		if (this.CanAffordSoulFlame)
		{
			this.m_cooldownRemaining = 0f;
		}
		this.LockSoulFlame = false;
		this.m_nagTimer = this.NagDuration;
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x0004FA88 File Offset: 0x0004DC88
	public override void OnDestroy()
	{
		base.OnDestroy();
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
		Game.Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
		if (this.m_checkpointMarkerGameObject)
		{
			InstantiateUtility.Destroy(this.m_checkpointMarkerGameObject);
			this.m_soulFlame = null;
			this.m_checkpointMarkerGameObject = null;
		}
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x0004FAF5 File Offset: 0x0004DCF5
	public void FillSoulFlameBar()
	{
		this.m_cooldownRemaining = 0f;
		this.m_nagTimer = 0f;
	}

	// Token: 0x1700031C RID: 796
	// (get) Token: 0x06001170 RID: 4464 RVA: 0x0004FB0D File Offset: 0x0004DD0D
	public bool InsideCheckpointMarker
	{
		get
		{
			return this.m_soulFlame && this.m_soulFlame.IsInside;
		}
	}

	// Token: 0x1700031D RID: 797
	// (get) Token: 0x06001171 RID: 4465 RVA: 0x0004FB30 File Offset: 0x0004DD30
	public SeinSoulFlame.SoulFlamePlacementSafety IsSafeToCastSoulFlame
	{
		get
		{
			Vector3 position = this.m_sein.Position;
			for (int i = 0; i < NoSoulFlameZone.All.Count; i++)
			{
				NoSoulFlameZone noSoulFlameZone = NoSoulFlameZone.All[i];
				if (noSoulFlameZone.BoundingRect.Contains(position))
				{
					return SeinSoulFlame.SoulFlamePlacementSafety.UnsafeZone;
				}
			}
			if (!Sein.World.Events.DarknessLifted && SpiritLightDarknessZone.IsInsideDarknessZone(position) && !SaveInTheDarkZone.IsInside(position) && !LightSource.TestPosition(position, 0f))
			{
				return SeinSoulFlame.SoulFlamePlacementSafety.UnsafeZone;
			}
			for (int j = 0; j < SavePedestal.All.Count; j++)
			{
				SavePedestal savePedestal = SavePedestal.All[j];
				if (savePedestal.IsInside)
				{
					return SeinSoulFlame.SoulFlamePlacementSafety.SavePedestal;
				}
			}
			for (int k = 0; k < this.m_sein.Abilities.SpiritFlameTargetting.ClosestAttackables.Count; k++)
			{
				ISpiritFlameAttackable spiritFlameAttackable = this.m_sein.Abilities.SpiritFlameTargetting.ClosestAttackables[k];
				EntityTargetting entityTargetting = spiritFlameAttackable as EntityTargetting;
				if (entityTargetting && entityTargetting.Entity is Enemy)
				{
					return SeinSoulFlame.SoulFlamePlacementSafety.UnsafeEnemies;
				}
			}
			for (int l = 0; l < RespawningPlaceholder.All.Count; l++)
			{
				RespawningPlaceholder respawningPlaceholder = RespawningPlaceholder.All[l];
				if (!respawningPlaceholder.EntityIsDead && Vector3.Distance(position, respawningPlaceholder.Position) < 10f)
				{
					return SeinSoulFlame.SoulFlamePlacementSafety.UnsafeEnemies;
				}
			}
			if (this.m_sein.Mortality.DamageReciever.IsInvinsible)
			{
				return SeinSoulFlame.SoulFlamePlacementSafety.UnsafeZone;
			}
			Collider groundCollider = this.m_sein.PlatformBehaviour.PlatformMovementListOfColliders.GroundCollider;
			if (groundCollider)
			{
				if (groundCollider.attachedRigidbody)
				{
					return SeinSoulFlame.SoulFlamePlacementSafety.UnsafeGround;
				}
				if (groundCollider.GetComponent<HeatUpPlatform>())
				{
					return SeinSoulFlame.SoulFlamePlacementSafety.UnsafeGround;
				}
			}
			bool flag = Physics.SphereCast(new Ray(position, Vector3.right), 0.5f, 0.7f, this.UnsafeMask);
			flag |= Physics.SphereCast(new Ray(position, -Vector3.right), 0.5f, 0.7f, this.UnsafeMask);
			if (flag)
			{
				return SeinSoulFlame.SoulFlamePlacementSafety.UnsafeGround;
			}
			return SeinSoulFlame.SoulFlamePlacementSafety.Safe;
		}
	}

	// Token: 0x1700031E RID: 798
	// (get) Token: 0x06001172 RID: 4466 RVA: 0x0004FD80 File Offset: 0x0004DF80
	public float BarValue
	{
		get
		{
			return (1f - this.CooldownRemaining) * (1f - this.m_holdDownTime);
		}
	}

	// Token: 0x1700031F RID: 799
	// (get) Token: 0x06001173 RID: 4467 RVA: 0x0004FD9B File Offset: 0x0004DF9B
	public float CooldownRemaining
	{
		get
		{
			return this.m_cooldownRemaining;
		}
	}

	// Token: 0x17000320 RID: 800
	// (get) Token: 0x06001174 RID: 4468 RVA: 0x0004FDA3 File Offset: 0x0004DFA3
	public bool ShowFlameOnUI
	{
		get
		{
			return Mathf.Approximately(this.BarValue, 1f);
		}
	}

	// Token: 0x17000321 RID: 801
	// (get) Token: 0x06001175 RID: 4469 RVA: 0x0004FDB5 File Offset: 0x0004DFB5
	public float SoulFlameCost
	{
		get
		{
			return (!this.m_sein.PlayerAbilities.SoulFlameEfficiency.HasAbility) ? 1f : 0.5f;
		}
	}

	// Token: 0x17000322 RID: 802
	// (get) Token: 0x06001176 RID: 4470 RVA: 0x0004FDE0 File Offset: 0x0004DFE0
	public bool CanAffordSoulFlame
	{
		get
		{
			return this.m_sein.Energy.CanAfford(this.SoulFlameCost);
		}
	}

	// Token: 0x17000323 RID: 803
	// (get) Token: 0x06001177 RID: 4471 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
	public bool AllowedToAccessSkillTree
	{
		get
		{
			return this.m_sein.Level.Current > 0 && this.IsSafeToCastSoulFlame == SeinSoulFlame.SoulFlamePlacementSafety.Safe;
		}
	}

	// Token: 0x17000324 RID: 804
	// (get) Token: 0x06001178 RID: 4472 RVA: 0x0004FE28 File Offset: 0x0004E028
	public bool PlayerCouldSoulFlame
	{
		get
		{
			return Characters.Sein.Controller.CanMove && !this.m_sein.Controller.IsSwimming && !UI.Fader.IsFadingInOrStay() && !SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities) && !this.LockSoulFlame;
		}
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x0004FE84 File Offset: 0x0004E084
	public void HandleNagging()
	{
		if (this.m_readyForReadySequence && this.PlayerCouldSoulFlame && this.IsSafeToCastSoulFlame == SeinSoulFlame.SoulFlamePlacementSafety.Safe && this.CanAffordSoulFlame)
		{
			this.m_readyForReadySequence = false;
			InstantiateUtility.Instantiate(this.SoulFlameReadyText, Characters.Ori.transform.position, Quaternion.identity);
			UI.SeinUI.OnSoulFlameReady();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.SoulFlameReadyEffect);
			gameObject.transform.parent = Characters.Ori.transform;
			gameObject.transform.localPosition = Vector3.zero;
			Sound.Play(this.SoulFlameReadySoundProvider.GetSound(null), Characters.Sein.Position, null);
			this.m_nagTimer = this.NagDuration;
		}
		if (this.m_nagTimer > 0f)
		{
			this.m_nagTimer -= Time.deltaTime;
			if (this.m_nagTimer <= 0f)
			{
				if (this.PlayerCouldSoulFlame && this.CanAffordSoulFlame && this.IsSafeToCastSoulFlame == SeinSoulFlame.SoulFlamePlacementSafety.Safe)
				{
					this.m_nagTimer = 0f;
					InstantiateUtility.Instantiate(this.SoulFlameReadyText, Characters.Ori.transform.position, Quaternion.identity);
					UI.SeinUI.OnSoulFlameReady();
					Sound.Play(this.SoulFlameReadySoundProvider.GetSound(null), Characters.Sein.Position, null);
					this.m_nagTimer = this.NagDuration;
				}
				else
				{
					this.m_nagTimer = 2f;
				}
			}
		}
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x00050009 File Offset: 0x0004E209
	private void HandleDelayOnGround()
	{
		if (!this.m_sein.IsOnGround)
		{
			this.m_delayOnGround = 0.1f;
		}
		else
		{
			this.m_delayOnGround = Mathf.Max(0f, this.m_delayOnGround - Time.deltaTime);
		}
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x00050048 File Offset: 0x0004E248
	public override void UpdateCharacterState()
	{
		if (this.m_sein.Controller.IsBashing)
		{
			return;
		}
		this.HandleDelayOnGround();
		this.HandleCooldown();
		this.HandleCheckpointMarkerVisibility();
		this.HandleNagging();
		this.HandleSkillTreeHint();
		this.HandleCharging();
		if (this.m_sein.Energy.Max == 0f)
		{
			this.m_cooldownRemaining = 1f;
		}
		if (!UI.Fader.IsFadingInOrStay())
		{
			if (Core.Input.SoulFlame.OnPressed && !Core.Input.SoulFlame.Used && !Core.Input.Cancel.Used)
			{
				this.m_isCasting = true;
				if (this.InsideCheckpointMarker)
				{
					this.m_tapRemainingTime = 0.3f;
				}
				else if (!this.CanAffordSoulFlame)
				{
					this.HideOtherMessages();
					UI.SeinUI.ShakeEnergyOrbBar();
					this.m_sein.Energy.NotifyOutOfEnergy();
				}
				else if (this.m_cooldownRemaining != 0f)
				{
					this.HideOtherMessages();
					this.m_notReadyHint = UI.Hints.Show(this.NotReadyMessage, HintLayer.SoulFlame, 1f);
					Sound.Play(this.NotReadySound.GetSound(null), base.transform.position, null);
				}
				else if (this.IsSafeToCastSoulFlame != SeinSoulFlame.SoulFlamePlacementSafety.Safe)
				{
					this.HideOtherMessages();
					switch (this.IsSafeToCastSoulFlame)
					{
					case SeinSoulFlame.SoulFlamePlacementSafety.UnsafeEnemies:
						this.m_notSafeHint = UI.Hints.Show(this.NotSafeEnemiesMessage, HintLayer.SoulFlame, 1f);
						break;
					case SeinSoulFlame.SoulFlamePlacementSafety.UnsafeGround:
						this.m_notSafeHint = UI.Hints.Show(this.NotSafeGroundMessage, HintLayer.SoulFlame, 1f);
						break;
					case SeinSoulFlame.SoulFlamePlacementSafety.UnsafeZone:
						this.m_notSafeHint = UI.Hints.Show(this.NotSafeZoneMessage, HintLayer.SoulFlame, 1f);
						break;
					case SeinSoulFlame.SoulFlamePlacementSafety.SavePedestal:
						this.m_notSafeHint = UI.Hints.Show(this.SavePedestalZoneMessage, HintLayer.SoulFlame, 1f);
						break;
					}
					Sound.Play(this.NotSafeSound.GetSound(null), base.transform.position, null);
				}
			}
			if (this.m_isCasting && this.m_sein.IsOnGround && this.m_delayOnGround == 0f && this.m_tapRemainingTime > 0f)
			{
				this.m_tapRemainingTime -= Time.deltaTime;
				if (this.m_tapRemainingTime < 0f && this.InsideCheckpointMarker && Characters.Sein.PlayerAbilities.Rekindle.HasAbility && this.IsSafeToCastSoulFlame == SeinSoulFlame.SoulFlamePlacementSafety.Safe)
				{
					SeinSoulFlame.OnSoulFlameCast();
					Vector3 position = Characters.Sein.Position;
					Characters.Sein.Position = this.m_soulFlame.Position;
					SaveSlotBackupsManager.CreateCurrentBackup();
					GameController.Instance.CreateCheckpoint();
					Characters.Sein.Position = position;
					GameController.Instance.SaveGameController.PerformSave();
					this.m_soulFlame.OnRekindle();
					GameController.Instance.PerformSaveGameSequence();
				}
			}
			if (Core.Input.SoulFlame.Released)
			{
				this.m_isCasting = false;
				if (this.m_tapRemainingTime > 0f)
				{
					this.m_tapRemainingTime = 0f;
					if (this.AllowedToAccessSkillTree && this.InsideCheckpointMarker)
					{
						if (this.m_skillTreeHint)
						{
							this.m_skillTreeHint.Visibility.HideImmediately();
						}
						Core.Input.Start.Used = true;
						UI.Menu.ShowSkillTree();
					}
				}
			}
		}
		else
		{
			this.m_tapRemainingTime = 0f;
		}
		if (this.m_holdDownTime == 1f && this.m_sein.IsOnGround && this.m_delayOnGround == 0f)
		{
			this.CastSoulFlame();
		}
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x0005040C File Offset: 0x0004E60C
	private void CastSoulFlame()
	{
		if (this.ChargingSound)
		{
			this.ChargingSound.StopAndFadeOut(0.1f);
		}
		this.m_sein.Energy.Spend(this.SoulFlameCost);
		this.m_cooldownRemaining = 1f;
		this.m_holdDownTime = 0f;
		if (this.m_sein.PlayerAbilities.Regroup.HasAbility)
		{
			this.m_sein.Mortality.Health.GainHealth(4);
		}
		if (this.m_sein.PlayerAbilities.UltraSoulFlame.HasAbility)
		{
			this.m_sein.Mortality.Health.GainHealth(4);
		}
		this.m_sceneCheckpoint = new MoonGuid(Scenes.Manager.CurrentScene.SceneMoonGuid);
		if (this.m_checkpointMarkerGameObject)
		{
			this.m_checkpointMarkerGameObject.GetComponent<SoulFlame>().Disappear();
		}
		this.SpawnSoulFlame(Characters.Sein.Position);
		SeinSoulFlame.OnSoulFlameCast();
		SaveSlotBackupsManager.CreateCurrentBackup();
		GameController.Instance.CreateCheckpoint();
		GameController.Instance.SaveGameController.PerformSave();
		this.m_numberOfSoulFlamesCast++;
		if (this.m_numberOfSoulFlamesCast == 50)
		{
			AchievementsController.AwardAchievement(AchievementsLogic.Instance.SoulLinkManyTimesAchievementAsset);
		}
		if (this.CheckpointSequence)
		{
			this.CheckpointSequence.Perform(null);
		}
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x00050580 File Offset: 0x0004E780
	private void HandleCharging()
	{
		if (this.m_isCasting && this.CanAffordSoulFlame && this.IsSafeToCastSoulFlame == SeinSoulFlame.SoulFlamePlacementSafety.Safe && this.m_cooldownRemaining == 0f && !this.InsideCheckpointMarker && this.PlayerCouldSoulFlame)
		{
			if (this.m_holdDownTime == 0f && this.ChargingSound)
			{
				this.ChargingSound.Play();
			}
			this.m_holdDownTime += Time.deltaTime / this.HoldDownDuration;
			if (this.m_holdDownTime > 1f)
			{
				this.m_holdDownTime = 1f;
			}
			this.ChargeEffectAnimator.AnimatorDriver.ContinueForward();
		}
		else
		{
			this.ChargeEffectAnimator.AnimatorDriver.ContinueBackwards();
			if (this.ChargingSound && this.ChargingSound.IsPlaying)
			{
				this.ChargingSound.StopAndFadeOut(0.1f);
			}
			if (this.m_holdDownTime > 0f)
			{
				if (this.AbortChargingSound && !this.AbortChargingSound.IsPlaying)
				{
					this.AbortChargingSound.Play();
				}
				this.m_holdDownTime -= Time.deltaTime / this.HoldDownDuration;
				if (this.m_holdDownTime <= 0f)
				{
					this.m_holdDownTime = 0f;
					if (this.AbortChargingSound)
					{
						this.AbortChargingSound.StopAndFadeOut(0.1f);
					}
					if (this.FullyAbortedSound)
					{
						Sound.Play(this.FullyAbortedSound.GetSound(null), base.transform.position, null);
					}
				}
			}
		}
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x00050748 File Offset: 0x0004E948
	private void HandleCooldown()
	{
		if (this.m_cooldownRemaining > 0f)
		{
			this.m_nagTimer = 0f;
			if (this.m_sein.PlayerAbilities.Rekindle.HasAbility)
			{
				this.m_cooldownRemaining -= Time.deltaTime / this.RekindleCooldownDuration;
			}
			else
			{
				this.m_cooldownRemaining -= Time.deltaTime / this.CooldownDuration;
			}
			if (this.m_cooldownRemaining <= 0f)
			{
				this.m_cooldownRemaining = 0f;
				this.m_readyForReadySequence = true;
			}
		}
	}

	// Token: 0x0600117F RID: 4479 RVA: 0x000507E4 File Offset: 0x0004E9E4
	private void HandleCheckpointMarkerVisibility()
	{
		if (this.m_checkpointMarkerGameObject)
		{
			bool flag = Scenes.Manager.SceneIsEnabled(this.m_sceneCheckpoint);
			bool flag2 = UI.Cameras.Current.IsOnScreenPadded(this.m_soulFlame.Position, 5f);
			if (this.m_checkpointMarkerGameObject.activeSelf)
			{
				if (!flag && !flag2)
				{
					this.m_checkpointMarkerGameObject.SetActive(false);
				}
			}
			else if (flag)
			{
				this.m_checkpointMarkerGameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x0005086C File Offset: 0x0004EA6C
	private void HandleSkillTreeHint()
	{
		if (this.AllowedToAccessSkillTree)
		{
			if (this.InsideCheckpointMarker && this.SkillTreeMessage && this.SkillTreeRekindleMessage && this.PlayerCouldSoulFlame)
			{
				if (this.m_skillTreeHint == null)
				{
					MessageProvider messageProvider = (!Characters.Sein.PlayerAbilities.Rekindle.HasAbility || this.IsSafeToCastSoulFlame != SeinSoulFlame.SoulFlamePlacementSafety.Safe) ? this.SkillTreeMessage : this.SkillTreeRekindleMessage;
					this.m_skillTreeHint = UI.Hints.Show(messageProvider, HintLayer.SoulFlame, float.PositiveInfinity);
				}
			}
			else if (this.m_skillTreeHint)
			{
				this.m_skillTreeHint.HideMessageScreen();
			}
		}
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x00050934 File Offset: 0x0004EB34
	public void HideOtherMessages()
	{
		if (this.m_notReadyHint)
		{
			this.m_notReadyHint.HideMessageScreen();
		}
		if (this.m_notSafeHint)
		{
			this.m_notSafeHint.HideMessageScreen();
		}
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x00050977 File Offset: 0x0004EB77
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
		this.m_sein.SoulFlame = this;
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x0005098C File Offset: 0x0004EB8C
	public override void Serialize(Archive ar)
	{
		base.Serialize(ar);
		ar.Serialize(ref this.m_cooldownRemaining);
		ar.Serialize(ref this.m_readyForReadySequence);
		ar.Serialize(ref this.m_nagTimer);
		this.m_sceneCheckpoint.Serialize(ar);
		ar.Serialize(ref this.m_numberOfSoulFlamesCast);
		if (ar.Writing)
		{
			ar.Serialize(this.m_soulFlame != null);
			if (this.m_soulFlame)
			{
				ar.Serialize(this.m_soulFlame.Position);
			}
		}
		else
		{
			bool flag = false;
			ar.Serialize(ref flag);
			if (flag)
			{
				Vector3 zero = Vector3.zero;
				ar.Serialize(ref zero);
				if (this.m_soulFlame)
				{
					this.m_soulFlame.Position = zero;
				}
				else
				{
					this.SpawnSoulFlame(zero);
				}
			}
			else
			{
				this.DestroySoulFlame();
			}
		}
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x00050A72 File Offset: 0x0004EC72
	public void SpawnSoulFlame(Vector3 position)
	{
		this.m_checkpointMarkerGameObject = (GameObject)InstantiateUtility.Instantiate(this.CheckpointMarker, position, Quaternion.identity);
		this.m_soulFlame = this.m_checkpointMarkerGameObject.GetComponent<SoulFlame>();
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x00050AA4 File Offset: 0x0004ECA4
	public void DestroySoulFlame()
	{
		if (this.m_soulFlame)
		{
			InstantiateUtility.Destroy(this.m_soulFlame.gameObject);
			this.m_soulFlame = null;
			this.m_checkpointMarkerGameObject = null;
		}
	}

	// Token: 0x04000EF7 RID: 3831
	public BaseAnimator ChargeEffectAnimator;

	// Token: 0x04000EF8 RID: 3832
	public GameObject CheckpointMarker;

	// Token: 0x04000EF9 RID: 3833
	public ActionMethod CheckpointSequence;

	// Token: 0x04000EFA RID: 3834
	public AnimationCurve ParticleRateOverSpeed;

	// Token: 0x04000EFB RID: 3835
	public AchievementAsset CreateManySoulLinkAchievement;

	// Token: 0x04000EFC RID: 3836
	public MessageProvider SkillTreeRekindleMessage;

	// Token: 0x04000EFD RID: 3837
	public MessageProvider SkillTreeMessage;

	// Token: 0x04000EFE RID: 3838
	public MessageProvider NotSafeZoneMessage;

	// Token: 0x04000EFF RID: 3839
	public MessageProvider NotSafeEnemiesMessage;

	// Token: 0x04000F00 RID: 3840
	public MessageProvider NotSafeGroundMessage;

	// Token: 0x04000F01 RID: 3841
	public MessageProvider SavePedestalZoneMessage;

	// Token: 0x04000F02 RID: 3842
	public MessageProvider NotReadyMessage;

	// Token: 0x04000F03 RID: 3843
	public LayerMask UnsafeMask;

	// Token: 0x04000F04 RID: 3844
	private MessageBox m_notSafeHint;

	// Token: 0x04000F05 RID: 3845
	private MessageBox m_notReadyHint;

	// Token: 0x04000F06 RID: 3846
	private MessageBox m_skillTreeHint;

	// Token: 0x04000F07 RID: 3847
	private GameObject m_checkpointMarkerGameObject;

	// Token: 0x04000F08 RID: 3848
	private SoulFlame m_soulFlame;

	// Token: 0x04000F09 RID: 3849
	private SeinCharacter m_sein;

	// Token: 0x04000F0A RID: 3850
	private int m_numberOfSoulFlamesCast;

	// Token: 0x04000F0B RID: 3851
	private float m_holdDownTime;

	// Token: 0x04000F0C RID: 3852
	public float HoldDownDuration = 0.7f;

	// Token: 0x04000F0D RID: 3853
	private float m_nagTimer;

	// Token: 0x04000F0E RID: 3854
	public float NagDuration = 120f;

	// Token: 0x04000F0F RID: 3855
	public bool LockSoulFlame;

	// Token: 0x04000F10 RID: 3856
	public SoundProvider NotSafeSound;

	// Token: 0x04000F11 RID: 3857
	public SoundProvider NotReadySound;

	// Token: 0x04000F12 RID: 3858
	public SoundSource ChargingSound;

	// Token: 0x04000F13 RID: 3859
	public SoundSource AbortChargingSound;

	// Token: 0x04000F14 RID: 3860
	public SoundProvider FullyAbortedSound;

	// Token: 0x04000F15 RID: 3861
	public SoundProvider SoulFlameReadySoundProvider;

	// Token: 0x04000F16 RID: 3862
	public GameObject SoulFlameReadyEffect;

	// Token: 0x04000F17 RID: 3863
	public GameObject SoulFlameReadyText;

	// Token: 0x04000F18 RID: 3864
	public float CooldownDuration = 60f;

	// Token: 0x04000F19 RID: 3865
	public float RekindleCooldownDuration = 10f;

	// Token: 0x04000F1A RID: 3866
	private float m_cooldownRemaining;

	// Token: 0x04000F1B RID: 3867
	private bool m_readyForReadySequence;

	// Token: 0x04000F1C RID: 3868
	private float m_tapRemainingTime;

	// Token: 0x04000F1D RID: 3869
	private MoonGuid m_sceneCheckpoint = new MoonGuid(0, 0, 0, 0);

	// Token: 0x04000F1E RID: 3870
	private bool m_isCasting;

	// Token: 0x04000F1F RID: 3871
	private float m_delayOnGround;

	// Token: 0x0200046D RID: 1133
	public enum SoulFlamePlacementSafety
	{
		// Token: 0x04001AF6 RID: 6902
		Safe,
		// Token: 0x04001AF7 RID: 6903
		UnsafeEnemies,
		// Token: 0x04001AF8 RID: 6904
		UnsafeGround,
		// Token: 0x04001AF9 RID: 6905
		UnsafeZone,
		// Token: 0x04001AFA RID: 6906
		SavePedestal
	}
}
