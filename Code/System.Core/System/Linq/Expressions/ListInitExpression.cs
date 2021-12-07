using System;
using System.Collections.ObjectModel;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x02000043 RID: 67
	public sealed class ListInitExpression : Expression
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x000139F4 File Offset: 0x00011BF4
		internal ListInitExpression(NewExpression new_expression, ReadOnlyCollection<ElementInit> initializers) : base(ExpressionType.ListInit, new_expression.Type)
		{
			this.new_expression = new_expression;
			this.initializers = initializers;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00013A14 File Offset: 0x00011C14
		public NewExpression NewExpression
		{
			get
			{
				return this.new_expression;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00013A1C File Offset: 0x00011C1C
		public ReadOnlyCollection<ElementInit> Initializers
		{
			get
			{
				return this.initializers;
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00013A24 File Offset: 0x00011C24
		internal override void Emit(EmitContext ec)
		{
			LocalBuilder local = ec.EmitStored(this.new_expression);
			ec.EmitCollection(this.initializers, local);
			ec.EmitLoad(local);
		}

		// Token: 0x04000102 RID: 258
		private NewExpression new_expression;

		// Token: 0x04000103 RID: 259
		private ReadOnlyCollection<ElementInit> initializers;
	}
}
