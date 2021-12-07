using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000370 RID: 880
public class SceneFPSTest : MonoBehaviour
{
	// Token: 0x06001926 RID: 6438 RVA: 0x0006B63F File Offset: 0x0006983F
	public static string GetInputFilePath()
	{
		return Path.Combine(OutputFolder.BuildOutputPath, "CheckScenesFPS.txt");
	}

	// Token: 0x06001927 RID: 6439 RVA: 0x0006B650 File Offset: 0x00069850
	public static bool IsRunning()
	{
		return SceneFPSTest.Instance;
	}

	// Token: 0x06001928 RID: 6440 RVA: 0x0006B664 File Offset: 0x00069864
	public static void SetupTheTest()
	{
		using (StreamWriter streamWriter = new StreamWriter(new FileStream(Path.Combine(OutputFolder.BuildOutputPath, "CheckScenesFPS.txt"), FileMode.Create)))
		{
			streamWriter.WriteLine((!SceneFPSTest.SHOULD_CREATE_SCREENSHOT) ? "false" : "true");
			streamWriter.WriteLine((!SceneFPSTest.SHOULD_CREATE_MEMORY_REPORT) ? "false" : "true");
			streamWriter.WriteLine((!SceneFPSTest.SHOULD_RUN_SAMPLE) ? "false" : "true");
			streamWriter.WriteLine((!SceneFPSTest.SHOULD_RUN_CPU_SAMPLE) ? "false" : "true");
			streamWriter.WriteLine((!SceneFPSTest.SHOULD_RUN_CPU_B_SAMPLE) ? "false" : "true");
		}
		QualitySettings.vSyncCount = 0;
		SceneFPSTest.ReadSettingsAndStartTest();
	}

	// Token: 0x06001929 RID: 6441 RVA: 0x0006B754 File Offset: 0x00069954
	public static void ReadSettingsAndStartTest()
	{
		bool shouldCreateScreenshot = true;
		bool shouldCreateMemoryReport = true;
		bool shouldRunSample = true;
		bool shouldRunCPUSample = true;
		bool shouldRunCPUBSample = true;
		string inputFilePath = SceneFPSTest.GetInputFilePath();
		if (File.Exists(inputFilePath))
		{
			using (StreamReader streamReader = new StreamReader(new FileStream(inputFilePath, FileMode.Open)))
			{
				shouldCreateScreenshot = bool.Parse(streamReader.ReadLine());
				shouldCreateMemoryReport = bool.Parse(streamReader.ReadLine());
				shouldRunSample = bool.Parse(streamReader.ReadLine());
				shouldRunCPUSample = bool.Parse(streamReader.ReadLine());
				shouldRunCPUBSample = bool.Parse(streamReader.ReadLine());
			}
			File.Delete(inputFilePath);
		}
		SceneFPSTest sceneFPSTest = GameController.Instance.gameObject.AddComponent<SceneFPSTest>();
		sceneFPSTest.ShouldCreateScreenshot = shouldCreateScreenshot;
		sceneFPSTest.ShouldCreateMemoryReport = shouldCreateMemoryReport;
		sceneFPSTest.ShouldRunSample = shouldRunSample;
		sceneFPSTest.ShouldRunCPUSample = shouldRunCPUSample;
		sceneFPSTest.ShouldRunCPUBSample = shouldRunCPUBSample;
		SceneFPSTest.Instance = sceneFPSTest;
	}

	// Token: 0x0600192A RID: 6442 RVA: 0x0006B83C File Offset: 0x00069A3C
	private void Start()
	{
		foreach (string text in Environment.GetCommandLineArgs())
		{
			if (text.Contains("workspace="))
			{
				this.m_workspace = text.Substring(text.IndexOf("=") + 1);
			}
		}
		if (this.m_workspace == string.Empty)
		{
			this.m_workspace = OutputFolder.BuildOutputPath;
		}
		this.OutputFilePath = Path.Combine(this.m_workspace, "testReport.xml");
		this.m_testSuite.BeginOutputFile(this.OutputFilePath);
	}

