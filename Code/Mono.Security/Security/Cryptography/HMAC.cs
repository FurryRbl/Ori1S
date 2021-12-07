using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000074 RID: 116
	internal class HMAC : KeyedHashAlgorithm
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0001ADAC File Offset: 0x00018FAC
		public HMAC()
		{
			this.hash = MD5.Create();
			this.HashSizeValue = this.hash.HashSize;
			byte[] array = new byte[64];
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			rngcryptoServiceProvider.GetNonZeroBytes(array);
			this.KeyValue = (byte[])array.Clone();
			this.Initialize();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001AE08 File Offset: 0x00019008
		public HMAC(string hashName, byte[] rgbKey)
		{
			if (hashName == null || hashName.Length == 0)
			{
				hashName = "MD5";
			}
			this.hash = HashAlgorithm.Create(hashName);
			this.HashSizeValue = this.hash.HashSize;
			if (rgbKey.Length > 64)
			{
				this.KeyValue = this.hash.ComputeHash(rgbKey);
			}
			else
			{
				this.KeyValue = (byte[])rgbKey.Clone();
			}
			this.Initialize();
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0001AE88 File Offset: 0x00019088
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x0001AE9C File Offset: 0x0001909C
		public override byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.hashing)
				{
					throw new Exception("Cannot change key during hash operation.");
				}
				if (value.Length > 64)
				{
					this.KeyValue = this.hash.ComputeHash(value);
				}
				else
				{
					this.KeyValue = (byte[])value.Clone();
				}
				this.initializePad();
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001AEF8 File Offset: 0x000190F8
		public override void Initialize()
		{
			this.hash.Initialize();
			this.initializePad();
			this.hashing = false;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001AF14 File Offset: 0x00019114
		protected override byte[] HashFinal()
		{
			if (!this.hashing)
			{
				this.hash.TransformBlock(this.innerPad, 0, this.innerPad.Length, this.innerPad, 0);
				this.hashing = true;
			}
			this.hash.TransformFinalBlock(new byte[0], 0, 0);
			byte[] array = this.hash.Hash;
			this.hash.Initialize();
			this.hash.TransformBlock(this.outerPad, 0, this.outerPad.Length, this.outerPad, 0);
			this.hash.TransformFinalBlock(array, 0, array.Length);
			this.Initialize();
			return this.hash.Hash;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001AFC4 File Offset: 0x000191C4
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			if (!this.hashing)
			{
				this.hash.TransformBlock(this.innerPad, 0, this.innerPad.Length, this.innerPad, 0);
				this.hashing = true;
			}
			this.hash.TransformBlock(array, ibStart, cbSize, array, ibStart);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001B018 File Offset: 0x00019218
		private void initializePad()
		{
			this.innerPad = new byte[64];
			this.outerPad = new byte[64];
			for (int i = 0; i < this.KeyValue.Length; i++)
			{
				this.innerPad[i] = (this.KeyValue[i] ^ 54);
				this.outerPad[i] = (this.KeyValue[i] ^ 92);
			}
			for (int j = this.KeyValue.Length; j < 64; j++)
			{
				this.innerPad[j] = 54;
				this.outerPad[j] = 92;
			}
		}

		// Token: 0x040001F1 RID: 497
		private HashAlgorithm hash;

		// Token: 0x040001F2 RID: 498
		private bool hashing;

		// Token: 0x040001F3 RID: 499
		private byte[] innerPad;

		// Token: 0x040001F4 RID: 500
		private byte[] outerPad;
	}
}
