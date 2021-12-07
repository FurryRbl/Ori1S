using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that consists of a binary operation between two expressions.</summary>
	// Token: 0x0200002B RID: 43
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeBinaryOperatorExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class.</summary>
		// Token: 0x06000184 RID: 388 RVA: 0x0000AA1C File Offset: 0x00008C1C
		public CodeBinaryOperatorExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class using the specified parameters.</summary>
		/// <param name="left">The <see cref="T:System.CodeDom.CodeExpression" /> on the left of the operator. </param>
		/// <param name="op">A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> indicating the type of operator. </param>
		/// <param name="right">The <see cref="T:System.CodeDom.CodeExpression" /> on the right of the operator. </param>
		// Token: 0x06000185 RID: 389 RVA: 0x0000AA24 File Offset: 0x00008C24
		public CodeBinaryOperatorExpression(CodeExpression left, CodeBinaryOperatorType op, CodeExpression right)
		{
			this.left = left;
			this.op = op;
			this.right = right;
		}

		/// <summary>Gets or sets the code expression on the left of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the left operand.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000AA44 File Offset: 0x00008C44
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000AA4C File Offset: 0x00008C4C
		public CodeExpression Left
		{
			get
			{
				return this.left;
			}
			set
			{
				this.left = value;
			}
		}

		/// <summary>Gets or sets the operator in the binary operator expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> that indicates the type of operator in the expression.</returns>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000AA58 File Offset: 0x00008C58
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000AA60 File Offset: 0x00008C60
		public CodeBinaryOperatorType Operator
		{
			get
			{
				return this.op;
			}
			set
			{
				this.op = value;
			}
		}

		/// <summary>Gets or sets the code expression on the right of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the right operand.</returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000AA6C File Offset: 0x00008C6C
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000AA74 File Offset: 0x00008C74
		public CodeExpression Right
		{
			get
			{
				return this.right;
			}
			set
			{
				this.right = value;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000AA80 File Offset: 0x00008C80
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0400006F RID: 111
		private CodeExpression left;

		// Token: 0x04000070 RID: 112
		private CodeExpression right;

		// Token: 0x04000071 RID: 113
		private CodeBinaryOperatorType op;
	}
}
