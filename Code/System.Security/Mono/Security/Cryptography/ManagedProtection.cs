using System;
using System.IO;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000004 RID: 4
	internal static class ManagedProtection
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002104 File Offset: 0x00000304
		public static byte[] Protect(byte[] userData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			Rijndael rijndael = Rijndael.Create();
			rijndael.KeySize = 128;
			byte[] array = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				ICryptoTransform transform = rijndael.CreateEncryptor();
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
				{
					cryptoStream.Write(userData, 0, userData.Length);
					cryptoStream.Close();
					array = memoryStream.ToArray();
				}
			}
			byte[] array2 = null;
			byte[] array3 = null;
			byte[] array4 = null;
			byte[] array5 = null;
			SHA256 sha = SHA256.Create();
			try
			{
				array2 = rijndael.Key;
				array3 = rijndael.IV;
				array4 = new byte[68];
				byte[] src = sha.ComputeHash(userData);
				if (optionalEntropy != null && optionalEntropy.Length > 0)
				{
					byte[] array6 = sha.ComputeHash(optionalEntropy);
					for (int i = 0; i < 16; i++)
					{
						byte[] array7 = array2;
						int num = i;
						array7[num] ^= array6[i];
						byte[] array8 = array3;
						int num2 = i;
						array8[num2] ^= array6[i + 16];
					}
					array4[0] = 2;
				}
				else
				{
					array4[0] = 1;
				}
				array4[1] = 16;
				Buffer.BlockCopy(array2, 0, array4, 2, 16);
				array4[18] = 16;
				Buffer.BlockCopy(array3, 0, array4, 19, 16);
				array4[35] = 32;
				Buffer.BlockCopy(src, 0, array4, 36, 32);
				RSAOAEPKeyExchangeFormatter rsaoaepkeyExchangeFormatter = new RSAOAEPKeyExchangeFormatter(ManagedProtection.GetKey(scope));
				array5 = rsaoaepkeyExchangeFormatter.CreateKeyExchange(array4);
			}
			finally
			{
				if (array2 != null)
				{
					Array.Clear(array2, 0, array2.Length);
					array2 = null;
				}
				if (array4 != null)
				{
					Array.Clear(array4, 0, array4.Length);
					array4 = null;
				}
				if (array3 != null)
				{
					Array.Clear(array3, 0, array3.Length);
					array3 = null;
				}
				rijndael.Clear();
				sha.Clear();
			}
			byte[] array9 = new byte[array5.Length + array.Length];
			Buffer.BlockCopy(array5, 0, array9, 0, array5.Length);
			Buffer.BlockCopy(array, 0, array9, array5.Length, array.Length);
			return array9;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002350 File Offset: 0x00000550
		public static byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			byte[] array = null;
			Rijndael rijndael = Rijndael.Create();
			RSA key = ManagedProtection.GetKey(scope);
			int num = key.KeySize >> 3;
			bool flag = encryptedData.Length >= num;
			if (!flag)
			{
				num = encryptedData.Length;
			}
			byte[] array2 = new byte[num];
			Buffer.BlockCopy(encryptedData, 0, array2, 0, num);
			byte[] array3 = null;
			byte[] array4 = null;
			byte[] array5 = null;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			SHA256 sha = SHA256.Create();
			try
			{
				try
				{
					RSAOAEPKeyExchangeDeformatter rsaoaepkeyExchangeDeformatter = new RSAOAEPKeyExchangeDeformatter(key);
					array3 = rsaoaepkeyExchangeDeformatter.DecryptKeyExchange(array2);
					flag2 = (array3.Length == 68);
				}
				catch
				{
					flag2 = false;
				}
				if (!flag2)
				{
					array3 = new byte[68];
				}
				flag3 = (array3[1] == 16 && array3[18] == 16 && array3[35] == 32);
				array4 = new byte[16];
				Buffer.BlockCopy(array3, 2, array4, 0, 16);
				array5 = new byte[16];
				Buffer.BlockCopy(array3, 19, array5, 0, 16);
				if (optionalEntropy != null && optionalEntropy.Length > 0)
				{
					byte[] array6 = sha.ComputeHash(optionalEntropy);
					for (int i = 0; i < 16; i++)
					{
						byte[] array7 = array4;
						int num2 = i;
						array7[num2] ^= array6[i];
						byte[] array8 = array5;
						int num3 = i;
						array8[num3] ^= array6[i + 16];
					}
					flag3 &= (array3[0] == 2);
				}
				else
				{
					flag3 &= (array3[0] == 1);
				}
				using (MemoryStream memoryStream = new MemoryStream())
				{
					ICryptoTransform transform = rijndael.CreateDecryptor(array4, array5);
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
					{
						try
						{
							cryptoStream.Write(encryptedData, num, encryptedData.Length - num);
							cryptoStream.Close();
						}
						catch
						{
						}
					}
					array = memoryStream.ToArray();
				}
				byte[] array9 = sha.ComputeHash(array);
				flag4 = true;
				for (int j = 0; j < 32; j++)
				{
					if (array9[j] != array3[36 + j])
					{
						flag4 = false;
					}
				}
			}
			finally
			{
				if (array4 != null)
				{
					Array.Clear(array4, 0, array4.Length);
					array4 = null;
				}
				if (array3 != null)
				{
					Array.Clear(array3, 0, array3.Length);
					array3 = null;
				}
				if (array5 != null)
				{
					Array.Clear(array5, 0, array5.Length);
					array5 = null;
				}
				rijndael.Clear();
				sha.Clear();
			}
			if (!flag || !flag2 || !flag3 || !flag4)
			{
				if (array != null)
				{
					Array.Clear(array, 0, array.Length);
					array = null;
				}
				throw new CryptographicException(Locale.GetText("Invalid data."));
			}
			return array;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002684 File Offset: 0x00000884
		private static RSA GetKey(DataProtectionScope scope)
		{
			if (scope == DataProtectionScope.CurrentUser)
			{
				if (ManagedProtection.user == null)
				{
					ManagedProtection.user = new RSACryptoServiceProvider(1536, new CspParameters
					{
						KeyContainerName = "DAPI"
					});
				}
				return ManagedProtection.user;
			}
			if (scope != DataProtectionScope.LocalMachine)
			{
				throw new CryptographicException(Locale.GetText("Invalid scope."));
			}
			if (ManagedProtection.machine == null)
			{
				ManagedProtection.machine = new RSACryptoServiceProvider(1536, new CspParameters
				{
					KeyContainerName = "DAPI",
					Flags = CspProviderFlags.UseMachineKeyStore
				});
			}
			return ManagedProtection.machine;
		}

		// Token: 0x0400001E RID: 30
		private static RSA user;

		// Token: 0x0400001F RID: 31
		private static RSA machine;
	}
}
