using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020004C2 RID: 1218
public class SceneFrameworkPerformanceMonitor : MonoBehaviour
{
	// Token: 0x17000598 RID: 1432
	// (get) Token: 0x06002101 RID: 8449 RVA: 0x00090AD2 File Offset: 0x0008ECD2
	// (set) Token: 0x06002102 RID: 8450 RVA: 0x00090AE0 File Offset: 0x0008ECE0
	public static bool Enabled
	{
		get
		{
			return SceneFrameworkPerformanceMonitor.m_instance != null;
		}
		set
		{
			if (SceneFrameworkPerformanceMonitor.m_instance && !value)
			{
				UnityEngine.Object.Destroy(SceneFrameworkPerformanceMonitor.m_instance.gameObject);
			}
			if (SceneFrameworkPerformanceMonitor.m_instance == null && value)
			{
				GameObject gameObject = new GameObject("sceneFrameworkPerformanceMonitor");
				gameObject.AddComponent<SceneFrameworkPerformanceMonitor>();
			}
		}
	}

	// Token: 0x06002103 RID: 8451 RVA: 0x00090B39 File Offset: 0x0008ED39
	public void OnDestroy()
	{
		this.CloseWriter();
	}

	// Token: 0x06002104 RID: 8452 RVA: 0x00090B44 File Offset: 0x0008ED44
	public void Awake()
	{
		SceneFrameworkPerformanceMonitor.m_instance = this;
		this.m_streamWriter = new StreamWriter(new FileStream(Path.Combine(OutputFolder.BuildOutputPath, "sceneFrameworkPerformance.txt"), FileMode.Create));
	}

	// Token: 0x06002105 RID: 8453 RVA: 0x00090B77 File Offset: 0x0008ED77
	public void CloseWriter()
	{
		if (this.m_streamWriter != null)
		{
			((IDisposable)this.m_streamWriter).Dispose();
			this.m_streamWriter = null;
		}
	}

	// Token: 0x17000599 RID: 1433
	// (get) Token: 0x06002106 RID: 8454 RVA: 0x00090B96 File Offset: 0x0008ED96
	public static bool Ready
	{
		get
		{
			return SceneFrameworkPerformanceMonitor.m_instance;
		}
	}

	// Token: 0x06002107 RID: 8455 RVA: 0x00090BA4 File Offset: 0x0008EDA4
	public void Write(string message)
	{
		this.m_lines.Add(Time.renderedFrameCount + "\t" + message);
	}

	// Token: 0x06002108 RID: 8456 RVA: 0x00090BD4 File Offset: 0x0008EDD4
	public static void AddSceneLoadItem(SceneManagerScene scene)
	{
		if (!SceneFrameworkPerformanceMonitor.Ready)
		{
			return;
		}
		SceneFrameworkPerformanceMonitor.m_instance.Write(string.Concat(new object[]
		{
			"Loaded\t",
			scene.MetaData.Scene,
			"\t",
			scene.LoadingTime
		}));
	}

	// Token: 0x06002109 RID: 8457 RVA: 0x00090C30 File Offset: 0x0008EE30
	public static void UnloadScene(SceneRoot scene)
	{
		if (!SceneFrameworkPerformanceMonitor.Ready)
		{
			return;
		}
		SceneFrameworkPerformanceMonitor.m_instance.Write("Unload\t" + scene.MetaData.name);
	}

	// Token: 0x0600210A RID: 8458 RVA: 0x00090C68 File Offset: 0x0008EE68
	public static void EnableScene(SceneRoot scene)
	{
		if (!SceneFrameworkPerformanceMonitor.Ready)
		{
			return;
		}
		SceneFrameworkPerformanceMonitor.m_instance.Write("Enable\t" + scene.MetaData.name);
	}

	// Token: 0x0600210B RID: 8459 RVA: 0x00090CA0 File Offset: 0x0008EEA0
	public static void DisableScene(SceneRoot scene)
	{
		if (!SceneFrameworkPerformanceMonitor.Ready)
		{
			return;
		}
		SceneFrameworkPerformanceMonitor.m_instance.Write("Disable\t" + scene.MetaData.name);
	}

	// Token: 0x0600210C RID: 8460 RVA: 0x00090CD8 File Offset: 0x0008EED8
	public void Update()
	{
		if (Time.renderedFrameCount % 300 == 0)
		{
			foreach (string value in this.m_lines)
			{
				SceneFrameworkPerformanceMonitor.m_instance.m_streamWriter.WriteLine(value);
			}
			this.m_lines.Clear();
		}
		if (Time.renderedFrameCount % 60 == 0)
		{
			this.PrintSoundEntry();
		}
	}

	// Token: 0x0600210D RID: 8461 RVA: 0x00090D6C File Offset: 0x0008EF6C
	public void PrintSoundEntry()
	{
	}

	// Token: 0x04001BF7 RID: 7159
	private StreamWriter m_streamWriter;

	// Token: 0x04001BF8 RID: 7160
	private static SceneFrameworkPerformanceMonitor m_instance;

	// Token: 0x04001BF9 RID: 7161
	private List<string> m_lines = new List<string>();
}
