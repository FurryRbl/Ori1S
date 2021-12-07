using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Provides the abstract base class for the listeners who monitor trace and debug output.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000260 RID: 608
	public abstract class TraceListener : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		// Token: 0x0600157C RID: 5500 RVA: 0x00038B58 File Offset: 0x00036D58
		protected TraceListener() : this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceListener" /> class using the specified name as the listener.</summary>
		/// <param name="name">The name of the <see cref="T:System.Diagnostics.TraceListener" />. </param>
		// Token: 0x0600157D RID: 5501 RVA: 0x00038B68 File Offset: 0x00036D68
		protected TraceListener(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets or sets the indent level.</summary>
		/// <returns>The indent level. The default is zero.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00038B9C File Offset: 0x00036D9C
		// (set) Token: 0x0600157F RID: 5503 RVA: 0x00038BA4 File Offset: 0x00036DA4
		public int IndentLevel
		{
			get
			{
				return this.indentLevel;
			}
			set
			{
				this.indentLevel = value;
			}
		}

		/// <summary>Gets or sets the number of spaces in an indent.</summary>
		/// <returns>The number of spaces in an indent. The default is four spaces.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Set operation failed because the value is less than zero.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x00038BB0 File Offset: 0x00036DB0
		// (set) Token: 0x06001581 RID: 5505 RVA: 0x00038BB8 File Offset: 0x00036DB8
		public int IndentSize
		{
			get
			{
				return this.indentSize;
			}
			set
			{
				this.indentSize = value;
			}
		}

		/// <summary>Gets or sets a name for this <see cref="T:System.Diagnostics.TraceListener" />.</summary>
		/// <returns>A name for this <see cref="T:System.Diagnostics.TraceListener" />. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x00038BC4 File Offset: 0x00036DC4
		// (set) Token: 0x06001583 RID: 5507 RVA: 0x00038BCC File Offset: 0x00036DCC
		public virtual string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to indent the output.</summary>
		/// <returns>true if the output should be indented; otherwise, false.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x00038BD8 File Offset: 0x00036DD8
		// (set) Token: 0x06001585 RID: 5509 RVA: 0x00038BE0 File Offset: 0x00036DE0
		protected bool NeedIndent
		{
			get
			{
				return this.needIndent;
			}
			set
			{
				this.needIndent = value;
			}
		}

		/// <summary>Gets a value indicating whether the trace listener is thread safe. </summary>
		/// <returns>true if the trace listener is thread safe; otherwise, false. The default is false.</returns>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00038BEC File Offset: 0x00036DEC
		[MonoLimitation("This property exists but is never considered.")]
		public virtual bool IsThreadSafe
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, closes the output stream so it no longer receives tracing or debugging output.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001587 RID: 5511 RVA: 0x00038BF0 File Offset: 0x00036DF0
		public virtual void Close()
		{
			this.Dispose();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Diagnostics.TraceListener" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001588 RID: 5512 RVA: 0x00038BF8 File Offset: 0x00036DF8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.TraceListener" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001589 RID: 5513 RVA: 0x00038C08 File Offset: 0x00036E08
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Emits an error message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to emit. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600158A RID: 5514 RVA: 0x00038C0C File Offset: 0x00036E0C
		public virtual void Fail(string message)
		{
			this.Fail(message, string.Empty);
		}

		/// <summary>Emits an error message and a detailed error message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to emit. </param>
		/// <param name="detailMessage">A detailed message to emit. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600158B RID: 5515 RVA: 0x00038C1C File Offset: 0x00036E1C
		public virtual void Fail(string message, string detailMessage)
		{
			this.WriteLine("---- DEBUG ASSERTION FAILED ----");
			this.WriteLine("---- Assert Short Message ----");
			this.WriteLine(message);
			this.WriteLine("---- Assert Long Message ----");
			this.WriteLine(detailMessage);
			this.WriteLine(string.Empty);
		}

		/// <summary>When overridden in a derived class, flushes the output buffer.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600158C RID: 5516 RVA: 0x00038C64 File Offset: 0x00036E64
		public virtual void Flush()
		{
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600158D RID: 5517 RVA: 0x00038C68 File Offset: 0x00036E68
		public virtual void Write(object o)
		{
			this.Write(o.ToString());
		}

		/// <summary>When overridden in a derived class, writes the specified message to the listener you create in the derived class.</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600158E RID: 5518
		public abstract void Write(string message);

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600158F RID: 5519 RVA: 0x00038C78 File Offset: 0x00036E78
		public virtual void Write(object o, string category)
		{
			this.Write(o.ToString(), category);
		}

		/// <summary>Writes a category name and a message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001590 RID: 5520 RVA: 0x00038C88 File Offset: 0x00036E88
		public virtual void Write(string message, string category)
		{
			this.Write(category + ": " + message);
		}

		/// <summary>Writes the indent to the listener you create when you implement this class, and resets the <see cref="P:System.Diagnostics.TraceListener.NeedIndent" /> property to false.</summary>
		// Token: 0x06001591 RID: 5521 RVA: 0x00038C9C File Offset: 0x00036E9C
		protected virtual void WriteIndent()
		{
			this.NeedIndent = false;
			string message = new string(' ', this.IndentLevel * this.IndentSize);
			this.Write(message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class, followed by a line terminator.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001592 RID: 5522 RVA: 0x00038CCC File Offset: 0x00036ECC
		public virtual void WriteLine(object o)
		{
			this.WriteLine(o.ToString());
		}

		/// <summary>When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001593 RID: 5523
		public abstract void WriteLine(string message);

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class, followed by a line terminator.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001594 RID: 5524 RVA: 0x00038CDC File Offset: 0x00036EDC
		public virtual void WriteLine(object o, string category)
		{
			this.WriteLine(o.ToString(), category);
		}

		/// <summary>Writes a category name and a message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class, followed by a line terminator.</summary>
		/// <param name="message">A message to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001595 RID: 5525 RVA: 0x00038CEC File Offset: 0x00036EEC
		public virtual void WriteLine(string message, string category)
		{
			this.WriteLine(category + ": " + message);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00038D00 File Offset: 0x00036F00
		internal static string FormatArray(ICollection list, string joiner)
		{
			string[] array = new string[list.Count];
			int num = 0;
			foreach (object obj in list)
			{
				array[num++] = ((obj == null) ? string.Empty : obj.ToString());
			}
			return string.Join(joiner, array);
		}

		/// <summary>Writes trace information, a data object and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">The trace data to emit.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001597 RID: 5527 RVA: 0x00038D94 File Offset: 0x00036F94
		[ComVisible(false)]
		public virtual void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
			{
				return;
			}
			this.WriteLine(string.Format("{0} {1}: {2} : {3}", new object[]
			{
				source,
				eventType,
				id,
				data
			}));
			if (eventCache == null)
			{
				return;
			}
			if ((this.TraceOutputOptions & TraceOptions.ProcessId) != TraceOptions.None)
			{
				this.WriteLine("    ProcessId=" + eventCache.ProcessId);
			}
			if ((this.TraceOutputOptions & TraceOptions.LogicalOperationStack) != TraceOptions.None)
			{
				this.WriteLine("    LogicalOperationStack=" + TraceListener.FormatArray(eventCache.LogicalOperationStack, ", "));
			}
			if ((this.TraceOutputOptions & TraceOptions.ThreadId) != TraceOptions.None)
			{
				this.WriteLine("    ThreadId=" + eventCache.ThreadId);
			}
			if ((this.TraceOutputOptions & TraceOptions.DateTime) != TraceOptions.None)
			{
				this.WriteLine("    DateTime=" + eventCache.DateTime.ToString("o"));
			}
			if ((this.TraceOutputOptions & TraceOptions.Timestamp) != TraceOptions.None)
			{
				this.WriteLine("    Timestamp=" + eventCache.Timestamp);
			}
			if ((this.TraceOutputOptions & TraceOptions.Callstack) != TraceOptions.None)
			{
				this.WriteLine("    Callstack=" + eventCache.Callstack);
			}
		}

		/// <summary>Writes trace information, an array of data objects and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">An array of objects to emit as data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001598 RID: 5528 RVA: 0x00038EF8 File Offset: 0x000370F8
		[ComVisible(false)]
		public virtual void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				return;
			}
			this.TraceData(eventCache, source, eventType, id, TraceListener.FormatArray(data, " "));
		}

		/// <summary>Writes trace and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001599 RID: 5529 RVA: 0x00038F44 File Offset: 0x00037144
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			this.TraceEvent(eventCache, source, eventType, id, null);
		}

		/// <summary>Writes trace information, a message, and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600159A RID: 5530 RVA: 0x00038F54 File Offset: 0x00037154
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			this.TraceData(eventCache, source, eventType, id, message);
		}

		/// <summary>Writes trace information, a formatted array of objects and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600159B RID: 5531 RVA: 0x00038F64 File Offset: 0x00037164
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			this.TraceEvent(eventCache, source, eventType, id, string.Format(format, args));
		}

		/// <summary>Writes trace information, a message, a related activity identity and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		/// <param name="relatedActivityId"> A <see cref="T:System.Guid" />  object identifying a related activity.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600159C RID: 5532 RVA: 0x00038F7C File Offset: 0x0003717C
		[ComVisible(false)]
		public virtual void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			this.TraceEvent(eventCache, source, TraceEventType.Transfer, id, string.Format("{0}, relatedActivityId={1}", message, relatedActivityId));
		}

		/// <summary>Gets the custom attributes supported by the trace listener.</summary>
		/// <returns>A string array naming the custom attributes supported by the trace listener, or null if there are no custom attributes.</returns>
		// Token: 0x0600159D RID: 5533 RVA: 0x00038FAC File Offset: 0x000371AC
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		/// <summary>Gets the custom trace listener attributes defined in the application configuration file.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> containing the custom attributes for the trace listener.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00038FB0 File Offset: 0x000371B0
		public System.Collections.Specialized.StringDictionary Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets and sets the trace filter for the trace listener.</summary>
		/// <returns>An object derived from the <see cref="T:System.Diagnostics.TraceFilter" /> base class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x00038FB8 File Offset: 0x000371B8
		// (set) Token: 0x060015A0 RID: 5536 RVA: 0x00038FC0 File Offset: 0x000371C0
		[ComVisible(false)]
		public TraceFilter Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				this.filter = value;
			}
		}

		/// <summary>Gets or sets the trace output options.</summary>
		/// <returns>A bitwise combination of the enumeration values. The default is <see cref="F:System.Diagnostics.TraceOptions.None" />. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Set operation failed because the value is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x00038FCC File Offset: 0x000371CC
		// (set) Token: 0x060015A2 RID: 5538 RVA: 0x00038FD4 File Offset: 0x000371D4
		[ComVisible(false)]
		public TraceOptions TraceOutputOptions
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x040006A2 RID: 1698
		[ThreadStatic]
		private int indentLevel;

		// Token: 0x040006A3 RID: 1699
		[ThreadStatic]
		private int indentSize = 4;

		// Token: 0x040006A4 RID: 1700
		[ThreadStatic]
		private System.Collections.Specialized.StringDictionary attributes = new System.Collections.Specialized.StringDictionary();

		// Token: 0x040006A5 RID: 1701
		[ThreadStatic]
		private TraceFilter filter;

		// Token: 0x040006A6 RID: 1702
		[ThreadStatic]
		private TraceOptions options;

		// Token: 0x040006A7 RID: 1703
		private string name;

		// Token: 0x040006A8 RID: 1704
		private bool needIndent = true;
	}
}
