using System;
using System.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x02000046 RID: 70
	public class X509CertificateBuilder : X509Builder
	{
		// Token: 0x0600031A RID: 794 RVA: 0x000163B4 File Offset: 0x000145B4
		public X509CertificateBuilder() : this(3)
		{
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000163C0 File Offset: 0x000145C0
		public X509CertificateBuilder(byte version)
		{
			if (version > 3)
			{
				throw new ArgumentException("Invalid certificate version");
			}
			this.version = version;
			this.extensions = new X509ExtensionCollection();
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000163F8 File Offset: 0x000145F8
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00016400 File Offset: 0x00014600
		public byte Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0001640C File Offset: 0x0001460C
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00016414 File Offset: 0x00014614
		public byte[] SerialNumber
		{
			get
			{
				return this.sn;
			}
			set
			{
				this.sn = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00016420 File Offset: 0x00014620
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00016428 File Offset: 0x00014628
		public string IssuerName
		{
			get
			{
				return this.issuer;
			}
			set
			{
				this.issuer = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00016434 File Offset: 0x00014634
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0001643C File Offset: 0x0001463C
		public DateTime NotBefore
		{
			get
			{
				return this.notBefore;
			}
			set
			{
				this.notBefore = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00016448 File Offset: 0x00014648
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00016450 File Offset: 0x00014650
		public DateTime NotAfter
		{
			get
			{
				return this.notAfter;
			}
			set
			{
				this.notAfter = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0001645C File Offset: 0x0001465C
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00016464 File Offset: 0x00014664
		public string SubjectName
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00016470 File Offset: 0x00014670
		// (set) Token: 0x06000329 RID: 809 RVA: 0x00016478 File Offset: 0x00014678
		public AsymmetricAlgorithm SubjectPublicKey
		{
			get
			{
				return this.aa;
			}
			set
			{
				this.aa = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00016484 File Offset: 0x00014684
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0001648C File Offset: 0x0001468C
		public byte[] IssuerUniqueId
		{
			get
			{
				return this.issuerUniqueID;
			}
			set
			{
				this.issuerUniqueID = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00016498 File Offset: 0x00014698
		// (set) Token: 0x0600032D RID: 813 RVA: 0x000164A0 File Offset: 0x000146A0
		public byte[] SubjectUniqueId
		{
			get
			{
				return this.subjectUniqueID;
			}
			set
			{
				this.subjectUniqueID = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600032E RID: 814 RVA: 0x000164AC File Offset: 0x000146AC
		public X509ExtensionCollection Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000164B4 File Offset: 0x000146B4
		private ASN1 SubjectPublicKeyInfo()
		{
			ASN1 asn = new ASN1(48);
			if (this.aa is RSA)
			{
				asn.Add(PKCS7.AlgorithmIdentifier("1.2.840.113549.1.1.1"));
				RSAParameters rsaparameters = (this.aa as RSA).ExportParameters(false);
				ASN1 asn2 = new ASN1(48);
				asn2.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Modulus));
				asn2.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Exponent));
				asn.Add(new ASN1(this.UniqueIdentifier(asn2.GetBytes())));
			}
			else
			{
				if (!(this.aa is DSA))
				{
					throw new NotSupportedException("Unknown Asymmetric Algorithm " + this.aa.ToString());
				}
				DSAParameters dsaparameters = (this.aa as DSA).ExportParameters(false);
				ASN1 asn3 = new ASN1(48);
				asn3.Add(ASN1Convert.FromUnsignedBigInteger(dsaparameters.P));
				asn3.Add(ASN1Convert.FromUnsignedBigInteger(dsaparameters.Q));
				asn3.Add(ASN1Convert.FromUnsignedBigInteger(dsaparameters.G));
				asn.Add(PKCS7.AlgorithmIdentifier("1.2.840.10040.4.1", asn3));
				ASN1 asn4 = asn.Add(new ASN1(3));
				asn4.Add(ASN1Convert.FromUnsignedBigInteger(dsaparameters.Y));
			}
			return asn;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00016604 File Offset: 0x00014804
		private byte[] UniqueIdentifier(byte[] id)
		{
			ASN1 asn = new ASN1(3);
			byte[] array = new byte[id.Length + 1];
			Buffer.BlockCopy(id, 0, array, 1, id.Length);
			asn.Value = array;
			return asn.GetBytes();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001663C File Offset: 0x0001483C
		protected override ASN1 ToBeSigned(string oid)
		{
			ASN1 asn = new ASN1(48);
			if (this.version > 1)
			{
				byte[] data = new byte[]
				{
					this.version - 1
				};
				ASN1 asn2 = asn.Add(new ASN1(160));
				asn2.Add(new ASN1(2, data));
			}
			asn.Add(new ASN1(2, this.sn));
			asn.Add(PKCS7.AlgorithmIdentifier(oid));
			asn.Add(X501.FromString(this.issuer));
			ASN1 asn3 = asn.Add(new ASN1(48));
			asn3.Add(ASN1Convert.FromDateTime(this.notBefore));
			asn3.Add(ASN1Convert.FromDateTime(this.notAfter));
			asn.Add(X501.FromString(this.subject));
			asn.Add(this.SubjectPublicKeyInfo());
			if (this.version > 1)
			{
				if (this.issuerUniqueID != null)
				{
					asn.Add(new ASN1(161, this.UniqueIdentifier(this.issuerUniqueID)));
				}
				if (this.subjectUniqueID != null)
				{
					asn.Add(new ASN1(161, this.UniqueIdentifier(this.subjectUniqueID)));
				}
				if (this.version > 2 && this.extensions.Count > 0)
				{
					asn.Add(new ASN1(163, this.extensions.GetBytes()));
				}
			}
			return asn;
		}

		// Token: 0x04000179 RID: 377
		private byte version;

		// Token: 0x0400017A RID: 378
		private byte[] sn;

		// Token: 0x0400017B RID: 379
		private string issuer;

		// Token: 0x0400017C RID: 380
		private DateTime notBefore;

		// Token: 0x0400017D RID: 381
		private DateTime notAfter;

		// Token: 0x0400017E RID: 382
		private string subject;

		// Token: 0x0400017F RID: 383
		private AsymmetricAlgorithm aa;

		// Token: 0x04000180 RID: 384
		private byte[] issuerUniqueID;

		// Token: 0x04000181 RID: 385
		private byte[] subjectUniqueID;

		// Token: 0x04000182 RID: 386
		private X509ExtensionCollection extensions;
	}
}
