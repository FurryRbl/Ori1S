using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a conditional branch statement, typically represented as an if statement.</summary>
	// Token: 0x02000035 RID: 53
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeConditionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class.</summary>
		// Token: 0x060001D5 RID: 469 RVA: 0x0000B06C File Offset: 0x0000926C
		public CodeConditionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class using the specified condition and statements.</summary>
		/// <param name="condition">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to evaluate. </param>
		/// <param name="trueStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is true. </param>
		// Token: 0x060001D6 RID: 470 RVA: 0x0000B074 File Offset: 0x00009274
		public CodeConditionStatement(CodeExpression condition, params CodeStatement[] trueStatements)
		{
			this.condition = condition;
			this.TrueStatements.AddRange(trueStatements);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class using the specified condition and statements.</summary>
		/// <param name="condition">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the condition to evaluate. </param>
		/// <param name="trueStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is true. </param>
		/// <param name="falseStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is false. </param>
		// Token: 0x060001D7 RID: 471 RVA: 0x0000B090 File Offset: 0x00009290
		public CodeConditionStatement(CodeExpression condition, CodeStatement[] trueStatements, CodeStatement[] falseStatements)
		{
			this.condition = condition;
			this.TrueStatements.AddRange(trueStatements);
			this.FalseStatements.AddRange(falseStatements);
		}

		/// <summary>Gets or sets the expression to evaluate true or false.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> to evaluate true or false.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000B0C4 File Offset: 0x000092C4
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000B0CC File Offset: 0x000092CC
		public CodeExpression Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				this.condition = value;
			}
		}

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to false.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to false.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000B0D8 File Offset: 0x000092D8
		public CodeStatementCollection FalseStatements
		{
			get
			{
				if (this.falseStatements == null)
				{
					this.falseStatements = new CodeStatementCollection();
				}
				return this.falseStatements;
			}
		}

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to true.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to true.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000B0F8 File Offset: 0x000092F8
		public CodeStatementCollection TrueStatements
		{
			get
			{
				if (this.trueStatements == null)
				{
					this.trueStatements = new CodeStatementCollection();
				}
				return this.trueStatements;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000B118 File Offset: 0x00009318
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000094 RID: 148
		private CodeExpression condition;

		// Token: 0x04000095 RID: 149
		private CodeStatementCollection trueStatements;

		// Token: 0x04000096 RID: 150
		private CodeStatementCollection falseStatements;
	}
}
