using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Game;
using UnityEngine;

// Token: 0x02000171 RID: 369
public class Recorder : MonoBehaviour
{
	// Token: 0x06000E88 RID: 3720 RVA: 0x00042CB1 File Offset: 0x00040EB1
	public static void AddReplayToQueue(string replayPath, int index = -1)
	{
		if (index == -1)
		{
			Recorder.ReplaysQueue.Add(replayPath);
		}
		else
		{
			Recorder.ReplaysQueue.Insert(index, replayPath);
		}
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x00042CD6 File Offset: 0x00040ED6
	public static bool IsReplayQueueEmpty()
	{
		return Recorder.ReplaysQueue.Count == 0;
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x00042CE8 File Offset: 0x00040EE8
	public static string GetReplayFromQueue()
	{
		if (Recorder.IsReplayQueueEmpty())
		{
			return string.Empty;
		}
		string result = Recorder.ReplaysQueue[0];
		Recorder.ReplaysQueue.RemoveAt(0);
		return result;
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00042D20 File Offset: 0x00040F20
	public static bool IsPlaying
	{
		get
		{
			return Recorder.Instance && Recorder.Instance.State == Recorder.RecorderState.Playing;
		}
	}

	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x06000E8C RID: 3724 RVA: 0x00042D51 File Offset: 0x00040F51
	public static bool IsRecordering
	{
		get
		{
			return Recorder.Instance && Recorder.Instance.State == Recorder.RecorderState.Recording;
		}
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x00042D74 File Offset: 0x00040F74
	public bool OnPerformLoad()
	{
		if (this.State == Recorder.RecorderState.Recording)
		{
			if (!this.Active)
			{
				return true;
			}
			base.GetComponent<CheckpointPlugin>().MakeCheckpointAtEndOfFrame();
		}
		return this.State != Recorder.RecorderState.Playing || base.GetComponent<CheckpointPlugin>().OnGameLoad();
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x00042DBD File Offset: 0x00040FBD
	public void OnPerformSave()
	{
		base.GetComponent<CheckpointPlugin>().OnGameLoad();
	}

	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00042DCB File Offset: 0x00040FCB
	public float PositionTolerance
	{
		get
		{
			return (!this.Strict) ? 0.1f : 0f;
		}
	}

	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x06000E90 RID: 3728 RVA: 0x00042DE7 File Offset: 0x00040FE7
	public float RotationTolerance
	{
		get
		{
			return (!this.Strict) ? 1f : 0f;
		}
	}

	// Token: 0x170002BA RID: 698
	// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00042E03 File Offset: 0x00041003
	public RecorderFrame CurrentFrame
	{
		get
		{
			return this.RecorderData.GetFrame(this.CurrentFrameIndex);
		}
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x00042E18 File Offset: 0x00041018
	public void OnSceneRootActivatedScene(SceneRoot sceneRoot)
	{
		if (this.State == Recorder.RecorderState.Recording && this.Active)
		{
			CheckpointPlugin component = base.GetComponent<CheckpointPlugin>();
			if (component)
			{
				component.MakeCheckpointAtEndOfFrame();
			}
		}
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x00042E54 File Offset: 0x00041054
	public void OnRestoreCheckpoint()
	{
		if (this.State == Recorder.RecorderState.Recording && this.Active)
		{
			CheckpointPlugin component = base.GetComponent<CheckpointPlugin>();
			if (component)
			{
				component.MakeCheckpointAtEndOfFrame();
			}
		}
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x00042E90 File Offset: 0x00041090
	public void Reset(string fileName = "")
	{
		string currentReplayPath = this.CurrentReplayPath;
		this.InitRecording();
		GameController.Instance.CreateCheckpoint();
		GameController.Instance.RestoreCheckpoint(null);
		try
		{
			if (fileName != string.Empty && File.Exists(currentReplayPath))
			{
				string text = Path.Combine(Path.GetDirectoryName(currentReplayPath).Replace("temporaryReplays", "specialReplays"), fileName + ".replay");
				if (!File.Exists(text))
				{
					File.Move(currentReplayPath, text);
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x00042F30 File Offset: 0x00041130
	public void InitRecording()
	{
		this.RecorderData = new RecorderData();
		this.CurrentFrameIndex = -1;
		this.CurrentReplayPath = Path.Combine(OutputFolder.BuildOutputPath, "temporaryReplays");
		this.CurrentReplayPath = Path.Combine(this.CurrentReplayPath, Environment.MachineName + "_" + DateTime.Now.ToString().Replace('/', '-').Replace(' ', '_').Replace(':', '-') + ".replay");
		if (this.RecorderStream != null)
		{
			this.FinishFrame();
			((IDisposable)this.RecorderStream).Dispose();
			this.RecorderStream = null;
		}
		this.RecorderStream = new BinaryWriter(File.Open(this.CurrentReplayPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite));
		this.RecorderStream.Write(this.RecorderData.FILE_FORMAT_IDENTIFIER);
		this.RecorderStream.Write(2);
		this.RecorderData.CurrentReplayPath = this.CurrentReplayPath;
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x00043020 File Offset: 0x00041220
	public void OnGameReset()
	{
		try
		{
			this.DestroyComponent(base.gameObject.GetComponent<CheckpointPlugin>());
			this.DestroyComponent(base.gameObject.GetComponent<InputPlugin>());
			this.DestroyComponent(base.gameObject.GetComponent<CharacterPlugin>());
			this.DestroyComponent(base.gameObject.GetComponent<CameraPlugin>());
			this.DestroyComponent(base.gameObject.GetComponent<RecorderMessagePlugin>());
			this.DestroyComponent(base.gameObject.GetComponent<DeathsPlugin>());
			this.AddRecordingComponents();
			this.InitRecording();
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x000430BC File Offset: 0x000412BC
	private void DestroyComponent(Component component)
	{
		if (component)
		{
			UnityEngine.Object.DestroyObject(component);
		}
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x000430D0 File Offset: 0x000412D0
	private void AddRecordingComponents()
	{
		base.gameObject.AddComponent<CheckpointPlugin>();
		base.gameObject.AddComponent<InputPlugin>();
		base.gameObject.AddComponent<CharacterPlugin>();
		base.gameObject.AddComponent<CameraPlugin>();
		base.gameObject.AddComponent<RecorderMessagePlugin>();
		base.gameObject.AddComponent<DeathsPlugin>();
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x00043128 File Offset: 0x00041328
	private void AddPlaybackComponents()
	{
		base.gameObject.AddComponent<CheckpointPlugin>();
		base.gameObject.AddComponent<InputPlugin>();
		base.gameObject.AddComponent<FPSPlugin>();
		base.gameObject.AddComponent<RecorderMessagePlugin>();
		if (TestSetManager.IsPerformingTests)
		{
			base.gameObject.AddComponent<TesterPlugin>();
		}
		else
		{
			base.gameObject.AddComponent<PositionCheckerPlugin>();
		}
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x0004318C File Offset: 0x0004138C
	public void DestoryPlaybackComponents()
	{
		this.DestroyComponent(base.gameObject.GetComponent<CheckpointPlugin>());
		this.DestroyComponent(base.gameObject.GetComponent<InputPlugin>());
		this.DestroyComponent(base.gameObject.GetComponent<FPSPlugin>());
		this.DestroyComponent(base.gameObject.GetComponent<RecorderMessagePlugin>());
		this.DestroyComponent(base.gameObject.GetComponent<TesterPlugin>());
		this.DestroyComponent(base.gameObject.GetComponent<PositionCheckerPlugin>());
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x00043200 File Offset: 0x00041400
	public void Awake()
	{
		Events.Scheduler.OnSceneStartLateAfterSerialize.Add(new Action<SceneRoot>(this.OnSceneRootActivatedScene));
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x00043260 File Offset: 0x00041460
	public void Start()
	{
		if (this.State == Recorder.RecorderState.Playing)
		{
			base.gameObject.GetComponent<CheckpointPlugin>().PerformLoadFromFrame(0);
			base.gameObject.GetComponent<CheckpointPlugin>().PerformLoadFromFrame(1);
		}
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x0004329C File Offset: 0x0004149C
	public IEnumerator ResetRecorderAndComponents()
	{
		this.DestoryPlaybackComponents();
		if (RecorderPlaybackUITimeline.Instance != null)
		{
			InstantiateUtility.Destroy(RecorderPlaybackUITimeline.Instance.gameObject);
		}
		yield return new WaitForFixedUpdate();
		this.SetupRecorderAccordingToState();
		yield break;
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x000432B8 File Offset: 0x000414B8
	public void SetupRecorderAccordingToState()
	{
		Recorder.RecorderState state = this.State;
		if (state != Recorder.RecorderState.Recording)
		{
			if (state == Recorder.RecorderState.Playing)
			{
				PerformanceMonitor.Enabled = true;
				PlayerInput.Instance.Active = false;
				this.AddPlaybackComponents();
				GameObject original = (GameObject)Resources.Load("recorderPlaybackUI", typeof(GameObject));
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
				gameObject.GetComponent<RecorderPlaybackUITimeline>().Recorder = this;
			}
		}
		else
		{
			this.InitRecording();
			this.AddRecordingComponents();
		}
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x00043338 File Offset: 0x00041538
	public void FinishFrame()
	{
		this.RecorderStream.Write(0);
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x00043348 File Offset: 0x00041548
	public void FixedUpdate()
	{
		if (!this.Active)
		{
			return;
		}
		if (GameController.FreezeFixedUpdate)
		{
			return;
		}
		this.CurrentFrameIndex++;
		Recorder.RecorderState state = this.State;
		if (state != Recorder.RecorderState.Recording)
		{
			if (state == Recorder.RecorderState.Playing)
			{
				if (this.RecorderData.Frames.Count == 0)
				{
					return;
				}
				if (this.CurrentFrameIndex == this.RecorderData.Frames.Count)
				{
					if (Frapser.IsFrapserActive())
					{
						Frapser.StopFrapser();
						this.OnFinishedReplay();
						this.State = Recorder.RecorderState.Idle;
						return;
					}
					if (PerformanceTestManager.Instance.ShouldRunReplayAgain())
					{
						Recorder.AddReplayToQueue(this.CurrentReplayPath, 0);
					}
					if (!Recorder.IsReplayQueueEmpty())
					{
						this.CurrentReplayPath = Recorder.GetReplayFromQueue();
						this.RecorderData.LoadFromFile(this.CurrentReplayPath);
						this.State = Recorder.RecorderState.Playing;
						base.StartCoroutine(this.ResetRecorderAndComponents());
						RecorderPlaybackUI.Instance.JumpToFrame(0);
						this.OnFinishedReplay();
						return;
					}
					this.OnFinishedReplay();
					this.State = Recorder.RecorderState.Idle;
					return;
				}
				else
				{
					if (this.CurrentFrame == null)
					{
						return;
					}
					for (int i = 0; i < this.m_plugins.Count; i++)
					{
						IRecorderPlugin recorderPlugin = this.m_plugins[i];
						recorderPlugin.PlayCycle(this.CurrentFrameIndex);
					}
				}
			}
		}
		else
		{
			if (this.CurrentFrameIndex == 0)
			{
				BuildData.Record(this.RecorderStream);
			}
			else
			{
				this.FinishFrame();
			}
			for (int j = 0; j < this.m_plugins.Count; j++)
			{
				IRecorderPlugin recorderPlugin2 = this.m_plugins[j];
				recorderPlugin2.RecordCycle(this.CurrentFrameIndex);
			}
		}
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x00043509 File Offset: 0x00041709
	public void RegisterPlugin(IRecorderPlugin plugin)
	{
		this.m_plugins.Add(plugin);
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x00043517 File Offset: 0x00041717
	public void DeregisterPlugin(IRecorderPlugin plugin)
	{
		this.m_plugins.Remove(plugin);
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x00043528 File Offset: 0x00041728
	private void OnDestroy()
	{
		Events.Scheduler.OnSceneStartLateAfterSerialize.Remove(new Action<SceneRoot>(this.OnSceneRootActivatedScene));
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
		if (this.State == Recorder.RecorderState.Recording && this.RecorderStream != null)
		{
			this.FinishFrame();
			((IDisposable)this.RecorderStream).Dispose();
			this.RecorderStream = null;
		}
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x000435B0 File Offset: 0x000417B0
	private void OnApplicationQuit()
	{
		if (this.State == Recorder.RecorderState.Recording && this.RecorderStream != null)
		{
			this.FinishFrame();
			((IDisposable)this.RecorderStream).Dispose();
			this.RecorderStream = null;
		}
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x000435EC File Offset: 0x000417EC
	public static void AssertFoldersExist()
	{
		string path = Path.Combine(OutputFolder.BuildOutputPath, "favoriteReplays");
		string path2 = Path.Combine(OutputFolder.BuildOutputPath, "temporaryReplays");
		string path3 = Path.Combine(OutputFolder.BuildOutputPath, "statisticsReplays");
		string path4 = Path.Combine(OutputFolder.BuildOutputPath, "specialReplays");
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		if (!Directory.Exists(path2))
		{
			Directory.CreateDirectory(path2);
		}
		if (!Directory.Exists(path3))
		{
			Directory.CreateDirectory(path3);
		}
		if (!Directory.Exists(path4))
		{
			Directory.CreateDirectory(path4);
		}
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x00043684 File Offset: 0x00041884
	private void HandleLog(string logString, string stackTrace, LogType logType)
	{
		LogCallbackData logCallbackData = new LogCallbackData();
		logCallbackData.LogString = logString;
		logCallbackData.StackTrace = stackTrace;
		logCallbackData.LogType = logType;
		this.RecorderData.LastFrame.FrameData.Add(logCallbackData);
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x000436C4 File Offset: 0x000418C4
	public void Exit()
	{
		for (int i = 0; i < this.m_plugins.Count; i++)
		{
			IRecorderPlugin recorderPlugin = this.m_plugins[i];
			recorderPlugin.Exit();
		}
		UnityEngine.Object.DestroyObject(this);
		PlayerInput.Instance.Active = true;
	}

	// Token: 0x04000BAC RID: 2988
	public const string TemporaryReplayFolderPath = "temporaryReplays";

	// Token: 0x04000BAD RID: 2989
	public const string FavoriteReplayFolderPath = "favoriteReplays";

	// Token: 0x04000BAE RID: 2990
	public const string StatisticsReplayFolderPath = "statisticsReplays";

	// Token: 0x04000BAF RID: 2991
	public const string SpecialReplayFolderPath = "specialReplays";

	// Token: 0x04000BB0 RID: 2992
	public static Recorder Instance;

	// Token: 0x04000BB1 RID: 2993
	public Recorder.RecorderState State;

	// Token: 0x04000BB2 RID: 2994
	public int CurrentFrameIndex = -1;

	// Token: 0x04000BB3 RID: 2995
	public string CurrentReplayPath = string.Empty;

	// Token: 0x04000BB4 RID: 2996
	private readonly List<IRecorderPlugin> m_plugins = new List<IRecorderPlugin>();

	// Token: 0x04000BB5 RID: 2997
	public RecorderData RecorderData;

	// Token: 0x04000BB6 RID: 2998
	public BinaryWriter RecorderStream;

	// Token: 0x04000BB7 RID: 2999
	public bool Strict;

	// Token: 0x04000BB8 RID: 3000
	public bool CorrectWrongPositions = true;

	// Token: 0x04000BB9 RID: 3001
	public bool ForceReloadScene;

	// Token: 0x04000BBA RID: 3002
	public bool Active = true;

	// Token: 0x04000BBB RID: 3003
	public Action OnFinishedReplay = delegate()
	{
	};

	// Token: 0x04000BBC RID: 3004
	private static List<string> ReplaysQueue = new List<string>();

	// Token: 0x02000172 RID: 370
	public enum RecorderState
	{
		// Token: 0x04000BBF RID: 3007
		Recording,
		// Token: 0x04000BC0 RID: 3008
		Playing,
		// Token: 0x04000BC1 RID: 3009
		Idle
	}
}
