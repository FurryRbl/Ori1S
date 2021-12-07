using System;
using System.Collections;
using System.Security.Cryptography;
using Mono.Security.X509;

namespace Mono.Security
{
	// Token: 0x02000011 RID: 17
	public sealed class PKCS7
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00005C0C File Offset: 0x00003E0C
		private PKCS7()
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005C14 File Offset: 0x00003E14
		public static ASN1 Attribute(string oid, ASN1 value)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			ASN1 asn2 = asn.Add(new ASN1(49));
			asn2.Add(value);
			return asn;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005C50 File Offset: 0x00003E50
		public static ASN1 AlgorithmIdentifier(string oid)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(new ASN1(5));
			return asn;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005C80 File Offset: 0x00003E80
		public static ASN1 AlgorithmIdentifier(string oid, ASN1 parameters)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(parameters);
			return asn;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005CAC File Offset: 0x00003EAC
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

		// Token: 0x02000012 RID: 18
		public class Oid
		{
			// Token: 0x04000034 RID: 52
			public const string rsaEncryption = "1.2.840.113549.1.1.1";

			// Token: 0x04000035 RID: 53
			public const string data = "1.2.840.113549.1.7.1";

			// Token: 0x04000036 RID: 54
			public const string signedData = "1.2.840.113549.1.7.2";

			// Token: 0x04000037 RID: 55
			public const string envelopedData = "1.2.840.113549.1.7.3";

			// Token: 0x04000038 RID: 56
			public const string signedAndEnvelopedData = "1.2.840.113549.1.7.4";

			// Token: 0x04000039 RID: 57
			public const string digestedData = "1.2.840.113549.1.7.5";

			// Token: 0x0400003A RID: 58
			public const string encryptedData = "1.2.840.113549.1.7.6";

			// Token: 0x0400003B RID: 59
			public const string contentType = "1.2.840.113549.1.9.3";

			// Token: 0x0400003C RID: 60
			public const string messageDigest = "1.2.840.113549.1.9.4";

			// Token: 0x0400003D RID: 61
			public const string signingTime = "1.2.840.113549.1.9.5";

			// Token: 0x0400003E RID: 62
			public const string countersignature = "1.2.840.113549.1.9.6";
		}

		// Token: 0x02000013 RID: 19
		public class ContentInfo
		{
			// Token: 0x060000B5 RID: 181 RVA: 0x00005D54 File Offset: 0x00003F54
			public ContentInfo()
			{
				this.content = new ASN1(160);
			}

