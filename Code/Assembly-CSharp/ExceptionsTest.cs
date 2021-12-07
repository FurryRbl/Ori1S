using System;
using System.IO;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000759 RID: 1881
public class ExceptionsTest : MonoBehaviour
{
	// Token: 0x06002BF6 RID: 11254 RVA: 0x000BC7CB File Offset: 0x000BA9CB
	private void Awake()
	{
	}

	// Token: 0x06002BF7 RID: 11255 RVA: 0x000BC7CD File Offset: 0x000BA9CD
	private void OnDestroy()
	{
		if (this.m_testSuite != null)
		{
			this.WriteResults();
		}
	}

	// Token: 0x06002BF8 RID: 11256 RVA: 0x000BC7E0 File Offset: 0x000BA9E0
	private void Start()
	{
		string path = Path.Combine(OutputFolder.BuildOutputPath, this.TestFolderName);
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
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
	}

	// Token: 0x06002BF9 RID: 11257 RVA: 0x000BC878 File Offset: 0x000BAA78
	private void FixedUpdate()
	{
		if (this.m_currentStateTime > 5f)
		{
			this.ChangeState(ExceptionsTest.State.EndTest);
		}
		switch (this.CurrentState)
		{
		case ExceptionsTest.State.Idle:
			if (this.m_currentStateTime > 0.5f)
			{
				Events.Scheduler.OnSceneRootLoadEarlyStart.Add(new Action<SceneRoot>(this.OnSceneRootLoadEarlyStart));
				Events.Scheduler.OnSceneRootPreEnabled.Add(new Action<SceneRoot>(this.OnSceneRootPreEnabled));
				if (Application.loadedLevelName == this.EmptyTestSceneName)
				{
					this.ChangeState(ExceptionsTest.State.StartLoadScene);
				}
				else
				{
					this.LoadEmptyLevel();
				}
			}
			break;
		case ExceptionsTest.State.StartTest:
			if (this.m_currentStateTime > this.StartTestGraceTime)
			{
				this.ChangeState(ExceptionsTest.State.Testing);
			}
			break;
		case ExceptionsTest.State.Testing:
			if (this.m_currentStateTime > this.TestDuration)
			{
				this.ChangeState(ExceptionsTest.State.EndTest);
			}
			break;
		}
		this.m_currentStateTime += Time.fixedDeltaTime;
	}

	// Token: 0x06002BFA RID: 11258 RVA: 0x000BC9C0 File Offset: 0x000BABC0
	private void ChangeState(ExceptionsTest.State state)
	{
		switch (this.CurrentState)
		{
		}
		this.m_currentStateTime = 0f;
		this.CurrentState = state;
		switch (this.CurrentState)
		{
		case ExceptionsTest.State.StartLoadScene:
			this.m_logCallbackHandler = new LogCallbackHandler();
			this.LoadLevel(this.CurrentSceneMetaDataIndex);
			this.ChangeState(ExceptionsTest.State.SceneLoading);
			break;
		case ExceptionsTest.State.EndLoadScene:
			this.ChangeState(ExceptionsTest.State.StartTest);
			break;
		case ExceptionsTest.State.EndTest:
			this.ChangeState(ExceptionsTest.State.StartUnloadScene);
			break;
		case ExceptionsTest.State.StartUnloadScene:
		{
			this.m_failure = null;
			bool flag = false;
			foreach (LogCallbackHandler.LogEntry logEntry in this.m_logCallbackHandler.LogEntries)
			{
				if (logEntry.LogType == LogType.Exception || logEntry.LogType == LogType.Assert || logEntry.LogType == LogType.Error)
				{
					flag = true;
					break;
				}
			}
			string text = string.Empty;
			if (flag)
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
			if (flag)
			{
				text = text.Replace("<", "[").Replace(">", "]");
				this.m_failure = new JUnitReporter.Failure(text, text, text);
			}
			this.m_logCallbackHandler.RemoveHandler();
			this.m_logCallbackHandler = null;
			this.m_testSuite.AddTestCase(this.GetMetaData(this.CurrentSceneMetaDataIndex).Scene, "Test", this.m_failure, 0f);
			this.LoadEmptyLevel();
			this.ChangeState(ExceptionsTest.State.UnloadingScene);
			break;
		}
		case ExceptionsTest.State.EndUnloadScene:
			if (this.IsLastLevel(this.CurrentSceneMetaDataIndex))
			{
				this.ChangeState(ExceptionsTest.State.Done);
			}
			else
			{
				this.CurrentSceneMetaDataIndex++;
				if (this.GetMetaData(this.CurrentSceneMetaDataIndex).Scene == this.EmptyTestSceneName)
				{
					while (this.GetMetaData(this.CurrentSceneMetaDataIndex).Scene == this.EmptyTestSceneName)
					{
						if (this.IsLastLevel(this.CurrentSceneMetaDataIndex))
						{
							this.ChangeState(ExceptionsTest.State.Done);
							return;
						}
						this.CurrentSceneMetaDataIndex++;
					}
				}
				this.ChangeState(ExceptionsTest.State.StartLoadScene);
			}
			break;
		case ExceptionsTest.State.Done:
			if (!Application.isEditor)
			{
				try
				{
					this.WriteResults();
					this.m_testSuite = null;
				}
				finally
				{
					Application.Quit();
				}
			}
			break;
		}
	}

