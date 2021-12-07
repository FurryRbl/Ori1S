﻿using System;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert expandable objects to and from various other representations.</summary>
	// Token: 0x0200014B RID: 331
	public class ExpandableObjectConverter : TypeConverter
	{
		/// <summary>Gets a collection of properties for the type of object specified by the value parameter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for the component, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of object to get the properties for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that will be used as a filter. </param>
		// Token: 0x06000C39 RID: 3129 RVA: 0x0001FEDC File Offset: 0x0001E0DC
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(value, attributes);
		}

		/// <summary>Gets a value indicating whether this object supports properties using the specified context.</summary>
		/// <returns>true because <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000C3A RID: 3130 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
