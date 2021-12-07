using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a try block with any number of catch clauses and, optionally, a finally block.</summary>
	// Token: 0x02000065 RID: 101
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTryCatchFinallyStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class.</summary>
		// Token: 0x0600033C RID: 828 RVA: 0x0000CE30 File Offset: 0x0000B030
		public CodeTryCatchFinallyStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class using the specified statements for try and catch clauses.</summary>
		/// <param name="tryStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the statements to try. </param>
		/// <param name="catchClauses">An array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects that indicate the clauses to catch. </param>
		// Token: 0x0600033D RID: 829 RVA: 0x0000CE38 File Offset: 0x0000B038
		public CodeTryCatchFinallyStatement(CodeStatement[] tryStatements, CodeCatchClause[] catchClauses)
		{
			this.TryStatements.AddRange(tryStatements);
			this.CatchClauses.AddRange(catchClauses);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class using the specified statements for try, catch clauses, and finally statements.</summary>
		/// <param name="tryStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the statements to try. </param>
		/// <param name="catchClauses">An array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects that indicate the clauses to catch. </param>
		/// <param name="finallyStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the finally statements to use. </param>
		// Token: 0x0600033E RID: 830 RVA: 0x0000CE64 File Offset: 0x0000B064
		public CodeTryCatchFinallyStatement(CodeStatement[] tryStatements, CodeCatchClause[] catchClauses, CodeStatement[] finallyStatements)
		{
			this.TryStatements.AddRange(tryStatements);
			this.CatchClauses.AddRange(catchClauses);
			this.FinallyStatements.AddRange(finallyStatements);
		}

		/// <summary>Gets the finally statements to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the finally statements.</returns>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000CE9C File Offset: 0x0000B09C
		public CodeStatementCollection FinallyStatements
		{
			get
			{
				if (this.finallyStatements == null)
				{
					this.finallyStatements = new CodeStatementCollection();
				}
				return this.finallyStatements;
			}
		}

		/// <summary>Gets the statements to try.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the statements to try.</returns>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		public CodeStatementCollection TryStatements
		{
			get
			{
				if (this.tryStatements == null)
				{
					this.tryStatements = new CodeStatementCollection();
				}
				return this.tryStatements;
			}
		}

		/// <summary>Gets the catch clauses to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> that indicates the catch clauses to use.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000CEDC File Offset: 0x0000B0DC
		public CodeCatchClauseCollection CatchClauses
		{
			get
			{
				if (this.catchClauses == null)
				{
					this.catchClauses = new CodeCatchClauseCollection();
				}
				return this.catchClauses;
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000F7 RID: 247
		private CodeStatementCollection tryStatements;

		// Token: 0x040000F8 RID: 248
		private CodeStatementCollection finallyStatements;

		// Token: 0x040000F9 RID: 249
		private CodeCatchClauseCollection catchClauses;
	}
}
