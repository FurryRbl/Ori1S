using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit signed integer objects to and from other representations.</summary>
	// Token: 0x02000167 RID: 359
	public class Int16Converter : BaseNumberConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Int16Converter" /> class. </summary>
		// Token: 0x06000CB4 RID: 3252 RVA: 0x000203D8 File Offset: 0x0001E5D8
		public Int16Converter()
		{
			this.InnerType = typeof(short);
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x000203F0 File Offset: 0x0001E5F0
		internal override bool SupportHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x000203F4 File Offset: 0x0001E5F4
		internal override string ConvertToString(object value, NumberFormatInfo format)
		{
			return ((short)value).ToString("G", format);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00020418 File Offset: 0x0001E618
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return short.Parse(value, NumberStyles.Integer, format);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00020428 File Offset: 0x0001E628
		internal override object ConvertFromString(string value, int fromBase)
		{
			return Convert.ToInt16(value, fromBase);
		}
	}
}