	// Token: 0x0600192B RID: 6443 RVA: 0x0006B8D8 File Offset: 0x00069AD8
	private void FixedUpdate()
	{
		switch (this.CurrentState)
		{
		case SceneFPSTest.State.Idle:
			if (this.m_currentStateTime > 0.5f && this.FPSTestOutput == null)
			{
				this.FPSTestOutput = new FPSTestOutput(string.Empty);
				this.fpsMonitor = base.gameObject.AddComponent<FPSMonitor>();
				this.fpsMonitor.Reset();
				Events.Scheduler.OnSceneRootLoadEarlyStart.Add(new Action<SceneRoot>(this.OnSceneRootLoadEarlyStart));
				this.LoadEmptyLevel();
			}
			break;
		case SceneFPSTest.State.SceneLoading:
			if (!Scenes.Manager.IsLoadingScenes && !Scenes.Manager.DestroyManager.IsDestroying && this.m_currentStateTime > this.SceneLoadingGraceTime)
			{
				this.ChangeState(SceneFPSTest.State.EndLoadScene);
			}
			if (this.m_currentStateTime > 40f)
			{
				this.ChangeState(SceneFPSTest.State.StartUnloadScene);
			}
			break;
		case SceneFPSTest.State.StartTakeSample:
			if (!Scenes.Manager.IsLoadingScenes && !Scenes.Manager.DestroyManager.IsDestroying && this.m_currentStateTime > this.SampleSwitchingGraceTime)
			{
				this.ChangeState(SceneFPSTest.State.TakingSample);
			}
			break;
		case SceneFPSTest.State.TakingSample:
			if (this.m_currentStateTime > this.SampleDuration)
			{
				this.ChangeState(SceneFPSTest.State.EndTakeSample);
			}
			break;
		case SceneFPSTest.State.StartCPUSample:
			if (this.m_currentStateTime > this.SampleSwitchingGraceTime)
			{
				this.ChangeState(SceneFPSTest.State.TakingCPUSample);
			}
			break;
		case SceneFPSTest.State.TakingCPUSample:
			if (this.m_currentStateTime > this.CPUSampleDuration)
			{
				this.ChangeState(SceneFPSTest.State.EndCPUSample);
			}
			break;
		case SceneFPSTest.State.StartCPUBSample:
			if (this.m_currentStateTime > this.SampleSwitchingGraceTime)
			{
				this.ChangeState(SceneFPSTest.State.TakingCPUBSample);
			}
			break;
		case SceneFPSTest.State.TakingCPUBSample:
			if (this.m_currentStateTime > this.CPUBSampleDuration)
			{
				this.ChangeState(SceneFPSTest.State.EndCPUBSample);
			}
			break;
		case SceneFPSTest.State.UnloadingScene:
			if (!Scenes.Manager.IsLoadingScenes && !Scenes.Manager.DestroyManager.IsDestroying)
			{
				this.FPSTestResult.SceneUnloadTime = Time.realtimeSinceStartup - this.m_sceneUnladingStartTime;
				this.ChangeState(SceneFPSTest.State.EndUnloadScene);
			}
			break;
		}
		if (this.m_currentStateTime > 40f)
		{
			switch (this.CurrentState)
			{
			case SceneFPSTest.State.StartUnloadScene:
				this.ChangeState(SceneFPSTest.State.UnloadingScene);
				break;
			case SceneFPSTest.State.UnloadingScene:
				this.ChangeState(SceneFPSTest.State.EndUnloadScene);
				break;
			case SceneFPSTest.State.EndUnloadScene:
				this.ChangeState(SceneFPSTest.State.StartLoadScene);
				break;
			default:
				this.ChangeState(SceneFPSTest.State.Done);
				break;
			}
		}
		this.m_currentStateTime += Time.fixedDeltaTime;
	}

