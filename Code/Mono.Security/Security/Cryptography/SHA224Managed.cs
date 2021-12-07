using System;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000039 RID: 57
	public class SHA224Managed : SHA224
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000F268 File Offset: 0x0000D468
		public SHA224Managed()
		{
			this._H = new uint[8];
			this._ProcessingBuffer = new byte[64];
			this.Initialize();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000F290 File Offset: 0x0000D490
		private uint Ch(uint u, uint v, uint w)
		{
			return (u & v) ^ (~u & w);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000F29C File Offset: 0x0000D49C
		private uint Maj(uint u, uint v, uint w)
		{
			return (u & v) ^ (u & w) ^ (v & w);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000F2AC File Offset: 0x0000D4AC
		private uint Ro0(uint x)
		{
			return (x >> 7 | x << 25) ^ (x >> 18 | x << 14) ^ x >> 3;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
		private uint Ro1(uint x)
		{
			return (x >> 17 | x << 15) ^ (x >> 19 | x << 13) ^ x >> 10;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		private uint Sig0(uint x)
		{
			return (x >> 2 | x << 30) ^ (x >> 13 | x << 19) ^ (x >> 22 | x << 10);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000F300 File Offset: 0x0000D500
		private uint Sig1(uint x)
		{
			return (x >> 6 | x << 26) ^ (x >> 11 | x << 21) ^ (x >> 25 | x << 7);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000F320 File Offset: 0x0000D520
		protected override void HashCore(byte[] rgb, int start, int size)
		{
			this.State = 1;
			if (this._ProcessingBufferCount != 0)
			{
				if (size < 64 - this._ProcessingBufferCount)
				{
					Buffer.BlockCopy(rgb, start, this._ProcessingBuffer, this._ProcessingBufferCount, size);
					this._ProcessingBufferCount += size;
					return;
				}
				int i = 64 - this._ProcessingBufferCount;
				Buffer.BlockCopy(rgb, start, this._ProcessingBuffer, this._ProcessingBufferCount, i);
				this.ProcessBlock(this._ProcessingBuffer, 0);
				this._ProcessingBufferCount = 0;
				start += i;
				size -= i;
			}
			for (int i = 0; i < size - size % 64; i += 64)
			{
				this.ProcessBlock(rgb, start + i);
			}
			if (size % 64 != 0)
			{
				Buffer.BlockCopy(rgb, size - size % 64 + start, this._ProcessingBuffer, 0, size % 64);
				this._ProcessingBufferCount = size % 64;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000F3FC File Offset: 0x0000D5FC
		protected override byte[] HashFinal()
		{
			byte[] array = new byte[28];
			this.ProcessFinalBlock(this._ProcessingBuffer, 0, this._ProcessingBufferCount);
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					array[i * 4 + j] = (byte)(this._H[i] >> 24 - j * 8);
				}
			}
			this.State = 0;
			return array;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000F46C File Offset: 0x0000D66C
		public override void Initialize()
		{
			this.count = 0UL;
			this._ProcessingBufferCount = 0;
			this._H[0] = 3238371032U;
			this._H[1] = 914150663U;
			this._H[2] = 812702999U;
			this._H[3] = 4144912697U;
			this._H[4] = 4290775857U;
			this._H[5] = 1750603025U;
			this._H[6] = 1694076839U;
			this._H[7] = 3204075428U;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		private void ProcessBlock(byte[] inputBuffer, int inputOffset)
		{
			this.count += 64UL;
			uint[] array = new uint[64];
			for (int i = 0; i < 16; i++)
			{
				array[i] = (uint)((int)inputBuffer[inputOffset + 4 * i] << 24 | (int)inputBuffer[inputOffset + 4 * i + 1] << 16 | (int)inputBuffer[inputOffset + 4 * i + 2] << 8 | (int)inputBuffer[inputOffset + 4 * i + 3]);
			}
			for (int i = 16; i < 64; i++)
			{
				array[i] = this.Ro1(array[i - 2]) + array[i - 7] + this.Ro0(array[i - 15]) + array[i - 16];
			}
			uint num = this._H[0];
			uint num2 = this._H[1];
			uint num3 = this._H[2];
			uint num4 = this._H[3];
			uint num5 = this._H[4];
			uint num6 = this._H[5];
			uint num7 = this._H[6];
			uint num8 = this._H[7];
			for (int i = 0; i < 64; i++)
			{
				uint num9 = num8 + this.Sig1(num5) + this.Ch(num5, num6, num7) + SHAConstants.K1[i] + array[i];
				uint num10 = this.Sig0(num) + this.Maj(num, num2, num3);
				num8 = num7;
				num7 = num6;
				num6 = num5;
				num5 = num4 + num9;
				num4 = num3;
				num3 = num2;
				num2 = num;
				num = num9 + num10;
			}
			this._H[0] += num;
			this._H[1] += num2;
			this._H[2] += num3;
			this._H[3] += num4;
			this._H[4] += num5;
			this._H[5] += num6;
			this._H[6] += num7;
			this._H[7] += num8;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		private void ProcessFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			ulong num = this.count + (ulong)((long)inputCount);
			int num2 = 56 - (int)(num % 64UL);
			if (num2 < 1)
			{
				num2 += 64;
			}
			byte[] array = new byte[inputCount + num2 + 8];
			for (int i = 0; i < inputCount; i++)
			{
				array[i] = inputBuffer[i + inputOffset];
			}
			array[inputCount] = 128;
			for (int j = inputCount + 1; j < inputCount + num2; j++)
			{
				array[j] = 0;
			}
			ulong length = num << 3;
			this.AddLength(length, array, inputCount + num2);
			this.ProcessBlock(array, 0);
			if (inputCount + num2 + 8 == 128)
			{
				this.ProcessBlock(array, 64);
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000F7A4 File Offset: 0x0000D9A4
		internal void AddLength(ulong length, byte[] buffer, int position)
		{
			buffer[position++] = (byte)(length >> 56);
			buffer[position++] = (byte)(length >> 48);
			buffer[position++] = (byte)(length >> 40);
			buffer[position++] = (byte)(length >> 32);
			buffer[position++] = (byte)(length >> 24);
			buffer[position++] = (byte)(length >> 16);
			buffer[position++] = (byte)(length >> 8);
			buffer[position] = (byte)length;
		}

		// Token: 0x04000101 RID: 257
		private const int BLOCK_SIZE_BYTES = 64;

		// Token: 0x04000102 RID: 258
		private uint[] _H;

		// Token: 0x04000103 RID: 259
		private ulong count;

		// Token: 0x04000104 RID: 260
		private byte[] _ProcessingBuffer;

		// Token: 0x04000105 RID: 261
		private int _ProcessingBufferCount;
	}
}
