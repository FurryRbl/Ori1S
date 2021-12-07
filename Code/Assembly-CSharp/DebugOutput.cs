using System;
using System.IO;
using UnityEngine;

// Token: 0x02000499 RID: 1177
public class DebugOutput : MonoBehaviour
{
	// Token: 0x06001FDA RID: 8154 RVA: 0x0008BD5C File Offset: 0x00089F5C
	private void Awake()
	{
		DebugOutput.sw = new StreamWriter(new FileStream("Output.txt", FileMode.Create));
		DebugOutput.init = true;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06001FDB RID: 8155 RVA: 0x0008BD8F File Offset: 0x00089F8F
	public static void Output(string text)
	{
		if (DebugOutput.init)
		{
			DebugOutput.sw.WriteLine(text);
			DebugOutput.sw.Flush();
		}
	}

	// Token: 0x06001FDC RID: 8156 RVA: 0x0008BDB0 File Offset: 0x00089FB0
	private void OnDestroy()
	{
		if (DebugOutput.sw != null)
		{
			((IDisposable)DebugOutput.sw).Dispose();
			DebugOutput.sw = null;
		}
	}

	// Token: 0x04001B72 RID: 7026
	private static StreamWriter sw;

	// Token: 0x04001B73 RID: 7027
	private static bool init;
}
