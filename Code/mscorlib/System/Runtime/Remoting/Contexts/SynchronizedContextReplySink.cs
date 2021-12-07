using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000483 RID: 1155
	internal class SynchronizedContextReplySink : IMessageSink
	{
		// Token: 0x06002F3A RID: 12090 RVA: 0x0009C744 File Offset: 0x0009A944
		public SynchronizedContextReplySink(IMessageSink next, SynchronizationAttribute att, bool newLock)
		{
			this._newLock = newLock;
			this._next = next;
			this._att = att;
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x0009C764 File Offset: 0x0009A964
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x0009C76C File Offset: 0x0009A96C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x0009C774 File Offset: 0x0009A974
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (this._newLock)
			{
				this._att.AcquireLock();
			}
			else
			{
				this._att.ReleaseLock();
			}
			IMessage result;
			try
			{
				result = this._next.SyncProcessMessage(msg);
			}
			finally
			{
				if (this._newLock)
				{
					this._att.ReleaseLock();
				}
			}
			return result;
		}

		// Token: 0x04001417 RID: 5143
		private IMessageSink _next;

		// Token: 0x04001418 RID: 5144
		private bool _newLock;

		// Token: 0x04001419 RID: 5145
		private SynchronizationAttribute _att;
	}
}
