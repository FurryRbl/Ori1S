using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a comment.</summary>
	// Token: 0x02000031 RID: 49
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeComment : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class.</summary>
		// Token: 0x060001B4 RID: 436 RVA: 0x0000ADC4 File Offset: 0x00008FC4
		public CodeComment()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class with the specified text as contents.</summary>
		/// <param name="text">The contents of the comment. </param>
		// Token: 0x060001B5 RID: 437 RVA: 0x0000ADCC File Offset: 0x00008FCC
		public CodeComment(string text)
		{
			this.text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class using the specified text and documentation comment flag.</summary>
		/// <param name="text">The contents of the comment. </param>
		/// <param name="docComment">true if the comment is a documentation comment; otherwise, false. </param>
		// Token: 0x060001B6 RID: 438 RVA: 0x0000ADDC File Offset: 0x00008FDC
		public CodeComment(string text, bool docComment)
		{
			this.text = text;
			this.docComment = docComment;
		}

		/// <summary>Gets or sets a value that indicates whether the comment is a documentation comment.</summary>
		/// <returns>true if the comment is a documentation comment; otherwise, false.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000ADF4 File Offset: 0x00008FF4
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000ADFC File Offset: 0x00008FFC
		public bool DocComment
		{
			get
			{
				return this.docComment;
			}
			set
			{
				this.docComment = value;
			}
		}

		/// <summary>Gets or sets the text of the comment.</summary>
		/// <returns>A string containing the comment text.</returns>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000AE08 File Offset: 0x00009008
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000AE24 File Offset: 0x00009024
		public string Text
		{
			get
			{
				if (this.text == null)
				{
					return string.Empty;
				}
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x0400008C RID: 140
		private bool docComment;

		// Token: 0x0400008D RID: 141
		private string text;
	}
}
