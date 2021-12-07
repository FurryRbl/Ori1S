using System;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004B6 RID: 1206
	internal class ServerContextTerminatorSink : IMessageSink
	{
		// Token: 0x060030FC RID: 12540 RVA: 0x000A0E14 File Offset: 0x0009F014
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (msg is IConstructionCallMessage)
			{
				return ActivationServices.CreateInstanceFromMessage((IConstructionCallMessage)msg);
			}
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			return serverIdentity.SyncObjectProcessMessage(msg);
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x000A0E4C File Offset: 0x0009F04C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			return serverIdentity.AsyncObjectProcessMessage(msg, replySink);
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x000A0E70 File Offset: 0x0009F070
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}
	}
}
