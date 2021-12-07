using System;
using System.Collections;
using Game;
using UnityEngine;

// Token: 0x0200023F RID: 575
public class AchievementsLogic : SaveSerialize
{
	// Token: 0x060012FF RID: 4863 RVA: 0x00058090 File Offset: 0x00056290
	public override void Awake()
	{
		AchievementsLogic.Instance = this;
		base.Awake();
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
		SeinDoubleJump.OnDoubleJumpEvent += this.OnDoubleJumpEvent;
		SeinStomp.OnStompDownEvent += this.OnStompDownEvent;
		SeinBashAttack.OnBashAttackEvent += this.OnBashAttackEvent;
		SeinBashAttack.OnBashEnemy += this.OnBashEnemy;
		EntityDamageReciever.OnEntityDeathEvent = (Action<Entity>)Delegate.Combine(EntityDamageReciever.OnEntityDeathEvent, new Action<Entity>(this.OnEntityDeathEvent));
		SkillTreeLaneLogic.OnSkillTreeDoneEvent = (Action<SkillTreeLaneLogic.SkillTreeType>)Delegate.Combine(SkillTreeLaneLogic.OnSkillTreeDoneEvent, new Action<SkillTreeLaneLogic.SkillTreeType>(this.OnSkillTreeDoneEvent));
		ReportLocationAction.OnAct1End = (Action)Delegate.Combine(ReportLocationAction.OnAct1End, new Action(this.OnAct1End));
		ReportLocationAction.OnAct2End = (Action)Delegate.Combine(ReportLocationAction.OnAct2End, new Action(this.OnAct2End));
		ReportLocationAction.OnAct3End = (Action)Delegate.Combine(ReportLocationAction.OnAct3End, new Action(this.OnAct3End));
		IgnitableSpiritTorch.OnLightTorchWithGrenadeEvent += this.OnTorchIgniteEvent;
	}

