using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000180 RID: 384
public class PerformanceMonitor : MonoBehaviour
{
	// Token: 0x06000F13 RID: 3859 RVA: 0x00045318 File Offset: 0x00043518
	private void Awake()
	{
		PerformanceMonitor.Instance = this;
		this.FPSMonitor = base.gameObject.AddComponent<FPSMonitor>();
		this.FPSMonitor.Reset();
		Events.Scheduler.OnSceneRootLoadEarlyStart.Add(new Action<SceneRoot>(this.OnSceneRootLoadEarlyStart));
	}

	// Token: 0x06000F14 RID: 3860 RVA: 0x00045362 File Offset: 0x00043562
	private void Start()
	{
	}

	// Token: 0x06000F15 RID: 3861 RVA: 0x00045364 File Offset: 0x00043564
	private void FixedUpdate()
	{
		if (!PerformanceMonitor.Enabled)
		{
			return;
		}
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x00045374 File Offset: 0x00043574
	private void Update()
	{
		if (!PerformanceMonitor.Enabled)
		{
			return;
		}
		this.m_performanceDipTimeout -= Time.deltaTime;
		if (this.m_performanceDipTimeout > 0f)
		{
			PerformanceMonitor.OnNotMonitoringPerformance();
			return;
		}
		if (this.FPSMonitor.MinimumFPS > 0 && this.FPSMonitor.AverageFPS > 0 && (this.FPSMonitor.MinimumFPS < 50 || this.FPSMonitor.AverageFPS < 50))
		{
			this.m_performanceDipTimeout = 3f;
			FPTTestResult fpstestResult = this.GetFPSTestResult(null);
			this.TestResults.Add(fpstestResult);
		}
		else
		{
			PerformanceMonitor.OnGoodPerformance();
		}
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x00045430 File Offset: 0x00043630
	private void OnSceneRootLoadEarlyStart(SceneRoot sceneRoot)
	{
		FPTTestResult fpstestResult = this.GetFPSTestResult(Scenes.Manager.GetSceneManagerScene(sceneRoot.MetaData.SceneName));
		this.TestResults.Add(fpstestResult);
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x00045468 File Offset: 0x00043668
	private FPTTestResult GetFPSTestResult(SceneManagerScene scene = null)
	{
		SceneManagerScene sceneManagerScene2 = scene;
		if (sceneManagerScene2 == null)
		{
			sceneManagerScene2 = Scenes.Manager.CurrentSceneManagerScene;
		}
		FPTTestResult fpttestResult = new FPTTestResult();
		fpttestResult.ActiveScenes = Scenes.Manager.ActiveScenes.Count;
		fpttestResult.LoadedScenes = Scenes.Manager.ActiveScenes.Count((SceneManagerScene sceneManagerScene) => sceneManagerScene.CurrentState == SceneManagerScene.State.Loaded);
		fpttestResult.SceneLoadTime = sceneManagerScene2.LoadingTime;
		fpttestResult.SceneUnloadTime = 0f;
		fpttestResult.DateTime = this.m_testTime;
		fpttestResult.SceneName = sceneManagerScene2.MetaData.Scene;
		fpttestResult.SampleList = new List<FPSSampleData>();
		FPSSampleData item = new FPSSampleData();
		this.SetSampleData(ref item);
		fpttestResult.SampleList.Add(item);
		return fpttestResult;
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x00045530 File Offset: 0x00043730
	private void SetSampleData(ref FPSSampleData fpsSampleData)
	{
		switch (PerformanceTestManager.Instance.CurrentState)
		{
		case PerformanceTestManager.State.Start:
			fpsSampleData.AverageFPS = this.FPSMonitor.AverageFPS;
			fpsSampleData.MinimumFPS = this.FPSMonitor.MinimumFPS;
			break;
		case PerformanceTestManager.State.RegularTest:
			fpsSampleData.AverageFPS = this.FPSMonitor.AverageFPS;
			fpsSampleData.MinimumFPS = this.FPSMonitor.MinimumFPS;
			break;
		case PerformanceTestManager.State.QuadScale:
			fpsSampleData.CPUBAverageFPS = this.FPSMonitor.AverageFPS;
			fpsSampleData.CPUBMinimumFPS = this.FPSMonitor.MinimumFPS;
			break;
		case PerformanceTestManager.State.NoCamera:
			fpsSampleData.CPUAverageFPS = this.FPSMonitor.AverageFPS;
			fpsSampleData.CPUMinimumFPS = this.FPSMonitor.MinimumFPS;
			break;
		}
		fpsSampleData.AudioMemory = 0f;
		fpsSampleData.TextureMemory = 0f;
		fpsSampleData.TotalMemory = 0f;
		try
		{
			fpsSampleData.SampleID = SceneFPSTest.GetSampleID(UI.Cameras.Current.CameraCenterInGameplayPlane.x, UI.Cameras.Current.CameraCenterInGameplayPlane.y, Scenes.Manager.CurrentScene.Scene);
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x00045688 File Offset: 0x00043888
	private void DebugLog(string message)
	{
		if (!PerformanceMonitor.DebugMode)
		{
			return;
		}
	}

	// Token: 0x04000BF5 RID: 3061
	public static PerformanceMonitor Instance;

	// Token: 0x04000BF6 RID: 3062
	public static bool DebugMode = false;

	// Token: 0x04000BF7 RID: 3063
	private FPSMonitor FPSMonitor;

	// Token: 0x04000BF8 RID: 3064
	private FPTTestResult m_fpsTestResult;

	// Token: 0x04000BF9 RID: 3065
	private FPSSampleData m_fpsSampleData;

	// Token: 0x04000BFA RID: 3066
	private DateTime m_testTime = DateTime.Now;

	// Token: 0x04000BFB RID: 3067
	private float m_performanceDipTimeout;

	// Token: 0x04000BFC RID: 3068
	public static Action OnGoodPerformance = delegate()
	{
	};

	// Token: 0x04000BFD RID: 3069
	public static Action OnNotMonitoringPerformance = delegate()
	{
	};

	// Token: 0x04000BFE RID: 3070
	public static bool Enabled = false;

	// Token: 0x04000BFF RID: 3071
	public List<FPTTestResult> TestResults = new List<FPTTestResult>();
}
