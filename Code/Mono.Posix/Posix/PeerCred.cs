using System;
using System.Net.Sockets;

namespace Mono.Posix
{
	// Token: 0x0200006C RID: 108
	[Obsolete("Use Mono.Unix.PeerCred")]
	public class PeerCred
	{
		// Token: 0x06000510 RID: 1296 RVA: 0x0000D468 File Offset: 0x0000B668
		public PeerCred(Socket sock)
		{
			if (sock.AddressFamily != AddressFamily.Unix)
			{
				throw new ArgumentException("Only Unix sockets are supported", "sock");
			}
			this.data = (PeerCredData)sock.GetSocketOption(SocketOptionLevel.Socket, (SocketOptionName)10001);
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		public int ProcessID
		{
			get
			{
				return this.data.pid;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000D4C4 File Offset: 0x0000B6C4
		public int UserID
		{
			get
			{
				return this.data.uid;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		public int GroupID
		{
			get
			{
				return this.data.gid;
			}
		}

		// Token: 0x040003B3 RID: 947
		private const int so_peercred = 10001;

		// Token: 0x040003B4 RID: 948
		private PeerCredData data;
	}
}
