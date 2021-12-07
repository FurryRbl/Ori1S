using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200045D RID: 1117
	internal class ServerDispatchSink : IChannelSinkBase, IServerChannelSink
	{
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002E8C RID: 11916 RVA: 0x0009AA34 File Offset: 0x00098C34
		public IServerChannelSink NextChannelSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002E8D RID: 11917 RVA: 0x0009AA38 File Offset: 0x00098C38
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x0009AA3C File Offset: 0x00098C3C
		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x0009AA44 File Offset: 0x00098C44
		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x0009AA48 File Offset: 0x00098C48
		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			responseHeaders = null;
			responseStream = null;
			return ChannelServices.DispatchMessage(sinkStack, requestMsg, out responseMsg);
		}
	}
}
