using System;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x020005C7 RID: 1479
	internal class RC2Transform : SymmetricTransform
	{
		// Token: 0x06003897 RID: 14487 RVA: 0x000B83F8 File Offset: 0x000B65F8
		public RC2Transform(RC2 rc2Algo, bool encryption, byte[] key, byte[] iv) : base(rc2Algo, encryption, iv)
		{
			int num = rc2Algo.EffectiveKeySize;
			if (key == null)
			{
				key = KeyBuilder.Key(rc2Algo.KeySize >> 3);
			}
			else
			{
				key = (byte[])key.Clone();
				num = Math.Min(num, key.Length << 3);
			}
			int num2 = key.Length;
			if (!KeySizes.IsLegalKeySize(rc2Algo.LegalKeySizes, num2 << 3))
			{
				string text = Locale.GetText("Key is too small ({0} bytes), it should be between {1} and {2} bytes long.", new object[]
				{
					num2,
					5,
					16
				});
				throw new CryptographicException(text);
			}
			byte[] array = new byte[128];
			int num3 = num + 7 >> 3;
			int num4 = 255 % (2 << 8 + num - (num3 << 3) - 1);
			for (int i = 0; i < num2; i++)
			{
				array[i] = key[i];
			}
			for (int j = num2; j < 128; j++)
			{
				array[j] = RC2Transform.pitable[(int)(array[j - 1] + array[j - num2] & byte.MaxValue)];
			}
			array[128 - num3] = RC2Transform.pitable[(int)array[128 - num3] & num4];
			for (int k = 127 - num3; k >= 0; k--)
			{
				array[k] = RC2Transform.pitable[(int)(array[k + 1] ^ array[k + num3])];
			}
			this.K = new ushort[64];
			int num5 = 0;
			for (int l = 0; l < 64; l++)
			{
				this.K[l] = (ushort)((int)array[num5++] + ((int)array[num5++] << 8));
			}
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000B85C4 File Offset: 0x000B67C4
		protected override void ECB(byte[] input, byte[] output)
		{
			this.R0 = (ushort)((int)input[0] | (int)input[1] << 8);
			this.R1 = (ushort)((int)input[2] | (int)input[3] << 8);
			this.R2 = (ushort)((int)input[4] | (int)input[5] << 8);
			this.R3 = (ushort)((int)input[6] | (int)input[7] << 8);
			if (this.encrypt)
			{
				this.j = 0;
				while (this.j <= 16)
				{
					this.R0 += this.K[this.j++] + (this.R3 & this.R2) + (~this.R3 & this.R1);
					this.R0 = (ushort)((int)this.R0 << 1 | this.R0 >> 15);
					this.R1 += this.K[this.j++] + (this.R0 & this.R3) + (~this.R0 & this.R2);
					this.R1 = (ushort)((int)this.R1 << 2 | this.R1 >> 14);
					this.R2 += this.K[this.j++] + (this.R1 & this.R0) + (~this.R1 & this.R3);
					this.R2 = (ushort)((int)this.R2 << 3 | this.R2 >> 13);
					this.R3 += this.K[this.j++] + (this.R2 & this.R1) + (~this.R2 & this.R0);
					this.R3 = (ushort)((int)this.R3 << 5 | this.R3 >> 11);
				}
				this.R0 += this.K[(int)(this.R3 & 63)];
				this.R1 += this.K[(int)(this.R0 & 63)];
				this.R2 += this.K[(int)(this.R1 & 63)];
				this.R3 += this.K[(int)(this.R2 & 63)];
				while (this.j <= 40)
				{
					this.R0 += this.K[this.j++] + (this.R3 & this.R2) + (~this.R3 & this.R1);
					this.R0 = (ushort)((int)this.R0 << 1 | this.R0 >> 15);
					this.R1 += this.K[this.j++] + (this.R0 & this.R3) + (~this.R0 & this.R2);
					this.R1 = (ushort)((int)this.R1 << 2 | this.R1 >> 14);
					this.R2 += this.K[this.j++] + (this.R1 & this.R0) + (~this.R1 & this.R3);
					this.R2 = (ushort)((int)this.R2 << 3 | this.R2 >> 13);
					this.R3 += this.K[this.j++] + (this.R2 & this.R1) + (~this.R2 & this.R0);
					this.R3 = (ushort)((int)this.R3 << 5 | this.R3 >> 11);
				}
				this.R0 += this.K[(int)(this.R3 & 63)];
				this.R1 += this.K[(int)(this.R0 & 63)];
				this.R2 += this.K[(int)(this.R1 & 63)];
				this.R3 += this.K[(int)(this.R2 & 63)];
				while (this.j < 64)
				{
					this.R0 += this.K[this.j++] + (this.R3 & this.R2) + (~this.R3 & this.R1);
					this.R0 = (ushort)((int)this.R0 << 1 | this.R0 >> 15);
					this.R1 += this.K[this.j++] + (this.R0 & this.R3) + (~this.R0 & this.R2);
					this.R1 = (ushort)((int)this.R1 << 2 | this.R1 >> 14);
					this.R2 += this.K[this.j++] + (this.R1 & this.R0) + (~this.R1 & this.R3);
					this.R2 = (ushort)((int)this.R2 << 3 | this.R2 >> 13);
					this.R3 += this.K[this.j++] + (this.R2 & this.R1) + (~this.R2 & this.R0);
					this.R3 = (ushort)((int)this.R3 << 5 | this.R3 >> 11);
				}
			}
			else
			{
				this.j = 63;
				while (this.j >= 44)
				{
					this.R3 = (ushort)(this.R3 >> 5 | (int)this.R3 << 11);
					this.R3 -= this.K[this.j--] + (this.R2 & this.R1) + (~this.R2 & this.R0);
					this.R2 = (ushort)(this.R2 >> 3 | (int)this.R2 << 13);
					this.R2 -= this.K[this.j--] + (this.R1 & this.R0) + (~this.R1 & this.R3);
					this.R1 = (ushort)(this.R1 >> 2 | (int)this.R1 << 14);
					this.R1 -= this.K[this.j--] + (this.R0 & this.R3) + (~this.R0 & this.R2);
					this.R0 = (ushort)(this.R0 >> 1 | (int)this.R0 << 15);
					this.R0 -= this.K[this.j--] + (this.R3 & this.R2) + (~this.R3 & this.R1);
				}
				this.R3 -= this.K[(int)(this.R2 & 63)];
				this.R2 -= this.K[(int)(this.R1 & 63)];
				this.R1 -= this.K[(int)(this.R0 & 63)];
				this.R0 -= this.K[(int)(this.R3 & 63)];
				while (this.j >= 20)
				{
					this.R3 = (ushort)(this.R3 >> 5 | (int)this.R3 << 11);
					this.R3 -= this.K[this.j--] + (this.R2 & this.R1) + (~this.R2 & this.R0);
					this.R2 = (ushort)(this.R2 >> 3 | (int)this.R2 << 13);
					this.R2 -= this.K[this.j--] + (this.R1 & this.R0) + (~this.R1 & this.R3);
					this.R1 = (ushort)(this.R1 >> 2 | (int)this.R1 << 14);
					this.R1 -= this.K[this.j--] + (this.R0 & this.R3) + (~this.R0 & this.R2);
					this.R0 = (ushort)(this.R0 >> 1 | (int)this.R0 << 15);
					this.R0 -= this.K[this.j--] + (this.R3 & this.R2) + (~this.R3 & this.R1);
				}
				this.R3 -= this.K[(int)(this.R2 & 63)];
				this.R2 -= this.K[(int)(this.R1 & 63)];
				this.R1 -= this.K[(int)(this.R0 & 63)];
				this.R0 -= this.K[(int)(this.R3 & 63)];
				while (this.j >= 0)
				{
					this.R3 = (ushort)(this.R3 >> 5 | (int)this.R3 << 11);
					this.R3 -= this.K[this.j--] + (this.R2 & this.R1) + (~this.R2 & this.R0);
					this.R2 = (ushort)(this.R2 >> 3 | (int)this.R2 << 13);
					this.R2 -= this.K[this.j--] + (this.R1 & this.R0) + (~this.R1 & this.R3);
					this.R1 = (ushort)(this.R1 >> 2 | (int)this.R1 << 14);
					this.R1 -= this.K[this.j--] + (this.R0 & this.R3) + (~this.R0 & this.R2);
					this.R0 = (ushort)(this.R0 >> 1 | (int)this.R0 << 15);
					this.R0 -= this.K[this.j--] + (this.R3 & this.R2) + (~this.R3 & this.R1);
				}
			}
			output[0] = (byte)this.R0;
			output[1] = (byte)(this.R0 >> 8);
			output[2] = (byte)this.R1;
			output[3] = (byte)(this.R1 >> 8);
			output[4] = (byte)this.R2;
			output[5] = (byte)(this.R2 >> 8);
			output[6] = (byte)this.R3;
			output[7] = (byte)(this.R3 >> 8);
		}

		// Token: 0x0400188A RID: 6282
		private ushort R0;

		// Token: 0x0400188B RID: 6283
		private ushort R1;

		// Token: 0x0400188C RID: 6284
		private ushort R2;

		// Token: 0x0400188D RID: 6285
		private ushort R3;

		// Token: 0x0400188E RID: 6286
		private ushort[] K;

		// Token: 0x0400188F RID: 6287
		private int j;

		// Token: 0x04001890 RID: 6288
		private static readonly byte[] pitable = new byte[]
		{
			217,
			120,
			249,
			196,
			25,
			221,
			181,
			237,
			40,
			233,
			253,
			121,
			74,
			160,
			216,
			157,
			198,
			126,
			55,
			131,
			43,
			118,
			83,
			142,
			98,
			76,
			100,
			136,
			68,
			139,
			251,
			162,
			23,
			154,
			89,
			245,
			135,
			179,
			79,
			19,
			97,
			69,
			109,
			141,
			9,
			129,
			125,
			50,
			189,
			143,
			64,
			235,
			134,
			183,
			123,
			11,
			240,
			149,
			33,
			34,
			92,
			107,
			78,
			130,
			84,
			214,
			101,
			147,
			206,
			96,
			178,
			28,
			115,
			86,
			192,
			20,
			167,
			140,
			241,
			220,
			18,
			117,
			202,
			31,
			59,
			190,
			228,
			209,
			66,
			61,
			212,
			48,
			163,
			60,
			182,
			38,
			111,
			191,
			14,
			218,
			70,
			105,
			7,
			87,
			39,
			242,
			29,
			155,
			188,
			148,
			67,
			3,
			248,
			17,
			199,
			246,
			144,
			239,
			62,
			231,
			6,
			195,
			213,
			47,
			200,
			102,
			30,
			215,
			8,
			232,
			234,
			222,
			128,
			82,
			238,
			247,
			132,
			170,
			114,
			172,
			53,
			77,
			106,
			42,
			150,
			26,
			210,
			113,
			90,
			21,
			73,
			116,
			75,
			159,
			208,
			94,
			4,
			24,
			164,
			236,
			194,
			224,
			65,
			110,
			15,
			81,
			203,
			204,
			36,
			145,
			175,
			80,
			161,
			244,
			112,
			57,
			153,
			124,
			58,
			133,
			35,
			184,
			180,
			122,
			252,
			2,
			54,
			91,
			37,
			85,
			151,
			49,
			45,
			93,
			250,
			152,
			227,
			138,
			146,
			174,
			5,
			223,
			41,
			16,
			103,
			108,
			186,
			201,
			211,
			0,
			230,
			207,
			225,
			158,
			168,
			44,
			99,
			22,
			1,
			63,
			88,
			226,
			137,
			169,
			13,
			56,
			52,
			27,
			171,
			51,
			byte.MaxValue,
			176,
			187,
			72,
			12,
			95,
			185,
			177,
			205,
			46,
			197,
			243,
			219,
			71,
			229,
			165,
			156,
			119,
			10,
			166,
			32,
			104,
			254,
			127,
			193,
			173
		};
	}
}