	// Token: 0x0600192C RID: 6444 RVA: 0x0006BBBC File Offset: 0x00069DBC
	private void ChangeState(SceneFPSTest.State state)
	{
		switch (this.CurrentState)
		{
		}
		this.m_currentStateTime = 0f;
		this.CurrentState = state;
		switch (this.CurrentState)
		{
		case SceneFPSTest.State.StartLoadScene:
			this.m_logCallbackHandler = new LogCallbackHandler();
			this.CurrentSampleIndex = 0;
			this.FPSTestResult = new FPTTestResult();
			this.FPSTestResult.DateTime = this.DateTime;
			this.AdvanceMetaDataIndexToNextGoodScene();
			this.m_sceneLoadingStartTime = Time.realtimeSinceStartup;
			this.ChangeState(SceneFPSTest.State.SceneLoading);
			this.LoadLevel(SceneFPSTest.CurrentSceneMetaDataIndex);
			if (SceneFPSTest.OVERRIDE_MISTYWOODS_CONDITION)
			{
				bool flag = false;
				RuntimeSceneMetaData metaData = this.GetMetaData(SceneFPSTest.CurrentSceneMetaDataIndex);
				if (metaData.LoadingCondition != null)
				{
					GetWorldEventCondition component = metaData.LoadingCondition.GetComponent<GetWorldEventCondition>();
					if (component != null && component.WorldEvents.name.ToLower().Contains("mistywoods"))
					{
						flag = true;
						this.m_mistyWoodsWorldEvents = component.WorldEvents;
						World.Events.Find(this.m_mistyWoodsWorldEvents).Value = component.States[0];
					}
				}
				if (!flag && this.m_mistyWoodsWorldEvents != null)
				{
					World.Events.Find(this.m_mistyWoodsWorldEvents).Value = this.m_mistyWoodsWorldEvents.DefaultValue;
					this.m_mistyWoodsWorldEvents = null;
				}
			}
			break;
		case SceneFPSTest.State.EndLoadScene:
			this.FPSTestResult.SceneName = this.GetMetaData(SceneFPSTest.CurrentSceneMetaDataIndex).Scene;
			if (this.SceneHasSamples(SceneFPSTest.CurrentSceneMetaDataIndex))
			{
				this.ChangeState(SceneFPSTest.State.StartTakeSample);
			}
			else
			{
				this.ChangeState(SceneFPSTest.State.StartUnloadScene);
			}
			break;
		case SceneFPSTest.State.StartTakeSample:
		{
			if (Characters.Sein)
			{
				Characters.Sein.Mortality.DamageReciever.IsImmortal = true;
				Characters.Sein.PlatformBehaviour.Gravity.Settings.GravityStrength = 0f;
				Characters.Sein.PlatformBehaviour.PlatformMovement.LocalSpeed = Vector2.zero;
				Characters.Sein.PlatformBehaviour.CapsuleController.CapsuleCollider.enabled = false;
			}
			this.FPSSampleData = new FPSSampleData();
			this.FPSSampleData.SampleID = SceneFPSTest.GetSampleID(SceneFPSTest.CurrentSceneMetaDataIndex, this.CurrentSampleIndex, this.GetMetaData(SceneFPSTest.CurrentSceneMetaDataIndex).Scene);
			if (!this.ShouldRunSample)
			{
				this.ChangeState(SceneFPSTest.State.StartCPUSample);
				return;
			}
			this.MoveToSamplePosition(SceneFPSTest.CurrentSceneMetaDataIndex, this.CurrentSampleIndex);
			string filename = Path.Combine(this.FPSTestOutput.GetOutputPath(), SceneFPSTest.GetSampleID(SceneFPSTest.CurrentSceneMetaDataIndex, this.CurrentSampleIndex, this.GetMetaData(SceneFPSTest.CurrentSceneMetaDataIndex).Scene) + ".png");
			if (this.ShouldCreateScreenshot)
			{
				Application.CaptureScreenshot(filename);
			}
			break;
		}
		case SceneFPSTest.State.TakingSample:
			this.fpsMonitor.Reset();
			break;
		case SceneFPSTest.State.EndTakeSample:
			this.FPSSampleData.AverageFPS = this.fpsMonitor.AverageFPS;
			this.FPSSampleData.MinimumFPS = this.fpsMonitor.MinimumFPS;
			YouCanLeaveYourHatOn.PrintReport(Path.Combine(this.FPSTestOutput.GetOutputPath(), this.GetMetaData(SceneFPSTest.CurrentSceneMetaDataIndex).Scene + this.CurrentSampleIndex + ".txt"));
			this.FPSSampleData.TextureMemory = 0f;
			this.FPSSampleData.AudioMemory = 0f;
			this.ChangeState(SceneFPSTest.State.StartCPUSample);
			break;
		case SceneFPSTest.State.StartCPUSample:
			if (!this.ShouldRunCPUSample)
			{
				this.ChangeState(SceneFPSTest.State.StartCPUBSample);
				return;
			}
			UI.Cameras.Current.Camera.enabled = false;
			UI.Cameras.System.GUICamera.Camera.enabled = false;
			break;
		case SceneFPSTest.State.TakingCPUSample:
			this.fpsMonitor.Reset();
			break;
		case SceneFPSTest.State.EndCPUSample:
			this.FPSSampleData.CPUAverageFPS = this.fpsMonitor.AverageFPS;
			this.FPSSampleData.CPUMinimumFPS = this.fpsMonitor.MinimumFPS;
			UI.Cameras.Current.Camera.enabled = true;
			UI.Cameras.System.GUICamera.Camera.enabled = true;
			this.ChangeState(SceneFPSTest.State.StartCPUBSample);
			break;
		case SceneFPSTest.State.StartCPUBSample:
			if (!this.ShouldRunCPUBSample)
			{
				this.HandleLastTestFinished();
				return;
			}
			Shader.SetGlobalFloat("_GlobalDebugScale", -1f);
			break;
		case SceneFPSTest.State.TakingCPUBSample:
			this.fpsMonitor.Reset();
			break;
		case SceneFPSTest.State.EndCPUBSample:
			this.FPSSampleData.CPUBAverageFPS = this.fpsMonitor.AverageFPS;
			this.FPSSampleData.CPUBMinimumFPS = this.fpsMonitor.MinimumFPS;
			Shader.SetGlobalFloat("_GlobalDebugScale", 0f);
			this.HandleLastTestFinished();
			break;
		case SceneFPSTest.State.StartUnloadScene:
			try
			{
				this.m_failure = null;
				bool flag2 = false;
				foreach (LogCallbackHandler.LogEntry logEntry in this.m_logCallbackHandler.LogEntries)
				{
					if (logEntry.LogType == LogType.Exception || logEntry.LogType == LogType.Assert || logEntry.LogType == LogType.Error)
					{
						flag2 = true;
						break;
					}
				}
				string text = string.Empty;
				if (flag2)
				{
					foreach (LogCallbackHandler.LogEntry logEntry2 in this.m_logCallbackHandler.LogEntries)
					{
						if (logEntry2.LogType == LogType.Exception || logEntry2.LogType == LogType.Assert || logEntry2.LogType == LogType.Error)
						{
							text += logEntry2.ToString();
							break;
						}
					}
				}
				if (flag2)
				{
					text = text.Replace("<", "[").Replace(">", "]");
					this.m_failure = new JUnitReporter.Failure(text, text, text);
				}
				this.m_logCallbackHandler.RemoveHandler();
				this.m_logCallbackHandler = null;
				this.m_testSuite.AddTestCase(this.GetMetaData(SceneFPSTest.CurrentSceneMetaDataIndex).Scene, "Test", this.m_failure, 0f);
				this.m_sceneUnladingStartTime = Time.realtimeSinceStartup;
			}
			finally
			{
				this.ChangeState(SceneFPSTest.State.UnloadingScene);
				this.LoadEmptyLevel();
			}
			break;
		case SceneFPSTest.State.EndUnloadScene:
			this.FPSTestOutput.Write(this.FPSTestResult);
			this.m_testSuite.WriteToFile(this.OutputFilePath);
			Resources.UnloadUnusedAssets();
			if (this.IsLastLevel(SceneFPSTest.CurrentSceneMetaDataIndex))
			{
				this.ChangeState(SceneFPSTest.State.Done);
			}
			else
			{
				this.ChangeState(SceneFPSTest.State.StartLoadScene);
			}
			break;
		case SceneFPSTest.State.Done:
			this.FPSTestOutput.Close();
			this.m_testSuite.WriteToFile(this.OutputFilePath);
			this.m_testSuite.FinalizeOutputFile(this.OutputFilePath);
			if (!Application.isEditor)
			{
				Application.Quit();
			}
			break;
		}
	}

