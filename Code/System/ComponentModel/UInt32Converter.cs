using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x020001C0 RID: 448
	public class UInt32Converter : BaseNumberConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.UInt32Converter" /> class. </summary>
		// Token: 0x06000FC1 RID: 4033 RVA: 0x00029728 File Offset: 0x00027928
		public UInt32Converter()
		{
			this.InnerType = typeof(uint);
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00029740 File Offset: 0x00027940
		internal override bool SupportHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00029744 File Offset: 0x00027944
		internal override string ConvertToString(object value, NumberFormatInfo format)
		{
			return ((uint)value).ToString("G", format);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00029768 File Offset: 0x00027968
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return uint.Parse(value, NumberStyles.Integer, format);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00029778 File Offset: 0x00027978
		internal override object ConvertFromString(string value, int fromBase)
		{
			return Convert.ToUInt32(value, fromBase);
		}
	}
}
