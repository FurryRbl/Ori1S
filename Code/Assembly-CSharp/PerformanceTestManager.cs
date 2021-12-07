using System;
using System.IO;
using Game;
using UnityEngine;

// Token: 0x02000182 RID: 386
public class PerformanceTestManager : MonoBehaviour
{
	// Token: 0x06000F25 RID: 3877 RVA: 0x00045734 File Offset: 0x00043934
	public void ChangeState(PerformanceTestManager.State state)
	{
		this.DebugLog("ChangeState - " + state);
		this.m_stateCurrentTime = 0f;
		switch (this.CurrentState)
		{
		case PerformanceTestManager.State.Start:
			break;
		case PerformanceTestManager.State.RegularTest:
			break;
		case PerformanceTestManager.State.QuadScale:
			Shader.SetGlobalFloat("_GlobalDebugScale", 0f);
			break;
		case PerformanceTestManager.State.NoCamera:
			UI.Cameras.Current.Camera.enabled = true;
			UI.Cameras.System.GUICamera.Camera.enabled = true;
			break;
		case PerformanceTestManager.State.FinishedTest:
			break;
		case PerformanceTestManager.State.Done:
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
		this.CurrentState = state;
		switch (this.CurrentState)
		{
		case PerformanceTestManager.State.Start:
			break;
		case PerformanceTestManager.State.RegularTest:
			break;
		case PerformanceTestManager.State.QuadScale:
			Shader.SetGlobalFloat("_GlobalDebugScale", -1f);
			break;
		case PerformanceTestManager.State.NoCamera:
			UI.Cameras.Current.Camera.enabled = false;
			UI.Cameras.System.GUICamera.Camera.enabled = false;
			break;
		case PerformanceTestManager.State.FinishedTest:
			break;
		case PerformanceTestManager.State.Done:
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x00045870 File Offset: 0x00043A70
	private void Awake()
	{
		PerformanceTestManager.Instance = this;
		if (!File.Exists(Path.Combine(OutputFolder.BuildOutputPath, "replaytest.txt")))
		{
			this.ChangeState(PerformanceTestManager.State.Done);
			return;
		}
		this.ChangeState(PerformanceTestManager.State.RegularTest);
		this.m_fpsTestOutput = new FPSTestOutput("performanceReport.csv");
		Recorder instance = Recorder.Instance;
		instance.OnFinishedReplay = (Action)Delegate.Combine(instance.OnFinishedReplay, new Action(this.OnFinishedReplay));
		PerformanceMonitor.OnGoodPerformance = (Action)Delegate.Combine(PerformanceMonitor.OnGoodPerformance, new Action(this.OnGoodPerformance));
		PerformanceMonitor.OnNotMonitoringPerformance = (Action)Delegate.Combine(PerformanceMonitor.OnNotMonitoringPerformance, new Action(this.OnNotMonitoringPerformance));
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x00045925 File Offset: 0x00043B25
	private void Start()
	{
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x00045927 File Offset: 0x00043B27
	private void Update()
	{
		this.m_stateCurrentTime += Time.time;
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x0004593B File Offset: 0x00043B3B
	private void FixedUpdate()
	{
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x0004593D File Offset: 0x00043B3D
	public void OnApplicationQuit()
	{
		this.FlushAllPerformanceResults();
		if (this.m_fpsTestOutput != null)
		{
			this.m_fpsTestOutput.Close();
		}
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x0004595C File Offset: 0x00043B5C
	public bool ShouldRunReplayAgain()
	{
		switch (this.CurrentState)
		{
		case PerformanceTestManager.State.Start:
			break;
		case PerformanceTestManager.State.RegularTest:
			return true;
		case PerformanceTestManager.State.QuadScale:
			return true;
		case PerformanceTestManager.State.NoCamera:
			break;
		case PerformanceTestManager.State.FinishedTest:
			break;
		case PerformanceTestManager.State.Done:
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
		return false;
	}

	// Token: 0x06000F2C RID: 3884 RVA: 0x000459B4 File Offset: 0x00043BB4
	private void FlushAllPerformanceResults()
	{
		if (this.m_fpsTestOutput == null)
		{
			return;
		}
		foreach (FPTTestResult fptTestResult in PerformanceMonitor.Instance.TestResults)
		{
			this.m_fpsTestOutput.Write(fptTestResult);
		}
		PerformanceMonitor.Instance.TestResults.Clear();
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x00045A34 File Offset: 0x00043C34
	private void WriteSomePerformanceResults()
	{
		for (int i = 0; i < PerformanceMonitor.Instance.TestResults.Count; i++)
		{
			this.m_fpsTestOutput.Write(PerformanceMonitor.Instance.TestResults[0]);
			PerformanceMonitor.Instance.TestResults.RemoveAt(0);
		}
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x00045A8C File Offset: 0x00043C8C
	public void OnFinishedReplay()
	{
		this.DebugLog("OnFinishedReplay");
		switch (this.CurrentState)
		{
		case PerformanceTestManager.State.Start:
			break;
		case PerformanceTestManager.State.RegularTest:
			this.ChangeState(PerformanceTestManager.State.QuadScale);
			break;
		case PerformanceTestManager.State.QuadScale:
			this.ChangeState(PerformanceTestManager.State.NoCamera);
			break;
		case PerformanceTestManager.State.NoCamera:
			this.ChangeState(PerformanceTestManager.State.FinishedTest);
			break;
		case PerformanceTestManager.State.FinishedTest:
			break;
		case PerformanceTestManager.State.Done:
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x00045B07 File Offset: 0x00043D07
	public void OnGoodPerformance()
	{
		this.WriteSomePerformanceResults();
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x00045B0F File Offset: 0x00043D0F
	public void OnNotMonitoringPerformance()
	{
		this.WriteSomePerformanceResults();
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x00045B17 File Offset: 0x00043D17
	private void DebugLog(string message)
	{
		if (!PerformanceTestManager.DebugMode)
		{
			return;
		}
	}

	// Token: 0x04000C05 RID: 3077
	public static PerformanceTestManager Instance;

	// Token: 0x04000C06 RID: 3078
	public static bool DebugMode;

	// Token: 0x04000C07 RID: 3079
	private FPSTestOutput m_fpsTestOutput;

	// Token: 0x04000C08 RID: 3080
	private float m_stateCurrentTime;

	// Token: 0x04000C09 RID: 3081
	public PerformanceTestManager.State CurrentState;

	// Token: 0x0200074F RID: 1871
	public enum State
	{
		// Token: 0x04002794 RID: 10132
		Start,
		// Token: 0x04002795 RID: 10133
		RegularTest,
		// Token: 0x04002796 RID: 10134
		QuadScale,
		// Token: 0x04002797 RID: 10135
		NoCamera,
		// Token: 0x04002798 RID: 10136
		FinishedTest,
		// Token: 0x04002799 RID: 10137
		Done
	}
}
