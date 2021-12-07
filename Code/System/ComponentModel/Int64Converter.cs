using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit signed integer objects to and from various other representations.</summary>
	// Token: 0x02000169 RID: 361
	public class Int64Converter : BaseNumberConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Int64Converter" /> class. </summary>
		// Token: 0x06000CBE RID: 3262 RVA: 0x00020498 File Offset: 0x0001E698
		public Int64Converter()
		{
			this.InnerType = typeof(long);
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x000204B0 File Offset: 0x0001E6B0
		internal override bool SupportHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000204B4 File Offset: 0x0001E6B4
		internal override string ConvertToString(object value, NumberFormatInfo format)
		{
			return ((long)value).ToString("G", format);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000204D8 File Offset: 0x0001E6D8
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return long.Parse(value, NumberStyles.Integer, format);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000204E8 File Offset: 0x0001E6E8
		internal override object ConvertFromString(string value, int fromBase)
		{
			return Convert.ToInt64(value, fromBase);
		}
	}
}
