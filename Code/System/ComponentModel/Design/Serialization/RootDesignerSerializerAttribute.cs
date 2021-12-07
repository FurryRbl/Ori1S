using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Indicates the base serializer to use for a root designer object. This class cannot be inherited.</summary>
	// Token: 0x02000137 RID: 311
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	[Obsolete("Use DesignerSerializerAttribute instead")]
	public sealed class RootDesignerSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerTypeName">The name of the base type of the serializer. A class can include multiple serializers as they all have different base types. </param>
		/// <param name="reloadable">true if this serializer supports dynamic reloading of the document; otherwise, false. </param>
		// Token: 0x06000BA5 RID: 2981 RVA: 0x0001E6AC File Offset: 0x0001C8AC
		public RootDesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName, bool reloadable)
		{
			this.serializer = serializerTypeName;
			this.baseserializer = baseSerializerTypeName;
			this.reload = reloadable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerType">The name of the base type of the serializer. A class can include multiple serializers, as they all have different base types. </param>
		/// <param name="reloadable">true if this serializer supports dynamic reloading of the document; otherwise, false. </param>
		// Token: 0x06000BA6 RID: 2982 RVA: 0x0001E6CC File Offset: 0x0001C8CC
		public RootDesignerSerializerAttribute(string serializerTypeName, Type baseSerializerType, bool reloadable) : this(serializerTypeName, baseSerializerType.AssemblyQualifiedName, reloadable)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerType">The data type of the serializer. </param>
		/// <param name="baseSerializerType">The base type of the serializer. A class can include multiple serializers as they all have different base types. </param>
		/// <param name="reloadable">true if this serializer supports dynamic reloading of the document; otherwise, false. </param>
		// Token: 0x06000BA7 RID: 2983 RVA: 0x0001E6DC File Offset: 0x0001C8DC
		public RootDesignerSerializerAttribute(Type serializerType, Type baseSerializerType, bool reloadable) : this(serializerType.AssemblyQualifiedName, baseSerializerType.AssemblyQualifiedName, reloadable)
		{
		}

		/// <summary>Gets a value indicating whether the root serializer supports reloading of the design document without first disposing the designer host.</summary>
		/// <returns>true if the root serializer supports reloading; otherwise, false.</returns>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0001E6FC File Offset: 0x0001C8FC
		public bool Reloadable
		{
			get
			{
				return this.reload;
			}
		}

		/// <summary>Gets the fully qualified type name of the base type of the serializer.</summary>
		/// <returns>The name of the base type of the serializer.</returns>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0001E704 File Offset: 0x0001C904
		public string SerializerBaseTypeName
		{
			get
			{
				return this.baseserializer;
			}
		}

		/// <summary>Gets the fully qualified type name of the serializer.</summary>
		/// <returns>The name of the type of the serializer.</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0001E70C File Offset: 0x0001C90C
		public string SerializerTypeName
		{
			get
			{
				return this.serializer;
			}
		}

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>An object containing a unique ID for this attribute type.</returns>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0001E714 File Offset: 0x0001C914
		public override object TypeId
		{
			get
			{
				return this.ToString() + this.baseserializer;
			}
		}

		// Token: 0x0400030C RID: 780
		private string serializer;

		// Token: 0x0400030D RID: 781
		private string baseserializer;

		// Token: 0x0400030E RID: 782
		private bool reload;
	}
}