	// Token: 0x0600192D RID: 6445 RVA: 0x0006C394 File Offset: 0x0006A594
	private void HandleLastTestFinished()
	{
		this.FPSTestResult.ActiveScenes = Scenes.Manager.ActiveScenes.Count;
		this.FPSTestResult.LoadedScenes = Scenes.Manager.ActiveScenes.Count((SceneManagerScene sceneManagerScene) => sceneManagerScene.CurrentState == SceneManagerScene.State.Loaded);
		this.FPSTestResult.SampleList.Add(this.FPSSampleData);
		if (this.isLastSample(this.CurrentSampleIndex))
		{
			this.ChangeState(SceneFPSTest.State.StartUnloadScene);
		}
		else
		{
			this.CurrentSampleIndex++;
			this.ChangeState(SceneFPSTest.State.StartTakeSample);
		}
	}

	// Token: 0x0600192E RID: 6446 RVA: 0x0006C43C File Offset: 0x0006A63C
	private void AdvanceMetaDataIndexToNextGoodScene()
	{
		SceneFPSTest.CurrentSceneMetaDataIndex++;
		if (this.IsLastLevel(SceneFPSTest.CurrentSceneMetaDataIndex))
		{
			this.ChangeState(SceneFPSTest.State.Done);
			return;
		}
		RuntimeSceneMetaData metaData = this.GetMetaData(SceneFPSTest.CurrentSceneMetaDataIndex);
		while (metaData.Scene == this.EmptyTestSceneName || ScenesToSkip.Instance.ShouldSkipScene(metaData.Scene) || this.IsCutscene(metaData.Scene))
		{
			if (this.IsLastLevel(SceneFPSTest.CurrentSceneMetaDataIndex))
			{
				this.ChangeState(SceneFPSTest.State.Done);
				return;
			}
			SceneFPSTest.CurrentSceneMetaDataIndex++;
			metaData = this.GetMetaData(SceneFPSTest.CurrentSceneMetaDataIndex);
		}
	}

