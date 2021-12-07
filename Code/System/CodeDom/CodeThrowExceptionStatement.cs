using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement that throws an exception.</summary>
	// Token: 0x02000064 RID: 100
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeThrowExceptionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeThrowExceptionStatement" /> class.</summary>
		// Token: 0x06000337 RID: 823 RVA: 0x0000CDF8 File Offset: 0x0000AFF8
		public CodeThrowExceptionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeThrowExceptionStatement" /> class with the specified exception type instance.</summary>
		/// <param name="toThrow">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the exception to throw. </param>
		// Token: 0x06000338 RID: 824 RVA: 0x0000CE00 File Offset: 0x0000B000
		public CodeThrowExceptionStatement(CodeExpression toThrow)
		{
			this.toThrow = toThrow;
		}

		/// <summary>Gets or sets the exception to throw.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> representing an instance of the exception to throw.</returns>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000CE10 File Offset: 0x0000B010
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000CE18 File Offset: 0x0000B018
		public CodeExpression ToThrow
		{
			get
			{
				return this.toThrow;
			}
			set
			{
				this.toThrow = value;
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000CE24 File Offset: 0x0000B024
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000F6 RID: 246
		private CodeExpression toThrow;
	}
}
