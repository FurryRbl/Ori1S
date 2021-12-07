using System;
using System.IO;
using System.Linq;

// Token: 0x02000181 RID: 385
public class Frapser
{
	// Token: 0x06000F20 RID: 3872 RVA: 0x000456B8 File Offset: 0x000438B8
	public static bool IsFrapserActive()
	{
		return Environment.GetCommandLineArgs().Any((string argument) => argument.Contains("fraps"));
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x000456F0 File Offset: 0x000438F0
	public static void StopFrapser()
	{
		string path = Path.Combine(OutputFolder.BuildOutputPath, Frapser.STOP_FRAPS_FILE_NAME);
		StreamWriter streamWriter = new StreamWriter(path, false);
		streamWriter.Close();
	}

	// Token: 0x04000C03 RID: 3075
	private static string STOP_FRAPS_FILE_NAME = "StopFraps.txt";
}
