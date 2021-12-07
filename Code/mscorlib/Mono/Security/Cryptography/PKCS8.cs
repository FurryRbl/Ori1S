using System;
using System.Collections;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x020000B9 RID: 185
	internal sealed class PKCS8
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x0002C4DC File Offset: 0x0002A6DC
		private PKCS8()
		{
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002C4E4 File Offset: 0x0002A6E4
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

		// Token: 0x020000BA RID: 186
		public enum KeyInfo
		{
			// Token: 0x0400026F RID: 623
			PrivateKey,
			// Token: 0x04000270 RID: 624
			EncryptedPrivateKey,
			// Token: 0x04000271 RID: 625
			Unknown
		}

		// Token: 0x020000BB RID: 187
		public class PrivateKeyInfo
		{
			// Token: 0x06000A76 RID: 2678 RVA: 0x0002C588 File Offset: 0x0002A788
			public PrivateKeyInfo()
			{
				this._version = 0;
				this._list = new ArrayList();
			}

			// Token: 0x06000A77 RID: 2679 RVA: 0x0002C5A4 File Offset: 0x0002A7A4
			public PrivateKeyInfo(byte[] data) : this()
			{
				this.Decode(data);
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0002C5B4 File Offset: 0x0002A7B4
			// (set) Token: 0x06000A79 RID: 2681 RVA: 0x0002C5BC File Offset: 0x0002A7BC
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

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0002C5C8 File Offset: 0x0002A7C8
			public ArrayList Attributes
			{
				get
				{
					return this._list;
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0002C5D0 File Offset: 0x0002A7D0
			// (set) Token: 0x06000A7C RID: 2684 RVA: 0x0002C5F0 File Offset: 0x0002A7F0
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

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0002C620 File Offset: 0x0002A820
			// (set) Token: 0x06000A7E RID: 2686 RVA: 0x0002C628 File Offset: 0x0002A828
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

			// Token: 0x06000A7F RID: 2687 RVA: 0x0002C644 File Offset: 0x0002A844
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

			// Token: 0x06000A80 RID: 2688 RVA: 0x0002C744 File Offset: 0x0002A944
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

			// Token: 0x06000A81 RID: 2689 RVA: 0x0002C84C File Offset: 0x0002AA4C
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

			// Token: 0x06000A82 RID: 2690 RVA: 0x0002C880 File Offset: 0x0002AA80
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

			// Token: 0x06000A83 RID: 2691 RVA: 0x0002C8C0 File Offset: 0x0002AAC0
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

			// Token: 0x06000A84 RID: 2692 RVA: 0x0002CA60 File Offset: 0x0002AC60
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

			// Token: 0x06000A85 RID: 2693 RVA: 0x0002CB30 File Offset: 0x0002AD30
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

			// Token: 0x06000A86 RID: 2694 RVA: 0x0002CB80 File Offset: 0x0002AD80
			public static byte[] Encode(DSA dsa)
			{
				return ASN1Convert.FromUnsignedBigInteger(dsa.ExportParameters(true).X).GetBytes();
			}

			// Token: 0x06000A87 RID: 2695 RVA: 0x0002CBA8 File Offset: 0x0002ADA8
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

			// Token: 0x04000272 RID: 626
			private int _version;

			// Token: 0x04000273 RID: 627
			private string _algorithm;

			// Token: 0x04000274 RID: 628
			private byte[] _key;

			// Token: 0x04000275 RID: 629
			private ArrayList _list;
		}

		// Token: 0x020000BC RID: 188
		public class EncryptedPrivateKeyInfo
		{
			// Token: 0x06000A88 RID: 2696 RVA: 0x0002CBF4 File Offset: 0x0002ADF4
			public EncryptedPrivateKeyInfo()
			{
			}

			// Token: 0x06000A89 RID: 2697 RVA: 0x0002CBFC File Offset: 0x0002ADFC
			public EncryptedPrivateKeyInfo(byte[] data) : this()
			{
				this.Decode(data);
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0002CC0C File Offset: 0x0002AE0C
			// (set) Token: 0x06000A8B RID: 2699 RVA: 0x0002CC14 File Offset: 0x0002AE14
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

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0002CC20 File Offset: 0x0002AE20
			// (set) Token: 0x06000A8D RID: 2701 RVA: 0x0002CC44 File Offset: 0x0002AE44
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

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0002CC64 File Offset: 0x0002AE64
			// (set) Token: 0x06000A8F RID: 2703 RVA: 0x0002CCAC File Offset: 0x0002AEAC
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

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0002CCC0 File Offset: 0x0002AEC0
			// (set) Token: 0x06000A91 RID: 2705 RVA: 0x0002CCC8 File Offset: 0x0002AEC8
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

			// Token: 0x06000A92 RID: 2706 RVA: 0x0002CCE8 File Offset: 0x0002AEE8
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

			// Token: 0x06000A93 RID: 2707 RVA: 0x0002CE18 File Offset: 0x0002B018
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

			// Token: 0x04000276 RID: 630
			private string _algorithm;

			// Token: 0x04000277 RID: 631
			private byte[] _salt;

			// Token: 0x04000278 RID: 632
			private int _iterations;

			// Token: 0x04000279 RID: 633
			private byte[] _data;
		}
	}
}
