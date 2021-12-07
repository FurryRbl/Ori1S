using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement that consists of a single expression.</summary>
	// Token: 0x02000041 RID: 65
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeExpressionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class.</summary>
		// Token: 0x06000222 RID: 546 RVA: 0x0000B638 File Offset: 0x00009838
		public CodeExpressionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class by using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> for the statement. </param>
		// Token: 0x06000223 RID: 547 RVA: 0x0000B640 File Offset: 0x00009840
		public CodeExpressionStatement(CodeExpression expression)
		{
			this.Expression = expression;
		}

		/// <summary>Gets or sets the expression for the statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression for the statement.</returns>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000B650 File Offset: 0x00009850
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000B658 File Offset: 0x00009858
		public CodeExpression Expression
		{
			get
			{
				return this.expression;
			}
			set
			{
				this.expression = value;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000B664 File Offset: 0x00009864
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000A3 RID: 163
		private CodeExpression expression;
	}
}
