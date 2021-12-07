using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x020000BD RID: 189
	internal class HMACAlgorithm
	{
		// Token: 0x06000A94 RID: 2708 RVA: 0x0002CED8 File Offset: 0x0002B0D8
		public HMACAlgorithm(string algoName)
		{
			this.CreateHash(algoName);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002CEE8 File Offset: 0x0002B0E8
		~HMACAlgorithm()
		{
			this.Dispose();
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002CF24 File Offset: 0x0002B124
		private void CreateHash(string algoName)
		{
			this.algo = HashAlgorithm.Create(algoName);
			this.hashName = algoName;
			this.block = new BlockProcessor(this.algo, 8);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002CF4C File Offset: 0x0002B14C
		public void Dispose()
		{
			if (this.key != null)
			{
				Array.Clear(this.key, 0, this.key.Length);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0002CF70 File Offset: 0x0002B170
		public HashAlgorithm Algo
		{
			get
			{
				return this.algo;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0002CF78 File Offset: 0x0002B178
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x0002CF80 File Offset: 0x0002B180
		public string HashName
		{
			get
			{
				return this.hashName;
			}
			set
			{
				this.CreateHash(value);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0002CF8C File Offset: 0x0002B18C
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0002CF94 File Offset: 0x0002B194
		public byte[] Key
		{
			get
			{
				return this.key;
			}
			set
			{
				if (value != null && value.Length > 64)
				{
					this.key = this.algo.ComputeHash(value);
				}
				else
				{
					this.key = (byte[])value.Clone();
				}
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002CFDC File Offset: 0x0002B1DC
		public void Initialize()
		{
			this.hash = null;
			this.block.Initialize();
			byte[] array = this.KeySetup(this.key, 54);
			this.algo.Initialize();
			this.block.Core(array);
			Array.Clear(array, 0, array.Length);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002D02C File Offset: 0x0002B22C
		private byte[] KeySetup(byte[] key, byte padding)
		{
			byte[] array = new byte[64];
			for (int i = 0; i < key.Length; i++)
			{
				array[i] = (key[i] ^ padding);
			}
			for (int j = key.Length; j < 64; j++)
			{
				array[j] = padding;
			}
			return array;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0002D078 File Offset: 0x0002B278
		public void Core(byte[] rgb, int ib, int cb)
		{
			this.block.Core(rgb, ib, cb);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002D088 File Offset: 0x0002B288
		public byte[] Final()
		{
			this.block.Final();
			byte[] array = this.algo.Hash;
			byte[] array2 = this.KeySetup(this.key, 92);
			this.algo.Initialize();
			this.algo.TransformBlock(array2, 0, array2.Length, array2, 0);
			this.algo.TransformFinalBlock(array, 0, array.Length);
			this.hash = this.algo.Hash;
			this.algo.Clear();
			Array.Clear(array2, 0, array2.Length);
			Array.Clear(array, 0, array.Length);
			return this.hash;
		}

		// Token: 0x0400027A RID: 634
		private byte[] key;

		// Token: 0x0400027B RID: 635
		private byte[] hash;

		// Token: 0x0400027C RID: 636
		private HashAlgorithm algo;

		// Token: 0x0400027D RID: 637
		private string hashName;

		// Token: 0x0400027E RID: 638
		private BlockProcessor block;
	}
}
