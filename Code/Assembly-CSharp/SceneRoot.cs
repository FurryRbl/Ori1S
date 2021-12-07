using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000099 RID: 153
[ExecuteInEditMode]
public class SceneRoot : MonoBehaviour
{
	// Token: 0x06000635 RID: 1589 RVA: 0x000183D0 File Offset: 0x000165D0
	public void OnValidate()
	{
		List<Component> list = new List<Component>(base.gameObject.FindComponentsInChildren<ISceneRootPreEnableObserver>());
		list.RemoveAll((Component a) => a.GetComponentInChildren<Respawner>() != null);
		this.m_sceneRootPreEnabledObservers = list.ToArray();
	}

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001841E File Offset: 0x0001661E
	public SceneSettingsComponent SceneSettings
	{
		get
		{
			if (this.m_sceneSettings == null)
			{
				this.m_sceneSettings = base.GetComponent<SceneSettingsComponent>();
			}
			return this.m_sceneSettings;
		}
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x00018443 File Offset: 0x00016643
	public static SceneRoot FindFromTransform(Transform transform)
	{
		return transform.root.GetComponent<SceneRoot>();
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x00018450 File Offset: 0x00016650
	public static SceneRoot FindFromPosition(Vector3 position)
	{
		foreach (SceneRoot sceneRoot in UnityEngine.Object.FindObjectsOfType<SceneRoot>())
		{
			if (sceneRoot.MetaData.SceneBounds.Contains(position))
			{
				return sceneRoot;
			}
		}
		throw new Exception("Sein is outside scene root");
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x000184A0 File Offset: 0x000166A0
	public void EditorAwake()
	{
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x000184A4 File Offset: 0x000166A4
	public void Awake()
	{
		if (!Application.isPlaying)
		{
			this.EditorAwake();
			return;
		}
		if (this.SaveSceneManager && (this.SaveSceneManager.SaveData.Count == 0 || Application.isEditor))
		{
			this.SaveSceneManager.AddChildSaveSerializables();
		}
		if (Scenes.Manager)
		{
			Scenes.Manager.Register(this);
		}
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x00018516 File Offset: 0x00016716
	public void EarlyStart()
	{
		if (Application.isPlaying)
		{
			Events.Scheduler.OnSceneRootLoadEarlyStart.Call(this);
			LateStartHook.AddLateStartMethod(new Action(this.LateStart));
		}
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x00018544 File Offset: 0x00016744
	public void LateStart()
	{
		Events.Scheduler.OnSceneStartLateBeforeSerialize.Call(this);
		if (this.SaveSceneManager)
		{
			if (Game.Checkpoint.SaveGameData.PendingScenes.ContainsKey(this.MetaData.SceneMoonGuid))
			{
				this.SaveSceneManager.Load(Game.Checkpoint.SaveGameData.PendingScenes[this.MetaData.SceneMoonGuid]);
			}
			else if (Game.Checkpoint.SaveGameData.SceneExists(this.MetaData.SceneMoonGuid))
			{
				this.SaveSceneManager.Load(Game.Checkpoint.SaveGameData.InsertScene(this.MetaData.SceneMoonGuid));
			}
			else
			{
				this.SaveSceneManager.Save(Game.Checkpoint.SaveGameData.InsertScene(this.MetaData.SceneMoonGuid));
			}
		}
		Events.Scheduler.OnSceneStartLateAfterSerialize.Call(this);
		Scenes.Manager.OnSceneStartCompleted(this);
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x00018634 File Offset: 0x00016834
	public void Save()
	{
		if (this.SaveSceneManager)
		{
			this.SaveSceneManager.Save(Game.Checkpoint.SaveGameData.InsertPendingScene(this.MetaData.SceneMoonGuid));
		}
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x00018671 File Offset: 0x00016871
	public void SaveAndUnload()
	{
		this.Save();
		this.Unload();
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0001867F File Offset: 0x0001687F
	public void Unload()
	{
		Scenes.Manager.DestroyManager.DestroyGameObjectAsync(base.gameObject);
		Scenes.Manager.DestroyManager.DestroyResourcesAsync(this.SceneResources);
		SceneFrameworkPerformanceMonitor.UnloadScene(this);
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x000186B1 File Offset: 0x000168B1
	public void DisableScene()
	{
		base.gameObject.SetActive(false);
		SceneFrameworkPerformanceMonitor.DisableScene(this);
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x000186C8 File Offset: 0x000168C8
	public void EnableScene()
	{
		if (this.m_sceneRootPreEnabledObservers != null)
		{
			for (int i = 0; i < this.m_sceneRootPreEnabledObservers.Length; i++)
			{
				Component component = this.m_sceneRootPreEnabledObservers[i];
				if (component != null)
				{
					ISceneRootPreEnableObserver sceneRootPreEnableObserver = (ISceneRootPreEnableObserver)component;
					sceneRootPreEnableObserver.OnSceneRootPreEnable();
				}
			}
		}
		base.gameObject.SetActive(true);
		SceneFrameworkPerformanceMonitor.EnableScene(this);
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0001872D File Offset: 0x0001692D
	public void RegisterSceneRootEnabledAfterSerialize()
	{
		Events.Scheduler.OnSceneRootEnabledAfterSerialize.Call(this);
	}

	// Token: 0x040004C7 RID: 1223
	public SaveSceneManager SaveSceneManager;

	// Token: 0x040004C8 RID: 1224
	public SceneMetaData MetaData;

	// Token: 0x040004C9 RID: 1225
	public List<UnityEngine.Object> SceneResources = new List<UnityEngine.Object>();

	// Token: 0x040004CA RID: 1226
	public SceneRootData SceneRootData = new SceneRootData();

	// Token: 0x040004CB RID: 1227
	[HideInInspector]
	[SerializeField]
	public Component[] m_sceneRootPreEnabledObservers;

	// Token: 0x040004CC RID: 1228
	private Vector3 m_previousPosition;

	// Token: 0x040004CD RID: 1229
	private SceneSettingsComponent m_sceneSettings;

	// Token: 0x040004CE RID: 1230
	public static bool ShouldDrawWorldMapGizmos = true;
}
