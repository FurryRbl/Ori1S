using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Mono.Security.Cryptography
{
	// Token: 0x020000B3 RID: 179
	internal sealed class CryptoConvert
	{
		// Token: 0x06000A08 RID: 2568 RVA: 0x0002944C File Offset: 0x0002764C
		private CryptoConvert()
		{
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00029454 File Offset: 0x00027654
		private static int ToInt32LE(byte[] bytes, int offset)
		{
			return (int)bytes[offset + 3] << 24 | (int)bytes[offset + 2] << 16 | (int)bytes[offset + 1] << 8 | (int)bytes[offset];
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00029474 File Offset: 0x00027674
		private static uint ToUInt32LE(byte[] bytes, int offset)
		{
			return (uint)((int)bytes[offset + 3] << 24 | (int)bytes[offset + 2] << 16 | (int)bytes[offset + 1] << 8 | (int)bytes[offset]);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00029494 File Offset: 0x00027694
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

		// Token: 0x06000A0C RID: 2572 RVA: 0x000294DC File Offset: 0x000276DC
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

		// Token: 0x06000A0D RID: 2573 RVA: 0x00029520 File Offset: 0x00027720
		public static RSA FromCapiPrivateKeyBlob(byte[] blob)
		{
			return CryptoConvert.FromCapiPrivateKeyBlob(blob, 0);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002952C File Offset: 0x0002772C
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

		// Token: 0x06000A0F RID: 2575 RVA: 0x000297FC File Offset: 0x000279FC
		public static DSA FromCapiPrivateKeyBlobDSA(byte[] blob)
		{
			return CryptoConvert.FromCapiPrivateKeyBlobDSA(blob, 0);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00029808 File Offset: 0x00027A08
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

		// Token: 0x06000A11 RID: 2577 RVA: 0x00029A4C File Offset: 0x00027C4C
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

		// Token: 0x06000A12 RID: 2578 RVA: 0x00029C34 File Offset: 0x00027E34
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

		// Token: 0x06000A13 RID: 2579 RVA: 0x00029D80 File Offset: 0x00027F80
		public static RSA FromCapiPublicKeyBlob(byte[] blob)
		{
			return CryptoConvert.FromCapiPublicKeyBlob(blob, 0);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00029D8C File Offset: 0x00027F8C
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

		// Token: 0x06000A15 RID: 2581 RVA: 0x00029F18 File Offset: 0x00028118
		public static DSA FromCapiPublicKeyBlobDSA(byte[] blob)
		{
			return CryptoConvert.FromCapiPublicKeyBlobDSA(blob, 0);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00029F24 File Offset: 0x00028124
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

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002A100 File Offset: 0x00028300
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

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002A1D8 File Offset: 0x000283D8
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

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002A320 File Offset: 0x00028520
		public static RSA FromCapiKeyBlob(byte[] blob)
		{
			return CryptoConvert.FromCapiKeyBlob(blob, 0);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002A32C File Offset: 0x0002852C
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

		// Token: 0x06000A1B RID: 2587 RVA: 0x0002A3B4 File Offset: 0x000285B4
		public static DSA FromCapiKeyBlobDSA(byte[] blob)
		{
			return CryptoConvert.FromCapiKeyBlobDSA(blob, 0);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002A3C0 File Offset: 0x000285C0
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

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002A424 File Offset: 0x00028624
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

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002A474 File Offset: 0x00028674
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

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002A4A8 File Offset: 0x000286A8
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

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002A4DC File Offset: 0x000286DC
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

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002A534 File Offset: 0x00028734
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

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002A594 File Offset: 0x00028794
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
