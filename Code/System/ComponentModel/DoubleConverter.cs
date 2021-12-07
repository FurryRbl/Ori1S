using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert double-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x02000140 RID: 320
	public class DoubleConverter : BaseNumberConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DoubleConverter" /> class. </summary>
		// Token: 0x06000BD6 RID: 3030 RVA: 0x0001F008 File Offset: 0x0001D208
		public DoubleConverter()
		{
			this.InnerType = typeof(double);
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0001F020 File Offset: 0x0001D220
		internal override bool SupportHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0001F024 File Offset: 0x0001D224
		internal override string ConvertToString(object value, NumberFormatInfo format)
		{
			return ((double)value).ToString("R", format);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0001F048 File Offset: 0x0001D248
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return double.Parse(value, NumberStyles.Float, format);
		}
	}
}