	// Token: 0x0600192F RID: 6447 RVA: 0x0006C4EC File Offset: 0x0006A6EC
	private bool IsCutscene(string scene)
	{
		scene = scene.ToLower();
		bool flag = scene.StartsWith("kuroattack") || scene.StartsWith("kuronest") || scene.StartsWith("thesacrifice") || scene.StartsWith("outro") || scene.StartsWith("titlescreen") || scene.StartsWith("credits") || scene.StartsWith("gameend") || scene.StartsWith("intrologos") || scene.StartsWith("kuromoment") || scene.Contains("background") || scene.StartsWith("kuroattack");
		if (SceneFPSTest.HACK_REVERSE_ISCUTSCENE)
		{
			return !flag;
		}
		return flag;
	}

	// Token: 0x06001930 RID: 6448 RVA: 0x0006C5C0 File Offset: 0x0006A7C0
	private List<Vector3> GetSceneSamples(int metaDataIndex)
	{
		if (this.m_sceneSamplesCache.ContainsKey(metaDataIndex))
		{
			return this.m_sceneSamplesCache[metaDataIndex];
		}
		RuntimeSceneMetaData metaData = this.GetMetaData(metaDataIndex);
		return new List<Vector3>
		{
			new Vector3(Mathf.Lerp(metaData.SceneBounds.xMin, metaData.SceneBounds.xMax, 0.25f), Mathf.Lerp(metaData.SceneBounds.yMin, metaData.SceneBounds.yMax, 0.25f)),
			new Vector3(Mathf.Lerp(metaData.SceneBounds.xMin, metaData.SceneBounds.xMax, 0.25f), Mathf.Lerp(metaData.SceneBounds.yMin, metaData.SceneBounds.yMax, 0.75f)),
			new Vector3(Mathf.Lerp(metaData.SceneBounds.xMin, metaData.SceneBounds.xMax, 0.75f), Mathf.Lerp(metaData.SceneBounds.yMin, metaData.SceneBounds.yMax, 0.25f)),
			new Vector3(Mathf.Lerp(metaData.SceneBounds.xMin, metaData.SceneBounds.xMax, 0.75f), Mathf.Lerp(metaData.SceneBounds.yMin, metaData.SceneBounds.yMax, 0.75f))
		};
	}

	// Token: 0x06001931 RID: 6449 RVA: 0x0006C764 File Offset: 0x0006A964
	private RuntimeSceneMetaData GetMetaData(int metaDataIndex)
	{
		return Scenes.Manager.AllScenes[metaDataIndex];
	}

	// Token: 0x06001932 RID: 6450 RVA: 0x0006C776 File Offset: 0x0006A976
	private bool SceneHasSamples(int metaDataIndex)
	{
		return this.GetSceneSamples(metaDataIndex).Count != 0;
	}

