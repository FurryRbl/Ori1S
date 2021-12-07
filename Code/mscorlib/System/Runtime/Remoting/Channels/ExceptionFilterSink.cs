using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200044C RID: 1100
	internal class ExceptionFilterSink : IMessageSink
	{
		// Token: 0x06002E5E RID: 11870 RVA: 0x0009A8C4 File Offset: 0x00098AC4
		public ExceptionFilterSink(IMessage call, IMessageSink next)
		{
			this._call = call;
			this._next = next;
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x0009A8DC File Offset: 0x00098ADC
		public IMessage SyncProcessMessage(IMessage msg)
		{
			return this._next.SyncProcessMessage(ChannelServices.CheckReturnMessage(this._call, msg));
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x0009A8F8 File Offset: 0x00098AF8
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x0009A900 File Offset: 0x00098B00
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x040013D5 RID: 5077
		private IMessageSink _next;

		// Token: 0x040013D6 RID: 5078
		private IMessage _call;
	}
}
