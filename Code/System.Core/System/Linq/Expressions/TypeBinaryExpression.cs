using System;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x0200004F RID: 79
	public sealed class TypeBinaryExpression : Expression
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x000142B4 File Offset: 0x000124B4
		internal TypeBinaryExpression(ExpressionType node_type, Expression expression, Type type_operand, Type type) : base(node_type, type)
		{
			this.expression = expression;
			this.type_operand = type_operand;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x000142D0 File Offset: 0x000124D0
		public Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000142D8 File Offset: 0x000124D8
		public Type TypeOperand
		{
			get
			{
				return this.type_operand;
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000142E0 File Offset: 0x000124E0
		internal override void Emit(EmitContext ec)
		{
			if (this.expression.Type == typeof(void))
			{
				ec.ig.Emit(OpCodes.Ldc_I4_0);
				return;
			}
			ec.EmitIsInst(this.expression, this.type_operand);
			ec.ig.Emit(OpCodes.Ldnull);
			ec.ig.Emit(OpCodes.Cgt_Un);
		}

		// Token: 0x04000119 RID: 281
		private Expression expression;

		// Token: 0x0400011A RID: 282
		private Type type_operand;
	}
}
