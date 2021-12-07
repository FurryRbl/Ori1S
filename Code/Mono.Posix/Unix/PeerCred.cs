using System;
using System.Net.Sockets;
using Mono.Posix;

namespace Mono.Unix
{
	// Token: 0x0200000B RID: 11
	public class PeerCred
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000254C File Offset: 0x0000074C
		public PeerCred(Socket sock)
		{
			if (sock.AddressFamily != AddressFamily.Unix)
			{
				throw new ArgumentException("Only Unix sockets are supported", "sock");
			}
			this.data = (PeerCredData)sock.GetSocketOption(SocketOptionLevel.Socket, (SocketOptionName)10001);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002598 File Offset: 0x00000798
		public int ProcessID
		{
			get
			{
				return this.data.pid;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000025A8 File Offset: 0x000007A8
		public int UserID
		{
			get
			{
				return this.data.uid;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025B8 File Offset: 0x000007B8
		public int GroupID
		{
			get
			{
				return this.data.gid;
			}
		}

		// Token: 0x04000041 RID: 65
		private const int so_peercred = 10001;

		// Token: 0x04000042 RID: 66
		private PeerCredData data;
	}
}
