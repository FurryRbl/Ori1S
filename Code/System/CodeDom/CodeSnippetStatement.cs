using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement using a literal code fragment.</summary>
	// Token: 0x0200005F RID: 95
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeSnippetStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetStatement" /> class.</summary>
		// Token: 0x06000319 RID: 793 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public CodeSnippetStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetStatement" /> class using the specified code fragment.</summary>
		/// <param name="value">The literal code fragment of the statement to represent. </param>
		// Token: 0x0600031A RID: 794 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		public CodeSnippetStatement(string value)
		{
			this.value = value;
		}

		/// <summary>Gets or sets the literal code fragment statement.</summary>
		/// <returns>The literal code fragment statement.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000CBD4 File Offset: 0x0000ADD4
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
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

		// Token: 0x040000F1 RID: 241
		private string value;
	}
}
