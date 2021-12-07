using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;
using Mono.Security.Protocol.Tls.Handshake;
using Mono.Security.X509;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x020000A2 RID: 162
	internal class TlsServerSettings
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00022F60 File Offset: 0x00021160
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00022F68 File Offset: 0x00021168
		public bool ServerKeyExchange
		{
			get
			{
				return this.serverKeyExchange;
			}
			set
			{
				this.serverKeyExchange = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00022F74 File Offset: 0x00021174
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00022F7C File Offset: 0x0002117C
		public X509CertificateCollection Certificates
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

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00022F88 File Offset: 0x00021188
		public RSA CertificateRSA
		{
			get
			{
				return this.certificateRSA;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00022F90 File Offset: 0x00021190
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00022F98 File Offset: 0x00021198
		public RSAParameters RsaParameters
		{
			get
			{
				return this.rsaParameters;
			}
			set
			{
				this.rsaParameters = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00022FA4 File Offset: 0x000211A4
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00022FAC File Offset: 0x000211AC
		public byte[] SignedParams
		{
			get
			{
				return this.signedParams;
			}
			set
			{
				this.signedParams = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00022FB8 File Offset: 0x000211B8
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00022FC0 File Offset: 0x000211C0
		public bool CertificateRequest
		{
			get
			{
				return this.certificateRequest;
			}
			set
			{
				this.certificateRequest = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00022FCC File Offset: 0x000211CC
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x00022FD4 File Offset: 0x000211D4
		public ClientCertificateType[] CertificateTypes
		{
			get
			{
				return this.certificateTypes;
			}
			set
			{
				this.certificateTypes = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00022FE0 File Offset: 0x000211E0
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x00022FE8 File Offset: 0x000211E8
		public string[] DistinguisedNames
		{
			get
			{
				return this.distinguisedNames;
			}
			set
			{
				this.distinguisedNames = value;
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00022FF4 File Offset: 0x000211F4
		public void UpdateCertificateRSA()
		{
			if (this.certificates == null || this.certificates.Count == 0)
			{
				this.certificateRSA = null;
			}
			else
			{
				this.certificateRSA = new RSAManaged(this.certificates[0].RSA.KeySize);
				this.certificateRSA.ImportParameters(this.certificates[0].RSA.ExportParameters(false));
			}
		}

		// Token: 0x040002F8 RID: 760
		private X509CertificateCollection certificates;

		// Token: 0x040002F9 RID: 761
		private RSA certificateRSA;

		// Token: 0x040002FA RID: 762
		private RSAParameters rsaParameters;

		// Token: 0x040002FB RID: 763
		private byte[] signedParams;

		// Token: 0x040002FC RID: 764
		private string[] distinguisedNames;

		// Token: 0x040002FD RID: 765
		private bool serverKeyExchange;

		// Token: 0x040002FE RID: 766
		private bool certificateRequest;

		// Token: 0x040002FF RID: 767
		private ClientCertificateType[] certificateTypes;
	}
}
