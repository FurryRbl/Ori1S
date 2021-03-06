using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	/// <summary>Represents a 64-bit signed integer.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200000F RID: 15
	[ComVisible(true)]
	[Serializable]
	public struct Int64 : IFormattable, IConvertible, IComparable, IComparable<long>, IEquatable<long>
	{
		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600008D RID: 141 RVA: 0x0000356C File Offset: 0x0000176C
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600008E RID: 142 RVA: 0x00003578 File Offset: 0x00001778
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Char" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600008F RID: 143 RVA: 0x00003584 File Offset: 0x00001784
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />. </summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000090 RID: 144 RVA: 0x00003590 File Offset: 0x00001790
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000091 RID: 145 RVA: 0x0000359C File Offset: 0x0000179C
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000092 RID: 146 RVA: 0x000035A8 File Offset: 0x000017A8
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000093 RID: 147 RVA: 0x000035B4 File Offset: 0x000017B4
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000094 RID: 148 RVA: 0x000035C0 File Offset: 0x000017C0
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000095 RID: 149 RVA: 0x000035CC File Offset: 0x000017CC
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.SByte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000096 RID: 150 RVA: 0x000035D8 File Offset: 0x000017D8
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000097 RID: 151 RVA: 0x000035E4 File Offset: 0x000017E4
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to <paramref name="type" />.</returns>
		/// <param name="type">The type to which to convert this <see cref="T:System.Int64" /> value.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that provides information about the format of the returned value.</param>
		// Token: 0x06000098 RID: 152 RVA: 0x000035F0 File Offset: 0x000017F0
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000099 RID: 153 RVA: 0x00003620 File Offset: 0x00001820
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600009A RID: 154 RVA: 0x0000362C File Offset: 0x0000182C
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600009B RID: 155 RVA: 0x00003638 File Offset: 0x00001838
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not an <see cref="T:System.Int64" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600009C RID: 156 RVA: 0x00003644 File Offset: 0x00001844
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is long))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Int64"));
			}
			long num = (long)value;
			if (this == num)
			{
				return 0;
			}
			return (this >= num) ? 1 : -1;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of an <see cref="T:System.Int64" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600009D RID: 157 RVA: 0x00003694 File Offset: 0x00001894
		public override bool Equals(object obj)
		{
			return obj is long && (long)obj == this;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600009E RID: 158 RVA: 0x000036B0 File Offset: 0x000018B0
		public override int GetHashCode()
		{
			return (int)(this & (long)((ulong)-1)) ^ (int)(this >> 32);
		}

		/// <summary>Compares this instance to a specified 64-bit signed integer and returns an indication of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />. </returns>
		/// <param name="value">An integer to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600009F RID: 159 RVA: 0x000036C0 File Offset: 0x000018C0
		public int CompareTo(long value)
		{
			if (this == value)
			{
				return 0;
			}
			if (this > value)
			{
				return 1;
			}
			return -1;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.Int64" /> value.</summary>
		/// <returns>true if <paramref name="obj" /> has the same value as this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Int64" /> value to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000A0 RID: 160 RVA: 0x000036D8 File Offset: 0x000018D8
		public bool Equals(long obj)
		{
			return obj == this;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000036E0 File Offset: 0x000018E0
		internal static bool Parse(string s, bool tryParse, out long result, out Exception exc)
		{
			long num = 0L;
			int num2 = 1;
			bool flag = false;
			result = 0L;
			exc = null;
			if (s == null)
			{
				if (!tryParse)
				{
					exc = new ArgumentNullException("s");
				}
				return false;
			}
			int length = s.Length;
			int i;
			char c;
			for (i = 0; i < length; i++)
			{
				c = s[i];
				if (!char.IsWhiteSpace(c))
				{
					break;
				}
			}
			if (i == length)
			{
				if (!tryParse)
				{
					exc = int.GetFormatException();
				}
				return false;
			}
			c = s[i];
			if (c == '+')
			{
				i++;
			}
			else if (c == '-')
			{
				num2 = -1;
				i++;
			}
			while (i < length)
			{
				c = s[i];
				if (c >= '0' && c <= '9')
				{
					byte b = (byte)(c - '0');
					if (num <= 922337203685477580L)
					{
						if (num != 922337203685477580L)
						{
							num = num * 10L + (long)b;
							flag = true;
							goto IL_166;
						}
						if ((long)b <= 7L || (num2 != 1 && (long)b <= 8L))
						{
							if (num2 == -1)
							{
								num = num * (long)num2 * 10L - (long)b;
							}
							else
							{
								num = num * 10L + (long)b;
							}
							if (int.ProcessTrailingWhitespace(tryParse, s, i + 1, ref exc))
							{
								result = num;
								return true;
							}
						}
					}
					if (!tryParse)
					{
						exc = new OverflowException("Value is too large");
					}
					return false;
				}
				if (!int.ProcessTrailingWhitespace(tryParse, s, i, ref exc))
				{
					return false;
				}
				IL_166:
				i++;
			}
			if (!flag)
			{
				if (!tryParse)
				{
					exc = int.GetFormatException();
				}
				return false;
			}
			if (num2 == -1)
			{
				result = num * (long)num2;
			}
			else
			{
				result = num;
			}
			return true;
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its 64-bit signed integer equivalent.</summary>
		/// <returns>A 64-bit signed integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060000A2 RID: 162 RVA: 0x000038A0 File Offset: 0x00001AA0
		public static long Parse(string s, IFormatProvider provider)
		{
			return long.Parse(s, NumberStyles.Integer, provider);
		}

		/// <summary>Converts the string representation of a number in a specified style to its 64-bit signed integer equivalent.</summary>
		/// <returns>A 64-bit signed integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" /> values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format compliant with <paramref name="style" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.-or-<paramref name="style" /> supports fractional digits but <paramref name="s" /> includes non-zero fractional digits. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060000A3 RID: 163 RVA: 0x000038AC File Offset: 0x00001AAC
		public static long Parse(string s, NumberStyles style)
		{
			return long.Parse(s, style, null);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000038B8 File Offset: 0x00001AB8
		internal static bool Parse(string s, NumberStyles style, IFormatProvider fp, bool tryParse, out long result, out Exception exc)
		{
			result = 0L;
			exc = null;
			if (s == null)
			{
				if (!tryParse)
				{
					exc = new ArgumentNullException("s");
				}
				return false;
			}
			if (s.Length == 0)
			{
				if (!tryParse)
				{
					exc = new FormatException("Input string was not in the correct format: s.Length==0.");
				}
				return false;
			}
			NumberFormatInfo numberFormatInfo = null;
			if (fp != null)
			{
				Type typeFromHandle = typeof(NumberFormatInfo);
				numberFormatInfo = (NumberFormatInfo)fp.GetFormat(typeFromHandle);
			}
			if (numberFormatInfo == null)
			{
				numberFormatInfo = Thread.CurrentThread.CurrentCulture.NumberFormat;
			}
			if (!int.CheckStyle(style, tryParse, ref exc))
			{
				return false;
			}
			bool flag = (style & NumberStyles.AllowCurrencySymbol) != NumberStyles.None;
			bool flag2 = (style & NumberStyles.AllowHexSpecifier) != NumberStyles.None;
			bool flag3 = (style & NumberStyles.AllowThousands) != NumberStyles.None;
			bool flag4 = (style & NumberStyles.AllowDecimalPoint) != NumberStyles.None;
			bool flag5 = (style & NumberStyles.AllowParentheses) != NumberStyles.None;
			bool flag6 = (style & NumberStyles.AllowTrailingSign) != NumberStyles.None;
			bool flag7 = (style & NumberStyles.AllowLeadingSign) != NumberStyles.None;
			bool flag8 = (style & NumberStyles.AllowTrailingWhite) != NumberStyles.None;
			bool flag9 = (style & NumberStyles.AllowLeadingWhite) != NumberStyles.None;
			int num = 0;
			if (flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
			{
				return false;
			}
			bool flag10 = false;
			bool flag11 = false;
			bool flag12 = false;
			bool flag13 = false;
			if (flag5 && s[num] == '(')
			{
				flag10 = true;
				flag12 = true;
				flag11 = true;
				num++;
				if (flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
				{
					return false;
				}
				if (s.Substring(num, numberFormatInfo.NegativeSign.Length) == numberFormatInfo.NegativeSign)
				{
					if (!tryParse)
					{
						exc = new FormatException("Input string was not in the correct format: Has Negative Sign.");
					}
					return false;
				}
				if (s.Substring(num, numberFormatInfo.PositiveSign.Length) == numberFormatInfo.PositiveSign)
				{
					if (!tryParse)
					{
						exc = new FormatException("Input string was not in the correct format: Has Positive Sign.");
					}
					return false;
				}
			}
			if (flag7 && !flag12)
			{
				int.FindSign(ref num, s, numberFormatInfo, ref flag12, ref flag11);
				if (flag12)
				{
					if (flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
					{
						return false;
					}
					if (flag)
					{
						int.FindCurrency(ref num, s, numberFormatInfo, ref flag13);
						if (flag13 && flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
						{
							return false;
						}
					}
				}
			}
			if (flag && !flag13)
			{
				int.FindCurrency(ref num, s, numberFormatInfo, ref flag13);
				if (flag13)
				{
					if (flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
					{
						return false;
					}
					if (flag13 && !flag12 && flag7)
					{
						int.FindSign(ref num, s, numberFormatInfo, ref flag12, ref flag11);
						if (flag12 && flag9 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
						{
							return false;
						}
					}
				}
			}
			long num2 = 0L;
			int num3 = 0;
			bool flag14 = false;
			do
			{
				if (!int.ValidDigit(s[num], flag2))
				{
					if (!flag3 || (!int.FindOther(ref num, s, numberFormatInfo.NumberGroupSeparator) && !int.FindOther(ref num, s, numberFormatInfo.CurrencyGroupSeparator)))
					{
						if (flag14 || !flag4 || (!int.FindOther(ref num, s, numberFormatInfo.NumberDecimalSeparator) && !int.FindOther(ref num, s, numberFormatInfo.CurrencyDecimalSeparator)))
						{
							break;
						}
						flag14 = true;
					}
				}
				else if (flag2)
				{
					num3++;
					char c = s[num++];
					int num4;
					if (char.IsDigit(c))
					{
						num4 = (int)(c - '0');
					}
					else if (char.IsLower(c))
					{
						num4 = (int)(c - 'a' + '\n');
					}
					else
					{
						num4 = (int)(c - 'A' + '\n');
					}
					ulong num5 = (ulong)num2;
					try
					{
						num2 = (long)(checked(num5 * 16UL + (ulong)num4));
					}
					catch (OverflowException ex)
					{
						if (!tryParse)
						{
							exc = ex;
						}
						return false;
					}
				}
				else if (flag14)
				{
					num3++;
					if (s[num++] != '0')
					{
						goto Block_49;
					}
				}
				else
				{
					num3++;
					try
					{
						num2 = checked(num2 * 10L - unchecked((long)(checked(s[num++] - '0'))));
					}
					catch (OverflowException)
					{
						if (!tryParse)
						{
							exc = new OverflowException("Value too large or too small.");
						}
						return false;
					}
				}
			}
			while (num < s.Length);
			goto IL_462;
			Block_49:
			if (!tryParse)
			{
				exc = new OverflowException("Value too large or too small.");
			}
			return false;
			IL_462:
			if (num3 == 0)
			{
				if (!tryParse)
				{
					exc = new FormatException("Input string was not in the correct format: nDigits == 0.");
				}
				return false;
			}
			if (flag6 && !flag12)
			{
				int.FindSign(ref num, s, numberFormatInfo, ref flag12, ref flag11);
				if (flag12)
				{
					if (flag8 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
					{
						return false;
					}
					if (flag)
					{
						int.FindCurrency(ref num, s, numberFormatInfo, ref flag13);
					}
				}
			}
			if (flag && !flag13)
			{
				if (numberFormatInfo.CurrencyPositivePattern == 3 && s[num++] != ' ')
				{
					if (tryParse)
					{
						return false;
					}
					throw new FormatException("Input string was not in the correct format: no space between number and currency symbol.");
				}
				else
				{
					int.FindCurrency(ref num, s, numberFormatInfo, ref flag13);
					if (flag13 && num < s.Length)
					{
						if (flag8 && !int.JumpOverWhite(ref num, s, true, tryParse, ref exc))
						{
							return false;
						}
						if (!flag12 && flag6)
						{
							int.FindSign(ref num, s, numberFormatInfo, ref flag12, ref flag11);
						}
					}
				}
			}
			if (flag8 && num < s.Length && !int.JumpOverWhite(ref num, s, false, tryParse, ref exc))
			{
				return false;
			}
			if (flag10)
			{
				if (num >= s.Length || s[num++] != ')')
				{
					if (!tryParse)
					{
						exc = new FormatException("Input string was not in the correct format: No room for close parens.");
					}
					return false;
				}
				if (flag8 && num < s.Length && !int.JumpOverWhite(ref num, s, false, tryParse, ref exc))
				{
					return false;
				}
			}
			if (num < s.Length && s[num] != '\0')
			{
				if (!tryParse)
				{
					exc = new FormatException(string.Concat(new object[]
					{
						"Input string was not in the correct format: Did not parse entire string. pos = ",
						num,
						" s.Length = ",
						s.Length
					}));
				}
				return false;
			}
			if (!flag11 && !flag2)
			{
				try
				{
					num2 = (long)(checked(unchecked((ulong)0) - (ulong)num2));
				}
				catch (OverflowException ex2)
				{
					if (!tryParse)
					{
						exc = ex2;
					}
					return false;
				}
			}
			result = num2;
			return true;
		}

		/// <summary>Converts the string representation of a number to its 64-bit signed integer equivalent.</summary>
		/// <returns>A 64-bit signed integer equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060000A5 RID: 165 RVA: 0x00003F94 File Offset: 0x00002194
		public static long Parse(string s)
		{
			long result;
			Exception ex;
			if (!long.Parse(s, false, out result, out ex))
			{
				throw ex;
			}
			return result;
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its 64-bit signed integer equivalent.</summary>
		/// <returns>A 64-bit signed integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format compliant with <paramref name="style" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. -or-<paramref name="style" /> supports fractional digits, but <paramref name="s" /> includes non-zero fractional digits.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060000A6 RID: 166 RVA: 0x00003FB4 File Offset: 0x000021B4
		public static long Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			long result;
			Exception ex;
			if (!long.Parse(s, style, provider, false, out result, out ex))
			{
				throw ex;
			}
			return result;
		}

		/// <summary>Converts the string representation of a number to its 64-bit signed integer equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="result">When this method returns, contains the 64-bit signed integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not of the correct format, or represents a number less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060000A7 RID: 167 RVA: 0x00003FD8 File Offset: 0x000021D8
		public static bool TryParse(string s, out long result)
		{
			Exception ex;
			if (!long.Parse(s, true, out result, out ex))
			{
				result = 0L;
				return false;
			}
			return true;
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its 64-bit signed integer equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a number to convert. The string is interpreted using the style specified by <paramref name="style" />. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains the 64-bit signed integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060000A8 RID: 168 RVA: 0x00003FFC File Offset: 0x000021FC
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out long result)
		{
			Exception ex;
			if (!long.Parse(s, style, provider, true, out result, out ex))
			{
				result = 0L;
				return false;
			}
			return true;
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance, consisting of a minus sign if the value is negative, and a sequence of digits ranging from 0 to 9 with no leading zeroes.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060000A9 RID: 169 RVA: 0x00004024 File Offset: 0x00002224
		public override string ToString()
		{
			return NumberFormatter.NumberToString(this, null);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060000AA RID: 170 RVA: 0x00004030 File Offset: 0x00002230
		public string ToString(IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(this, provider);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A numeric format string (see Remarks).</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid or not supported.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060000AB RID: 171 RVA: 0x0000403C File Offset: 0x0000223C
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A numeric format string (see Remarks).</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about this instance. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid or not supported.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060000AC RID: 172 RVA: 0x00004048 File Offset: 0x00002248
		public string ToString(string format, IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(format, this, provider);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Int64" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Int64" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000AD RID: 173 RVA: 0x00004054 File Offset: 0x00002254
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int64;
		}

		/// <summary>Represents the largest possible value of an Int64. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000008 RID: 8
		public const long MaxValue = 9223372036854775807L;

		/// <summary>Represents the smallest possible value of an Int64. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000009 RID: 9
		public const long MinValue = -9223372036854775808L;

		// Token: 0x0400000A RID: 10
		internal long m_value;
	}
}
