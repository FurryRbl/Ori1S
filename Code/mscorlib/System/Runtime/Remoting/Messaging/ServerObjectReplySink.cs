using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004B8 RID: 1208
	internal class ServerObjectReplySink : IMessageSink
	{
		// Token: 0x06003103 RID: 12547 RVA: 0x000A0F24 File Offset: 0x0009F124
		public ServerObjectReplySink(ServerIdentity identity, IMessageSink replySink)
		{
			this._replySink = replySink;
			this._identity = identity;
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x000A0F3C File Offset: 0x0009F13C
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._identity.NotifyServerDynamicSinks(false, msg, true, true);
			return this._replySink.SyncProcessMessage(msg);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000A0F5C File Offset: 0x0009F15C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000A0F64 File Offset: 0x0009F164
		public IMessageSink NextSink
		{
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x040014BD RID: 5309
		private IMessageSink _replySink;

		// Token: 0x040014BE RID: 5310
		private ServerIdentity _identity;
	}
}
