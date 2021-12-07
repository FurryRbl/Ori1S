using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000146 RID: 326
public class GameMapUI : MenuScreen, ISuspendable
{
	// Token: 0x06000CD3 RID: 3283 RVA: 0x0003AD2C File Offset: 0x00038F2C
	public void ChangeState(GameMapUI.WorldMapStates state)
	{
		this.m_mapState = state;
		switch (state)
		{
		case GameMapUI.WorldMapStates.Normal:
			this.BottomLegend.SetActive(true);
			this.BottomTeleportersLegend.SetActive(false);
			this.AreaMapLegend.SetActive(true);
			break;
		case GameMapUI.WorldMapStates.ShowingObjective:
			this.BottomLegend.SetActive(false);
			this.BottomTeleportersLegend.SetActive(false);
			this.AreaMapLegend.SetActive(false);
			break;
		case GameMapUI.WorldMapStates.ShowingTeleporters:
			this.BottomLegend.SetActive(false);
			this.BottomTeleportersLegend.SetActive(true);
			this.AreaMapLegend.SetActive(false);
			this.Teleporters.ShowTeleporters();
			break;
		}
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0003ADE3 File Offset: 0x00038FE3
	// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x0003ADEB File Offset: 0x00038FEB
	public GameMapShowObjective ShowObjective { get; set; }

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0003ADF4 File Offset: 0x00038FF4
	// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x0003ADFC File Offset: 0x00038FFC
	public GameMapObjectiveIcons ObjectiveIcons { get; set; }

	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0003AE05 File Offset: 0x00039005
	// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x0003AE0D File Offset: 0x0003900D
	public GameMapTeleporters Teleporters { get; set; }

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0003AE16 File Offset: 0x00039016
	// (set) Token: 0x06000CDB RID: 3291 RVA: 0x0003AE1E File Offset: 0x0003901E
	public bool IsSuspended { get; set; }

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0003AE27 File Offset: 0x00039027
	// (set) Token: 0x06000CDD RID: 3293 RVA: 0x0003AE30 File Offset: 0x00039030
	public bool IsVisible
	{
		get
		{
			return this.m_isVisible;
		}
		set
		{
			if (value)
			{
				this.m_isVisible = true;
				base.gameObject.SetActive(true);
				this.FadeAnimator.Initialize();
				this.FadeAnimator.AnimatorDriver.ContinueForward();
				this.m_fadeStarted = true;
			}
			else
			{
				this.m_isVisible = false;
				this.FadeAnimator.Initialize();
				this.FadeAnimator.AnimatorDriver.ContinueBackwards();
			}
		}
	}

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0003AE9F File Offset: 0x0003909F
	public bool ShowingObjective
	{
		get
		{
			return this.m_mapState == GameMapUI.WorldMapStates.ShowingObjective;
		}
	}

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0003AEAA File Offset: 0x000390AA
	public bool RevealingMap
	{
		get
		{
			return this.m_mapState == GameMapUI.WorldMapStates.RevealingMap;
		}
	}

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0003AEB5 File Offset: 0x000390B5
	public bool ShowingTeleporters
	{
		get
		{
			return this.m_mapState == GameMapUI.WorldMapStates.ShowingTeleporters;
		}
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0003AEC0 File Offset: 0x000390C0
	public void OnEnable()
	{
		this.m_isVisible = true;
		this.FadeAnimator.Initialize();
		this.FadeAnimator.AnimatorDriver.ContinueForward();
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x0003AEF0 File Offset: 0x000390F0
	public void Awake()
	{
		GameMapUI.Instance = this;
		this.SetNormal();
		SuspensionManager.Register(this);
		this.ShowObjective = base.GetComponent<GameMapShowObjective>();
		this.ObjectiveIcons = base.GetComponent<GameMapObjectiveIcons>();
		this.Teleporters = base.GetComponent<GameMapTeleporters>();
		this.CurrentHighlightedArea = World.CurrentArea;
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x0003AF3E File Offset: 0x0003913E
	public void OnDestroy()
	{
		if (GameMapUI.Instance == this)
		{
			GameMapUI.Instance = null;
		}
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x0003AF5C File Offset: 0x0003915C
	public void UpdateAreaText()
	{
		if (this.ShowingTeleporters)
		{
			if (this.Teleporters.SelectedIndex != -1)
			{
				RuntimeGameWorldArea area = this.Teleporters.SelectedTeleporter.Area;
				if (this.AreaText)
				{
					this.AreaText.SetMessageProvider(this.Teleporters.SelectedTeleporter.Name);
				}
				if (this.AreaCompletion)
				{
					this.AreaCompletion.SetMessage(new MessageDescriptor(area.CompletionPercentage + "% "));
				}
				if (this.AreaCompletionIcon)
				{
					this.AreaCompletionIcon.SampleValue(area.CompletionAmount, true);
				}
			}
			else
			{
				if (this.AreaText)
				{
					this.AreaText.SetMessage(new MessageDescriptor(string.Empty));
				}
				if (this.AreaCompletion)
				{
					this.AreaCompletion.SetMessage(new MessageDescriptor(string.Empty));
				}
			}
		}
		else if (this.CurrentHighlightedArea != null)
		{
			if (this.AreaText)
			{
				this.AreaText.SetMessageProvider(this.CurrentHighlightedArea.Area.AreaName);
			}
			if (this.AreaCompletion)
			{
				this.AreaCompletion.SetMessage(new MessageDescriptor(this.CurrentHighlightedArea.CompletionPercentage + "% "));
			}
			if (this.AreaCompletionIcon)
			{
				this.AreaCompletionIcon.SampleValue(this.CurrentHighlightedArea.CompletionAmount, true);
			}
		}
		else
		{
			if (this.AreaText)
			{
				this.AreaText.SetMessage(new MessageDescriptor(string.Empty));
			}
			if (this.AreaCompletion)
			{
				this.AreaCompletion.SetMessage(new MessageDescriptor(string.Empty));
			}
		}
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x0003B154 File Offset: 0x00039354
	public override void Show()
	{
		this.Group.gameObject.SetActive(false);
		this.IsVisible = true;
		this.Init();
		this.CurrentHighlightedArea = World.CurrentArea;
		AreaMapUI.Instance.Show();
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0003B194 File Offset: 0x00039394
	public override void Hide()
	{
		this.IsVisible = false;
		this.ObjectiveIcons.HideIcons();
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x0003B1A8 File Offset: 0x000393A8
	public override void ShowImmediate()
	{
		this.Group.gameObject.SetActive(false);
		this.Show();
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x0003B1C1 File Offset: 0x000393C1
	public override void HideImmediate()
	{
		this.Group.gameObject.SetActive(false);
		this.Hide();
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x0003B1DA File Offset: 0x000393DA
	public void Init()
	{
		this.ObjectiveIcons.ShowIcons();
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x0003B1E8 File Offset: 0x000393E8
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.FadeAnimator.AnimatorDriver.CurrentTime > this.FadeAnimator.Duration * 0.5f)
		{
			UberPostProcess.Instance.DoRender = false;
			this.Group.gameObject.SetActive(true);
		}
		else
		{
			this.Group.gameObject.SetActive(false);
			UberPostProcess.Instance.DoRender = true;
		}
		if (!this.IsVisible)
		{
			if (this.FadeAnimator.AnimatorDriver.IsReversed && !this.FadeAnimator.AnimatorDriver.IsPlaying)
			{
				base.gameObject.SetActive(false);
			}
			return;
		}
		if (!this.Group.gameObject.activeSelf)
		{
			return;
		}
		if (this.m_fadeStarted && !this.FadeAnimator.AnimatorDriver.IsReversed && !this.FadeAnimator.AnimatorDriver.IsPlaying)
		{
			UberGCManager.CollectResourcesIfNeeded();
			this.m_fadeStarted = false;
		}
		GameMapTransitionManager.Instance.Advance();
		this.ObjectiveIcons.Advance();
		this.Teleporters.Advance();
		this.UpdateAreaText();
		if (Core.Input.Cancel.OnPressed)
		{
			Core.Input.Cancel.Used = true;
			Core.Input.SoulFlame.Used = true;
			UI.Menu.HideMenuScreen(false);
		}
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0003B353 File Offset: 0x00039553
	public void SetShowingObjective()
	{
		this.ChangeState(GameMapUI.WorldMapStates.ShowingObjective);
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x0003B35C File Offset: 0x0003955C
	public void SetRevealingMap()
	{
		this.ChangeState(GameMapUI.WorldMapStates.ShowingObjective);
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x0003B365 File Offset: 0x00039565
	public void SetNormal()
	{
		this.ChangeState(GameMapUI.WorldMapStates.Normal);
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x0003B36E File Offset: 0x0003956E
	public void SetShowingTeleporters()
	{
		this.ChangeState(GameMapUI.WorldMapStates.ShowingTeleporters);
	}

	// Token: 0x04000AA8 RID: 2728
	public static GameMapUI Instance;

	// Token: 0x04000AA9 RID: 2729
	public MessageBox AreaText;

	// Token: 0x04000AAA RID: 2730
	public MessageBox AreaCompletion;

	// Token: 0x04000AAB RID: 2731
	public TimelineSequence AreaCompletionIcon;

	// Token: 0x04000AAC RID: 2732
	public RuntimeGameWorldArea CurrentHighlightedArea;

	// Token: 0x04000AAD RID: 2733
	private GameMapUI.WorldMapStates m_mapState;

	// Token: 0x04000AAE RID: 2734
	private bool m_showingObjective;

	// Token: 0x04000AAF RID: 2735
	public TransparencyAnimator FadeAnimator;

	// Token: 0x04000AB0 RID: 2736
	private bool m_isVisible;

	// Token: 0x04000AB1 RID: 2737
	public GameObject BottomLegend;

	// Token: 0x04000AB2 RID: 2738
	public GameObject BottomTeleportersLegend;

	// Token: 0x04000AB3 RID: 2739
	public GameObject AreaMapLegend;

	// Token: 0x04000AB4 RID: 2740
	private bool m_fadeStarted;

	// Token: 0x04000AB5 RID: 2741
	public Transform Group;

	// Token: 0x02000882 RID: 2178
	public enum WorldMapStates
	{
		// Token: 0x04002C6B RID: 11371
		Normal,
		// Token: 0x04002C6C RID: 11372
		ShowingObjective,
		// Token: 0x04002C6D RID: 11373
		RevealingMap,
		// Token: 0x04002C6E RID: 11374
		ShowingTeleporters
	}
}