	// Token: 0x06001933 RID: 6451 RVA: 0x0006C78A File Offset: 0x0006A98A
	private Vector3 GetSamplePosition(int metaDataIndex, int sampleIndex)
	{
		return this.GetSceneSamples(metaDataIndex)[sampleIndex];
	}

	// Token: 0x06001934 RID: 6452 RVA: 0x0006C799 File Offset: 0x0006A999
	private bool isLastSample(int sampleIndex)
	{
		return sampleIndex + 1 == this.GetSceneSamples(SceneFPSTest.CurrentSceneMetaDataIndex).Count;
	}

	// Token: 0x06001935 RID: 6453 RVA: 0x0006C7B0 File Offset: 0x0006A9B0
	private bool IsLastLevel(int metaDataIndex)
	{
		return metaDataIndex + 1 == Scenes.Manager.AllScenes.Count;
	}

	// Token: 0x06001936 RID: 6454 RVA: 0x0006C7C8 File Offset: 0x0006A9C8
	private void LoadLevel(int metaDataIndex)
	{
		Scenes.Manager.AllowUnloadingOnAllScenes();
		GoToSceneController.Instance.GoToSceneAsync(Scenes.Manager.AllScenes[metaDataIndex], null, true);
	}

	// Token: 0x06001937 RID: 6455 RVA: 0x0006C7FC File Offset: 0x0006A9FC
	private void LoadEmptyLevel()
	{
		Scenes.Manager.AllowUnloadingOnAllScenes();
		GoToSceneController.Instance.GoToSceneAsync(Scenes.Manager.GetSceneInformation(this.EmptyTestSceneName), null, true);
	}

	// Token: 0x06001938 RID: 6456 RVA: 0x0006C830 File Offset: 0x0006AA30
	private void MoveToSamplePosition(int metaDataIndex, int sampleIndex)
	{
		if (Characters.Sein)
		{
			Characters.Sein.PlatformBehaviour.PlatformMovement.Position = this.GetSamplePosition(metaDataIndex, sampleIndex);
		}
		if (Characters.Ori)
		{
			Characters.Ori.MoveOriToPlayer();
		}
		UI.Cameras.Current.MoveToTargetCharacter(0f);
	}

	// Token: 0x06001939 RID: 6457 RVA: 0x0006C890 File Offset: 0x0006AA90
	private float GetTexturesMBytes()
	{
		int num = 0;
		try
		{
			string path = Path.Combine(this.FPSTestOutput.GetOutputPath(), this.GetMetaData(SceneFPSTest.CurrentSceneMetaDataIndex).Scene + this.CurrentSampleIndex + ".csv");
			if (this.ShouldCreateMemoryReport)
			{
				StreamWriter streamWriter = null;
				if (!File.Exists(path))
				{
					streamWriter = new StreamWriter(new FileStream(path, FileMode.Create));
				}
				UnityEngine.Object[] array = Resources.FindObjectsOfTypeAll(typeof(Texture));
				foreach (Texture texture in array)
				{
					int runtimeMemorySize = Profiler.GetRuntimeMemorySize(texture);
					num += runtimeMemorySize;
					if (streamWriter != null)
					{
						streamWriter.WriteLine(texture.name + "," + (float)runtimeMemorySize / 1024f);
					}
				}
				if (streamWriter != null)
				{
					((IDisposable)streamWriter).Dispose();
				}
			}
			num /= 1024;
			num /= 1024;
		}
		catch (Exception ex)
		{
		}
		return (float)num;
	}

	// Token: 0x0600193A RID: 6458 RVA: 0x0006C9A8 File Offset: 0x0006ABA8
	private float GetAudioMBytes()
	{
		UnityEngine.Object[] array = Resources.FindObjectsOfTypeAll(typeof(AudioClip));
		float num = 0f;
		foreach (AudioClip o in array)
		{
			num += (float)Profiler.GetRuntimeMemorySize(o);
		}
		num /= 1024f;
		return num / 1024f;
	}

