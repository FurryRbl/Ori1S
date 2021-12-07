using System;
using System.Net.Sockets;

namespace Mono.Unix
{
	// Token: 0x0200000D RID: 13
	public class UnixClient : MarshalByRefObject, IDisposable
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002F38 File Offset: 0x00001138
		public UnixClient()
		{
			if (this.client != null)
			{
				this.client.Close();
				this.client = null;
			}
			this.client = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002F78 File Offset: 0x00001178
		public UnixClient(string path) : this()
		{
			if (path == null)
			{
				throw new ArgumentNullException("ep");
			}
			this.Connect(path);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002F98 File Offset: 0x00001198
		public UnixClient(UnixEndPoint ep) : this()
		{
			if (ep == null)
			{
				throw new ArgumentNullException("ep");
			}
			this.Connect(ep);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002FB8 File Offset: 0x000011B8
		internal UnixClient(Socket sock)
		{
			this.Client = sock;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002FC8 File Offset: 0x000011C8
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002FD0 File Offset: 0x000011D0
		public Socket Client
		{
			get
			{
				return this.client;
			}
			set
			{
				this.client = value;
				this.stream = null;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002FE0 File Offset: 0x000011E0
		public PeerCred PeerCredential
		{
			get
			{
				this.CheckDisposed();
				return new PeerCred(this.client);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002FF4 File Offset: 0x000011F4
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00003024 File Offset: 0x00001224
		public LingerOption LingerState
		{
			get
			{
				this.CheckDisposed();
				return (LingerOption)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger);
			}
			set
			{
				this.CheckDisposed();
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003044 File Offset: 0x00001244
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003074 File Offset: 0x00001274
		public int ReceiveBufferSize
		{
			get
			{
				this.CheckDisposed();
				return (int)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer);
			}
			set
			{
				this.CheckDisposed();
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003094 File Offset: 0x00001294
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000030C4 File Offset: 0x000012C4
		public int ReceiveTimeout
		{
			get
			{
				this.CheckDisposed();
				return (int)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			}
			set
			{
				this.CheckDisposed();
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000030E4 File Offset: 0x000012E4
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00003114 File Offset: 0x00001314
		public int SendBufferSize
		{
			get
			{
				this.CheckDisposed();
				return (int)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer);
			}
			set
			{
				this.CheckDisposed();
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003134 File Offset: 0x00001334
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00003164 File Offset: 0x00001364
		public int SendTimeout
		{
			get
			{
				this.CheckDisposed();
				return (int)this.client.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
			}
			set
			{
				this.CheckDisposed();
				this.client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, value);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003184 File Offset: 0x00001384
		public void Close()
		{
			this.CheckDisposed();
			this.Dispose();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003194 File Offset: 0x00001394
		public void Connect(UnixEndPoint remoteEndPoint)
		{
			this.CheckDisposed();
			this.client.Connect(remoteEndPoint);
			this.stream = new NetworkStream(this.client, true);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000031C8 File Offset: 0x000013C8
		public void Connect(string path)
		{
			this.CheckDisposed();
			this.Connect(new UnixEndPoint(path));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000031DC File Offset: 0x000013DC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000031EC File Offset: 0x000013EC
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				NetworkStream networkStream = this.stream;
				this.stream = null;
				if (networkStream != null)
				{
					networkStream.Close();
				}
				else if (this.client != null)
				{
					this.client.Close();
				}
				this.client = null;
			}
			this.disposed = true;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003250 File Offset: 0x00001450
		public NetworkStream GetStream()
		{
			this.CheckDisposed();
			if (this.stream == null)
			{
				this.stream = new NetworkStream(this.client, true);
			}
			return this.stream;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000327C File Offset: 0x0000147C
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000329C File Offset: 0x0000149C
		~UnixClient()
		{
			this.Dispose(false);
		}

		// Token: 0x0400004C RID: 76
		private NetworkStream stream;

		// Token: 0x0400004D RID: 77
		private Socket client;

		// Token: 0x0400004E RID: 78
		private bool disposed;
	}
}
