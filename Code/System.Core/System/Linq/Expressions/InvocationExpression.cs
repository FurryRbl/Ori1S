using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000042 RID: 66
	public sealed class InvocationExpression : Expression
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x00013998 File Offset: 0x00011B98
		internal InvocationExpression(Expression expression, Type type, ReadOnlyCollection<Expression> arguments) : base(ExpressionType.Invoke, type)
		{
			this.expression = expression;
			this.arguments = arguments;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x000139B4 File Offset: 0x00011BB4
		public Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x000139BC File Offset: 0x00011BBC
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000139C4 File Offset: 0x00011BC4
		internal override void Emit(EmitContext ec)
		{
			ec.EmitCall(this.expression, this.arguments, this.expression.Type.GetInvokeMethod());
		}

		// Token: 0x04000100 RID: 256
		private Expression expression;

		// Token: 0x04000101 RID: 257
		private ReadOnlyCollection<Expression> arguments;
	}
}
