using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x020001B8 RID: 440
public class SkillItem : MonoBehaviour
{
	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06001067 RID: 4199 RVA: 0x0004B22F File Offset: 0x0004942F
	public int ActualRequiredSkillPoints
	{
		get
		{
			if (DifficultyController.Instance.Difficulty == DifficultyMode.Hard)
			{
				return this.RequiredHardSkillPoints;
			}
			return this.RequiredSkillPoints;
		}
	}

	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x06001068 RID: 4200 RVA: 0x0004B24E File Offset: 0x0004944E
	// (set) Token: 0x06001069 RID: 4201 RVA: 0x0004B256 File Offset: 0x00049456
	public Color LargeIconColor { get; set; }

	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x0600106A RID: 4202 RVA: 0x0004B25F File Offset: 0x0004945F
	public bool Visible
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x0600106B RID: 4203 RVA: 0x0004B264 File Offset: 0x00049464
	public bool RequiresAbilitiesOrItems
	{
		get
		{
			return this.RequiredAbilities.Count != 0 || this.RequiredItems.Count != 0;
		}
	}

	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x0600106C RID: 4204 RVA: 0x0004B295 File Offset: 0x00049495
	public bool SoulRequirementMet
	{
		get
		{
			return this.ActualRequiredSkillPoints <= Characters.Sein.Level.SkillPoints;
		}
	}

	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x0600106D RID: 4205 RVA: 0x0004B2B4 File Offset: 0x000494B4
	public bool AbilitiesRequirementMet
	{
		get
		{
			foreach (AbilityType ability in this.RequiredAbilities)
			{
				if (!Characters.Sein.PlayerAbilities.HasAbility(ability))
				{
					return false;
				}
			}
			foreach (SkillItem skillItem in this.RequiredItems)
			{
				if (!skillItem.HasSkillItem)
				{
					return false;
				}
			}
			return true;
		}
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x0004B380 File Offset: 0x00049580
	public void Awake()
	{
		this.m_animator = this.Icon.GetComponent<TransparencyAnimator>();
	}

	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x0600106F RID: 4207 RVA: 0x0004B393 File Offset: 0x00049593
	public bool CanEarnSkill
	{
		get
		{
			return this.SoulRequirementMet && this.AbilitiesRequirementMet;
		}
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x0004B3A9 File Offset: 0x000495A9
	public void FixedUpdate()
	{
		this.UpdateItem();
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x0004B3B4 File Offset: 0x000495B4
	public void UpdateItem()
	{
		this.LearntSkillGlow.SetActive(this.HasSkillItem && this.Visible);
		this.Icon.gameObject.SetActive(this.Visible);
		if (this.HasSkillItem == this.m_animator.AnimatorDriver.IsReversed)
		{
			this.m_animator.Initialize();
			if (this.HasSkillItem)
			{
				this.m_animator.AnimatorDriver.ContinueForward();
			}
			else
			{
				this.m_animator.AnimatorDriver.ContinueBackwards();
			}
		}
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x0004B44C File Offset: 0x0004964C
	public void OnEnable()
	{
		this.HasSkillItem = Characters.Sein.PlayerAbilities.HasAbility(this.Ability);
		this.UpdateItem();
		this.m_animator.Initialize();
		if (this.HasSkillItem)
		{
			this.m_animator.AnimatorDriver.GoToEnd();
		}
		else
		{
			this.m_animator.AnimatorDriver.GoToStart();
		}
	}

	// Token: 0x04000DB6 RID: 3510
	public int RequiredSkillPoints = 1;

	// Token: 0x04000DB7 RID: 3511
	public int RequiredHardSkillPoints = 1;

	// Token: 0x04000DB8 RID: 3512
	public List<AbilityType> RequiredAbilities = new List<AbilityType>();

	// Token: 0x04000DB9 RID: 3513
	public List<SkillItem> RequiredItems = new List<SkillItem>();

	// Token: 0x04000DBA RID: 3514
	public AbilityType Ability;

	// Token: 0x04000DBB RID: 3515
	public Texture LargeIcon;

	// Token: 0x04000DBC RID: 3516
	public MessageProvider NameMessageProvider;

	// Token: 0x04000DBD RID: 3517
	public MessageProvider DescriptionMessageProvider;

	// Token: 0x04000DBE RID: 3518
	public Renderer Icon;

	// Token: 0x04000DBF RID: 3519
	public ActionMethod GainSkillSequence;

	// Token: 0x04000DC0 RID: 3520
	private TransparencyAnimator m_animator;

	// Token: 0x04000DC1 RID: 3521
	public GameObject LearntSkillGlow;

	// Token: 0x04000DC2 RID: 3522
	public bool HasSkillItem;
}
