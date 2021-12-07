using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000086 RID: 134
	internal class UnixServerTransportSink : IServerChannelSink, IChannelSinkBase
	{
		// Token: 0x060005DE RID: 1502 RVA: 0x0000F53C File Offset: 0x0000D73C
		public UnixServerTransportSink(IServerChannelSink next)
		{
			this.next_sink = next;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000F54C File Offset: 0x0000D74C
		public IServerChannelSink NextChannelSink
		{
			get
			{
				return this.next_sink;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000F554 File Offset: 0x0000D754
		public IDictionary Properties
		{
			get
			{
				if (this.next_sink != null)
				{
					return this.next_sink.Properties;
				}
				return null;
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000F570 File Offset: 0x0000D770
		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream responseStream)
		{
			ClientConnection clientConnection = (ClientConnection)state;
			NetworkStream networkStream = new NetworkStream(clientConnection.Client);
			UnixMessageIO.SendMessageStream(networkStream, responseStream, headers, clientConnection.Buffer);
			networkStream.Flush();
			networkStream.Close();
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
		internal void InternalProcessMessage(ClientConnection connection, Stream stream)
		{
			ITransportHeaders requestHeaders;
			Stream requestStream = UnixMessageIO.ReceiveMessageStream(stream, out requestHeaders, connection.Buffer);
			ServerChannelSinkStack serverChannelSinkStack = new ServerChannelSinkStack();
			serverChannelSinkStack.Push(this, connection);
			IMessage message;
			ITransportHeaders requestHeaders2;
			Stream data;
			switch (this.next_sink.ProcessMessage(serverChannelSinkStack, null, requestHeaders, requestStream, out message, out requestHeaders2, out data))
			{
			case ServerProcessing.Complete:
				UnixMessageIO.SendMessageStream(stream, data, requestHeaders2, connection.Buffer);
				stream.Flush();
				break;
			}
		}

		// Token: 0x04000452 RID: 1106
		private IServerChannelSink next_sink;
	}
}
