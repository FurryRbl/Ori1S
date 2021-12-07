using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.X509;

namespace Mono.Security.Authenticode
{
	// Token: 0x02000020 RID: 32
	public class AuthenticodeFormatter : AuthenticodeBase
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00008E1C File Offset: 0x0000701C
		public AuthenticodeFormatter()
		{
			this.certs = new X509CertificateCollection();
			this.crls = new ArrayList();
			this.authority = Authority.Maximum;
			this.pkcs7 = new PKCS7.SignedData();
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00008E74 File Offset: 0x00007074
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00008E7C File Offset: 0x0000707C
		public Authority Authority
		{
			get
			{
				return this.authority;
			}
			set
			{
				this.authority = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00008E88 File Offset: 0x00007088
		public X509CertificateCollection Certificates
		{
			get
			{
				return this.certs;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00008E90 File Offset: 0x00007090
		public ArrayList Crl
		{
			get
			{
				return this.crls;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00008E98 File Offset: 0x00007098
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00008EB8 File Offset: 0x000070B8
		public string Hash
		{
			get
			{
				if (this.hash == null)
				{
					this.hash = "MD5";
				}
				return this.hash;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Hash");
				}
				string text = value.ToUpper(CultureInfo.InvariantCulture);
				string text2 = text;
				if (text2 != null)
				{
					if (AuthenticodeFormatter.<>f__switch$map4 == null)
					{
						AuthenticodeFormatter.<>f__switch$map4 = new Dictionary<string, int>(2)
						{
							{
								"MD5",
								0
							},
							{
								"SHA1",
								0
							}
						};
					}
					int num;
					if (AuthenticodeFormatter.<>f__switch$map4.TryGetValue(text2, out num))
					{
						if (num == 0)
						{
							this.hash = text;
							return;
						}
					}
				}
				throw new ArgumentException("Invalid Authenticode hash algorithm");
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00008F50 File Offset: 0x00007150
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00008F58 File Offset: 0x00007158
		public RSA RSA
		{
			get
			{
				return this.rsa;
			}
			set
			{
				this.rsa = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00008F64 File Offset: 0x00007164
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00008F6C File Offset: 0x0000716C
		public Uri TimestampUrl
		{
			get
			{
				return this.timestamp;
			}
			set
			{
				this.timestamp = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00008F78 File Offset: 0x00007178
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00008F80 File Offset: 0x00007180
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00008F8C File Offset: 0x0000718C
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00008F94 File Offset: 0x00007194
		public Uri Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008FA0 File Offset: 0x000071A0
		private ASN1 AlgorithmIdentifier(string oid)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(new ASN1(5));
			return asn;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008FD0 File Offset: 0x000071D0
		private ASN1 Attribute(string oid, ASN1 value)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			ASN1 asn2 = asn.Add(new ASN1(49));
			asn2.Add(value);
			return asn;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000900C File Offset: 0x0000720C
		private ASN1 Opus(string description, string url)
		{
			ASN1 asn = new ASN1(48);
			if (description != null)
			{
				ASN1 asn2 = asn.Add(new ASN1(160));
				asn2.Add(new ASN1(128, Encoding.BigEndianUnicode.GetBytes(description)));
			}
			if (url != null)
			{
				ASN1 asn3 = asn.Add(new ASN1(161));
				asn3.Add(new ASN1(128, Encoding.ASCII.GetBytes(url)));
			}
			return asn;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00009088 File Offset: 0x00007288
		private byte[] Header(byte[] fileHash, string hashAlgorithm)
		{
			string oid = CryptoConfig.MapNameToOID(hashAlgorithm);
			ASN1 asn = new ASN1(48);
			ASN1 asn2 = asn.Add(new ASN1(48));
			asn2.Add(ASN1Convert.FromOid("1.3.6.1.4.1.311.2.1.15"));
			asn2.Add(new ASN1(48, AuthenticodeFormatter.obsolete));
			ASN1 asn3 = asn.Add(new ASN1(48));
			asn3.Add(this.AlgorithmIdentifier(oid));
			asn3.Add(new ASN1(4, fileHash));
			this.pkcs7.HashName = hashAlgorithm;
			this.pkcs7.Certificates.AddRange(this.certs);
			this.pkcs7.ContentInfo.ContentType = "1.3.6.1.4.1.311.2.1.4";
			this.pkcs7.ContentInfo.Content.Add(asn);
			this.pkcs7.SignerInfo.Certificate = this.certs[0];
			this.pkcs7.SignerInfo.Key = this.rsa;
			ASN1 value;
			if (this.url == null)
			{
				value = this.Attribute("1.3.6.1.4.1.311.2.1.12", this.Opus(this.description, null));
			}
			else
			{
				value = this.Attribute("1.3.6.1.4.1.311.2.1.12", this.Opus(this.description, this.url.ToString()));
			}
			this.pkcs7.SignerInfo.AuthenticatedAttributes.Add(value);
			this.pkcs7.GetASN1();
			return this.pkcs7.SignerInfo.Signature;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000920C File Offset: 0x0000740C
		public ASN1 TimestampRequest(byte[] signature)
		{
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo("1.2.840.113549.1.7.1");
			contentInfo.Content.Add(new ASN1(4, signature));
			return PKCS7.AlgorithmIdentifier("1.3.6.1.4.1.311.3.2.1", contentInfo.ASN1);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00009248 File Offset: 0x00007448
		public void ProcessTimestamp(byte[] response)
		{
			ASN1 asn = new ASN1(Convert.FromBase64String(Encoding.ASCII.GetString(response)));
			for (int i = 0; i < asn[1][0][3].Count; i++)
			{
				this.pkcs7.Certificates.Add(new X509Certificate(asn[1][0][3][i].GetBytes()));
			}
			this.pkcs7.SignerInfo.UnauthenticatedAttributes.Add(this.Attribute("1.2.840.113549.1.9.6", asn[1][0][4][0]));
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00009304 File Offset: 0x00007504
		private byte[] Timestamp(byte[] signature)
		{
			ASN1 asn = this.TimestampRequest(signature);
			WebClient webClient = new WebClient();
			webClient.Headers.Add("Content-Type", "application/octet-stream");
			webClient.Headers.Add("Accept", "application/octet-stream");
			byte[] bytes = Encoding.ASCII.GetBytes(Convert.ToBase64String(asn.GetBytes()));
			return webClient.UploadData(this.timestamp.ToString(), bytes);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00009374 File Offset: 0x00007574
		private bool Save(string fileName, byte[] asn)
		{
			File.Copy(fileName, fileName + ".bak", true);
			using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite))
			{
				int num;
				if (base.SecurityOffset > 0)
				{
					num = base.SecurityOffset;
				}
				else if (base.CoffSymbolTableOffset > 0)
				{
					fileStream.Seek((long)(base.PEOffset + 12), SeekOrigin.Begin);
					for (int i = 0; i < 8; i++)
					{
						fileStream.WriteByte(0);
					}
					num = base.CoffSymbolTableOffset;
				}
				else
				{
					num = (int)fileStream.Length;
				}
				int num2 = num & 7;
				if (num2 > 0)
				{
					num2 = 8 - num2;
				}
				byte[] bytes = BitConverterLE.GetBytes(num + num2);
				fileStream.Seek((long)(base.PEOffset + 152), SeekOrigin.Begin);
				fileStream.Write(bytes, 0, 4);
				int num3 = asn.Length + 8;
				int num4 = num3 & 7;
				if (num4 > 0)
				{
					num4 = 8 - num4;
				}
				bytes = BitConverterLE.GetBytes(num3 + num4);
				fileStream.Seek((long)(base.PEOffset + 156), SeekOrigin.Begin);
				fileStream.Write(bytes, 0, 4);
				fileStream.Seek((long)num, SeekOrigin.Begin);
				if (num2 > 0)
				{
					byte[] array = new byte[num2];
					fileStream.Write(array, 0, array.Length);
				}
				fileStream.Write(bytes, 0, bytes.Length);
				bytes = BitConverterLE.GetBytes(131584);
				fileStream.Write(bytes, 0, bytes.Length);
				fileStream.Write(asn, 0, asn.Length);
				if (num4 > 0)
				{
					byte[] array2 = new byte[num4];
					fileStream.Write(array2, 0, array2.Length);
				}
				fileStream.Close();
			}
			return true;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00009528 File Offset: 0x00007728
		public bool Sign(string fileName)
		{
			try
			{
				base.Open(fileName);
				HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.Hash);
				byte[] fileHash = base.GetHash(hashAlgorithm);
				byte[] signature = this.Header(fileHash, this.Hash);
				if (this.timestamp != null)
				{
					byte[] response = this.Timestamp(signature);
					this.ProcessTimestamp(response);
				}
				PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo("1.2.840.113549.1.7.2");
				contentInfo.Content.Add(this.pkcs7.ASN1);
				this.authenticode = contentInfo.ASN1;
				base.Close();
				return this.Save(fileName, this.authenticode.GetBytes());
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return false;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00009604 File Offset: 0x00007804
		public bool Timestamp(string fileName)
		{
			try
			{
				AuthenticodeDeformatter authenticodeDeformatter = new AuthenticodeDeformatter(fileName);
				byte[] signature = authenticodeDeformatter.Signature;
				if (signature != null)
				{
					base.Open(fileName);
					PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(signature);
					this.pkcs7 = new PKCS7.SignedData(contentInfo.Content);
					byte[] bytes = this.Timestamp(this.pkcs7.SignerInfo.Signature);
					ASN1 asn = new ASN1(Convert.FromBase64String(Encoding.ASCII.GetString(bytes)));
					ASN1 asn2 = new ASN1(signature);
					ASN1 asn3 = asn2.Element(1, 160);
					if (asn3 == null)
					{
						return false;
					}
					ASN1 asn4 = asn3.Element(0, 48);
					if (asn4 == null)
					{
						return false;
					}
					ASN1 asn5 = asn4.Element(3, 160);
					if (asn5 == null)
					{
						asn5 = new ASN1(160);
						asn4.Add(asn5);
					}
					for (int i = 0; i < asn[1][0][3].Count; i++)
					{
						asn5.Add(asn[1][0][3][i]);
					}
					ASN1 asn6 = asn4[asn4.Count - 1];
					ASN1 asn7 = asn6[0];
					ASN1 asn8 = asn7[asn7.Count - 1];
					if (asn8.Tag != 161)
					{
						asn8 = new ASN1(161);
						asn7.Add(asn8);
					}
					asn8.Add(this.Attribute("1.2.840.113549.1.9.6", asn[1][0][4][0]));
					return this.Save(fileName, asn2.GetBytes());
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return false;
		}

		// Token: 0x0400008E RID: 142
		private const string signedData = "1.2.840.113549.1.7.2";

		// Token: 0x0400008F RID: 143
		private const string countersignature = "1.2.840.113549.1.9.6";

		// Token: 0x04000090 RID: 144
		private const string spcStatementType = "1.3.6.1.4.1.311.2.1.11";

		// Token: 0x04000091 RID: 145
		private const string spcSpOpusInfo = "1.3.6.1.4.1.311.2.1.12";

		// Token: 0x04000092 RID: 146
		private const string spcPelmageData = "1.3.6.1.4.1.311.2.1.15";

		// Token: 0x04000093 RID: 147
		private const string commercialCodeSigning = "1.3.6.1.4.1.311.2.1.22";

		// Token: 0x04000094 RID: 148
		private const string timestampCountersignature = "1.3.6.1.4.1.311.3.2.1";

		// Token: 0x04000095 RID: 149
		private Authority authority;

		// Token: 0x04000096 RID: 150
		private X509CertificateCollection certs;

		// Token: 0x04000097 RID: 151
		private ArrayList crls;

		// Token: 0x04000098 RID: 152
		private string hash;

		// Token: 0x04000099 RID: 153
		private RSA rsa;

		// Token: 0x0400009A RID: 154
		private Uri timestamp;

		// Token: 0x0400009B RID: 155
		private ASN1 authenticode;

		// Token: 0x0400009C RID: 156
		private PKCS7.SignedData pkcs7;

		// Token: 0x0400009D RID: 157
		private string description;

		// Token: 0x0400009E RID: 158
		private Uri url;

		// Token: 0x0400009F RID: 159
		private static byte[] obsolete = new byte[]
		{
			3,
			1,
			0,
			160,
			32,
			162,
			30,
			128,
			28,
			0,
			60,
			0,
			60,
			0,
			60,
			0,
			79,
			0,
			98,
			0,
			115,
			0,
			111,
			0,
			108,
			0,
			101,
			0,
			116,
			0,
			101,
			0,
			62,
			0,
			62,
			0,
			62
		};
	}
}
