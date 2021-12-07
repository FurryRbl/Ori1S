using System;
using System.Collections;
using System.Security.Cryptography;
using Mono.Security.X509;

namespace Mono.Security
{
	// Token: 0x020000A0 RID: 160
	internal sealed class PKCS7
	{
		// Token: 0x0600092D RID: 2349 RVA: 0x00023810 File Offset: 0x00021A10
		private PKCS7()
		{
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00023818 File Offset: 0x00021A18
		public static ASN1 Attribute(string oid, ASN1 value)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			ASN1 asn2 = asn.Add(new ASN1(49));
			asn2.Add(value);
			return asn;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00023854 File Offset: 0x00021A54
		public static ASN1 AlgorithmIdentifier(string oid)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(new ASN1(5));
			return asn;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00023884 File Offset: 0x00021A84
		public static ASN1 AlgorithmIdentifier(string oid, ASN1 parameters)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(parameters);
			return asn;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000238B0 File Offset: 0x00021AB0
		public static ASN1 IssuerAndSerialNumber(X509Certificate x509)
		{
			ASN1 asn = null;
			ASN1 asn2 = null;
			ASN1 asn3 = new ASN1(x509.RawData);
			int i = 0;
			bool flag = false;
			while (i < asn3[0].Count)
			{
				ASN1 asn4 = asn3[0][i++];
				if (asn4.Tag == 2)
				{
					asn2 = asn4;
				}
				else if (asn4.Tag == 48)
				{
					if (flag)
					{
						asn = asn4;
						break;
					}
					flag = true;
				}
			}
			ASN1 asn5 = new ASN1(48);
			asn5.Add(asn);
			asn5.Add(asn2);
			return asn5;
		}

		// Token: 0x020000A1 RID: 161
		public class Oid
		{
			// Token: 0x040001CA RID: 458
			public const string rsaEncryption = "1.2.840.113549.1.1.1";

			// Token: 0x040001CB RID: 459
			public const string data = "1.2.840.113549.1.7.1";

			// Token: 0x040001CC RID: 460
			public const string signedData = "1.2.840.113549.1.7.2";

			// Token: 0x040001CD RID: 461
			public const string envelopedData = "1.2.840.113549.1.7.3";

			// Token: 0x040001CE RID: 462
			public const string signedAndEnvelopedData = "1.2.840.113549.1.7.4";

			// Token: 0x040001CF RID: 463
			public const string digestedData = "1.2.840.113549.1.7.5";

			// Token: 0x040001D0 RID: 464
			public const string encryptedData = "1.2.840.113549.1.7.6";

			// Token: 0x040001D1 RID: 465
			public const string contentType = "1.2.840.113549.1.9.3";

			// Token: 0x040001D2 RID: 466
			public const string messageDigest = "1.2.840.113549.1.9.4";

			// Token: 0x040001D3 RID: 467
			public const string signingTime = "1.2.840.113549.1.9.5";

			// Token: 0x040001D4 RID: 468
			public const string countersignature = "1.2.840.113549.1.9.6";
		}

		// Token: 0x020000A2 RID: 162
		public class ContentInfo
		{
			// Token: 0x06000933 RID: 2355 RVA: 0x00023958 File Offset: 0x00021B58
			public ContentInfo()
			{
				this.content = new ASN1(160);
			}

			// Token: 0x06000934 RID: 2356 RVA: 0x00023970 File Offset: 0x00021B70
			public ContentInfo(string oid) : this()
			{
				this.contentType = oid;
			}

			// Token: 0x06000935 RID: 2357 RVA: 0x00023980 File Offset: 0x00021B80
			public ContentInfo(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x06000936 RID: 2358 RVA: 0x00023990 File Offset: 0x00021B90
			public ContentInfo(ASN1 asn1)
			{
				if (asn1.Tag != 48 || (asn1.Count < 1 && asn1.Count > 2))
				{
					throw new ArgumentException("Invalid ASN1");
				}
				if (asn1[0].Tag != 6)
				{
					throw new ArgumentException("Invalid contentType");
				}
				this.contentType = ASN1Convert.ToOid(asn1[0]);
				if (asn1.Count > 1)
				{
					if (asn1[1].Tag != 160)
					{
						throw new ArgumentException("Invalid content");
					}
					this.content = asn1[1];
				}
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x06000937 RID: 2359 RVA: 0x00023A3C File Offset: 0x00021C3C
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x06000938 RID: 2360 RVA: 0x00023A44 File Offset: 0x00021C44
			// (set) Token: 0x06000939 RID: 2361 RVA: 0x00023A4C File Offset: 0x00021C4C
			public ASN1 Content
			{
				get
				{
					return this.content;
				}
				set
				{
					this.content = value;
				}
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x0600093A RID: 2362 RVA: 0x00023A58 File Offset: 0x00021C58
			// (set) Token: 0x0600093B RID: 2363 RVA: 0x00023A60 File Offset: 0x00021C60
			public string ContentType
			{
				get
				{
					return this.contentType;
				}
				set
				{
					this.contentType = value;
				}
			}

			// Token: 0x0600093C RID: 2364 RVA: 0x00023A6C File Offset: 0x00021C6C
			internal ASN1 GetASN1()
			{
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this.contentType));
				if (this.content != null && this.content.Count > 0)
				{
					asn.Add(this.content);
				}
				return asn;
			}

			// Token: 0x0600093D RID: 2365 RVA: 0x00023AC0 File Offset: 0x00021CC0
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x040001D5 RID: 469
			private string contentType;

			// Token: 0x040001D6 RID: 470
			private ASN1 content;
		}

		// Token: 0x020000A3 RID: 163
		public class EncryptedData
		{
			// Token: 0x0600093E RID: 2366 RVA: 0x00023AD0 File Offset: 0x00021CD0
			public EncryptedData()
			{
				this._version = 0;
			}

			// Token: 0x0600093F RID: 2367 RVA: 0x00023AE0 File Offset: 0x00021CE0
			public EncryptedData(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x06000940 RID: 2368 RVA: 0x00023AF0 File Offset: 0x00021CF0
			public EncryptedData(ASN1 asn1) : this()
			{
				if (asn1.Tag != 48 || asn1.Count < 2)
				{
					throw new ArgumentException("Invalid EncryptedData");
				}
				if (asn1[0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this._version = asn1[0].Value[0];
				ASN1 asn2 = asn1[1];
				if (asn2.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo");
				}
				ASN1 asn3 = asn2[0];
				if (asn3.Tag != 6)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentType");
				}
				this._content = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn3));
				ASN1 asn4 = asn2[1];
				if (asn4.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentEncryptionAlgorithmIdentifier");
				}
				this._encryptionAlgorithm = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn4[0]));
				this._encryptionAlgorithm.Content = asn4[1];
				ASN1 asn5 = asn2[2];
				if (asn5.Tag != 128)
				{
					throw new ArgumentException("missing EncryptedContentInfo.EncryptedContent");
				}
				this._encrypted = asn5.Value;
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x06000941 RID: 2369 RVA: 0x00023C20 File Offset: 0x00021E20
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x06000942 RID: 2370 RVA: 0x00023C28 File Offset: 0x00021E28
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this._content;
				}
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x06000943 RID: 2371 RVA: 0x00023C30 File Offset: 0x00021E30
			public PKCS7.ContentInfo EncryptionAlgorithm
			{
				get
				{
					return this._encryptionAlgorithm;
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x06000944 RID: 2372 RVA: 0x00023C38 File Offset: 0x00021E38
			public byte[] EncryptedContent
			{
				get
				{
					if (this._encrypted == null)
					{
						return null;
					}
					return (byte[])this._encrypted.Clone();
				}
			}

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x06000945 RID: 2373 RVA: 0x00023C58 File Offset: 0x00021E58
			// (set) Token: 0x06000946 RID: 2374 RVA: 0x00023C60 File Offset: 0x00021E60
			public byte Version
			{
				get
				{
					return this._version;
				}
				set
				{
					this._version = value;
				}
			}

			// Token: 0x06000947 RID: 2375 RVA: 0x00023C6C File Offset: 0x00021E6C
			internal ASN1 GetASN1()
			{
				return null;
			}

			// Token: 0x06000948 RID: 2376 RVA: 0x00023C70 File Offset: 0x00021E70
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x040001D7 RID: 471
			private byte _version;

			// Token: 0x040001D8 RID: 472
			private PKCS7.ContentInfo _content;

			// Token: 0x040001D9 RID: 473
			private PKCS7.ContentInfo _encryptionAlgorithm;

			// Token: 0x040001DA RID: 474
			private byte[] _encrypted;
		}

		// Token: 0x020000A4 RID: 164
		public class EnvelopedData
		{
			// Token: 0x06000949 RID: 2377 RVA: 0x00023C80 File Offset: 0x00021E80
			public EnvelopedData()
			{
				this._version = 0;
				this._content = new PKCS7.ContentInfo();
				this._encryptionAlgorithm = new PKCS7.ContentInfo();
				this._recipientInfos = new ArrayList();
			}

			// Token: 0x0600094A RID: 2378 RVA: 0x00023CBC File Offset: 0x00021EBC
			public EnvelopedData(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x0600094B RID: 2379 RVA: 0x00023CCC File Offset: 0x00021ECC
			public EnvelopedData(ASN1 asn1) : this()
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 3)
				{
					throw new ArgumentException("Invalid EnvelopedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this._version = asn1[0][0].Value[0];
				ASN1 asn2 = asn1[0][1];
				if (asn2.Tag != 49)
				{
					throw new ArgumentException("missing RecipientInfos");
				}
				for (int i = 0; i < asn2.Count; i++)
				{
					ASN1 data = asn2[i];
					this._recipientInfos.Add(new PKCS7.RecipientInfo(data));
				}
				ASN1 asn3 = asn1[0][2];
				if (asn3.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo");
				}
				ASN1 asn4 = asn3[0];
				if (asn4.Tag != 6)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentType");
				}
				this._content = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn4));
				ASN1 asn5 = asn3[1];
				if (asn5.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentEncryptionAlgorithmIdentifier");
				}
				this._encryptionAlgorithm = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn5[0]));
				this._encryptionAlgorithm.Content = asn5[1];
				ASN1 asn6 = asn3[2];
				if (asn6.Tag != 128)
				{
					throw new ArgumentException("missing EncryptedContentInfo.EncryptedContent");
				}
				this._encrypted = asn6.Value;
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x0600094C RID: 2380 RVA: 0x00023E7C File Offset: 0x0002207C
			public ArrayList RecipientInfos
			{
				get
				{
					return this._recipientInfos;
				}
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x0600094D RID: 2381 RVA: 0x00023E84 File Offset: 0x00022084
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x0600094E RID: 2382 RVA: 0x00023E8C File Offset: 0x0002208C
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this._content;
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x0600094F RID: 2383 RVA: 0x00023E94 File Offset: 0x00022094
			public PKCS7.ContentInfo EncryptionAlgorithm
			{
				get
				{
					return this._encryptionAlgorithm;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000950 RID: 2384 RVA: 0x00023E9C File Offset: 0x0002209C
			public byte[] EncryptedContent
			{
				get
				{
					if (this._encrypted == null)
					{
						return null;
					}
					return (byte[])this._encrypted.Clone();
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000951 RID: 2385 RVA: 0x00023EBC File Offset: 0x000220BC
			// (set) Token: 0x06000952 RID: 2386 RVA: 0x00023EC4 File Offset: 0x000220C4
			public byte Version
			{
				get
				{
					return this._version;
				}
				set
				{
					this._version = value;
				}
			}

			// Token: 0x06000953 RID: 2387 RVA: 0x00023ED0 File Offset: 0x000220D0
			internal ASN1 GetASN1()
			{
				return new ASN1(48);
			}

			// Token: 0x06000954 RID: 2388 RVA: 0x00023EE8 File Offset: 0x000220E8
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x040001DB RID: 475
			private byte _version;

			// Token: 0x040001DC RID: 476
			private PKCS7.ContentInfo _content;

			// Token: 0x040001DD RID: 477
			private PKCS7.ContentInfo _encryptionAlgorithm;

			// Token: 0x040001DE RID: 478
			private ArrayList _recipientInfos;

			// Token: 0x040001DF RID: 479
			private byte[] _encrypted;
		}

		// Token: 0x020000A5 RID: 165
		public class RecipientInfo
		{
			// Token: 0x06000955 RID: 2389 RVA: 0x00023EF8 File Offset: 0x000220F8
			public RecipientInfo()
			{
			}

			// Token: 0x06000956 RID: 2390 RVA: 0x00023F00 File Offset: 0x00022100
			public RecipientInfo(ASN1 data)
			{
				if (data.Tag != 48)
				{
					throw new ArgumentException("Invalid RecipientInfo");
				}
				ASN1 asn = data[0];
				if (asn.Tag != 2)
				{
					throw new ArgumentException("missing Version");
				}
				this._version = (int)asn.Value[0];
				ASN1 asn2 = data[1];
				if (asn2.Tag == 128 && this._version == 3)
				{
					this._ski = asn2.Value;
				}
				else
				{
					this._issuer = X501.ToString(asn2[0]);
					this._serial = asn2[1].Value;
				}
				ASN1 asn3 = data[2];
				this._oid = ASN1Convert.ToOid(asn3[0]);
				ASN1 asn4 = data[3];
				this._key = asn4.Value;
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x06000957 RID: 2391 RVA: 0x00023FE0 File Offset: 0x000221E0
			public string Oid
			{
				get
				{
					return this._oid;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x06000958 RID: 2392 RVA: 0x00023FE8 File Offset: 0x000221E8
			public byte[] Key
			{
				get
				{
					if (this._key == null)
					{
						return null;
					}
					return (byte[])this._key.Clone();
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x06000959 RID: 2393 RVA: 0x00024008 File Offset: 0x00022208
			public byte[] SubjectKeyIdentifier
			{
				get
				{
					if (this._ski == null)
					{
						return null;
					}
					return (byte[])this._ski.Clone();
				}
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x0600095A RID: 2394 RVA: 0x00024028 File Offset: 0x00022228
			public string Issuer
			{
				get
				{
					return this._issuer;
				}
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x0600095B RID: 2395 RVA: 0x00024030 File Offset: 0x00022230
			public byte[] Serial
			{
				get
				{
					if (this._serial == null)
					{
						return null;
					}
					return (byte[])this._serial.Clone();
				}
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x0600095C RID: 2396 RVA: 0x00024050 File Offset: 0x00022250
			public int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x040001E0 RID: 480
			private int _version;

			// Token: 0x040001E1 RID: 481
			private string _oid;

			// Token: 0x040001E2 RID: 482
			private byte[] _key;

			// Token: 0x040001E3 RID: 483
			private byte[] _ski;

			// Token: 0x040001E4 RID: 484
			private string _issuer;

			// Token: 0x040001E5 RID: 485
			private byte[] _serial;
		}

		// Token: 0x020000A6 RID: 166
		public class SignedData
		{
			// Token: 0x0600095D RID: 2397 RVA: 0x00024058 File Offset: 0x00022258
			public SignedData()
			{
				this.version = 1;
				this.contentInfo = new PKCS7.ContentInfo();
				this.certs = new X509CertificateCollection();
				this.crls = new ArrayList();
				this.signerInfo = new PKCS7.SignerInfo();
				this.mda = true;
				this.signed = false;
			}

			// Token: 0x0600095E RID: 2398 RVA: 0x000240AC File Offset: 0x000222AC
			public SignedData(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x0600095F RID: 2399 RVA: 0x000240BC File Offset: 0x000222BC
			public SignedData(ASN1 asn1)
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 4)
				{
					throw new ArgumentException("Invalid SignedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this.version = asn1[0][0].Value[0];
				this.contentInfo = new PKCS7.ContentInfo(asn1[0][2]);
				int num = 3;
				this.certs = new X509CertificateCollection();
				if (asn1[0][num].Tag == 160)
				{
					for (int i = 0; i < asn1[0][num].Count; i++)
					{
						this.certs.Add(new X509Certificate(asn1[0][num][i].GetBytes()));
					}
					num++;
				}
				this.crls = new ArrayList();
				if (asn1[0][num].Tag == 161)
				{
					for (int j = 0; j < asn1[0][num].Count; j++)
					{
						this.crls.Add(asn1[0][num][j].GetBytes());
					}
					num++;
				}
				if (asn1[0][num].Count > 0)
				{
					this.signerInfo = new PKCS7.SignerInfo(asn1[0][num]);
				}
				else
				{
					this.signerInfo = new PKCS7.SignerInfo();
				}
				if (this.signerInfo.HashName != null)
				{
					this.HashName = this.OidToName(this.signerInfo.HashName);
				}
				this.mda = (this.signerInfo.AuthenticatedAttributes.Count > 0);
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000960 RID: 2400 RVA: 0x000242C0 File Offset: 0x000224C0
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x06000961 RID: 2401 RVA: 0x000242C8 File Offset: 0x000224C8
			public X509CertificateCollection Certificates
			{
				get
				{
					return this.certs;
				}
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x06000962 RID: 2402 RVA: 0x000242D0 File Offset: 0x000224D0
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this.contentInfo;
				}
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06000963 RID: 2403 RVA: 0x000242D8 File Offset: 0x000224D8
			public ArrayList Crls
			{
				get
				{
					return this.crls;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x06000964 RID: 2404 RVA: 0x000242E0 File Offset: 0x000224E0
			// (set) Token: 0x06000965 RID: 2405 RVA: 0x000242E8 File Offset: 0x000224E8
			public string HashName
			{
				get
				{
					return this.hashAlgorithm;
				}
				set
				{
					this.hashAlgorithm = value;
					this.signerInfo.HashName = value;
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06000966 RID: 2406 RVA: 0x00024300 File Offset: 0x00022500
			public PKCS7.SignerInfo SignerInfo
			{
				get
				{
					return this.signerInfo;
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000967 RID: 2407 RVA: 0x00024308 File Offset: 0x00022508
			// (set) Token: 0x06000968 RID: 2408 RVA: 0x00024310 File Offset: 0x00022510
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

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x06000969 RID: 2409 RVA: 0x0002431C File Offset: 0x0002251C
			// (set) Token: 0x0600096A RID: 2410 RVA: 0x00024324 File Offset: 0x00022524
			public bool UseAuthenticatedAttributes
			{
				get
				{
					return this.mda;
				}
				set
				{
					this.mda = value;
				}
			}

			// Token: 0x0600096B RID: 2411 RVA: 0x00024330 File Offset: 0x00022530
			public bool VerifySignature(AsymmetricAlgorithm aa)
			{
				if (aa == null)
				{
					return false;
				}
				RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(aa);
				rsapkcs1SignatureDeformatter.SetHashAlgorithm(this.hashAlgorithm);
				HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.hashAlgorithm);
				byte[] signature = this.signerInfo.Signature;
				byte[] array;
				if (this.mda)
				{
					ASN1 asn = new ASN1(49);
					foreach (object obj in this.signerInfo.AuthenticatedAttributes)
					{
						ASN1 asn2 = (ASN1)obj;
						asn.Add(asn2);
					}
					array = hashAlgorithm.ComputeHash(asn.GetBytes());
				}
				else
				{
					array = hashAlgorithm.ComputeHash(this.contentInfo.Content[0].Value);
				}
				return array != null && signature != null && rsapkcs1SignatureDeformatter.VerifySignature(array, signature);
			}

			// Token: 0x0600096C RID: 2412 RVA: 0x00024440 File Offset: 0x00022640
			internal string OidToName(string oid)
			{
				switch (oid)
				{
				case "1.3.14.3.2.26":
					return "SHA1";
				case "1.2.840.113549.2.2":
					return "MD2";
				case "1.2.840.113549.2.5":
					return "MD5";
				case "2.16.840.1.101.3.4.1":
					return "SHA256";
				case "2.16.840.1.101.3.4.2":
					return "SHA384";
				case "2.16.840.1.101.3.4.3":
					return "SHA512";
				}
				return oid;
			}

			// Token: 0x0600096D RID: 2413 RVA: 0x00024514 File Offset: 0x00022714
			internal ASN1 GetASN1()
			{
				ASN1 asn = new ASN1(48);
				byte[] data = new byte[]
				{
					this.version
				};
				asn.Add(new ASN1(2, data));
				ASN1 asn2 = asn.Add(new ASN1(49));
				if (this.hashAlgorithm != null)
				{
					string oid = CryptoConfig.MapNameToOID(this.hashAlgorithm);
					asn2.Add(PKCS7.AlgorithmIdentifier(oid));
				}
				ASN1 asn3 = this.contentInfo.ASN1;
				asn.Add(asn3);
				if (!this.signed && this.hashAlgorithm != null)
				{
					if (this.mda)
					{
						ASN1 value = PKCS7.Attribute("1.2.840.113549.1.9.3", asn3[0]);
						this.signerInfo.AuthenticatedAttributes.Add(value);
						HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.hashAlgorithm);
						byte[] data2 = hashAlgorithm.ComputeHash(asn3[1][0].Value);
						ASN1 asn4 = new ASN1(48);
						ASN1 value2 = PKCS7.Attribute("1.2.840.113549.1.9.4", asn4.Add(new ASN1(4, data2)));
						this.signerInfo.AuthenticatedAttributes.Add(value2);
					}
					else
					{
						RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(this.signerInfo.Key);
						rsapkcs1SignatureFormatter.SetHashAlgorithm(this.hashAlgorithm);
						HashAlgorithm hashAlgorithm2 = HashAlgorithm.Create(this.hashAlgorithm);
						byte[] rgbHash = hashAlgorithm2.ComputeHash(asn3[1][0].Value);
						this.signerInfo.Signature = rsapkcs1SignatureFormatter.CreateSignature(rgbHash);
					}
					this.signed = true;
				}
				if (this.certs.Count > 0)
				{
					ASN1 asn5 = asn.Add(new ASN1(160));
					foreach (X509Certificate x509Certificate in this.certs)
					{
						asn5.Add(new ASN1(x509Certificate.RawData));
					}
				}
				if (this.crls.Count > 0)
				{
					ASN1 asn6 = asn.Add(new ASN1(161));
					foreach (object obj in this.crls)
					{
						byte[] data3 = (byte[])obj;
						asn6.Add(new ASN1(data3));
					}
				}
				ASN1 asn7 = asn.Add(new ASN1(49));
				if (this.signerInfo.Key != null)
				{
					asn7.Add(this.signerInfo.ASN1);
				}
				return asn;
			}

			// Token: 0x0600096E RID: 2414 RVA: 0x000247F8 File Offset: 0x000229F8
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x040001E6 RID: 486
			private byte version;

			// Token: 0x040001E7 RID: 487
			private string hashAlgorithm;

			// Token: 0x040001E8 RID: 488
			private PKCS7.ContentInfo contentInfo;

			// Token: 0x040001E9 RID: 489
			private X509CertificateCollection certs;

			// Token: 0x040001EA RID: 490
			private ArrayList crls;

			// Token: 0x040001EB RID: 491
			private PKCS7.SignerInfo signerInfo;

			// Token: 0x040001EC RID: 492
			private bool mda;

			// Token: 0x040001ED RID: 493
			private bool signed;
		}

		// Token: 0x020000A7 RID: 167
		public class SignerInfo
		{
			// Token: 0x0600096F RID: 2415 RVA: 0x00024808 File Offset: 0x00022A08
			public SignerInfo()
			{
				this.version = 1;
				this.authenticatedAttributes = new ArrayList();
				this.unauthenticatedAttributes = new ArrayList();
			}

			// Token: 0x06000970 RID: 2416 RVA: 0x00024830 File Offset: 0x00022A30
			public SignerInfo(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x06000971 RID: 2417 RVA: 0x00024840 File Offset: 0x00022A40
			public SignerInfo(ASN1 asn1) : this()
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 5)
				{
					throw new ArgumentException("Invalid SignedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this.version = asn1[0][0].Value[0];
				ASN1 asn2 = asn1[0][1];
				if (asn2.Tag == 128 && this.version == 3)
				{
					this.ski = asn2.Value;
				}
				else
				{
					this.issuer = X501.ToString(asn2[0]);
					this.serial = asn2[1].Value;
				}
				ASN1 asn3 = asn1[0][2];
				this.hashAlgorithm = ASN1Convert.ToOid(asn3[0]);
				int num = 3;
				ASN1 asn4 = asn1[0][num];
				if (asn4.Tag == 160)
				{
					num++;
					for (int i = 0; i < asn4.Count; i++)
					{
						this.authenticatedAttributes.Add(asn4[i]);
					}
				}
				num++;
				ASN1 asn5 = asn1[0][num++];
				if (asn5.Tag == 4)
				{
					this.signature = asn5.Value;
				}
				ASN1 asn6 = asn1[0][num];
				if (asn6 != null && asn6.Tag == 161)
				{
					for (int j = 0; j < asn6.Count; j++)
					{
						this.unauthenticatedAttributes.Add(asn6[j]);
					}
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x06000972 RID: 2418 RVA: 0x00024A1C File Offset: 0x00022C1C
			public string IssuerName
			{
				get
				{
					return this.issuer;
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06000973 RID: 2419 RVA: 0x00024A24 File Offset: 0x00022C24
			public byte[] SerialNumber
			{
				get
				{
					if (this.serial == null)
					{
						return null;
					}
					return (byte[])this.serial.Clone();
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000974 RID: 2420 RVA: 0x00024A44 File Offset: 0x00022C44
			public byte[] SubjectKeyIdentifier
			{
				get
				{
					if (this.ski == null)
					{
						return null;
					}
					return (byte[])this.ski.Clone();
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06000975 RID: 2421 RVA: 0x00024A64 File Offset: 0x00022C64
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x06000976 RID: 2422 RVA: 0x00024A6C File Offset: 0x00022C6C
			public ArrayList AuthenticatedAttributes
			{
				get
				{
					return this.authenticatedAttributes;
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x06000977 RID: 2423 RVA: 0x00024A74 File Offset: 0x00022C74
			// (set) Token: 0x06000978 RID: 2424 RVA: 0x00024A7C File Offset: 0x00022C7C
			public X509Certificate Certificate
			{
				get
				{
					return this.x509;
				}
				set
				{
					this.x509 = value;
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000979 RID: 2425 RVA: 0x00024A88 File Offset: 0x00022C88
			// (set) Token: 0x0600097A RID: 2426 RVA: 0x00024A90 File Offset: 0x00022C90
			public string HashName
			{
				get
				{
					return this.hashAlgorithm;
				}
				set
				{
					this.hashAlgorithm = value;
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x0600097B RID: 2427 RVA: 0x00024A9C File Offset: 0x00022C9C
			// (set) Token: 0x0600097C RID: 2428 RVA: 0x00024AA4 File Offset: 0x00022CA4
			public AsymmetricAlgorithm Key
			{
				get
				{
					return this.key;
				}
				set
				{
					this.key = value;
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x0600097D RID: 2429 RVA: 0x00024AB0 File Offset: 0x00022CB0
			// (set) Token: 0x0600097E RID: 2430 RVA: 0x00024AD0 File Offset: 0x00022CD0
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
				set
				{
					if (value != null)
					{
						this.signature = (byte[])value.Clone();
					}
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x0600097F RID: 2431 RVA: 0x00024AEC File Offset: 0x00022CEC
			public ArrayList UnauthenticatedAttributes
			{
				get
				{
					return this.unauthenticatedAttributes;
				}
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x06000980 RID: 2432 RVA: 0x00024AF4 File Offset: 0x00022CF4
			// (set) Token: 0x06000981 RID: 2433 RVA: 0x00024AFC File Offset: 0x00022CFC
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

			// Token: 0x06000982 RID: 2434 RVA: 0x00024B08 File Offset: 0x00022D08
			internal ASN1 GetASN1()
			{
				if (this.key == null || this.hashAlgorithm == null)
				{
					return null;
				}
				byte[] data = new byte[]
				{
					this.version
				};
				ASN1 asn = new ASN1(48);
				asn.Add(new ASN1(2, data));
				asn.Add(PKCS7.IssuerAndSerialNumber(this.x509));
				string oid = CryptoConfig.MapNameToOID(this.hashAlgorithm);
				asn.Add(PKCS7.AlgorithmIdentifier(oid));
				ASN1 asn2 = null;
				if (this.authenticatedAttributes.Count > 0)
				{
					asn2 = asn.Add(new ASN1(160));
					foreach (object obj in this.authenticatedAttributes)
					{
						ASN1 asn3 = (ASN1)obj;
						asn2.Add(asn3);
					}
				}
				if (this.key is RSA)
				{
					asn.Add(PKCS7.AlgorithmIdentifier("1.2.840.113549.1.1.1"));
					if (asn2 != null)
					{
						RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(this.key);
						rsapkcs1SignatureFormatter.SetHashAlgorithm(this.hashAlgorithm);
						byte[] bytes = asn2.GetBytes();
						bytes[0] = 49;
						HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.hashAlgorithm);
						byte[] rgbHash = hashAlgorithm.ComputeHash(bytes);
						this.signature = rsapkcs1SignatureFormatter.CreateSignature(rgbHash);
					}
					asn.Add(new ASN1(4, this.signature));
					if (this.unauthenticatedAttributes.Count > 0)
					{
						ASN1 asn4 = asn.Add(new ASN1(161));
						foreach (object obj2 in this.unauthenticatedAttributes)
						{
							ASN1 asn5 = (ASN1)obj2;
							asn4.Add(asn5);
						}
					}
					return asn;
				}
				if (this.key is DSA)
				{
					throw new NotImplementedException("not yet");
				}
				throw new CryptographicException("Unknown assymetric algorithm");
			}

			// Token: 0x06000983 RID: 2435 RVA: 0x00024D50 File Offset: 0x00022F50
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x040001EF RID: 495
			private byte version;

			// Token: 0x040001F0 RID: 496
			private X509Certificate x509;

			// Token: 0x040001F1 RID: 497
			private string hashAlgorithm;

			// Token: 0x040001F2 RID: 498
			private AsymmetricAlgorithm key;

			// Token: 0x040001F3 RID: 499
			private ArrayList authenticatedAttributes;

			// Token: 0x040001F4 RID: 500
			private ArrayList unauthenticatedAttributes;

			// Token: 0x040001F5 RID: 501
			private byte[] signature;

			// Token: 0x040001F6 RID: 502
			private string issuer;

			// Token: 0x040001F7 RID: 503
			private byte[] serial;

			// Token: 0x040001F8 RID: 504
			private byte[] ski;
		}
	}
}
