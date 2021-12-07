using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace Mono.Security.Authenticode
{
	// Token: 0x0200001F RID: 31
	public class AuthenticodeDeformatter : AuthenticodeBase
	{
		// Token: 0x06000138 RID: 312 RVA: 0x000082C0 File Offset: 0x000064C0
		public AuthenticodeDeformatter()
		{
			this.reason = -1;
			this.signerChain = new X509Chain();
			this.timestampChain = new X509Chain();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000082E8 File Offset: 0x000064E8
		public AuthenticodeDeformatter(string fileName) : this()
		{
			this.FileName = fileName;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000082F8 File Offset: 0x000064F8
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00008300 File Offset: 0x00006500
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000836C File Offset: 0x0000656C
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000839C File Offset: 0x0000659C
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

		// Token: 0x0600013E RID: 318 RVA: 0x000083B8 File Offset: 0x000065B8
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000849C File Offset: 0x0000669C
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000084BC File Offset: 0x000066BC
		public DateTime Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000084C4 File Offset: 0x000066C4
		public X509CertificateCollection Certificates
		{
			get
			{
				return this.coll;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000084CC File Offset: 0x000066CC
		public X509Certificate SigningCertificate
		{
			get
			{
				return this.signingCertificate;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000084D4 File Offset: 0x000066D4
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

		// Token: 0x06000144 RID: 324 RVA: 0x00008674 File Offset: 0x00006874
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

		// Token: 0x06000145 RID: 325 RVA: 0x000086D8 File Offset: 0x000068D8
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
						if (AuthenticodeDeformatter.<>f__switch$map2 == null)
						{
							AuthenticodeDeformatter.<>f__switch$map2 = new Dictionary<string, int>(1)
							{
								{
									"1.2.840.113549.1.9.6",
									0
								}
							};
						}
						int num;
						if (AuthenticodeDeformatter.<>f__switch$map2.TryGetValue(text2, out num))
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

		// Token: 0x06000146 RID: 326 RVA: 0x00008AA8 File Offset: 0x00006CA8
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

		// Token: 0x06000147 RID: 327 RVA: 0x00008DB4 File Offset: 0x00006FB4
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

		// Token: 0x0400007F RID: 127
		private string filename;

		// Token: 0x04000080 RID: 128
		private byte[] hash;

		// Token: 0x04000081 RID: 129
		private X509CertificateCollection coll;

		// Token: 0x04000082 RID: 130
		private ASN1 signedHash;

		// Token: 0x04000083 RID: 131
		private DateTime timestamp;

		// Token: 0x04000084 RID: 132
		private X509Certificate signingCertificate;

		// Token: 0x04000085 RID: 133
		private int reason;

		// Token: 0x04000086 RID: 134
		private bool trustedRoot;

		// Token: 0x04000087 RID: 135
		private bool trustedTimestampRoot;

		// Token: 0x04000088 RID: 136
		private byte[] entry;

		// Token: 0x04000089 RID: 137
		private X509Chain signerChain;

		// Token: 0x0400008A RID: 138
		private X509Chain timestampChain;
	}
}
