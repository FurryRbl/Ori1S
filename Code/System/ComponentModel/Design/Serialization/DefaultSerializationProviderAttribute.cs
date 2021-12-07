using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>The <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> attribute is placed on a serializer to indicate the class to use as a default provider of that type of serializer. </summary>
	// Token: 0x02000139 RID: 313
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class DefaultSerializationProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> class with the named provider type.</summary>
		/// <param name="providerTypeName">The name of the serialization provider type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerTypeName" /> is null.</exception>
		// Token: 0x06000BB2 RID: 2994 RVA: 0x0001E74C File Offset: 0x0001C94C
		public DefaultSerializationProviderAttribute(string providerTypeName)
		{
			if (providerTypeName == null)
			{
				throw new ArgumentNullException("providerTypeName");
			}
			this._providerTypeName = providerTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> class with the given provider type.</summary>
		/// <param name="providerType">The <see cref="T:System.Type" /> of the serialization provider.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerType" /> is null.</exception>
		// Token: 0x06000BB3 RID: 2995 RVA: 0x0001E76C File Offset: 0x0001C96C
		public DefaultSerializationProviderAttribute(Type providerType)
		{
			if (providerType == null)
			{
				throw new ArgumentNullException("providerType");
			}
			this._providerTypeName = providerType.AssemblyQualifiedName;
		}

		/// <summary>Gets the type name of the serialization provider.</summary>
		/// <returns>A string containing the name of the provider.</returns>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0001E794 File Offset: 0x0001C994
		public string ProviderTypeName
		{
			get
			{
				return this._providerTypeName;
			}
		}

		// Token: 0x0400030F RID: 783
		private string _providerTypeName;
	}
}
