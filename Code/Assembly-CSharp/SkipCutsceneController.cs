using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200015E RID: 350
public class SkipCutsceneController : MonoBehaviour
{
	// Token: 0x06000E1B RID: 3611 RVA: 0x00041A09 File Offset: 0x0003FC09
	public void RegisterSkipCutscene(Action action)
	{
		this.m_skipCutsceneAction = action;
	}

	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x06000E1C RID: 3612 RVA: 0x00041A12 File Offset: 0x0003FC12
	public bool SkippingAvailable
	{
		get
		{
			return GameStateMachine.Instance.CurrentState != GameStateMachine.State.WatchCutscenes && (this.m_skipCutsceneAction != null || GameStateMachine.Instance.CurrentState == GameStateMachine.State.Prologue);
		}
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x00041A41 File Offset: 0x0003FC41
	public void Awake()
	{
		SkipCutsceneController.Instance = this;
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x00041A64 File Offset: 0x0003FC64
	public void OnDestroy()
	{
		if (SkipCutsceneController.Instance == this)
		{
			SkipCutsceneController.Instance = null;
		}
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x00041A97 File Offset: 0x0003FC97
	public void OnGameReset()
	{
		this.Clear();
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x00041A9F File Offset: 0x0003FC9F
	public void Clear()
	{
		this.m_skipCutsceneAction = null;
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x00041AA8 File Offset: 0x0003FCA8
	public void SkipPrologue()
	{
		UI.Fader.FadeIn(0f);
		CharacterFactory.Instance.DestroyCharacter();
		GameController.Instance.RequireInitialValues = true;
		GoToSceneController.Instance.GoToSceneAsync(Scenes.Manager.GetSceneInformation("sunkenGladesRunaway"), null, false);
		Scenes.Manager.AllowUnloadingOnAllScenes();
		InstantLoadScenesController.Instance.FreezeIfLoadingScenes();
		GameStateMachine.Instance.SetToGame();
		Core.SoundComposition.Manager.StopMusic();
		SoundPlayer.DestroyAll();
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x00041B24 File Offset: 0x0003FD24
	public void SkipCutscene()
	{
		UI.Menu.LockClosingMenu = false;
		if (GameStateMachine.Instance.CurrentState == GameStateMachine.State.Prologue)
		{
			this.SkipPrologue();
		}
		else if (this.m_skipCutsceneAction != null)
		{
			Scenes.Manager.AllowUnloadingOnAllScenes();
			UI.Fader.FadeOut(0.5f);
			this.m_skipCutsceneAction();
		}
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x00041B8C File Offset: 0x0003FD8C
	public void PrewarmSkip()
	{
		if (GameStateMachine.Instance.CurrentState == GameStateMachine.State.Prologue)
		{
			RuntimeSceneMetaData sceneInformation = Scenes.Manager.GetSceneInformation("sunkenGladesRunaway");
			if (sceneInformation != null)
			{
				Scenes.Manager.PreloadScene(sceneInformation);
			}
		}
	}

	// Token: 0x04000B69 RID: 2921
	public static SkipCutsceneController Instance;

	// Token: 0x04000B6A RID: 2922
	private Action m_skipCutsceneAction;
}
