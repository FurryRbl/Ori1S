using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>The base class for the configuration converter types.</summary>
	// Token: 0x02000020 RID: 32
	public abstract class ConfigurationConverterBase : TypeConverter
	{
		/// <summary>Determines whether the conversion is allowed.</summary>
		/// <returns>true if the conversion is allowed; otherwise, false.</returns>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversions.</param>
		/// <param name="type">The <see cref="T:System.Type" /> to convert from.</param>
		// Token: 0x06000118 RID: 280 RVA: 0x00003CA0 File Offset: 0x00001EA0
		public override bool CanConvertFrom(ITypeDescriptorContext ctx, Type type)
		{
			return type == typeof(string) || base.CanConvertFrom(ctx, type);
		}

		/// <summary>Determines whether the conversion is allowed.</summary>
		/// <returns>true if the conversion is allowed; otherwise, false. </returns>
		/// <param name="ctx">The <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object used for type conversion.</param>
		/// <param name="type">The type to convert to.</param>
		// Token: 0x06000119 RID: 281 RVA: 0x00003CBC File Offset: 0x00001EBC
		public override bool CanConvertTo(ITypeDescriptorContext ctx, Type type)
		{
			return type == typeof(string) || base.CanConvertTo(ctx, type);
		}
	}
}
