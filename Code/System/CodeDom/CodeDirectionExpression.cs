using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression used as a method invoke parameter along with a reference direction indicator.</summary>
	// Token: 0x0200003A RID: 58
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeDirectionExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectionExpression" /> class.</summary>
		// Token: 0x060001F6 RID: 502 RVA: 0x0000B2EC File Offset: 0x000094EC
		public CodeDirectionExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectionExpression" /> class using the specified field direction and expression.</summary>
		/// <param name="direction">A <see cref="T:System.CodeDom.FieldDirection" /> that indicates the field direction of the expression. </param>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the code expression to represent. </param>
		// Token: 0x060001F7 RID: 503 RVA: 0x0000B2F4 File Offset: 0x000094F4
		public CodeDirectionExpression(FieldDirection direction, CodeExpression expression)
		{
			this.direction = direction;
			this.expression = expression;
		}

		/// <summary>Gets or sets the field direction for this direction expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.FieldDirection" /> that indicates the field direction for this direction expression.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000B30C File Offset: 0x0000950C
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000B314 File Offset: 0x00009514
		public FieldDirection Direction
		{
			get
			{
				return this.direction;
			}
			set
			{
				this.direction = value;
			}
		}

		/// <summary>Gets or sets the code expression to represent.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to represent.</returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000B320 File Offset: 0x00009520
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000B328 File Offset: 0x00009528
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

		// Token: 0x060001FC RID: 508 RVA: 0x0000B334 File Offset: 0x00009534
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0400009F RID: 159
		private FieldDirection direction;

		// Token: 0x040000A0 RID: 160
		private CodeExpression expression;
	}
}
