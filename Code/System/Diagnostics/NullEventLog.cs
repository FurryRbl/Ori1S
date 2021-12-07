using System;

namespace System.Diagnostics
{
	// Token: 0x02000233 RID: 563
	internal class NullEventLog : EventLogImpl
	{
		// Token: 0x06001355 RID: 4949 RVA: 0x00033E24 File Offset: 0x00032024
		public NullEventLog(EventLog coreEventLog) : base(coreEventLog)
		{
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00033E30 File Offset: 0x00032030
		public override void BeginInit()
		{
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00033E34 File Offset: 0x00032034
		public override void Clear()
		{
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00033E38 File Offset: 0x00032038
		public override void Close()
		{
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00033E3C File Offset: 0x0003203C
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00033E40 File Offset: 0x00032040
		public override void Delete(string logName, string machineName)
		{
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00033E44 File Offset: 0x00032044
		public override void DeleteEventSource(string source, string machineName)
		{
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00033E48 File Offset: 0x00032048
		public override void Dispose(bool disposing)
		{
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00033E4C File Offset: 0x0003204C
		public override void DisableNotification()
		{
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00033E50 File Offset: 0x00032050
		public override void EnableNotification()
		{
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00033E54 File Offset: 0x00032054
		public override void EndInit()
		{
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00033E58 File Offset: 0x00032058
		public override bool Exists(string logName, string machineName)
		{
			return true;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00033E5C File Offset: 0x0003205C
		protected override string FormatMessage(string source, uint messageID, string[] replacementStrings)
		{
			return string.Join(", ", replacementStrings);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00033E6C File Offset: 0x0003206C
		protected override int GetEntryCount()
		{
			return 0;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00033E70 File Offset: 0x00032070
		protected override EventLogEntry GetEntry(int index)
		{
			return null;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00033E74 File Offset: 0x00032074
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00033E84 File Offset: 0x00032084
		protected override string[] GetLogNames(string machineName)
		{
			return new string[0];
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00033E8C File Offset: 0x0003208C
		public override string LogNameFromSourceName(string source, string machineName)
		{
			return null;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00033E90 File Offset: 0x00032090
		public override bool SourceExists(string source, string machineName)
		{
			return false;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00033E94 File Offset: 0x00032094
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x00033E98 File Offset: 0x00032098
		public override OverflowAction OverflowAction
		{
			get
			{
				return OverflowAction.DoNotOverwrite;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x00033E9C File Offset: 0x0003209C
		public override int MinimumRetentionDays
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x00033EA4 File Offset: 0x000320A4
		// (set) Token: 0x0600136C RID: 4972 RVA: 0x00033EB0 File Offset: 0x000320B0
		public override long MaximumKilobytes
		{
			get
			{
				return long.MaxValue;
			}
			set
			{
				throw new NotSupportedException("This EventLog implementation does not support setting max kilobytes policy");
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00033EBC File Offset: 0x000320BC
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotSupportedException("This EventLog implementation does not support modifying overflow policy");
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00033EC8 File Offset: 0x000320C8
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotSupportedException("This EventLog implementation does not support registering display name");
		}
	}
}
