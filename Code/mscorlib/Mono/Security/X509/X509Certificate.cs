using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;

namespace Mono.Security.X509
{
	// Token: 0x020000C8 RID: 200
	internal class X509Certificate : ISerializable
	{
		// Token: 0x06000B29 RID: 2857 RVA: 0x000339A4 File Offset: 0x00031BA4
		public X509Certificate(byte[] data)
		{
			if (data != null)
			{
				if (data.Length > 0 && data[0] != 48)
				{
					try
					{
						data = X509Certificate.PEM("CERTIFICATE", data);
					}
					catch (Exception inner)
					{
						throw new CryptographicException(X509Certificate.encoding_error, inner);
					}
				}
				this.Parse(data);
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00033A18 File Offset: 0x00031C18
		protected X509Certificate(SerializationInfo info, StreamingContext context)
		{
			this.Parse((byte[])info.GetValue("raw", typeof(byte[])));
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00033A60 File Offset: 0x00031C60
		private void Parse(byte[] data)
		{
			try
			{
				this.decoder = new ASN1(data);
				if (this.decoder.Tag != 48)
				{
					throw new CryptographicException(X509Certificate.encoding_error);
				}
				if (this.decoder[0].Tag != 48)
				{
					throw new CryptographicException(X509Certificate.encoding_error);
				}
				ASN1 asn = this.decoder[0];
				int num = 0;
				ASN1 asn2 = this.decoder[0][num];
				this.version = 1;
				if (asn2.Tag == 160 && asn2.Count > 0)
				{
					this.version += (int)asn2[0].Value[0];
					num++;
				}
				ASN1 asn3 = this.decoder[0][num++];
				if (asn3.Tag != 2)
				{
					throw new CryptographicException(X509Certificate.encoding_error);
				}
				this.serialnumber = asn3.Value;
				Array.Reverse(this.serialnumber, 0, this.serialnumber.Length);
				num++;
				this.issuer = asn.Element(num++, 48);
				this.m_issuername = X501.ToString(this.issuer);
				ASN1 asn4 = asn.Element(num++, 48);
				ASN1 time = asn4[0];
				this.m_from = ASN1Convert.ToDateTime(time);
				ASN1 time2 = asn4[1];
				this.m_until = ASN1Convert.ToDateTime(time2);
				this.subject = asn.Element(num++, 48);
				this.m_subject = X501.ToString(this.subject);
				ASN1 asn5 = asn.Element(num++, 48);
				ASN1 asn6 = asn5.Element(0, 48);
				ASN1 asn7 = asn6.Element(0, 6);
				this.m_keyalgo = ASN1Convert.ToOid(asn7);
				ASN1 asn8 = asn6[1];
				this.m_keyalgoparams = ((asn6.Count <= 1) ? null : asn8.GetBytes());
				ASN1 asn9 = asn5.Element(1, 3);
				int num2 = asn9.Length - 1;
				this.m_publickey = new byte[num2];
				Buffer.BlockCopy(asn9.Value, 1, this.m_publickey, 0, num2);
				byte[] value = this.decoder[2].Value;
				this.signature = new byte[value.Length - 1];
				Buffer.BlockCopy(value, 1, this.signature, 0, this.signature.Length);
				asn6 = this.decoder[1];
				asn7 = asn6.Element(0, 6);
				this.m_signaturealgo = ASN1Convert.ToOid(asn7);
				asn8 = asn6[1];
				if (asn8 != null)
				{
					this.m_signaturealgoparams = asn8.GetBytes();
				}
				else
				{
					this.m_signaturealgoparams = null;
				}
				ASN1 asn10 = asn.Element(num, 129);
				if (asn10 != null)
				{
					num++;
					this.issuerUniqueID = asn10.Value;
				}
				ASN1 asn11 = asn.Element(num, 130);
				if (asn11 != null)
				{
					num++;
					this.subjectUniqueID = asn11.Value;
				}
				ASN1 asn12 = asn.Element(num, 163);
				if (asn12 != null && asn12.Count == 1)
				{
					this.extensions = new X509ExtensionCollection(asn12[0]);
				}
				else
				{
					this.extensions = new X509ExtensionCollection(null);
				}
				this.m_encodedcert = (byte[])data.Clone();
			}
			catch (Exception inner)
			{
				throw new CryptographicException(X509Certificate.encoding_error, inner);
			}
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00033DF0 File Offset: 0x00031FF0
		private byte[] GetUnsignedBigInteger(byte[] integer)
		{
			if (integer[0] == 0)
			{
				int num = integer.Length - 1;
				byte[] array = new byte[num];
				Buffer.BlockCopy(integer, 1, array, 0, num);
				return array;
			}
			return integer;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00033E20 File Offset: 0x00032020
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x00033F64 File Offset: 0x00032164
		public DSA DSA
		{
			get
			{
				if (this.m_keyalgoparams == null)
				{
					throw new CryptographicException("Missing key algorithm parameters.");
				}
				if (this._dsa == null)
				{
					DSAParameters parameters = default(DSAParameters);
					ASN1 asn = new ASN1(this.m_publickey);
					if (asn == null || asn.Tag != 2)
					{
						return null;
					}
					parameters.Y = this.GetUnsignedBigInteger(asn.Value);
					ASN1 asn2 = new ASN1(this.m_keyalgoparams);
					if (asn2 == null || asn2.Tag != 48 || asn2.Count < 3)
					{
						return null;
					}
					if (asn2[0].Tag != 2 || asn2[1].Tag != 2 || asn2[2].Tag != 2)
					{
						return null;
					}
					parameters.P = this.GetUnsignedBigInteger(asn2[0].Value);
					parameters.Q = this.GetUnsignedBigInteger(asn2[1].Value);
					parameters.G = this.GetUnsignedBigInteger(asn2[2].Value);
					this._dsa = new DSACryptoServiceProvider(parameters.Y.Length << 3);
					this._dsa.ImportParameters(parameters);
				}
				return this._dsa;
			}
			set
			{
				this._dsa = value;
				if (value != null)
				{
					this._rsa = null;
				}
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00033F7C File Offset: 0x0003217C
		public X509ExtensionCollection Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00033F84 File Offset: 0x00032184
		public byte[] Hash
		{
			get
			{
				if (this.certhash == null)
				{
					string signaturealgo = this.m_signaturealgo;
					if (signaturealgo != null)
					{
						if (X509Certificate.<>f__switch$map13 == null)
						{
							X509Certificate.<>f__switch$map13 = new Dictionary<string, int>(5)
							{
								{
									"1.2.840.113549.1.1.2",
									0
								},
								{
									"1.2.840.113549.1.1.4",
									1
								},
								{
									"1.2.840.113549.1.1.5",
									2
								},
								{
									"1.3.14.3.2.29",
									2
								},
								{
									"1.2.840.10040.4.3",
									2
								}
							};
						}
						int num;
						if (X509Certificate.<>f__switch$map13.TryGetValue(signaturealgo, out num))
						{
							HashAlgorithm hashAlgorithm;
							switch (num)
							{
							case 0:
								hashAlgorithm = HashAlgorithm.Create("MD2");
								break;
							case 1:
								hashAlgorithm = MD5.Create();
								break;
							case 2:
								hashAlgorithm = SHA1.Create();
								break;
							default:
								goto IL_BD;
							}
							if (this.decoder == null || this.decoder.Count < 1)
							{
								return null;
							}
							byte[] bytes = this.decoder[0].GetBytes();
							this.certhash = hashAlgorithm.ComputeHash(bytes, 0, bytes.Length);
							goto IL_100;
						}
					}
					IL_BD:
					return null;
				}
				IL_100:
				return (byte[])this.certhash.Clone();
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x000340A4 File Offset: 0x000322A4
		public virtual string IssuerName
		{
			get
			{
				return this.m_issuername;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x000340AC File Offset: 0x000322AC
		public virtual string KeyAlgorithm
		{
			get
			{
				return this.m_keyalgo;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x000340B4 File Offset: 0x000322B4
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x000340D4 File Offset: 0x000322D4
		public virtual byte[] KeyAlgorithmParameters
		{
			get
			{
				if (this.m_keyalgoparams == null)
				{
					return null;
				}
				return (byte[])this.m_keyalgoparams.Clone();
			}
			set
			{
				this.m_keyalgoparams = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x000340E0 File Offset: 0x000322E0
		public virtual byte[] PublicKey
		{
			get
			{
				if (this.m_publickey == null)
				{
					return null;
				}
				return (byte[])this.m_publickey.Clone();
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00034100 File Offset: 0x00032300
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x000341AC File Offset: 0x000323AC
		public virtual RSA RSA
		{
			get
			{
				if (this._rsa == null)
				{
					RSAParameters parameters = default(RSAParameters);
					ASN1 asn = new ASN1(this.m_publickey);
					ASN1 asn2 = asn[0];
					if (asn2 == null || asn2.Tag != 2)
					{
						return null;
					}
					ASN1 asn3 = asn[1];
					if (asn3.Tag != 2)
					{
						return null;
					}
					parameters.Modulus = this.GetUnsignedBigInteger(asn2.Value);
					parameters.Exponent = asn3.Value;
					int dwKeySize = parameters.Modulus.Length << 3;
					this._rsa = new RSACryptoServiceProvider(dwKeySize);
					this._rsa.ImportParameters(parameters);
				}
				return this._rsa;
			}
			set
			{
				if (value != null)
				{
					this._dsa = null;
				}
				this._rsa = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x000341C4 File Offset: 0x000323C4
		public virtual byte[] RawData
		{
			get
			{
				if (this.m_encodedcert == null)
				{
					return null;
				}
				return (byte[])this.m_encodedcert.Clone();
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x000341E4 File Offset: 0x000323E4
		public virtual byte[] SerialNumber
		{
			get
			{
				if (this.serialnumber == null)
				{
					return null;
				}
				return (byte[])this.serialnumber.Clone();
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00034204 File Offset: 0x00032404
		public virtual byte[] Signature
		{
			get
			{
				if (this.signature == null)
				{
					return null;
				}
				string signaturealgo = this.m_signaturealgo;
				if (signaturealgo != null)
				{
					if (X509Certificate.<>f__switch$map14 == null)
					{
						X509Certificate.<>f__switch$map14 = new Dictionary<string, int>(5)
						{
							{
								"1.2.840.113549.1.1.2",
								0
							},
							{
								"1.2.840.113549.1.1.4",
								0
							},
							{
								"1.2.840.113549.1.1.5",
								0
							},
							{
								"1.3.14.3.2.29",
								0
							},
							{
								"1.2.840.10040.4.3",
								1
							}
						};
					}
					int num;
					if (X509Certificate.<>f__switch$map14.TryGetValue(signaturealgo, out num))
					{
						if (num == 0)
						{
							return (byte[])this.signature.Clone();
						}
						if (num == 1)
						{
							ASN1 asn = new ASN1(this.signature);
							if (asn == null || asn.Count != 2)
							{
								return null;
							}
							byte[] value = asn[0].Value;
							byte[] value2 = asn[1].Value;
							byte[] array = new byte[40];
							int num2 = Math.Max(0, value.Length - 20);
							int dstOffset = Math.Max(0, 20 - value.Length);
							Buffer.BlockCopy(value, num2, array, dstOffset, value.Length - num2);
							int num3 = Math.Max(0, value2.Length - 20);
							int dstOffset2 = Math.Max(20, 40 - value2.Length);
							Buffer.BlockCopy(value2, num3, array, dstOffset2, value2.Length - num3);
							return array;
						}
					}
				}
				throw new CryptographicException("Unsupported hash algorithm: " + this.m_signaturealgo);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00034374 File Offset: 0x00032574
		public virtual string SignatureAlgorithm
		{
			get
			{
				return this.m_signaturealgo;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0003437C File Offset: 0x0003257C
		public virtual byte[] SignatureAlgorithmParameters
		{
			get
			{
				if (this.m_signaturealgoparams == null)
				{
					return this.m_signaturealgoparams;
				}
				return (byte[])this.m_signaturealgoparams.Clone();
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x000343AC File Offset: 0x000325AC
		public virtual string SubjectName
		{
			get
			{
				return this.m_subject;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x000343B4 File Offset: 0x000325B4
		public virtual DateTime ValidFrom
		{
			get
			{
				return this.m_from;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000343BC File Offset: 0x000325BC
		public virtual DateTime ValidUntil
		{
			get
			{
				return this.m_until;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x000343C4 File Offset: 0x000325C4
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x000343CC File Offset: 0x000325CC
		public bool IsCurrent
		{
			get
			{
				return this.WasCurrent(DateTime.UtcNow);
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000343DC File Offset: 0x000325DC
		public bool WasCurrent(DateTime instant)
		{
			return instant > this.ValidFrom && instant <= this.ValidUntil;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0003440C File Offset: 0x0003260C
		public byte[] IssuerUniqueIdentifier
		{
			get
			{
				if (this.issuerUniqueID == null)
				{
					return null;
				}
				return (byte[])this.issuerUniqueID.Clone();
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0003442C File Offset: 0x0003262C
		public byte[] SubjectUniqueIdentifier
		{
			get
			{
				if (this.subjectUniqueID == null)
				{
					return null;
				}
				return (byte[])this.subjectUniqueID.Clone();
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0003444C File Offset: 0x0003264C
		internal bool VerifySignature(DSA dsa)
		{
			DSASignatureDeformatter dsasignatureDeformatter = new DSASignatureDeformatter(dsa);
			dsasignatureDeformatter.SetHashAlgorithm("SHA1");
			return dsasignatureDeformatter.VerifySignature(this.Hash, this.Signature);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00034480 File Offset: 0x00032680
		internal string GetHashNameFromOID(string oid)
		{
			switch (oid)
			{
			case "1.2.840.113549.1.1.2":
				return "MD2";
			case "1.2.840.113549.1.1.4":
				return "MD5";
			case "1.2.840.113549.1.1.5":
			case "1.3.14.3.2.29":
				return "SHA1";
			}
			return null;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00034518 File Offset: 0x00032718
		internal bool VerifySignature(RSA rsa)
		{
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			string hashNameFromOID = this.GetHashNameFromOID(this.m_signaturealgo);
			if (hashNameFromOID == null)
			{
				throw new CryptographicException("Unsupported hash algorithm: " + this.m_signaturealgo);
			}
			rsapkcs1SignatureDeformatter.SetHashAlgorithm(hashNameFromOID);
			return rsapkcs1SignatureDeformatter.VerifySignature(this.Hash, this.Signature);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00034570 File Offset: 0x00032770
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

		// Token: 0x06000B4A RID: 2890 RVA: 0x000345D4 File Offset: 0x000327D4
		public bool CheckSignature(byte[] hash, string hashAlgorithm, byte[] signature)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)this.RSA;
			return rsacryptoServiceProvider.VerifyHash(hash, hashAlgorithm, signature);
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x000345F8 File Offset: 0x000327F8
		public bool IsSelfSigned
		{
			get
			{
				return this.m_issuername == this.m_subject && this.VerifySignature(this.RSA);
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0003462C File Offset: 0x0003282C
		public ASN1 GetIssuerName()
		{
			return this.issuer;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00034634 File Offset: 0x00032834
		public ASN1 GetSubjectName()
		{
			return this.subject;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0003463C File Offset: 0x0003283C
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("raw", this.m_encodedcert);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00034650 File Offset: 0x00032850
		private static byte[] PEM(string type, byte[] data)
		{
			string @string = Encoding.ASCII.GetString(data);
			string text = string.Format("-----BEGIN {0}-----", type);
			string value = string.Format("-----END {0}-----", type);
			int num = @string.IndexOf(text) + text.Length;
			int num2 = @string.IndexOf(value, num);
			string s = @string.Substring(num, num2 - num);
			return Convert.FromBase64String(s);
		}

		// Token: 0x040002E7 RID: 743
		private ASN1 decoder;

		// Token: 0x040002E8 RID: 744
		private byte[] m_encodedcert;

		// Token: 0x040002E9 RID: 745
		private DateTime m_from;

		// Token: 0x040002EA RID: 746
		private DateTime m_until;

		// Token: 0x040002EB RID: 747
		private ASN1 issuer;

		// Token: 0x040002EC RID: 748
		private string m_issuername;

		// Token: 0x040002ED RID: 749
		private string m_keyalgo;

		// Token: 0x040002EE RID: 750
		private byte[] m_keyalgoparams;

		// Token: 0x040002EF RID: 751
		private ASN1 subject;

		// Token: 0x040002F0 RID: 752
		private string m_subject;

		// Token: 0x040002F1 RID: 753
		private byte[] m_publickey;

		// Token: 0x040002F2 RID: 754
		private byte[] signature;

		// Token: 0x040002F3 RID: 755
		private string m_signaturealgo;

		// Token: 0x040002F4 RID: 756
		private byte[] m_signaturealgoparams;

		// Token: 0x040002F5 RID: 757
		private byte[] certhash;

		// Token: 0x040002F6 RID: 758
		private RSA _rsa;

		// Token: 0x040002F7 RID: 759
		private DSA _dsa;

		// Token: 0x040002F8 RID: 760
		private int version;

		// Token: 0x040002F9 RID: 761
		private byte[] serialnumber;

		// Token: 0x040002FA RID: 762
		private byte[] issuerUniqueID;

		// Token: 0x040002FB RID: 763
		private byte[] subjectUniqueID;

		// Token: 0x040002FC RID: 764
		private X509ExtensionCollection extensions;

		// Token: 0x040002FD RID: 765
		private static string encoding_error = Locale.GetText("Input data cannot be coded as a valid certificate.");
	}
}
