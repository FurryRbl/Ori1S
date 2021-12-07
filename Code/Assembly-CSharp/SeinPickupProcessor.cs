using System;
using Game;
using UnityEngine;

// Token: 0x02000369 RID: 873
public class SeinPickupProcessor : SaveSerialize, ISeinReceiver, IPickupCollector, ICheckpointZoneReciever
{
	// Token: 0x06001904 RID: 6404 RVA: 0x0006AAA4 File Offset: 0x00068CA4
	public void OnCollectSkillPointPickup(SkillPointPickup skillPointPickup)
	{
		this.Sein.Level.GainSkillPoint();
		this.Sein.Inventory.SkillPointsCollected++;
		skillPointPickup.Collected();
		UI.SeinUI.ShakeExperienceBar();
		if (this.SkillPointSequence)
		{
			this.SkillPointSequence.Perform(null);
		}
		if (GameWorld.Instance.CurrentArea != null)
		{
			GameWorld.Instance.CurrentArea.DirtyCompletionAmount();
		}
	}

	// Token: 0x06001905 RID: 6405 RVA: 0x0006AB24 File Offset: 0x00068D24
	public void OnCollectEnergyOrbPickup(EnergyOrbPickup energyOrbPickup)
	{
		float num = (float)energyOrbPickup.Amount;
		if (this.Sein.PlayerAbilities.EnergyEfficiency.HasAbility)
		{
			num *= 1.5f;
		}
		bool canAffordSoulFlame = this.Sein.SoulFlame.CanAffordSoulFlame;
		AchievementsLogic.Instance.OnCollectedEnergyShard();
		this.Sein.Energy.Gain(num);
		energyOrbPickup.Collected();
		if (!canAffordSoulFlame && this.Sein.SoulFlame.CanAffordSoulFlame)
		{
			UI.SeinUI.ShakeSoulFlame();
		}
		if (!this.Sein.PlayerAbilities.WallJump.HasAbility)
		{
			this.EnergyOrbInfo.RunActionIfFirstTime();
		}
		UI.SeinUI.ShakeEnergyOrbBar();
	}

	// Token: 0x06001906 RID: 6406 RVA: 0x0006ABE4 File Offset: 0x00068DE4
	public void OnCollectMaxEnergyContainerPickup(MaxEnergyContainerPickup energyContainerPickup)
	{
		int amount = energyContainerPickup.Amount;
		if (this.Sein.Energy.Max == 0f)
		{
			this.Sein.SoulFlame.FillSoulFlameBar();
		}
		this.Sein.Energy.Max += (float)amount;
		this.Sein.Energy.Current = this.Sein.Energy.Max;
		energyContainerPickup.Collected();
		if (this.EnergyContainerSequence)
		{
			this.EnergyContainerSequence.Perform(null);
		}
		UI.SeinUI.ShakeEnergyOrbBar();
		SeinPickupProcessor.OnCollectMaxEnergyContainer();
		this.m_collectedMaxEnergySlotsCount++;
		if (!this.m_energySlotsAchievementAwarded && this.m_collectedMaxEnergySlotsCount >= 15)
		{
			this.m_energySlotsAchievementAwarded = true;
			AchievementsController.AwardAchievement(this.AllEnergyCellsCollected);
		}
		if (GameWorld.Instance.CurrentArea != null)
		{
			GameWorld.Instance.CurrentArea.DirtyCompletionAmount();
		}
	}

	// Token: 0x06001907 RID: 6407 RVA: 0x0006ACE8 File Offset: 0x00068EE8
	public void OnCollectExpOrbPickup(ExpOrbPickup expOrbPickup)
	{
		int num = expOrbPickup.Amount * ((!this.Sein.PlayerAbilities.SoulEfficiency.HasAbility) ? 1 : 2);
		this.Sein.Level.GainExperience(num);
		expOrbPickup.Collected();
		if (this.m_expText && this.m_expText.gameObject.activeInHierarchy)
		{
			this.m_expText.Amount += num;
		}
		else
		{
			this.m_expText = Orbs.OrbDisplayText.Create(Characters.Sein.Transform, Vector3.up, num);
		}
		UI.SeinUI.ShakeExperienceBar();
		switch (expOrbPickup.MessageType)
		{
		case ExpOrbPickup.ExpOrbMessageType.None:
			if (!this.Sein.PlayerAbilities.WallJump.HasAbility)
			{
				this.ExpOrbInfo.RunActionIfFirstTime();
			}
			break;
		case ExpOrbPickup.ExpOrbMessageType.PickupSmall:
			if (!this.Sein.PlayerAbilities.Bash.HasAbility)
			{
				this.SmallExpOrbInfo.RunActionIfFirstTime();
			}
			break;
		case ExpOrbPickup.ExpOrbMessageType.PickupMedium:
			if (!this.Sein.PlayerAbilities.Bash.HasAbility)
			{
				this.MediumExpOrbInfo.RunActionIfFirstTime();
			}
			break;
		case ExpOrbPickup.ExpOrbMessageType.PickupLarge:
			if (!this.Sein.PlayerAbilities.Bash.HasAbility)
			{
				this.LargeExpOrbInfo.RunActionIfFirstTime();
			}
			break;
		}
		if (GameWorld.Instance.CurrentArea != null)
		{
			GameWorld.Instance.CurrentArea.DirtyCompletionAmount();
		}
	}

