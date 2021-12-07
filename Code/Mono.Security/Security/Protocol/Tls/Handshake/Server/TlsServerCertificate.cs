using System;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x020000B7 RID: 183
	internal class TlsServerCertificate : HandshakeMessage
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x0002578C File Offset: 0x0002398C
		public TlsServerCertificate(Context context) : base(context, HandshakeType.Certificate)
		{
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00025798 File Offset: 0x00023998
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000257A0 File Offset: 0x000239A0
		protected override void ProcessAsTls1()
		{
			TlsStream tlsStream = new TlsStream();
			foreach (X509Certificate x509Certificate in base.Context.ServerSettings.Certificates)
			{
				tlsStream.WriteInt24(x509Certificate.RawData.Length);
				tlsStream.Write(x509Certificate.RawData);
			}
			base.WriteInt24(Convert.ToInt32(tlsStream.Length));
			base.Write(tlsStream.ToArray());
			tlsStream.Close();
		}
	}
}
