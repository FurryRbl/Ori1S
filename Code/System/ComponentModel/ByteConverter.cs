using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x020000D4 RID: 212
	public class ByteConverter : BaseNumberConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ByteConverter" /> class. </summary>
		// Token: 0x0600092E RID: 2350 RVA: 0x0001AA50 File Offset: 0x00018C50
		public ByteConverter()
		{
			this.InnerType = typeof(byte);
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0001AA68 File Offset: 0x00018C68
		internal override bool SupportHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001AA6C File Offset: 0x00018C6C
		internal override string ConvertToString(object value, NumberFormatInfo format)
		{
			return ((byte)value).ToString("G", format);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001AA90 File Offset: 0x00018C90
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return byte.Parse(value, NumberStyles.Integer, format);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001AAA0 File Offset: 0x00018CA0
		internal override object ConvertFromString(string value, int fromBase)
		{
			return Convert.ToByte(value, fromBase);
		}
	}
}
