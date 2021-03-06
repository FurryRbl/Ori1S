using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides a pool of threads that can be used to post work items, process asynchronous I/O, wait on behalf of other threads, and process timers.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006B1 RID: 1713
	public static class ThreadPool
	{
		/// <summary>Binds an operating system handle to the <see cref="T:System.Threading.ThreadPool" />.</summary>
		/// <returns>true if the handle is bound; otherwise, false.</returns>
		/// <param name="osHandle">An <see cref="T:System.IntPtr" /> that holds the handle. The handle must have been opened for overlapped I/O on the unmanaged side. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004194 RID: 16788 RVA: 0x000E13AC File Offset: 0x000DF5AC
		[Obsolete("This method is obsolete, use BindHandle(SafeHandle) instead")]
		public static bool BindHandle(IntPtr osHandle)
		{
			return true;
		}

		/// <summary>Binds an operating system handle to the <see cref="T:System.Threading.ThreadPool" />.</summary>
		/// <returns>true if the handle is bound; otherwise, false.</returns>
		/// <param name="osHandle">A <see cref="T:System.Runtime.InteropServices.SafeHandle" />  that holds the operating system handle. The handle must have been opened for overlapped I/O on the unmanaged side.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="osHandle" /> is null. </exception>
		// Token: 0x06004195 RID: 16789 RVA: 0x000E13B0 File Offset: 0x000DF5B0
		public static bool BindHandle(SafeHandle osHandle)
		{
			if (osHandle == null)
			{
				throw new ArgumentNullException("osHandle");
			}
			return true;
		}

		/// <summary>Retrieves the difference between the maximum number of thread pool threads returned by the <see cref="M:System.Threading.ThreadPool.GetMaxThreads(System.Int32@,System.Int32@)" /> method, and the number currently active.</summary>
		/// <param name="workerThreads">The number of available worker threads. </param>
		/// <param name="completionPortThreads">The number of available asynchronous I/O threads. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004196 RID: 16790
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetAvailableThreads(out int workerThreads, out int completionPortThreads);

		/// <summary>Retrieves the number of requests to the thread pool that can be active concurrently. All requests above that number remain queued until thread pool threads become available.</summary>
		/// <param name="workerThreads">The maximum number of worker threads in the thread pool. </param>
		/// <param name="completionPortThreads">The maximum number of asynchronous I/O threads in the thread pool. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004197 RID: 16791
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetMaxThreads(out int workerThreads, out int completionPortThreads);

		/// <summary>Retrieves the number of idle threads the thread pool maintains in anticipation of new requests.</summary>
		/// <param name="workerThreads">The minimum number of idle worker threads currently maintained by the thread pool. </param>
		/// <param name="completionPortThreads">The minimum number of idle asynchronous I/O threads currently maintained by the thread pool. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004198 RID: 16792
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetMinThreads(out int workerThreads, out int completionPortThreads);

		/// <summary>Sets the number of idle threads the thread pool maintains in anticipation of new requests.</summary>
		/// <returns>true if the change is successful; otherwise, false.</returns>
		/// <param name="workerThreads">The new minimum number of idle worker threads to be maintained by the thread pool. </param>
		/// <param name="completionPortThreads">The new minimum number of idle asynchronous I/O threads to be maintained by the thread pool. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x06004199 RID: 16793
		[MonoTODO("The min number of completion port threads is not evaluated.")]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlThread\"/>\n</PermissionSet>\n")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SetMinThreads(int workerThreads, int completionPortThreads);

		/// <summary>Sets the number of requests to the thread pool that can be active concurrently. All requests above that number remain queued until thread pool threads become available.</summary>
		/// <returns>true if the change is successful; otherwise, false.</returns>
		/// <param name="workerThreads">The maximum number of worker threads in the thread pool. </param>
		/// <param name="completionPortThreads">The maximum number of asynchronous I/O threads in the thread pool. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x0600419A RID: 16794
		[MonoTODO("The max number of threads cannot be decremented.")]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlThread\"/>\n</PermissionSet>\n")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SetMaxThreads(int workerThreads, int completionPortThreads);

		/// <summary>Queues a method for execution. The method executes when a thread pool thread becomes available.</summary>
		/// <returns>true if the method is successfully queued; <see cref="T:System.NotSupportedException" /> is thrown if the work item is not queued.</returns>
		/// <param name="callBack">A <see cref="T:System.Threading.WaitCallback" /> that represents the method to be executed. </param>
		/// <exception cref="T:System.NotSupportedException">The common language runtime (CLR) is hosted, and the host does not support this action.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callBack" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600419B RID: 16795 RVA: 0x000E13C4 File Offset: 0x000DF5C4
		public static bool QueueUserWorkItem(WaitCallback callBack)
		{
			return ThreadPool.QueueUserWorkItem(callBack, null);
		}

		/// <summary>Queues a method for execution, and specifies an object containing data to be used by the method. The method executes when a thread pool thread becomes available.</summary>
		/// <returns>true if the method is successfully queued; <see cref="T:System.NotSupportedException" /> is thrown if the work item could not be queued.</returns>
		/// <param name="callBack">A <see cref="T:System.Threading.WaitCallback" /> representing the method to execute. </param>
		/// <param name="state">An object containing data to be used by the method. </param>
		/// <exception cref="T:System.NotSupportedException">The common language runtime (CLR) is hosted, and the host does not support this action.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callBack" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600419C RID: 16796 RVA: 0x000E13D0 File Offset: 0x000DF5D0
		public static bool QueueUserWorkItem(WaitCallback callBack, object state)
		{
			if (callBack == null)
			{
				throw new ArgumentNullException("callBack");
			}
			return callBack.BeginInvoke(state, null, null) != null;
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 32-bit signed integer for the time-out in milliseconds.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> that encapsulates the native handle.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The <see cref="T:System.Threading.WaitOrTimerCallback" /> delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object that is passed to the delegate. </param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600419D RID: 16797 RVA: 0x000E1404 File Offset: 0x000DF604
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (long)millisecondsTimeOutInterval, executeOnlyOnce);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 64-bit signed integer for the time-out in milliseconds.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> that encapsulates the native handle.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The <see cref="T:System.Threading.WaitOrTimerCallback" /> delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object passed to the delegate. </param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600419E RID: 16798 RVA: 0x000E1414 File Offset: 0x000DF614
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", "timeout < -1");
			}
			if (millisecondsTimeOutInterval > 2147483647L)
			{
				throw new NotSupportedException("Timeout is too big. Maximum is Int32.MaxValue");
			}
			TimeSpan timeout = new TimeSpan(0, 0, 0, 0, (int)millisecondsTimeOutInterval);
			RegisteredWaitHandle registeredWaitHandle = new RegisteredWaitHandle(waitObject, callBack, state, timeout, executeOnlyOnce);
			ThreadPool.QueueUserWorkItem(new WaitCallback(registeredWaitHandle.Wait), null);
			return registeredWaitHandle;
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> that encapsulates the native handle.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The <see cref="T:System.Threading.WaitOrTimerCallback" /> delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object passed to the delegate. </param>
		/// <param name="timeout">The time-out represented by a <see cref="T:System.TimeSpan" />. If <paramref name="timeout" /> is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="timeout" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is less than -1. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="timeout" /> parameter is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600419F RID: 16799 RVA: 0x000E1480 File Offset: 0x000DF680
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (long)timeout.TotalMilliseconds, executeOnlyOnce);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 32-bit unsigned integer for the time-out in milliseconds.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> that can be used to cancel the registered wait operation.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The <see cref="T:System.Threading.WaitOrTimerCallback" /> delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object passed to the delegate. </param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060041A0 RID: 16800 RVA: 0x000E1494 File Offset: 0x000DF694
		[CLSCompliant(false)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (long)((ulong)millisecondsTimeOutInterval), executeOnlyOnce);
		}

		/// <summary>Queues an overlapped I/O operation for execution.</summary>
		/// <returns>true if the operation was successfully queued to an I/O completion port; otherwise, false.</returns>
		/// <param name="overlapped">The <see cref="T:System.Threading.NativeOverlapped" /> structure to queue.</param>
		// Token: 0x060041A1 RID: 16801 RVA: 0x000E14A4 File Offset: 0x000DF6A4
		[CLSCompliant(false)]
		public unsafe static bool UnsafeQueueNativeOverlapped(NativeOverlapped* overlapped)
		{
			throw new NotImplementedException();
		}

		/// <summary>Queues the specified delegate to the thread pool, but does not propagate the calling stack to the worker thread.</summary>
		/// <returns>true if the method succeeds; <see cref="T:System.OutOfMemoryException" /> is thrown if the work item could not be queued.</returns>
		/// <param name="callBack">A <see cref="T:System.Threading.WaitCallback" /> that represents the delegate to invoke when a thread in the thread pool picks up the work item. </param>
		/// <param name="state">The object that is passed to the delegate when serviced from the thread pool. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ApplicationException">An out-of-memory condition was encountered.</exception>
		/// <exception cref="T:System.OutOfMemoryException">The work item could not be queued.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callBack" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x060041A2 RID: 16802 RVA: 0x000E14AC File Offset: 0x000DF6AC
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
		public static bool UnsafeQueueUserWorkItem(WaitCallback callBack, object state)
		{
			IAsyncResult asyncResult = null;
			try
			{
				if (!ExecutionContext.IsFlowSuppressed())
				{
					ExecutionContext.SuppressFlow();
				}
				asyncResult = callBack.BeginInvoke(state, null, null);
			}
			finally
			{
				if (ExecutionContext.IsFlowSuppressed())
				{
					ExecutionContext.RestoreFlow();
				}
			}
			return asyncResult != null;
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, using a 32-bit signed integer for the time-out in milliseconds.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> object that can be used to cancel the registered wait operation.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object that is passed to the delegate. </param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x060041A3 RID: 16803 RVA: 0x000E1510 File Offset: 0x000DF710
		[MonoTODO("Not implemented")]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			throw new NotImplementedException();
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 64-bit signed integer for the time-out in milliseconds.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> object that can be used to cancel the registered wait operation.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object that is passed to the delegate. </param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x060041A4 RID: 16804 RVA: 0x000E1518 File Offset: 0x000DF718
		[MonoTODO("Not implemented")]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			throw new NotImplementedException();
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> object that can be used to cancel the registered wait operation.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object that is passed to the delegate. </param>
		/// <param name="timeout">The time-out represented by a <see cref="T:System.TimeSpan" />. If <paramref name="timeout" /> is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="timeout" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is less than -1. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="timeout" /> parameter is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x060041A5 RID: 16805 RVA: 0x000E1520 File Offset: 0x000DF720
		[MonoTODO("Not implemented")]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			throw new NotImplementedException();
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 32-bit unsigned integer for the time-out in milliseconds.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> object that can be used to cancel the registered wait operation.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object that is passed to the delegate. </param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x060041A6 RID: 16806 RVA: 0x000E1528 File Offset: 0x000DF728
		[CLSCompliant(false)]
		[MonoTODO("Not implemented")]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			throw new NotImplementedException();
		}
	}
}
