using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000482 RID: 1154
	internal class SynchronizedServerContextSink : IMessageSink
	{
		// Token: 0x06002F36 RID: 12086 RVA: 0x0009C6A0 File Offset: 0x0009A8A0
		public SynchronizedServerContextSink(IMessageSink next, SynchronizationAttribute att)
		{
			this._att = att;
			this._next = next;
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002F37 RID: 12087 RVA: 0x0009C6B8 File Offset: 0x0009A8B8
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x0009C6C0 File Offset: 0x0009A8C0
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this._att.AcquireLock();
			replySink = new SynchronizedContextReplySink(replySink, this._att, false);
			return this._next.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x0009C6EC File Offset: 0x0009A8EC
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._att.AcquireLock();
			IMessage result;
			try
			{
				result = this._next.SyncProcessMessage(msg);
			}
			finally
			{
				this._att.ReleaseLock();
			}
			return result;
		}

		// Token: 0x04001415 RID: 5141
		private IMessageSink _next;

		// Token: 0x04001416 RID: 5142
		private SynchronizationAttribute _att;
	}
}
