using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000077 RID: 119
	internal class UnixBinaryClientFormatterSink : IClientChannelSink, IMessageSink, IChannelSinkBase, IClientFormatterSink
	{
		// Token: 0x06000572 RID: 1394 RVA: 0x0000D97C File Offset: 0x0000BB7C
		public UnixBinaryClientFormatterSink(IClientChannelSink nextSink)
		{
			this._nextInChain = nextSink;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000D998 File Offset: 0x0000BB98
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
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

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000D9AC File Offset: 0x0000BBAC
		public IClientChannelSink NextChannelSink
		{
			get
			{
				return this._nextInChain;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000D9B4 File Offset: 0x0000BBB4
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000D9BC File Offset: 0x0000BBBC
		public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException("UnixBinaryClientFormatterSink must be the first sink in the IClientChannelSink chain");
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000D9C8 File Offset: 0x0000BBC8
		public void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream)
		{
			IMessage msg = (IMessage)this._binaryCore.Deserializer.DeserializeMethodResponse(stream, null, (IMethodCallMessage)state);
			sinkStack.DispatchReplyMessage(msg);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		public Stream GetRequestStream(IMessage msg, ITransportHeaders headers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000DA04 File Offset: 0x0000BC04
		public void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000DA0C File Offset: 0x0000BC0C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ITransportHeaders headers = new TransportHeaders();
			Stream stream = this._nextInChain.GetRequestStream(msg, headers);
			if (stream == null)
			{
				stream = new MemoryStream();
			}
			this._binaryCore.Serializer.Serialize(stream, msg, null);
			if (stream is MemoryStream)
			{
				stream.Position = 0L;
			}
			ClientChannelSinkStack clientChannelSinkStack = new ClientChannelSinkStack(replySink);
			clientChannelSinkStack.Push(this, msg);
			this._nextInChain.AsyncProcessRequest(clientChannelSinkStack, msg, headers, stream);
			return null;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000DA80 File Offset: 0x0000BC80
		public IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage result;
			try
			{
				ITransportHeaders transportHeaders = new TransportHeaders();
				transportHeaders["__RequestUri"] = ((IMethodCallMessage)msg).Uri;
				transportHeaders["Content-Type"] = "application/octet-stream";
				Stream stream = this._nextInChain.GetRequestStream(msg, transportHeaders);
				if (stream == null)
				{
					stream = new MemoryStream();
				}
				this._binaryCore.Serializer.Serialize(stream, msg, null);
				if (stream is MemoryStream)
				{
					stream.Position = 0L;
				}
				ITransportHeaders transportHeaders2;
				Stream serializationStream;
				this._nextInChain.ProcessMessage(msg, transportHeaders, stream, out transportHeaders2, out serializationStream);
				result = (IMessage)this._binaryCore.Deserializer.DeserializeMethodResponse(serializationStream, null, (IMethodCallMessage)msg);
			}
			catch (Exception e)
			{
				result = new ReturnMessage(e, (IMethodCallMessage)msg);
			}
			return result;
		}

		// Token: 0x0400041B RID: 1051
		private UnixBinaryCore _binaryCore = UnixBinaryCore.DefaultInstance;

		// Token: 0x0400041C RID: 1052
		private IClientChannelSink _nextInChain;
	}
}
