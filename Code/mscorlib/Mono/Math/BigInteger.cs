﻿using System;
using System.Security.Cryptography;
using Mono.Math.Prime;
using Mono.Math.Prime.Generator;

namespace Mono.Math
{
	// Token: 0x02000093 RID: 147
	internal class BigInteger
	{
		// Token: 0x06000882 RID: 2178 RVA: 0x0001FD04 File Offset: 0x0001DF04
		public BigInteger()
		{
			this.data = new uint[20];
			this.length = 20U;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001FD2C File Offset: 0x0001DF2C
		public BigInteger(BigInteger.Sign sign, uint len)
		{
			this.data = new uint[len];
			this.length = len;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001FD50 File Offset: 0x0001DF50
		public BigInteger(BigInteger bi)
		{
			this.data = (uint[])bi.data.Clone();
			this.length = bi.length;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001FD84 File Offset: 0x0001DF84
		public BigInteger(BigInteger bi, uint len)
		{
			this.data = new uint[len];
			for (uint num = 0U; num < bi.length; num += 1U)
			{
				this.data[(int)((UIntPtr)num)] = bi.data[(int)((UIntPtr)num)];
			}
			this.length = bi.length;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
		public BigInteger(byte[] inData)
		{
			this.length = (uint)inData.Length >> 2;
			int num = inData.Length & 3;
			if (num != 0)
			{
				this.length += 1U;
			}
			this.data = new uint[this.length];
			int i = inData.Length - 1;
			int num2 = 0;
			while (i >= 3)
			{
				this.data[num2] = (uint)((int)inData[i - 3] << 24 | (int)inData[i - 2] << 16 | (int)inData[i - 1] << 8 | (int)inData[i]);
				i -= 4;
				num2++;
			}
			switch (num)
			{
			case 1:
				this.data[(int)((UIntPtr)(this.length - 1U))] = (uint)inData[0];
				break;
			case 2:
				this.data[(int)((UIntPtr)(this.length - 1U))] = (uint)((int)inData[0] << 8 | (int)inData[1]);
				break;
			case 3:
				this.data[(int)((UIntPtr)(this.length - 1U))] = (uint)((int)inData[0] << 16 | (int)inData[1] << 8 | (int)inData[2]);
				break;
			}
			this.Normalize();
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001FEF0 File Offset: 0x0001E0F0
		public BigInteger(uint[] inData)
		{
			this.length = (uint)inData.Length;
			this.data = new uint[this.length];
			int i = (int)(this.length - 1U);
			int num = 0;
			while (i >= 0)
			{
				this.data[num] = inData[i];
				i--;
				num++;
			}
			this.Normalize();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001FF58 File Offset: 0x0001E158
		public BigInteger(uint ui)
		{
			this.data = new uint[]
			{
				ui
			};
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001FF78 File Offset: 0x0001E178
		public BigInteger(ulong ul)
		{
			this.data = new uint[]
			{
				(uint)ul,
				(uint)(ul >> 32)
			};
			this.length = 2U;
			this.Normalize();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001FFD4 File Offset: 0x0001E1D4
		public static BigInteger Parse(string number)
		{
			if (number == null)
			{
				throw new ArgumentNullException("number");
			}
			int i = 0;
			int num = number.Length;
			bool flag = false;
			BigInteger bigInteger = new BigInteger(0U);
			if (number[i] == '+')
			{
				i++;
			}
			else if (number[i] == '-')
			{
				throw new FormatException("Operation would return a negative value");
			}
			while (i < num)
			{
				char c = number[i];
				if (c == '\0')
				{
					i = num;
				}
				else if (c >= '0' && c <= '9')
				{
					bigInteger = bigInteger * 10 + (int)(c - '0');
					flag = true;
				}
				else
				{
					if (char.IsWhiteSpace(c))
					{
						for (i++; i < num; i++)
						{
							if (!char.IsWhiteSpace(number[i]))
							{
								throw new FormatException();
							}
						}
						break;
					}
					throw new FormatException();
				}
				i++;
			}
			if (!flag)
			{
				throw new FormatException();
			}
			return bigInteger;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000200DC File Offset: 0x0001E2DC
		public static BigInteger Add(BigInteger bi1, BigInteger bi2)
		{
			return bi1 + bi2;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000200E8 File Offset: 0x0001E2E8
		public static BigInteger Subtract(BigInteger bi1, BigInteger bi2)
		{
			return bi1 - bi2;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000200F4 File Offset: 0x0001E2F4
		public static int Modulus(BigInteger bi, int i)
		{
			return bi % i;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00020100 File Offset: 0x0001E300
		public static uint Modulus(BigInteger bi, uint ui)
		{
			return bi % ui;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002010C File Offset: 0x0001E30C
		public static BigInteger Modulus(BigInteger bi1, BigInteger bi2)
		{
			return bi1 % bi2;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00020118 File Offset: 0x0001E318
		public static BigInteger Divid(BigInteger bi, int i)
		{
			return bi / i;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00020124 File Offset: 0x0001E324
		public static BigInteger Divid(BigInteger bi1, BigInteger bi2)
		{
			return bi1 / bi2;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00020130 File Offset: 0x0001E330
		public static BigInteger Multiply(BigInteger bi1, BigInteger bi2)
		{
			return bi1 * bi2;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0002013C File Offset: 0x0001E33C
		public static BigInteger Multiply(BigInteger bi, int i)
		{
			return bi * i;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00020148 File Offset: 0x0001E348
		private static RandomNumberGenerator Rng
		{
			get
			{
				if (BigInteger.rng == null)
				{
					BigInteger.rng = RandomNumberGenerator.Create();
				}
				return BigInteger.rng;
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00020164 File Offset: 0x0001E364
		public static BigInteger GenerateRandom(int bits, RandomNumberGenerator rng)
		{
			int num = bits >> 5;
			int num2 = bits & 31;
			if (num2 != 0)
			{
				num++;
			}
			BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, (uint)(num + 1));
			byte[] src = new byte[num << 2];
			rng.GetBytes(src);
			Buffer.BlockCopy(src, 0, bigInteger.data, 0, num << 2);
			if (num2 != 0)
			{
				uint num3 = 1U << num2 - 1;
				bigInteger.data[num - 1] |= num3;
				num3 = uint.MaxValue >> 32 - num2;
				bigInteger.data[num - 1] &= num3;
			}
			else
			{
				bigInteger.data[num - 1] |= 2147483648U;
			}
			bigInteger.Normalize();
			return bigInteger;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00020218 File Offset: 0x0001E418
		public static BigInteger GenerateRandom(int bits)
		{
			return BigInteger.GenerateRandom(bits, BigInteger.Rng);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00020228 File Offset: 0x0001E428
		public void Randomize(RandomNumberGenerator rng)
		{
			if (this == 0U)
			{
				return;
			}
			int num = this.BitCount();
			int num2 = num >> 5;
			int num3 = num & 31;
			if (num3 != 0)
			{
				num2++;
			}
			byte[] src = new byte[num2 << 2];
			rng.GetBytes(src);
			Buffer.BlockCopy(src, 0, this.data, 0, num2 << 2);
			if (num3 != 0)
			{
				uint num4 = 1U << num3 - 1;
				this.data[num2 - 1] |= num4;
				num4 = uint.MaxValue >> 32 - num3;
				this.data[num2 - 1] &= num4;
			}
			else
			{
				this.data[num2 - 1] |= 2147483648U;
			}
			this.Normalize();
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000202E4 File Offset: 0x0001E4E4
		public void Randomize()
		{
			this.Randomize(BigInteger.Rng);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x000202F4 File Offset: 0x0001E4F4
		public int BitCount()
		{
			this.Normalize();
			uint num = this.data[(int)((UIntPtr)(this.length - 1U))];
			uint num2 = 2147483648U;
			uint num3 = 32U;
			while (num3 > 0U && (num & num2) == 0U)
			{
				num3 -= 1U;
				num2 >>= 1;
			}
			return (int)(num3 + (this.length - 1U << 5));
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0002034C File Offset: 0x0001E54C
		public bool TestBit(uint bitNum)
		{
			uint num = bitNum >> 5;
			byte b = (byte)(bitNum & 31U);
			uint num2 = 1U << (int)b;
			return (this.data[(int)((UIntPtr)num)] & num2) != 0U;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0002037C File Offset: 0x0001E57C
		public bool TestBit(int bitNum)
		{
			if (bitNum < 0)
			{
				throw new IndexOutOfRangeException("bitNum out of range");
			}
			uint num = (uint)bitNum >> 5;
			byte b = (byte)(bitNum & 31);
			uint num2 = 1U << (int)b;
			return (this.data[(int)((UIntPtr)num)] | num2) == this.data[(int)((UIntPtr)num)];
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000203C4 File Offset: 0x0001E5C4
		public void SetBit(uint bitNum)
		{
			this.SetBit(bitNum, true);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x000203D0 File Offset: 0x0001E5D0
		public void ClearBit(uint bitNum)
		{
			this.SetBit(bitNum, false);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000203DC File Offset: 0x0001E5DC
		public void SetBit(uint bitNum, bool value)
		{
			uint num = bitNum >> 5;
			if (num < this.length)
			{
				uint num2 = 1U << (int)bitNum;
				if (value)
				{
					this.data[(int)((UIntPtr)num)] |= num2;
				}
				else
				{
					this.data[(int)((UIntPtr)num)] &= ~num2;
				}
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00020434 File Offset: 0x0001E634
		public int LowestSetBit()
		{
			if (this == 0U)
			{
				return -1;
			}
			int num = 0;
			while (!this.TestBit(num))
			{
				num++;
			}
			return num;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00020468 File Offset: 0x0001E668
		public byte[] GetBytes()
		{
			if (this == 0U)
			{
				return new byte[1];
			}
			int num = this.BitCount();
			int num2 = num >> 3;
			if ((num & 7) != 0)
			{
				num2++;
			}
			byte[] array = new byte[num2];
			int num3 = num2 & 3;
			if (num3 == 0)
			{
				num3 = 4;
			}
			int num4 = 0;
			for (int i = (int)(this.length - 1U); i >= 0; i--)
			{
				uint num5 = this.data[i];
				for (int j = num3 - 1; j >= 0; j--)
				{
					array[num4 + j] = (byte)(num5 & 255U);
					num5 >>= 8;
				}
				num4 += num3;
				num3 = 4;
			}
			return array;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00020514 File Offset: 0x0001E714
		public BigInteger.Sign Compare(BigInteger bi)
		{
			return BigInteger.Kernel.Compare(this, bi);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00020520 File Offset: 0x0001E720
		public string ToString(uint radix)
		{
			return this.ToString(radix, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00020530 File Offset: 0x0001E730
		public string ToString(uint radix, string characterSet)
		{
			if ((long)characterSet.Length < (long)((ulong)radix))
			{
				throw new ArgumentException("charSet length less than radix", "characterSet");
			}
			if (radix == 1U)
			{
				throw new ArgumentException("There is no such thing as radix one notation", "radix");
			}
			if (this == 0U)
			{
				return "0";
			}
			if (this == 1U)
			{
				return "1";
			}
			string text = string.Empty;
			BigInteger bigInteger = new BigInteger(this);
			while (bigInteger != 0U)
			{
				uint index = BigInteger.Kernel.SingleByteDivideInPlace(bigInteger, radix);
				text = characterSet[(int)index] + text;
			}
			return text;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000205D0 File Offset: 0x0001E7D0
		private void Normalize()
		{
			while (this.length > 0U && this.data[(int)((UIntPtr)(this.length - 1U))] == 0U)
			{
				this.length -= 1U;
			}
			if (this.length == 0U)
			{
				this.length += 1U;
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002062C File Offset: 0x0001E82C
		public void Clear()
		{
			int num = 0;
			while ((long)num < (long)((ulong)this.length))
			{
				this.data[num] = 0U;
				num++;
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002065C File Offset: 0x0001E85C
		public override int GetHashCode()
		{
			uint num = 0U;
			for (uint num2 = 0U; num2 < this.length; num2 += 1U)
			{
				num ^= this.data[(int)((UIntPtr)num2)];
			}
			return (int)num;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00020690 File Offset: 0x0001E890
		public override string ToString()
		{
			return this.ToString(10U);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002069C File Offset: 0x0001E89C
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			if (o is int)
			{
				return (int)o >= 0 && this == (uint)o;
			}
			BigInteger bigInteger = o as BigInteger;
			return !(bigInteger == null) && BigInteger.Kernel.Compare(this, bigInteger) == BigInteger.Sign.Zero;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000206F8 File Offset: 0x0001E8F8
		public BigInteger GCD(BigInteger bi)
		{
			return BigInteger.Kernel.gcd(this, bi);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00020704 File Offset: 0x0001E904
		public BigInteger ModInverse(BigInteger modulus)
		{
			return BigInteger.Kernel.modInverse(this, modulus);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00020710 File Offset: 0x0001E910
		public BigInteger ModPow(BigInteger exp, BigInteger n)
		{
			BigInteger.ModulusRing modulusRing = new BigInteger.ModulusRing(n);
			return modulusRing.Pow(this, exp);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0002072C File Offset: 0x0001E92C
		public bool IsProbablePrime()
		{
			if (this <= BigInteger.smallPrimes[BigInteger.smallPrimes.Length - 1])
			{
				for (int i = 0; i < BigInteger.smallPrimes.Length; i++)
				{
					if (this == BigInteger.smallPrimes[i])
					{
						return true;
					}
				}
				return false;
			}
			for (int j = 0; j < BigInteger.smallPrimes.Length; j++)
			{
				if (this % BigInteger.smallPrimes[j] == 0U)
				{
					return false;
				}
			}
			return PrimalityTests.Test(this, ConfidenceFactor.Medium);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000207BC File Offset: 0x0001E9BC
		public static BigInteger NextHighestPrime(BigInteger bi)
		{
			NextPrimeFinder nextPrimeFinder = new NextPrimeFinder();
			return nextPrimeFinder.GenerateNewPrime(0, bi);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x000207D8 File Offset: 0x0001E9D8
		public static BigInteger GeneratePseudoPrime(int bits)
		{
			SequentialSearchPrimeGeneratorBase sequentialSearchPrimeGeneratorBase = new SequentialSearchPrimeGeneratorBase();
			return sequentialSearchPrimeGeneratorBase.GenerateNewPrime(bits);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000207F4 File Offset: 0x0001E9F4
		public void Incr2()
		{
			int num = 0;
			this.data[0] += 2U;
			if (this.data[0] < 2U)
			{
				this.data[++num] += 1U;
				while (this.data[num++] == 0U)
				{
					this.data[num] += 1U;
				}
				if (this.length == (uint)num)
				{
					this.length += 1U;
				}
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00020878 File Offset: 0x0001EA78
		public static implicit operator BigInteger(uint value)
		{
			return new BigInteger(value);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00020880 File Offset: 0x0001EA80
		public static implicit operator BigInteger(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			return new BigInteger((uint)value);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0002089C File Offset: 0x0001EA9C
		public static implicit operator BigInteger(ulong value)
		{
			return new BigInteger(value);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000208A4 File Offset: 0x0001EAA4
		public static BigInteger operator +(BigInteger bi1, BigInteger bi2)
		{
			if (bi1 == 0U)
			{
				return new BigInteger(bi2);
			}
			if (bi2 == 0U)
			{
				return new BigInteger(bi1);
			}
			return BigInteger.Kernel.AddSameSign(bi1, bi2);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000208D4 File Offset: 0x0001EAD4
		public static BigInteger operator -(BigInteger bi1, BigInteger bi2)
		{
			if (bi2 == 0U)
			{
				return new BigInteger(bi1);
			}
			if (bi1 == 0U)
			{
				throw new ArithmeticException("Operation would return a negative value");
			}
			BigInteger.Sign sign = BigInteger.Kernel.Compare(bi1, bi2);
			switch (sign + 1)
			{
			case BigInteger.Sign.Zero:
				throw new ArithmeticException("Operation would return a negative value");
			case BigInteger.Sign.Positive:
				return 0;
			case (BigInteger.Sign)2:
				return BigInteger.Kernel.Subtract(bi1, bi2);
			default:
				throw new Exception();
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0002094C File Offset: 0x0001EB4C
		public static int operator %(BigInteger bi, int i)
		{
			if (i > 0)
			{
				return (int)BigInteger.Kernel.DwordMod(bi, (uint)i);
			}
			return (int)(-(int)BigInteger.Kernel.DwordMod(bi, (uint)(-(uint)i)));
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00020968 File Offset: 0x0001EB68
		public static uint operator %(BigInteger bi, uint ui)
		{
			return BigInteger.Kernel.DwordMod(bi, ui);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00020974 File Offset: 0x0001EB74
		public static BigInteger operator %(BigInteger bi1, BigInteger bi2)
		{
			return BigInteger.Kernel.multiByteDivide(bi1, bi2)[1];
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00020980 File Offset: 0x0001EB80
		public static BigInteger operator /(BigInteger bi, int i)
		{
			if (i > 0)
			{
				return BigInteger.Kernel.DwordDiv(bi, (uint)i);
			}
			throw new ArithmeticException("Operation would return a negative value");
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002099C File Offset: 0x0001EB9C
		public static BigInteger operator /(BigInteger bi1, BigInteger bi2)
		{
			return BigInteger.Kernel.multiByteDivide(bi1, bi2)[0];
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000209A8 File Offset: 0x0001EBA8
		public static BigInteger operator *(BigInteger bi1, BigInteger bi2)
		{
			if (bi1 == 0U || bi2 == 0U)
			{
				return 0;
			}
			if ((long)bi1.data.Length < (long)((ulong)bi1.length))
			{
				throw new IndexOutOfRangeException("bi1 out of range");
			}
			if ((long)bi2.data.Length < (long)((ulong)bi2.length))
			{
				throw new IndexOutOfRangeException("bi2 out of range");
			}
			BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, bi1.length + bi2.length);
			BigInteger.Kernel.Multiply(bi1.data, 0U, bi1.length, bi2.data, 0U, bi2.length, bigInteger.data, 0U);
			bigInteger.Normalize();
			return bigInteger;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00020A58 File Offset: 0x0001EC58
		public static BigInteger operator *(BigInteger bi, int i)
		{
			if (i < 0)
			{
				throw new ArithmeticException("Operation would return a negative value");
			}
			if (i == 0)
			{
				return 0;
			}
			if (i == 1)
			{
				return new BigInteger(bi);
			}
			return BigInteger.Kernel.MultiplyByDword(bi, (uint)i);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00020A9C File Offset: 0x0001EC9C
		public static BigInteger operator <<(BigInteger bi1, int shiftVal)
		{
			return BigInteger.Kernel.LeftShift(bi1, shiftVal);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00020AA8 File Offset: 0x0001ECA8
		public static BigInteger operator >>(BigInteger bi1, int shiftVal)
		{
			return BigInteger.Kernel.RightShift(bi1, shiftVal);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00020AB4 File Offset: 0x0001ECB4
		public static bool operator ==(BigInteger bi1, uint ui)
		{
			if (bi1.length != 1U)
			{
				bi1.Normalize();
			}
			return bi1.length == 1U && bi1.data[0] == ui;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00020AF0 File Offset: 0x0001ECF0
		public static bool operator !=(BigInteger bi1, uint ui)
		{
			if (bi1.length != 1U)
			{
				bi1.Normalize();
			}
			return bi1.length != 1U || bi1.data[0] != ui;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00020B24 File Offset: 0x0001ED24
		public static bool operator ==(BigInteger bi1, BigInteger bi2)
		{
			return bi1 == bi2 || (!(null == bi1) && !(null == bi2) && BigInteger.Kernel.Compare(bi1, bi2) == BigInteger.Sign.Zero);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00020B54 File Offset: 0x0001ED54
		public static bool operator !=(BigInteger bi1, BigInteger bi2)
		{
			return bi1 != bi2 && (null == bi1 || null == bi2 || BigInteger.Kernel.Compare(bi1, bi2) != BigInteger.Sign.Zero);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00020B94 File Offset: 0x0001ED94
		public static bool operator >(BigInteger bi1, BigInteger bi2)
		{
			return BigInteger.Kernel.Compare(bi1, bi2) > BigInteger.Sign.Zero;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00020BA0 File Offset: 0x0001EDA0
		public static bool operator <(BigInteger bi1, BigInteger bi2)
		{
			return BigInteger.Kernel.Compare(bi1, bi2) < BigInteger.Sign.Zero;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00020BAC File Offset: 0x0001EDAC
		public static bool operator >=(BigInteger bi1, BigInteger bi2)
		{
			return BigInteger.Kernel.Compare(bi1, bi2) >= BigInteger.Sign.Zero;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00020BBC File Offset: 0x0001EDBC
		public static bool operator <=(BigInteger bi1, BigInteger bi2)
		{
			return BigInteger.Kernel.Compare(bi1, bi2) <= BigInteger.Sign.Zero;
		}

		// Token: 0x040001B4 RID: 436
		private const uint DEFAULT_LEN = 20U;

		// Token: 0x040001B5 RID: 437
		private const string WouldReturnNegVal = "Operation would return a negative value";

		// Token: 0x040001B6 RID: 438
		private uint length = 1U;

		// Token: 0x040001B7 RID: 439
		private uint[] data;

		// Token: 0x040001B8 RID: 440
		internal static readonly uint[] smallPrimes = new uint[]
		{
			2U,
			3U,
			5U,
			7U,
			11U,
			13U,
			17U,
			19U,
			23U,
			29U,
			31U,
			37U,
			41U,
			43U,
			47U,
			53U,
			59U,
			61U,
			67U,
			71U,
			73U,
			79U,
			83U,
			89U,
			97U,
			101U,
			103U,
			107U,
			109U,
			113U,
			127U,
			131U,
			137U,
			139U,
			149U,
			151U,
			157U,
			163U,
			167U,
			173U,
			179U,
			181U,
			191U,
			193U,
			197U,
			199U,
			211U,
			223U,
			227U,
			229U,
			233U,
			239U,
			241U,
			251U,
			257U,
			263U,
			269U,
			271U,
			277U,
			281U,
			283U,
			293U,
			307U,
			311U,
			313U,
			317U,
			331U,
			337U,
			347U,
			349U,
			353U,
			359U,
			367U,
			373U,
			379U,
			383U,
			389U,
			397U,
			401U,
			409U,
			419U,
			421U,
			431U,
			433U,
			439U,
			443U,
			449U,
			457U,
			461U,
			463U,
			467U,
			479U,
			487U,
			491U,
			499U,
			503U,
			509U,
			521U,
			523U,
			541U,
			547U,
			557U,
			563U,
			569U,
			571U,
			577U,
			587U,
			593U,
			599U,
			601U,
			607U,
			613U,
			617U,
			619U,
			631U,
			641U,
			643U,
			647U,
			653U,
			659U,
			661U,
			673U,
			677U,
			683U,
			691U,
			701U,
			709U,
			719U,
			727U,
			733U,
			739U,
			743U,
			751U,
			757U,
			761U,
			769U,
			773U,
			787U,
			797U,
			809U,
			811U,
			821U,
			823U,
			827U,
			829U,
			839U,
			853U,
			857U,
			859U,
			863U,
			877U,
			881U,
			883U,
			887U,
			907U,
			911U,
			919U,
			929U,
			937U,
			941U,
			947U,
			953U,
			967U,
			971U,
			977U,
			983U,
			991U,
			997U,
			1009U,
			1013U,
			1019U,
			1021U,
			1031U,
			1033U,
			1039U,
			1049U,
			1051U,
			1061U,
			1063U,
			1069U,
			1087U,
			1091U,
			1093U,
			1097U,
			1103U,
			1109U,
			1117U,
			1123U,
			1129U,
			1151U,
			1153U,
			1163U,
			1171U,
			1181U,
			1187U,
			1193U,
			1201U,
			1213U,
			1217U,
			1223U,
			1229U,
			1231U,
			1237U,
			1249U,
			1259U,
			1277U,
			1279U,
			1283U,
			1289U,
			1291U,
			1297U,
			1301U,
			1303U,
			1307U,
			1319U,
			1321U,
			1327U,
			1361U,
			1367U,
			1373U,
			1381U,
			1399U,
			1409U,
			1423U,
			1427U,
			1429U,
			1433U,
			1439U,
			1447U,
			1451U,
			1453U,
			1459U,
			1471U,
			1481U,
			1483U,
			1487U,
			1489U,
			1493U,
			1499U,
			1511U,
			1523U,
			1531U,
			1543U,
			1549U,
			1553U,
			1559U,
			1567U,
			1571U,
			1579U,
			1583U,
			1597U,
			1601U,
			1607U,
			1609U,
			1613U,
			1619U,
			1621U,
			1627U,
			1637U,
			1657U,
			1663U,
			1667U,
			1669U,
			1693U,
			1697U,
			1699U,
			1709U,
			1721U,
			1723U,
			1733U,
			1741U,
			1747U,
			1753U,
			1759U,
			1777U,
			1783U,
			1787U,
			1789U,
			1801U,
			1811U,
			1823U,
			1831U,
			1847U,
			1861U,
			1867U,
			1871U,
			1873U,
			1877U,
			1879U,
			1889U,
			1901U,
			1907U,
			1913U,
			1931U,
			1933U,
			1949U,
			1951U,
			1973U,
			1979U,
			1987U,
			1993U,
			1997U,
			1999U,
			2003U,
			2011U,
			2017U,
			2027U,
			2029U,
			2039U,
			2053U,
			2063U,
			2069U,
			2081U,
			2083U,
			2087U,
			2089U,
			2099U,
			2111U,
			2113U,
			2129U,
			2131U,
			2137U,
			2141U,
			2143U,
			2153U,
			2161U,
			2179U,
			2203U,
			2207U,
			2213U,
			2221U,
			2237U,
			2239U,
			2243U,
			2251U,
			2267U,
			2269U,
			2273U,
			2281U,
			2287U,
			2293U,
			2297U,
			2309U,
			2311U,
			2333U,
			2339U,
			2341U,
			2347U,
			2351U,
			2357U,
			2371U,
			2377U,
			2381U,
			2383U,
			2389U,
			2393U,
			2399U,
			2411U,
			2417U,
			2423U,
			2437U,
			2441U,
			2447U,
			2459U,
			2467U,
			2473U,
			2477U,
			2503U,
			2521U,
			2531U,
			2539U,
			2543U,
			2549U,
			2551U,
			2557U,
			2579U,
			2591U,
			2593U,
			2609U,
			2617U,
			2621U,
			2633U,
			2647U,
			2657U,
			2659U,
			2663U,
			2671U,
			2677U,
			2683U,
			2687U,
			2689U,
			2693U,
			2699U,
			2707U,
			2711U,
			2713U,
			2719U,
			2729U,
			2731U,
			2741U,
			2749U,
			2753U,
			2767U,
			2777U,
			2789U,
			2791U,
			2797U,
			2801U,
			2803U,
			2819U,
			2833U,
			2837U,
			2843U,
			2851U,
			2857U,
			2861U,
			2879U,
			2887U,
			2897U,
			2903U,
			2909U,
			2917U,
			2927U,
			2939U,
			2953U,
			2957U,
			2963U,
			2969U,
			2971U,
			2999U,
			3001U,
			3011U,
			3019U,
			3023U,
			3037U,
			3041U,
			3049U,
			3061U,
			3067U,
			3079U,
			3083U,
			3089U,
			3109U,
			3119U,
			3121U,
			3137U,
			3163U,
			3167U,
			3169U,
			3181U,
			3187U,
			3191U,
			3203U,
			3209U,
			3217U,
			3221U,
			3229U,
			3251U,
			3253U,
			3257U,
			3259U,
			3271U,
			3299U,
			3301U,
			3307U,
			3313U,
			3319U,
			3323U,
			3329U,
			3331U,
			3343U,
			3347U,
			3359U,
			3361U,
			3371U,
			3373U,
			3389U,
			3391U,
			3407U,
			3413U,
			3433U,
			3449U,
			3457U,
			3461U,
			3463U,
			3467U,
			3469U,
			3491U,
			3499U,
			3511U,
			3517U,
			3527U,
			3529U,
			3533U,
			3539U,
			3541U,
			3547U,
			3557U,
			3559U,
			3571U,
			3581U,
			3583U,
			3593U,
			3607U,
			3613U,
			3617U,
			3623U,
			3631U,
			3637U,
			3643U,
			3659U,
			3671U,
			3673U,
			3677U,
			3691U,
			3697U,
			3701U,
			3709U,
			3719U,
			3727U,
			3733U,
			3739U,
			3761U,
			3767U,
			3769U,
			3779U,
			3793U,
			3797U,
			3803U,
			3821U,
			3823U,
			3833U,
			3847U,
			3851U,
			3853U,
			3863U,
			3877U,
			3881U,
			3889U,
			3907U,
			3911U,
			3917U,
			3919U,
			3923U,
			3929U,
			3931U,
			3943U,
			3947U,
			3967U,
			3989U,
			4001U,
			4003U,
			4007U,
			4013U,
			4019U,
			4021U,
			4027U,
			4049U,
			4051U,
			4057U,
			4073U,
			4079U,
			4091U,
			4093U,
			4099U,
			4111U,
			4127U,
			4129U,
			4133U,
			4139U,
			4153U,
			4157U,
			4159U,
			4177U,
			4201U,
			4211U,
			4217U,
			4219U,
			4229U,
			4231U,
			4241U,
			4243U,
			4253U,
			4259U,
			4261U,
			4271U,
			4273U,
			4283U,
			4289U,
			4297U,
			4327U,
			4337U,
			4339U,
			4349U,
			4357U,
			4363U,
			4373U,
			4391U,
			4397U,
			4409U,
			4421U,
			4423U,
			4441U,
			4447U,
			4451U,
			4457U,
			4463U,
			4481U,
			4483U,
			4493U,
			4507U,
			4513U,
			4517U,
			4519U,
			4523U,
			4547U,
			4549U,
			4561U,
			4567U,
			4583U,
			4591U,
			4597U,
			4603U,
			4621U,
			4637U,
			4639U,
			4643U,
			4649U,
			4651U,
			4657U,
			4663U,
			4673U,
			4679U,
			4691U,
			4703U,
			4721U,
			4723U,
			4729U,
			4733U,
			4751U,
			4759U,
			4783U,
			4787U,
			4789U,
			4793U,
			4799U,
			4801U,
			4813U,
			4817U,
			4831U,
			4861U,
			4871U,
			4877U,
			4889U,
			4903U,
			4909U,
			4919U,
			4931U,
			4933U,
			4937U,
			4943U,
			4951U,
			4957U,
			4967U,
			4969U,
			4973U,
			4987U,
			4993U,
			4999U,
			5003U,
			5009U,
			5011U,
			5021U,
			5023U,
			5039U,
			5051U,
			5059U,
			5077U,
			5081U,
			5087U,
			5099U,
			5101U,
			5107U,
			5113U,
			5119U,
			5147U,
			5153U,
			5167U,
			5171U,
			5179U,
			5189U,
			5197U,
			5209U,
			5227U,
			5231U,
			5233U,
			5237U,
			5261U,
			5273U,
			5279U,
			5281U,
			5297U,
			5303U,
			5309U,
			5323U,
			5333U,
			5347U,
			5351U,
			5381U,
			5387U,
			5393U,
			5399U,
			5407U,
			5413U,
			5417U,
			5419U,
			5431U,
			5437U,
			5441U,
			5443U,
			5449U,
			5471U,
			5477U,
			5479U,
			5483U,
			5501U,
			5503U,
			5507U,
			5519U,
			5521U,
			5527U,
			5531U,
			5557U,
			5563U,
			5569U,
			5573U,
			5581U,
			5591U,
			5623U,
			5639U,
			5641U,
			5647U,
			5651U,
			5653U,
			5657U,
			5659U,
			5669U,
			5683U,
			5689U,
			5693U,
			5701U,
			5711U,
			5717U,
			5737U,
			5741U,
			5743U,
			5749U,
			5779U,
			5783U,
			5791U,
			5801U,
			5807U,
			5813U,
			5821U,
			5827U,
			5839U,
			5843U,
			5849U,
			5851U,
			5857U,
			5861U,
			5867U,
			5869U,
			5879U,
			5881U,
			5897U,
			5903U,
			5923U,
			5927U,
			5939U,
			5953U,
			5981U,
			5987U
		};

		// Token: 0x040001B9 RID: 441
		private static RandomNumberGenerator rng;

		// Token: 0x02000094 RID: 148
		public enum Sign
		{
			// Token: 0x040001BB RID: 443
			Negative = -1,
			// Token: 0x040001BC RID: 444
			Zero,
			// Token: 0x040001BD RID: 445
			Positive
		}

		// Token: 0x02000095 RID: 149
		internal sealed class ModulusRing
		{
			// Token: 0x060008C7 RID: 2247 RVA: 0x00020BCC File Offset: 0x0001EDCC
			public ModulusRing(BigInteger modulus)
			{
				this.mod = modulus;
				uint num = this.mod.length << 1;
				this.constant = new BigInteger(BigInteger.Sign.Positive, num + 1U);
				this.constant.data[(int)((UIntPtr)num)] = 1U;
				this.constant /= this.mod;
			}

			// Token: 0x060008C8 RID: 2248 RVA: 0x00020C2C File Offset: 0x0001EE2C
			public void BarrettReduction(BigInteger x)
			{
				BigInteger bigInteger = this.mod;
				uint length = bigInteger.length;
				uint num = length + 1U;
				uint num2 = length - 1U;
				if (x.length < length)
				{
					return;
				}
				if ((long)x.data.Length < (long)((ulong)x.length))
				{
					throw new IndexOutOfRangeException("x out of range");
				}
				BigInteger bigInteger2 = new BigInteger(BigInteger.Sign.Positive, x.length - num2 + this.constant.length);
				BigInteger.Kernel.Multiply(x.data, num2, x.length - num2, this.constant.data, 0U, this.constant.length, bigInteger2.data, 0U);
				uint length2 = (x.length <= num) ? x.length : num;
				x.length = length2;
				x.Normalize();
				BigInteger bigInteger3 = new BigInteger(BigInteger.Sign.Positive, num);
				BigInteger.Kernel.MultiplyMod2p32pmod(bigInteger2.data, (int)num, (int)(bigInteger2.length - num), bigInteger.data, 0, (int)bigInteger.length, bigInteger3.data, 0, (int)num);
				bigInteger3.Normalize();
				if (bigInteger3 <= x)
				{
					BigInteger.Kernel.MinusEq(x, bigInteger3);
				}
				else
				{
					BigInteger bigInteger4 = new BigInteger(BigInteger.Sign.Positive, num + 1U);
					bigInteger4.data[(int)((UIntPtr)num)] = 1U;
					BigInteger.Kernel.MinusEq(bigInteger4, bigInteger3);
					BigInteger.Kernel.PlusEq(x, bigInteger4);
				}
				while (x >= bigInteger)
				{
					BigInteger.Kernel.MinusEq(x, bigInteger);
				}
			}

			// Token: 0x060008C9 RID: 2249 RVA: 0x00020D88 File Offset: 0x0001EF88
			public BigInteger Multiply(BigInteger a, BigInteger b)
			{
				if (a == 0U || b == 0U)
				{
					return 0;
				}
				if (a > this.mod)
				{
					a %= this.mod;
				}
				if (b > this.mod)
				{
					b %= this.mod;
				}
				BigInteger bigInteger = a * b;
				this.BarrettReduction(bigInteger);
				return bigInteger;
			}

			// Token: 0x060008CA RID: 2250 RVA: 0x00020E04 File Offset: 0x0001F004
			public BigInteger Difference(BigInteger a, BigInteger b)
			{
				BigInteger.Sign sign = BigInteger.Kernel.Compare(a, b);
				BigInteger.Sign sign2 = sign;
				BigInteger bigInteger;
				switch (sign2 + 1)
				{
				case BigInteger.Sign.Zero:
					bigInteger = b - a;
					break;
				case BigInteger.Sign.Positive:
					return 0;
				case (BigInteger.Sign)2:
					bigInteger = a - b;
					break;
				default:
					throw new Exception();
				}
				if (bigInteger >= this.mod)
				{
					if (bigInteger.length >= this.mod.length << 1)
					{
						bigInteger %= this.mod;
					}
					else
					{
						this.BarrettReduction(bigInteger);
					}
				}
				if (sign == BigInteger.Sign.Negative)
				{
					bigInteger = this.mod - bigInteger;
				}
				return bigInteger;
			}

			// Token: 0x060008CB RID: 2251 RVA: 0x00020EB4 File Offset: 0x0001F0B4
			public BigInteger Pow(BigInteger a, BigInteger k)
			{
				BigInteger bigInteger = new BigInteger(1U);
				if (k == 0U)
				{
					return bigInteger;
				}
				BigInteger bigInteger2 = a;
				if (k.TestBit(0))
				{
					bigInteger = a;
				}
				for (int i = 1; i < k.BitCount(); i++)
				{
					bigInteger2 = this.Multiply(bigInteger2, bigInteger2);
					if (k.TestBit(i))
					{
						bigInteger = this.Multiply(bigInteger2, bigInteger);
					}
				}
				return bigInteger;
			}

			// Token: 0x060008CC RID: 2252 RVA: 0x00020F1C File Offset: 0x0001F11C
			public BigInteger Pow(uint b, BigInteger exp)
			{
				return this.Pow(new BigInteger(b), exp);
			}

			// Token: 0x040001BE RID: 446
			private BigInteger mod;

			// Token: 0x040001BF RID: 447
			private BigInteger constant;
		}

		// Token: 0x02000096 RID: 150
		internal sealed class Montgomery
		{
			// Token: 0x060008CD RID: 2253 RVA: 0x00020F2C File Offset: 0x0001F12C
			private Montgomery()
			{
			}

			// Token: 0x060008CE RID: 2254 RVA: 0x00020F34 File Offset: 0x0001F134
			public static uint Inverse(uint n)
			{
				uint num = n;
				uint num2;
				while ((num2 = n * num) != 1U)
				{
					num *= 2U - num2;
				}
				return (uint)(-(uint)((ulong)num));
			}

			// Token: 0x060008CF RID: 2255 RVA: 0x00020F60 File Offset: 0x0001F160
			public static BigInteger ToMont(BigInteger n, BigInteger m)
			{
				n.Normalize();
				m.Normalize();
				n <<= (int)(m.length * 32U);
				n %= m;
				return n;
			}

			// Token: 0x060008D0 RID: 2256 RVA: 0x00020F94 File Offset: 0x0001F194
			public unsafe static BigInteger Reduce(BigInteger n, BigInteger m, uint mPrime)
			{
				fixed (uint* ptr = ref (n.data != null && n.data.Length != 0) ? ref n.data[0] : ref *null, ptr2 = ref (m.data != null && m.data.Length != 0) ? ref m.data[0] : ref *null)
				{
					for (uint num = 0U; num < m.length; num += 1U)
					{
						uint num2 = *ptr * mPrime;
						uint* ptr3 = ptr2;
						uint* ptr4 = ptr;
						uint* ptr5 = ptr;
						ulong num3 = (ulong)num2 * (ulong)(*(ptr3++)) + (ulong)(*(ptr4++));
						num3 >>= 32;
						uint num4;
						for (num4 = 1U; num4 < m.length; num4 += 1U)
						{
							num3 += (ulong)num2 * (ulong)(*(ptr3++)) + (ulong)(*(ptr4++));
							*(ptr5++) = (uint)num3;
							num3 >>= 32;
						}
						while (num4 < n.length)
						{
							num3 += (ulong)(*(ptr4++));
							*(ptr5++) = (uint)num3;
							num3 >>= 32;
							if (num3 == 0UL)
							{
								num4 += 1U;
								break;
							}
							num4 += 1U;
						}
						while (num4 < n.length)
						{
							*(ptr5++) = *(ptr4++);
							num4 += 1U;
						}
						*(ptr5++) = (uint)num3;
					}
					while (n.length > 1U && ptr[n.length - 1U] == 0U)
					{
						n.length -= 1U;
					}
				}
				if (n >= m)
				{
					BigInteger.Kernel.MinusEq(n, m);
				}
				return n;
			}
		}

		// Token: 0x02000097 RID: 151
		private sealed class Kernel
		{
			// Token: 0x060008D2 RID: 2258 RVA: 0x00021168 File Offset: 0x0001F368
			public static BigInteger AddSameSign(BigInteger bi1, BigInteger bi2)
			{
				uint num = 0U;
				uint[] data;
				uint length;
				uint[] data2;
				uint length2;
				if (bi1.length < bi2.length)
				{
					data = bi2.data;
					length = bi2.length;
					data2 = bi1.data;
					length2 = bi1.length;
				}
				else
				{
					data = bi1.data;
					length = bi1.length;
					data2 = bi2.data;
					length2 = bi2.length;
				}
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, length + 1U);
				uint[] data3 = bigInteger.data;
				ulong num2 = 0UL;
				do
				{
					num2 = (ulong)data[(int)((UIntPtr)num)] + (ulong)data2[(int)((UIntPtr)num)] + num2;
					data3[(int)((UIntPtr)num)] = (uint)num2;
					num2 >>= 32;
				}
				while ((num += 1U) < length2);
				bool flag = num2 != 0UL;
				if (flag)
				{
					if (num < length)
					{
						do
						{
							flag = ((data3[(int)((UIntPtr)num)] = data[(int)((UIntPtr)num)] + 1U) == 0U);
						}
						while ((num += 1U) < length && flag);
					}
					if (flag)
					{
						data3[(int)((UIntPtr)num)] = 1U;
						bigInteger.length = num + 1U;
						return bigInteger;
					}
				}
				if (num < length)
				{
					do
					{
						data3[(int)((UIntPtr)num)] = data[(int)((UIntPtr)num)];
					}
					while ((num += 1U) < length);
				}
				bigInteger.Normalize();
				return bigInteger;
			}

			// Token: 0x060008D3 RID: 2259 RVA: 0x0002129C File Offset: 0x0001F49C
			public static BigInteger Subtract(BigInteger big, BigInteger small)
			{
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, big.length);
				uint[] data = bigInteger.data;
				uint[] data2 = big.data;
				uint[] data3 = small.data;
				uint num = 0U;
				uint num2 = 0U;
				do
				{
					uint num3 = data3[(int)((UIntPtr)num)];
					if ((num3 += num2) < num2 | (data[(int)((UIntPtr)num)] = data2[(int)((UIntPtr)num)] - num3) > ~num3)
					{
						num2 = 1U;
					}
					else
					{
						num2 = 0U;
					}
				}
				while ((num += 1U) < small.length);
				if (num != big.length)
				{
					if (num2 == 1U)
					{
						do
						{
							data[(int)((UIntPtr)num)] = data2[(int)((UIntPtr)num)] - 1U;
						}
						while (data2[(int)((UIntPtr)(num++))] == 0U && num < big.length);
						if (num == big.length)
						{
							goto IL_E5;
						}
					}
					do
					{
						data[(int)((UIntPtr)num)] = data2[(int)((UIntPtr)num)];
					}
					while ((num += 1U) < big.length);
				}
				IL_E5:
				bigInteger.Normalize();
				return bigInteger;
			}

			// Token: 0x060008D4 RID: 2260 RVA: 0x00021398 File Offset: 0x0001F598
			public static void MinusEq(BigInteger big, BigInteger small)
			{
				uint[] data = big.data;
				uint[] data2 = small.data;
				uint num = 0U;
				uint num2 = 0U;
				do
				{
					uint num3 = data2[(int)((UIntPtr)num)];
					if ((num3 += num2) < num2 | (data[(int)((UIntPtr)num)] -= num3) > ~num3)
					{
						num2 = 1U;
					}
					else
					{
						num2 = 0U;
					}
				}
				while ((num += 1U) < small.length);
				if (num != big.length)
				{
					if (num2 == 1U)
					{
						do
						{
							data[(int)((UIntPtr)num)] -= 1U;
						}
						while (data[(int)((UIntPtr)(num++))] == 0U && num < big.length);
					}
				}
				while (big.length > 0U && big.data[(int)((UIntPtr)(big.length - 1U))] == 0U)
				{
					big.length -= 1U;
				}
				if (big.length == 0U)
				{
					big.length += 1U;
				}
			}

			// Token: 0x060008D5 RID: 2261 RVA: 0x0002148C File Offset: 0x0001F68C
			public static void PlusEq(BigInteger bi1, BigInteger bi2)
			{
				uint num = 0U;
				bool flag = false;
				uint[] data;
				uint length;
				uint[] data2;
				uint length2;
				if (bi1.length < bi2.length)
				{
					flag = true;
					data = bi2.data;
					length = bi2.length;
					data2 = bi1.data;
					length2 = bi1.length;
				}
				else
				{
					data = bi1.data;
					length = bi1.length;
					data2 = bi2.data;
					length2 = bi2.length;
				}
				uint[] data3 = bi1.data;
				ulong num2 = 0UL;
				do
				{
					num2 += (ulong)data[(int)((UIntPtr)num)] + (ulong)data2[(int)((UIntPtr)num)];
					data3[(int)((UIntPtr)num)] = (uint)num2;
					num2 >>= 32;
				}
				while ((num += 1U) < length2);
				bool flag2 = num2 != 0UL;
				if (flag2)
				{
					if (num < length)
					{
						do
						{
							flag2 = ((data3[(int)((UIntPtr)num)] = data[(int)((UIntPtr)num)] + 1U) == 0U);
						}
						while ((num += 1U) < length && flag2);
					}
					if (flag2)
					{
						data3[(int)((UIntPtr)num)] = 1U;
						bi1.length = num + 1U;
						return;
					}
				}
				if (flag && num < length - 1U)
				{
					do
					{
						data3[(int)((UIntPtr)num)] = data[(int)((UIntPtr)num)];
					}
					while ((num += 1U) < length);
				}
				bi1.length = length + 1U;
				bi1.Normalize();
			}

			// Token: 0x060008D6 RID: 2262 RVA: 0x000215C4 File Offset: 0x0001F7C4
			public static BigInteger.Sign Compare(BigInteger bi1, BigInteger bi2)
			{
				uint num = bi1.length;
				uint num2 = bi2.length;
				while (num > 0U && bi1.data[(int)((UIntPtr)(num - 1U))] == 0U)
				{
					num -= 1U;
				}
				while (num2 > 0U && bi2.data[(int)((UIntPtr)(num2 - 1U))] == 0U)
				{
					num2 -= 1U;
				}
				if (num == 0U && num2 == 0U)
				{
					return BigInteger.Sign.Zero;
				}
				if (num < num2)
				{
					return BigInteger.Sign.Negative;
				}
				if (num > num2)
				{
					return BigInteger.Sign.Positive;
				}
				uint num3 = num - 1U;
				while (num3 != 0U && bi1.data[(int)((UIntPtr)num3)] == bi2.data[(int)((UIntPtr)num3)])
				{
					num3 -= 1U;
				}
				if (bi1.data[(int)((UIntPtr)num3)] < bi2.data[(int)((UIntPtr)num3)])
				{
					return BigInteger.Sign.Negative;
				}
				if (bi1.data[(int)((UIntPtr)num3)] > bi2.data[(int)((UIntPtr)num3)])
				{
					return BigInteger.Sign.Positive;
				}
				return BigInteger.Sign.Zero;
			}

			// Token: 0x060008D7 RID: 2263 RVA: 0x0002169C File Offset: 0x0001F89C
			public static uint SingleByteDivideInPlace(BigInteger n, uint d)
			{
				ulong num = 0UL;
				uint length = n.length;
				while (length-- > 0U)
				{
					num <<= 32;
					num |= (ulong)n.data[(int)((UIntPtr)length)];
					n.data[(int)((UIntPtr)length)] = (uint)(num / (ulong)d);
					num %= (ulong)d;
				}
				n.Normalize();
				return (uint)num;
			}

			// Token: 0x060008D8 RID: 2264 RVA: 0x000216F0 File Offset: 0x0001F8F0
			public static uint DwordMod(BigInteger n, uint d)
			{
				ulong num = 0UL;
				uint length = n.length;
				while (length-- > 0U)
				{
					num <<= 32;
					num |= (ulong)n.data[(int)((UIntPtr)length)];
					num %= (ulong)d;
				}
				return (uint)num;
			}

			// Token: 0x060008D9 RID: 2265 RVA: 0x00021730 File Offset: 0x0001F930
			public static BigInteger DwordDiv(BigInteger n, uint d)
			{
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, n.length);
				ulong num = 0UL;
				uint length = n.length;
				while (length-- > 0U)
				{
					num <<= 32;
					num |= (ulong)n.data[(int)((UIntPtr)length)];
					bigInteger.data[(int)((UIntPtr)length)] = (uint)(num / (ulong)d);
					num %= (ulong)d;
				}
				bigInteger.Normalize();
				return bigInteger;
			}

			// Token: 0x060008DA RID: 2266 RVA: 0x00021790 File Offset: 0x0001F990
			public static BigInteger[] DwordDivMod(BigInteger n, uint d)
			{
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, n.length);
				ulong num = 0UL;
				uint length = n.length;
				while (length-- > 0U)
				{
					num <<= 32;
					num |= (ulong)n.data[(int)((UIntPtr)length)];
					bigInteger.data[(int)((UIntPtr)length)] = (uint)(num / (ulong)d);
					num %= (ulong)d;
				}
				bigInteger.Normalize();
				BigInteger bigInteger2 = (uint)num;
				return new BigInteger[]
				{
					bigInteger,
					bigInteger2
				};
			}

			// Token: 0x060008DB RID: 2267 RVA: 0x00021808 File Offset: 0x0001FA08
			public static BigInteger[] multiByteDivide(BigInteger bi1, BigInteger bi2)
			{
				if (BigInteger.Kernel.Compare(bi1, bi2) == BigInteger.Sign.Negative)
				{
					return new BigInteger[]
					{
						0,
						new BigInteger(bi1)
					};
				}
				bi1.Normalize();
				bi2.Normalize();
				if (bi2.length == 1U)
				{
					return BigInteger.Kernel.DwordDivMod(bi1, bi2.data[0]);
				}
				uint num = bi1.length + 1U;
				int num2 = (int)(bi2.length + 1U);
				uint num3 = 2147483648U;
				uint num4 = bi2.data[(int)((UIntPtr)(bi2.length - 1U))];
				int num5 = 0;
				int num6 = (int)(bi1.length - bi2.length);
				while (num3 != 0U && (num4 & num3) == 0U)
				{
					num5++;
					num3 >>= 1;
				}
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, bi1.length - bi2.length + 1U);
				BigInteger bigInteger2 = bi1 << num5;
				uint[] data = bigInteger2.data;
				bi2 <<= num5;
				int i = (int)(num - bi2.length);
				int num7 = (int)(num - 1U);
				uint num8 = bi2.data[(int)((UIntPtr)(bi2.length - 1U))];
				ulong num9 = (ulong)bi2.data[(int)((UIntPtr)(bi2.length - 2U))];
				while (i > 0)
				{
					ulong num10 = ((ulong)data[num7] << 32) + (ulong)data[num7 - 1];
					ulong num11 = num10 / (ulong)num8;
					ulong num12 = num10 % (ulong)num8;
					while (num11 == 4294967296UL || num11 * num9 > (num12 << 32) + (ulong)data[num7 - 2])
					{
						num11 -= 1UL;
						num12 += (ulong)num8;
						if (num12 >= 4294967296UL)
						{
							break;
						}
					}
					uint num13 = 0U;
					int num14 = num7 - num2 + 1;
					ulong num15 = 0UL;
					uint num16 = (uint)num11;
					do
					{
						num15 += (ulong)bi2.data[(int)((UIntPtr)num13)] * (ulong)num16;
						uint num17 = data[num14];
						data[num14] -= (uint)num15;
						num15 >>= 32;
						if (data[num14] > num17)
						{
							num15 += 1UL;
						}
						num13 += 1U;
						num14++;
					}
					while ((ulong)num13 < (ulong)((long)num2));
					num14 = num7 - num2 + 1;
					num13 = 0U;
					if (num15 != 0UL)
					{
						num16 -= 1U;
						ulong num18 = 0UL;
						do
						{
							num18 = (ulong)data[num14] + (ulong)bi2.data[(int)((UIntPtr)num13)] + num18;
							data[num14] = (uint)num18;
							num18 >>= 32;
							num13 += 1U;
							num14++;
						}
						while ((ulong)num13 < (ulong)((long)num2));
					}
					bigInteger.data[num6--] = num16;
					num7--;
					i--;
				}
				bigInteger.Normalize();
				bigInteger2.Normalize();
				BigInteger[] array = new BigInteger[]
				{
					bigInteger,
					bigInteger2
				};
				if (num5 != 0)
				{
					BigInteger[] array2 = array;
					int num19 = 1;
					array2[num19] >>= num5;
				}
				return array;
			}

			// Token: 0x060008DC RID: 2268 RVA: 0x00021AC8 File Offset: 0x0001FCC8
			public static BigInteger LeftShift(BigInteger bi, int n)
			{
				if (n == 0)
				{
					return new BigInteger(bi, bi.length + 1U);
				}
				int num = n >> 5;
				n &= 31;
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, bi.length + 1U + (uint)num);
				uint num2 = 0U;
				uint length = bi.length;
				if (n != 0)
				{
					uint num3 = 0U;
					while (num2 < length)
					{
						uint num4 = bi.data[(int)((UIntPtr)num2)];
						bigInteger.data[(int)(checked((IntPtr)(unchecked((ulong)num2 + (ulong)((long)num)))))] = (num4 << n | num3);
						num3 = num4 >> 32 - n;
						num2 += 1U;
					}
					bigInteger.data[(int)(checked((IntPtr)(unchecked((ulong)num2 + (ulong)((long)num)))))] = num3;
				}
				else
				{
					while (num2 < length)
					{
						bigInteger.data[(int)(checked((IntPtr)(unchecked((ulong)num2 + (ulong)((long)num)))))] = bi.data[(int)((UIntPtr)num2)];
						num2 += 1U;
					}
				}
				bigInteger.Normalize();
				return bigInteger;
			}

			// Token: 0x060008DD RID: 2269 RVA: 0x00021B98 File Offset: 0x0001FD98
			public static BigInteger RightShift(BigInteger bi, int n)
			{
				if (n == 0)
				{
					return new BigInteger(bi);
				}
				int num = n >> 5;
				int num2 = n & 31;
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, bi.length - (uint)num + 1U);
				uint num3 = (uint)(bigInteger.data.Length - 1);
				if (num2 != 0)
				{
					uint num4 = 0U;
					while (num3-- > 0U)
					{
						uint num5 = bi.data[(int)(checked((IntPtr)(unchecked((ulong)num3 + (ulong)((long)num)))))];
						bigInteger.data[(int)((UIntPtr)num3)] = (num5 >> n | num4);
						num4 = num5 << 32 - n;
					}
				}
				else
				{
					while (num3-- > 0U)
					{
						bigInteger.data[(int)((UIntPtr)num3)] = bi.data[(int)(checked((IntPtr)(unchecked((ulong)num3 + (ulong)((long)num)))))];
					}
				}
				bigInteger.Normalize();
				return bigInteger;
			}

			// Token: 0x060008DE RID: 2270 RVA: 0x00021C50 File Offset: 0x0001FE50
			public static BigInteger MultiplyByDword(BigInteger n, uint f)
			{
				BigInteger bigInteger = new BigInteger(BigInteger.Sign.Positive, n.length + 1U);
				uint num = 0U;
				ulong num2 = 0UL;
				do
				{
					num2 += (ulong)n.data[(int)((UIntPtr)num)] * (ulong)f;
					bigInteger.data[(int)((UIntPtr)num)] = (uint)num2;
					num2 >>= 32;
				}
				while ((num += 1U) < n.length);
				bigInteger.data[(int)((UIntPtr)num)] = (uint)num2;
				bigInteger.Normalize();
				return bigInteger;
			}

			// Token: 0x060008DF RID: 2271 RVA: 0x00021CB4 File Offset: 0x0001FEB4
			public unsafe static void Multiply(uint[] x, uint xOffset, uint xLen, uint[] y, uint yOffset, uint yLen, uint[] d, uint dOffset)
			{
				fixed (uint* ptr = ref (x != null && x.Length != 0) ? ref x[0] : ref *null, ptr2 = ref (y != null && y.Length != 0) ? ref y[0] : ref *null, ptr3 = ref (d != null && d.Length != 0) ? ref d[0] : ref *null)
				{
					uint* ptr4 = ptr + xOffset;
					uint* ptr5 = ptr4 + xLen;
					uint* ptr6 = ptr2 + yOffset;
					uint* ptr7 = ptr6 + yLen;
					uint* ptr8 = ptr3 + dOffset;
					while (ptr4 < ptr5)
					{
						if (*ptr4 != 0U)
						{
							ulong num = 0UL;
							uint* ptr9 = ptr8;
							uint* ptr10 = ptr6;
							while (ptr10 < ptr7)
							{
								num += (ulong)(*ptr4) * (ulong)(*ptr10) + (ulong)(*ptr9);
								*ptr9 = (uint)num;
								num >>= 32;
								ptr10++;
								ptr9++;
							}
							if (num != 0UL)
							{
								*ptr9 = (uint)num;
							}
						}
						ptr4++;
						ptr8++;
					}
				}
			}

			// Token: 0x060008E0 RID: 2272 RVA: 0x00021DC8 File Offset: 0x0001FFC8
			public unsafe static void MultiplyMod2p32pmod(uint[] x, int xOffset, int xLen, uint[] y, int yOffest, int yLen, uint[] d, int dOffset, int mod)
			{
				fixed (uint* ptr = ref (x != null && x.Length != 0) ? ref x[0] : ref *null, ptr2 = ref (y != null && y.Length != 0) ? ref y[0] : ref *null, ptr3 = ref (d != null && d.Length != 0) ? ref d[0] : ref *null)
				{
					uint* ptr4 = ptr + xOffset;
					uint* ptr5 = ptr4 + xLen;
					uint* ptr6 = ptr2 + yOffest;
					uint* ptr7 = ptr6 + yLen;
					uint* ptr8 = ptr3 + dOffset;
					uint* ptr9 = ptr8 + mod;
					while (ptr4 < ptr5)
					{
						if (*ptr4 != 0U)
						{
							ulong num = 0UL;
							uint* ptr10 = ptr8;
							uint* ptr11 = ptr6;
							while (ptr11 < ptr7 && ptr10 < ptr9)
							{
								num += (ulong)(*ptr4) * (ulong)(*ptr11) + (ulong)(*ptr10);
								*ptr10 = (uint)num;
								num >>= 32;
								ptr11++;
								ptr10++;
							}
							if (num != 0UL && ptr10 < ptr9)
							{
								*ptr10 = (uint)num;
							}
						}
						ptr4++;
						ptr8++;
					}
				}
			}

			// Token: 0x060008E1 RID: 2273 RVA: 0x00021EF4 File Offset: 0x000200F4
			public unsafe static void SquarePositive(BigInteger bi, ref uint[] wkSpace)
			{
				uint[] array = wkSpace;
				wkSpace = bi.data;
				uint[] data = bi.data;
				uint length = bi.length;
				bi.data = array;
				fixed (uint* ptr = ref (data != null && data.Length != 0) ? ref data[0] : ref *null, ptr2 = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
				{
					uint* ptr3 = ptr2 + array.Length;
					for (uint* ptr4 = ptr2; ptr4 < ptr3; ptr4++)
					{
						*ptr4 = 0U;
					}
					uint* ptr5 = ptr;
					uint* ptr6 = ptr2;
					uint num = 0U;
					while (num < length)
					{
						if (*ptr5 != 0U)
						{
							ulong num2 = 0UL;
							uint num3 = *ptr5;
							uint* ptr7 = ptr5 + 1;
							uint* ptr8 = ptr6 + 2U * num + 1;
							uint num4 = num + 1U;
							while (num4 < length)
							{
								num2 += (ulong)num3 * (ulong)(*ptr7) + (ulong)(*ptr8);
								*ptr8 = (uint)num2;
								num2 >>= 32;
								num4 += 1U;
								ptr8++;
								ptr7++;
							}
							if (num2 != 0UL)
							{
								*ptr8 = (uint)num2;
							}
						}
						num += 1U;
						ptr5++;
					}
					ptr6 = ptr2;
					uint num5 = 0U;
					while (ptr6 < ptr3)
					{
						uint num6 = *ptr6;
						*ptr6 = (num6 << 1 | num5);
						num5 = num6 >> 31;
						ptr6++;
					}
					if (num5 != 0U)
					{
						*ptr6 = num5;
					}
					ptr5 = ptr;
					ptr6 = ptr2;
					uint* ptr9 = ptr5 + length;
					while (ptr5 < ptr9)
					{
						ulong num7 = (ulong)(*ptr5) * (ulong)(*ptr5) + (ulong)(*ptr6);
						*ptr6 = (uint)num7;
						num7 >>= 32;
						*(++ptr6) += (uint)num7;
						if (*ptr6 < (uint)num7)
						{
							uint* ptr10 = ptr6;
							*(++ptr10) += 1U;
							while (*(ptr10++) == 0U)
							{
								*ptr10 += 1U;
							}
						}
						ptr5++;
						ptr6++;
					}
					bi.length <<= 1;
					while (ptr2[bi.length - 1U] == 0U && bi.length > 1U)
					{
						bi.length -= 1U;
					}
				}
			}

			// Token: 0x060008E2 RID: 2274 RVA: 0x00022138 File Offset: 0x00020338
			public static BigInteger gcd(BigInteger a, BigInteger b)
			{
				BigInteger bigInteger = a;
				BigInteger bigInteger2 = b;
				BigInteger bigInteger3 = bigInteger2;
				while (bigInteger.length > 1U)
				{
					bigInteger3 = bigInteger;
					bigInteger = bigInteger2 % bigInteger;
					bigInteger2 = bigInteger3;
				}
				if (bigInteger == 0U)
				{
					return bigInteger3;
				}
				uint num = bigInteger.data[0];
				uint num2 = bigInteger2 % num;
				int num3 = 0;
				while (((num2 | num) & 1U) == 0U)
				{
					num2 >>= 1;
					num >>= 1;
					num3++;
				}
				while (num2 != 0U)
				{
					while ((num2 & 1U) == 0U)
					{
						num2 >>= 1;
					}
					while ((num & 1U) == 0U)
					{
						num >>= 1;
					}
					if (num2 >= num)
					{
						num2 = num2 - num >> 1;
					}
					else
					{
						num = num - num2 >> 1;
					}
				}
				return num << num3;
			}

			// Token: 0x060008E3 RID: 2275 RVA: 0x00022204 File Offset: 0x00020404
			public static uint modInverse(BigInteger bi, uint modulus)
			{
				uint num = modulus;
				uint num2 = bi % modulus;
				uint num3 = 0U;
				uint num4 = 1U;
				while (num2 != 0U)
				{
					if (num2 == 1U)
					{
						return num4;
					}
					num3 += num / num2 * num4;
					num %= num2;
					if (num == 0U)
					{
						break;
					}
					if (num == 1U)
					{
						return modulus - num3;
					}
					num4 += num2 / num * num3;
					num2 %= num;
				}
				return 0U;
			}

			// Token: 0x060008E4 RID: 2276 RVA: 0x00022264 File Offset: 0x00020464
			public static BigInteger modInverse(BigInteger bi, BigInteger modulus)
			{
				if (modulus.length == 1U)
				{
					return BigInteger.Kernel.modInverse(bi, modulus.data[0]);
				}
				BigInteger[] array = new BigInteger[]
				{
					0,
					1
				};
				BigInteger[] array2 = new BigInteger[2];
				BigInteger[] array3 = new BigInteger[]
				{
					0,
					0
				};
				int num = 0;
				BigInteger bi2 = modulus;
				BigInteger bigInteger = bi;
				BigInteger.ModulusRing modulusRing = new BigInteger.ModulusRing(modulus);
				while (bigInteger != 0U)
				{
					if (num > 1)
					{
						BigInteger bigInteger2 = modulusRing.Difference(array[0], array[1] * array2[0]);
						array[0] = array[1];
						array[1] = bigInteger2;
					}
					BigInteger[] array4 = BigInteger.Kernel.multiByteDivide(bi2, bigInteger);
					array2[0] = array2[1];
					array2[1] = array4[0];
					array3[0] = array3[1];
					array3[1] = array4[1];
					bi2 = bigInteger;
					bigInteger = array4[1];
					num++;
				}
				if (array3[0] != 1U)
				{
					throw new ArithmeticException("No inverse!");
				}
				return modulusRing.Difference(array[0], array[1] * array2[0]);
			}
		}
	}
}
