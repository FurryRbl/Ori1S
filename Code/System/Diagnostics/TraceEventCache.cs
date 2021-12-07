using System;
using System.Collections;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides trace event data specific to a thread and a process.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000259 RID: 601
	public class TraceEventCache
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceEventCache" /> class. </summary>
		// Token: 0x0600152A RID: 5418 RVA: 0x00037A70 File Offset: 0x00035C70
		public TraceEventCache()
		{
			this.started = DateTime.Now;
			this.manager = Trace.CorrelationManager;
			this.callstack = Environment.StackTrace;
			this.timestamp = Stopwatch.GetTimestamp();
			this.thread = Thread.CurrentThread.Name;
			this.process = Process.GetCurrentProcess().Id;
		}

		/// <summary>Gets the call stack for the current thread.</summary>
		/// <returns>A string containing stack trace information. This value can be an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" />
		/// </PermissionSet>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x00037AD0 File Offset: 0x00035CD0
		public string Callstack
		{
			get
			{
				return this.callstack;
			}
		}

		/// <summary>Gets the date and time at which the event trace occurred.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> structure whose value is a date and time expressed in Coordinated Universal Time (UTC).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00037AD8 File Offset: 0x00035CD8
		public DateTime DateTime
		{
			get
			{
				return this.started;
			}
		}

		/// <summary>Gets the correlation data, contained in a stack. </summary>
		/// <returns>A <see cref="T:System.Collections.Stack" /> containing correlation data.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x00037AE0 File Offset: 0x00035CE0
		public Stack LogicalOperationStack
		{
			get
			{
				return this.manager.LogicalOperationStack;
			}
		}

		/// <summary>Gets the unique identifier of the current process.</summary>
		/// <returns>The system-generated unique identifier of the current process.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00037AF0 File Offset: 0x00035CF0
		public int ProcessId
		{
			get
			{
				return this.process;
			}
		}

		/// <summary>Gets a unique identifier for the current managed thread.  </summary>
		/// <returns>A string that represents a unique integer identifier for this managed thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x00037AF8 File Offset: 0x00035CF8
		public string ThreadId
		{
			get
			{
				return this.thread;
			}
		}

		/// <summary>Gets the current number of ticks in the timer mechanism.</summary>
		/// <returns>The tick counter value of the underlying timer mechanism.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x00037B00 File Offset: 0x00035D00
		public long Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x0400067E RID: 1662
		private DateTime started;

		// Token: 0x0400067F RID: 1663
		private CorrelationManager manager;

		// Token: 0x04000680 RID: 1664
		private string callstack;

		// Token: 0x04000681 RID: 1665
		private string thread;

		// Token: 0x04000682 RID: 1666
		private int process;

		// Token: 0x04000683 RID: 1667
		private long timestamp;
	}
}
