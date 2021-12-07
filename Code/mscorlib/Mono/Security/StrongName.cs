using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security
{
	// Token: 0x020000A8 RID: 168
	internal sealed class StrongName
	{
		// Token: 0x06000984 RID: 2436 RVA: 0x00024D60 File Offset: 0x00022F60
		public StrongName()
		{
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00024D68 File Offset: 0x00022F68
		public StrongName(int keySize)
		{
			this.rsa = new RSAManaged(keySize);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00024D7C File Offset: 0x00022F7C
		public StrongName(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length == 16)
			{
				int i = 0;
				int num = 0;
				while (i < data.Length)
				{
					num += (int)data[i++];
				}
				if (num == 4)
				{
					this.publicKey = (byte[])data.Clone();
				}
			}
			else
			{
				this.RSA = CryptoConvert.FromCapiKeyBlob(data);
				if (this.rsa == null)
				{
					throw new ArgumentException("data isn't a correctly encoded RSA public key");
				}
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00024E08 File Offset: 0x00023008
		public StrongName(RSA rsa)
		{
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			this.RSA = rsa;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00024E3C File Offset: 0x0002303C
		private void InvalidateCache()
		{
			this.publicKey = null;
			this.keyToken = null;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00024E4C File Offset: 0x0002304C
		public bool CanSign
		{
			get
			{
				if (this.rsa == null)
				{
					return false;
				}
				if (this.RSA is RSACryptoServiceProvider)
				{
					return !(this.rsa as RSACryptoServiceProvider).PublicOnly;
				}
				if (this.RSA is RSAManaged)
				{
					return !(this.rsa as RSAManaged).PublicOnly;
				}
				bool result;
				try
				{
					RSAParameters rsaparameters = this.rsa.ExportParameters(true);
					result = (rsaparameters.D != null && rsaparameters.P != null && rsaparameters.Q != null);
				}
				catch (CryptographicException)
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00024F18 File Offset: 0x00023118
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00024F38 File Offset: 0x00023138
		public RSA RSA
		{
			get
			{
				if (this.rsa == null)
				{
					this.rsa = RSA.Create();
				}
				return this.rsa;
			}
			set
			{
				this.rsa = value;
				this.InvalidateCache();
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00024F48 File Offset: 0x00023148
		public byte[] PublicKey
		{
			get
			{
				if (this.publicKey == null)
				{
					byte[] array = CryptoConvert.ToCapiKeyBlob(this.rsa, false);
					this.publicKey = new byte[32 + (this.rsa.KeySize >> 3)];
					this.publicKey[0] = array[4];
					this.publicKey[1] = array[5];
					this.publicKey[2] = array[6];
					this.publicKey[3] = array[7];
					this.publicKey[4] = 4;
					this.publicKey[5] = 128;
					this.publicKey[6] = 0;
					this.publicKey[7] = 0;
					byte[] bytes = BitConverterLE.GetBytes(this.publicKey.Length - 12);
					this.publicKey[8] = bytes[0];
					this.publicKey[9] = bytes[1];
					this.publicKey[10] = bytes[2];
					this.publicKey[11] = bytes[3];
					this.publicKey[12] = 6;
					Buffer.BlockCopy(array, 1, this.publicKey, 13, this.publicKey.Length - 13);
					this.publicKey[23] = 49;
				}
				return (byte[])this.publicKey.Clone();
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0002505C File Offset: 0x0002325C
		public byte[] PublicKeyToken
		{
			get
			{
				if (this.keyToken == null)
				{
					byte[] array = this.PublicKey;
					if (array == null)
					{
						return null;
					}
					HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.TokenAlgorithm);
					byte[] array2 = hashAlgorithm.ComputeHash(array);
					this.keyToken = new byte[8];
					Buffer.BlockCopy(array2, array2.Length - 8, this.keyToken, 0, 8);
					Array.Reverse(this.keyToken, 0, 8);
				}
				return (byte[])this.keyToken.Clone();
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x000250D4 File Offset: 0x000232D4
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x000250F4 File Offset: 0x000232F4
		public string TokenAlgorithm
		{
			get
			{
				if (this.tokenAlgorithm == null)
				{
					this.tokenAlgorithm = "SHA1";
				}
				return this.tokenAlgorithm;
			}
			set
			{
				string a = value.ToUpper(CultureInfo.InvariantCulture);
				if (a == "SHA1" || a == "MD5")
				{
					this.tokenAlgorithm = value;
					this.InvalidateCache();
					return;
				}
				throw new ArgumentException("Unsupported hash algorithm for token");
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002514C File Offset: 0x0002334C
		public byte[] GetBytes()
		{
			return CryptoConvert.ToCapiPrivateKeyBlob(this.RSA);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002515C File Offset: 0x0002335C
		private uint RVAtoPosition(uint r, int sections, byte[] headers)
		{
			for (int i = 0; i < sections; i++)
			{
				uint num = BitConverterLE.ToUInt32(headers, i * 40 + 20);
				uint num2 = BitConverterLE.ToUInt32(headers, i * 40 + 12);
				int num3 = (int)BitConverterLE.ToUInt32(headers, i * 40 + 8);
				if (num2 <= r && (ulong)r < (ulong)num2 + (ulong)((long)num3))
				{
					return num + r - num2;
				}
			}
			return 0U;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000251C0 File Offset: 0x000233C0
		internal StrongName.StrongNameSignature StrongHash(Stream stream, StrongName.StrongNameOptions options)
		{
			StrongName.StrongNameSignature strongNameSignature = new StrongName.StrongNameSignature();
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.TokenAlgorithm);
			CryptoStream cryptoStream = new CryptoStream(Stream.Null, hashAlgorithm, CryptoStreamMode.Write);
			byte[] array = new byte[128];
			stream.Read(array, 0, 128);
			if (BitConverterLE.ToUInt16(array, 0) != 23117)
			{
				return null;
			}
			uint num = BitConverterLE.ToUInt32(array, 60);
			cryptoStream.Write(array, 0, 128);
			if (num != 128U)
			{
				byte[] array2 = new byte[num - 128U];
				stream.Read(array2, 0, array2.Length);
				cryptoStream.Write(array2, 0, array2.Length);
			}
			byte[] array3 = new byte[248];
			stream.Read(array3, 0, 248);
			if (BitConverterLE.ToUInt32(array3, 0) != 17744U)
			{
				return null;
			}
			if (BitConverterLE.ToUInt16(array3, 4) != 332)
			{
				return null;
			}
			byte[] src = new byte[8];
			Buffer.BlockCopy(src, 0, array3, 88, 4);
			Buffer.BlockCopy(src, 0, array3, 152, 8);
			cryptoStream.Write(array3, 0, 248);
			ushort num2 = BitConverterLE.ToUInt16(array3, 6);
			int num3 = (int)(num2 * 40);
			byte[] array4 = new byte[num3];
			stream.Read(array4, 0, num3);
			cryptoStream.Write(array4, 0, num3);
			uint r = BitConverterLE.ToUInt32(array3, 232);
			uint num4 = this.RVAtoPosition(r, (int)num2, array4);
			int num5 = (int)BitConverterLE.ToUInt32(array3, 236);
			byte[] array5 = new byte[num5];
			stream.Position = (long)((ulong)num4);
			stream.Read(array5, 0, num5);
			uint r2 = BitConverterLE.ToUInt32(array5, 32);
			strongNameSignature.SignaturePosition = this.RVAtoPosition(r2, (int)num2, array4);
			strongNameSignature.SignatureLength = BitConverterLE.ToUInt32(array5, 36);
			uint r3 = BitConverterLE.ToUInt32(array5, 8);
			strongNameSignature.MetadataPosition = this.RVAtoPosition(r3, (int)num2, array4);
			strongNameSignature.MetadataLength = BitConverterLE.ToUInt32(array5, 12);
			if (options == StrongName.StrongNameOptions.Metadata)
			{
				cryptoStream.Close();
				hashAlgorithm.Initialize();
				byte[] array6 = new byte[strongNameSignature.MetadataLength];
				stream.Position = (long)((ulong)strongNameSignature.MetadataPosition);
				stream.Read(array6, 0, array6.Length);
				strongNameSignature.Hash = hashAlgorithm.ComputeHash(array6);
				return strongNameSignature;
			}
			for (int i = 0; i < (int)num2; i++)
			{
				uint num6 = BitConverterLE.ToUInt32(array4, i * 40 + 20);
				int num7 = (int)BitConverterLE.ToUInt32(array4, i * 40 + 16);
				byte[] array7 = new byte[num7];
				stream.Position = (long)((ulong)num6);
				stream.Read(array7, 0, num7);
				if (num6 <= strongNameSignature.SignaturePosition && (ulong)strongNameSignature.SignaturePosition < (ulong)num6 + (ulong)((long)num7))
				{
					int num8 = (int)(strongNameSignature.SignaturePosition - num6);
					if (num8 > 0)
					{
						cryptoStream.Write(array7, 0, num8);
					}
					strongNameSignature.Signature = new byte[strongNameSignature.SignatureLength];
					Buffer.BlockCopy(array7, num8, strongNameSignature.Signature, 0, (int)strongNameSignature.SignatureLength);
					Array.Reverse(strongNameSignature.Signature);
					int num9 = (int)((long)num8 + (long)((ulong)strongNameSignature.SignatureLength));
					int num10 = num7 - num9;
					if (num10 > 0)
					{
						cryptoStream.Write(array7, num9, num10);
					}
				}
				else
				{
					cryptoStream.Write(array7, 0, num7);
				}
			}
			cryptoStream.Close();
			strongNameSignature.Hash = hashAlgorithm.Hash;
			return strongNameSignature;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002550C File Offset: 0x0002370C
		public byte[] Hash(string fileName)
		{
			FileStream fileStream = File.OpenRead(fileName);
			StrongName.StrongNameSignature strongNameSignature = this.StrongHash(fileStream, StrongName.StrongNameOptions.Metadata);
			fileStream.Close();
			return strongNameSignature.Hash;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00025538 File Offset: 0x00023738
		public bool Sign(string fileName)
		{
			bool result = false;
			StrongName.StrongNameSignature strongNameSignature;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				strongNameSignature = this.StrongHash(fileStream, StrongName.StrongNameOptions.Signature);
				fileStream.Close();
			}
			if (strongNameSignature.Hash == null)
			{
				return false;
			}
			byte[] array = null;
			try
			{
				RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(this.rsa);
				rsapkcs1SignatureFormatter.SetHashAlgorithm(this.TokenAlgorithm);
				array = rsapkcs1SignatureFormatter.CreateSignature(strongNameSignature.Hash);
				Array.Reverse(array);
			}
			catch (CryptographicException)
			{
				return false;
			}
			using (FileStream fileStream2 = File.OpenWrite(fileName))
			{
				fileStream2.Position = (long)((ulong)strongNameSignature.SignaturePosition);
				fileStream2.Write(array, 0, array.Length);
				fileStream2.Close();
				result = true;
			}
			return result;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00025650 File Offset: 0x00023850
		public bool Verify(string fileName)
		{
			bool result = false;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				result = this.Verify(fileStream);
				fileStream.Close();
			}
			return result;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x000256A4 File Offset: 0x000238A4
		public bool Verify(Stream stream)
		{
			StrongName.StrongNameSignature strongNameSignature = this.StrongHash(stream, StrongName.StrongNameOptions.Signature);
			if (strongNameSignature.Hash == null)
			{
				return false;
			}
			bool result;
			try
			{
				AssemblyHashAlgorithm algorithm = AssemblyHashAlgorithm.SHA1;
				if (this.tokenAlgorithm == "MD5")
				{
					algorithm = AssemblyHashAlgorithm.MD5;
				}
				result = StrongName.Verify(this.rsa, algorithm, strongNameSignature.Hash, strongNameSignature.Signature);
			}
			catch (CryptographicException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00025738 File Offset: 0x00023938
		public static bool IsAssemblyStrongnamed(string assemblyName)
		{
			if (!StrongName.initialized)
			{
				object obj = StrongName.lockObject;
				lock (obj)
				{
					if (!StrongName.initialized)
					{
						string machineConfigPath = Environment.GetMachineConfigPath();
						StrongNameManager.LoadConfig(machineConfigPath);
						StrongName.initialized = true;
					}
				}
			}
			bool result;
			try
			{
				AssemblyName assemblyName2 = AssemblyName.GetAssemblyName(assemblyName);
				if (assemblyName2 == null)
				{
					result = false;
				}
				else
				{
					byte[] mappedPublicKey = StrongNameManager.GetMappedPublicKey(assemblyName2.GetPublicKeyToken());
					if (mappedPublicKey == null || mappedPublicKey.Length < 12)
					{
						mappedPublicKey = assemblyName2.GetPublicKey();
						if (mappedPublicKey == null || mappedPublicKey.Length < 12)
						{
							return false;
						}
					}
					if (!StrongNameManager.MustVerify(assemblyName2))
					{
						result = true;
					}
					else
					{
						RSA rsa = CryptoConvert.FromCapiPublicKeyBlob(mappedPublicKey, 12);
						StrongName strongName = new StrongName(rsa);
						bool flag = strongName.Verify(assemblyName);
						result = flag;
					}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00025858 File Offset: 0x00023A58
		public static bool VerifySignature(byte[] publicKey, int algorithm, byte[] hash, byte[] signature)
		{
			bool result;
			try
			{
				RSA rsa = CryptoConvert.FromCapiPublicKeyBlob(publicKey);
				result = StrongName.Verify(rsa, (AssemblyHashAlgorithm)algorithm, hash, signature);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000258AC File Offset: 0x00023AAC
		private static bool Verify(RSA rsa, AssemblyHashAlgorithm algorithm, byte[] hash, byte[] signature)
		{
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			if (algorithm != AssemblyHashAlgorithm.MD5)
			{
				if (algorithm != AssemblyHashAlgorithm.SHA1 && algorithm != AssemblyHashAlgorithm.None)
				{
				}
				rsapkcs1SignatureDeformatter.SetHashAlgorithm("SHA1");
			}
			else
			{
				rsapkcs1SignatureDeformatter.SetHashAlgorithm("MD5");
			}
			return rsapkcs1SignatureDeformatter.VerifySignature(hash, signature);
		}

		// Token: 0x040001F9 RID: 505
		private RSA rsa;

		// Token: 0x040001FA RID: 506
		private byte[] publicKey;

		// Token: 0x040001FB RID: 507
		private byte[] keyToken;

		// Token: 0x040001FC RID: 508
		private string tokenAlgorithm;

		// Token: 0x040001FD RID: 509
		private static object lockObject = new object();

		// Token: 0x040001FE RID: 510
		private static bool initialized = false;

		// Token: 0x020000A9 RID: 169
		internal class StrongNameSignature
		{
			// Token: 0x1700011A RID: 282
			// (get) Token: 0x0600099C RID: 2460 RVA: 0x00025914 File Offset: 0x00023B14
			// (set) Token: 0x0600099D RID: 2461 RVA: 0x0002591C File Offset: 0x00023B1C
			public byte[] Hash
			{
				get
				{
					return this.hash;
				}
				set
				{
					this.hash = value;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x0600099E RID: 2462 RVA: 0x00025928 File Offset: 0x00023B28
			// (set) Token: 0x0600099F RID: 2463 RVA: 0x00025930 File Offset: 0x00023B30
			public byte[] Signature
			{
				get
				{
					return this.signature;
				}
				set
				{
					this.signature = value;
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0002593C File Offset: 0x00023B3C
			// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00025944 File Offset: 0x00023B44
			public uint MetadataPosition
			{
				get
				{
					return this.metadataPosition;
				}
				set
				{
					this.metadataPosition = value;
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00025950 File Offset: 0x00023B50
			// (set) Token: 0x060009A3 RID: 2467 RVA: 0x00025958 File Offset: 0x00023B58
			public uint MetadataLength
			{
				get
				{
					return this.metadataLength;
				}
				set
				{
					this.metadataLength = value;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00025964 File Offset: 0x00023B64
			// (set) Token: 0x060009A5 RID: 2469 RVA: 0x0002596C File Offset: 0x00023B6C
			public uint SignaturePosition
			{
				get
				{
					return this.signaturePosition;
				}
				set
				{
					this.signaturePosition = value;
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00025978 File Offset: 0x00023B78
			// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00025980 File Offset: 0x00023B80
			public uint SignatureLength
			{
				get
				{
					return this.signatureLength;
				}
				set
				{
					this.signatureLength = value;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0002598C File Offset: 0x00023B8C
			// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00025994 File Offset: 0x00023B94
			public byte CliFlag
			{
				get
				{
					return this.cliFlag;
				}
				set
				{
					this.cliFlag = value;
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060009AA RID: 2474 RVA: 0x000259A0 File Offset: 0x00023BA0
			// (set) Token: 0x060009AB RID: 2475 RVA: 0x000259A8 File Offset: 0x00023BA8
			public uint CliFlagPosition
			{
				get
				{
					return this.cliFlagPosition;
				}
				set
				{
					this.cliFlagPosition = value;
				}
			}

			// Token: 0x040001FF RID: 511
			private byte[] hash;

			// Token: 0x04000200 RID: 512
			private byte[] signature;

			// Token: 0x04000201 RID: 513
			private uint signaturePosition;

			// Token: 0x04000202 RID: 514
			private uint signatureLength;

			// Token: 0x04000203 RID: 515
			private uint metadataPosition;

			// Token: 0x04000204 RID: 516
			private uint metadataLength;

			// Token: 0x04000205 RID: 517
			private byte cliFlag;

			// Token: 0x04000206 RID: 518
			private uint cliFlagPosition;
		}

		// Token: 0x020000AA RID: 170
		internal enum StrongNameOptions
		{
			// Token: 0x04000208 RID: 520
			Metadata,
			// Token: 0x04000209 RID: 521
			Signature
		}
	}
}
