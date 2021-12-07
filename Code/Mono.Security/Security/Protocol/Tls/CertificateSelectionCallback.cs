using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x020000CD RID: 205
	// (Invoke) Token: 0x06000719 RID: 1817
	public delegate X509Certificate CertificateSelectionCallback(X509CertificateCollection clientCertificates, X509Certificate serverCertificate, string targetHost, X509CertificateCollection serverRequestedCertificates);
}
