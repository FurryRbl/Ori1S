using System;
using System.IO;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000089 RID: 137
	internal class UnixConnection
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x0000F828 File Offset: 0x0000DA28
		public UnixConnection(HostConnectionPool pool, ReusableUnixClient client)
		{
			this._pool = pool;
			this._client = client;
			this._stream = new BufferedStream(client.GetStream());
			this._controlTime = DateTime.Now;
			this._buffer = new byte[UnixMessageIO.DefaultStreamBufferSize];
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000F878 File Offset: 0x0000DA78
		public Stream Stream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0000F880 File Offset: 0x0000DA80
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0000F888 File Offset: 0x0000DA88
		public DateTime ControlTime
		{
			get
			{
				return this._controlTime;
			}
			set
			{
				this._controlTime = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0000F894 File Offset: 0x0000DA94
		public bool IsAlive
		{
			get
			{
				return this._client.IsAlive;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		public byte[] Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000F8AC File Offset: 0x0000DAAC
		public void Release()
		{
			this._pool.ReleaseConnection(this);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000F8BC File Offset: 0x0000DABC
		public void Close()
		{
			this._client.Close();
		}

		// Token: 0x04000457 RID: 1111
		private DateTime _controlTime;

		// Token: 0x04000458 RID: 1112
		private Stream _stream;

		// Token: 0x04000459 RID: 1113
		private ReusableUnixClient _client;

		// Token: 0x0400045A RID: 1114
		private HostConnectionPool _pool;

		// Token: 0x0400045B RID: 1115
		private byte[] _buffer;
	}
}
