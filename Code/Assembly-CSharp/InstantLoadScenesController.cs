using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class InstantLoadScenesController : MonoBehaviour
{
	// Token: 0x06000DF0 RID: 3568 RVA: 0x000410E4 File Offset: 0x0003F2E4
	public void Awake()
	{
		InstantLoadScenesController.Instance = this;
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x000410EC File Offset: 0x0003F2EC
	public void OnDestroy()
	{
		if (InstantLoadScenesController.Instance == this)
		{
			InstantLoadScenesController.Instance = null;
		}
	}

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00041104 File Offset: 0x0003F304
	public ScenesManager ScenesManager
	{
		get
		{
			return Scenes.Manager;
		}
	}

	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0004110B File Offset: 0x0003F30B
	public bool IsLoading
	{
		get
		{
			return this.m_isLoading;
		}
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x00041113 File Offset: 0x0003F313
	public void FreezeIfLoadingScenes()
	{
		if (this.ScenesManager.IsLoadingScene(this.m_position))
		{
			this.FreezeIfNotFrozen();
		}
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x00041136 File Offset: 0x0003F336
	public void FreezeIfNotFrozen()
	{
		if (!this.m_entireGameFrozen)
		{
			SuspensionManager.SuspendAll();
			GameController.FreezeFixedUpdate = true;
			this.m_entireGameFrozen = true;
		}
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x00041155 File Offset: 0x0003F355
	public void UnfreezeIfFrozen()
	{
		if (this.m_entireGameFrozen)
		{
			SuspensionManager.ResumeAll();
			GameController.FreezeFixedUpdate = false;
			this.m_entireGameFrozen = false;
		}
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x00041174 File Offset: 0x0003F374
	public void LoadScenesAtPosition(Action action, bool async, bool pointSample = true)
	{
		if (!this.ScenesManager.IsInsideASceneBoundary(this.ScenesManager.CurrentCameraTargetPosition))
		{
			return;
		}
		this.m_onFinishedLoading = action;
		this.ScenesManager.AutoLoadingUnloading = false;
		this.ScenesManager.MarkLoadingScenesAsCancel();
		this.m_position = this.ScenesManager.CurrentCameraTargetPosition;
		Rect rect = default(Rect);
		Rect rect2 = rect;
		rect2.width = 48f;
		rect2.height = 48f;
		rect2.center = this.m_position;
		rect = rect2;
		Rect rect3;
		if (!pointSample && this.ScenesManager.GetSceneBoundaryAtPosition(rect.center, out rect3))
		{
			rect.xMin = Mathf.Max(rect.xMin, rect3.xMin + 0.1f);
			rect.yMin = Mathf.Max(rect.yMin, rect3.yMin + 0.1f);
			rect.xMax = Mathf.Min(rect.xMax, rect3.xMax - 0.1f);
			rect.yMax = Mathf.Min(rect.yMax, rect3.yMax - 0.1f);
			this.ScenesManager.AdditivelyLoadScenesInsideRect(rect, async, true, false);
		}
		else
		{
			this.ScenesManager.AdditivelyLoadScenesAtPosition(this.m_position, async, true, false);
		}
		if (!this.m_isLoading)
		{
			this.m_isLoading = true;
			this.m_startInstantlyLoadingScenesTime = Time.realtimeSinceStartup;
		}
		if (!async)
		{
			this.FreezeIfLoadingScenes();
		}
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x000412FC File Offset: 0x0003F4FC
	public void LogState()
	{
		foreach (SceneManagerScene sceneManagerScene in this.ScenesManager.ActiveScenes)
		{
			if (sceneManagerScene.MetaData == null)
			{
			}
		}
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x00041364 File Offset: 0x0003F564
	public void OnScenesManagerFixedUpdate()
	{
		if (this.m_isLoading)
		{
			if (this.ScenesManager.IsLoadingScene(this.m_position))
			{
				if (!this.ScenesManager.HasReportedScenesLoading && Time.realtimeSinceStartup > this.m_startInstantlyLoadingScenesTime + 10f)
				{
					this.ScenesManager.ReportScenesThatAreStillLoading();
				}
			}
			else
			{
				this.ScenesManager.HasReportedScenesLoading = false;
				if (this.LockFinishingLoading)
				{
					return;
				}
				this.FreezeIfNotFrozen();
				this.ScenesManager.EnableDisabledScenesAtPosition(false);
				GoToSceneController.Instance.OnScenesEnabled();
				if (this.OnScenesEnabledCallback != null)
				{
					this.OnScenesEnabledCallback();
					this.OnScenesEnabledCallback = null;
				}
				this.m_isLoading = false;
				LateStartHook.AddLateStartMethod(new Action(this.CompleteLoading));
			}
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00041436 File Offset: 0x0003F636
	// (set) Token: 0x06000DFB RID: 3579 RVA: 0x0004143E File Offset: 0x0003F63E
	public bool LockFinishingLoading { get; set; }

	// Token: 0x06000DFC RID: 3580 RVA: 0x00041448 File Offset: 0x0003F648
	public void CompleteLoading()
	{
		this.UnfreezeIfFrozen();
		UI.Cameras.Current.MoveCameraToTargetInstantly(false);
		this.ScenesManager.ClearCameraPuppetPositions();
		this.ScenesManager.UnloadScenesAtPosition(true);
		this.ScenesManager.AutoLoadingUnloading = true;
		if (this.m_onFinishedLoading != null)
		{
			this.m_onFinishedLoading();
			this.m_onFinishedLoading = null;
		}
		GoToSceneController.Instance.OnInstantLoadScenesControllerCompletedLoading();
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x000414B0 File Offset: 0x0003F6B0
	public void OnGameReset()
	{
		this.OnScenesEnabledCallback = null;
		this.LockFinishingLoading = false;
		if (this.m_isLoading)
		{
			this.m_isLoading = false;
			this.UnfreezeIfFrozen();
			this.m_onFinishedLoading = null;
		}
	}

	// Token: 0x04000B4F RID: 2895
	public static InstantLoadScenesController Instance;

	// Token: 0x04000B50 RID: 2896
	private Action m_onFinishedLoading;

	// Token: 0x04000B51 RID: 2897
	private bool m_isLoading;

	// Token: 0x04000B52 RID: 2898
	private float m_startInstantlyLoadingScenesTime;

	// Token: 0x04000B53 RID: 2899
	private bool m_entireGameFrozen;

	// Token: 0x04000B54 RID: 2900
	private Vector2 m_position;

	// Token: 0x04000B55 RID: 2901
	public Action OnScenesEnabledCallback;
}
