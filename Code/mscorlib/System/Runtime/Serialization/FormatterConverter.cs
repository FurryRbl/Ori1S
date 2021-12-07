using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Represents a base implementation of the <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> interface that uses the <see cref="T:System.Convert" /> class and the <see cref="T:System.IConvertible" /> interface.</summary>
	// Token: 0x020004EF RID: 1263
	[ComVisible(true)]
	public class FormatterConverter : IFormatterConverter
	{
		/// <summary>Converts a value to the given <see cref="T:System.Type" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <param name="type">The <see cref="T:System.Type" /> into which <paramref name="value" /> is converted. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032C0 RID: 12992 RVA: 0x000A47CC File Offset: 0x000A29CC
		public object Convert(object value, Type type)
		{
			return System.Convert.ChangeType(value, type);
		}

		/// <summary>Converts a value to the given <see cref="T:System.TypeCode" />.</summary>
		/// <returns>The converted <paramref name="value" />, or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <param name="typeCode">The <see cref="T:System.TypeCode" /> into which <paramref name="value" /> is converted. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032C1 RID: 12993 RVA: 0x000A47D8 File Offset: 0x000A29D8
		public object Convert(object value, TypeCode typeCode)
		{
			return System.Convert.ChangeType(value, typeCode);
		}

		/// <summary>Converts a value to a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032C2 RID: 12994 RVA: 0x000A47E4 File Offset: 0x000A29E4
		public bool ToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToBoolean(value);
		}

		/// <summary>Converts a value to an 8-bit unsigned integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032C3 RID: 12995 RVA: 0x000A4800 File Offset: 0x000A2A00
		public byte ToByte(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToByte(value);
		}

		/// <summary>Converts a value to a Unicode character.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032C4 RID: 12996 RVA: 0x000A481C File Offset: 0x000A2A1C
		public char ToChar(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToChar(value);
		}

		/// <summary>Converts a value to a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032C5 RID: 12997 RVA: 0x000A4838 File Offset: 0x000A2A38
		public DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToDateTime(value);
		}

		/// <summary>Converts a value to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032C6 RID: 12998 RVA: 0x000A4854 File Offset: 0x000A2A54
		public decimal ToDecimal(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToDecimal(value);
		}

		/// <summary>Converts a value to a double-precision floating-point number.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032C7 RID: 12999 RVA: 0x000A4870 File Offset: 0x000A2A70
		public double ToDouble(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToDouble(value);
		}

		/// <summary>Converts a value to a 16-bit signed integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032C8 RID: 13000 RVA: 0x000A488C File Offset: 0x000A2A8C
		public short ToInt16(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToInt16(value);
		}

		/// <summary>Converts a value to a 32-bit signed integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032C9 RID: 13001 RVA: 0x000A48A8 File Offset: 0x000A2AA8
		public int ToInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToInt32(value);
		}

		/// <summary>Converts a value to a 64-bit signed integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032CA RID: 13002 RVA: 0x000A48C4 File Offset: 0x000A2AC4
		public long ToInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToInt64(value);
		}

		/// <summary>Converts a value to a single-precision floating-point number.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032CB RID: 13003 RVA: 0x000A48E0 File Offset: 0x000A2AE0
		public float ToSingle(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToSingle(value);
		}

		/// <summary>Converts the specified object to a <see cref="T:System.String" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032CC RID: 13004 RVA: 0x000A48FC File Offset: 0x000A2AFC
		public string ToString(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToString(value);
		}

		/// <summary>Converts a value to a <see cref="T:System.SByte" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032CD RID: 13005 RVA: 0x000A4918 File Offset: 0x000A2B18
		[CLSCompliant(false)]
		public sbyte ToSByte(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToSByte(value);
		}

		/// <summary>Converts a value to a 16-bit unsigned integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032CE RID: 13006 RVA: 0x000A4934 File Offset: 0x000A2B34
		[CLSCompliant(false)]
		public ushort ToUInt16(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToUInt16(value);
		}

		/// <summary>Converts a value to a 32-bit unsigned integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032CF RID: 13007 RVA: 0x000A4950 File Offset: 0x000A2B50
		[CLSCompliant(false)]
		public uint ToUInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToUInt32(value);
		}

		/// <summary>Converts a value to a 64-bit unsigned integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060032D0 RID: 13008 RVA: 0x000A496C File Offset: 0x000A2B6C
		[CLSCompliant(false)]
		public ulong ToUInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value is null.");
			}
			return System.Convert.ToUInt64(value);
		}
	}
}
