using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200004B RID: 75
	public sealed class MethodCallExpression : Expression
	{
		// Token: 0x06000476 RID: 1142 RVA: 0x00013EA4 File Offset: 0x000120A4
		internal MethodCallExpression(MethodInfo method, ReadOnlyCollection<Expression> arguments) : base(ExpressionType.Call, method.ReturnType)
		{
			this.method = method;
			this.arguments = arguments;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00013EC4 File Offset: 0x000120C4
		internal MethodCallExpression(Expression obj, MethodInfo method, ReadOnlyCollection<Expression> arguments) : base(ExpressionType.Call, method.ReturnType)
		{
			this.obj = obj;
			this.method = method;
			this.arguments = arguments;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00013EF4 File Offset: 0x000120F4
		public Expression Object
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00013EFC File Offset: 0x000120FC
		public MethodInfo Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00013F04 File Offset: 0x00012104
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00013F0C File Offset: 0x0001210C
		internal override void Emit(EmitContext ec)
		{
			ec.EmitCall(this.obj, this.arguments, this.method);
		}

		// Token: 0x04000111 RID: 273
		private Expression obj;

		// Token: 0x04000112 RID: 274
		private MethodInfo method;

		// Token: 0x04000113 RID: 275
		private ReadOnlyCollection<Expression> arguments;
	}
}
