using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200046D RID: 1133
	internal class AsyncRequest
	{
		// Token: 0x06002ED3 RID: 11987 RVA: 0x0009B130 File Offset: 0x00099330
		public AsyncRequest(IMessage msgRequest, IMessageSink replySink)
		{
			this.ReplySink = replySink;
			this.MsgRequest = msgRequest;
		}

		// Token: 0x040013F0 RID: 5104
		internal IMessageSink ReplySink;

		// Token: 0x040013F1 RID: 5105
		internal IMessage MsgRequest;
	}
}
