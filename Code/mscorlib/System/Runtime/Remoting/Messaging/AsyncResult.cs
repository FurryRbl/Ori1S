using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Encapsulates the results of an asynchronous operation on a delegate.</summary>
	// Token: 0x0200048E RID: 1166
	[ComVisible(true)]
	public class AsyncResult : IAsyncResult, IMessageSink
	{
		// Token: 0x06002F83 RID: 12163 RVA: 0x0009D320 File Offset: 0x0009B520
		internal AsyncResult()
		{
		}

		/// <summary>Gets the object provided as the last parameter of a BeginInvoke method call.</summary>
		/// <returns>The object provided as the last parameter of a BeginInvoke method call.</returns>
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002F84 RID: 12164 RVA: 0x0009D328 File Offset: 0x0009B528
		public virtual object AsyncState
		{
			get
			{
				return this.async_state;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that encapsulates Win32 synchronization handles, and allows the implementation of various synchronization schemes.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that encapsulates Win32 synchronization handles, and allows the implementation of various synchronization schemes.</returns>
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002F85 RID: 12165 RVA: 0x0009D330 File Offset: 0x0009B530
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle result;
				lock (this)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
					result = this.handle;
				}
				return result;
			}
		}

		/// <summary>Gets a value indicating whether the BeginInvoke call completed synchronously.</summary>
		/// <returns>true if the BeginInvoke call completed synchronously; otherwise, false.</returns>
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002F86 RID: 12166 RVA: 0x0009D398 File Offset: 0x0009B598
		public virtual bool CompletedSynchronously
		{
			get
			{
				return this.sync_completed;
			}
		}

		/// <summary>Gets a value indicating whether the server has completed the call.</summary>
		/// <returns>true after the server has completed the call; otherwise, false.</returns>
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002F87 RID: 12167 RVA: 0x0009D3A0 File Offset: 0x0009B5A0
		public virtual bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		/// <summary>Gets or sets a value indicating whether EndInvoke has been called on the current <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" />.</summary>
		/// <returns>true if EndInvoke has been called on the current <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" />; otherwise, false.</returns>
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x0009D3A8 File Offset: 0x0009B5A8
		// (set) Token: 0x06002F89 RID: 12169 RVA: 0x0009D3B0 File Offset: 0x0009B5B0
		public bool EndInvokeCalled
		{
			get
			{
				return this.endinvoke_called;
			}
			set
			{
				this.endinvoke_called = value;
			}
		}

		/// <summary>Gets the delegate object on which the asynchronous call was invoked.</summary>
		/// <returns>The delegate object on which the asynchronous call was invoked.</returns>
		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002F8A RID: 12170 RVA: 0x0009D3BC File Offset: 0x0009B5BC
		public virtual object AsyncDelegate
		{
			get
			{
				return this.async_delegate;
			}
		}

		/// <summary>Gets the next message sink in the sink chain.</summary>
		/// <returns>An <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface that represents the next message sink in the sink chain.</returns>
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002F8B RID: 12171 RVA: 0x0009D3C4 File Offset: 0x0009B5C4
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface.</summary>
		/// <returns>No value is returned.</returns>
		/// <param name="msg">The request <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface. </param>
		/// <param name="replySink">The response <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface. </param>
		// Token: 0x06002F8C RID: 12172 RVA: 0x0009D3C8 File Offset: 0x0009B5C8
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the response message for the asynchronous call.</summary>
		/// <returns>A remoting message that should represent a response to a method call on a remote object.</returns>
		// Token: 0x06002F8D RID: 12173 RVA: 0x0009D3D0 File Offset: 0x0009B5D0
		public virtual IMessage GetReplyMessage()
		{
			return this.reply_message;
		}

		/// <summary>Sets an <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> for the current remote method call, which provides a way to control asynchronous messages after they have been dispatched.</summary>
		/// <param name="mc">The <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> for the current remote method call. </param>
		// Token: 0x06002F8E RID: 12174 RVA: 0x0009D3D8 File Offset: 0x0009B5D8
		public virtual void SetMessageCtrl(IMessageCtrl mc)
		{
			this.message_ctrl = mc;
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x0009D3E4 File Offset: 0x0009B5E4
		internal void SetCompletedSynchronously(bool completed)
		{
			this.sync_completed = completed;
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x0009D3F0 File Offset: 0x0009B5F0
		internal IMessage EndInvoke()
		{
			lock (this)
			{
				if (this.completed)
				{
					return this.reply_message;
				}
			}
			this.AsyncWaitHandle.WaitOne();
			return this.reply_message;
		}

		/// <summary>Synchronously processes a response message returned by a method call on a remote object.</summary>
		/// <returns>Returns null.</returns>
		/// <param name="msg">A response message to a method call on a remote object.</param>
		// Token: 0x06002F91 RID: 12177 RVA: 0x0009D458 File Offset: 0x0009B658
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			this.reply_message = msg;
			lock (this)
			{
				this.completed = true;
				if (this.handle != null)
				{
					((ManualResetEvent)this.AsyncWaitHandle).Set();
				}
			}
			if (this.async_callback != null)
			{
				AsyncCallback asyncCallback = (AsyncCallback)this.async_callback;
				asyncCallback(this);
			}
			return null;
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x0009D4E0 File Offset: 0x0009B6E0
		// (set) Token: 0x06002F93 RID: 12179 RVA: 0x0009D4E8 File Offset: 0x0009B6E8
		internal MonoMethodMessage CallMessage
		{
			get
			{
				return this.call_message;
			}
			set
			{
				this.call_message = value;
			}
		}

		// Token: 0x04001438 RID: 5176
		private object async_state;

		// Token: 0x04001439 RID: 5177
		private WaitHandle handle;

		// Token: 0x0400143A RID: 5178
		private object async_delegate;

		// Token: 0x0400143B RID: 5179
		private IntPtr data;

		// Token: 0x0400143C RID: 5180
		private object object_data;

		// Token: 0x0400143D RID: 5181
		private bool sync_completed;

		// Token: 0x0400143E RID: 5182
		private bool completed;

		// Token: 0x0400143F RID: 5183
		private bool endinvoke_called;

		// Token: 0x04001440 RID: 5184
		private object async_callback;

		// Token: 0x04001441 RID: 5185
		private ExecutionContext current;

		// Token: 0x04001442 RID: 5186
		private ExecutionContext original;

		// Token: 0x04001443 RID: 5187
		private int gchandle;

		// Token: 0x04001444 RID: 5188
		private MonoMethodMessage call_message;

		// Token: 0x04001445 RID: 5189
		private IMessageCtrl message_ctrl;

		// Token: 0x04001446 RID: 5190
		private IMessage reply_message;
	}
}
