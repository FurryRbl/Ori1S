using System;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x020000A0 RID: 160
	internal sealed class TlsClientSettings
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x00022E10 File Offset: 0x00021010
		public TlsClientSettings()
		{
			this.certificates = new System.Security.Cryptography.X509Certificates.X509CertificateCollection();
			this.targetHost = string.Empty;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00022E30 File Offset: 0x00021030
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x00022E38 File Offset: 0x00021038
		public string TargetHost
		{
			get
			{
				return this.targetHost;
			}
			set
			{
				this.targetHost = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00022E44 File Offset: 0x00021044
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x00022E4C File Offset: 0x0002104C
		public System.Security.Cryptography.X509Certificates.X509CertificateCollection Certificates
		{
			get
			{
				return this.certificates;
			}
			set
			{
				this.certificates = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00022E58 File Offset: 0x00021058
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x00022E60 File Offset: 0x00021060
		public System.Security.Cryptography.X509Certificates.X509Certificate ClientCertificate
		{
			get
			{
				return this.clientCertificate;
			}
			set
			{
				this.clientCertificate = value;
				this.UpdateCertificateRSA();
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00022E70 File Offset: 0x00021070
		public RSAManaged CertificateRSA
		{
			get
			{
				return this.certificateRSA;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00022E78 File Offset: 0x00021078
		public void UpdateCertificateRSA()
		{
			if (this.clientCertificate == null)
			{
				this.certificateRSA = null;
			}
			else
			{
				Mono.Security.X509.X509Certificate x509Certificate = new Mono.Security.X509.X509Certificate(this.clientCertificate.GetRawCertData());
				this.certificateRSA = new RSAManaged(x509Certificate.RSA.KeySize);
				this.certificateRSA.ImportParameters(x509Certificate.RSA.ExportParameters(false));
			}
		}

		// Token: 0x040002F3 RID: 755
		private string targetHost;

		// Token: 0x040002F4 RID: 756
		private System.Security.Cryptography.X509Certificates.X509CertificateCollection certificates;

		// Token: 0x040002F5 RID: 757
		private System.Security.Cryptography.X509Certificates.X509Certificate clientCertificate;

		// Token: 0x040002F6 RID: 758
		private RSAManaged certificateRSA;
	}
}
