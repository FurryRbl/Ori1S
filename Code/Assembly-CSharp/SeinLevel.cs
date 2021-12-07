using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x020001BA RID: 442
public class SeinLevel : SaveSerialize, ISeinReceiver
{
	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x06001075 RID: 4213 RVA: 0x0004B528 File Offset: 0x00049728
	public int TotalExperience
	{
		get
		{
			return this.Experience + this.ConsumedExperience;
		}
	}

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x06001076 RID: 4214 RVA: 0x0004B537 File Offset: 0x00049737
	public int TotalExperienceForNextLevel
	{
		get
		{
			return this.ExperienceForNextLevel + this.ConsumedExperience;
		}
	}

	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06001077 RID: 4215 RVA: 0x0004B546 File Offset: 0x00049746
	public int ExperienceNeedForNextLevel
	{
		get
		{
			return this.ExperienceForNextLevel - this.Experience;
		}
	}

	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06001078 RID: 4216 RVA: 0x0004B555 File Offset: 0x00049755
	public float ExperienceVisualMinNormalized
	{
		get
		{
			return this.ExperienceVisualMin / (float)this.ExperienceForNextLevel;
		}
	}

	// Token: 0x170002ED RID: 749
	// (get) Token: 0x06001079 RID: 4217 RVA: 0x0004B565 File Offset: 0x00049765
	public float ExperienceVisualMaxNormalized
	{
		get
		{
			return this.ExperienceVisualMax / (float)this.ExperienceForNextLevel;
		}
	}

	// Token: 0x170002EE RID: 750
	// (get) Token: 0x0600107A RID: 4218 RVA: 0x0004B575 File Offset: 0x00049775
	public int ExperienceForNextLevel
	{
		get
		{
			return Mathf.RoundToInt(this.ExperienceRequiredPerLevel.Evaluate((float)this.Current));
		}
	}

	// Token: 0x170002EF RID: 751
	// (get) Token: 0x0600107B RID: 4219 RVA: 0x0004B590 File Offset: 0x00049790
	public int ConsumedExperience
	{
		get
		{
			int num = 0;
			for (int i = this.Current - 1; i >= 0; i--)
			{
				num += Mathf.RoundToInt(this.ExperienceRequiredPerLevel.Evaluate((float)i));
			}
			return num;
		}
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x0004B5CE File Offset: 0x000497CE
	public void GainExperience(int amount)
	{
		this.Experience += amount;
		this.ExperienceVisualMax = (float)this.Experience;
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x0004B5EB File Offset: 0x000497EB
	public void Update()
	{
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x0004B5F0 File Offset: 0x000497F0
	public void FixedUpdate()
	{
		if (this.m_sein.IsSuspended)
		{
			return;
		}
		float maxDelta = Time.deltaTime * this.ExperienceGainPerSecond * (float)this.ExperienceForNextLevel;
		this.ExperienceVisualMax = Mathf.MoveTowards(this.ExperienceVisualMax, (float)this.Experience, maxDelta);
		this.ExperienceVisualMin = Mathf.MoveTowards(this.ExperienceVisualMin, (float)this.Experience, maxDelta);
		if (this.ExperienceVisualMin >= (float)this.ExperienceForNextLevel)
		{
			this.LevelUp();
		}
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x0004B670 File Offset: 0x00049870
	public void LevelUp()
	{
		this.Experience -= this.ExperienceForNextLevel;
		this.ExperienceVisualMin = 0f;
		this.ExperienceVisualMax = (float)this.Experience;
		if (this.Current < 99)
		{
			this.Current++;
			this.SkillPoints++;
		}
		if (this.OnLevelUpGameObject)
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.OnLevelUpGameObject, Characters.Sein.Position, Quaternion.identity);
			TargetPositionFollower component = gameObject.GetComponent<TargetPositionFollower>();
			component.Target = Characters.Sein.Transform;
		}
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x0004B718 File Offset: 0x00049918
	public void LoseExperience(int amount)
	{
		this.Experience -= amount;
		this.ExperienceVisualMin = (float)this.Experience;
		if (this.Experience < 0)
		{
			this.Experience = 0;
		}
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x0004B754 File Offset: 0x00049954
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Current);
		ar.Serialize(ref this.Experience);
		ar.Serialize(ref this.SkillPoints);
		ar.Serialize(ref SeinLevel.HasSpentSkillPoint);
		if (ar.Reading)
		{
			this.ExperienceVisualMax = (this.ExperienceVisualMin = (float)this.Current);
		}
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0004B7B1 File Offset: 0x000499B1
	public float ApplyLevelingToDamage(float damage)
	{
		return damage + damage * (float)this.m_sein.PlayerAbilities.OriStrength * 0.5f;
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x0004B7CE File Offset: 0x000499CE
	public float CalculateLevelBasedMaxHealth(int level, float health)
	{
		return (float)Mathf.RoundToInt(health * this.DamageMultiplierPerOriStrength.Evaluate((float)level));
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x0004B7E5 File Offset: 0x000499E5
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x0004B7EE File Offset: 0x000499EE
	public void GainSkillPoint()
	{
		this.SkillPoints++;
	}

	// Token: 0x04000DF0 RID: 3568
	public int SkillPoints;

	// Token: 0x04000DF1 RID: 3569
	public int Current;

	// Token: 0x04000DF2 RID: 3570
	public AnimationCurve DamageMultiplierPerOriStrength;

	// Token: 0x04000DF3 RID: 3571
	public int Experience;

	// Token: 0x04000DF4 RID: 3572
	public float ExperienceVisualMin;

	// Token: 0x04000DF5 RID: 3573
	public float ExperienceVisualMax;

	// Token: 0x04000DF6 RID: 3574
	public AnimationCurve ExperienceRequiredPerLevel;

	// Token: 0x04000DF7 RID: 3575
	public GameObject OnLevelUpGameObject;

	// Token: 0x04000DF8 RID: 3576
	public static bool HasSpentSkillPoint = false;

	// Token: 0x04000DF9 RID: 3577
	public float ExperienceGainPerSecond = 30f;

	// Token: 0x04000DFA RID: 3578
	private static readonly HashSet<string> CollectablesToSerialize = new HashSet<string>
	{
		"largeExpOrbPlaceholder",
		"mediumExpOrbPlaceholder",
		"smallExpOrbPlaceholder"
	};

	// Token: 0x04000DFB RID: 3579
	private static HashSet<Type> TypesToSerialize = new HashSet<Type>
	{
		typeof(ExpOrbPickup)
	};

	// Token: 0x04000DFC RID: 3580
	private SeinCharacter m_sein;
}
