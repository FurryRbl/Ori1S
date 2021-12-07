using System;
using System.Collections;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000031 RID: 49
	public sealed class PKCS8
	{
		// Token: 0x06000217 RID: 535 RVA: 0x0000D978 File Offset: 0x0000BB78
		private PKCS8()
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000D980 File Offset: 0x0000BB80
		public static PKCS8.KeyInfo GetType(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			PKCS8.KeyInfo result = PKCS8.KeyInfo.Unknown;
			try
			{
				ASN1 asn = new ASN1(data);
				if (asn.Tag == 48 && asn.Count > 0)
				{
					ASN1 asn2 = asn[0];
					byte tag = asn2.Tag;
					if (tag != 2)
					{
						if (tag == 48)
						{
							result = PKCS8.KeyInfo.EncryptedPrivateKey;
						}
					}
					else
					{
						result = PKCS8.KeyInfo.PrivateKey;
					}
				}
			}
			catch
			{
				throw new CryptographicException("invalid ASN.1 data");
			}
			return result;
		}

		// Token: 0x02000032 RID: 50
		public enum KeyInfo
		{
			// Token: 0x040000E5 RID: 229
			PrivateKey,
			// Token: 0x040000E6 RID: 230
			EncryptedPrivateKey,
			// Token: 0x040000E7 RID: 231
			Unknown
		}

		// Token: 0x02000033 RID: 51
		public class PrivateKeyInfo
		{
			// Token: 0x06000219 RID: 537 RVA: 0x0000DA24 File Offset: 0x0000BC24
			public PrivateKeyInfo()
			{
				this._version = 0;
				this._list = new ArrayList();
			}

			// Token: 0x0600021A RID: 538 RVA: 0x0000DA40 File Offset: 0x0000BC40
			public PrivateKeyInfo(byte[] data) : this()
			{
				this.Decode(data);
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x0600021B RID: 539 RVA: 0x0000DA50 File Offset: 0x0000BC50
			// (set) Token: 0x0600021C RID: 540 RVA: 0x0000DA58 File Offset: 0x0000BC58
			public string Algorithm
			{
				get
				{
					return this._algorithm;
				}
				set
				{
					this._algorithm = value;
				}
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x0600021D RID: 541 RVA: 0x0000DA64 File Offset: 0x0000BC64
			public ArrayList Attributes
			{
				get
				{
					return this._list;
				}
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x0600021E RID: 542 RVA: 0x0000DA6C File Offset: 0x0000BC6C
			// (set) Token: 0x0600021F RID: 543 RVA: 0x0000DA8C File Offset: 0x0000BC8C
			public byte[] PrivateKey
			{
				get
				{
					if (this._key == null)
					{
						return null;
					}
					return (byte[])this._key.Clone();
				}
				set
				{
					if (value == null)
					{
						throw new ArgumentNullException("PrivateKey");
					}
					this._key = (byte[])value.Clone();
				}
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06000220 RID: 544 RVA: 0x0000DABC File Offset: 0x0000BCBC
			// (set) Token: 0x06000221 RID: 545 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
			public int Version
			{
				get
				{
					return this._version;
				}
				set
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("negative version");
					}
					this._version = value;
				}
			}

			// Token: 0x06000222 RID: 546 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
			private void Decode(byte[] data)
			{
				ASN1 asn = new ASN1(data);
				if (asn.Tag != 48)
				{
					throw new CryptographicException("invalid PrivateKeyInfo");
				}
				ASN1 asn2 = asn[0];
				if (asn2.Tag != 2)
				{
					throw new CryptographicException("invalid version");
				}
				this._version = (int)asn2.Value[0];
				ASN1 asn3 = asn[1];
				if (asn3.Tag != 48)
				{
					throw new CryptographicException("invalid algorithm");
				}
				ASN1 asn4 = asn3[0];
				if (asn4.Tag != 6)
				{
					throw new CryptographicException("missing algorithm OID");
				}
				this._algorithm = ASN1Convert.ToOid(asn4);
				ASN1 asn5 = asn[2];
				this._key = asn5.Value;
				if (asn.Count > 3)
				{
					ASN1 asn6 = asn[3];
					for (int i = 0; i < asn6.Count; i++)
					{
						this._list.Add(asn6[i]);
					}
				}
			}

			// Token: 0x06000223 RID: 547 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
			public byte[] GetBytes()
			{
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this._algorithm));
				asn.Add(new ASN1(5));
				ASN1 asn2 = new ASN1(48);
				asn2.Add(new ASN1(2, new byte[]
				{
					(byte)this._version
				}));
				asn2.Add(asn);
				asn2.Add(new ASN1(4, this._key));
				if (this._list.Count > 0)
				{
					ASN1 asn3 = new ASN1(160);
					foreach (object obj in this._list)
					{
						ASN1 asn4 = (ASN1)obj;
						asn3.Add(asn4);
					}
					asn2.Add(asn3);
				}
				return asn2.GetBytes();
			}

			// Token: 0x06000224 RID: 548 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
			private static byte[] RemoveLeadingZero(byte[] bigInt)
			{
				int srcOffset = 0;
				int num = bigInt.Length;
				if (bigInt[0] == 0)
				{
					srcOffset = 1;
					num--;
				}
				byte[] array = new byte[num];
				Buffer.BlockCopy(bigInt, srcOffset, array, 0, num);
				return array;
			}

			// Token: 0x06000225 RID: 549 RVA: 0x0000DD1C File Offset: 0x0000BF1C
			private static byte[] Normalize(byte[] bigInt, int length)
			{
				if (bigInt.Length == length)
				{
					return bigInt;
				}
				if (bigInt.Length > length)
				{
					return PKCS8.PrivateKeyInfo.RemoveLeadingZero(bigInt);
				}
				byte[] array = new byte[length];
				Buffer.BlockCopy(bigInt, 0, array, length - bigInt.Length, bigInt.Length);
				return array;
			}

			// Token: 0x06000226 RID: 550 RVA: 0x0000DD5C File Offset: 0x0000BF5C
			public static RSA DecodeRSA(byte[] keypair)
			{
				ASN1 asn = new ASN1(keypair);
				if (asn.Tag != 48)
				{
					throw new CryptographicException("invalid private key format");
				}
				ASN1 asn2 = asn[0];
				if (asn2.Tag != 2)
				{
					throw new CryptographicException("missing version");
				}
				if (asn.Count < 9)
				{
					throw new CryptographicException("not enough key parameters");
				}
				RSAParameters parameters = default(RSAParameters);
				parameters.Modulus = PKCS8.PrivateKeyInfo.RemoveLeadingZero(asn[1].Value);
				int num = parameters.Modulus.Length;
				int length = num >> 1;
				parameters.D = PKCS8.PrivateKeyInfo.Normalize(asn[3].Value, num);
				parameters.DP = PKCS8.PrivateKeyInfo.Normalize(asn[6].Value, length);
				parameters.DQ = PKCS8.PrivateKeyInfo.Normalize(asn[7].Value, length);
				parameters.Exponent = PKCS8.PrivateKeyInfo.RemoveLeadingZero(asn[2].Value);
				parameters.InverseQ = PKCS8.PrivateKeyInfo.Normalize(asn[8].Value, length);
				parameters.P = PKCS8.PrivateKeyInfo.Normalize(asn[4].Value, length);
				parameters.Q = PKCS8.PrivateKeyInfo.Normalize(asn[5].Value, length);
				RSA rsa = null;
				try
				{
					rsa = RSA.Create();
					rsa.ImportParameters(parameters);
				}
				catch (CryptographicException)
				{
					rsa = new RSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					rsa.ImportParameters(parameters);
				}
				return rsa;
			}

			// Token: 0x06000227 RID: 551 RVA: 0x0000DEFC File Offset: 0x0000C0FC
			public static byte[] Encode(RSA rsa)
			{
				RSAParameters rsaparameters = rsa.ExportParameters(true);
				ASN1 asn = new ASN1(48);
				asn.Add(new ASN1(2, new byte[1]));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Modulus));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Exponent));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.D));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.P));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Q));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.DP));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.DQ));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.InverseQ));
				return asn.GetBytes();
			}

			// Token: 0x06000228 RID: 552 RVA: 0x0000DFCC File Offset: 0x0000C1CC
			public static DSA DecodeDSA(byte[] privateKey, DSAParameters dsaParameters)
			{
				ASN1 asn = new ASN1(privateKey);
				if (asn.Tag != 2)
				{
					throw new CryptographicException("invalid private key format");
				}
				dsaParameters.X = PKCS8.PrivateKeyInfo.Normalize(asn.Value, 20);
				DSA dsa = DSA.Create();
				dsa.ImportParameters(dsaParameters);
				return dsa;
			}

			// Token: 0x06000229 RID: 553 RVA: 0x0000E01C File Offset: 0x0000C21C
			public static byte[] Encode(DSA dsa)
			{
				return ASN1Convert.FromUnsignedBigInteger(dsa.ExportParameters(true).X).GetBytes();
			}

			// Token: 0x0600022A RID: 554 RVA: 0x0000E044 File Offset: 0x0000C244
			public static byte[] Encode(AsymmetricAlgorithm aa)
			{
				if (aa is RSA)
				{
					return PKCS8.PrivateKeyInfo.Encode((RSA)aa);
				}
				if (aa is DSA)
				{
					return PKCS8.PrivateKeyInfo.Encode((DSA)aa);
				}
				throw new CryptographicException("Unknown asymmetric algorithm {0}", aa.ToString());
			}

			// Token: 0x040000E8 RID: 232
			private int _version;

			// Token: 0x040000E9 RID: 233
			private string _algorithm;

			// Token: 0x040000EA RID: 234
			private byte[] _key;

			// Token: 0x040000EB RID: 235
			private ArrayList _list;
		}

		// Token: 0x02000034 RID: 52
		public class EncryptedPrivateKeyInfo
		{
			// Token: 0x0600022B RID: 555 RVA: 0x0000E090 File Offset: 0x0000C290
			public EncryptedPrivateKeyInfo()
			{
			}

			// Token: 0x0600022C RID: 556 RVA: 0x0000E098 File Offset: 0x0000C298
			public EncryptedPrivateKeyInfo(byte[] data) : this()
			{
				this.Decode(data);
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x0600022D RID: 557 RVA: 0x0000E0A8 File Offset: 0x0000C2A8
			// (set) Token: 0x0600022E RID: 558 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
			public string Algorithm
			{
				get
				{
					return this._algorithm;
				}
				set
				{
					this._algorithm = value;
				}
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x0600022F RID: 559 RVA: 0x0000E0BC File Offset: 0x0000C2BC
			// (set) Token: 0x06000230 RID: 560 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
			public byte[] EncryptedData
			{
				get
				{
					return (this._data != null) ? ((byte[])this._data.Clone()) : null;
				}
				set
				{
					this._data = ((value != null) ? ((byte[])value.Clone()) : null);
				}
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000231 RID: 561 RVA: 0x0000E100 File Offset: 0x0000C300
			// (set) Token: 0x06000232 RID: 562 RVA: 0x0000E148 File Offset: 0x0000C348
			public byte[] Salt
			{
				get
				{
					if (this._salt == null)
					{
						RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
						this._salt = new byte[8];
						randomNumberGenerator.GetBytes(this._salt);
					}
					return (byte[])this._salt.Clone();
				}
				set
				{
					this._salt = (byte[])value.Clone();
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x06000233 RID: 563 RVA: 0x0000E15C File Offset: 0x0000C35C
			// (set) Token: 0x06000234 RID: 564 RVA: 0x0000E164 File Offset: 0x0000C364
			public int IterationCount
			{
				get
				{
					return this._iterations;
				}
				set
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("IterationCount", "Negative");
					}
					this._iterations = value;
				}
			}

			// Token: 0x06000235 RID: 565 RVA: 0x0000E184 File Offset: 0x0000C384
			private void Decode(byte[] data)
			{
				ASN1 asn = new ASN1(data);
				if (asn.Tag != 48)
				{
					throw new CryptographicException("invalid EncryptedPrivateKeyInfo");
				}
				ASN1 asn2 = asn[0];
				if (asn2.Tag != 48)
				{
					throw new CryptographicException("invalid encryptionAlgorithm");
				}
				ASN1 asn3 = asn2[0];
				if (asn3.Tag != 6)
				{
					throw new CryptographicException("invalid algorithm");
				}
				this._algorithm = ASN1Convert.ToOid(asn3);
				if (asn2.Count > 1)
				{
					ASN1 asn4 = asn2[1];
					if (asn4.Tag != 48)
					{
						throw new CryptographicException("invalid parameters");
					}
					ASN1 asn5 = asn4[0];
					if (asn5.Tag != 4)
					{
						throw new CryptographicException("invalid salt");
					}
					this._salt = asn5.Value;
					ASN1 asn6 = asn4[1];
					if (asn6.Tag != 2)
					{
						throw new CryptographicException("invalid iterationCount");
					}
					this._iterations = ASN1Convert.ToInt32(asn6);
				}
				ASN1 asn7 = asn[1];
				if (asn7.Tag != 4)
				{
					throw new CryptographicException("invalid EncryptedData");
				}
				this._data = asn7.Value;
			}

			// Token: 0x06000236 RID: 566 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
			public byte[] GetBytes()
			{
				if (this._algorithm == null)
				{
					throw new CryptographicException("No algorithm OID specified");
				}
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this._algorithm));
				if (this._iterations > 0 || this._salt != null)
				{
					ASN1 asn2 = new ASN1(4, this._salt);
					ASN1 asn3 = ASN1Convert.FromInt32(this._iterations);
					ASN1 asn4 = new ASN1(48);
					asn4.Add(asn2);
					asn4.Add(asn3);
					asn.Add(asn4);
				}
				ASN1 asn5 = new ASN1(4, this._data);
				ASN1 asn6 = new ASN1(48);
				asn6.Add(asn);
				asn6.Add(asn5);
				return asn6.GetBytes();
			}

			// Token: 0x040000EC RID: 236
			private string _algorithm;

			// Token: 0x040000ED RID: 237
			private byte[] _salt;

			// Token: 0x040000EE RID: 238
			private int _iterations;

			// Token: 0x040000EF RID: 239
			private byte[] _data;
		}
	}
}
