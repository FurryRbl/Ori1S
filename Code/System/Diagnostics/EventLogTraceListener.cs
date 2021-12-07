using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a simple listener that directs tracing or debugging output to an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000229 RID: 553
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public sealed class EventLogTraceListener : TraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class without a trace listener.</summary>
		// Token: 0x060012D9 RID: 4825 RVA: 0x000328C4 File Offset: 0x00030AC4
		public EventLogTraceListener()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class using the specified event log.</summary>
		/// <param name="eventLog">An <see cref="T:System.Diagnostics.EventLog" /> that specifies the event log to write to. </param>
		// Token: 0x060012DA RID: 4826 RVA: 0x000328CC File Offset: 0x00030ACC
		public EventLogTraceListener(EventLog eventLog)
		{
			if (eventLog == null)
			{
				throw new ArgumentNullException("eventLog");
			}
			this.event_log = eventLog;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class using the specified source.</summary>
		/// <param name="source">The name of an existing event log source. </param>
		// Token: 0x060012DB RID: 4827 RVA: 0x000328EC File Offset: 0x00030AEC
		public EventLogTraceListener(string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.event_log = new EventLog();
			this.event_log.Source = source;
		}

		/// <summary>Gets or sets the event log to write to.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLog" /> that specifies the event log to write to.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00032928 File Offset: 0x00030B28
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x00032930 File Offset: 0x00030B30
		public EventLog EventLog
		{
			get
			{
				return this.event_log;
			}
			set
			{
				this.event_log = value;
			}
		}

		/// <summary>Gets or sets the name of this <see cref="T:System.Diagnostics.EventLogTraceListener" />.</summary>
		/// <returns>The name of this trace listener.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x0003293C File Offset: 0x00030B3C
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x00032960 File Offset: 0x00030B60
		public override string Name
		{
			get
			{
				return (this.name == null) ? this.event_log.Source : this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Closes the event log so that it no longer receives tracing or debugging output.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060012E0 RID: 4832 RVA: 0x0003296C File Offset: 0x00030B6C
		public override void Close()
		{
			this.event_log.Close();
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0003297C File Offset: 0x00030B7C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.event_log.Dispose();
			}
		}

		/// <summary>Writes a message to the event log for this instance.</summary>
		/// <param name="message">A message to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="message" /> exceeds 32,766 characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060012E2 RID: 4834 RVA: 0x00032990 File Offset: 0x00030B90
		public override void Write(string message)
		{
			this.TraceData(new TraceEventCache(), this.event_log.Source, TraceEventType.Information, 0, message);
		}

		/// <summary>Writes a message to the event log for this instance.</summary>
		/// <param name="message">The message to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="message" /> exceeds 32,766 characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060012E3 RID: 4835 RVA: 0x000329B8 File Offset: 0x00030BB8
		public override void WriteLine(string message)
		{
			this.Write(message);
		}

		/// <summary>Writes trace information, a data object and event information to the event log.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="data">A data object to write to the output file or stream.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.-or-The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x060012E4 RID: 4836 RVA: 0x000329C4 File Offset: 0x00030BC4
		[ComVisible(false)]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			EventLogEntryType type;
			switch (eventType)
			{
			case TraceEventType.Critical:
			case TraceEventType.Error:
				type = EventLogEntryType.Error;
				goto IL_34;
			case TraceEventType.Warning:
				type = EventLogEntryType.Warning;
				goto IL_34;
			}
			type = EventLogEntryType.Information;
			IL_34:
			this.event_log.WriteEntry((data == null) ? string.Empty : data.ToString(), type, id, 0);
		}

		/// <summary>Writes trace information, an array of data objects and event information to the event log.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="data">An array of data objects.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.-or-The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x060012E5 RID: 4837 RVA: 0x00032A2C File Offset: 0x00030C2C
		[ComVisible(false)]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			string data2 = string.Empty;
			if (data != null)
			{
				string[] array = new string[data.Length];
				for (int i = 0; i < data.Length; i++)
				{
					array[i] = ((data[i] == null) ? string.Empty : data[i].ToString());
				}
				data2 = string.Join(", ", array);
			}
			this.TraceData(eventCache, source, eventType, id, data2);
		}

		/// <summary>Writes trace information, a message and event information to the event log.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="message">The trace message.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.-or-The log entry string exceeds 32,766 characters.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060012E6 RID: 4838 RVA: 0x00032A9C File Offset: 0x00030C9C
		[ComVisible(false)]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			this.TraceData(eventCache, source, eventType, id, message);
		}

		/// <summary>Writes trace information, a formatted array of objects and event information to the event log.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="format">A format string that contains zero or more format items that correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.-or-The log entry string exceeds 32,766 characters.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060012E7 RID: 4839 RVA: 0x00032AAC File Offset: 0x00030CAC
		[ComVisible(false)]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			this.TraceEvent(eventCache, source, eventType, id, (format == null) ? null : string.Format(format, args));
		}

		// Token: 0x04000564 RID: 1380
		private EventLog event_log;

		// Token: 0x04000565 RID: 1381
		private string name;
	}
}
