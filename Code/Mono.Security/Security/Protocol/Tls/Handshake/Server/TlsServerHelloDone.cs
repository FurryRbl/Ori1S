using System;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x020000BB RID: 187
	internal class TlsServerHelloDone : HandshakeMessage
	{
		// Token: 0x060006BD RID: 1725 RVA: 0x00025BDC File Offset: 0x00023DDC
		public TlsServerHelloDone(Context context) : base(context, HandshakeType.ServerHelloDone)
		{
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00025BE8 File Offset: 0x00023DE8
		protected override void ProcessAsSsl3()
		{
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00025BEC File Offset: 0x00023DEC
		protected override void ProcessAsTls1()
		{
		}
	}
}
