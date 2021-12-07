using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Indicates a serializer for the serialization manager to use to serialize the values of the type this attribute is applied to. This class cannot be inherited.</summary>
	// Token: 0x0200012B RID: 299
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerTypeName">The fully qualified name of the base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types. </param>
		// Token: 0x06000B62 RID: 2914 RVA: 0x0001E06C File Offset: 0x0001C26C
		public DesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName)
		{
			this.serializerTypeName = serializerTypeName;
			this.baseSerializerTypeName = baseSerializerTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerType">The base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types. </param>
		// Token: 0x06000B63 RID: 2915 RVA: 0x0001E084 File Offset: 0x0001C284
		public DesignerSerializerAttribute(string serializerTypeName, Type baseSerializerType) : this(serializerTypeName, baseSerializerType.AssemblyQualifiedName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerType">The data type of the serializer. </param>
		/// <param name="baseSerializerType">The base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types. </param>
		// Token: 0x06000B64 RID: 2916 RVA: 0x0001E094 File Offset: 0x0001C294
		public DesignerSerializerAttribute(Type serializerType, Type baseSerializerType) : this(serializerType.AssemblyQualifiedName, baseSerializerType.AssemblyQualifiedName)
		{
		}

		/// <summary>Gets the fully qualified type name of the serializer base type.</summary>
		/// <returns>The fully qualified type name of the serializer base type.</returns>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0001E0A8 File Offset: 0x0001C2A8
		public string SerializerBaseTypeName
		{
			get
			{
				return this.baseSerializerTypeName;
			}
		}

		/// <summary>Gets the fully qualified type name of the serializer.</summary>
		/// <returns>The fully qualified type name of the serializer.</returns>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0001E0B0 File Offset: 0x0001C2B0
		public string SerializerTypeName
		{
			get
			{
				return this.serializerTypeName;
			}
		}

		/// <summary>Indicates a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0001E0B8 File Offset: 0x0001C2B8
		public override object TypeId
		{
			get
			{
				return this.ToString() + this.baseSerializerTypeName;
			}
		}

		// Token: 0x040002FF RID: 767
		private string serializerTypeName;

		// Token: 0x04000300 RID: 768
		private string baseSerializerTypeName;
	}
}
