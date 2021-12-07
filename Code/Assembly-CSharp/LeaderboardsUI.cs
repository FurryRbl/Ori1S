using System;
using Core;
using UnityEngine;

// Token: 0x020008AE RID: 2222
public class LeaderboardsUI
{
	// Token: 0x0600318D RID: 12685 RVA: 0x000D324C File Offset: 0x000D144C
	public void NextFilter()
	{
		switch (this.CurrentFilter)
		{
		case Leaderboards.Filter.Overall:
			this.CurrentFilter = Leaderboards.Filter.Friends;
			break;
		case Leaderboards.Filter.Friends:
			this.CurrentFilter = Leaderboards.Filter.MyScore;
			break;
		case Leaderboards.Filter.MyScore:
			this.CurrentFilter = Leaderboards.Filter.Overall;
			break;
		}
	}

	// Token: 0x0600318E RID: 12686 RVA: 0x000D329C File Offset: 0x000D149C
	public void PreviousFilter()
	{
		switch (this.CurrentFilter)
		{
		case Leaderboards.Filter.Overall:
			this.CurrentFilter = Leaderboards.Filter.MyScore;
			break;
		case Leaderboards.Filter.Friends:
			this.CurrentFilter = Leaderboards.Filter.Overall;
			break;
		case Leaderboards.Filter.MyScore:
			this.CurrentFilter = Leaderboards.Filter.Friends;
			break;
		}
	}

	// Token: 0x0600318F RID: 12687 RVA: 0x000D32EB File Offset: 0x000D14EB
	public void ShowLeaderboards()
	{
		if (this.m_visible)
		{
			return;
		}
		this.m_visible = true;
		this.UpdateLeaderboardData();
		SuspensionManager.SuspendAll();
	}

	// Token: 0x06003190 RID: 12688 RVA: 0x000D330B File Offset: 0x000D150B
	public void HideLeaderboards()
	{
		if (!this.m_visible)
		{
			return;
		}
		this.m_visible = false;
		SuspensionManager.ResumeAll();
	}

	// Token: 0x06003191 RID: 12689 RVA: 0x000D3325 File Offset: 0x000D1525
	public void Destroy()
	{
	}

	// Token: 0x06003192 RID: 12690 RVA: 0x000D3327 File Offset: 0x000D1527
	public LeaderboardsUI.State GetCurrentState()
	{
		return this.m_currentState;
	}

	// Token: 0x06003193 RID: 12691 RVA: 0x000D3330 File Offset: 0x000D1530
	public void Update()
	{
		if (this.m_visible)
		{
			if (Core.Input.Cancel.OnPressed)
			{
				this.HideLeaderboards();
			}
			if (this.m_currentState == LeaderboardsUI.State.Idle)
			{
				if (Core.Input.RightShoulder.OnPressed)
				{
					this.NextFilter();
					this.UpdateLeaderboardData();
				}
				if (Core.Input.LeftShoulder.OnPressed)
				{
					this.PreviousFilter();
					this.UpdateLeaderboardData();
				}
			}
		}
	}

	// Token: 0x06003194 RID: 12692 RVA: 0x000D339F File Offset: 0x000D159F
	private void ChangeState(LeaderboardsUI.State state)
	{
		this.m_currentState = state;
	}

	// Token: 0x06003195 RID: 12693 RVA: 0x000D33A8 File Offset: 0x000D15A8
	public void UpdateLeaderboardData()
	{
		this.DataIndex = 0;
	}

	// Token: 0x06003196 RID: 12694 RVA: 0x000D33B4 File Offset: 0x000D15B4
	public void DrawContent()
	{
		GUILayout.Label("Press 'b' to Close.", new GUILayoutOption[0]);
		GUILayout.Label("Filter: " + this.CurrentFilter + " (lb + rb)", new GUILayoutOption[0]);
		GUILayout.Label("State: " + this.m_currentState, new GUILayoutOption[0]);
	}

	// Token: 0x04002CCF RID: 11471
	private LeaderboardsUI.State m_currentState;

	// Token: 0x04002CD0 RID: 11472
	public Leaderboards.Filter CurrentFilter;

	// Token: 0x04002CD1 RID: 11473
	public Leaderboards.Views CurrentView;

	// Token: 0x04002CD2 RID: 11474
	public int DataIndex;

	// Token: 0x04002CD3 RID: 11475
	private bool m_visible;

	// Token: 0x020008AF RID: 2223
	public enum State
	{
		// Token: 0x04002CD5 RID: 11477
		Uninitialized,
		// Token: 0x04002CD6 RID: 11478
		Requesting,
		// Token: 0x04002CD7 RID: 11479
		Idle
	}
}
