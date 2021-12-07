using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates a new instance of a type.</summary>
	// Token: 0x02000053 RID: 83
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeObjectCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class.</summary>
		// Token: 0x060002CD RID: 717 RVA: 0x0000C668 File Offset: 0x0000A868
		public CodeObjectCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the object to create. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object. </param>
		// Token: 0x060002CE RID: 718 RVA: 0x0000C670 File Offset: 0x0000A870
		public CodeObjectCreateExpression(CodeTypeReference createType, params CodeExpression[] parameters)
		{
			this.createType = createType;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">The name of the data type of object to create. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object. </param>
		// Token: 0x060002CF RID: 719 RVA: 0x0000C68C File Offset: 0x0000A88C
		public CodeObjectCreateExpression(string createType, params CodeExpression[] parameters)
		{
			this.createType = new CodeTypeReference(createType);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">The data type of the object to create. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object. </param>
		// Token: 0x060002D0 RID: 720 RVA: 0x0000C6AC File Offset: 0x0000A8AC
		public CodeObjectCreateExpression(Type createType, params CodeExpression[] parameters)
		{
			this.createType = new CodeTypeReference(createType);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the data type of the object to create.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> to the data type of the object to create.</returns>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000C6CC File Offset: 0x0000A8CC
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		public CodeTypeReference CreateType
		{
			get
			{
				if (this.createType == null)
				{
					this.createType = new CodeTypeReference(string.Empty);
				}
				return this.createType;
			}
			set
			{
				this.createType = value;
			}
		}

		/// <summary>Gets or sets the parameters to use in creating the object.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the parameters to use when creating the object.</returns>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		public CodeExpressionCollection Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new CodeExpressionCollection();
				}
				return this.parameters;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000C71C File Offset: 0x0000A91C
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000DC RID: 220
		private CodeTypeReference createType;

		// Token: 0x040000DD RID: 221
		private CodeExpressionCollection parameters;
	}
}