	// Token: 0x06001300 RID: 4864 RVA: 0x000581BC File Offset: 0x000563BC
	public override void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
		SeinDoubleJump.OnDoubleJumpEvent -= this.OnDoubleJumpEvent;
		SeinStomp.OnStompDownEvent -= this.OnStompDownEvent;
		SeinBashAttack.OnBashAttackEvent -= this.OnBashAttackEvent;
		SeinBashAttack.OnBashEnemy -= this.OnBashEnemy;
		EntityDamageReciever.OnEntityDeathEvent = (Action<Entity>)Delegate.Remove(EntityDamageReciever.OnEntityDeathEvent, new Action<Entity>(this.OnEntityDeathEvent));
		SkillTreeLaneLogic.OnSkillTreeDoneEvent = (Action<SkillTreeLaneLogic.SkillTreeType>)Delegate.Remove(SkillTreeLaneLogic.OnSkillTreeDoneEvent, new Action<SkillTreeLaneLogic.SkillTreeType>(this.OnSkillTreeDoneEvent));
		ReportLocationAction.OnAct1End = (Action)Delegate.Remove(ReportLocationAction.OnAct1End, new Action(this.OnAct1End));
		ReportLocationAction.OnAct2End = (Action)Delegate.Remove(ReportLocationAction.OnAct2End, new Action(this.OnAct2End));
		ReportLocationAction.OnAct3End = (Action)Delegate.Remove(ReportLocationAction.OnAct3End, new Action(this.OnAct3End));
		IgnitableSpiritTorch.OnLightTorchWithGrenadeEvent -= this.OnTorchIgniteEvent;
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x000582E0 File Offset: 0x000564E0
	private void OnGameReset()
	{
		this.m_completeMapAchievementAwarded = false;
		this.m_chargeFlameWallsDestroyed = 0f;
		this.m_enemiesKilledByProjectile = 0;
		this.m_enemiesKilledBySuperJumping = 0;
		this.m_savePedestalsUsed = 0;
		this.m_enemiesKilledByOtherEnemies = 0;
		this.m_collectedEnergySlotsCount = 0;
		this.m_doubleJumpFiveTimesNoGroundAchieved = false;
		this.m_bashTenEnemiesAchieved = false;
		this.m_kill3EnemiesWithoutTouchingTheGroundAchieved = false;
		this.m_crushARamEnemyWithAStomperAchieved = false;
		this.m_causeEnemyToDestroyThemselvesAchieved = false;
		this.m_superJump5EnemiesAchieved = false;
		this.m_kill50EnemiesWithStompAchieved = false;
		this.m_kill100EnemiesWithChargeFlameAchieved = false;
		this.m_kill500EnemiesWithSpiritFlameAchieved = false;
		this.m_anEyeForAnEyeAchieved = false;
		this.m_enemiesKilledBySpiritFlameCount = 0;
		this.m_enemiesKilledByChargeFlameCount = 0;
		this.m_enemiesKilledByStompCount = 0;
		this.m_secretsRevealedCount = 0;
		this.m_mapStonesFoundCount = 0;
		AchievementsLogic.Act3Ended = false;
		this.m_chargeDashKillCount = 0;
		this.m_torchIgniteCount = 0;
		this.m_enemiesKilledByGrenadeCount = 0;
		this.m_chargeDashKillFiveTimesAchieved = false;
		this.m_ignitedThreeTorchesAchieved = false;
		this.m_kill50WithGrenadeAchieved = false;
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x000583BB File Offset: 0x000565BB
	public void OnChargeDashKilledEnemy()
	{
		this.m_chargeDashKillCount++;
		if (this.m_chargeDashKillCount == 5 && !this.m_chargeDashKillFiveTimesAchieved)
		{
			AchievementsController.AwardAchievement(this.ChargeDashFiveTimesAchievementAsset);
			this.m_chargeDashKillFiveTimesAchieved = true;
		}
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x000583F4 File Offset: 0x000565F4
	public void OnTorchIgniteEvent()
	{
		this.m_torchIgniteCount++;
		if (this.m_torchIgniteCount == 3 && !this.m_ignitedThreeTorchesAchieved)
		{
			AchievementsController.AwardAchievement(this.IgnitedThreeTorchesAchievementAsset);
			this.m_ignitedThreeTorchesAchieved = true;
		}
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x00058430 File Offset: 0x00056630
	public void OnDoubleJumpEvent(float jumpStrength)
	{
		this.m_doubleJumpCount++;
		if (this.m_doubleJumpCount == 5)
		{
			float past = Time.realtimeSinceStartup - this.m_firstDoubleJumpTime + 10f;
			if (!this.m_doubleJumpFiveTimesNoGroundAchieved)
			{
				AchievementsController.AwardAchievement(this.DoubleJumpFiveTimesNoGroundAchievementAsset);
				XboxOneDVRManager.RecordPastDelayed(15f, past, XboxOneDVR.QUINTUPLE_JUMP_ID);
				this.m_doubleJumpFiveTimesNoGroundAchieved = true;
			}
		}
		if (this.m_doubleJumpCount == 1)
		{
			this.m_firstDoubleJumpTime = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x06001305 RID: 4869 RVA: 0x000584AE File Offset: 0x000566AE
	public void OnStompDownEvent()
	{
		this.m_doubleJumpCount = 0;
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x000584B7 File Offset: 0x000566B7
	public void OnBashAttackEvent(Vector2 speed)
	{
		this.m_doubleJumpCount = 0;
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x000584C0 File Offset: 0x000566C0
	public void OnBashEnemy(EntityTargetting entityTargetting)
	{
		this.m_subsequentEnemiesBashCount++;
		if (this.m_subsequentEnemiesBashCount == 10)
		{
			float past = Time.realtimeSinceStartup - this.m_firstBashTime + 10f;
			if (!this.m_bashTenEnemiesAchieved)
			{
				XboxOneDVRManager.RecordPastDelayed(15f, past, XboxOneDVR.BASHING_SPREE_ID);
				AchievementsController.AwardAchievement(this.BashTenEnemiesAchievementAsset);
				this.m_bashTenEnemiesAchieved = true;
			}
		}
		if (this.m_subsequentEnemiesBashCount == 1)
		{
			this.m_firstBashTime = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x00058540 File Offset: 0x00056740
	public void OnEntityDeathEvent(Entity entity)
	{
		if (Characters.Sein && (Characters.Sein.PlatformBehaviour.PlatformMovement.IsOnGround || Characters.Sein.PlatformBehaviour.PlatformMovement.IsOnWall))
		{
			return;
		}
		if (entity is Enemy)
		{
			this.m_enemiesKilledWithNoTouchingGoundCount++;
			if (this.m_enemiesKilledWithNoTouchingGoundCount == 3)
			{
				float past = Time.realtimeSinceStartup - this.m_firstKilledEnemyInAirTime + 10f;
				if (!this.m_kill3EnemiesWithoutTouchingTheGroundAchieved)
				{
					XboxOneDVRManager.RecordPastDelayed(15f, past, XboxOneDVR.AIRSTRIKE_ID);
					AchievementsController.AwardAchievement(this.Kill3EnemiesWithoutTouchingTheGroundAchievementAsset);
					this.m_kill3EnemiesWithoutTouchingTheGroundAchieved = true;
				}
			}
			if (this.m_enemiesKilledWithNoTouchingGoundCount == 1)
			{
				this.m_firstKilledEnemyInAirTime = Time.realtimeSinceStartup;
			}
		}
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x0005860C File Offset: 0x0005680C
	public void OnSkillTreeDoneEvent(SkillTreeLaneLogic.SkillTreeType type)
	{
		switch (type)
		{
		case SkillTreeLaneLogic.SkillTreeType.Energy:
			AchievementsController.AwardAchievement(this.AllSkillsBranch1AchievementAsset);
			break;
		case SkillTreeLaneLogic.SkillTreeType.Utility:
			AchievementsController.AwardAchievement(this.AllSkillsBranch2AchievementAsset);
			break;
		case SkillTreeLaneLogic.SkillTreeType.Combat:
			AchievementsController.AwardAchievement(this.AllSkillsBranch3AchievementAsset);
			break;
		}
		if (SkillTreeManager.Instance.AllLanesFull)
		{
			AchievementsController.AwardAchievement(this.AllSkillsAchievementAsset);
		}
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x0005867C File Offset: 0x0005687C
	public void OnChargeFlameWallDestroyed()
	{
		this.m_chargeFlameWallsDestroyed += 1f;
		if (this.m_chargeFlameWallsDestroyed == 5f)
		{
			AchievementsController.AwardAchievement(this.FullyChargedAchievementAsset);
		}
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x000586AB File Offset: 0x000568AB
	public void OnAct1End()
	{
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x000586AD File Offset: 0x000568AD
	public void OnAct2End()
	{
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x000586AF File Offset: 0x000568AF
	public void OnAct3End()
	{
		AchievementsLogic.Act3Ended = true;
		base.StartCoroutine(this.OnAct3EndIEnumerator());
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x000586C4 File Offset: 0x000568C4
	private IEnumerator OnAct3EndIEnumerator()
	{
		int deathCount = SeinDeathCounter.Count;
		int completionPercentage = GameWorld.Instance.CompletionPercentage;
		int time = GameController.Instance.GameTimeInSeconds;
		DifficultyMode difficultyMode = DifficultyController.Instance.LowestDifficulty;
		LeaderboardsController.Instance.UploadScoresRoutine(time, deathCount, completionPercentage, difficultyMode);
		yield return new WaitForSeconds(1f);
		LeaderboardsController.SendSurvivorLeaderboardData(new LeaderboardEntryData(time, deathCount, completionPercentage), difficultyMode);
		yield return new WaitForSeconds(1f);
		LeaderboardsController.SendSpeedRunnersLeaderboardData(new LeaderboardEntryData(time, deathCount, completionPercentage), difficultyMode);
		yield return new WaitForSeconds(1f);
		if (GameController.Instance.GameTimeInSeconds <= 10800)
		{
			AchievementsController.AwardAchievement(this.FinishGameUnder6HoursAchievementAsset);
			yield return new WaitForSeconds(1f);
		}
		if (SkillTreeManager.Instance && SkillTreeManager.Instance.CombatLane.Index == 0f && SkillTreeManager.Instance.EnergyLane.Index == 0f && SkillTreeManager.Instance.UtilityLane.Index == 0f)
		{
			AchievementsController.AwardAchievement(this.NoAbilityPointsAchievementAsset);
			yield return new WaitForSeconds(1f);
		}
		if (SeinDeathCounter.Instance == null)
		{
			yield break;
		}
		if (SeinDeathCounter.Count == 0)
		{
			AchievementsController.AwardAchievement(this.NoDeathsAchievementAsset);
		}
		switch (DifficultyController.Instance.LowestDifficulty)
		{
		case DifficultyMode.Hard:
			AchievementsController.AwardAchievement(this.BeatHardAchievementAsset);
			break;
		case DifficultyMode.OneLife:
			AchievementsController.AwardAchievement(this.BeatOneLifeAchievementAsset);
			break;
		}
		Steamworks.SteamInterface.Stats.StoreStats();
		yield break;
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x000586E0 File Offset: 0x000568E0
	public void FixedUpdate()
	{
		if (this.CompletePrologueAchievementAsset.IsEarnt && !GameSettings.Instance.Achieved_CompletePrologue)
		{
			GameSettings.Instance.Achieved_CompletePrologue = true;
		}
		if (this.ReachSpiritTreeAchievementAsset.IsEarnt && !GameSettings.Instance.Achieved_ReachSpiritTree)
		{
			GameSettings.Instance.Achieved_ReachSpiritTree = true;
		}
		if (this.UseFirstAbilityPointAchievementAsset.IsEarnt && !GameSettings.Instance.Achieved_ActivateFirstSkill)
		{
			GameSettings.Instance.Achieved_ActivateFirstSkill = true;
		}
		if (this.FindOneSecretAchievementAsset.IsEarnt && !GameSettings.Instance.Achieved_FindSecret)
		{
			GameSettings.Instance.Achieved_FindSecret = true;
		}
		if (this.ReplaceOneStoneTabletAchievementAsset.IsEarnt && !GameSettings.Instance.Achieved_FindMapStone)
		{
			GameSettings.Instance.Achieved_FindMapStone = true;
		}
		if (GameSettings.Instance.IsTrialAchievementsDirty)
		{
			GameSettings.Instance.SaveSettings();
		}
		if (this.m_shouldGrantAchievements)
		{
			if (GameSettings.Instance.Achieved_CompletePrologue)
			{
				AchievementsController.AwardAchievement(this.CompletePrologueAchievementAsset);
			}
			if (GameSettings.Instance.Achieved_ReachSpiritTree)
			{
				AchievementsController.AwardAchievement(this.ReachSpiritTreeAchievementAsset);
			}
			if (GameSettings.Instance.Achieved_ActivateFirstSkill)
			{
				AchievementsController.AwardAchievement(this.UseFirstAbilityPointAchievementAsset);
			}
			if (GameSettings.Instance.Achieved_FindSecret)
			{
				AchievementsController.AwardAchievement(this.FindOneSecretAchievementAsset);
			}
			if (GameSettings.Instance.Achieved_FindMapStone)
			{
				AchievementsController.AwardAchievement(this.ReplaceOneStoneTabletAchievementAsset);
			}
			this.m_shouldGrantAchievements = false;
		}
		if (this.m_completeMapSampleTime <= 0f)
		{
			this.m_completeMapSampleTime = 5f;
			if (!this.m_completeMapAchievementAwarded && GameWorld.Instance && Mathf.Approximately(GameWorld.Instance.CompletionAmount, 1f))
			{
				this.m_completeMapAchievementAwarded = true;
				AchievementsController.AwardAchievement(this.CompleteMapAchievementAsset);
			}
		}
		this.m_completeMapSampleTime -= Time.fixedDeltaTime;
		if (Characters.Sein && (Characters.Sein.PlatformBehaviour.PlatformMovement.IsOnWall || Characters.Sein.PlatformBehaviour.PlatformMovement.IsOnGround || Characters.Sein.Controller.IsSwimming || Characters.Sein.Controller.IsGliding))
		{
			this.m_doubleJumpCount = 0;
			this.m_subsequentEnemiesBashCount = 0;
			this.m_enemiesKilledWithNoTouchingGoundCount = 0;
		}
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x0005895C File Offset: 0x00056B5C
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_chargeFlameWallsDestroyed);
		ar.Serialize(ref this.m_enemiesKilledByProjectile);
		ar.Serialize(ref this.m_enemiesKilledBySuperJumping);
		ar.Serialize(ref this.m_savePedestalsUsed);
		ar.Serialize(ref this.m_enemiesKilledByOtherEnemies);
		ar.Serialize(ref this.m_collectedEnergySlotsCount);
		ar.Serialize(ref this.m_doubleJumpFiveTimesNoGroundAchieved);
		ar.Serialize(ref this.m_bashTenEnemiesAchieved);
		ar.Serialize(ref this.m_kill3EnemiesWithoutTouchingTheGroundAchieved);
		ar.Serialize(ref this.m_crushARamEnemyWithAStomperAchieved);
		ar.Serialize(ref this.m_causeEnemyToDestroyThemselvesAchieved);
		ar.Serialize(ref this.m_superJump5EnemiesAchieved);
		ar.Serialize(ref this.m_kill50EnemiesWithStompAchieved);
		ar.Serialize(ref this.m_kill100EnemiesWithChargeFlameAchieved);
		ar.Serialize(ref this.m_kill500EnemiesWithSpiritFlameAchieved);
		ar.Serialize(ref this.m_anEyeForAnEyeAchieved);
		ar.Serialize(ref this.m_enemiesKilledBySpiritFlameCount);
		ar.Serialize(ref this.m_enemiesKilledByChargeFlameCount);
		ar.Serialize(ref this.m_enemiesKilledByStompCount);
		ar.Serialize(ref this.m_secretsRevealedCount);
		ar.Serialize(ref this.m_mapStonesFoundCount);
		ar.Serialize(ref this.m_chargeDashKillCount);
		ar.Serialize(ref this.m_torchIgniteCount);
		ar.Serialize(ref this.m_enemiesKilledByGrenadeCount);
		ar.Serialize(ref this.m_chargeDashKillFiveTimesAchieved);
		ar.Serialize(ref this.m_ignitedThreeTorchesAchieved);
		ar.Serialize(ref this.m_kill50WithGrenadeAchieved);
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x00058AB0 File Offset: 0x00056CB0
	public void HandleTrialAchievements()
	{
		if (GameController.Instance.IsTrial)
		{
			return;
		}
		for (int i = 0; i < SaveSlotsManager.Instance.SaveSlots.Count; i++)
		{
			if (SaveSlotsManager.Instance.SaveSlots[i] != null && SaveSlotsManager.Instance.SaveSlots[i].IsTrialSave)
			{
				this.m_shouldGrantAchievements = true;
			}
		}
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x00058B24 File Offset: 0x00056D24
	public void OnProjectileKilledEnemy()
	{
		this.m_enemiesKilledByProjectile++;
		if (this.m_enemiesKilledByProjectile == 25)
		{
			AchievementsController.AwardAchievement(this.KillManyWithBashRedirectAchievementAsset);
		}
		if (this.m_enemiesKilledByProjectile == 25 && !this.m_anEyeForAnEyeAchieved)
		{
			XboxOneDVRManager.RecordPastDelayed(10f, 20f, XboxOneDVR.PROJECTILE_TAKEDOWN_ID);
			this.m_anEyeForAnEyeAchieved = true;
		}
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x00058B8A File Offset: 0x00056D8A
	public void OnSpiritFlameKilledEnemy()
	{
		this.m_enemiesKilledBySpiritFlameCount++;
		if (this.m_enemiesKilledBySpiritFlameCount == 500)
		{
			AchievementsController.AwardAchievement(this.Kill500WithSpiritFlameAchievementAsset);
		}
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x00058BB8 File Offset: 0x00056DB8
	public void OnChargeFlameKilledEnemy()
	{
		this.m_enemiesKilledByChargeFlameCount++;
		if (this.m_enemiesKilledByChargeFlameCount == 100)
		{
			AchievementsController.AwardAchievement(this.Kill100WithChargeFlameAchievementAsset);
		}
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x00058BEC File Offset: 0x00056DEC
	public void OnStompKilledEnemy()
	{
		this.m_enemiesKilledByStompCount++;
		if (this.m_enemiesKilledByStompCount == 50)
		{
			AchievementsController.AwardAchievement(this.Kill50WithStompAchievementAsset);
		}
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x00058C20 File Offset: 0x00056E20
	public void OnGrenaedKilledEnemy()
	{
		this.m_enemiesKilledByGrenadeCount++;
		if (this.m_enemiesKilledByGrenadeCount == 50)
		{
			AchievementsController.AwardAchievement(this.Kill50WithGrenadeAchievementAsset);
		}
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x00058C54 File Offset: 0x00056E54
	public void OnSuperJumpedThroughEnemy()
	{
		this.m_enemiesKilledBySuperJumping++;
		if (this.m_enemiesKilledBySuperJumping == 5 && !this.m_superJump5EnemiesAchieved)
		{
			XboxOneDVRManager.RecordPastDelayed(10f, 20f, XboxOneDVR.SUPER_JUMP_ID);
			AchievementsController.AwardAchievement(this.SuperJump5EnemiesAchievementAsset);
			this.m_superJump5EnemiesAchieved = true;
		}
	}

	// Token: 0x06001318 RID: 4888 RVA: 0x00058CAC File Offset: 0x00056EAC
	public void RevealTransparentWall()
	{
		this.m_secretsRevealedCount++;
		int secretsRevealedCount = this.m_secretsRevealedCount;
		if (secretsRevealedCount != 1)
		{
			if (secretsRevealedCount != 22)
			{
				if (secretsRevealedCount == 45)
				{
					AchievementsController.AwardAchievement(this.FindAllSecretsAchievementAsset);
				}
			}
			else
			{
				AchievementsController.AwardAchievement(this.FindHalfSecretsAchievementAsset);
			}
		}
		else
		{
			AchievementsController.AwardAchievement(this.FindOneSecretAchievementAsset);
		}
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x00058D1C File Offset: 0x00056F1C
	public void OnSavePedestalUsedFirstTime()
	{
		this.m_savePedestalsUsed++;
		if (this.m_savePedestalsUsed == 12)
		{
			AchievementsController.AwardAchievement(this.ActivateEverySpiritPortalAchievementAsset);
		}
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x00058D50 File Offset: 0x00056F50
	public void OnMapStoneActivated()
	{
		this.m_mapStonesFoundCount++;
		int mapStonesFoundCount = this.m_mapStonesFoundCount;
		switch (mapStonesFoundCount)
		{
		case 1:
			AchievementsController.AwardAchievement(this.ReplaceOneStoneTabletAchievementAsset);
			break;
		default:
			if (mapStonesFoundCount == 9)
			{
				AchievementsController.AwardAchievement(this.ReplacedAllStoneTabletsAchievementAsset);
			}
			break;
		case 4:
			AchievementsController.AwardAchievement(this.ReplaceHalfStoneTabletsAchievementAsset);
			break;
		}
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x00058DC7 File Offset: 0x00056FC7
	public void OnJuggledPushBlock()
	{
		AchievementsController.AwardAchievement(this.JuggleRockAchievementAsset);
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x00058DD4 File Offset: 0x00056FD4
	public void OnCrushRamWithStomper()
	{
		if (!this.m_crushARamEnemyWithAStomperAchieved)
		{
			XboxOneDVRManager.RecordPastDelayed(10f, 20f, XboxOneDVR.CRUSHED_RAM_ID);
			AchievementsController.AwardAchievement(this.CrushARamEnemyWithAStomperAchievementAsset);
			this.m_crushARamEnemyWithAStomperAchieved = true;
		}
	}

	// Token: 0x0600131D RID: 4893 RVA: 0x00058E07 File Offset: 0x00057007
	public void OnEnemyKilledItself()
	{
		if (!this.m_causeEnemyToDestroyThemselvesAchieved)
		{
			AchievementsController.AwardAchievement(this.CauseEnemyToDestroyThemselvesAchievementAsset);
			XboxOneDVRManager.RecordPastDelayed(10f, 20f, XboxOneDVR.TRICKED_ENEMY_ID);
			this.m_causeEnemyToDestroyThemselvesAchieved = true;
		}
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x00058E3A File Offset: 0x0005703A
	public void OnEnemyKilledAnotherEnemy()
	{
		this.m_enemiesKilledByOtherEnemies++;
		if (this.m_enemiesKilledByOtherEnemies == 5)
		{
			AchievementsController.AwardAchievement(this.TrickEnemiesToKillEachOtherAchievementAsset);
		}
	}

	// Token: 0x0600131F RID: 4895 RVA: 0x00058E61 File Offset: 0x00057061
	public void OnCollectedEnergyShard()
	{
		this.m_collectedEnergySlotsCount++;
		if (this.m_collectedEnergySlotsCount == 200)
		{
			AchievementsController.AwardAchievement(this.Collect200EnergyCrystalsAchievementAsset);
		}
	}

	// Token: 0x040010BF RID: 4287
	public const int NUMBER_OF_SAVE_PEDESTALS = 12;

	// Token: 0x040010C0 RID: 4288
	public static AchievementsLogic Instance;

	// Token: 0x040010C1 RID: 4289
	public AchievementAsset DoubleJumpFiveTimesNoGroundAchievementAsset;

	// Token: 0x040010C2 RID: 4290
	public AchievementAsset BashTenEnemiesAchievementAsset;

	// Token: 0x040010C3 RID: 4291
	public AchievementAsset Kill3EnemiesWithoutTouchingTheGroundAchievementAsset;

	// Token: 0x040010C4 RID: 4292
	public AchievementAsset FinishGameUnder6HoursAchievementAsset;

	// Token: 0x040010C5 RID: 4293
	public AchievementAsset JuggleRockAchievementAsset;

	// Token: 0x040010C6 RID: 4294
	public AchievementAsset NoAbilityPointsAchievementAsset;

	// Token: 0x040010C7 RID: 4295
	public AchievementAsset TrickEnemiesToKillEachOtherAchievementAsset;

	// Token: 0x040010C8 RID: 4296
	public AchievementAsset CauseEnemyToDestroyThemselvesAchievementAsset;

	// Token: 0x040010C9 RID: 4297
	public AchievementAsset CrushARamEnemyWithAStomperAchievementAsset;

	// Token: 0x040010CA RID: 4298
	public AchievementAsset NoDeathsAchievementAsset;

	// Token: 0x040010CB RID: 4299
	public AchievementAsset Kill50WithStompAchievementAsset;

	// Token: 0x040010CC RID: 4300
	public AchievementAsset Kill100WithChargeFlameAchievementAsset;

	// Token: 0x040010CD RID: 4301
	public AchievementAsset Kill500WithSpiritFlameAchievementAsset;

	// Token: 0x040010CE RID: 4302
	private bool m_doubleJumpFiveTimesNoGroundAchieved;

	// Token: 0x040010CF RID: 4303
	private bool m_bashTenEnemiesAchieved;

	// Token: 0x040010D0 RID: 4304
	private bool m_kill3EnemiesWithoutTouchingTheGroundAchieved;

	// Token: 0x040010D1 RID: 4305
	private bool m_crushARamEnemyWithAStomperAchieved;

	// Token: 0x040010D2 RID: 4306
	private bool m_causeEnemyToDestroyThemselvesAchieved;

	// Token: 0x040010D3 RID: 4307
	public AchievementAsset AllSkillsBranch1AchievementAsset;

	// Token: 0x040010D4 RID: 4308
	public AchievementAsset AllSkillsBranch2AchievementAsset;

	// Token: 0x040010D5 RID: 4309
	public AchievementAsset AllSkillsBranch3AchievementAsset;

	// Token: 0x040010D6 RID: 4310
	public AchievementAsset AllSkillsAchievementAsset;

	// Token: 0x040010D7 RID: 4311
	public AchievementAsset CompleteMapAchievementAsset;

	// Token: 0x040010D8 RID: 4312
	public AchievementAsset FullyChargedAchievementAsset;

	// Token: 0x040010D9 RID: 4313
	public AchievementAsset Collect200EnergyCrystalsAchievementAsset;

	// Token: 0x040010DA RID: 4314
	public AchievementAsset SuperJump5EnemiesAchievementAsset;

	// Token: 0x040010DB RID: 4315
	public AchievementAsset AnEyeForAnEyeAchievementAsset;

	// Token: 0x040010DC RID: 4316
	public AchievementAsset SoulLinkManyTimesAchievementAsset;

	// Token: 0x040010DD RID: 4317
	public AchievementAsset KillManyWithBashRedirectAchievementAsset;

	// Token: 0x040010DE RID: 4318
	public AchievementAsset UseFirstAbilityPointAchievementAsset;

	// Token: 0x040010DF RID: 4319
	private bool m_superJump5EnemiesAchieved;

	// Token: 0x040010E0 RID: 4320
	private bool m_kill50EnemiesWithStompAchieved;

	// Token: 0x040010E1 RID: 4321
	private bool m_kill100EnemiesWithChargeFlameAchieved;

	// Token: 0x040010E2 RID: 4322
	private bool m_kill500EnemiesWithSpiritFlameAchieved;

	// Token: 0x040010E3 RID: 4323
	private bool m_anEyeForAnEyeAchieved;

	// Token: 0x040010E4 RID: 4324
	public AchievementAsset ActivateEverySpiritPortalAchievementAsset;

	// Token: 0x040010E5 RID: 4325
	public AchievementAsset CompletePrologueAchievementAsset;

	// Token: 0x040010E6 RID: 4326
	public AchievementAsset ReachSpiritTreeAchievementAsset;

	// Token: 0x040010E7 RID: 4327
	public AchievementAsset FindLostCorridorInMistyWoodsAchievementAsset;

	// Token: 0x040010E8 RID: 4328
	public AchievementAsset FindOneSecretAchievementAsset;

	// Token: 0x040010E9 RID: 4329
	public AchievementAsset FindHalfSecretsAchievementAsset;

	// Token: 0x040010EA RID: 4330
	public AchievementAsset FindAllSecretsAchievementAsset;

	// Token: 0x040010EB RID: 4331
	public AchievementAsset ReplaceOneStoneTabletAchievementAsset;

	// Token: 0x040010EC RID: 4332
	public AchievementAsset ReplaceHalfStoneTabletsAchievementAsset;

	// Token: 0x040010ED RID: 4333
	public AchievementAsset ReplacedAllStoneTabletsAchievementAsset;

	// Token: 0x040010EE RID: 4334
	public AchievementAsset ChargeDashFiveTimesAchievementAsset;

	// Token: 0x040010EF RID: 4335
	public AchievementAsset IgnitedThreeTorchesAchievementAsset;

	// Token: 0x040010F0 RID: 4336
	public AchievementAsset Kill50WithGrenadeAchievementAsset;

	// Token: 0x040010F1 RID: 4337
	public AchievementAsset BeatHardAchievementAsset;

	// Token: 0x040010F2 RID: 4338
	public AchievementAsset BeatOneLifeAchievementAsset;

	// Token: 0x040010F3 RID: 4339
	private int m_chargeDashKillCount;

	// Token: 0x040010F4 RID: 4340
	private int m_torchIgniteCount;

	// Token: 0x040010F5 RID: 4341
	private int m_enemiesKilledByGrenadeCount;

	// Token: 0x040010F6 RID: 4342
	private bool m_chargeDashKillFiveTimesAchieved;

	// Token: 0x040010F7 RID: 4343
	private bool m_ignitedThreeTorchesAchieved;

	// Token: 0x040010F8 RID: 4344
	private bool m_kill50WithGrenadeAchieved;

	// Token: 0x040010F9 RID: 4345
	private float m_completeMapSampleTime = 5f;

	// Token: 0x040010FA RID: 4346
	private bool m_completeMapAchievementAwarded;

	// Token: 0x040010FB RID: 4347
	private float m_chargeFlameWallsDestroyed;

	// Token: 0x040010FC RID: 4348
	private int m_enemiesKilledBySuperJumping;

	// Token: 0x040010FD RID: 4349
	private int m_savePedestalsUsed;

	// Token: 0x040010FE RID: 4350
	private int m_collectedEnergySlotsCount;

	// Token: 0x040010FF RID: 4351
	private float m_timePlayedAct1;

	// Token: 0x04001100 RID: 4352
	private float m_timePlayedAct2;

	// Token: 0x04001101 RID: 4353
	private float m_timePlayedAct3;

	// Token: 0x04001102 RID: 4354
	private int m_doubleJumpCount;

	// Token: 0x04001103 RID: 4355
	private int m_subsequentEnemiesBashCount;

	// Token: 0x04001104 RID: 4356
	private int m_enemiesKilledWithNoTouchingGoundCount;

	// Token: 0x04001105 RID: 4357
	private int m_enemiesKilledByOtherEnemies;

	// Token: 0x04001106 RID: 4358
	private int m_enemiesKilledByProjectile;

	// Token: 0x04001107 RID: 4359
	private float m_firstDoubleJumpTime;

	// Token: 0x04001108 RID: 4360
	private float m_firstKilledEnemyInAirTime;

	// Token: 0x04001109 RID: 4361
	private float m_firstBashTime;

	// Token: 0x0400110A RID: 4362
	private int m_enemiesKilledBySpiritFlameCount;

	// Token: 0x0400110B RID: 4363
	private int m_enemiesKilledByChargeFlameCount;

	// Token: 0x0400110C RID: 4364
	private int m_enemiesKilledByStompCount;

	// Token: 0x0400110D RID: 4365
	private int m_secretsRevealedCount;

	// Token: 0x0400110E RID: 4366
	private int m_mapStonesFoundCount;

	// Token: 0x0400110F RID: 4367
	public static bool Act3Ended;

	// Token: 0x04001110 RID: 4368
	public bool m_shouldGrantAchievements;
}
