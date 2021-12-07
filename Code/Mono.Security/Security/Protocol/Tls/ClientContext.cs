using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000083 RID: 131
	internal class ClientContext : Context
	{
		// Token: 0x060004BD RID: 1213 RVA: 0x0001D288 File Offset: 0x0001B488
		public ClientContext(SslClientStream stream, SecurityProtocolType securityProtocolType, string targetHost, X509CertificateCollection clientCertificates) : base(securityProtocolType)
		{
			this.sslStream = stream;
			base.ClientSettings.Certificates = clientCertificates;
			base.ClientSettings.TargetHost = targetHost;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		public SslClientStream SslStream
		{
			get
			{
				return this.sslStream;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001D2C4 File Offset: 0x0001B4C4
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0001D2CC File Offset: 0x0001B4CC
		public short ClientHelloProtocol
		{
			get
			{
				return this.clientHelloProtocol;
			}
			set
			{
				this.clientHelloProtocol = value;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001D2D8 File Offset: 0x0001B4D8
		public override void Clear()
		{
			this.clientHelloProtocol = 0;
			base.Clear();
		}

		// Token: 0x04000253 RID: 595
		private SslClientStream sslStream;

		// Token: 0x04000254 RID: 596
		private short clientHelloProtocol;
	}
}
