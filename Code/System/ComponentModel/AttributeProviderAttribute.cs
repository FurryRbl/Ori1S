using System;

namespace System.ComponentModel
{
	/// <summary>Enables attribute redirection. This class cannot be inherited.</summary>
	// Token: 0x020000CA RID: 202
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class AttributeProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type.</summary>
		/// <param name="type">The type to specify.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x060008AB RID: 2219 RVA: 0x00019AB4 File Offset: 0x00017CB4
		public AttributeProviderAttribute(Type type)
		{
			this.type_name = type.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type name and property name.</summary>
		/// <param name="typeName">The name of the type to specify.</param>
		/// <param name="propertyName">The name of the property for which attributes will be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyName" /> is null.</exception>
		// Token: 0x060008AC RID: 2220 RVA: 0x00019AC8 File Offset: 0x00017CC8
		public AttributeProviderAttribute(string typeName, string propertyName)
		{
			this.type_name = typeName;
			this.property_name = propertyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type name.</summary>
		/// <param name="typeName">The name of the type to specify.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null.</exception>
		// Token: 0x060008AD RID: 2221 RVA: 0x00019AE0 File Offset: 0x00017CE0
		public AttributeProviderAttribute(string typeName)
		{
			this.type_name = typeName;
		}

		/// <summary>Gets the name of the property for which attributes will be retrieved.</summary>
		/// <returns>The name of the property for which attributes will be retrieved.</returns>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00019AF0 File Offset: 0x00017CF0
		public string PropertyName
		{
			get
			{
				return this.property_name;
			}
		}

		/// <summary>Gets the assembly qualified type name passed into the constructor.</summary>
		/// <returns>The assembly qualified name of the type specified in the constructor.</returns>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00019AF8 File Offset: 0x00017CF8
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
		}

		// Token: 0x04000240 RID: 576
		private string type_name;

		// Token: 0x04000241 RID: 577
		private string property_name;
	}
}
