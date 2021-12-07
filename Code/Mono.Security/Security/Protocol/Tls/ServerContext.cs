using System;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Protocol.Tls.Handshake;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000096 RID: 150
	internal class ServerContext : Context
	{
		// Token: 0x0600056E RID: 1390 RVA: 0x0001F808 File Offset: 0x0001DA08
		public ServerContext(SslServerStream stream, SecurityProtocolType securityProtocolType, System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, bool requestClientCertificate) : base(securityProtocolType)
		{
			this.sslStream = stream;
			this.clientCertificateRequired = clientCertificateRequired;
			this.request_client_certificate = requestClientCertificate;
			Mono.Security.X509.X509Certificate value = new Mono.Security.X509.X509Certificate(serverCertificate.GetRawCertData());
			base.ServerSettings.Certificates = new Mono.Security.X509.X509CertificateCollection();
			base.ServerSettings.Certificates.Add(value);
			base.ServerSettings.UpdateCertificateRSA();
			base.ServerSettings.CertificateTypes = new ClientCertificateType[1];
			base.ServerSettings.CertificateTypes[0] = ClientCertificateType.RSA;
			Mono.Security.X509.X509CertificateCollection trustedRootCertificates = X509StoreManager.TrustedRootCertificates;
			string[] array = new string[trustedRootCertificates.Count];
			int num = 0;
			foreach (Mono.Security.X509.X509Certificate x509Certificate in trustedRootCertificates)
			{
				array[num++] = x509Certificate.IssuerName;
			}
			base.ServerSettings.DistinguisedNames = array;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0001F914 File Offset: 0x0001DB14
		public SslServerStream SslStream
		{
			get
			{
				return this.sslStream;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001F91C File Offset: 0x0001DB1C
		public bool ClientCertificateRequired
		{
			get
			{
				return this.clientCertificateRequired;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0001F924 File Offset: 0x0001DB24
		public bool RequestClientCertificate
		{
			get
			{
				return this.request_client_certificate;
			}
		}

		// Token: 0x040002BC RID: 700
		private SslServerStream sslStream;

		// Token: 0x040002BD RID: 701
		private bool request_client_certificate;

		// Token: 0x040002BE RID: 702
		private bool clientCertificateRequired;
	}
}
