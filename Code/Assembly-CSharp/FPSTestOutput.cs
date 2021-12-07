using System;
using System.Globalization;
using System.IO;
using UnityEngine;

// Token: 0x02000750 RID: 1872
public class FPSTestOutput
{
	// Token: 0x06002BD0 RID: 11216 RVA: 0x000BBC2C File Offset: 0x000B9E2C
	public FPSTestOutput(string outputFileName = "")
	{
		this.ID = (float)(DateTime.Now.Hour * 100000 + DateTime.Now.Minute * 10000 + DateTime.Now.Millisecond);
		this.m_outputFolder = Path.Combine(Application.dataPath, Path.Combine(OutputFolder.BuildOutputPath, "fpsTest"));
		string path = Path.Combine(this.m_outputFolder, "FPSTestOutput.csv");
		if (outputFileName != string.Empty)
		{
			path = Path.Combine(this.m_outputFolder, outputFileName);
		}
		if (!Directory.Exists(this.m_outputFolder))
		{
			Directory.CreateDirectory(this.m_outputFolder);
		}
		bool flag = File.Exists(path);
		this.m_streamWriter = new StreamWriter(new FileStream(path, FileMode.Append));
		if (!flag)
		{
			this.m_streamWriter.WriteLine("ID, SceneName, SceneLoadTime, SceneUnloadTime, Time, Active Scenes, Loaded Scenes, Min FPS, Average FPS, Min FPS - No Camera, Average FPS - No Camera, Min FPS - quad=0, Average FPS - quad-0, Total Memory, Audio Memory, Texture Memory, SampleID");
		}
	}

	// Token: 0x06002BD1 RID: 11217 RVA: 0x000BBD20 File Offset: 0x000B9F20
	public void Write(FPTTestResult fptTestResult)
	{
		if (this.m_streamWriter == null)
		{
			return;
		}
		foreach (FPSSampleData fpssampleData in fptTestResult.SampleList)
		{
			this.m_streamWriter.WriteLine(string.Concat(new object[]
			{
				this.ID,
				",",
				fptTestResult.SceneName,
				",",
				fptTestResult.SceneLoadTime,
				",",
				fptTestResult.SceneUnloadTime,
				",",
				fptTestResult.DateTime.ToString("G", new CultureInfo("fr-FR")),
				",",
				fptTestResult.ActiveScenes,
				",",
				fptTestResult.LoadedScenes,
				",",
				fpssampleData.MinimumFPS,
				",",
				fpssampleData.AverageFPS,
				",",
				fpssampleData.CPUMinimumFPS,
				",",
				fpssampleData.CPUAverageFPS,
				",",
				fpssampleData.CPUBMinimumFPS,
				",",
				fpssampleData.CPUBAverageFPS,
				",",
				fpssampleData.TotalMemory.ToString("F3"),
				",",
				fpssampleData.AudioMemory.ToString("F3"),
				",",
				fpssampleData.TextureMemory.ToString("F3"),
				",",
				fpssampleData.SampleID,
				","
			}));
		}
		this.m_streamWriter.Flush();
	}

	// Token: 0x06002BD2 RID: 11218 RVA: 0x000BBF50 File Offset: 0x000BA150
	public void Close()
	{
		if (this.m_streamWriter != null)
		{
			((IDisposable)this.m_streamWriter).Dispose();
			this.m_streamWriter = null;
		}
	}

	// Token: 0x06002BD3 RID: 11219 RVA: 0x000BBF6F File Offset: 0x000BA16F
	public string GetOutputPath()
	{
		return this.m_outputFolder;
	}

	// Token: 0x0400279A RID: 10138
	private float ID;

	// Token: 0x0400279B RID: 10139
	private StreamWriter m_streamWriter;

	// Token: 0x0400279C RID: 10140
	private string m_outputFolder = string.Empty;
}