	// Token: 0x06001908 RID: 6408 RVA: 0x0006AE80 File Offset: 0x00069080
	public void OnCollectKeystonePickup(KeystonePickup keystonePickup)
	{
		this.Sein.Inventory.CollectKeystones(keystonePickup.Amount);
		UI.SeinUI.ShakeKeystones();
		keystonePickup.Collected();
		if (!this.Sein.PlayerAbilities.WallJump.HasAbility)
		{
			this.KeystoneInfo.RunActionIfFirstTime();
		}
		if (GameWorld.Instance.CurrentArea != null)
		{
			GameWorld.Instance.CurrentArea.DirtyCompletionAmount();
		}
	}

	// Token: 0x06001909 RID: 6409 RVA: 0x0006AEF8 File Offset: 0x000690F8
	public void OnCollectMaxHealthContainerPickup(MaxHealthContainerPickup maxHealthContainerPickup)
	{
		this.Sein.Mortality.Health.GainMaxHeartContainer();
		maxHealthContainerPickup.Collected();
		UI.SeinUI.ShakeHealthbar();
		if (this.HeartContainerSequence)
		{
			this.HeartContainerSequence.Perform(null);
		}
		this.m_collectedHealthSlotsCount++;
		if (!this.m_healthSlotsAchievementAwarded && this.m_collectedHealthSlotsCount >= 12)
		{
			this.m_healthSlotsAchievementAwarded = true;
			AchievementsController.AwardAchievement(this.AllHealthCellsCollected);
		}
		if (GameWorld.Instance.CurrentArea != null)
		{
			GameWorld.Instance.CurrentArea.DirtyCompletionAmount();
		}
	}

	// Token: 0x0600190A RID: 6410 RVA: 0x0006AF9C File Offset: 0x0006919C
	public void OnCollectRestoreHealthPickup(RestoreHealthPickup restoreHealthPickup)
	{
		int amount = restoreHealthPickup.Amount * ((!this.Sein.PlayerAbilities.HealthEfficiency.HasAbility) ? 1 : 2);
		this.Sein.Mortality.Health.GainHealth(amount);
		restoreHealthPickup.Collected();
		UI.SeinUI.ShakeHealthbar();
		if (!this.Sein.PlayerAbilities.WallJump.HasAbility)
		{
			this.HealthOrbInfo.RunActionIfFirstTime();
		}
	}

	// Token: 0x0600190B RID: 6411 RVA: 0x0006B020 File Offset: 0x00069220
	public void OnCollectMapStonePickup(MapStonePickup mapStonePickup)
	{
		this.Sein.Inventory.MapStones++;
		mapStonePickup.Collected();
		UI.SeinUI.ShakeMapstones();
		if (this.MapStoneSequence)
		{
			this.MapStoneSequence.Perform(null);
		}
		if (GameWorld.Instance.CurrentArea != null)
		{
			GameWorld.Instance.CurrentArea.DirtyCompletionAmount();
		}
	}

	// Token: 0x0600190C RID: 6412 RVA: 0x0006B08F File Offset: 0x0006928F
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x0600190D RID: 6413 RVA: 0x0006B098 File Offset: 0x00069298
	public void OnEnterCheckpoint(InvisibleCheckpoint checkpoint)
	{
		if (this.Sein.IsSuspended)
		{
			return;
		}
		Vector3 position = this.Sein.Position;
		if (checkpoint.RespawnPosition != Vector2.zero)
		{
			this.Sein.Position = checkpoint.RespawnPosition + checkpoint.transform.position;
		}
		GameController.Instance.CreateCheckpoint();
		this.Sein.Position = position;
		checkpoint.OnCheckpointCreated();
	}

