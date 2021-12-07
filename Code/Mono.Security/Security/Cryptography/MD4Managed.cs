using System;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200002F RID: 47
	public class MD4Managed : MD4
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x0000C910 File Offset: 0x0000AB10
		public MD4Managed()
		{
			this.state = new uint[4];
			this.count = new uint[2];
			this.buffer = new byte[64];
			this.digest = new byte[16];
			this.x = new uint[16];
			this.Initialize();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000C968 File Offset: 0x0000AB68
		public override void Initialize()
		{
			this.count[0] = 0U;
			this.count[1] = 0U;
			this.state[0] = 1732584193U;
			this.state[1] = 4023233417U;
			this.state[2] = 2562383102U;
			this.state[3] = 271733878U;
			Array.Clear(this.buffer, 0, 64);
			Array.Clear(this.x, 0, 16);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000C9D8 File Offset: 0x0000ABD8
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			int num = (int)(this.count[0] >> 3 & 63U);
			this.count[0] += (uint)((uint)cbSize << 3);
			if ((ulong)this.count[0] < (ulong)((long)((long)cbSize << 3)))
			{
				this.count[1] += 1U;
			}
			this.count[1] += (uint)(cbSize >> 29);
			int num2 = 64 - num;
			int num3 = 0;
			if (cbSize >= num2)
			{
				Buffer.BlockCopy(array, ibStart, this.buffer, num, num2);
				this.MD4Transform(this.state, this.buffer, 0);
				num3 = num2;
				while (num3 + 63 < cbSize)
				{
					this.MD4Transform(this.state, array, num3);
					num3 += 64;
				}
				num = 0;
			}
			Buffer.BlockCopy(array, ibStart + num3, this.buffer, num, cbSize - num3);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		protected override byte[] HashFinal()
		{
			byte[] array = new byte[8];
			this.Encode(array, this.count);
			uint num = this.count[0] >> 3 & 63U;
			int num2 = (int)((num >= 56U) ? (120U - num) : (56U - num));
			this.HashCore(this.Padding(num2), 0, num2);
			this.HashCore(array, 0, 8);
			this.Encode(this.digest, this.state);
			this.Initialize();
			return this.digest;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000CB24 File Offset: 0x0000AD24
		private byte[] Padding(int nLength)
		{
			if (nLength > 0)
			{
				byte[] array = new byte[nLength];
				array[0] = 128;
				return array;
			}
			return null;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000CB4C File Offset: 0x0000AD4C
		private uint F(uint x, uint y, uint z)
		{
			return (x & y) | (~x & z);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000CB58 File Offset: 0x0000AD58
		private uint G(uint x, uint y, uint z)
		{
			return (x & y) | (x & z) | (y & z);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000CB68 File Offset: 0x0000AD68
		private uint H(uint x, uint y, uint z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000CB70 File Offset: 0x0000AD70
		private uint ROL(uint x, byte n)
		{
			return x << (int)n | x >> (int)(32 - n);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000CB84 File Offset: 0x0000AD84
		private void FF(ref uint a, uint b, uint c, uint d, uint x, byte s)
		{
			a += this.F(b, c, d) + x;
			a = this.ROL(a, s);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
		private void GG(ref uint a, uint b, uint c, uint d, uint x, byte s)
		{
			a += this.G(b, c, d) + x + 1518500249U;
			a = this.ROL(a, s);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
		private void HH(ref uint a, uint b, uint c, uint d, uint x, byte s)
		{
			a += this.H(b, c, d) + x + 1859775393U;
			a = this.ROL(a, s);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		private void Encode(byte[] output, uint[] input)
		{
			int num = 0;
			for (int i = 0; i < output.Length; i += 4)
			{
				output[i] = (byte)input[num];
				output[i + 1] = (byte)(input[num] >> 8);
				output[i + 2] = (byte)(input[num] >> 16);
				output[i + 3] = (byte)(input[num] >> 24);
				num++;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000CC60 File Offset: 0x0000AE60
		private void Decode(uint[] output, byte[] input, int index)
		{
			int i = 0;
			int num = index;
			while (i < output.Length)
			{
				output[i] = (uint)((int)input[num] | (int)input[num + 1] << 8 | (int)input[num + 2] << 16 | (int)input[num + 3] << 24);
				i++;
				num += 4;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000CCA8 File Offset: 0x0000AEA8
		private void MD4Transform(uint[] state, byte[] block, int index)
		{
			uint num = state[0];
			uint num2 = state[1];
			uint num3 = state[2];
			uint num4 = state[3];
			this.Decode(this.x, block, index);
			this.FF(ref num, num2, num3, num4, this.x[0], 3);
			this.FF(ref num4, num, num2, num3, this.x[1], 7);
			this.FF(ref num3, num4, num, num2, this.x[2], 11);
			this.FF(ref num2, num3, num4, num, this.x[3], 19);
			this.FF(ref num, num2, num3, num4, this.x[4], 3);
			this.FF(ref num4, num, num2, num3, this.x[5], 7);
			this.FF(ref num3, num4, num, num2, this.x[6], 11);
			this.FF(ref num2, num3, num4, num, this.x[7], 19);
			this.FF(ref num, num2, num3, num4, this.x[8], 3);
			this.FF(ref num4, num, num2, num3, this.x[9], 7);
			this.FF(ref num3, num4, num, num2, this.x[10], 11);
			this.FF(ref num2, num3, num4, num, this.x[11], 19);
			this.FF(ref num, num2, num3, num4, this.x[12], 3);
			this.FF(ref num4, num, num2, num3, this.x[13], 7);
			this.FF(ref num3, num4, num, num2, this.x[14], 11);
			this.FF(ref num2, num3, num4, num, this.x[15], 19);
			this.GG(ref num, num2, num3, num4, this.x[0], 3);
			this.GG(ref num4, num, num2, num3, this.x[4], 5);
			this.GG(ref num3, num4, num, num2, this.x[8], 9);
			this.GG(ref num2, num3, num4, num, this.x[12], 13);
			this.GG(ref num, num2, num3, num4, this.x[1], 3);
			this.GG(ref num4, num, num2, num3, this.x[5], 5);
			this.GG(ref num3, num4, num, num2, this.x[9], 9);
			this.GG(ref num2, num3, num4, num, this.x[13], 13);
			this.GG(ref num, num2, num3, num4, this.x[2], 3);
			this.GG(ref num4, num, num2, num3, this.x[6], 5);
			this.GG(ref num3, num4, num, num2, this.x[10], 9);
			this.GG(ref num2, num3, num4, num, this.x[14], 13);
			this.GG(ref num, num2, num3, num4, this.x[3], 3);
			this.GG(ref num4, num, num2, num3, this.x[7], 5);
			this.GG(ref num3, num4, num, num2, this.x[11], 9);
			this.GG(ref num2, num3, num4, num, this.x[15], 13);
			this.HH(ref num, num2, num3, num4, this.x[0], 3);
			this.HH(ref num4, num, num2, num3, this.x[8], 9);
			this.HH(ref num3, num4, num, num2, this.x[4], 11);
			this.HH(ref num2, num3, num4, num, this.x[12], 15);
			this.HH(ref num, num2, num3, num4, this.x[2], 3);
			this.HH(ref num4, num, num2, num3, this.x[10], 9);
			this.HH(ref num3, num4, num, num2, this.x[6], 11);
			this.HH(ref num2, num3, num4, num, this.x[14], 15);
			this.HH(ref num, num2, num3, num4, this.x[1], 3);
			this.HH(ref num4, num, num2, num3, this.x[9], 9);
			this.HH(ref num3, num4, num, num2, this.x[5], 11);
			this.HH(ref num2, num3, num4, num, this.x[13], 15);
			this.HH(ref num, num2, num3, num4, this.x[3], 3);
			this.HH(ref num4, num, num2, num3, this.x[11], 9);
			this.HH(ref num3, num4, num, num2, this.x[7], 11);
			this.HH(ref num2, num3, num4, num, this.x[15], 15);
			state[0] += num;
			state[1] += num2;
			state[2] += num3;
			state[3] += num4;
		}

		// Token: 0x040000CF RID: 207
		private const int S11 = 3;

		// Token: 0x040000D0 RID: 208
		private const int S12 = 7;

		// Token: 0x040000D1 RID: 209
		private const int S13 = 11;

		// Token: 0x040000D2 RID: 210
		private const int S14 = 19;

		// Token: 0x040000D3 RID: 211
		private const int S21 = 3;

		// Token: 0x040000D4 RID: 212
		private const int S22 = 5;

		// Token: 0x040000D5 RID: 213
		private const int S23 = 9;

		// Token: 0x040000D6 RID: 214
		private const int S24 = 13;

		// Token: 0x040000D7 RID: 215
		private const int S31 = 3;

		// Token: 0x040000D8 RID: 216
		private const int S32 = 9;

		// Token: 0x040000D9 RID: 217
		private const int S33 = 11;

		// Token: 0x040000DA RID: 218
		private const int S34 = 15;

		// Token: 0x040000DB RID: 219
		private uint[] state;

		// Token: 0x040000DC RID: 220
		private byte[] buffer;

		// Token: 0x040000DD RID: 221
		private uint[] count;

		// Token: 0x040000DE RID: 222
		private uint[] x;

		// Token: 0x040000DF RID: 223
		private byte[] digest;
	}
}
