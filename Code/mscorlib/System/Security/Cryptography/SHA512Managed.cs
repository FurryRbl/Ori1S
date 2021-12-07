using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA512" /> hash algorithm for the input data using the managed library.</summary>
	// Token: 0x020005E2 RID: 1506
	[ComVisible(true)]
	public class SHA512Managed : SHA512
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA512Managed" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">The Federal Information Processing Standards (FIPS) security setting is enabled. This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.</exception>
		// Token: 0x06003974 RID: 14708 RVA: 0x000C50A4 File Offset: 0x000C32A4
		public SHA512Managed()
		{
			this.xBuf = new byte[8];
			this.W = new ulong[80];
			this.Initialize(false);
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000C50D8 File Offset: 0x000C32D8
		private void Initialize(bool reuse)
		{
			this.H1 = 7640891576956012808UL;
			this.H2 = 13503953896175478587UL;
			this.H3 = 4354685564936845355UL;
			this.H4 = 11912009170470909681UL;
			this.H5 = 5840696475078001361UL;
			this.H6 = 11170449401992604703UL;
			this.H7 = 2270897969802886507UL;
			this.H8 = 6620516959819538809UL;
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

		/// <summary>Initializes an instance of the <see cref="T:System.Security.Cryptography.SHA512Managed" /> class using the managed library.</summary>
		// Token: 0x06003976 RID: 14710 RVA: 0x000C51C8 File Offset: 0x000C33C8
		public override void Initialize()
		{
			this.Initialize(true);
		}

		/// <summary>When overridden in a derived class, routes data written to the object into the <see cref="T:System.Security.Cryptography.SHA512Managed" /> hash algorithm for computing the hash.</summary>
		/// <param name="rgb">The input data. </param>
		/// <param name="ibStart">The offset into the byte array from which to begin using data. </param>
		/// <param name="cbSize">The number of bytes in the array to use as data. </param>
		// Token: 0x06003977 RID: 14711 RVA: 0x000C51D4 File Offset: 0x000C33D4
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
		// Token: 0x06003978 RID: 14712 RVA: 0x000C5274 File Offset: 0x000C3474
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
			byte[] array = new byte[64];
			this.unpackWord(this.H1, array, 0);
			this.unpackWord(this.H2, array, 8);
			this.unpackWord(this.H3, array, 16);
			this.unpackWord(this.H4, array, 24);
			this.unpackWord(this.H5, array, 32);
			this.unpackWord(this.H6, array, 40);
			this.unpackWord(this.H7, array, 48);
			this.unpackWord(this.H8, array, 56);
			this.Initialize();
			return array;
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000C534C File Offset: 0x000C354C
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

		// Token: 0x0600397A RID: 14714 RVA: 0x000C53A8 File Offset: 0x000C35A8
		private void processWord(byte[] input, int inOff)
		{
			this.W[this.wOff++] = ((ulong)input[inOff] << 56 | (ulong)input[inOff + 1] << 48 | (ulong)input[inOff + 2] << 40 | (ulong)input[inOff + 3] << 32 | (ulong)input[inOff + 4] << 24 | (ulong)input[inOff + 5] << 16 | (ulong)input[inOff + 6] << 8 | (ulong)input[inOff + 7]);
			if (this.wOff == 16)
			{
				this.processBlock();
			}
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x000C542C File Offset: 0x000C362C
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

		// Token: 0x0600397C RID: 14716 RVA: 0x000C5484 File Offset: 0x000C3684
		private void adjustByteCounts()
		{
			if (this.byteCount1 > 2305843009213693951UL)
			{
				this.byteCount2 += this.byteCount1 >> 61;
				this.byteCount1 &= 2305843009213693951UL;
			}
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x000C54D4 File Offset: 0x000C36D4
		private void processLength(ulong lowW, ulong hiW)
		{
			if (this.wOff > 14)
			{
				this.processBlock();
			}
			this.W[14] = hiW;
			this.W[15] = lowW;
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x000C5500 File Offset: 0x000C3700
		private void processBlock()
		{
			this.adjustByteCounts();
			for (int i = 16; i <= 79; i++)
			{
				this.W[i] = this.Sigma1(this.W[i - 2]) + this.W[i - 7] + this.Sigma0(this.W[i - 15]) + this.W[i - 16];
			}
			ulong num = this.H1;
			ulong num2 = this.H2;
			ulong num3 = this.H3;
			ulong num4 = this.H4;
			ulong num5 = this.H5;
			ulong num6 = this.H6;
			ulong num7 = this.H7;
			ulong num8 = this.H8;
			for (int j = 0; j <= 79; j++)
			{
				ulong num9 = num8 + this.Sum1(num5) + this.Ch(num5, num6, num7) + SHAConstants.K2[j] + this.W[j];
				ulong num10 = this.Sum0(num) + this.Maj(num, num2, num3);
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
			for (int num11 = 0; num11 != this.W.Length; num11++)
			{
				this.W[num11] = 0UL;
			}
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x000C56C0 File Offset: 0x000C38C0
		private ulong rotateRight(ulong x, int n)
		{
			return x >> n | x << 64 - n;
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x000C56D4 File Offset: 0x000C38D4
		private ulong Ch(ulong x, ulong y, ulong z)
		{
			return (x & y) ^ (~x & z);
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000C56E0 File Offset: 0x000C38E0
		private ulong Maj(ulong x, ulong y, ulong z)
		{
			return (x & y) ^ (x & z) ^ (y & z);
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000C56F0 File Offset: 0x000C38F0
		private ulong Sum0(ulong x)
		{
			return this.rotateRight(x, 28) ^ this.rotateRight(x, 34) ^ this.rotateRight(x, 39);
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000C571C File Offset: 0x000C391C
		private ulong Sum1(ulong x)
		{
			return this.rotateRight(x, 14) ^ this.rotateRight(x, 18) ^ this.rotateRight(x, 41);
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000C5748 File Offset: 0x000C3948
		private ulong Sigma0(ulong x)
		{
			return this.rotateRight(x, 1) ^ this.rotateRight(x, 8) ^ x >> 7;
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000C5760 File Offset: 0x000C3960
		private ulong Sigma1(ulong x)
		{
			return this.rotateRight(x, 19) ^ this.rotateRight(x, 61) ^ x >> 6;
		}

		// Token: 0x040018EC RID: 6380
		private byte[] xBuf;

		// Token: 0x040018ED RID: 6381
		private int xBufOff;

		// Token: 0x040018EE RID: 6382
		private ulong byteCount1;

		// Token: 0x040018EF RID: 6383
		private ulong byteCount2;

		// Token: 0x040018F0 RID: 6384
		private ulong H1;

		// Token: 0x040018F1 RID: 6385
		private ulong H2;

		// Token: 0x040018F2 RID: 6386
		private ulong H3;

		// Token: 0x040018F3 RID: 6387
		private ulong H4;

		// Token: 0x040018F4 RID: 6388
		private ulong H5;

		// Token: 0x040018F5 RID: 6389
		private ulong H6;

		// Token: 0x040018F6 RID: 6390
		private ulong H7;

		// Token: 0x040018F7 RID: 6391
		private ulong H8;

		// Token: 0x040018F8 RID: 6392
		private ulong[] W;

		// Token: 0x040018F9 RID: 6393
		private int wOff;
	}
}
