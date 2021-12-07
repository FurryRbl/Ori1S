using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a literal code fragment that can be compiled.</summary>
	// Token: 0x0200005D RID: 93
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeSnippetCompileUnit : CodeCompileUnit
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetCompileUnit" /> class. </summary>
		// Token: 0x0600030E RID: 782 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		public CodeSnippetCompileUnit()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetCompileUnit" /> class.</summary>
		/// <param name="value">The literal code fragment to represent. </param>
		// Token: 0x0600030F RID: 783 RVA: 0x0000CB24 File Offset: 0x0000AD24
		public CodeSnippetCompileUnit(string value)
		{
			this.value = value;
		}

		/// <summary>Gets or sets the line and file information about where the code is located in a source code document.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the position of the code fragment.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000CB34 File Offset: 0x0000AD34
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000CB3C File Offset: 0x0000AD3C
		public CodeLinePragma LinePragma
		{
			get
			{
				return this.linePragma;
			}
			set
			{
				this.linePragma = value;
			}
		}

		/// <summary>Gets or sets the literal code fragment to represent.</summary>
		/// <returns>The literal code fragment.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000CB48 File Offset: 0x0000AD48
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0000CB64 File Offset: 0x0000AD64
		public string Value
		{
			get
			{
				if (this.value == null)
				{
					return string.Empty;
				}
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x040000EE RID: 238
		private CodeLinePragma linePragma;

		// Token: 0x040000EF RID: 239
		private string value;
	}
}
