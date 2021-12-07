using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression cast to a data type or interface.</summary>
	// Token: 0x0200002D RID: 45
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeCastExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class.</summary>
		// Token: 0x0600018D RID: 397 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public CodeCastExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the destination type of the cast. </param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast. </param>
		// Token: 0x0600018E RID: 398 RVA: 0x0000AA94 File Offset: 0x00008C94
		public CodeCastExpression(CodeTypeReference targetType, CodeExpression expression)
		{
			this.targetType = targetType;
			this.expression = expression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">The name of the destination type of the cast. </param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast. </param>
		// Token: 0x0600018F RID: 399 RVA: 0x0000AAAC File Offset: 0x00008CAC
		public CodeCastExpression(string targetType, CodeExpression expression)
		{
			this.targetType = new CodeTypeReference(targetType);
			this.expression = expression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">The destination data type of the cast. </param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast. </param>
		// Token: 0x06000190 RID: 400 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		public CodeCastExpression(Type targetType, CodeExpression expression)
		{
			this.targetType = new CodeTypeReference(targetType);
			this.expression = expression;
		}

		/// <summary>Gets or sets the expression to cast.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the code to cast.</returns>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000AAE4 File Offset: 0x00008CE4
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000AAEC File Offset: 0x00008CEC
		public CodeExpression Expression
		{
			get
			{
				return this.expression;
			}
			set
			{
				this.expression = value;
			}
		}

		/// <summary>Gets or sets the destination type of the cast.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the destination type to cast to.</returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000AB1C File Offset: 0x00008D1C
		public CodeTypeReference TargetType
		{
			get
			{
				if (this.targetType == null)
				{
					this.targetType = new CodeTypeReference(string.Empty);
				}
				return this.targetType;
			}
			set
			{
				this.targetType = value;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000AB28 File Offset: 0x00008D28
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000084 RID: 132
		private CodeTypeReference targetType;

		// Token: 0x04000085 RID: 133
		private CodeExpression expression;
	}
}
