using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class ScenesManager : SaveSerialize
{
	// Token: 0x17000188 RID: 392
	// (get) Token: 0x0600068D RID: 1677 RVA: 0x00019CC8 File Offset: 0x00017EC8
	// (set) Token: 0x0600068E RID: 1678 RVA: 0x00019CD0 File Offset: 0x00017ED0
	public bool ScenesNotLoadedOnTime { get; private set; }

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x0600068F RID: 1679 RVA: 0x00019CD9 File Offset: 0x00017ED9
	// (set) Token: 0x06000690 RID: 1680 RVA: 0x00019CE1 File Offset: 0x00017EE1
	public float PaddingWidthExtension { get; set; }

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x06000691 RID: 1681 RVA: 0x00019CEC File Offset: 0x00017EEC
	public RuntimeSceneMetaData CurrentScene
	{
		get
		{
			for (int i = 0; i < this.ActiveScenes.Count; i++)
			{
				SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
				if (!sceneManagerScene.MetaData.DependantScene)
				{
					if (sceneManagerScene.IsVisible && UI.Cameras.Current && sceneManagerScene.MetaData.IsInsideSceneBounds(this.CurrentCameraTargetPosition))
					{
						return this.ActiveScenes[i].MetaData;
					}
				}
			}
			return null;
		}
	}

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x06000692 RID: 1682 RVA: 0x00019D80 File Offset: 0x00017F80
	public SceneManagerScene CurrentSceneManagerScene
	{
		get
		{
			for (int i = 0; i < this.ActiveScenes.Count; i++)
			{
				if (!this.ActiveScenes[i].MetaData.DependantScene)
				{
					if (UI.Cameras.Current && this.ActiveScenes[i].MetaData.IsInsideSceneBounds(this.CurrentCameraTargetPosition))
					{
						return this.ActiveScenes[i];
					}
				}
			}
			return null;
		}
	}

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x06000693 RID: 1683 RVA: 0x00019E0C File Offset: 0x0001800C
	// (set) Token: 0x06000694 RID: 1684 RVA: 0x00019E14 File Offset: 0x00018014
	public Vector2 CurrentCameraTargetPosition { get; private set; }

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x06000695 RID: 1685 RVA: 0x00019E1D File Offset: 0x0001801D
	// (set) Token: 0x06000696 RID: 1686 RVA: 0x00019E25 File Offset: 0x00018025
	public Vector2 CurrentCameraTargetPositionExtrapolated { get; private set; }

	// Token: 0x06000697 RID: 1687 RVA: 0x00019E30 File Offset: 0x00018030
	public bool SceneVisibleAtPosition(Vector3 position)
	{
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			if (!this.ActiveScenes[i].MetaData.DependantScene)
			{
				if (this.ActiveScenes[i].IsVisible && this.ActiveScenes[i].MetaData.IsInsideSceneBounds(position))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x00019EAE File Offset: 0x000180AE
	public bool SceneIsEnabled(SceneMetaData sceneMetaData)
	{
		return this.SceneIsEnabled(sceneMetaData.SceneMoonGuid);
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x00019EBC File Offset: 0x000180BC
	public bool SceneIsEnabled(MoonGuid sceneMoonGuid)
	{
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			if (sceneManagerScene.MetaData.SceneMoonGuid == sceneMoonGuid && sceneManagerScene.CurrentState == SceneManagerScene.State.Loaded)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x00019F17 File Offset: 0x00018117
	public void SetTargetPositions(Vector3 target)
	{
		this.CurrentCameraTargetPosition = target;
		this.CurrentCameraTargetPositionExtrapolated = target;
		this.m_cameraPositions.Clear();
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x0600069B RID: 1691 RVA: 0x00019F3C File Offset: 0x0001813C
	public bool IsLoadingScenes
	{
		get
		{
			for (int i = 0; i < this.ActiveScenes.Count; i++)
			{
				SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
				if (sceneManagerScene.CurrentState == SceneManagerScene.State.Loading)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x00019F84 File Offset: 0x00018184
	public Rect GetClampedRect(Vector3 position)
	{
		Rect rect = default(Rect);
		Rect rect2 = rect;
		rect2.width = 48f;
		rect2.height = 48f;
		rect2.center = position;
		rect = rect2;
		Rect rect3;
		if (this.GetSceneBoundaryAtPosition(rect.center, out rect3))
		{
			rect.xMin = Mathf.Max(rect.xMin, rect3.xMin + 0.1f);
			rect.yMin = Mathf.Max(rect.yMin, rect3.yMin + 0.1f);
			rect.xMax = Mathf.Min(rect.xMax, rect3.xMax - 0.1f);
			rect.yMax = Mathf.Min(rect.yMax, rect3.yMax - 0.1f);
		}
		return rect;
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x0001A05C File Offset: 0x0001825C
	public bool IsLoadingScene(Vector3 position)
	{
		Rect clampedRect = this.GetClampedRect(position);
		Rect currentSceneBounds;
		this.GetSceneBoundaryAtPosition(position, out currentSceneBounds);
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			if (!sceneManagerScene.MetaData.DependantScene)
			{
				if (sceneManagerScene.MetaData.IsInsideSceneBounds(clampedRect) || sceneManagerScene.MetaData.IsInsideScenePaddingBounds(clampedRect, currentSceneBounds))
				{
					if (!sceneManagerScene.IsLoadingComplete)
					{
						this.m_scenes.Clear();
						return true;
					}
					foreach (MoonGuid item in sceneManagerScene.MetaData.IncludedScenes)
					{
						this.m_scenes.Add(item);
					}
				}
			}
		}
		for (int j = 0; j < this.ActiveScenes.Count; j++)
		{
			SceneManagerScene sceneManagerScene2 = this.ActiveScenes[j];
			if (sceneManagerScene2.MetaData.DependantScene && this.m_scenes.Contains(sceneManagerScene2.MetaData.SceneMoonGuid) && !sceneManagerScene2.IsLoadingComplete)
			{
				this.m_scenes.Clear();
				return true;
			}
		}
		this.m_scenes.Clear();
		return false;
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0001A1D4 File Offset: 0x000183D4
	public bool PositionInsideSceneStillLoading(Vector3 position)
	{
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			if (!sceneManagerScene.MetaData.DependantScene)
			{
				if (sceneManagerScene.CurrentState == SceneManagerScene.State.Loading && sceneManagerScene.MetaData.IsInsideSceneBounds(position))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x0600069F RID: 1695 RVA: 0x0001A23F File Offset: 0x0001843F
	public bool ResourcesNeedUnloading
	{
		get
		{
			return this.m_resourcesNeedUnloading != 0;
		}
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0001A250 File Offset: 0x00018450
	public void DrawScenesManagerDebugData()
	{
		GUILayout.BeginArea(new Rect(8f, 16f, 550f, 500f));
		foreach (SceneManagerScene sceneManagerScene in this.ActiveScenes)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			switch (sceneManagerScene.CurrentState)
			{
			case SceneManagerScene.State.Disabling:
				GUI.color = new Color(0.8f, 0.8f, 0.8f, 1f);
				break;
			case SceneManagerScene.State.Disabled:
				GUI.color = new Color(0.2f, 0.2f, 0.5f, 1f);
				break;
			case SceneManagerScene.State.Loading:
				GUI.color = Color.yellow;
				break;
			case SceneManagerScene.State.LoadingCancelled:
				GUI.color = Color.red;
				break;
			case SceneManagerScene.State.Loaded:
				GUI.color = Color.white;
				break;
			}
			GUILayout.Label(sceneManagerScene.MetaData.Scene, new GUILayoutOption[0]);
			GUILayout.Label("Loading Time: " + sceneManagerScene.LoadingTime, new GUILayoutOption[0]);
			if (sceneManagerScene.KeepLoadedForCheckpoint)
			{
				GUILayout.Label("(checkpoint)", new GUILayoutOption[0]);
			}
			if (sceneManagerScene.PreventUnloading)
			{
				GUILayout.Label("(preloaded)", new GUILayoutOption[0]);
			}
			GUILayout.EndHorizontal();
		}
		GUI.color = Color.white;
		GUILayout.EndArea();
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0001A3F4 File Offset: 0x000185F4
	public RuntimeSceneMetaData GetSceneInformation(string sceneName)
	{
		for (int i = 0; i < this.AllScenes.Count; i++)
		{
			RuntimeSceneMetaData runtimeSceneMetaData = this.AllScenes[i];
			if (runtimeSceneMetaData.Scene == sceneName)
			{
				return runtimeSceneMetaData;
			}
		}
		return null;
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x0001A440 File Offset: 0x00018640
	public SceneManagerScene GetSceneManagerScene(string sceneName)
	{
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			if (this.ActiveScenes[i].MetaData.Scene == sceneName)
			{
				return this.ActiveScenes[i];
			}
		}
		return null;
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0001A498 File Offset: 0x00018698
	public override void Awake()
	{
		base.Awake();
		Scenes.Manager = this;
		this.GenerateGuidToRuntimeSceneMetaDataDictionary();
		GameController.Instance.GameScheduler.OnPassThroughScrollLock.Add(new Action(this.OnPassThroughScrollLock));
		Game.Checkpoint.Events.OnPostCreate.Add(new Action(this.OnCreateCheckpoint));
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
		AspectRatioManager.OnAspectChanged.Add(new Action(this.OnAspectRatioChanged));
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0001A520 File Offset: 0x00018720
	public void OnGameReset()
	{
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			sceneManagerScene.KeepLoadedForCheckpoint = false;
			sceneManagerScene.PreventUnloading = false;
		}
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0001A564 File Offset: 0x00018764
	private void GenerateGuidToRuntimeSceneMetaDataDictionary()
	{
		for (int i = 0; i < this.AllScenes.Count; i++)
		{
			RuntimeSceneMetaData runtimeSceneMetaData = this.AllScenes[i];
			this.m_guidToRuntimeSceneMetaDatas[runtimeSceneMetaData.SceneMoonGuid] = runtimeSceneMetaData;
		}
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x0001A5AC File Offset: 0x000187AC
	public override void OnDestroy()
	{
		GameController.Instance.GameScheduler.OnPassThroughScrollLock.Remove(new Action(this.OnPassThroughScrollLock));
		Game.Checkpoint.Events.OnPostCreate.Remove(new Action(this.OnCreateCheckpoint));
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
		AspectRatioManager.OnAspectChanged.Remove(new Action(this.OnAspectRatioChanged));
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x0001A620 File Offset: 0x00018820
	public void OnAspectRatioChanged()
	{
		this.UpdatePaddingWidthExtension();
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x0001A628 File Offset: 0x00018828
	public override void Serialize(Archive ar)
	{
		this.CurrentCameraTargetPosition = ar.Serialize(this.CurrentCameraTargetPosition);
		if (ar.Reading)
		{
			this.CurrentCameraTargetPositionExtrapolated = this.CurrentCameraTargetPosition;
		}
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0001A660 File Offset: 0x00018860
	public void MarkLoadingScenesAsCancel()
	{
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			if (!sceneManagerScene.MetaData.DependantScene && sceneManagerScene.CurrentState == SceneManagerScene.State.Loading)
			{
				sceneManagerScene.ChangeState(SceneManagerScene.State.LoadingCancelled);
				if (this.CancelScene(sceneManagerScene))
				{
					i--;
				}
			}
		}
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x0001A6C9 File Offset: 0x000188C9
	public void OnCreateCheckpoint()
	{
		this.MarkActiveScenesAsKeepLoaded();
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0001A6D4 File Offset: 0x000188D4
	public void MarkActiveScenesAsKeepLoaded()
	{
		Rect rect = default(Rect);
		Rect rect2 = rect;
		rect2.width = 48f;
		rect2.height = 48f;
		rect2.center = this.CurrentCameraTargetPosition;
		rect = rect2;
		Rect rect3;
		if (this.GetSceneBoundaryAtPosition(rect.center, out rect3))
		{
			rect.xMin = Mathf.Max(rect.xMin, rect3.xMin + 0.1f);
			rect.yMin = Mathf.Max(rect.yMin, rect3.yMin + 0.1f);
			rect.xMax = Mathf.Max(rect.xMax, rect3.xMax - 0.1f);
			rect.yMax = Mathf.Max(rect.yMin, rect3.yMax - 0.1f);
		}
		else
		{
			rect.width = 0f;
			rect.height = 0f;
		}
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			if (sceneManagerScene.MetaData.IsInsideSceneBounds(rect))
			{
				sceneManagerScene.KeepLoadedForCheckpoint = true;
			}
			else if (sceneManagerScene.MetaData.IsInsideSceneLoadingZone(rect))
			{
				sceneManagerScene.KeepLoadedForCheckpoint = true;
			}
			else if (sceneManagerScene.MetaData.IsInsideScenePaddingBounds(rect))
			{
				sceneManagerScene.KeepLoadedForCheckpoint = true;
			}
			else
			{
				sceneManagerScene.KeepLoadedForCheckpoint = false;
			}
		}
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x0001A854 File Offset: 0x00018A54
	public void ClearKeepLoadedForCheckpoint()
	{
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			sceneManagerScene.KeepLoadedForCheckpoint = false;
		}
	}

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001A891 File Offset: 0x00018A91
	// (set) Token: 0x060006AE RID: 1710 RVA: 0x0001A899 File Offset: 0x00018A99
	public bool HasReportedScenesLoading { get; set; }

	// Token: 0x060006AF RID: 1711 RVA: 0x0001A8A4 File Offset: 0x00018AA4
	public void ReportScenesThatAreStillLoading()
	{
		this.HasReportedScenesLoading = true;
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			if (sceneManagerScene.CurrentState == SceneManagerScene.State.Loading)
			{
			}
		}
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x0001A8F0 File Offset: 0x00018AF0
	private void DetectScenesNotLoadedInTime()
	{
		if (this.ScenesNotLoadedOnTime)
		{
			if (!this.AnyMissingScenesAtCurrentPosition())
			{
				this.ScenesNotLoadedOnTime = false;
			}
		}
		else if (this.AnyMissingScenesAtCurrentPosition())
		{
			this.ScenesNotLoadedOnTime = true;
		}
	}

	// Token: 0x17000191 RID: 401
	// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001A934 File Offset: 0x00018B34
	private string SceneToLoad
	{
		get
		{
			if (this.m_scenesToLoad.Count > 0)
			{
				return this.m_scenesToLoad[0];
			}
			if (this.m_backgroundsToLoad.Count > 0)
			{
				return this.m_backgroundsToLoad[0];
			}
			return string.Empty;
		}
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0001A984 File Offset: 0x00018B84
	private string PopSceneToLoad()
	{
		if (this.m_scenesToLoad.Count > 0)
		{
			string text = this.m_scenesToLoad[0];
			this.m_scenesToLoad.Remove(text);
			return text;
		}
		if (this.m_backgroundsToLoad.Count > 0)
		{
			string text2 = this.m_backgroundsToLoad[0];
			this.m_backgroundsToLoad.Remove(text2);
			return text2;
		}
		return string.Empty;
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x0001A9F0 File Offset: 0x00018BF0
	private void UpdateLoadingScenes()
	{
		if (this.m_currentLoad != null && this.m_currentLoad.isDone)
		{
			this.m_currentLoad = null;
		}
		if (this.m_currentLoad == null && this.SceneToLoad != string.Empty && this.CanLoadScenes)
		{
			this.m_currentLoad = Application.LoadLevelAdditiveAsync(this.PopSceneToLoad());
		}
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0001AA5C File Offset: 0x00018C5C
	public void TestForFallOutOfWorld()
	{
		if (this.m_testDelayTime <= 0f)
		{
			this.m_testDelayTime = 1f;
			if (!this.IsInsideASceneBoundary(this.CurrentCameraTargetPosition))
			{
				GameController.Instance.RestoreCheckpoint(null);
			}
		}
		this.m_testDelayTime -= Time.deltaTime;
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0001AAB8 File Offset: 0x00018CB8
	private IEnumerator ShowFellOutOfWorldMessage()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		MessageBox message = UI.MessageController.ShowHintMessage(Scenes.Manager.FellOutOfWorldMessage, OnScreenPositions.TopCenter, 3f);
		yield break;
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0001AACC File Offset: 0x00018CCC
	public void ForceTestForOutOfWorld()
	{
		this.m_testDelayTime = 0f;
		this.TestForFallOutOfWorld();
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0001AAE0 File Offset: 0x00018CE0
	public void FixedUpdate()
	{
		if (UI.Cameras.Current.ScrollLockIsFadingOut)
		{
			return;
		}
		if (this.AutoLoadingUnloading)
		{
			this.DetectScenesNotLoadedInTime();
			this.UpdateScenes();
			this.UpdateExtrapolatedPosition();
			this.EnableDisabledScenesAtPosition(true);
			this.TestForFallOutOfWorld();
		}
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0001AB28 File Offset: 0x00018D28
	private void UpdatePaddingWidthExtension()
	{
		GameplayCamera current = UI.Cameras.Current;
		if (current)
		{
			float cameraWidthWorldUnits = UI.Cameras.Current.CameraWidthWorldUnits;
			float num = cameraWidthWorldUnits - cameraWidthWorldUnits * 1.7777778f / AspectRatioManager.AspectRatio;
			this.PaddingWidthExtension = num * 0.5f;
		}
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x0001AB70 File Offset: 0x00018D70
	public void UpdatePosition()
	{
		this.m_cameraPositions.Clear();
		for (int i = 0; i < UI.Cameras.Manager.Cameras.Count; i++)
		{
			CameraController cameraController = UI.Cameras.Manager.Cameras[i];
			if (cameraController.PuppetController.Tween > 0.5f)
			{
				this.m_cameraPositions.Add(cameraController.Position);
			}
		}
		if (UI.Cameras.Current.Target)
		{
			if (!Scenes.Manager.ScenesNotLoadedOnTime)
			{
				UI.Cameras.Current.CameraTarget.UpdateTargetPosition();
			}
			this.CurrentCameraTargetPosition = UI.Cameras.Current.CameraTarget.TargetPosition;
			this.CurrentCameraTargetPositionExtrapolated = this.CurrentCameraTargetPosition;
			this.UpdateExtrapolatedPosition();
		}
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x0001AC47 File Offset: 0x00018E47
	public void ClearCameraPuppetPositions()
	{
		this.m_cameraPositions.Clear();
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x0001AC54 File Offset: 0x00018E54
	public void UpdateExtrapolatedPosition()
	{
		Rect rect;
		if (Characters.Sein && this.GetSceneBoundaryAtPosition(this.CurrentCameraTargetPosition, out rect))
		{
			Vector2 vector = this.CurrentCameraTargetPosition + Vector2.ClampMagnitude(Characters.Sein.PhysicsSpeed * 2f, 24f);
			this.CurrentCameraTargetPositionExtrapolated = new Vector2(Mathf.Clamp(vector.x, rect.xMin + 0.1f, rect.xMax - 0.1f), Mathf.Clamp(vector.y, rect.yMin + 0.1f, rect.yMax - 0.1f));
			Vector3 vector2 = this.CurrentCameraTargetPosition;
			Vector3 vector3 = this.CurrentCameraTargetPositionExtrapolated;
			bool flag = Mathf.Abs(vector3.x - vector2.x) > Mathf.Abs(vector3.y - vector2.y);
			for (int i = 0; i < 6; i++)
			{
				Debug.DrawLine(vector2, vector3, Color.gray);
				RaycastHit raycastHit;
				if (!Physics.Linecast(vector2, vector3, out raycastHit, this.RaycastMask))
				{
					this.CurrentCameraTargetPositionExtrapolated = vector3;
					break;
				}
				vector2 = raycastHit.point - (vector3 - vector2).normalized * 0.02f;
				if (i == 5)
				{
					this.CurrentCameraTargetPositionExtrapolated = vector2;
					break;
				}
				Vector3 vector4 = vector2;
				vector4 += 4f * ((!flag) ? ((raycastHit.normal.x <= 0f) ? Vector3.left : Vector3.right) : ((raycastHit.normal.y <= 0f) ? Vector3.down : Vector3.up));
				Debug.DrawLine(vector2, vector4, Color.gray);
				vector2 = ((!Physics.Linecast(vector2, vector4, out raycastHit, this.RaycastMask)) ? vector4 : (raycastHit.point - (vector4 - vector2).normalized * 0.02f));
			}
		}
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x0001AEB0 File Offset: 0x000190B0
	public bool GetSceneBoundaryAtPosition(Vector3 position, out Rect bound)
	{
		for (int i = 0; i < this.AllScenes.Count; i++)
		{
			RuntimeSceneMetaData runtimeSceneMetaData = this.AllScenes[i];
			if (!runtimeSceneMetaData.DependantScene)
			{
				if (runtimeSceneMetaData.IsInTotal(position))
				{
					if (runtimeSceneMetaData.CanBeLoaded)
					{
						for (int j = 0; j < runtimeSceneMetaData.SceneBoundaries.Count; j++)
						{
							Rect rect = runtimeSceneMetaData.SceneBoundaries[j];
							if (rect.Contains(position))
							{
								bound = rect;
								return true;
							}
						}
					}
				}
			}
		}
		bound = new Rect(0f, 0f, 0f, 0f);
		return false;
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x0001AF74 File Offset: 0x00019174
	public bool IsInsideASceneBoundary(Vector3 position)
	{
		List<RuntimeSceneMetaData> allScenes = this.AllScenes;
		for (int i = 0; i < allScenes.Count; i++)
		{
			RuntimeSceneMetaData runtimeSceneMetaData = allScenes[i];
			if (!runtimeSceneMetaData.DependantScene)
			{
				if (runtimeSceneMetaData.IsInTotal(position) && runtimeSceneMetaData.IsInsideSceneBounds(position))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x0001AFD4 File Offset: 0x000191D4
	public bool IsInsideActiveSceneBoundary(Vector3 position)
	{
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			if (!sceneManagerScene.MetaData.DependantScene)
			{
				if ((sceneManagerScene.CurrentState == SceneManagerScene.State.Loaded || sceneManagerScene.CurrentState == SceneManagerScene.State.Disabling) && sceneManagerScene.MetaData.IsInsideSceneBounds(position))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x0001B04C File Offset: 0x0001924C
	public bool IsInsideAScenePaddingBoundary(Vector3 position)
	{
		Rect currentSceneBounds;
		this.GetSceneBoundaryAtPosition(position, out currentSceneBounds);
		List<RuntimeSceneMetaData> allScenes = this.AllScenes;
		for (int i = 0; i < allScenes.Count; i++)
		{
			if (allScenes[i].IsInsideScenePaddingBounds(position, currentSceneBounds))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x0001B098 File Offset: 0x00019298
	public void Update()
	{
		if (this.m_resourcesNeedUnloading == 1)
		{
			SaveSceneManager.Master.ReleaseNullReferences();
			SuspensionManager.CleanupSuspendables();
		}
		if (this.m_resourcesNeedUnloading > 0)
		{
			this.m_resourcesNeedUnloading--;
		}
		this.DestroyManager.Update();
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x0001B0E8 File Offset: 0x000192E8
	public SceneRoot FindLoadedSceneRootFromPosition(Vector3 position)
	{
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			if (sceneManagerScene.CurrentState == SceneManagerScene.State.Loaded || sceneManagerScene.CurrentState == SceneManagerScene.State.Disabled || sceneManagerScene.CurrentState == SceneManagerScene.State.Disabling)
			{
				if (sceneManagerScene.SceneRoot && sceneManagerScene.SceneRoot.MetaData)
				{
					if (!sceneManagerScene.SceneRoot.MetaData.DependantScene)
					{
						if (sceneManagerScene.MetaData.IsInsideSceneBounds(position))
						{
							if (!sceneManagerScene.MetaData.LoadingCondition || sceneManagerScene.MetaData.LoadingCondition.Validate(null))
							{
								return sceneManagerScene.SceneRoot;
							}
						}
					}
				}
			}
		}
		return null;
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x0001B1D0 File Offset: 0x000193D0
	public SceneManagerScene GetFromCurrentScenes(RuntimeSceneMetaData sceneMetaData)
	{
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			if (sceneManagerScene.MetaData == sceneMetaData)
			{
				return sceneManagerScene;
			}
		}
		return null;
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x0001B218 File Offset: 0x00019418
	public RuntimeSceneMetaData FindRuntimeSceneMetaData(MoonGuid sceneGuid)
	{
		RuntimeSceneMetaData result;
		if (this.m_guidToRuntimeSceneMetaDatas.TryGetValue(sceneGuid, out result))
		{
			return result;
		}
		return null;
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0001B23B File Offset: 0x0001943B
	public void PreloadScene(RuntimeSceneMetaData sceneMetaData)
	{
		this.AdditivelyLoadScenesAtPosition(sceneMetaData.PlaceholderPosition, true, false, true);
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x0001B251 File Offset: 0x00019451
	public void PreloadScene(SceneMetaData sceneMetaData)
	{
		this.AdditivelyLoadScenesAtPosition(sceneMetaData.SeinPlaceholderPosition, true, false, true);
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x0001B262 File Offset: 0x00019462
	private void RemoveScene(SceneManagerScene scene)
	{
		this.ActiveScenes.Remove(scene);
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x0001B274 File Offset: 0x00019474
	private bool CanLevelBeLoaded(string sceneName)
	{
		bool flag;
		if (this.m_canBeStreamed.TryGetValue(sceneName, out flag))
		{
			return flag;
		}
		flag = Application.CanStreamedLevelBeLoaded(sceneName);
		this.m_canBeStreamed[sceneName] = flag;
		return flag;
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0001B2AC File Offset: 0x000194AC
	public void AdditivelyLoadScenesAtPosition(Vector3 position, bool async, bool loadingZones = true, bool keepPreloaded = false)
	{
		if (Time.timeScale > 2f)
		{
			async = false;
		}
		List<RuntimeSceneMetaData> allScenes = this.AllScenes;
		int count = allScenes.Count;
		Rect currentSceneBounds;
		this.GetSceneBoundaryAtPosition(position, out currentSceneBounds);
		for (int i = 0; i < count; i++)
		{
			RuntimeSceneMetaData runtimeSceneMetaData = allScenes[i];
			if (!runtimeSceneMetaData.DependantScene)
			{
				if (runtimeSceneMetaData.IsInTotal(position))
				{
					if (runtimeSceneMetaData.IsInsideSceneBounds(position))
					{
						if (runtimeSceneMetaData.CanBeLoaded)
						{
							this.AdditivelyLoadScene(runtimeSceneMetaData, async, keepPreloaded);
						}
					}
					else if (runtimeSceneMetaData.IsInsideScenePaddingBounds(position, currentSceneBounds))
					{
						if (runtimeSceneMetaData.CanBeLoaded)
						{
							this.AdditivelyLoadScene(runtimeSceneMetaData, async, keepPreloaded);
						}
					}
					else if (runtimeSceneMetaData.IsInsideSceneLoadingZone(position) && runtimeSceneMetaData.CanBeLoaded && loadingZones)
					{
						this.AdditivelyLoadScene(runtimeSceneMetaData, true, keepPreloaded);
					}
				}
			}
		}
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x0001B3A0 File Offset: 0x000195A0
	public void AdditivelyLoadScenesInsideRect(Rect rect, bool async, bool loadingZones = true, bool keepPreloaded = false)
	{
		if (Time.timeScale > 2f)
		{
			async = false;
		}
		List<RuntimeSceneMetaData> allScenes = this.AllScenes;
		int count = allScenes.Count;
		Rect currentSceneBounds;
		this.GetSceneBoundaryAtPosition(rect.center, out currentSceneBounds);
		for (int i = 0; i < count; i++)
		{
			RuntimeSceneMetaData runtimeSceneMetaData = allScenes[i];
			if (!runtimeSceneMetaData.DependantScene)
			{
				if (runtimeSceneMetaData.IsInTotal(rect))
				{
					if (runtimeSceneMetaData.IsInsideSceneBounds(rect))
					{
						if (runtimeSceneMetaData.CanBeLoaded)
						{
							this.AdditivelyLoadScene(runtimeSceneMetaData, async, keepPreloaded);
						}
					}
					else if (runtimeSceneMetaData.IsInsideScenePaddingBounds(rect, currentSceneBounds))
					{
						if (runtimeSceneMetaData.CanBeLoaded)
						{
							this.AdditivelyLoadScene(runtimeSceneMetaData, async, keepPreloaded);
						}
					}
					else if (runtimeSceneMetaData.IsInsideSceneLoadingZone(rect) && runtimeSceneMetaData.CanBeLoaded && loadingZones)
					{
						this.AdditivelyLoadScene(runtimeSceneMetaData, true, keepPreloaded);
					}
				}
			}
		}
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x0001B498 File Offset: 0x00019698
	private void AdditivelyLoadScene(RuntimeSceneMetaData sceneMetaData, bool async, bool keepPreloaded = false)
	{
		SceneManagerScene fromCurrentScenes = this.GetFromCurrentScenes(sceneMetaData);
		if (fromCurrentScenes != null)
		{
			if (fromCurrentScenes.CurrentState == SceneManagerScene.State.LoadingCancelled)
			{
				fromCurrentScenes.ChangeState(SceneManagerScene.State.Loading);
				this.LoadDependantScenes(fromCurrentScenes.MetaData, true);
				if (keepPreloaded)
				{
					fromCurrentScenes.PreventUnloading = true;
				}
			}
		}
		else if (this.CanLevelBeLoaded(sceneMetaData.Scene))
		{
			if (this.CanLoadScenes)
			{
				if (async)
				{
					AsyncOperation asyncOperation = Application.LoadLevelAdditiveAsync(sceneMetaData.Scene);
					asyncOperation.priority = 2;
				}
				else
				{
					Application.LoadLevelAdditive(sceneMetaData.Scene);
				}
			}
			SceneManagerScene sceneManagerScene = new SceneManagerScene(sceneMetaData);
			this.ActiveScenes.Add(sceneManagerScene);
			sceneManagerScene.PreventUnloading = keepPreloaded;
			this.LoadDependantScenes(sceneMetaData, async);
		}
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0001B554 File Offset: 0x00019754
	private void LoadDependantScenes(RuntimeSceneMetaData sceneMetaData, bool async)
	{
		for (int i = 0; i < sceneMetaData.IncludedScenes.Count; i++)
		{
			RuntimeSceneMetaData runtimeSceneMetaData = this.FindRuntimeSceneMetaData(sceneMetaData.IncludedScenes[i]);
			if (runtimeSceneMetaData != null && runtimeSceneMetaData.CanBeLoaded)
			{
				this.AdditivelyLoadScene(runtimeSceneMetaData, async, false);
			}
		}
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x0001B5AC File Offset: 0x000197AC
	public void UnloadScenesAtPosition(bool instant)
	{
		Rect clampedRect = this.GetClampedRect(this.CurrentCameraTargetPosition);
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			RuntimeSceneMetaData metaData = sceneManagerScene.MetaData;
			if (metaData != null)
			{
				if (!metaData.DependantScene)
				{
					bool flag = metaData.IsInsideSceneBounds(this.CurrentCameraTargetPosition) || metaData.IsInsideScenePaddingBounds(this.CurrentCameraTargetPosition);
					for (int j = 0; j < this.m_cameraPositions.Count; j++)
					{
						Vector3 position = this.m_cameraPositions[j];
						if (metaData.IsInsideSceneBounds(position) || metaData.IsInsideScenePaddingBounds(position))
						{
							flag = true;
						}
					}
					if (!flag || !metaData.CanBeLoaded)
					{
						bool keepInMemory = (metaData.CanBeLoaded && (metaData.IsInsideSceneLoadingZone(clampedRect) || metaData.IsInsideSceneBounds(clampedRect) || metaData.IsInsideScenePaddingBoundsExpanded(clampedRect))) || sceneManagerScene.PreventUnloading || sceneManagerScene.KeepLoadedForCheckpoint || sceneManagerScene.IsTitleScreen;
						if (this.UnloadScene(sceneManagerScene, keepInMemory, instant || !metaData.CanBeLoaded))
						{
							i--;
						}
					}
				}
			}
		}
		this.UnloadDependantScenes();
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x0001B724 File Offset: 0x00019924
	public void OnPassThroughScrollLock()
	{
		this.UpdateScenes();
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x0001B72C File Offset: 0x0001992C
	public void OnDisableSceneRoot(SceneRoot sceneRoot)
	{
		try
		{
			Events.Scheduler.OnSceneRootDisabled.Call(sceneRoot);
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x0001B764 File Offset: 0x00019964
	public bool UnloadScene(SceneManagerScene scene, bool keepInMemory, bool instant)
	{
		if (!this.AllowDestroying)
		{
			keepInMemory = true;
		}
		if (keepInMemory)
		{
			switch (scene.CurrentState)
			{
			case SceneManagerScene.State.Disabling:
				if (Time.time > scene.UnloadTime || instant)
				{
					this.OnDisableSceneRoot(scene.SceneRoot);
					scene.ChangeState(SceneManagerScene.State.Disabled);
					scene.SceneRoot.Save();
					scene.SceneRoot.DisableScene();
				}
				return false;
			case SceneManagerScene.State.LoadingCancelled:
				scene.ChangeState(SceneManagerScene.State.Loading);
				return false;
			case SceneManagerScene.State.Loaded:
				if (instant)
				{
					scene.ChangeState(SceneManagerScene.State.Disabled);
					scene.SceneRoot.Save();
					this.OnDisableSceneRoot(scene.SceneRoot);
					scene.SceneRoot.DisableScene();
				}
				else
				{
					scene.ChangeState(SceneManagerScene.State.Disabling);
					scene.UnloadTime = Time.time + this.UnloadDelay;
				}
				return false;
			}
		}
		else
		{
			switch (scene.CurrentState)
			{
			case SceneManagerScene.State.Disabling:
				if (Time.time > scene.UnloadTime)
				{
					this.OnDisableSceneRoot(scene.SceneRoot);
					scene.SceneRoot.SaveAndUnload();
					this.RemoveScene(scene);
					return true;
				}
				return false;
			case SceneManagerScene.State.Disabled:
				scene.SceneRoot.Unload();
				this.RemoveScene(scene);
				return true;
			case SceneManagerScene.State.Loading:
				scene.ChangeState(SceneManagerScene.State.LoadingCancelled);
				return this.CancelScene(scene);
			case SceneManagerScene.State.Loaded:
				if (instant)
				{
					this.OnDisableSceneRoot(scene.SceneRoot);
					scene.SceneRoot.SaveAndUnload();
					this.RemoveScene(scene);
					return true;
				}
				scene.ChangeState(SceneManagerScene.State.Disabling);
				scene.UnloadTime = Time.time + this.UnloadDelay;
				return false;
			}
		}
		return false;
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x0001B906 File Offset: 0x00019B06
	public void ReleaseUnusedResources()
	{
		this.m_resourcesNeedUnloading = 3;
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x0001B910 File Offset: 0x00019B10
	public void UnloadDependantScenes()
	{
		Vector3 position = this.CurrentCameraTargetPosition;
		this.m_scenesToDisable.Clear();
		this.m_scenesToInclude.Clear();
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			if (!sceneManagerScene.MetaData.DependantScene)
			{
				if (sceneManagerScene.CurrentState == SceneManagerScene.State.Disabled || sceneManagerScene.CurrentState == SceneManagerScene.State.Loading)
				{
					for (int j = 0; j < sceneManagerScene.MetaData.IncludedScenes.Count; j++)
					{
						RuntimeSceneMetaData runtimeSceneMetaData = this.FindRuntimeSceneMetaData(sceneManagerScene.MetaData.IncludedScenes[j]);
						if (runtimeSceneMetaData != null)
						{
							this.m_scenesToDisable.Add(runtimeSceneMetaData);
						}
					}
				}
				if (sceneManagerScene.CurrentState == SceneManagerScene.State.Loaded || sceneManagerScene.CurrentState == SceneManagerScene.State.Disabling)
				{
					if (sceneManagerScene.MetaData.IsInsideSceneBounds(position) || sceneManagerScene.MetaData.IsInsideScenePaddingBounds(position))
					{
						for (int k = 0; k < sceneManagerScene.MetaData.IncludedScenes.Count; k++)
						{
							RuntimeSceneMetaData runtimeSceneMetaData2 = this.FindRuntimeSceneMetaData(sceneManagerScene.MetaData.IncludedScenes[k]);
							if (runtimeSceneMetaData2 != null)
							{
								this.m_scenesToInclude.Add(runtimeSceneMetaData2);
							}
						}
					}
					else
					{
						for (int l = 0; l < sceneManagerScene.MetaData.IncludedScenes.Count; l++)
						{
							RuntimeSceneMetaData runtimeSceneMetaData3 = this.FindRuntimeSceneMetaData(sceneManagerScene.MetaData.IncludedScenes[l]);
							if (runtimeSceneMetaData3 != null)
							{
								this.m_scenesToDisable.Add(runtimeSceneMetaData3);
							}
						}
					}
				}
			}
		}
		for (int m = 0; m < this.ActiveScenes.Count; m++)
		{
			SceneManagerScene sceneManagerScene2 = this.ActiveScenes[m];
			RuntimeSceneMetaData metaData = sceneManagerScene2.MetaData;
			if (metaData.DependantScene && !this.m_scenesToInclude.Contains(metaData) && this.UnloadScene(sceneManagerScene2, this.m_scenesToDisable.Contains(metaData), true))
			{
				m--;
			}
		}
		this.m_scenesToDisable.Clear();
		this.m_scenesToInclude.Clear();
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x0001BB58 File Offset: 0x00019D58
	public void UpdateScenes()
	{
		this.UpdatePosition();
		if (this.IsInsideASceneBoundary(this.CurrentCameraTargetPosition) && !this.ScenesNotLoadedOnTime)
		{
			this.UnloadScenesAtPosition(false);
		}
		this.AdditivelyLoadScenesAtPosition(this.CurrentCameraTargetPositionExtrapolated, true, true, false);
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x0001BBA8 File Offset: 0x00019DA8
	public void OnSceneStartCompleted(SceneRoot sceneRoot)
	{
		RuntimeSceneMetaData sceneMetaData = this.FindRuntimeSceneMetaData(sceneRoot.MetaData.SceneMoonGuid);
		SceneManagerScene fromCurrentScenes = this.GetFromCurrentScenes(sceneMetaData);
		if (fromCurrentScenes != null)
		{
			fromCurrentScenes.HasStartBeenCalled = true;
		}
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x0001BBDC File Offset: 0x00019DDC
	public void Register(SceneRoot sceneRoot)
	{
		if (sceneRoot.name == "worldMapScene")
		{
			WorldMapUI.OnFinishedLoading(sceneRoot);
			return;
		}
		RuntimeSceneMetaData sceneMetaData = this.FindRuntimeSceneMetaData(sceneRoot.MetaData.SceneMoonGuid);
		SceneManagerScene sceneManagerScene = this.GetFromCurrentScenes(sceneMetaData);
		SceneMetaData metaData = sceneRoot.MetaData;
		if (sceneManagerScene == null)
		{
			sceneManagerScene = new SceneManagerScene(sceneRoot, sceneMetaData);
			this.UpdatePosition();
			if (sceneRoot.MetaData.IsInsideSceneBounds(this.CurrentCameraTargetPosition) || sceneRoot.MetaData.IsInsideScenePaddingBounds(this.CurrentCameraTargetPosition))
			{
				this.ActiveScenes.Add(sceneManagerScene);
				this.EnableDisabledScene(sceneManagerScene);
			}
			else
			{
				sceneManagerScene.CurrentState = SceneManagerScene.State.Disabled;
				this.ActiveScenes.Add(sceneManagerScene);
				sceneRoot.DisableScene();
			}
		}
		else
		{
			if (sceneManagerScene.SceneRoot == sceneRoot)
			{
				return;
			}
			if (sceneManagerScene.CurrentState == SceneManagerScene.State.Loading)
			{
				sceneManagerScene.ChangeState(SceneManagerScene.State.Disabled);
				if (sceneRoot.MetaData.RootPosition != sceneRoot.transform.position)
				{
					sceneRoot.transform.position = sceneRoot.MetaData.RootPosition;
				}
				sceneManagerScene.SceneRoot = sceneRoot;
				sceneRoot.DisableScene();
			}
			else if (sceneManagerScene.CurrentState == SceneManagerScene.State.LoadingCancelled)
			{
				sceneManagerScene.SceneRoot = sceneRoot;
				sceneRoot.Unload();
				this.RemoveScene(sceneManagerScene);
			}
			else
			{
				sceneRoot.Unload();
			}
		}
		sceneManagerScene.LoadingTime = Time.realtimeSinceStartup - sceneManagerScene.TimeOfLoad;
		SceneFrameworkPerformanceMonitor.AddSceneLoadItem(sceneManagerScene);
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x0001BD58 File Offset: 0x00019F58
	public bool AnyMissingScenesAtCurrentPosition()
	{
		Vector3 vector = this.CurrentCameraTargetPosition;
		Bounds cameraBoundingBox = UI.Cameras.Current.CameraBoundingBox;
		cameraBoundingBox.Expand(2f);
		cameraBoundingBox.center = vector;
		Rect rect = Utility.RectFromBounds(cameraBoundingBox);
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			RuntimeSceneMetaData metaData = sceneManagerScene.MetaData;
			if (!metaData.DependantScene)
			{
				bool flag = metaData.IsInsideSceneBounds(rect) && (metaData.IsInsideSceneBounds(vector) || metaData.IsInsideScenePaddingBounds(vector));
				if (flag && sceneManagerScene.UnityIsLoading)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x0001BE1C File Offset: 0x0001A01C
	public void EnableDisabledScenesAtPosition(bool limitOnce = false)
	{
		Vector3 position = this.CurrentCameraTargetPosition;
		this.m_scenesToEnable.Clear();
		Rect currentSceneBounds;
		this.GetSceneBoundaryAtPosition(position, out currentSceneBounds);
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			if (!sceneManagerScene.UnityIsLoading)
			{
				if (sceneManagerScene.MetaData != null)
				{
					RuntimeSceneMetaData metaData = sceneManagerScene.MetaData;
					if (!metaData.DependantScene)
					{
						if (sceneManagerScene.CurrentState == SceneManagerScene.State.Disabled || sceneManagerScene.CurrentState == SceneManagerScene.State.Disabling)
						{
							bool flag = metaData.IsInsideSceneBounds(position) || metaData.IsInsideScenePaddingBounds(position, currentSceneBounds);
							for (int j = 0; j < this.m_cameraPositions.Count; j++)
							{
								Vector3 position2 = this.m_cameraPositions[j];
								if (metaData.IsInsideSceneBounds(position2) || metaData.IsInsideScenePaddingBounds(position2, currentSceneBounds))
								{
									flag = true;
								}
							}
							if (flag && metaData.CanBeLoaded)
							{
								if (sceneManagerScene.CurrentState == SceneManagerScene.State.Disabled)
								{
									this.EnableDisabledScene(sceneManagerScene);
									if (limitOnce)
									{
										this.m_scenesToEnable.Clear();
										return;
									}
								}
								else
								{
									sceneManagerScene.CurrentState = SceneManagerScene.State.Loaded;
								}
							}
						}
						if ((sceneManagerScene.CurrentState == SceneManagerScene.State.Loaded || sceneManagerScene.CurrentState == SceneManagerScene.State.Disabling) && (sceneManagerScene.MetaData.IsInsideSceneBounds(position) || metaData.IsInsideScenePaddingBounds(position)))
						{
							for (int k = 0; k < sceneManagerScene.MetaData.IncludedScenes.Count; k++)
							{
								MoonGuid sceneGuid = sceneManagerScene.MetaData.IncludedScenes[k];
								RuntimeSceneMetaData runtimeSceneMetaData = this.FindRuntimeSceneMetaData(sceneGuid);
								if (runtimeSceneMetaData != null)
								{
									this.m_scenesToEnable.Add(runtimeSceneMetaData);
								}
							}
						}
					}
				}
			}
		}
		for (int l = 0; l < this.ActiveScenes.Count; l++)
		{
			SceneManagerScene sceneManagerScene2 = this.ActiveScenes[l];
			if (sceneManagerScene2.CurrentState == SceneManagerScene.State.Disabled)
			{
				RuntimeSceneMetaData metaData2 = sceneManagerScene2.MetaData;
				if (metaData2.DependantScene && this.m_scenesToEnable.Contains(sceneManagerScene2.MetaData))
				{
					this.EnableDisabledScene(sceneManagerScene2);
					if (limitOnce)
					{
						this.m_scenesToEnable.Clear();
						return;
					}
				}
			}
		}
		this.m_scenesToEnable.Clear();
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x0001C08C File Offset: 0x0001A28C
	private void EnableDisabledScene(SceneManagerScene scene)
	{
		scene.ChangeState(SceneManagerScene.State.Loaded);
		Events.Scheduler.OnSceneRootPreEnabled.Call(scene.SceneRoot);
		scene.PreventUnloading = false;
		scene.SceneRoot.EnableScene();
		if (!scene.HasStartBeenCalled)
		{
			scene.SceneRoot.EarlyStart();
		}
		LateStartHook.AddLateStartMethod(new Action(scene.SceneRoot.RegisterSceneRootEnabledAfterSerialize));
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0001C0F3 File Offset: 0x0001A2F3
	public void CheckForScenesFinishedLoading()
	{
		InstantLoadScenesController.Instance.OnScenesManagerFixedUpdate();
		GoToSceneController.Instance.OnScenesManagerFixedUpdate();
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x0001C10C File Offset: 0x0001A30C
	public void UnloadAllScenes()
	{
		foreach (SceneManagerScene sceneManagerScene in this.ActiveScenes.ToArray())
		{
			if (!sceneManagerScene.IsTitleScreen)
			{
				switch (sceneManagerScene.CurrentState)
				{
				case SceneManagerScene.State.Disabling:
					sceneManagerScene.SceneRoot.SaveAndUnload();
					this.RemoveScene(sceneManagerScene);
					break;
				case SceneManagerScene.State.Disabled:
					sceneManagerScene.SceneRoot.Unload();
					this.RemoveScene(sceneManagerScene);
					break;
				case SceneManagerScene.State.Loading:
					sceneManagerScene.ChangeState(SceneManagerScene.State.LoadingCancelled);
					this.CancelScene(sceneManagerScene);
					break;
				case SceneManagerScene.State.Loaded:
					sceneManagerScene.SceneRoot.SaveAndUnload();
					this.RemoveScene(sceneManagerScene);
					break;
				}
			}
		}
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0001C1CC File Offset: 0x0001A3CC
	private bool CancelScene(SceneManagerScene scene)
	{
		return false;
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x0001C1D0 File Offset: 0x0001A3D0
	public void AllowUnloadingOnAllScenes()
	{
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			sceneManagerScene.PreventUnloading = false;
			sceneManagerScene.KeepLoadedForCheckpoint = false;
		}
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x0001C214 File Offset: 0x0001A414
	public void AllowUnloadingOnScenes(Vector3 position)
	{
		Rect clampedRect = this.GetClampedRect(position);
		for (int i = 0; i < this.ActiveScenes.Count; i++)
		{
			SceneManagerScene sceneManagerScene = this.ActiveScenes[i];
			RuntimeSceneMetaData metaData = sceneManagerScene.MetaData;
			if (!metaData.DependantScene)
			{
				if (metaData.CanBeLoaded && (metaData.IsInsideSceneLoadingZone(clampedRect) || metaData.IsInsideSceneBounds(clampedRect) || metaData.IsInsideScenePaddingBounds(clampedRect)))
				{
					sceneManagerScene.PreventUnloading = false;
				}
			}
		}
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0001C2A0 File Offset: 0x0001A4A0
	public bool SceneIsLoaded(MoonGuid sceneGuid)
	{
		foreach (SceneManagerScene sceneManagerScene in this.ActiveScenes)
		{
			if (sceneManagerScene.MetaData.SceneMoonGuid == sceneGuid)
			{
				if (sceneManagerScene.CurrentState == SceneManagerScene.State.Loading || sceneManagerScene.CurrentState == SceneManagerScene.State.LoadingCancelled)
				{
					return false;
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x0001C334 File Offset: 0x0001A534
	public void OnFinishedStreamingInstall()
	{
		this.m_canBeStreamed.Clear();
	}

	// Token: 0x040004FD RID: 1277
	public float UnloadDelay = 1f;

	// Token: 0x040004FE RID: 1278
	public List<SceneManagerScene> ActiveScenes = new List<SceneManagerScene>();

	// Token: 0x040004FF RID: 1279
	public DestroyManager DestroyManager = new DestroyManager();

	// Token: 0x04000500 RID: 1280
	public bool AutoLoadingUnloading = true;

	// Token: 0x04000501 RID: 1281
	public MessageProvider FellOutOfWorldMessage;

	// Token: 0x04000502 RID: 1282
	public bool AllowDestroying;

	// Token: 0x04000503 RID: 1283
	public List<RuntimeSceneMetaData> AllScenes = new List<RuntimeSceneMetaData>();

	// Token: 0x04000504 RID: 1284
	private readonly List<Vector3> m_cameraPositions = new List<Vector3>();

	// Token: 0x04000505 RID: 1285
	private int m_resourcesNeedUnloading;

	// Token: 0x04000506 RID: 1286
	private readonly HashSet<RuntimeSceneMetaData> m_scenesToDisable = new HashSet<RuntimeSceneMetaData>();

	// Token: 0x04000507 RID: 1287
	private readonly HashSet<RuntimeSceneMetaData> m_scenesToInclude = new HashSet<RuntimeSceneMetaData>();

	// Token: 0x04000508 RID: 1288
	private readonly HashSet<RuntimeSceneMetaData> m_scenesToEnable = new HashSet<RuntimeSceneMetaData>();

	// Token: 0x04000509 RID: 1289
	private readonly Dictionary<string, bool> m_canBeStreamed = new Dictionary<string, bool>();

	// Token: 0x0400050A RID: 1290
	private readonly Dictionary<MoonGuid, RuntimeSceneMetaData> m_guidToRuntimeSceneMetaDatas = new Dictionary<MoonGuid, RuntimeSceneMetaData>();

	// Token: 0x0400050B RID: 1291
	public bool CanLoadScenes = true;

	// Token: 0x0400050C RID: 1292
	private AsyncOperation m_currentLoad;

	// Token: 0x0400050D RID: 1293
	private List<string> m_scenesToLoad = new List<string>();

	// Token: 0x0400050E RID: 1294
	private List<string> m_backgroundsToLoad = new List<string>();

	// Token: 0x0400050F RID: 1295
	private HashSet<MoonGuid> m_scenes = new HashSet<MoonGuid>();

	// Token: 0x04000510 RID: 1296
	private float m_testDelayTime;

	// Token: 0x04000511 RID: 1297
	public LayerMask RaycastMask;
}
