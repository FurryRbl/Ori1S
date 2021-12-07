using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a method of a type.</summary>
	// Token: 0x0200004A RID: 74
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeMemberMethod : CodeTypeMember
	{
		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeMemberMethod.ImplementationTypes" /> collection is accessed.</summary>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000262 RID: 610 RVA: 0x0000BA2C File Offset: 0x00009C2C
		// (remove) Token: 0x06000263 RID: 611 RVA: 0x0000BA48 File Offset: 0x00009C48
		public event EventHandler PopulateImplementationTypes;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeMemberMethod.Parameters" /> collection is accessed.</summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000264 RID: 612 RVA: 0x0000BA64 File Offset: 0x00009C64
		// (remove) Token: 0x06000265 RID: 613 RVA: 0x0000BA80 File Offset: 0x00009C80
		public event EventHandler PopulateParameters;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeMemberMethod.Statements" /> collection is accessed.</summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000266 RID: 614 RVA: 0x0000BA9C File Offset: 0x00009C9C
		// (remove) Token: 0x06000267 RID: 615 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		public event EventHandler PopulateStatements;

		/// <summary>Gets the data types of the interfaces implemented by this method, unless it is a private method implementation, which is indicated by the <see cref="P:System.CodeDom.CodeMemberMethod.PrivateImplementationType" /> property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the interfaces implemented by this method.</returns>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				if (this.implementationTypes == null)
				{
					this.implementationTypes = new CodeTypeReferenceCollection();
					if (this.PopulateImplementationTypes != null)
					{
						this.PopulateImplementationTypes(this, EventArgs.Empty);
					}
				}
				return this.implementationTypes;
			}
		}

		/// <summary>Gets the parameter declarations for the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the method parameters.</returns>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000BB1C File Offset: 0x00009D1C
		public CodeParameterDeclarationExpressionCollection Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new CodeParameterDeclarationExpressionCollection();
					if (this.PopulateParameters != null)
					{
						this.PopulateParameters(this, EventArgs.Empty);
					}
				}
				return this.parameters;
			}
		}

		/// <summary>Gets or sets the data type of the interface this method, if private, implements a method of, if any.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the interface with the method that the private method whose declaration is represented by this <see cref="T:System.CodeDom.CodeMemberMethod" /> implements.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000BB64 File Offset: 0x00009D64
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000BB6C File Offset: 0x00009D6C
		public CodeTypeReference PrivateImplementationType
		{
			get
			{
				return this.privateImplements;
			}
			set
			{
				this.privateImplements = value;
			}
		}

		/// <summary>Gets or sets the data type of the return value of the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the value returned by the method.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000BB78 File Offset: 0x00009D78
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000BB9C File Offset: 0x00009D9C
		public CodeTypeReference ReturnType
		{
			get
			{
				if (this.returnType == null)
				{
					return new CodeTypeReference(typeof(void));
				}
				return this.returnType;
			}
			set
			{
				this.returnType = value;
			}
		}

		/// <summary>Gets the statements within the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the statements within the method.</returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000BBA8 File Offset: 0x00009DA8
		public CodeStatementCollection Statements
		{
			get
			{
				if (this.statements == null)
				{
					this.statements = new CodeStatementCollection();
					if (this.PopulateStatements != null)
					{
						this.PopulateStatements(this, EventArgs.Empty);
					}
				}
				return this.statements;
			}
		}

		/// <summary>Gets the custom attributes of the return type of the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000BBF0 File Offset: 0x00009DF0
		public CodeAttributeDeclarationCollection ReturnTypeCustomAttributes
		{
			get
			{
				if (this.returnAttributes == null)
				{
					this.returnAttributes = new CodeAttributeDeclarationCollection();
				}
				return this.returnAttributes;
			}
		}

		/// <summary>Gets the type parameters for the current generic method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> that contains the type parameters for the generic method.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000BC10 File Offset: 0x00009E10
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

		// Token: 0x06000271 RID: 625 RVA: 0x0000BC30 File Offset: 0x00009E30
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000B6 RID: 182
		private CodeTypeReferenceCollection implementationTypes;

		// Token: 0x040000B7 RID: 183
		private CodeParameterDeclarationExpressionCollection parameters;

		// Token: 0x040000B8 RID: 184
		private CodeTypeReference privateImplements;

		// Token: 0x040000B9 RID: 185
		private CodeTypeReference returnType;

		// Token: 0x040000BA RID: 186
		private CodeStatementCollection statements;

		// Token: 0x040000BB RID: 187
		private CodeAttributeDeclarationCollection returnAttributes;

		// Token: 0x040000BC RID: 188
		private int populated;

		// Token: 0x040000BD RID: 189
		private CodeTypeParameterCollection typeParameters;
	}
}
