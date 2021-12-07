using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a typeof expression, an expression that returns a <see cref="T:System.Type" /> for a specified type name.</summary>
	// Token: 0x0200006C RID: 108
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeTypeOfExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		// Token: 0x06000385 RID: 901 RVA: 0x0000D55C File Offset: 0x0000B75C
		public CodeTypeOfExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type for the typeof expression. </param>
		// Token: 0x06000386 RID: 902 RVA: 0x0000D564 File Offset: 0x0000B764
		public CodeTypeOfExpression(CodeTypeReference type)
		{
			this.type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class using the specified type.</summary>
		/// <param name="type">The name of the data type for the typeof expression. </param>
		// Token: 0x06000387 RID: 903 RVA: 0x0000D574 File Offset: 0x0000B774
		public CodeTypeOfExpression(string type)
		{
			this.type = new CodeTypeReference(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class using the specified type.</summary>
		/// <param name="type">The data type of the data type of the typeof expression. </param>
		// Token: 0x06000388 RID: 904 RVA: 0x0000D588 File Offset: 0x0000B788
		public CodeTypeOfExpression(Type type)
		{
			this.type = new CodeTypeReference(type);
		}

		/// <summary>Gets or sets the data type referenced by the typeof expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type referenced by the typeof expression. This property will never return null, and defaults to the <see cref="T:System.Void" /> type.</returns>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000D59C File Offset: 0x0000B79C
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		public CodeTypeReference Type
		{
			get
			{
				if (this.type == null)
				{
					this.type = new CodeTypeReference(string.Empty);
				}
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0400010D RID: 269
		private CodeTypeReference type;
	}
}
