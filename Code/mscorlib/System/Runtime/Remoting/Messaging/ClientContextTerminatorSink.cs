using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000491 RID: 1169
	internal class ClientContextTerminatorSink : IMessageSink
	{
		// Token: 0x06002FA3 RID: 12195 RVA: 0x0009D71C File Offset: 0x0009B91C
		public ClientContextTerminatorSink(Context ctx)
		{
			this._context = ctx;
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x0009D72C File Offset: 0x0009B92C
		public IMessage SyncProcessMessage(IMessage msg)
		{
			Context.NotifyGlobalDynamicSinks(true, msg, true, false);
			this._context.NotifyDynamicSinks(true, msg, true, false);
			IMessage result;
			if (msg is IConstructionCallMessage)
			{
				result = ActivationServices.RemoteActivate((IConstructionCallMessage)msg);
			}
			else
			{
				Identity messageTargetIdentity = RemotingServices.GetMessageTargetIdentity(msg);
				result = messageTargetIdentity.ChannelSink.SyncProcessMessage(msg);
			}
			Context.NotifyGlobalDynamicSinks(false, msg, true, false);
			this._context.NotifyDynamicSinks(false, msg, true, false);
			return result;
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x0009D79C File Offset: 0x0009B99C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._context.HasDynamicSinks || Context.HasGlobalDynamicSinks)
			{
				Context.NotifyGlobalDynamicSinks(true, msg, true, true);
				this._context.NotifyDynamicSinks(true, msg, true, true);
				if (replySink != null)
				{
					replySink = new ClientContextReplySink(this._context, replySink);
				}
			}
			Identity messageTargetIdentity = RemotingServices.GetMessageTargetIdentity(msg);
			IMessageCtrl result = messageTargetIdentity.ChannelSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null && (this._context.HasDynamicSinks || Context.HasGlobalDynamicSinks))
			{
				Context.NotifyGlobalDynamicSinks(false, msg, true, true);
				this._context.NotifyDynamicSinks(false, msg, true, true);
			}
			return result;
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x0009D840 File Offset: 0x0009BA40
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001449 RID: 5193
		private Context _context;
	}
}
