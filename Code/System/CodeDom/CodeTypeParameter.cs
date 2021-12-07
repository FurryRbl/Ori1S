using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a type parameter of a generic type or method.</summary>
	// Token: 0x0200006E RID: 110
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeTypeParameter : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameter" /> class. </summary>
		// Token: 0x0600039A RID: 922 RVA: 0x0000D71C File Offset: 0x0000B91C
		public CodeTypeParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameter" /> class with the specified type parameter name. </summary>
		/// <param name="name">The name of the type parameter.</param>
		// Token: 0x0600039B RID: 923 RVA: 0x0000D724 File Offset: 0x0000B924
		public CodeTypeParameter(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the constraints for the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> object that contains the constraints for the type parameter.</returns>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000D734 File Offset: 0x0000B934
		public CodeTypeReferenceCollection Constraints
		{
			get
			{
				if (this.constraints == null)
				{
					this.constraints = new CodeTypeReferenceCollection();
				}
				return this.constraints;
			}
		}

		/// <summary>Gets the custom attributes of the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes of the type parameter. The default is null.</returns>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000D754 File Offset: 0x0000B954
		public CodeAttributeDeclarationCollection CustomAttributes
		{
			get
			{
				if (this.customAttributes == null)
				{
					this.customAttributes = new CodeAttributeDeclarationCollection();
				}
				return this.customAttributes;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type parameter has a constructor constraint.</summary>
		/// <returns>true if the type parameter has a constructor constraint; otherwise, false. The default is false.</returns>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000D774 File Offset: 0x0000B974
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000D77C File Offset: 0x0000B97C
		public bool HasConstructorConstraint
		{
			get
			{
				return this.hasConstructorConstraint;
			}
			set
			{
				this.hasConstructorConstraint = value;
			}
		}

		/// <summary>Gets or sets the name of the type parameter.</summary>
		/// <returns>The name of the type parameter. The default is an empty string ("").</returns>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000D788 File Offset: 0x0000B988
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
		public string Name
		{
			get
			{
				if (this.name == null)
				{
					return string.Empty;
				}
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x0400010E RID: 270
		private CodeTypeReferenceCollection constraints;

		// Token: 0x0400010F RID: 271
		private CodeAttributeDeclarationCollection customAttributes;

		// Token: 0x04000110 RID: 272
		private bool hasConstructorConstraint;

		// Token: 0x04000111 RID: 273
		private string name;
	}
}
