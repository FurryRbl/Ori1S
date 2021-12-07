using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x020000C4 RID: 196
	internal class PKCS12 : ICloneable
	{
		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002EE64 File Offset: 0x0002D064
		public PKCS12()
		{
			this._iterations = PKCS12.recommendedIterationCount;
			this._keyBags = new ArrayList();
			this._secretBags = new ArrayList();
			this._certs = new X509CertificateCollection();
			this._keyBagsChanged = false;
			this._secretBagsChanged = false;
			this._certsChanged = false;
			this._safeBags = new ArrayList();
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002EEC4 File Offset: 0x0002D0C4
		public PKCS12(byte[] data) : this()
		{
			this.Password = null;
			this.Decode(data);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002EEDC File Offset: 0x0002D0DC
		public PKCS12(byte[] data, string password) : this()
		{
			this.Password = password;
			this.Decode(data);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002EEF4 File Offset: 0x0002D0F4
		public PKCS12(byte[] data, byte[] password) : this()
		{
			this._password = password;
			this.Decode(data);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002EF24 File Offset: 0x0002D124
		private void Decode(byte[] data)
		{
			ASN1 asn = new ASN1(data);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("invalid data");
			}
			ASN1 asn2 = asn[0];
			if (asn2.Tag != 2)
			{
				throw new ArgumentException("invalid PFX version");
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(asn[1]);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.1")
			{
				throw new ArgumentException("invalid authenticated safe");
			}
			if (asn.Count > 2)
			{
				ASN1 asn3 = asn[2];
				if (asn3.Tag != 48)
				{
					throw new ArgumentException("invalid MAC");
				}
				ASN1 asn4 = asn3[0];
				if (asn4.Tag != 48)
				{
					throw new ArgumentException("invalid MAC");
				}
				ASN1 asn5 = asn4[0];
				string a = ASN1Convert.ToOid(asn5[0]);
				if (a != "1.3.14.3.2.26")
				{
					throw new ArgumentException("unsupported HMAC");
				}
				byte[] value = asn4[1].Value;
				ASN1 asn6 = asn3[1];
				if (asn6.Tag != 4)
				{
					throw new ArgumentException("missing MAC salt");
				}
				this._iterations = 1;
				if (asn3.Count > 2)
				{
					ASN1 asn7 = asn3[2];
					if (asn7.Tag != 2)
					{
						throw new ArgumentException("invalid MAC iteration");
					}
					this._iterations = ASN1Convert.ToInt32(asn7);
				}
				byte[] value2 = contentInfo.Content[0].Value;
				byte[] actual = this.MAC(this._password, asn6.Value, this._iterations, value2);
				if (!this.Compare(value, actual))
				{
					throw new CryptographicException("Invalid MAC - file may have been tampered!");
				}
			}
			ASN1 asn8 = new ASN1(contentInfo.Content[0].Value);
			int i = 0;
			while (i < asn8.Count)
			{
				PKCS7.ContentInfo contentInfo2 = new PKCS7.ContentInfo(asn8[i]);
				string contentType = contentInfo2.ContentType;
				if (contentType != null)
				{
					if (PKCS12.<>f__switch$mapA == null)
					{
						PKCS12.<>f__switch$mapA = new Dictionary<string, int>(3)
						{
							{
								"1.2.840.113549.1.7.1",
								0
							},
							{
								"1.2.840.113549.1.7.6",
								1
							},
							{
								"1.2.840.113549.1.7.3",
								2
							}
						};
					}
					int num;
					if (PKCS12.<>f__switch$mapA.TryGetValue(contentType, out num))
					{
						switch (num)
						{
						case 0:
						{
							ASN1 asn9 = new ASN1(contentInfo2.Content[0].Value);
							for (int j = 0; j < asn9.Count; j++)
							{
								ASN1 safeBag = asn9[j];
								this.ReadSafeBag(safeBag);
							}
							break;
						}
						case 1:
						{
							PKCS7.EncryptedData ed = new PKCS7.EncryptedData(contentInfo2.Content[0]);
							ASN1 asn10 = new ASN1(this.Decrypt(ed));
							for (int k = 0; k < asn10.Count; k++)
							{
								ASN1 safeBag2 = asn10[k];
								this.ReadSafeBag(safeBag2);
							}
							break;
						}
						case 2:
							throw new NotImplementedException("public key encrypted");
						default:
							goto IL_303;
						}
						i++;
						continue;
					}
				}
				IL_303:
				throw new ArgumentException("unknown authenticatedSafe");
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002F254 File Offset: 0x0002D454
		~PKCS12()
		{
			if (this._password != null)
			{
				Array.Clear(this._password, 0, this._password.Length);
			}
			this._password = null;
		}

		// Token: 0x17000164 RID: 356
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x0002F2B0 File Offset: 0x0002D4B0
		public string Password
		{
			set
			{
				if (value != null)
				{
					if (value.Length > 0)
					{
						int num = value.Length;
						int num2 = 0;
						if (num < PKCS12.MaximumPasswordLength)
						{
							if (value[num - 1] != '\0')
							{
								num2 = 1;
							}
						}
						else
						{
							num = PKCS12.MaximumPasswordLength;
						}
						this._password = new byte[num + num2 << 1];
						Encoding.BigEndianUnicode.GetBytes(value, 0, num, this._password, 0);
					}
					else
					{
						this._password = new byte[2];
					}
				}
				else
				{
					this._password = null;
				}
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0002F340 File Offset: 0x0002D540
		// (set) Token: 0x06000ADE RID: 2782 RVA: 0x0002F348 File Offset: 0x0002D548
		public int IterationCount
		{
			get
			{
				return this._iterations;
			}
			set
			{
				this._iterations = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0002F354 File Offset: 0x0002D554
		public ArrayList Keys
		{
			get
			{
				if (this._keyBagsChanged)
				{
					this._keyBags.Clear();
					foreach (object obj in this._safeBags)
					{
						SafeBag safeBag = (SafeBag)obj;
						if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
						{
							ASN1 asn = safeBag.ASN1;
							ASN1 asn2 = asn[1];
							PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo(asn2.Value);
							byte[] privateKey = privateKeyInfo.PrivateKey;
							byte b = privateKey[0];
							if (b != 2)
							{
								if (b == 48)
								{
									this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeRSA(privateKey));
								}
							}
							else
							{
								DSAParameters dsaParameters = default(DSAParameters);
								this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, dsaParameters));
							}
							Array.Clear(privateKey, 0, privateKey.Length);
						}
						else if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
						{
							ASN1 asn3 = safeBag.ASN1;
							ASN1 asn4 = asn3[1];
							PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(asn4.Value);
							byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
							PKCS8.PrivateKeyInfo privateKeyInfo2 = new PKCS8.PrivateKeyInfo(array);
							byte[] privateKey2 = privateKeyInfo2.PrivateKey;
							byte b = privateKey2[0];
							if (b != 2)
							{
								if (b == 48)
								{
									this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeRSA(privateKey2));
								}
							}
							else
							{
								DSAParameters dsaParameters2 = default(DSAParameters);
								this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeDSA(privateKey2, dsaParameters2));
							}
							Array.Clear(privateKey2, 0, privateKey2.Length);
							Array.Clear(array, 0, array.Length);
						}
					}
					this._keyBagsChanged = false;
				}
				return ArrayList.ReadOnly(this._keyBags);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0002F56C File Offset: 0x0002D76C
		public ArrayList Secrets
		{
			get
			{
				if (this._secretBagsChanged)
				{
					this._secretBags.Clear();
					foreach (object obj in this._safeBags)
					{
						SafeBag safeBag = (SafeBag)obj;
						if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
						{
							ASN1 asn = safeBag.ASN1;
							ASN1 asn2 = asn[1];
							byte[] value = asn2.Value;
							this._secretBags.Add(value);
						}
					}
					this._secretBagsChanged = false;
				}
				return ArrayList.ReadOnly(this._secretBags);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0002F63C File Offset: 0x0002D83C
		public X509CertificateCollection Certificates
		{
			get
			{
				if (this._certsChanged)
				{
					this._certs.Clear();
					foreach (object obj in this._safeBags)
					{
						SafeBag safeBag = (SafeBag)obj;
						if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
						{
							ASN1 asn = safeBag.ASN1;
							ASN1 asn2 = asn[1];
							PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(asn2.Value);
							this._certs.Add(new X509Certificate(contentInfo.Content[0].Value));
						}
					}
					this._certsChanged = false;
				}
				return this._certs;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0002F720 File Offset: 0x0002D920
		internal RandomNumberGenerator RNG
		{
			get
			{
				if (this._rng == null)
				{
					this._rng = RandomNumberGenerator.Create();
				}
				return this._rng;
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002F740 File Offset: 0x0002D940
		private bool Compare(byte[] expected, byte[] actual)
		{
			bool result = false;
			if (expected.Length == actual.Length)
			{
				for (int i = 0; i < expected.Length; i++)
				{
					if (expected[i] != actual[i])
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002F780 File Offset: 0x0002D980
		private SymmetricAlgorithm GetSymmetricAlgorithm(string algorithmOid, byte[] salt, int iterationCount)
		{
			string text = null;
			int size = 8;
			int num = 8;
			PKCS12.DeriveBytes deriveBytes = new PKCS12.DeriveBytes();
			deriveBytes.Password = this._password;
			deriveBytes.Salt = salt;
			deriveBytes.IterationCount = iterationCount;
			if (algorithmOid != null)
			{
				if (PKCS12.<>f__switch$mapB == null)
				{
					PKCS12.<>f__switch$mapB = new Dictionary<string, int>(12)
					{
						{
							"1.2.840.113549.1.5.1",
							0
						},
						{
							"1.2.840.113549.1.5.3",
							1
						},
						{
							"1.2.840.113549.1.5.4",
							2
						},
						{
							"1.2.840.113549.1.5.6",
							3
						},
						{
							"1.2.840.113549.1.5.10",
							4
						},
						{
							"1.2.840.113549.1.5.11",
							5
						},
						{
							"1.2.840.113549.1.12.1.1",
							6
						},
						{
							"1.2.840.113549.1.12.1.2",
							7
						},
						{
							"1.2.840.113549.1.12.1.3",
							8
						},
						{
							"1.2.840.113549.1.12.1.4",
							9
						},
						{
							"1.2.840.113549.1.12.1.5",
							10
						},
						{
							"1.2.840.113549.1.12.1.6",
							11
						}
					};
				}
				int num2;
				if (PKCS12.<>f__switch$mapB.TryGetValue(algorithmOid, out num2))
				{
					switch (num2)
					{
					case 0:
						deriveBytes.HashName = "MD2";
						text = "DES";
						break;
					case 1:
						deriveBytes.HashName = "MD5";
						text = "DES";
						break;
					case 2:
						deriveBytes.HashName = "MD2";
						text = "RC2";
						size = 4;
						break;
					case 3:
						deriveBytes.HashName = "MD5";
						text = "RC2";
						size = 4;
						break;
					case 4:
						deriveBytes.HashName = "SHA1";
						text = "DES";
						break;
					case 5:
						deriveBytes.HashName = "SHA1";
						text = "RC2";
						size = 4;
						break;
					case 6:
						deriveBytes.HashName = "SHA1";
						text = "RC4";
						size = 16;
						num = 0;
						break;
					case 7:
						deriveBytes.HashName = "SHA1";
						text = "RC4";
						size = 5;
						num = 0;
						break;
					case 8:
						deriveBytes.HashName = "SHA1";
						text = "TripleDES";
						size = 24;
						break;
					case 9:
						deriveBytes.HashName = "SHA1";
						text = "TripleDES";
						size = 16;
						break;
					case 10:
						deriveBytes.HashName = "SHA1";
						text = "RC2";
						size = 16;
						break;
					case 11:
						deriveBytes.HashName = "SHA1";
						text = "RC2";
						size = 5;
						break;
					default:
						goto IL_25A;
					}
					SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create(text);
					symmetricAlgorithm.Key = deriveBytes.DeriveKey(size);
					if (num > 0)
					{
						symmetricAlgorithm.IV = deriveBytes.DeriveIV(num);
						symmetricAlgorithm.Mode = CipherMode.CBC;
					}
					return symmetricAlgorithm;
				}
			}
			IL_25A:
			throw new NotSupportedException("unknown oid " + text);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002FA30 File Offset: 0x0002DC30
		public byte[] Decrypt(string algorithmOid, byte[] salt, int iterationCount, byte[] encryptedData)
		{
			SymmetricAlgorithm symmetricAlgorithm = null;
			byte[] result = null;
			try
			{
				symmetricAlgorithm = this.GetSymmetricAlgorithm(algorithmOid, salt, iterationCount);
				ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateDecryptor();
				result = cryptoTransform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
			}
			finally
			{
				if (symmetricAlgorithm != null)
				{
					symmetricAlgorithm.Clear();
				}
			}
			return result;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002FA90 File Offset: 0x0002DC90
		public byte[] Decrypt(PKCS7.EncryptedData ed)
		{
			return this.Decrypt(ed.EncryptionAlgorithm.ContentType, ed.EncryptionAlgorithm.Content[0].Value, ASN1Convert.ToInt32(ed.EncryptionAlgorithm.Content[1]), ed.EncryptedContent);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002FAE0 File Offset: 0x0002DCE0
		public byte[] Encrypt(string algorithmOid, byte[] salt, int iterationCount, byte[] data)
		{
			byte[] result = null;
			using (SymmetricAlgorithm symmetricAlgorithm = this.GetSymmetricAlgorithm(algorithmOid, salt, iterationCount))
			{
				ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateEncryptor();
				result = cryptoTransform.TransformFinalBlock(data, 0, data.Length);
			}
			return result;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002FB40 File Offset: 0x0002DD40
		private DSAParameters GetExistingParameters(out bool found)
		{
			foreach (X509Certificate x509Certificate in this.Certificates)
			{
				if (x509Certificate.KeyAlgorithmParameters != null)
				{
					DSA dsa = x509Certificate.DSA;
					if (dsa != null)
					{
						found = true;
						return dsa.ExportParameters(false);
					}
				}
			}
			found = false;
			return default(DSAParameters);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002FBE0 File Offset: 0x0002DDE0
		private void AddPrivateKey(PKCS8.PrivateKeyInfo pki)
		{
			byte[] privateKey = pki.PrivateKey;
			byte b = privateKey[0];
			if (b != 2)
			{
				if (b != 48)
				{
					Array.Clear(privateKey, 0, privateKey.Length);
					throw new CryptographicException("Unknown private key format");
				}
				this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeRSA(privateKey));
			}
			else
			{
				bool flag;
				DSAParameters existingParameters = this.GetExistingParameters(out flag);
				if (flag)
				{
					this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, existingParameters));
				}
			}
			Array.Clear(privateKey, 0, privateKey.Length);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002FC6C File Offset: 0x0002DE6C
		private void ReadSafeBag(ASN1 safeBag)
		{
			if (safeBag.Tag != 48)
			{
				throw new ArgumentException("invalid safeBag");
			}
			ASN1 asn = safeBag[0];
			if (asn.Tag != 6)
			{
				throw new ArgumentException("invalid safeBag id");
			}
			ASN1 asn2 = safeBag[1];
			string text = ASN1Convert.ToOid(asn);
			string text2 = text;
			if (text2 != null)
			{
				if (PKCS12.<>f__switch$mapC == null)
				{
					PKCS12.<>f__switch$mapC = new Dictionary<string, int>(6)
					{
						{
							"1.2.840.113549.1.12.10.1.1",
							0
						},
						{
							"1.2.840.113549.1.12.10.1.2",
							1
						},
						{
							"1.2.840.113549.1.12.10.1.3",
							2
						},
						{
							"1.2.840.113549.1.12.10.1.4",
							3
						},
						{
							"1.2.840.113549.1.12.10.1.5",
							4
						},
						{
							"1.2.840.113549.1.12.10.1.6",
							5
						}
					};
				}
				int num;
				if (PKCS12.<>f__switch$mapC.TryGetValue(text2, out num))
				{
					switch (num)
					{
					case 0:
						this.AddPrivateKey(new PKCS8.PrivateKeyInfo(asn2.Value));
						break;
					case 1:
					{
						PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(asn2.Value);
						byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
						this.AddPrivateKey(new PKCS8.PrivateKeyInfo(array));
						Array.Clear(array, 0, array.Length);
						break;
					}
					case 2:
					{
						PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(asn2.Value);
						if (contentInfo.ContentType != "1.2.840.113549.1.9.22.1")
						{
							throw new NotSupportedException("unsupport certificate type");
						}
						X509Certificate value = new X509Certificate(contentInfo.Content[0].Value);
						this._certs.Add(value);
						break;
					}
					case 3:
						break;
					case 4:
					{
						byte[] value2 = asn2.Value;
						this._secretBags.Add(value2);
						break;
					}
					case 5:
						break;
					default:
						goto IL_1CD;
					}
					if (safeBag.Count > 2)
					{
						ASN1 asn3 = safeBag[2];
						if (asn3.Tag != 49)
						{
							throw new ArgumentException("invalid safeBag attributes id");
						}
						for (int i = 0; i < asn3.Count; i++)
						{
							ASN1 asn4 = asn3[i];
							if (asn4.Tag != 48)
							{
								throw new ArgumentException("invalid PKCS12 attributes id");
							}
							ASN1 asn5 = asn4[0];
							if (asn5.Tag != 6)
							{
								throw new ArgumentException("invalid attribute id");
							}
							string text3 = ASN1Convert.ToOid(asn5);
							ASN1 asn6 = asn4[1];
							int j = 0;
							while (j < asn6.Count)
							{
								ASN1 asn7 = asn6[j];
								text2 = text3;
								if (text2 != null)
								{
									if (PKCS12.<>f__switch$mapD == null)
									{
										PKCS12.<>f__switch$mapD = new Dictionary<string, int>(2)
										{
											{
												"1.2.840.113549.1.9.20",
												0
											},
											{
												"1.2.840.113549.1.9.21",
												1
											}
										};
									}
									if (PKCS12.<>f__switch$mapD.TryGetValue(text2, out num))
									{
										if (num != 0)
										{
											if (num == 1)
											{
												if (asn7.Tag != 4)
												{
													throw new ArgumentException("invalid attribute value id");
												}
											}
										}
										else if (asn7.Tag != 30)
										{
											throw new ArgumentException("invalid attribute value id");
										}
									}
								}
								IL_31F:
								j++;
								continue;
								goto IL_31F;
							}
						}
					}
					this._safeBags.Add(new SafeBag(text, safeBag));
					return;
				}
			}
			IL_1CD:
			throw new ArgumentException("unknown safeBag oid");
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002FFD4 File Offset: 0x0002E1D4
		private ASN1 Pkcs8ShroudedKeyBagSafeBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo();
			if (aa is RSA)
			{
				privateKeyInfo.Algorithm = "1.2.840.113549.1.1.1";
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((RSA)aa);
			}
			else
			{
				if (!(aa is DSA))
				{
					throw new CryptographicException("Unknown asymmetric algorithm {0}", aa.ToString());
				}
				privateKeyInfo.Algorithm = null;
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((DSA)aa);
			}
			PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo();
			encryptedPrivateKeyInfo.Algorithm = "1.2.840.113549.1.12.1.3";
			encryptedPrivateKeyInfo.IterationCount = this._iterations;
			encryptedPrivateKeyInfo.EncryptedData = this.Encrypt("1.2.840.113549.1.12.1.3", encryptedPrivateKeyInfo.Salt, this._iterations, privateKeyInfo.GetBytes());
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.2"));
			ASN1 asn2 = new ASN1(160);
			asn2.Add(new ASN1(encryptedPrivateKeyInfo.GetBytes()));
			asn.Add(asn2);
			if (attributes != null)
			{
				ASN1 asn3 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					string text2 = text;
					if (text2 != null)
					{
						if (PKCS12.<>f__switch$mapE == null)
						{
							PKCS12.<>f__switch$mapE = new Dictionary<string, int>(2)
							{
								{
									"1.2.840.113549.1.9.20",
									0
								},
								{
									"1.2.840.113549.1.9.21",
									1
								}
							};
						}
						int num;
						if (PKCS12.<>f__switch$mapE.TryGetValue(text2, out num))
						{
							if (num != 0)
							{
								if (num == 1)
								{
									ArrayList arrayList = (ArrayList)enumerator.Value;
									if (arrayList.Count > 0)
									{
										ASN1 asn4 = new ASN1(48);
										asn4.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
										ASN1 asn5 = new ASN1(49);
										foreach (object obj in arrayList)
										{
											byte[] value = (byte[])obj;
											asn5.Add(new ASN1(4)
											{
												Value = value
											});
										}
										asn4.Add(asn5);
										asn3.Add(asn4);
									}
								}
							}
							else
							{
								ArrayList arrayList2 = (ArrayList)enumerator.Value;
								if (arrayList2.Count > 0)
								{
									ASN1 asn6 = new ASN1(48);
									asn6.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
									ASN1 asn7 = new ASN1(49);
									foreach (object obj2 in arrayList2)
									{
										byte[] value2 = (byte[])obj2;
										asn7.Add(new ASN1(30)
										{
											Value = value2
										});
									}
									asn6.Add(asn7);
									asn3.Add(asn6);
								}
							}
						}
					}
				}
				if (asn3.Count > 0)
				{
					asn.Add(asn3);
				}
			}
			return asn;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00030324 File Offset: 0x0002E524
		private ASN1 KeyBagSafeBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo();
			if (aa is RSA)
			{
				privateKeyInfo.Algorithm = "1.2.840.113549.1.1.1";
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((RSA)aa);
			}
			else
			{
				if (!(aa is DSA))
				{
					throw new CryptographicException("Unknown asymmetric algorithm {0}", aa.ToString());
				}
				privateKeyInfo.Algorithm = null;
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((DSA)aa);
			}
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.1"));
			ASN1 asn2 = new ASN1(160);
			asn2.Add(new ASN1(privateKeyInfo.GetBytes()));
			asn.Add(asn2);
			if (attributes != null)
			{
				ASN1 asn3 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					string text2 = text;
					if (text2 != null)
					{
						if (PKCS12.<>f__switch$mapF == null)
						{
							PKCS12.<>f__switch$mapF = new Dictionary<string, int>(2)
							{
								{
									"1.2.840.113549.1.9.20",
									0
								},
								{
									"1.2.840.113549.1.9.21",
									1
								}
							};
						}
						int num;
						if (PKCS12.<>f__switch$mapF.TryGetValue(text2, out num))
						{
							if (num != 0)
							{
								if (num == 1)
								{
									ArrayList arrayList = (ArrayList)enumerator.Value;
									if (arrayList.Count > 0)
									{
										ASN1 asn4 = new ASN1(48);
										asn4.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
										ASN1 asn5 = new ASN1(49);
										foreach (object obj in arrayList)
										{
											byte[] value = (byte[])obj;
											asn5.Add(new ASN1(4)
											{
												Value = value
											});
										}
										asn4.Add(asn5);
										asn3.Add(asn4);
									}
								}
							}
							else
							{
								ArrayList arrayList2 = (ArrayList)enumerator.Value;
								if (arrayList2.Count > 0)
								{
									ASN1 asn6 = new ASN1(48);
									asn6.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
									ASN1 asn7 = new ASN1(49);
									foreach (object obj2 in arrayList2)
									{
										byte[] value2 = (byte[])obj2;
										asn7.Add(new ASN1(30)
										{
											Value = value2
										});
									}
									asn6.Add(asn7);
									asn3.Add(asn6);
								}
							}
						}
					}
				}
				if (asn3.Count > 0)
				{
					asn.Add(asn3);
				}
			}
			return asn;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00030630 File Offset: 0x0002E830
		private ASN1 SecretBagSafeBag(byte[] secret, IDictionary attributes)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.5"));
			ASN1 asn2 = new ASN1(128, secret);
			asn.Add(asn2);
			if (attributes != null)
			{
				ASN1 asn3 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					string text2 = text;
					if (text2 != null)
					{
						if (PKCS12.<>f__switch$map10 == null)
						{
							PKCS12.<>f__switch$map10 = new Dictionary<string, int>(2)
							{
								{
									"1.2.840.113549.1.9.20",
									0
								},
								{
									"1.2.840.113549.1.9.21",
									1
								}
							};
						}
						int num;
						if (PKCS12.<>f__switch$map10.TryGetValue(text2, out num))
						{
							if (num != 0)
							{
								if (num == 1)
								{
									ArrayList arrayList = (ArrayList)enumerator.Value;
									if (arrayList.Count > 0)
									{
										ASN1 asn4 = new ASN1(48);
										asn4.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
										ASN1 asn5 = new ASN1(49);
										foreach (object obj in arrayList)
										{
											byte[] value = (byte[])obj;
											asn5.Add(new ASN1(4)
											{
												Value = value
											});
										}
										asn4.Add(asn5);
										asn3.Add(asn4);
									}
								}
							}
							else
							{
								ArrayList arrayList2 = (ArrayList)enumerator.Value;
								if (arrayList2.Count > 0)
								{
									ASN1 asn6 = new ASN1(48);
									asn6.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
									ASN1 asn7 = new ASN1(49);
									foreach (object obj2 in arrayList2)
									{
										byte[] value2 = (byte[])obj2;
										asn7.Add(new ASN1(30)
										{
											Value = value2
										});
									}
									asn6.Add(asn7);
									asn3.Add(asn6);
								}
							}
						}
					}
				}
				if (asn3.Count > 0)
				{
					asn.Add(asn3);
				}
			}
			return asn;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x000308B8 File Offset: 0x0002EAB8
		private ASN1 CertificateSafeBag(X509Certificate x509, IDictionary attributes)
		{
			ASN1 asn = new ASN1(4, x509.RawData);
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo();
			contentInfo.ContentType = "1.2.840.113549.1.9.22.1";
			contentInfo.Content.Add(asn);
			ASN1 asn2 = new ASN1(160);
			asn2.Add(contentInfo.ASN1);
			ASN1 asn3 = new ASN1(48);
			asn3.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.3"));
			asn3.Add(asn2);
			if (attributes != null)
			{
				ASN1 asn4 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					string text2 = text;
					if (text2 != null)
					{
						if (PKCS12.<>f__switch$map11 == null)
						{
							PKCS12.<>f__switch$map11 = new Dictionary<string, int>(2)
							{
								{
									"1.2.840.113549.1.9.20",
									0
								},
								{
									"1.2.840.113549.1.9.21",
									1
								}
							};
						}
						int num;
						if (PKCS12.<>f__switch$map11.TryGetValue(text2, out num))
						{
							if (num != 0)
							{
								if (num == 1)
								{
									ArrayList arrayList = (ArrayList)enumerator.Value;
									if (arrayList.Count > 0)
									{
										ASN1 asn5 = new ASN1(48);
										asn5.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
										ASN1 asn6 = new ASN1(49);
										foreach (object obj in arrayList)
										{
											byte[] value = (byte[])obj;
											asn6.Add(new ASN1(4)
											{
												Value = value
											});
										}
										asn5.Add(asn6);
										asn4.Add(asn5);
									}
								}
							}
							else
							{
								ArrayList arrayList2 = (ArrayList)enumerator.Value;
								if (arrayList2.Count > 0)
								{
									ASN1 asn7 = new ASN1(48);
									asn7.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
									ASN1 asn8 = new ASN1(49);
									foreach (object obj2 in arrayList2)
									{
										byte[] value2 = (byte[])obj2;
										asn8.Add(new ASN1(30)
										{
											Value = value2
										});
									}
									asn7.Add(asn8);
									asn4.Add(asn7);
								}
							}
						}
					}
				}
				if (asn4.Count > 0)
				{
					asn3.Add(asn4);
				}
			}
			return asn3;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00030B84 File Offset: 0x0002ED84
		private byte[] MAC(byte[] password, byte[] salt, int iterations, byte[] data)
		{
			PKCS12.DeriveBytes deriveBytes = new PKCS12.DeriveBytes();
			deriveBytes.HashName = "SHA1";
			deriveBytes.Password = password;
			deriveBytes.Salt = salt;
			deriveBytes.IterationCount = iterations;
			HMACSHA1 hmacsha = (HMACSHA1)HMAC.Create();
			hmacsha.Key = deriveBytes.DeriveMAC(20);
			return hmacsha.ComputeHash(data, 0, data.Length);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00030BE0 File Offset: 0x0002EDE0
		public byte[] GetBytes()
		{
			ASN1 asn = new ASN1(48);
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this._safeBags)
			{
				SafeBag safeBag = (SafeBag)obj;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					ASN1 asn2 = safeBag.ASN1;
					ASN1 asn3 = asn2[1];
					PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(asn3.Value);
					arrayList.Add(new X509Certificate(contentInfo.Content[0].Value));
				}
			}
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			foreach (X509Certificate x509Certificate in this.Certificates)
			{
				bool flag = false;
				foreach (object obj2 in arrayList)
				{
					X509Certificate x509Certificate2 = (X509Certificate)obj2;
					if (this.Compare(x509Certificate.RawData, x509Certificate2.RawData))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					arrayList2.Add(x509Certificate);
				}
			}
			foreach (object obj3 in arrayList)
			{
				X509Certificate x509Certificate3 = (X509Certificate)obj3;
				bool flag2 = false;
				foreach (X509Certificate x509Certificate4 in this.Certificates)
				{
					if (this.Compare(x509Certificate3.RawData, x509Certificate4.RawData))
					{
						flag2 = true;
					}
				}
				if (!flag2)
				{
					arrayList3.Add(x509Certificate3);
				}
			}
			foreach (object obj4 in arrayList3)
			{
				X509Certificate cert = (X509Certificate)obj4;
				this.RemoveCertificate(cert);
			}
			foreach (object obj5 in arrayList2)
			{
				X509Certificate cert2 = (X509Certificate)obj5;
				this.AddCertificate(cert2);
			}
			if (this._safeBags.Count > 0)
			{
				ASN1 asn4 = new ASN1(48);
				foreach (object obj6 in this._safeBags)
				{
					SafeBag safeBag2 = (SafeBag)obj6;
					if (safeBag2.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
					{
						asn4.Add(safeBag2.ASN1);
					}
				}
				if (asn4.Count > 0)
				{
					PKCS7.ContentInfo contentInfo2 = this.EncryptedContentInfo(asn4, "1.2.840.113549.1.12.1.3");
					asn.Add(contentInfo2.ASN1);
				}
			}
			if (this._safeBags.Count > 0)
			{
				ASN1 asn5 = new ASN1(48);
				foreach (object obj7 in this._safeBags)
				{
					SafeBag safeBag3 = (SafeBag)obj7;
					if (safeBag3.BagOID.Equals("1.2.840.113549.1.12.10.1.1") || safeBag3.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
					{
						asn5.Add(safeBag3.ASN1);
					}
				}
				if (asn5.Count > 0)
				{
					ASN1 asn6 = new ASN1(160);
					asn6.Add(new ASN1(4, asn5.GetBytes()));
					asn.Add(new PKCS7.ContentInfo("1.2.840.113549.1.7.1")
					{
						Content = asn6
					}.ASN1);
				}
			}
			if (this._safeBags.Count > 0)
			{
				ASN1 asn7 = new ASN1(48);
				foreach (object obj8 in this._safeBags)
				{
					SafeBag safeBag4 = (SafeBag)obj8;
					if (safeBag4.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
					{
						asn7.Add(safeBag4.ASN1);
					}
				}
				if (asn7.Count > 0)
				{
					PKCS7.ContentInfo contentInfo3 = this.EncryptedContentInfo(asn7, "1.2.840.113549.1.12.1.3");
					asn.Add(contentInfo3.ASN1);
				}
			}
			ASN1 asn8 = new ASN1(4, asn.GetBytes());
			ASN1 asn9 = new ASN1(160);
			asn9.Add(asn8);
			PKCS7.ContentInfo contentInfo4 = new PKCS7.ContentInfo("1.2.840.113549.1.7.1");
			contentInfo4.Content = asn9;
			ASN1 asn10 = new ASN1(48);
			if (this._password != null)
			{
				byte[] array = new byte[20];
				this.RNG.GetBytes(array);
				byte[] data = this.MAC(this._password, array, this._iterations, contentInfo4.Content[0].Value);
				ASN1 asn11 = new ASN1(48);
				asn11.Add(ASN1Convert.FromOid("1.3.14.3.2.26"));
				asn11.Add(new ASN1(5));
				ASN1 asn12 = new ASN1(48);
				asn12.Add(asn11);
				asn12.Add(new ASN1(4, data));
				asn10.Add(asn12);
				asn10.Add(new ASN1(4, array));
				asn10.Add(ASN1Convert.FromInt32(this._iterations));
			}
			ASN1 asn13 = new ASN1(2, new byte[]
			{
				3
			});
			ASN1 asn14 = new ASN1(48);
			asn14.Add(asn13);
			asn14.Add(contentInfo4.ASN1);
			if (asn10.Count > 0)
			{
				asn14.Add(asn10);
			}
			return asn14.GetBytes();
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00031334 File Offset: 0x0002F534
		private PKCS7.ContentInfo EncryptedContentInfo(ASN1 safeBags, string algorithmOid)
		{
			byte[] array = new byte[8];
			this.RNG.GetBytes(array);
			ASN1 asn = new ASN1(48);
			asn.Add(new ASN1(4, array));
			asn.Add(ASN1Convert.FromInt32(this._iterations));
			ASN1 asn2 = new ASN1(48);
			asn2.Add(ASN1Convert.FromOid(algorithmOid));
			asn2.Add(asn);
			byte[] data = this.Encrypt(algorithmOid, array, this._iterations, safeBags.GetBytes());
			ASN1 asn3 = new ASN1(128, data);
			ASN1 asn4 = new ASN1(48);
			asn4.Add(ASN1Convert.FromOid("1.2.840.113549.1.7.1"));
			asn4.Add(asn2);
			asn4.Add(asn3);
			ASN1 asn5 = new ASN1(2, new byte[1]);
			ASN1 asn6 = new ASN1(48);
			asn6.Add(asn5);
			asn6.Add(asn4);
			ASN1 asn7 = new ASN1(160);
			asn7.Add(asn6);
			return new PKCS7.ContentInfo("1.2.840.113549.1.7.6")
			{
				Content = asn7
			};
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00031444 File Offset: 0x0002F644
		public void AddCertificate(X509Certificate cert)
		{
			this.AddCertificate(cert, null);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00031450 File Offset: 0x0002F650
		public void AddCertificate(X509Certificate cert, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					ASN1 asn = safeBag.ASN1;
					ASN1 asn2 = asn[1];
					PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(asn2.Value);
					X509Certificate x509Certificate = new X509Certificate(contentInfo.Content[0].Value);
					if (this.Compare(cert.RawData, x509Certificate.RawData))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				this._safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.3", this.CertificateSafeBag(cert, attributes)));
				this._certsChanged = true;
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00031524 File Offset: 0x0002F724
		public void RemoveCertificate(X509Certificate cert)
		{
			this.RemoveCertificate(cert, null);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00031530 File Offset: 0x0002F730
		public void RemoveCertificate(X509Certificate cert, IDictionary attrs)
		{
			int num = -1;
			int num2 = 0;
			while (num == -1 && num2 < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num2];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					ASN1 asn = safeBag.ASN1;
					ASN1 asn2 = asn[1];
					PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(asn2.Value);
					X509Certificate x509Certificate = new X509Certificate(contentInfo.Content[0].Value);
					if (this.Compare(cert.RawData, x509Certificate.RawData))
					{
						if (attrs != null)
						{
							if (asn.Count == 3)
							{
								ASN1 asn3 = asn[2];
								int num3 = 0;
								for (int i = 0; i < asn3.Count; i++)
								{
									ASN1 asn4 = asn3[i];
									ASN1 asn5 = asn4[0];
									string key = ASN1Convert.ToOid(asn5);
									ArrayList arrayList = (ArrayList)attrs[key];
									if (arrayList != null)
									{
										ASN1 asn6 = asn4[1];
										if (arrayList.Count == asn6.Count)
										{
											int num4 = 0;
											for (int j = 0; j < asn6.Count; j++)
											{
												ASN1 asn7 = asn6[j];
												byte[] expected = (byte[])arrayList[j];
												if (this.Compare(expected, asn7.Value))
												{
													num4++;
												}
											}
											if (num4 == asn6.Count)
											{
												num3++;
											}
										}
									}
								}
								if (num3 == asn3.Count)
								{
									num = num2;
								}
							}
						}
						else
						{
							num = num2;
						}
					}
				}
				num2++;
			}
			if (num != -1)
			{
				this._safeBags.RemoveAt(num);
				this._certsChanged = true;
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000316FC File Offset: 0x0002F8FC
		private bool CompareAsymmetricAlgorithm(AsymmetricAlgorithm a1, AsymmetricAlgorithm a2)
		{
			return a1.KeySize == a2.KeySize && a1.ToXmlString(false) == a2.ToXmlString(false);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00031730 File Offset: 0x0002F930
		public void AddPkcs8ShroudedKeyBag(AsymmetricAlgorithm aa)
		{
			this.AddPkcs8ShroudedKeyBag(aa, null);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0003173C File Offset: 0x0002F93C
		public void AddPkcs8ShroudedKeyBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					ASN1 asn = safeBag.ASN1[1];
					PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(asn.Value);
					byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
					PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo(array);
					byte[] privateKey = privateKeyInfo.PrivateKey;
					byte b = privateKey[0];
					AsymmetricAlgorithm a;
					if (b != 2)
					{
						if (b != 48)
						{
							Array.Clear(array, 0, array.Length);
							Array.Clear(privateKey, 0, privateKey.Length);
							throw new CryptographicException("Unknown private key format");
						}
						a = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
					}
					else
					{
						a = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
					}
					Array.Clear(array, 0, array.Length);
					Array.Clear(privateKey, 0, privateKey.Length);
					if (this.CompareAsymmetricAlgorithm(aa, a))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				this._safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.2", this.Pkcs8ShroudedKeyBagSafeBag(aa, attributes)));
				this._keyBagsChanged = true;
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000318A0 File Offset: 0x0002FAA0
		public void RemovePkcs8ShroudedKeyBag(AsymmetricAlgorithm aa)
		{
			int num = -1;
			int num2 = 0;
			while (num == -1 && num2 < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num2];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					ASN1 asn = safeBag.ASN1[1];
					PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(asn.Value);
					byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
					PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo(array);
					byte[] privateKey = privateKeyInfo.PrivateKey;
					byte b = privateKey[0];
					AsymmetricAlgorithm a;
					if (b != 2)
					{
						if (b != 48)
						{
							Array.Clear(array, 0, array.Length);
							Array.Clear(privateKey, 0, privateKey.Length);
							throw new CryptographicException("Unknown private key format");
						}
						a = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
					}
					else
					{
						a = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
					}
					Array.Clear(array, 0, array.Length);
					Array.Clear(privateKey, 0, privateKey.Length);
					if (this.CompareAsymmetricAlgorithm(aa, a))
					{
						num = num2;
					}
				}
				num2++;
			}
			if (num != -1)
			{
				this._safeBags.RemoveAt(num);
				this._keyBagsChanged = true;
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000319F4 File Offset: 0x0002FBF4
		public void AddKeyBag(AsymmetricAlgorithm aa)
		{
			this.AddKeyBag(aa, null);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00031A00 File Offset: 0x0002FC00
		public void AddKeyBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
				{
					ASN1 asn = safeBag.ASN1[1];
					PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo(asn.Value);
					byte[] privateKey = privateKeyInfo.PrivateKey;
					byte b = privateKey[0];
					AsymmetricAlgorithm a;
					if (b != 2)
					{
						if (b != 48)
						{
							Array.Clear(privateKey, 0, privateKey.Length);
							throw new CryptographicException("Unknown private key format");
						}
						a = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
					}
					else
					{
						a = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
					}
					Array.Clear(privateKey, 0, privateKey.Length);
					if (this.CompareAsymmetricAlgorithm(aa, a))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				this._safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.1", this.KeyBagSafeBag(aa, attributes)));
				this._keyBagsChanged = true;
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00031B20 File Offset: 0x0002FD20
		public void RemoveKeyBag(AsymmetricAlgorithm aa)
		{
			int num = -1;
			int num2 = 0;
			while (num == -1 && num2 < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num2];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
				{
					ASN1 asn = safeBag.ASN1[1];
					PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo(asn.Value);
					byte[] privateKey = privateKeyInfo.PrivateKey;
					byte b = privateKey[0];
					AsymmetricAlgorithm a;
					if (b != 2)
					{
						if (b != 48)
						{
							Array.Clear(privateKey, 0, privateKey.Length);
							throw new CryptographicException("Unknown private key format");
						}
						a = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
					}
					else
					{
						a = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
					}
					Array.Clear(privateKey, 0, privateKey.Length);
					if (this.CompareAsymmetricAlgorithm(aa, a))
					{
						num = num2;
					}
				}
				num2++;
			}
			if (num != -1)
			{
				this._safeBags.RemoveAt(num);
				this._keyBagsChanged = true;
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00031C30 File Offset: 0x0002FE30
		public void AddSecretBag(byte[] secret)
		{
			this.AddSecretBag(secret, null);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00031C3C File Offset: 0x0002FE3C
		public void AddSecretBag(byte[] secret, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
				{
					ASN1 asn = safeBag.ASN1[1];
					byte[] value = asn.Value;
					if (this.Compare(secret, value))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				this._safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.5", this.SecretBagSafeBag(secret, attributes)));
				this._secretBagsChanged = true;
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00031CE4 File Offset: 0x0002FEE4
		public void RemoveSecretBag(byte[] secret)
		{
			int num = -1;
			int num2 = 0;
			while (num == -1 && num2 < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num2];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
				{
					ASN1 asn = safeBag.ASN1[1];
					byte[] value = asn.Value;
					if (this.Compare(secret, value))
					{
						num = num2;
					}
				}
				num2++;
			}
			if (num != -1)
			{
				this._safeBags.RemoveAt(num);
				this._secretBagsChanged = true;
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00031D7C File Offset: 0x0002FF7C
		public AsymmetricAlgorithm GetAsymmetricAlgorithm(IDictionary attrs)
		{
			foreach (object obj in this._safeBags)
			{
				SafeBag safeBag = (SafeBag)obj;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1") || safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					ASN1 asn = safeBag.ASN1;
					if (asn.Count == 3)
					{
						ASN1 asn2 = asn[2];
						int num = 0;
						for (int i = 0; i < asn2.Count; i++)
						{
							ASN1 asn3 = asn2[i];
							ASN1 asn4 = asn3[0];
							string key = ASN1Convert.ToOid(asn4);
							ArrayList arrayList = (ArrayList)attrs[key];
							if (arrayList != null)
							{
								ASN1 asn5 = asn3[1];
								if (arrayList.Count == asn5.Count)
								{
									int num2 = 0;
									for (int j = 0; j < asn5.Count; j++)
									{
										ASN1 asn6 = asn5[j];
										byte[] expected = (byte[])arrayList[j];
										if (this.Compare(expected, asn6.Value))
										{
											num2++;
										}
									}
									if (num2 == asn5.Count)
									{
										num++;
									}
								}
							}
						}
						if (num == asn2.Count)
						{
							ASN1 asn7 = asn[1];
							AsymmetricAlgorithm result = null;
							if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
							{
								PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo(asn7.Value);
								byte[] privateKey = privateKeyInfo.PrivateKey;
								byte b = privateKey[0];
								if (b != 2)
								{
									if (b == 48)
									{
										result = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
									}
								}
								else
								{
									result = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
								}
								Array.Clear(privateKey, 0, privateKey.Length);
							}
							else if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
							{
								PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(asn7.Value);
								byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
								PKCS8.PrivateKeyInfo privateKeyInfo2 = new PKCS8.PrivateKeyInfo(array);
								byte[] privateKey2 = privateKeyInfo2.PrivateKey;
								byte b = privateKey2[0];
								if (b != 2)
								{
									if (b == 48)
									{
										result = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey2);
									}
								}
								else
								{
									result = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey2, default(DSAParameters));
								}
								Array.Clear(privateKey2, 0, privateKey2.Length);
								Array.Clear(array, 0, array.Length);
							}
							return result;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00032064 File Offset: 0x00030264
		public byte[] GetSecret(IDictionary attrs)
		{
			foreach (object obj in this._safeBags)
			{
				SafeBag safeBag = (SafeBag)obj;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
				{
					ASN1 asn = safeBag.ASN1;
					if (asn.Count == 3)
					{
						ASN1 asn2 = asn[2];
						int num = 0;
						for (int i = 0; i < asn2.Count; i++)
						{
							ASN1 asn3 = asn2[i];
							ASN1 asn4 = asn3[0];
							string key = ASN1Convert.ToOid(asn4);
							ArrayList arrayList = (ArrayList)attrs[key];
							if (arrayList != null)
							{
								ASN1 asn5 = asn3[1];
								if (arrayList.Count == asn5.Count)
								{
									int num2 = 0;
									for (int j = 0; j < asn5.Count; j++)
									{
										ASN1 asn6 = asn5[j];
										byte[] expected = (byte[])arrayList[j];
										if (this.Compare(expected, asn6.Value))
										{
											num2++;
										}
									}
									if (num2 == asn5.Count)
									{
										num++;
									}
								}
							}
						}
						if (num == asn2.Count)
						{
							ASN1 asn7 = asn[1];
							return asn7.Value;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00032200 File Offset: 0x00030400
		public X509Certificate GetCertificate(IDictionary attrs)
		{
			foreach (object obj in this._safeBags)
			{
				SafeBag safeBag = (SafeBag)obj;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					ASN1 asn = safeBag.ASN1;
					if (asn.Count == 3)
					{
						ASN1 asn2 = asn[2];
						int num = 0;
						for (int i = 0; i < asn2.Count; i++)
						{
							ASN1 asn3 = asn2[i];
							ASN1 asn4 = asn3[0];
							string key = ASN1Convert.ToOid(asn4);
							ArrayList arrayList = (ArrayList)attrs[key];
							if (arrayList != null)
							{
								ASN1 asn5 = asn3[1];
								if (arrayList.Count == asn5.Count)
								{
									int num2 = 0;
									for (int j = 0; j < asn5.Count; j++)
									{
										ASN1 asn6 = asn5[j];
										byte[] expected = (byte[])arrayList[j];
										if (this.Compare(expected, asn6.Value))
										{
											num2++;
										}
									}
									if (num2 == asn5.Count)
									{
										num++;
									}
								}
							}
						}
						if (num == asn2.Count)
						{
							ASN1 asn7 = asn[1];
							PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(asn7.Value);
							return new X509Certificate(contentInfo.Content[0].Value);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000323B8 File Offset: 0x000305B8
		public IDictionary GetAttributes(AsymmetricAlgorithm aa)
		{
			IDictionary dictionary = new Hashtable();
			foreach (object obj in this._safeBags)
			{
				SafeBag safeBag = (SafeBag)obj;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1") || safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					ASN1 asn = safeBag.ASN1;
					ASN1 asn2 = asn[1];
					AsymmetricAlgorithm asymmetricAlgorithm = null;
					if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
					{
						PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo(asn2.Value);
						byte[] privateKey = privateKeyInfo.PrivateKey;
						byte b = privateKey[0];
						if (b != 2)
						{
							if (b == 48)
							{
								asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
							}
						}
						else
						{
							asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
						}
						Array.Clear(privateKey, 0, privateKey.Length);
					}
					else if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
					{
						PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(asn2.Value);
						byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
						PKCS8.PrivateKeyInfo privateKeyInfo2 = new PKCS8.PrivateKeyInfo(array);
						byte[] privateKey2 = privateKeyInfo2.PrivateKey;
						byte b = privateKey2[0];
						if (b != 2)
						{
							if (b == 48)
							{
								asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey2);
							}
						}
						else
						{
							asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey2, default(DSAParameters));
						}
						Array.Clear(privateKey2, 0, privateKey2.Length);
						Array.Clear(array, 0, array.Length);
					}
					if (asymmetricAlgorithm != null && this.CompareAsymmetricAlgorithm(asymmetricAlgorithm, aa) && asn.Count == 3)
					{
						ASN1 asn3 = asn[2];
						for (int i = 0; i < asn3.Count; i++)
						{
							ASN1 asn4 = asn3[i];
							ASN1 asn5 = asn4[0];
							string key = ASN1Convert.ToOid(asn5);
							ArrayList arrayList = new ArrayList();
							ASN1 asn6 = asn4[1];
							for (int j = 0; j < asn6.Count; j++)
							{
								ASN1 asn7 = asn6[j];
								arrayList.Add(asn7.Value);
							}
							dictionary.Add(key, arrayList);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00032658 File Offset: 0x00030858
		public IDictionary GetAttributes(X509Certificate cert)
		{
			IDictionary dictionary = new Hashtable();
			foreach (object obj in this._safeBags)
			{
				SafeBag safeBag = (SafeBag)obj;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					ASN1 asn = safeBag.ASN1;
					ASN1 asn2 = asn[1];
					PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(asn2.Value);
					X509Certificate x509Certificate = new X509Certificate(contentInfo.Content[0].Value);
					if (this.Compare(cert.RawData, x509Certificate.RawData) && asn.Count == 3)
					{
						ASN1 asn3 = asn[2];
						for (int i = 0; i < asn3.Count; i++)
						{
							ASN1 asn4 = asn3[i];
							ASN1 asn5 = asn4[0];
							string key = ASN1Convert.ToOid(asn5);
							ArrayList arrayList = new ArrayList();
							ASN1 asn6 = asn4[1];
							for (int j = 0; j < asn6.Count; j++)
							{
								ASN1 asn7 = asn6[j];
								arrayList.Add(asn7.Value);
							}
							dictionary.Add(key, arrayList);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000327D0 File Offset: 0x000309D0
		public void SaveToFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			using (FileStream fileStream = File.Create(filename))
			{
				byte[] bytes = this.GetBytes();
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00032838 File Offset: 0x00030A38
		public object Clone()
		{
			PKCS12 pkcs;
			if (this._password != null)
			{
				pkcs = new PKCS12(this.GetBytes(), Encoding.BigEndianUnicode.GetString(this._password));
			}
			else
			{
				pkcs = new PKCS12(this.GetBytes());
			}
			pkcs.IterationCount = this.IterationCount;
			return pkcs;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0003288C File Offset: 0x00030A8C
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00032894 File Offset: 0x00030A94
		public static int MaximumPasswordLength
		{
			get
			{
				return PKCS12.password_max_length;
			}
			set
			{
				if (value < 32)
				{
					string text = Locale.GetText("Maximum password length cannot be less than {0}.", new object[]
					{
						32
					});
					throw new ArgumentOutOfRangeException(text);
				}
				PKCS12.password_max_length = value;
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000328D4 File Offset: 0x00030AD4
		private static byte[] LoadFile(string filename)
		{
			byte[] array = null;
			using (FileStream fileStream = File.OpenRead(filename))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return array;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00032938 File Offset: 0x00030B38
		public static PKCS12 LoadFromFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			return new PKCS12(PKCS12.LoadFile(filename));
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00032958 File Offset: 0x00030B58
		public static PKCS12 LoadFromFile(string filename, string password)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			return new PKCS12(PKCS12.LoadFile(filename), password);
		}

		// Token: 0x040002A8 RID: 680
		public const string pbeWithSHAAnd128BitRC4 = "1.2.840.113549.1.12.1.1";

		// Token: 0x040002A9 RID: 681
		public const string pbeWithSHAAnd40BitRC4 = "1.2.840.113549.1.12.1.2";

		// Token: 0x040002AA RID: 682
		public const string pbeWithSHAAnd3KeyTripleDESCBC = "1.2.840.113549.1.12.1.3";

		// Token: 0x040002AB RID: 683
		public const string pbeWithSHAAnd2KeyTripleDESCBC = "1.2.840.113549.1.12.1.4";

		// Token: 0x040002AC RID: 684
		public const string pbeWithSHAAnd128BitRC2CBC = "1.2.840.113549.1.12.1.5";

		// Token: 0x040002AD RID: 685
		public const string pbeWithSHAAnd40BitRC2CBC = "1.2.840.113549.1.12.1.6";

		// Token: 0x040002AE RID: 686
		public const string keyBag = "1.2.840.113549.1.12.10.1.1";

		// Token: 0x040002AF RID: 687
		public const string pkcs8ShroudedKeyBag = "1.2.840.113549.1.12.10.1.2";

		// Token: 0x040002B0 RID: 688
		public const string certBag = "1.2.840.113549.1.12.10.1.3";

		// Token: 0x040002B1 RID: 689
		public const string crlBag = "1.2.840.113549.1.12.10.1.4";

		// Token: 0x040002B2 RID: 690
		public const string secretBag = "1.2.840.113549.1.12.10.1.5";

		// Token: 0x040002B3 RID: 691
		public const string safeContentsBag = "1.2.840.113549.1.12.10.1.6";

		// Token: 0x040002B4 RID: 692
		public const string x509Certificate = "1.2.840.113549.1.9.22.1";

		// Token: 0x040002B5 RID: 693
		public const string sdsiCertificate = "1.2.840.113549.1.9.22.2";

		// Token: 0x040002B6 RID: 694
		public const string x509Crl = "1.2.840.113549.1.9.23.1";

		// Token: 0x040002B7 RID: 695
		public const int CryptoApiPasswordLimit = 32;

		// Token: 0x040002B8 RID: 696
		private static int recommendedIterationCount = 2000;

		// Token: 0x040002B9 RID: 697
		private byte[] _password;

		// Token: 0x040002BA RID: 698
		private ArrayList _keyBags;

		// Token: 0x040002BB RID: 699
		private ArrayList _secretBags;

		// Token: 0x040002BC RID: 700
		private X509CertificateCollection _certs;

		// Token: 0x040002BD RID: 701
		private bool _keyBagsChanged;

		// Token: 0x040002BE RID: 702
		private bool _secretBagsChanged;

		// Token: 0x040002BF RID: 703
		private bool _certsChanged;

		// Token: 0x040002C0 RID: 704
		private int _iterations;

		// Token: 0x040002C1 RID: 705
		private ArrayList _safeBags;

		// Token: 0x040002C2 RID: 706
		private RandomNumberGenerator _rng;

		// Token: 0x040002C3 RID: 707
		private static int password_max_length = int.MaxValue;

		// Token: 0x020000C5 RID: 197
		public class DeriveBytes
		{
			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06000B0E RID: 2830 RVA: 0x000329D4 File Offset: 0x00030BD4
			// (set) Token: 0x06000B0F RID: 2831 RVA: 0x000329DC File Offset: 0x00030BDC
			public string HashName
			{
				get
				{
					return this._hashName;
				}
				set
				{
					this._hashName = value;
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000B10 RID: 2832 RVA: 0x000329E8 File Offset: 0x00030BE8
			// (set) Token: 0x06000B11 RID: 2833 RVA: 0x000329F0 File Offset: 0x00030BF0
			public int IterationCount
			{
				get
				{
					return this._iterations;
				}
				set
				{
					this._iterations = value;
				}
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x06000B12 RID: 2834 RVA: 0x000329FC File Offset: 0x00030BFC
			// (set) Token: 0x06000B13 RID: 2835 RVA: 0x00032A10 File Offset: 0x00030C10
			public byte[] Password
			{
				get
				{
					return (byte[])this._password.Clone();
				}
				set
				{
					if (value == null)
					{
						this._password = new byte[0];
					}
					else
					{
						this._password = (byte[])value.Clone();
					}
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00032A48 File Offset: 0x00030C48
			// (set) Token: 0x06000B15 RID: 2837 RVA: 0x00032A5C File Offset: 0x00030C5C
			public byte[] Salt
			{
				get
				{
					return (byte[])this._salt.Clone();
				}
				set
				{
					if (value != null)
					{
						this._salt = (byte[])value.Clone();
					}
					else
					{
						this._salt = null;
					}
				}
			}

			// Token: 0x06000B16 RID: 2838 RVA: 0x00032A84 File Offset: 0x00030C84
			private void Adjust(byte[] a, int aOff, byte[] b)
			{
				int num = (int)((b[b.Length - 1] & byte.MaxValue) + (a[aOff + b.Length - 1] & byte.MaxValue) + 1);
				a[aOff + b.Length - 1] = (byte)num;
				num >>= 8;
				for (int i = b.Length - 2; i >= 0; i--)
				{
					num += (int)((b[i] & byte.MaxValue) + (a[aOff + i] & byte.MaxValue));
					a[aOff + i] = (byte)num;
					num >>= 8;
				}
			}

			// Token: 0x06000B17 RID: 2839 RVA: 0x00032AFC File Offset: 0x00030CFC
			private byte[] Derive(byte[] diversifier, int n)
			{
				HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this._hashName);
				int num = hashAlgorithm.HashSize >> 3;
				int num2 = 64;
				byte[] array = new byte[n];
				byte[] array2;
				if (this._salt != null && this._salt.Length != 0)
				{
					array2 = new byte[num2 * ((this._salt.Length + num2 - 1) / num2)];
					for (int num3 = 0; num3 != array2.Length; num3++)
					{
						array2[num3] = this._salt[num3 % this._salt.Length];
					}
				}
				else
				{
					array2 = new byte[0];
				}
				byte[] array3;
				if (this._password != null && this._password.Length != 0)
				{
					array3 = new byte[num2 * ((this._password.Length + num2 - 1) / num2)];
					for (int num4 = 0; num4 != array3.Length; num4++)
					{
						array3[num4] = this._password[num4 % this._password.Length];
					}
				}
				else
				{
					array3 = new byte[0];
				}
				byte[] array4 = new byte[array2.Length + array3.Length];
				Buffer.BlockCopy(array2, 0, array4, 0, array2.Length);
				Buffer.BlockCopy(array3, 0, array4, array2.Length, array3.Length);
				byte[] array5 = new byte[num2];
				int num5 = (n + num - 1) / num;
				for (int i = 1; i <= num5; i++)
				{
					hashAlgorithm.TransformBlock(diversifier, 0, diversifier.Length, diversifier, 0);
					hashAlgorithm.TransformFinalBlock(array4, 0, array4.Length);
					byte[] array6 = hashAlgorithm.Hash;
					hashAlgorithm.Initialize();
					for (int num6 = 1; num6 != this._iterations; num6++)
					{
						array6 = hashAlgorithm.ComputeHash(array6, 0, array6.Length);
					}
					for (int num7 = 0; num7 != array5.Length; num7++)
					{
						array5[num7] = array6[num7 % array6.Length];
					}
					for (int num8 = 0; num8 != array4.Length / num2; num8++)
					{
						this.Adjust(array4, num8 * num2, array5);
					}
					if (i == num5)
					{
						Buffer.BlockCopy(array6, 0, array, (i - 1) * num, array.Length - (i - 1) * num);
					}
					else
					{
						Buffer.BlockCopy(array6, 0, array, (i - 1) * num, array6.Length);
					}
				}
				return array;
			}

			// Token: 0x06000B18 RID: 2840 RVA: 0x00032D3C File Offset: 0x00030F3C
			public byte[] DeriveKey(int size)
			{
				return this.Derive(PKCS12.DeriveBytes.keyDiversifier, size);
			}

			// Token: 0x06000B19 RID: 2841 RVA: 0x00032D4C File Offset: 0x00030F4C
			public byte[] DeriveIV(int size)
			{
				return this.Derive(PKCS12.DeriveBytes.ivDiversifier, size);
			}

			// Token: 0x06000B1A RID: 2842 RVA: 0x00032D5C File Offset: 0x00030F5C
			public byte[] DeriveMAC(int size)
			{
				return this.Derive(PKCS12.DeriveBytes.macDiversifier, size);
			}

			// Token: 0x040002CC RID: 716
			private static byte[] keyDiversifier = new byte[]
			{
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1,
				1
			};

			// Token: 0x040002CD RID: 717
			private static byte[] ivDiversifier = new byte[]
			{
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2,
				2
			};

			// Token: 0x040002CE RID: 718
			private static byte[] macDiversifier = new byte[]
			{
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3,
				3
			};

			// Token: 0x040002CF RID: 719
			private string _hashName;

			// Token: 0x040002D0 RID: 720
			private int _iterations;

			// Token: 0x040002D1 RID: 721
			private byte[] _password;

			// Token: 0x040002D2 RID: 722
			private byte[] _salt;

			// Token: 0x020000C6 RID: 198
			public enum Purpose
			{
				// Token: 0x040002D4 RID: 724
				Key,
				// Token: 0x040002D5 RID: 725
				IV,
				// Token: 0x040002D6 RID: 726
				MAC
			}
		}
	}
}
