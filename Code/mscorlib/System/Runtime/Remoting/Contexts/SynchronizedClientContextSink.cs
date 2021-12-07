using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000481 RID: 1153
	internal class SynchronizedClientContextSink : IMessageSink
	{
		// Token: 0x06002F32 RID: 12082 RVA: 0x0009C5C4 File Offset: 0x0009A7C4
		public SynchronizedClientContextSink(IMessageSink next, SynchronizationAttribute att)
		{
			this._att = att;
			this._next = next;
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x0009C5DC File Offset: 0x0009A7DC
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x0009C5E4 File Offset: 0x0009A7E4
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._att.IsReEntrant)
			{
				this._att.ReleaseLock();
				replySink = new SynchronizedContextReplySink(replySink, this._att, true);
			}
			return this._next.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x0009C628 File Offset: 0x0009A828
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (this._att.IsReEntrant)
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
				if (this._att.IsReEntrant)
				{
					this._att.AcquireLock();
				}
			}
			return result;
		}

		// Token: 0x04001413 RID: 5139
		private IMessageSink _next;

		// Token: 0x04001414 RID: 5140
		private SynchronizationAttribute _att;
	}
}
