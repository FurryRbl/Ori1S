using System;
using System.IO;
using UnityEngine;

// Token: 0x02000121 RID: 289
public static class OutputFolder
{
	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00034A6C File Offset: 0x00032C6C
	public static string AppData
	{
		get
		{
			if (OutputFolder.m_appData == string.Empty)
			{
				OutputFolder.m_appData = Environment.GetEnvironmentVariable("LocalAppData");
				if (OutputFolder.m_appData == null)
				{
					OutputFolder.m_appData = Environment.GetEnvironmentVariable("UserProfile");
					if (OutputFolder.m_appData == null)
					{
						OutputFolder.m_appData = Directory.GetCurrentDirectory();
						return OutputFolder.m_appData;
					}
					string text = Path.Combine(OutputFolder.m_appData, "AppData");
					string text2 = Path.Combine(text, "Local");
					if (Directory.Exists(OutputFolder.m_appData) && Directory.Exists(text) && Directory.Exists(text2))
					{
						OutputFolder.m_appData = text2;
					}
					else
					{
						OutputFolder.m_appData = Directory.GetCurrentDirectory();
					}
				}
			}
			return OutputFolder.m_appData;
		}
	}

	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00034B2C File Offset: 0x00032D2C
	public static string PlayerDataFolderPath
	{
		get
		{
			if (OutputFolder.m_playerDataFolderPath == string.Empty)
			{
				if (GameController.Instance.IsTrial)
				{
					OutputFolder.m_playerDataFolderPath = Path.Combine(OutputFolder.AppData, OutputFolder.OriTrialFolderName);
				}
				else
				{
					OutputFolder.m_playerDataFolderPath = Path.Combine(OutputFolder.AppData, OutputFolder.OriFolderName);
				}
				if (!Directory.Exists(OutputFolder.m_playerDataFolderPath))
				{
					Directory.CreateDirectory(OutputFolder.m_playerDataFolderPath);
				}
			}
			return OutputFolder.m_playerDataFolderPath;
		}
	}

	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00034BA8 File Offset: 0x00032DA8
	public static string PlayerTrialDataFolderPath
	{
		get
		{
			if (OutputFolder.m_playerTrialDataFolderPath == string.Empty)
			{
				OutputFolder.m_playerTrialDataFolderPath = Path.Combine(OutputFolder.AppData, OutputFolder.OriTrialFolderName);
				if (!Directory.Exists(OutputFolder.m_playerTrialDataFolderPath))
				{
					Directory.CreateDirectory(OutputFolder.m_playerTrialDataFolderPath);
				}
			}
			return OutputFolder.m_playerTrialDataFolderPath;
		}
	}

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00034BFC File Offset: 0x00032DFC
	public static string BuildOutputPath
	{
		get
		{
			if (OutputFolder.m_path == null)
			{
				OutputFolder.m_path = Path.Combine(Application.dataPath, "output");
				if (!Directory.Exists(OutputFolder.m_path))
				{
					Directory.CreateDirectory(OutputFolder.m_path);
				}
			}
			return OutputFolder.m_path;
		}
	}

	// Token: 0x04000993 RID: 2451
	private static string m_appData = string.Empty;

	// Token: 0x04000994 RID: 2452
	private static string m_playerDataFolderPath = string.Empty;

	// Token: 0x04000995 RID: 2453
	private static string m_playerTrialDataFolderPath = string.Empty;

	// Token: 0x04000996 RID: 2454
	public static string EditorOriFolderName = "Ori Editor";

	// Token: 0x04000997 RID: 2455
	public static string OriTrialFolderName = "Ori and the Blind Forest DE Trial";

	// Token: 0x04000998 RID: 2456
	public static string OriFolderName = "Ori and the Blind Forest DE";

	// Token: 0x04000999 RID: 2457
	private static string m_path;
}