	// Token: 0x0600193B RID: 6459 RVA: 0x0006CA0C File Offset: 0x0006AC0C
	private Texture2D GetBackgroundTexture()
	{
		if (this.m_backgroundTexture == null)
		{
			this.m_backgroundTexture = new Texture2D(2, 2);
			this.m_backgroundTexture.SetPixels(new Color[]
			{
				Color.grey,
				Color.grey,
				Color.grey,
				Color.grey
			});
			this.m_backgroundTexture.Apply();
			this.m_backgroundTexture.hideFlags = HideFlags.HideAndDontSave;
		}
		return this.m_backgroundTexture;
	}

	// Token: 0x0600193C RID: 6460 RVA: 0x0006CAAC File Offset: 0x0006ACAC
	private void OnWindowUi(int windowId)
	{
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		GUILayout.Label("CurrentState: " + this.CurrentState, new GUILayoutOption[0]);
		GUILayout.Label("CurrentSceneMetaDataIndex: " + SceneFPSTest.CurrentSceneMetaDataIndex, new GUILayoutOption[0]);
		GUILayout.Label("MetaData.Scene: " + this.GetMetaData(SceneFPSTest.CurrentSceneMetaDataIndex).Scene, new GUILayoutOption[0]);
		GUILayout.Label("CurrentSampleIndex: " + this.CurrentSampleIndex, new GUILayoutOption[0]);
		GUILayout.Label("Current state time: " + this.m_currentStateTime, new GUILayoutOption[0]);
		GUILayout.EndVertical();
	}

	// Token: 0x0600193D RID: 6461 RVA: 0x0006CB70 File Offset: 0x0006AD70
	public void OnSceneRootLoadEarlyStart(SceneRoot sceneRoot)
	{
		if (this.CurrentState == SceneFPSTest.State.Idle)
		{
			if (sceneRoot.name == this.EmptyTestSceneName)
			{
				this.ChangeState(SceneFPSTest.State.StartLoadScene);
			}
		}
		else if (this.CurrentState == SceneFPSTest.State.SceneLoading && sceneRoot.name == this.GetMetaData(SceneFPSTest.CurrentSceneMetaDataIndex).Scene)
		{
			this.FPSTestResult.SceneLoadTime = Time.realtimeSinceStartup - this.m_sceneLoadingStartTime;
		}
	}

	// Token: 0x0600193E RID: 6462 RVA: 0x0006CBED File Offset: 0x0006ADED
	public static string GetSampleID(float xPosition, float yPosition, string sceneName)
	{
		return sceneName + Mathf.RoundToInt(Mathf.Abs(xPosition * 1000f)) + Mathf.RoundToInt(Mathf.Abs(yPosition * 1000f));
	}

	// Token: 0x0600193F RID: 6463 RVA: 0x0006CC24 File Offset: 0x0006AE24
	public static string GetSampleID(int metaDataIndex, int sampleIndex, string sceneName)
	{
		return SceneFPSTest.GetSampleID(SceneFPSTest.Instance.GetSamplePosition(metaDataIndex, sampleIndex).x * 1000f, SceneFPSTest.Instance.GetSamplePosition(metaDataIndex, sampleIndex).y * 1000f, sceneName);
	}

	// Token: 0x0400158B RID: 5515
	public const string FPS_TEST_INPUT_FILE_NAME = "CheckScenesFPS.txt";

	// Token: 0x0400158C RID: 5516
	public static SceneFPSTest Instance;

	// Token: 0x0400158D RID: 5517
	private SceneFPSTest.State CurrentState;

	// Token: 0x0400158E RID: 5518
	private float m_currentStateTime;

	// Token: 0x0400158F RID: 5519
	public static int CurrentSceneMetaDataIndex;

	// Token: 0x04001590 RID: 5520
	private int CurrentSampleIndex;

	// Token: 0x04001591 RID: 5521
	private FPSMonitor fpsMonitor;

	// Token: 0x04001592 RID: 5522
	private float m_sceneLoadingStartTime;

	// Token: 0x04001593 RID: 5523
	private float m_sceneUnladingStartTime;

	// Token: 0x04001594 RID: 5524
	private float SceneLoadingGraceTime = 2f;

	// Token: 0x04001595 RID: 5525
	private float SampleSwitchingGraceTime = 2f;

	// Token: 0x04001596 RID: 5526
	private float SampleDuration = 2f;

	// Token: 0x04001597 RID: 5527
	private float CPUSampleDuration = 2f;