	// Token: 0x06002BFB RID: 11259 RVA: 0x000BCD5C File Offset: 0x000BAF5C
	private void WriteResults()
	{
		using (StreamWriter streamWriter = new StreamWriter(new FileStream(Path.Combine(this.m_workspace, "projectTestReport.xml"), FileMode.Create)))
		{
			streamWriter.WriteLine(this.m_testSuite.ToString());
		}
	}

	// Token: 0x06002BFC RID: 11260 RVA: 0x000BCDB8 File Offset: 0x000BAFB8
	private RuntimeSceneMetaData GetMetaData(int metaDataIndex)
	{
		return Scenes.Manager.AllScenes[metaDataIndex];
	}

	// Token: 0x06002BFD RID: 11261 RVA: 0x000BCDCA File Offset: 0x000BAFCA
	private bool IsLastLevel(int metaDataIndex)
	{
		return metaDataIndex + 1 == Scenes.Manager.AllScenes.Count;
	}

	// Token: 0x06002BFE RID: 11262 RVA: 0x000BCDE0 File Offset: 0x000BAFE0
	private void LoadLevel(int metaDataIndex)
	{
		GoToSceneController.Instance.GoToScene(this.GetMetaData(metaDataIndex).Scene);
	}

	// Token: 0x06002BFF RID: 11263 RVA: 0x000BCDF8 File Offset: 0x000BAFF8
	private void LoadEmptyLevel()
	{
		GoToSceneController.Instance.GoToScene(this.EmptyTestSceneName);
	}

	// Token: 0x06002C00 RID: 11264 RVA: 0x000BCE0A File Offset: 0x000BB00A
	public void OnSceneRootPreEnabled(SceneRoot sceneRoot)
	{
	}

	// Token: 0x06002C01 RID: 11265 RVA: 0x000BCE0C File Offset: 0x000BB00C
	public void OnSceneRootLoadEarlyStart(SceneRoot sceneRoot)
	{
		if (this.CurrentState == ExceptionsTest.State.Idle)
		{
			if (sceneRoot.name == this.EmptyTestSceneName)
			{
				this.ChangeState(ExceptionsTest.State.StartLoadScene);
			}
		}
		else if (this.CurrentState == ExceptionsTest.State.SceneLoading)
		{
			if (sceneRoot.name == this.GetMetaData(this.CurrentSceneMetaDataIndex).Scene)
			{
				this.ChangeState(ExceptionsTest.State.EndLoadScene);
			}
		}
		else if (this.CurrentState == ExceptionsTest.State.UnloadingScene && sceneRoot.name == this.EmptyTestSceneName)
		{
			this.ChangeState(ExceptionsTest.State.EndUnloadScene);
		}
	}

	// Token: 0x040027AF RID: 10159
	public string TestFolderName = "test";

	// Token: 0x040027B0 RID: 10160
	private ExceptionsTest.State CurrentState;

	// Token: 0x040027B1 RID: 10161
	private float m_currentStateTime;

	// Token: 0x040027B2 RID: 10162
	private int CurrentSceneMetaDataIndex;

	// Token: 0x040027B3 RID: 10163
	private float StartTestGraceTime;

	// Token: 0x040027B4 RID: 10164
	private float TestDuration = 0.3f;

	// Token: 0x040027B5 RID: 10165
	private string EmptyTestSceneName = "emptyTestScene";

	// Token: 0x040027B6 RID: 10166
	private LogCallbackHandler m_logCallbackHandler;

	// Token: 0x040027B7 RID: 10167
	private string m_workspace = string.Empty;

	// Token: 0x040027B8 RID: 10168
	private JUnitReporter.TestSuite m_testSuite = new JUnitReporter.TestSuite();

	// Token: 0x040027B9 RID: 10169
	private JUnitReporter.Failure m_failure;

	// Token: 0x0200075A RID: 1882
	public enum State
	{
		// Token: 0x040027BB RID: 10171
		Idle,
		// Token: 0x040027BC RID: 10172
		StartLoadScene,
		// Token: 0x040027BD RID: 10173
		SceneLoading,
		// Token: 0x040027BE RID: 10174
		EndLoadScene,
		// Token: 0x040027BF RID: 10175
		StartTest,
		// Token: 0x040027C0 RID: 10176
		Testing,
		// Token: 0x040027C1 RID: 10177
		EndTest,
		// Token: 0x040027C2 RID: 10178
		StartUnloadScene,
		// Token: 0x040027C3 RID: 10179
		UnloadingScene,
		// Token: 0x040027C4 RID: 10180
		EndUnloadScene,
		// Token: 0x040027C5 RID: 10181
		Done
	}
}
