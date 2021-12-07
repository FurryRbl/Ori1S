using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000080 RID: 128
	internal class UnixClientTransportSink : IClientChannelSink, IChannelSinkBase
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x0000E580 File Offset: 0x0000C780
		public UnixClientTransportSink(string url)
		{
			string text;
			this._path = UnixChannel.ParseUnixURL(url, out text);
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		public IClientChannelSink NextChannelSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000E5AC File Offset: 0x0000C7AC
		public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream requestStream)
		{
			UnixConnection unixConnection = null;
			bool flag = RemotingServices.IsOneWay(((IMethodMessage)msg).MethodBase);
			try
			{
				if (headers == null)
				{
					headers = new TransportHeaders();
				}
				headers["__RequestUri"] = ((IMethodMessage)msg).Uri;
				unixConnection = UnixConnectionPool.GetConnection(this._path);
				UnixMessageIO.SendMessageStream(unixConnection.Stream, requestStream, headers, unixConnection.Buffer);
				unixConnection.Stream.Flush();
				if (!flag)
				{
					sinkStack.Push(this, unixConnection);
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.ReadAsyncUnixMessage), sinkStack);
				}
				else
				{
					unixConnection.Release();
				}
			}
			catch
			{
				if (unixConnection != null)
				{
					unixConnection.Release();
				}
				if (!flag)
				{
					throw;
				}
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0000E684 File Offset: 0x0000C884
		private void ReadAsyncUnixMessage(object data)
		{
			IClientChannelSinkStack clientChannelSinkStack = (IClientChannelSinkStack)data;
			UnixConnection unixConnection = (UnixConnection)clientChannelSinkStack.Pop(this);
			try
			{
				MessageStatus messageStatus = UnixMessageIO.ReceiveMessageStatus(unixConnection.Stream, unixConnection.Buffer);
				if (messageStatus != MessageStatus.MethodMessage)
				{
					throw new RemotingException("Unknown response message from server");
				}
				ITransportHeaders headers;
				Stream stream = UnixMessageIO.ReceiveMessageStream(unixConnection.Stream, out headers, unixConnection.Buffer);
				unixConnection.Release();
				unixConnection = null;
				clientChannelSinkStack.AsyncProcessResponse(headers, stream);
			}
			catch
			{
				if (unixConnection != null)
				{
					unixConnection.Release();
				}
				throw;
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000E724 File Offset: 0x0000C924
		public void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000E72C File Offset: 0x0000C92C
		public Stream GetRequestStream(IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000E730 File Offset: 0x0000C930
		public void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			UnixConnection unixConnection = null;
			try
			{
				if (requestHeaders == null)
				{
					requestHeaders = new TransportHeaders();
				}
				requestHeaders["__RequestUri"] = ((IMethodMessage)msg).Uri;
				unixConnection = UnixConnectionPool.GetConnection(this._path);
				UnixMessageIO.SendMessageStream(unixConnection.Stream, requestStream, requestHeaders, unixConnection.Buffer);
				unixConnection.Stream.Flush();
				MessageStatus messageStatus = UnixMessageIO.ReceiveMessageStatus(unixConnection.Stream, unixConnection.Buffer);
				if (messageStatus != MessageStatus.MethodMessage)
				{
					throw new RemotingException("Unknown response message from server");
				}
				responseStream = UnixMessageIO.ReceiveMessageStream(unixConnection.Stream, out responseHeaders, unixConnection.Buffer);
			}
			finally
			{
				if (unixConnection != null)
				{
					unixConnection.Release();
				}
			}
		}

		// Token: 0x04000436 RID: 1078
		private string _path;
	}
}
