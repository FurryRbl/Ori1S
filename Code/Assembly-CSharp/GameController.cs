using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Core;
using Frameworks;
using Game;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class GameController : SaveSerialize, ISuspendable
{
	// Token: 0x17000057 RID: 87
	// (get) Token: 0x0600010F RID: 271 RVA: 0x000058E3 File Offset: 0x00003AE3
	// (set) Token: 0x06000110 RID: 272 RVA: 0x000058EB File Offset: 0x00003AEB
	public bool MainMenuCanBeOpened { get; set; }

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06000111 RID: 273 RVA: 0x000058F4 File Offset: 0x00003AF4
	public int GameTimeInSeconds
	{
		get
		{
			return Mathf.RoundToInt(this.Timer.CurrentTime);
		}
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00005906 File Offset: 0x00003B06
	public void PerformSaveGameSequence()
	{
		if (this.GameSaveSequence)
		{
			this.GameSaveSequence.Perform(null);
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000113 RID: 275 RVA: 0x00005924 File Offset: 0x00003B24
	public bool IsPackageFullyInstalled
	{
		get
		{
			return !DebugMenuB.IsFullyInstalledDebugOverride;
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000114 RID: 276 RVA: 0x00005933 File Offset: 0x00003B33
	public bool IsTrial
	{
		get
		{
			return this.PCTrialValue;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06000115 RID: 277 RVA: 0x0000593C File Offset: 0x00003B3C
	public bool IsDemo
	{
		get
		{
			WorldEventsRuntime worldEventsRuntime = World.Events.Find(this.DebugWorldEvents);
			return worldEventsRuntime.Value == this.DebugWorldEvents.GetIDFromName("Demo");
		}
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00005970 File Offset: 0x00003B70
	public void ExitGame()
	{
		if (this.IsTrial)
		{
			GameController.Instance.GoToEndTrialScreen();
		}
		else
		{
			GameController.Instance.QuitApplication();
		}
	}

	// Token: 0x06000117 RID: 279 RVA: 0x000059A1 File Offset: 0x00003BA1
	public void ExitTrial()
	{
		GameController.Instance.RestartGame();
	}

	// Token: 0x06000118 RID: 280 RVA: 0x000059AD File Offset: 0x00003BAD
	public void QuitApplication()
	{
		Application.Quit();
	}

	// Token: 0x06000119 RID: 281 RVA: 0x000059B4 File Offset: 0x00003BB4
	public void GoToEndTrialScreen()
	{
		this.MainMenuCanBeOpened = false;
		GameStateMachine.Instance.SetToTrialEnd();
		RuntimeSceneMetaData sceneInformation = Scenes.Manager.GetSceneInformation("trialEndScreen");
		GoToSceneController.Instance.GoToScene(sceneInformation, new Action(this.OnFinishedLoadingTrialEndScene), false);
	}

	// Token: 0x0600011A RID: 282 RVA: 0x000059FA File Offset: 0x00003BFA
	public void OnFinishedLoadingTrialEndScene()
	{
		this.RemoveGameplayObjects();
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00005A04 File Offset: 0x00003C04
	public void OnGameReset()
	{
		SaveSlotsManager.BackupIndex = -1;
		TriggerByString.OnGameReset();
		SeinLevel.HasSpentSkillPoint = false;
		WorldEventsManager.Instance.OnGameReset();
		SoundPlayer.DestroyAll();
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00005A34 File Offset: 0x00003C34
	public void RemoveGameplayObjects()
	{
		CharacterFactory.Instance.DestroyCharacter();
		if (Characters.Sein)
		{
			InstantiateUtility.Destroy(Characters.Sein.gameObject);
		}
		if (Characters.Naru)
		{
			InstantiateUtility.Destroy(Characters.Naru.gameObject);
		}
		if (Characters.BabySein)
		{
			InstantiateUtility.Destroy(Characters.BabySein.gameObject);
		}
		if (Characters.Ori)
		{
			InstantiateUtility.Destroy(Characters.Ori.gameObject);
		}
		if (UI.SeinUI)
		{
			InstantiateUtility.Destroy(UI.SeinUI.gameObject);
		}
		Core.SoundComposition.Manager.StopMusic();
		UI.Cameras.Current.Target = null;
		if (UI.MainMenuVisible)
		{
			UI.Menu.HideMenuScreen(false);
		}
		UI.Menu.RemoveGameplayObjects();
		WorldMapUI.CancelLoading();
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00005B1A File Offset: 0x00003D1A
	public void ResetStateForDebugMenuGoToScene()
	{
		this.RemoveGameplayObjects();
		this.RequireInitialValues = true;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00005B2C File Offset: 0x00003D2C
	public void RestartGame()
	{
		if (this.m_isRestartingGame)
		{
			return;
		}
		RuntimeSceneMetaData sceneInformation = Scenes.Manager.GetSceneInformation("titleScreenSwallowsNest");
		if (sceneInformation == null)
		{
			return;
		}
		this.Timer.Reset();
		this.MainMenuCanBeOpened = false;
		this.RequireInitialValues = true;
		GameController.Instance.IsLoadingGame = false;
		InstantLoadScenesController.Instance.OnGameReset();
		GoToSceneController.Instance.GoToScene(sceneInformation, new Action(this.OnFinishedRestarting), false);
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00005BA2 File Offset: 0x00003DA2
	private void OnFinishedRestarting()
	{
		base.StartCoroutine(this.RestartingCleanupNextFrame());
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00005BB4 File Offset: 0x00003DB4
	public IEnumerator RestartingCleanupNextFrame()
	{
		this.RemoveGameplayObjects();
		this.ResetInputLocks();
		if (UI.Fader.IsFadingInOrStay() || UI.Fader.IsTimelineFading())
		{
			UI.Fader.FadeOut(2f);
		}
		XboxLiveController.Instance.Reset();
		XboxOneController.ResetCurrentGamepad();
		XboxOneFlow.Engage = false;
		XboxOneSession.EndSession();
		yield return new WaitForFixedUpdate();
		this.m_isRestartingGame = false;
		this.ActiveObjectives.Clear();
		Game.Checkpoint.SaveGameData = new SaveGameData();
		Events.Scheduler.OnGameSerializeLoad.Call();
		Events.Scheduler.OnGameReset.Call();
		if (UI.Fader.IsFadingInOrStay() || UI.Fader.IsTimelineFading())
		{
			UI.Fader.FadeOut(2f);
		}
		TitleScreenManager.OnReturnToTitleScreen();
		this.CreateCheckpoint();
		yield break;
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000121 RID: 289 RVA: 0x00005BCF File Offset: 0x00003DCF
	// (set) Token: 0x06000122 RID: 290 RVA: 0x00005BD7 File Offset: 0x00003DD7
	public bool GameplaySuspended { get; set; }

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000123 RID: 291 RVA: 0x00005BE0 File Offset: 0x00003DE0
	// (set) Token: 0x06000124 RID: 292 RVA: 0x00005BE8 File Offset: 0x00003DE8
	public bool GameplaySuspendedForUI { get; set; }

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000125 RID: 293 RVA: 0x00005BF1 File Offset: 0x00003DF1
	public bool InputLocked
	{
		get
		{
			return this.LockInput || this.LockInputByAction;
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000126 RID: 294 RVA: 0x00005C07 File Offset: 0x00003E07
	// (set) Token: 0x06000127 RID: 295 RVA: 0x00005C0F File Offset: 0x00003E0F
	public bool LockInputByAction { get; set; }

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x06000128 RID: 296 RVA: 0x00005C18 File Offset: 0x00003E18
	// (set) Token: 0x06000129 RID: 297 RVA: 0x00005C20 File Offset: 0x00003E20
	public bool LockInput { get; set; }

	// Token: 0x0600012A RID: 298 RVA: 0x00005C2C File Offset: 0x00003E2C
	[ContextMenu("Print out sizes of SaveSlot")]
	public void PrintOutSizesOfSaveSlot()
	{
		int num = 0;
		foreach (KeyValuePair<MoonGuid, SaveScene> keyValuePair in Game.Checkpoint.SaveGameData.Scenes)
		{
			foreach (SaveObject saveObject in keyValuePair.Value.SaveObjects)
			{
				num += saveObject.Data.MemoryStream.Capacity;
			}
			num += 16;
		}
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00005CE8 File Offset: 0x00003EE8
	public override void Awake()
	{
		if (GameController.Instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		GameController.Instance = this;
		this.HandleTrialData();
		this.WarmUpResources();
		base.Awake();
		if (LoadingBootstrap.Instance)
		{
			UnityEngine.Object.Destroy(LoadingBootstrap.Instance.gameObject);
		}
		this.GameScheduler.OnGameAwake.Add(new Action(this.OnGameAwake));
		this.GameScheduler.OnGameAwake.Call();
		this.GameScheduler.OnGameReset.Add(new Action(this.OnGameReset));
		UberGCManager.OnGameStart();
		this.m_systemsGameObject = new GameObject("systems");
		Utility.DontAssociateWithAnyScene(this.m_systemsGameObject);
		base.transform.parent = this.m_systemsGameObject.transform;
		foreach (GameObject gameObject in this.Systems)
		{
			try
			{
				if (gameObject)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.name = gameObject.name;
					gameObject2.transform.SetParentMaintainingLocalTransform(this.m_systemsGameObject.transform);
				}
			}
			catch (Exception ex)
			{
			}
		}
		new Telemetry();
		UI.LoadMessageController();
		this.Systems.Clear();
		Application.targetFrameRate = 60;
		UberGCManager.CollectProactiveFull();
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00005E80 File Offset: 0x00004080
	private void OnGameAwake()
	{
		this.m_restoreCheckpointController = new RestoreCheckpointController();
		Frameworks.Shader.Globals.FogGradientRange = 100f;
		Frameworks.Shader.Globals.FogGradientTexture = Frameworks.Shader.DefaultTextures.Transparent;
		FixedRandom.UpdateValues();
		if (ScenesToSkip.Instance == null)
		{
			new ScenesToSkip();
		}
		SaveSceneManager.Master = base.GetComponent<SaveSceneManager>();
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00005ECC File Offset: 0x000040CC
	public IEnumerator Start()
	{
		GameplayCamera currentCamera = UI.Cameras.Current;
		currentCamera.ChangeTargetToCurrentCharacter();
		Scenes.Manager.EnableDisabledScenesAtPosition(false);
		currentCamera.UpdateTargetHelperPosition();
		currentCamera.MoveCameraToTargetPosition();
		currentCamera.OffsetController.UpdateOffset(true);
		currentCamera.MoveCameraToTargetInstantly(true);
		yield return new WaitForFixedUpdate();
		GameSettings.Instance.LoadSettings();
		this.CreateCheckpoint();
		SaveSceneManager.Master.RegisterGameObject(this.m_systemsGameObject);
		SuspensionManager.Register(this);
		if (!this.IsTrial)
		{
			WaitForSaveGameLogic.OnCompletedStatic = (Action)Delegate.Combine(WaitForSaveGameLogic.OnCompletedStatic, new Action(AchievementsLogic.Instance.HandleTrialAchievements));
		}
		yield break;
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00005EE8 File Offset: 0x000040E8
	private void OnApplicationFocus(bool focusStatus)
	{
		if (focusStatus)
		{
			this.m_setRunInBackgroundToFalse = false;
			Application.runInBackground = true;
		}
		else
		{
			if (!GameStateMachine.Instance.IsInExtendedTitleScreen() && !UI.MainMenuVisible)
			{
				UI.Menu.ShowResumeScreen();
			}
			this.m_setRunInBackgroundToFalse = true;
			base.StartCoroutine(this.SetRunInBackgroundToTrue());
		}
		GameController.IsFocused = focusStatus;
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00005F4C File Offset: 0x0000414C
	private IEnumerator SetRunInBackgroundToTrue()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (this.m_setRunInBackgroundToFalse && !this.PreventFocusPause)
		{
			this.m_setRunInBackgroundToFalse = false;
			Application.runInBackground = false;
		}
		yield break;
	}

	// Token: 0x06000130 RID: 304 RVA: 0x00005F68 File Offset: 0x00004168
	private IEnumerator LoadAssets(List<string> assetsToLoad)
	{
		foreach (string assetToLoad in assetsToLoad)
		{
			WWW www = new WWW(assetToLoad);
			yield return www;
			UnityEngine.Object.Instantiate(www.assetBundle.mainAsset);
		}
		yield break;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00005F8A File Offset: 0x0000418A
	public override void OnDestroy()
	{
		InstantiateUtility.Destroy(this.m_systemsGameObject);
		SuspensionManager.Unregister(this);
		base.OnDestroy();
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00005FA3 File Offset: 0x000041A3
	public void ResetInputLocks()
	{
		this.LockInputByAction = false;
		this.LockInput = false;
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00005FB4 File Offset: 0x000041B4
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.ResetInputLocks();
		}
		WorldEventsManager.Instance.Serialize(ar);
		TriggerByString.SerializeStringTriggers(ar);
		ar.Serialize(0f);
		ar.Serialize(ref this.GameTime);
		ar.Serialize(0);
		ar.Serialize(0);
		ar.Serialize(ref this.RequireInitialValues);
		if (ar.Reading)
		{
			this.RequireInitialValues = false;
		}
		Game.Objectives.Serialize(ar);
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00006030 File Offset: 0x00004230
	public void WarmUpResources()
	{
		Timer timer = new Timer();
		UI.LoadMessageController();
		Orbs.OrbDisplayText.LoadOrbText();
		Attacking.DamageDisplayText.LoadDamageText();
		Sound.LoadAudioParent();
		UberGhostTrail.WarmUpResource();
		MixerManager.WarmUpResource();
		InteractionRotationModifier.WarmUpResource();
		timer.Report("Warming resources");
		this.Resources.Clear();
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000607C File Offset: 0x0000427C
	public void SetupGameplay(SceneRoot sceneRoot, WorldEventsOnAwake worldEventsOnAwake)
	{
		sceneRoot.MetaData.InitialValues.ApplyInitialValues();
		this.WarmUpResources();
		if (worldEventsOnAwake != null)
		{
			worldEventsOnAwake.Apply();
		}
		LateStartHook.AddLateStartMethod(new Action(this.CreateCheckpoint));
	}

	// Token: 0x06000136 RID: 310 RVA: 0x000060C2 File Offset: 0x000042C2
	public void OnApplicationQuit()
	{
		GameController.IsClosing = true;
		if (this.m_logCallbackHandler != null)
		{
			this.m_logCallbackHandler.FlushEntriesToFile(this.m_logOutputFile);
		}
		MoonDebug.OnApplicationQuit();
	}

	// Token: 0x06000137 RID: 311 RVA: 0x000060EB File Offset: 0x000042EB
	public void Update()
	{
		if ((MoonInput.GetKey(KeyCode.LeftAlt) || MoonInput.GetKey(KeyCode.RightAlt)) && MoonInput.GetKeyDown(KeyCode.U))
		{
			SeinUI.DebugHideUI = !SeinUI.DebugHideUI;
		}
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00006124 File Offset: 0x00004324
	private static void CheckPackageFullyInstalled()
	{
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00006128 File Offset: 0x00004328
	public void FixedUpdate()
	{
		if (Scenes.Manager)
		{
			Scenes.Manager.CheckForScenesFinishedLoading();
		}
		if (!GameController.FreezeFixedUpdate)
		{
			FixedRandom.FixedUpdateIndex++;
			FixedRandom.UpdateValues();
		}
		Music.UpdateMusic();
		Ambience.UpdateAmbience();
		this.GameScheduler.OnGameFixedUpdate.Call();
		Respawner.UpdateRespawners();
		if (!GameStateMachine.Instance.IsInExtendedTitleScreen() && !UI.MainMenuVisible && (Screen.width != this.m_previousScreenWidth || Screen.height != this.m_previousScreenHeight))
		{
			UI.Menu.ShowResumeScreen();
		}
		this.m_previousScreenWidth = Screen.width;
		this.m_previousScreenHeight = Screen.height;
		if (this.m_lastDebugControlsEnabledValue != DebugMenuB.DebugControlsEnabled)
		{
			this.m_lastDebugControlsEnabledValue = DebugMenuB.DebugControlsEnabled;
		}
		if (!this.IsSuspended)
		{
			this.GameTime += Time.deltaTime;
		}
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0000621C File Offset: 0x0000441C
	public Objective GetObjectiveFromIndex(int index)
	{
		if (this.Objectives.Count > index && index >= 0)
		{
			return this.Objectives[index];
		}
		return null;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0000624F File Offset: 0x0000444F
	public int GetObjectiveIndex(Objective objective)
	{
		return this.Objectives.IndexOf(objective);
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00006260 File Offset: 0x00004460
	public void SuspendGameplay()
	{
		if (!this.GameplaySuspended)
		{
			Component[] suspendables = Characters.Sein.Controller.Suspendables;
			this.m_suspendablesToIgnoreForGameplay = new HashSet<ISuspendable>(suspendables.Cast<ISuspendable>());
			SuspensionManager.SuspendExcluding(this.m_suspendablesToIgnoreForGameplay);
			this.GameplaySuspended = true;
		}
	}

	// Token: 0x0600013D RID: 317 RVA: 0x000062AB File Offset: 0x000044AB
	public void ResumeGameplay()
	{
		if (this.GameplaySuspended)
		{
			SuspensionManager.ResumeExcluding(this.m_suspendablesToIgnoreForGameplay);
			this.m_suspendablesToIgnoreForGameplay.Clear();
			this.GameplaySuspended = false;
		}
	}

	// Token: 0x0600013E RID: 318 RVA: 0x000062D5 File Offset: 0x000044D5
	public void SuspendGameplayForUI()
	{
		if (!this.GameplaySuspendedForUI)
		{
			SuspensionManager.SuspendAll();
			this.GameplaySuspendedForUI = true;
		}
	}

	// Token: 0x0600013F RID: 319 RVA: 0x000062EE File Offset: 0x000044EE
	public void ResumeGameplayForUI()
	{
		if (this.GameplaySuspendedForUI)
		{
			SuspensionManager.ResumeAll();
			this.GameplaySuspendedForUI = false;
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00006308 File Offset: 0x00004508
	public void CreateCheckpoint()
	{
		SaveGameData saveGameData = Game.Checkpoint.SaveGameData;
		SaveSceneManager.Master.SaveWithoutClearing(saveGameData.Master);
		saveGameData.ApplyPendingScenes();
		if (Scenes.Manager)
		{
			foreach (SceneManagerScene sceneManagerScene in Scenes.Manager.ActiveScenes)
			{
				if (sceneManagerScene.IsVisible && sceneManagerScene.HasStartBeenCalled && sceneManagerScene.SceneRoot.SaveSceneManager)
				{
					sceneManagerScene.SceneRoot.SaveSceneManager.Save(saveGameData.InsertScene(sceneManagerScene.MetaData.SceneMoonGuid));
				}
			}
		}
		Game.Checkpoint.Events.OnPostCreate.Call();
	}

	// Token: 0x06000141 RID: 321 RVA: 0x000063E0 File Offset: 0x000045E0
	public void ClearCheckpointData()
	{
		Game.Checkpoint.SaveGameData.ClearAllData();
	}

	// Token: 0x06000142 RID: 322 RVA: 0x000063EC File Offset: 0x000045EC
	public void RestoreCheckpoint(Action onFinished = null)
	{
		this.IsLoadingGame = true;
		this.m_onRestoreCheckpointFinished = onFinished;
		LateStartHook.AddLateStartMethod(new Action(this.RestoreCheckpointImmediate));
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00006410 File Offset: 0x00004610
	public void RestoreCheckpointImmediate()
	{
		this.m_restoreCheckpointController.RestoreCheckpoint();
		if (this.m_onRestoreCheckpointFinished != null)
		{
			this.m_onRestoreCheckpointFinished();
			this.m_onRestoreCheckpointFinished = null;
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00006448 File Offset: 0x00004648
	private void HandleTrialData()
	{
		if (this.IsTrial)
		{
			return;
		}
		if (OutputFolder.PlayerTrialDataFolderPath == OutputFolder.PlayerDataFolderPath)
		{
			return;
		}
		if (!Directory.Exists(OutputFolder.PlayerTrialDataFolderPath))
		{
			return;
		}
		string[] files = Directory.GetFiles(OutputFolder.PlayerTrialDataFolderPath);
		for (int i = 0; i < files.Length; i++)
		{
			string fileName = Path.GetFileName(files[i]);
			string path = Path.Combine(OutputFolder.PlayerDataFolderPath, fileName);
			if (!File.Exists(path))
			{
				File.Move(files[i], Path.Combine(OutputFolder.PlayerDataFolderPath, fileName));
			}
		}
		if (Directory.GetFiles(OutputFolder.PlayerTrialDataFolderPath).Length == 0)
		{
			Directory.Delete(OutputFolder.PlayerTrialDataFolderPath);
		}
	}

	// Token: 0x06000145 RID: 325 RVA: 0x000064F8 File Offset: 0x000046F8
	[Conditional("NOT_FINAL_BUILD")]
	private void HandleBuildName()
	{
	}

	// Token: 0x06000146 RID: 326 RVA: 0x000064FA File Offset: 0x000046FA
	[Conditional("NOT_FINAL_BUILD")]
	private void HandleCommands()
	{
	}

	// Token: 0x06000147 RID: 327 RVA: 0x000064FC File Offset: 0x000046FC
	[Conditional("NOT_FINAL_BUILD")]
	private void HandleBuildIDString()
	{
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x06000148 RID: 328 RVA: 0x00006500 File Offset: 0x00004700
	public bool GameInTitleScreen
	{
		get
		{
			return GameStateMachine.Instance.CurrentState == GameStateMachine.State.TitleScreen || GameStateMachine.Instance.CurrentState == GameStateMachine.State.StartScreen;
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06000149 RID: 329 RVA: 0x0000652D File Offset: 0x0000472D
	// (set) Token: 0x0600014A RID: 330 RVA: 0x00006535 File Offset: 0x00004735
	public bool IsSuspended { get; set; }

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x0600014B RID: 331 RVA: 0x0000653E File Offset: 0x0000473E
	// (set) Token: 0x0600014C RID: 332 RVA: 0x00006546 File Offset: 0x00004746
	public bool PreventFocusPause { get; set; }

	// Token: 0x040000E2 RID: 226
	public const string TitleScreenSceneName = "titleScreenSwallowsNest";

	// Token: 0x040000E3 RID: 227
	public const string TrialEndScreenSceneName = "trialEndScreen";

	// Token: 0x040000E4 RID: 228
	public const string IntroLogosSceneName = "introLogos";

	// Token: 0x040000E5 RID: 229
	public const string TrailerSceneName = "trailerScene";

	// Token: 0x040000E6 RID: 230
	public const string WorldMapSceneName = "worldMapScene";

	// Token: 0x040000E7 RID: 231
	public const string EmptyTestSceneName = "emptyTestScene";

	// Token: 0x040000E8 RID: 232
	public const string BootLoadSceneName = "loadBootstrap";

	// Token: 0x040000E9 RID: 233
	public const string GameStartScene = "sunkenGladesRunaway";

	// Token: 0x040000EA RID: 234
	public GameTimer Timer;

	// Token: 0x040000EB RID: 235
	public static GameController Instance;

	// Token: 0x040000EC RID: 236
	public static bool FreezeFixedUpdate;

	// Token: 0x040000ED RID: 237
	public static bool IsClosing;

	// Token: 0x040000EE RID: 238
	public SaveGameController SaveGameController = new SaveGameController();

	// Token: 0x040000EF RID: 239
	public List<GameObject> Systems = new List<GameObject>();

	// Token: 0x040000F0 RID: 240
	public GameScheduler GameScheduler = new GameScheduler();

	// Token: 0x040000F1 RID: 241
	public AllContainer<Objective> ActiveObjectives = new AllContainer<Objective>();

	// Token: 0x040000F2 RID: 242
	public List<Objective> Objectives = new List<Objective>();

	// Token: 0x040000F3 RID: 243
	public string BuildIDString = string.Empty;

	// Token: 0x040000F4 RID: 244
	public string BuildName = string.Empty;

	// Token: 0x040000F5 RID: 245
	public UberAtlassingPlatform AtlasPlatform;

	// Token: 0x040000F6 RID: 246
	private HashSet<ISuspendable> m_suspendablesToIgnoreForGameplay = new HashSet<ISuspendable>();

	// Token: 0x040000F7 RID: 247
	private GameObject m_systemsGameObject;

	// Token: 0x040000F8 RID: 248
	private LogCallbackHandler m_logCallbackHandler;

	// Token: 0x040000F9 RID: 249
	private RestoreCheckpointController m_restoreCheckpointController = new RestoreCheckpointController();

	// Token: 0x040000FA RID: 250
	public int VSyncCount = 1;

	// Token: 0x040000FB RID: 251
	private string m_logOutputFile = string.Empty;

	// Token: 0x040000FC RID: 252
	public float GameTime;

	// Token: 0x040000FD RID: 253
	public ActionSequence GameSaveSequence;

	// Token: 0x040000FE RID: 254
	public static bool IsFocused = true;

	// Token: 0x040000FF RID: 255
	private static volatile bool m_isPackageFullyInstalled;

	// Token: 0x04000100 RID: 256
	public bool PCTrialValue;

	// Token: 0x04000101 RID: 257
	public bool EditorTrialValue;

	// Token: 0x04000102 RID: 258
	public WorldEvents DebugWorldEvents;

	// Token: 0x04000103 RID: 259
	private bool m_isRestartingGame;

	// Token: 0x04000104 RID: 260
	private bool m_setRunInBackgroundToFalse;

	// Token: 0x04000105 RID: 261
	public bool RequireInitialValues = true;

	// Token: 0x04000106 RID: 262
	public bool IsLoadingGame;

	// Token: 0x04000107 RID: 263
	public List<UnityEngine.Object> Resources;

	// Token: 0x04000108 RID: 264
	private bool m_lastDebugControlsEnabledValue;

	// Token: 0x04000109 RID: 265
	private int m_previousScreenWidth;

	// Token: 0x0400010A RID: 266
	private int m_previousScreenHeight;

	// Token: 0x0400010B RID: 267
	private float m_isPackageFullyInstalledTimer;

	// Token: 0x0400010C RID: 268
	private Action m_onRestoreCheckpointFinished;
}
