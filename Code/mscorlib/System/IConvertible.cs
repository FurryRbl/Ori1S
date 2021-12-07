using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Defines methods that convert the value of the implementing reference or value type to a common language runtime type that has an equivalent value.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000008 RID: 8
	[ComVisible(true)]
	[CLSCompliant(false)]
	public interface IConvertible
	{
		/// <summary>Returns the <see cref="T:System.TypeCode" /> for this instance.</summary>
		/// <returns>The enumerated constant that is the <see cref="T:System.TypeCode" /> of the class or value type that implements this interface.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000070 RID: 112
		TypeCode GetTypeCode();

		/// <summary>Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.</summary>
		/// <returns>A Boolean value equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000071 RID: 113
		bool ToBoolean(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 8-bit unsigned integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000072 RID: 114
		byte ToByte(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.</summary>
		/// <returns>A Unicode character equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000073 RID: 115
		char ToChar(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent <see cref="T:System.DateTime" /> using the specified culture-specific formatting information.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000074 RID: 116
		DateTime ToDateTime(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent <see cref="T:System.Decimal" /> number using the specified culture-specific formatting information.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000075 RID: 117
		decimal ToDecimal(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting information.</summary>
		/// <returns>A double-precision floating-point number equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000076 RID: 118
		double ToDouble(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 16-bit signed integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000077 RID: 119
		short ToInt16(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 32-bit signed integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000078 RID: 120
		int ToInt32(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 64-bit signed integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000079 RID: 121
		long ToInt64(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 8-bit signed integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600007A RID: 122
		sbyte ToSByte(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.</summary>
		/// <returns>A single-precision floating-point number equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600007B RID: 123
		float ToSingle(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent <see cref="T:System.String" /> using the specified culture-specific formatting information.</summary>
		/// <returns>A <see cref="T:System.String" /> instance equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600007C RID: 124
		string ToString(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an <see cref="T:System.Object" /> of the specified <see cref="T:System.Type" /> that has an equivalent value, using the specified culture-specific formatting information.</summary>
		/// <returns>An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" /> whose value is equivalent to the value of this instance.</returns>
		/// <param name="conversionType">The <see cref="T:System.Type" /> to which the value of this instance is converted. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600007D RID: 125
		object ToType(Type conversionType, IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 16-bit unsigned integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600007E RID: 126
		ushort ToUInt16(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 32-bit unsigned integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600007F RID: 127
		uint ToUInt32(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.</summary>
		/// <returns>An 64-bit unsigned integer equivalent to the value of this instance.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000080 RID: 128
		ulong ToUInt64(IFormatProvider provider);
	}
}
