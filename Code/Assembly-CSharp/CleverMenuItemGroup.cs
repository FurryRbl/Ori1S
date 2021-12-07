using System;
using System.Collections.Generic;
using Core;

// Token: 0x02000108 RID: 264
public class CleverMenuItemGroup : CleverMenuItemGroupBase
{
	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0002C495 File Offset: 0x0002A695
	// (set) Token: 0x06000A48 RID: 2632 RVA: 0x0002C4A4 File Offset: 0x0002A6A4
	public override bool IsVisible
	{
		get
		{
			return this.SelectionManager.IsVisible;
		}
		set
		{
			if (this.SelectionManager.FadeAnimator && this.SelectionManager.FadeAnimator.FinalOpacity < 0.05f && !value)
			{
				this.SelectionManager.SetVisibleImmediate(false);
			}
			else
			{
				this.SelectionManager.SetVisible(value);
			}
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0002C503 File Offset: 0x0002A703
	public override bool CanBeEntered
	{
		get
		{
			return !this.CanBeEnteredCondition || this.CanBeEnteredCondition.Validate(null);
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0002C523 File Offset: 0x0002A723
	// (set) Token: 0x06000A4B RID: 2635 RVA: 0x0002C530 File Offset: 0x0002A730
	public override bool IsActive
	{
		get
		{
			return this.SelectionManager.IsActive;
		}
		set
		{
			this.SelectionManager.IsActive = value;
			this.UpdateHighlight();
			if (this.SuspendOnActivated)
			{
				if (value)
				{
					if (!this.m_isFrozen)
					{
						this.m_isFrozen = true;
						this.m_suspendablesIgnore.Clear();
						SuspensionManager.GetSuspendables(this.m_suspendablesIgnore, base.gameObject);
						SuspensionManager.SuspendExcluding(this.m_suspendablesIgnore);
					}
				}
				else if (this.m_isFrozen)
				{
					this.m_isFrozen = false;
					SuspensionManager.ResumeExcluding(this.m_suspendablesIgnore);
					this.m_suspendablesIgnore.Clear();
				}
			}
		}
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x0002C5C6 File Offset: 0x0002A7C6
	public void OnDisable()
	{
		if (this.SuspendOnActivated && this.m_isFrozen)
		{
			this.m_isFrozen = false;
			SuspensionManager.ResumeExcluding(this.m_suspendablesIgnore);
			this.m_suspendablesIgnore.Clear();
		}
	}

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0002C5FB File Offset: 0x0002A7FB
	// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0002C608 File Offset: 0x0002A808
	public override bool IsHighlightVisible
	{
		get
		{
			return this.SelectionManager.IsHighlightVisible;
		}
		set
		{
			this.SelectionManager.IsHighlightVisible = value;
		}
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x0002C616 File Offset: 0x0002A816
	public void OnSelectionManagerBackPressed()
	{
		this.OnBackPressed();
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x0002C624 File Offset: 0x0002A824
	public new void Awake()
	{
		base.Awake();
		CleverMenuItemSelectionManager selectionManager = this.SelectionManager;
		selectionManager.OptionChangeCallback = (Action)Delegate.Combine(selectionManager.OptionChangeCallback, new Action(this.OnMenuItemChange));
		CleverMenuItemSelectionManager selectionManager2 = this.SelectionManager;
		selectionManager2.OptionPressedCallback = (Action)Delegate.Combine(selectionManager2.OptionPressedCallback, new Action(this.OnMenuItemPressed));
		CleverMenuItemSelectionManager selectionManager3 = this.SelectionManager;
		selectionManager3.OnBackPressedCallback = (Action)Delegate.Combine(selectionManager3.OnBackPressedCallback, new Action(this.OnSelectionManagerBackPressed));
		foreach (CleverMenuItemGroup.CleverMenuItemGroupItem cleverMenuItemGroupItem in this.Options)
		{
			cleverMenuItemGroupItem.ItemGroup.IsActive = false;
			CleverMenuItemGroupBase itemGroup = cleverMenuItemGroupItem.ItemGroup;
			itemGroup.OnBackPressed = (Action)Delegate.Combine(itemGroup.OnBackPressed, new Action(this.OnOptionBackPressed));
		}
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x0002C728 File Offset: 0x0002A928
	public new void OnDestroy()
	{
		base.OnDestroy();
		CleverMenuItemSelectionManager selectionManager = this.SelectionManager;
		selectionManager.OptionChangeCallback = (Action)Delegate.Remove(selectionManager.OptionChangeCallback, new Action(this.OnMenuItemChange));
		CleverMenuItemSelectionManager selectionManager2 = this.SelectionManager;
		selectionManager2.OptionPressedCallback = (Action)Delegate.Remove(selectionManager2.OptionPressedCallback, new Action(this.OnMenuItemPressed));
		CleverMenuItemSelectionManager selectionManager3 = this.SelectionManager;
		selectionManager3.OnBackPressedCallback = (Action)Delegate.Remove(selectionManager3.OnBackPressedCallback, new Action(this.OnSelectionManagerBackPressed));
		foreach (CleverMenuItemGroup.CleverMenuItemGroupItem cleverMenuItemGroupItem in this.Options)
		{
			CleverMenuItemGroupBase itemGroup = cleverMenuItemGroupItem.ItemGroup;
			itemGroup.OnBackPressed = (Action)Delegate.Remove(itemGroup.OnBackPressed, new Action(this.OnOptionBackPressed));
		}
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x0002C820 File Offset: 0x0002AA20
	public void Start()
	{
		foreach (CleverMenuItemGroup.CleverMenuItemGroupItem cleverMenuItemGroupItem in this.Options)
		{
			bool isVisible = this.SelectionManager.CurrentMenuItem == cleverMenuItemGroupItem.MenuItem && this.ExpandOnHighlight;
			cleverMenuItemGroupItem.ItemGroup.IsVisible = isVisible;
		}
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x0002C8A4 File Offset: 0x0002AAA4
	public void OnOptionBackPressed()
	{
		foreach (CleverMenuItemGroup.CleverMenuItemGroupItem cleverMenuItemGroupItem in this.Options)
		{
			if (!this.ExpandOnHighlight)
			{
				cleverMenuItemGroupItem.ItemGroup.IsVisible = false;
			}
			cleverMenuItemGroupItem.ItemGroup.IsActive = false;
			cleverMenuItemGroupItem.ItemGroup.IsHighlightVisible = false;
		}
		if (!this.IsActive && this.OnCollapseSound)
		{
			Sound.Play(this.OnCollapseSound.GetSound(null), base.transform.position, null);
		}
		this.IsActive = true;
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x0002C968 File Offset: 0x0002AB68
	public void OnMenuItemChange()
	{
		foreach (CleverMenuItemGroup.CleverMenuItemGroupItem cleverMenuItemGroupItem in this.Options)
		{
			if (this.SelectionManager.CurrentMenuItem == cleverMenuItemGroupItem.MenuItem && this.ExpandOnHighlight)
			{
				cleverMenuItemGroupItem.ItemGroup.IsVisible = true;
			}
			else
			{
				cleverMenuItemGroupItem.ItemGroup.IsVisible = false;
			}
		}
		if (this.OnChangeSelectionSound && this.IsActive && this.m_playChangeSound)
		{
			Sound.Play(this.OnChangeSelectionSound.GetSound(null), base.transform.position, null);
		}
		this.IsActive = true;
		this.Root.OnMenuItemChangedInGroup(this);
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x0002CA58 File Offset: 0x0002AC58
	public override bool OnMenuItemChangedInGroup(CleverMenuItemGroup group)
	{
		bool flag = false;
		if (group == this)
		{
			flag = true;
		}
		else
		{
			this.IsActive = false;
		}
		foreach (CleverMenuItemGroup.CleverMenuItemGroupItem cleverMenuItemGroupItem in this.Options)
		{
			if (cleverMenuItemGroupItem.ItemGroup.OnMenuItemChangedInGroup(group))
			{
				flag = true;
			}
		}
		this.IsHighlightVisible = flag;
		return flag;
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x0002CAE4 File Offset: 0x0002ACE4
	public void OnMenuItemPressed()
	{
		foreach (CleverMenuItemGroup.CleverMenuItemGroupItem cleverMenuItemGroupItem in this.Options)
		{
			cleverMenuItemGroupItem.ItemGroup.IsVisible = (this.SelectionManager.CurrentMenuItem == cleverMenuItemGroupItem.MenuItem);
		}
		foreach (CleverMenuItemGroup.CleverMenuItemGroupItem cleverMenuItemGroupItem2 in this.Options)
		{
			if (this.SelectionManager.CurrentMenuItem == cleverMenuItemGroupItem2.MenuItem && cleverMenuItemGroupItem2.ItemGroup.CanBeEntered)
			{
				cleverMenuItemGroupItem2.ItemGroup.EnterInGroup();
				this.OnEnteredChildGroup();
				if (this.SelectionManager.CurrentMenuItem == cleverMenuItemGroupItem2.MenuItem && this.OnExpandSound)
				{
					Sound.Play(this.OnExpandSound.GetSound(null), base.transform.position, null);
				}
			}
		}
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x0002CC20 File Offset: 0x0002AE20
	public void UpdateHighlight()
	{
		if (this.HighlightAnimator == null)
		{
			return;
		}
		if (this.IsActive)
		{
			this.HighlightAnimator.Initialize();
			this.HighlightAnimator.AnimatorDriver.ContinueForward();
		}
		else
		{
			this.HighlightAnimator.Initialize();
			this.HighlightAnimator.AnimatorDriver.ContinueBackwards();
		}
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x0002CC85 File Offset: 0x0002AE85
	public void OnEnteredChildGroup()
	{
		this.IsActive = false;
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x0002CC90 File Offset: 0x0002AE90
	public override void EnterInGroup()
	{
		this.m_playChangeSound = false;
		this.SelectionManager.SetIndexToFirst();
		this.m_playChangeSound = true;
		this.IsActive = true;
		this.IsHighlightVisible = true;
		if (!this.ExpandOnHighlight)
		{
			foreach (CleverMenuItemGroup.CleverMenuItemGroupItem cleverMenuItemGroupItem in this.Options)
			{
				cleverMenuItemGroupItem.ItemGroup.IsVisible = false;
			}
		}
	}

	// Token: 0x04000867 RID: 2151
	public CleverMenuItemGroup Root;

	// Token: 0x04000868 RID: 2152
	public List<CleverMenuItemGroup.CleverMenuItemGroupItem> Options;

	// Token: 0x04000869 RID: 2153
	public CleverMenuItemSelectionManager SelectionManager;

	// Token: 0x0400086A RID: 2154
	public SoundProvider OnExpandSound;

	// Token: 0x0400086B RID: 2155
	public SoundProvider OnCollapseSound;

	// Token: 0x0400086C RID: 2156
	public SoundProvider OnChangeSelectionSound;

	// Token: 0x0400086D RID: 2157
	public bool ExpandOnHighlight;

	// Token: 0x0400086E RID: 2158
	public Condition CanBeEnteredCondition;

	// Token: 0x0400086F RID: 2159
	public TransparencyAnimator HighlightAnimator;

	// Token: 0x04000870 RID: 2160
	public bool SuspendOnActivated;

	// Token: 0x04000871 RID: 2161
	private bool m_playChangeSound = true;

	// Token: 0x04000872 RID: 2162
	private bool m_isFrozen;

	// Token: 0x04000873 RID: 2163
	private HashSet<ISuspendable> m_suspendablesIgnore = new HashSet<ISuspendable>();

	// Token: 0x02000109 RID: 265
	[Serializable]
	public class CleverMenuItemGroupItem
	{
		// Token: 0x04000874 RID: 2164
		public CleverMenuItem MenuItem;

		// Token: 0x04000875 RID: 2165
		public CleverMenuItemGroupBase ItemGroup;
	}
}