			// Token: 0x060000B6 RID: 182 RVA: 0x00005D6C File Offset: 0x00003F6C
			public ContentInfo(string oid) : this()
			{
				this.contentType = oid;
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x00005D7C File Offset: 0x00003F7C
			public ContentInfo(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x00005D8C File Offset: 0x00003F8C
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

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005E38 File Offset: 0x00004038
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x060000BA RID: 186 RVA: 0x00005E40 File Offset: 0x00004040
			// (set) Token: 0x060000BB RID: 187 RVA: 0x00005E48 File Offset: 0x00004048
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

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x060000BC RID: 188 RVA: 0x00005E54 File Offset: 0x00004054
			// (set) Token: 0x060000BD RID: 189 RVA: 0x00005E5C File Offset: 0x0000405C
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

			// Token: 0x060000BE RID: 190 RVA: 0x00005E68 File Offset: 0x00004068
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

			// Token: 0x060000BF RID: 191 RVA: 0x00005EBC File Offset: 0x000040BC
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x0400003F RID: 63
			private string contentType;

			// Token: 0x04000040 RID: 64
			private ASN1 content;
		}

		// Token: 0x02000014 RID: 20
		public class EncryptedData
		{
			// Token: 0x060000C0 RID: 192 RVA: 0x00005ECC File Offset: 0x000040CC
			public EncryptedData()
			{
				this._version = 0;
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x00005EDC File Offset: 0x000040DC
			public EncryptedData(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x00005EEC File Offset: 0x000040EC
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

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000601C File Offset: 0x0000421C
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x060000C4 RID: 196 RVA: 0x00006024 File Offset: 0x00004224
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this._content;
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000602C File Offset: 0x0000422C
			public PKCS7.ContentInfo EncryptionAlgorithm
			{
				get
				{
					return this._encryptionAlgorithm;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x060000C6 RID: 198 RVA: 0x00006034 File Offset: 0x00004234
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

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x060000C7 RID: 199 RVA: 0x00006054 File Offset: 0x00004254
			// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000605C File Offset: 0x0000425C
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

			// Token: 0x060000C9 RID: 201 RVA: 0x00006068 File Offset: 0x00004268
			internal ASN1 GetASN1()
			{
				return null;
			}

			// Token: 0x060000CA RID: 202 RVA: 0x0000606C File Offset: 0x0000426C
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x04000041 RID: 65
			private byte _version;

			// Token: 0x04000042 RID: 66
			private PKCS7.ContentInfo _content;

			// Token: 0x04000043 RID: 67
			private PKCS7.ContentInfo _encryptionAlgorithm;

			// Token: 0x04000044 RID: 68
			private byte[] _encrypted;
		}

		// Token: 0x02000015 RID: 21
		public class EnvelopedData
		{
			// Token: 0x060000CB RID: 203 RVA: 0x0000607C File Offset: 0x0000427C
			public EnvelopedData()
			{
				this._version = 0;
				this._content = new PKCS7.ContentInfo();
				this._encryptionAlgorithm = new PKCS7.ContentInfo();
				this._recipientInfos = new ArrayList();
			}

			// Token: 0x060000CC RID: 204 RVA: 0x000060B8 File Offset: 0x000042B8
			public EnvelopedData(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x060000CD RID: 205 RVA: 0x000060C8 File Offset: 0x000042C8
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

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x060000CE RID: 206 RVA: 0x00006278 File Offset: 0x00004478
			public ArrayList RecipientInfos
			{
				get
				{
					return this._recipientInfos;
				}
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x060000CF RID: 207 RVA: 0x00006280 File Offset: 0x00004480
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x060000D0 RID: 208 RVA: 0x00006288 File Offset: 0x00004488
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this._content;
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x060000D1 RID: 209 RVA: 0x00006290 File Offset: 0x00004490
			public PKCS7.ContentInfo EncryptionAlgorithm
			{
				get
				{
					return this._encryptionAlgorithm;
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x060000D2 RID: 210 RVA: 0x00006298 File Offset: 0x00004498
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

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x000062B8 File Offset: 0x000044B8
			// (set) Token: 0x060000D4 RID: 212 RVA: 0x000062C0 File Offset: 0x000044C0
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

			// Token: 0x060000D5 RID: 213 RVA: 0x000062CC File Offset: 0x000044CC
			internal ASN1 GetASN1()
			{
				return new ASN1(48);
			}

			// Token: 0x060000D6 RID: 214 RVA: 0x000062E4 File Offset: 0x000044E4
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x04000045 RID: 69
			private byte _version;

			// Token: 0x04000046 RID: 70
			private PKCS7.ContentInfo _content;

			// Token: 0x04000047 RID: 71
			private PKCS7.ContentInfo _encryptionAlgorithm;

			// Token: 0x04000048 RID: 72
			private ArrayList _recipientInfos;

			// Token: 0x04000049 RID: 73
			private byte[] _encrypted;
		}

		// Token: 0x02000016 RID: 22
		public class RecipientInfo
		{
			// Token: 0x060000D7 RID: 215 RVA: 0x000062F4 File Offset: 0x000044F4
			public RecipientInfo()
			{
			}

			// Token: 0x060000D8 RID: 216 RVA: 0x000062FC File Offset: 0x000044FC
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

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x060000D9 RID: 217 RVA: 0x000063DC File Offset: 0x000045DC
			public string Oid
			{
				get
				{
					return this._oid;
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x060000DA RID: 218 RVA: 0x000063E4 File Offset: 0x000045E4
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

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000DB RID: 219 RVA: 0x00006404 File Offset: 0x00004604
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

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000DC RID: 220 RVA: 0x00006424 File Offset: 0x00004624
			public string Issuer
			{
				get
				{
					return this._issuer;
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x060000DD RID: 221 RVA: 0x0000642C File Offset: 0x0000462C
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

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000DE RID: 222 RVA: 0x0000644C File Offset: 0x0000464C
			public int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x0400004A RID: 74
			private int _version;

			// Token: 0x0400004B RID: 75
			private string _oid;

			// Token: 0x0400004C RID: 76
			private byte[] _key;

			// Token: 0x0400004D RID: 77
			private byte[] _ski;

			// Token: 0x0400004E RID: 78
			private string _issuer;

			// Token: 0x0400004F RID: 79
			private byte[] _serial;
		}

		// Token: 0x02000017 RID: 23
		public class SignedData
		{
			// Token: 0x060000DF RID: 223 RVA: 0x00006454 File Offset: 0x00004654
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

			// Token: 0x060000E0 RID: 224 RVA: 0x000064A8 File Offset: 0x000046A8
			public SignedData(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x060000E1 RID: 225 RVA: 0x000064B8 File Offset: 0x000046B8
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

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000E2 RID: 226 RVA: 0x000066BC File Offset: 0x000048BC
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000E3 RID: 227 RVA: 0x000066C4 File Offset: 0x000048C4
			public X509CertificateCollection Certificates
			{
				get
				{
					return this.certs;
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000E4 RID: 228 RVA: 0x000066CC File Offset: 0x000048CC
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this.contentInfo;
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000E5 RID: 229 RVA: 0x000066D4 File Offset: 0x000048D4
			public ArrayList Crls
			{
				get
				{
					return this.crls;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000E6 RID: 230 RVA: 0x000066DC File Offset: 0x000048DC
			// (set) Token: 0x060000E7 RID: 231 RVA: 0x000066E4 File Offset: 0x000048E4
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

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000E8 RID: 232 RVA: 0x000066FC File Offset: 0x000048FC
			public PKCS7.SignerInfo SignerInfo
			{
				get
				{
					return this.signerInfo;
				}
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000E9 RID: 233 RVA: 0x00006704 File Offset: 0x00004904
			// (set) Token: 0x060000EA RID: 234 RVA: 0x0000670C File Offset: 0x0000490C
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

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000EB RID: 235 RVA: 0x00006718 File Offset: 0x00004918
			// (set) Token: 0x060000EC RID: 236 RVA: 0x00006720 File Offset: 0x00004920
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

			// Token: 0x060000ED RID: 237 RVA: 0x0000672C File Offset: 0x0000492C
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

			// Token: 0x060000EE RID: 238 RVA: 0x0000683C File Offset: 0x00004A3C
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

			// Token: 0x060000EF RID: 239 RVA: 0x00006910 File Offset: 0x00004B10
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

			// Token: 0x060000F0 RID: 240 RVA: 0x00006BF4 File Offset: 0x00004DF4
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x04000050 RID: 80
			private byte version;

			// Token: 0x04000051 RID: 81
			private string hashAlgorithm;

			// Token: 0x04000052 RID: 82
			private PKCS7.ContentInfo contentInfo;

			// Token: 0x04000053 RID: 83
			private X509CertificateCollection certs;

			// Token: 0x04000054 RID: 84
			private ArrayList crls;

			// Token: 0x04000055 RID: 85
			private PKCS7.SignerInfo signerInfo;

			// Token: 0x04000056 RID: 86
			private bool mda;

			// Token: 0x04000057 RID: 87
			private bool signed;
		}

		// Token: 0x02000018 RID: 24
		public class SignerInfo
		{
			// Token: 0x060000F1 RID: 241 RVA: 0x00006C04 File Offset: 0x00004E04
			public SignerInfo()
			{
				this.version = 1;
				this.authenticatedAttributes = new ArrayList();
				this.unauthenticatedAttributes = new ArrayList();
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x00006C2C File Offset: 0x00004E2C
			public SignerInfo(byte[] data) : this(new ASN1(data))
			{
			}

			// Token: 0x060000F3 RID: 243 RVA: 0x00006C3C File Offset: 0x00004E3C
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

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060000F4 RID: 244 RVA: 0x00006E18 File Offset: 0x00005018
			public string IssuerName
			{
				get
				{
					return this.issuer;
				}
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006E20 File Offset: 0x00005020
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

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x00006E40 File Offset: 0x00005040
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

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006E60 File Offset: 0x00005060
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x00006E68 File Offset: 0x00005068
			public ArrayList AuthenticatedAttributes
			{
				get
				{
					return this.authenticatedAttributes;
				}
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006E70 File Offset: 0x00005070
			// (set) Token: 0x060000FA RID: 250 RVA: 0x00006E78 File Offset: 0x00005078
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

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000FB RID: 251 RVA: 0x00006E84 File Offset: 0x00005084
			// (set) Token: 0x060000FC RID: 252 RVA: 0x00006E8C File Offset: 0x0000508C
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

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000FD RID: 253 RVA: 0x00006E98 File Offset: 0x00005098
			// (set) Token: 0x060000FE RID: 254 RVA: 0x00006EA0 File Offset: 0x000050A0
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

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000FF RID: 255 RVA: 0x00006EAC File Offset: 0x000050AC
			// (set) Token: 0x06000100 RID: 256 RVA: 0x00006ECC File Offset: 0x000050CC
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

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000101 RID: 257 RVA: 0x00006EE8 File Offset: 0x000050E8
			public ArrayList UnauthenticatedAttributes
			{
				get
				{
					return this.unauthenticatedAttributes;
				}
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x06000102 RID: 258 RVA: 0x00006EF0 File Offset: 0x000050F0
			// (set) Token: 0x06000103 RID: 259 RVA: 0x00006EF8 File Offset: 0x000050F8
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

			// Token: 0x06000104 RID: 260 RVA: 0x00006F04 File Offset: 0x00005104
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
					this.authenticatedAttributes.Sort(new PKCS7.SortedSet());
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
						this.unauthenticatedAttributes.Sort(new PKCS7.SortedSet());
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

			// Token: 0x06000105 RID: 261 RVA: 0x0000716C File Offset: 0x0000536C
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x04000059 RID: 89
			private byte version;

			// Token: 0x0400005A RID: 90
			private X509Certificate x509;

			// Token: 0x0400005B RID: 91
			private string hashAlgorithm;

			// Token: 0x0400005C RID: 92
			private AsymmetricAlgorithm key;

			// Token: 0x0400005D RID: 93
			private ArrayList authenticatedAttributes;

			// Token: 0x0400005E RID: 94
			private ArrayList unauthenticatedAttributes;

			// Token: 0x0400005F RID: 95
			private byte[] signature;

			// Token: 0x04000060 RID: 96
			private string issuer;

			// Token: 0x04000061 RID: 97
			private byte[] serial;

			// Token: 0x04000062 RID: 98
			private byte[] ski;
		}

		// Token: 0x02000019 RID: 25
		internal class SortedSet : IComparer
		{
			// Token: 0x06000107 RID: 263 RVA: 0x00007184 File Offset: 0x00005384
			public int Compare(object x, object y)
			{
				if (x == null)
				{
					return (y != null) ? -1 : 0;
				}
				if (y == null)
				{
					return 1;
				}
				ASN1 asn = x as ASN1;
				ASN1 asn2 = y as ASN1;
				if (asn == null || asn2 == null)
				{
					throw new ArgumentException(Locale.GetText("Invalid objects."));
				}
				byte[] bytes = asn.GetBytes();
				byte[] bytes2 = asn2.GetBytes();
				for (int i = 0; i < bytes.Length; i++)
				{
					if (i == bytes2.Length)
					{
						break;
					}
					if (bytes[i] != bytes2[i])
					{
						return (bytes[i] >= bytes2[i]) ? 1 : -1;
					}
				}
				if (bytes.Length > bytes2.Length)
				{
					return 1;
				}
				if (bytes.Length < bytes2.Length)
				{
					return -1;
				}
				return 0;
			}
		}
	}
}
