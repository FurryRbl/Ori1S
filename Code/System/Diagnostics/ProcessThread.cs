using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Represents an operating system process thread.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200024B RID: 587
	[System.ComponentModel.Designer("System.Diagnostics.Design.ProcessThreadDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ProcessThread : System.ComponentModel.Component
	{
		// Token: 0x060014B1 RID: 5297 RVA: 0x0003700C File Offset: 0x0003520C
		[MonoTODO("Parse parameters")]
		internal ProcessThread()
		{
		}

		/// <summary>Gets the base priority of the thread.</summary>
		/// <returns>The base priority of the thread, which the operating system computes by combining the process priority class with the priority level of the associated thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x00037014 File Offset: 0x00035214
		[MonoTODO]
		[MonitoringDescription("The base priority of this thread.")]
		public int BasePriority
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the current priority of the thread.</summary>
		/// <returns>The current priority of the thread, which may deviate from the base priority based on how the operating system is scheduling the thread. The priority may be temporarily boosted for an active thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x00037018 File Offset: 0x00035218
		[MonoTODO]
		[MonitoringDescription("The current priority of this thread.")]
		public int CurrentPriority
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the unique identifier of the thread.</summary>
		/// <returns>The unique identifier associated with a specific thread.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0003701C File Offset: 0x0003521C
		[MonoTODO]
		[MonitoringDescription("The ID of this thread.")]
		public int Id
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Sets the preferred processor for this thread to run on.</summary>
		/// <returns>The preferred processor for the thread, used when the system schedules threads, to determine which processor to run the thread on.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The system could not set the thread to start on the specified processor. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004EB RID: 1259
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x00037020 File Offset: 0x00035220
		[System.ComponentModel.Browsable(false)]
		[MonoTODO]
		public int IdealProcessor
		{
			set
			{
			}
		}

		/// <summary>Gets or sets a value indicating whether the operating system should temporarily boost the priority of the associated thread whenever the main window of the thread's process receives the focus.</summary>
		/// <returns>true to boost the thread's priority when the user interacts with the process's interface; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The priority boost information could not be retrieved.-or-The priority boost information could not be set. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x00037024 File Offset: 0x00035224
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x00037028 File Offset: 0x00035228
		[MonoTODO]
		[MonitoringDescription("Thread gets a priority boot when interactively used by a user.")]
		public bool PriorityBoostEnabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the priority level of the thread.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.ThreadPriorityLevel" /> values, specifying a range that bounds the thread's priority.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread priority level information could not be retrieved. -or-The thread priority level could not be set.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0003702C File Offset: 0x0003522C
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x00037030 File Offset: 0x00035230
		[MonoTODO]
		[MonitoringDescription("The priority level of this thread.")]
		public ThreadPriorityLevel PriorityLevel
		{
			get
			{
				return ThreadPriorityLevel.Idle;
			}
			set
			{
			}
		}

		/// <summary>Gets the amount of time that the thread has spent running code inside the operating system core.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> indicating the amount of time that the thread has spent running code inside the operating system core.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread time could not be retrieved. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x00037034 File Offset: 0x00035234
		[MonoTODO]
		[MonitoringDescription("The amount of CPU time used in privileged mode.")]
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				return new TimeSpan(0L);
			}
		}

		/// <summary>Sets the processors on which the associated thread can run.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that points to a set of bits, each of which represents a processor that the thread can run on.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The processor affinity could not be set. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004EF RID: 1263
		// (set) Token: 0x060014BB RID: 5307 RVA: 0x00037040 File Offset: 0x00035240
		[MonoTODO]
		[System.ComponentModel.Browsable(false)]
		public IntPtr ProcessorAffinity
		{
			set
			{
			}
		}

		/// <summary>Gets the memory address of the function that the operating system called that started this thread.</summary>
		/// <returns>The thread's starting address, which points to the application-defined function that the thread executes.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x00037044 File Offset: 0x00035244
		[MonitoringDescription("The start address in memory of this thread.")]
		[MonoTODO]
		public IntPtr StartAddress
		{
			get
			{
				return (IntPtr)0;
			}
		}

		/// <summary>Gets the time that the operating system started the thread.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing the time that was on the system when the operating system started the thread.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread time could not be retrieved. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x0003704C File Offset: 0x0003524C
		[MonitoringDescription("The time this thread was started.")]
		[MonoTODO]
		public DateTime StartTime
		{
			get
			{
				return new DateTime(0L);
			}
		}

		/// <summary>Gets the current state of this thread.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.ThreadState" /> that indicates the thread's execution, for example, running, waiting, or terminated.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x00037058 File Offset: 0x00035258
		[MonitoringDescription("The current state of this thread.")]
		[MonoTODO]
		public ThreadState ThreadState
		{
			get
			{
				return ThreadState.Initialized;
			}
		}

		/// <summary>Gets the total amount of time that this thread has spent using the processor.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the amount of time that the thread has had control of the processor.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread time could not be retrieved. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x0003705C File Offset: 0x0003525C
		[MonitoringDescription("The total amount of CPU time used.")]
		[MonoTODO]
		public TimeSpan TotalProcessorTime
		{
			get
			{
				return new TimeSpan(0L);
			}
		}

		/// <summary>Gets the amount of time that the associated thread has spent running code inside the application.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> indicating the amount of time that the thread has spent running code inside the application, as opposed to inside the operating system core.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The thread time could not be retrieved. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x00037068 File Offset: 0x00035268
		[MonoTODO]
		[MonitoringDescription("The amount of CPU time used in user mode.")]
		public TimeSpan UserProcessorTime
		{
			get
			{
				return new TimeSpan(0L);
			}
		}

		/// <summary>Gets the reason that the thread is waiting.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.ThreadWaitReason" /> representing the reason that the thread is in the wait state.</returns>
		/// <exception cref="T:System.InvalidOperationException">The thread is not in the wait state. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x00037074 File Offset: 0x00035274
		[MonitoringDescription("The reason why this thread is waiting.")]
		[MonoTODO]
		public ThreadWaitReason WaitReason
		{
			get
			{
				return ThreadWaitReason.Executive;
			}
		}

		/// <summary>Resets the ideal processor for this thread to indicate that there is no single ideal processor. In other words, so that any processor is ideal.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The ideal processor could not be reset. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.NotSupportedException">The process is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060014C2 RID: 5314 RVA: 0x00037078 File Offset: 0x00035278
		[MonoTODO]
		public void ResetIdealProcessor()
		{
		}
	}
}
