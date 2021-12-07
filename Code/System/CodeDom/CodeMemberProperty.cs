using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a property of a type.</summary>
	// Token: 0x0200004B RID: 75
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeMemberProperty : CodeTypeMember
	{
		/// <summary>Gets the collection of get statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the get statements for the member property.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000BC44 File Offset: 0x00009E44
		public CodeStatementCollection GetStatements
		{
			get
			{
				if (this.getStatements == null)
				{
					this.getStatements = new CodeStatementCollection();
				}
				return this.getStatements;
			}
		}

		/// <summary>Gets or sets a value indicating whether the property has a get method accessor.</summary>
		/// <returns>true if the Count property of the <see cref="P:System.CodeDom.CodeMemberProperty.GetStatements" /> collection is non-zero, or if the value of this property has been set to true; otherwise, false.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000BC64 File Offset: 0x00009E64
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000BC9C File Offset: 0x00009E9C
		public bool HasGet
		{
			get
			{
				return this.hasGet || (this.getStatements != null && this.getStatements.Count > 0);
			}
			set
			{
				this.hasGet = value;
				if (!this.hasGet && this.getStatements != null)
				{
					this.getStatements.Clear();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the property has a set method accessor.</summary>
		/// <returns>true if the <see cref="P:System.Collections.CollectionBase.Count" /> property of the <see cref="P:System.CodeDom.CodeMemberProperty.SetStatements" /> collection is non-zero; otherwise, false.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000BD0C File Offset: 0x00009F0C
		public bool HasSet
		{
			get
			{
				return this.hasSet || (this.setStatements != null && this.setStatements.Count > 0);
			}
			set
			{
				this.hasSet = value;
				if (!this.hasSet && this.setStatements != null)
				{
					this.setStatements.Clear();
				}
			}
		}

		/// <summary>Gets the data types of any interfaces that the property implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data types the property implements.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000BD44 File Offset: 0x00009F44
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				if (this.implementationTypes == null)
				{
					this.implementationTypes = new CodeTypeReferenceCollection();
				}
				return this.implementationTypes;
			}
		}

		/// <summary>Gets the collection of declaration expressions for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the declaration expressions for the property.</returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000BD64 File Offset: 0x00009F64
		public CodeParameterDeclarationExpressionCollection Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new CodeParameterDeclarationExpressionCollection();
				}
				return this.parameters;
			}
		}

		/// <summary>Gets or sets the data type of the interface, if any, this property, if private, implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the interface, if any, the property, if private, implements.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000BD84 File Offset: 0x00009F84
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000BD8C File Offset: 0x00009F8C
		public CodeTypeReference PrivateImplementationType
		{
			get
			{
				return this.privateImplementationType;
			}
			set
			{
				this.privateImplementationType = value;
			}
		}

		/// <summary>Gets the collection of set statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the set statements for the member property.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000BD98 File Offset: 0x00009F98
		public CodeStatementCollection SetStatements
		{
			get
			{
				if (this.setStatements == null)
				{
					this.setStatements = new CodeStatementCollection();
				}
				return this.setStatements;
			}
		}

		/// <summary>Gets or sets the data type of the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the property.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000BDDC File Offset: 0x00009FDC
		public CodeTypeReference Type
		{
			get
			{
				if (this.type == null)
				{
					this.type = new CodeTypeReference(string.Empty);
				}
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000BDE8 File Offset: 0x00009FE8
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000C1 RID: 193
		private CodeStatementCollection getStatements;

		// Token: 0x040000C2 RID: 194
		private bool hasGet;

		// Token: 0x040000C3 RID: 195
		private bool hasSet;

		// Token: 0x040000C4 RID: 196
		private CodeTypeReferenceCollection implementationTypes;

		// Token: 0x040000C5 RID: 197
		private CodeParameterDeclarationExpressionCollection parameters;

		// Token: 0x040000C6 RID: 198
		private CodeTypeReference privateImplementationType;

		// Token: 0x040000C7 RID: 199
		private CodeStatementCollection setStatements;

		// Token: 0x040000C8 RID: 200
		private CodeTypeReference type;
	}
}