	// Token: 0x0600190E RID: 6414 RVA: 0x0006B120 File Offset: 0x00069320
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.ExpOrbInfo.HasBeenCollectedBefore);
		ar.Serialize(ref this.KeystoneInfo.HasBeenCollectedBefore);
		ar.Serialize(ref this.EnergyOrbInfo.HasBeenCollectedBefore);
		ar.Serialize(ref this.HealthOrbInfo.HasBeenCollectedBefore);
		ar.Serialize(ref this.SmallExpOrbInfo.HasBeenCollectedBefore);
		ar.Serialize(ref this.MediumExpOrbInfo.HasBeenCollectedBefore);
		ar.Serialize(ref this.LargeExpOrbInfo.HasBeenCollectedBefore);
		ar.Serialize(ref this.m_collectedMaxEnergySlotsCount);
		ar.Serialize(ref this.m_energySlotsAchievementAwarded);
		ar.Serialize(ref this.m_collectedHealthSlotsCount);
		ar.Serialize(ref this.m_healthSlotsAchievementAwarded);
	}

	// Token: 0x04001564 RID: 5476
	public SeinCharacter Sein;

	// Token: 0x04001565 RID: 5477
	public SeinPickupProcessor.CollectableInformation ExpOrbInfo = new SeinPickupProcessor.CollectableInformation();

	// Token: 0x04001566 RID: 5478
	public SeinPickupProcessor.CollectableInformation KeystoneInfo = new SeinPickupProcessor.CollectableInformation();

	// Token: 0x04001567 RID: 5479
	public SeinPickupProcessor.CollectableInformation EnergyOrbInfo = new SeinPickupProcessor.CollectableInformation();

	// Token: 0x04001568 RID: 5480
	public SeinPickupProcessor.CollectableInformation HealthOrbInfo = new SeinPickupProcessor.CollectableInformation();

	// Token: 0x04001569 RID: 5481
	public SeinPickupProcessor.CollectableInformation SmallExpOrbInfo = new SeinPickupProcessor.CollectableInformation();

	// Token: 0x0400156A RID: 5482
	public SeinPickupProcessor.CollectableInformation MediumExpOrbInfo = new SeinPickupProcessor.CollectableInformation();

	// Token: 0x0400156B RID: 5483
	public SeinPickupProcessor.CollectableInformation LargeExpOrbInfo = new SeinPickupProcessor.CollectableInformation();

	// Token: 0x0400156C RID: 5484
	public ActionMethod HeartContainerSequence;

	// Token: 0x0400156D RID: 5485
	public ActionMethod SkillPointSequence;

	// Token: 0x0400156E RID: 5486
	public ActionMethod EnergyContainerSequence;

	// Token: 0x0400156F RID: 5487
	public ActionMethod MapStoneSequence;

	// Token: 0x04001570 RID: 5488
	private ExpText m_expText;

	// Token: 0x04001571 RID: 5489
	public AchievementAsset Collect200EnergyCrystalsAchievementAsset;

	// Token: 0x04001572 RID: 5490
	public AchievementAsset AllEnergyCellsCollected;

	// Token: 0x04001573 RID: 5491
	public AchievementAsset AllHealthCellsCollected;

	// Token: 0x04001574 RID: 5492
	private int m_collectedMaxEnergySlotsCount;

	// Token: 0x04001575 RID: 5493
	private bool m_energySlotsAchievementAwarded;

	// Token: 0x04001576 RID: 5494
	private int m_collectedHealthSlotsCount;

	// Token: 0x04001577 RID: 5495
	private bool m_healthSlotsAchievementAwarded;

	// Token: 0x04001578 RID: 5496
	public static Action OnCollectMaxEnergyContainer = delegate()
	{
	};

	// Token: 0x0200094A RID: 2378
	[Serializable]
	public class CollectableInformation
	{
		// Token: 0x06003469 RID: 13417 RVA: 0x000DC302 File Offset: 0x000DA502
		public void RunActionIfFirstTime()
		{
			if (this.HasBeenCollectedBefore)
			{
				return;
			}
			this.HasBeenCollectedBefore = true;
			if (this.FirstTimeCollectedSequence)
			{
				this.FirstTimeCollectedSequence.Perform(null);
			}
		}

		// Token: 0x04002F47 RID: 12103
		public bool HasBeenCollectedBefore;

		// Token: 0x04002F48 RID: 12104
		public ActionMethod FirstTimeCollectedSequence;
	}
}
