using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003BC RID: 956
	internal class TcpConnectionInformationImpl : TcpConnectionInformation
	{
		// Token: 0x06002128 RID: 8488 RVA: 0x00062080 File Offset: 0x00060280
		public TcpConnectionInformationImpl(IPEndPoint local, IPEndPoint remote, TcpState state)
		{
			this.local = local;
			this.remote = remote;
			this.state = state;
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x000620A0 File Offset: 0x000602A0
		public override IPEndPoint LocalEndPoint
		{
			get
			{
				return this.local;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x000620A8 File Offset: 0x000602A8
		public override IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.remote;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x000620B0 File Offset: 0x000602B0
		public override TcpState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x04001441 RID: 5185
		private IPEndPoint local;

		// Token: 0x04001442 RID: 5186
		private IPEndPoint remote;

		// Token: 0x04001443 RID: 5187
		private TcpState state;
	}
}
