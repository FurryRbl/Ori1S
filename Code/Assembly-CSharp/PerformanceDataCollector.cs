using System;
using System.IO;
using UnityEngine;

// Token: 0x0200049D RID: 1181
[RequireComponent(typeof(FPSCounter))]
public class PerformanceDataCollector : MonoBehaviour
{
	// Token: 0x06001FE9 RID: 8169 RVA: 0x0008BF38 File Offset: 0x0008A138
	private void Start()
	{
		if (Application.isEditor)
		{
			UnityEngine.Object.DestroyObject(this);
			return;
		}
		if (Application.platform == RuntimePlatform.XBOX360)
		{
			this.m_mainPath = "net:\\smb";
			if (!Directory.Exists("net:\\smb\\moonstation"))
			{
				UnityEngine.Object.DestroyObject(this);
				return;
			}
			this.m_serverName = "moonstation";
		}
		else
		{
			if (Application.platform != RuntimePlatform.WindowsPlayer)
			{
				UnityEngine.Object.DestroyObject(this);
				return;
			}
			if (!(Environment.MachineName.ToLower() == "moonstation"))
			{
				UnityEngine.Object.DestroyObject(this);
				return;
			}
			this.m_serverName = "moonstation";
			this.m_mainPath = "C:\\Dropbox";
		}
		this.m_fpsLogPath = Path.Combine(this.m_mainPath, "MoonStudios\\Tech\\XboxShare\\PerformanceLog_" + this.m_serverName + ".csv");
		this.m_fpsCounter = base.GetComponent<FPSCounter>();
	}

	// Token: 0x06001FEA RID: 8170 RVA: 0x0008C01C File Offset: 0x0008A21C
	private void OnApplicationQuit()
	{
		this.WriteData();
	}

	// Token: 0x06001FEB RID: 8171 RVA: 0x0008C024 File Offset: 0x0008A224
	private void WriteData()
	{
		using (StreamWriter streamWriter = new StreamWriter(new FileStream(this.m_fpsLogPath, FileMode.Append)))
		{
			streamWriter.WriteLine(string.Concat(new object[]
			{
				DateTime.Now.ToString(),
				", ",
				Application.loadedLevelName,
				", ",
				this.m_fpsCounter.AverageFPS,
				", ",
				this.m_fpsCounter.MinimumFPS,
				", ",
				Time.timeSinceLevelLoad
			}));
		}
	}

	// Token: 0x04001B83 RID: 7043
	private string m_serverName;

	// Token: 0x04001B84 RID: 7044
	private string m_mainPath;

	// Token: 0x04001B85 RID: 7045
	private FPSCounter m_fpsCounter;

	// Token: 0x04001B86 RID: 7046
	private string m_fpsLogPath;
}
