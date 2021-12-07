using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020004BF RID: 1215
public class LogCallbackHandler
{
	// Token: 0x060020E5 RID: 8421 RVA: 0x0008FF2F File Offset: 0x0008E12F
	public LogCallbackHandler()
	{
		LogCallbackHandler.Instance = this;
	}

	// Token: 0x060020E7 RID: 8423 RVA: 0x0008FF6E File Offset: 0x0008E16E
	public void RemoveHandler()
	{
		LogCallbackHandler.Instance = null;
	}

	// Token: 0x060020E8 RID: 8424 RVA: 0x0008FF78 File Offset: 0x0008E178
	public void FlushEntriesToFile(string filePath)
	{
		using (StreamWriter streamWriter = new StreamWriter(new FileStream(filePath, FileMode.Create)))
		{
			foreach (LogCallbackHandler.LogEntry logEntry in this.LogEntries)
			{
				streamWriter.WriteLine(string.Concat(new object[]
				{
					"LogType: ",
					logEntry.LogType,
					"\t Condition: ",
					logEntry.Condition,
					"\t StackTrack: ",
					logEntry.StackTrace
				}));
			}
		}
	}

	// Token: 0x060020E9 RID: 8425 RVA: 0x00090040 File Offset: 0x0008E240
	private void HandleException(string condition, string stackTrace, LogType type)
	{
		if (this.ShouldFilterOut(condition + " " + stackTrace))
		{
			return;
		}
		if (this.m_autoScroll)
		{
			this.m_scrollPosition.y = float.PositiveInfinity;
		}
		this.m_tempLogEntries.Add(new LogCallbackHandler.LogEntry(condition, stackTrace, type));
		switch (type)
		{
		case LogType.Error:
			this.m_showWindow = true;
			this.m_errorCount++;
			break;
		case LogType.Assert:
			this.m_showWindow = true;
			this.m_assertCount++;
			break;
		case LogType.Warning:
			this.m_warningCount++;
			break;
		case LogType.Log:
			this.m_logCount++;
			break;
		case LogType.Exception:
			this.m_showWindow = true;
			this.m_exceptionCount++;
			break;
		}
	}

	// Token: 0x060020EA RID: 8426 RVA: 0x00090128 File Offset: 0x0008E328
	private void MoveTempEntries()
	{
		for (int i = 0; i < this.m_tempLogEntries.Count; i++)
		{
			this.LogEntries.Add(this.m_tempLogEntries[i]);
		}
		this.m_tempLogEntries.Clear();
	}

	// Token: 0x060020EB RID: 8427 RVA: 0x00090173 File Offset: 0x0008E373
	public int GetEditorEntriesCount()
	{
		return this.m_errorCount + this.m_exceptionCount + this.m_assertCount;
	}

	// Token: 0x060020EC RID: 8428 RVA: 0x0009018C File Offset: 0x0008E38C
	private void ClearLogEntries()
	{
		this.m_logCount = 0;
		this.m_warningCount = 0;
		this.m_errorCount = 0;
		this.m_exceptionCount = 0;
		this.m_assertCount = 0;
		this.LogEntries.Clear();
	}

	// Token: 0x060020ED RID: 8429 RVA: 0x000901C7 File Offset: 0x0008E3C7
	public void Show()
	{
		this.m_showWindow = true;
	}

	// Token: 0x060020EE RID: 8430 RVA: 0x000901D0 File Offset: 0x0008E3D0
	public void Hide()
	{
		this.m_showWindow = false;
	}

	// Token: 0x060020EF RID: 8431 RVA: 0x000901D9 File Offset: 0x0008E3D9
	public bool IsVisible()
	{
		return this.m_showWindow;
	}

	// Token: 0x060020F0 RID: 8432 RVA: 0x000901E4 File Offset: 0x0008E3E4
	public bool ShouldFilterOut(string msg)
	{
		return msg.Contains("rt->GetGLWidth() &&") || msg.Contains("(m_Channels[shaderChannelIndex].dimension != ") || msg.Contains("curveT < GetRange ().first || curveT > GetRange ().second") || msg.Contains("AsyncHTTPClient:Done") || msg.Contains("SetPass") || msg.Contains("IDToPointer->find (obj->") || msg.Contains("There are inconsistent line endings") || (msg.Contains("possible use of uninitialized variable") && msg.ToLower().Contains("shader")) || msg.Contains("MoonTextMeshRenderer.cs:91");
	}

	// Token: 0x060020F1 RID: 8433 RVA: 0x000902AC File Offset: 0x0008E4AC
	public bool ShouldShowWarnings()
	{
		return Environment.MachineName == "MOONSTATION" || Environment.MachineName == "ARTHURBRUSSEE" || Environment.MachineName == "HENRYKOROL-PC" || Environment.MachineName == "DAVID-BEAST-PCC";
	}

	// Token: 0x04001BD7 RID: 7127
	private const float m_windowHeight = 0.5f;

	// Token: 0x04001BD8 RID: 7128
	private const float m_windowWidth = 1f;

	// Token: 0x04001BD9 RID: 7129
	public static LogCallbackHandler Instance;

	// Token: 0x04001BDA RID: 7130
	public List<LogCallbackHandler.LogEntry> LogEntries = new List<LogCallbackHandler.LogEntry>();

	// Token: 0x04001BDB RID: 7131
	private List<LogCallbackHandler.LogEntry> m_tempLogEntries = new List<LogCallbackHandler.LogEntry>();

	// Token: 0x04001BDC RID: 7132
	private bool m_showWindow = true;

	// Token: 0x04001BDD RID: 7133
	private bool m_autoScroll = true;

	// Token: 0x04001BDE RID: 7134
	private Vector2 m_scrollPosition;

	// Token: 0x04001BDF RID: 7135
	private bool m_showInfo;

	// Token: 0x04001BE0 RID: 7136
	private bool m_showWarnings;

	// Token: 0x04001BE1 RID: 7137
	private bool m_showErrors;

	// Token: 0x04001BE2 RID: 7138
	private float m_logEntryHeight = 100f;

	// Token: 0x04001BE3 RID: 7139
	private int m_logCount;

	// Token: 0x04001BE4 RID: 7140
	private int m_warningCount;

	// Token: 0x04001BE5 RID: 7141
	private int m_errorCount;

	// Token: 0x04001BE6 RID: 7142
	private int m_exceptionCount;

	// Token: 0x04001BE7 RID: 7143
	private int m_assertCount;

	// Token: 0x0200075C RID: 1884
	public class LogEntry
	{
		// Token: 0x06002C0A RID: 11274 RVA: 0x000BCF7A File Offset: 0x000BB17A
		public LogEntry(string condition, string stackTrace, LogType logType)
		{
			this.m_condition = condition;
			this.m_stackTrace = stackTrace;
			this.m_logType = logType;
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x000BCF97 File Offset: 0x000BB197
		public string Condition
		{
			get
			{
				return this.m_condition;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000BCF9F File Offset: 0x000BB19F
		public string StackTrace
		{
			get
			{
				return this.m_stackTrace;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x000BCFA7 File Offset: 0x000BB1A7
		public LogType LogType
		{
			get
			{
				return this.m_logType;
			}
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000BCFB0 File Offset: 0x000BB1B0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.LogType,
				"\\n",
				this.Condition,
				"\\n",
				this.StackTrace,
				"\\n"
			});
		}

		// Token: 0x040027C9 RID: 10185
		private string m_condition;

		// Token: 0x040027CA RID: 10186
		private string m_stackTrace;

		// Token: 0x040027CB RID: 10187
		private LogType m_logType;
	}
}
