using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x020000CB RID: 203
	// (Invoke) Token: 0x06000711 RID: 1809
	public delegate bool CertificateValidationCallback(X509Certificate certificate, int[] certificateErrors);
}
