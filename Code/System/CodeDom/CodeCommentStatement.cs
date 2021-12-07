using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement consisting of a single comment.</summary>
	// Token: 0x02000033 RID: 51
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeCommentStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class.</summary>
		// Token: 0x060001C8 RID: 456 RVA: 0x0000AF60 File Offset: 0x00009160
		public CodeCommentStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class using the specified comment.</summary>
		/// <param name="comment">A <see cref="T:System.CodeDom.CodeComment" /> that indicates the comment. </param>
		// Token: 0x060001C9 RID: 457 RVA: 0x0000AF68 File Offset: 0x00009168
		public CodeCommentStatement(CodeComment comment)
		{
			this.comment = comment;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class using the specified text as contents.</summary>
		/// <param name="text">The contents of the comment. </param>
		// Token: 0x060001CA RID: 458 RVA: 0x0000AF78 File Offset: 0x00009178
		public CodeCommentStatement(string text)
		{
			this.comment = new CodeComment(text);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class using the specified text and documentation comment flag.</summary>
		/// <param name="text">The contents of the comment. </param>
		/// <param name="docComment">true if the comment is a documentation comment; otherwise, false. </param>
		// Token: 0x060001CB RID: 459 RVA: 0x0000AF8C File Offset: 0x0000918C
		public CodeCommentStatement(string text, bool docComment)
		{
			this.comment = new CodeComment(text, docComment);
		}

		/// <summary>Gets or sets the contents of the comment.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeComment" /> that indicates the comment.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000AFA4 File Offset: 0x000091A4
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000AFAC File Offset: 0x000091AC
		public CodeComment Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000AFB8 File Offset: 0x000091B8
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0400008E RID: 142
		private CodeComment comment;
	}
}
