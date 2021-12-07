using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000026 RID: 38
	public class BlockProcessor
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000B510 File Offset: 0x00009710
		public BlockProcessor(ICryptoTransform transform) : this(transform, transform.InputBlockSize)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000B520 File Offset: 0x00009720
		public BlockProcessor(ICryptoTransform transform, int blockSize)
		{
			this.transform = transform;
			this.blockSize = blockSize;
			this.block = new byte[blockSize];
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000B550 File Offset: 0x00009750
		~BlockProcessor()
		{
			Array.Clear(this.block, 0, this.blockSize);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000B598 File Offset: 0x00009798
		public void Initialize()
		{
			Array.Clear(this.block, 0, this.blockSize);
			this.blockCount = 0;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000B5B4 File Offset: 0x000097B4
		public void Core(byte[] rgb)
		{
			this.Core(rgb, 0, rgb.Length);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000B5C4 File Offset: 0x000097C4
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

		// Token: 0x060001AF RID: 431 RVA: 0x0000B6A8 File Offset: 0x000098A8
		public byte[] Final()
		{
			return this.transform.TransformFinalBlock(this.block, 0, this.blockCount);
		}

		// Token: 0x040000AF RID: 175
		private ICryptoTransform transform;

		// Token: 0x040000B0 RID: 176
		private byte[] block;

		// Token: 0x040000B1 RID: 177
		private int blockSize;

		// Token: 0x040000B2 RID: 178
		private int blockCount;
	}
}
