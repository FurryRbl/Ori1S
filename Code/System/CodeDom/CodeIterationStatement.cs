using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a for statement, or a loop through a block of statements, using a test expression as a condition for continuing to loop.</summary>
	// Token: 0x02000045 RID: 69
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeIterationStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIterationStatement" /> class.</summary>
		// Token: 0x06000239 RID: 569 RVA: 0x0000B788 File Offset: 0x00009988
		public CodeIterationStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIterationStatement" /> class using the specified parameters.</summary>
		/// <param name="initStatement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the loop initialization statement. </param>
		/// <param name="testExpression">A <see cref="T:System.CodeDom.CodeExpression" /> containing the expression to test for exit condition. </param>
		/// <param name="incrementStatement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the per-cycle increment statement. </param>
		/// <param name="statements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements within the loop. </param>
		// Token: 0x0600023A RID: 570 RVA: 0x0000B790 File Offset: 0x00009990
		public CodeIterationStatement(CodeStatement initStatement, CodeExpression testExpression, CodeStatement incrementStatement, params CodeStatement[] statements)
		{
			this.initStatement = initStatement;
			this.testExpression = testExpression;
			this.incrementStatement = incrementStatement;
			this.Statements.AddRange(statements);
		}

		/// <summary>Gets or sets the statement that is called after each loop cycle.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the per cycle increment statement.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000B7C8 File Offset: 0x000099C8
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000B7D0 File Offset: 0x000099D0
		public CodeStatement IncrementStatement
		{
			get
			{
				return this.incrementStatement;
			}
			set
			{
				this.incrementStatement = value;
			}
		}

		/// <summary>Gets or sets the loop initialization statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the loop initialization statement.</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000B7DC File Offset: 0x000099DC
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000B7E4 File Offset: 0x000099E4
		public CodeStatement InitStatement
		{
			get
			{
				return this.initStatement;
			}
			set
			{
				this.initStatement = value;
			}
		}

		/// <summary>Gets the collection of statements to be executed within the loop.</summary>
		/// <returns>An array of type <see cref="T:System.CodeDom.CodeStatement" /> that indicates the statements within the loop.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000B7F0 File Offset: 0x000099F0
		public CodeStatementCollection Statements
		{
			get
			{
				if (this.statements == null)
				{
					this.statements = new CodeStatementCollection();
				}
				return this.statements;
			}
		}

		/// <summary>Gets or sets the expression to test as the condition that continues the loop.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to test.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000B810 File Offset: 0x00009A10
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000B818 File Offset: 0x00009A18
		public CodeExpression TestExpression
		{
			get
			{
				return this.testExpression;
			}
			set
			{
				this.testExpression = value;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000B824 File Offset: 0x00009A24
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000A9 RID: 169
		private CodeStatement incrementStatement;

		// Token: 0x040000AA RID: 170
		private CodeStatement initStatement;

		// Token: 0x040000AB RID: 171
		private CodeStatementCollection statements;

		// Token: 0x040000AC RID: 172
		private CodeExpression testExpression;
	}
}
