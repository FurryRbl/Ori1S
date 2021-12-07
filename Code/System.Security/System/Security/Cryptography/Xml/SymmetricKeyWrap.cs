using System;
using System.IO;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000051 RID: 81
	internal class SymmetricKeyWrap
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0000C734 File Offset: 0x0000A934
		public static byte[] AESKeyWrapEncrypt(byte[] rgbKey, byte[] rgbWrappedKeyData)
		{
			SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("Rijndael");
			symmetricAlgorithm.Mode = CipherMode.ECB;
			symmetricAlgorithm.IV = new byte[16];
			ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateEncryptor(rgbKey, symmetricAlgorithm.IV);
			int num = rgbWrappedKeyData.Length / 8;
			byte[] array = new byte[16];
			byte[] array2 = new byte[8 * (num + 1)];
			if (num == 1)
			{
				byte[] array3 = new byte[]
				{
					166,
					166,
					166,
					166,
					166,
					166,
					166,
					166
				};
				cryptoTransform.TransformBlock(SymmetricKeyWrap.Concatenate(array3, rgbWrappedKeyData), 0, 16, array, 0);
				Buffer.BlockCopy(SymmetricKeyWrap.MSB(array), 0, array2, 0, 8);
				Buffer.BlockCopy(SymmetricKeyWrap.LSB(array), 0, array2, 8, 8);
			}
			else
			{
				byte[] array3 = new byte[]
				{
					166,
					166,
					166,
					166,
					166,
					166,
					166,
					166
				};
				byte[][] array4 = new byte[num + 1][];
				for (int i = 1; i <= num; i++)
				{
					array4[i] = new byte[8];
					Buffer.BlockCopy(rgbWrappedKeyData, 8 * (i - 1), array4[i], 0, 8);
				}
				for (int j = 0; j <= 5; j++)
				{
					for (int k = 1; k <= num; k++)
					{
						cryptoTransform.TransformBlock(SymmetricKeyWrap.Concatenate(array3, array4[k]), 0, 16, array, 0);
						byte[] bytes = BitConverter.GetBytes((long)(num * j + k));
						if (BitConverter.IsLittleEndian)
						{
							Array.Reverse(bytes);
						}
						array3 = SymmetricKeyWrap.Xor(bytes, SymmetricKeyWrap.MSB(array));
						array4[k] = SymmetricKeyWrap.LSB(array);
					}
				}
				Buffer.BlockCopy(array3, 0, array2, 0, 8);
				for (int l = 1; l <= num; l++)
				{
					Buffer.BlockCopy(array4[l], 0, array2, 8 * l, 8);
				}
			}
			return array2;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		public static byte[] AESKeyWrapDecrypt(byte[] rgbKey, byte[] rgbEncryptedWrappedKeyData)
		{
			SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("Rijndael");
			symmetricAlgorithm.Mode = CipherMode.ECB;
			symmetricAlgorithm.Key = rgbKey;
			int num = rgbEncryptedWrappedKeyData.Length / 8 - 1;
			byte[] array = new byte[8];
			Buffer.BlockCopy(rgbEncryptedWrappedKeyData, 0, array, 0, 8);
			byte[] array2 = new byte[num * 8];
			Buffer.BlockCopy(rgbEncryptedWrappedKeyData, 8, array2, 0, rgbEncryptedWrappedKeyData.Length - 8);
			ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateDecryptor();
			for (int i = 5; i >= 0; i--)
			{
				for (int j = num; j >= 1; j--)
				{
					byte[] bytes = BitConverter.GetBytes((long)num * (long)i + (long)j);
					if (BitConverter.IsLittleEndian)
					{
						Array.Reverse(bytes);
					}
					byte[] array3 = new byte[16];
					byte[] array4 = new byte[8];
					Buffer.BlockCopy(array2, 8 * (j - 1), array4, 0, 8);
					byte[] inputBuffer = SymmetricKeyWrap.Concatenate(SymmetricKeyWrap.Xor(array, bytes), array4);
					cryptoTransform.TransformBlock(inputBuffer, 0, 16, array3, 0);
					array = SymmetricKeyWrap.MSB(array3);
					Buffer.BlockCopy(SymmetricKeyWrap.LSB(array3), 0, array2, 8 * (j - 1), 8);
				}
			}
			return array2;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		public static byte[] TripleDESKeyWrapEncrypt(byte[] rgbKey, byte[] rgbWrappedKeyData)
		{
			SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("TripleDES");
			byte[] buf = SymmetricKeyWrap.ComputeCMSKeyChecksum(rgbWrappedKeyData);
			byte[] data = SymmetricKeyWrap.Concatenate(rgbWrappedKeyData, buf);
			symmetricAlgorithm.GenerateIV();
			symmetricAlgorithm.Mode = CipherMode.CBC;
			symmetricAlgorithm.Padding = PaddingMode.None;
			symmetricAlgorithm.Key = rgbKey;
			byte[] buf2 = SymmetricKeyWrap.Transform(data, symmetricAlgorithm.CreateEncryptor());
			byte[] array = SymmetricKeyWrap.Concatenate(symmetricAlgorithm.IV, buf2);
			Array.Reverse(array);
			symmetricAlgorithm.IV = new byte[]
			{
				74,
				221,
				162,
				44,
				121,
				232,
				33,
				5
			};
			return SymmetricKeyWrap.Transform(array, symmetricAlgorithm.CreateEncryptor());
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		public static byte[] TripleDESKeyWrapDecrypt(byte[] rgbKey, byte[] rgbEncryptedWrappedKeyData)
		{
			SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("TripleDES");
			symmetricAlgorithm.Mode = CipherMode.CBC;
			symmetricAlgorithm.Padding = PaddingMode.None;
			symmetricAlgorithm.Key = rgbKey;
			symmetricAlgorithm.IV = new byte[]
			{
				74,
				221,
				162,
				44,
				121,
				232,
				33,
				5
			};
			byte[] array = SymmetricKeyWrap.Transform(rgbEncryptedWrappedKeyData, symmetricAlgorithm.CreateDecryptor());
			Array.Reverse(array);
			byte[] array2 = new byte[array.Length - 8];
			byte[] array3 = new byte[8];
			Buffer.BlockCopy(array, 0, array3, 0, 8);
			Buffer.BlockCopy(array, 8, array2, 0, array2.Length);
			symmetricAlgorithm.IV = array3;
			byte[] array4 = SymmetricKeyWrap.Transform(array2, symmetricAlgorithm.CreateDecryptor());
			byte[] dst = new byte[8];
			byte[] array5 = new byte[array4.Length - 8];
			Buffer.BlockCopy(array4, 0, array5, 0, array5.Length);
			Buffer.BlockCopy(array4, array5.Length, dst, 0, 8);
			return array5;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000CB48 File Offset: 0x0000AD48
		private static byte[] Transform(byte[] data, ICryptoTransform t)
		{
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, t, CryptoStreamMode.Write);
			cryptoStream.Write(data, 0, data.Length);
			cryptoStream.FlushFinalBlock();
			byte[] result = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			return result;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		private static byte[] ComputeCMSKeyChecksum(byte[] data)
		{
			byte[] src = HashAlgorithm.Create("SHA1").ComputeHash(data);
			byte[] array = new byte[8];
			Buffer.BlockCopy(src, 0, array, 0, 8);
			return array;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		private static byte[] Concatenate(byte[] buf1, byte[] buf2)
		{
			byte[] array = new byte[buf1.Length + buf2.Length];
			Buffer.BlockCopy(buf1, 0, array, 0, buf1.Length);
			Buffer.BlockCopy(buf2, 0, array, buf1.Length, buf2.Length);
			return array;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000CBF4 File Offset: 0x0000ADF4
		private static byte[] MSB(byte[] input)
		{
			return SymmetricKeyWrap.MSB(input, 8);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000CC00 File Offset: 0x0000AE00
		private static byte[] MSB(byte[] input, int bytes)
		{
			byte[] array = new byte[bytes];
			Buffer.BlockCopy(input, 0, array, 0, bytes);
			return array;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000CC20 File Offset: 0x0000AE20
		private static byte[] LSB(byte[] input)
		{
			return SymmetricKeyWrap.LSB(input, 8);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000CC2C File Offset: 0x0000AE2C
		private static byte[] LSB(byte[] input, int bytes)
		{
			byte[] array = new byte[bytes];
			Buffer.BlockCopy(input, bytes, array, 0, bytes);
			return array;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000CC4C File Offset: 0x0000AE4C
		private static byte[] Xor(byte[] x, byte[] y)
		{
			if (x.Length != y.Length)
			{
				throw new CryptographicException("Error performing Xor: arrays different length.");
			}
			byte[] array = new byte[x.Length];
			for (int i = 0; i < x.Length; i++)
			{
				array[i] = (x[i] ^ y[i]);
			}
			return array;
		}
	}
}
