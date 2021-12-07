using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that enable applications to trace the execution of code and associate trace messages with their source. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000262 RID: 610
	public class TraceSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSource" /> class, using the specified name for the source. </summary>
		/// <param name="name">The name of the source, typically the name of the application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").</exception>
		// Token: 0x060015A3 RID: 5539 RVA: 0x00038FE0 File Offset: 0x000371E0
		public TraceSource(string name) : this(name, SourceLevels.Off)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSource" /> class, using the specified name for the source and the default source level at which tracing is to occur.  </summary>
		/// <param name="name">The name of the source, typically the name of the application.</param>
		/// <param name="defaultLevel">A bitwise combination of the <see cref="T:System.Diagnostics.SourceLevels" /> values that specifies the default source level at which to trace.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").</exception>
		// Token: 0x060015A4 RID: 5540 RVA: 0x00038FEC File Offset: 0x000371EC
		public TraceSource(string name, SourceLevels sourceLevels)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Hashtable hashtable = DiagnosticsConfiguration.Settings["sources"] as Hashtable;
			TraceSourceInfo traceSourceInfo = (hashtable == null) ? null : (hashtable[name] as TraceSourceInfo);
			this.source_switch = new SourceSwitch(name);
			if (traceSourceInfo == null)
			{
				this.listeners = new TraceListenerCollection();
			}
			else
			{
				this.source_switch.Level = traceSourceInfo.Levels;
				this.listeners = traceSourceInfo.Listeners;
			}
		}

		/// <summary>Gets the custom switch attributes defined in the application configuration file.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> containing the custom attributes for the trace switch.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x00039080 File Offset: 0x00037280
		public System.Collections.Specialized.StringDictionary Attributes
		{
			get
			{
				return this.source_switch.Attributes;
			}
		}

		/// <summary>Gets the collection of trace listeners for the trace source.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.TraceListenerCollection" /> that contains the active trace listeners associated with the source. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x00039090 File Offset: 0x00037290
		public TraceListenerCollection Listeners
		{
			get
			{
				return this.listeners;
			}
		}

		/// <summary>Gets the name of the trace source.</summary>
		/// <returns>The name of the trace source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x00039098 File Offset: 0x00037298
		public string Name
		{
			get
			{
				return this.source_switch.DisplayName;
			}
		}

		/// <summary>Gets or sets the source switch value.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.SourceSwitch" /> object representing the source switch value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.TraceSource.Switch" /> is set to null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x000390A8 File Offset: 0x000372A8
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x000390B0 File Offset: 0x000372B0
		public SourceSwitch Switch
		{
			get
			{
				return this.source_switch;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.source_switch = value;
			}
		}

		/// <summary>Closes all the trace listeners in the trace listener collection.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060015AA RID: 5546 RVA: 0x000390CC File Offset: 0x000372CC
		public void Close()
		{
			object syncRoot = ((ICollection)this.listeners).SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.Close();
				}
			}
		}

		/// <summary>Flushes all the trace listeners in the trace listener collection.</summary>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015AB RID: 5547 RVA: 0x00039170 File Offset: 0x00037370
		public void Flush()
		{
			object syncRoot = ((ICollection)this.listeners).SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.Flush();
				}
			}
		}

		/// <summary>Writes trace data to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type, event identifier, and trace data.</summary>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">The trace data.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060015AC RID: 5548 RVA: 0x00039214 File Offset: 0x00037414
		[Conditional("TRACE")]
		public void TraceData(TraceEventType eventType, int id, object data)
		{
			if (!this.source_switch.ShouldTrace(eventType))
			{
				return;
			}
			object syncRoot = ((ICollection)this.listeners).SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.TraceData(null, this.Name, eventType, id, data);
				}
			}
		}

		/// <summary>Writes trace data to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type, event identifier, and trace data array.</summary>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">An object array containing the trace data.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060015AD RID: 5549 RVA: 0x000392D4 File Offset: 0x000374D4
		[Conditional("TRACE")]
		public void TraceData(TraceEventType eventType, int id, params object[] data)
		{
			if (!this.source_switch.ShouldTrace(eventType))
			{
				return;
			}
			object syncRoot = ((ICollection)this.listeners).SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.TraceData(null, this.Name, eventType, id, data);
				}
			}
		}

		/// <summary>Writes a trace event message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type and event identifier.</summary>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060015AE RID: 5550 RVA: 0x00039394 File Offset: 0x00037594
		[Conditional("TRACE")]
		public void TraceEvent(TraceEventType eventType, int id)
		{
			if (!this.source_switch.ShouldTrace(eventType))
			{
				return;
			}
			object syncRoot = ((ICollection)this.listeners).SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.TraceEvent(null, this.Name, eventType, id);
				}
			}
		}

		/// <summary>Writes a trace event message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type, event identifier, and message.</summary>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">The trace message to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060015AF RID: 5551 RVA: 0x00039454 File Offset: 0x00037654
		[Conditional("TRACE")]
		public void TraceEvent(TraceEventType eventType, int id, string message)
		{
			if (!this.source_switch.ShouldTrace(eventType))
			{
				return;
			}
			object syncRoot = ((ICollection)this.listeners).SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.TraceEvent(null, this.Name, eventType, id, message);
				}
			}
		}

		/// <summary>Writes a trace event to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified event type, event identifier, and argument array and format.</summary>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values that specifies the event type of the trace data.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A composite format string (see Remarks) that contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The number that indicates an argument to format is less than zero, or greater than or equal to the number of specified objects to format. </exception>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060015B0 RID: 5552 RVA: 0x00039514 File Offset: 0x00037714
		[Conditional("TRACE")]
		public void TraceEvent(TraceEventType eventType, int id, string format, params object[] args)
		{
			if (!this.source_switch.ShouldTrace(eventType))
			{
				return;
			}
			object syncRoot = ((ICollection)this.listeners).SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.TraceEvent(null, this.Name, eventType, id, format, args);
				}
			}
		}

		/// <summary>Writes an informational message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified message.</summary>
		/// <param name="message">The informative message to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060015B1 RID: 5553 RVA: 0x000395D8 File Offset: 0x000377D8
		[Conditional("TRACE")]
		public void TraceInformation(string format)
		{
		}

		/// <summary>Writes an informational message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified object array and formatting information.</summary>
		/// <param name="format">A composite format string (see Remarks) that contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An array containing zero or more objects to format.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The number that indicates an argument to format is less than zero, or greater than or equal to the number of specified objects to format. </exception>
		/// <exception cref="T:System.ObjectDisposedException">An attempt was made to trace an event during finalization.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060015B2 RID: 5554 RVA: 0x000395DC File Offset: 0x000377DC
		[Conditional("TRACE")]
		public void TraceInformation(string format, params object[] args)
		{
		}

		/// <summary>Writes a trace transfer message to the trace listeners in the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection using the specified numeric identifier, message, and related activity identifier.</summary>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">The trace message to write.</param>
		/// <param name="relatedActivityId">A <see cref="T:System.Guid" /> structure that identifies the related activity.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015B3 RID: 5555 RVA: 0x000395E0 File Offset: 0x000377E0
		[Conditional("TRACE")]
		public void TraceTransfer(int id, string message, Guid relatedActivityId)
		{
			if (!this.source_switch.ShouldTrace(TraceEventType.Transfer))
			{
				return;
			}
			object syncRoot = ((ICollection)this.listeners).SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.listeners)
				{
					TraceListener traceListener = (TraceListener)obj;
					traceListener.TraceTransfer(null, this.Name, id, message, relatedActivityId);
				}
			}
		}

		/// <summary>Gets the custom attributes supported by the trace source.</summary>
		/// <returns>A string array naming the custom attributes supported by the trace source, or null if there are no custom attributes.</returns>
		// Token: 0x060015B4 RID: 5556 RVA: 0x000396A4 File Offset: 0x000378A4
		protected virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		// Token: 0x040006B1 RID: 1713
		private SourceSwitch source_switch;

		// Token: 0x040006B2 RID: 1714
		private TraceListenerCollection listeners;
	}
}
