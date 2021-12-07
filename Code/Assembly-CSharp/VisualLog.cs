using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004BE RID: 1214
public class VisualLog : MonoBehaviour
{
	// Token: 0x060020E1 RID: 8417 RVA: 0x0008FED4 File Offset: 0x0008E0D4
	public static void RegisterStatus(MonoBehaviour behaviour)
	{
	}

	// Token: 0x17000596 RID: 1430
	// (get) Token: 0x060020E2 RID: 8418 RVA: 0x0008FED6 File Offset: 0x0008E0D6
	public static VisualLog Instance
	{
		get
		{
			if (VisualLog.s_instance == null)
			{
				VisualLog.s_instance = UnityEngine.Object.FindObjectOfType<VisualLog>();
			}
			return VisualLog.s_instance;
		}
	}

	// Token: 0x060020E3 RID: 8419 RVA: 0x0008FEF7 File Offset: 0x0008E0F7
	public static void Disable()
	{
		UnityEngine.Object.DestroyObject(VisualLog.Instance);
	}

	// Token: 0x060020E4 RID: 8420 RVA: 0x0008FF03 File Offset: 0x0008E103
	private static void OnLog(string message, string stack, LogType logType)
	{
		VisualLog.Instance.m_log.Add(string.Format("[{0}][{1}]: {2}", Time.frameCount, logType, message));
	}

	// Token: 0x04001BD5 RID: 7125
	private List<string> m_log = new List<string>();

	// Token: 0x04001BD6 RID: 7126
	private static VisualLog s_instance;
}
