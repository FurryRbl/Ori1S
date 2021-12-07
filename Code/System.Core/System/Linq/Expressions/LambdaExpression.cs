using System;
using System.Collections.ObjectModel;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x02000004 RID: 4
	public class LambdaExpression : Expression
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002118 File Offset: 0x00000318
		internal LambdaExpression(Type delegateType, Expression body, ReadOnlyCollection<ParameterExpression> parameters) : base(ExpressionType.Lambda, delegateType)
		{
			this.body = body;
			this.parameters = parameters;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002134 File Offset: 0x00000334
		public Expression Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000213C File Offset: 0x0000033C
		public ReadOnlyCollection<ParameterExpression> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		private void EmitPopIfNeeded(EmitContext ec)
		{
			if (this.GetReturnType() == typeof(void) && this.body.Type != typeof(void))
			{
				ec.ig.Emit(OpCodes.Pop);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002190 File Offset: 0x00000390
		internal override void Emit(EmitContext ec)
		{
			ec.EmitCreateDelegate(this);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000219C File Offset: 0x0000039C
		internal void EmitBody(EmitContext ec)
		{
			this.body.Emit(ec);
			this.EmitPopIfNeeded(ec);
			ec.ig.Emit(OpCodes.Ret);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C4 File Offset: 0x000003C4
		internal Type GetReturnType()
		{
			return base.Type.GetInvokeMethod().ReturnType;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D8 File Offset: 0x000003D8
		public Delegate Compile()
		{
			CompilationContext compilationContext = new CompilationContext();
			compilationContext.AddCompilationUnit(this);
			return compilationContext.CreateDelegate();
		}

		// Token: 0x04000001 RID: 1
		private Expression body;

		// Token: 0x04000002 RID: 2
		private ReadOnlyCollection<ParameterExpression> parameters;
	}
}
