using System;

namespace System.ComponentModel
{
	/// <summary>Specifies what type to use as a converter for the object this attribute is bound to. This class cannot be inherited.</summary>
	// Token: 0x020001AE RID: 430
	[AttributeUsage(AttributeTargets.All)]
	public sealed class TypeConverterAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class with the default type converter, which is an empty string ("").</summary>
		// Token: 0x06000EF2 RID: 3826 RVA: 0x00026B3C File Offset: 0x00024D3C
		public TypeConverterAttribute()
		{
			this.converter_type = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class, using the specified type name as the data converter for the object this attribute is bound to.</summary>
		/// <param name="typeName">The fully qualified name of the class to use for data conversion for the object this attribute is bound to. </param>
		// Token: 0x06000EF3 RID: 3827 RVA: 0x00026B50 File Offset: 0x00024D50
		public TypeConverterAttribute(string typeName)
		{
			this.converter_type = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class, using the specified type as the data converter for the object this attribute is bound to.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the converter class to use for data conversion for the object this attribute is bound to. </param>
		// Token: 0x06000EF4 RID: 3828 RVA: 0x00026B60 File Offset: 0x00024D60
		public TypeConverterAttribute(Type type)
		{
			this.converter_type = type.AssemblyQualifiedName;
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000EF6 RID: 3830 RVA: 0x00026B80 File Offset: 0x00024D80
		public override bool Equals(object obj)
		{
			return obj is TypeConverterAttribute && ((TypeConverterAttribute)obj).ConverterTypeName == this.converter_type;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />.</returns>
		// Token: 0x06000EF7 RID: 3831 RVA: 0x00026BA8 File Offset: 0x00024DA8
		public override int GetHashCode()
		{
			return this.converter_type.GetHashCode();
		}

		/// <summary>Gets the fully qualified type name of the <see cref="T:System.Type" /> to use as a converter for the object this attribute is bound to.</summary>
		/// <returns>The fully qualified type name of the <see cref="T:System.Type" /> to use as a converter for the object this attribute is bound to, or an empty string ("") if none exists. The default value is an empty string ("").</returns>
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00026BB8 File Offset: 0x00024DB8
		public string ConverterTypeName
		{
			get
			{
				return this.converter_type;
			}
		}

		/// <summary>Specifies the type to use as a converter for the object this attribute is bound to. This static field is read-only.</summary>
		// Token: 0x04000442 RID: 1090
		public static readonly TypeConverterAttribute Default = new TypeConverterAttribute();

		// Token: 0x04000443 RID: 1091
		private string converter_type;
	}
}
