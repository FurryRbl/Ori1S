using System;
using System.Diagnostics;

// Token: 0x020007A7 RID: 1959
public class ProcessUtility
{
	// Token: 0x06002D6E RID: 11630 RVA: 0x000C21D0 File Offset: 0x000C03D0
	public static string ExcecuteCommandLine(string application, string arguments)
	{
		Process process = new Process();
		process.StartInfo.CreateNoWindow = true;
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.FileName = application;
		process.StartInfo.Arguments = arguments;
		process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		process.Start();
		string result = process.StandardOutput.ReadToEnd();
		process.WaitForExit();
		return result;
	}
}
