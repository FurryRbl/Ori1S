using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x020000B5 RID: 181
	internal class BlockProcessor
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x0002A680 File Offset: 0x00028880
		public BlockProcessor(ICryptoTransform transform) : this(transform, transform.InputBlockSize)
		{
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002A690 File Offset: 0x00028890
		public BlockProcessor(ICryptoTransform transform, int blockSize)
		{
			this.transform = transform;
			this.blockSize = blockSize;
			this.block = new byte[blockSize];
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002A6C0 File Offset: 0x000288C0
		~BlockProcessor()
		{
			Array.Clear(this.block, 0, this.blockSize);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002A708 File Offset: 0x00028908
		public void Initialize()
		{
			Array.Clear(this.block, 0, this.blockSize);
			this.blockCount = 0;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002A724 File Offset: 0x00028924
		public void Core(byte[] rgb)
		{
			this.Core(rgb, 0, rgb.Length);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002A734 File Offset: 0x00028934
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

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002A818 File Offset: 0x00028A18
		public byte[] Final()
		{
			return this.transform.TransformFinalBlock(this.block, 0, this.blockCount);
		}

		// Token: 0x0400024F RID: 591
		private ICryptoTransform transform;

		// Token: 0x04000250 RID: 592
		private byte[] block;

		// Token: 0x04000251 RID: 593
		private int blockSize;

		// Token: 0x04000252 RID: 594
		private int blockCount;
	}
}
