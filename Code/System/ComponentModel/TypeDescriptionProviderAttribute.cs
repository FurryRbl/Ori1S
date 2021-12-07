using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the custom type description provider for a class. This class cannot be inherited.</summary>
	// Token: 0x020001B4 RID: 436
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public sealed class TypeDescriptionProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProviderAttribute" /> class using the specified type name.</summary>
		/// <param name="typeName">The qualified name of the type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null.</exception>
		// Token: 0x06000F3F RID: 3903 RVA: 0x00027190 File Offset: 0x00025390
		public TypeDescriptionProviderAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.typeName = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProviderAttribute" /> class using the specified type.</summary>
		/// <param name="type">The type to store in the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06000F40 RID: 3904 RVA: 0x000271B0 File Offset: 0x000253B0
		public TypeDescriptionProviderAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.typeName = type.AssemblyQualifiedName;
		}

		/// <summary>Gets the type name for the type description provider.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the qualified type name for the <see cref="T:System.ComponentModel.TypeDescriptionProvider" />.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x000271D8 File Offset: 0x000253D8
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x04000449 RID: 1097
		private string typeName;
	}
}
