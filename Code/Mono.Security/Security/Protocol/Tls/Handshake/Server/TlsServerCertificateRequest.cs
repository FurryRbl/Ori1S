using System;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x020000B8 RID: 184
	internal class TlsServerCertificateRequest : HandshakeMessage
	{
		// Token: 0x060006B2 RID: 1714 RVA: 0x00025850 File Offset: 0x00023A50
		public TlsServerCertificateRequest(Context context) : base(context, HandshakeType.CertificateRequest)
		{
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0002585C File Offset: 0x00023A5C
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00025864 File Offset: 0x00023A64
		protected override void ProcessAsTls1()
		{
			ServerContext serverContext = (ServerContext)base.Context;
			int num = serverContext.ServerSettings.CertificateTypes.Length;
			this.WriteByte(Convert.ToByte(num));
			for (int i = 0; i < num; i++)
			{
				this.WriteByte((byte)serverContext.ServerSettings.CertificateTypes[i]);
			}
			if (serverContext.ServerSettings.DistinguisedNames.Length > 0)
			{
				TlsStream tlsStream = new TlsStream();
				foreach (string rdn in serverContext.ServerSettings.DistinguisedNames)
				{
					byte[] bytes = X501.FromString(rdn).GetBytes();
					tlsStream.Write((short)bytes.Length);
					tlsStream.Write(bytes);
				}
				base.Write((short)tlsStream.Length);
				base.Write(tlsStream.ToArray());
			}
			else
			{
				base.Write(0);
			}
		}
	}
}
