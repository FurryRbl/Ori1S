using System;
using System.IO;
using UnityEngine;

// Token: 0x020004A1 RID: 1185
public class BinaryProfilerLogMaker : MonoBehaviour
{
	// Token: 0x17000583 RID: 1411
	// (get) Token: 0x06002058 RID: 8280 RVA: 0x0008CE82 File Offset: 0x0008B082
	// (set) Token: 0x06002059 RID: 8281 RVA: 0x0008CE90 File Offset: 0x0008B090
	public static bool Enabled
	{
		get
		{
			return BinaryProfilerLogMaker.m_instance != null;
		}
		set
		{
			if (BinaryProfilerLogMaker.m_instance && !value)
			{
				InstantiateUtility.Destroy(BinaryProfilerLogMaker.m_instance.gameObject);
			}
			if (BinaryProfilerLogMaker.m_instance == null && value)
			{
				GameObject gameObject = new GameObject("binaryProfilerLogMaker");
				gameObject.AddComponent<BinaryProfilerLogMaker>();
			}
		}
	}

	// Token: 0x0600205A RID: 8282 RVA: 0x0008CEE9 File Offset: 0x0008B0E9
	public void Awake()
	{
		BinaryProfilerLogMaker.m_instance = this;
		Profiler.enabled = true;
		Profiler.enableBinaryLog = true;
		this.m_index = 0;
	}

	// Token: 0x0600205B RID: 8283 RVA: 0x0008CF04 File Offset: 0x0008B104
	public void OnDestroy()
	{
		if (BinaryProfilerLogMaker.m_instance == this)
		{
			BinaryProfilerLogMaker.m_instance = null;
		}
		Profiler.enabled = false;
		Profiler.enableBinaryLog = false;
	}

	// Token: 0x17000584 RID: 1412
	// (get) Token: 0x0600205C RID: 8284 RVA: 0x0008CF34 File Offset: 0x0008B134
	public static string OutputPath
	{
		get
		{
			string text = OutputFolder.BuildOutputPath + "/profiler";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}
	}

	// Token: 0x0600205D RID: 8285 RVA: 0x0008CF64 File Offset: 0x0008B164
	public void Update()
	{
		if (this.m_index == 0)
		{
			Profiler.logFile = string.Concat(new object[]
			{
				BinaryProfilerLogMaker.OutputPath,
				"/profiler_",
				BinaryProfilerLogMaker.m_fileIndex,
				".log"
			});
			Application.CaptureScreenshot(string.Concat(new object[]
			{
				BinaryProfilerLogMaker.OutputPath,
				"/screenshot_",
				BinaryProfilerLogMaker.m_fileIndex,
				".png"
			}));
			BinaryProfilerLogMaker.m_fileIndex++;
		}
		this.m_index++;
		if (this.m_index > 240)
		{
			this.m_index = 0;
		}
	}

	// Token: 0x04001B8B RID: 7051
	private static BinaryProfilerLogMaker m_instance;

	// Token: 0x04001B8C RID: 7052
	private int m_index;

	// Token: 0x04001B8D RID: 7053
	private static int m_fileIndex;
}
