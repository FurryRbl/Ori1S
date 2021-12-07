using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security
{
	// Token: 0x0200001A RID: 26
	public sealed class StrongName
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00007250 File Offset: 0x00005450
		public StrongName()
		{
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00007258 File Offset: 0x00005458
		public StrongName(int keySize)
		{
			this.rsa = new RSAManaged(keySize);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000726C File Offset: 0x0000546C
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

		// Token: 0x0600010B RID: 267 RVA: 0x000072F8 File Offset: 0x000054F8
		public StrongName(RSA rsa)
		{
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			this.RSA = rsa;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007318 File Offset: 0x00005518
		private void InvalidateCache()
		{
			this.publicKey = null;
			this.keyToken = null;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00007328 File Offset: 0x00005528
		public bool CanSign
		{
			get
			{
				if (this.rsa == null)
				{
					return false;
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000073D0 File Offset: 0x000055D0
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000073F0 File Offset: 0x000055F0
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00007400 File Offset: 0x00005600
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

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00007514 File Offset: 0x00005714
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000758C File Offset: 0x0000578C
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000075AC File Offset: 0x000057AC
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

		// Token: 0x06000114 RID: 276 RVA: 0x00007604 File Offset: 0x00005804
		public byte[] GetBytes()
		{
			return CryptoConvert.ToCapiPrivateKeyBlob(this.RSA);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007614 File Offset: 0x00005814
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

		// Token: 0x06000116 RID: 278 RVA: 0x00007678 File Offset: 0x00005878
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

		// Token: 0x06000117 RID: 279 RVA: 0x000079C4 File Offset: 0x00005BC4
		public byte[] Hash(string fileName)
		{
			FileStream fileStream = File.OpenRead(fileName);
			StrongName.StrongNameSignature strongNameSignature = this.StrongHash(fileStream, StrongName.StrongNameOptions.Metadata);
			fileStream.Close();
			return strongNameSignature.Hash;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000079F0 File Offset: 0x00005BF0
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

		// Token: 0x06000119 RID: 281 RVA: 0x00007B08 File Offset: 0x00005D08
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

		// Token: 0x0600011A RID: 282 RVA: 0x00007B5C File Offset: 0x00005D5C
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

		// Token: 0x0600011B RID: 283 RVA: 0x00007BF0 File Offset: 0x00005DF0
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

		// Token: 0x04000063 RID: 99
		private RSA rsa;

		// Token: 0x04000064 RID: 100
		private byte[] publicKey;

		// Token: 0x04000065 RID: 101
		private byte[] keyToken;

		// Token: 0x04000066 RID: 102
		private string tokenAlgorithm;

		// Token: 0x0200001B RID: 27
		internal class StrongNameSignature
		{
			// Token: 0x17000036 RID: 54
			// (get) Token: 0x0600011D RID: 285 RVA: 0x00007C58 File Offset: 0x00005E58
			// (set) Token: 0x0600011E RID: 286 RVA: 0x00007C60 File Offset: 0x00005E60
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

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x0600011F RID: 287 RVA: 0x00007C6C File Offset: 0x00005E6C
			// (set) Token: 0x06000120 RID: 288 RVA: 0x00007C74 File Offset: 0x00005E74
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

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000121 RID: 289 RVA: 0x00007C80 File Offset: 0x00005E80
			// (set) Token: 0x06000122 RID: 290 RVA: 0x00007C88 File Offset: 0x00005E88
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

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x06000123 RID: 291 RVA: 0x00007C94 File Offset: 0x00005E94
			// (set) Token: 0x06000124 RID: 292 RVA: 0x00007C9C File Offset: 0x00005E9C
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

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x06000125 RID: 293 RVA: 0x00007CA8 File Offset: 0x00005EA8
			// (set) Token: 0x06000126 RID: 294 RVA: 0x00007CB0 File Offset: 0x00005EB0
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

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x06000127 RID: 295 RVA: 0x00007CBC File Offset: 0x00005EBC
			// (set) Token: 0x06000128 RID: 296 RVA: 0x00007CC4 File Offset: 0x00005EC4
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

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000129 RID: 297 RVA: 0x00007CD0 File Offset: 0x00005ED0
			// (set) Token: 0x0600012A RID: 298 RVA: 0x00007CD8 File Offset: 0x00005ED8
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

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x0600012B RID: 299 RVA: 0x00007CE4 File Offset: 0x00005EE4
			// (set) Token: 0x0600012C RID: 300 RVA: 0x00007CEC File Offset: 0x00005EEC
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

			// Token: 0x04000067 RID: 103
			private byte[] hash;

			// Token: 0x04000068 RID: 104
			private byte[] signature;

			// Token: 0x04000069 RID: 105
			private uint signaturePosition;

			// Token: 0x0400006A RID: 106
			private uint signatureLength;

			// Token: 0x0400006B RID: 107
			private uint metadataPosition;

			// Token: 0x0400006C RID: 108
			private uint metadataLength;

			// Token: 0x0400006D RID: 109
			private byte cliFlag;

			// Token: 0x0400006E RID: 110
			private uint cliFlagPosition;
		}

		// Token: 0x0200001C RID: 28
		internal enum StrongNameOptions
		{
			// Token: 0x04000070 RID: 112
			Metadata,
			// Token: 0x04000071 RID: 113
			Signature
		}
	}
}
