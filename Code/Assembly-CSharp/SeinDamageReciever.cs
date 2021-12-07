using System;
using System.Collections;
using Core;
using Game;
using UnityEngine;

// Token: 0x020000BE RID: 190
public class SeinDamageReciever : CharacterState, IDamageReciever, ISeinReceiver, IProjectileDetonatable
{
	// Token: 0x170001BF RID: 447
	// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00022077 File Offset: 0x00020277
	public CharacterLeftRightMovement CharacterLeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x060007EA RID: 2026 RVA: 0x00022089 File Offset: 0x00020289
	public CharacterGravity CharacterGravity
	{
		get
		{
			return this.Sein.PlatformBehaviour.Gravity;
		}
	}

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x060007EB RID: 2027 RVA: 0x0002209B File Offset: 0x0002029B
	public CharacterInstantStop CharacterInstantStop
	{
		get
		{
			return this.Sein.PlatformBehaviour.InstantStop;
		}
	}

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x060007EC RID: 2028 RVA: 0x000220AD File Offset: 0x000202AD
	public SeinHealthController HealthController
	{
		get
		{
			return this.Sein.Mortality.Health;
		}
	}

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x060007ED RID: 2029 RVA: 0x000220BF File Offset: 0x000202BF
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x060007EE RID: 2030 RVA: 0x000220D1 File Offset: 0x000202D1
	public Renderer Sprite
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.SpriteRenderer;
		}
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x000220E8 File Offset: 0x000202E8
	public void Start()
	{
		this.CharacterGravity.ModifyGravityPlatformMovementSettingsEvent += this.ModifyGravityPlatformMovementSettings;
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent += this.ModifyHorizontalPlatformMovementSettings;
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0002213C File Offset: 0x0002033C
	public new void OnDestroy()
	{
		base.OnDestroy();
		this.CharacterGravity.ModifyGravityPlatformMovementSettingsEvent -= this.ModifyGravityPlatformMovementSettings;
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent -= this.ModifyHorizontalPlatformMovementSettings;
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00022193 File Offset: 0x00020393
	public override void OnEnter()
	{
		this.CharacterInstantStop.Active = false;
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x000221A1 File Offset: 0x000203A1
	public override void OnExit()
	{
		this.CharacterInstantStop.Active = true;
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x000221B0 File Offset: 0x000203B0
	public void OnRecieveDamage(Damage damage)
	{
		if (this.IsImmortal)
		{
			return;
		}
		if (!this.Sein.Controller.CanMove)
		{
			return;
		}
		if (damage.Type == DamageType.SpiritFlameSplatter || damage.Type == DamageType.LevelUp)
		{
			return;
		}
		damage.SetAmount(Mathf.Round(damage.Amount));
		bool flag = this.m_invincibleTimeRemaining > 0f;
		bool flag2 = this.m_invincibleToEnemiesTimeRemaining > 0f;
		if (this.Sein.Abilities.Stomp && this.Sein.Abilities.Stomp.Logic.CurrentState == this.Sein.Abilities.Stomp.State.StompDown)
		{
			flag = true;
		}
		if (!this.Sein.gameObject.activeInHierarchy)
		{
			return;
		}
		if (flag && damage.Amount < 100f && damage.Type != DamageType.Drowning)
		{
			damage.SetAmount(0f);
		}
		if (flag2 && damage.Amount < 100f && (damage.Type == DamageType.Enemy || damage.Type == DamageType.Projectile || damage.Type == DamageType.SlugSpike))
		{
			damage.SetAmount(0f);
		}
		if (damage.Amount == 0f)
		{
			return;
		}
		if (damage.Amount < 100f)
		{
			switch (DifficultyController.Instance.Difficulty)
			{
			case DifficultyMode.Easy:
				if (damage.Type != DamageType.Lava && damage.Type != DamageType.Spikes)
				{
					damage.SetAmount(damage.Amount / 2f);
				}
				else
				{
					int num = Mathf.RoundToInt(damage.Amount / 4f);
					if (num > 3)
					{
						num = Mathf.FloorToInt((float)(num - 3) * 0.5f) + 3;
					}
					damage.SetAmount((float)(num * 4));
				}
				break;
			case DifficultyMode.Hard:
				damage.SetAmount(damage.Amount * 2f);
				if (damage.Amount < 8f)
				{
					damage.SetAmount(8f);
				}
				break;
			}
		}
		UI.Vignette.SeinHurt.Restart();
		SoundDescriptor soundForDamage = ((damage.Amount >= this.BadlyHurtAmount) ? this.SeinBadlyHurtSound : this.SeinHurtSound).GetSoundForDamage(damage);
		if (soundForDamage != null)
		{
			SoundPlayer soundPlayer = Sound.Play(soundForDamage, this.PlatformMovement.Position, null);
			if (soundPlayer)
			{
				soundPlayer.AttachTo = this.Sein.PlatformBehaviour.transform;
			}
		}
		int num2 = Mathf.CeilToInt(damage.Amount / 4f);
		damage.SetAmount((float)num2);
		if (damage.Amount < 1000f && this.Sein.PlayerAbilities.UltraDefense.HasAbility)
		{
			damage.SetAmount((float)Mathf.RoundToInt((float)num2 * 0.8f));
		}
		Attacking.DamageDisplayText.Create(damage, this.Sein.transform);
		damage.SetAmount((float)(num2 * 4));
		if (damage.Amount < 1000f && this.Sein.PlayerAbilities.UltraDefense.HasAbility)
		{
			damage.SetAmount((float)(Mathf.FloorToInt((float)(num2 * 2) * 0.8f) * 2));
		}
		int num3 = Mathf.RoundToInt(damage.Amount);
		if ((float)num3 >= this.HealthController.Amount)
		{
			this.Sein.Mortality.Health.TakeDamage(num3);
			this.OnKill(damage);
		}
		else
		{
			this.Sein.Mortality.Health.TakeDamage(num3);
			if (damage.Type != DamageType.Drowning)
			{
				this.MakeInvincible(1f);
				base.StartCoroutine(this.FlashSprite());
				if (this.HurtEffect)
				{
					GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.HurtEffect);
					gameObject.transform.position = base.transform.position;
					Vector3 vector = this.PlatformMovement.LocalSpeed.normalized + damage.Force.normalized;
					float z = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
					gameObject.transform.rotation = Quaternion.Euler(0f, 0f, z);
				}
				base.Active = true;
				if (this.Sein.Abilities.GrabWall)
				{
					this.Sein.Abilities.GrabWall.Exit();
				}
				if (this.Sein.Abilities.Dash)
				{
					this.Sein.Abilities.Dash.Exit();
				}
				this.PlatformMovement.LocalSpeed = ((damage.Force.x <= 0f) ? new Vector2(-this.HurtSpeed.x, this.HurtSpeed.y) : this.HurtSpeed);
				this.m_hurtTimeRemaining = this.HurtDuration;
				this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.HurtAnimation, 140, new Func<bool>(this.ShouldHurtAnimationKeepPlaying));
			}
			else
			{
				base.StartCoroutine(this.FlashSprite());
			}
		}
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x00022735 File Offset: 0x00020935
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x00022740 File Offset: 0x00020940
	public override void UpdateCharacterState()
	{
		if (this.Sein.IsSuspended)
		{
			return;
		}
		this.m_hurtTimeRemaining -= Time.deltaTime;
		this.m_invincibleTimeRemaining -= Time.deltaTime;
		this.m_invincibleToEnemiesTimeRemaining -= Time.deltaTime;
		if (this.m_hurtTimeRemaining < 0f)
		{
			this.m_hurtTimeRemaining = 0f;
		}
		if (this.m_invincibleTimeRemaining < 0f)
		{
			this.m_invincibleTimeRemaining = 0f;
		}
		if (this.m_invincibleToEnemiesTimeRemaining < 0f)
		{
			this.m_invincibleToEnemiesTimeRemaining = 0f;
		}
		if (base.Active && this.m_hurtTimeRemaining == 0f)
		{
			base.Active = false;
		}
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x00022807 File Offset: 0x00020A07
	public void ModifyHorizontalPlatformMovementSettings(HorizontalPlatformMovementSettings settings)
	{
		if (base.Active)
		{
			settings.Ground.ApplySpeedMultiplier(this.MoveSpeed);
			settings.Air.ApplySpeedMultiplier(this.MoveSpeed);
		}
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x00022836 File Offset: 0x00020A36
	public void ModifyGravityPlatformMovementSettings(GravityPlatformMovementSettings settings)
	{
		if (base.Active)
		{
			settings.GravityStrength *= this.GravityMultiplier;
		}
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x00022856 File Offset: 0x00020A56
	public void MakeInvincible(float duration)
	{
		this.m_invincibleTimeRemaining = Mathf.Max(this.m_invincibleTimeRemaining, duration);
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x0002286A File Offset: 0x00020A6A
	public void MakeInvincibleToEnemies(float duration)
	{
		this.m_invincibleToEnemiesTimeRemaining = Mathf.Max(this.m_invincibleToEnemiesTimeRemaining, duration);
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x0002287E File Offset: 0x00020A7E
	public void ResetInviciblity()
	{
		this.m_invincibleTimeRemaining = 0f;
		this.m_invincibleToEnemiesTimeRemaining = 0f;
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00022898 File Offset: 0x00020A98
	public void OnRestoreCheckpoint()
	{
		this.SpriteMaterialTintColor(new Color(0f, 0f, 0f, 0f));
		CameraFrustumOptimizer.ForceUpdate();
		if (this.m_died)
		{
			this.m_died = false;
			this.Sein.Active = true;
			this.Sein.GetComponent<GoThroughPlatformHandler>().UpdateColliders();
			this.Sein.Mortality.Health.OnRespawn();
			if (WorldMapLogic.Instance.MapEnabledArea.FindFaceAtPositionFaster(Characters.Sein.Position) != null)
			{
				GameController.Instance.SaveGameController.PerformSave();
			}
		}
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x0002293C File Offset: 0x00020B3C
	public IEnumerator FlashSprite()
	{
		yield return new WaitForFixedUpdate();
		for (int i = 0; i < 8; i++)
		{
			this.SpriteMaterialTintColor(Color.red);
			yield return new WaitForSeconds(0.05f);
			this.SpriteMaterialTintColor(new Color(0f, 0f, 0f, 0f));
			yield return new WaitForSeconds(0.05f);
		}
		yield break;
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x00022958 File Offset: 0x00020B58
	public void SpriteMaterialTintColor(Color color)
	{
		if (this.Sprite)
		{
			this.Sprite.sharedMaterial.SetColor(ShaderProperties.TintColor, color);
		}
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x0002298B File Offset: 0x00020B8B
	public void OnEnable()
	{
		this.SpriteMaterialTintColor(new Color(0f, 0f, 0f, 0f));
		this.m_invincibleTimeRemaining = 0f;
		this.m_invincibleToEnemiesTimeRemaining = 0f;
	}

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x060007FF RID: 2047 RVA: 0x000229C2 File Offset: 0x00020BC2
	public bool IsInvinsible
	{
		get
		{
			return this.m_invincibleTimeRemaining > 0f;
		}
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x000229D1 File Offset: 0x00020BD1
	public bool ShouldHurtAnimationKeepPlaying()
	{
		return !this.PlatformMovement.IsOnGround;
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x000229E4 File Offset: 0x00020BE4
	public void OnKill(Damage damage)
	{
		if (!this.Sein.Active)
		{
			return;
		}
		this.m_died = true;
		SoundDescriptor soundForDamage = this.SeinDeathSound.GetSoundForDamage(damage);
		if (soundForDamage != null)
		{
			SoundPlayer soundPlayer = Sound.Play(soundForDamage, this.PlatformMovement.Position, null);
			if (soundPlayer)
			{
				soundPlayer.AttachTo = this.Sein.PlatformBehaviour.transform;
			}
		}
		Utility.DisableLate(this.Sein);
		SeinDeathCounter.Count++;
		SeinDeathsManager.OnDeath();
		GameController.Instance.ResumeGameplay();
		if (this.DeathEffectProvider)
		{
			this.InstantiateDeathEffect(damage);
		}
		Events.Scheduler.OnPlayerDeath.Call();
		if (DifficultyController.Instance.Difficulty == DifficultyMode.OneLife)
		{
			SaveSlotsManager.CurrentSaveSlot.WasKilled = true;
			GameController.Instance.SaveGameController.PerformSave();
			SaveSlotBackupsManager.DeleteAllBackups(SaveSlotsManager.CurrentSlotIndex);
		}
		GameController.Instance.StartCoroutine(this.OnKillRoutine());
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00022AE4 File Offset: 0x00020CE4
	private void InstantiateDeathEffect(Damage damage)
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.DeathEffectProvider.Prefab(new DamageContext(damage)));
		damage.DealToComponents(gameObject);
		Transform transform = this.Sein.PlatformBehaviour.Visuals.SpriteMirror.transform;
		gameObject.transform.localPosition = transform.position;
		gameObject.transform.localScale = transform.localScale;
		gameObject.transform.localRotation = transform.localRotation;
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x00022B68 File Offset: 0x00020D68
	public IEnumerator OnKillRoutine()
	{
		float deathDuration = this.DeathDuration;
		for (float t = 0f; t < deathDuration; t += ((!this.Sein.IsSuspended) ? Time.deltaTime : 0f))
		{
			if (Characters.Sein == null)
			{
				yield break;
			}
			yield return new WaitForFixedUpdate();
		}
		if (DifficultyController.Instance.Difficulty == DifficultyMode.OneLife)
		{
			InstantiateUtility.Instantiate(this.GameOverScreen, Vector3.zero, Quaternion.identity);
		}
		else
		{
			UI.Fader.Fade(this.DeathFadeInDuration, 0f, this.DeathFadeOutDuration, new Action(this.OnKillFadeInComplete), null);
		}
		yield return new WaitForSeconds(this.DeathFadeInDuration);
		SeinDeathCounter.SendTelemetryData();
		yield break;
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x00022B83 File Offset: 0x00020D83
	public void OnKillFadeInComplete()
	{
		GameController.Instance.RestoreCheckpoint(null);
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00022B90 File Offset: 0x00020D90
	public bool CanDetonateProjectiles()
	{
		return this.m_invincibleToEnemiesTimeRemaining == 0f;
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x00022B9F File Offset: 0x00020D9F
	public override void Serialize(Archive ar)
	{
		base.Serialize(ar);
		ar.Serialize(ref this.m_serializationFiller);
	}

	// Token: 0x04000660 RID: 1632
	public SeinCharacter Sein;

	// Token: 0x04000661 RID: 1633
	public TextureAnimationWithTransitions HurtAnimation;

	// Token: 0x04000662 RID: 1634
	public DamageBasedSoundProvider SeinDeathSound;

	// Token: 0x04000663 RID: 1635
	public DamageBasedSoundProvider SeinHurtSound;

	// Token: 0x04000664 RID: 1636
	public DamageBasedSoundProvider SeinBadlyHurtSound;

	// Token: 0x04000665 RID: 1637
	public float BadlyHurtAmount = 4f;

	// Token: 0x04000666 RID: 1638
	public bool IsImmortal;

	// Token: 0x04000667 RID: 1639
	public float HurtDropPickupSpeed = 20f;

	// Token: 0x04000668 RID: 1640
	private float m_invincibleTimeRemaining;

	// Token: 0x04000669 RID: 1641
	private float m_invincibleToEnemiesTimeRemaining;

	// Token: 0x0400066A RID: 1642
	private bool m_died;

	// Token: 0x0400066B RID: 1643
	public GameObject GameOverScreen;

	// Token: 0x0400066C RID: 1644
	public float HurtDuration = 0.25f;

	// Token: 0x0400066D RID: 1645
	public Vector2 HurtSpeed = new Vector2(6f, 9f);

	// Token: 0x0400066E RID: 1646
	public HorizontalPlatformMovementSettings.SpeedMultiplierSet MoveSpeed = new HorizontalPlatformMovementSettings.SpeedMultiplierSet
	{
		AccelerationMultiplier = 0f,
		DeceelerationMultiplier = 0f,
		MaxSpeedMultiplier = 1f
	};

	// Token: 0x0400066F RID: 1647
	public float GravityMultiplier = 2f;

	// Token: 0x04000670 RID: 1648
	public GameObject HurtEffect;

	// Token: 0x04000671 RID: 1649
	public GameObject HurtDropPickup;

	// Token: 0x04000672 RID: 1650
	private float m_hurtTimeRemaining;

	// Token: 0x04000673 RID: 1651
	public GameObject KillFader;

	// Token: 0x04000674 RID: 1652
	public float DeathDuration;

	// Token: 0x04000675 RID: 1653
	public float OneLifeDeathDuration = 2f;

	// Token: 0x04000676 RID: 1654
	public float SpawnDuration;

	// Token: 0x04000677 RID: 1655
	public float DeathFadeInDuration = 0.05f;

	// Token: 0x04000678 RID: 1656
	public float DeathFadeOutDuration = 1f;

	// Token: 0x04000679 RID: 1657
	public DamageBasedPrefabProvider DeathEffectProvider;

	// Token: 0x0400067A RID: 1658
	private int m_serializationFiller;
}