	// Token: 0x04001598 RID: 5528
	private float CPUBSampleDuration = 2f;

	// Token: 0x04001599 RID: 5529
	private string EmptyTestSceneName = "emptyTestScene";

	// Token: 0x0400159A RID: 5530
	private FPSTestOutput FPSTestOutput;

	// Token: 0x0400159B RID: 5531
	private FPTTestResult FPSTestResult;

	// Token: 0x0400159C RID: 5532
	private FPSSampleData FPSSampleData;

	// Token: 0x0400159D RID: 5533
	private DateTime DateTime = DateTime.Now;

	// Token: 0x0400159E RID: 5534
	private string m_workspace = string.Empty;

	// Token: 0x0400159F RID: 5535
	private LogCallbackHandler m_logCallbackHandler;

	// Token: 0x040015A0 RID: 5536
	private JUnitReporter.TestSuite m_testSuite = new JUnitReporter.TestSuite();

	// Token: 0x040015A1 RID: 5537
	private JUnitReporter.Failure m_failure;

	// Token: 0x040015A2 RID: 5538
	public bool ShouldCreateScreenshot = true;

	// Token: 0x040015A3 RID: 5539
	public bool ShouldCreateMemoryReport = true;

	// Token: 0x040015A4 RID: 5540
	public bool ShouldRunSample = true;

	// Token: 0x040015A5 RID: 5541
	public bool ShouldRunCPUSample = true;

	// Token: 0x040015A6 RID: 5542
	public bool ShouldRunCPUBSample = true;

	// Token: 0x040015A7 RID: 5543
	public string OutputFilePath = string.Empty;

	// Token: 0x040015A8 RID: 5544
	public static bool SHOULD_CREATE_SCREENSHOT = true;

	// Token: 0x040015A9 RID: 5545
	public static bool SHOULD_CREATE_MEMORY_REPORT;

	// Token: 0x040015AA RID: 5546
	public static bool SHOULD_RUN_SAMPLE = true;

	// Token: 0x040015AB RID: 5547
	public static bool SHOULD_RUN_CPU_SAMPLE;

	// Token: 0x040015AC RID: 5548
	public static bool SHOULD_RUN_CPU_B_SAMPLE;

	// Token: 0x040015AD RID: 5549
	public static bool OVERRIDE_MISTYWOODS_CONDITION = true;

	// Token: 0x040015AE RID: 5550
	private WorldEvents m_mistyWoodsWorldEvents;

	// Token: 0x040015AF RID: 5551
	public static bool HACK_REVERSE_ISCUTSCENE;

	// Token: 0x040015B0 RID: 5552
	private Dictionary<int, List<Vector3>> m_sceneSamplesCache = new Dictionary<int, List<Vector3>>();

	// Token: 0x040015B1 RID: 5553
	private Texture2D m_backgroundTexture;

	// Token: 0x040015B2 RID: 5554
	public static bool DRAW_DEBUG_UI;

	// Token: 0x0200075F RID: 1887
	public enum State
	{
		// Token: 0x040027D1 RID: 10193
		Idle,
		// Token: 0x040027D2 RID: 10194
		StartLoadScene,
		// Token: 0x040027D3 RID: 10195
		SceneLoading,
		// Token: 0x040027D4 RID: 10196
		EndLoadScene,
		// Token: 0x040027D5 RID: 10197
		StartTakeSample,
		// Token: 0x040027D6 RID: 10198
		TakingSample,
		// Token: 0x040027D7 RID: 10199
		EndTakeSample,
		// Token: 0x040027D8 RID: 10200
		StartCPUSample,
		// Token: 0x040027D9 RID: 10201
		TakingCPUSample,
		// Token: 0x040027DA RID: 10202
		EndCPUSample,
		// Token: 0x040027DB RID: 10203
		StartCPUBSample,
		// Token: 0x040027DC RID: 10204
		TakingCPUBSample,
		// Token: 0x040027DD RID: 10205
		EndCPUBSample,
		// Token: 0x040027DE RID: 10206
		StartUnloadScene,
		// Token: 0x040027DF RID: 10207
		UnloadingScene,
		// Token: 0x040027E0 RID: 10208
		EndUnloadScene,
		// Token: 0x040027E1 RID: 10209
		Done
	}
}
