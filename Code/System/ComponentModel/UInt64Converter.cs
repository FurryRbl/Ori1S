using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x020001C1 RID: 449
	public class UInt64Converter : BaseNumberConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.UInt64Converter" /> class. </summary>
		// Token: 0x06000FC6 RID: 4038 RVA: 0x00029788 File Offset: 0x00027988
		public UInt64Converter()
		{
			this.InnerType = typeof(ulong);
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x000297A0 File Offset: 0x000279A0
		internal override bool SupportHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x000297A4 File Offset: 0x000279A4
		internal override string ConvertToString(object value, NumberFormatInfo format)
		{
			return ((ulong)value).ToString("G", format);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x000297C8 File Offset: 0x000279C8
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return ulong.Parse(value, NumberStyles.Integer, format);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x000297D8 File Offset: 0x000279D8
		internal override object ConvertFromString(string value, int fromBase)
		{
			return Convert.ToUInt64(value, fromBase);
		}
	}
}
