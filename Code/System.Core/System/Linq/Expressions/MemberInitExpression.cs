using System;
using System.Collections.ObjectModel;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x02000048 RID: 72
	public sealed class MemberInitExpression : Expression
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x00013D2C File Offset: 0x00011F2C
		internal MemberInitExpression(NewExpression new_expression, ReadOnlyCollection<MemberBinding> bindings) : base(ExpressionType.MemberInit, new_expression.Type)
		{
			this.new_expression = new_expression;
			this.bindings = bindings;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00013D4C File Offset: 0x00011F4C
		public NewExpression NewExpression
		{
			get
			{
				return this.new_expression;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00013D54 File Offset: 0x00011F54
		public ReadOnlyCollection<MemberBinding> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00013D5C File Offset: 0x00011F5C
		internal override void Emit(EmitContext ec)
		{
			LocalBuilder local = ec.EmitStored(this.new_expression);
			ec.EmitCollection(this.bindings, local);
			ec.EmitLoad(local);
		}

		// Token: 0x0400010D RID: 269
		private NewExpression new_expression;

		// Token: 0x0400010E RID: 270
		private ReadOnlyCollection<MemberBinding> bindings;
	}
}
