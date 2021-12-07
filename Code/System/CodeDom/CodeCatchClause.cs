using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a catch exception block of a try/catch statement.</summary>
	// Token: 0x0200002F RID: 47
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeCatchClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class.</summary>
		// Token: 0x060001A3 RID: 419 RVA: 0x0000AC64 File Offset: 0x00008E64
		public CodeCatchClause()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class using the specified local variable name for the exception.</summary>
		/// <param name="localName">The name of the local variable declared in the catch clause for the exception. This is optional. </param>
		// Token: 0x060001A4 RID: 420 RVA: 0x0000AC6C File Offset: 0x00008E6C
		public CodeCatchClause(string localName)
		{
			this.localName = localName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class using the specified local variable name for the exception and exception type.</summary>
		/// <param name="localName">The name of the local variable declared in the catch clause for the exception. This is optional. </param>
		/// <param name="catchExceptionType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of exception to catch. </param>
		// Token: 0x060001A5 RID: 421 RVA: 0x0000AC7C File Offset: 0x00008E7C
		public CodeCatchClause(string localName, CodeTypeReference catchExceptionType)
		{
			this.localName = localName;
			this.catchExceptionType = catchExceptionType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class using the specified local variable name for the exception, exception type and statement collection.</summary>
		/// <param name="localName">The name of the local variable declared in the catch clause for the exception. This is optional. </param>
		/// <param name="catchExceptionType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of exception to catch. </param>
		/// <param name="statements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that represent the contents of the catch block. </param>
		// Token: 0x060001A6 RID: 422 RVA: 0x0000AC94 File Offset: 0x00008E94
		public CodeCatchClause(string localName, CodeTypeReference catchExceptionType, params CodeStatement[] statements)
		{
			this.localName = localName;
			this.catchExceptionType = catchExceptionType;
			this.Statements.AddRange(statements);
		}

		/// <summary>Gets or sets the type of the exception to handle with the catch block.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the exception to handle.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000ACC4 File Offset: 0x00008EC4
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		public CodeTypeReference CatchExceptionType
		{
			get
			{
				if (this.catchExceptionType == null)
				{
					this.catchExceptionType = new CodeTypeReference(typeof(Exception));
				}
				return this.catchExceptionType;
			}
			set
			{
				this.catchExceptionType = value;
			}
		}

		/// <summary>Gets or sets the variable name of the exception that the catch clause handles.</summary>
		/// <returns>The name for the exception variable that the catch clause handles.</returns>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000AD04 File Offset: 0x00008F04
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000AD20 File Offset: 0x00008F20
		public string LocalName
		{
			get
			{
				if (this.localName == null)
				{
					return string.Empty;
				}
				return this.localName;
			}
			set
			{
				this.localName = value;
			}
		}

		/// <summary>Gets the statements within the catch block.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements within the catch block.</returns>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000AD2C File Offset: 0x00008F2C
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

		// Token: 0x04000086 RID: 134
		private CodeTypeReference catchExceptionType;

		// Token: 0x04000087 RID: 135
		private string localName;

		// Token: 0x04000088 RID: 136
		private CodeStatementCollection statements;
	}
}
