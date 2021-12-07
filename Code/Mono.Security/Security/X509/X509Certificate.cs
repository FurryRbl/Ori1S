using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x02000043 RID: 67
	public class X509Certificate : ISerializable
	{
		// Token: 0x060002DC RID: 732 RVA: 0x000153CC File Offset: 0x000135CC
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

		// Token: 0x060002DD RID: 733 RVA: 0x00015440 File Offset: 0x00013640
		protected X509Certificate(SerializationInfo info, StreamingContext context)
		{
			this.Parse((byte[])info.GetValue("raw", typeof(byte[])));
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00015488 File Offset: 0x00013688
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

		// Token: 0x060002E0 RID: 736 RVA: 0x00015818 File Offset: 0x00013A18
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00015848 File Offset: 0x00013A48
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0001598C File Offset: 0x00013B8C
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x000159A4 File Offset: 0x00013BA4
		public X509ExtensionCollection Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000159AC File Offset: 0x00013BAC
		public byte[] Hash
		{
			get
			{
				if (this.certhash == null)
				{
					string signaturealgo = this.m_signaturealgo;
					if (signaturealgo != null)
					{
						if (X509Certificate.<>f__switch$mapF == null)
						{
							X509Certificate.<>f__switch$mapF = new Dictionary<string, int>(6)
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
								},
								{
									"1.2.840.113549.1.1.11",
									3
								}
							};
						}
						int num;
						if (X509Certificate.<>f__switch$mapF.TryGetValue(signaturealgo, out num))
						{
							HashAlgorithm hashAlgorithm;
							switch (num)
							{
							case 0:
								hashAlgorithm = MD2.Create();
								break;
							case 1:
								hashAlgorithm = MD5.Create();
								break;
							case 2:
								hashAlgorithm = SHA1.Create();
								break;
							case 3:
								hashAlgorithm = SHA256.Create();
								break;
							default:
								goto IL_D3;
							}
							if (this.decoder == null || this.decoder.Count < 1)
							{
								return null;
							}
							byte[] bytes = this.decoder[0].GetBytes();
							this.certhash = hashAlgorithm.ComputeHash(bytes, 0, bytes.Length);
							goto IL_116;
						}
					}
					IL_D3:
					return null;
				}
				IL_116:
				return (byte[])this.certhash.Clone();
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00015AE0 File Offset: 0x00013CE0
		public virtual string IssuerName
		{
			get
			{
				return this.m_issuername;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00015AE8 File Offset: 0x00013CE8
		public virtual string KeyAlgorithm
		{
			get
			{
				return this.m_keyalgo;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00015AF0 File Offset: 0x00013CF0
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00015B10 File Offset: 0x00013D10
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

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00015B1C File Offset: 0x00013D1C
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

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00015B3C File Offset: 0x00013D3C
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00015BE8 File Offset: 0x00013DE8
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00015C00 File Offset: 0x00013E00
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

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00015C20 File Offset: 0x00013E20
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00015C40 File Offset: 0x00013E40
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
					if (X509Certificate.<>f__switch$map10 == null)
					{
						X509Certificate.<>f__switch$map10 = new Dictionary<string, int>(6)
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
								"1.2.840.113549.1.1.11",
								0
							},
							{
								"1.2.840.10040.4.3",
								1
							}
						};
					}
					int num;
					if (X509Certificate.<>f__switch$map10.TryGetValue(signaturealgo, out num))
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00015DBC File Offset: 0x00013FBC
		public virtual string SignatureAlgorithm
		{
			get
			{
				return this.m_signaturealgo;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00015DC4 File Offset: 0x00013FC4
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

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00015DF4 File Offset: 0x00013FF4
		public virtual string SubjectName
		{
			get
			{
				return this.m_subject;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00015DFC File Offset: 0x00013FFC
		public virtual DateTime ValidFrom
		{
			get
			{
				return this.m_from;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00015E04 File Offset: 0x00014004
		public virtual DateTime ValidUntil
		{
			get
			{
				return this.m_until;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00015E0C File Offset: 0x0001400C
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00015E14 File Offset: 0x00014014
		public bool IsCurrent
		{
			get
			{
				return this.WasCurrent(DateTime.UtcNow);
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00015E24 File Offset: 0x00014024
		public bool WasCurrent(DateTime instant)
		{
			return instant > this.ValidFrom && instant <= this.ValidUntil;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00015E54 File Offset: 0x00014054
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00015E74 File Offset: 0x00014074
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

		// Token: 0x060002F9 RID: 761 RVA: 0x00015E94 File Offset: 0x00014094
		internal bool VerifySignature(DSA dsa)
		{
			DSASignatureDeformatter dsasignatureDeformatter = new DSASignatureDeformatter(dsa);
			dsasignatureDeformatter.SetHashAlgorithm("SHA1");
			return dsasignatureDeformatter.VerifySignature(this.Hash, this.Signature);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00015EC8 File Offset: 0x000140C8
		internal bool VerifySignature(RSA rsa)
		{
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			string signaturealgo = this.m_signaturealgo;
			if (signaturealgo != null)
			{
				if (X509Certificate.<>f__switch$map11 == null)
				{
					X509Certificate.<>f__switch$map11 = new Dictionary<string, int>(5)
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
							"1.2.840.113549.1.1.11",
							3
						}
					};
				}
				int num;
				if (X509Certificate.<>f__switch$map11.TryGetValue(signaturealgo, out num))
				{
					switch (num)
					{
					case 0:
						rsapkcs1SignatureDeformatter.SetHashAlgorithm("MD2");
						break;
					case 1:
						rsapkcs1SignatureDeformatter.SetHashAlgorithm("MD5");
						break;
					case 2:
						rsapkcs1SignatureDeformatter.SetHashAlgorithm("SHA1");
						break;
					case 3:
						rsapkcs1SignatureDeformatter.SetHashAlgorithm("SHA256");
						break;
					default:
						goto IL_D4;
					}
					return rsapkcs1SignatureDeformatter.VerifySignature(this.Hash, this.Signature);
				}
			}
			IL_D4:
			throw new CryptographicException("Unsupported hash algorithm: " + this.m_signaturealgo);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00015FD4 File Offset: 0x000141D4
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

		// Token: 0x060002FC RID: 764 RVA: 0x00016038 File Offset: 0x00014238
		public bool CheckSignature(byte[] hash, string hashAlgorithm, byte[] signature)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)this.RSA;
			return rsacryptoServiceProvider.VerifyHash(hash, hashAlgorithm, signature);
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0001605C File Offset: 0x0001425C
		public bool IsSelfSigned
		{
			get
			{
				return this.m_issuername == this.m_subject && this.VerifySignature(this.RSA);
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00016090 File Offset: 0x00014290
		public ASN1 GetIssuerName()
		{
			return this.issuer;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00016098 File Offset: 0x00014298
		public ASN1 GetSubjectName()
		{
			return this.subject;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000160A0 File Offset: 0x000142A0
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("raw", this.m_encodedcert);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000160B4 File Offset: 0x000142B4
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

		// Token: 0x0400015E RID: 350
		private ASN1 decoder;

		// Token: 0x0400015F RID: 351
		private byte[] m_encodedcert;

		// Token: 0x04000160 RID: 352
		private DateTime m_from;

		// Token: 0x04000161 RID: 353
		private DateTime m_until;

		// Token: 0x04000162 RID: 354
		private ASN1 issuer;

		// Token: 0x04000163 RID: 355
		private string m_issuername;

		// Token: 0x04000164 RID: 356
		private string m_keyalgo;

		// Token: 0x04000165 RID: 357
		private byte[] m_keyalgoparams;

		// Token: 0x04000166 RID: 358
		private ASN1 subject;

		// Token: 0x04000167 RID: 359
		private string m_subject;

		// Token: 0x04000168 RID: 360
		private byte[] m_publickey;

		// Token: 0x04000169 RID: 361
		private byte[] signature;

		// Token: 0x0400016A RID: 362
		private string m_signaturealgo;

		// Token: 0x0400016B RID: 363
		private byte[] m_signaturealgoparams;

		// Token: 0x0400016C RID: 364
		private byte[] certhash;

		// Token: 0x0400016D RID: 365
		private RSA _rsa;

		// Token: 0x0400016E RID: 366
		private DSA _dsa;

		// Token: 0x0400016F RID: 367
		private int version;

		// Token: 0x04000170 RID: 368
		private byte[] serialnumber;

		// Token: 0x04000171 RID: 369
		private byte[] issuerUniqueID;

		// Token: 0x04000172 RID: 370
		private byte[] subjectUniqueID;

		// Token: 0x04000173 RID: 371
		private X509ExtensionCollection extensions;

		// Token: 0x04000174 RID: 372
		private static string encoding_error = Locale.GetText("Input data cannot be coded as a valid certificate.");
	}
}
