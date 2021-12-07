using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Provides interaction with Windows event logs.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200021D RID: 541
	[System.ComponentModel.InstallerType(typeof(EventLogInstaller))]
	[MonitoringDescription("Represents an event log")]
	[System.ComponentModel.DefaultEvent("EntryWritten")]
	public class EventLog : System.ComponentModel.Component, System.ComponentModel.ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Does not associate the instance with any log.</summary>
		// Token: 0x06001232 RID: 4658 RVA: 0x00031060 File Offset: 0x0002F260
		public EventLog() : this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the local computer.</summary>
		/// <param name="logName">The name of the log on the local computer. </param>
		/// <exception cref="T:System.ArgumentNullException">The log name is null. </exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid. </exception>
		// Token: 0x06001233 RID: 4659 RVA: 0x00031070 File Offset: 0x0002F270
		public EventLog(string logName) : this(logName, ".")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the specified computer.</summary>
		/// <param name="logName">The name of the log on the specified computer. </param>
		/// <param name="machineName">The computer on which the log exists. </param>
		/// <exception cref="T:System.ArgumentNullException">The log name is null. </exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid.-or- The computer name is invalid. </exception>
		// Token: 0x06001234 RID: 4660 RVA: 0x00031080 File Offset: 0x0002F280
		public EventLog(string logName, string machineName) : this(logName, machineName, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the specified computer and creates or assigns the specified source to the <see cref="T:System.Diagnostics.EventLog" />.</summary>
		/// <param name="logName">The name of the log on the specified computer </param>
		/// <param name="machineName">The computer on which the log exists. </param>
		/// <param name="source">The source of event log entries. </param>
		/// <exception cref="T:System.ArgumentNullException">The log name is null. </exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid.-or- The computer name is invalid. </exception>
		// Token: 0x06001235 RID: 4661 RVA: 0x00031090 File Offset: 0x0002F290
		public EventLog(string logName, string machineName, string source)
		{
			if (logName == null)
			{
				throw new ArgumentNullException("logName");
			}
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'machineName'.", new object[]
				{
					machineName
				}));
			}
			this.source = source;
			this.machineName = machineName;
			this.logName = logName;
			this.Impl = EventLog.CreateEventLogImpl(this);
		}

		/// <summary>Occurs when an entry is written to an event log on the local computer.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06001236 RID: 4662 RVA: 0x0003110C File Offset: 0x0002F30C
		// (remove) Token: 0x06001237 RID: 4663 RVA: 0x00031128 File Offset: 0x0002F328
		[MonitoringDescription("Raised for each EventLog entry written.")]
		public event EntryWrittenEventHandler EntryWritten;

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Diagnostics.EventLog" /> receives <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event notifications.</summary>
		/// <returns>true if the <see cref="T:System.Diagnostics.EventLog" /> receives notification when an entry is written to the log; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The event log is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x00031144 File Offset: 0x0002F344
		// (set) Token: 0x06001239 RID: 4665 RVA: 0x0003114C File Offset: 0x0002F34C
		[System.ComponentModel.DefaultValue(false)]
		[MonitoringDescription("If enabled raises event when a log is written.")]
		[System.ComponentModel.Browsable(false)]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.doRaiseEvents;
			}
			set
			{
				if (value == this.doRaiseEvents)
				{
					return;
				}
				if (value)
				{
					this.Impl.EnableNotification();
				}
				else
				{
					this.Impl.DisableNotification();
				}
				this.doRaiseEvents = value;
			}
		}

		/// <summary>Gets the contents of the event log.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLogEntryCollection" /> holding the entries in the event log. Each entry is associated with an instance of the <see cref="T:System.Diagnostics.EventLogEntry" /> class.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x00031184 File Offset: 0x0002F384
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		[MonitoringDescription("The entries in the log.")]
		public EventLogEntryCollection Entries
		{
			get
			{
				return new EventLogEntryCollection(this.Impl);
			}
		}

		/// <summary>Gets or sets the name of the log to read from or write to.</summary>
		/// <returns>The name of the log. This can be Application, System, Security, or a custom log name. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x00031194 File Offset: 0x0002F394
		// (set) Token: 0x0600123C RID: 4668 RVA: 0x000311C0 File Offset: 0x0002F3C0
		[System.ComponentModel.DefaultValue("")]
		[MonitoringDescription("Name of the log that is read and written.")]
		[System.ComponentModel.TypeConverter("System.Diagnostics.Design.LogConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[System.ComponentModel.RecommendedAsConfigurable(true)]
		[System.ComponentModel.ReadOnly(true)]
		public string Log
		{
			get
			{
				if (this.source != null && this.source.Length > 0)
				{
					return this.GetLogName();
				}
				return this.logName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (string.Compare(this.logName, value, true) != 0)
				{
					this.logName = value;
					this.Reset();
				}
			}
		}

		/// <summary>Gets the event log's friendly name.</summary>
		/// <returns>A name that represents the event log in the system's event viewer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified <see cref="P:System.Diagnostics.EventLog.Log" /> does not exist in the registry for this computer.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x00031200 File Offset: 0x0002F400
		[System.ComponentModel.Browsable(false)]
		public string LogDisplayName
		{
			get
			{
				return this.Impl.LogDisplayName;
			}
		}

		/// <summary>Gets or sets the name of the computer on which to read or write events.</summary>
		/// <returns>The name of the server on which the event log resides. The default is the local computer (".").</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x00031210 File Offset: 0x0002F410
		// (set) Token: 0x0600123F RID: 4671 RVA: 0x00031218 File Offset: 0x0002F418
		[System.ComponentModel.ReadOnly(true)]
		[MonitoringDescription("Name of the machine that this log get written to.")]
		[System.ComponentModel.RecommendedAsConfigurable(true)]
		[System.ComponentModel.DefaultValue(".")]
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				if (value == null || value.Trim().Length == 0)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value {0} for property MachineName.", new object[]
					{
						value
					}));
				}
				if (string.Compare(this.machineName, value, true) != 0)
				{
					this.Close();
					this.machineName = value;
				}
			}
		}

		/// <summary>Gets or sets the source name to register and use when writing to the event log.</summary>
		/// <returns>The name registered with the event log as a source of entries. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The source name results in a registry key path longer than 254 characters.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x0003127C File Offset: 0x0002F47C
		// (set) Token: 0x06001241 RID: 4673 RVA: 0x00031284 File Offset: 0x0002F484
		[System.ComponentModel.ReadOnly(true)]
		[MonitoringDescription("The application name that writes the log.")]
		[System.ComponentModel.TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[System.ComponentModel.RecommendedAsConfigurable(true)]
		[System.ComponentModel.DefaultValue("")]
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.source == null || (this.source.Length == 0 && (this.logName == null || this.logName.Length == 0)))
				{
					this.source = value;
				}
				else if (string.Compare(this.source, value, true) != 0)
				{
					this.source = value;
					this.Reset();
				}
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls issued as a result of an <see cref="T:System.Diagnostics.EventLog" /> entry written event.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> used to marshal event-handler calls issued as a result of an <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event on the event log.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00031300 File Offset: 0x0002F500
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x00031308 File Offset: 0x0002F508
		[MonitoringDescription("An object that synchronizes event handler calls.")]
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DefaultValue(null)]
		public System.ComponentModel.ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Gets the configured behavior for storing new entries when the event log reaches its maximum log file size.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.OverflowAction" /> value that specifies the configured behavior for storing new entries when the event log reaches its maximum log size. The default is <see cref="F:System.Diagnostics.OverflowAction.OverwriteOlder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x00031314 File Offset: 0x0002F514
		[MonoTODO]
		[ComVisible(false)]
		[System.ComponentModel.Browsable(false)]
		public OverflowAction OverflowAction
		{
			get
			{
				return this.Impl.OverflowAction;
			}
		}

		/// <summary>Gets the number of days to retain entries in the event log.</summary>
		/// <returns>The number of days that entries in the event log are retained. The default value is 7.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x00031324 File Offset: 0x0002F524
		[MonoTODO]
		[System.ComponentModel.Browsable(false)]
		[ComVisible(false)]
		public int MinimumRetentionDays
		{
			get
			{
				return this.Impl.MinimumRetentionDays;
			}
		}

		/// <summary>Gets or sets the maximum event log size in kilobytes.</summary>
		/// <returns>The maximum event log size in kilobytes. The default is 512, indicating a maximum file size of 512 kilobytes.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than 64, or greater than 4194240, or not an even multiple of 64. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.EventLog.Log" /> value is not a valid log name.- or -The registry key for the event log could not be opened on the target computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x00031334 File Offset: 0x0002F534
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x00031344 File Offset: 0x0002F544
		[MonoTODO]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		[ComVisible(false)]
		public long MaximumKilobytes
		{
			get
			{
				return this.Impl.MaximumKilobytes;
			}
			set
			{
				this.Impl.MaximumKilobytes = value;
			}
		}

		/// <summary>Changes the configured behavior for writing new entries when the event log reaches its maximum file size.</summary>
		/// <param name="action">The overflow behavior for writing new entries to the event log. </param>
		/// <param name="retentionDays">The minimum number of days each event log entry is retained. This parameter is used only if <paramref name="action" /> is set to <see cref="F:System.Diagnostics.OverflowAction.OverwriteOlder" />. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="P:System.Diagnostics.EventLog.OverflowAction" /> value. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="retentionDays" /> is less than one, or larger than 365. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.EventLog.Log" /> value is not a valid log name.- or -The registry key for the event log could not be opened on the target computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001248 RID: 4680 RVA: 0x00031354 File Offset: 0x0002F554
		[ComVisible(false)]
		[MonoTODO]
		public void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			this.Impl.ModifyOverflowPolicy(action, retentionDays);
		}

		/// <summary>Specifies the localized name of the event log, which is displayed in the server Event Viewer.</summary>
		/// <param name="resourceFile">The fully specified path to a localized resource file. </param>
		/// <param name="resourceId">The resource identifier that indexes a localized string within the resource file. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.EventLog.Log" /> value is not a valid log name.- or -The registry key for the event log could not be opened on the target computer.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceFile " />is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001249 RID: 4681 RVA: 0x00031364 File Offset: 0x0002F564
		[MonoTODO]
		[ComVisible(false)]
		public void RegisterDisplayName(string resourceFile, long resourceId)
		{
			this.Impl.RegisterDisplayName(resourceFile, resourceId);
		}

		/// <summary>Begins the initialization of an <see cref="T:System.Diagnostics.EventLog" /> used on a form or used by another component. The initialization occurs at runtime.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="T:System.Diagnostics.EventLog" /> is already initialized.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600124A RID: 4682 RVA: 0x00031374 File Offset: 0x0002F574
		public void BeginInit()
		{
			this.Impl.BeginInit();
		}

		/// <summary>Removes all entries from the event log.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log was not cleared successfully.-or- The log cannot be opened. A Windows error code is not available. </exception>
		/// <exception cref="T:System.ArgumentException">A value is not specified for the <see cref="P:System.Diagnostics.EventLog.Log" /> property. Make sure the log name is not an empty string. </exception>
		/// <exception cref="T:System.InvalidOperationException">The log does not exist. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600124B RID: 4683 RVA: 0x00031384 File Offset: 0x0002F584
		public void Clear()
		{
			string log = this.Log;
			if (log == null || log.Length == 0)
			{
				throw new ArgumentException("Log property value has not been specified.");
			}
			if (!EventLog.Exists(log, this.MachineName))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' does not exist on computer '{1}'.", new object[]
				{
					log,
					this.machineName
				}));
			}
			this.Impl.Clear();
			this.Reset();
		}

		/// <summary>Closes the event log and releases read and write handles.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log's read handle or write handle was not released successfully. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600124C RID: 4684 RVA: 0x00031400 File Offset: 0x0002F600
		public void Close()
		{
			this.Impl.Close();
			this.EnableRaisingEvents = false;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00031414 File Offset: 0x0002F614
		internal void Reset()
		{
			bool enableRaisingEvents = this.EnableRaisingEvents;
			this.Close();
			this.EnableRaisingEvents = enableRaisingEvents;
		}

		/// <summary>Establishes the specified source name as a valid event source for writing entries to a log on the local computer. This method can also create a new custom log on the local computer.</summary>
		/// <param name="source">The source name by which the application is registered on the local computer. </param>
		/// <param name="logName">The name of the log the source's entries are written to. Possible values include Application, System, or a custom event log. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is an empty string ("") or null.- or - <paramref name="logName" /> is not a valid event log name. Event log names must consist of printable characters, and cannot include the characters '*', '?', or '\'.- or - <paramref name="logName" /> is not valid for user log creation. The event log names AppEvent, SysEvent, and SecEvent are reserved for system use.- or - The log name matches an existing event source name.- or - The source name results in a registry key path longer than 254 characters.- or - The first 8 characters of <paramref name="logName" /> match the first 8 characters of an existing event log name.- or - The source cannot be registered because it already exists on the local computer.- or - The source name matches an existing event log name. </exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the local computer. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600124E RID: 4686 RVA: 0x00031438 File Offset: 0x0002F638
		public static void CreateEventSource(string source, string logName)
		{
			EventLog.CreateEventSource(source, logName, ".");
		}

		/// <summary>Establishes the specified source name as a valid event source for writing entries to a log on the specified computer. This method can also be used to create a new custom log on the specified computer.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="logName">The name of the log the source's entries are written to. Possible values include Application, System, or a custom event log. If you do not specify a value, <paramref name="logName" /> defaults to Application. </param>
		/// <param name="machineName">The name of the computer to register this event source with, or "." for the local computer. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="machineName" /> is not a valid computer name.- or - <paramref name="source" /> is an empty string ("") or null.- or - <paramref name="logName" /> is not a valid event log name. Event log names must consist of printable characters, and cannot include the characters '*', '?', or '\'.- or - <paramref name="logName" /> is not valid for user log creation. The event log names AppEvent, SysEvent, and SecEvent are reserved for system use.- or - The log name matches an existing event source name.- or - The source name results in a registry key path longer than 254 characters.- or - The first 8 characters of <paramref name="logName" /> match the first 8 characters of an existing event log name on the specified computer.- or - The source cannot be registered because it already exists on the specified computer.- or - The source name matches an existing event source name. </exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the specified computer. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600124F RID: 4687 RVA: 0x00031448 File Offset: 0x0002F648
		[Obsolete("use CreateEventSource(EventSourceCreationData) instead")]
		public static void CreateEventSource(string source, string logName, string machineName)
		{
			EventLog.CreateEventSource(new EventSourceCreationData(source, logName, machineName));
		}

		/// <summary>Establishes a valid event source for writing localized event messages, using the specified configuration properties for the event source and the corresponding event log.</summary>
		/// <param name="sourceData">The configuration properties for the event source and its target event log. </param>
		/// <exception cref="T:System.ArgumentException">The computer name specified in <paramref name="sourceData" /> is not valid.- or - The source name specified in <paramref name="sourceData" /> is null.- or - The log name specified in <paramref name="sourceData" /> is not valid. Event log names must consist of printable characters and cannot include the characters '*', '?', or '\'.- or - The log name specified in <paramref name="sourceData" /> is not valid for user log creation. The Event log names AppEvent, SysEvent, and SecEvent are reserved for system use.- or - The log name matches an existing event source name.- or - The source name specified in <paramref name="sourceData" /> results in a registry key path longer than 254 characters.- or - The first 8 characters of the log name specified in <paramref name="sourceData" /> are not unique.- or - The source name specified in <paramref name="sourceData" /> is already registered.- or - The source name specified in <paramref name="sourceData" /> matches an existing event log name.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceData" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001250 RID: 4688 RVA: 0x00031458 File Offset: 0x0002F658
		[MonoNotSupported("remote machine is not supported")]
		public static void CreateEventSource(EventSourceCreationData sourceData)
		{
			if (sourceData.Source == null || sourceData.Source.Length == 0)
			{
				throw new ArgumentException("Source property value has not been specified.");
			}
			if (sourceData.LogName == null || sourceData.LogName.Length == 0)
			{
				throw new ArgumentException("Log property value has not been specified.");
			}
			if (EventLog.SourceExists(sourceData.Source, sourceData.MachineName))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Source '{0}' already exists on '{1}'.", new object[]
				{
					sourceData.Source,
					sourceData.MachineName
				}));
			}
			EventLogImpl eventLogImpl = EventLog.CreateEventLogImpl(sourceData.LogName, sourceData.MachineName, sourceData.Source);
			eventLogImpl.CreateEventSource(sourceData);
		}

		/// <summary>Removes an event log from the local computer.</summary>
		/// <param name="logName">The name of the log to delete. Possible values include: Application, Security, System, and any custom event logs on the computer. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="logName" /> is an empty string ("") or null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the local computer.- or - The log does not exist on the local computer. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log was not cleared successfully.-or- The log cannot be opened. A Windows error code is not available. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001251 RID: 4689 RVA: 0x00031514 File Offset: 0x0002F714
		public static void Delete(string logName)
		{
			EventLog.Delete(logName, ".");
		}

		/// <summary>Removes an event log from the specified computer.</summary>
		/// <param name="logName">The name of the log to delete. Possible values include: Application, Security, System, and any custom event logs on the specified computer. </param>
		/// <param name="machineName">The name of the computer to delete the log from, or "." for the local computer. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="logName" /> is an empty string ("") or null. - or - <paramref name="machineName" /> is not a valid computer name. </exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the specified computer.- or - The log does not exist on the specified computer. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log was not cleared successfully.-or- The log cannot be opened. A Windows error code is not available. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001252 RID: 4690 RVA: 0x00031524 File Offset: 0x0002F724
		[MonoNotSupported("remote machine is not supported")]
		public static void Delete(string logName, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException("Invalid format for argument machineName.");
			}
			if (logName == null || logName.Length == 0)
			{
				throw new ArgumentException("Log to delete was not specified.");
			}
			EventLogImpl eventLogImpl = EventLog.CreateEventLogImpl(logName, machineName, string.Empty);
			eventLogImpl.Delete(logName, machineName);
		}

		/// <summary>Removes the event source registration from the event log of the local computer.</summary>
		/// <param name="source">The name by which the application is registered in the event log system. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> parameter does not exist in the registry of the local computer.- or - You do not have write access on the registry key for the event log.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001253 RID: 4691 RVA: 0x00031584 File Offset: 0x0002F784
		public static void DeleteEventSource(string source)
		{
			EventLog.DeleteEventSource(source, ".");
		}

		/// <summary>Removes the application's event source registration from the specified computer.</summary>
		/// <param name="source">The name by which the application is registered in the event log system. </param>
		/// <param name="machineName">The name of the computer to remove the registration from, or "." for the local computer. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is invalid. - or - The <paramref name="source" /> parameter does not exist in the registry of the specified computer.- or - You do not have write access on the registry key for the event log.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> cannot be deleted because in the registry, the parent registry key for <paramref name="source" /> does not contain a subkey with the same name.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001254 RID: 4692 RVA: 0x00031594 File Offset: 0x0002F794
		[MonoNotSupported("remote machine is not supported")]
		public static void DeleteEventSource(string source, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'machineName'.", new object[]
				{
					machineName
				}));
			}
			EventLogImpl eventLogImpl = EventLog.CreateEventLogImpl(string.Empty, machineName, source);
			eventLogImpl.DeleteEventSource(source, machineName);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.EventLog" />, and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001255 RID: 4693 RVA: 0x000315EC File Offset: 0x0002F7EC
		protected override void Dispose(bool disposing)
		{
			if (this.Impl != null)
			{
				this.Impl.Dispose(disposing);
			}
		}

		/// <summary>Ends the initialization of an <see cref="T:System.Diagnostics.EventLog" /> used on a form or by another component. The initialization occurs at runtime.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001256 RID: 4694 RVA: 0x00031608 File Offset: 0x0002F808
		public void EndInit()
		{
			this.Impl.EndInit();
		}

		/// <summary>Determines whether the log exists on the local computer.</summary>
		/// <returns>true if the log exists on the local computer; otherwise, false.</returns>
		/// <param name="logName">The name of the log to search for. Possible values include: Application, Security, System, other application-specific logs (such as those associated with Active Directory), or any custom log on the computer. </param>
		/// <exception cref="T:System.ArgumentException">The logName is null or the value is empty. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001257 RID: 4695 RVA: 0x00031618 File Offset: 0x0002F818
		public static bool Exists(string logName)
		{
			return EventLog.Exists(logName, ".");
		}

		/// <summary>Determines whether the log exists on the specified computer.</summary>
		/// <returns>true if the log exists on the specified computer; otherwise, false.</returns>
		/// <param name="logName">The log for which to search. Possible values include: Application, Security, System, other application-specific logs (such as those associated with Active Directory), or any custom log on the computer. </param>
		/// <param name="machineName">The name of the computer on which to search for the log, or "." for the local computer. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is an invalid format. Make sure you have used proper syntax for the computer on which you are searching.-or- The <paramref name="logName" /> is null or the value is empty. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001258 RID: 4696 RVA: 0x00031628 File Offset: 0x0002F828
		[MonoNotSupported("remote machine is not supported")]
		public static bool Exists(string logName, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException("Invalid format for argument machineName.");
			}
			if (logName == null || logName.Length == 0)
			{
				return false;
			}
			EventLogImpl eventLogImpl = EventLog.CreateEventLogImpl(logName, machineName, string.Empty);
			return eventLogImpl.Exists(logName, machineName);
		}

		/// <summary>Searches for all event logs on the local computer and creates an array of <see cref="T:System.Diagnostics.EventLog" /> objects that contain the list.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.EventLog" /> that represents the logs on the local computer.</returns>
		/// <exception cref="T:System.SystemException">You do not have read access to the registry.-or- There is no event log service on the computer. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001259 RID: 4697 RVA: 0x00031680 File Offset: 0x0002F880
		public static EventLog[] GetEventLogs()
		{
			return EventLog.GetEventLogs(".");
		}

		/// <summary>Searches for all event logs on the given computer and creates an array of <see cref="T:System.Diagnostics.EventLog" /> objects that contain the list.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.EventLog" /> that represents the logs on the given computer.</returns>
		/// <param name="machineName">The computer on which to search for event logs. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is an invalid computer name. </exception>
		/// <exception cref="T:System.InvalidOperationException">You do not have read access to the registry.-or- There is no event log service on the computer. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600125A RID: 4698 RVA: 0x0003168C File Offset: 0x0002F88C
		[MonoNotSupported("remote machine is not supported")]
		public static EventLog[] GetEventLogs(string machineName)
		{
			EventLogImpl eventLogImpl = EventLog.CreateEventLogImpl(new EventLog());
			return eventLogImpl.GetEventLogs(machineName);
		}

		/// <summary>Gets the name of the log to which the specified source is registered.</summary>
		/// <returns>The name of the log associated with the specified source in the registry.</returns>
		/// <param name="source">The name of the event source. </param>
		/// <param name="machineName">The name of the computer on which to look, or "." for the local computer. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600125B RID: 4699 RVA: 0x000316AC File Offset: 0x0002F8AC
		[MonoNotSupported("remote machine is not supported")]
		public static string LogNameFromSourceName(string source, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'MachineName'.", new object[]
				{
					machineName
				}));
			}
			EventLogImpl eventLogImpl = EventLog.CreateEventLogImpl(string.Empty, machineName, source);
			return eventLogImpl.LogNameFromSourceName(source, machineName);
		}

		/// <summary>Determines whether an event source is registered on the local computer.</summary>
		/// <returns>true if the event source is registered on the local computer; otherwise, false.</returns>
		/// <param name="source">The name of the event source. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <paramref name="source" /> was not found, but some or all of the event logs could not be searched.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600125C RID: 4700 RVA: 0x00031704 File Offset: 0x0002F904
		public static bool SourceExists(string source)
		{
			return EventLog.SourceExists(source, ".");
		}

		/// <summary>Determines whether an event source is registered on a specified computer.</summary>
		/// <returns>true if the event source is registered on the given computer; otherwise, false.</returns>
		/// <param name="source">The name of the event source. </param>
		/// <param name="machineName">The name the computer on which to look, or "." for the local computer. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="machineName" /> is an invalid computer name. </exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <paramref name="source" /> was not found, but some or all of the event logs could not be searched.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600125D RID: 4701 RVA: 0x00031714 File Offset: 0x0002F914
		[MonoNotSupported("remote machine is not supported")]
		public static bool SourceExists(string source, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'machineName'.", new object[]
				{
					machineName
				}));
			}
			EventLogImpl eventLogImpl = EventLog.CreateEventLogImpl(string.Empty, machineName, source);
			return eventLogImpl.SourceExists(source, machineName);
		}

		/// <summary>Writes an information type entry, with the given message text, to the event log.</summary>
		/// <param name="message">The string to write to the event log. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" />  is not valid.- or -The source is already registered for a different event log.- or -The message string is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600125E RID: 4702 RVA: 0x0003176C File Offset: 0x0002F96C
		public void WriteEntry(string message)
		{
			this.WriteEntry(message, EventLogEntryType.Information);
		}

		/// <summary>Writes an error, warning, information, success audit, or failure audit entry with the given message text to the event log.</summary>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" />  is not valid.- or -The source is already registered for a different event log.- or -The message string is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600125F RID: 4703 RVA: 0x00031778 File Offset: 0x0002F978
		public void WriteEntry(string message, EventLogEntryType type)
		{
			this.WriteEntry(message, type, 0);
		}

		/// <summary>Writes an entry with the given message text and application-defined event identifier to the event log.</summary>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.- or -The source is already registered for a different event log.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001260 RID: 4704 RVA: 0x00031784 File Offset: 0x0002F984
		public void WriteEntry(string message, EventLogEntryType type, int eventID)
		{
			this.WriteEntry(message, type, eventID, 0);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log.</summary>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <param name="category">The application-specific subcategory associated with the message. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.- or -The source is already registered for a different event log.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001261 RID: 4705 RVA: 0x00031790 File Offset: 0x0002F990
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category)
		{
			this.WriteEntry(message, type, eventID, category, null);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log, and appends binary data to the message.</summary>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <param name="category">The application-specific subcategory associated with the message. </param>
		/// <param name="rawData">An array of bytes that holds the binary data associated with the entry. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.- or -The source is already registered for a different event log.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001262 RID: 4706 RVA: 0x000317A0 File Offset: 0x0002F9A0
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			this.WriteEntry(new string[]
			{
				message
			}, type, (long)eventID, category, rawData);
		}

		/// <summary>Writes an information type entry with the given message text to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -The message string is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001263 RID: 4707 RVA: 0x000317BC File Offset: 0x0002F9BC
		public static void WriteEntry(string source, string message)
		{
			EventLog.WriteEntry(source, message, EventLogEntryType.Information);
		}

		/// <summary>Writes an error, warning, information, success audit, or failure audit entry with the given message text to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -The message string is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001264 RID: 4708 RVA: 0x000317C8 File Offset: 0x0002F9C8
		public static void WriteEntry(string source, string message, EventLogEntryType type)
		{
			EventLog.WriteEntry(source, message, type, 0);
		}

		/// <summary>Writes an entry with the given message text and application-defined event identifier to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001265 RID: 4709 RVA: 0x000317D4 File Offset: 0x0002F9D4
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID)
		{
			EventLog.WriteEntry(source, message, type, eventID, 0);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log, using the specified registered event source. The <paramref name="category" /> can be used by the Event Viewer to filter events in the log.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <param name="category">The application-specific subcategory associated with the message. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001266 RID: 4710 RVA: 0x000317E0 File Offset: 0x0002F9E0
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category)
		{
			EventLog.WriteEntry(source, message, type, eventID, category, null);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log (using the specified registered event source) and appends binary data to the message.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <param name="category">The application-specific subcategory associated with the message. </param>
		/// <param name="rawData">An array of bytes that holds the binary data associated with the entry. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001267 RID: 4711 RVA: 0x000317F0 File Offset: 0x0002F9F0
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			using (EventLog eventLog = new EventLog())
			{
				eventLog.Source = source;
				eventLog.WriteEntry(message, type, eventID, category, rawData);
			}
		}

		/// <summary>Writes a localized entry to the event log.</summary>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry. </param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.- or -The source is already registered for a different event log.- or -<paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -<paramref name="values" /> has more than 256 elements.- or -One of the <paramref name="values" /> elements is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001268 RID: 4712 RVA: 0x00031848 File Offset: 0x0002FA48
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, params object[] values)
		{
			this.WriteEvent(instance, null, values);
		}

		/// <summary>Writes an event log entry with the given event data, message replacement strings, and associated binary data.</summary>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry. </param>
		/// <param name="data">An array of bytes that holds the binary data associated with the entry. </param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.- or -The source is already registered for a different event log.- or -<paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -<paramref name="values" /> has more than 256 elements.- or -One of the <paramref name="values" /> elements is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001269 RID: 4713 RVA: 0x00031854 File Offset: 0x0002FA54
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, byte[] data, params object[] values)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			string[] array;
			if (values != null)
			{
				array = new string[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i] == null)
					{
						array[i] = string.Empty;
					}
					else
					{
						array[i] = values[i].ToString();
					}
				}
			}
			else
			{
				array = new string[0];
			}
			this.WriteEntry(array, instance.EntryType, instance.InstanceId, (short)instance.CategoryId, data);
		}

		/// <summary>Writes an event log entry with the given event data and message replacement strings, using the specified registered event source.</summary>
		/// <param name="source">The name of the event source registered for the application on the specified computer. </param>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry. </param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -<paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -<paramref name="values" /> has more than 256 elements.- or -One of the <paramref name="values" /> elements is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600126A RID: 4714 RVA: 0x000318E0 File Offset: 0x0002FAE0
		public static void WriteEvent(string source, EventInstance instance, params object[] values)
		{
			EventLog.WriteEvent(source, instance, null, values);
		}

		/// <summary>Writes an event log entry with the given event data, message replacement strings, and associated binary data, and using the specified registered event source.</summary>
		/// <param name="source">The name of the event source registered for the application on the specified computer. </param>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry. </param>
		/// <param name="data">An array of bytes that holds the binary data associated with the entry. </param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -<paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -<paramref name="values" /> has more than 256 elements.- or -One of the <paramref name="values" /> elements is longer than 32766 bytes.- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600126B RID: 4715 RVA: 0x000318EC File Offset: 0x0002FAEC
		public static void WriteEvent(string source, EventInstance instance, byte[] data, params object[] values)
		{
			using (EventLog eventLog = new EventLog())
			{
				eventLog.Source = source;
				eventLog.WriteEvent(instance, data, values);
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00031940 File Offset: 0x0002FB40
		internal void OnEntryWritten(EventLogEntry newEntry)
		{
			if (this.doRaiseEvents && this.EntryWritten != null)
			{
				this.EntryWritten(this, new EntryWrittenEventArgs(newEntry));
			}
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00031978 File Offset: 0x0002FB78
		internal string GetLogName()
		{
			if (this.logName != null && this.logName.Length > 0)
			{
				return this.logName;
			}
			this.logName = EventLog.LogNameFromSourceName(this.source, this.machineName);
			return this.logName;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x000319C8 File Offset: 0x0002FBC8
		private static EventLogImpl CreateEventLogImpl(string logName, string machineName, string source)
		{
			EventLog eventLog = new EventLog(logName, machineName, source);
			return EventLog.CreateEventLogImpl(eventLog);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000319E4 File Offset: 0x0002FBE4
		private static EventLogImpl CreateEventLogImpl(EventLog eventLog)
		{
			string eventLogImplType = EventLog.EventLogImplType;
			switch (eventLogImplType)
			{
			case "local":
				return new LocalFileEventLog(eventLog);
			case "win32":
				return new Win32EventLog(eventLog);
			case "null":
				return new NullEventLog(eventLog);
			}
			throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Eventlog implementation '{0}' is not supported.", new object[]
			{
				EventLog.EventLogImplType
			}));
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00031A98 File Offset: 0x0002FC98
		private static bool Win32EventLogEnabled
		{
			get
			{
				return Environment.OSVersion.Platform == PlatformID.Win32NT;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00031AA8 File Offset: 0x0002FCA8
		private static string EventLogImplType
		{
			get
			{
				string text = Environment.GetEnvironmentVariable("MONO_EVENTLOG_TYPE");
				if (text == null)
				{
					if (EventLog.Win32EventLogEnabled)
					{
						return "win32";
					}
					text = "null";
				}
				else if (EventLog.Win32EventLogEnabled && string.Compare(text, "win32", true) == 0)
				{
					text = "win32";
				}
				else if (string.Compare(text, "null", true) == 0)
				{
					text = "null";
				}
				else
				{
					if (string.Compare(text, 0, "local", 0, "local".Length, true) != 0)
					{
						throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Eventlog implementation '{0}' is not supported.", new object[]
						{
							text
						}));
					}
					text = "local";
				}
				return text;
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00031B6C File Offset: 0x0002FD6C
		private void WriteEntry(string[] replacementStrings, EventLogEntryType type, long instanceID, short category, byte[] rawData)
		{
			if (this.Source.Length == 0)
			{
				throw new ArgumentException("Source property was not setbefore writing to the event log.");
			}
			if (!Enum.IsDefined(typeof(EventLogEntryType), type))
			{
				throw new System.ComponentModel.InvalidEnumArgumentException("type", (int)type, typeof(EventLogEntryType));
			}
			this.ValidateEventID(instanceID);
			if (!EventLog.SourceExists(this.Source, this.MachineName))
			{
				if (this.Log == null || this.Log.Length == 0)
				{
					this.Log = "Application";
				}
				EventLog.CreateEventSource(this.Source, this.Log, this.MachineName);
			}
			else if (this.logName != null && this.logName.Length != 0)
			{
				string text = EventLog.LogNameFromSourceName(this.Source, this.MachineName);
				if (string.Compare(this.logName, text, true, CultureInfo.InvariantCulture) != 0)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered in log '{1}' (it is registered in log '{2}'). The Source and Log properties must be matched, or you may set Log to the empty string, and it will automatically be matched to the Source property.", new object[]
					{
						this.Source,
						this.logName,
						text
					}));
				}
			}
			if (rawData == null)
			{
				rawData = new byte[0];
			}
			this.Impl.WriteEntry(replacementStrings, type, (uint)instanceID, category, rawData);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00031CBC File Offset: 0x0002FEBC
		private void ValidateEventID(long instanceID)
		{
			int eventID = EventLog.GetEventID(instanceID);
			if (eventID < 0 || eventID > 65535)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid eventID value '{0}'. It must be in the range between '{1}' and '{2}'.", new object[]
				{
					instanceID,
					0,
					ushort.MaxValue
				}));
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00031D1C File Offset: 0x0002FF1C
		internal static int GetEventID(long instanceID)
		{
			long num = (instanceID >= 0L) ? instanceID : (-instanceID);
			int num2 = (int)(num & 1073741823L);
			return (instanceID >= 0L) ? num2 : (-num2);
		}

		// Token: 0x04000532 RID: 1330
		internal const string LOCAL_FILE_IMPL = "local";

		// Token: 0x04000533 RID: 1331
		private const string WIN32_IMPL = "win32";

		// Token: 0x04000534 RID: 1332
		private const string NULL_IMPL = "null";

		// Token: 0x04000535 RID: 1333
		internal const string EVENTLOG_TYPE_VAR = "MONO_EVENTLOG_TYPE";

		// Token: 0x04000536 RID: 1334
		private string source;

		// Token: 0x04000537 RID: 1335
		private string logName;

		// Token: 0x04000538 RID: 1336
		private string machineName;

		// Token: 0x04000539 RID: 1337
		private bool doRaiseEvents;

		// Token: 0x0400053A RID: 1338
		private System.ComponentModel.ISynchronizeInvoke synchronizingObject;

		// Token: 0x0400053B RID: 1339
		private EventLogImpl Impl;
	}
}
