using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents a handle that has been registered when calling <see cref="M:System.Threading.ThreadPool.RegisterWaitForSingleObject(System.Threading.WaitHandle,System.Threading.WaitOrTimerCallback,System.Object,System.UInt32,System.Boolean)" />. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006AB RID: 1707
	[ComVisible(true)]
	public sealed class RegisteredWaitHandle : MarshalByRefObject
	{
		// Token: 0x060040FD RID: 16637 RVA: 0x000E0378 File Offset: 0x000DE578
		internal RegisteredWaitHandle(WaitHandle waitObject, WaitOrTimerCallback callback, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			this._waitObject = waitObject;
			this._callback = callback;
			this._state = state;
			this._timeout = timeout;
			this._executeOnlyOnce = executeOnlyOnce;
			this._finalEvent = null;
			this._cancelEvent = new ManualResetEvent(false);
			this._callsInProcess = 0;
			this._unregistered = false;
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x000E03D4 File Offset: 0x000DE5D4
		internal void Wait(object state)
		{
			try
			{
				WaitHandle[] waitHandles = new WaitHandle[]
				{
					this._waitObject,
					this._cancelEvent
				};
				do
				{
					int num = WaitHandle.WaitAny(waitHandles, this._timeout, false);
					if (!this._unregistered)
					{
						lock (this)
						{
							this._callsInProcess++;
						}
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoCallBack), num == 258);
					}
				}
				while (!this._unregistered && !this._executeOnlyOnce);
			}
			catch
			{
			}
			lock (this)
			{
				this._unregistered = true;
				if (this._callsInProcess == 0 && this._finalEvent != null)
				{
					NativeEventCalls.SetEvent_internal(this._finalEvent.Handle);
				}
			}
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x000E0504 File Offset: 0x000DE704
		private void DoCallBack(object timedOut)
		{
			if (this._callback != null)
			{
				this._callback(this._state, (bool)timedOut);
			}
			lock (this)
			{
				this._callsInProcess--;
				if (this._unregistered && this._callsInProcess == 0 && this._finalEvent != null)
				{
					NativeEventCalls.SetEvent_internal(this._finalEvent.Handle);
				}
			}
		}

		/// <summary>Cancels a registered wait operation issued by the <see cref="M:System.Threading.ThreadPool.RegisterWaitForSingleObject(System.Threading.WaitHandle,System.Threading.WaitOrTimerCallback,System.Object,System.UInt32,System.Boolean)" /> method.</summary>
		/// <returns>true if the function succeeds; otherwise, false.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to be signaled. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004100 RID: 16640 RVA: 0x000E05A4 File Offset: 0x000DE7A4
		[ComVisible(true)]
		public bool Unregister(WaitHandle waitObject)
		{
			bool result;
			lock (this)
			{
				if (this._unregistered)
				{
					result = false;
				}
				else
				{
					this._finalEvent = waitObject;
					this._unregistered = true;
					this._cancelEvent.Set();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04001BCB RID: 7115
		private WaitHandle _waitObject;

		// Token: 0x04001BCC RID: 7116
		private WaitOrTimerCallback _callback;

		// Token: 0x04001BCD RID: 7117
		private TimeSpan _timeout;

		// Token: 0x04001BCE RID: 7118
		private object _state;

		// Token: 0x04001BCF RID: 7119
		private bool _executeOnlyOnce;

		// Token: 0x04001BD0 RID: 7120
		private WaitHandle _finalEvent;

		// Token: 0x04001BD1 RID: 7121
		private ManualResetEvent _cancelEvent;

		// Token: 0x04001BD2 RID: 7122
		private int _callsInProcess;

		// Token: 0x04001BD3 RID: 7123
		private bool _unregistered;
	}
}
