using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a simple assignment statement.</summary>
	// Token: 0x02000024 RID: 36
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeAssignStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAssignStatement" /> class.</summary>
		// Token: 0x06000149 RID: 329 RVA: 0x0000A564 File Offset: 0x00008764
		public CodeAssignStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAssignStatement" /> class using the specified expressions.</summary>
		/// <param name="left">The variable to assign to. </param>
		/// <param name="right">The value to assign. </param>
		// Token: 0x0600014A RID: 330 RVA: 0x0000A56C File Offset: 0x0000876C
		public CodeAssignStatement(CodeExpression left, CodeExpression right)
		{
			this.left = left;
			this.right = right;
		}

		/// <summary>Gets or sets the expression representing the object or reference to assign to.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object or reference to assign to.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000A584 File Offset: 0x00008784
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000A58C File Offset: 0x0000878C
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

		/// <summary>Gets or sets the expression representing the object or reference to assign.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object or reference to assign.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000A598 File Offset: 0x00008798
		// (set) Token: 0x0600014E RID: 334 RVA: 0x0000A5A0 File Offset: 0x000087A0
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

		// Token: 0x0600014F RID: 335 RVA: 0x0000A5AC File Offset: 0x000087AC
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000066 RID: 102
		private CodeExpression left;

		// Token: 0x04000067 RID: 103
		private CodeExpression right;
	}
}
