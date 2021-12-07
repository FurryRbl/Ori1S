using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides a unified way of converting types of values to other types, as well as for accessing standard values and subproperties.</summary>
	// Token: 0x020001AF RID: 431
	[ComVisible(true)]
	public class TypeConverter
	{
		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from. </param>
		// Token: 0x06000EFA RID: 3834 RVA: 0x00026BC8 File Offset: 0x00024DC8
		public bool CanConvertFrom(Type sourceType)
		{
			return this.CanConvertFrom(null, sourceType);
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from. </param>
		// Token: 0x06000EFB RID: 3835 RVA: 0x00026BD4 File Offset: 0x00024DD4
		public virtual bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
		// Token: 0x06000EFC RID: 3836 RVA: 0x00026BEC File Offset: 0x00024DEC
		public bool CanConvertTo(Type destinationType)
		{
			return this.CanConvertTo(null, destinationType);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
		// Token: 0x06000EFD RID: 3837 RVA: 0x00026BF8 File Offset: 0x00024DF8
		public virtual bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		/// <summary>Converts the given value to the type of this converter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000EFE RID: 3838 RVA: 0x00026C08 File Offset: 0x00024E08
		public object ConvertFrom(object o)
		{
			return this.ConvertFrom(null, CultureInfo.CurrentCulture, o);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000EFF RID: 3839 RVA: 0x00026C18 File Offset: 0x00024E18
		public virtual object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is System.ComponentModel.Design.Serialization.InstanceDescriptor)
			{
				return ((System.ComponentModel.Design.Serialization.InstanceDescriptor)value).Invoke();
			}
			return this.GetConvertFromException(value);
		}

		/// <summary>Converts the given string to the type of this converter, using the invariant culture.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="text">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000F00 RID: 3840 RVA: 0x00026C38 File Offset: 0x00024E38
		public object ConvertFromInvariantString(string text)
		{
			return this.ConvertFromInvariantString(null, text);
		}

		/// <summary>Converts the given string to the type of this converter, using the invariant culture and the specified context.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="text">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000F01 RID: 3841 RVA: 0x00026C44 File Offset: 0x00024E44
		public object ConvertFromInvariantString(ITypeDescriptorContext context, string text)
		{
			return this.ConvertFromString(context, CultureInfo.InvariantCulture, text);
		}

		/// <summary>Converts the specified text to an object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="text">The text representation of the object to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The string cannot be converted into the appropriate object. </exception>
		// Token: 0x06000F02 RID: 3842 RVA: 0x00026C54 File Offset: 0x00024E54
		public object ConvertFromString(string text)
		{
			return this.ConvertFrom(text);
		}

		/// <summary>Converts the given text to an object, using the specified context.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="text">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000F03 RID: 3843 RVA: 0x00026C60 File Offset: 0x00024E60
		public object ConvertFromString(ITypeDescriptorContext context, string text)
		{
			return this.ConvertFromString(context, CultureInfo.CurrentCulture, text);
		}

		/// <summary>Converts the given text to an object, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted text.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="text">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000F04 RID: 3844 RVA: 0x00026C70 File Offset: 0x00024E70
		public object ConvertFromString(ITypeDescriptorContext context, CultureInfo culture, string text)
		{
			return this.ConvertFrom(context, culture, text);
		}

		/// <summary>Converts the given value object to the specified type, using the arguments.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000F05 RID: 3845 RVA: 0x00026C7C File Offset: 0x00024E7C
		public object ConvertTo(object value, Type destinationType)
		{
			return this.ConvertTo(null, null, value, destinationType);
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000F06 RID: 3846 RVA: 0x00026C88 File Offset: 0x00024E88
		public virtual object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType != typeof(string))
			{
				return this.GetConvertToException(value, destinationType);
			}
			if (value != null)
			{
				return value.ToString();
			}
			return string.Empty;
		}

		/// <summary>Converts the specified value to a culture-invariant string representation.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the converted value.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000F07 RID: 3847 RVA: 0x00026CD4 File Offset: 0x00024ED4
		public string ConvertToInvariantString(object value)
		{
			return this.ConvertToInvariantString(null, value);
		}

		/// <summary>Converts the specified value to a culture-invariant string representation, using the specified context.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000F08 RID: 3848 RVA: 0x00026CE0 File Offset: 0x00024EE0
		public string ConvertToInvariantString(ITypeDescriptorContext context, object value)
		{
			return (string)this.ConvertTo(context, CultureInfo.InvariantCulture, value, typeof(string));
		}

		/// <summary>Converts the specified value to a string representation.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000F09 RID: 3849 RVA: 0x00026D00 File Offset: 0x00024F00
		public string ConvertToString(object value)
		{
			return (string)this.ConvertTo(null, CultureInfo.CurrentCulture, value, typeof(string));
		}

		/// <summary>Converts the given value to a string representation, using the given context.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000F0A RID: 3850 RVA: 0x00026D20 File Offset: 0x00024F20
		public string ConvertToString(ITypeDescriptorContext context, object value)
		{
			return (string)this.ConvertTo(context, CultureInfo.CurrentCulture, value, typeof(string));
		}

		/// <summary>Converts the given value to a string representation, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000F0B RID: 3851 RVA: 0x00026D40 File Offset: 0x00024F40
		public string ConvertToString(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return (string)this.ConvertTo(context, culture, value, typeof(string));
		}

		/// <summary>Returns an exception to throw when a conversion cannot be performed.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception to throw when a conversion cannot be performed.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert, or null if the object is not available. </param>
		/// <exception cref="T:System.NotSupportedException">Automatically thrown by this method. </exception>
		// Token: 0x06000F0C RID: 3852 RVA: 0x00026D5C File Offset: 0x00024F5C
		protected Exception GetConvertFromException(object value)
		{
			string text;
			if (value == null)
			{
				text = "(null)";
			}
			else
			{
				text = value.GetType().FullName;
			}
			throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "{0} cannot convert from {1}.", new object[]
			{
				base.GetType().Name,
				text
			}));
		}

		/// <summary>Returns an exception to throw when a conversion cannot be performed.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the exception to throw when a conversion cannot be performed.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to convert, or null if the object is not available. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type the conversion was trying to convert to. </param>
		/// <exception cref="T:System.NotSupportedException">Automatically thrown by this method. </exception>
		// Token: 0x06000F0D RID: 3853 RVA: 0x00026DB4 File Offset: 0x00024FB4
		protected Exception GetConvertToException(object value, Type destinationType)
		{
			string text;
			if (value == null)
			{
				text = "(null)";
			}
			else
			{
				text = value.GetType().FullName;
			}
			throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "'{0}' is unable to convert '{1}' to '{2}'.", new object[]
			{
				base.GetType().Name,
				text,
				destinationType.FullName
			}));
		}

		/// <summary>Re-creates an <see cref="T:System.Object" /> given a set of property values for the object.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or null if the object cannot be created. This method always returns null.</returns>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> that represents a dictionary of new property values. </param>
		// Token: 0x06000F0E RID: 3854 RVA: 0x00026E14 File Offset: 0x00025014
		public object CreateInstance(IDictionary propertyValues)
		{
			return this.CreateInstance(null, propertyValues);
		}

		/// <summary>Creates an instance of the type that this <see cref="T:System.ComponentModel.TypeConverter" /> is associated with, using the specified context, given a set of property values for the object.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or null if the object cannot be created. This method always returns null.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values. </param>
		// Token: 0x06000F0F RID: 3855 RVA: 0x00026E20 File Offset: 0x00025020
		public virtual object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return null;
		}

		/// <summary>Returns whether changing a value on this object requires a call to the <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> method to create a new value.</summary>
		/// <returns>true if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, false.</returns>
		// Token: 0x06000F10 RID: 3856 RVA: 0x00026E24 File Offset: 0x00025024
		public bool GetCreateInstanceSupported()
		{
			return this.GetCreateInstanceSupported(null);
		}

		/// <summary>Returns whether changing a value on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value, using the specified context.</summary>
		/// <returns>true if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000F11 RID: 3857 RVA: 0x00026E30 File Offset: 0x00025030
		public virtual bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
		// Token: 0x06000F12 RID: 3858 RVA: 0x00026E34 File Offset: 0x00025034
		public PropertyDescriptorCollection GetProperties(object value)
		{
			return this.GetProperties(null, value);
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
		// Token: 0x06000F13 RID: 3859 RVA: 0x00026E40 File Offset: 0x00025040
		public PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value)
		{
			return this.GetProperties(context, value, new Attribute[]
			{
				BrowsableAttribute.Yes
			});
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter. </param>
		// Token: 0x06000F14 RID: 3860 RVA: 0x00026E58 File Offset: 0x00025058
		public virtual PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return null;
		}

		/// <summary>Returns whether this object supports properties.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, false.</returns>
		// Token: 0x06000F15 RID: 3861 RVA: 0x00026E5C File Offset: 0x0002505C
		public bool GetPropertiesSupported()
		{
			return this.GetPropertiesSupported(null);
		}

		/// <summary>Returns whether this object supports properties, using the specified context.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000F16 RID: 3862 RVA: 0x00026E68 File Offset: 0x00025068
		public virtual bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns a collection of standard values from the default context for the data type this type converter is designed for.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> containing a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		// Token: 0x06000F17 RID: 3863 RVA: 0x00026E6C File Offset: 0x0002506C
		public ICollection GetStandardValues()
		{
			return this.GetStandardValues(null);
		}

		/// <summary>Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null. </param>
		// Token: 0x06000F18 RID: 3864 RVA: 0x00026E78 File Offset: 0x00025078
		public virtual TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return null;
		}

		/// <summary>Returns whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; false if other values are possible.</returns>
		// Token: 0x06000F19 RID: 3865 RVA: 0x00026E7C File Offset: 0x0002507C
		public bool GetStandardValuesExclusive()
		{
			return this.GetStandardValuesExclusive(null);
		}

		/// <summary>Returns whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list of possible values, using the specified context.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; false if other values are possible.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000F1A RID: 3866 RVA: 0x00026E88 File Offset: 0x00025088
		public virtual bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns whether this object supports a standard set of values that can be picked from a list.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false.</returns>
		// Token: 0x06000F1B RID: 3867 RVA: 0x00026E8C File Offset: 0x0002508C
		public bool GetStandardValuesSupported()
		{
			return this.GetStandardValuesSupported(null);
		}

		/// <summary>Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000F1C RID: 3868 RVA: 0x00026E98 File Offset: 0x00025098
		public virtual bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns whether the given value object is valid for this type.</summary>
		/// <returns>true if the specified value is valid for this object; otherwise, false.</returns>
		/// <param name="value">The object to test for validity. </param>
		// Token: 0x06000F1D RID: 3869 RVA: 0x00026E9C File Offset: 0x0002509C
		public bool IsValid(object value)
		{
			return this.IsValid(null, value);
		}

		/// <summary>Returns whether the given value object is valid for this type and for the specified context.</summary>
		/// <returns>true if the specified value is valid for this object; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to test for validity. </param>
		// Token: 0x06000F1E RID: 3870 RVA: 0x00026EA8 File Offset: 0x000250A8
		public virtual bool IsValid(ITypeDescriptorContext context, object value)
		{
			return true;
		}

		/// <summary>Sorts a collection of properties.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted properties.</returns>
		/// <param name="props">A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that has the properties to sort. </param>
		/// <param name="names">An array of names in the order you want the properties to appear in the collection. </param>
		// Token: 0x06000F1F RID: 3871 RVA: 0x00026EAC File Offset: 0x000250AC
		protected PropertyDescriptorCollection SortProperties(PropertyDescriptorCollection props, string[] names)
		{
			props.Sort(names);
			return props;
		}

		/// <summary>Represents a collection of values.</summary>
		// Token: 0x020001B0 RID: 432
		public class StandardValuesCollection : ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> class.</summary>
			/// <param name="values">An <see cref="T:System.Collections.ICollection" /> that represents the objects to put into the collection. </param>
			// Token: 0x06000F20 RID: 3872 RVA: 0x00026EB8 File Offset: 0x000250B8
			public StandardValuesCollection(ICollection values)
			{
				this.values = values;
			}

			/// <summary>Copies the contents of this collection to an array.</summary>
			/// <param name="array">The array to copy to. </param>
			/// <param name="index">The index in the array where copying should begin. </param>
			// Token: 0x06000F21 RID: 3873 RVA: 0x00026EC8 File Offset: 0x000250C8
			void ICollection.CopyTo(Array array, int index)
			{
				this.CopyTo(array, index);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x06000F22 RID: 3874 RVA: 0x00026ED4 File Offset: 0x000250D4
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000374 RID: 884
			// (get) Token: 0x06000F23 RID: 3875 RVA: 0x00026EDC File Offset: 0x000250DC
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>null in all cases.</returns>
			// Token: 0x17000375 RID: 885
			// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00026EE0 File Offset: 0x000250E0
			object ICollection.SyncRoot
			{
				get
				{
					return null;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
			// Token: 0x17000376 RID: 886
			// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00026EE4 File Offset: 0x000250E4
			int ICollection.Count
			{
				get
				{
					return this.Count;
				}
			}

			/// <summary>Copies the contents of this collection to an array.</summary>
			/// <param name="array">An <see cref="T:System.Array" /> that represents the array to copy to. </param>
			/// <param name="index">The index to start from. </param>
			// Token: 0x06000F26 RID: 3878 RVA: 0x00026EEC File Offset: 0x000250EC
			public void CopyTo(Array array, int index)
			{
				this.values.CopyTo(array, index);
			}

			/// <summary>Returns an enumerator for this collection.</summary>
			/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
			// Token: 0x06000F27 RID: 3879 RVA: 0x00026EFC File Offset: 0x000250FC
			public IEnumerator GetEnumerator()
			{
				return this.values.GetEnumerator();
			}

			/// <summary>Gets the number of objects in the collection.</summary>
			/// <returns>The number of objects in the collection.</returns>
			// Token: 0x17000377 RID: 887
			// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00026F0C File Offset: 0x0002510C
			public int Count
			{
				get
				{
					return this.values.Count;
				}
			}

			/// <summary>Gets the object at the specified index number.</summary>
			/// <returns>The <see cref="T:System.Object" /> with the specified index.</returns>
			/// <param name="index">The zero-based index of the <see cref="T:System.Object" /> to get from the collection. </param>
			// Token: 0x17000378 RID: 888
			public object this[int index]
			{
				get
				{
					return ((IList)this.values)[index];
				}
			}

			// Token: 0x04000444 RID: 1092
			private ICollection values;
		}

		/// <summary>Represents an abstract class that provides properties for objects that do not have properties.</summary>
		// Token: 0x020001B1 RID: 433
		protected abstract class SimplePropertyDescriptor : PropertyDescriptor
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverter.SimplePropertyDescriptor" /> class.</summary>
			/// <param name="componentType">A <see cref="T:System.Type" /> that represents the type of component to which this property descriptor binds. </param>
			/// <param name="name">The name of the property. </param>
			/// <param name="propertyType">A <see cref="T:System.Type" /> that represents the data type for this property. </param>
			// Token: 0x06000F2A RID: 3882 RVA: 0x00026F30 File Offset: 0x00025130
			public SimplePropertyDescriptor(Type componentType, string name, Type propertyType) : this(componentType, name, propertyType, null)
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverter.SimplePropertyDescriptor" /> class.</summary>
			/// <param name="componentType">A <see cref="T:System.Type" /> that represents the type of component to which this property descriptor binds. </param>
			/// <param name="name">The name of the property. </param>
			/// <param name="propertyType">A <see cref="T:System.Type" /> that represents the data type for this property. </param>
			/// <param name="attributes">An <see cref="T:System.Attribute" /> array with the attributes to associate with the property. </param>
			// Token: 0x06000F2B RID: 3883 RVA: 0x00026F3C File Offset: 0x0002513C
			public SimplePropertyDescriptor(Type componentType, string name, Type propertyType, Attribute[] attributes) : base(name, attributes)
			{
				this.componentType = componentType;
				this.propertyType = propertyType;
			}

			/// <summary>Gets the type of component to which this property description binds.</summary>
			/// <returns>A <see cref="T:System.Type" /> that represents the type of component to which this property binds.</returns>
			// Token: 0x17000379 RID: 889
			// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00026F58 File Offset: 0x00025158
			public override Type ComponentType
			{
				get
				{
					return this.componentType;
				}
			}

			/// <summary>Gets the type of the property.</summary>
			/// <returns>A <see cref="T:System.Type" /> that represents the type of the property.</returns>
			// Token: 0x1700037A RID: 890
			// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00026F60 File Offset: 0x00025160
			public override Type PropertyType
			{
				get
				{
					return this.propertyType;
				}
			}

			/// <summary>Gets a value indicating whether this property is read-only.</summary>
			/// <returns>true if the property is read-only; false if the property is read/write.</returns>
			// Token: 0x1700037B RID: 891
			// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00026F68 File Offset: 0x00025168
			public override bool IsReadOnly
			{
				get
				{
					return this.Attributes.Contains(ReadOnlyAttribute.Yes);
				}
			}

			/// <summary>Returns whether the value of this property can persist.</summary>
			/// <returns>true if the value of the property can persist; otherwise, false.</returns>
			/// <param name="component">The component with the property that is to be examined for persistence. </param>
			// Token: 0x06000F2F RID: 3887 RVA: 0x00026F7C File Offset: 0x0002517C
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			/// <summary>Returns whether resetting the component changes the value of the component.</summary>
			/// <returns>true if resetting the component changes the value of the component; otherwise, false.</returns>
			/// <param name="component">The component to test for reset capability. </param>
			// Token: 0x06000F30 RID: 3888 RVA: 0x00026F80 File Offset: 0x00025180
			public override bool CanResetValue(object component)
			{
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
				return defaultValueAttribute != null && defaultValueAttribute.Value == this.GetValue(component);
			}

			/// <summary>Resets the value for this property of the component.</summary>
			/// <param name="component">The component with the property value to be reset. </param>
			// Token: 0x06000F31 RID: 3889 RVA: 0x00026FC0 File Offset: 0x000251C0
			public override void ResetValue(object component)
			{
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
				if (defaultValueAttribute != null)
				{
					this.SetValue(component, defaultValueAttribute.Value);
				}
			}

			// Token: 0x04000445 RID: 1093
			private Type componentType;

			// Token: 0x04000446 RID: 1094
			private Type propertyType;
		}
	}
}
