using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the type of an object.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018D RID: 397
	[ComVisible(true)]
	[Serializable]
	public enum TypeCode
	{
		/// <summary>A null reference.</summary>
		// Token: 0x040007EA RID: 2026
		Empty,
		/// <summary>A general type representing any reference or value type not explicitly represented by another TypeCode.</summary>
		// Token: 0x040007EB RID: 2027
		Object,
		/// <summary>A database null (column) value.</summary>
		// Token: 0x040007EC RID: 2028
		DBNull,
		/// <summary>A simple type representing Boolean values of true or false.</summary>
		// Token: 0x040007ED RID: 2029
		Boolean,
		/// <summary>An integral type representing unsigned 16-bit integers with values between 0 and 65535. The set of possible values for the <see cref="F:System.TypeCode.Char" /> type corresponds to the Unicode character set.</summary>
		// Token: 0x040007EE RID: 2030
		Char,
		/// <summary>An integral type representing signed 8-bit integers with values between -128 and 127.</summary>
		// Token: 0x040007EF RID: 2031
		SByte,
		/// <summary>An integral type representing unsigned 8-bit integers with values between 0 and 255.</summary>
		// Token: 0x040007F0 RID: 2032
		Byte,
		/// <summary>An integral type representing signed 16-bit integers with values between -32768 and 32767.</summary>
		// Token: 0x040007F1 RID: 2033
		Int16,
		/// <summary>An integral type representing unsigned 16-bit integers with values between 0 and 65535.</summary>
		// Token: 0x040007F2 RID: 2034
		UInt16,
		/// <summary>An integral type representing signed 32-bit integers with values between -2147483648 and 2147483647.</summary>
		// Token: 0x040007F3 RID: 2035
		Int32,
		/// <summary>An integral type representing unsigned 32-bit integers with values between 0 and 4294967295.</summary>
		// Token: 0x040007F4 RID: 2036
		UInt32,
		/// <summary>An integral type representing signed 64-bit integers with values between -9223372036854775808 and 9223372036854775807.</summary>
		// Token: 0x040007F5 RID: 2037
		Int64,
		/// <summary>An integral type representing unsigned 64-bit integers with values between 0 and 18446744073709551615.</summary>
		// Token: 0x040007F6 RID: 2038
		UInt64,
		/// <summary>A floating point type representing values ranging from approximately 1.5 x 10 -45 to 3.4 x 10 38 with a precision of 7 digits.</summary>
		// Token: 0x040007F7 RID: 2039
		Single,
		/// <summary>A floating point type representing values ranging from approximately 5.0 x 10 -324 to 1.7 x 10 308 with a precision of 15-16 digits.</summary>
		// Token: 0x040007F8 RID: 2040
		Double,
		/// <summary>A simple type representing values ranging from 1.0 x 10 -28 to approximately 7.9 x 10 28 with 28-29 significant digits.</summary>
		// Token: 0x040007F9 RID: 2041
		Decimal,
		/// <summary>A type representing a date and time value.</summary>
		// Token: 0x040007FA RID: 2042
		DateTime,
		/// <summary>A sealed class type representing Unicode character strings.</summary>
		// Token: 0x040007FB RID: 2043
		String = 18
	}
}
