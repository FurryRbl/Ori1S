using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a single-precision floating-point number.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200001F RID: 31
	[ComVisible(true)]
	[Serializable]
	public struct Single : IFormattable, IConvertible, IComparable, IComparable<float>, IEquatable<float>
	{
		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600027D RID: 637 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600027E RID: 638 RVA: 0x0000AD00 File Offset: 0x00008F00
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />. </summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600027F RID: 639 RVA: 0x0000AD0C File Offset: 0x00008F0C
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000280 RID: 640 RVA: 0x0000AD18 File Offset: 0x00008F18
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000281 RID: 641 RVA: 0x0000AD24 File Offset: 0x00008F24
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000282 RID: 642 RVA: 0x0000AD30 File Offset: 0x00008F30
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000283 RID: 643 RVA: 0x0000AD3C File Offset: 0x00008F3C
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000284 RID: 644 RVA: 0x0000AD48 File Offset: 0x00008F48
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000285 RID: 645 RVA: 0x0000AD54 File Offset: 0x00008F54
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.SByte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000286 RID: 646 RVA: 0x0000AD60 File Offset: 0x00008F60
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000287 RID: 647 RVA: 0x0000AD6C File Offset: 0x00008F6C
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to <paramref name="type" />.</returns>
		/// <param name="type">The type to which to convert this <see cref="T:System.Single" /> value.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies information about the format of the returned value.</param>
		// Token: 0x06000288 RID: 648 RVA: 0x0000AD78 File Offset: 0x00008F78
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000289 RID: 649 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600028A RID: 650 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600028B RID: 651 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />.-or- This instance is not a number (<see cref="F:System.Single.NaN" />) and <paramref name="value" /> is a number. Zero This instance is equal to <paramref name="value" />.-or- This instance and value are both not a number (<see cref="F:System.Single.NaN" />), <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />. Greater than zero This instance is greater than <paramref name="value" />.-or- This instance is a number and <paramref name="value" /> is not a number (<see cref="F:System.Single.NaN" />).-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Single" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600028C RID: 652 RVA: 0x0000ADCC File Offset: 0x00008FCC
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is float))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Single."));
			}
			float num = (float)value;
			if (float.IsPositiveInfinity(this) && float.IsPositiveInfinity(num))
			{
				return 0;
			}
			if (float.IsNegativeInfinity(this) && float.IsNegativeInfinity(num))
			{
				return 0;
			}
			if (float.IsNaN(num))
			{
				if (float.IsNaN(this))
				{
					return 0;
				}
				return 1;
			}
			else if (float.IsNaN(this))
			{
				if (float.IsNaN(num))
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (this == num)
				{
					return 0;
				}
				if (this > num)
				{
					return 1;
				}
				return -1;
			}
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Single" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600028D RID: 653 RVA: 0x0000AE80 File Offset: 0x00009080
		public override bool Equals(object obj)
		{
			if (!(obj is float))
			{
				return false;
			}
			float num = (float)obj;
			if (float.IsNaN(num))
			{
				return float.IsNaN(this);
			}
			return num == this;
		}

		/// <summary>Compares this instance to a specified single-precision floating-point number and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified single-precision floating-point number.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />.-or- This instance is not a number (<see cref="F:System.Single.NaN" />) and <paramref name="value" /> is a number. Zero This instance is equal to <paramref name="value" />.-or- Both this instance and <paramref name="value" /> are not a number (<see cref="F:System.Single.NaN" />), <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />. Greater than zero This instance is greater than <paramref name="value" />.-or- This instance is a number and <paramref name="value" /> is not a number (<see cref="F:System.Single.NaN" />). </returns>
		/// <param name="value">A single-precision floating-point number to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600028E RID: 654 RVA: 0x0000AEBC File Offset: 0x000090BC
		public int CompareTo(float value)
		{
			if (float.IsPositiveInfinity(this) && float.IsPositiveInfinity(value))
			{
				return 0;
			}
			if (float.IsNegativeInfinity(this) && float.IsNegativeInfinity(value))
			{
				return 0;
			}
			if (float.IsNaN(value))
			{
				if (float.IsNaN(this))
				{
					return 0;
				}
				return 1;
			}
			else if (float.IsNaN(this))
			{
				if (float.IsNaN(value))
				{
					return 0;
				}
				return -1;
			}
			else
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
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Single" /> object represent the same value.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">A <see cref="T:System.Single" /> object to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600028F RID: 655 RVA: 0x0000AF48 File Offset: 0x00009148
		public bool Equals(float obj)
		{
			if (float.IsNaN(obj))
			{
				return float.IsNaN(this);
			}
			return obj == this;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000290 RID: 656 RVA: 0x0000AF64 File Offset: 0x00009164
		public override int GetHashCode()
		{
			return (int)this;
		}

		/// <summary>Returns a value indicating whether the specified number evaluates to negative or positive infinity.</summary>
		/// <returns>true if <paramref name="f" /> evaluates to <see cref="F:System.Single.PositiveInfinity" /> or <see cref="F:System.Single.NegativeInfinity" />; otherwise, false.</returns>
		/// <param name="f">A single-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000291 RID: 657 RVA: 0x0000AF78 File Offset: 0x00009178
		public static bool IsInfinity(float f)
		{
			return f == float.PositiveInfinity || f == float.NegativeInfinity;
		}

		/// <summary>Returns a value indicating whether the specified number evaluates to not a number (<see cref="F:System.Single.NaN" />).</summary>
		/// <returns>true if <paramref name="f" /> evaluates to not a number (<see cref="F:System.Single.NaN" />); otherwise, false.</returns>
		/// <param name="f">A single-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000292 RID: 658 RVA: 0x0000AF90 File Offset: 0x00009190
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool IsNaN(float f)
		{
			return f != f;
		}

		/// <summary>Returns a value indicating whether the specified number evaluates to negative infinity.</summary>
		/// <returns>true if <paramref name="f" /> evaluates to <see cref="F:System.Single.NegativeInfinity" />; otherwise, false.</returns>
		/// <param name="f">A single-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000293 RID: 659 RVA: 0x0000AF9C File Offset: 0x0000919C
		public static bool IsNegativeInfinity(float f)
		{
			return f < 0f && (f == float.NegativeInfinity || f == float.PositiveInfinity);
		}

		/// <summary>Returns a value indicating whether the specified number evaluates to positive infinity.</summary>
		/// <returns>true if <paramref name="f" /> evaluates to <see cref="F:System.Single.PositiveInfinity" />; otherwise, false.</returns>
		/// <param name="f">A single-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000294 RID: 660 RVA: 0x0000AFD0 File Offset: 0x000091D0
		public static bool IsPositiveInfinity(float f)
		{
			return f > 0f && (f == float.NegativeInfinity || f == float.PositiveInfinity);
		}

		/// <summary>Converts the string representation of a number to its single-precision floating-point number equivalent.</summary>
		/// <returns>A single-precision floating-point number equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not a number in a valid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000295 RID: 661 RVA: 0x0000B004 File Offset: 0x00009204
		public static float Parse(string s)
		{
			double num = double.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, null);
			if (num - 3.4028234663852886E+38 > 3.6147112457961776E+29 && !double.IsPositiveInfinity(num))
			{
				throw new OverflowException();
			}
			return (float)num;
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its single-precision floating-point number equivalent.</summary>
		/// <returns>A single-precision floating-point number equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not a number in a valid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000296 RID: 662 RVA: 0x0000B04C File Offset: 0x0000924C
		public static float Parse(string s, IFormatProvider provider)
		{
			double num = double.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, provider);
			if (num - 3.4028234663852886E+38 > 3.6147112457961776E+29 && !double.IsPositiveInfinity(num))
			{
				throw new OverflowException();
			}
			return (float)num;
		}

		/// <summary>Converts the string representation of a number in a specified style to its single-precision floating-point number equivalent.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not a number in a valid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000297 RID: 663 RVA: 0x0000B094 File Offset: 0x00009294
		public static float Parse(string s, NumberStyles style)
		{
			double num = double.Parse(s, style, null);
			if (num - 3.4028234663852886E+38 > 3.6147112457961776E+29 && !double.IsPositiveInfinity(num))
			{
				throw new OverflowException();
			}
			return (float)num;
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its single-precision floating-point number equivalent.</summary>
		/// <returns>A single-precision floating-point number equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. </param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" /> values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not a numeric value. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000298 RID: 664 RVA: 0x0000B0D8 File Offset: 0x000092D8
		public static float Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			double num = double.Parse(s, style, provider);
			if (num - 3.4028234663852886E+38 > 3.6147112457961776E+29 && !double.IsPositiveInfinity(num))
			{
				throw new OverflowException();
			}
			return (float)num;
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its single-precision floating-point number equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string representing a number to convert. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <param name="provider">An object object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains the single-precision floating-point number equivalent to the numeric value or symbol contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in a format compliant with <paramref name="style" />, represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />, or <paramref name="style" /> is not a valid combination of <see cref="T:System.Globalization.NumberStyles" /> enumerated constants. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000299 RID: 665 RVA: 0x0000B11C File Offset: 0x0000931C
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out float result)
		{
			double num;
			Exception ex;
			if (!double.Parse(s, style, provider, true, out num, out ex))
			{
				result = 0f;
				return false;
			}
			if (num - 3.4028234663852886E+38 > 3.6147112457961776E+29 && !double.IsPositiveInfinity(num))
			{
				result = 0f;
				return false;
			}
			result = (float)num;
			return true;
		}

		/// <summary>Converts the string representation of a number to its single-precision floating-point number equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string representing a number to convert. </param>
		/// <param name="result">When this method returns, contains single-precision floating-point number equivalent to the numeric value or symbol contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not a number in a valid format, or represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600029A RID: 666 RVA: 0x0000B178 File Offset: 0x00009378
		public static bool TryParse(string s, out float result)
		{
			return float.TryParse(s, NumberStyles.Any, null, out result);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600029B RID: 667 RVA: 0x0000B188 File Offset: 0x00009388
		public override string ToString()
		{
			return NumberFormatter.NumberToString(this, null);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600029C RID: 668 RVA: 0x0000B194 File Offset: 0x00009394
		public string ToString(IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(this, provider);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A numeric format string (see Remarks).</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600029D RID: 669 RVA: 0x0000B1A0 File Offset: 0x000093A0
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A numeric format string (see Remarks).</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600029E RID: 670 RVA: 0x0000B1AC File Offset: 0x000093AC
		public string ToString(string format, IFormatProvider provider)
		{
			return NumberFormatter.NumberToString(format, this, provider);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Single" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Single" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600029F RID: 671 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public TypeCode GetTypeCode()
		{
			return TypeCode.Single;
		}

		/// <summary>Represents the smallest positive <see cref="T:System.Single" /> value greater than zero. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400002C RID: 44
		public const float Epsilon = 1E-45f;

		/// <summary>Represents the largest possible value of <see cref="T:System.Single" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400002D RID: 45
		public const float MaxValue = 3.4028235E+38f;

		/// <summary>Represents the smallest possible value of <see cref="T:System.Single" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400002E RID: 46
		public const float MinValue = -3.4028235E+38f;

		/// <summary>Represents not a number (NaN). This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400002F RID: 47
		public const float NaN = float.NaN;

		/// <summary>Represents positive infinity. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000030 RID: 48
		public const float PositiveInfinity = float.PositiveInfinity;

		/// <summary>Represents negative infinity. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000031 RID: 49
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x04000032 RID: 50
		private const double MaxValueEpsilon = 3.6147112457961776E+29;

		// Token: 0x04000033 RID: 51
		internal float m_value;
	}
}
