using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200007B RID: 123
	internal class UnixBinaryServerFormatterSink : IServerChannelSink, IChannelSinkBase
	{
		// Token: 0x0600058E RID: 1422 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		public UnixBinaryServerFormatterSink(IServerChannelSink nextSink, IChannelReceiver receiver)
		{
			this.next_sink = nextSink;
			this.receiver = receiver;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0000DEF0 File Offset: 0x0000C0F0
		internal UnixBinaryCore BinaryCore
		{
			get
			{
				return this._binaryCore;
			}
			set
			{
				this._binaryCore = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		public IServerChannelSink NextChannelSink
		{
			get
			{
				return this.next_sink;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000DF04 File Offset: 0x0000C104
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000DF08 File Offset: 0x0000C108
		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage message, ITransportHeaders headers, Stream stream)
		{
			ITransportHeaders headers2 = new TransportHeaders();
			if (sinkStack != null)
			{
				stream = sinkStack.GetResponseStream(message, headers2);
			}
			if (stream == null)
			{
				stream = new MemoryStream();
			}
			this._binaryCore.Serializer.Serialize(stream, message, null);
			if (stream is MemoryStream)
			{
				stream.Position = 0L;
			}
			sinkStack.AsyncProcessResponse(message, headers2, stream);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000DF6C File Offset: 0x0000C16C
		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000DF70 File Offset: 0x0000C170
		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			sinkStack.Push(this, null);
			ServerProcessing serverProcessing;
			try
			{
				string text = (string)requestHeaders["__RequestUri"];
				string text2;
				this.receiver.Parse(text, out text2);
				if (text2 == null)
				{
					text2 = text;
				}
				MethodCallHeaderHandler @object = new MethodCallHeaderHandler(text2);
				requestMsg = (IMessage)this._binaryCore.Deserializer.Deserialize(requestStream, new HeaderHandler(@object.HandleHeaders));
				serverProcessing = this.next_sink.ProcessMessage(sinkStack, requestMsg, requestHeaders, null, out responseMsg, out responseHeaders, out responseStream);
			}
			catch (Exception e)
			{
				responseMsg = new ReturnMessage(e, (IMethodCallMessage)requestMsg);
				serverProcessing = ServerProcessing.Complete;
				responseHeaders = null;
				responseStream = null;
			}
			if (serverProcessing == ServerProcessing.Complete)
			{
				for (int i = 0; i < 3; i++)
				{
					responseStream = null;
					responseHeaders = new TransportHeaders();
					if (sinkStack != null)
					{
						responseStream = sinkStack.GetResponseStream(responseMsg, responseHeaders);
					}
					if (responseStream == null)
					{
						responseStream = new MemoryStream();
					}
					try
					{
						this._binaryCore.Serializer.Serialize(responseStream, responseMsg);
						break;
					}
					catch (Exception ex)
					{
						if (i == 2)
						{
							throw ex;
						}
						responseMsg = new ReturnMessage(ex, (IMethodCallMessage)requestMsg);
					}
				}
				if (responseStream is MemoryStream)
				{
					responseStream.Position = 0L;
				}
				sinkStack.Pop(this);
			}
			return serverProcessing;
		}

		// Token: 0x04000428 RID: 1064
		private UnixBinaryCore _binaryCore = UnixBinaryCore.DefaultInstance;

		// Token: 0x04000429 RID: 1065
		private IServerChannelSink next_sink;

		// Token: 0x0400042A RID: 1066
		private IChannelReceiver receiver;
	}
}
