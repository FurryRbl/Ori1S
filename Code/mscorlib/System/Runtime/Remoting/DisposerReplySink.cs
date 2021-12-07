using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000431 RID: 1073
	internal class DisposerReplySink : IMessageSink
	{
		// Token: 0x06002DA2 RID: 11682 RVA: 0x00097F0C File Offset: 0x0009610C
		public DisposerReplySink(IMessageSink next, IDisposable disposable)
		{
			this._next = next;
			this._disposable = disposable;
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x00097F24 File Offset: 0x00096124
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._disposable.Dispose();
			return this._next.SyncProcessMessage(msg);
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x00097F40 File Offset: 0x00096140
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x00097F48 File Offset: 0x00096148
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x040013A2 RID: 5026
		private IMessageSink _next;

		// Token: 0x040013A3 RID: 5027
		private IDisposable _disposable;
	}
}
