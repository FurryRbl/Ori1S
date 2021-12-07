using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Globalization.Unicode;

namespace System
{
	/// <summary>Represents text as a series of Unicode characters.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200001C RID: 28
	[ComVisible(true)]
	[Serializable]
	public sealed class String : IConvertible, IComparable, IEnumerable, ICloneable, IComparable<string>, IEquatable<string>, IEnumerable<char>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified pointer to an array of Unicode characters.</summary>
		/// <param name="value">A pointer to a null terminated array of Unicode characters. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current process does not have read access to all the addressed characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> specifies an array that contains an invalid Unicode character, or <paramref name="value" /> specifies an address less than 64000.</exception>
		// Token: 0x060001BD RID: 445
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(char* value);

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified pointer to an array of Unicode characters, a starting character position within that array, and a length.</summary>
		/// <param name="value">A pointer to an array of Unicode characters. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <param name="length">The number of characters within <paramref name="value" /> to use. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero, <paramref name="value" /> + <paramref name="startIndex" /> cause a pointer overflow, or the current process does not have read access to all the addressed characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> specifies an array that contains an invalid Unicode character, or <paramref name="value" /> + <paramref name="startIndex" /> specifies an address less than 64000.</exception>
		// Token: 0x060001BE RID: 446
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(char* value, int startIndex, int length);

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a pointer to an array of 8-bit signed integers.</summary>
		/// <param name="value">A pointer to a null-terminated array of 8-bit signed integers. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A new instance of <see cref="T:System.String" /> could not be initialized using <paramref name="value" />, assuming <paramref name="value" /> is encoded in ANSI. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the new string to initialize, which is determined by the null termination character of <paramref name="value" />, is too large to allocate. </exception>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="value" /> specifies an invalid address.</exception>
		// Token: 0x060001BF RID: 447
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value);

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified pointer to an array of 8-bit signed integers, a starting character position within that array, and a length.</summary>
		/// <param name="value">A pointer to an array of 8-bit signed integers. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <param name="length">The number of characters within <paramref name="value" /> to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero. -or-The address specified by <paramref name="value" /> + <paramref name="startIndex" /> is too large for the current platform; that is, the address calculation overflowed. -or-The length of the new string to initialize is too large to allocate.</exception>
		/// <exception cref="T:System.ArgumentException">The address specified by <paramref name="value" /> + <paramref name="startIndex" /> is less than 64K.-or- A new instance of <see cref="T:System.String" /> could not be initialized using <paramref name="value" />, assuming <paramref name="value" /> is encoded in ANSI. </exception>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="value" />, <paramref name="startIndex" />, and <paramref name="length" /> collectively specify an invalid address.</exception>
		// Token: 0x060001C0 RID: 448
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value, int startIndex, int length);

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified pointer to an array of 8-bit signed integers, a starting character position within that array, a length, and an <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <param name="value">A pointer to an array of 8-bit signed integers. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <param name="length">The number of characters within <paramref name="value" /> to use. </param>
		/// <param name="enc">An <see cref="T:System.Text.Encoding" /> object that specifies how the array referenced by <paramref name="value" /> is encoded. If <paramref name="enc" /> is null, ANSI encoding is assumed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero. -or-The address specified by <paramref name="value" /> + <paramref name="startIndex" /> is too large for the current platform; that is, the address calculation overflowed. -or-The length of the new string to initialize is too large to allocate.</exception>
		/// <exception cref="T:System.ArgumentException">The address specified by <paramref name="value" /> + <paramref name="startIndex" /> is less than 64K.-or- A new instance of <see cref="T:System.String" /> could not be initialized using <paramref name="value" />, assuming <paramref name="value" /> is encoded as specified by <paramref name="enc" />. </exception>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="value" />, <paramref name="startIndex" />, and <paramref name="length" /> collectively specify an invalid address.</exception>
		// Token: 0x060001C1 RID: 449
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value, int startIndex, int length, Encoding enc);

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by an array of Unicode characters, a starting character position within that array, and a length.</summary>
		/// <param name="value">An array of Unicode characters. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <param name="length">The number of characters within <paramref name="value" /> to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero.-or- The sum of <paramref name="startIndex" /> and <paramref name="length" /> is greater than the number of elements in <paramref name="value" />. </exception>
		// Token: 0x060001C2 RID: 450
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char[] value, int startIndex, int length);

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by an array of Unicode characters.</summary>
		/// <param name="value">An array of Unicode characters. </param>
		// Token: 0x060001C3 RID: 451
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char[] value);

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified Unicode character repeated a specified number of times.</summary>
		/// <param name="c">A Unicode character. </param>
		/// <param name="count">The number of times <paramref name="c" /> occurs. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		// Token: 0x060001C4 RID: 452
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char c, int count);

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />.</summary>
		/// <returns>true if the value of the current <see cref="T:System.String" /> object is <see cref="F:System.Boolean.TrueString" />, or false if the value of the current <see cref="T:System.String" /> object is <see cref="F:System.Boolean.FalseString" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object is not <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</exception>
		// Token: 0x060001C6 RID: 454 RVA: 0x000067E4 File Offset: 0x000049E4
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater than <see cref="F:System.Byte.MaxValue" /> or less than <see cref="F:System.Byte.MinValue" />. </exception>
		// Token: 0x060001C7 RID: 455 RVA: 0x000067F0 File Offset: 0x000049F0
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />.</summary>
		/// <returns>The character at index 0 in the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that provides culture-specific formatting information. </param>
		// Token: 0x060001C8 RID: 456 RVA: 0x000067FC File Offset: 0x000049FC
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDateTime(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that provides culture-specific formatting information. </param>
		// Token: 0x060001C9 RID: 457 RVA: 0x00006808 File Offset: 0x00004A08
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number less than <see cref="F:System.Decimal.MinValue" /> or than <see cref="F:System.Decimal.MaxValue" /> greater. </exception>
		// Token: 0x060001CA RID: 458 RVA: 0x00006814 File Offset: 0x00004A14
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		// Token: 0x060001CB RID: 459 RVA: 0x00006820 File Offset: 0x00004A20
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />.</exception>
		// Token: 0x060001CC RID: 460 RVA: 0x0000682C File Offset: 0x00004A2C
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that provides culture-specific formatting information. </param>
		// Token: 0x060001CD RID: 461 RVA: 0x00006838 File Offset: 0x00004A38
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that provides culture-specific formatting information. </param>
		// Token: 0x060001CE RID: 462 RVA: 0x00006844 File Offset: 0x00004A44
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		// Token: 0x060001CF RID: 463 RVA: 0x00006850 File Offset: 0x00004A50
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that provides culture-specific formatting information. </param>
		// Token: 0x060001D0 RID: 464 RVA: 0x0000685C File Offset: 0x00004A5C
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="type">The type of the returned object. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that provides culture-specific formatting information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">The value of the current <see cref="T:System.String" /> object cannot be converted to the type specified by the <paramref name="type" /> parameter. </exception>
		// Token: 0x060001D1 RID: 465 RVA: 0x00006868 File Offset: 0x00004A68
		object IConvertible.ToType(Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("type");
			}
			return Convert.ToType(this, targetType, provider, false);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater than <see cref="F:System.UInt16.MaxValue" /> or less than <see cref="F:System.UInt16.MinValue" />.</exception>
		// Token: 0x060001D2 RID: 466 RVA: 0x00006884 File Offset: 0x00004A84
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater <see cref="F:System.UInt32.MaxValue" /> or less than <see cref="F:System.UInt32.MinValue" /></exception>
		// Token: 0x060001D3 RID: 467 RVA: 0x00006890 File Offset: 0x00004A90
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. </param>
		// Token: 0x060001D4 RID: 468 RVA: 0x0000689C File Offset: 0x00004A9C
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this, provider);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000068A8 File Offset: 0x00004AA8
		IEnumerator<char> IEnumerable<char>.GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through the current <see cref="T:System.String" /> object. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the current <see cref="T:System.String" /> object.</returns>
		// Token: 0x060001D6 RID: 470 RVA: 0x000068B0 File Offset: 0x00004AB0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		/// <summary>Determines whether two specified <see cref="T:System.String" /> objects have the same value.</summary>
		/// <returns>true if the value of <paramref name="a" /> is the same as the value of <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.String" /> or null. </param>
		/// <param name="b">A <see cref="T:System.String" /> or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001D7 RID: 471 RVA: 0x000068B8 File Offset: 0x00004AB8
		public unsafe static bool Equals(string a, string b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			int i = a.length;
			if (i != b.length)
			{
				return false;
			}
			char* ptr = &a.start_char;
			char* ptr2 = &b.start_char;
			while (i >= 8)
			{
				if (*(int*)ptr != *(int*)ptr2 || *(int*)(ptr + 2) != *(int*)(ptr2 + 2) || *(int*)(ptr + 4) != *(int*)(ptr2 + 4) || *(int*)(ptr + 6) != *(int*)(ptr2 + 6))
				{
					return false;
				}
				ptr += 8;
				ptr2 += 8;
				i -= 8;
			}
			if (i >= 4)
			{
				if (*(int*)ptr != *(int*)ptr2 || *(int*)(ptr + 2) != *(int*)(ptr2 + 2))
				{
					return false;
				}
				ptr += 4;
				ptr2 += 4;
				i -= 4;
			}
			if (i > 1)
			{
				if (*(int*)ptr != *(int*)ptr2)
				{
					return false;
				}
				ptr += 2;
				ptr2 += 2;
				i -= 2;
			}
			return i == 0 || *ptr == *ptr2;
		}

		/// <summary>Determines whether this instance of <see cref="T:System.String" /> and a specified object, which must also be a <see cref="T:System.String" /> object, have the same value.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.String" /> and its value is the same as this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001D8 RID: 472 RVA: 0x000069B8 File Offset: 0x00004BB8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public override bool Equals(object obj)
		{
			return string.Equals(this, obj as string);
		}

		/// <summary>Determines whether this instance and another specified <see cref="T:System.String" /> object have the same value.</summary>
		/// <returns>true if the value of the <paramref name="value" /> parameter is the same as this instance; otherwise, false.</returns>
		/// <param name="value">A <see cref="T:System.String" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001D9 RID: 473 RVA: 0x000069C8 File Offset: 0x00004BC8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public bool Equals(string value)
		{
			return string.Equals(this, value);
		}

		/// <summary>Gets the character at a specified character position in the current <see cref="T:System.String" /> object.</summary>
		/// <returns>A Unicode character.</returns>
		/// <param name="index">A character position in the current <see cref="T:System.String" /> object. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is greater than or equal to the length of this object or less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000009 RID: 9
		[IndexerName("Chars")]
		public unsafe char this[int index]
		{
			get
			{
				if (index < 0 || index >= this.length)
				{
					throw new IndexOutOfRangeException();
				}
				return *(ref this.start_char + index * 2);
			}
		}

		/// <summary>Returns a reference to this instance of <see cref="T:System.String" />.</summary>
		/// <returns>This instance of String.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001DB RID: 475 RVA: 0x00006A08 File Offset: 0x00004C08
		public object Clone()
		{
			return this;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for class <see cref="T:System.String" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.String" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001DC RID: 476 RVA: 0x00006A0C File Offset: 0x00004C0C
		public TypeCode GetTypeCode()
		{
			return TypeCode.String;
		}

		/// <summary>Copies a specified number of characters from a specified position in this instance to a specified position in an array of Unicode characters.</summary>
		/// <param name="sourceIndex">The index of the first character in this instance to copy. </param>
		/// <param name="destination">An array of Unicode characters to which characters in this instance are copied. </param>
		/// <param name="destinationIndex">An index in <paramref name="destination" /> at which the copy operation begins. </param>
		/// <param name="count">The number of characters in this instance to copy to <paramref name="destination" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sourceIndex" />, <paramref name="destinationIndex" />, or <paramref name="count" /> is negative -or- <paramref name="count" /> is greater than the length of the substring from <paramref name="startIndex" /> to the end of this instance -or- <paramref name="count" /> is greater than the length of the subarray from <paramref name="destinationIndex" /> to the end of <paramref name="destination" /></exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001DD RID: 477 RVA: 0x00006A10 File Offset: 0x00004C10
		public unsafe void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (sourceIndex < 0)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", "Cannot be negative");
			}
			if (destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", "Cannot be negative.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative.");
			}
			if (sourceIndex > this.Length - count)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", "sourceIndex + count > Length");
			}
			if (destinationIndex > destination.Length - count)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", "destinationIndex + count > destination.Length");
			}
			fixed (char* ptr = ref (destination != null && destination.Length != 0) ? ref destination[0] : ref *null)
			{
				fixed (string text = this)
				{
					fixed (char* ptr2 = text + RuntimeHelpers.OffsetToStringData / 2)
					{
						string.CharCopy(ptr + destinationIndex, ptr2 + sourceIndex, count);
						ptr = null;
						text = null;
						return;
					}
				}
			}
		}

		/// <summary>Copies the characters in this instance to a Unicode character array.</summary>
		/// <returns>A Unicode character array whose elements are the individual characters of this instance. If this instance is an empty string, the returned array is empty and has a zero length.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001DE RID: 478 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public char[] ToCharArray()
		{
			return this.ToCharArray(0, this.length);
		}

		/// <summary>Copies the characters in a specified substring in this instance to a Unicode character array.</summary>
		/// <returns>A Unicode character array whose elements are the <paramref name="length" /> number of characters in this instance starting from character position <paramref name="startIndex" />.</returns>
		/// <param name="startIndex">The starting position of a substring in this instance. </param>
		/// <param name="length">The length of the substring in this instance. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero.-or- <paramref name="startIndex" /> plus <paramref name="length" /> is greater than the length of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001DF RID: 479 RVA: 0x00006B00 File Offset: 0x00004D00
		public unsafe char[] ToCharArray(int startIndex, int length)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "< 0");
			}
			if (startIndex > this.length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Must be greater than the length of the string.");
			}
			char[] array = new char[length];
			fixed (char* dest = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
			{
				fixed (string text = this)
				{
					fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
					{
						string.CharCopy(dest, ptr + startIndex, length);
						dest = null;
						text = null;
						return array;
					}
				}
			}
		}

		/// <summary>Returns a string array that contains the substrings in this instance that are delimited by elements of a specified Unicode character array.</summary>
		/// <returns>An array whose elements contain the substrings in this instance that are delimited by one or more characters in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of Unicode characters that delimit the substrings in this instance, an empty array that contains no delimiters, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001E0 RID: 480 RVA: 0x00006B9C File Offset: 0x00004D9C
		public string[] Split(params char[] separator)
		{
			return this.Split(separator, int.MaxValue);
		}

		/// <summary>Returns a string array that contains the substrings in this instance that are delimited by elements of a specified Unicode character array. A parameter specifies the maximum number of substrings to return.</summary>
		/// <returns>An array whose elements contain the substrings in this instance that are delimited by one or more characters in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of Unicode characters that delimit the substrings in this instance, an empty array that contains no delimiters, or null. </param>
		/// <param name="count">The maximum number of substrings to return. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001E1 RID: 481 RVA: 0x00006BAC File Offset: 0x00004DAC
		public string[] Split(char[] separator, int count)
		{
			if (separator == null || separator.Length == 0)
			{
				separator = string.WhiteChars;
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				return new string[0];
			}
			if (count == 1)
			{
				return new string[]
				{
					this
				};
			}
			return this.InternalSplit(separator, count, 0);
		}

		/// <summary>Returns a string array that contains the substrings in this string that are delimited by elements of a specified Unicode character array. Parameters specify the maximum number of substrings to return and whether to return empty array elements.</summary>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by one or more characters in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of Unicode characters that delimit the substrings in this string, an empty array that contains no delimiters, or null.</param>
		/// <param name="count">The maximum number of substrings to return. </param>
		/// <param name="options">
		///   <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned, or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not one of the <see cref="T:System.StringSplitOptions" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001E2 RID: 482 RVA: 0x00006C08 File Offset: 0x00004E08
		[ComVisible(false)]
		[MonoDocumentationNote("code should be moved to managed")]
		public string[] Split(char[] separator, int count, StringSplitOptions options)
		{
			if (separator == null || separator.Length == 0)
			{
				return this.Split(string.WhiteChars, count, options);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero.");
			}
			if (options != StringSplitOptions.None && options != StringSplitOptions.RemoveEmptyEntries)
			{
				throw new ArgumentException("Illegal enum value: " + options + ".");
			}
			if (count == 0)
			{
				return new string[0];
			}
			return this.InternalSplit(separator, count, (int)options);
		}

		/// <summary>Returns a string array that contains the substrings in this string that are delimited by elements of a specified string array. Parameters specify the maximum number of substrings to return and whether to return empty array elements.</summary>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by one or more strings in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of strings that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param>
		/// <param name="count">The maximum number of substrings to return. </param>
		/// <param name="options">Specify <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned, or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not one of the <see cref="T:System.StringSplitOptions" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001E3 RID: 483 RVA: 0x00006C88 File Offset: 0x00004E88
		[ComVisible(false)]
		public string[] Split(string[] separator, int count, StringSplitOptions options)
		{
			if (separator == null || separator.Length == 0)
			{
				return this.Split(string.WhiteChars, count, options);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero.");
			}
			if (options != StringSplitOptions.None && options != StringSplitOptions.RemoveEmptyEntries)
			{
				throw new ArgumentException("Illegal enum value: " + options + ".");
			}
			if (count == 1)
			{
				return new string[]
				{
					this
				};
			}
			bool flag = (options & StringSplitOptions.RemoveEmptyEntries) == StringSplitOptions.RemoveEmptyEntries;
			if (count == 0 || (this == string.Empty && flag))
			{
				return new string[0];
			}
			List<string> list = new List<string>();
			int i = 0;
			int num = 0;
			while (i < this.Length)
			{
				int num2 = -1;
				int num3 = int.MaxValue;
				for (int j = 0; j < separator.Length; j++)
				{
					string text = separator[j];
					if (text != null && !(text == string.Empty))
					{
						int num4 = this.IndexOf(text, i);
						if (num4 > -1 && num4 < num3)
						{
							num2 = j;
							num3 = num4;
						}
					}
				}
				if (num2 == -1)
				{
					break;
				}
				if (num3 != i || !flag)
				{
					if (list.Count == count - 1)
					{
						break;
					}
					list.Add(this.Substring(i, num3 - i));
				}
				i = num3 + separator[num2].Length;
				num++;
			}
			if (num == 0)
			{
				return new string[]
				{
					this
				};
			}
			if (flag && num != 0 && i == this.Length && list.Count == 0)
			{
				return new string[0];
			}
			if (!flag || i != this.Length)
			{
				list.Add(this.Substring(i));
			}
			return list.ToArray();
		}

		/// <summary>Returns a string array that contains the substrings in this string that are delimited by elements of a specified Unicode character array. A parameter specifies whether to return empty array elements.</summary>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by one or more characters in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of Unicode characters that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param>
		/// <param name="options">Specify <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned, or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not one of the <see cref="T:System.StringSplitOptions" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001E4 RID: 484 RVA: 0x00006E60 File Offset: 0x00005060
		[ComVisible(false)]
		public string[] Split(char[] separator, StringSplitOptions options)
		{
			return this.Split(separator, int.MaxValue, options);
		}

		/// <summary>Returns a string array that contains the substrings in this string that are delimited by elements of a specified string array. A parameter specifies whether to return empty array elements.</summary>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by one or more strings in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of strings that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param>
		/// <param name="options">Specify <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned, or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not one of the <see cref="T:System.StringSplitOptions" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001E5 RID: 485 RVA: 0x00006E70 File Offset: 0x00005070
		[ComVisible(false)]
		public string[] Split(string[] separator, StringSplitOptions options)
		{
			return this.Split(separator, int.MaxValue, options);
		}

		/// <summary>Retrieves a substring from this instance. The substring starts at a specified character position.</summary>
		/// <returns>A <see cref="T:System.String" /> object equivalent to the substring that begins at <paramref name="startIndex" /> in this instance, or <see cref="F:System.String.Empty" /> if <paramref name="startIndex" /> is equal to the length of this instance.</returns>
		/// <param name="startIndex">The zero-based starting character position of a substring in this instance. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero or greater than the length of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001E6 RID: 486 RVA: 0x00006E80 File Offset: 0x00005080
		public string Substring(int startIndex)
		{
			if (startIndex == 0)
			{
				return this;
			}
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			return this.SubstringUnchecked(startIndex, this.length - startIndex);
		}

		/// <summary>Retrieves a substring from this instance. The substring starts at a specified character position and has a specified length.</summary>
		/// <returns>A <see cref="T:System.String" /> equivalent to the substring of length <paramref name="length" /> that begins at <paramref name="startIndex" /> in this instance, or <see cref="F:System.String.Empty" /> if <paramref name="startIndex" /> is equal to the length of this instance and <paramref name="length" /> is zero.</returns>
		/// <param name="startIndex">The zero-based starting character position of a substring in this instance. </param>
		/// <param name="length">The number of characters in the substring. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> plus <paramref name="length" /> indicates a position not within this instance.-or- <paramref name="startIndex" /> or <paramref name="length" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001E7 RID: 487 RVA: 0x00006EB8 File Offset: 0x000050B8
		public string Substring(int startIndex, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Cannot be negative.");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative.");
			}
			if (startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot exceed length of string.");
			}
			if (startIndex > this.length - length)
			{
				throw new ArgumentOutOfRangeException("length", "startIndex + length > this.length");
			}
			if (startIndex == 0 && length == this.length)
			{
				return this;
			}
			return this.SubstringUnchecked(startIndex, length);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006F4C File Offset: 0x0000514C
		internal unsafe string SubstringUnchecked(int startIndex, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(length);
			fixed (string text2 = text)
			{
				fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = this)
					{
						fixed (char* ptr = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(dest, ptr + startIndex, length);
							text2 = null;
							text3 = null;
							return text;
						}
					}
				}
			}
		}

		/// <summary>Removes all leading and trailing white-space characters from the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The string that remains after all white-space characters are removed from the start and end of the current <see cref="T:System.String" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001E9 RID: 489 RVA: 0x00006F98 File Offset: 0x00005198
		public string Trim()
		{
			if (this.length == 0)
			{
				return string.Empty;
			}
			int num = this.FindNotWhiteSpace(0, this.length, 1);
			if (num == this.length)
			{
				return string.Empty;
			}
			int num2 = this.FindNotWhiteSpace(this.length - 1, num, -1);
			int num3 = num2 - num + 1;
			if (num3 == this.length)
			{
				return this;
			}
			return this.SubstringUnchecked(num, num3);
		}

		/// <summary>Removes all leading and trailing occurrences of a set of characters specified in an array from the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The string that remains after all occurrences of the characters in the <paramref name="trimChars" /> parameter are removed from the start and end of the current <see cref="T:System.String" /> object. If <paramref name="trimChars" /> is null or an empty array, white-space characters are removed instead.</returns>
		/// <param name="trimChars">An array of Unicode characters to remove or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001EA RID: 490 RVA: 0x00007004 File Offset: 0x00005204
		public string Trim(params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.Trim();
			}
			if (this.length == 0)
			{
				return string.Empty;
			}
			int num = this.FindNotInTable(0, this.length, 1, trimChars);
			if (num == this.length)
			{
				return string.Empty;
			}
			int num2 = this.FindNotInTable(this.length - 1, num, -1, trimChars);
			int num3 = num2 - num + 1;
			if (num3 == this.length)
			{
				return this;
			}
			return this.SubstringUnchecked(num, num3);
		}

		/// <summary>Removes all leading occurrences of a set of characters specified in an array from the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The string that remains after all occurrences of characters in the <paramref name="trimChars" /> parameter are removed from the start of the current <see cref="T:System.String" /> object. If <paramref name="trimChars" /> is null or an empty array, white-space characters are removed instead.</returns>
		/// <param name="trimChars">An array of Unicode characters to remove or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001EB RID: 491 RVA: 0x00007088 File Offset: 0x00005288
		public string TrimStart(params char[] trimChars)
		{
			if (this.length == 0)
			{
				return string.Empty;
			}
			int num;
			if (trimChars == null || trimChars.Length == 0)
			{
				num = this.FindNotWhiteSpace(0, this.length, 1);
			}
			else
			{
				num = this.FindNotInTable(0, this.length, 1, trimChars);
			}
			if (num == 0)
			{
				return this;
			}
			return this.SubstringUnchecked(num, this.length - num);
		}

		/// <summary>Removes all trailing occurrences of a set of characters specified in an array from the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The string that remains after all occurrences of the characters in the <paramref name="trimChars" /> parameter are removed from the end of the current <see cref="T:System.String" /> object. If <paramref name="trimChars" /> is null or an empty array, white-space characters are removed instead.</returns>
		/// <param name="trimChars">An array of Unicode characters to remove or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001EC RID: 492 RVA: 0x000070F0 File Offset: 0x000052F0
		public string TrimEnd(params char[] trimChars)
		{
			if (this.length == 0)
			{
				return string.Empty;
			}
			int num;
			if (trimChars == null || trimChars.Length == 0)
			{
				num = this.FindNotWhiteSpace(this.length - 1, -1, -1);
			}
			else
			{
				num = this.FindNotInTable(this.length - 1, -1, -1, trimChars);
			}
			num++;
			if (num == this.length)
			{
				return this;
			}
			return this.SubstringUnchecked(0, num);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007160 File Offset: 0x00005360
		private int FindNotWhiteSpace(int pos, int target, int change)
		{
			while (pos != target)
			{
				char c = this[pos];
				if (c < '\u0085')
				{
					if (c != ' ' && (c < '\t' || c > '\r'))
					{
						return pos;
					}
				}
				else if (c != '\u00a0' && c != '﻿' && c != '\u3000' && c != '\u0085' && c != '\u1680' && c != '\u2028' && c != '\u2029' && (c < '\u2000' || c > '​'))
				{
					return pos;
				}
				pos += change;
			}
			return pos;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007218 File Offset: 0x00005418
		private unsafe int FindNotInTable(int pos, int target, int change, char[] table)
		{
			fixed (char* ptr = ref (table != null && table.Length != 0) ? ref table[0] : ref *null)
			{
				fixed (string text = this)
				{
					fixed (char* ptr2 = text + RuntimeHelpers.OffsetToStringData / 2)
					{
						while (pos != target)
						{
							char c = ptr2[pos];
							int i;
							for (i = 0; i < table.Length; i++)
							{
								if (c == ptr[i])
								{
									break;
								}
							}
							if (i == table.Length)
							{
								return pos;
							}
							pos += change;
						}
						ptr = null;
						text = null;
						return pos;
					}
				}
			}
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects and returns an integer that indicates their relationship to one another in the sort order.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> equals <paramref name="strB" />. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first <see cref="T:System.String" />. </param>
		/// <param name="strB">The second <see cref="T:System.String" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001EF RID: 495 RVA: 0x000072A4 File Offset: 0x000054A4
		public static int Compare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects, ignoring or honoring their case, and returns an integer that indicates their relationship to one another in the sort order.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> equals <paramref name="strB" />. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first <see cref="T:System.String" />. </param>
		/// <param name="strB">The second <see cref="T:System.String" />. </param>
		/// <param name="ignoreCase">A <see cref="T:System.Boolean" /> indicating a case-sensitive or insensitive comparison. (true indicates a case-insensitive comparison.) </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001F0 RID: 496 RVA: 0x000072B8 File Offset: 0x000054B8
		public static int Compare(string strA, string strB, bool ignoreCase)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, (!ignoreCase) ? CompareOptions.None : CompareOptions.IgnoreCase);
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects, ignoring or honoring their case, and using culture-specific information to influence the comparison, and returns an integer that indicates their relationship to one another in the sort order.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> equals <paramref name="strB" />. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first <see cref="T:System.String" />. </param>
		/// <param name="strB">The second <see cref="T:System.String" />. </param>
		/// <param name="ignoreCase">A <see cref="T:System.Boolean" /> indicating a case-sensitive or insensitive comparison. (true indicates a case-insensitive comparison.) </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that supplies culture-specific comparison information. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001F1 RID: 497 RVA: 0x000072E4 File Offset: 0x000054E4
		public static int Compare(string strA, string strB, bool ignoreCase, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.CompareInfo.Compare(strA, strB, (!ignoreCase) ? CompareOptions.None : CompareOptions.IgnoreCase);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition Less than zero The substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />. Zero The substrings are equal, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first <see cref="T:System.String" />. </param>
		/// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
		/// <param name="strB">The second <see cref="T:System.String" />. </param>
		/// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="indexA" /> or <paramref name="indexB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001F2 RID: 498 RVA: 0x00007314 File Offset: 0x00005514
		public static int Compare(string strA, int indexA, string strB, int indexB, int length)
		{
			return string.Compare(strA, indexA, strB, indexB, length, false, CultureInfo.CurrentCulture);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects, ignoring or honoring their case, and returns an integer that indicates their relationship to one another in the sort order.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.ValueCondition Less than zero The substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />. Zero The substrings are equal, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first <see cref="T:System.String" />. </param>
		/// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
		/// <param name="strB">The second <see cref="T:System.String" />. </param>
		/// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <param name="ignoreCase">A <see cref="T:System.Boolean" /> indicating a case-sensitive or insensitive comparison. (true indicates a case-insensitive comparison.) </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="indexA" /> or <paramref name="indexB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001F3 RID: 499 RVA: 0x00007334 File Offset: 0x00005534
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase)
		{
			return string.Compare(strA, indexA, strB, indexB, length, ignoreCase, CultureInfo.CurrentCulture);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects, ignoring or honoring their case and using culture-specific information to influence the comparison. The method returns an integer that indicates their relationship to one another in the sort order.</summary>
		/// <returns>An integer indicating the lexical relationship between the two comparands.Value Condition Less than zero The substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />. Zero The substrings are equal, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first <see cref="T:System.String" />. </param>
		/// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
		/// <param name="strB">The second <see cref="T:System.String" />. </param>
		/// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <param name="ignoreCase">A <see cref="T:System.Boolean" /> indicating a case-sensitive or insensitive comparison. (true indicates a case-insensitive comparison.) </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that supplies culture-specific comparison information. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="strA" /> or <paramref name="strB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001F4 RID: 500 RVA: 0x00007354 File Offset: 0x00005554
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (indexA > strA.Length || indexB > strB.Length || indexA < 0 || indexB < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (length == 0)
			{
				return 0;
			}
			if (strA == null)
			{
				if (strB == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (strB == null)
				{
					return 1;
				}
				CompareOptions options;
				if (ignoreCase)
				{
					options = CompareOptions.IgnoreCase;
				}
				else
				{
					options = CompareOptions.None;
				}
				int length2 = length;
				int length3 = length;
				if (length > strA.Length - indexA)
				{
					length2 = strA.Length - indexA;
				}
				if (length > strB.Length - indexB)
				{
					length3 = strB.Length - indexB;
				}
				return culture.CompareInfo.Compare(strA, indexA, length2, strB, indexB, length3, options);
			}
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects. A parameter specifies whether the comparison uses the current or invariant culture, honors or ignores case, and uses word or ordinal sort rules. The method returns an integer that indicates the relationship of the two <see cref="T:System.String" /> objects to one another in the sort order.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> equals <paramref name="strB" />. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first <see cref="T:System.String" /> object.</param>
		/// <param name="strB">The second <see cref="T:System.String" /> object. </param>
		/// <param name="comparisonType">One of the <see cref="T:System.StringComparison" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="T:System.StringComparison" /> is not supported.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001F5 RID: 501 RVA: 0x00007424 File Offset: 0x00005624
		public static int Compare(string strA, string strB, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return string.Compare(strA, strB, false, CultureInfo.CurrentCulture);
			case StringComparison.CurrentCultureIgnoreCase:
				return string.Compare(strA, strB, true, CultureInfo.CurrentCulture);
			case StringComparison.InvariantCulture:
				return string.Compare(strA, strB, false, CultureInfo.InvariantCulture);
			case StringComparison.InvariantCultureIgnoreCase:
				return string.Compare(strA, strB, true, CultureInfo.InvariantCulture);
			case StringComparison.Ordinal:
				return string.CompareOrdinalUnchecked(strA, 0, int.MaxValue, strB, 0, int.MaxValue);
			case StringComparison.OrdinalIgnoreCase:
				return string.CompareOrdinalCaseInsensitiveUnchecked(strA, 0, int.MaxValue, strB, 0, int.MaxValue);
			default:
			{
				string text = Locale.GetText("Invalid value '{0}' for StringComparison", new object[]
				{
					comparisonType
				});
				throw new ArgumentException(text, "comparisonType");
			}
			}
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects and returns an integer that indicates their relationship to one another in the sort order.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition Less than zero The substring in the <paramref name="strA" /> parameter is less than the substring in the <paramref name="strB" /> parameter.Zero The substrings are equal, or the <paramref name="length" /> parameter is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to use in the comparison. </param>
		/// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
		/// <param name="strB">The second string to use in the comparison.</param>
		/// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <param name="comparisonType">One of the <see cref="T:System.StringComparison" /> values. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="indexA" /> or <paramref name="indexB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001F6 RID: 502 RVA: 0x000074DC File Offset: 0x000056DC
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return string.Compare(strA, indexA, strB, indexB, length, false, CultureInfo.CurrentCulture);
			case StringComparison.CurrentCultureIgnoreCase:
				return string.Compare(strA, indexA, strB, indexB, length, true, CultureInfo.CurrentCulture);
			case StringComparison.InvariantCulture:
				return string.Compare(strA, indexA, strB, indexB, length, false, CultureInfo.InvariantCulture);
			case StringComparison.InvariantCultureIgnoreCase:
				return string.Compare(strA, indexA, strB, indexB, length, true, CultureInfo.InvariantCulture);
			case StringComparison.Ordinal:
				return string.CompareOrdinal(strA, indexA, strB, indexB, length);
			case StringComparison.OrdinalIgnoreCase:
				return string.CompareOrdinalCaseInsensitive(strA, indexA, strB, indexB, length);
			default:
			{
				string text = Locale.GetText("Invalid value '{0}' for StringComparison", new object[]
				{
					comparisonType
				});
				throw new ArgumentException(text, "comparisonType");
			}
			}
		}

		/// <summary>Determines whether two specified <see cref="T:System.String" /> objects have the same value. A parameter specifies the culture, case, and sort rules used in the comparison.</summary>
		/// <returns>true if the value of the <paramref name="a" /> parameter is equal to the value of the <paramref name="b" /> parameter; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.String" /> object or null. </param>
		/// <param name="b">A <see cref="T:System.String" /> object or null. </param>
		/// <param name="comparisonType">One of the <see cref="T:System.StringComparison" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001F7 RID: 503 RVA: 0x00007598 File Offset: 0x00005798
		public static bool Equals(string a, string b, StringComparison comparisonType)
		{
			return string.Compare(a, b, comparisonType) == 0;
		}

		/// <summary>Determines whether this string and a specified <see cref="T:System.String" /> object have the same value. A parameter specifies the culture, case, and sort rules used in the comparison.</summary>
		/// <returns>true if the value of the <paramref name="value" /> parameter is the same as this string; otherwise, false.</returns>
		/// <param name="value">A string to compare.</param>
		/// <param name="comparisonType">A value that defines the type of comparison. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001F8 RID: 504 RVA: 0x000075A8 File Offset: 0x000057A8
		public bool Equals(string value, StringComparison comparisonType)
		{
			return string.Compare(value, this, comparisonType) == 0;
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects using the specified comparison options and culture-specific information to influence the comparison, and returns an integer that indicates the relationship of the two strings to each other in the sort order.</summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between <paramref name="strA" /> and <paramref name="strB" />, as shown in the following tableValueConditionLess than zero<paramref name="strA" /> is less than <paramref name="strB" />.Zero<paramref name="strA" /> equals <paramref name="strB" />.Greater than zero<paramref name="strA" /> is greater than <paramref name="strB" />.</returns>
		/// <param name="strA">The first string.  </param>
		/// <param name="strB">The second string.</param>
		/// <param name="culture">The culture that supplies culture-specific comparison information.</param>
		/// <param name="options">Options to use when performing the comparison (such as ignoring case or symbols).  </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null.</exception>
		// Token: 0x060001F9 RID: 505 RVA: 0x000075B8 File Offset: 0x000057B8
		public static int Compare(string strA, string strB, CultureInfo culture, CompareOptions options)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.CompareInfo.Compare(strA, strB, options);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects using the specified comparison options and culture-specific information to influence the comparison, and returns an integer that indicates the relationship of the two substrings to each other in the sort order.</summary>
		/// <returns>An integer that indicates the lexical relationship between the two substrings, as shown in the following table.ValueConditionLess than zeroThe substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />.ZeroThe substrings are equal or <paramref name="length" /> is zero.Greater than zeroThe substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />.</returns>
		/// <param name="strA">The first string.   </param>
		/// <param name="indexA">The starting position of the substring within <paramref name="strA" />.</param>
		/// <param name="strB">The second string.</param>
		/// <param name="indexB">The starting position of the substring within <paramref name="strB" />.</param>
		/// <param name="length">The maximum number of characters in the substrings to compare.</param>
		/// <param name="culture">The culture that supplies culture-specific comparison information.</param>
		/// <param name="options">Options to use when performing the comparison (such as ignoring case or symbols).  </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.Length.-or-<paramref name="indexB" /> is greater than <paramref name="strB" />.Length.-or-<paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative.-or-Either <paramref name="strA" /> or <paramref name="strB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null.</exception>
		// Token: 0x060001FA RID: 506 RVA: 0x000075DC File Offset: 0x000057DC
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, CultureInfo culture, CompareOptions options)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			int length2 = length;
			int length3 = length;
			if (length > strA.Length - indexA)
			{
				length2 = strA.Length - indexA;
			}
			if (length > strB.Length - indexB)
			{
				length3 = strB.Length - indexB;
			}
			return culture.CompareInfo.Compare(strA, indexA, length2, strB, indexB, length3, options);
		}

		/// <summary>Compares this instance with a specified <see cref="T:System.Object" /> and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Object" />.</summary>
		/// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="value" /> parameter.Value Condition Less than zero This instance precedes <paramref name="value" />. Zero This instance has the same position in the sort order as <paramref name="value" />. Greater than zero This instance follows <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An <see cref="T:System.Object" /> that evaluates to a String. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.String" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001FB RID: 507 RVA: 0x00007648 File Offset: 0x00005848
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is string))
			{
				throw new ArgumentException();
			}
			return string.Compare(this, (string)value);
		}

		/// <summary>Compares this instance with a specified <see cref="T:System.String" /> object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.String" />.</summary>
		/// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="value" /> parameter.Value Condition Less than zero This instance precedes <paramref name="strB" />. Zero This instance has the same position in the sort order as <paramref name="strB" />. Greater than zero This instance follows <paramref name="strB" />.-or- <paramref name="strB" /> is null. </returns>
		/// <param name="strB">A <see cref="T:System.String" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001FC RID: 508 RVA: 0x00007670 File Offset: 0x00005870
		public int CompareTo(string strB)
		{
			if (strB == null)
			{
				return 1;
			}
			return string.Compare(this, strB);
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects by evaluating the numeric values of the corresponding <see cref="T:System.Char" /> objects in each string.</summary>
		/// <returns>An integer indicating the lexical relationship between the two comparands.ValueCondition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> and <paramref name="strB" /> are equal. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first <see cref="T:System.String" />. </param>
		/// <param name="strB">The second <see cref="T:System.String" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001FD RID: 509 RVA: 0x00007684 File Offset: 0x00005884
		public static int CompareOrdinal(string strA, string strB)
		{
			return string.CompareOrdinalUnchecked(strA, 0, int.MaxValue, strB, 0, int.MaxValue);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects by evaluating the numeric values of the corresponding <see cref="T:System.Char" /> objects in each substring. </summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.ValueCondition Less than zero The substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />. Zero The substrings are equal, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first <see cref="T:System.String" />. </param>
		/// <param name="indexA">The starting index of the substring in <paramref name="strA" />. </param>
		/// <param name="strB">The second <see cref="T:System.String" />. </param>
		/// <param name="indexB">The starting index of the substring in <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />. <see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />. <see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001FE RID: 510 RVA: 0x0000769C File Offset: 0x0000589C
		public static int CompareOrdinal(string strA, int indexA, string strB, int indexB, int length)
		{
			if (indexA > strA.Length || indexB > strB.Length || indexA < 0 || indexB < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return string.CompareOrdinalUnchecked(strA, indexA, length, strB, indexB, length);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000076EC File Offset: 0x000058EC
		internal static int CompareOrdinalCaseInsensitive(string strA, int indexA, string strB, int indexB, int length)
		{
			if (indexA > strA.Length || indexB > strB.Length || indexA < 0 || indexB < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return string.CompareOrdinalCaseInsensitiveUnchecked(strA, indexA, length, strB, indexB, length);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000773C File Offset: 0x0000593C
		internal unsafe static int CompareOrdinalUnchecked(string strA, int indexA, int lenA, string strB, int indexB, int lenB)
		{
			if (strA == null)
			{
				if (strB == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (strB == null)
				{
					return 1;
				}
				int num = Math.Min(lenA, strA.Length - indexA);
				int num2 = Math.Min(lenB, strB.Length - indexB);
				if (num == num2 && object.ReferenceEquals(strA, strB))
				{
					return 0;
				}
				fixed (char* ptr = strA + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (char* ptr2 = strB + RuntimeHelpers.OffsetToStringData / 2)
					{
						char* ptr3 = ptr + indexA;
						char* ptr4 = ptr3 + Math.Min(num, num2);
						char* ptr5 = ptr2 + indexB;
						while (ptr3 < ptr4)
						{
							if (*ptr3 != *ptr5)
							{
								return (int)(*ptr3 - *ptr5);
							}
							ptr3++;
							ptr5++;
						}
						return num - num2;
					}
				}
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007800 File Offset: 0x00005A00
		internal unsafe static int CompareOrdinalCaseInsensitiveUnchecked(string strA, int indexA, int lenA, string strB, int indexB, int lenB)
		{
			if (strA == null)
			{
				if (strB == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (strB == null)
				{
					return 1;
				}
				int num = Math.Min(lenA, strA.Length - indexA);
				int num2 = Math.Min(lenB, strB.Length - indexB);
				if (num == num2 && object.ReferenceEquals(strA, strB))
				{
					return 0;
				}
				fixed (char* ptr = strA + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (char* ptr2 = strB + RuntimeHelpers.OffsetToStringData / 2)
					{
						char* ptr3 = ptr + indexA;
						char* ptr4 = ptr3 + Math.Min(num, num2);
						char* ptr5 = ptr2 + indexB;
						while (ptr3 < ptr4)
						{
							if (*ptr3 != *ptr5)
							{
								char c = char.ToUpperInvariant(*ptr3);
								char c2 = char.ToUpperInvariant(*ptr5);
								if (c != c2)
								{
									return (int)(c - c2);
								}
							}
							ptr3++;
							ptr5++;
						}
						return num - num2;
					}
				}
			}
		}

		/// <summary>Determines whether the end of this instance matches the specified string.</summary>
		/// <returns>true if <paramref name="value" /> matches the end of this instance; otherwise, false.</returns>
		/// <param name="value">A <see cref="T:System.String" /> to compare to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000202 RID: 514 RVA: 0x000078E0 File Offset: 0x00005AE0
		public bool EndsWith(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
		}

		/// <summary>Determines whether the end of this string matches the specified string when compared using the specified culture.</summary>
		/// <returns>true if the <paramref name="value" /> parameter matches the end of this string; otherwise, false.</returns>
		/// <param name="value">A <see cref="T:System.String" /> object to compare to. </param>
		/// <param name="ignoreCase">true to ignore case when comparing this instance and <paramref name="value" />; otherwise, false.</param>
		/// <param name="culture">Cultural information that determines how this instance and <paramref name="value" /> are compared. If <paramref name="culture" /> is null, the current culture is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000203 RID: 515 RVA: 0x00007908 File Offset: 0x00005B08
		public bool EndsWith(string value, bool ignoreCase, CultureInfo culture)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			return culture.CompareInfo.IsSuffix(this, value, (!ignoreCase) ? CompareOptions.None : CompareOptions.IgnoreCase);
		}

		/// <summary>Reports the index of the first occurrence in this instance of any character in a specified array of Unicode characters.</summary>
		/// <returns>The zero-based index position of the first occurrence in this instance where any character in <paramref name="anyOf" /> was found; otherwise, -1 if no character in <paramref name="anyOf" /> was found.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000204 RID: 516 RVA: 0x00007950 File Offset: 0x00005B50
		public int IndexOfAny(char[] anyOf)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			if (this.length == 0)
			{
				return -1;
			}
			return this.IndexOfAnyUnchecked(anyOf, 0, this.length);
		}

		/// <summary>Reports the index of the first occurrence in this instance of any character in a specified array of Unicode characters. The search starts at a specified character position.</summary>
		/// <returns>The zero-based index position of the first occurrence in this instance where any character in <paramref name="anyOf" /> was found; otherwise, -1 if no character in <paramref name="anyOf" /> was found.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is negative.-or- <paramref name="startIndex" /> is greater than the number of characters in this instance. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000205 RID: 517 RVA: 0x0000797C File Offset: 0x00005B7C
		public int IndexOfAny(char[] anyOf, int startIndex)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.IndexOfAnyUnchecked(anyOf, startIndex, this.length - startIndex);
		}

		/// <summary>Reports the index of the first occurrence in this instance of any character in a specified array of Unicode characters. The search starts at a specified character position and examines a specified number of character positions.</summary>
		/// <returns>The zero-based index position of the first occurrence in this instance where any character in <paramref name="anyOf" /> was found; otherwise, -1 if no character in <paramref name="anyOf" /> was found.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="count" /> + <paramref name="startIndex" /> is greater than the number of characters in this instance. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000206 RID: 518 RVA: 0x000079B4 File Offset: 0x00005BB4
		public int IndexOfAny(char[] anyOf, int startIndex, int count)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (count < 0 || startIndex > this.length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be negative, and startIndex + count must be less than length of the string.");
			}
			return this.IndexOfAnyUnchecked(anyOf, startIndex, count);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007A14 File Offset: 0x00005C14
		private unsafe int IndexOfAnyUnchecked(char[] anyOf, int startIndex, int count)
		{
			if (anyOf.Length == 0)
			{
				return -1;
			}
			if (anyOf.Length == 1)
			{
				return this.IndexOfUnchecked(anyOf[0], startIndex, count);
			}
			fixed (char* ptr = ref (anyOf != null && anyOf.Length != 0) ? ref anyOf[0] : ref *null)
			{
				int num = (int)(*ptr);
				int num2 = (int)(*ptr);
				char* ptr2 = ptr + anyOf.Length;
				char* ptr3 = ptr;
				while (++ptr3 != ptr2)
				{
					if ((int)(*ptr3) > num)
					{
						num = (int)(*ptr3);
					}
					else if ((int)(*ptr3) < num2)
					{
						num2 = (int)(*ptr3);
					}
				}
				fixed (char* ptr4 = &this.start_char)
				{
					char* ptr5 = ptr4 + startIndex;
					char* ptr6 = ptr5 + count;
					while (ptr5 != ptr6)
					{
						if ((int)(*ptr5) > num || (int)(*ptr5) < num2)
						{
							ptr5++;
						}
						else
						{
							if (*ptr5 == *ptr)
							{
								return (int)((long)(ptr5 - ptr4));
							}
							ptr3 = ptr;
							while (++ptr3 != ptr2)
							{
								if (*ptr5 == *ptr3)
								{
									return (int)((long)(ptr5 - ptr4));
								}
							}
							ptr5++;
						}
					}
				}
			}
			return -1;
		}

		/// <summary>Reports the index of the first occurrence of the specified string in the current <see cref="T:System.String" /> object. A parameter specifies the type of search to use for the specified string.</summary>
		/// <returns>The index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is 0.</returns>
		/// <param name="value">The <see cref="T:System.String" /> object to seek. </param>
		/// <param name="comparisonType">One of the <see cref="T:System.StringComparison" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x06000208 RID: 520 RVA: 0x00007B34 File Offset: 0x00005D34
		public int IndexOf(string value, StringComparison comparisonType)
		{
			return this.IndexOf(value, 0, this.Length, comparisonType);
		}

		/// <summary>Reports the index of the first occurrence of the specified string in the current <see cref="T:System.String" /> object. Parameters specify the starting search position in the current string and the type of search to use for the specified string.</summary>
		/// <returns>The zero-based index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> object to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="comparisonType">One of the <see cref="T:System.StringComparison" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is negative, or specifies a position that is not within this instance. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x06000209 RID: 521 RVA: 0x00007B48 File Offset: 0x00005D48
		public int IndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.IndexOf(value, startIndex, this.Length - startIndex, comparisonType);
		}

		/// <summary>Reports the index of the first occurrence of the specified string in the current <see cref="T:System.String" /> object. Parameters specify the starting search position in the current string, the number of characters in the current string to search, and the type of search to use for the specified string.</summary>
		/// <returns>The zero-based index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> object to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <param name="comparisonType">One of the <see cref="T:System.StringComparison" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="count" /> plus <paramref name="startIndex" /> specify a position that is not within this instance. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x0600020A RID: 522 RVA: 0x00007B5C File Offset: 0x00005D5C
		public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return this.IndexOfOrdinal(value, startIndex, count, CompareOptions.Ordinal);
			case StringComparison.OrdinalIgnoreCase:
				return this.IndexOfOrdinal(value, startIndex, count, CompareOptions.OrdinalIgnoreCase);
			default:
			{
				string text = Locale.GetText("Invalid value '{0}' for StringComparison", new object[]
				{
					comparisonType
				});
				throw new ArgumentException(text, "comparisonType");
			}
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007C28 File Offset: 0x00005E28
		internal int IndexOfOrdinal(string value, int startIndex, int count, CompareOptions options)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (count < 0 || this.length - startIndex < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (options == CompareOptions.Ordinal)
			{
				return this.IndexOfOrdinalUnchecked(value, startIndex, count);
			}
			return this.IndexOfOrdinalIgnoreCaseUnchecked(value, startIndex, count);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007C98 File Offset: 0x00005E98
		internal unsafe int IndexOfOrdinalUnchecked(string value, int startIndex, int count)
		{
			int num = value.Length;
			if (count < num)
			{
				return -1;
			}
			if (num > 1)
			{
				fixed (string text = this)
				{
					fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text2 = value)
						{
							fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
							{
								char* ptr3 = ptr + startIndex;
								char* ptr4 = ptr3 + count - num + 1;
								while (ptr3 != ptr4)
								{
									if (*ptr3 == *ptr2)
									{
										for (int i = 1; i < num; i++)
										{
											if (ptr3[i] != ptr2[i])
											{
												goto IL_A1;
											}
										}
										return (int)((long)(ptr3 - ptr));
									}
									IL_A1:
									ptr3++;
								}
								text = null;
								text2 = null;
								return -1;
							}
						}
					}
				}
			}
			if (num == 1)
			{
				return this.IndexOfUnchecked(value[0], startIndex, count);
			}
			return startIndex;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007D5C File Offset: 0x00005F5C
		internal unsafe int IndexOfOrdinalIgnoreCaseUnchecked(string value, int startIndex, int count)
		{
			int num = value.Length;
			if (count < num)
			{
				return -1;
			}
			if (num == 0)
			{
				return startIndex;
			}
			fixed (string text = this)
			{
				fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text2 = value)
					{
						fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
						{
							char* ptr3 = ptr + startIndex;
							char* ptr4 = ptr3 + count - num + 1;
							IL_8F:
							while (ptr3 != ptr4)
							{
								for (int i = 0; i < num; i++)
								{
									if (char.ToUpperInvariant(ptr3[i]) != char.ToUpperInvariant(ptr2[i]))
									{
										ptr3++;
										goto IL_8F;
									}
								}
								return (int)((long)(ptr3 - ptr));
							}
							text = null;
							text2 = null;
							return -1;
						}
					}
				}
			}
		}

		/// <summary>Reports the index of the last occurrence of a specified string within the current <see cref="T:System.String" /> object. A parameter specifies the type of search to use for the specified string.</summary>
		/// <returns>The index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the last index position in this instance.</returns>
		/// <param name="value">The <see cref="T:System.String" /> object to seek. </param>
		/// <param name="comparisonType">One of the <see cref="T:System.StringComparison" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x0600020E RID: 526 RVA: 0x00007E08 File Offset: 0x00006008
		public int LastIndexOf(string value, StringComparison comparisonType)
		{
			if (this.Length == 0)
			{
				return (!(value == string.Empty)) ? -1 : 0;
			}
			return this.LastIndexOf(value, this.Length - 1, this.Length, comparisonType);
		}

		/// <summary>Reports the index of the last occurrence of a specified string within the current <see cref="T:System.String" /> object. Parameters specify the starting search position in the current string, and type of search to use for the specified string.</summary>
		/// <returns>The index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> object to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="comparisonType">One of the <see cref="T:System.StringComparison" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" /> and <paramref name="startIndex" /> is less than zero or specifies a position that is not within this instance. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x0600020F RID: 527 RVA: 0x00007E50 File Offset: 0x00006050
		public int LastIndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1, comparisonType);
		}

		/// <summary>Reports the index position of the last occurrence of a specified <see cref="T:System.String" /> object within this instance. Parameters specify the starting search position in the current string, the number of characters in the current string to search, and the type of search to use for the specified string.</summary>
		/// <returns>The index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> object to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <param name="comparisonType">One of the <see cref="T:System.StringComparison" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" /> and <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="startIndex" /> is greater than the length of this instance.-or-<paramref name="startIndex" /> + 1 - <paramref name="count" /> specifies a position that is not within this instance. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x06000210 RID: 528 RVA: 0x00007E60 File Offset: 0x00006060
		public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return this.LastIndexOfOrdinal(value, startIndex, count, CompareOptions.Ordinal);
			case StringComparison.OrdinalIgnoreCase:
				return this.LastIndexOfOrdinal(value, startIndex, count, CompareOptions.OrdinalIgnoreCase);
			default:
			{
				string text = Locale.GetText("Invalid value '{0}' for StringComparison", new object[]
				{
					comparisonType
				});
				throw new ArgumentException(text, "comparisonType");
			}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007F2C File Offset: 0x0000612C
		internal int LastIndexOfOrdinal(string value, int startIndex, int count, CompareOptions options)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (count < 0 || startIndex < count - 1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (options == CompareOptions.Ordinal)
			{
				return this.LastIndexOfOrdinalUnchecked(value, startIndex, count);
			}
			return this.LastIndexOfOrdinalIgnoreCaseUnchecked(value, startIndex, count);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007FA4 File Offset: 0x000061A4
		internal unsafe int LastIndexOfOrdinalUnchecked(string value, int startIndex, int count)
		{
			int num = value.Length;
			if (count < num)
			{
				return -1;
			}
			if (num > 1)
			{
				fixed (string text = this)
				{
					fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text2 = value)
						{
							fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
							{
								char* ptr3 = ptr + startIndex - num + 1;
								char* ptr4 = ptr3 - count + num - 1;
								while (ptr3 != ptr4)
								{
									if (*ptr3 == *ptr2)
									{
										for (int i = 1; i < num; i++)
										{
											if (ptr3[i] != ptr2[i])
											{
												goto IL_A7;
											}
										}
										return (int)((long)(ptr3 - ptr));
									}
									IL_A7:
									ptr3--;
								}
								text = null;
								text2 = null;
								return -1;
							}
						}
					}
				}
			}
			if (num == 1)
			{
				return this.LastIndexOfUnchecked(value[0], startIndex, count);
			}
			return startIndex;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000806C File Offset: 0x0000626C
		internal unsafe int LastIndexOfOrdinalIgnoreCaseUnchecked(string value, int startIndex, int count)
		{
			int num = value.Length;
			if (count < num)
			{
				return -1;
			}
			if (num == 0)
			{
				return startIndex;
			}
			fixed (string text = this)
			{
				fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text2 = value)
					{
						fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
						{
							char* ptr3 = ptr + startIndex - num + 1;
							char* ptr4 = ptr3 - count + num - 1;
							IL_95:
							while (ptr3 != ptr4)
							{
								for (int i = 0; i < num; i++)
								{
									if (char.ToUpperInvariant(ptr3[i]) != char.ToUpperInvariant(ptr2[i]))
									{
										ptr3--;
										goto IL_95;
									}
								}
								return (int)((long)(ptr3 - ptr));
							}
							text = null;
							text2 = null;
							return -1;
						}
					}
				}
			}
		}

		/// <summary>Reports the index of the first occurrence of the specified Unicode character in this string.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not.</returns>
		/// <param name="value">A Unicode character to seek. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000214 RID: 532 RVA: 0x00008120 File Offset: 0x00006320
		public int IndexOf(char value)
		{
			if (this.length == 0)
			{
				return -1;
			}
			return this.IndexOfUnchecked(value, 0, this.length);
		}

		/// <summary>Reports the index of the first occurrence of the specified Unicode character in this string. The search starts at a specified character position.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not.</returns>
		/// <param name="value">A Unicode character to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero or specifies a position beyond the end of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000215 RID: 533 RVA: 0x00008140 File Offset: 0x00006340
		public int IndexOf(char value, int startIndex)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0");
			}
			if (startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "startIndex > this.length");
			}
			if ((startIndex == 0 && this.length == 0) || startIndex == this.length)
			{
				return -1;
			}
			return this.IndexOfUnchecked(value, startIndex, this.length - startIndex);
		}

		/// <summary>Reports the index of the first occurrence of the specified character in this instance. The search starts at a specified character position and examines a specified number of character positions.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not.</returns>
		/// <param name="value">A Unicode character to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="count" /> + <paramref name="startIndex" /> specifies a position beyond the end of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000216 RID: 534 RVA: 0x000081B0 File Offset: 0x000063B0
		public int IndexOf(char value, int startIndex, int count)
		{
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative and must be< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (startIndex > this.length - count)
			{
				throw new ArgumentOutOfRangeException("count", "startIndex + count > this.length");
			}
			if ((startIndex == 0 && this.length == 0) || startIndex == this.length || count == 0)
			{
				return -1;
			}
			return this.IndexOfUnchecked(value, startIndex, count);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008244 File Offset: 0x00006444
		internal unsafe int IndexOfUnchecked(char value, int startIndex, int count)
		{
			char* ptr = ref this.start_char + startIndex * 2;
			char* ptr2 = ptr + (count >> 3 << 3);
			while (ptr != ptr2)
			{
				if (*ptr == value)
				{
					return (ptr - ref this.start_char / 2) / 2;
				}
				if (ptr[1] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 1L / 2L;
				}
				if (ptr[2] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 2L / 2L;
				}
				if (ptr[3] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 3L / 2L;
				}
				if (ptr[4] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 4L / 2L;
				}
				if (ptr[5] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 5L / 2L;
				}
				if (ptr[6] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 6L / 2L;
				}
				if (ptr[7] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 + 7L / 2L;
				}
				ptr += 8;
			}
			ptr2 += (count & 7);
			while (ptr != ptr2)
			{
				if (*ptr == value)
				{
					return (ptr - ref this.start_char / 2) / 2;
				}
				ptr++;
			}
			return -1;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000834C File Offset: 0x0000654C
		internal unsafe int IndexOfOrdinalIgnoreCase(char value, int startIndex, int count)
		{
			if (this.length == 0)
			{
				return -1;
			}
			int num = startIndex + count;
			char c = char.ToUpperInvariant(value);
			fixed (char* ptr = &this.start_char)
			{
				for (int i = startIndex; i < num; i++)
				{
					if (char.ToUpperInvariant(ptr[i]) == c)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Reports the index of the first occurrence of the specified <see cref="T:System.String" /> in this instance.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is 0.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000219 RID: 537 RVA: 0x000083A4 File Offset: 0x000065A4
		public int IndexOf(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.length == 0)
			{
				return 0;
			}
			if (this.length == 0)
			{
				return -1;
			}
			return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, 0, this.length, CompareOptions.Ordinal);
		}

		/// <summary>Reports the index of the first occurrence of the specified <see cref="T:System.String" /> in this instance. The search starts at a specified character position.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is negative.-or- <paramref name="startIndex" /> specifies a position not within this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600021A RID: 538 RVA: 0x000083FC File Offset: 0x000065FC
		public int IndexOf(string value, int startIndex)
		{
			return this.IndexOf(value, startIndex, this.length - startIndex);
		}

		/// <summary>Reports the index of the first occurrence of the specified <see cref="T:System.String" /> in this instance. The search starts at a specified character position and examines a specified number of character positions.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="count" /> plus <paramref name="startIndex" /> specify a position not within this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600021B RID: 539 RVA: 0x00008410 File Offset: 0x00006610
		public int IndexOf(string value, int startIndex, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative, and should not exceed length of string.");
			}
			if (count < 0 || startIndex > this.length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative, and should point to location in string.");
			}
			if (value.length == 0)
			{
				return startIndex;
			}
			if (startIndex == 0 && this.length == 0)
			{
				return -1;
			}
			if (count == 0)
			{
				return -1;
			}
			return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count);
		}

		/// <summary>Reports the index position of the last occurrence in this instance of one or more characters specified in a Unicode array.</summary>
		/// <returns>The index position of the last occurrence in this instance where any character in <paramref name="anyOf" /> was found; otherwise, -1 if no character in <paramref name="anyOf" /> was found.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600021C RID: 540 RVA: 0x000084B4 File Offset: 0x000066B4
		public int LastIndexOfAny(char[] anyOf)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			return this.LastIndexOfAnyUnchecked(anyOf, this.length - 1, this.length);
		}

		/// <summary>Reports the index position of the last occurrence in this instance of one or more characters specified in a Unicode array. The search starts at a specified character position.</summary>
		/// <returns>The index position of the last occurrence in this instance where any character in <paramref name="anyOf" /> was found; otherwise, -1 if no character in <paramref name="anyOf" /> was found or if the current instance equals <see cref="F:System.String.Empty" />.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" /> and <paramref name="startIndex" /> specifies a position not within this instance. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600021D RID: 541 RVA: 0x000084D8 File Offset: 0x000066D8
		public int LastIndexOfAny(char[] anyOf, int startIndex)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			if (startIndex < 0 || startIndex >= this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative, and should be less than length of string.");
			}
			if (this.length == 0)
			{
				return -1;
			}
			return this.LastIndexOfAnyUnchecked(anyOf, startIndex, startIndex + 1);
		}

		/// <summary>Reports the index position of the last occurrence in this instance of one or more characters specified in a Unicode array. The search starts at a specified character position and examines a specified number of character positions.</summary>
		/// <returns>The index position of the last occurrence in this instance where any character in <paramref name="anyOf" /> was found; -1 if no character in <paramref name="anyOf" /> was found or if the current instance equals <see cref="F:System.String.Empty" />.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- The current instance does not equal <see cref="F:System.String.Empty" /> and <paramref name="startIndex" /> minus <paramref name="count" /> specifies a position that is not within this instance. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600021E RID: 542 RVA: 0x0000852C File Offset: 0x0000672C
		public int LastIndexOfAny(char[] anyOf, int startIndex, int count)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			if (startIndex < 0 || startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0 || > this.Length");
			}
			if (count < 0 || count > this.Length)
			{
				throw new ArgumentOutOfRangeException("count", "< 0 || > this.Length");
			}
			if (startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex - count + 1 < 0");
			}
			if (this.length == 0)
			{
				return -1;
			}
			return this.LastIndexOfAnyUnchecked(anyOf, startIndex, count);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000085B8 File Offset: 0x000067B8
		private unsafe int LastIndexOfAnyUnchecked(char[] anyOf, int startIndex, int count)
		{
			if (anyOf.Length == 1)
			{
				return this.LastIndexOfUnchecked(anyOf[0], startIndex, count);
			}
			fixed (char* ptr = this + RuntimeHelpers.OffsetToStringData / 2)
			{
				fixed (char* ptr2 = ref (anyOf != null && anyOf.Length != 0) ? ref anyOf[0] : ref *null)
				{
					char* ptr3 = ptr + startIndex;
					char* ptr4 = ptr3 - count;
					char* ptr5 = ptr2 + anyOf.Length;
					while (ptr3 != ptr4)
					{
						for (char* ptr6 = ptr2; ptr6 != ptr5; ptr6++)
						{
							if (*ptr6 == *ptr3)
							{
								return (int)((long)(ptr3 - ptr));
							}
						}
						ptr3--;
					}
					return -1;
				}
			}
		}

		/// <summary>Reports the index position of the last occurrence of a specified Unicode character within this instance.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not.</returns>
		/// <param name="value">A Unicode character to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000220 RID: 544 RVA: 0x00008658 File Offset: 0x00006858
		public int LastIndexOf(char value)
		{
			if (this.length == 0)
			{
				return -1;
			}
			return this.LastIndexOfUnchecked(value, this.length - 1, this.length);
		}

		/// <summary>Reports the index position of the last occurrence of a specified Unicode character within this instance. The search starts at a specified character position.</summary>
		/// <returns>The index position of <paramref name="value" /> if that character is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />.</returns>
		/// <param name="value">A Unicode character to seek. </param>
		/// <param name="startIndex">The starting position of a substring within this instance. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" /> and <paramref name="startIndex" /> is less than zero or greater than the length of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000221 RID: 545 RVA: 0x00008688 File Offset: 0x00006888
		public int LastIndexOf(char value, int startIndex)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1);
		}

		/// <summary>Reports the index position of the last occurrence of the specified Unicode character in a substring within this instance. The search starts at a specified character position and examines a specified number of character positions.</summary>
		/// <returns>The index position of <paramref name="value" /> if that character is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />.</returns>
		/// <param name="value">A Unicode character to seek. </param>
		/// <param name="startIndex">The starting position of a substring within this instance. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" /> and <paramref name="startIndex" /> is less than zero, or greater than or equal to the length of this instance.-or-<paramref name="startIndex" /> + 1 - <paramref name="count" /> is less than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000222 RID: 546 RVA: 0x00008698 File Offset: 0x00006898
		public int LastIndexOf(char value, int startIndex, int count)
		{
			if (startIndex == 0 && this.length == 0)
			{
				return -1;
			}
			if (startIndex < 0 || startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0 || >= this.Length");
			}
			if (count < 0 || count > this.Length)
			{
				throw new ArgumentOutOfRangeException("count", "< 0 || > this.Length");
			}
			if (startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex - count + 1 < 0");
			}
			return this.LastIndexOfUnchecked(value, startIndex, count);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00008720 File Offset: 0x00006920
		internal unsafe int LastIndexOfUnchecked(char value, int startIndex, int count)
		{
			char* ptr = ref this.start_char + startIndex * 2;
			char* ptr2 = ptr - (count >> 3 << 3);
			while (ptr != ptr2)
			{
				if (*ptr == value)
				{
					return (ptr - ref this.start_char / 2) / 2;
				}
				if (ptr[-1] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 1;
				}
				if (ptr[-2] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 2;
				}
				if (ptr[-3] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 3;
				}
				if (ptr[-4] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 4;
				}
				if (ptr[-5] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 5;
				}
				if (ptr[-6] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 6;
				}
				if (ptr[-7] == value)
				{
					return (ptr - ref this.start_char / 2) / 2 - 7;
				}
				ptr -= 8;
			}
			ptr2 -= (count & 7);
			while (ptr != ptr2)
			{
				if (*ptr == value)
				{
					return (ptr - ref this.start_char / 2) / 2;
				}
				ptr--;
			}
			return -1;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008824 File Offset: 0x00006A24
		internal unsafe int LastIndexOfOrdinalIgnoreCase(char value, int startIndex, int count)
		{
			if (this.length == 0)
			{
				return -1;
			}
			int num = startIndex - count;
			char c = char.ToUpperInvariant(value);
			fixed (char* ptr = &this.start_char)
			{
				for (int i = startIndex; i > num; i--)
				{
					if (char.ToUpperInvariant(ptr[i]) == c)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Reports the index position of the last occurrence of a specified <see cref="T:System.String" /> within this instance.</summary>
		/// <returns>The index position of <paramref name="value" /> if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the last index position in this instance.</returns>
		/// <param name="value">A <see cref="T:System.String" /> to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000225 RID: 549 RVA: 0x0000887C File Offset: 0x00006A7C
		public int LastIndexOf(string value)
		{
			if (this.length == 0)
			{
				return this.LastIndexOf(value, 0, 0);
			}
			return this.LastIndexOf(value, this.length - 1, this.length);
		}

		/// <summary>Reports the index position of the last occurrence of a specified <see cref="T:System.String" /> within this instance. The search starts at a specified character position.</summary>
		/// <returns>The index position of <paramref name="value" /> if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" /> and <paramref name="startIndex" /> is less than zero or specifies a position not within this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000226 RID: 550 RVA: 0x000088B4 File Offset: 0x00006AB4
		public int LastIndexOf(string value, int startIndex)
		{
			int num = startIndex;
			if (num < this.Length)
			{
				num++;
			}
			return this.LastIndexOf(value, startIndex, num);
		}

		/// <summary>Reports the index position of the last occurrence of a specified <see cref="T:System.String" /> within this instance. The search starts at a specified character position and examines a specified number of character positions.</summary>
		/// <returns>The index position of <paramref name="value" /> if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" /> and <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="startIndex" /> is greater than the length of this instance.-or-<paramref name="startIndex" /> + 1 - <paramref name="count" /> specifies a position that is not within this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000227 RID: 551 RVA: 0x000088DC File Offset: 0x00006ADC
		public int LastIndexOf(string value, int startIndex, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < -1 || startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0 || > this.Length");
			}
			if (count < 0 || count > this.Length)
			{
				throw new ArgumentOutOfRangeException("count", "< 0 || > this.Length");
			}
			if (startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex - count + 1 < 0");
			}
			if (value.Length == 0)
			{
				return startIndex;
			}
			if (startIndex == 0 && this.length == 0)
			{
				return -1;
			}
			if (this.length == 0 && value.length > 0)
			{
				return -1;
			}
			if (count == 0)
			{
				return -1;
			}
			if (startIndex == this.Length)
			{
				startIndex--;
			}
			return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count);
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.String" /> object occurs within this string.</summary>
		/// <returns>true if the <paramref name="value" /> parameter occurs within this string, or if <paramref name="value" /> is the empty string (""); otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.String" /> object to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000228 RID: 552 RVA: 0x000089BC File Offset: 0x00006BBC
		public bool Contains(string value)
		{
			return this.IndexOf(value) != -1;
		}

		/// <summary>Indicates whether the specified string is null or an <see cref="F:System.String.Empty" /> string.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is null or an empty string (""); otherwise, false.</returns>
		/// <param name="value">The string to test.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000229 RID: 553 RVA: 0x000089CC File Offset: 0x00006BCC
		public static bool IsNullOrEmpty(string value)
		{
			return value == null || value.Length == 0;
		}

		/// <summary>Returns a new string whose textual value is the same as this string, but whose binary representation is in Unicode normalization form C.</summary>
		/// <returns>A new, normalized string whose textual value is the same as this string, but whose binary representation is in normalization form C.</returns>
		/// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600022A RID: 554 RVA: 0x000089E0 File Offset: 0x00006BE0
		public string Normalize()
		{
			return Normalization.Normalize(this, 0);
		}

		/// <summary>Returns a new string whose textual value is the same as this string, but whose binary representation is in the specified Unicode normalization form.</summary>
		/// <returns>A new string whose textual value is the same as this string, but whose binary representation is in the normalization form specified by the <paramref name="normalizationForm" /> parameter.</returns>
		/// <param name="normalizationForm">A Unicode normalization form. </param>
		/// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600022B RID: 555 RVA: 0x000089EC File Offset: 0x00006BEC
		public string Normalize(NormalizationForm normalizationForm)
		{
			switch (normalizationForm)
			{
			case NormalizationForm.FormD:
				return Normalization.Normalize(this, 1);
			default:
				return Normalization.Normalize(this, 0);
			case NormalizationForm.FormKC:
				return Normalization.Normalize(this, 2);
			case NormalizationForm.FormKD:
				return Normalization.Normalize(this, 3);
			}
		}

		/// <summary>Indicates whether this string is in Unicode normalization form C.</summary>
		/// <returns>true if this string is in normalization form C; otherwise, false.</returns>
		/// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600022C RID: 556 RVA: 0x00008A3C File Offset: 0x00006C3C
		public bool IsNormalized()
		{
			return Normalization.IsNormalized(this, 0);
		}

		/// <summary>Indicates whether this string is in the specified Unicode normalization form.</summary>
		/// <returns>true if this string is in the normalization form specified by the <paramref name="normalizationForm" /> parameter; otherwise, false.</returns>
		/// <param name="normalizationForm">A Unicode normalization form. </param>
		/// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600022D RID: 557 RVA: 0x00008A48 File Offset: 0x00006C48
		public bool IsNormalized(NormalizationForm normalizationForm)
		{
			switch (normalizationForm)
			{
			case NormalizationForm.FormD:
				return Normalization.IsNormalized(this, 1);
			default:
				return Normalization.IsNormalized(this, 0);
			case NormalizationForm.FormKC:
				return Normalization.IsNormalized(this, 2);
			case NormalizationForm.FormKD:
				return Normalization.IsNormalized(this, 3);
			}
		}

		/// <summary>Deletes all the characters from this string beginning at a specified position and continuing through the last position.</summary>
		/// <returns>A new <see cref="T:System.String" /> object that is equivalent to this string less the removed characters.</returns>
		/// <param name="startIndex">The zero-based position to begin deleting characters. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero.-or- <paramref name="startIndex" /> specifies a position that is not within this string. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600022E RID: 558 RVA: 0x00008A98 File Offset: 0x00006C98
		public string Remove(int startIndex)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex can not be less than zero");
			}
			if (startIndex >= this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "StartIndex must be less than the length of the string");
			}
			return this.Remove(startIndex, this.length - startIndex);
		}

		/// <summary>Right-aligns the characters in this instance, padding with spaces on the left for a specified total length.</summary>
		/// <returns>A new <see cref="T:System.String" /> that is equivalent to this instance, but right-aligned and padded on the left with as many spaces as needed to create a length of <paramref name="totalWidth" />. Or, if <paramref name="totalWidth" /> is less than the length of this instance, a new <see cref="T:System.String" /> object that is identical to this instance.</returns>
		/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalWidth" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600022F RID: 559 RVA: 0x00008AE8 File Offset: 0x00006CE8
		public string PadLeft(int totalWidth)
		{
			return this.PadLeft(totalWidth, ' ');
		}

		/// <summary>Right-aligns the characters in this instance, padding on the left with a specified Unicode character for a specified total length.</summary>
		/// <returns>A new <see cref="T:System.String" /> that is equivalent to this instance, but right-aligned and padded on the left with as many <paramref name="paddingChar" /> characters as needed to create a length of <paramref name="totalWidth" />. Or, if <paramref name="totalWidth" /> is less than the length of this instance, a new <see cref="T:System.String" /> that is identical to this instance.</returns>
		/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param>
		/// <param name="paddingChar">A Unicode padding character. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalWidth" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000230 RID: 560 RVA: 0x00008AF4 File Offset: 0x00006CF4
		public unsafe string PadLeft(int totalWidth, char paddingChar)
		{
			if (totalWidth < 0)
			{
				throw new ArgumentOutOfRangeException("totalWidth", "< 0");
			}
			if (totalWidth < this.length)
			{
				return this;
			}
			string text = string.InternalAllocateStr(totalWidth);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = this)
					{
						fixed (char* src = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							char* ptr2 = ptr;
							char* ptr3 = ptr + (totalWidth - this.length);
							while (ptr2 != ptr3)
							{
								*(ptr2++) = paddingChar;
							}
							string.CharCopy(ptr3, src, this.length);
							text2 = null;
							text3 = null;
							return text;
						}
					}
				}
			}
		}

		/// <summary>Left-aligns the characters in this string, padding with spaces on the right, for a specified total length.</summary>
		/// <returns>A new <see cref="T:System.String" /> that is equivalent to this instance, but left-aligned and padded on the right with as many spaces as needed to create a length of <paramref name="totalWidth" />. Or, if <paramref name="totalWidth" /> is less than the length of this instance, a new <see cref="T:System.String" /> that is identical to this instance.</returns>
		/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalWidth" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000231 RID: 561 RVA: 0x00008B84 File Offset: 0x00006D84
		public string PadRight(int totalWidth)
		{
			return this.PadRight(totalWidth, ' ');
		}

		/// <summary>Left-aligns the characters in this string, padding on the right with a specified Unicode character, for a specified total length.</summary>
		/// <returns>A new <see cref="T:System.String" /> that is equivalent to this instance, but left-aligned and padded on the right with as many <paramref name="paddingChar" /> characters as needed to create a length of <paramref name="totalWidth" />. Or, if <paramref name="totalWidth" /> is less than the length of this instance, a new <see cref="T:System.String" /> that is identical to this instance.</returns>
		/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param>
		/// <param name="paddingChar">A Unicode padding character. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalWidth" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000232 RID: 562 RVA: 0x00008B90 File Offset: 0x00006D90
		public unsafe string PadRight(int totalWidth, char paddingChar)
		{
			if (totalWidth < 0)
			{
				throw new ArgumentOutOfRangeException("totalWidth", "< 0");
			}
			if (totalWidth < this.length)
			{
				return this;
			}
			if (totalWidth == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(totalWidth);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = this)
					{
						fixed (char* src = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(ptr, src, this.length);
							char* ptr2 = ptr + this.length;
							char* ptr3 = ptr + totalWidth;
							while (ptr2 != ptr3)
							{
								*(ptr2++) = paddingChar;
							}
							text2 = null;
							text3 = null;
							return text;
						}
					}
				}
			}
		}

		/// <summary>Determines whether the beginning of this instance matches the specified string.</summary>
		/// <returns>true if <paramref name="value" /> matches the beginning of this string; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to compare. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000233 RID: 563 RVA: 0x00008C2C File Offset: 0x00006E2C
		public bool StartsWith(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
		}

		/// <summary>Determines whether the beginning of this string matches the specified string when compared using the specified comparison option.</summary>
		/// <returns>true if the <paramref name="value" /> parameter matches the beginning of this string; otherwise, false.</returns>
		/// <param name="value">A <see cref="T:System.String" /> object to compare to. </param>
		/// <param name="comparisonType">One of the <see cref="T:System.StringComparison" /> values that determines how this string and <paramref name="value" /> are compared. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x06000234 RID: 564 RVA: 0x00008C54 File Offset: 0x00006E54
		[ComVisible(false)]
		public bool StartsWith(string value, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.Ordinal);
			case StringComparison.OrdinalIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.OrdinalIgnoreCase);
			default:
			{
				string text = Locale.GetText("Invalid value '{0}' for StringComparison", new object[]
				{
					comparisonType
				});
				throw new ArgumentException(text, "comparisonType");
			}
			}
		}

		/// <summary>Determines whether the end of this string matches the specified string when compared using the specified comparison option.</summary>
		/// <returns>true if the <paramref name="value" /> parameter matches the end of this string; otherwise, false.</returns>
		/// <param name="value">A <see cref="T:System.String" /> object to compare to. </param>
		/// <param name="comparisonType">One of the <see cref="T:System.StringComparison" /> values that determines how this string and <paramref name="value" /> are compared. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x06000235 RID: 565 RVA: 0x00008D38 File Offset: 0x00006F38
		[ComVisible(false)]
		public bool EndsWith(string value, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.Ordinal);
			case StringComparison.OrdinalIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.OrdinalIgnoreCase);
			default:
			{
				string text = Locale.GetText("Invalid value '{0}' for StringComparison", new object[]
				{
					comparisonType
				});
				throw new ArgumentException(text, "comparisonType");
			}
			}
		}

		/// <summary>Determines whether the beginning of this string matches the specified string when compared using the specified culture.</summary>
		/// <returns>true if the <paramref name="value" /> parameter matches the beginning of this string; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.String" /> object to compare. </param>
		/// <param name="ignoreCase">true to ignore case when comparing this string and <paramref name="value" />; otherwise, false.</param>
		/// <param name="culture">Cultural information that determines how this string and <paramref name="value" /> are compared. If <paramref name="culture" /> is null, the current culture is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000236 RID: 566 RVA: 0x00008E1C File Offset: 0x0000701C
		public bool StartsWith(string value, bool ignoreCase, CultureInfo culture)
		{
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			return culture.CompareInfo.IsPrefix(this, value, (!ignoreCase) ? CompareOptions.None : CompareOptions.IgnoreCase);
		}

		/// <summary>Returns a new string in which all occurrences of a specified Unicode character in this instance are replaced with another specified Unicode character.</summary>
		/// <returns>A <see cref="T:System.String" /> equivalent to this instance but with all instances of <paramref name="oldChar" /> replaced with <paramref name="newChar" />.</returns>
		/// <param name="oldChar">A Unicode character to be replaced. </param>
		/// <param name="newChar">A Unicode character to replace all occurrences of <paramref name="oldChar" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000237 RID: 567 RVA: 0x00008E50 File Offset: 0x00007050
		public unsafe string Replace(char oldChar, char newChar)
		{
			if (this.length == 0 || oldChar == newChar)
			{
				return this;
			}
			int num = this.IndexOfUnchecked(oldChar, 0, this.length);
			if (num == -1)
			{
				return this;
			}
			if (num < 4)
			{
				num = 0;
			}
			string text = string.InternalAllocateStr(this.length);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (char* ptr2 = &this.start_char)
					{
						if (num != 0)
						{
							string.CharCopy(ptr, ptr2, num);
						}
						char* ptr3 = ptr + this.length;
						char* ptr4 = ptr + num;
						char* ptr5 = ptr2 + num;
						while (ptr4 != ptr3)
						{
							if (*ptr5 == oldChar)
							{
								*ptr4 = newChar;
							}
							else
							{
								*ptr4 = *ptr5;
							}
							ptr5++;
							ptr4++;
						}
						text2 = null;
					}
					return text;
				}
			}
		}

		/// <summary>Returns a new string in which all occurrences of a specified string in this instance are replaced with another specified string.</summary>
		/// <returns>A <see cref="T:System.String" /> equivalent to this instance but with all instances of <paramref name="oldValue" /> replaced with <paramref name="newValue" />.</returns>
		/// <param name="oldValue">A string to be replaced. </param>
		/// <param name="newValue">A string to replace all occurrences of <paramref name="oldValue" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldValue" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="oldValue" /> is the empty string (""). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000238 RID: 568 RVA: 0x00008F18 File Offset: 0x00007118
		public string Replace(string oldValue, string newValue)
		{
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException("oldValue is the empty string.");
			}
			if (this.Length == 0)
			{
				return this;
			}
			if (newValue == null)
			{
				newValue = string.Empty;
			}
			return this.ReplaceUnchecked(oldValue, newValue);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00008F70 File Offset: 0x00007170
		private unsafe string ReplaceUnchecked(string oldValue, string newValue)
		{
			if (oldValue.length > this.length)
			{
				return this;
			}
			if (oldValue.length == 1 && newValue.length == 1)
			{
				return this.Replace(oldValue[0], newValue[0]);
			}
			int* ptr = stackalloc int[checked(200 * 4)];
			fixed (char* ptr2 = this + RuntimeHelpers.OffsetToStringData / 2)
			{
				fixed (char* src = newValue + RuntimeHelpers.OffsetToStringData / 2)
				{
					int i = 0;
					int num = 0;
					while (i < this.length)
					{
						int num2 = this.IndexOfOrdinalUnchecked(oldValue, i, this.length - i);
						if (num2 < 0)
						{
							break;
						}
						if (num >= 200)
						{
							return this.ReplaceFallback(oldValue, newValue, 200);
						}
						ptr[num++ * 4] = num2;
						i = num2 + oldValue.length;
					}
					if (num == 0)
					{
						return this;
					}
					int num3 = this.length + (newValue.length - oldValue.length) * num;
					string text = string.InternalAllocateStr(num3);
					int num4 = 0;
					int num5 = 0;
					fixed (string text2 = text)
					{
						fixed (char* ptr3 = text2 + RuntimeHelpers.OffsetToStringData / 2)
						{
							for (int j = 0; j < num; j++)
							{
								int num6 = ptr[j] - num5;
								string.CharCopy(ptr3 + num4, ptr2 + num5, num6);
								num4 += num6;
								num5 = ptr[j] + oldValue.length;
								string.CharCopy(ptr3 + num4, src, newValue.length);
								num4 += newValue.length;
							}
							string.CharCopy(ptr3 + num4, ptr2 + num5, this.length - num5);
							text2 = null;
							return text;
						}
					}
				}
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009120 File Offset: 0x00007320
		private string ReplaceFallback(string oldValue, string newValue, int testedCount)
		{
			int capacity = this.length + (newValue.length - oldValue.length) * testedCount;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			int num;
			for (int i = 0; i < this.length; i = num + oldValue.Length)
			{
				num = this.IndexOfOrdinalUnchecked(oldValue, i, this.length - i);
				if (num < 0)
				{
					stringBuilder.Append(this.SubstringUnchecked(i, this.length - i));
					break;
				}
				stringBuilder.Append(this.SubstringUnchecked(i, num - i));
				stringBuilder.Append(newValue);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Deletes a specified number of characters from this instance beginning at a specified position.</summary>
		/// <returns>A new <see cref="T:System.String" /> that is equivalent to this instance less <paramref name="count" /> number of characters.</returns>
		/// <param name="startIndex">The zero-based position to begin deleting characters. </param>
		/// <param name="count">The number of characters to delete. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Either <paramref name="startIndex" /> or <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> plus <paramref name="count" /> specify a position outside this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600023B RID: 571 RVA: 0x000091BC File Offset: 0x000073BC
		public unsafe string Remove(int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative.");
			}
			if (startIndex > this.length - count)
			{
				throw new ArgumentOutOfRangeException("count", "startIndex + count > this.length");
			}
			string text = string.InternalAllocateStr(this.length - count);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = this)
					{
						fixed (char* ptr2 = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							char* ptr3 = ptr;
							string.CharCopy(ptr3, ptr2, startIndex);
							int num = startIndex + count;
							ptr3 += startIndex;
							string.CharCopy(ptr3, ptr2 + num, this.length - num);
							text2 = null;
							text3 = null;
							return text;
						}
					}
				}
			}
		}

		/// <summary>Returns a copy of this <see cref="T:System.String" /> converted to lowercase, using the casing rules of the current culture.</summary>
		/// <returns>A <see cref="T:System.String" /> in lowercase.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600023C RID: 572 RVA: 0x00009270 File Offset: 0x00007470
		public string ToLower()
		{
			return this.ToLower(CultureInfo.CurrentCulture);
		}

		/// <summary>Returns a copy of this <see cref="T:System.String" /> converted to lowercase, using the casing rules of the specified culture.</summary>
		/// <returns>A <see cref="T:System.String" /> in lowercase.</returns>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that supplies culture-specific casing rules. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600023D RID: 573 RVA: 0x00009280 File Offset: 0x00007480
		public string ToLower(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (culture.LCID == 127)
			{
				return this.ToLowerInvariant();
			}
			return culture.TextInfo.ToLower(this);
		}

		/// <summary>Returns a copy of this <see cref="T:System.String" /> object converted to lowercase using the casing rules of the invariant culture.</summary>
		/// <returns>A <see cref="T:System.String" /> object in lowercase.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600023E RID: 574 RVA: 0x000092C0 File Offset: 0x000074C0
		public unsafe string ToLowerInvariant()
		{
			if (this.length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(this.length);
			fixed (char* ptr = &this.start_char)
			{
				fixed (string text2 = text)
				{
					fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						char* ptr3 = ptr2;
						char* ptr4 = ptr;
						for (int i = 0; i < this.length; i++)
						{
							*ptr3 = char.ToLowerInvariant(*ptr4);
							ptr4++;
							ptr3++;
						}
						ptr = null;
						text2 = null;
						return text;
					}
				}
			}
		}

		/// <summary>Returns a copy of this <see cref="T:System.String" /> converted to uppercase, using the casing rules of the current culture.</summary>
		/// <returns>A <see cref="T:System.String" /> in uppercase.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600023F RID: 575 RVA: 0x0000933C File Offset: 0x0000753C
		public string ToUpper()
		{
			return this.ToUpper(CultureInfo.CurrentCulture);
		}

		/// <summary>Returns a copy of this <see cref="T:System.String" /> converted to uppercase, using the casing rules of the specified culture.</summary>
		/// <returns>A <see cref="T:System.String" /> in uppercase.</returns>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that supplies culture-specific casing rules. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000240 RID: 576 RVA: 0x0000934C File Offset: 0x0000754C
		public string ToUpper(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (culture.LCID == 127)
			{
				return this.ToUpperInvariant();
			}
			return culture.TextInfo.ToUpper(this);
		}

		/// <summary>Returns a copy of this <see cref="T:System.String" /> object converted to uppercase using the casing rules of the invariant culture.</summary>
		/// <returns>A <see cref="T:System.String" /> object in uppercase.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000241 RID: 577 RVA: 0x0000938C File Offset: 0x0000758C
		public unsafe string ToUpperInvariant()
		{
			if (this.length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(this.length);
			fixed (char* ptr = &this.start_char)
			{
				fixed (string text2 = text)
				{
					fixed (char* ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						char* ptr3 = ptr2;
						char* ptr4 = ptr;
						for (int i = 0; i < this.length; i++)
						{
							*ptr3 = char.ToUpperInvariant(*ptr4);
							ptr4++;
							ptr3++;
						}
						ptr = null;
						text2 = null;
						return text;
					}
				}
			}
		}

		/// <summary>Returns this instance of <see cref="T:System.String" />; no actual conversion is performed.</summary>
		/// <returns>This <see cref="T:System.String" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000242 RID: 578 RVA: 0x00009408 File Offset: 0x00007608
		public override string ToString()
		{
			return this;
		}

		/// <summary>Returns this instance of <see cref="T:System.String" />; no actual conversion is performed.</summary>
		/// <returns>This <see cref="T:System.String" />.</returns>
		/// <param name="provider">(Reserved) An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000243 RID: 579 RVA: 0x0000940C File Offset: 0x0000760C
		public string ToString(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>Replaces one or more format items in a specified string with the string representation of a specified object.</summary>
		/// <returns>A copy of <paramref name="format" /> in which any format items are replaced by the string representation of <paramref name="arg0" />.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">An object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format item in <paramref name="format" /> is invalid.-or- The index of a format item is greater or less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000244 RID: 580 RVA: 0x00009410 File Offset: 0x00007610
		public static string Format(string format, object arg0)
		{
			return string.Format(null, format, new object[]
			{
				arg0
			});
		}

		/// <summary>Replaces the format items in a specified string with the string representations of two specified objects.</summary>
		/// <returns>A copy of <paramref name="format" /> in which format items have been replaced by the string equivalents of <paramref name="arg0" /> and <paramref name="arg1" />.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to format. </param>
		/// <param name="arg1">The second object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The index of a format item is less than zero, or greater than one. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000245 RID: 581 RVA: 0x00009424 File Offset: 0x00007624
		public static string Format(string format, object arg0, object arg1)
		{
			return string.Format(null, format, new object[]
			{
				arg0,
				arg1
			});
		}

		/// <summary>Replaces the format items in a specified string with the string representation of three specified objects.</summary>
		/// <returns>A copy of <paramref name="format" /> in which the format items have been replaced by the string representations of <paramref name="arg0" />, <paramref name="arg1" />, and <paramref name="arg2" />.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to format. </param>
		/// <param name="arg1">The second object to format. </param>
		/// <param name="arg2">The third object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The index of a format item is less than zero, or greater than two. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000246 RID: 582 RVA: 0x0000943C File Offset: 0x0000763C
		public static string Format(string format, object arg0, object arg1, object arg2)
		{
			return string.Format(null, format, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		/// <summary>Replaces the format item in a specified string with the string representation of a corresponding object in a specified array.</summary>
		/// <returns>A copy of <paramref name="format" /> in which the format items have been replaced by the string representation of the corresponding objects in <paramref name="args" />.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="args">An object array that contains zero or more objects to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args" /> array. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000247 RID: 583 RVA: 0x00009458 File Offset: 0x00007658
		public static string Format(string format, params object[] args)
		{
			return string.Format(null, format, args);
		}

		/// <summary>Replaces the format item in a specified string with the string representation of a corresponding object in a specified array. A specified parameter supplies culture-specific formatting information.</summary>
		/// <returns>A copy of <paramref name="format" /> in which the format items have been replaced by the string representation of the corresponding objects in <paramref name="args" />.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies culture-specific formatting information. </param>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="args">An <see cref="T:System.Object" /> array containing zero or more objects to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args" /> array. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000248 RID: 584 RVA: 0x00009464 File Offset: 0x00007664
		public static string Format(IFormatProvider provider, string format, params object[] args)
		{
			StringBuilder stringBuilder = string.FormatHelper(null, provider, format, args);
			return stringBuilder.ToString();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009484 File Offset: 0x00007684
		internal static StringBuilder FormatHelper(StringBuilder result, IFormatProvider provider, string format, params object[] args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (result == null)
			{
				int num = 0;
				int i;
				for (i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text == null)
					{
						break;
					}
					num += text.length;
				}
				if (i == args.Length)
				{
					result = new StringBuilder(num + format.length);
				}
				else
				{
					result = new StringBuilder();
				}
			}
			int j = 0;
			int num2 = j;
			while (j < format.length)
			{
				char c = format[j++];
				if (c == '{')
				{
					result.Append(format, num2, j - num2 - 1);
					if (format[j] == '{')
					{
						num2 = j++;
					}
					else
					{
						int num3;
						int num4;
						bool flag;
						string format2;
						string.ParseFormatSpecifier(format, ref j, out num3, out num4, out flag, out format2);
						if (num3 >= args.Length)
						{
							throw new FormatException("Index (zero based) must be greater than or equal to zero and less than the size of the argument list.");
						}
						object obj = args[num3];
						ICustomFormatter customFormatter = null;
						if (provider != null)
						{
							customFormatter = (provider.GetFormat(typeof(ICustomFormatter)) as ICustomFormatter);
						}
						string text2;
						if (obj == null)
						{
							text2 = string.Empty;
						}
						else if (customFormatter != null)
						{
							text2 = customFormatter.Format(format2, obj, provider);
						}
						else if (obj is IFormattable)
						{
							text2 = ((IFormattable)obj).ToString(format2, provider);
						}
						else
						{
							text2 = obj.ToString();
						}
						if (num4 > text2.length)
						{
							int repeatCount = num4 - text2.length;
							if (flag)
							{
								result.Append(text2);
								result.Append(' ', repeatCount);
							}
							else
							{
								result.Append(' ', repeatCount);
								result.Append(text2);
							}
						}
						else
						{
							result.Append(text2);
						}
						num2 = j;
					}
				}
				else if (c == '}' && j < format.length && format[j] == '}')
				{
					result.Append(format, num2, j - num2 - 1);
					num2 = j++;
				}
				else if (c == '}')
				{
					throw new FormatException("Input string was not in a correct format.");
				}
			}
			if (num2 < format.length)
			{
				result.Append(format, num2, format.Length - num2);
			}
			return result;
		}

		/// <summary>Creates a new instance of <see cref="T:System.String" /> with the same value as a specified <see cref="T:System.String" />.</summary>
		/// <returns>A new <see cref="T:System.String" /> with the same value as <paramref name="str" />.</returns>
		/// <param name="str">The <see cref="T:System.String" /> to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600024A RID: 586 RVA: 0x000096E8 File Offset: 0x000078E8
		public unsafe static string Copy(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int num = str.length;
			string text = string.InternalAllocateStr(num);
			if (num != 0)
			{
				fixed (string text2 = text)
				{
					fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text3 = str, src = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(dest, src, num);
							text2 = null;
						}
					}
				}
			}
			return text;
		}

		/// <summary>Creates the string representation of a specified object.</summary>
		/// <returns>The string representation of the value of <paramref name="arg0" />, or <see cref="F:System.String.Empty" /> if <paramref name="arg0" /> is null.</returns>
		/// <param name="arg0">The object to represent, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600024B RID: 587 RVA: 0x00009744 File Offset: 0x00007944
		public static string Concat(object arg0)
		{
			if (arg0 == null)
			{
				return string.Empty;
			}
			return arg0.ToString();
		}

		/// <summary>Concatenates the string representations of two specified objects.</summary>
		/// <returns>The concatenated string representations of the values of <paramref name="arg0" /> and <paramref name="arg1" />.</returns>
		/// <param name="arg0">The first object to concatenate. </param>
		/// <param name="arg1">The second object to concatenate.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600024C RID: 588 RVA: 0x00009758 File Offset: 0x00007958
		public static string Concat(object arg0, object arg1)
		{
			return ((arg0 == null) ? null : arg0.ToString()) + ((arg1 == null) ? null : arg1.ToString());
		}

		/// <summary>Concatenates the string representations of three specified objects.</summary>
		/// <returns>The concatenated string representations of the values of <paramref name="arg0" />, <paramref name="arg1" />, and <paramref name="arg2" />.</returns>
		/// <param name="arg0">The first object to concatenate. </param>
		/// <param name="arg1">The second object to concatenate. </param>
		/// <param name="arg2">The third object to concatenate.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600024D RID: 589 RVA: 0x00009790 File Offset: 0x00007990
		public static string Concat(object arg0, object arg1, object arg2)
		{
			string str;
			if (arg0 == null)
			{
				str = string.Empty;
			}
			else
			{
				str = arg0.ToString();
			}
			string str2;
			if (arg1 == null)
			{
				str2 = string.Empty;
			}
			else
			{
				str2 = arg1.ToString();
			}
			string str3;
			if (arg2 == null)
			{
				str3 = string.Empty;
			}
			else
			{
				str3 = arg2.ToString();
			}
			return str + str2 + str3;
		}

		/// <summary>Concatenates the string representations of four specified objects and any objects specified in an optional variable length parameter list.</summary>
		/// <returns>The concatenated string representation of each value in the parameter list.</returns>
		/// <param name="arg0">The first object to concatenate. </param>
		/// <param name="arg1">The second object to concatenate. </param>
		/// <param name="arg2">The third object to concatenate. </param>
		/// <param name="arg3">The fourth object to concatenate.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600024E RID: 590 RVA: 0x000097F0 File Offset: 0x000079F0
		[CLSCompliant(false)]
		public static string Concat(object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			string str;
			if (arg0 == null)
			{
				str = string.Empty;
			}
			else
			{
				str = arg0.ToString();
			}
			string str2;
			if (arg1 == null)
			{
				str2 = string.Empty;
			}
			else
			{
				str2 = arg1.ToString();
			}
			string str3;
			if (arg2 == null)
			{
				str3 = string.Empty;
			}
			else
			{
				str3 = arg2.ToString();
			}
			ArgIterator argIterator = new ArgIterator(__arglist);
			int remainingCount = argIterator.GetRemainingCount();
			StringBuilder stringBuilder = new StringBuilder();
			if (arg3 != null)
			{
				stringBuilder.Append(arg3.ToString());
			}
			for (int i = 0; i < remainingCount; i++)
			{
				TypedReference nextArg = argIterator.GetNextArg();
				stringBuilder.Append(TypedReference.ToObject(nextArg));
			}
			string str4 = stringBuilder.ToString();
			return str + str2 + str3 + str4;
		}

		/// <summary>Concatenates two specified instances of <see cref="T:System.String" />.</summary>
		/// <returns>The concatenation of <paramref name="str0" /> and <paramref name="str1" />.</returns>
		/// <param name="str0">The first string to concatenate. </param>
		/// <param name="str1">The second string to concatenate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600024F RID: 591 RVA: 0x000098B4 File Offset: 0x00007AB4
		public unsafe static string Concat(string str0, string str1)
		{
			if (str0 == null || str0.Length == 0)
			{
				if (str1 == null || str1.Length == 0)
				{
					return string.Empty;
				}
				return str1;
			}
			else
			{
				if (str1 == null || str1.Length == 0)
				{
					return str0;
				}
				string text = string.InternalAllocateStr(str0.length + str1.length);
				fixed (string text2 = text)
				{
					fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text3 = str0)
						{
							fixed (char* src = text3 + RuntimeHelpers.OffsetToStringData / 2)
							{
								string.CharCopy(dest, src, str0.length);
								text2 = null;
								text3 = null;
								fixed (string text4 = text)
								{
									fixed (char* ptr = text4 + RuntimeHelpers.OffsetToStringData / 2)
									{
										fixed (string text5 = str1)
										{
											fixed (char* src2 = text5 + RuntimeHelpers.OffsetToStringData / 2)
											{
												string.CharCopy(ptr + str0.Length, src2, str1.length);
												text4 = null;
												text5 = null;
												return text;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		/// <summary>Concatenates three specified instances of <see cref="T:System.String" />.</summary>
		/// <returns>The concatenation of <paramref name="str0" />, <paramref name="str1" />, and <paramref name="str2" />.</returns>
		/// <param name="str0">The first <see cref="T:System.String" />. </param>
		/// <param name="str1">The second <see cref="T:System.String" />. </param>
		/// <param name="str2">The third <see cref="T:System.String" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000250 RID: 592 RVA: 0x00009978 File Offset: 0x00007B78
		public unsafe static string Concat(string str0, string str1, string str2)
		{
			if (str0 == null || str0.Length == 0)
			{
				if (str1 == null || str1.Length == 0)
				{
					if (str2 == null || str2.Length == 0)
					{
						return string.Empty;
					}
					return str2;
				}
				else
				{
					if (str2 == null || str2.Length == 0)
					{
						return str1;
					}
					str0 = string.Empty;
				}
			}
			else if (str1 == null || str1.Length == 0)
			{
				if (str2 == null || str2.Length == 0)
				{
					return str0;
				}
				str1 = string.Empty;
			}
			else if (str2 == null || str2.Length == 0)
			{
				str2 = string.Empty;
			}
			string text = string.InternalAllocateStr(str0.length + str1.length + str2.length);
			if (str0.Length != 0)
			{
				fixed (string text2 = text)
				{
					fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text3 = str0, src = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(dest, src, str0.length);
							text2 = null;
						}
					}
				}
			}
			if (str1.Length != 0)
			{
				fixed (string text4 = text)
				{
					fixed (char* ptr = text4 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text5 = str1, src2 = text5 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(ptr + str0.Length, src2, str1.length);
							text4 = null;
						}
					}
				}
			}
			if (str2.Length != 0)
			{
				fixed (string text6 = text)
				{
					fixed (char* ptr2 = text6 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text7 = str2, src3 = text7 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(ptr2 + str0.Length + str1.Length, src3, str2.length);
							text6 = null;
						}
					}
				}
			}
			return text;
		}

		/// <summary>Concatenates four specified instances of <see cref="T:System.String" />.</summary>
		/// <returns>The concatenation of <paramref name="str0" />, <paramref name="str1" />, <paramref name="str2" />, and <paramref name="str3" />.</returns>
		/// <param name="str0">The first string to concatenate.</param>
		/// <param name="str1">The second string to concatenate.</param>
		/// <param name="str2">The third string to concatenate.</param>
		/// <param name="str3">The fourth string to concatenate.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000251 RID: 593 RVA: 0x00009B0C File Offset: 0x00007D0C
		public unsafe static string Concat(string str0, string str1, string str2, string str3)
		{
			if (str0 == null && str1 == null && str2 == null && str3 == null)
			{
				return string.Empty;
			}
			if (str0 == null)
			{
				str0 = string.Empty;
			}
			if (str1 == null)
			{
				str1 = string.Empty;
			}
			if (str2 == null)
			{
				str2 = string.Empty;
			}
			if (str3 == null)
			{
				str3 = string.Empty;
			}
			string text = string.InternalAllocateStr(str0.length + str1.length + str2.length + str3.length);
			if (str0.Length != 0)
			{
				fixed (string text2 = text)
				{
					fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text3 = str0, src = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(dest, src, str0.length);
							text2 = null;
						}
					}
				}
			}
			if (str1.Length != 0)
			{
				fixed (string text4 = text)
				{
					fixed (char* ptr = text4 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text5 = str1, src2 = text5 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(ptr + str0.Length, src2, str1.length);
							text4 = null;
						}
					}
				}
			}
			if (str2.Length != 0)
			{
				fixed (string text6 = text)
				{
					fixed (char* ptr2 = text6 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text7 = str2, src3 = text7 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(ptr2 + str0.Length + str1.Length, src3, str2.length);
							text6 = null;
						}
					}
				}
			}
			if (str3.Length != 0)
			{
				fixed (string text8 = text)
				{
					fixed (char* ptr3 = text8 + RuntimeHelpers.OffsetToStringData / 2)
					{
						fixed (string text9 = str3, src4 = text9 + RuntimeHelpers.OffsetToStringData / 2)
						{
							string.CharCopy(ptr3 + str0.Length + str1.Length + str2.Length, src4, str3.length);
							text8 = null;
						}
					}
				}
			}
			return text;
		}

		/// <summary>Concatenates the string representations of the elements in a specified <see cref="T:System.Object" /> array.</summary>
		/// <returns>The concatenated string representations of the values of the elements in <paramref name="args" />.</returns>
		/// <param name="args">An object array that contains the elements to concatenate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000252 RID: 594 RVA: 0x00009CB0 File Offset: 0x00007EB0
		public static string Concat(params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			int num = args.Length;
			if (num == 0)
			{
				return string.Empty;
			}
			string[] array = new string[num];
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if (args[i] != null)
				{
					array[i] = args[i].ToString();
					num2 += array[i].length;
				}
			}
			return string.ConcatInternal(array, num2);
		}

		/// <summary>Concatenates the elements of a specified <see cref="T:System.String" /> array.</summary>
		/// <returns>The concatenated elements of <paramref name="values" />.</returns>
		/// <param name="values">An array of string instances. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000253 RID: 595 RVA: 0x00009D20 File Offset: 0x00007F20
		public static string Concat(params string[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int num = 0;
			foreach (string text in values)
			{
				if (text != null)
				{
					num += text.length;
				}
			}
			return string.ConcatInternal(values, num);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009D70 File Offset: 0x00007F70
		private unsafe static string ConcatInternal(string[] values, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(length);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					int num = 0;
					foreach (string text3 in values)
					{
						if (text3 != null)
						{
							fixed (string text4 = text3)
							{
								fixed (char* src = text4 + RuntimeHelpers.OffsetToStringData / 2)
								{
									string.CharCopy(ptr + num, src, text3.length);
									text4 = null;
									num += text3.Length;
								}
							}
						}
					}
					text2 = null;
					return text;
				}
			}
		}

		/// <summary>Inserts a specified instance of <see cref="T:System.String" /> at a specified index position in this instance.</summary>
		/// <returns>A new <see cref="T:System.String" /> equivalent to this instance but with <paramref name="value" /> inserted at position <paramref name="startIndex" />.</returns>
		/// <param name="startIndex">The index position of the insertion. </param>
		/// <param name="value">The <see cref="T:System.String" /> to insert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is negative or greater than the length of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000255 RID: 597 RVA: 0x00009DF4 File Offset: 0x00007FF4
		public unsafe string Insert(int startIndex, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0 || startIndex > this.length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative and must be less than or equal to length of string.");
			}
			if (value.Length == 0)
			{
				return this;
			}
			if (this.Length == 0)
			{
				return value;
			}
			string text = string.InternalAllocateStr(this.length + value.length);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = this)
					{
						fixed (char* ptr2 = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							fixed (string text4 = value)
							{
								fixed (char* src = text4 + RuntimeHelpers.OffsetToStringData / 2)
								{
									char* ptr3 = ptr;
									string.CharCopy(ptr3, ptr2, startIndex);
									ptr3 += startIndex;
									string.CharCopy(ptr3, src, value.length);
									ptr3 += value.length;
									string.CharCopy(ptr3, ptr2 + startIndex, this.length - startIndex);
									text2 = null;
									text3 = null;
									text4 = null;
									return text;
								}
							}
						}
					}
				}
			}
		}

		/// <summary>Retrieves the system's reference to the specified <see cref="T:System.String" />.</summary>
		/// <returns>If the value of <paramref name="str" /> is already interned, the system's reference is returned; otherwise, a new reference to a string with the value of <paramref name="str" /> is returned.</returns>
		/// <param name="str">A <see cref="T:System.String" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000256 RID: 598 RVA: 0x00009ED8 File Offset: 0x000080D8
		public static string Intern(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return string.InternalIntern(str);
		}

		/// <summary>Retrieves a reference to a specified <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> reference to <paramref name="str" /> if it is in the common language runtime "intern pool"; otherwise null.</returns>
		/// <param name="str">A <see cref="T:System.String" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000257 RID: 599 RVA: 0x00009EF4 File Offset: 0x000080F4
		public static string IsInterned(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return string.InternalIsInterned(str);
		}

		/// <summary>Concatenates a specified separator <see cref="T:System.String" /> between each element of a specified <see cref="T:System.String" /> array, yielding a single concatenated string.</summary>
		/// <returns>A <see cref="T:System.String" /> consisting of the elements of <paramref name="value" /> interspersed with the <paramref name="separator" /> string.</returns>
		/// <param name="separator">A <see cref="T:System.String" />. </param>
		/// <param name="value">An array of <see cref="T:System.String" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000258 RID: 600 RVA: 0x00009F10 File Offset: 0x00008110
		public static string Join(string separator, string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			return string.JoinUnchecked(separator, value, 0, value.Length);
		}

		/// <summary>Concatenates a specified separator <see cref="T:System.String" /> between each element of a specified <see cref="T:System.String" /> array, yielding a single concatenated string. Parameters specify the first array element and number of elements to use.</summary>
		/// <returns>A <see cref="T:System.String" /> object consisting of the strings in <paramref name="value" /> joined by <paramref name="separator" />. Or, <see cref="F:System.String.Empty" /> if <paramref name="count" /> is zero, <paramref name="value" /> has no elements, or <paramref name="separator" /> and all the elements of <paramref name="value" /> are <see cref="F:System.String.Empty" />.</returns>
		/// <param name="separator">A <see cref="T:System.String" />. </param>
		/// <param name="value">An array of <see cref="T:System.String" />. </param>
		/// <param name="startIndex">The first array element in <paramref name="value" /> to use. </param>
		/// <param name="count">The number of elements of <paramref name="value" /> to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="count" /> is less than 0.-or- <paramref name="startIndex" /> plus <paramref name="count" /> is greater than the number of elements in <paramref name="value" />. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000259 RID: 601 RVA: 0x00009F3C File Offset: 0x0000813C
		public static string Join(string separator, string[] value, int startIndex, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (startIndex > value.Length - count)
			{
				throw new ArgumentOutOfRangeException("startIndex", "startIndex + count > value.length");
			}
			if (startIndex == value.Length)
			{
				return string.Empty;
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			return string.JoinUnchecked(separator, value, startIndex, count);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009FC8 File Offset: 0x000081C8
		private unsafe static string JoinUnchecked(string separator, string[] value, int startIndex, int count)
		{
			int num = 0;
			int num2 = startIndex + count;
			for (int i = startIndex; i < num2; i++)
			{
				string text = value[i];
				if (text != null)
				{
					num += text.length;
				}
			}
			num += separator.length * (count - 1);
			if (num <= 0)
			{
				return string.Empty;
			}
			string text2 = string.InternalAllocateStr(num);
			num2--;
			fixed (string text3 = text2)
			{
				fixed (char* ptr = text3 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text4 = separator)
					{
						fixed (char* src = text4 + RuntimeHelpers.OffsetToStringData / 2)
						{
							int num3 = 0;
							for (int j = startIndex; j < num2; j++)
							{
								string text5 = value[j];
								if (text5 != null && text5.Length > 0)
								{
									fixed (string text6 = text5)
									{
										fixed (char* src2 = text6 + RuntimeHelpers.OffsetToStringData / 2)
										{
											string.CharCopy(ptr + num3, src2, text5.Length);
											text6 = null;
											num3 += text5.Length;
										}
									}
								}
								if (separator.Length > 0)
								{
									string.CharCopy(ptr + num3, src, separator.Length);
									num3 += separator.Length;
								}
							}
							string text7 = value[num2];
							if (text7 != null && text7.Length > 0)
							{
								fixed (string text8 = text7, src3 = text8 + RuntimeHelpers.OffsetToStringData / 2)
								{
									string.CharCopy(ptr + num3, src3, text7.Length);
								}
							}
							text3 = null;
							text4 = null;
							return text2;
						}
					}
				}
			}
		}

		/// <summary>Gets the number of characters in the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The number of characters in this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000A11C File Offset: 0x0000831C
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		/// <summary>Retrieves an object that can iterate through the individual characters in this string.</summary>
		/// <returns>A <see cref="T:System.CharEnumerator" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600025C RID: 604 RVA: 0x0000A124 File Offset: 0x00008324
		public CharEnumerator GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A12C File Offset: 0x0000832C
		private static void ParseFormatSpecifier(string str, ref int ptr, out int n, out int width, out bool left_align, out string format)
		{
			try
			{
				n = string.ParseDecimal(str, ref ptr);
				if (n < 0)
				{
					throw new FormatException("Input string was not in a correct format.");
				}
				if (str[ptr] == ',')
				{
					ptr++;
					while (char.IsWhiteSpace(str[ptr]))
					{
						ptr++;
					}
					int num = ptr;
					format = str.Substring(num, ptr - num);
					left_align = (str[ptr] == '-');
					if (left_align)
					{
						ptr++;
					}
					width = string.ParseDecimal(str, ref ptr);
					if (width < 0)
					{
						throw new FormatException("Input string was not in a correct format.");
					}
				}
				else
				{
					width = 0;
					left_align = false;
					format = string.Empty;
				}
				if (str[ptr] == ':')
				{
					int num2 = ++ptr;
					while (str[ptr] != '}')
					{
						ptr++;
					}
					format += str.Substring(num2, ptr - num2);
				}
				else
				{
					format = null;
				}
				if (str[ptr++] != '}')
				{
					throw new FormatException("Input string was not in a correct format.");
				}
			}
			catch (IndexOutOfRangeException)
			{
				throw new FormatException("Input string was not in a correct format.");
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A28C File Offset: 0x0000848C
		private static int ParseDecimal(string str, ref int ptr)
		{
			int num = ptr;
			int num2 = 0;
			for (;;)
			{
				char c = str[num];
				if (c < '0' || '9' < c)
				{
					break;
				}
				num2 = num2 * 10 + (int)c - 48;
				num++;
			}
			if (num == ptr)
			{
				return -1;
			}
			ptr = num;
			return num2;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A2DC File Offset: 0x000084DC
		internal unsafe void InternalSetChar(int idx, char val)
		{
			if (idx >= this.Length)
			{
				throw new ArgumentOutOfRangeException("idx");
			}
			fixed (char* ptr = &this.start_char)
			{
				ptr[idx] = val;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A314 File Offset: 0x00008514
		internal unsafe void InternalSetLength(int newLength)
		{
			if (newLength > this.length)
			{
				throw new ArgumentOutOfRangeException("newLength", "newLength as to be <= length");
			}
			fixed (char* ptr = &this.start_char)
			{
				char* ptr2 = ptr + newLength;
				char* ptr3 = ptr + this.length;
				while (ptr2 < ptr3)
				{
					*ptr2 = '\0';
					ptr2++;
				}
			}
			this.length = newLength;
		}

		/// <summary>Returns the hash code for this string.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000261 RID: 609 RVA: 0x0000A374 File Offset: 0x00008574
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe override int GetHashCode()
		{
			fixed (char* ptr = this + RuntimeHelpers.OffsetToStringData / 2)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + this.length - 1;
				int num = 0;
				while (ptr2 < ptr3)
				{
					num = (num << 5) - num + (int)(*ptr2);
					num = (num << 5) - num + (int)ptr2[1];
					ptr2 += 2;
				}
				ptr3++;
				if (ptr2 < ptr3)
				{
					num = (num << 5) - num + (int)(*ptr2);
				}
				return num;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A3DC File Offset: 0x000085DC
		internal unsafe int GetCaseInsensitiveHashCode()
		{
			fixed (char* ptr = this + RuntimeHelpers.OffsetToStringData / 2)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + this.length - 1;
				int num = 0;
				while (ptr2 < ptr3)
				{
					num = (num << 5) - num + (int)char.ToUpperInvariant(*ptr2);
					num = (num << 5) - num + (int)char.ToUpperInvariant(ptr2[1]);
					ptr2 += 2;
				}
				ptr3++;
				if (ptr2 < ptr3)
				{
					num = (num << 5) - num + (int)char.ToUpperInvariant(*ptr2);
				}
				return num;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A450 File Offset: 0x00008650
		private unsafe string CreateString(sbyte* value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			byte* ptr = (byte*)value;
			int num = 0;
			try
			{
				while (*(ptr++) != 0)
				{
					num++;
				}
			}
			catch (NullReferenceException)
			{
				throw new ArgumentOutOfRangeException("ptr", "Value does not refer to a valid string.");
			}
			catch (AccessViolationException)
			{
				throw new ArgumentOutOfRangeException("ptr", "Value does not refer to a valid string.");
			}
			return this.CreateString(value, 0, num, null);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A4F4 File Offset: 0x000086F4
		private unsafe string CreateString(sbyte* value, int startIndex, int length)
		{
			return this.CreateString(value, startIndex, length, null);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A500 File Offset: 0x00008700
		private unsafe string CreateString(sbyte* value, int startIndex, int length, Encoding enc)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Non-negative number required.");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Non-negative number required.");
			}
			if (value + startIndex < value)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Value, startIndex and length do not refer to a valid string.");
			}
			bool flag;
			if (flag = (enc == null))
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (length == 0)
				{
					return string.Empty;
				}
				enc = Encoding.Default;
			}
			byte[] array = new byte[length];
			if (length != 0)
			{
				fixed (byte* dest = ref (array != null && array.Length != 0) ? ref array[0] : ref *null)
				{
					try
					{
						string.memcpy(dest, (byte*)(value + startIndex), length);
					}
					catch (NullReferenceException)
					{
						throw new ArgumentOutOfRangeException("ptr", "Value, startIndex and length do not refer to a valid string.");
					}
					catch (AccessViolationException)
					{
						if (!flag)
						{
							throw;
						}
						throw new ArgumentOutOfRangeException("value", "Value, startIndex and length do not refer to a valid string.");
					}
				}
			}
			return enc.GetString(array);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A630 File Offset: 0x00008830
		private unsafe string CreateString(char* value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			char* ptr = value;
			int num = 0;
			while (*ptr != '\0')
			{
				num++;
				ptr++;
			}
			string text = string.InternalAllocateStr(num);
			if (num != 0)
			{
				fixed (string text2 = text, dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					string.CharCopy(dest, value, num);
				}
			}
			return text;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A688 File Offset: 0x00008888
		private unsafe string CreateString(char* value, int startIndex, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			string text = string.InternalAllocateStr(length);
			fixed (string text2 = text)
			{
				fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					string.CharCopy(dest, value + startIndex, length);
					text2 = null;
					return text;
				}
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A6F8 File Offset: 0x000088F8
		private unsafe string CreateString(char[] val, int startIndex, int length)
		{
			if (val == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative.");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Cannot be negative.");
			}
			if (startIndex > val.Length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative, and should be less than length of string.");
			}
			if (length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(length);
			fixed (string text2 = text)
			{
				fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (char* ptr = ref (val != null && val.Length != 0) ? ref val[0] : ref *null)
					{
						string.CharCopy(dest, ptr + startIndex, length);
						text2 = null;
					}
					return text;
				}
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000A7AC File Offset: 0x000089AC
		private unsafe string CreateString(char[] val)
		{
			if (val == null)
			{
				return string.Empty;
			}
			if (val.Length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(val.Length);
			fixed (string text2 = text)
			{
				fixed (char* dest = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (char* src = ref (val != null && val.Length != 0) ? ref val[0] : ref *null)
					{
						string.CharCopy(dest, src, val.Length);
						text2 = null;
					}
					return text;
				}
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A814 File Offset: 0x00008A14
		private unsafe string CreateString(char c, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(count);
			fixed (string text2 = text)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					char* ptr2 = ptr;
					char* ptr3 = ptr2 + count;
					while (ptr2 < ptr3)
					{
						*ptr2 = c;
						ptr2++;
					}
					text2 = null;
					return text;
				}
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A874 File Offset: 0x00008A74
		internal unsafe static void memset(byte* dest, int val, int len)
		{
			if (len < 8)
			{
				while (len != 0)
				{
					*dest = (byte)val;
					dest++;
					len--;
				}
				return;
			}
			if (val != 0)
			{
				val |= val << 8;
				val |= val << 16;
			}
			int num = dest & 3;
			if (num != 0)
			{
				num = 4 - num;
				len -= num;
				do
				{
					*dest = (byte)val;
					dest++;
					num--;
				}
				while (num != 0);
			}
			while (len >= 16)
			{
				*(int*)dest = val;
				*(int*)(dest + 4) = val;
				*(int*)(dest + 8) = val;
				*(int*)(dest + 12) = val;
				dest += 16;
				len -= 16;
			}
			while (len >= 4)
			{
				*(int*)dest = val;
				dest += 4;
				len -= 4;
			}
			while (len > 0)
			{
				*dest = (byte)val;
				dest++;
				len--;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000A940 File Offset: 0x00008B40
		private unsafe static void memcpy4(byte* dest, byte* src, int size)
		{
			while (size >= 16)
			{
				*(int*)dest = *(int*)src;
				*(int*)(dest + 4) = *(int*)(src + 4);
				*(int*)(dest + 8) = *(int*)(src + 8);
				*(int*)(dest + 12) = *(int*)(src + 12);
				dest += 16;
				src += 16;
				size -= 16;
			}
			while (size >= 4)
			{
				*(int*)dest = *(int*)src;
				dest += 4;
				src += 4;
				size -= 4;
			}
			while (size > 0)
			{
				*dest = *src;
				dest++;
				src++;
				size--;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A9C8 File Offset: 0x00008BC8
		private unsafe static void memcpy2(byte* dest, byte* src, int size)
		{
			while (size >= 8)
			{
				*(short*)dest = *(short*)src;
				*(short*)(dest + 2) = *(short*)(src + 2);
				*(short*)(dest + 4) = *(short*)(src + 4);
				*(short*)(dest + 6) = *(short*)(src + 6);
				dest += 8;
				src += 8;
				size -= 8;
			}
			while (size >= 2)
			{
				*(short*)dest = *(short*)src;
				dest += 2;
				src += 2;
				size -= 2;
			}
			if (size > 0)
			{
				*dest = *src;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000AA38 File Offset: 0x00008C38
		private unsafe static void memcpy1(byte* dest, byte* src, int size)
		{
			while (size >= 8)
			{
				*dest = *src;
				dest[1] = src[1];
				dest[2] = src[2];
				dest[3] = src[3];
				dest[4] = src[4];
				dest[5] = src[5];
				dest[6] = src[6];
				dest[7] = src[7];
				dest += 8;
				src += 8;
				size -= 8;
			}
			while (size >= 2)
			{
				*dest = *src;
				dest[1] = src[1];
				dest += 2;
				src += 2;
				size -= 2;
			}
			if (size > 0)
			{
				*dest = *src;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		internal unsafe static void memcpy(byte* dest, byte* src, int size)
		{
			if (((dest | src) & 3) != 0)
			{
				if ((dest & 1) != 0 && (src & 1) != 0 && size >= 1)
				{
					*dest = *src;
					dest++;
					src++;
					size--;
				}
				if ((dest & 2) != 0 && (src & 2) != 0 && size >= 2)
				{
					*(short*)dest = *(short*)src;
					dest += 2;
					src += 2;
					size -= 2;
				}
				if (((dest | src) & 1) != 0)
				{
					string.memcpy1(dest, src, size);
					return;
				}
				if (((dest | src) & 2) != 0)
				{
					string.memcpy2(dest, src, size);
					return;
				}
			}
			string.memcpy4(dest, src, size);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000AB78 File Offset: 0x00008D78
		internal unsafe static void CharCopy(char* dest, char* src, int count)
		{
			if (((dest | src) & 3) != 0)
			{
				if ((dest & 2) != 0 && (src & 2) != 0 && count > 0)
				{
					*dest = (char)(*(short*)src);
					dest++;
					src++;
					count--;
				}
				if (((dest | src) & 2) != 0)
				{
					string.memcpy2((byte*)dest, (byte*)src, count * 2);
					return;
				}
			}
			string.memcpy4((byte*)dest, (byte*)src, count * 2);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		internal unsafe static void CharCopyReverse(char* dest, char* src, int count)
		{
			dest += count;
			src += count;
			for (int i = count; i > 0; i--)
			{
				dest--;
				src--;
				*dest = *src;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000AC20 File Offset: 0x00008E20
		internal unsafe static void CharCopy(string target, int targetIndex, string source, int sourceIndex, int count)
		{
			fixed (string text = target)
			{
				fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text2 = source, ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						string.CharCopy(ptr + targetIndex, ptr2 + sourceIndex, count);
						text = null;
					}
				}
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000AC58 File Offset: 0x00008E58
		internal unsafe static void CharCopy(string target, int targetIndex, char[] source, int sourceIndex, int count)
		{
			fixed (string text = target)
			{
				fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (char* ptr2 = ref (source != null && source.Length != 0) ? ref source[0] : ref *null)
					{
						string.CharCopy(ptr + targetIndex, ptr2 + sourceIndex, count);
						text = null;
					}
				}
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000ACA4 File Offset: 0x00008EA4
		internal unsafe static void CharCopyReverse(string target, int targetIndex, string source, int sourceIndex, int count)
		{
			fixed (string text = target)
			{
				fixed (char* ptr = text + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text2 = source, ptr2 = text2 + RuntimeHelpers.OffsetToStringData / 2)
					{
						string.CharCopyReverse(ptr + targetIndex, ptr2 + sourceIndex, count);
						text = null;
					}
				}
			}
		}

		// Token: 0x06000275 RID: 629
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string[] InternalSplit(char[] separator, int count, int options);

		// Token: 0x06000276 RID: 630
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string InternalAllocateStr(int length);

		// Token: 0x06000277 RID: 631
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string InternalIntern(string str);

		// Token: 0x06000278 RID: 632
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string InternalIsInterned(string str);

		/// <summary>Determines whether two specified <see cref="T:System.String" /> objects have the same value.</summary>
		/// <returns>true if the value of <paramref name="a" /> is the same as the value of <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.String" /> or null. </param>
		/// <param name="b">A <see cref="T:System.String" /> or null. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000279 RID: 633 RVA: 0x0000ACDC File Offset: 0x00008EDC
		public static bool operator ==(string a, string b)
		{
			return string.Equals(a, b);
		}

		/// <summary>Determines whether two specified <see cref="T:System.String" /> objects have different values.</summary>
		/// <returns>true if the value of <paramref name="a" /> is different from the value of <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">A String or null. </param>
		/// <param name="b">A String or null. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600027A RID: 634 RVA: 0x0000ACE8 File Offset: 0x00008EE8
		public static bool operator !=(string a, string b)
		{
			return !string.Equals(a, b);
		}

		// Token: 0x04000028 RID: 40
		[NonSerialized]
		private int length;

		// Token: 0x04000029 RID: 41
		[NonSerialized]
		private char start_char;

		/// <summary>Represents the empty string. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400002A RID: 42
		public static readonly string Empty = "";

		// Token: 0x0400002B RID: 43
		private static readonly char[] WhiteChars = new char[]
		{
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			'\u0085',
			'\u1680',
			'\u2028',
			'\u2029',
			' ',
			'\u00a0',
			'\u2000',
			'\u2001',
			'\u2002',
			'\u2003',
			'\u2004',
			'\u2005',
			'\u2006',
			'\u2007',
			'\u2008',
			'\u2009',
			'\u200a',
			'​',
			'\u3000',
			'﻿'
		};
	}
}
