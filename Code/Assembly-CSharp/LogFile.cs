using System;
using System.IO;
using UnityEngine;

// Token: 0x02000985 RID: 2437
public class LogFile : MonoSingleton<LogFile>
{
	// Token: 0x06003554 RID: 13652 RVA: 0x000DFB79 File Offset: 0x000DDD79
	private void OnDestroy()
	{
		if (this.m_file != null)
		{
			((IDisposable)this.m_file).Dispose();
		}
	}

	// Token: 0x06003555 RID: 13653 RVA: 0x000DFB91 File Offset: 0x000DDD91
	public void Write(object s)
	{
		this.m_file.Write(s);
		this.m_file.Write("\n");
	}

	// Token: 0x06003556 RID: 13654 RVA: 0x000DFBAF File Offset: 0x000DDDAF
	public static void Log(object s)
	{
		if (Application.isEditor)
		{
			return;
		}
		MonoSingleton<LogFile>.Instance.Write(s);
	}

	// Token: 0x04002FF2 RID: 12274
	private readonly StreamWriter m_file = new StreamWriter(new FileStream("C:/SeinLog.txt", FileMode.Create));
}
