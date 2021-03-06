using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.RIPEMD160" /> hash for the input data using the managed library. </summary>
	// Token: 0x020005CE RID: 1486
	[ComVisible(true)]
	public class RIPEMD160Managed : RIPEMD160
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RIPEMD160" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">The policy is not compliant with the FIPS algorithm.</exception>
		// Token: 0x060038C7 RID: 14535 RVA: 0x000C0EE4 File Offset: 0x000BF0E4
		public RIPEMD160Managed()
		{
			this._X = new uint[16];
			this._HashValue = new uint[5];
			this._ProcessingBuffer = new byte[64];
			this.Initialize();
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Security.Cryptography.RIPEMD160Managed" /> class using the managed library.</summary>
		// Token: 0x060038C8 RID: 14536 RVA: 0x000C0F24 File Offset: 0x000BF124
		public override void Initialize()
		{
			this._HashValue[0] = 1732584193U;
			this._HashValue[1] = 4023233417U;
			this._HashValue[2] = 2562383102U;
			this._HashValue[3] = 271733878U;
			this._HashValue[4] = 3285377520U;
			this._Length = 0UL;
			this._ProcessingBufferCount = 0;
			Array.Clear(this._X, 0, this._X.Length);
			Array.Clear(this._ProcessingBuffer, 0, this._ProcessingBuffer.Length);
		}

		/// <summary>When overridden in a derived class, routes data written to the object into the <see cref="T:System.Security.Cryptography.RIPEMD160" /> hash algorithm for computing the hash.</summary>
		/// <param name="rgb">The input data. </param>
		/// <param name="ibStart">The offset into the byte array from which to begin using data. </param>
		/// <param name="cbSize">The number of bytes in the array to use as data. </param>
		// Token: 0x060038C9 RID: 14537 RVA: 0x000C0FAC File Offset: 0x000BF1AC
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			this.State = 1;
			this._Length += (ulong)cbSize;
			if (this._ProcessingBufferCount != 0)
			{
				if (cbSize < 64 - this._ProcessingBufferCount)
				{
					Buffer.BlockCopy(rgb, ibStart, this._ProcessingBuffer, this._ProcessingBufferCount, cbSize);
					this._ProcessingBufferCount += cbSize;
					return;
				}
				int i = 64 - this._ProcessingBufferCount;
				Buffer.BlockCopy(rgb, ibStart, this._ProcessingBuffer, this._ProcessingBufferCount, i);
				this.ProcessBlock(this._ProcessingBuffer, 0);
				this._ProcessingBufferCount = 0;
				ibStart += i;
				cbSize -= i;
			}
			for (int i = 0; i < cbSize - cbSize % 64; i += 64)
			{
				this.ProcessBlock(rgb, ibStart + i);
			}
			if (cbSize % 64 != 0)
			{
				Buffer.BlockCopy(rgb, cbSize - cbSize % 64 + ibStart, this._ProcessingBuffer, 0, cbSize % 64);
				this._ProcessingBufferCount = cbSize % 64;
			}
		}

		/// <summary>When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.</summary>
		/// <returns>The computed hash code in a byte array.</returns>
		// Token: 0x060038CA RID: 14538 RVA: 0x000C1098 File Offset: 0x000BF298
		protected override byte[] HashFinal()
		{
			this.CompressFinal(this._Length);
			byte[] array = new byte[20];
			if (!BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < 5; i++)
				{
					for (int j = 0; j < 4; j++)
					{
						array[i * 4 + j] = (byte)(this._HashValue[i] >> j * 8);
					}
				}
			}
			else
			{
				Buffer.BlockCopy(this._HashValue, 0, array, 0, 20);
			}
			return array;
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x000C1114 File Offset: 0x000BF314
		~RIPEMD160Managed()
		{
			this.Dispose(false);
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x000C1150 File Offset: 0x000BF350
		private void ProcessBlock(byte[] buffer, int offset)
		{
			if (!BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < this._X.Length; i++)
				{
					this._X[i] = (uint)((int)buffer[offset] | (int)buffer[offset + 1] << 8 | (int)buffer[offset + 2] << 16 | (int)buffer[offset + 3] << 24);
					offset += 4;
				}
			}
			else
			{
				Buffer.BlockCopy(buffer, offset, this._X, 0, 64);
			}
			this.Compress();
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x000C11C8 File Offset: 0x000BF3C8
		private void Compress()
		{
			uint num = this._HashValue[0];
			uint num2 = this._HashValue[1];
			uint num3 = this._HashValue[2];
			uint num4 = this._HashValue[3];
			uint num5 = this._HashValue[4];
			uint num6 = this._HashValue[0];
			uint num7 = this._HashValue[1];
			uint num8 = this._HashValue[2];
			uint num9 = this._HashValue[3];
			uint num10 = this._HashValue[4];
			this.FF(ref num, num2, ref num3, num4, num5, this._X[0], 11);
			this.FF(ref num5, num, ref num2, num3, num4, this._X[1], 14);
			this.FF(ref num4, num5, ref num, num2, num3, this._X[2], 15);
			this.FF(ref num3, num4, ref num5, num, num2, this._X[3], 12);
			this.FF(ref num2, num3, ref num4, num5, num, this._X[4], 5);
			this.FF(ref num, num2, ref num3, num4, num5, this._X[5], 8);
			this.FF(ref num5, num, ref num2, num3, num4, this._X[6], 7);
			this.FF(ref num4, num5, ref num, num2, num3, this._X[7], 9);
			this.FF(ref num3, num4, ref num5, num, num2, this._X[8], 11);
			this.FF(ref num2, num3, ref num4, num5, num, this._X[9], 13);
			this.FF(ref num, num2, ref num3, num4, num5, this._X[10], 14);
			this.FF(ref num5, num, ref num2, num3, num4, this._X[11], 15);
			this.FF(ref num4, num5, ref num, num2, num3, this._X[12], 6);
			this.FF(ref num3, num4, ref num5, num, num2, this._X[13], 7);
			this.FF(ref num2, num3, ref num4, num5, num, this._X[14], 9);
			this.FF(ref num, num2, ref num3, num4, num5, this._X[15], 8);
			this.GG(ref num5, num, ref num2, num3, num4, this._X[7], 7);
			this.GG(ref num4, num5, ref num, num2, num3, this._X[4], 6);
			this.GG(ref num3, num4, ref num5, num, num2, this._X[13], 8);
			this.GG(ref num2, num3, ref num4, num5, num, this._X[1], 13);
			this.GG(ref num, num2, ref num3, num4, num5, this._X[10], 11);
			this.GG(ref num5, num, ref num2, num3, num4, this._X[6], 9);
			this.GG(ref num4, num5, ref num, num2, num3, this._X[15], 7);
			this.GG(ref num3, num4, ref num5, num, num2, this._X[3], 15);
			this.GG(ref num2, num3, ref num4, num5, num, this._X[12], 7);
			this.GG(ref num, num2, ref num3, num4, num5, this._X[0], 12);
			this.GG(ref num5, num, ref num2, num3, num4, this._X[9], 15);
			this.GG(ref num4, num5, ref num, num2, num3, this._X[5], 9);
			this.GG(ref num3, num4, ref num5, num, num2, this._X[2], 11);
			this.GG(ref num2, num3, ref num4, num5, num, this._X[14], 7);
			this.GG(ref num, num2, ref num3, num4, num5, this._X[11], 13);
			this.GG(ref num5, num, ref num2, num3, num4, this._X[8], 12);
			this.HH(ref num4, num5, ref num, num2, num3, this._X[3], 11);
			this.HH(ref num3, num4, ref num5, num, num2, this._X[10], 13);
			this.HH(ref num2, num3, ref num4, num5, num, this._X[14], 6);
			this.HH(ref num, num2, ref num3, num4, num5, this._X[4], 7);
			this.HH(ref num5, num, ref num2, num3, num4, this._X[9], 14);
			this.HH(ref num4, num5, ref num, num2, num3, this._X[15], 9);
			this.HH(ref num3, num4, ref num5, num, num2, this._X[8], 13);
			this.HH(ref num2, num3, ref num4, num5, num, this._X[1], 15);
			this.HH(ref num, num2, ref num3, num4, num5, this._X[2], 14);
			this.HH(ref num5, num, ref num2, num3, num4, this._X[7], 8);
			this.HH(ref num4, num5, ref num, num2, num3, this._X[0], 13);
			this.HH(ref num3, num4, ref num5, num, num2, this._X[6], 6);
			this.HH(ref num2, num3, ref num4, num5, num, this._X[13], 5);
			this.HH(ref num, num2, ref num3, num4, num5, this._X[11], 12);
			this.HH(ref num5, num, ref num2, num3, num4, this._X[5], 7);
			this.HH(ref num4, num5, ref num, num2, num3, this._X[12], 5);
			this.II(ref num3, num4, ref num5, num, num2, this._X[1], 11);
			this.II(ref num2, num3, ref num4, num5, num, this._X[9], 12);
			this.II(ref num, num2, ref num3, num4, num5, this._X[11], 14);
			this.II(ref num5, num, ref num2, num3, num4, this._X[10], 15);
			this.II(ref num4, num5, ref num, num2, num3, this._X[0], 14);
			this.II(ref num3, num4, ref num5, num, num2, this._X[8], 15);
			this.II(ref num2, num3, ref num4, num5, num, this._X[12], 9);
			this.II(ref num, num2, ref num3, num4, num5, this._X[4], 8);
			this.II(ref num5, num, ref num2, num3, num4, this._X[13], 9);
			this.II(ref num4, num5, ref num, num2, num3, this._X[3], 14);
			this.II(ref num3, num4, ref num5, num, num2, this._X[7], 5);
			this.II(ref num2, num3, ref num4, num5, num, this._X[15], 6);
			this.II(ref num, num2, ref num3, num4, num5, this._X[14], 8);
			this.II(ref num5, num, ref num2, num3, num4, this._X[5], 6);
			this.II(ref num4, num5, ref num, num2, num3, this._X[6], 5);
			this.II(ref num3, num4, ref num5, num, num2, this._X[2], 12);
			this.JJ(ref num2, num3, ref num4, num5, num, this._X[4], 9);
			this.JJ(ref num, num2, ref num3, num4, num5, this._X[0], 15);
			this.JJ(ref num5, num, ref num2, num3, num4, this._X[5], 5);
			this.JJ(ref num4, num5, ref num, num2, num3, this._X[9], 11);
			this.JJ(ref num3, num4, ref num5, num, num2, this._X[7], 6);
			this.JJ(ref num2, num3, ref num4, num5, num, this._X[12], 8);
			this.JJ(ref num, num2, ref num3, num4, num5, this._X[2], 13);
			this.JJ(ref num5, num, ref num2, num3, num4, this._X[10], 12);
			this.JJ(ref num4, num5, ref num, num2, num3, this._X[14], 5);
			this.JJ(ref num3, num4, ref num5, num, num2, this._X[1], 12);
			this.JJ(ref num2, num3, ref num4, num5, num, this._X[3], 13);
			this.JJ(ref num, num2, ref num3, num4, num5, this._X[8], 14);
			this.JJ(ref num5, num, ref num2, num3, num4, this._X[11], 11);
			this.JJ(ref num4, num5, ref num, num2, num3, this._X[6], 8);
			this.JJ(ref num3, num4, ref num5, num, num2, this._X[15], 5);
			this.JJ(ref num2, num3, ref num4, num5, num, this._X[13], 6);
			this.JJJ(ref num6, num7, ref num8, num9, num10, this._X[5], 8);
			this.JJJ(ref num10, num6, ref num7, num8, num9, this._X[14], 9);
			this.JJJ(ref num9, num10, ref num6, num7, num8, this._X[7], 9);
			this.JJJ(ref num8, num9, ref num10, num6, num7, this._X[0], 11);
			this.JJJ(ref num7, num8, ref num9, num10, num6, this._X[9], 13);
			this.JJJ(ref num6, num7, ref num8, num9, num10, this._X[2], 15);
			this.JJJ(ref num10, num6, ref num7, num8, num9, this._X[11], 15);
			this.JJJ(ref num9, num10, ref num6, num7, num8, this._X[4], 5);
			this.JJJ(ref num8, num9, ref num10, num6, num7, this._X[13], 7);
			this.JJJ(ref num7, num8, ref num9, num10, num6, this._X[6], 7);
			this.JJJ(ref num6, num7, ref num8, num9, num10, this._X[15], 8);
			this.JJJ(ref num10, num6, ref num7, num8, num9, this._X[8], 11);
			this.JJJ(ref num9, num10, ref num6, num7, num8, this._X[1], 14);
			this.JJJ(ref num8, num9, ref num10, num6, num7, this._X[10], 14);
			this.JJJ(ref num7, num8, ref num9, num10, num6, this._X[3], 12);
			this.JJJ(ref num6, num7, ref num8, num9, num10, this._X[12], 6);
			this.III(ref num10, num6, ref num7, num8, num9, this._X[6], 9);
			this.III(ref num9, num10, ref num6, num7, num8, this._X[11], 13);
			this.III(ref num8, num9, ref num10, num6, num7, this._X[3], 15);
			this.III(ref num7, num8, ref num9, num10, num6, this._X[7], 7);
			this.III(ref num6, num7, ref num8, num9, num10, this._X[0], 12);
			this.III(ref num10, num6, ref num7, num8, num9, this._X[13], 8);
			this.III(ref num9, num10, ref num6, num7, num8, this._X[5], 9);
			this.III(ref num8, num9, ref num10, num6, num7, this._X[10], 11);
			this.III(ref num7, num8, ref num9, num10, num6, this._X[14], 7);
			this.III(ref num6, num7, ref num8, num9, num10, this._X[15], 7);
			this.III(ref num10, num6, ref num7, num8, num9, this._X[8], 12);
			this.III(ref num9, num10, ref num6, num7, num8, this._X[12], 7);
			this.III(ref num8, num9, ref num10, num6, num7, this._X[4], 6);
			this.III(ref num7, num8, ref num9, num10, num6, this._X[9], 15);
			this.III(ref num6, num7, ref num8, num9, num10, this._X[1], 13);
			this.III(ref num10, num6, ref num7, num8, num9, this._X[2], 11);
			this.HHH(ref num9, num10, ref num6, num7, num8, this._X[15], 9);
			this.HHH(ref num8, num9, ref num10, num6, num7, this._X[5], 7);
			this.HHH(ref num7, num8, ref num9, num10, num6, this._X[1], 15);
			this.HHH(ref num6, num7, ref num8, num9, num10, this._X[3], 11);
			this.HHH(ref num10, num6, ref num7, num8, num9, this._X[7], 8);
			this.HHH(ref num9, num10, ref num6, num7, num8, this._X[14], 6);
			this.HHH(ref num8, num9, ref num10, num6, num7, this._X[6], 6);
			this.HHH(ref num7, num8, ref num9, num10, num6, this._X[9], 14);
			this.HHH(ref num6, num7, ref num8, num9, num10, this._X[11], 12);
			this.HHH(ref num10, num6, ref num7, num8, num9, this._X[8], 13);
			this.HHH(ref num9, num10, ref num6, num7, num8, this._X[12], 5);
			this.HHH(ref num8, num9, ref num10, num6, num7, this._X[2], 14);
			this.HHH(ref num7, num8, ref num9, num10, num6, this._X[10], 13);
			this.HHH(ref num6, num7, ref num8, num9, num10, this._X[0], 13);
			this.HHH(ref num10, num6, ref num7, num8, num9, this._X[4], 7);
			this.HHH(ref num9, num10, ref num6, num7, num8, this._X[13], 5);
			this.GGG(ref num8, num9, ref num10, num6, num7, this._X[8], 15);
			this.GGG(ref num7, num8, ref num9, num10, num6, this._X[6], 5);
			this.GGG(ref num6, num7, ref num8, num9, num10, this._X[4], 8);
			this.GGG(ref num10, num6, ref num7, num8, num9, this._X[1], 11);
			this.GGG(ref num9, num10, ref num6, num7, num8, this._X[3], 14);
			this.GGG(ref num8, num9, ref num10, num6, num7, this._X[11], 14);
			this.GGG(ref num7, num8, ref num9, num10, num6, this._X[15], 6);
			this.GGG(ref num6, num7, ref num8, num9, num10, this._X[0], 14);
			this.GGG(ref num10, num6, ref num7, num8, num9, this._X[5], 6);
			this.GGG(ref num9, num10, ref num6, num7, num8, this._X[12], 9);
			this.GGG(ref num8, num9, ref num10, num6, num7, this._X[2], 12);
			this.GGG(ref num7, num8, ref num9, num10, num6, this._X[13], 9);
			this.GGG(ref num6, num7, ref num8, num9, num10, this._X[9], 12);
			this.GGG(ref num10, num6, ref num7, num8, num9, this._X[7], 5);
			this.GGG(ref num9, num10, ref num6, num7, num8, this._X[10], 15);
			this.GGG(ref num8, num9, ref num10, num6, num7, this._X[14], 8);
			this.FFF(ref num7, num8, ref num9, num10, num6, this._X[12], 8);
			this.FFF(ref num6, num7, ref num8, num9, num10, this._X[15], 5);
			this.FFF(ref num10, num6, ref num7, num8, num9, this._X[10], 12);
			this.FFF(ref num9, num10, ref num6, num7, num8, this._X[4], 9);
			this.FFF(ref num8, num9, ref num10, num6, num7, this._X[1], 12);
			this.FFF(ref num7, num8, ref num9, num10, num6, this._X[5], 5);
			this.FFF(ref num6, num7, ref num8, num9, num10, this._X[8], 14);
			this.FFF(ref num10, num6, ref num7, num8, num9, this._X[7], 6);
			this.FFF(ref num9, num10, ref num6, num7, num8, this._X[6], 8);
			this.FFF(ref num8, num9, ref num10, num6, num7, this._X[2], 13);
			this.FFF(ref num7, num8, ref num9, num10, num6, this._X[13], 6);
			this.FFF(ref num6, num7, ref num8, num9, num10, this._X[14], 5);
			this.FFF(ref num10, num6, ref num7, num8, num9, this._X[0], 15);
			this.FFF(ref num9, num10, ref num6, num7, num8, this._X[3], 13);
			this.FFF(ref num8, num9, ref num10, num6, num7, this._X[9], 11);
			this.FFF(ref num7, num8, ref num9, num10, num6, this._X[11], 11);
			num9 += num3 + this._HashValue[1];
			this._HashValue[1] = this._HashValue[2] + num4 + num10;
			this._HashValue[2] = this._HashValue[3] + num5 + num6;
			this._HashValue[3] = this._HashValue[4] + num + num7;
			this._HashValue[4] = this._HashValue[0] + num2 + num8;
			this._HashValue[0] = num9;
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000C222C File Offset: 0x000C042C
		private void CompressFinal(ulong length)
		{
			uint num = (uint)(length & (ulong)-1);
			uint num2 = (uint)(length >> 32);
			Array.Clear(this._X, 0, this._X.Length);
			int num3 = 0;
			for (uint num4 = 0U; num4 < (num & 63U); num4 += 1U)
			{
				this._X[(int)((UIntPtr)(num4 >> 2))] ^= (uint)((uint)this._ProcessingBuffer[num3++] << (int)(8U * (num4 & 3U) & 31U & 31U));
			}
			this._X[(int)((UIntPtr)(num >> 2 & 15U))] ^= 1U << (int)(8U * (num & 3U) + 7U);
			if ((num & 63U) > 55U)
			{
				this.Compress();
				Array.Clear(this._X, 0, this._X.Length);
			}
			this._X[14] = num << 3;
			this._X[15] = (num >> 29 | num2 << 3);
			this.Compress();
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000C2308 File Offset: 0x000C0508
		private uint ROL(uint x, int n)
		{
			return x << n | x >> 32 - n;
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x000C231C File Offset: 0x000C051C
		private uint F(uint x, uint y, uint z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x000C2324 File Offset: 0x000C0524
		private uint G(uint x, uint y, uint z)
		{
			return (x & y) | (~x & z);
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x000C2330 File Offset: 0x000C0530
		private uint H(uint x, uint y, uint z)
		{
			return (x | ~y) ^ z;
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x000C2338 File Offset: 0x000C0538
		private uint I(uint x, uint y, uint z)
		{
			return (x & z) | (y & ~z);
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x000C2344 File Offset: 0x000C0544
		private uint J(uint x, uint y, uint z)
		{
			return x ^ (y | ~z);
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000C234C File Offset: 0x000C054C
		private void FF(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
		{
			a += this.F(b, c, d) + x;
			a = this.ROL(a, s) + e;
			c = this.ROL(c, 10);
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000C2388 File Offset: 0x000C0588
		private void GG(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
		{
			a += this.G(b, c, d) + x + 1518500249U;
			a = this.ROL(a, s) + e;
			c = this.ROL(c, 10);
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000C23CC File Offset: 0x000C05CC
		private void HH(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
		{
			a += this.H(b, c, d) + x + 1859775393U;
			a = this.ROL(a, s) + e;
			c = this.ROL(c, 10);
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000C2410 File Offset: 0x000C0610
		private void II(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
		{
			a += this.I(b, c, d) + x + 2400959708U;
			a = this.ROL(a, s) + e;
			c = this.ROL(c, 10);
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x000C2454 File Offset: 0x000C0654
		private void JJ(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
		{
			a += this.J(b, c, d) + x + 2840853838U;
			a = this.ROL(a, s) + e;
			c = this.ROL(c, 10);
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000C2498 File Offset: 0x000C0698
		private void FFF(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
		{
			a += this.F(b, c, d) + x;
			a = this.ROL(a, s) + e;
			c = this.ROL(c, 10);
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000C24D4 File Offset: 0x000C06D4
		private void GGG(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
		{
			a += this.G(b, c, d) + x + 2053994217U;
			a = this.ROL(a, s) + e;
			c = this.ROL(c, 10);
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000C2518 File Offset: 0x000C0718
		private void HHH(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
		{
			a += this.H(b, c, d) + x + 1836072691U;
			a = this.ROL(a, s) + e;
			c = this.ROL(c, 10);
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x000C255C File Offset: 0x000C075C
		private void III(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
		{
			a += this.I(b, c, d) + x + 1548603684U;
			a = this.ROL(a, s) + e;
			c = this.ROL(c, 10);
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x000C25A0 File Offset: 0x000C07A0
		private void JJJ(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
		{
			a += this.J(b, c, d) + x + 1352829926U;
			a = this.ROL(a, s) + e;
			c = this.ROL(c, 10);
		}

		// Token: 0x040018A9 RID: 6313
		private const int BLOCK_SIZE_BYTES = 64;

		// Token: 0x040018AA RID: 6314
		private byte[] _ProcessingBuffer;

		// Token: 0x040018AB RID: 6315
		private uint[] _X;

		// Token: 0x040018AC RID: 6316
		private uint[] _HashValue;

		// Token: 0x040018AD RID: 6317
		private ulong _Length;

		// Token: 0x040018AE RID: 6318
		private int _ProcessingBufferCount;
	}
}
