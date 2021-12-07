using System;
using System.Globalization;

namespace System.Diagnostics
{
	// Token: 0x02000222 RID: 546
	internal abstract class EventLogImpl
	{
		// Token: 0x06001294 RID: 4756 RVA: 0x000320B0 File Offset: 0x000302B0
		protected EventLogImpl(EventLog coreEventLog)
		{
			this._coreEventLog = coreEventLog;
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x000320C0 File Offset: 0x000302C0
		protected EventLog CoreEventLog
		{
			get
			{
				return this._coreEventLog;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x000320C8 File Offset: 0x000302C8
		public int EntryCount
		{
			get
			{
				if (this._coreEventLog.Log == null || this._coreEventLog.Log.Length == 0)
				{
					throw new ArgumentException("Log property is not set.");
				}
				if (!EventLog.Exists(this._coreEventLog.Log, this._coreEventLog.MachineName))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The event log '{0}' on  computer '{1}' does not exist.", new object[]
					{
						this._coreEventLog.Log,
						this._coreEventLog.MachineName
					}));
				}
				return this.GetEntryCount();
			}
		}

		// Token: 0x1700043F RID: 1087
		public EventLogEntry this[int index]
		{
			get
			{
				if (this._coreEventLog.Log == null || this._coreEventLog.Log.Length == 0)
				{
					throw new ArgumentException("Log property is not set.");
				}
				if (!EventLog.Exists(this._coreEventLog.Log, this._coreEventLog.MachineName))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The event log '{0}' on  computer '{1}' does not exist.", new object[]
					{
						this._coreEventLog.Log,
						this._coreEventLog.MachineName
					}));
				}
				if (index < 0 || index >= this.EntryCount)
				{
					throw new ArgumentException("Index out of range");
				}
				return this.GetEntry(index);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00032220 File Offset: 0x00030420
		public string LogDisplayName
		{
			get
			{
				if (this._coreEventLog.Log != null && this._coreEventLog.Log.Length == 0)
				{
					throw new InvalidOperationException("Event log names must consist of printable characters and cannot contain \\, *, ?, or spaces.");
				}
				if (this._coreEventLog.Log != null)
				{
					if (this._coreEventLog.Log.Length == 0)
					{
						return string.Empty;
					}
					if (!EventLog.Exists(this._coreEventLog.Log, this._coreEventLog.MachineName))
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Cannot find Log {0} on computer {1}.", new object[]
						{
							this._coreEventLog.Log,
							this._coreEventLog.MachineName
						}));
					}
				}
				return this.GetLogDisplayName();
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000322E8 File Offset: 0x000304E8
		public EventLogEntry[] GetEntries()
		{
			string log = this.CoreEventLog.Log;
			if (log == null || log.Length == 0)
			{
				throw new ArgumentException("Log property value has not been specified.");
			}
			if (!EventLog.Exists(log))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The event log '{0}' on  computer '{1}' does not exist.", new object[]
				{
					log,
					this._coreEventLog.MachineName
				}));
			}
			int entryCount = this.GetEntryCount();
			EventLogEntry[] array = new EventLogEntry[entryCount];
			for (int i = 0; i < entryCount; i++)
			{
				array[i] = this.GetEntry(i);
			}
			return array;
		}

		// Token: 0x0600129A RID: 4762
		public abstract void DisableNotification();

		// Token: 0x0600129B RID: 4763
		public abstract void EnableNotification();

		// Token: 0x0600129C RID: 4764
		public abstract void BeginInit();

		// Token: 0x0600129D RID: 4765
		public abstract void Clear();

		// Token: 0x0600129E RID: 4766
		public abstract void Close();

		// Token: 0x0600129F RID: 4767
		public abstract void CreateEventSource(EventSourceCreationData sourceData);

		// Token: 0x060012A0 RID: 4768
		public abstract void Delete(string logName, string machineName);

		// Token: 0x060012A1 RID: 4769
		public abstract void DeleteEventSource(string source, string machineName);

		// Token: 0x060012A2 RID: 4770
		public abstract void Dispose(bool disposing);

		// Token: 0x060012A3 RID: 4771
		public abstract void EndInit();

		// Token: 0x060012A4 RID: 4772
		public abstract bool Exists(string logName, string machineName);

		// Token: 0x060012A5 RID: 4773
		protected abstract int GetEntryCount();

		// Token: 0x060012A6 RID: 4774
		protected abstract EventLogEntry GetEntry(int index);

		// Token: 0x060012A7 RID: 4775 RVA: 0x00032380 File Offset: 0x00030580
		public EventLog[] GetEventLogs(string machineName)
		{
			string[] logNames = this.GetLogNames(machineName);
			EventLog[] array = new EventLog[logNames.Length];
			for (int i = 0; i < logNames.Length; i++)
			{
				EventLog eventLog = new EventLog(logNames[i], machineName);
				array[i] = eventLog;
			}
			return array;
		}

		// Token: 0x060012A8 RID: 4776
		protected abstract string GetLogDisplayName();

		// Token: 0x060012A9 RID: 4777
		public abstract string LogNameFromSourceName(string source, string machineName);

		// Token: 0x060012AA RID: 4778
		public abstract bool SourceExists(string source, string machineName);

		// Token: 0x060012AB RID: 4779
		public abstract void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData);

		// Token: 0x060012AC RID: 4780
		protected abstract string FormatMessage(string source, uint messageID, string[] replacementStrings);

		// Token: 0x060012AD RID: 4781
		protected abstract string[] GetLogNames(string machineName);

		// Token: 0x060012AE RID: 4782 RVA: 0x000323C4 File Offset: 0x000305C4
		protected void ValidateCustomerLogName(string logName, string machineName)
		{
			if (logName.Length >= 8)
			{
				string text = logName.Substring(0, 8);
				if (string.Compare(text, "AppEvent", true) == 0 || string.Compare(text, "SysEvent", true) == 0 || string.Compare(text, "SecEvent", true) == 0)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The log name: '{0}' is invalid for customer log creation.", new object[]
					{
						logName
					}));
				}
				foreach (string text2 in this.GetLogNames(machineName))
				{
					if (text2.Length >= 8 && string.Compare(text2, 0, text, 0, 8, true) == 0)
					{
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Only the first eight characters of a custom log name are significant, and there is already another log on the system using the first eight characters of the name given. Name given: '{0}', name of existing log: '{1}'.", new object[]
						{
							logName,
							text2
						}));
					}
				}
			}
			if (!this.SourceExists(logName, machineName))
			{
				return;
			}
			if (machineName == ".")
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Log {0} has already been registered as a source on the local computer.", new object[]
				{
					logName
				}));
			}
			throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Log {0} has already been registered as a source on the computer {1}.", new object[]
			{
				logName,
				machineName
			}));
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060012AF RID: 4783
		public abstract OverflowAction OverflowAction { get; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060012B0 RID: 4784
		public abstract int MinimumRetentionDays { get; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060012B1 RID: 4785
		// (set) Token: 0x060012B2 RID: 4786
		public abstract long MaximumKilobytes { get; set; }

		// Token: 0x060012B3 RID: 4787
		public abstract void ModifyOverflowPolicy(OverflowAction action, int retentionDays);

		// Token: 0x060012B4 RID: 4788
		public abstract void RegisterDisplayName(string resourceFile, long resourceId);

		// Token: 0x04000556 RID: 1366
		private readonly EventLog _coreEventLog;
	}
}
