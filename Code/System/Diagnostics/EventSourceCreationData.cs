using System;

namespace System.Diagnostics
{
	/// <summary>Represents the configuration settings used to create an event log source on the local computer or a remote computer.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022A RID: 554
	public class EventSourceCreationData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSourceCreationData" /> class with a specified event source and event log name.</summary>
		/// <param name="source">The name to register with the event log as a source of entries. </param>
		/// <param name="logName">The name of the log to which entries from the source are written. </param>
		// Token: 0x060012E8 RID: 4840 RVA: 0x00032ADC File Offset: 0x00030CDC
		public EventSourceCreationData(string source, string logName)
		{
			this._source = source;
			this._logName = logName;
			this._machineName = ".";
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00032B00 File Offset: 0x00030D00
		internal EventSourceCreationData(string source, string logName, string machineName)
		{
			this._source = source;
			if (logName == null || logName.Length == 0)
			{
				this._logName = "Application";
			}
			else
			{
				this._logName = logName;
			}
			this._machineName = machineName;
		}

		/// <summary>Gets or sets the number of categories in the category resource file.</summary>
		/// <returns>The number of categories in the category resource file. The default value is zero.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a negative value or to a value larger than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x00032B4C File Offset: 0x00030D4C
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x00032B54 File Offset: 0x00030D54
		public int CategoryCount
		{
			get
			{
				return this._categoryCount;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._categoryCount = value;
			}
		}

		/// <summary>Gets or sets the path of the resource file that contains category strings for the source.</summary>
		/// <returns>The path of the category resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x00032B70 File Offset: 0x00030D70
		// (set) Token: 0x060012ED RID: 4845 RVA: 0x00032B78 File Offset: 0x00030D78
		public string CategoryResourceFile
		{
			get
			{
				return this._categoryResourceFile;
			}
			set
			{
				this._categoryResourceFile = value;
			}
		}

		/// <summary>Gets or sets the name of the event log to which the source writes entries.</summary>
		/// <returns>The name of the event log. This can be Application, System, or a custom log name. The default value is "Application."</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00032B84 File Offset: 0x00030D84
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x00032B8C File Offset: 0x00030D8C
		public string LogName
		{
			get
			{
				return this._logName;
			}
			set
			{
				this._logName = value;
			}
		}

		/// <summary>Gets or sets the name of the computer on which to register the event source.</summary>
		/// <returns>The name of the system on which to register the event source. The default is the local computer (".").</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00032B98 File Offset: 0x00030D98
		// (set) Token: 0x060012F1 RID: 4849 RVA: 0x00032BA0 File Offset: 0x00030DA0
		public string MachineName
		{
			get
			{
				return this._machineName;
			}
			set
			{
				this._machineName = value;
			}
		}

		/// <summary>Gets or sets the path of the message resource file that contains message formatting strings for the source.</summary>
		/// <returns>The path of the message resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00032BAC File Offset: 0x00030DAC
		// (set) Token: 0x060012F3 RID: 4851 RVA: 0x00032BB4 File Offset: 0x00030DB4
		public string MessageResourceFile
		{
			get
			{
				return this._messageResourceFile;
			}
			set
			{
				this._messageResourceFile = value;
			}
		}

		/// <summary>Gets or sets the path of the resource file that contains message parameter strings for the source.</summary>
		/// <returns>The path of the parameter resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x00032BC0 File Offset: 0x00030DC0
		// (set) Token: 0x060012F5 RID: 4853 RVA: 0x00032BC8 File Offset: 0x00030DC8
		public string ParameterResourceFile
		{
			get
			{
				return this._parameterResourceFile;
			}
			set
			{
				this._parameterResourceFile = value;
			}
		}

		/// <summary>Gets or sets the name to register with the event log as an event source.</summary>
		/// <returns>The name to register with the event log as a source of entries. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00032BD4 File Offset: 0x00030DD4
		// (set) Token: 0x060012F7 RID: 4855 RVA: 0x00032BDC File Offset: 0x00030DDC
		public string Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		// Token: 0x04000566 RID: 1382
		private string _source;

		// Token: 0x04000567 RID: 1383
		private string _logName;

		// Token: 0x04000568 RID: 1384
		private string _machineName;

		// Token: 0x04000569 RID: 1385
		private string _messageResourceFile;

		// Token: 0x0400056A RID: 1386
		private string _parameterResourceFile;

		// Token: 0x0400056B RID: 1387
		private string _categoryResourceFile;

		// Token: 0x0400056C RID: 1388
		private int _categoryCount;
	}
}
