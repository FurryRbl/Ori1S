using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x02000049 RID: 73
	public class X509Crl
	{
		// Token: 0x06000343 RID: 835 RVA: 0x00016CF0 File Offset: 0x00014EF0
		public X509Crl(byte[] crl)
		{
			if (crl == null)
			{
				throw new ArgumentNullException("crl");
			}
			this.encoded = (byte[])crl.Clone();
			this.Parse(this.encoded);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00016D34 File Offset: 0x00014F34
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00016FB4 File Offset: 0x000151B4
		public ArrayList Entries
		{
			get
			{
				return ArrayList.ReadOnly(this.entries);
			}
		}

		// Token: 0x170000B0 RID: 176
		public X509Crl.X509CrlEntry this[int index]
		{
			get
			{
				return (X509Crl.X509CrlEntry)this.entries[index];
			}
		}

		// Token: 0x170000B1 RID: 177
		public X509Crl.X509CrlEntry this[byte[] serialNumber]
		{
			get
			{
				return this.GetCrlEntry(serialNumber);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00016FE4 File Offset: 0x000151E4
		public X509ExtensionCollection Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00016FEC File Offset: 0x000151EC
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

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0001703C File Offset: 0x0001523C
		public string IssuerName
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00017044 File Offset: 0x00015244
		public DateTime NextUpdate
		{
			get
			{
				return this.nextUpdate;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0001704C File Offset: 0x0001524C
		public DateTime ThisUpdate
		{
			get
			{
				return this.thisUpdate;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00017054 File Offset: 0x00015254
		public string SignatureAlgorithm
		{
			get
			{
				return this.signatureOID;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0001705C File Offset: 0x0001525C
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

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0001707C File Offset: 0x0001527C
		public byte[] RawData
		{
			get
			{
				return (byte[])this.encoded.Clone();
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00017090 File Offset: 0x00015290
		public byte Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00017098 File Offset: 0x00015298
		public bool IsCurrent
		{
			get
			{
				return this.WasCurrent(DateTime.Now);
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000170A8 File Offset: 0x000152A8
		public bool WasCurrent(DateTime instant)
		{
			if (this.nextUpdate == DateTime.MinValue)
			{
				return instant >= this.thisUpdate;
			}
			return instant >= this.thisUpdate && instant <= this.nextUpdate;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000170F8 File Offset: 0x000152F8
		public byte[] GetBytes()
		{
			return (byte[])this.encoded.Clone();
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001710C File Offset: 0x0001530C
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

		// Token: 0x06000355 RID: 853 RVA: 0x00017164 File Offset: 0x00015364
		public X509Crl.X509CrlEntry GetCrlEntry(X509Certificate x509)
		{
			if (x509 == null)
			{
				throw new ArgumentNullException("x509");
			}
			return this.GetCrlEntry(x509.SerialNumber);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00017184 File Offset: 0x00015384
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

		// Token: 0x06000357 RID: 855 RVA: 0x000171E8 File Offset: 0x000153E8
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
				if (X509Crl.<>f__switch$map12 == null)
				{
					X509Crl.<>f__switch$map12 = new Dictionary<string, int>(1)
					{
						{
							"1.2.840.10040.4.3",
							0
						}
					};
				}
				int num;
				if (X509Crl.<>f__switch$map12.TryGetValue(text, out num))
				{
					if (num == 0)
					{
						return this.VerifySignature(x509.DSA);
					}
				}
			}
			return this.VerifySignature(x509.RSA);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000172EC File Offset: 0x000154EC
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

		// Token: 0x06000359 RID: 857 RVA: 0x000173A0 File Offset: 0x000155A0
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

		// Token: 0x0600035A RID: 858 RVA: 0x00017498 File Offset: 0x00015698
		internal bool VerifySignature(RSA rsa)
		{
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			rsapkcs1SignatureDeformatter.SetHashAlgorithm(this.GetHashName());
			return rsapkcs1SignatureDeformatter.VerifySignature(this.Hash, this.signature);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000174CC File Offset: 0x000156CC
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

		// Token: 0x0600035C RID: 860 RVA: 0x00017530 File Offset: 0x00015730
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

		// Token: 0x04000190 RID: 400
		private string issuer;

		// Token: 0x04000191 RID: 401
		private byte version;

		// Token: 0x04000192 RID: 402
		private DateTime thisUpdate;

		// Token: 0x04000193 RID: 403
		private DateTime nextUpdate;

		// Token: 0x04000194 RID: 404
		private ArrayList entries;

		// Token: 0x04000195 RID: 405
		private string signatureOID;

		// Token: 0x04000196 RID: 406
		private byte[] signature;

		// Token: 0x04000197 RID: 407
		private X509ExtensionCollection extensions;

		// Token: 0x04000198 RID: 408
		private byte[] encoded;

		// Token: 0x04000199 RID: 409
		private byte[] hash_value;

		// Token: 0x0200004A RID: 74
		public class X509CrlEntry
		{
			// Token: 0x0600035D RID: 861 RVA: 0x0001759C File Offset: 0x0001579C
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

			// Token: 0x0600035E RID: 862 RVA: 0x000175D0 File Offset: 0x000157D0
			internal X509CrlEntry(ASN1 entry)
			{
				this.sn = entry[0].Value;
				Array.Reverse(this.sn);
				this.revocationDate = ASN1Convert.ToDateTime(entry[1]);
				this.extensions = new X509ExtensionCollection(entry[2]);
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x0600035F RID: 863 RVA: 0x00017624 File Offset: 0x00015824
			public byte[] SerialNumber
			{
				get
				{
					return (byte[])this.sn.Clone();
				}
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06000360 RID: 864 RVA: 0x00017638 File Offset: 0x00015838
			public DateTime RevocationDate
			{
				get
				{
					return this.revocationDate;
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000361 RID: 865 RVA: 0x00017640 File Offset: 0x00015840
			public X509ExtensionCollection Extensions
			{
				get
				{
					return this.extensions;
				}
			}

			// Token: 0x06000362 RID: 866 RVA: 0x00017648 File Offset: 0x00015848
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

			// Token: 0x0400019C RID: 412
			private byte[] sn;

			// Token: 0x0400019D RID: 413
			private DateTime revocationDate;

			// Token: 0x0400019E RID: 414
			private X509ExtensionCollection extensions;
		}
	}
}
