using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200000F RID: 15
	public class BlockProcessor
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00005918 File Offset: 0x00003B18
		public BlockProcessor(ICryptoTransform transform) : this(transform, transform.InputBlockSize)
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005928 File Offset: 0x00003B28
		public BlockProcessor(ICryptoTransform transform, int blockSize)
		{
			this.transform = transform;
			this.blockSize = blockSize;
			this.block = new byte[blockSize];
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005958 File Offset: 0x00003B58
		~BlockProcessor()
		{
			Array.Clear(this.block, 0, this.blockSize);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000059A0 File Offset: 0x00003BA0
		public void Initialize()
		{
			Array.Clear(this.block, 0, this.blockSize);
			this.blockCount = 0;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000059BC File Offset: 0x00003BBC
		public void Core(byte[] rgb)
		{
			this.Core(rgb, 0, rgb.Length);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000059CC File Offset: 0x00003BCC
		public void Core(byte[] rgb, int ib, int cb)
		{
			int num = Math.Min(this.blockSize - this.blockCount, cb);
			Buffer.BlockCopy(rgb, ib, this.block, this.blockCount, num);
			this.blockCount += num;
			if (this.blockCount == this.blockSize)
			{
				this.transform.TransformBlock(this.block, 0, this.blockSize, this.block, 0);
				int num2 = (cb - num) / this.blockSize;
				for (int i = 0; i < num2; i++)
				{
					this.transform.TransformBlock(rgb, num + ib, this.blockSize, this.block, 0);
					num += this.blockSize;
				}
				this.blockCount = cb - num;
				if (this.blockCount > 0)
				{
					Buffer.BlockCopy(rgb, num + ib, this.block, 0, this.blockCount);
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005AB0 File Offset: 0x00003CB0
		public byte[] Final()
		{
			return this.transform.TransformFinalBlock(this.block, 0, this.blockCount);
		}

		// Token: 0x04000033 RID: 51
		private ICryptoTransform transform;

		// Token: 0x04000034 RID: 52
		private byte[] block;

		// Token: 0x04000035 RID: 53
		private int blockSize;

		// Token: 0x04000036 RID: 54
		private int blockCount;
	}
}
