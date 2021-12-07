using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000730 RID: 1840
public class SoundLog : MonoBehaviour
{
	// Token: 0x06002B40 RID: 11072 RVA: 0x000B94CC File Offset: 0x000B76CC
	public static void AddSoundCall(string clipName, string providerName)
	{
		string text = clipName.ToLower();
		string text2 = providerName.ToLower();
		if (SoundLog.m_filters != null)
		{
			foreach (string value in SoundLog.m_filters)
			{
				if (text.Contains(value) || text2.Contains(value))
				{
					return;
				}
			}
		}
		if (text.Contains("ambience") || text.Contains("music") || text.Contains("seinlands") || text.Contains("seinjumps") || text.Contains("seinfootsteps"))
		{
		}
		if (SoundLog.recentSoundCalls == null)
		{
			SoundLog.recentSoundCalls = new Queue<string>();
		}
		SoundLog.recentSoundCalls.Enqueue(clipName + " " + providerName);
		if (SoundLog.recentSoundCalls.Count > 12)
		{
			SoundLog.recentSoundCalls.Dequeue();
		}
	}

	// Token: 0x06002B41 RID: 11073 RVA: 0x000B95EC File Offset: 0x000B77EC
	private void OnGUI()
	{
		GUI.Box(new Rect(10f, 10f, 700f, 255f), "Sound Log");
		SoundLog.m_currentText = GUI.TextArea(new Rect(20f, 35f, 300f, 30f), SoundLog.m_currentText);
		if (GUI.Button(new Rect(330f, 35f, 95f, 30f), "Set as Filter"))
		{
			SoundLog.AddFilter(SoundLog.m_currentText);
			SoundLog.m_currentText = string.Empty;
		}
		if (GUI.Button(new Rect(430f, 35f, 95f, 30f), "Reset Filter"))
		{
			SoundLog.ResetFilters();
		}
		string text = string.Empty;
		if (SoundLog.recentSoundCalls != null)
		{
			foreach (string str in SoundLog.recentSoundCalls)
			{
				text = text + "- play " + str + "\n";
			}
		}
		GUI.Label(new Rect(30f, 70f, 650f, 250f), text);
	}

	// Token: 0x06002B42 RID: 11074 RVA: 0x000B9734 File Offset: 0x000B7934
	public static void AddFilter(string filter)
	{
		if (SoundLog.m_filters == null)
		{
			SoundLog.m_filters = new List<string>();
		}
		SoundLog.m_filters.Add(filter);
	}

	// Token: 0x06002B43 RID: 11075 RVA: 0x000B9755 File Offset: 0x000B7955
	public static void ResetFilters()
	{
		SoundLog.m_filters = new List<string>();
	}

	// Token: 0x06002B44 RID: 11076 RVA: 0x000B9761 File Offset: 0x000B7961
	public static void ResetLog()
	{
		SoundLog.ResetFilters();
		SoundLog.recentSoundCalls = null;
	}

	// Token: 0x04002701 RID: 9985
	private static Queue<string> recentSoundCalls;

	// Token: 0x04002702 RID: 9986
	private static List<string> m_filters;

	// Token: 0x04002703 RID: 9987
	private static string m_currentText = string.Empty;
}
