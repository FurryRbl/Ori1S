using System;
using System.IO;

// Token: 0x0200018D RID: 397
public class RecorderInput
{
	// Token: 0x06000F88 RID: 3976 RVA: 0x0004797F File Offset: 0x00045B7F
	public static bool IsReplayProvided()
	{
		return RecorderInput.ReplayPath != string.Empty;
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x00047990 File Offset: 0x00045B90
	public static void Init()
	{
		string replayCommandFilePath = RecorderInput.GetReplayCommandFilePath();
		if (File.Exists(replayCommandFilePath))
		{
			using (StreamReader streamReader = new StreamReader(new FileStream(replayCommandFilePath, FileMode.Open)))
			{
				RecorderInput.ReplayPath = streamReader.ReadLine();
			}
			File.Delete(replayCommandFilePath);
		}
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x000479F0 File Offset: 0x00045BF0
	public static string GetReplayCommandFilePath()
	{
		return Path.Combine(OutputFolder.BuildOutputPath, "replayFilePath.txt");
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x00047A04 File Offset: 0x00045C04
	public static void Save()
	{
		using (StreamWriter streamWriter = new StreamWriter(new FileStream(RecorderInput.GetReplayCommandFilePath(), FileMode.Create)))
		{
			streamWriter.WriteLine(RecorderInput.ReplayPath);
		}
	}

	// Token: 0x04000C51 RID: 3153
	public static string ReplayPath = string.Empty;
}
