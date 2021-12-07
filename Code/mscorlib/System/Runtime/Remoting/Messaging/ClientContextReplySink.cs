using System;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000492 RID: 1170
	internal class ClientContextReplySink : IMessageSink
	{
		// Token: 0x06002FA7 RID: 12199 RVA: 0x0009D844 File Offset: 0x0009BA44
		public ClientContextReplySink(Context ctx, IMessageSink replySink)
		{
			this._replySink = replySink;
			this._context = ctx;
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x0009D85C File Offset: 0x0009BA5C
		public IMessage SyncProcessMessage(IMessage msg)
		{
			Context.NotifyGlobalDynamicSinks(false, msg, true, true);
			this._context.NotifyDynamicSinks(false, msg, true, true);
			return this._replySink.SyncProcessMessage(msg);
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x0009D890 File Offset: 0x0009BA90
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002FAA RID: 12202 RVA: 0x0009D898 File Offset: 0x0009BA98
		public IMessageSink NextSink
		{
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x0400144A RID: 5194
		private IMessageSink _replySink;

		// Token: 0x0400144B RID: 5195
		private Context _context;
	}
}
