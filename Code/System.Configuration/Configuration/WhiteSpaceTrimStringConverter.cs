using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	/// <summary>Converts a string to its canonical format. </summary>
	// Token: 0x02000081 RID: 129
	public sealed class WhiteSpaceTrimStringConverter : ConfigurationConverterBase
	{
		/// <summary>Converts a <see cref="T:System.String" /> to canonical form.</summary>
		/// <returns>An object representing the converted value.</returns>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="data">The <see cref="T:System.String" /> object to convert.</param>
		// Token: 0x06000438 RID: 1080 RVA: 0x0000BE70 File Offset: 0x0000A070
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			return ((string)data).Trim();
		}

		/// <summary>Converts a <see cref="T:System.String" /> to canonical form.</summary>
		/// <returns>An object representing the converted value.</returns>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object used during conversion.</param>
		/// <param name="value">The value to convert to.</param>
		/// <param name="type">The type to convert to.</param>
		// Token: 0x06000439 RID: 1081 RVA: 0x0000BE80 File Offset: 0x0000A080
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (!(value is string))
			{
				throw new ArgumentException("value");
			}
			return ((string)value).Trim();
		}
	}
}
