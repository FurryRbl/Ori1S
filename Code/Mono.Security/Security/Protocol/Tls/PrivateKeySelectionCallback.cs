using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x020000CE RID: 206
	// (Invoke) Token: 0x0600071D RID: 1821
	public delegate AsymmetricAlgorithm PrivateKeySelectionCallback(X509Certificate certificate, string targetHost);
}
