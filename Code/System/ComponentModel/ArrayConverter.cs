using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Array" /> objects to and from various other representations.</summary>
	// Token: 0x020000C5 RID: 197
	public class ArrayConverter : CollectionConverter
	{
		/// <summary>Converts the given value object to the specified destination type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The culture into which <paramref name="value" /> will be converted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x0600088F RID: 2191 RVA: 0x000197B4 File Offset: 0x000179B4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is Array)
			{
				return value.GetType().Name + " Array";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Gets a collection of properties for the type of array specified by the value parameter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for an array, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array to get the properties for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that will be used as a filter. </param>
		// Token: 0x06000890 RID: 2192 RVA: 0x00019810 File Offset: 0x00017A10
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (value == null)
			{
				throw new NullReferenceException();
			}
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			if (value is Array)
			{
				Array array = (Array)value;
				for (int i = 0; i < array.Length; i++)
				{
					propertyDescriptorCollection.Add(new ArrayConverter.ArrayPropertyDescriptor(i, array.GetType()));
				}
			}
			return propertyDescriptorCollection;
		}

		/// <summary>Gets a value indicating whether this object supports properties.</summary>
		/// <returns>true because <see cref="M:System.ComponentModel.ArrayConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> should be called to find the properties of this object. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000891 RID: 2193 RVA: 0x00019870 File Offset: 0x00017A70
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x020000C6 RID: 198
		internal class ArrayPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x06000892 RID: 2194 RVA: 0x00019874 File Offset: 0x00017A74
			public ArrayPropertyDescriptor(int index, Type array_type) : base(string.Format("[{0}]", index), null)
			{
				this.index = index;
				this.array_type = array_type;
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06000893 RID: 2195 RVA: 0x0001989C File Offset: 0x00017A9C
			public override Type ComponentType
			{
				get
				{
					return this.array_type;
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06000894 RID: 2196 RVA: 0x000198A4 File Offset: 0x00017AA4
			public override Type PropertyType
			{
				get
				{
					return this.array_type.GetElementType();
				}
			}

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x06000895 RID: 2197 RVA: 0x000198B4 File Offset: 0x00017AB4
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000896 RID: 2198 RVA: 0x000198B8 File Offset: 0x00017AB8
			public override object GetValue(object component)
			{
				if (component == null)
				{
					return null;
				}
				return ((Array)component).GetValue(this.index);
			}

			// Token: 0x06000897 RID: 2199 RVA: 0x000198D4 File Offset: 0x00017AD4
			public override void SetValue(object component, object value)
			{
				if (component == null)
				{
					return;
				}
				((Array)component).SetValue(value, this.index);
			}

			// Token: 0x06000898 RID: 2200 RVA: 0x000198F0 File Offset: 0x00017AF0
			public override void ResetValue(object component)
			{
			}

			// Token: 0x06000899 RID: 2201 RVA: 0x000198F4 File Offset: 0x00017AF4
			public override bool CanResetValue(object component)
			{
				return false;
			}

			// Token: 0x0600089A RID: 2202 RVA: 0x000198F8 File Offset: 0x00017AF8
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x04000238 RID: 568
			private int index;

			// Token: 0x04000239 RID: 569
			private Type array_type;
		}
	}
}
