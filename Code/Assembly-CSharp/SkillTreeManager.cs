using System;
using System.Collections.Generic;
using System.Text;
using Game;
using UnityEngine;

// Token: 0x020000A8 RID: 168
public class SkillTreeManager : MenuScreen
{
	// Token: 0x170001AC RID: 428
	// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001E243 File Offset: 0x0001C443
	public bool AllLanesFull
	{
		get
		{
			return this.EnergyLane.HasAllSkills && this.UtilityLane.HasAllSkills && this.CombatLane.HasAllSkills;
		}
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x0001E274 File Offset: 0x0001C474
	public void Awake()
	{
		SkillTreeManager.Instance = this;
		CleverMenuItemSelectionManager navigationManager = this.NavigationManager;
		navigationManager.OptionChangeCallback = (Action)Delegate.Combine(navigationManager.OptionChangeCallback, new Action(this.OnMenuItemChange));
		CleverMenuItemSelectionManager navigationManager2 = this.NavigationManager;
		navigationManager2.OptionPressedCallback = (Action)Delegate.Combine(navigationManager2.OptionPressedCallback, new Action(this.OnMenuItemPressed));
		CleverMenuItemSelectionManager navigationManager3 = this.NavigationManager;
		navigationManager3.OnBackPressedCallback = (Action)Delegate.Combine(navigationManager3.OnBackPressedCallback, new Action(this.OnBackPressed));
		this.OnMenuItemChange();
		foreach (CleverMenuItemSelectionManager.NavigationData navigationData in this.NavigationManager.Navigation)
		{
			navigationData.Condition = new Func<CleverMenuItemSelectionManager.NavigationData, bool>(SkillTreeManager.Condition);
		}
		this.UpdateRequirementsText();
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x0001E368 File Offset: 0x0001C568
	public void OnBackPressed()
	{
		UI.Menu.HideMenuScreen(false);
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x0001E375 File Offset: 0x0001C575
	public override void Hide()
	{
		this.NavigationManager.SetVisible(false);
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x0001E383 File Offset: 0x0001C583
	public override void ShowImmediate()
	{
		this.NavigationManager.SetVisibleImmediate(true);
		this.OnMenuItemChange();
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x0001E397 File Offset: 0x0001C597
	public override void HideImmediate()
	{
		this.NavigationManager.SetVisibleImmediate(false);
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x0001E3A5 File Offset: 0x0001C5A5
	public override void Show()
	{
		this.NavigationManager.SetVisible(true);
		this.OnMenuItemChange();
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x0001E3BC File Offset: 0x0001C5BC
	public static bool Condition(CleverMenuItemSelectionManager.NavigationData navigationData)
	{
		SkillItem component = navigationData.To.GetComponent<SkillItem>();
		return !component || component.Visible;
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x0001E3E8 File Offset: 0x0001C5E8
	public void OnDestroy()
	{
		CleverMenuItemSelectionManager navigationManager = this.NavigationManager;
		navigationManager.OptionChangeCallback = (Action)Delegate.Remove(navigationManager.OptionChangeCallback, new Action(this.OnMenuItemChange));
		CleverMenuItemSelectionManager navigationManager2 = this.NavigationManager;
		navigationManager2.OptionPressedCallback = (Action)Delegate.Remove(navigationManager2.OptionPressedCallback, new Action(this.OnMenuItemPressed));
		CleverMenuItemSelectionManager navigationManager3 = this.NavigationManager;
		navigationManager3.OnBackPressedCallback = (Action)Delegate.Remove(navigationManager3.OnBackPressedCallback, new Action(this.OnBackPressed));
		SkillTreeManager.Instance = null;
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x0001E470 File Offset: 0x0001C670
	public void OnMenuItemPressed()
	{
		if (this.CurrentSkillItem == null)
		{
			return;
		}
		if (this.CurrentSkillItem.HasSkillItem)
		{
			if (this.OnAlreadyEarnedAbility)
			{
				this.RequirementsLineAShake.Restart();
				this.OnAlreadyEarnedAbility.Perform(null);
			}
			return;
		}
		if (this.CurrentSkillItem.CanEarnSkill)
		{
			this.CurrentSkillItem.HasSkillItem = true;
			Characters.Sein.PlayerAbilities.SetAbility(this.CurrentSkillItem.Ability, true);
			Characters.Sein.PlayerAbilities.GainAbilityAction = this.CurrentSkillItem.GainSkillSequence;
			InstantiateUtility.Instantiate(this.GainSkillEffect, this.CurrentSkillItem.transform.position, Quaternion.identity);
			Characters.Sein.Level.SkillPoints -= this.CurrentSkillItem.ActualRequiredSkillPoints;
			if (this.OnGainAbility)
			{
				this.OnGainAbility.Perform(null);
			}
			SeinLevel.HasSpentSkillPoint = true;
			AchievementsController.AwardAchievement(this.SpentFirstSkillPointAchievement);
			GameController.Instance.CreateCheckpoint();
			GameController.Instance.SaveGameController.PerformSave();
			this.UpdateRequirementsText();
		}
		else
		{
			if (!this.CurrentSkillItem.SoulRequirementMet)
			{
				if (this.CurrentSkillItem.RequiresAbilitiesOrItems)
				{
					this.RequirementsLineAShake.Restart();
				}
				else
				{
					this.RequirementsLineAShake.Restart();
				}
			}
			if (!this.CurrentSkillItem.AbilitiesRequirementMet)
			{
				this.RequirementsLineAShake.Restart();
			}
			if (this.OnCantEarnSkill)
			{
				this.OnCantEarnSkill.Perform(null);
			}
		}
	}

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x06000761 RID: 1889 RVA: 0x0001E61D File Offset: 0x0001C81D
	public MessageDescriptor AbilityMastered
	{
		get
		{
			return new MessageDescriptor("$" + this.AbilityMasteredMessageProvider + "$");
		}
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x0001E63C File Offset: 0x0001C83C
	public MessageProvider AbilityName(AbilityType ability)
	{
		foreach (SkillTreeManager.AbilityMessageProvider abilityMessageProvider in this.AbilityMessages)
		{
			if (abilityMessageProvider.AbilityType == ability)
			{
				return abilityMessageProvider.MessageProvider;
			}
		}
		return null;
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x0001E6AC File Offset: 0x0001C8AC
	public string RequiredAbilitiesText(SkillItem skillItem)
	{
		bool abilitiesRequirementMet = skillItem.AbilitiesRequirementMet;
		StringBuilder stringBuilder = new StringBuilder(30);
		stringBuilder.Append(" ");
		for (int i = 0; i < skillItem.RequiredAbilities.Count; i++)
		{
			AbilityType ability = skillItem.RequiredAbilities[i];
			if (abilitiesRequirementMet)
			{
				MessageProvider messageProvider = this.AbilityName(ability);
				if (messageProvider)
				{
					stringBuilder.Append("$" + messageProvider + "$");
				}
			}
			else
			{
				MessageProvider messageProvider2 = this.AbilityName(ability);
				if (messageProvider2)
				{
					stringBuilder.Append("#" + messageProvider2 + "#");
				}
			}
			if (i != skillItem.RequiredAbilities.Count - 1 || skillItem.RequiredItems.Count > 0)
			{
				stringBuilder.Append((!abilitiesRequirementMet) ? "@,@ " : "$,$ ");
			}
		}
		for (int j = 0; j < skillItem.RequiredItems.Count; j++)
		{
			SkillItem skillItem2 = skillItem.RequiredItems[j];
			if (abilitiesRequirementMet)
			{
				stringBuilder.Append("$" + skillItem2.NameMessageProvider + "$");
			}
			else
			{
				stringBuilder.Append("#" + skillItem2.NameMessageProvider + "#");
			}
			if (j != skillItem.RequiredItems.Count - 1)
			{
				stringBuilder.Append((!abilitiesRequirementMet) ? "@,@ " : "$,$ ");
			}
		}
		return (!abilitiesRequirementMet) ? ("@" + this.RequiresMessageProvider.ToString().Replace("[Requirements]", "@" + stringBuilder + "@") + "@") : ("$" + this.RequiresMessageProvider.ToString().Replace("[Requirements]", "$" + stringBuilder + "$") + "$");
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x0001E8C0 File Offset: 0x0001CAC0
	public void UpdateRequirementsText()
	{
		this.CurrentSkillItem = this.NavigationManager.CurrentMenuItem.GetComponent<SkillItem>();
		if (this.CurrentSkillItem)
		{
			this.AbilityTitle.SetMessageProvider(this.CurrentSkillItem.NameMessageProvider);
			this.AbilityDescription.SetMessageProvider(this.CurrentSkillItem.DescriptionMessageProvider);
			if (this.CurrentSkillItem.HasSkillItem)
			{
				this.RequirementsLineA.SetMessage(this.AbilityMastered);
			}
			else if (this.CurrentSkillItem.RequiresAbilitiesOrItems)
			{
				this.RequirementsLineA.SetMessage(new MessageDescriptor(this.RequiredAbilitiesText(this.CurrentSkillItem) + "\n" + this.RequiredSoulsText(this.CurrentSkillItem)));
			}
			else
			{
				this.RequirementsLineA.SetMessage(new MessageDescriptor(this.RequiredSoulsText(this.CurrentSkillItem)));
			}
		}
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
	public string NameText(SkillItem skillItem)
	{
		if (skillItem.HasSkillItem)
		{
			return "$" + skillItem.NameMessageProvider + "$";
		}
		if (skillItem.CanEarnSkill)
		{
			return "#" + skillItem.NameMessageProvider + "#";
		}
		return "@" + skillItem.NameMessageProvider + "@";
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x0001EA0C File Offset: 0x0001CC0C
	public string RequiredSoulsText(SkillItem skillItem)
	{
		if (skillItem.HasSkillItem)
		{
			return string.Empty;
		}
		MessageProvider messageProvider = (skillItem.ActualRequiredSkillPoints != 1) ? this.AbilityPointsMessageProvider : this.AbilityPointMessageProvider;
		if (skillItem.ActualRequiredSkillPoints <= Characters.Sein.Level.SkillPoints)
		{
			return "$" + messageProvider.ToString().Replace("[Amount]", skillItem.ActualRequiredSkillPoints.ToString()) + "$";
		}
		return "@" + messageProvider.ToString().Replace("[Amount]", skillItem.ActualRequiredSkillPoints.ToString()) + "@";
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
	public void OnMenuItemChange()
	{
		this.CurrentSkillItem = this.NavigationManager.CurrentMenuItem.GetComponent<SkillItem>();
		if (this.CurrentSkillItem == null)
		{
			this.Cursor.gameObject.SetActive(false);
			this.InfoPanel.SetActive(false);
			this.AbilityDiskInfoPanel.SetActive(true);
			this.AbilityDiskInfoPanelDescription.RefreshText();
		}
		else
		{
			this.Cursor.gameObject.SetActive(true);
			this.Cursor.position = this.CurrentSkillItem.transform.position;
			foreach (object obj in this.LargeIcon.transform)
			{
				Transform transform = (Transform)obj;
				transform.gameObject.SetActive(transform.name == this.CurrentSkillItem.LargeIcon.name);
			}
			this.InfoPanel.SetActive(true);
			this.AbilityDiskInfoPanel.SetActive(false);
			this.UpdateRequirementsText();
		}
	}

	// Token: 0x04000564 RID: 1380
	public static SkillTreeManager Instance;

	// Token: 0x04000565 RID: 1381
	public CleverMenuItemSelectionManager NavigationManager;

	// Token: 0x04000566 RID: 1382
	public SkillItem CurrentSkillItem;

	// Token: 0x04000567 RID: 1383
	public Transform Cursor;

	// Token: 0x04000568 RID: 1384
	public SoundProvider OpenSound;

	// Token: 0x04000569 RID: 1385
	public SoundProvider CloseSound;

	// Token: 0x0400056A RID: 1386
	public GameObject LargeIcon;

	// Token: 0x0400056B RID: 1387
	public Renderer LargeIconGlow;

	// Token: 0x0400056C RID: 1388
	public MessageBox RequirementsLineA;

	// Token: 0x0400056D RID: 1389
	public MessageBox AbilityTitle;

	// Token: 0x0400056E RID: 1390
	public MessageBox AbilityDescription;

	// Token: 0x0400056F RID: 1391
	public GameObject InfoPanel;

	// Token: 0x04000570 RID: 1392
	public MessageBox AbilityDiskInfoPanelDescription;

	// Token: 0x04000571 RID: 1393
	public GameObject AbilityDiskInfoPanel;

	// Token: 0x04000572 RID: 1394
	public SkillTreeLaneLogic EnergyLane;

	// Token: 0x04000573 RID: 1395
	public SkillTreeLaneLogic UtilityLane;

	// Token: 0x04000574 RID: 1396
	public SkillTreeLaneLogic CombatLane;

	// Token: 0x04000575 RID: 1397
	public GameObject GainSkillEffect;

	// Token: 0x04000576 RID: 1398
	public LegacyAnimator RequirementsLineAShake;

	// Token: 0x04000577 RID: 1399
	public ActionMethod OnGainAbility;

	// Token: 0x04000578 RID: 1400
	public ActionMethod OnAlreadyEarnedAbility;

	// Token: 0x04000579 RID: 1401
	public ActionMethod OnCantEarnSkill;

	// Token: 0x0400057A RID: 1402
	public MessageProvider AbilityPointMessageProvider;

	// Token: 0x0400057B RID: 1403
	public MessageProvider AbilityPointsMessageProvider;

	// Token: 0x0400057C RID: 1404
	public MessageProvider RequiresMessageProvider;

	// Token: 0x0400057D RID: 1405
	public MessageProvider AbilityMasteredMessageProvider;

	// Token: 0x0400057E RID: 1406
	public AchievementAsset SpentFirstSkillPointAchievement;

	// Token: 0x0400057F RID: 1407
	public List<SkillTreeManager.AbilityMessageProvider> AbilityMessages;

	// Token: 0x020001C0 RID: 448
	[Serializable]
	public class AbilityMessageProvider
	{
		// Token: 0x04000E0A RID: 3594
		public AbilityType AbilityType;

		// Token: 0x04000E0B RID: 3595
		public MessageProvider MessageProvider;
	}
}
