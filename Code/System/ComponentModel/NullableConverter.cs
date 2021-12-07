using System;
using System.Collections;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides automatic conversion between a nullable type and its underlying primitive type.</summary>
	// Token: 0x02000191 RID: 401
	public class NullableConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NullableConverter" /> class.</summary>
		/// <param name="type">The specified nullable type.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a nullable type.</exception>
		// Token: 0x06000DFD RID: 3581 RVA: 0x00024164 File Offset: 0x00022364
		public NullableConverter(Type nullableType)
		{
			if (nullableType == null)
			{
				throw new ArgumentNullException("nullableType");
			}
			this.nullableType = nullableType;
			this.underlyingType = Nullable.GetUnderlyingType(nullableType);
			this.underlyingTypeConverter = TypeDescriptor.GetConverter(this.underlyingType);
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
		// Token: 0x06000DFE RID: 3582 RVA: 0x000241A4 File Offset: 0x000223A4
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == this.underlyingType)
			{
				return true;
			}
			if (this.underlyingTypeConverter != null)
			{
				return this.underlyingTypeConverter.CanConvertFrom(context, sourceType);
			}
			return base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
		// Token: 0x06000DFF RID: 3583 RVA: 0x000241D8 File Offset: 0x000223D8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == this.underlyingType)
			{
				return true;
			}
			if (this.underlyingTypeConverter != null)
			{
				return this.underlyingTypeConverter.CanConvertTo(context, destinationType);
			}
			return base.CanConvertFrom(context, destinationType);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000E00 RID: 3584 RVA: 0x0002420C File Offset: 0x0002240C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || value.GetType() == this.underlyingType)
			{
				return value;
			}
			if (value is string && string.IsNullOrEmpty((string)value))
			{
				return null;
			}
			if (this.underlyingTypeConverter != null)
			{
				return this.underlyingTypeConverter.ConvertFrom(context, culture, value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000E01 RID: 3585 RVA: 0x00024274 File Offset: 0x00022474
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == this.underlyingType && value.GetType() == this.nullableType)
			{
				return value;
			}
			if (this.underlyingTypeConverter != null && value != null)
			{
				return this.underlyingTypeConverter.ConvertTo(context, culture, value, destinationType);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or null if the object cannot be created. This method always returns null.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values. </param>
		// Token: 0x06000E02 RID: 3586 RVA: 0x000242E0 File Offset: 0x000224E0
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (this.underlyingTypeConverter != null)
			{
				return this.underlyingTypeConverter.CreateInstance(context, propertyValues);
			}
			return base.CreateInstance(context, propertyValues);
		}

		/// <returns>true if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06000E03 RID: 3587 RVA: 0x00024304 File Offset: 0x00022504
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			if (this.underlyingTypeConverter != null)
			{
				return this.underlyingTypeConverter.GetCreateInstanceSupported(context);
			}
			return base.GetCreateInstanceSupported(context);
		}

		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter. </param>
		// Token: 0x06000E04 RID: 3588 RVA: 0x00024328 File Offset: 0x00022528
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (this.underlyingTypeConverter != null)
			{
				return this.underlyingTypeConverter.GetProperties(context, value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06000E05 RID: 3589 RVA: 0x00024350 File Offset: 0x00022550
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			if (this.underlyingTypeConverter != null)
			{
				return this.underlyingTypeConverter.GetCreateInstanceSupported(context);
			}
			return base.GetCreateInstanceSupported(context);
		}

		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
		// Token: 0x06000E06 RID: 3590 RVA: 0x00024374 File Offset: 0x00022574
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.underlyingTypeConverter != null && this.underlyingTypeConverter.GetStandardValuesSupported(context))
			{
				TypeConverter.StandardValuesCollection standardValues = this.underlyingTypeConverter.GetStandardValues(context);
				if (standardValues != null)
				{
					return new TypeConverter.StandardValuesCollection(new ArrayList(standardValues)
					{
						null
					});
				}
			}
			return base.GetStandardValues(context);
		}

		/// <returns>true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; false if other values are possible.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06000E07 RID: 3591 RVA: 0x000243D0 File Offset: 0x000225D0
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			if (this.underlyingTypeConverter != null)
			{
				return this.underlyingTypeConverter.GetStandardValuesExclusive(context);
			}
			return base.GetStandardValuesExclusive(context);
		}

		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06000E08 RID: 3592 RVA: 0x000243F4 File Offset: 0x000225F4
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			if (this.underlyingTypeConverter != null)
			{
				return this.underlyingTypeConverter.GetStandardValuesSupported(context);
			}
			return base.GetStandardValuesSupported(context);
		}

		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to test for validity. </param>
		// Token: 0x06000E09 RID: 3593 RVA: 0x00024418 File Offset: 0x00022618
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (this.underlyingTypeConverter != null)
			{
				return this.underlyingTypeConverter.IsValid(context, value);
			}
			return base.IsValid(context, value);
		}

		/// <summary>Gets the nullable type.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the nullable type.</returns>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0002443C File Offset: 0x0002263C
		public Type NullableType
		{
			get
			{
				return this.nullableType;
			}
		}

		/// <summary>Gets the underlying type.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the underlying type.</returns>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00024444 File Offset: 0x00022644
		public Type UnderlyingType
		{
			get
			{
				return this.underlyingType;
			}
		}

		/// <summary>Gets the underlying type converter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that represents the underlying type converter.</returns>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0002444C File Offset: 0x0002264C
		public TypeConverter UnderlyingTypeConverter
		{
			get
			{
				return this.underlyingTypeConverter;
			}
		}

		// Token: 0x040003F7 RID: 1015
		private Type nullableType;

		// Token: 0x040003F8 RID: 1016
		private Type underlyingType;

		// Token: 0x040003F9 RID: 1017
		private TypeConverter underlyingTypeConverter;
	}
}
