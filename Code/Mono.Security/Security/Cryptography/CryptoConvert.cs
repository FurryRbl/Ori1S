using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000024 RID: 36
	public sealed class CryptoConvert
	{
		// Token: 0x0600018A RID: 394 RVA: 0x0000A2DC File Offset: 0x000084DC
		private CryptoConvert()
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000A2E4 File Offset: 0x000084E4
		private static int ToInt32LE(byte[] bytes, int offset)
		{
			return (int)bytes[offset + 3] << 24 | (int)bytes[offset + 2] << 16 | (int)bytes[offset + 1] << 8 | (int)bytes[offset];
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000A304 File Offset: 0x00008504
		private static uint ToUInt32LE(byte[] bytes, int offset)
		{
			return (uint)((int)bytes[offset + 3] << 24 | (int)bytes[offset + 2] << 16 | (int)bytes[offset + 1] << 8 | (int)bytes[offset]);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000A324 File Offset: 0x00008524
		private static byte[] GetBytesLE(int val)
		{
			return new byte[]
			{
				(byte)(val & 255),
				(byte)(val >> 8 & 255),
				(byte)(val >> 16 & 255),
				(byte)(val >> 24 & 255)
			};
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000A36C File Offset: 0x0000856C
		private static byte[] Trim(byte[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != 0)
				{
					byte[] array2 = new byte[array.Length - i];
					Buffer.BlockCopy(array, i, array2, 0, array2.Length);
					return array2;
				}
			}
			return null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000A3B0 File Offset: 0x000085B0
		public static RSA FromCapiPrivateKeyBlob(byte[] blob)
		{
			return CryptoConvert.FromCapiPrivateKeyBlob(blob, 0);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000A3BC File Offset: 0x000085BC
		public static RSA FromCapiPrivateKeyBlob(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			RSAParameters parameters = default(RSAParameters);
			try
			{
				if (blob[offset] != 7 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || CryptoConvert.ToUInt32LE(blob, offset + 8) != 843141970U)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = CryptoConvert.ToInt32LE(blob, offset + 12);
				byte[] array = new byte[4];
				Buffer.BlockCopy(blob, offset + 16, array, 0, 4);
				Array.Reverse(array);
				parameters.Exponent = CryptoConvert.Trim(array);
				int num2 = offset + 20;
				int num3 = num >> 3;
				parameters.Modulus = new byte[num3];
				Buffer.BlockCopy(blob, num2, parameters.Modulus, 0, num3);
				Array.Reverse(parameters.Modulus);
				num2 += num3;
				int num4 = num3 >> 1;
				parameters.P = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.P, 0, num4);
				Array.Reverse(parameters.P);
				num2 += num4;
				parameters.Q = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.Q, 0, num4);
				Array.Reverse(parameters.Q);
				num2 += num4;
				parameters.DP = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.DP, 0, num4);
				Array.Reverse(parameters.DP);
				num2 += num4;
				parameters.DQ = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.DQ, 0, num4);
				Array.Reverse(parameters.DQ);
				num2 += num4;
				parameters.InverseQ = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.InverseQ, 0, num4);
				Array.Reverse(parameters.InverseQ);
				num2 += num4;
				parameters.D = new byte[num3];
				if (num2 + num3 + offset <= blob.Length)
				{
					Buffer.BlockCopy(blob, num2, parameters.D, 0, num3);
					Array.Reverse(parameters.D);
				}
			}
			catch (Exception inner)
			{
				throw new CryptographicException("Invalid blob.", inner);
			}
			RSA rsa = null;
			try
			{
				rsa = RSA.Create();
				rsa.ImportParameters(parameters);
			}
			catch (CryptographicException ex)
			{
				try
				{
					rsa = new RSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					rsa.ImportParameters(parameters);
				}
				catch
				{
					throw ex;
				}
			}
			return rsa;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000A68C File Offset: 0x0000888C
		public static DSA FromCapiPrivateKeyBlobDSA(byte[] blob)
		{
			return CryptoConvert.FromCapiPrivateKeyBlobDSA(blob, 0);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000A698 File Offset: 0x00008898
		public static DSA FromCapiPrivateKeyBlobDSA(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			DSAParameters parameters = default(DSAParameters);
			try
			{
				if (blob[offset] != 7 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || CryptoConvert.ToUInt32LE(blob, offset + 8) != 844321604U)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = CryptoConvert.ToInt32LE(blob, offset + 12);
				int num2 = num >> 3;
				int num3 = offset + 16;
				parameters.P = new byte[num2];
				Buffer.BlockCopy(blob, num3, parameters.P, 0, num2);
				Array.Reverse(parameters.P);
				num3 += num2;
				parameters.Q = new byte[20];
				Buffer.BlockCopy(blob, num3, parameters.Q, 0, 20);
				Array.Reverse(parameters.Q);
				num3 += 20;
				parameters.G = new byte[num2];
				Buffer.BlockCopy(blob, num3, parameters.G, 0, num2);
				Array.Reverse(parameters.G);
				num3 += num2;
				parameters.X = new byte[20];
				Buffer.BlockCopy(blob, num3, parameters.X, 0, 20);
				Array.Reverse(parameters.X);
				num3 += 20;
				parameters.Counter = CryptoConvert.ToInt32LE(blob, num3);
				num3 += 4;
				parameters.Seed = new byte[20];
				Buffer.BlockCopy(blob, num3, parameters.Seed, 0, 20);
				Array.Reverse(parameters.Seed);
				num3 += 20;
			}
			catch (Exception inner)
			{
				throw new CryptographicException("Invalid blob.", inner);
			}
			DSA dsa = null;
			try
			{
				dsa = DSA.Create();
				dsa.ImportParameters(parameters);
			}
			catch (CryptographicException ex)
			{
				try
				{
					dsa = new DSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					dsa.ImportParameters(parameters);
				}
				catch
				{
					throw ex;
				}
			}
			return dsa;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000A8DC File Offset: 0x00008ADC
		public static byte[] ToCapiPrivateKeyBlob(RSA rsa)
		{
			RSAParameters rsaparameters = rsa.ExportParameters(true);
			int num = rsaparameters.Modulus.Length;
			byte[] array = new byte[20 + (num << 2) + (num >> 1)];
			array[0] = 7;
			array[1] = 2;
			array[5] = 36;
			array[8] = 82;
			array[9] = 83;
			array[10] = 65;
			array[11] = 50;
			byte[] bytesLE = CryptoConvert.GetBytesLE(num << 3);
			array[12] = bytesLE[0];
			array[13] = bytesLE[1];
			array[14] = bytesLE[2];
			array[15] = bytesLE[3];
			int num2 = 16;
			int i = rsaparameters.Exponent.Length;
			while (i > 0)
			{
				array[num2++] = rsaparameters.Exponent[--i];
			}
			num2 = 20;
			byte[] array2 = rsaparameters.Modulus;
			int num3 = array2.Length;
			Array.Reverse(array2, 0, num3);
			Buffer.BlockCopy(array2, 0, array, num2, num3);
			num2 += num3;
			array2 = rsaparameters.P;
			num3 = array2.Length;
			Array.Reverse(array2, 0, num3);
			Buffer.BlockCopy(array2, 0, array, num2, num3);
			num2 += num3;
			array2 = rsaparameters.Q;
			num3 = array2.Length;
			Array.Reverse(array2, 0, num3);
			Buffer.BlockCopy(array2, 0, array, num2, num3);
			num2 += num3;
			array2 = rsaparameters.DP;
			num3 = array2.Length;
			Array.Reverse(array2, 0, num3);
			Buffer.BlockCopy(array2, 0, array, num2, num3);
			num2 += num3;
			array2 = rsaparameters.DQ;
			num3 = array2.Length;
			Array.Reverse(array2, 0, num3);
			Buffer.BlockCopy(array2, 0, array, num2, num3);
			num2 += num3;
			array2 = rsaparameters.InverseQ;
			num3 = array2.Length;
			Array.Reverse(array2, 0, num3);
			Buffer.BlockCopy(array2, 0, array, num2, num3);
			num2 += num3;
			array2 = rsaparameters.D;
			num3 = array2.Length;
			Array.Reverse(array2, 0, num3);
			Buffer.BlockCopy(array2, 0, array, num2, num3);
			return array;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		public static byte[] ToCapiPrivateKeyBlob(DSA dsa)
		{
			DSAParameters dsaparameters = dsa.ExportParameters(true);
			int num = dsaparameters.P.Length;
			byte[] array = new byte[16 + num + 20 + num + 20 + 4 + 20];
			array[0] = 7;
			array[1] = 2;
			array[5] = 34;
			array[8] = 68;
			array[9] = 83;
			array[10] = 83;
			array[11] = 50;
			byte[] bytesLE = CryptoConvert.GetBytesLE(num << 3);
			array[12] = bytesLE[0];
			array[13] = bytesLE[1];
			array[14] = bytesLE[2];
			array[15] = bytesLE[3];
			int num2 = 16;
			byte[] array2 = dsaparameters.P;
			Array.Reverse(array2);
			Buffer.BlockCopy(array2, 0, array, num2, num);
			num2 += num;
			array2 = dsaparameters.Q;
			Array.Reverse(array2);
			Buffer.BlockCopy(array2, 0, array, num2, 20);
			num2 += 20;
			array2 = dsaparameters.G;
			Array.Reverse(array2);
			Buffer.BlockCopy(array2, 0, array, num2, num);
			num2 += num;
			array2 = dsaparameters.X;
			Array.Reverse(array2);
			Buffer.BlockCopy(array2, 0, array, num2, 20);
			num2 += 20;
			Buffer.BlockCopy(CryptoConvert.GetBytesLE(dsaparameters.Counter), 0, array, num2, 4);
			num2 += 4;
			array2 = dsaparameters.Seed;
			Array.Reverse(array2);
			Buffer.BlockCopy(array2, 0, array, num2, 20);
			return array;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000AC10 File Offset: 0x00008E10
		public static RSA FromCapiPublicKeyBlob(byte[] blob)
		{
			return CryptoConvert.FromCapiPublicKeyBlob(blob, 0);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000AC1C File Offset: 0x00008E1C
		public static RSA FromCapiPublicKeyBlob(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			RSA result;
			try
			{
				if (blob[offset] != 6 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || CryptoConvert.ToUInt32LE(blob, offset + 8) != 826364754U)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = CryptoConvert.ToInt32LE(blob, offset + 12);
				RSAParameters parameters = default(RSAParameters);
				parameters.Exponent = new byte[3];
				parameters.Exponent[0] = blob[offset + 18];
				parameters.Exponent[1] = blob[offset + 17];
				parameters.Exponent[2] = blob[offset + 16];
				int srcOffset = offset + 20;
				int num2 = num >> 3;
				parameters.Modulus = new byte[num2];
				Buffer.BlockCopy(blob, srcOffset, parameters.Modulus, 0, num2);
				Array.Reverse(parameters.Modulus);
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
				result = rsa;
			}
			catch (Exception inner)
			{
				throw new CryptographicException("Invalid blob.", inner);
			}
			return result;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		public static DSA FromCapiPublicKeyBlobDSA(byte[] blob)
		{
			return CryptoConvert.FromCapiPublicKeyBlobDSA(blob, 0);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		public static DSA FromCapiPublicKeyBlobDSA(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			DSA result;
			try
			{
				if (blob[offset] != 6 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || CryptoConvert.ToUInt32LE(blob, offset + 8) != 827544388U)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = CryptoConvert.ToInt32LE(blob, offset + 12);
				DSAParameters parameters = default(DSAParameters);
				int num2 = num >> 3;
				int num3 = offset + 16;
				parameters.P = new byte[num2];
				Buffer.BlockCopy(blob, num3, parameters.P, 0, num2);
				Array.Reverse(parameters.P);
				num3 += num2;
				parameters.Q = new byte[20];
				Buffer.BlockCopy(blob, num3, parameters.Q, 0, 20);
				Array.Reverse(parameters.Q);
				num3 += 20;
				parameters.G = new byte[num2];
				Buffer.BlockCopy(blob, num3, parameters.G, 0, num2);
				Array.Reverse(parameters.G);
				num3 += num2;
				parameters.Y = new byte[num2];
				Buffer.BlockCopy(blob, num3, parameters.Y, 0, num2);
				Array.Reverse(parameters.Y);
				num3 += num2;
				parameters.Counter = CryptoConvert.ToInt32LE(blob, num3);
				num3 += 4;
				parameters.Seed = new byte[20];
				Buffer.BlockCopy(blob, num3, parameters.Seed, 0, 20);
				Array.Reverse(parameters.Seed);
				num3 += 20;
				DSA dsa = DSA.Create();
				dsa.ImportParameters(parameters);
				result = dsa;
			}
			catch (Exception inner)
			{
				throw new CryptographicException("Invalid blob.", inner);
			}
			return result;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000AF90 File Offset: 0x00009190
		public static byte[] ToCapiPublicKeyBlob(RSA rsa)
		{
			RSAParameters rsaparameters = rsa.ExportParameters(false);
			int num = rsaparameters.Modulus.Length;
			byte[] array = new byte[20 + num];
			array[0] = 6;
			array[1] = 2;
			array[5] = 36;
			array[8] = 82;
			array[9] = 83;
			array[10] = 65;
			array[11] = 49;
			byte[] bytesLE = CryptoConvert.GetBytesLE(num << 3);
			array[12] = bytesLE[0];
			array[13] = bytesLE[1];
			array[14] = bytesLE[2];
			array[15] = bytesLE[3];
			int num2 = 16;
			int i = rsaparameters.Exponent.Length;
			while (i > 0)
			{
				array[num2++] = rsaparameters.Exponent[--i];
			}
			num2 = 20;
			byte[] modulus = rsaparameters.Modulus;
			int num3 = modulus.Length;
			Array.Reverse(modulus, 0, num3);
			Buffer.BlockCopy(modulus, 0, array, num2, num3);
			num2 += num3;
			return array;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000B068 File Offset: 0x00009268
		public static byte[] ToCapiPublicKeyBlob(DSA dsa)
		{
			DSAParameters dsaparameters = dsa.ExportParameters(false);
			int num = dsaparameters.P.Length;
			byte[] array = new byte[16 + num + 20 + num + num + 4 + 20];
			array[0] = 6;
			array[1] = 2;
			array[5] = 34;
			array[8] = 68;
			array[9] = 83;
			array[10] = 83;
			array[11] = 49;
			byte[] bytesLE = CryptoConvert.GetBytesLE(num << 3);
			array[12] = bytesLE[0];
			array[13] = bytesLE[1];
			array[14] = bytesLE[2];
			array[15] = bytesLE[3];
			int num2 = 16;
			byte[] array2 = dsaparameters.P;
			Array.Reverse(array2);
			Buffer.BlockCopy(array2, 0, array, num2, num);
			num2 += num;
			array2 = dsaparameters.Q;
			Array.Reverse(array2);
			Buffer.BlockCopy(array2, 0, array, num2, 20);
			num2 += 20;
			array2 = dsaparameters.G;
			Array.Reverse(array2);
			Buffer.BlockCopy(array2, 0, array, num2, num);
			num2 += num;
			array2 = dsaparameters.Y;
			Array.Reverse(array2);
			Buffer.BlockCopy(array2, 0, array, num2, num);
			num2 += num;
			Buffer.BlockCopy(CryptoConvert.GetBytesLE(dsaparameters.Counter), 0, array, num2, 4);
			num2 += 4;
			array2 = dsaparameters.Seed;
			Array.Reverse(array2);
			Buffer.BlockCopy(array2, 0, array, num2, 20);
			return array;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000B1B0 File Offset: 0x000093B0
		public static RSA FromCapiKeyBlob(byte[] blob)
		{
			return CryptoConvert.FromCapiKeyBlob(blob, 0);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000B1BC File Offset: 0x000093BC
		public static RSA FromCapiKeyBlob(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			byte b = blob[offset];
			if (b == 6)
			{
				return CryptoConvert.FromCapiPublicKeyBlob(blob, offset);
			}
			if (b != 7)
			{
				if (b == 0)
				{
					if (blob[offset + 12] == 6)
					{
						return CryptoConvert.FromCapiPublicKeyBlob(blob, offset + 12);
					}
				}
				throw new CryptographicException("Unknown blob format.");
			}
			return CryptoConvert.FromCapiPrivateKeyBlob(blob, offset);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000B244 File Offset: 0x00009444
		public static DSA FromCapiKeyBlobDSA(byte[] blob)
		{
			return CryptoConvert.FromCapiKeyBlobDSA(blob, 0);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000B250 File Offset: 0x00009450
		public static DSA FromCapiKeyBlobDSA(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			byte b = blob[offset];
			if (b == 6)
			{
				return CryptoConvert.FromCapiPublicKeyBlobDSA(blob, offset);
			}
			if (b != 7)
			{
				throw new CryptographicException("Unknown blob format.");
			}
			return CryptoConvert.FromCapiPrivateKeyBlobDSA(blob, offset);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000B2B4 File Offset: 0x000094B4
		public static byte[] ToCapiKeyBlob(AsymmetricAlgorithm keypair, bool includePrivateKey)
		{
			if (keypair == null)
			{
				throw new ArgumentNullException("keypair");
			}
			if (keypair is RSA)
			{
				return CryptoConvert.ToCapiKeyBlob((RSA)keypair, includePrivateKey);
			}
			if (keypair is DSA)
			{
				return CryptoConvert.ToCapiKeyBlob((DSA)keypair, includePrivateKey);
			}
			return null;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000B304 File Offset: 0x00009504
		public static byte[] ToCapiKeyBlob(RSA rsa, bool includePrivateKey)
		{
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			if (includePrivateKey)
			{
				return CryptoConvert.ToCapiPrivateKeyBlob(rsa);
			}
			return CryptoConvert.ToCapiPublicKeyBlob(rsa);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000B338 File Offset: 0x00009538
		public static byte[] ToCapiKeyBlob(DSA dsa, bool includePrivateKey)
		{
			if (dsa == null)
			{
				throw new ArgumentNullException("dsa");
			}
			if (includePrivateKey)
			{
				return CryptoConvert.ToCapiPrivateKeyBlob(dsa);
			}
			return CryptoConvert.ToCapiPublicKeyBlob(dsa);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000B36C File Offset: 0x0000956C
		public static string ToHex(byte[] input)
		{
			if (input == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(input.Length * 2);
			foreach (byte b in input)
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000B3C4 File Offset: 0x000095C4
		private static byte FromHexChar(char c)
		{
			if (c >= 'a' && c <= 'f')
			{
				return (byte)(c - 'a' + '\n');
			}
			if (c >= 'A' && c <= 'F')
			{
				return (byte)(c - 'A' + '\n');
			}
			if (c >= '0' && c <= '9')
			{
				return (byte)(c - '0');
			}
			throw new ArgumentException("invalid hex char");
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000B424 File Offset: 0x00009624
		public static byte[] FromHex(string hex)
		{
			if (hex == null)
			{
				return null;
			}
			if ((hex.Length & 1) == 1)
			{
				throw new ArgumentException("Length must be a multiple of 2");
			}
			byte[] array = new byte[hex.Length >> 1];
			int i = 0;
			int num = 0;
			while (i < array.Length)
			{
				array[i] = (byte)(CryptoConvert.FromHexChar(hex[num++]) << 4);
				byte[] array2 = array;
				int num2 = i++;
				array2[num2] += CryptoConvert.FromHexChar(hex[num++]);
			}
			return array;
		}
	}
}
