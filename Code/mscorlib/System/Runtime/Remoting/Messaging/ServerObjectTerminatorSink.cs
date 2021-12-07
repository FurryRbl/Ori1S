using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004B7 RID: 1207
	internal class ServerObjectTerminatorSink : IMessageSink
	{
		// Token: 0x060030FF RID: 12543 RVA: 0x000A0E74 File Offset: 0x0009F074
		public ServerObjectTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000A0E84 File Offset: 0x0009F084
		public IMessage SyncProcessMessage(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			serverIdentity.NotifyServerDynamicSinks(true, msg, false, false);
			IMessage result = this._nextSink.SyncProcessMessage(msg);
			serverIdentity.NotifyServerDynamicSinks(false, msg, false, false);
			return result;
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000A0EC0 File Offset: 0x0009F0C0
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			if (serverIdentity.HasServerDynamicSinks)
			{
				serverIdentity.NotifyServerDynamicSinks(true, msg, false, true);
				if (replySink != null)
				{
					replySink = new ServerObjectReplySink(serverIdentity, replySink);
				}
			}
			IMessageCtrl result = this._nextSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null)
			{
				serverIdentity.NotifyServerDynamicSinks(false, msg, true, true);
			}
			return result;
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x000A0F1C File Offset: 0x0009F11C
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x040014BC RID: 5308
		private IMessageSink _nextSink;
	}
}
