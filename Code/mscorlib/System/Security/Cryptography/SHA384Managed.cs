using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA384" /> hash for the input data using the managed library. </summary>
	// Token: 0x020005E0 RID: 1504
	[ComVisible(true)]
	public class SHA384Managed : SHA384
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA384Managed" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">The Federal Information Processing Standards (FIPS) security setting is enabled. This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.</exception>
		// Token: 0x06003966 RID: 14694 RVA: 0x000C4A20 File Offset: 0x000C2C20
		public SHA384Managed()
		{
			this.xBuf = new byte[8];
			this.W = new ulong[80];
			this.Initialize(false);
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x000C4A54 File Offset: 0x000C2C54
		private void Initialize(bool reuse)
		{
			this.H1 = 14680500436340154072UL;
			this.H2 = 7105036623409894663UL;
			this.H3 = 10473403895298186519UL;
			this.H4 = 1526699215303891257UL;
			this.H5 = 7436329637833083697UL;
			this.H6 = 10282925794625328401UL;
			this.H7 = 15784041429090275239UL;
			this.H8 = 5167115440072839076UL;
			if (reuse)
			{
				this.byteCount1 = 0UL;
				this.byteCount2 = 0UL;
				this.xBufOff = 0;
				for (int i = 0; i < this.xBuf.Length; i++)
				{
					this.xBuf[i] = 0;
				}
				this.wOff = 0;
				for (int num = 0; num != this.W.Length; num++)
				{
					this.W[num] = 0UL;
				}
			}
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Cryptography.SHA384Managed" />.</summary>
		// Token: 0x06003968 RID: 14696 RVA: 0x000C4B44 File Offset: 0x000C2D44
		public override void Initialize()
		{
			this.Initialize(true);
		}

		/// <summary>When overridden in a derived class, routes data written to the object into the <see cref="T:System.Security.Cryptography.SHA384Managed" /> hash algorithm for computing the hash.</summary>
		/// <param name="rgb">The input data. </param>
		/// <param name="ibStart">The offset into the byte array from which to begin using data. </param>
		/// <param name="cbSize">The number of bytes in the array to use as data. </param>
		// Token: 0x06003969 RID: 14697 RVA: 0x000C4B50 File Offset: 0x000C2D50
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			while (this.xBufOff != 0 && cbSize > 0)
			{
				this.update(rgb[ibStart]);
				ibStart++;
				cbSize--;
			}
			while (cbSize > this.xBuf.Length)
			{
				this.processWord(rgb, ibStart);
				ibStart += this.xBuf.Length;
				cbSize -= this.xBuf.Length;
				this.byteCount1 += (ulong)((long)this.xBuf.Length);
			}
			while (cbSize > 0)
			{
				this.update(rgb[ibStart]);
				ibStart++;
				cbSize--;
			}
		}

		/// <summary>When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.</summary>
		/// <returns>The computed hash code.</returns>
		// Token: 0x0600396A RID: 14698 RVA: 0x000C4BF0 File Offset: 0x000C2DF0
		protected override byte[] HashFinal()
		{
			this.adjustByteCounts();
			ulong lowW = this.byteCount1 << 3;
			ulong hiW = this.byteCount2;
			this.update(128);
			while (this.xBufOff != 0)
			{
				this.update(0);
			}
			this.processLength(lowW, hiW);
			this.processBlock();
			byte[] array = new byte[48];
			this.unpackWord(this.H1, array, 0);
			this.unpackWord(this.H2, array, 8);
			this.unpackWord(this.H3, array, 16);
			this.unpackWord(this.H4, array, 24);
			this.unpackWord(this.H5, array, 32);
			this.unpackWord(this.H6, array, 40);
			this.Initialize();
			return array;
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000C4CAC File Offset: 0x000C2EAC
		private void update(byte input)
		{
			this.xBuf[this.xBufOff++] = input;
			if (this.xBufOff == this.xBuf.Length)
			{
				this.processWord(this.xBuf, 0);
				this.xBufOff = 0;
			}
			this.byteCount1 += 1UL;
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000C4D08 File Offset: 0x000C2F08
		private void processWord(byte[] input, int inOff)
		{
			this.W[this.wOff++] = ((ulong)input[inOff] << 56 | (ulong)input[inOff + 1] << 48 | (ulong)input[inOff + 2] << 40 | (ulong)input[inOff + 3] << 32 | (ulong)input[inOff + 4] << 24 | (ulong)input[inOff + 5] << 16 | (ulong)input[inOff + 6] << 8 | (ulong)input[inOff + 7]);
			if (this.wOff == 16)
			{
				this.processBlock();
			}
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000C4D8C File Offset: 0x000C2F8C
		private void unpackWord(ulong word, byte[] output, int outOff)
		{
			output[outOff] = (byte)(word >> 56);
			output[outOff + 1] = (byte)(word >> 48);
			output[outOff + 2] = (byte)(word >> 40);
			output[outOff + 3] = (byte)(word >> 32);
			output[outOff + 4] = (byte)(word >> 24);
			output[outOff + 5] = (byte)(word >> 16);
			output[outOff + 6] = (byte)(word >> 8);
			output[outOff + 7] = (byte)word;
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000C4DE4 File Offset: 0x000C2FE4
		private void adjustByteCounts()
		{
			if (this.byteCount1 > 2305843009213693951UL)
			{
				this.byteCount2 += this.byteCount1 >> 61;
				this.byteCount1 &= 2305843009213693951UL;
			}
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000C4E34 File Offset: 0x000C3034
		private void processLength(ulong lowW, ulong hiW)
		{
			if (this.wOff > 14)
			{
				this.processBlock();
			}
			this.W[14] = hiW;
			this.W[15] = lowW;
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000C4E60 File Offset: 0x000C3060
		private void processBlock()
		{
			ulong[] w = this.W;
			ulong[] k = SHAConstants.K2;
			this.adjustByteCounts();
			ulong num;
			ulong num2;
			for (int i = 16; i <= 79; i++)
			{
				num = w[i - 15];
				num = ((num >> 1 | num << 63) ^ (num >> 8 | num << 56) ^ num >> 7);
				num2 = w[i - 2];
				num2 = ((num2 >> 19 | num2 << 45) ^ (num2 >> 61 | num2 << 3) ^ num2 >> 6);
				w[i] = num2 + w[i - 7] + num + w[i - 16];
			}
			num = this.H1;
			num2 = this.H2;
			ulong num3 = this.H3;
			ulong num4 = this.H4;
			ulong num5 = this.H5;
			ulong num6 = this.H6;
			ulong num7 = this.H7;
			ulong num8 = this.H8;
			for (int j = 0; j <= 79; j++)
			{
				ulong num9 = (num5 >> 14 | num5 << 50) ^ (num5 >> 18 | num5 << 46) ^ (num5 >> 41 | num5 << 23);
				num9 += num8 + ((num5 & num6) ^ (~num5 & num7)) + k[j] + w[j];
				ulong num10 = (num >> 28 | num << 36) ^ (num >> 34 | num << 30) ^ (num >> 39 | num << 25);
				num10 += ((num & num2) ^ (num & num3) ^ (num2 & num3));
				num8 = num7;
				num7 = num6;
				num6 = num5;
				num5 = num4 + num9;
				num4 = num3;
				num3 = num2;
				num2 = num;
				num = num9 + num10;
			}
			this.H1 += num;
			this.H2 += num2;
			this.H3 += num3;
			this.H4 += num4;
			this.H5 += num5;
			this.H6 += num6;
			this.H7 += num7;
			this.H8 += num8;
			this.wOff = 0;
			for (int num11 = 0; num11 != w.Length; num11++)
			{
				w[num11] = 0UL;
			}
		}

		// Token: 0x040018DE RID: 6366
		private byte[] xBuf;

		// Token: 0x040018DF RID: 6367
		private int xBufOff;

		// Token: 0x040018E0 RID: 6368
		private ulong byteCount1;

		// Token: 0x040018E1 RID: 6369
		private ulong byteCount2;

		// Token: 0x040018E2 RID: 6370
		private ulong H1;

		// Token: 0x040018E3 RID: 6371
		private ulong H2;

		// Token: 0x040018E4 RID: 6372
		private ulong H3;

		// Token: 0x040018E5 RID: 6373
		private ulong H4;

		// Token: 0x040018E6 RID: 6374
		private ulong H5;

		// Token: 0x040018E7 RID: 6375
		private ulong H6;

		// Token: 0x040018E8 RID: 6376
		private ulong H7;

		// Token: 0x040018E9 RID: 6377
		private ulong H8;

		// Token: 0x040018EA RID: 6378
		private ulong[] W;

		// Token: 0x040018EB RID: 6379
		private int wOff;
	}
}
