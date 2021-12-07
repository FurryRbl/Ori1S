using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Mono.Unix
{
	// Token: 0x0200001A RID: 26
	public class UnixListener : MarshalByRefObject, IDisposable
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00005F5C File Offset: 0x0000415C
		public UnixListener(string path)
		{
			if (!Directory.Exists(Path.GetDirectoryName(path)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(path));
			}
			this.Init(new UnixEndPoint(path));
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005F98 File Offset: 0x00004198
		public UnixListener(UnixEndPoint localEndPoint)
		{
			if (localEndPoint == null)
			{
				throw new ArgumentNullException("localendPoint");
			}
			this.Init(localEndPoint);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005FB8 File Offset: 0x000041B8
		private void Init(UnixEndPoint ep)
		{
			this.listening = false;
			string filename = ep.Filename;
			if (File.Exists(filename))
			{
				Socket socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
				try
				{
					socket.Connect(ep);
					socket.Close();
					throw new InvalidOperationException("There's already a server listening on " + filename);
				}
				catch (SocketException)
				{
				}
				File.Delete(filename);
			}
			this.server = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
			this.server.Bind(ep);
			this.savedEP = this.server.LocalEndPoint;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006060 File Offset: 0x00004260
		public EndPoint LocalEndpoint
		{
			get
			{
				return this.savedEP;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006068 File Offset: 0x00004268
		protected Socket Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006070 File Offset: 0x00004270
		public Socket AcceptSocket()
		{
			this.CheckDisposed();
			if (!this.listening)
			{
				throw new InvalidOperationException("Socket is not listening");
			}
			return this.server.Accept();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000609C File Offset: 0x0000429C
		public UnixClient AcceptUnixClient()
		{
			this.CheckDisposed();
			if (!this.listening)
			{
				throw new InvalidOperationException("Socket is not listening");
			}
			return new UnixClient(this.AcceptSocket());
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000060C8 File Offset: 0x000042C8
		~UnixListener()
		{
			this.Dispose(false);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006104 File Offset: 0x00004304
		public bool Pending()
		{
			this.CheckDisposed();
			if (!this.listening)
			{
				throw new InvalidOperationException("Socket is not listening");
			}
			return this.server.Poll(1000, SelectMode.SelectRead);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006134 File Offset: 0x00004334
		public void Start()
		{
			this.Start(5);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006140 File Offset: 0x00004340
		public void Start(int backlog)
		{
			this.CheckDisposed();
			if (this.listening)
			{
				return;
			}
			this.server.Listen(backlog);
			this.listening = true;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006168 File Offset: 0x00004368
		public void Stop()
		{
			this.CheckDisposed();
			this.Dispose(true);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006178 File Offset: 0x00004378
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006188 File Offset: 0x00004388
		protected void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				if (this.server != null)
				{
					this.server.Close();
				}
				this.server = null;
			}
			this.disposed = true;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000061CC File Offset: 0x000043CC
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x04000069 RID: 105
		private bool disposed;

		// Token: 0x0400006A RID: 106
		private bool listening;

		// Token: 0x0400006B RID: 107
		private Socket server;

		// Token: 0x0400006C RID: 108
		private EndPoint savedEP;
	}
}
