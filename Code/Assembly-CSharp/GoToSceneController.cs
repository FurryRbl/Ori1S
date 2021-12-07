using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200009C RID: 156
public class GoToSceneController : MonoBehaviour
{
	// Token: 0x17000187 RID: 391
	// (get) Token: 0x06000667 RID: 1639 RVA: 0x00018FBF File Offset: 0x000171BF
	public ScenesManager ScenesManager
	{
		get
		{
			return Scenes.Manager;
		}
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x00018FC8 File Offset: 0x000171C8
	public static bool CheckStartInScene(MoonGuid guid)
	{
		return GoToSceneController.Instance == null || GoToSceneController.Instance.StartInScene == guid || GoToSceneController.Instance.StartInScene == MoonGuid.Empty;
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x00019011 File Offset: 0x00017211
	public void Awake()
	{
		GoToSceneController.Instance = this;
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x00019019 File Offset: 0x00017219
	public void OnDestroy()
	{
		if (GoToSceneController.Instance == this)
		{
			GoToSceneController.Instance = null;
		}
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x00019034 File Offset: 0x00017234
	private void GoToScene(MoonGuid sceneGuid, Vector3 position, string sceneName, Action onComplete, bool createCheckpoint, bool async)
	{
		this.StartInScene = sceneGuid;
		if (sceneName == "titleScreenSwallowsNest")
		{
			GameStateMachine.Instance.SetToStartScreen();
		}
		this.m_onCompleteLoad = onComplete;
		this.m_position = position;
		this.ScenesManager.SetTargetPositions(this.m_position);
		InstantLoadScenesController.Instance.LoadScenesAtPosition(null, async, true);
		this.m_createCheckpointLater = createCheckpoint;
		this.m_useAfterSceneLoad = true;
		this.ScenesManager.AllowUnloadingOnScenes(position);
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x000190AC File Offset: 0x000172AC
	private void FinishGoingToPositionImmediately()
	{
		UI.Cameras.Current.MoveCameraToTargetInstantly(false);
		this.ScenesManager.UnloadScenesAtPosition(true);
		this.ScenesManager.AutoLoadingUnloading = true;
		if (this.m_onCompleteImmediateLoad != null)
		{
			this.m_onCompleteImmediateLoad();
			this.m_onCompleteImmediateLoad = null;
		}
		UI.Cameras.Current.Controller.UpdateCamera();
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x00019108 File Offset: 0x00017308
	public void OnScenesEnabled()
	{
		if (this.m_useAfterSceneLoad)
		{
			this.m_useAfterSceneLoad = false;
			this.CompleteGoingToAScene();
		}
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x00019124 File Offset: 0x00017324
	public void CompleteGoingToAScene()
	{
		if (Characters.Current != null)
		{
			Characters.Current.Position = this.m_position;
			Characters.Current.PlaceOnGround();
		}
		UI.Cameras.Current.CameraTarget.SetTargetPosition(this.m_position);
		UI.Cameras.Current.Controller.PuppetController.Reset();
		UI.Cameras.Current.GoToChaseMode();
		UI.Cameras.Current.MoveCameraToTargetInstantly(false);
		UI.Cameras.Current.OffsetController.UpdateOffset(true);
		UI.Cameras.Current.MoveCameraToTargetInstantly(false);
		if (Characters.Ori)
		{
			Characters.Ori.MoveOriBackToPlayer();
		}
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x000191C8 File Offset: 0x000173C8
	public void OnInstantLoadScenesControllerCompletedLoading()
	{
		if (this.m_onCompleteLoad != null)
		{
			this.m_onCompleteLoad();
			this.m_onCompleteLoad = null;
		}
		UI.Cameras.Current.Controller.UpdateCamera();
		if (this.m_createCheckpointLater)
		{
			this.m_createCheckpointLater = false;
			GameController.Instance.CreateCheckpoint();
			GameController.Instance.SaveGameController.PerformSave();
			GameController.Instance.PerformSaveGameSequence();
		}
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x00019238 File Offset: 0x00017438
	public void OnScenesManagerFixedUpdate()
	{
		if (this.m_isMovingImmediately)
		{
			this.m_isMovingImmediately = false;
			this.ScenesManager.SetTargetPositions(this.m_position);
			this.ScenesManager.AutoLoadingUnloading = false;
			this.ScenesManager.EnableDisabledScenesAtPosition(false);
			this.CompleteGoingToAScene();
			LateStartHook.AddLateStartMethod(new Action(this.FinishGoingToPositionImmediately));
		}
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x00019298 File Offset: 0x00017498
	public void GoToScene(SceneMetaData sceneMetaData, Action onComplete, bool createCheckpoint)
	{
		this.GoToScene(sceneMetaData.SceneMoonGuid, sceneMetaData.SeinPlaceholderPosition, sceneMetaData.name, onComplete, createCheckpoint, false);
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x000192C0 File Offset: 0x000174C0
	public void GoToScene(RuntimeSceneMetaData sceneMetaData, Action onComplete, bool createCheckpoint)
	{
		this.GoToScene(sceneMetaData.SceneMoonGuid, sceneMetaData.PlaceholderPosition, sceneMetaData.Scene, onComplete, createCheckpoint, false);
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x000192F0 File Offset: 0x000174F0
	public void GoToSceneAsync(SceneMetaData sceneMetaData, Action onComplete, bool createCheckpoint)
	{
		this.GoToScene(sceneMetaData.SceneMoonGuid, sceneMetaData.SeinPlaceholderPosition, sceneMetaData.name, onComplete, createCheckpoint, true);
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x00019318 File Offset: 0x00017518
	public void GoToSceneAsync(RuntimeSceneMetaData sceneMetaData, Action onComplete, bool createCheckpoint)
	{
		this.GoToScene(sceneMetaData.SceneMoonGuid, sceneMetaData.PlaceholderPosition, sceneMetaData.Scene, onComplete, createCheckpoint, true);
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x00019348 File Offset: 0x00017548
	public void GoToSceneImmediately(SceneMetaData scene, Action onComplete)
	{
		this.StartInScene = scene.SceneMoonGuid;
		this.m_position = scene.SeinPlaceholderPosition;
		this.m_onCompleteImmediateLoad = onComplete;
		this.m_isMovingImmediately = true;
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0001937C File Offset: 0x0001757C
	public void GoToScene(string path)
	{
		RuntimeSceneMetaData sceneInformation = Scenes.Manager.GetSceneInformation(path);
		if (sceneInformation == null)
		{
			return;
		}
		this.GoToScene(sceneInformation, null, true);
	}

	// Token: 0x040004EE RID: 1262
	public static GoToSceneController Instance;

	// Token: 0x040004EF RID: 1263
	public MoonGuid StartInScene;

	// Token: 0x040004F0 RID: 1264
	private Vector3 m_position;

	// Token: 0x040004F1 RID: 1265
	private bool m_useAfterSceneLoad;

	// Token: 0x040004F2 RID: 1266
	private bool m_createCheckpointLater;

	// Token: 0x040004F3 RID: 1267
	private Action m_onCompleteLoad;

	// Token: 0x040004F4 RID: 1268
	private Action m_onCompleteImmediateLoad;

	// Token: 0x040004F5 RID: 1269
	private bool m_isMovingImmediately;
}
