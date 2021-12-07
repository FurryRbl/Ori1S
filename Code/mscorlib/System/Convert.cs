using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace System
{
	/// <summary>Converts a base data type to another base data type.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200011C RID: 284
	public static class Convert
	{
		// Token: 0x06000E9C RID: 3740
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] InternalFromBase64String(string str, bool allowWhitespaceOnly);

		// Token: 0x06000E9D RID: 3741
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] InternalFromBase64CharArray(char[] arr, int offset, int length);

		/// <summary>Converts a subset of a Unicode character array, which encodes binary data as base-64 digits, to an equivalent 8-bit unsigned integer array. Parameters specify the subset in the input array and the number of elements to convert.</summary>
		/// <returns>An array of 8-bit unsigned integers equivalent to <paramref name="length" /> elements at position <paramref name="offset" /> in <paramref name="inArray" />.</returns>
		/// <param name="inArray">A Unicode character array. </param>
		/// <param name="offset">A position within <paramref name="inArray" />. </param>
		/// <param name="length">The number of elements in <paramref name="inArray" /> to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="length" /> is less than 0.-or- <paramref name="offset" /> plus <paramref name="length" /> indicates a position not within <paramref name="inArray" />. </exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="inArray" />, ignoring white-space characters, is not zero or a multiple of 4. -or-The format of <paramref name="inArray" /> is invalid. <paramref name="inArray" /> contains a non-base-64 character, more than two padding characters, or a non-white-space character among the padding characters. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E9E RID: 3742 RVA: 0x0003D0AC File Offset: 0x0003B2AC
		public static byte[] FromBase64CharArray(char[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset < 0");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length < 0");
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset + length > array.Length");
			}
			return Convert.InternalFromBase64CharArray(inArray, offset, length);
		}

		/// <summary>Converts the specified string, which encodes binary data as base-64 digits, to an equivalent 8-bit unsigned integer array.</summary>
		/// <returns>An array of 8-bit unsigned integers that is equivalent to <paramref name="s" />.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="s" />, ignoring white-space characters, is not zero or a multiple of 4. -or-The format of <paramref name="s" /> is invalid. <paramref name="s" /> contains a non-base-64 character, more than two padding characters, or a non-white space-character among the padding characters.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E9F RID: 3743 RVA: 0x0003D10C File Offset: 0x0003B30C
		public static byte[] FromBase64String(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length == 0)
			{
				return new byte[0];
			}
			return Convert.InternalFromBase64String(s, true);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for the specified object.</summary>
		/// <returns>The <see cref="T:System.TypeCode" /> for <paramref name="value" />, or <see cref="F:System.TypeCode.Empty" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003D144 File Offset: 0x0003B344
		public static TypeCode GetTypeCode(object value)
		{
			if (value == null)
			{
				return TypeCode.Empty;
			}
			return Type.GetTypeCode(value.GetType());
		}

		/// <summary>Returns an indication whether the specified object is of type <see cref="T:System.DBNull" />.</summary>
		/// <returns>true if <paramref name="value" /> is of type <see cref="T:System.DBNull" />; otherwise, false.</returns>
		/// <param name="value">An object. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003D15C File Offset: 0x0003B35C
		public static bool IsDBNull(object value)
		{
			return value is DBNull;
		}

		/// <summary>Converts a subset of an 8-bit unsigned integer array to an equivalent subset of a Unicode character array encoded with base-64 digits. Parameters specify the subsets as offsets in the input and output arrays, and the number of elements in the input array to convert.</summary>
		/// <returns>A 32-bit signed integer containing the number of bytes in <paramref name="outArray" />.</returns>
		/// <param name="inArray">An input array of 8-bit unsigned integers. </param>
		/// <param name="offsetIn">A position within <paramref name="inArray" />. </param>
		/// <param name="length">The number of elements of <paramref name="inArray" /> to convert. </param>
		/// <param name="outArray">An output array of Unicode characters. </param>
		/// <param name="offsetOut">A position within <paramref name="outArray" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> or <paramref name="outArray" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offsetIn" />, <paramref name="offsetOut" />, or <paramref name="length" /> is negative.-or- <paramref name="offsetIn" /> plus <paramref name="length" /> is greater than the length of <paramref name="inArray" />.-or- <paramref name="offsetOut" /> plus the number of elements to return is greater than the length of <paramref name="outArray" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003D16C File Offset: 0x0003B36C
		public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (outArray == null)
			{
				throw new ArgumentNullException("outArray");
			}
			if (offsetIn < 0 || length < 0 || offsetOut < 0)
			{
				throw new ArgumentOutOfRangeException("offsetIn, length, offsetOut < 0");
			}
			if (offsetIn > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offsetIn + length > array.Length");
			}
			byte[] bytes = ToBase64Transform.InternalTransformFinalBlock(inArray, offsetIn, length);
			char[] chars = new ASCIIEncoding().GetChars(bytes);
			if (offsetOut > outArray.Length - chars.Length)
			{
				throw new ArgumentOutOfRangeException("offsetOut + cOutArr.Length > outArray.Length");
			}
			Array.Copy(chars, 0, outArray, offsetOut, chars.Length);
			return chars.Length;
		}

		/// <summary>Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits.</summary>
		/// <returns>The string representation, in base 64, of the contents of <paramref name="inArray" />.</returns>
		/// <param name="inArray">An array of 8-bit unsigned integers. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA3 RID: 3747 RVA: 0x0003D210 File Offset: 0x0003B410
		public static string ToBase64String(byte[] inArray)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(inArray, 0, inArray.Length);
		}

		/// <summary>Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits. Parameters specify the subset as an offset in the input array, and the number of elements in the array to convert.</summary>
		/// <returns>The string representation in base 64 of <paramref name="length" /> elements of <paramref name="inArray" />, starting at position <paramref name="offset" />.</returns>
		/// <param name="inArray">An array of 8-bit unsigned integers. </param>
		/// <param name="offset">An offset in <paramref name="inArray" />. </param>
		/// <param name="length">The number of elements of <paramref name="inArray" /> to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="length" /> is negative.-or- <paramref name="offset" /> plus <paramref name="length" /> is greater than the length of <paramref name="inArray" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA4 RID: 3748 RVA: 0x0003D230 File Offset: 0x0003B430
		public static string ToBase64String(byte[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (offset < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException("offset < 0 || length < 0");
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset + length > array.Length");
			}
			byte[] bytes = ToBase64Transform.InternalTransformFinalBlock(inArray, offset, length);
			return new ASCIIEncoding().GetString(bytes);
		}

		/// <summary>Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits. A parameter specifies whether to insert line breaks in the return value.</summary>
		/// <returns>The string representation in base 64 of the elements in <paramref name="inArray" />.</returns>
		/// <param name="inArray">An array of 8-bit unsigned integers. </param>
		/// <param name="options">
		///   <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" /> to insert a line break every 76 characters, or <see cref="F:System.Base64FormattingOptions.None" /> to not insert line breaks.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Base64FormattingOptions" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003D294 File Offset: 0x0003B494
		[ComVisible(false)]
		public static string ToBase64String(byte[] inArray, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(inArray, 0, inArray.Length, options);
		}

		/// <summary>Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits. Parameters specify the subset as an offset in the input array, the number of elements in the array to convert, and whether to insert line breaks in the return value.</summary>
		/// <returns>The string representation in base 64 of <paramref name="length" /> elements of <paramref name="inArray" />, starting at position <paramref name="offset" />.</returns>
		/// <param name="inArray">An array of 8-bit unsigned integers. </param>
		/// <param name="offset">An offset in <paramref name="inArray" />. </param>
		/// <param name="length">The number of elements of <paramref name="inArray" /> to convert. </param>
		/// <param name="options">
		///   <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" /> to insert a line break every 76 characters, or <see cref="F:System.Base64FormattingOptions.None" /> to not insert line breaks.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="length" /> is negative.-or- <paramref name="offset" /> plus <paramref name="length" /> is greater than the length of <paramref name="inArray" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Base64FormattingOptions" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003D2B4 File Offset: 0x0003B4B4
		[ComVisible(false)]
		public static string ToBase64String(byte[] inArray, int offset, int length, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (offset < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException("offset < 0 || length < 0");
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset + length > array.Length");
			}
			if (length == 0)
			{
				return string.Empty;
			}
			if (options == Base64FormattingOptions.InsertLineBreaks)
			{
				return Convert.ToBase64StringBuilderWithLine(inArray, offset, length).ToString();
			}
			return Encoding.ASCII.GetString(ToBase64Transform.InternalTransformFinalBlock(inArray, offset, length));
		}

		/// <summary>Converts a subset of an 8-bit unsigned integer array to an equivalent subset of a Unicode character array encoded with base-64 digits. Parameters specify the subsets as offsets in the input and output arrays, the number of elements in the input array to convert, and whether line breaks are inserted in the output array.</summary>
		/// <returns>A 32-bit signed integer containing the number of bytes in <paramref name="outArray" />.</returns>
		/// <param name="inArray">An input array of 8-bit unsigned integers. </param>
		/// <param name="offsetIn">A position within <paramref name="inArray" />. </param>
		/// <param name="length">The number of elements of <paramref name="inArray" /> to convert. </param>
		/// <param name="outArray">An output array of Unicode characters. </param>
		/// <param name="offsetOut">A position within <paramref name="outArray" />. </param>
		/// <param name="options">
		///   <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" /> to insert a line break every 76 characters, or <see cref="F:System.Base64FormattingOptions.None" /> to not insert line breaks.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> or <paramref name="outArray" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offsetIn" />, <paramref name="offsetOut" />, or <paramref name="length" /> is negative.-or- <paramref name="offsetIn" /> plus <paramref name="length" /> is greater than the length of <paramref name="inArray" />.-or- <paramref name="offsetOut" /> plus the number of elements to return is greater than the length of <paramref name="outArray" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Base64FormattingOptions" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003D334 File Offset: 0x0003B534
		[ComVisible(false)]
		public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (outArray == null)
			{
				throw new ArgumentNullException("outArray");
			}
			if (offsetIn < 0 || length < 0 || offsetOut < 0)
			{
				throw new ArgumentOutOfRangeException("offsetIn, length, offsetOut < 0");
			}
			if (offsetIn > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offsetIn + length > array.Length");
			}
			if (length == 0)
			{
				return 0;
			}
			if (options == Base64FormattingOptions.InsertLineBreaks)
			{
				StringBuilder stringBuilder = Convert.ToBase64StringBuilderWithLine(inArray, offsetIn, length);
				stringBuilder.CopyTo(0, outArray, offsetOut, stringBuilder.Length);
				return stringBuilder.Length;
			}
			byte[] bytes = ToBase64Transform.InternalTransformFinalBlock(inArray, offsetIn, length);
			char[] chars = Encoding.ASCII.GetChars(bytes);
			if (offsetOut > outArray.Length - chars.Length)
			{
				throw new ArgumentOutOfRangeException("offsetOut + cOutArr.Length > outArray.Length");
			}
			Array.Copy(chars, 0, outArray, offsetOut, chars.Length);
			return chars.Length;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003D408 File Offset: 0x0003B608
		private static StringBuilder ToBase64StringBuilderWithLine(byte[] inArray, int offset, int length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num2;
			int num = Math.DivRem(length, 57, out num2);
			for (int i = 0; i < num; i++)
			{
				byte[] bytes = ToBase64Transform.InternalTransformFinalBlock(inArray, offset, 57);
				stringBuilder.AppendLine(Encoding.ASCII.GetString(bytes));
				offset += 57;
			}
			if (num2 == 0)
			{
				int length2 = Environment.NewLine.Length;
				stringBuilder.Remove(stringBuilder.Length - length2, length2);
			}
			else
			{
				byte[] bytes2 = ToBase64Transform.InternalTransformFinalBlock(inArray, offset, num2);
				stringBuilder.Append(Encoding.ASCII.GetString(bytes2));
			}
			return stringBuilder;
		}

		/// <summary>Returns the specified Boolean value; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The Boolean value to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003D4A4 File Offset: 0x0003B6A4
		public static bool ToBoolean(bool value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EAA RID: 3754 RVA: 0x0003D4A8 File Offset: 0x0003B6A8
		public static bool ToBoolean(byte value)
		{
			return value != 0;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EAB RID: 3755 RVA: 0x0003D4B4 File Offset: 0x0003B6B4
		public static bool ToBoolean(char value)
		{
			throw new InvalidCastException(Locale.GetText("Can't convert char to bool"));
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EAC RID: 3756 RVA: 0x0003D4C8 File Offset: 0x0003B6C8
		public static bool ToBoolean(DateTime value)
		{
			throw new InvalidCastException(Locale.GetText("Can't convert date to bool"));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EAD RID: 3757 RVA: 0x0003D4DC File Offset: 0x0003B6DC
		public static bool ToBoolean(decimal value)
		{
			return value != 0m;
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EAE RID: 3758 RVA: 0x0003D4EC File Offset: 0x0003B6EC
		public static bool ToBoolean(double value)
		{
			return value != 0.0;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EAF RID: 3759 RVA: 0x0003D500 File Offset: 0x0003B700
		public static bool ToBoolean(float value)
		{
			return value != 0f;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003D510 File Offset: 0x0003B710
		public static bool ToBoolean(int value)
		{
			return value != 0;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003D51C File Offset: 0x0003B71C
		public static bool ToBoolean(long value)
		{
			return value != 0L;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003D528 File Offset: 0x0003B728
		[CLSCompliant(false)]
		public static bool ToBoolean(sbyte value)
		{
			return (int)value != 0;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003D534 File Offset: 0x0003B734
		public static bool ToBoolean(short value)
		{
			return value != 0;
		}

		/// <summary>Converts the specified string representation of a logical value to its Boolean equivalent.</summary>
		/// <returns>true if <paramref name="value" /> equals <see cref="F:System.Boolean.TrueString" />, or false if <paramref name="value" /> equals <see cref="F:System.Boolean.FalseString" /> or null.</returns>
		/// <param name="value">A string that contains the value of either <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not equal to <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003D540 File Offset: 0x0003B740
		public static bool ToBoolean(string value)
		{
			return value != null && bool.Parse(value);
		}

		/// <summary>Converts the specified string representation of a logical value to its Boolean equivalent, using the specified culture-specific formatting information.</summary>
		/// <returns>true if <paramref name="value" /> equals <see cref="F:System.Boolean.TrueString" />, or false if <paramref name="value" /> equals <see cref="F:System.Boolean.FalseString" /> or null.</returns>
		/// <param name="value">A string that contains the value of either <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. This parameter is ignored.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not equal to <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003D550 File Offset: 0x0003B750
		public static bool ToBoolean(string value, IFormatProvider provider)
		{
			return value != null && bool.Parse(value);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003D560 File Offset: 0x0003B760
		[CLSCompliant(false)]
		public static bool ToBoolean(uint value)
		{
			return value != 0U;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EB7 RID: 3767 RVA: 0x0003D56C File Offset: 0x0003B76C
		[CLSCompliant(false)]
		public static bool ToBoolean(ulong value)
		{
			return value != 0UL;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EB8 RID: 3768 RVA: 0x0003D578 File Offset: 0x0003B778
		[CLSCompliant(false)]
		public static bool ToBoolean(ushort value)
		{
			return value != 0;
		}

		/// <summary>Converts the value of a specified object to an equivalent Boolean value.</summary>
		/// <returns>true or false, which reflects the value returned by invoking the <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" /> method for the underlying type of <paramref name="value" />. If <paramref name="value" /> is null, the method returns false. </returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is a string that does not equal <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.-or-The conversion of <paramref name="value" /> to a <see cref="T:System.Boolean" /> is not supported.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EB9 RID: 3769 RVA: 0x0003D584 File Offset: 0x0003B784
		public static bool ToBoolean(object value)
		{
			return value != null && Convert.ToBoolean(value, null);
		}

		/// <summary>Converts the value of the specified object to an equivalent Boolean value, using the specified culture-specific formatting information.</summary>
		/// <returns>true or false, which reflects the value returned by invoking the <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" /> method for the underlying type of <paramref name="value" />. If <paramref name="value" /> is null, the method returns false.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is a string that does not equal <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.-or-The conversion of <paramref name="value" /> to a <see cref="T:System.Boolean" /> is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EBA RID: 3770 RVA: 0x0003D598 File Offset: 0x0003B798
		public static bool ToBoolean(object value, IFormatProvider provider)
		{
			return value != null && ((IConvertible)value).ToBoolean(provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent 8-bit unsigned integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EBB RID: 3771 RVA: 0x0003D5B0 File Offset: 0x0003B7B0
		public static byte ToByte(bool value)
		{
			return (!value) ? 0 : 1;
		}

		/// <summary>Returns the specified 8-bit unsigned integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 8-bit unsigned integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EBC RID: 3772 RVA: 0x0003D5C0 File Offset: 0x0003B7C0
		public static byte ToByte(byte value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EBD RID: 3773 RVA: 0x0003D5C4 File Offset: 0x0003B7C4
		public static byte ToByte(char value)
		{
			if (value > 'ÿ')
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue"));
			}
			return (byte)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EBE RID: 3774 RVA: 0x0003D5E4 File Offset: 0x0003B7E4
		public static byte ToByte(DateTime value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" /> or less than <see cref="F:System.Byte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EBF RID: 3775 RVA: 0x0003D5F0 File Offset: 0x0003B7F0
		public static byte ToByte(decimal value)
		{
			if (value > 255m || value < 0m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue or less than Byte.MinValue"));
			}
			return (byte)Math.Round(value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" /> or less than <see cref="F:System.Byte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003D640 File Offset: 0x0003B840
		public static byte ToByte(double value)
		{
			if (value > 255.0 || value < 0.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue or less than Byte.MinValue"));
			}
			if (double.IsNaN(value) || double.IsInfinity(value))
			{
				throw new OverflowException(Locale.GetText("Value is equal to Double.NaN, Double.PositiveInfinity, or Double.NegativeInfinity"));
			}
			return (byte)Math.Round(value);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">A single-precision floating-point number. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" /> or less than <see cref="F:System.Byte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003D6A8 File Offset: 0x0003B8A8
		public static byte ToByte(float value)
		{
			if (value > 255f || value < 0f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue or less than Byte.Minalue"));
			}
			if (float.IsNaN(value) || float.IsInfinity(value))
			{
				throw new OverflowException(Locale.GetText("Value is equal to Single.NaN, Single.PositiveInfinity, or Single.NegativeInfinity"));
			}
			return (byte)Math.Round((double)value);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EC2 RID: 3778 RVA: 0x0003D70C File Offset: 0x0003B90C
		public static byte ToByte(int value)
		{
			if (value > 255 || value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue or less than Byte.MinValue"));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003D740 File Offset: 0x0003B940
		public static byte ToByte(long value)
		{
			if (value > 255L || value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue or less than Byte.MinValue"));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to be converted. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003D774 File Offset: 0x0003B974
		[CLSCompliant(false)]
		public static byte ToByte(sbyte value)
		{
			if ((int)value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than Byte.MinValue"));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EC5 RID: 3781 RVA: 0x0003D790 File Offset: 0x0003B990
		public static byte ToByte(short value)
		{
			if (value > 255 || value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue or less than Byte.MinValue"));
			}
			return (byte)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EC6 RID: 3782 RVA: 0x0003D7C4 File Offset: 0x0003B9C4
		public static byte ToByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 8-bit unsigned integer, using specified culture-specific formatting information.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EC7 RID: 3783 RVA: 0x0003D7D4 File Offset: 0x0003B9D4
		public static byte ToByte(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value, provider);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a base 10 unsigned number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003D7E8 File Offset: 0x0003B9E8
		public static byte ToByte(string value, int fromBase)
		{
			int num = Convert.ConvertFromBase(value, fromBase, true);
			if (num < 0 || num > 255)
			{
				throw new OverflowException();
			}
			return (byte)num;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003D818 File Offset: 0x0003BA18
		[CLSCompliant(false)]
		public static byte ToByte(uint value)
		{
			if (value > 255U)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue"));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ECA RID: 3786 RVA: 0x0003D838 File Offset: 0x0003BA38
		[CLSCompliant(false)]
		public static byte ToByte(ulong value)
		{
			if (value > 255UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue"));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ECB RID: 3787 RVA: 0x0003D858 File Offset: 0x0003BA58
		[CLSCompliant(false)]
		public static byte ToByte(ushort value)
		{
			if (value > 255)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Byte.MaxValue"));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified object to an 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in the property format for a <see cref="T:System.Byte" /> value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />. -or-Conversion from <paramref name="value" /> to the <see cref="T:System.Byte" /> type is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ECC RID: 3788 RVA: 0x0003D878 File Offset: 0x0003BA78
		public static byte ToByte(object value)
		{
			if (value == null)
			{
				return 0;
			}
			return Convert.ToByte(value, null);
		}

		/// <summary>Converts the value of the specified object to an 8-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in the property format for a <see cref="T:System.Byte" /> value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />. -or-Conversion from <paramref name="value" /> to the <see cref="T:System.Byte" /> type is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ECD RID: 3789 RVA: 0x0003D88C File Offset: 0x0003BA8C
		public static byte ToByte(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ((IConvertible)value).ToByte(provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ECE RID: 3790 RVA: 0x0003D8A4 File Offset: 0x0003BAA4
		public static char ToChar(bool value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ECF RID: 3791 RVA: 0x0003D8B0 File Offset: 0x0003BAB0
		public static char ToChar(byte value)
		{
			return (char)value;
		}

		/// <summary>Returns the specified Unicode character value; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The Unicode character to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003D8B4 File Offset: 0x0003BAB4
		public static char ToChar(char value)
		{
			return value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003D8B8 File Offset: 0x0003BAB8
		public static char ToChar(DateTime value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED2 RID: 3794 RVA: 0x0003D8C4 File Offset: 0x0003BAC4
		public static char ToChar(decimal value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED3 RID: 3795 RVA: 0x0003D8D0 File Offset: 0x0003BAD0
		public static char ToChar(double value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003D8DC File Offset: 0x0003BADC
		public static char ToChar(int value)
		{
			if (value > 65535 || value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Char.MaxValue or less than Char.MinValue"));
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003D910 File Offset: 0x0003BB10
		public static char ToChar(long value)
		{
			if (value > 65535L || value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Char.MaxValue or less than Char.MinValue"));
			}
			return (char)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003D944 File Offset: 0x0003BB44
		public static char ToChar(float value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003D950 File Offset: 0x0003BB50
		[CLSCompliant(false)]
		public static char ToChar(sbyte value)
		{
			if ((int)value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than Char.MinValue"));
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003D96C File Offset: 0x0003BB6C
		public static char ToChar(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than Char.MinValue"));
			}
			return (char)value;
		}

		/// <summary>Converts the first character of a specified string to a Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to the first and only character in <paramref name="value" />.</returns>
		/// <param name="value">A string of length 1. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="value" /> is not 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003D988 File Offset: 0x0003BB88
		public static char ToChar(string value)
		{
			return char.Parse(value);
		}

		/// <summary>Converts the first character of a specified string to a Unicode character, using specified culture-specific formatting information.</summary>
		/// <returns>A Unicode character that is equivalent to the first and only character in <paramref name="value" />.</returns>
		/// <param name="value">A string of length 1 or null. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. This parameter is ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="value" /> is not 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDA RID: 3802 RVA: 0x0003D990 File Offset: 0x0003BB90
		public static char ToChar(string value, IFormatProvider provider)
		{
			return char.Parse(value);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Char.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDB RID: 3803 RVA: 0x0003D998 File Offset: 0x0003BB98
		[CLSCompliant(false)]
		public static char ToChar(uint value)
		{
			if (value > 65535U)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Char.MaxValue"));
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Char.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDC RID: 3804 RVA: 0x0003D9B8 File Offset: 0x0003BBB8
		[CLSCompliant(false)]
		public static char ToChar(ulong value)
		{
			if (value > 65535UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Char.MaxValue"));
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDD RID: 3805 RVA: 0x0003D9D8 File Offset: 0x0003BBD8
		[CLSCompliant(false)]
		public static char ToChar(ushort value)
		{
			return (char)value;
		}

		/// <summary>Converts the value of the specified object to a Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to value, or <see cref="F:System.Char.MinValue" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is a null string.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.-or-The conversion of <paramref name="value" /> to a <see cref="T:System.Char" /> is not supported. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDE RID: 3806 RVA: 0x0003D9DC File Offset: 0x0003BBDC
		public static char ToChar(object value)
		{
			if (value == null)
			{
				return '\0';
			}
			return Convert.ToChar(value, null);
		}

		/// <summary>Converts the value of the specified object to its equivalent Unicode character, using the specified culture-specific formatting information.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />, or <see cref="F:System.Char.MinValue" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is a null string.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion of <paramref name="value" /> to a <see cref="T:System.Char" /> is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EDF RID: 3807 RVA: 0x0003D9F0 File Offset: 0x0003BBF0
		public static char ToChar(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return '\0';
			}
			return ((IConvertible)value).ToChar(provider);
		}

		/// <summary>Converts the specified string representation of a date and time to an equivalent date and time value.</summary>
		/// <returns>The date and time equivalent of the value of <paramref name="value" />, or the date and time equivalent of <see cref="F:System.DateTime.MinValue" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">The string representation of a date and time.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a properly formatted date and time string. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003DA08 File Offset: 0x0003BC08
		public static DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				return DateTime.MinValue;
			}
			return DateTime.Parse(value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent date and time, using the specified culture-specific formatting information.</summary>
		/// <returns>The date and time equivalent of the value of <paramref name="value" />, or the date and time equivalent of <see cref="F:System.DateTime.MinValue" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains a date and time to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a properly formatted date and time string. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003DA1C File Offset: 0x0003BC1C
		public static DateTime ToDateTime(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return DateTime.MinValue;
			}
			return DateTime.Parse(value, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003DA34 File Offset: 0x0003BC34
		public static DateTime ToDateTime(bool value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003DA40 File Offset: 0x0003BC40
		public static DateTime ToDateTime(byte value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003DA4C File Offset: 0x0003BC4C
		public static DateTime ToDateTime(char value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Returns the specified <see cref="T:System.DateTime" /> object; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">A date and time value. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003DA58 File Offset: 0x0003BC58
		public static DateTime ToDateTime(DateTime value)
		{
			return value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The number to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003DA5C File Offset: 0x0003BC5C
		public static DateTime ToDateTime(decimal value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The double-precision floating-point value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003DA68 File Offset: 0x0003BC68
		public static DateTime ToDateTime(double value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003DA74 File Offset: 0x0003BC74
		public static DateTime ToDateTime(short value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003DA80 File Offset: 0x0003BC80
		public static DateTime ToDateTime(int value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EEA RID: 3818 RVA: 0x0003DA8C File Offset: 0x0003BC8C
		public static DateTime ToDateTime(long value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The single-precision floating-point value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EEB RID: 3819 RVA: 0x0003DA98 File Offset: 0x0003BC98
		public static DateTime ToDateTime(float value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the value of the specified object to a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The date and time equivalent of the value of <paramref name="value" />, or a date and time equivalent of <see cref="F:System.DateTime.MinValue" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a valid date and time value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EEC RID: 3820 RVA: 0x0003DAA4 File Offset: 0x0003BCA4
		public static DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				return DateTime.MinValue;
			}
			return Convert.ToDateTime(value, null);
		}

		/// <summary>Converts the value of the specified object to a <see cref="T:System.DateTime" /> object, using the specified culture-specific formatting information.</summary>
		/// <returns>The date and time equivalent of the value of <paramref name="value" />, or the date and time equivalent of <see cref="F:System.DateTime.MinValue" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a valid date and time value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EED RID: 3821 RVA: 0x0003DABC File Offset: 0x0003BCBC
		public static DateTime ToDateTime(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return DateTime.MinValue;
			}
			return ((IConvertible)value).ToDateTime(provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EEE RID: 3822 RVA: 0x0003DAD8 File Offset: 0x0003BCD8
		[CLSCompliant(false)]
		public static DateTime ToDateTime(sbyte value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EEF RID: 3823 RVA: 0x0003DAE4 File Offset: 0x0003BCE4
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ushort value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003DAF0 File Offset: 0x0003BCF0
		[CLSCompliant(false)]
		public static DateTime ToDateTime(uint value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003DAFC File Offset: 0x0003BCFC
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ulong value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the specified Boolean value to the equivalent decimal number.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003DB08 File Offset: 0x0003BD08
		public static decimal ToDecimal(bool value)
		{
			return (!value) ? 0 : 1;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent decimal number.</summary>
		/// <returns>The decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003DB1C File Offset: 0x0003BD1C
		public static decimal ToDecimal(byte value)
		{
			return value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EF4 RID: 3828 RVA: 0x0003DB24 File Offset: 0x0003BD24
		public static decimal ToDecimal(char value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EF5 RID: 3829 RVA: 0x0003DB30 File Offset: 0x0003BD30
		public static decimal ToDecimal(DateTime value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Returns the specified decimal number; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">A decimal number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003DB3C File Offset: 0x0003BD3C
		public static decimal ToDecimal(decimal value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />. </returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003DB40 File Offset: 0x0003BD40
		public static decimal ToDecimal(double value)
		{
			return (decimal)value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to the equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />. </returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003DB48 File Offset: 0x0003BD48
		public static decimal ToDecimal(float value)
		{
			return (decimal)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003DB50 File Offset: 0x0003BD50
		public static decimal ToDecimal(int value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EFA RID: 3834 RVA: 0x0003DB58 File Offset: 0x0003BD58
		public static decimal ToDecimal(long value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EFB RID: 3835 RVA: 0x0003DB60 File Offset: 0x0003BD60
		[CLSCompliant(false)]
		public static decimal ToDecimal(sbyte value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EFC RID: 3836 RVA: 0x0003DB68 File Offset: 0x0003BD68
		public static decimal ToDecimal(short value)
		{
			return value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains a number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EFD RID: 3837 RVA: 0x0003DB70 File Offset: 0x0003BD70
		public static decimal ToDecimal(string value)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent decimal number, using the specified culture-specific formatting information.</summary>
		/// <returns>A decimal number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains a number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EFE RID: 3838 RVA: 0x0003DB88 File Offset: 0x0003BD88
		public static decimal ToDecimal(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value, provider);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EFF RID: 3839 RVA: 0x0003DBA0 File Offset: 0x0003BDA0
		[CLSCompliant(false)]
		public static decimal ToDecimal(uint value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F00 RID: 3840 RVA: 0x0003DBA8 File Offset: 0x0003BDA8
		[CLSCompliant(false)]
		public static decimal ToDecimal(ulong value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to an equivalent decimal number.</summary>
		/// <returns>The decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F01 RID: 3841 RVA: 0x0003DBB0 File Offset: 0x0003BDB0
		[CLSCompliant(false)]
		public static decimal ToDecimal(ushort value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified object to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for a <see cref="T:System.Decimal" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F02 RID: 3842 RVA: 0x0003DBB8 File Offset: 0x0003BDB8
		public static decimal ToDecimal(object value)
		{
			if (value == null)
			{
				return 0m;
			}
			return Convert.ToDecimal(value, null);
		}

		/// <summary>Converts the value of the specified object to an equivalent decimal number, using the specified culture-specific formatting information.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for a <see cref="T:System.Decimal" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.-or-The conversion is not supported. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F03 RID: 3843 RVA: 0x0003DBD0 File Offset: 0x0003BDD0
		public static decimal ToDecimal(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0m;
			}
			return ((IConvertible)value).ToDecimal(provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent double-precision floating-point number.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F04 RID: 3844 RVA: 0x0003DBEC File Offset: 0x0003BDEC
		public static double ToDouble(bool value)
		{
			return (double)((!value) ? 0 : 1);
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent double-precision floating-point number.</summary>
		/// <returns>The double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F05 RID: 3845 RVA: 0x0003DBFC File Offset: 0x0003BDFC
		public static double ToDouble(byte value)
		{
			return (double)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F06 RID: 3846 RVA: 0x0003DC00 File Offset: 0x0003BE00
		public static double ToDouble(char value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F07 RID: 3847 RVA: 0x0003DC0C File Offset: 0x0003BE0C
		public static double ToDouble(DateTime value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F08 RID: 3848 RVA: 0x0003DC18 File Offset: 0x0003BE18
		public static double ToDouble(decimal value)
		{
			return (double)value;
		}

		/// <summary>Returns the specified double-precision floating-point number; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The double-precision floating-point number to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F09 RID: 3849 RVA: 0x0003DC20 File Offset: 0x0003BE20
		public static double ToDouble(double value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The single-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F0A RID: 3850 RVA: 0x0003DC24 File Offset: 0x0003BE24
		public static double ToDouble(float value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F0B RID: 3851 RVA: 0x0003DC28 File Offset: 0x0003BE28
		public static double ToDouble(int value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F0C RID: 3852 RVA: 0x0003DC2C File Offset: 0x0003BE2C
		public static double ToDouble(long value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent double-precision floating-point number.</summary>
		/// <returns>The 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F0D RID: 3853 RVA: 0x0003DC30 File Offset: 0x0003BE30
		[CLSCompliant(false)]
		public static double ToDouble(sbyte value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F0E RID: 3854 RVA: 0x0003DC34 File Offset: 0x0003BE34
		public static double ToDouble(short value)
		{
			return (double)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null. Because of differences in precision, the return value may not be exactly equal to <paramref name="value" />, and for values of <paramref name="value" /> that are less than <see cref="F:System.Double.Epsilon" />, the return value may also differ depending on processor architecture. For more information, see the Remarks section of <see cref="T:System.Double" />.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F0F RID: 3855 RVA: 0x0003DC38 File Offset: 0x0003BE38
		public static double ToDouble(string value)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent double-precision floating-point number, using the specified culture-specific formatting information.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null. Because of differences in precision, the return value may not be exactly equal to <paramref name="value" />, and for values of <paramref name="value" /> that are less than <see cref="F:System.Double.Epsilon" />, the return value may also differ depending on processor architecture. For more information, see the Remarks section of <see cref="T:System.Double" />.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F10 RID: 3856 RVA: 0x0003DC50 File Offset: 0x0003BE50
		public static double ToDouble(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value, provider);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F11 RID: 3857 RVA: 0x0003DC6C File Offset: 0x0003BE6C
		[CLSCompliant(false)]
		public static double ToDouble(uint value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F12 RID: 3858 RVA: 0x0003DC74 File Offset: 0x0003BE74
		[CLSCompliant(false)]
		public static double ToDouble(ulong value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F13 RID: 3859 RVA: 0x0003DC7C File Offset: 0x0003BE7C
		[CLSCompliant(false)]
		public static double ToDouble(ushort value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified object to a double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for a <see cref="T:System.Double" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F14 RID: 3860 RVA: 0x0003DC80 File Offset: 0x0003BE80
		public static double ToDouble(object value)
		{
			if (value == null)
			{
				return 0.0;
			}
			return Convert.ToDouble(value, null);
		}

		/// <summary>Converts the value of the specified object to an double-precision floating-point number, using the specified culture-specific formatting information.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for a <see cref="T:System.Double" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F15 RID: 3861 RVA: 0x0003DC9C File Offset: 0x0003BE9C
		public static double ToDouble(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0.0;
			}
			return ((IConvertible)value).ToDouble(provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent 16-bit signed integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F16 RID: 3862 RVA: 0x0003DCBC File Offset: 0x0003BEBC
		public static short ToInt16(bool value)
		{
			return (!value) ? 0 : 1;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F17 RID: 3863 RVA: 0x0003DCCC File Offset: 0x0003BECC
		public static short ToInt16(byte value)
		{
			return (short)value;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />. </returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F18 RID: 3864 RVA: 0x0003DCD0 File Offset: 0x0003BED0
		public static short ToInt16(char value)
		{
			if (value > '翿')
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue"));
			}
			return (short)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F19 RID: 3865 RVA: 0x0003DCF0 File Offset: 0x0003BEF0
		public static short ToInt16(DateTime value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 16-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F1A RID: 3866 RVA: 0x0003DCFC File Offset: 0x0003BEFC
		public static short ToInt16(decimal value)
		{
			if (value > 32767m || value < -32768m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue or less than Int16.MinValue"));
			}
			return (short)Math.Round(value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 16-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F1B RID: 3867 RVA: 0x0003DD50 File Offset: 0x0003BF50
		public static short ToInt16(double value)
		{
			if (value > 32767.0 || value < -32768.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue or less than Int16.MinValue"));
			}
			return (short)Math.Round(value);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 16-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F1C RID: 3868 RVA: 0x0003DD88 File Offset: 0x0003BF88
		public static short ToInt16(float value)
		{
			if (value > 32767f || value < -32768f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue or less than Int16.MinValue"));
			}
			return (short)Math.Round((double)value);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 16-bit signed integer.</summary>
		/// <returns>The 16-bit signed integer equivalent of <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F1D RID: 3869 RVA: 0x0003DDC4 File Offset: 0x0003BFC4
		public static short ToInt16(int value)
		{
			if (value > 32767 || value < -32768)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue or less than Int16.MinValue"));
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F1E RID: 3870 RVA: 0x0003DDFC File Offset: 0x0003BFFC
		public static short ToInt16(long value)
		{
			if (value > 32767L || value < -32768L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue or less than Int16.MinValue"));
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 16-bit signed integer.</summary>
		/// <returns>A 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F1F RID: 3871 RVA: 0x0003DE34 File Offset: 0x0003C034
		[CLSCompliant(false)]
		public static short ToInt16(sbyte value)
		{
			return (short)value;
		}

		/// <summary>Returns the specified 16-bit signed integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 16-bit signed integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F20 RID: 3872 RVA: 0x0003DE38 File Offset: 0x0003C038
		public static short ToInt16(short value)
		{
			return value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F21 RID: 3873 RVA: 0x0003DE3C File Offset: 0x0003C03C
		public static short ToInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 16-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F22 RID: 3874 RVA: 0x0003DE4C File Offset: 0x0003C04C
		public static short ToInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value, provider);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F23 RID: 3875 RVA: 0x0003DE60 File Offset: 0x0003C060
		public static short ToInt16(string value, int fromBase)
		{
			int num = Convert.ConvertFromBase(value, fromBase, false);
			if (fromBase != 10)
			{
				if (num > 65535)
				{
					throw new OverflowException("Value was either too large or too small for an Int16.");
				}
				if (num > 32767)
				{
					return Convert.ToInt16(-(65536 - num));
				}
			}
			return Convert.ToInt16(num);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F24 RID: 3876 RVA: 0x0003DEB4 File Offset: 0x0003C0B4
		[CLSCompliant(false)]
		public static short ToInt16(uint value)
		{
			if ((ulong)value > 32767UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue"));
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F25 RID: 3877 RVA: 0x0003DED8 File Offset: 0x0003C0D8
		[CLSCompliant(false)]
		public static short ToInt16(ulong value)
		{
			if (value > 32767UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue"));
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F26 RID: 3878 RVA: 0x0003DEF8 File Offset: 0x0003C0F8
		[CLSCompliant(false)]
		public static short ToInt16(ushort value)
		{
			if (value > 32767)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int16.MaxValue"));
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified object to a 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for an <see cref="T:System.Int16" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F27 RID: 3879 RVA: 0x0003DF18 File Offset: 0x0003C118
		public static short ToInt16(object value)
		{
			if (value == null)
			{
				return 0;
			}
			return Convert.ToInt16(value, null);
		}

		/// <summary>Converts the value of the specified object to a 16-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for an <see cref="T:System.Int16" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F28 RID: 3880 RVA: 0x0003DF2C File Offset: 0x0003C12C
		public static short ToInt16(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ((IConvertible)value).ToInt16(provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent 32-bit signed integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F29 RID: 3881 RVA: 0x0003DF44 File Offset: 0x0003C144
		public static int ToInt32(bool value)
		{
			return (!value) ? 0 : 1;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F2A RID: 3882 RVA: 0x0003DF54 File Offset: 0x0003C154
		public static int ToInt32(byte value)
		{
			return (int)value;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F2B RID: 3883 RVA: 0x0003DF58 File Offset: 0x0003C158
		public static int ToInt32(char value)
		{
			return (int)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert.</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F2C RID: 3884 RVA: 0x0003DF5C File Offset: 0x0003C15C
		public static int ToInt32(DateTime value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 32-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" /> or less than <see cref="F:System.Int32.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F2D RID: 3885 RVA: 0x0003DF68 File Offset: 0x0003C168
		public static int ToInt32(decimal value)
		{
			if (value > 2147483647m || value < -2147483648m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int32.MaxValue or less than Int32.MinValue"));
			}
			return (int)Math.Round(value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 32-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" /> or less than <see cref="F:System.Int32.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F2E RID: 3886 RVA: 0x0003DFBC File Offset: 0x0003C1BC
		public static int ToInt32(double value)
		{
			if (value > 2147483647.0 || value < -2147483648.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int32.MaxValue or less than Int32.MinValue"));
			}
			return checked((int)Math.Round(value));
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 32-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" /> or less than <see cref="F:System.Int32.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F2F RID: 3887 RVA: 0x0003DFF4 File Offset: 0x0003C1F4
		public static int ToInt32(float value)
		{
			if (value > 2.1474836E+09f || value < -2.1474836E+09f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int32.MaxValue or less than Int32.MinValue"));
			}
			return checked((int)Math.Round((double)value));
		}

		/// <summary>Returns the specified 32-bit signed integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 32-bit signed integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F30 RID: 3888 RVA: 0x0003E030 File Offset: 0x0003C230
		public static int ToInt32(int value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" /> or less than <see cref="F:System.Int32.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F31 RID: 3889 RVA: 0x0003E034 File Offset: 0x0003C234
		public static int ToInt32(long value)
		{
			if (value > 2147483647L || value < -2147483648L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int32.MaxValue or less than Int32.MinValue"));
			}
			return (int)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 32-bit signed integer.</summary>
		/// <returns>A 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F32 RID: 3890 RVA: 0x0003E06C File Offset: 0x0003C26C
		[CLSCompliant(false)]
		public static int ToInt32(sbyte value)
		{
			return (int)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F33 RID: 3891 RVA: 0x0003E070 File Offset: 0x0003C270
		public static int ToInt32(short value)
		{
			return (int)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F34 RID: 3892 RVA: 0x0003E074 File Offset: 0x0003C274
		public static int ToInt32(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 32-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F35 RID: 3893 RVA: 0x0003E084 File Offset: 0x0003C284
		public static int ToInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value, provider);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F36 RID: 3894 RVA: 0x0003E098 File Offset: 0x0003C298
		public static int ToInt32(string value, int fromBase)
		{
			return Convert.ConvertFromBase(value, fromBase, false);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F37 RID: 3895 RVA: 0x0003E0A4 File Offset: 0x0003C2A4
		[CLSCompliant(false)]
		public static int ToInt32(uint value)
		{
			if (value > 2147483647U)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int32.MaxValue"));
			}
			return (int)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F38 RID: 3896 RVA: 0x0003E0C4 File Offset: 0x0003C2C4
		[CLSCompliant(false)]
		public static int ToInt32(ulong value)
		{
			if (value > 2147483647UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int32.MaxValue"));
			}
			return (int)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F39 RID: 3897 RVA: 0x0003E0E4 File Offset: 0x0003C2E4
		[CLSCompliant(false)]
		public static int ToInt32(ushort value)
		{
			return (int)value;
		}

		/// <summary>Converts the value of the specified object to a 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the  <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F3A RID: 3898 RVA: 0x0003E0E8 File Offset: 0x0003C2E8
		public static int ToInt32(object value)
		{
			if (value == null)
			{
				return 0;
			}
			return Convert.ToInt32(value, null);
		}

		/// <summary>Converts the value of the specified object to a 32-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F3B RID: 3899 RVA: 0x0003E0FC File Offset: 0x0003C2FC
		public static int ToInt32(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ((IConvertible)value).ToInt32(provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent 64-bit signed integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F3C RID: 3900 RVA: 0x0003E114 File Offset: 0x0003C314
		public static long ToInt64(bool value)
		{
			return (!value) ? 0L : 1L;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F3D RID: 3901 RVA: 0x0003E124 File Offset: 0x0003C324
		public static long ToInt64(byte value)
		{
			return (long)((ulong)value);
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F3E RID: 3902 RVA: 0x0003E128 File Offset: 0x0003C328
		public static long ToInt64(char value)
		{
			return (long)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F3F RID: 3903 RVA: 0x0003E12C File Offset: 0x0003C32C
		public static long ToInt64(DateTime value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 64-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int64.MaxValue" /> or less than <see cref="F:System.Int64.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F40 RID: 3904 RVA: 0x0003E138 File Offset: 0x0003C338
		public static long ToInt64(decimal value)
		{
			if (value > 9223372036854775807m || value < -9223372036854775808m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int64.MaxValue or less than Int64.MinValue"));
			}
			return (long)Math.Round(value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 64-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int64.MaxValue" /> or less than <see cref="F:System.Int64.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F41 RID: 3905 RVA: 0x0003E194 File Offset: 0x0003C394
		public static long ToInt64(double value)
		{
			if (value > 9.223372036854776E+18 || value < -9.223372036854776E+18)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int64.MaxValue or less than Int64.MinValue"));
			}
			return (long)Math.Round(value);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 64-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int64.MaxValue" /> or less than <see cref="F:System.Int64.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F42 RID: 3906 RVA: 0x0003E1CC File Offset: 0x0003C3CC
		public static long ToInt64(float value)
		{
			if (value > 9.223372E+18f || value < -9.223372E+18f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int64.MaxValue or less than Int64.MinValue"));
			}
			return (long)Math.Round((double)value);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F43 RID: 3907 RVA: 0x0003E208 File Offset: 0x0003C408
		public static long ToInt64(int value)
		{
			return (long)value;
		}

		/// <summary>Returns the specified 64-bit signed integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">A 64-bit signed integer. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F44 RID: 3908 RVA: 0x0003E20C File Offset: 0x0003C40C
		public static long ToInt64(long value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F45 RID: 3909 RVA: 0x0003E210 File Offset: 0x0003C410
		[CLSCompliant(false)]
		public static long ToInt64(sbyte value)
		{
			return (long)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F46 RID: 3910 RVA: 0x0003E214 File Offset: 0x0003C414
		public static long ToInt64(short value)
		{
			return (long)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains a number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F47 RID: 3911 RVA: 0x0003E218 File Offset: 0x0003C418
		public static long ToInt64(string value)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 64-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F48 RID: 3912 RVA: 0x0003E22C File Offset: 0x0003C42C
		public static long ToInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value, provider);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F49 RID: 3913 RVA: 0x0003E240 File Offset: 0x0003C440
		public static long ToInt64(string value, int fromBase)
		{
			return Convert.ConvertFromBase64(value, fromBase, false);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F4A RID: 3914 RVA: 0x0003E24C File Offset: 0x0003C44C
		[CLSCompliant(false)]
		public static long ToInt64(uint value)
		{
			return (long)((ulong)value);
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F4B RID: 3915 RVA: 0x0003E250 File Offset: 0x0003C450
		[CLSCompliant(false)]
		public static long ToInt64(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than Int64.MaxValue"));
			}
			return (long)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F4C RID: 3916 RVA: 0x0003E280 File Offset: 0x0003C480
		[CLSCompliant(false)]
		public static long ToInt64(ushort value)
		{
			return (long)((ulong)value);
		}

		/// <summary>Converts the value of the specified object to a 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F4D RID: 3917 RVA: 0x0003E284 File Offset: 0x0003C484
		public static long ToInt64(object value)
		{
			if (value == null)
			{
				return 0L;
			}
			return Convert.ToInt64(value, null);
		}

		/// <summary>Converts the value of the specified object to a 64-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.-or-The conversion is not supported. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F4E RID: 3918 RVA: 0x0003E298 File Offset: 0x0003C498
		public static long ToInt64(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0L;
			}
			return ((IConvertible)value).ToInt64(provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent 8-bit signed integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F4F RID: 3919 RVA: 0x0003E2B0 File Offset: 0x0003C4B0
		[CLSCompliant(false)]
		public static sbyte ToSByte(bool value)
		{
			return (!value) ? 0 : 1;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F50 RID: 3920 RVA: 0x0003E2C0 File Offset: 0x0003C4C0
		[CLSCompliant(false)]
		public static sbyte ToSByte(byte value)
		{
			if (value > 127)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue"));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F51 RID: 3921 RVA: 0x0003E2DC File Offset: 0x0003C4DC
		[CLSCompliant(false)]
		public static sbyte ToSByte(char value)
		{
			if (value > '\u007f')
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue"));
			}
			return (sbyte)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F52 RID: 3922 RVA: 0x0003E2F8 File Offset: 0x0003C4F8
		[CLSCompliant(false)]
		public static sbyte ToSByte(DateTime value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 8-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F53 RID: 3923 RVA: 0x0003E304 File Offset: 0x0003C504
		[CLSCompliant(false)]
		public static sbyte ToSByte(decimal value)
		{
			if (value > 127m || value < -128m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue or less than SByte.MinValue"));
			}
			return (sbyte)Math.Round(value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 8-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F54 RID: 3924 RVA: 0x0003E350 File Offset: 0x0003C550
		[CLSCompliant(false)]
		public static sbyte ToSByte(double value)
		{
			if (value > 127.0 || value < -128.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue or less than SByte.MinValue"));
			}
			return (sbyte)Math.Round(value);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 8-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F55 RID: 3925 RVA: 0x0003E388 File Offset: 0x0003C588
		[CLSCompliant(false)]
		public static sbyte ToSByte(float value)
		{
			if (value > 127f || value < -128f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue or less than SByte.Minalue"));
			}
			return (sbyte)Math.Round((double)value);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F56 RID: 3926 RVA: 0x0003E3C4 File Offset: 0x0003C5C4
		[CLSCompliant(false)]
		public static sbyte ToSByte(int value)
		{
			if (value > 127 || value < -128)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue or less than SByte.MinValue"));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F57 RID: 3927 RVA: 0x0003E3F4 File Offset: 0x0003C5F4
		[CLSCompliant(false)]
		public static sbyte ToSByte(long value)
		{
			if (value > 127L || value < -128L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue or less than SByte.MinValue"));
			}
			return (sbyte)value;
		}

		/// <summary>Returns the specified 8-bit signed integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 8-bit signed integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F58 RID: 3928 RVA: 0x0003E428 File Offset: 0x0003C628
		[CLSCompliant(false)]
		public static sbyte ToSByte(sbyte value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to the equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F59 RID: 3929 RVA: 0x0003E42C File Offset: 0x0003C62C
		[CLSCompliant(false)]
		public static sbyte ToSByte(short value)
		{
			if (value > 127 || value < -128)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue or less than SByte.MinValue"));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if value is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F5A RID: 3930 RVA: 0x0003E45C File Offset: 0x0003C65C
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return sbyte.Parse(value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 8-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F5B RID: 3931 RVA: 0x0003E46C File Offset: 0x0003C66C
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return sbyte.Parse(value, provider);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F5C RID: 3932 RVA: 0x0003E488 File Offset: 0x0003C688
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value, int fromBase)
		{
			int num = Convert.ConvertFromBase(value, fromBase, false);
			if (fromBase != 10 && num > 127)
			{
				return Convert.ToSByte(-(256 - num));
			}
			return Convert.ToSByte(num);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F5D RID: 3933 RVA: 0x0003E4C4 File Offset: 0x0003C6C4
		[CLSCompliant(false)]
		public static sbyte ToSByte(uint value)
		{
			if ((ulong)value > 127UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue"));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F5E RID: 3934 RVA: 0x0003E4E4 File Offset: 0x0003C6E4
		[CLSCompliant(false)]
		public static sbyte ToSByte(ulong value)
		{
			if (value > 127UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue"));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F5F RID: 3935 RVA: 0x0003E504 File Offset: 0x0003C704
		[CLSCompliant(false)]
		public static sbyte ToSByte(ushort value)
		{
			if (value > 127)
			{
				throw new OverflowException(Locale.GetText("Value is greater than SByte.MaxValue"));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified object to an 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format. </exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F60 RID: 3936 RVA: 0x0003E520 File Offset: 0x0003C720
		[CLSCompliant(false)]
		public static sbyte ToSByte(object value)
		{
			if (value == null)
			{
				return 0;
			}
			return Convert.ToSByte(value, null);
		}

		/// <summary>Converts the value of the specified object to an 8-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format. </exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F61 RID: 3937 RVA: 0x0003E534 File Offset: 0x0003C734
		[CLSCompliant(false)]
		public static sbyte ToSByte(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ((IConvertible)value).ToSByte(provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent single-precision floating-point number.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F62 RID: 3938 RVA: 0x0003E54C File Offset: 0x0003C74C
		public static float ToSingle(bool value)
		{
			return (float)((!value) ? 0 : 1);
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F63 RID: 3939 RVA: 0x0003E55C File Offset: 0x0003C75C
		public static float ToSingle(byte value)
		{
			return (float)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F64 RID: 3940 RVA: 0x0003E560 File Offset: 0x0003C760
		public static float ToSingle(char value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F65 RID: 3941 RVA: 0x0003E56C File Offset: 0x0003C76C
		public static float ToSingle(DateTime value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.<paramref name="value" /> is rounded using rounding to nearest. For example, when rounded to two decimals, the value 2.345 becomes 2.34 and the value 2.355 becomes 2.36.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F66 RID: 3942 RVA: 0x0003E578 File Offset: 0x0003C778
		public static float ToSingle(decimal value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.<paramref name="value" /> is rounded using rounding to nearest. For example, when rounded to two decimals, the value 2.345 becomes 2.34 and the value 2.355 becomes 2.36.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F67 RID: 3943 RVA: 0x0003E580 File Offset: 0x0003C780
		public static float ToSingle(double value)
		{
			return (float)value;
		}

		/// <summary>Returns the specified single-precision floating-point number; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The single-precision floating-point number to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F68 RID: 3944 RVA: 0x0003E584 File Offset: 0x0003C784
		public static float ToSingle(float value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F69 RID: 3945 RVA: 0x0003E588 File Offset: 0x0003C788
		public static float ToSingle(int value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F6A RID: 3946 RVA: 0x0003E58C File Offset: 0x0003C78C
		public static float ToSingle(long value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent single-precision floating-point number.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F6B RID: 3947 RVA: 0x0003E590 File Offset: 0x0003C790
		[CLSCompliant(false)]
		public static float ToSingle(sbyte value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F6C RID: 3948 RVA: 0x0003E594 File Offset: 0x0003C794
		public static float ToSingle(short value)
		{
			return (float)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F6D RID: 3949 RVA: 0x0003E598 File Offset: 0x0003C798
		public static float ToSingle(string value)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent single-precision floating-point number, using the specified culture-specific formatting information.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F6E RID: 3950 RVA: 0x0003E5AC File Offset: 0x0003C7AC
		public static float ToSingle(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value, provider);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F6F RID: 3951 RVA: 0x0003E5C4 File Offset: 0x0003C7C4
		[CLSCompliant(false)]
		public static float ToSingle(uint value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F70 RID: 3952 RVA: 0x0003E5CC File Offset: 0x0003C7CC
		[CLSCompliant(false)]
		public static float ToSingle(ulong value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F71 RID: 3953 RVA: 0x0003E5D4 File Offset: 0x0003C7D4
		[CLSCompliant(false)]
		public static float ToSingle(ushort value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified object to a single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F72 RID: 3954 RVA: 0x0003E5D8 File Offset: 0x0003C7D8
		public static float ToSingle(object value)
		{
			if (value == null)
			{
				return 0f;
			}
			return Convert.ToSingle(value, null);
		}

		/// <summary>Converts the value of the specified object to an single-precision floating-point number, using the specified culture-specific formatting information.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F73 RID: 3955 RVA: 0x0003E5F0 File Offset: 0x0003C7F0
		public static float ToSingle(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0f;
			}
			return ((IConvertible)value).ToSingle(provider);
		}

		/// <summary>Converts the specified Boolean value to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F74 RID: 3956 RVA: 0x0003E60C File Offset: 0x0003C80C
		public static string ToString(bool value)
		{
			return value.ToString();
		}

		/// <summary>Converts the specified Boolean value to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <param name="provider">An instance of an object. This parameter is ignored.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F75 RID: 3957 RVA: 0x0003E618 File Offset: 0x0003C818
		public static string ToString(bool value, IFormatProvider provider)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F76 RID: 3958 RVA: 0x0003E624 File Offset: 0x0003C824
		public static string ToString(byte value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F77 RID: 3959 RVA: 0x0003E630 File Offset: 0x0003C830
		public static string ToString(byte value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of an 8-bit unsigned integer to its equivalent string representation in a specified base.</summary>
		/// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toBase" /> is not 2, 8, 10, or 16. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F78 RID: 3960 RVA: 0x0003E63C File Offset: 0x0003C83C
		public static string ToString(byte value, int toBase)
		{
			if (value == 0)
			{
				return "0";
			}
			if (toBase == 10)
			{
				return value.ToString();
			}
			byte[] bytes = BitConverter.GetBytes((short)value);
			if (toBase == 2)
			{
				return Convert.ConvertToBase2(bytes);
			}
			if (toBase == 8)
			{
				return Convert.ConvertToBase8(bytes);
			}
			if (toBase != 16)
			{
				throw new ArgumentException(Locale.GetText("toBase is not valid."));
			}
			return Convert.ConvertToBase16(bytes);
		}

		/// <summary>Converts the value of the specified Unicode character to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F79 RID: 3961 RVA: 0x0003E6B0 File Offset: 0x0003C8B0
		public static string ToString(char value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified Unicode character to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F7A RID: 3962 RVA: 0x0003E6BC File Offset: 0x0003C8BC
		public static string ToString(char value, IFormatProvider provider)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified <see cref="T:System.DateTime" /> to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F7B RID: 3963 RVA: 0x0003E6C8 File Offset: 0x0003C8C8
		public static string ToString(DateTime value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified <see cref="T:System.DateTime" /> to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F7C RID: 3964 RVA: 0x0003E6D4 File Offset: 0x0003C8D4
		public static string ToString(DateTime value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified decimal number to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F7D RID: 3965 RVA: 0x0003E6E0 File Offset: 0x0003C8E0
		public static string ToString(decimal value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified decimal number to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F7E RID: 3966 RVA: 0x0003E6EC File Offset: 0x0003C8EC
		public static string ToString(decimal value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F7F RID: 3967 RVA: 0x0003E6F8 File Offset: 0x0003C8F8
		public static string ToString(double value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F80 RID: 3968 RVA: 0x0003E704 File Offset: 0x0003C904
		public static string ToString(double value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F81 RID: 3969 RVA: 0x0003E710 File Offset: 0x0003C910
		public static string ToString(float value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F82 RID: 3970 RVA: 0x0003E71C File Offset: 0x0003C91C
		public static string ToString(float value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F83 RID: 3971 RVA: 0x0003E728 File Offset: 0x0003C928
		public static string ToString(int value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of a 32-bit signed integer to its equivalent string representation in a specified base.</summary>
		/// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toBase" /> is not 2, 8, 10, or 16. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F84 RID: 3972 RVA: 0x0003E734 File Offset: 0x0003C934
		public static string ToString(int value, int toBase)
		{
			if (value == 0)
			{
				return "0";
			}
			if (toBase == 10)
			{
				return value.ToString();
			}
			byte[] bytes = BitConverter.GetBytes(value);
			if (toBase == 2)
			{
				return Convert.ConvertToBase2(bytes);
			}
			if (toBase == 8)
			{
				return Convert.ConvertToBase8(bytes);
			}
			if (toBase != 16)
			{
				throw new ArgumentException(Locale.GetText("toBase is not valid."));
			}
			return Convert.ConvertToBase16(bytes);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F85 RID: 3973 RVA: 0x0003E7A8 File Offset: 0x0003C9A8
		public static string ToString(int value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F86 RID: 3974 RVA: 0x0003E7B4 File Offset: 0x0003C9B4
		public static string ToString(long value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of a 64-bit signed integer to its equivalent string representation in a specified base.</summary>
		/// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toBase" /> is not 2, 8, 10, or 16. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F87 RID: 3975 RVA: 0x0003E7C0 File Offset: 0x0003C9C0
		public static string ToString(long value, int toBase)
		{
			if (value == 0L)
			{
				return "0";
			}
			if (toBase == 10)
			{
				return value.ToString();
			}
			byte[] bytes = BitConverter.GetBytes(value);
			if (toBase == 2)
			{
				return Convert.ConvertToBase2(bytes);
			}
			if (toBase == 8)
			{
				return Convert.ConvertToBase8(bytes);
			}
			if (toBase != 16)
			{
				throw new ArgumentException(Locale.GetText("toBase is not valid."));
			}
			return Convert.ConvertToBase16(bytes);
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F88 RID: 3976 RVA: 0x0003E834 File Offset: 0x0003CA34
		public static string ToString(long value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified object to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />, or <see cref="F:System.String.Empty" /> if value is null.</returns>
		/// <param name="value">An object that supplies the value to convert, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F89 RID: 3977 RVA: 0x0003E840 File Offset: 0x0003CA40
		public static string ToString(object value)
		{
			return Convert.ToString(value, null);
		}

		/// <summary>Converts the value of the specified object to its equivalent string representation using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />, or <see cref="F:System.String.Empty" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that supplies the value to convert, or null. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F8A RID: 3978 RVA: 0x0003E84C File Offset: 0x0003CA4C
		public static string ToString(object value, IFormatProvider provider)
		{
			if (value is IConvertible)
			{
				return ((IConvertible)value).ToString(provider);
			}
			if (value != null)
			{
				return value.ToString();
			}
			return string.Empty;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F8B RID: 3979 RVA: 0x0003E884 File Offset: 0x0003CA84
		[CLSCompliant(false)]
		public static string ToString(sbyte value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F8C RID: 3980 RVA: 0x0003E890 File Offset: 0x0003CA90
		[CLSCompliant(false)]
		public static string ToString(sbyte value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F8D RID: 3981 RVA: 0x0003E89C File Offset: 0x0003CA9C
		public static string ToString(short value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of a 16-bit signed integer to its equivalent string representation in a specified base.</summary>
		/// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toBase" /> is not 2, 8, 10, or 16. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F8E RID: 3982 RVA: 0x0003E8A8 File Offset: 0x0003CAA8
		public static string ToString(short value, int toBase)
		{
			if (value == 0)
			{
				return "0";
			}
			if (toBase == 10)
			{
				return value.ToString();
			}
			byte[] bytes = BitConverter.GetBytes(value);
			if (toBase == 2)
			{
				return Convert.ConvertToBase2(bytes);
			}
			if (toBase == 8)
			{
				return Convert.ConvertToBase8(bytes);
			}
			if (toBase != 16)
			{
				throw new ArgumentException(Locale.GetText("toBase is not valid."));
			}
			return Convert.ConvertToBase16(bytes);
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F8F RID: 3983 RVA: 0x0003E91C File Offset: 0x0003CB1C
		public static string ToString(short value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Returns the specified string instance; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The string to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F90 RID: 3984 RVA: 0x0003E928 File Offset: 0x0003CB28
		public static string ToString(string value)
		{
			return value;
		}

		/// <summary>Returns the specified string instance; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The string to return. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. This parameter is ignored.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F91 RID: 3985 RVA: 0x0003E92C File Offset: 0x0003CB2C
		public static string ToString(string value, IFormatProvider provider)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F92 RID: 3986 RVA: 0x0003E930 File Offset: 0x0003CB30
		[CLSCompliant(false)]
		public static string ToString(uint value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F93 RID: 3987 RVA: 0x0003E93C File Offset: 0x0003CB3C
		[CLSCompliant(false)]
		public static string ToString(uint value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F94 RID: 3988 RVA: 0x0003E948 File Offset: 0x0003CB48
		[CLSCompliant(false)]
		public static string ToString(ulong value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F95 RID: 3989 RVA: 0x0003E954 File Offset: 0x0003CB54
		[CLSCompliant(false)]
		public static string ToString(ulong value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F96 RID: 3990 RVA: 0x0003E960 File Offset: 0x0003CB60
		[CLSCompliant(false)]
		public static string ToString(ushort value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F97 RID: 3991 RVA: 0x0003E96C File Offset: 0x0003CB6C
		[CLSCompliant(false)]
		public static string ToString(ushort value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent 16-bit unsigned integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F98 RID: 3992 RVA: 0x0003E978 File Offset: 0x0003CB78
		[CLSCompliant(false)]
		public static ushort ToUInt16(bool value)
		{
			return (!value) ? 0 : 1;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F99 RID: 3993 RVA: 0x0003E988 File Offset: 0x0003CB88
		[CLSCompliant(false)]
		public static ushort ToUInt16(byte value)
		{
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 16-bit unsigned integer.</summary>
		/// <returns>The 16-bit unsigned integer equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F9A RID: 3994 RVA: 0x0003E98C File Offset: 0x0003CB8C
		[CLSCompliant(false)]
		public static ushort ToUInt16(char value)
		{
			return (ushort)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F9B RID: 3995 RVA: 0x0003E990 File Offset: 0x0003CB90
		[CLSCompliant(false)]
		public static ushort ToUInt16(DateTime value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F9C RID: 3996 RVA: 0x0003E99C File Offset: 0x0003CB9C
		[CLSCompliant(false)]
		public static ushort ToUInt16(decimal value)
		{
			if (value > 65535m || value < 0m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue or less than UInt16.MinValue"));
			}
			return (ushort)Math.Round(value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F9D RID: 3997 RVA: 0x0003E9EC File Offset: 0x0003CBEC
		[CLSCompliant(false)]
		public static ushort ToUInt16(double value)
		{
			if (value > 65535.0 || value < 0.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue or less than UInt16.MinValue"));
			}
			return (ushort)Math.Round(value);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F9E RID: 3998 RVA: 0x0003EA24 File Offset: 0x0003CC24
		[CLSCompliant(false)]
		public static ushort ToUInt16(float value)
		{
			if (value > 65535f || value < 0f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue or less than UInt16.MinValue"));
			}
			return (ushort)Math.Round((double)value);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F9F RID: 3999 RVA: 0x0003EA60 File Offset: 0x0003CC60
		[CLSCompliant(false)]
		public static ushort ToUInt16(int value)
		{
			if (value > 65535 || value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue or less than UInt16.MinValue"));
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FA0 RID: 4000 RVA: 0x0003EA94 File Offset: 0x0003CC94
		[CLSCompliant(false)]
		public static ushort ToUInt16(long value)
		{
			if (value > 65535L || value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue or less than UInt16.MinValue"));
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FA1 RID: 4001 RVA: 0x0003EAC8 File Offset: 0x0003CCC8
		[CLSCompliant(false)]
		public static ushort ToUInt16(sbyte value)
		{
			if ((int)value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt16.MinValue"));
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to the equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FA2 RID: 4002 RVA: 0x0003EAE4 File Offset: 0x0003CCE4
		[CLSCompliant(false)]
		public static ushort ToUInt16(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt16.MinValue"));
			}
			return (ushort)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FA3 RID: 4003 RVA: 0x0003EB00 File Offset: 0x0003CD00
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 16-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FA4 RID: 4004 RVA: 0x0003EB10 File Offset: 0x0003CD10
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value, provider);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FA5 RID: 4005 RVA: 0x0003EB24 File Offset: 0x0003CD24
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value, int fromBase)
		{
			return Convert.ToUInt16(Convert.ConvertFromBase(value, fromBase, true));
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FA6 RID: 4006 RVA: 0x0003EB34 File Offset: 0x0003CD34
		[CLSCompliant(false)]
		public static ushort ToUInt16(uint value)
		{
			if (value > 65535U)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue"));
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003EB54 File Offset: 0x0003CD54
		[CLSCompliant(false)]
		public static ushort ToUInt16(ulong value)
		{
			if (value > 65535UL)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt16.MaxValue"));
			}
			return (ushort)value;
		}

		/// <summary>Returns the specified 16-bit unsigned integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 16-bit unsigned integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003EB74 File Offset: 0x0003CD74
		[CLSCompliant(false)]
		public static ushort ToUInt16(ushort value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified object to a 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the  <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FA9 RID: 4009 RVA: 0x0003EB78 File Offset: 0x0003CD78
		[CLSCompliant(false)]
		public static ushort ToUInt16(object value)
		{
			if (value == null)
			{
				return 0;
			}
			return Convert.ToUInt16(value, null);
		}

		/// <summary>Converts the value of the specified object to a 16-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the  <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FAA RID: 4010 RVA: 0x0003EB8C File Offset: 0x0003CD8C
		[CLSCompliant(false)]
		public static ushort ToUInt16(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ((IConvertible)value).ToUInt16(provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FAB RID: 4011 RVA: 0x0003EBA4 File Offset: 0x0003CDA4
		[CLSCompliant(false)]
		public static uint ToUInt32(bool value)
		{
			return (!value) ? 0U : 1U;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FAC RID: 4012 RVA: 0x0003EBB4 File Offset: 0x0003CDB4
		[CLSCompliant(false)]
		public static uint ToUInt32(byte value)
		{
			return (uint)value;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FAD RID: 4013 RVA: 0x0003EBB8 File Offset: 0x0003CDB8
		[CLSCompliant(false)]
		public static uint ToUInt32(char value)
		{
			return (uint)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FAE RID: 4014 RVA: 0x0003EBBC File Offset: 0x0003CDBC
		[CLSCompliant(false)]
		public static uint ToUInt32(DateTime value)
		{
			throw new InvalidCastException("This conversion is not supported.");
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FAF RID: 4015 RVA: 0x0003EBC8 File Offset: 0x0003CDC8
		[CLSCompliant(false)]
		public static uint ToUInt32(decimal value)
		{
			if (value > 4294967295m || value < 0m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt32.MaxValue or less than UInt32.MinValue"));
			}
			return (uint)Math.Round(value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FB0 RID: 4016 RVA: 0x0003EC18 File Offset: 0x0003CE18
		[CLSCompliant(false)]
		public static uint ToUInt32(double value)
		{
			if (value > 4294967295.0 || value < 0.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt32.MaxValue or less than UInt32.MinValue"));
			}
			return (uint)Math.Round(value);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FB1 RID: 4017 RVA: 0x0003EC50 File Offset: 0x0003CE50
		[CLSCompliant(false)]
		public static uint ToUInt32(float value)
		{
			if (value > 4.2949673E+09f || value < 0f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt32.MaxValue or less than UInt32.MinValue"));
			}
			return (uint)Math.Round((double)value);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FB2 RID: 4018 RVA: 0x0003EC8C File Offset: 0x0003CE8C
		[CLSCompliant(false)]
		public static uint ToUInt32(int value)
		{
			if ((long)value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt32.MinValue"));
			}
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FB3 RID: 4019 RVA: 0x0003ECA8 File Offset: 0x0003CEA8
		[CLSCompliant(false)]
		public static uint ToUInt32(long value)
		{
			if (value > (long)((ulong)-1) || value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt32.MaxValue or less than UInt32.MinValue"));
			}
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FB4 RID: 4020 RVA: 0x0003ECD8 File Offset: 0x0003CED8
		[CLSCompliant(false)]
		public static uint ToUInt32(sbyte value)
		{
			if ((long)value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt32.MinValue"));
			}
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FB5 RID: 4021 RVA: 0x0003ECF8 File Offset: 0x0003CEF8
		[CLSCompliant(false)]
		public static uint ToUInt32(short value)
		{
			if ((long)value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt32.MinValue"));
			}
			return (uint)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003ED18 File Offset: 0x0003CF18
		[CLSCompliant(false)]
		public static uint ToUInt32(string value)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 32-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003ED28 File Offset: 0x0003CF28
		[CLSCompliant(false)]
		public static uint ToUInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value, provider);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003ED3C File Offset: 0x0003CF3C
		[CLSCompliant(false)]
		public static uint ToUInt32(string value, int fromBase)
		{
			return (uint)Convert.ConvertFromBase(value, fromBase, true);
		}

		/// <summary>Returns the specified 32-bit unsigned integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 32-bit unsigned integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FB9 RID: 4025 RVA: 0x0003ED48 File Offset: 0x0003CF48
		[CLSCompliant(false)]
		public static uint ToUInt32(uint value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FBA RID: 4026 RVA: 0x0003ED4C File Offset: 0x0003CF4C
		[CLSCompliant(false)]
		public static uint ToUInt32(ulong value)
		{
			if (value > (ulong)-1)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt32.MaxValue"));
			}
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FBB RID: 4027 RVA: 0x0003ED68 File Offset: 0x0003CF68
		[CLSCompliant(false)]
		public static uint ToUInt32(ushort value)
		{
			return (uint)value;
		}

		/// <summary>Converts the value of the specified object to a 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FBC RID: 4028 RVA: 0x0003ED6C File Offset: 0x0003CF6C
		[CLSCompliant(false)]
		public static uint ToUInt32(object value)
		{
			if (value == null)
			{
				return 0U;
			}
			return Convert.ToUInt32(value, null);
		}

		/// <summary>Converts the value of the specified object to a 32-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FBD RID: 4029 RVA: 0x0003ED80 File Offset: 0x0003CF80
		[CLSCompliant(false)]
		public static uint ToUInt32(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0U;
			}
			return ((IConvertible)value).ToUInt32(provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FBE RID: 4030 RVA: 0x0003ED98 File Offset: 0x0003CF98
		[CLSCompliant(false)]
		public static ulong ToUInt64(bool value)
		{
			return (ulong)((!value) ? 0L : 1L);
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FBF RID: 4031 RVA: 0x0003EDA8 File Offset: 0x0003CFA8
		[CLSCompliant(false)]
		public static ulong ToUInt64(byte value)
		{
			return (ulong)value;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003EDAC File Offset: 0x0003CFAC
		[CLSCompliant(false)]
		public static ulong ToUInt64(char value)
		{
			return (ulong)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003EDB0 File Offset: 0x0003CFB0
		[CLSCompliant(false)]
		public static ulong ToUInt64(DateTime value)
		{
			throw new InvalidCastException("The conversion is not supported.");
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FC2 RID: 4034 RVA: 0x0003EDBC File Offset: 0x0003CFBC
		[CLSCompliant(false)]
		public static ulong ToUInt64(decimal value)
		{
			if (value > 18446744073709551615m || value < 0m)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt64.MaxValue or less than UInt64.MinValue"));
			}
			return (ulong)Math.Round(value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003EE0C File Offset: 0x0003D00C
		[CLSCompliant(false)]
		public static ulong ToUInt64(double value)
		{
			if (value > 1.8446744073709552E+19 || value < 0.0)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt64.MaxValue or less than UInt64.MinValue"));
			}
			return (ulong)Math.Round(value);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003EE44 File Offset: 0x0003D044
		[CLSCompliant(false)]
		public static ulong ToUInt64(float value)
		{
			if (value > 1.8446744E+19f || value < 0f)
			{
				throw new OverflowException(Locale.GetText("Value is greater than UInt64.MaxValue or less than UInt64.MinValue"));
			}
			return (ulong)Math.Round((double)value);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FC5 RID: 4037 RVA: 0x0003EE80 File Offset: 0x0003D080
		[CLSCompliant(false)]
		public static ulong ToUInt64(int value)
		{
			if (value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt64.MinValue"));
			}
			return (ulong)((long)value);
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FC6 RID: 4038 RVA: 0x0003EE9C File Offset: 0x0003D09C
		[CLSCompliant(false)]
		public static ulong ToUInt64(long value)
		{
			if (value < 0L)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt64.MinValue"));
			}
			return (ulong)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FC7 RID: 4039 RVA: 0x0003EEB8 File Offset: 0x0003D0B8
		[CLSCompliant(false)]
		public static ulong ToUInt64(sbyte value)
		{
			if ((int)value < 0)
			{
				throw new OverflowException("Value is less than UInt64.MinValue");
			}
			return (ulong)((long)value);
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FC8 RID: 4040 RVA: 0x0003EED0 File Offset: 0x0003D0D0
		[CLSCompliant(false)]
		public static ulong ToUInt64(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Locale.GetText("Value is less than UInt64.MinValue"));
			}
			return (ulong)((long)value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003EEEC File Offset: 0x0003D0EC
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 64-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FCA RID: 4042 RVA: 0x0003EF00 File Offset: 0x0003D100
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value, provider);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FCB RID: 4043 RVA: 0x0003EF14 File Offset: 0x0003D114
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value, int fromBase)
		{
			return (ulong)Convert.ConvertFromBase64(value, fromBase, true);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FCC RID: 4044 RVA: 0x0003EF20 File Offset: 0x0003D120
		[CLSCompliant(false)]
		public static ulong ToUInt64(uint value)
		{
			return (ulong)value;
		}

		/// <summary>Returns the specified 64-bit unsigned integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 64-bit unsigned integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FCD RID: 4045 RVA: 0x0003EF24 File Offset: 0x0003D124
		[CLSCompliant(false)]
		public static ulong ToUInt64(ulong value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FCE RID: 4046 RVA: 0x0003EF28 File Offset: 0x0003D128
		[CLSCompliant(false)]
		public static ulong ToUInt64(ushort value)
		{
			return (ulong)value;
		}

		/// <summary>Converts the value of the specified object to a 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FCF RID: 4047 RVA: 0x0003EF2C File Offset: 0x0003D12C
		[CLSCompliant(false)]
		public static ulong ToUInt64(object value)
		{
			if (value == null)
			{
				return 0UL;
			}
			return Convert.ToUInt64(value, null);
		}

		/// <summary>Converts the value of the specified object to a 64-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003EF40 File Offset: 0x0003D140
		[CLSCompliant(false)]
		public static ulong ToUInt64(object value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ((IConvertible)value).ToUInt64(provider);
		}

		/// <summary>Returns an object of the specified type and whose value is equivalent to the specified object.</summary>
		/// <returns>An object whose type is <paramref name="conversionType" /> and whose value is equivalent to <paramref name="value" />.-or-A null reference (Nothing in Visual Basic), if <paramref name="value" /> is null and <paramref name="conversionType" /> is not a value type. </returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="conversionType">The type of object to return. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.  -or-<paramref name="value" /> is null and <paramref name="conversionType" /> is a value type.-or-<paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in a format recognized by <paramref name="conversionType" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is out of the range of <paramref name="conversionType" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="conversionType" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003EF58 File Offset: 0x0003D158
		public static object ChangeType(object value, Type conversionType)
		{
			if (value != null && conversionType == null)
			{
				throw new ArgumentNullException("conversionType");
			}
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			IFormatProvider provider;
			if (conversionType == typeof(DateTime))
			{
				provider = currentCulture.DateTimeFormat;
			}
			else
			{
				provider = currentCulture.NumberFormat;
			}
			return Convert.ToType(value, conversionType, provider, true);
		}

		/// <summary>Returns an object of the specified type whose value is equivalent to the specified object.</summary>
		/// <returns>An object whose underlying type is <paramref name="typeCode" /> and whose value is equivalent to <paramref name="value" />.-or-A null reference (Nothing in Visual Basic), if <paramref name="value" /> is null and <paramref name="typeCode" /> is <see cref="F:System.TypeCode.Empty" />, <see cref="F:System.TypeCode.String" />, or <see cref="F:System.TypeCode.Object" />.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="typeCode">The type of object to return. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.  -or-<paramref name="value" /> is null and <paramref name="typeCode" /> specifies a value type.-or-<paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in a format recognized by the <paramref name="typeCode" /> type.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is out of the range of the <paramref name="typeCode" /> type.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeCode" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003EFB0 File Offset: 0x0003D1B0
		public static object ChangeType(object value, TypeCode typeCode)
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			Type type = Convert.conversionTable[(int)typeCode];
			IFormatProvider provider;
			if (type == typeof(DateTime))
			{
				provider = currentCulture.DateTimeFormat;
			}
			else
			{
				provider = currentCulture.NumberFormat;
			}
			return Convert.ToType(value, type, provider, true);
		}

		/// <summary>Returns an object of the specified type whose value is equivalent to the specified object. A parameter supplies culture-specific formatting information.</summary>
		/// <returns>An object whose type is <paramref name="conversionType" /> and whose value is equivalent to <paramref name="value" />.-or- <paramref name="value" />, if the <see cref="T:System.Type" /> of <paramref name="value" /> and <paramref name="conversionType" /> are equal.-or- A null reference (Nothing in Visual Basic), if <paramref name="value" /> is null and <paramref name="conversionType" /> is not a value type.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="conversionType">The type of object to return. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. -or-<paramref name="value" /> is null and <paramref name="conversionType" /> is a value type.-or-<paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in a format for <paramref name="conversionType" /> recognized by <paramref name="provider" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is out of the range of <paramref name="conversionType" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="conversionType" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003EFF8 File Offset: 0x0003D1F8
		public static object ChangeType(object value, Type conversionType, IFormatProvider provider)
		{
			if (value != null && conversionType == null)
			{
				throw new ArgumentNullException("conversionType");
			}
			return Convert.ToType(value, conversionType, provider, true);
		}

		/// <summary>Returns an object of the specified type whose value is equivalent to the specified object. A parameter supplies culture-specific formatting information.</summary>
		/// <returns>An object whose underlying type is <paramref name="typeCode" /> and whose value is equivalent to <paramref name="value" />.-or- A null reference (Nothing in Visual Basic), if <paramref name="value" /> is null and <paramref name="typeCode" /> is <see cref="F:System.TypeCode.Empty" />, <see cref="F:System.TypeCode.String" />, or <see cref="F:System.TypeCode.Object" />.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="typeCode">The type of object to return. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.  -or-<paramref name="value" /> is null and <paramref name="typeCode" /> specifies a value type.-or-<paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in a format for the <paramref name="typeCode" /> type recognized by <paramref name="provider" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is out of the range of the <paramref name="typeCode" /> type.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeCode" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003F028 File Offset: 0x0003D228
		public static object ChangeType(object value, TypeCode typeCode, IFormatProvider provider)
		{
			Type conversionType = Convert.conversionTable[(int)typeCode];
			return Convert.ToType(value, conversionType, provider, true);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0003F048 File Offset: 0x0003D248
		private static bool NotValidBase(int value)
		{
			return value != 2 && value != 8 && value != 10 && value != 16;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0003F06C File Offset: 0x0003D26C
		private static int ConvertFromBase(string value, int fromBase, bool unsigned)
		{
			if (Convert.NotValidBase(fromBase))
			{
				throw new ArgumentException("fromBase is not valid.");
			}
			if (value == null)
			{
				return 0;
			}
			int num = 0;
			int num2 = 0;
			int i = 0;
			int length = value.Length;
			bool flag = false;
			if (fromBase != 10)
			{
				if (fromBase != 16)
				{
					if (value.Substring(i, 1) == "-")
					{
						throw new ArgumentException("String cannot contain a minus sign if the base is not 10.");
					}
				}
				else
				{
					if (value.Substring(i, 1) == "-")
					{
						throw new ArgumentException("String cannot contain a minus sign if the base is not 10.");
					}
					if (length >= i + 2 && value[i] == '0' && (value[i + 1] == 'x' || value[i + 1] == 'X'))
					{
						i += 2;
					}
				}
			}
			else if (value.Substring(i, 1) == "-")
			{
				if (unsigned)
				{
					throw new OverflowException(Locale.GetText("The string was being parsed as an unsigned number and could not have a negative sign."));
				}
				flag = true;
				i++;
			}
			if (length == i)
			{
				throw new FormatException("Could not find any parsable digits.");
			}
			if (value[i] == '+')
			{
				i++;
			}
			while (i < length)
			{
				char c = value[i++];
				int num3;
				if (char.IsNumber(c))
				{
					num3 = (int)(c - '0');
				}
				else if (char.IsLetter(c))
				{
					num3 = (int)(char.ToLowerInvariant(c) - 'a' + '\n');
				}
				else
				{
					if (num > 0)
					{
						throw new FormatException("Additional unparsable characters are at the end of the string.");
					}
					throw new FormatException("Could not find any parsable digits.");
				}
				if (num3 >= fromBase)
				{
					if (num > 0)
					{
						throw new FormatException("Additional unparsable characters are at the end of the string.");
					}
					throw new FormatException("Could not find any parsable digits.");
				}
				else
				{
					num2 = fromBase * num2 + num3;
					num++;
				}
			}
			if (num == 0)
			{
				throw new FormatException("Could not find any parsable digits.");
			}
			if (flag)
			{
				return -num2;
			}
			return num2;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0003F264 File Offset: 0x0003D464
		private static long ConvertFromBase64(string value, int fromBase, bool unsigned)
		{
			if (Convert.NotValidBase(fromBase))
			{
				throw new ArgumentException("fromBase is not valid.");
			}
			if (value == null)
			{
				return 0L;
			}
			int num = 0;
			long num2 = 0L;
			bool flag = false;
			int i = 0;
			int length = value.Length;
			if (fromBase != 10)
			{
				if (fromBase != 16)
				{
					if (value.Substring(i, 1) == "-")
					{
						throw new ArgumentException("String cannot contain a minus sign if the base is not 10.");
					}
				}
				else
				{
					if (value.Substring(i, 1) == "-")
					{
						throw new ArgumentException("String cannot contain a minus sign if the base is not 10.");
					}
					if (length >= i + 2 && value[i] == '0' && (value[i + 1] == 'x' || value[i + 1] == 'X'))
					{
						i += 2;
					}
				}
			}
			else if (value.Substring(i, 1) == "-")
			{
				if (unsigned)
				{
					throw new OverflowException(Locale.GetText("The string was being parsed as an unsigned number and could not have a negative sign."));
				}
				flag = true;
				i++;
			}
			if (length == i)
			{
				throw new FormatException("Could not find any parsable digits.");
			}
			if (value[i] == '+')
			{
				i++;
			}
			while (i < length)
			{
				char c = value[i++];
				int num3;
				if (char.IsNumber(c))
				{
					num3 = (int)(c - '0');
				}
				else if (char.IsLetter(c))
				{
					num3 = (int)(char.ToLowerInvariant(c) - 'a' + '\n');
				}
				else
				{
					if (num > 0)
					{
						throw new FormatException("Additional unparsable characters are at the end of the string.");
					}
					throw new FormatException("Could not find any parsable digits.");
				}
				if (num3 >= fromBase)
				{
					if (num > 0)
					{
						throw new FormatException("Additional unparsable characters are at the end of the string.");
					}
					throw new FormatException("Could not find any parsable digits.");
				}
				else
				{
					num2 = (long)fromBase * num2 + (long)num3;
					num++;
				}
			}
			if (num == 0)
			{
				throw new FormatException("Could not find any parsable digits.");
			}
			if (flag)
			{
				return -1L * num2;
			}
			return num2;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003F474 File Offset: 0x0003D674
		private static void EndianSwap(ref byte[] value)
		{
			byte[] array = new byte[value.Length];
			for (int i = 0; i < value.Length; i++)
			{
				array[i] = value[value.Length - 1 - i];
			}
			value = array;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0003F4B4 File Offset: 0x0003D6B4
		private static string ConvertToBase2(byte[] value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				Convert.EndianSwap(ref value);
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = value.Length - 1; i >= 0; i--)
			{
				byte b = value[i];
				for (int j = 0; j < 8; j++)
				{
					if ((b & 128) == 128)
					{
						stringBuilder.Append('1');
					}
					else if (stringBuilder.Length > 0)
					{
						stringBuilder.Append('0');
					}
					b = (byte)(b << 1);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0003F544 File Offset: 0x0003D744
		private static string ConvertToBase8(byte[] value)
		{
			switch (value.Length)
			{
			case 1:
			{
				ulong num = (ulong)value[0];
				goto IL_74;
			}
			case 2:
			{
				ulong num = (ulong)BitConverter.ToUInt16(value, 0);
				goto IL_74;
			}
			case 4:
			{
				ulong num = (ulong)BitConverter.ToUInt32(value, 0);
				goto IL_74;
			}
			case 8:
			{
				ulong num = BitConverter.ToUInt64(value, 0);
				goto IL_74;
			}
			}
			throw new ArgumentException("value");
			IL_74:
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 21; i >= 0; i--)
			{
				ulong num;
				char c = (char)(num >> i * 3 & 7UL);
				if (c != '\0' || stringBuilder.Length > 0)
				{
					c += '0';
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0003F614 File Offset: 0x0003D814
		private static string ConvertToBase16(byte[] value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				Convert.EndianSwap(ref value);
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = value.Length - 1; i >= 0; i--)
			{
				char c = (char)(value[i] >> 4 & 15);
				if (c != '\0' || stringBuilder.Length > 0)
				{
					if (c < '\n')
					{
						c += '0';
					}
					else
					{
						c -= '\n';
						c += 'a';
					}
					stringBuilder.Append(c);
				}
				char c2 = (char)(value[i] & 15);
				if (c2 != '\0' || stringBuilder.Length > 0)
				{
					if (c2 < '\n')
					{
						c2 += '0';
					}
					else
					{
						c2 -= '\n';
						c2 += 'a';
					}
					stringBuilder.Append(c2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0003F6D8 File Offset: 0x0003D8D8
		internal static object ToType(object value, Type conversionType, IFormatProvider provider, bool try_target_to_type)
		{
			if (value == null)
			{
				if (conversionType != null && conversionType.IsValueType)
				{
					throw new InvalidCastException("Null object can not be converted to a value type.");
				}
				return null;
			}
			else
			{
				if (conversionType == null)
				{
					throw new InvalidCastException("Cannot cast to destination type.");
				}
				if (value.GetType() == conversionType)
				{
					return value;
				}
				if (value is IConvertible)
				{
					IConvertible convertible = (IConvertible)value;
					if (conversionType == Convert.conversionTable[0])
					{
						throw new ArgumentNullException();
					}
					if (conversionType == Convert.conversionTable[1])
					{
						return value;
					}
					if (conversionType == Convert.conversionTable[2])
					{
						throw new InvalidCastException("Cannot cast to DBNull, it's not IConvertible");
					}
					if (conversionType == Convert.conversionTable[3])
					{
						return convertible.ToBoolean(provider);
					}
					if (conversionType == Convert.conversionTable[4])
					{
						return convertible.ToChar(provider);
					}
					if (conversionType == Convert.conversionTable[5])
					{
						return convertible.ToSByte(provider);
					}
					if (conversionType == Convert.conversionTable[6])
					{
						return convertible.ToByte(provider);
					}
					if (conversionType == Convert.conversionTable[7])
					{
						return convertible.ToInt16(provider);
					}
					if (conversionType == Convert.conversionTable[8])
					{
						return convertible.ToUInt16(provider);
					}
					if (conversionType == Convert.conversionTable[9])
					{
						return convertible.ToInt32(provider);
					}
					if (conversionType == Convert.conversionTable[10])
					{
						return convertible.ToUInt32(provider);
					}
					if (conversionType == Convert.conversionTable[11])
					{
						return convertible.ToInt64(provider);
					}
					if (conversionType == Convert.conversionTable[12])
					{
						return convertible.ToUInt64(provider);
					}
					if (conversionType == Convert.conversionTable[13])
					{
						return convertible.ToSingle(provider);
					}
					if (conversionType == Convert.conversionTable[14])
					{
						return convertible.ToDouble(provider);
					}
					if (conversionType == Convert.conversionTable[15])
					{
						return convertible.ToDecimal(provider);
					}
					if (conversionType == Convert.conversionTable[16])
					{
						return convertible.ToDateTime(provider);
					}
					if (conversionType == Convert.conversionTable[18])
					{
						return convertible.ToString(provider);
					}
					if (try_target_to_type)
					{
						return convertible.ToType(conversionType, provider);
					}
				}
				throw new InvalidCastException(Locale.GetText("Value is not a convertible object: " + value.GetType().ToString() + " to " + conversionType.FullName));
			}
		}

		// Token: 0x04000490 RID: 1168
		private const int MaxBytesPerLine = 57;

		/// <summary>A constant that represents a database column that is absent of data; that is, database null.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000491 RID: 1169
		public static readonly object DBNull = System.DBNull.Value;

		// Token: 0x04000492 RID: 1170
		private static readonly Type[] conversionTable = new Type[]
		{
			null,
			typeof(object),
			typeof(DBNull),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			null,
			typeof(string)
		};
	}
}
