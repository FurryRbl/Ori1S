using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class GameStateMachine : SaveSerialize
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600002F RID: 47 RVA: 0x00002E59 File Offset: 0x00001059
	// (set) Token: 0x06000030 RID: 48 RVA: 0x00002E61 File Offset: 0x00001061
	public GameStateMachine.State CurrentState
	{
		get
		{
			return this.m_currentState;
		}
		set
		{
			if (this.m_currentState != value)
			{
				this.m_currentState = value;
			}
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002E78 File Offset: 0x00001078
	public bool IsInExtendedTitleScreen()
	{
		return GameStateMachine.Instance.CurrentState == GameStateMachine.State.Logos || GameStateMachine.Instance.CurrentState == GameStateMachine.State.TitleScreen || GameStateMachine.Instance.CurrentState == GameStateMachine.State.StartScreen;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002EB4 File Offset: 0x000010B4
	public void SetToLogos()
	{
		this.CurrentState = GameStateMachine.State.Logos;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002EBD File Offset: 0x000010BD
	public void SetToStartScreen()
	{
		this.CurrentState = GameStateMachine.State.StartScreen;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002EC6 File Offset: 0x000010C6
	public void SetToTitleScreen()
	{
		this.CurrentState = GameStateMachine.State.TitleScreen;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002ECF File Offset: 0x000010CF
	public void SetToGame()
	{
		this.CurrentState = GameStateMachine.State.Game;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002ED8 File Offset: 0x000010D8
	public void SetToWatchCutscene()
	{
		UberGCManager.CollectResourcesIfNeeded();
		this.CurrentState = GameStateMachine.State.WatchCutscenes;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002EE6 File Offset: 0x000010E6
	public void SetToTrialEnd()
	{
		this.CurrentState = GameStateMachine.State.TrialEnd;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002EEF File Offset: 0x000010EF
	public void SetToPrologue()
	{
		this.CurrentState = GameStateMachine.State.Prologue;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002EF8 File Offset: 0x000010F8
	public override void Awake()
	{
		base.Awake();
		GameStateMachine.Instance = this;
		if (Application.loadedLevelName == "loadBootstrap")
		{
			this.SetToLogos();
		}
		else if (Application.loadedLevelName == "introLogos")
		{
			this.SetToLogos();
		}
		else if (Application.loadedLevelName == "titleScreenSwallowsNest")
		{
			this.SetToStartScreen();
		}
		else
		{
			this.SetToGame();
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002F74 File Offset: 0x00001174
	public override void Serialize(Archive ar)
	{
		this.CurrentState = (GameStateMachine.State)ar.Serialize((int)this.CurrentState);
	}

	// Token: 0x04000016 RID: 22
	public static GameStateMachine Instance;

	// Token: 0x04000017 RID: 23
	private GameStateMachine.State m_currentState;

	// Token: 0x02000006 RID: 6
	public enum State
	{
		// Token: 0x04000019 RID: 25
		Logos,
		// Token: 0x0400001A RID: 26
		StartScreen,
		// Token: 0x0400001B RID: 27
		TitleScreen,
		// Token: 0x0400001C RID: 28
		Game,
		// Token: 0x0400001D RID: 29
		WatchCutscenes,
		// Token: 0x0400001E RID: 30
		TrialEnd,
		// Token: 0x0400001F RID: 31
		Prologue
	}
}
