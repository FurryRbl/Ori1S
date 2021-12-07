using System;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000496 RID: 1174
	[Serializable]
	internal class EnvoyTerminatorSink : IMessageSink
	{
		// Token: 0x06002FC9 RID: 12233 RVA: 0x0009DDC8 File Offset: 0x0009BFC8
		public IMessage SyncProcessMessage(IMessage msg)
		{
			return Thread.CurrentContext.GetClientContextSinkChain().SyncProcessMessage(msg);
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x0009DDDC File Offset: 0x0009BFDC
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			return Thread.CurrentContext.GetClientContextSinkChain().AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002FCB RID: 12235 RVA: 0x0009DDF0 File Offset: 0x0009BFF0
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001457 RID: 5207
		public static EnvoyTerminatorSink Instance = new EnvoyTerminatorSink();
	}
}
