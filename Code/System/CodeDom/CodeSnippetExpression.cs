using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a literal expression.</summary>
	// Token: 0x0200005E RID: 94
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeSnippetExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetExpression" /> class.</summary>
		// Token: 0x06000314 RID: 788 RVA: 0x0000CB70 File Offset: 0x0000AD70
		public CodeSnippetExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetExpression" /> class using the specified literal expression.</summary>
		/// <param name="value">The literal expression to represent. </param>
		// Token: 0x06000315 RID: 789 RVA: 0x0000CB78 File Offset: 0x0000AD78
		public CodeSnippetExpression(string value)
		{
			this.value = value;
		}

		/// <summary>Gets or sets the literal string of code.</summary>
		/// <returns>The literal string.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000CB88 File Offset: 0x0000AD88
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
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

		// Token: 0x06000318 RID: 792 RVA: 0x0000CBB0 File Offset: 0x0000ADB0
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000F0 RID: 240
		private string value;
	}
}
