using System;
using System.Runtime.ConstrainedExecution;

namespace System.Threading
{
	/// <summary>Provides the basic functionality for propagating a synchronization context in various synchronization models. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006AC RID: 1708
	public class SynchronizationContext
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Threading.SynchronizationContext" /> class.</summary>
		// Token: 0x06004101 RID: 16641 RVA: 0x000E0618 File Offset: 0x000DE818
		public SynchronizationContext()
		{
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x000E0620 File Offset: 0x000DE820
		internal SynchronizationContext(SynchronizationContext context)
		{
			SynchronizationContext.currentContext = context;
		}

		/// <summary>Gets the synchronization context for the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.SynchronizationContext" /> object representing the current synchronization context.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004103 RID: 16643 RVA: 0x000E0630 File Offset: 0x000DE830
		public static SynchronizationContext Current
		{
			get
			{
				return SynchronizationContext.currentContext;
			}
		}

		/// <summary>When overridden in a derived class, creates a copy of the synchronization context.  </summary>
		/// <returns>A new <see cref="T:System.Threading.SynchronizationContext" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004104 RID: 16644 RVA: 0x000E0638 File Offset: 0x000DE838
		public virtual SynchronizationContext CreateCopy()
		{
			return new SynchronizationContext(this);
		}

		/// <summary>Determines if wait notification is required.</summary>
		/// <returns>true if wait notification is required; otherwise, false. </returns>
		// Token: 0x06004105 RID: 16645 RVA: 0x000E0640 File Offset: 0x000DE840
		public bool IsWaitNotificationRequired()
		{
			return this.notification_required;
		}

		/// <summary>When overridden in a derived class, responds to the notification that an operation has completed.</summary>
		// Token: 0x06004106 RID: 16646 RVA: 0x000E0648 File Offset: 0x000DE848
		public virtual void OperationCompleted()
		{
		}

		/// <summary>When overridden in a derived class, responds to the notification that an operation has started.</summary>
		// Token: 0x06004107 RID: 16647 RVA: 0x000E064C File Offset: 0x000DE84C
		public virtual void OperationStarted()
		{
		}

		/// <summary>When overridden in a derived class, dispatches an asynchronous message to a synchronization context.</summary>
		/// <param name="d">The <see cref="T:System.Threading.SendOrPostCallback" /> delegate to call.</param>
		/// <param name="state">The object passed to the delegate.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004108 RID: 16648 RVA: 0x000E0650 File Offset: 0x000DE850
		public virtual void Post(SendOrPostCallback d, object state)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(d.Invoke), state);
		}

		/// <summary>When overridden in a derived class, dispatches a synchronous message to a synchronization context.</summary>
		/// <param name="d">The <see cref="T:System.Threading.SendOrPostCallback" /> delegate to call.</param>
		/// <param name="state">The object passed to the delegate.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004109 RID: 16649 RVA: 0x000E0668 File Offset: 0x000DE868
		public virtual void Send(SendOrPostCallback d, object state)
		{
			d(state);
		}

		/// <summary>Sets the current synchronization context.</summary>
		/// <param name="syncContext">The <see cref="T:System.Threading.SynchronizationContext" /> object to be set.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x0600410A RID: 16650 RVA: 0x000E0674 File Offset: 0x000DE874
		public static void SetSynchronizationContext(SynchronizationContext syncContext)
		{
			SynchronizationContext.currentContext = syncContext;
		}

		/// <summary>Sets notification that wait notification is required and prepares the callback method so it can be called more reliably when a wait occurs.</summary>
		// Token: 0x0600410B RID: 16651 RVA: 0x000E067C File Offset: 0x000DE87C
		[MonoTODO]
		protected void SetWaitNotificationRequired()
		{
			this.notification_required = true;
			throw new NotImplementedException();
		}

		/// <summary>Waits for any or all the elements in the specified array to receive a signal.</summary>
		/// <returns>The array index of the object that satisfied the wait.</returns>
		/// <param name="waitHandles">An array of type <see cref="T:System.IntPtr" /> that contains the native operating system handles.</param>
		/// <param name="waitAll">true to wait for all handles; false to wait for any handle. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x0600410C RID: 16652 RVA: 0x000E068C File Offset: 0x000DE88C
		[CLSCompliant(false)]
		[PrePrepareMethod]
		public virtual int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			return SynchronizationContext.WaitHelper(waitHandles, waitAll, millisecondsTimeout);
		}

		/// <summary>Helper function that waits for any or all the elements in the specified array to receive a signal.</summary>
		/// <returns>The array index of the object that satisfied the wait.</returns>
		/// <param name="waitHandles">An array of type <see cref="T:System.IntPtr" /> that contains the native operating system handles.</param>
		/// <param name="waitAll">true to wait for all handles;  false to wait for any handle. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		// Token: 0x0600410D RID: 16653 RVA: 0x000E0698 File Offset: 0x000DE898
		[CLSCompliant(false)]
		[PrePrepareMethod]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MonoTODO]
		protected static int WaitHelper(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001BD4 RID: 7124
		private bool notification_required;

		// Token: 0x04001BD5 RID: 7125
		[ThreadStatic]
		private static SynchronizationContext currentContext;
	}
}
