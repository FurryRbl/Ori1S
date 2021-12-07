using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a type declaration for a class, structure, interface, or enumeration.</summary>
	// Token: 0x02000068 RID: 104
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeTypeDeclaration : CodeTypeMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> class.</summary>
		// Token: 0x06000352 RID: 850 RVA: 0x0000D058 File Offset: 0x0000B258
		public CodeTypeDeclaration()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> class with the specified name.</summary>
		/// <param name="name">The name for the new type. </param>
		// Token: 0x06000353 RID: 851 RVA: 0x0000D068 File Offset: 0x0000B268
		public CodeTypeDeclaration(string name)
		{
			base.Name = name;
		}

		/// <summary>Occurs when the <see cref="P:System.CodeDom.CodeTypeDeclaration.BaseTypes" /> collection is accessed for the first time.</summary>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000354 RID: 852 RVA: 0x0000D080 File Offset: 0x0000B280
		// (remove) Token: 0x06000355 RID: 853 RVA: 0x0000D09C File Offset: 0x0000B29C
		public event EventHandler PopulateBaseTypes;

		/// <summary>Occurs when the <see cref="P:System.CodeDom.CodeTypeDeclaration.Members" /> collection is accessed for the first time.</summary>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000356 RID: 854 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		// (remove) Token: 0x06000357 RID: 855 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
		public event EventHandler PopulateMembers;

		/// <summary>Gets the base types of the type.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> object that indicates the base types of the type.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000D0F0 File Offset: 0x0000B2F0
		public CodeTypeReferenceCollection BaseTypes
		{
			get
			{
				if (this.baseTypes == null)
				{
					this.baseTypes = new CodeTypeReferenceCollection();
					if (this.PopulateBaseTypes != null)
					{
						this.PopulateBaseTypes(this, EventArgs.Empty);
					}
				}
				return this.baseTypes;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is a class or reference type.</summary>
		/// <returns>true if the type is a class or reference type; otherwise, false.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000D138 File Offset: 0x0000B338
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000D168 File Offset: 0x0000B368
		public bool IsClass
		{
			get
			{
				return (this.attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !this.isEnum && !this.isStruct;
			}
			set
			{
				if (value)
				{
					this.attributes &= ~TypeAttributes.ClassSemanticsMask;
					this.isEnum = false;
					this.isStruct = false;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is an enumeration.</summary>
		/// <returns>true if the type is an enumeration; otherwise, false.</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000D190 File Offset: 0x0000B390
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000D198 File Offset: 0x0000B398
		public bool IsEnum
		{
			get
			{
				return this.isEnum;
			}
			set
			{
				if (value)
				{
					this.attributes &= ~TypeAttributes.ClassSemanticsMask;
					this.isEnum = true;
					this.isStruct = false;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is an interface.</summary>
		/// <returns>true if the type is an interface; otherwise, false.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
		public bool IsInterface
		{
			get
			{
				return (this.attributes & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic;
			}
			set
			{
				if (value)
				{
					this.attributes |= TypeAttributes.ClassSemanticsMask;
					this.isEnum = false;
					this.isStruct = false;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is a value type (struct).</summary>
		/// <returns>true if the type is a value type; otherwise, false.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000D1FC File Offset: 0x0000B3FC
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000D204 File Offset: 0x0000B404
		public bool IsStruct
		{
			get
			{
				return this.isStruct;
			}
			set
			{
				if (value)
				{
					this.attributes &= ~TypeAttributes.ClassSemanticsMask;
					this.isEnum = false;
					this.isStruct = true;
				}
			}
		}

		/// <summary>Gets the collection of class members for the represented type.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> object that indicates the class members.</returns>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000D22C File Offset: 0x0000B42C
		public CodeTypeMemberCollection Members
		{
			get
			{
				if (this.members == null)
				{
					this.members = new CodeTypeMemberCollection();
					if (this.PopulateMembers != null)
					{
						this.PopulateMembers(this, EventArgs.Empty);
					}
				}
				return this.members;
			}
		}

		/// <summary>Gets or sets the attributes of the type.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object that indicates the attributes of the type.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000D274 File Offset: 0x0000B474
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000D27C File Offset: 0x0000B47C
		public TypeAttributes TypeAttributes
		{
			get
			{
				return this.attributes;
			}
			set
			{
				this.attributes = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type declaration is complete or partial.</summary>
		/// <returns>true if the class or structure declaration is a partial representation of the implementation; false if the declaration is a complete implementation of the class or structure. The default is false.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000D288 File Offset: 0x0000B488
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000D290 File Offset: 0x0000B490
		public bool IsPartial
		{
			get
			{
				return this.isPartial;
			}
			set
			{
				this.isPartial = value;
			}
		}

		/// <summary>Gets the type parameters for the type declaration.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> that contains the type parameters for the type declaration.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000D29C File Offset: 0x0000B49C
		[ComVisible(false)]
		public CodeTypeParameterCollection TypeParameters
		{
			get
			{
				if (this.typeParameters == null)
				{
					this.typeParameters = new CodeTypeParameterCollection();
				}
				return this.typeParameters;
			}
		}

		// Token: 0x040000FA RID: 250
		private CodeTypeReferenceCollection baseTypes;

		// Token: 0x040000FB RID: 251
		private CodeTypeMemberCollection members;

		// Token: 0x040000FC RID: 252
		private TypeAttributes attributes = TypeAttributes.Public;

		// Token: 0x040000FD RID: 253
		private bool isEnum;

		// Token: 0x040000FE RID: 254
		private bool isStruct;

		// Token: 0x040000FF RID: 255
		private int populated;

		// Token: 0x04000100 RID: 256
		private bool isPartial;

		// Token: 0x04000101 RID: 257
		private CodeTypeParameterCollection typeParameters;
	}
}
