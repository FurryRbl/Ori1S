using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the editor to use to change a property. This class cannot be inherited.</summary>
	// Token: 0x02000142 RID: 322
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	public sealed class EditorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the default editor, which is no editor.</summary>
		// Token: 0x06000BDE RID: 3038 RVA: 0x0001F088 File Offset: 0x0001D288
		public EditorAttribute()
		{
			this.name = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type name and base type name of the editor.</summary>
		/// <param name="typeName">The fully qualified type name of the editor. </param>
		/// <param name="baseTypeName">The fully qualified type name of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />. </param>
		// Token: 0x06000BDF RID: 3039 RVA: 0x0001F09C File Offset: 0x0001D29C
		public EditorAttribute(string typeName, string baseTypeName)
		{
			this.name = typeName;
			this.basename = baseTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type name and the base type.</summary>
		/// <param name="typeName">The fully qualified type name of the editor. </param>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />. </param>
		// Token: 0x06000BE0 RID: 3040 RVA: 0x0001F0B4 File Offset: 0x0001D2B4
		public EditorAttribute(string typeName, Type baseType) : this(typeName, baseType.AssemblyQualifiedName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type and the base type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the editor. </param>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />. </param>
		// Token: 0x06000BE1 RID: 3041 RVA: 0x0001F0C4 File Offset: 0x0001D2C4
		public EditorAttribute(Type type, Type baseType) : this(type.AssemblyQualifiedName, baseType.AssemblyQualifiedName)
		{
		}

		/// <summary>Gets the name of the base class or interface serving as a lookup key for this editor.</summary>
		/// <returns>The name of the base class or interface serving as a lookup key for this editor.</returns>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
		public string EditorBaseTypeName
		{
			get
			{
				return this.basename;
			}
		}

		/// <summary>Gets the name of the editor class in the <see cref="P:System.Type.AssemblyQualifiedName" /> format.</summary>
		/// <returns>The name of the editor class in the <see cref="P:System.Type.AssemblyQualifiedName" /> format.</returns>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0001F0E0 File Offset: 0x0001D2E0
		public string EditorTypeName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0001F0E8 File Offset: 0x0001D2E8
		public override object TypeId
		{
			get
			{
				return base.GetType();
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.EditorAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current object; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000BE5 RID: 3045 RVA: 0x0001F0F0 File Offset: 0x0001D2F0
		public override bool Equals(object obj)
		{
			return obj is EditorAttribute && ((EditorAttribute)obj).EditorBaseTypeName.Equals(this.basename) && ((EditorAttribute)obj).EditorTypeName.Equals(this.name);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0001F140 File Offset: 0x0001D340
		public override int GetHashCode()
		{
			return (this.name + this.basename).GetHashCode();
		}

		// Token: 0x0400035E RID: 862
		private string name;

		// Token: 0x0400035F RID: 863
		private string basename;
	}
}
