using System;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x020000B0 RID: 176
	internal class TlsServerHelloDone : HandshakeMessage
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x00024B48 File Offset: 0x00022D48
		public TlsServerHelloDone(Context context, byte[] buffer) : base(context, HandshakeType.ServerHelloDone, buffer)
		{
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00024B54 File Offset: 0x00022D54
		protected override void ProcessAsSsl3()
		{
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00024B58 File Offset: 0x00022D58
		protected override void ProcessAsTls1()
		{
		}
	}
}
