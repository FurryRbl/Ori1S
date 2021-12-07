using System;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x02000036 RID: 54
	public sealed class ConditionalExpression : Expression
	{
		// Token: 0x06000398 RID: 920 RVA: 0x00010D64 File Offset: 0x0000EF64
		internal ConditionalExpression(Expression test, Expression if_true, Expression if_false) : base(ExpressionType.Conditional, if_true.Type)
		{
			this.test = test;
			this.if_true = if_true;
			this.if_false = if_false;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00010D94 File Offset: 0x0000EF94
		public Expression Test
		{
			get
			{
				return this.test;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00010D9C File Offset: 0x0000EF9C
		public Expression IfTrue
		{
			get
			{
				return this.if_true;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00010DA4 File Offset: 0x0000EFA4
		public Expression IfFalse
		{
			get
			{
				return this.if_false;
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00010DAC File Offset: 0x0000EFAC
		internal override void Emit(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			this.test.Emit(ec);
			ig.Emit(OpCodes.Brfalse, label);
			this.if_true.Emit(ec);
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			this.if_false.Emit(ec);
			ig.MarkLabel(label2);
		}

		// Token: 0x040000B9 RID: 185
		private Expression test;

		// Token: 0x040000BA RID: 186
		private Expression if_true;

		// Token: 0x040000BB RID: 187
		private Expression if_false;
	}
}
