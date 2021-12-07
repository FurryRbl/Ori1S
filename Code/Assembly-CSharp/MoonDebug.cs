using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Core;
using UnityEngine;

// Token: 0x0200064D RID: 1613
public class MoonDebug
{
	// Token: 0x06002777 RID: 10103 RVA: 0x000ABD8C File Offset: 0x000A9F8C
	public MoonDebug()
	{
		this.m_outputFilePath = Path.Combine(OutputFolder.BuildOutputPath, this.m_outputFilePath);
	}

	// Token: 0x06002779 RID: 10105 RVA: 0x000ABDE0 File Offset: 0x000A9FE0
	public static void OnApplicationQuit()
	{
		MoonDebug.Flush();
		if (MoonDebug.m_instance == null || MoonDebug.m_instance.m_streamWriter == null)
		{
			return;
		}
		((IDisposable)MoonDebug.m_instance.m_streamWriter).Dispose();
	}

	// Token: 0x0600277A RID: 10106 RVA: 0x000ABE1C File Offset: 0x000AA01C
	private static void AddMessage(MoonDebugMessageType typ, string text, UnityEngine.Object obj)
	{
		MoonDebug.m_instance.m_debugMessages.Add(new MoonDebugMessage(typ, text, obj));
		if (MoonDebug.m_instance.m_debugMessages.Count > MoonDebug.m_instance.m_flushThreshhold)
		{
			MoonDebug.Flush();
		}
	}

	// Token: 0x0600277B RID: 10107 RVA: 0x000ABE64 File Offset: 0x000AA064
	public static void DrawCircle(Vector3 position, float radius, Color color)
	{
		for (int i = 0; i < 64; i++)
		{
			Vector3 start = position + radius * new Vector3(Mathf.Sin((float)i / 64f * 3.1415927f * 2f), Mathf.Cos((float)i / 64f * 3.1415927f * 2f));
			Vector3 end = position + radius * new Vector3(Mathf.Sin((float)(i + 1) / 64f * 3.1415927f * 2f), Mathf.Cos((float)(i + 1) / 64f * 3.1415927f * 2f));
			UnityEngine.Debug.DrawLine(start, end, color);
		}
	}

	// Token: 0x0600277C RID: 10108 RVA: 0x000ABF18 File Offset: 0x000AA118
	private static void Flush()
	{
		if (MoonDebug.m_instance == null)
		{
			return;
		}
		if (MoonDebug.m_instance.m_streamWriter == null)
		{
			MoonDebug.m_instance.m_streamWriter = new StreamWriter(new FileStream(MoonDebug.m_instance.m_outputFilePath, FileMode.Append));
		}
		foreach (MoonDebugMessage moonDebugMessage in MoonDebug.m_instance.m_debugMessages)
		{
			MoonDebug.m_instance.m_streamWriter.WriteLine(moonDebugMessage.ToString());
		}
		MoonDebug.m_instance.m_debugMessages.Clear();
		MoonDebug.m_instance.m_streamWriter.Flush();
		InstantiateUtility.DumpInstanceCount();
	}

	// Token: 0x0600277D RID: 10109 RVA: 0x000ABFE0 File Offset: 0x000AA1E0
	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void Log(object message, UnityEngine.Object obj = null)
	{
	}

	// Token: 0x0600277E RID: 10110 RVA: 0x000ABFE2 File Offset: 0x000AA1E2
	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void LogWarning(string text, UnityEngine.Object obj = null)
	{
	}

	// Token: 0x0600277F RID: 10111 RVA: 0x000ABFE4 File Offset: 0x000AA1E4
	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void LogError(string text, UnityEngine.Object obj = null)
	{
	}

	// Token: 0x06002780 RID: 10112 RVA: 0x000ABFE6 File Offset: 0x000AA1E6
	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void LogException(Exception exception, UnityEngine.Object obj = null)
	{
	}

	// Token: 0x06002781 RID: 10113 RVA: 0x000ABFE8 File Offset: 0x000AA1E8
	public static string GetCurrentSceneName()
	{
		if (Scenes.Manager == null || Scenes.Manager.CurrentScene == null)
		{
			return "nullScene";
		}
		return Scenes.Manager.CurrentScene.Scene;
	}

	// Token: 0x06002782 RID: 10114 RVA: 0x000AC01E File Offset: 0x000AA21E
	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void DrawLine(Vector3 origin, Vector3 target, Color color)
	{
		UnityEngine.Debug.DrawLine(origin, target, color);
	}

	// Token: 0x04002215 RID: 8725
	private static MoonDebug m_instance = new MoonDebug();

	// Token: 0x04002216 RID: 8726
	public List<MoonDebugMessage> m_debugMessages = new List<MoonDebugMessage>();

	// Token: 0x04002217 RID: 8727
	private int m_flushThreshhold = 50;

	// Token: 0x04002218 RID: 8728
	private string m_outputFilePath = "moonDebug.txt";

	// Token: 0x04002219 RID: 8729
	private StreamWriter m_streamWriter;
}
