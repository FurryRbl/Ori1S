using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a return value statement.</summary>
	// Token: 0x0200004E RID: 78
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeMethodReturnStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class.</summary>
		// Token: 0x06000290 RID: 656 RVA: 0x0000BF44 File Offset: 0x0000A144
		public CodeMethodReturnStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the return value. </param>
		// Token: 0x06000291 RID: 657 RVA: 0x0000BF4C File Offset: 0x0000A14C
		public CodeMethodReturnStatement(CodeExpression expression)
		{
			this.expression = expression;
		}

		/// <summary>Gets or sets the return value.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the value to return for the return statement, or null if the statement is part of a subroutine.</returns>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000BF5C File Offset: 0x0000A15C
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000BF64 File Offset: 0x0000A164
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

		// Token: 0x06000294 RID: 660 RVA: 0x0000BF70 File Offset: 0x0000A170
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000CE RID: 206
		private CodeExpression expression;
	}
}
