using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace Mono.Security.Authenticode
{
	// Token: 0x020000B2 RID: 178
	internal class AuthenticodeDeformatter : AuthenticodeBase
	{
		// Token: 0x060009F8 RID: 2552 RVA: 0x000288F0 File Offset: 0x00026AF0
		public AuthenticodeDeformatter()
		{
			this.reason = -1;
			this.signerChain = new X509Chain();
			this.timestampChain = new X509Chain();
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00028918 File Offset: 0x00026B18
		public AuthenticodeDeformatter(string fileName) : this()
		{
			this.FileName = fileName;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00028928 File Offset: 0x00026B28
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x00028930 File Offset: 0x00026B30
		public string FileName
		{
			get
			{
				return this.filename;
			}
			set
			{
				this.Reset();
				try
				{
					this.CheckSignature(value);
				}
				catch (SecurityException)
				{
					throw;
				}
				catch (Exception)
				{
					this.reason = 1;
				}
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002899C File Offset: 0x00026B9C
		public byte[] Hash
		{
			get
			{
				if (this.signedHash == null)
				{
					return null;
				}
				return (byte[])this.signedHash.Value.Clone();
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x000289CC File Offset: 0x00026BCC
		public int Reason
		{
			get
			{
				if (this.reason == -1)
				{
					this.IsTrusted();
				}
				return this.reason;
			}
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x000289E8 File Offset: 0x00026BE8
		public bool IsTrusted()
		{
			if (this.entry == null)
			{
				this.reason = 1;
				return false;
			}
			if (this.signingCertificate == null)
			{
				this.reason = 7;
				return false;
			}
			if (this.signerChain.Root == null || !this.trustedRoot)
			{
				this.reason = 6;
				return false;
			}
			if (this.timestamp != DateTime.MinValue)
			{
				if (this.timestampChain.Root == null || !this.trustedTimestampRoot)
				{
					this.reason = 6;
					return false;
				}
				if (!this.signingCertificate.WasCurrent(this.Timestamp))
				{
					this.reason = 4;
					return false;
				}
			}
			else if (!this.signingCertificate.IsCurrent)
			{
				this.reason = 8;
				return false;
			}
			if (this.reason == -1)
			{
				this.reason = 0;
			}
			return true;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00028ACC File Offset: 0x00026CCC
		public byte[] Signature
		{
			get
			{
				if (this.entry == null)
				{
					return null;
				}
				return (byte[])this.entry.Clone();
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00028AEC File Offset: 0x00026CEC
		public DateTime Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00028AF4 File Offset: 0x00026CF4
		public X509CertificateCollection Certificates
		{
			get
			{
				return this.coll;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00028AFC File Offset: 0x00026CFC
		public X509Certificate SigningCertificate
		{
			get
			{
				return this.signingCertificate;
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00028B04 File Offset: 0x00026D04
		private bool CheckSignature(string fileName)
		{
			this.filename = fileName;
			base.Open(this.filename);
			this.entry = base.GetSecurityEntry();
			if (this.entry == null)
			{
				this.reason = 1;
				base.Close();
				return false;
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(this.entry);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.2")
			{
				base.Close();
				return false;
			}
			PKCS7.SignedData signedData = new PKCS7.SignedData(contentInfo.Content);
			if (signedData.ContentInfo.ContentType != "1.3.6.1.4.1.311.2.1.4")
			{
				base.Close();
				return false;
			}
			this.coll = signedData.Certificates;
			ASN1 content = signedData.ContentInfo.Content;
			this.signedHash = content[0][1][1];
			int length = this.signedHash.Length;
			HashAlgorithm hashAlgorithm;
			if (length != 16)
			{
				if (length != 20)
				{
					this.reason = 5;
					base.Close();
					return false;
				}
				hashAlgorithm = HashAlgorithm.Create("SHA1");
				this.hash = base.GetHash(hashAlgorithm);
			}
			else
			{
				hashAlgorithm = HashAlgorithm.Create("MD5");
				this.hash = base.GetHash(hashAlgorithm);
			}
			base.Close();
			if (!this.signedHash.CompareValue(this.hash))
			{
				this.reason = 2;
			}
			byte[] value = content[0].Value;
			hashAlgorithm.Initialize();
			byte[] calculatedMessageDigest = hashAlgorithm.ComputeHash(value);
			bool flag = this.VerifySignature(signedData, calculatedMessageDigest, hashAlgorithm);
			return flag && this.reason == 0;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00028CA4 File Offset: 0x00026EA4
		private bool CompareIssuerSerial(string issuer, byte[] serial, X509Certificate x509)
		{
			if (issuer != x509.IssuerName)
			{
				return false;
			}
			if (serial.Length != x509.SerialNumber.Length)
			{
				return false;
			}
			int num = serial.Length;
			for (int i = 0; i < serial.Length; i++)
			{
				if (serial[i] != x509.SerialNumber[--num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00028D08 File Offset: 0x00026F08
		private bool VerifySignature(PKCS7.SignedData sd, byte[] calculatedMessageDigest, HashAlgorithm ha)
		{
			string a = null;
			ASN1 asn = null;
			int i = 0;
			while (i < sd.SignerInfo.AuthenticatedAttributes.Count)
			{
				ASN1 asn2 = (ASN1)sd.SignerInfo.AuthenticatedAttributes[i];
				string text = ASN1Convert.ToOid(asn2[0]);
				string text2 = text;
				switch (text2)
				{
				case "1.2.840.113549.1.9.3":
					a = ASN1Convert.ToOid(asn2[1][0]);
					break;
				case "1.2.840.113549.1.9.4":
					asn = asn2[1][0];
					break;
				}
				IL_F1:
				i++;
				continue;
				goto IL_F1;
			}
			if (a != "1.3.6.1.4.1.311.2.1.4")
			{
				return false;
			}
			if (asn == null)
			{
				return false;
			}
			if (!asn.CompareValue(calculatedMessageDigest))
			{
				return false;
			}
			string str = CryptoConfig.MapNameToOID(ha.ToString());
			ASN1 asn3 = new ASN1(49);
			foreach (object obj in sd.SignerInfo.AuthenticatedAttributes)
			{
				ASN1 asn4 = (ASN1)obj;
				asn3.Add(asn4);
			}
			ha.Initialize();
			byte[] rgbHash = ha.ComputeHash(asn3.GetBytes());
			byte[] signature = sd.SignerInfo.Signature;
			string issuerName = sd.SignerInfo.IssuerName;
			byte[] serialNumber = sd.SignerInfo.SerialNumber;
			foreach (X509Certificate x509Certificate in this.coll)
			{
				if (this.CompareIssuerSerial(issuerName, serialNumber, x509Certificate) && x509Certificate.PublicKey.Length > signature.Length >> 3)
				{
					this.signingCertificate = x509Certificate;
					RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)x509Certificate.RSA;
					if (rsacryptoServiceProvider.VerifyHash(rgbHash, str, signature))
					{
						this.signerChain.LoadCertificates(this.coll);
						this.trustedRoot = this.signerChain.Build(x509Certificate);
						break;
					}
				}
			}
			if (sd.SignerInfo.UnauthenticatedAttributes.Count == 0)
			{
				this.trustedTimestampRoot = true;
			}
			else
			{
				int j = 0;
				while (j < sd.SignerInfo.UnauthenticatedAttributes.Count)
				{
					ASN1 asn5 = (ASN1)sd.SignerInfo.UnauthenticatedAttributes[j];
					string text3 = ASN1Convert.ToOid(asn5[0]);
					string text2 = text3;
					if (text2 != null)
					{
						if (AuthenticodeDeformatter.<>f__switch$map8 == null)
						{
							AuthenticodeDeformatter.<>f__switch$map8 = new Dictionary<string, int>(1)
							{
								{
									"1.2.840.113549.1.9.6",
									0
								}
							};
						}
						int num;
						if (AuthenticodeDeformatter.<>f__switch$map8.TryGetValue(text2, out num))
						{
							if (num == 0)
							{
								PKCS7.SignerInfo cs = new PKCS7.SignerInfo(asn5[1]);
								this.trustedTimestampRoot = this.VerifyCounterSignature(cs, signature);
							}
						}
					}
					IL_35D:
					j++;
					continue;
					goto IL_35D;
				}
			}
			return this.trustedRoot && this.trustedTimestampRoot;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000290D8 File Offset: 0x000272D8
		private bool VerifyCounterSignature(PKCS7.SignerInfo cs, byte[] signature)
		{
			if (cs.Version != 1)
			{
				return false;
			}
			string a = null;
			ASN1 asn = null;
			int i = 0;
			while (i < cs.AuthenticatedAttributes.Count)
			{
				ASN1 asn2 = (ASN1)cs.AuthenticatedAttributes[i];
				string text = ASN1Convert.ToOid(asn2[0]);
				string text2 = text;
				switch (text2)
				{
				case "1.2.840.113549.1.9.3":
					a = ASN1Convert.ToOid(asn2[1][0]);
					break;
				case "1.2.840.113549.1.9.4":
					asn = asn2[1][0];
					break;
				case "1.2.840.113549.1.9.5":
					this.timestamp = ASN1Convert.ToDateTime(asn2[1][0]);
					break;
				}
				IL_FC:
				i++;
				continue;
				goto IL_FC;
			}
			if (a != "1.2.840.113549.1.7.1")
			{
				return false;
			}
			if (asn == null)
			{
				return false;
			}
			string hashName = null;
			int length = asn.Length;
			if (length != 16)
			{
				if (length == 20)
				{
					hashName = "SHA1";
				}
			}
			else
			{
				hashName = "MD5";
			}
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashName);
			if (!asn.CompareValue(hashAlgorithm.ComputeHash(signature)))
			{
				return false;
			}
			byte[] signature2 = cs.Signature;
			ASN1 asn3 = new ASN1(49);
			foreach (object obj in cs.AuthenticatedAttributes)
			{
				ASN1 asn4 = (ASN1)obj;
				asn3.Add(asn4);
			}
			byte[] hashValue = hashAlgorithm.ComputeHash(asn3.GetBytes());
			string issuerName = cs.IssuerName;
			byte[] serialNumber = cs.SerialNumber;
			foreach (X509Certificate x509Certificate in this.coll)
			{
				if (this.CompareIssuerSerial(issuerName, serialNumber, x509Certificate) && x509Certificate.PublicKey.Length > signature2.Length)
				{
					RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)x509Certificate.RSA;
					RSAManaged rsamanaged = new RSAManaged();
					rsamanaged.ImportParameters(rsacryptoServiceProvider.ExportParameters(false));
					if (PKCS1.Verify_v15(rsamanaged, hashAlgorithm, hashValue, signature2, true))
					{
						this.timestampChain.LoadCertificates(this.coll);
						return this.timestampChain.Build(x509Certificate);
					}
				}
			}
			return false;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000293E4 File Offset: 0x000275E4
		private void Reset()
		{
			this.filename = null;
			this.entry = null;
			this.hash = null;
			this.signedHash = null;
			this.signingCertificate = null;
			this.reason = -1;
			this.trustedRoot = false;
			this.trustedTimestampRoot = false;
			this.signerChain.Reset();
			this.timestampChain.Reset();
			this.timestamp = DateTime.MinValue;
		}

		// Token: 0x0400023F RID: 575
		private string filename;

		// Token: 0x04000240 RID: 576
		private byte[] hash;

		// Token: 0x04000241 RID: 577
		private X509CertificateCollection coll;

		// Token: 0x04000242 RID: 578
		private ASN1 signedHash;

		// Token: 0x04000243 RID: 579
		private DateTime timestamp;

		// Token: 0x04000244 RID: 580
		private X509Certificate signingCertificate;

		// Token: 0x04000245 RID: 581
		private int reason;

		// Token: 0x04000246 RID: 582
		private bool trustedRoot;

		// Token: 0x04000247 RID: 583
		private bool trustedTimestampRoot;

		// Token: 0x04000248 RID: 584
		private byte[] entry;

		// Token: 0x04000249 RID: 585
		private X509Chain signerChain;

		// Token: 0x0400024A RID: 586
		private X509Chain timestampChain;
	}
}
