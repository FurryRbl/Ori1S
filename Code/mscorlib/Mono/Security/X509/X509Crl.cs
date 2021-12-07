using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x020000CD RID: 205
	internal class X509Crl
	{
		// Token: 0x06000B79 RID: 2937 RVA: 0x00034E8C File Offset: 0x0003308C
		public X509Crl(byte[] crl)
		{
			if (crl == null)
			{
				throw new ArgumentNullException("crl");
			}
			this.encoded = (byte[])crl.Clone();
			this.Parse(this.encoded);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00034ED0 File Offset: 0x000330D0
		private void Parse(byte[] crl)
		{
			string text = "Input data cannot be coded as a valid CRL.";
			try
			{
				ASN1 asn = new ASN1(this.encoded);
				if (asn.Tag != 48 || asn.Count != 3)
				{
					throw new CryptographicException(text);
				}
				ASN1 asn2 = asn[0];
				if (asn2.Tag != 48 || asn2.Count < 3)
				{
					throw new CryptographicException(text);
				}
				int num = 0;
				if (asn2[num].Tag == 2)
				{
					this.version = asn2[num++].Value[0] + 1;
				}
				else
				{
					this.version = 1;
				}
				this.signatureOID = ASN1Convert.ToOid(asn2[num++][0]);
				this.issuer = X501.ToString(asn2[num++]);
				this.thisUpdate = ASN1Convert.ToDateTime(asn2[num++]);
				ASN1 asn3 = asn2[num++];
				if (asn3.Tag == 23 || asn3.Tag == 24)
				{
					this.nextUpdate = ASN1Convert.ToDateTime(asn3);
					asn3 = asn2[num++];
				}
				this.entries = new ArrayList();
				if (asn3 != null && asn3.Tag == 48)
				{
					ASN1 asn4 = asn3;
					for (int i = 0; i < asn4.Count; i++)
					{
						this.entries.Add(new X509Crl.X509CrlEntry(asn4[i]));
					}
				}
				else
				{
					num--;
				}
				ASN1 asn5 = asn2[num];
				if (asn5 != null && asn5.Tag == 160 && asn5.Count == 1)
				{
					this.extensions = new X509ExtensionCollection(asn5[0]);
				}
				else
				{
					this.extensions = new X509ExtensionCollection(null);
				}
				string b = ASN1Convert.ToOid(asn[1][0]);
				if (this.signatureOID != b)
				{
					throw new CryptographicException(text + " [Non-matching signature algorithms in CRL]");
				}
				byte[] value = asn[2].Value;
				this.signature = new byte[value.Length - 1];
				Buffer.BlockCopy(value, 1, this.signature, 0, this.signature.Length);
			}
			catch
			{
				throw new CryptographicException(text);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00035150 File Offset: 0x00033350
		public ArrayList Entries
		{
			get
			{
				return ArrayList.ReadOnly(this.entries);
			}
		}

		// Token: 0x1700018C RID: 396
		public X509Crl.X509CrlEntry this[int index]
		{
			get
			{
				return (X509Crl.X509CrlEntry)this.entries[index];
			}
		}

		// Token: 0x1700018D RID: 397
		public X509Crl.X509CrlEntry this[byte[] serialNumber]
		{
			get
			{
				return this.GetCrlEntry(serialNumber);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00035180 File Offset: 0x00033380
		public X509ExtensionCollection Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x00035188 File Offset: 0x00033388
		public byte[] Hash
		{
			get
			{
				if (this.hash_value == null)
				{
					ASN1 asn = new ASN1(this.encoded);
					byte[] bytes = asn[0].GetBytes();
					HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.GetHashName());
					this.hash_value = hashAlgorithm.ComputeHash(bytes);
				}
				return this.hash_value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x000351D8 File Offset: 0x000333D8
		public string IssuerName
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x000351E0 File Offset: 0x000333E0
		public DateTime NextUpdate
		{
			get
			{
				return this.nextUpdate;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x000351E8 File Offset: 0x000333E8
		public DateTime ThisUpdate
		{
			get
			{
				return this.thisUpdate;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x000351F0 File Offset: 0x000333F0
		public string SignatureAlgorithm
		{
			get
			{
				return this.signatureOID;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x000351F8 File Offset: 0x000333F8
		public byte[] Signature
		{
			get
			{
				if (this.signature == null)
				{
					return null;
				}
				return (byte[])this.signature.Clone();
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x00035218 File Offset: 0x00033418
		public byte[] RawData
		{
			get
			{
				return (byte[])this.encoded.Clone();
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0003522C File Offset: 0x0003342C
		public byte Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x00035234 File Offset: 0x00033434
		public bool IsCurrent
		{
			get
			{
				return this.WasCurrent(DateTime.Now);
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00035244 File Offset: 0x00033444
		public bool WasCurrent(DateTime instant)
		{
			if (this.nextUpdate == DateTime.MinValue)
			{
				return instant >= this.thisUpdate;
			}
			return instant >= this.thisUpdate && instant <= this.nextUpdate;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00035294 File Offset: 0x00033494
		public byte[] GetBytes()
		{
			return (byte[])this.encoded.Clone();
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000352A8 File Offset: 0x000334A8
		private bool Compare(byte[] array1, byte[] array2)
		{
			if (array1 == null && array2 == null)
			{
				return true;
			}
			if (array1 == null || array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00035300 File Offset: 0x00033500
		public X509Crl.X509CrlEntry GetCrlEntry(X509Certificate x509)
		{
			if (x509 == null)
			{
				throw new ArgumentNullException("x509");
			}
			return this.GetCrlEntry(x509.SerialNumber);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00035320 File Offset: 0x00033520
		public X509Crl.X509CrlEntry GetCrlEntry(byte[] serialNumber)
		{
			if (serialNumber == null)
			{
				throw new ArgumentNullException("serialNumber");
			}
			for (int i = 0; i < this.entries.Count; i++)
			{
				X509Crl.X509CrlEntry x509CrlEntry = (X509Crl.X509CrlEntry)this.entries[i];
				if (this.Compare(serialNumber, x509CrlEntry.SerialNumber))
				{
					return x509CrlEntry;
				}
			}
			return null;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00035384 File Offset: 0x00033584
		public bool VerifySignature(X509Certificate x509)
		{
			if (x509 == null)
			{
				throw new ArgumentNullException("x509");
			}
			if (x509.Version >= 3)
			{
				X509Extension x509Extension = x509.Extensions["2.5.29.15"];
				if (x509Extension != null)
				{
					KeyUsageExtension keyUsageExtension = new KeyUsageExtension(x509Extension);
					if (!keyUsageExtension.Support(KeyUsages.cRLSign))
					{
						return false;
					}
				}
				x509Extension = x509.Extensions["2.5.29.19"];
				if (x509Extension != null)
				{
					BasicConstraintsExtension basicConstraintsExtension = new BasicConstraintsExtension(x509Extension);
					if (!basicConstraintsExtension.CertificateAuthority)
					{
						return false;
					}
				}
			}
			if (this.issuer != x509.SubjectName)
			{
				return false;
			}
			string text = this.signatureOID;
			if (text != null)
			{
				if (X509Crl.<>f__switch$map16 == null)
				{
					X509Crl.<>f__switch$map16 = new Dictionary<string, int>(1)
					{
						{
							"1.2.840.10040.4.3",
							0
						}
					};
				}
				int num;
				if (X509Crl.<>f__switch$map16.TryGetValue(text, out num))
				{
					if (num == 0)
					{
						return this.VerifySignature(x509.DSA);
					}
				}
			}
			return this.VerifySignature(x509.RSA);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00035488 File Offset: 0x00033688
		private string GetHashName()
		{
			string text = this.signatureOID;
			switch (text)
			{
			case "1.2.840.113549.1.1.2":
				return "MD2";
			case "1.2.840.113549.1.1.4":
				return "MD5";
			case "1.2.840.10040.4.3":
			case "1.2.840.113549.1.1.5":
				return "SHA1";
			}
			throw new CryptographicException("Unsupported hash algorithm: " + this.signatureOID);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0003553C File Offset: 0x0003373C
		internal bool VerifySignature(DSA dsa)
		{
			if (this.signatureOID != "1.2.840.10040.4.3")
			{
				throw new CryptographicException("Unsupported hash algorithm: " + this.signatureOID);
			}
			DSASignatureDeformatter dsasignatureDeformatter = new DSASignatureDeformatter(dsa);
			dsasignatureDeformatter.SetHashAlgorithm("SHA1");
			ASN1 asn = new ASN1(this.signature);
			if (asn == null || asn.Count != 2)
			{
				return false;
			}
			byte[] value = asn[0].Value;
			byte[] value2 = asn[1].Value;
			byte[] array = new byte[40];
			int num = Math.Max(0, value.Length - 20);
			int dstOffset = Math.Max(0, 20 - value.Length);
			Buffer.BlockCopy(value, num, array, dstOffset, value.Length - num);
			int num2 = Math.Max(0, value2.Length - 20);
			int dstOffset2 = Math.Max(20, 40 - value2.Length);
			Buffer.BlockCopy(value2, num2, array, dstOffset2, value2.Length - num2);
			return dsasignatureDeformatter.VerifySignature(this.Hash, array);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00035634 File Offset: 0x00033834
		internal bool VerifySignature(RSA rsa)
		{
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			rsapkcs1SignatureDeformatter.SetHashAlgorithm(this.GetHashName());
			return rsapkcs1SignatureDeformatter.VerifySignature(this.Hash, this.signature);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00035668 File Offset: 0x00033868
		public bool VerifySignature(AsymmetricAlgorithm aa)
		{
			if (aa == null)
			{
				throw new ArgumentNullException("aa");
			}
			if (aa is RSA)
			{
				return this.VerifySignature(aa as RSA);
			}
			if (aa is DSA)
			{
				return this.VerifySignature(aa as DSA);
			}
			throw new NotSupportedException("Unknown Asymmetric Algorithm " + aa.ToString());
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000356CC File Offset: 0x000338CC
		public static X509Crl CreateFromFile(string filename)
		{
			byte[] array = null;
			using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return new X509Crl(array);
		}

		// Token: 0x0400030F RID: 783
		private string issuer;

		// Token: 0x04000310 RID: 784
		private byte version;

		// Token: 0x04000311 RID: 785
		private DateTime thisUpdate;

		// Token: 0x04000312 RID: 786
		private DateTime nextUpdate;

		// Token: 0x04000313 RID: 787
		private ArrayList entries;

		// Token: 0x04000314 RID: 788
		private string signatureOID;

		// Token: 0x04000315 RID: 789
		private byte[] signature;

		// Token: 0x04000316 RID: 790
		private X509ExtensionCollection extensions;

		// Token: 0x04000317 RID: 791
		private byte[] encoded;

		// Token: 0x04000318 RID: 792
		private byte[] hash_value;

		// Token: 0x020000CE RID: 206
		public class X509CrlEntry
		{
			// Token: 0x06000B93 RID: 2963 RVA: 0x00035738 File Offset: 0x00033938
			internal X509CrlEntry(byte[] serialNumber, DateTime revocationDate, X509ExtensionCollection extensions)
			{
				this.sn = serialNumber;
				this.revocationDate = revocationDate;
				if (extensions == null)
				{
					this.extensions = new X509ExtensionCollection();
				}
				else
				{
					this.extensions = extensions;
				}
			}

			// Token: 0x06000B94 RID: 2964 RVA: 0x0003576C File Offset: 0x0003396C
			internal X509CrlEntry(ASN1 entry)
			{
				this.sn = entry[0].Value;
				Array.Reverse(this.sn);
				this.revocationDate = ASN1Convert.ToDateTime(entry[1]);
				this.extensions = new X509ExtensionCollection(entry[2]);
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x06000B95 RID: 2965 RVA: 0x000357C0 File Offset: 0x000339C0
			public byte[] SerialNumber
			{
				get
				{
					return (byte[])this.sn.Clone();
				}
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x06000B96 RID: 2966 RVA: 0x000357D4 File Offset: 0x000339D4
			public DateTime RevocationDate
			{
				get
				{
					return this.revocationDate;
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x06000B97 RID: 2967 RVA: 0x000357DC File Offset: 0x000339DC
			public X509ExtensionCollection Extensions
			{
				get
				{
					return this.extensions;
				}
			}

			// Token: 0x06000B98 RID: 2968 RVA: 0x000357E4 File Offset: 0x000339E4
			public byte[] GetBytes()
			{
				ASN1 asn = new ASN1(48);
				asn.Add(new ASN1(2, this.sn));
				asn.Add(ASN1Convert.FromDateTime(this.revocationDate));
				if (this.extensions.Count > 0)
				{
					asn.Add(new ASN1(this.extensions.GetBytes()));
				}
				return asn.GetBytes();
			}

			// Token: 0x0400031B RID: 795
			private byte[] sn;

			// Token: 0x0400031C RID: 796
			private DateTime revocationDate;

			// Token: 0x0400031D RID: 797
			private X509ExtensionCollection extensions;
		}
	}
}
