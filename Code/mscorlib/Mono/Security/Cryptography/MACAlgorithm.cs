using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x020000BE RID: 190
	internal class MACAlgorithm
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002D124 File Offset: 0x0002B324
		public MACAlgorithm(SymmetricAlgorithm algorithm)
		{
			this.algo = algorithm;
			this.algo.Mode = CipherMode.CBC;
			this.blockSize = this.algo.BlockSize >> 3;
			this.algo.IV = new byte[this.blockSize];
			this.block = new byte[this.blockSize];
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002D184 File Offset: 0x0002B384
		public void Initialize(byte[] key)
		{
			this.algo.Key = key;
			if (this.enc == null)
			{
				this.enc = this.algo.CreateEncryptor();
			}
			Array.Clear(this.block, 0, this.blockSize);
			this.blockCount = 0;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002D1D4 File Offset: 0x0002B3D4
		public void Core(byte[] rgb, int ib, int cb)
		{
			int num = Math.Min(this.blockSize - this.blockCount, cb);
			Array.Copy(rgb, ib, this.block, this.blockCount, num);
			this.blockCount += num;
			if (this.blockCount == this.blockSize)
			{
				this.enc.TransformBlock(this.block, 0, this.blockSize, this.block, 0);
				int num2 = (cb - num) / this.blockSize;
				for (int i = 0; i < num2; i++)
				{
					this.enc.TransformBlock(rgb, num, this.blockSize, this.block, 0);
					num += this.blockSize;
				}
				this.blockCount = cb - num;
				if (this.blockCount > 0)
				{
					Array.Copy(rgb, num, this.block, 0, this.blockCount);
				}
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002D2B4 File Offset: 0x0002B4B4
		public byte[] Final()
		{
			byte[] result;
			if (this.blockCount > 0 || (this.algo.Padding != PaddingMode.Zeros && this.algo.Padding != PaddingMode.None))
			{
				result = this.enc.TransformFinalBlock(this.block, 0, this.blockCount);
			}
			else
			{
				result = (byte[])this.block.Clone();
			}
			if (!this.enc.CanReuseTransform)
			{
				this.enc.Dispose();
				this.enc = null;
			}
			return result;
		}

		// Token: 0x0400027F RID: 639
		private SymmetricAlgorithm algo;

		// Token: 0x04000280 RID: 640
		private ICryptoTransform enc;

		// Token: 0x04000281 RID: 641
		private byte[] block;

		// Token: 0x04000282 RID: 642
		private int blockSize;

		// Token: 0x04000283 RID: 643
		private int blockCount;
	}
}
