using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x020001BF RID: 447
	public class UInt16Converter : BaseNumberConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.UInt16Converter" /> class. </summary>
		// Token: 0x06000FBC RID: 4028 RVA: 0x000296C8 File Offset: 0x000278C8
		public UInt16Converter()
		{
			this.InnerType = typeof(ushort);
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x000296E0 File Offset: 0x000278E0
		internal override bool SupportHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000296E4 File Offset: 0x000278E4
		internal override string ConvertToString(object value, NumberFormatInfo format)
		{
			return ((ushort)value).ToString("G", format);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00029708 File Offset: 0x00027908
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return ushort.Parse(value, NumberStyles.Integer, format);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00029718 File Offset: 0x00027918
		internal override object ConvertFromString(string value, int fromBase)
		{
			return Convert.ToUInt16(value, fromBase);
		}
	}
}
