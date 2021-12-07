using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a Boolean value.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000022 RID: 34
	[ComVisible(true)]
	[Serializable]
	public struct Boolean : IConvertible, IComparable, IComparable<bool>, IEquatable<bool>
	{
		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>An object of the specified type, with a value that is equivalent to the value of this Boolean object.</returns>
		/// <param name="type">The desired type. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies culture-specific information about the format of the returned value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The requested type conversion is not supported. </exception>
		// Token: 0x06000344 RID: 836 RVA: 0x0000D63C File Offset: 0x0000B83C
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true or false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000345 RID: 837 RVA: 0x0000D66C File Offset: 0x0000B86C
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>true, coerced to byte, if the value of this instance is nonzero; otherwise, false, coerced to byte.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000346 RID: 838 RVA: 0x0000D670 File Offset: 0x0000B870
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">You attempt to convert a <see cref="T:System.Boolean" /> value to a <see cref="T:System.Char" /> value. This conversion is not supported.</exception>
		// Token: 0x06000347 RID: 839 RVA: 0x0000D67C File Offset: 0x0000B87C
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">You attempt to convert a <see cref="T:System.Boolean" /> value to a <see cref="T:System.DateTime" /> value. This conversion is not supported.</exception>
		// Token: 0x06000348 RID: 840 RVA: 0x0000D684 File Offset: 0x0000B884
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />..</summary>
		/// <returns>1 if this instance is nonzero (that is, true); otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000349 RID: 841 RVA: 0x0000D68C File Offset: 0x0000B88C
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />..</summary>
		/// <returns>1 if this instance is nonzero (that is, true); otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600034A RID: 842 RVA: 0x0000D698 File Offset: 0x0000B898
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is nonzero (that is, true); otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600034B RID: 843 RVA: 0x0000D6A4 File Offset: 0x0000B8A4
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is nonzero (that is, true); otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600034C RID: 844 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is nonzero (that is, true); otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600034D RID: 845 RVA: 0x0000D6BC File Offset: 0x0000B8BC
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is nonzero (that is, true); otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600034E RID: 846 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />..</summary>
		/// <returns>1 if this instance is nonzero (that is, true); otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600034F RID: 847 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is nonzero (that is, true); otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000350 RID: 848 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is nonzero (that is, true); otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000351 RID: 849 RVA: 0x0000D6EC File Offset: 0x0000B8EC
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is nonzero (that is, true); otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000352 RID: 850 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates their relationship to one another.</summary>
		/// <returns>A signed integer that indicates the relative order of this instance and <paramref name="obj" />.Return Value Condition Less than zero This instance is false and <paramref name="obj" /> is true. Zero This instance and <paramref name="obj" /> are equal (either both are true or both are false). Greater than zero This instance is true and <paramref name="obj" /> is false.-or- <paramref name="obj" /> is null. </returns>
		/// <param name="obj">An object to compare to this instance, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Boolean" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000353 RID: 851 RVA: 0x0000D704 File Offset: 0x0000B904
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is bool))
			{
				throw new ArgumentException(Locale.GetText("Object is not a Boolean."));
			}
			bool flag = (bool)obj;
			if (this && !flag)
			{
				return 1;
			}
			return (this != flag) ? -1 : 0;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Boolean" /> and has the same value as this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000354 RID: 852 RVA: 0x0000D75C File Offset: 0x0000B95C
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is bool))
			{
				return false;
			}
			bool flag = (bool)obj;
			return (!this) ? (!flag) : flag;
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Boolean" /> object and returns an integer that indicates their relationship to one another.</summary>
		/// <returns>A signed integer that indicates the relative values of this instance and <paramref name="value" />.Return Value Condition Less than zero This instance is false and <paramref name="value" /> is true. Zero This instance and <paramref name="value" /> are equal (either both are true or both are false). Greater than zero This instance is true and <paramref name="value" /> is false. </returns>
		/// <param name="value">A <see cref="T:System.Boolean" /> object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000355 RID: 853 RVA: 0x0000D794 File Offset: 0x0000B994
		public int CompareTo(bool value)
		{
			if (this == value)
			{
				return 0;
			}
			return this ? 1 : -1;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.Boolean" /> object.</summary>
		/// <returns>true if <paramref name="obj" /> has the same value as this instance; otherwise, false.</returns>
		/// <param name="obj">A <see cref="T:System.Boolean" /> value to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000356 RID: 854 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		public bool Equals(bool obj)
		{
			return this == obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Boolean" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000357 RID: 855 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		public override int GetHashCode()
		{
			return (!this) ? 0 : 1;
		}

		/// <summary>Converts the specified string representation of a logical value to its <see cref="T:System.Boolean" /> equivalent, or throws an exception if the string is not equivalent to the value of <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</summary>
		/// <returns>true if <paramref name="value" /> is equivalent to the value of the <see cref="F:System.Boolean.TrueString" /> field; false if <paramref name="value" /> is equivalent to the value of the <see cref="F:System.Boolean.FalseString" /> field.</returns>
		/// <param name="value">A string containing the value to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not equivalent to the value of the <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" /> field. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000358 RID: 856 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		public static bool Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			value = value.Trim();
			if (string.Compare(value, bool.TrueString, true, CultureInfo.InvariantCulture) == 0)
			{
				return true;
			}
			if (string.Compare(value, bool.FalseString, true, CultureInfo.InvariantCulture) == 0)
			{
				return false;
			}
			throw new FormatException(Locale.GetText("Value is not equivalent to either TrueString or FalseString."));
		}

		/// <summary>Tries to convert the specified string representation of a logical value to its <see cref="T:System.Boolean" /> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="value" /> was converted successfully; otherwise, false.</returns>
		/// <param name="value">A string containing the value to convert. </param>
		/// <param name="result">When this method returns, if the conversion succeeded, contains true if <paramref name="value" /> is equivalent to <see cref="F:System.Boolean.TrueString" /> or false if <paramref name="value" /> is equivalent to <see cref="F:System.Boolean.FalseString" />. If the conversion failed, contains false. The conversion fails if <paramref name="value" /> is null or is not equivalent to the value of either the <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" /> field.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000359 RID: 857 RVA: 0x0000D830 File Offset: 0x0000BA30
		public static bool TryParse(string value, out bool result)
		{
			result = false;
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			if (string.Compare(value, bool.TrueString, true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				return true;
			}
			return string.Compare(value, bool.FalseString, true, CultureInfo.InvariantCulture) == 0;
		}

		/// <summary>Converts the value of this instance to its equivalent string representation (either "True" or "False").</summary>
		/// <returns>
		///   <see cref="F:System.Boolean.TrueString" /> if the value of this instance is true, or <see cref="F:System.Boolean.FalseString" /> if the value of this instance is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600035A RID: 858 RVA: 0x0000D884 File Offset: 0x0000BA84
		public override string ToString()
		{
			return (!this) ? bool.FalseString : bool.TrueString;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Boolean" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Boolean" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600035B RID: 859 RVA: 0x0000D89C File Offset: 0x0000BA9C
		public TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
		}

		/// <summary>Converts the value of this instance to its equivalent string representation (either "True" or "False").</summary>
		/// <returns>
		///   <see cref="F:System.Boolean.TrueString" /> if the value of this instance is true, or <see cref="F:System.Boolean.FalseString" /> if the value of this instance is false.</returns>
		/// <param name="provider">(Reserved) An <see cref="T:System.IFormatProvider" /> object. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600035C RID: 860 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
		public string ToString(IFormatProvider provider)
		{
			return this.ToString();
		}

		/// <summary>Represents the Boolean value false as a string. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000053 RID: 83
		public static readonly string FalseString = "False";

		/// <summary>Represents the Boolean value true as a string. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000054 RID: 84
		public static readonly string TrueString = "True";

		// Token: 0x04000055 RID: 85
		internal bool m_value;
	}
}
