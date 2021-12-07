using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace System
{
	// Token: 0x02000163 RID: 355
	internal sealed class NumberFormatter
	{
		// Token: 0x060012D8 RID: 4824 RVA: 0x00049868 File Offset: 0x00047A68
		public NumberFormatter(Thread current)
		{
			this._cbuf = new char[0];
			if (current == null)
			{
				return;
			}
			this._thread = current;
			this.CurrentCulture = this._thread.CurrentCulture;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0004989C File Offset: 0x00047A9C
		static NumberFormatter()
		{
			NumberFormatter.GetFormatterTables(out NumberFormatter.MantissaBitsTable, out NumberFormatter.TensExponentTable, out NumberFormatter.DigitLowerTable, out NumberFormatter.DigitUpperTable, out NumberFormatter.TenPowersList, out NumberFormatter.DecHexDigits);
		}

		// Token: 0x060012DA RID: 4826
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GetFormatterTables(out ulong* MantissaBitsTable, out int* TensExponentTable, out char* DigitLowerTable, out char* DigitUpperTable, out long* TenPowersList, out int* DecHexDigits);

		// Token: 0x060012DB RID: 4827 RVA: 0x000498C4 File Offset: 0x00047AC4
		private unsafe static long GetTenPowerOf(int i)
		{
			return NumberFormatter.TenPowersList[i];
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000498D0 File Offset: 0x00047AD0
		private void InitDecHexDigits(uint value)
		{
			if (value >= 100000000U)
			{
				int num = (int)(value / 100000000U);
				value -= (uint)(100000000 * num);
				this._val2 = NumberFormatter.FastToDecHex(num);
			}
			this._val1 = NumberFormatter.ToDecHex((int)value);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00049914 File Offset: 0x00047B14
		private void InitDecHexDigits(ulong value)
		{
			if (value >= 100000000UL)
			{
				long num = (long)(value / 100000000UL);
				value -= (ulong)(100000000L * num);
				if (num >= 100000000L)
				{
					int num2 = (int)(num / 100000000L);
					num -= (long)num2 * 100000000L;
					this._val3 = NumberFormatter.ToDecHex(num2);
				}
				if (num != 0L)
				{
					this._val2 = NumberFormatter.ToDecHex((int)num);
				}
			}
			if (value != 0UL)
			{
				this._val1 = NumberFormatter.ToDecHex((int)value);
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00049998 File Offset: 0x00047B98
		private void InitDecHexDigits(uint hi, ulong lo)
		{
			if (hi == 0U)
			{
				this.InitDecHexDigits(lo);
				return;
			}
			uint num = hi / 100000000U;
			ulong num2 = (ulong)(hi - num * 100000000U);
			ulong num3 = lo / 100000000UL;
			ulong num4 = lo - num3 * 100000000UL + num2 * 9551616UL;
			hi = num;
			lo = num3 + num2 * 184467440737UL;
			num3 = num4 / 100000000UL;
			num4 -= num3 * 100000000UL;
			lo += num3;
			this._val1 = NumberFormatter.ToDecHex((int)num4);
			num3 = lo / 100000000UL;
			num4 = lo - num3 * 100000000UL;
			lo = num3;
			if (hi != 0U)
			{
				lo += (ulong)hi * 184467440737UL;
				num4 += (ulong)hi * 9551616UL;
				num3 = num4 / 100000000UL;
				lo += num3;
				num4 -= num3 * 100000000UL;
			}
			this._val2 = NumberFormatter.ToDecHex((int)num4);
			if (lo >= 100000000UL)
			{
				num3 = lo / 100000000UL;
				lo -= num3 * 100000000UL;
				this._val4 = NumberFormatter.ToDecHex((int)num3);
			}
			this._val3 = NumberFormatter.ToDecHex((int)lo);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00049AB8 File Offset: 0x00047CB8
		private unsafe static uint FastToDecHex(int val)
		{
			if (val < 100)
			{
				return (uint)NumberFormatter.DecHexDigits[val];
			}
			int num = val * 5243 >> 19;
			return (uint)(NumberFormatter.DecHexDigits[num] << 8 | NumberFormatter.DecHexDigits[val - num * 100]);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00049B00 File Offset: 0x00047D00
		private static uint ToDecHex(int val)
		{
			uint num = 0U;
			if (val >= 10000)
			{
				int num2 = val / 10000;
				val -= num2 * 10000;
				num = NumberFormatter.FastToDecHex(num2) << 16;
			}
			return num | NumberFormatter.FastToDecHex(val);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00049B40 File Offset: 0x00047D40
		private static int FastDecHexLen(int val)
		{
			if (val < 256)
			{
				if (val < 16)
				{
					return 1;
				}
				return 2;
			}
			else
			{
				if (val < 4096)
				{
					return 3;
				}
				return 4;
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00049B68 File Offset: 0x00047D68
		private static int DecHexLen(uint val)
		{
			if (val < 65536U)
			{
				return NumberFormatter.FastDecHexLen((int)val);
			}
			return 4 + NumberFormatter.FastDecHexLen((int)(val >> 16));
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00049B88 File Offset: 0x00047D88
		private int DecHexLen()
		{
			if (this._val4 != 0U)
			{
				return NumberFormatter.DecHexLen(this._val4) + 24;
			}
			if (this._val3 != 0U)
			{
				return NumberFormatter.DecHexLen(this._val3) + 16;
			}
			if (this._val2 != 0U)
			{
				return NumberFormatter.DecHexLen(this._val2) + 8;
			}
			if (this._val1 != 0U)
			{
				return NumberFormatter.DecHexLen(this._val1);
			}
			return 0;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00049BFC File Offset: 0x00047DFC
		private static int ScaleOrder(long hi)
		{
			for (int i = 18; i >= 0; i--)
			{
				if (hi >= NumberFormatter.GetTenPowerOf(i))
				{
					return i + 1;
				}
			}
			return 1;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00049C30 File Offset: 0x00047E30
		private int InitialFloatingPrecision()
		{
			if (this._specifier == 'R')
			{
				return this._defPrecision + 2;
			}
			if (this._precision < this._defPrecision)
			{
				return this._defPrecision;
			}
			if (this._specifier == 'G')
			{
				return Math.Min(this._defPrecision + 2, this._precision);
			}
			if (this._specifier == 'E')
			{
				return Math.Min(this._defPrecision + 2, this._precision + 1);
			}
			return this._defPrecision;
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00049CB8 File Offset: 0x00047EB8
		private static int ParsePrecision(string format)
		{
			int num = 0;
			for (int i = 1; i < format.Length; i++)
			{
				int num2 = (int)(format[i] - '0');
				num = num * 10 + num2;
				if (num2 < 0 || num2 > 9 || num > 99)
				{
					return -2;
				}
			}
			return num;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00049D0C File Offset: 0x00047F0C
		private void Init(string format)
		{
			this._val1 = (this._val2 = (this._val3 = (this._val4 = 0U)));
			this._offset = 0;
			this._NaN = (this._infinity = false);
			this._isCustomFormat = false;
			this._specifierIsUpper = true;
			this._precision = -1;
			if (format == null || format.Length == 0)
			{
				this._specifier = 'G';
				return;
			}
			char c = format[0];
			if (c >= 'a' && c <= 'z')
			{
				c = c - 'a' + 'A';
				this._specifierIsUpper = false;
			}
			else if (c < 'A' || c > 'Z')
			{
				this._isCustomFormat = true;
				this._specifier = '0';
				return;
			}
			this._specifier = c;
			if (format.Length > 1)
			{
				this._precision = NumberFormatter.ParsePrecision(format);
				if (this._precision == -2)
				{
					this._isCustomFormat = true;
					this._specifier = '0';
					this._precision = -1;
				}
			}
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00049E10 File Offset: 0x00048010
		private void InitHex(ulong value)
		{
			int defPrecision = this._defPrecision;
			switch (defPrecision)
			{
			case 3:
				value = (ulong)((byte)value);
				break;
			default:
				if (defPrecision == 10)
				{
					value = (ulong)((uint)value);
				}
				break;
			case 5:
				value = (ulong)((ushort)value);
				break;
			}
			this._val1 = (uint)value;
			this._val2 = (uint)(value >> 32);
			this._decPointPos = (this._digitsLen = this.DecHexLen());
			if (value == 0UL)
			{
				this._decPointPos = 1;
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00049E98 File Offset: 0x00048098
		private void Init(string format, int value, int defPrecision)
		{
			this.Init(format);
			this._defPrecision = defPrecision;
			this._positive = (value >= 0);
			if (value == 0 || this._specifier == 'X')
			{
				this.InitHex((ulong)((long)value));
				return;
			}
			if (value < 0)
			{
				value = -value;
			}
			this.InitDecHexDigits((uint)value);
			this._decPointPos = (this._digitsLen = this.DecHexLen());
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00049F04 File Offset: 0x00048104
		private void Init(string format, uint value, int defPrecision)
		{
			this.Init(format);
			this._defPrecision = defPrecision;
			this._positive = true;
			if (value == 0U || this._specifier == 'X')
			{
				this.InitHex((ulong)value);
				return;
			}
			this.InitDecHexDigits(value);
			this._decPointPos = (this._digitsLen = this.DecHexLen());
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00049F60 File Offset: 0x00048160
		private void Init(string format, long value)
		{
			this.Init(format);
			this._defPrecision = 19;
			this._positive = (value >= 0L);
			if (value == 0L || this._specifier == 'X')
			{
				this.InitHex((ulong)value);
				return;
			}
			if (value < 0L)
			{
				value = -value;
			}
			this.InitDecHexDigits((ulong)value);
			this._decPointPos = (this._digitsLen = this.DecHexLen());
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00049FD0 File Offset: 0x000481D0
		private void Init(string format, ulong value)
		{
			this.Init(format);
			this._defPrecision = 20;
			this._positive = true;
			if (value == 0UL || this._specifier == 'X')
			{
				this.InitHex(value);
				return;
			}
			this.InitDecHexDigits(value);
			this._decPointPos = (this._digitsLen = this.DecHexLen());
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0004A02C File Offset: 0x0004822C
		private unsafe void Init(string format, double value, int defPrecision)
		{
			this.Init(format);
			this._defPrecision = defPrecision;
			long num = BitConverter.DoubleToInt64Bits(value);
			this._positive = (num >= 0L);
			num &= long.MaxValue;
			if (num == 0L)
			{
				this._decPointPos = 1;
				this._digitsLen = 0;
				this._positive = true;
				return;
			}
			int num2 = (int)(num >> 52);
			long num3 = num & 4503599627370495L;
			if (num2 == 2047)
			{
				this._NaN = (num3 != 0L);
				this._infinity = (num3 == 0L);
				return;
			}
			int num4 = 0;
			if (num2 == 0)
			{
				num2 = 1;
				int num5 = NumberFormatter.ScaleOrder(num3);
				if (num5 < 15)
				{
					num4 = num5 - 15;
					num3 *= NumberFormatter.GetTenPowerOf(-num4);
				}
			}
			else
			{
				num3 = (num3 + 4503599627370495L + 1L) * 10L;
				num4 = -1;
			}
			ulong num6 = (ulong)((uint)num3);
			ulong num7 = (ulong)num3 >> 32;
			ulong num8 = NumberFormatter.MantissaBitsTable[num2];
			ulong num9 = num8 >> 32;
			num8 = (ulong)((uint)num8);
			ulong num10 = num7 * num8 + num6 * num9 + (num6 * num8 >> 32);
			long num11 = (long)(num7 * num9 + (num10 >> 32));
			while (num11 < 10000000000000000L)
			{
				num10 = (num10 & (ulong)-1) * 10UL;
				num11 = num11 * 10L + (long)(num10 >> 32);
				num4--;
			}
			if ((num10 & (ulong)-2147483648) != 0UL)
			{
				num11 += 1L;
			}
			int num12 = 17;
			this._decPointPos = NumberFormatter.TensExponentTable[num2] + num4 + num12;
			int num13 = this.InitialFloatingPrecision();
			if (num12 > num13)
			{
				long tenPowerOf = NumberFormatter.GetTenPowerOf(num12 - num13);
				num11 = (num11 + (tenPowerOf >> 1)) / tenPowerOf;
				num12 = num13;
			}
			if (num11 >= NumberFormatter.GetTenPowerOf(num12))
			{
				num12++;
				this._decPointPos++;
			}
			this.InitDecHexDigits((ulong)num11);
			this._offset = this.CountTrailingZeros();
			this._digitsLen = num12 - this._offset;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0004A21C File Offset: 0x0004841C
		private void Init(string format, decimal value)
		{
			this.Init(format);
			this._defPrecision = 100;
			int[] bits = decimal.GetBits(value);
			int num = (bits[3] & 2031616) >> 16;
			this._positive = (bits[3] >= 0);
			if (bits[0] == 0 && bits[1] == 0 && bits[2] == 0)
			{
				this._decPointPos = -num;
				this._positive = true;
				this._digitsLen = 0;
				return;
			}
			this.InitDecHexDigits((uint)bits[2], (ulong)((long)bits[1] << 32 | (long)((ulong)bits[0])));
			this._digitsLen = this.DecHexLen();
			this._decPointPos = this._digitsLen - num;
			if (this._precision != -1 || this._specifier != 'G')
			{
				this._offset = this.CountTrailingZeros();
				this._digitsLen -= this._offset;
			}
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0004A2F4 File Offset: 0x000484F4
		private void ResetCharBuf(int size)
		{
			this._ind = 0;
			if (this._cbuf.Length < size)
			{
				this._cbuf = new char[size];
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0004A318 File Offset: 0x00048518
		private void Resize(int len)
		{
			char[] array = new char[len];
			Array.Copy(this._cbuf, array, this._ind);
			this._cbuf = array;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0004A348 File Offset: 0x00048548
		private void Append(char c)
		{
			if (this._ind == this._cbuf.Length)
			{
				this.Resize(this._ind + 10);
			}
			this._cbuf[this._ind++] = c;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0004A390 File Offset: 0x00048590
		private void Append(char c, int cnt)
		{
			if (this._ind + cnt > this._cbuf.Length)
			{
				this.Resize(this._ind + cnt + 10);
			}
			while (cnt-- > 0)
			{
				this._cbuf[this._ind++] = c;
			}
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0004A3F0 File Offset: 0x000485F0
		private void Append(string s)
		{
			int length = s.Length;
			if (this._ind + length > this._cbuf.Length)
			{
				this.Resize(this._ind + length + 10);
			}
			for (int i = 0; i < length; i++)
			{
				this._cbuf[this._ind++] = s[i];
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0004A45C File Offset: 0x0004865C
		private NumberFormatInfo GetNumberFormatInstance(IFormatProvider fp)
		{
			if (this._nfi != null && fp == null)
			{
				return this._nfi;
			}
			return NumberFormatInfo.GetInstance(fp);
		}

		// Token: 0x170002B9 RID: 697
		// (set) Token: 0x060012F5 RID: 4853 RVA: 0x0004A47C File Offset: 0x0004867C
		public CultureInfo CurrentCulture
		{
			set
			{
				if (value != null && value.IsReadOnly)
				{
					this._nfi = value.NumberFormat;
				}
				else
				{
					this._nfi = null;
				}
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0004A4A8 File Offset: 0x000486A8
		private int IntegerDigits
		{
			get
			{
				return (this._decPointPos <= 0) ? 1 : this._decPointPos;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x0004A4C4 File Offset: 0x000486C4
		private int DecimalDigits
		{
			get
			{
				return (this._digitsLen <= this._decPointPos) ? 0 : (this._digitsLen - this._decPointPos);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x0004A4F8 File Offset: 0x000486F8
		private bool IsFloatingSource
		{
			get
			{
				return this._defPrecision == 15 || this._defPrecision == 7;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0004A514 File Offset: 0x00048714
		private bool IsZero
		{
			get
			{
				return this._digitsLen == 0;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0004A520 File Offset: 0x00048720
		private bool IsZeroInteger
		{
			get
			{
				return this._digitsLen == 0 || this._decPointPos <= 0;
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0004A53C File Offset: 0x0004873C
		private void RoundPos(int pos)
		{
			this.RoundBits(this._digitsLen - pos);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0004A550 File Offset: 0x00048750
		private bool RoundDecimal(int decimals)
		{
			return this.RoundBits(this._digitsLen - this._decPointPos - decimals);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0004A568 File Offset: 0x00048768
		private bool RoundBits(int shift)
		{
			if (shift <= 0)
			{
				return false;
			}
			if (shift > this._digitsLen)
			{
				this._digitsLen = 0;
				this._decPointPos = 1;
				this._val1 = (this._val2 = (this._val3 = (this._val4 = 0U)));
				this._positive = true;
				return false;
			}
			shift += this._offset;
			this._digitsLen += this._offset;
			while (shift > 8)
			{
				this._val1 = this._val2;
				this._val2 = this._val3;
				this._val3 = this._val4;
				this._val4 = 0U;
				this._digitsLen -= 8;
				shift -= 8;
			}
			shift = shift - 1 << 2;
			uint num = this._val1 >> shift;
			uint num2 = num & 15U;
			this._val1 = (num ^ num2) << shift;
			bool result = false;
			if (num2 >= 5U)
			{
				this._val1 |= 2576980377U >> 28 - shift;
				this.AddOneToDecHex();
				int num3 = this.DecHexLen();
				result = (num3 != this._digitsLen);
				this._decPointPos = this._decPointPos + num3 - this._digitsLen;
				this._digitsLen = num3;
			}
			this.RemoveTrailingZeros();
			return result;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0004A6BC File Offset: 0x000488BC
		private void RemoveTrailingZeros()
		{
			this._offset = this.CountTrailingZeros();
			this._digitsLen -= this._offset;
			if (this._digitsLen == 0)
			{
				this._offset = 0;
				this._decPointPos = 1;
				this._positive = true;
			}
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0004A708 File Offset: 0x00048908
		private void AddOneToDecHex()
		{
			if (this._val1 == 2576980377U)
			{
				this._val1 = 0U;
				if (this._val2 == 2576980377U)
				{
					this._val2 = 0U;
					if (this._val3 == 2576980377U)
					{
						this._val3 = 0U;
						this._val4 = NumberFormatter.AddOneToDecHex(this._val4);
					}
					else
					{
						this._val3 = NumberFormatter.AddOneToDecHex(this._val3);
					}
				}
				else
				{
					this._val2 = NumberFormatter.AddOneToDecHex(this._val2);
				}
			}
			else
			{
				this._val1 = NumberFormatter.AddOneToDecHex(this._val1);
			}
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0004A7B0 File Offset: 0x000489B0
		private static uint AddOneToDecHex(uint val)
		{
			if ((val & 65535U) == 39321U)
			{
				if ((val & 16777215U) == 10066329U)
				{
					if ((val & 268435455U) == 161061273U)
					{
						return val + 107374183U;
					}
					return val + 6710887U;
				}
				else
				{
					if ((val & 1048575U) == 629145U)
					{
						return val + 419431U;
					}
					return val + 26215U;
				}
			}
			else if ((val & 255U) == 153U)
			{
				if ((val & 4095U) == 2457U)
				{
					return val + 1639U;
				}
				return val + 103U;
			}
			else
			{
				if ((val & 15U) == 9U)
				{
					return val + 7U;
				}
				return val + 1U;
			}
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0004A864 File Offset: 0x00048A64
		private int CountTrailingZeros()
		{
			if (this._val1 != 0U)
			{
				return NumberFormatter.CountTrailingZeros(this._val1);
			}
			if (this._val2 != 0U)
			{
				return NumberFormatter.CountTrailingZeros(this._val2) + 8;
			}
			if (this._val3 != 0U)
			{
				return NumberFormatter.CountTrailingZeros(this._val3) + 16;
			}
			if (this._val4 != 0U)
			{
				return NumberFormatter.CountTrailingZeros(this._val4) + 24;
			}
			return this._digitsLen;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0004A8DC File Offset: 0x00048ADC
		private static int CountTrailingZeros(uint val)
		{
			if ((val & 65535U) == 0U)
			{
				if ((val & 16777215U) == 0U)
				{
					if ((val & 268435455U) == 0U)
					{
						return 7;
					}
					return 6;
				}
				else
				{
					if ((val & 1048575U) == 0U)
					{
						return 5;
					}
					return 4;
				}
			}
			else if ((val & 255U) == 0U)
			{
				if ((val & 4095U) == 0U)
				{
					return 3;
				}
				return 2;
			}
			else
			{
				if ((val & 15U) == 0U)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0004A94C File Offset: 0x00048B4C
		private static NumberFormatter GetInstance()
		{
			NumberFormatter numberFormatter = NumberFormatter.threadNumberFormatter;
			NumberFormatter.threadNumberFormatter = null;
			if (numberFormatter == null)
			{
				return new NumberFormatter(Thread.CurrentThread);
			}
			return numberFormatter;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0004A978 File Offset: 0x00048B78
		private void Release()
		{
			NumberFormatter.threadNumberFormatter = this;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0004A980 File Offset: 0x00048B80
		internal static void SetThreadCurrentCulture(CultureInfo culture)
		{
			if (NumberFormatter.threadNumberFormatter != null)
			{
				NumberFormatter.threadNumberFormatter.CurrentCulture = culture;
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0004A998 File Offset: 0x00048B98
		public static string NumberToString(string format, sbyte value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, (int)value, 3);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0004A9C8 File Offset: 0x00048BC8
		public static string NumberToString(string format, byte value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, (int)value, 3);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0004A9F4 File Offset: 0x00048BF4
		public static string NumberToString(string format, ushort value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, (int)value, 5);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0004AA20 File Offset: 0x00048C20
		public static string NumberToString(string format, short value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, (int)value, 5);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0004AA4C File Offset: 0x00048C4C
		public static string NumberToString(string format, uint value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, value, 10);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0004AA7C File Offset: 0x00048C7C
		public static string NumberToString(string format, int value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, value, 10);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0004AAAC File Offset: 0x00048CAC
		public static string NumberToString(string format, ulong value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, value);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0004AAD8 File Offset: 0x00048CD8
		public static string NumberToString(string format, long value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, value);
			string result = instance.IntegerToString(format, fp);
			instance.Release();
			return result;
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0004AB04 File Offset: 0x00048D04
		public static string NumberToString(string format, float value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, (double)value, 7);
			NumberFormatInfo numberFormatInstance = instance.GetNumberFormatInstance(fp);
			string result;
			if (instance._NaN)
			{
				result = numberFormatInstance.NaNSymbol;
			}
			else if (instance._infinity)
			{
				if (instance._positive)
				{
					result = numberFormatInstance.PositiveInfinitySymbol;
				}
				else
				{
					result = numberFormatInstance.NegativeInfinitySymbol;
				}
			}
			else if (instance._specifier == 'R')
			{
				result = instance.FormatRoundtrip(value, numberFormatInstance);
			}
			else
			{
				result = instance.NumberToString(format, numberFormatInstance);
			}
			instance.Release();
			return result;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0004AB9C File Offset: 0x00048D9C
		public static string NumberToString(string format, double value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, value, 15);
			NumberFormatInfo numberFormatInstance = instance.GetNumberFormatInstance(fp);
			string result;
			if (instance._NaN)
			{
				result = numberFormatInstance.NaNSymbol;
			}
			else if (instance._infinity)
			{
				if (instance._positive)
				{
					result = numberFormatInstance.PositiveInfinitySymbol;
				}
				else
				{
					result = numberFormatInstance.NegativeInfinitySymbol;
				}
			}
			else if (instance._specifier == 'R')
			{
				result = instance.FormatRoundtrip(value, numberFormatInstance);
			}
			else
			{
				result = instance.NumberToString(format, numberFormatInstance);
			}
			instance.Release();
			return result;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0004AC34 File Offset: 0x00048E34
		public static string NumberToString(string format, decimal value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(format, value);
			string result = instance.NumberToString(format, instance.GetNumberFormatInstance(fp));
			instance.Release();
			return result;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0004AC68 File Offset: 0x00048E68
		public static string NumberToString(uint value, IFormatProvider fp)
		{
			if (value >= 100000000U)
			{
				return NumberFormatter.NumberToString(null, value, fp);
			}
			NumberFormatter instance = NumberFormatter.GetInstance();
			string result = instance.FastIntegerToString((int)value, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0004ACA0 File Offset: 0x00048EA0
		public static string NumberToString(int value, IFormatProvider fp)
		{
			if (value >= 100000000 || value <= -100000000)
			{
				return NumberFormatter.NumberToString(null, value, fp);
			}
			NumberFormatter instance = NumberFormatter.GetInstance();
			string result = instance.FastIntegerToString(value, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004ACE4 File Offset: 0x00048EE4
		public static string NumberToString(ulong value, IFormatProvider fp)
		{
			if (value >= 100000000UL)
			{
				return NumberFormatter.NumberToString(null, value, fp);
			}
			NumberFormatter instance = NumberFormatter.GetInstance();
			string result = instance.FastIntegerToString((int)value, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0004AD20 File Offset: 0x00048F20
		public static string NumberToString(long value, IFormatProvider fp)
		{
			if (value >= 100000000L || value <= -100000000L)
			{
				return NumberFormatter.NumberToString(null, value, fp);
			}
			NumberFormatter instance = NumberFormatter.GetInstance();
			string result = instance.FastIntegerToString((int)value, fp);
			instance.Release();
			return result;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0004AD68 File Offset: 0x00048F68
		public static string NumberToString(float value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			instance.Init(null, (double)value, 7);
			NumberFormatInfo numberFormatInstance = instance.GetNumberFormatInstance(fp);
			string result;
			if (instance._NaN)
			{
				result = numberFormatInstance.NaNSymbol;
			}
			else if (instance._infinity)
			{
				if (instance._positive)
				{
					result = numberFormatInstance.PositiveInfinitySymbol;
				}
				else
				{
					result = numberFormatInstance.NegativeInfinitySymbol;
				}
			}
			else
			{
				result = instance.FormatGeneral(-1, numberFormatInstance);
			}
			instance.Release();
			return result;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0004ADE4 File Offset: 0x00048FE4
		public static string NumberToString(double value, IFormatProvider fp)
		{
			NumberFormatter instance = NumberFormatter.GetInstance();
			NumberFormatInfo numberFormatInstance = instance.GetNumberFormatInstance(fp);
			instance.Init(null, value, 15);
			string result;
			if (instance._NaN)
			{
				result = numberFormatInstance.NaNSymbol;
			}
			else if (instance._infinity)
			{
				if (instance._positive)
				{
					result = numberFormatInstance.PositiveInfinitySymbol;
				}
				else
				{
					result = numberFormatInstance.NegativeInfinitySymbol;
				}
			}
			else
			{
				result = instance.FormatGeneral(-1, numberFormatInstance);
			}
			instance.Release();
			return result;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0004AE60 File Offset: 0x00049060
		private string FastIntegerToString(int value, IFormatProvider fp)
		{
			if (value < 0)
			{
				string negativeSign = this.GetNumberFormatInstance(fp).NegativeSign;
				this.ResetCharBuf(8 + negativeSign.Length);
				value = -value;
				this.Append(negativeSign);
			}
			else
			{
				this.ResetCharBuf(8);
			}
			if (value >= 10000)
			{
				int num = value / 10000;
				this.FastAppendDigits(num, false);
				this.FastAppendDigits(value - num * 10000, true);
			}
			else
			{
				this.FastAppendDigits(value, false);
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0004AEF0 File Offset: 0x000490F0
		private string IntegerToString(string format, IFormatProvider fp)
		{
			NumberFormatInfo numberFormatInstance = this.GetNumberFormatInstance(fp);
			char specifier = this._specifier;
			switch (specifier)
			{
			case 'C':
				return this.FormatCurrency(this._precision, numberFormatInstance);
			case 'D':
				return this.FormatDecimal(this._precision, numberFormatInstance);
			case 'E':
				return this.FormatExponential(this._precision, numberFormatInstance);
			case 'F':
				return this.FormatFixedPoint(this._precision, numberFormatInstance);
			case 'G':
				if (this._precision <= 0)
				{
					return this.FormatDecimal(-1, numberFormatInstance);
				}
				return this.FormatGeneral(this._precision, numberFormatInstance);
			default:
				if (specifier == 'X')
				{
					return this.FormatHexadecimal(this._precision);
				}
				if (this._isCustomFormat)
				{
					return this.FormatCustom(format, numberFormatInstance);
				}
				throw new FormatException("The specified format '" + format + "' is invalid");
			case 'N':
				return this.FormatNumber(this._precision, numberFormatInstance);
			case 'P':
				return this.FormatPercent(this._precision, numberFormatInstance);
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0004B008 File Offset: 0x00049208
		private string NumberToString(string format, NumberFormatInfo nfi)
		{
			char specifier = this._specifier;
			switch (specifier)
			{
			case 'C':
				return this.FormatCurrency(this._precision, nfi);
			default:
				switch (specifier)
				{
				case 'N':
					return this.FormatNumber(this._precision, nfi);
				default:
					if (specifier != 'X')
					{
					}
					if (this._isCustomFormat)
					{
						return this.FormatCustom(format, nfi);
					}
					throw new FormatException("The specified format '" + format + "' is invalid");
				case 'P':
					return this.FormatPercent(this._precision, nfi);
				}
				break;
			case 'E':
				return this.FormatExponential(this._precision, nfi);
			case 'F':
				return this.FormatFixedPoint(this._precision, nfi);
			case 'G':
				return this.FormatGeneral(this._precision, nfi);
			}
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0004B0D8 File Offset: 0x000492D8
		public string FormatCurrency(int precision, NumberFormatInfo nfi)
		{
			precision = ((precision < 0) ? nfi.CurrencyDecimalDigits : precision);
			this.RoundDecimal(precision);
			this.ResetCharBuf(this.IntegerDigits * 2 + precision * 2 + 16);
			if (this._positive)
			{
				switch (nfi.CurrencyPositivePattern)
				{
				case 0:
					this.Append(nfi.CurrencySymbol);
					break;
				case 2:
					this.Append(nfi.CurrencySymbol);
					this.Append(' ');
					break;
				}
			}
			else
			{
				switch (nfi.CurrencyNegativePattern)
				{
				case 0:
					this.Append('(');
					this.Append(nfi.CurrencySymbol);
					break;
				case 1:
					this.Append(nfi.NegativeSign);
					this.Append(nfi.CurrencySymbol);
					break;
				case 2:
					this.Append(nfi.CurrencySymbol);
					this.Append(nfi.NegativeSign);
					break;
				case 3:
					this.Append(nfi.CurrencySymbol);
					break;
				case 4:
					this.Append('(');
					break;
				case 5:
					this.Append(nfi.NegativeSign);
					break;
				case 8:
					this.Append(nfi.NegativeSign);
					break;
				case 9:
					this.Append(nfi.NegativeSign);
					this.Append(nfi.CurrencySymbol);
					this.Append(' ');
					break;
				case 11:
					this.Append(nfi.CurrencySymbol);
					this.Append(' ');
					break;
				case 12:
					this.Append(nfi.CurrencySymbol);
					this.Append(' ');
					this.Append(nfi.NegativeSign);
					break;
				case 14:
					this.Append('(');
					this.Append(nfi.CurrencySymbol);
					this.Append(' ');
					break;
				case 15:
					this.Append('(');
					break;
				}
			}
			this.AppendIntegerStringWithGroupSeparator(nfi.RawCurrencyGroupSizes, nfi.CurrencyGroupSeparator);
			if (precision > 0)
			{
				this.Append(nfi.CurrencyDecimalSeparator);
				this.AppendDecimalString(precision);
			}
			if (this._positive)
			{
				switch (nfi.CurrencyPositivePattern)
				{
				case 1:
					this.Append(nfi.CurrencySymbol);
					break;
				case 3:
					this.Append(' ');
					this.Append(nfi.CurrencySymbol);
					break;
				}
			}
			else
			{
				switch (nfi.CurrencyNegativePattern)
				{
				case 0:
					this.Append(')');
					break;
				case 3:
					this.Append(nfi.NegativeSign);
					break;
				case 4:
					this.Append(nfi.CurrencySymbol);
					this.Append(')');
					break;
				case 5:
					this.Append(nfi.CurrencySymbol);
					break;
				case 6:
					this.Append(nfi.NegativeSign);
					this.Append(nfi.CurrencySymbol);
					break;
				case 7:
					this.Append(nfi.CurrencySymbol);
					this.Append(nfi.NegativeSign);
					break;
				case 8:
					this.Append(' ');
					this.Append(nfi.CurrencySymbol);
					break;
				case 10:
					this.Append(' ');
					this.Append(nfi.CurrencySymbol);
					this.Append(nfi.NegativeSign);
					break;
				case 11:
					this.Append(nfi.NegativeSign);
					break;
				case 13:
					this.Append(nfi.NegativeSign);
					this.Append(' ');
					this.Append(nfi.CurrencySymbol);
					break;
				case 14:
					this.Append(')');
					break;
				case 15:
					this.Append(' ');
					this.Append(nfi.CurrencySymbol);
					this.Append(')');
					break;
				}
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0004B4F4 File Offset: 0x000496F4
		private string FormatDecimal(int precision, NumberFormatInfo nfi)
		{
			if (precision < this._digitsLen)
			{
				precision = this._digitsLen;
			}
			if (precision == 0)
			{
				return "0";
			}
			this.ResetCharBuf(precision + 1);
			if (!this._positive)
			{
				this.Append(nfi.NegativeSign);
			}
			this.AppendDigits(0, precision);
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0004B55C File Offset: 0x0004975C
		private unsafe string FormatHexadecimal(int precision)
		{
			int i = Math.Max(precision, this._decPointPos);
			char* ptr = (!this._specifierIsUpper) ? NumberFormatter.DigitLowerTable : NumberFormatter.DigitUpperTable;
			this.ResetCharBuf(i);
			this._ind = i;
			ulong num = (ulong)this._val1 | (ulong)this._val2 << 32;
			while (i > 0)
			{
				this._cbuf[--i] = ptr[(num & 15UL) * 2UL / 2UL];
				num >>= 4;
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0004B5EC File Offset: 0x000497EC
		public string FormatFixedPoint(int precision, NumberFormatInfo nfi)
		{
			if (precision == -1)
			{
				precision = nfi.NumberDecimalDigits;
			}
			this.RoundDecimal(precision);
			this.ResetCharBuf(this.IntegerDigits + precision + 2);
			if (!this._positive)
			{
				this.Append(nfi.NegativeSign);
			}
			this.AppendIntegerString(this.IntegerDigits);
			if (precision > 0)
			{
				this.Append(nfi.NumberDecimalSeparator);
				this.AppendDecimalString(precision);
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0004B670 File Offset: 0x00049870
		private string FormatRoundtrip(double origval, NumberFormatInfo nfi)
		{
			NumberFormatter clone = this.GetClone();
			if (origval >= -1.79769313486231E+308 && origval <= 1.79769313486231E+308)
			{
				string text = this.FormatGeneral(this._defPrecision, nfi);
				if (origval == double.Parse(text, nfi))
				{
					return text;
				}
			}
			return clone.FormatGeneral(this._defPrecision + 2, nfi);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0004B6D0 File Offset: 0x000498D0
		private string FormatRoundtrip(float origval, NumberFormatInfo nfi)
		{
			NumberFormatter clone = this.GetClone();
			string text = this.FormatGeneral(this._defPrecision, nfi);
			if (origval == float.Parse(text, nfi))
			{
				return text;
			}
			return clone.FormatGeneral(this._defPrecision + 2, nfi);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0004B710 File Offset: 0x00049910
		private string FormatGeneral(int precision, NumberFormatInfo nfi)
		{
			bool flag;
			if (precision == -1)
			{
				flag = this.IsFloatingSource;
				precision = this._defPrecision;
			}
			else
			{
				flag = true;
				if (precision == 0)
				{
					precision = this._defPrecision;
				}
				this.RoundPos(precision);
			}
			int num = this._decPointPos;
			int digitsLen = this._digitsLen;
			int num2 = digitsLen - num;
			if ((num > precision || num <= -4) && flag)
			{
				return this.FormatExponential(digitsLen - 1, nfi, 2);
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num < 0)
			{
				num = 0;
			}
			this.ResetCharBuf(num2 + num + 3);
			if (!this._positive)
			{
				this.Append(nfi.NegativeSign);
			}
			if (num == 0)
			{
				this.Append('0');
			}
			else
			{
				this.AppendDigits(digitsLen - num, digitsLen);
			}
			if (num2 > 0)
			{
				this.Append(nfi.NumberDecimalSeparator);
				this.AppendDigits(0, num2);
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0004B800 File Offset: 0x00049A00
		public string FormatNumber(int precision, NumberFormatInfo nfi)
		{
			precision = ((precision < 0) ? nfi.NumberDecimalDigits : precision);
			this.ResetCharBuf(this.IntegerDigits * 3 + precision);
			this.RoundDecimal(precision);
			if (!this._positive)
			{
				switch (nfi.NumberNegativePattern)
				{
				case 0:
					this.Append('(');
					break;
				case 1:
					this.Append(nfi.NegativeSign);
					break;
				case 2:
					this.Append(nfi.NegativeSign);
					this.Append(' ');
					break;
				}
			}
			this.AppendIntegerStringWithGroupSeparator(nfi.RawNumberGroupSizes, nfi.NumberGroupSeparator);
			if (precision > 0)
			{
				this.Append(nfi.NumberDecimalSeparator);
				this.AppendDecimalString(precision);
			}
			if (!this._positive)
			{
				switch (nfi.NumberNegativePattern)
				{
				case 0:
					this.Append(')');
					break;
				case 3:
					this.Append(nfi.NegativeSign);
					break;
				case 4:
					this.Append(' ');
					this.Append(nfi.NegativeSign);
					break;
				}
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0004B940 File Offset: 0x00049B40
		public string FormatPercent(int precision, NumberFormatInfo nfi)
		{
			precision = ((precision < 0) ? nfi.PercentDecimalDigits : precision);
			this.Multiply10(2);
			this.RoundDecimal(precision);
			this.ResetCharBuf(this.IntegerDigits * 2 + precision + 16);
			if (this._positive)
			{
				if (nfi.PercentPositivePattern == 2)
				{
					this.Append(nfi.PercentSymbol);
				}
			}
			else
			{
				switch (nfi.PercentNegativePattern)
				{
				case 0:
					this.Append(nfi.NegativeSign);
					break;
				case 1:
					this.Append(nfi.NegativeSign);
					break;
				case 2:
					this.Append(nfi.NegativeSign);
					this.Append(nfi.PercentSymbol);
					break;
				}
			}
			this.AppendIntegerStringWithGroupSeparator(nfi.RawPercentGroupSizes, nfi.PercentGroupSeparator);
			if (precision > 0)
			{
				this.Append(nfi.PercentDecimalSeparator);
				this.AppendDecimalString(precision);
			}
			if (this._positive)
			{
				int num = nfi.PercentPositivePattern;
				if (num != 0)
				{
					if (num == 1)
					{
						this.Append(nfi.PercentSymbol);
					}
				}
				else
				{
					this.Append(' ');
					this.Append(nfi.PercentSymbol);
				}
			}
			else
			{
				int num = nfi.PercentNegativePattern;
				if (num != 0)
				{
					if (num == 1)
					{
						this.Append(nfi.PercentSymbol);
					}
				}
				else
				{
					this.Append(' ');
					this.Append(nfi.PercentSymbol);
				}
			}
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0004BAE0 File Offset: 0x00049CE0
		public string FormatExponential(int precision, NumberFormatInfo nfi)
		{
			if (precision == -1)
			{
				precision = 6;
			}
			this.RoundPos(precision + 1);
			return this.FormatExponential(precision, nfi, 3);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0004BB00 File Offset: 0x00049D00
		private string FormatExponential(int precision, NumberFormatInfo nfi, int expDigits)
		{
			int decPointPos = this._decPointPos;
			int digitsLen = this._digitsLen;
			int exponent = decPointPos - 1;
			int num = this._decPointPos = 1;
			this.ResetCharBuf(precision + 8);
			if (!this._positive)
			{
				this.Append(nfi.NegativeSign);
			}
			this.AppendOneDigit(digitsLen - 1);
			if (precision > 0)
			{
				this.Append(nfi.NumberDecimalSeparator);
				this.AppendDigits(digitsLen - precision - 1, digitsLen - this._decPointPos);
			}
			this.AppendExponent(nfi, exponent, expDigits);
			return new string(this._cbuf, 0, this._ind);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0004BB94 File Offset: 0x00049D94
		public string FormatCustom(string format, NumberFormatInfo nfi)
		{
			bool positive = this._positive;
			int offset = 0;
			int num = 0;
			NumberFormatter.CustomInfo.GetActiveSection(format, ref positive, this.IsZero, ref offset, ref num);
			if (num == 0)
			{
				return (!this._positive) ? nfi.NegativeSign : string.Empty;
			}
			this._positive = positive;
			NumberFormatter.CustomInfo customInfo = NumberFormatter.CustomInfo.Parse(format, offset, num, nfi);
			StringBuilder stringBuilder = new StringBuilder(customInfo.IntegerDigits * 2);
			StringBuilder stringBuilder2 = new StringBuilder(customInfo.DecimalDigits * 2);
			StringBuilder stringBuilder3 = (!customInfo.UseExponent) ? null : new StringBuilder(customInfo.ExponentDigits * 2);
			int num2 = 0;
			if (customInfo.Percents > 0)
			{
				this.Multiply10(2 * customInfo.Percents);
			}
			if (customInfo.Permilles > 0)
			{
				this.Multiply10(3 * customInfo.Permilles);
			}
			if (customInfo.DividePlaces > 0)
			{
				this.Divide10(customInfo.DividePlaces);
			}
			bool flag = true;
			if (customInfo.UseExponent && (customInfo.DecimalDigits > 0 || customInfo.IntegerDigits > 0))
			{
				if (!this.IsZero)
				{
					this.RoundPos(customInfo.DecimalDigits + customInfo.IntegerDigits);
					num2 -= this._decPointPos - customInfo.IntegerDigits;
					this._decPointPos = customInfo.IntegerDigits;
				}
				flag = (num2 <= 0);
				NumberFormatter.AppendNonNegativeNumber(stringBuilder3, (num2 >= 0) ? num2 : (-num2));
			}
			else
			{
				this.RoundDecimal(customInfo.DecimalDigits);
			}
			if (customInfo.IntegerDigits != 0 || !this.IsZeroInteger)
			{
				this.AppendIntegerString(this.IntegerDigits, stringBuilder);
			}
			this.AppendDecimalString(this.DecimalDigits, stringBuilder2);
			if (customInfo.UseExponent)
			{
				if (customInfo.DecimalDigits <= 0 && customInfo.IntegerDigits <= 0)
				{
					this._positive = true;
				}
				if (stringBuilder.Length < customInfo.IntegerDigits)
				{
					stringBuilder.Insert(0, "0", customInfo.IntegerDigits - stringBuilder.Length);
				}
				while (stringBuilder3.Length < customInfo.ExponentDigits - customInfo.ExponentTailSharpDigits)
				{
					stringBuilder3.Insert(0, '0');
				}
				if (flag && !customInfo.ExponentNegativeSignOnly)
				{
					stringBuilder3.Insert(0, nfi.PositiveSign);
				}
				else if (!flag)
				{
					stringBuilder3.Insert(0, nfi.NegativeSign);
				}
			}
			else
			{
				if (stringBuilder.Length < customInfo.IntegerDigits - customInfo.IntegerHeadSharpDigits)
				{
					stringBuilder.Insert(0, "0", customInfo.IntegerDigits - customInfo.IntegerHeadSharpDigits - stringBuilder.Length);
				}
				if (customInfo.IntegerDigits == customInfo.IntegerHeadSharpDigits && NumberFormatter.IsZeroOnly(stringBuilder))
				{
					stringBuilder.Remove(0, stringBuilder.Length);
				}
			}
			NumberFormatter.ZeroTrimEnd(stringBuilder2, true);
			while (stringBuilder2.Length < customInfo.DecimalDigits - customInfo.DecimalTailSharpDigits)
			{
				stringBuilder2.Append('0');
			}
			if (stringBuilder2.Length > customInfo.DecimalDigits)
			{
				stringBuilder2.Remove(customInfo.DecimalDigits, stringBuilder2.Length - customInfo.DecimalDigits);
			}
			return customInfo.Format(format, offset, num, nfi, this._positive, stringBuilder, stringBuilder2, stringBuilder3);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0004BEE8 File Offset: 0x0004A0E8
		private static void ZeroTrimEnd(StringBuilder sb, bool canEmpty)
		{
			int num = 0;
			int num2 = sb.Length - 1;
			while ((!canEmpty) ? (num2 > 0) : (num2 >= 0))
			{
				if (sb[num2] != '0')
				{
					break;
				}
				num++;
				num2--;
			}
			if (num > 0)
			{
				sb.Remove(sb.Length - num, num);
			}
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0004BF54 File Offset: 0x0004A154
		private static bool IsZeroOnly(StringBuilder sb)
		{
			for (int i = 0; i < sb.Length; i++)
			{
				if (char.IsDigit(sb[i]) && sb[i] != '0')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0004BF9C File Offset: 0x0004A19C
		private static void AppendNonNegativeNumber(StringBuilder sb, int v)
		{
			if (v < 0)
			{
				throw new ArgumentException();
			}
			int num = NumberFormatter.ScaleOrder((long)v) - 1;
			do
			{
				int num2 = v / (int)NumberFormatter.GetTenPowerOf(num);
				sb.Append((char)(48 | num2));
				v -= (int)NumberFormatter.GetTenPowerOf(num--) * num2;
			}
			while (num >= 0);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0004BFF0 File Offset: 0x0004A1F0
		private void AppendIntegerString(int minLength, StringBuilder sb)
		{
			if (this._decPointPos <= 0)
			{
				sb.Append('0', minLength);
				return;
			}
			if (this._decPointPos < minLength)
			{
				sb.Append('0', minLength - this._decPointPos);
			}
			this.AppendDigits(this._digitsLen - this._decPointPos, this._digitsLen, sb);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004C04C File Offset: 0x0004A24C
		private void AppendIntegerString(int minLength)
		{
			if (this._decPointPos <= 0)
			{
				this.Append('0', minLength);
				return;
			}
			if (this._decPointPos < minLength)
			{
				this.Append('0', minLength - this._decPointPos);
			}
			this.AppendDigits(this._digitsLen - this._decPointPos, this._digitsLen);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0004C0A4 File Offset: 0x0004A2A4
		private void AppendDecimalString(int precision, StringBuilder sb)
		{
			this.AppendDigits(this._digitsLen - precision - this._decPointPos, this._digitsLen - this._decPointPos, sb);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0004C0CC File Offset: 0x0004A2CC
		private void AppendDecimalString(int precision)
		{
			this.AppendDigits(this._digitsLen - precision - this._decPointPos, this._digitsLen - this._decPointPos);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0004C0FC File Offset: 0x0004A2FC
		private void AppendIntegerStringWithGroupSeparator(int[] groups, string groupSeparator)
		{
			if (this.IsZeroInteger)
			{
				this.Append('0');
				return;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < groups.Length; i++)
			{
				num += groups[i];
				if (num > this._decPointPos)
				{
					break;
				}
				num2 = i;
			}
			if (groups.Length > 0 && num > 0)
			{
				int num3 = groups[num2];
				int num4 = (this._decPointPos <= num) ? 0 : (this._decPointPos - num);
				if (num3 == 0)
				{
					while (num2 >= 0 && groups[num2] == 0)
					{
						num2--;
					}
					num3 = ((num4 <= 0) ? groups[num2] : num4);
				}
				int num5;
				if (num4 == 0)
				{
					num5 = num3;
				}
				else
				{
					num2 += num4 / num3;
					num5 = num4 % num3;
					if (num5 == 0)
					{
						num5 = num3;
					}
					else
					{
						num2++;
					}
				}
				int num6 = 0;
				while (this._decPointPos - num6 > num5 && num5 != 0)
				{
					this.AppendDigits(this._digitsLen - num6 - num5, this._digitsLen - num6);
					num6 += num5;
					this.Append(groupSeparator);
					if (--num2 < groups.Length && num2 >= 0)
					{
						num3 = groups[num2];
					}
					num5 = num3;
				}
				this.AppendDigits(this._digitsLen - this._decPointPos, this._digitsLen - num6);
			}
			else
			{
				this.AppendDigits(this._digitsLen - this._decPointPos, this._digitsLen);
			}
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0004C28C File Offset: 0x0004A48C
		private void AppendExponent(NumberFormatInfo nfi, int exponent, int minDigits)
		{
			if (this._specifierIsUpper || this._specifier == 'R')
			{
				this.Append('E');
			}
			else
			{
				this.Append('e');
			}
			if (exponent >= 0)
			{
				this.Append(nfi.PositiveSign);
			}
			else
			{
				this.Append(nfi.NegativeSign);
				exponent = -exponent;
			}
			if (exponent == 0)
			{
				this.Append('0', minDigits);
			}
			else if (exponent < 10)
			{
				this.Append('0', minDigits - 1);
				this.Append((char)(48 | exponent));
			}
			else
			{
				uint num = NumberFormatter.FastToDecHex(exponent);
				if (exponent >= 100 || minDigits == 3)
				{
					this.Append((char)(48U | num >> 8));
				}
				this.Append((char)(48U | (num >> 4 & 15U)));
				this.Append((char)(48U | (num & 15U)));
			}
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004C368 File Offset: 0x0004A568
		private void AppendOneDigit(int start)
		{
			if (this._ind == this._cbuf.Length)
			{
				this.Resize(this._ind + 10);
			}
			start += this._offset;
			uint num;
			if (start < 0)
			{
				num = 0U;
			}
			else if (start < 8)
			{
				num = this._val1;
			}
			else if (start < 16)
			{
				num = this._val2;
			}
			else if (start < 24)
			{
				num = this._val3;
			}
			else if (start < 32)
			{
				num = this._val4;
			}
			else
			{
				num = 0U;
			}
			num >>= (start & 7) << 2;
			this._cbuf[this._ind++] = (char)(48U | (num & 15U));
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0004C42C File Offset: 0x0004A62C
		private unsafe void FastAppendDigits(int val, bool force)
		{
			int ind = this._ind;
			int num2;
			if (force || val >= 100)
			{
				int num = val * 5243 >> 19;
				num2 = NumberFormatter.DecHexDigits[num];
				if (force || val >= 1000)
				{
					this._cbuf[ind++] = (char)(48 | num2 >> 4);
				}
				this._cbuf[ind++] = (char)(48 | (num2 & 15));
				num2 = NumberFormatter.DecHexDigits[val - num * 100];
			}
			else
			{
				num2 = NumberFormatter.DecHexDigits[val];
			}
			if (force || val >= 10)
			{
				this._cbuf[ind++] = (char)(48 | num2 >> 4);
			}
			this._cbuf[ind++] = (char)(48 | (num2 & 15));
			this._ind = ind;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0004C4F8 File Offset: 0x0004A6F8
		private void AppendDigits(int start, int end)
		{
			if (start >= end)
			{
				return;
			}
			int num = this._ind + (end - start);
			if (num > this._cbuf.Length)
			{
				this.Resize(num + 10);
			}
			this._ind = num;
			end += this._offset;
			start += this._offset;
			int num2 = start + 8 - (start & 7);
			for (;;)
			{
				uint num3;
				if (num2 == 8)
				{
					num3 = this._val1;
				}
				else if (num2 == 16)
				{
					num3 = this._val2;
				}
				else if (num2 == 24)
				{
					num3 = this._val3;
				}
				else if (num2 == 32)
				{
					num3 = this._val4;
				}
				else
				{
					num3 = 0U;
				}
				num3 >>= (start & 7) << 2;
				if (num2 > end)
				{
					num2 = end;
				}
				this._cbuf[--num] = (char)(48U | (num3 & 15U));
				switch (num2 - start)
				{
				case 1:
					goto IL_1C8;
				case 2:
					goto IL_1AB;
				case 3:
					goto IL_18E;
				case 4:
					goto IL_171;
				case 5:
					goto IL_154;
				case 6:
					goto IL_137;
				case 7:
					goto IL_11A;
				case 8:
					this._cbuf[--num] = (char)(48U | ((num3 >>= 4) & 15U));
					goto IL_11A;
				}
				IL_1D5:
				start = num2;
				num2 += 8;
				continue;
				IL_1C8:
				if (num2 == end)
				{
					break;
				}
				goto IL_1D5;
				IL_1AB:
				this._cbuf[--num] = (char)(48U | (num3 >> 4 & 15U));
				goto IL_1C8;
				IL_18E:
				this._cbuf[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_1AB;
				IL_171:
				this._cbuf[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_18E;
				IL_154:
				this._cbuf[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_171;
				IL_137:
				this._cbuf[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_154;
				IL_11A:
				this._cbuf[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_137;
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0004C6E8 File Offset: 0x0004A8E8
		private void AppendDigits(int start, int end, StringBuilder sb)
		{
			if (start >= end)
			{
				return;
			}
			int num = sb.Length + (end - start);
			sb.Length = num;
			end += this._offset;
			start += this._offset;
			int num2 = start + 8 - (start & 7);
			for (;;)
			{
				uint num3;
				if (num2 == 8)
				{
					num3 = this._val1;
				}
				else if (num2 == 16)
				{
					num3 = this._val2;
				}
				else if (num2 == 24)
				{
					num3 = this._val3;
				}
				else if (num2 == 32)
				{
					num3 = this._val4;
				}
				else
				{
					num3 = 0U;
				}
				num3 >>= (start & 7) << 2;
				if (num2 > end)
				{
					num2 = end;
				}
				sb[--num] = (char)(48U | (num3 & 15U));
				switch (num2 - start)
				{
				case 1:
					goto IL_1A8;
				case 2:
					goto IL_18C;
				case 3:
					goto IL_170;
				case 4:
					goto IL_154;
				case 5:
					goto IL_138;
				case 6:
					goto IL_11C;
				case 7:
					goto IL_100;
				case 8:
					sb[--num] = (char)(48U | ((num3 >>= 4) & 15U));
					goto IL_100;
				}
				IL_1B5:
				start = num2;
				num2 += 8;
				continue;
				IL_1A8:
				if (num2 == end)
				{
					break;
				}
				goto IL_1B5;
				IL_18C:
				sb[--num] = (char)(48U | (num3 >> 4 & 15U));
				goto IL_1A8;
				IL_170:
				sb[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_18C;
				IL_154:
				sb[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_170;
				IL_138:
				sb[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_154;
				IL_11C:
				sb[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_138;
				IL_100:
				sb[--num] = (char)(48U | ((num3 >>= 4) & 15U));
				goto IL_11C;
			}
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0004C8B8 File Offset: 0x0004AAB8
		private void Multiply10(int count)
		{
			if (count <= 0 || this._digitsLen == 0)
			{
				return;
			}
			this._decPointPos += count;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0004C8DC File Offset: 0x0004AADC
		private void Divide10(int count)
		{
			if (count <= 0 || this._digitsLen == 0)
			{
				return;
			}
			this._decPointPos -= count;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0004C900 File Offset: 0x0004AB00
		private NumberFormatter GetClone()
		{
			return (NumberFormatter)base.MemberwiseClone();
		}

		// Token: 0x0400054E RID: 1358
		private const int DefaultExpPrecision = 6;

		// Token: 0x0400054F RID: 1359
		private const int HundredMillion = 100000000;

		// Token: 0x04000550 RID: 1360
		private const long SeventeenDigitsThreshold = 10000000000000000L;

		// Token: 0x04000551 RID: 1361
		private const ulong ULongDivHundredMillion = 184467440737UL;

		// Token: 0x04000552 RID: 1362
		private const ulong ULongModHundredMillion = 9551616UL;

		// Token: 0x04000553 RID: 1363
		private const int DoubleBitsExponentShift = 52;

		// Token: 0x04000554 RID: 1364
		private const int DoubleBitsExponentMask = 2047;

		// Token: 0x04000555 RID: 1365
		private const long DoubleBitsMantissaMask = 4503599627370495L;

		// Token: 0x04000556 RID: 1366
		private const int DecimalBitsScaleMask = 2031616;

		// Token: 0x04000557 RID: 1367
		private const int SingleDefPrecision = 7;

		// Token: 0x04000558 RID: 1368
		private const int DoubleDefPrecision = 15;

		// Token: 0x04000559 RID: 1369
		private const int Int8DefPrecision = 3;

		// Token: 0x0400055A RID: 1370
		private const int UInt8DefPrecision = 3;

		// Token: 0x0400055B RID: 1371
		private const int Int16DefPrecision = 5;

		// Token: 0x0400055C RID: 1372
		private const int UInt16DefPrecision = 5;

		// Token: 0x0400055D RID: 1373
		private const int Int32DefPrecision = 10;

		// Token: 0x0400055E RID: 1374
		private const int UInt32DefPrecision = 10;

		// Token: 0x0400055F RID: 1375
		private const int Int64DefPrecision = 19;

		// Token: 0x04000560 RID: 1376
		private const int UInt64DefPrecision = 20;

		// Token: 0x04000561 RID: 1377
		private const int DecimalDefPrecision = 100;

		// Token: 0x04000562 RID: 1378
		private const int TenPowersListLength = 19;

		// Token: 0x04000563 RID: 1379
		private const double MinRoundtripVal = -1.79769313486231E+308;

		// Token: 0x04000564 RID: 1380
		private const double MaxRoundtripVal = 1.79769313486231E+308;

		// Token: 0x04000565 RID: 1381
		private unsafe static readonly ulong* MantissaBitsTable;

		// Token: 0x04000566 RID: 1382
		private unsafe static readonly int* TensExponentTable;

		// Token: 0x04000567 RID: 1383
		private unsafe static readonly char* DigitLowerTable;

		// Token: 0x04000568 RID: 1384
		private unsafe static readonly char* DigitUpperTable;

		// Token: 0x04000569 RID: 1385
		private unsafe static readonly long* TenPowersList;

		// Token: 0x0400056A RID: 1386
		private unsafe static readonly int* DecHexDigits;

		// Token: 0x0400056B RID: 1387
		private Thread _thread;

		// Token: 0x0400056C RID: 1388
		private NumberFormatInfo _nfi;

		// Token: 0x0400056D RID: 1389
		private bool _NaN;

		// Token: 0x0400056E RID: 1390
		private bool _infinity;

		// Token: 0x0400056F RID: 1391
		private bool _isCustomFormat;

		// Token: 0x04000570 RID: 1392
		private bool _specifierIsUpper;

		// Token: 0x04000571 RID: 1393
		private bool _positive;

		// Token: 0x04000572 RID: 1394
		private char _specifier;

		// Token: 0x04000573 RID: 1395
		private int _precision;

		// Token: 0x04000574 RID: 1396
		private int _defPrecision;

		// Token: 0x04000575 RID: 1397
		private int _digitsLen;

		// Token: 0x04000576 RID: 1398
		private int _offset;

		// Token: 0x04000577 RID: 1399
		private int _decPointPos;

		// Token: 0x04000578 RID: 1400
		private uint _val1;

		// Token: 0x04000579 RID: 1401
		private uint _val2;

		// Token: 0x0400057A RID: 1402
		private uint _val3;

		// Token: 0x0400057B RID: 1403
		private uint _val4;

		// Token: 0x0400057C RID: 1404
		private char[] _cbuf;

		// Token: 0x0400057D RID: 1405
		private int _ind;

		// Token: 0x0400057E RID: 1406
		[ThreadStatic]
		private static NumberFormatter threadNumberFormatter;

		// Token: 0x02000164 RID: 356
		private class CustomInfo
		{
			// Token: 0x06001337 RID: 4919 RVA: 0x0004C928 File Offset: 0x0004AB28
			public static void GetActiveSection(string format, ref bool positive, bool zero, ref int offset, ref int length)
			{
				int[] array = new int[3];
				int num = 0;
				int num2 = 0;
				char c = '\0';
				for (int i = 0; i < format.Length; i++)
				{
					char c2 = format[i];
					if (c2 == c || (c == '\0' && (c2 == '"' || c2 == '\'')))
					{
						if (c == '\0')
						{
							c = c2;
						}
						else
						{
							c = '\0';
						}
					}
					else if (c == '\0' && format[i] == ';' && (i == 0 || format[i - 1] != '\\'))
					{
						array[num++] = i - num2;
						num2 = i + 1;
						if (num == 3)
						{
							break;
						}
					}
				}
				if (num == 0)
				{
					offset = 0;
					length = format.Length;
					return;
				}
				if (num == 1)
				{
					if (positive || zero)
					{
						offset = 0;
						length = array[0];
						return;
					}
					if (array[0] + 1 < format.Length)
					{
						positive = true;
						offset = array[0] + 1;
						length = format.Length - offset;
						return;
					}
					offset = 0;
					length = array[0];
					return;
				}
				else if (num == 2)
				{
					if (zero)
					{
						offset = array[0] + array[1] + 2;
						length = format.Length - offset;
						return;
					}
					if (positive)
					{
						offset = 0;
						length = array[0];
						return;
					}
					if (array[1] > 0)
					{
						positive = true;
						offset = array[0] + 1;
						length = array[1];
						return;
					}
					offset = 0;
					length = array[0];
					return;
				}
				else
				{
					if (num != 3)
					{
						throw new ArgumentException();
					}
					if (zero)
					{
						offset = array[0] + array[1] + 2;
						length = array[2];
						return;
					}
					if (positive)
					{
						offset = 0;
						length = array[0];
						return;
					}
					if (array[1] > 0)
					{
						positive = true;
						offset = array[0] + 1;
						length = array[1];
						return;
					}
					offset = 0;
					length = array[0];
					return;
				}
			}

			// Token: 0x06001338 RID: 4920 RVA: 0x0004CAFC File Offset: 0x0004ACFC
			public static NumberFormatter.CustomInfo Parse(string format, int offset, int length, NumberFormatInfo nfi)
			{
				char c = '\0';
				bool flag = true;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = true;
				NumberFormatter.CustomInfo customInfo = new NumberFormatter.CustomInfo();
				int num = 0;
				int num2 = offset;
				while (num2 - offset < length)
				{
					char c2 = format[num2];
					if (c2 == c && c2 != '\0')
					{
						c = '\0';
					}
					else if (c == '\0')
					{
						if (flag3 && c2 != '\0' && c2 != '0' && c2 != '#')
						{
							flag3 = false;
							flag = (customInfo.DecimalPointPos < 0);
							flag2 = !flag;
							num2--;
						}
						else
						{
							char c3 = c2;
							switch (c3)
							{
							case '"':
							case '\'':
								if (c2 == '"' || c2 == '\'')
								{
									c = c2;
								}
								goto IL_311;
							case '#':
								if (flag4 && flag)
								{
									customInfo.IntegerHeadSharpDigits++;
								}
								else if (flag2)
								{
									customInfo.DecimalTailSharpDigits++;
								}
								else if (flag3)
								{
									customInfo.ExponentTailSharpDigits++;
								}
								break;
							default:
								switch (c3)
								{
								case ',':
									if (flag && customInfo.IntegerDigits > 0)
									{
										num++;
									}
									goto IL_311;
								default:
									if (c3 != 'E')
									{
										if (c3 == '\\')
										{
											num2++;
											goto IL_311;
										}
										if (c3 != 'e')
										{
											if (c3 != '‰')
											{
												goto IL_311;
											}
											customInfo.Permilles++;
											goto IL_311;
										}
									}
									if (customInfo.UseExponent)
									{
										goto IL_311;
									}
									customInfo.UseExponent = true;
									flag = false;
									flag2 = false;
									flag3 = true;
									if (num2 + 1 - offset < length)
									{
										char c4 = format[num2 + 1];
										if (c4 == '+')
										{
											customInfo.ExponentNegativeSignOnly = false;
										}
										if (c4 == '+' || c4 == '-')
										{
											num2++;
										}
										else if (c4 != '0' && c4 != '#')
										{
											customInfo.UseExponent = false;
											if (customInfo.DecimalPointPos < 0)
											{
												flag = true;
											}
										}
									}
									goto IL_311;
								case '.':
									flag = false;
									flag2 = true;
									flag3 = false;
									if (customInfo.DecimalPointPos == -1)
									{
										customInfo.DecimalPointPos = num2;
									}
									goto IL_311;
								case '0':
									break;
								}
								break;
							case '%':
								customInfo.Percents++;
								goto IL_311;
							}
							if (c2 != '#')
							{
								flag4 = false;
								if (flag2)
								{
									customInfo.DecimalTailSharpDigits = 0;
								}
								else if (flag3)
								{
									customInfo.ExponentTailSharpDigits = 0;
								}
							}
							if (customInfo.IntegerHeadPos == -1)
							{
								customInfo.IntegerHeadPos = num2;
							}
							if (flag)
							{
								customInfo.IntegerDigits++;
								if (num > 0)
								{
									customInfo.UseGroup = true;
								}
								num = 0;
							}
							else if (flag2)
							{
								customInfo.DecimalDigits++;
							}
							else if (flag3)
							{
								customInfo.ExponentDigits++;
							}
						}
					}
					IL_311:
					num2++;
				}
				if (customInfo.ExponentDigits == 0)
				{
					customInfo.UseExponent = false;
				}
				else
				{
					customInfo.IntegerHeadSharpDigits = 0;
				}
				if (customInfo.DecimalDigits == 0)
				{
					customInfo.DecimalPointPos = -1;
				}
				customInfo.DividePlaces += num * 3;
				return customInfo;
			}

			// Token: 0x06001339 RID: 4921 RVA: 0x0004CE74 File Offset: 0x0004B074
			public string Format(string format, int offset, int length, NumberFormatInfo nfi, bool positive, StringBuilder sb_int, StringBuilder sb_dec, StringBuilder sb_exp)
			{
				StringBuilder stringBuilder = new StringBuilder();
				char c = '\0';
				bool flag = true;
				bool flag2 = false;
				int num = 0;
				int i = 0;
				int num2 = 0;
				int[] rawNumberGroupSizes = nfi.RawNumberGroupSizes;
				string numberGroupSeparator = nfi.NumberGroupSeparator;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				if (this.UseGroup && rawNumberGroupSizes.Length > 0)
				{
					num3 = sb_int.Length;
					for (int j = 0; j < rawNumberGroupSizes.Length; j++)
					{
						num4 += rawNumberGroupSizes[j];
						if (num4 <= num3)
						{
							num5 = j;
						}
					}
					num7 = rawNumberGroupSizes[num5];
					int num8 = (num3 <= num4) ? 0 : (num3 - num4);
					if (num7 == 0)
					{
						while (num5 >= 0 && rawNumberGroupSizes[num5] == 0)
						{
							num5--;
						}
						num7 = ((num8 <= 0) ? rawNumberGroupSizes[num5] : num8);
					}
					if (num8 == 0)
					{
						num6 = num7;
					}
					else
					{
						num5 += num8 / num7;
						num6 = num8 % num7;
						if (num6 == 0)
						{
							num6 = num7;
						}
						else
						{
							num5++;
						}
					}
				}
				else
				{
					this.UseGroup = false;
				}
				int num9 = offset;
				while (num9 - offset < length)
				{
					char c2 = format[num9];
					if (c2 == c && c2 != '\0')
					{
						c = '\0';
					}
					else if (c != '\0')
					{
						stringBuilder.Append(c2);
					}
					else
					{
						char c3 = c2;
						switch (c3)
						{
						case '"':
						case '\'':
							if (c2 == '"' || c2 == '\'')
							{
								c = c2;
							}
							goto IL_474;
						case '#':
							break;
						default:
							switch (c3)
							{
							case ',':
								goto IL_474;
							default:
							{
								if (c3 != 'E')
								{
									if (c3 == '\\')
									{
										num9++;
										if (num9 - offset < length)
										{
											stringBuilder.Append(format[num9]);
										}
										goto IL_474;
									}
									if (c3 != 'e')
									{
										if (c3 != '‰')
										{
											stringBuilder.Append(c2);
											goto IL_474;
										}
										stringBuilder.Append(nfi.PerMilleSymbol);
										goto IL_474;
									}
								}
								if (sb_exp == null || !this.UseExponent)
								{
									stringBuilder.Append(c2);
									goto IL_474;
								}
								bool flag3 = true;
								bool flag4 = false;
								int num10 = num9 + 1;
								while (num10 - offset < length)
								{
									if (format[num10] == '0')
									{
										flag4 = true;
									}
									else if (num10 != num9 + 1 || (format[num10] != '+' && format[num10] != '-'))
									{
										if (!flag4)
										{
											flag3 = false;
										}
										break;
									}
									num10++;
								}
								if (flag3)
								{
									num9 = num10 - 1;
									flag = (this.DecimalPointPos < 0);
									flag2 = !flag;
									stringBuilder.Append(c2);
									stringBuilder.Append(sb_exp);
									sb_exp = null;
								}
								else
								{
									stringBuilder.Append(c2);
								}
								goto IL_474;
							}
							case '.':
								if (this.DecimalPointPos == num9)
								{
									if (this.DecimalDigits > 0)
									{
										while (i < sb_int.Length)
										{
											stringBuilder.Append(sb_int[i++]);
										}
									}
									if (sb_dec.Length > 0)
									{
										stringBuilder.Append(nfi.NumberDecimalSeparator);
									}
								}
								flag = false;
								flag2 = true;
								goto IL_474;
							case '0':
								break;
							}
							break;
						case '%':
							stringBuilder.Append(nfi.PercentSymbol);
							goto IL_474;
						}
						if (flag)
						{
							num++;
							if (this.IntegerDigits - num < sb_int.Length + i || c2 == '0')
							{
								while (this.IntegerDigits - num + i < sb_int.Length)
								{
									stringBuilder.Append(sb_int[i++]);
									if (this.UseGroup && --num3 > 0 && --num6 == 0)
									{
										stringBuilder.Append(numberGroupSeparator);
										if (--num5 < rawNumberGroupSizes.Length && num5 >= 0)
										{
											num7 = rawNumberGroupSizes[num5];
										}
										num6 = num7;
									}
								}
							}
						}
						else if (flag2)
						{
							if (num2 < sb_dec.Length)
							{
								stringBuilder.Append(sb_dec[num2++]);
							}
						}
						else
						{
							stringBuilder.Append(c2);
						}
					}
					IL_474:
					num9++;
				}
				if (!positive)
				{
					stringBuilder.Insert(0, nfi.NegativeSign);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x0400057F RID: 1407
			public bool UseGroup;

			// Token: 0x04000580 RID: 1408
			public int DecimalDigits;

			// Token: 0x04000581 RID: 1409
			public int DecimalPointPos = -1;

			// Token: 0x04000582 RID: 1410
			public int DecimalTailSharpDigits;

			// Token: 0x04000583 RID: 1411
			public int IntegerDigits;

			// Token: 0x04000584 RID: 1412
			public int IntegerHeadSharpDigits;

			// Token: 0x04000585 RID: 1413
			public int IntegerHeadPos;

			// Token: 0x04000586 RID: 1414
			public bool UseExponent;

			// Token: 0x04000587 RID: 1415
			public int ExponentDigits;

			// Token: 0x04000588 RID: 1416
			public int ExponentTailSharpDigits;

			// Token: 0x04000589 RID: 1417
			public bool ExponentNegativeSignOnly = true;

			// Token: 0x0400058A RID: 1418
			public int DividePlaces;

			// Token: 0x0400058B RID: 1419
			public int Percents;

			// Token: 0x0400058C RID: 1420
			public int Permilles;
		}
	}
}
