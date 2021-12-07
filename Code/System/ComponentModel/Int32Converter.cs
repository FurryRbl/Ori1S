using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit signed integer objects to and from other representations.</summary>
	// Token: 0x02000168 RID: 360
	public class Int32Converter : BaseNumberConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Int32Converter" /> class. </summary>
		// Token: 0x06000CB9 RID: 3257 RVA: 0x00020438 File Offset: 0x0001E638
		public Int32Converter()
		{
			this.InnerType = typeof(int);
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00020450 File Offset: 0x0001E650
		internal override bool SupportHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00020454 File Offset: 0x0001E654
		internal override string ConvertToString(object value, NumberFormatInfo format)
		{
			return ((int)value).ToString("G", format);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00020478 File Offset: 0x0001E678
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return int.Parse(value, NumberStyles.Integer, format);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00020488 File Offset: 0x0001E688
		internal override object ConvertFromString(string value, int fromBase)
		{
			return Convert.ToInt32(value, fromBase);
		}
	}
}
