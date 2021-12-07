using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000085 RID: 133
	internal class ClientConnection
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x0000F410 File Offset: 0x0000D610
		public ClientConnection(UnixServerChannel serverChannel, Socket client, UnixServerTransportSink sink)
		{
			this._serverChannel = serverChannel;
			this._client = client;
			this._sink = sink;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000F440 File Offset: 0x0000D640
		public Socket Client
		{
			get
			{
				return this._client;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000F448 File Offset: 0x0000D648
		public byte[] Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000F450 File Offset: 0x0000D650
		public void ProcessMessages()
		{
			byte[] buffer = new byte[256];
			this._stream = new BufferedStream(new NetworkStream(this._client));
			try
			{
				bool flag = false;
				while (!flag)
				{
					MessageStatus messageStatus = UnixMessageIO.ReceiveMessageStatus(this._stream, buffer);
					MessageStatus messageStatus2 = messageStatus;
					if (messageStatus2 != MessageStatus.MethodMessage)
					{
						if (messageStatus2 == MessageStatus.CancelSignal || messageStatus2 == MessageStatus.Unknown)
						{
							flag = true;
						}
					}
					else
					{
						this._sink.InternalProcessMessage(this, this._stream);
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				this._stream.Close();
				this._client.Close();
				this._serverChannel.ReleaseConnection(Thread.CurrentThread);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000F538 File Offset: 0x0000D738
		public bool IsLocal
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400044D RID: 1101
		private Socket _client;

		// Token: 0x0400044E RID: 1102
		private UnixServerTransportSink _sink;

		// Token: 0x0400044F RID: 1103
		private Stream _stream;

		// Token: 0x04000450 RID: 1104
		private UnixServerChannel _serverChannel;

		// Token: 0x04000451 RID: 1105
		private byte[] _buffer = new byte[UnixMessageIO.DefaultStreamBufferSize];
	}
}
