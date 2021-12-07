using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a labeled statement or a stand-alone label.</summary>
	// Token: 0x02000046 RID: 70
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeLabeledStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class.</summary>
		// Token: 0x06000243 RID: 579 RVA: 0x0000B830 File Offset: 0x00009A30
		public CodeLabeledStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class using the specified label name.</summary>
		/// <param name="label">The name of the label. </param>
		// Token: 0x06000244 RID: 580 RVA: 0x0000B838 File Offset: 0x00009A38
		public CodeLabeledStatement(string label)
		{
			this.label = label;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class using the specified label name and statement.</summary>
		/// <param name="label">The name of the label. </param>
		/// <param name="statement">The <see cref="T:System.CodeDom.CodeStatement" /> to associate with the label. </param>
		// Token: 0x06000245 RID: 581 RVA: 0x0000B848 File Offset: 0x00009A48
		public CodeLabeledStatement(string label, CodeStatement statement)
		{
			this.label = label;
			this.statement = statement;
		}

		/// <summary>Gets or sets the name of the label.</summary>
		/// <returns>The name of the label.</returns>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000B860 File Offset: 0x00009A60
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000B87C File Offset: 0x00009A7C
		public string Label
		{
			get
			{
				if (this.label == null)
				{
					return string.Empty;
				}
				return this.label;
			}
			set
			{
				this.label = value;
			}
		}

		/// <summary>Gets or sets the optional associated statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the statement associated with the label.</returns>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000B888 File Offset: 0x00009A88
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000B890 File Offset: 0x00009A90
		public CodeStatement Statement
		{
			get
			{
				return this.statement;
			}
			set
			{
				this.statement = value;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000B89C File Offset: 0x00009A9C
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000AD RID: 173
		private string label;

		// Token: 0x040000AE RID: 174
		private CodeStatement statement;
	}
}
