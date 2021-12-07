using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000039 RID: 57
	internal class CompilationContext
	{
		// Token: 0x060003AA RID: 938 RVA: 0x00011378 File Offset: 0x0000F578
		public int AddGlobal(object global)
		{
			return CompilationContext.AddItemToList<object>(global, this.globals);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00011388 File Offset: 0x0000F588
		public object[] GetGlobals()
		{
			return this.globals.ToArray();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00011398 File Offset: 0x0000F598
		private static int AddItemToList<T>(T item, IList<T> list)
		{
			list.Add(item);
			return list.Count - 1;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000113AC File Offset: 0x0000F5AC
		public int AddCompilationUnit(LambdaExpression lambda)
		{
			this.DetectHoistedVariables(lambda);
			return this.AddCompilationUnit(null, lambda);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000113C0 File Offset: 0x0000F5C0
		public int AddCompilationUnit(EmitContext parent, LambdaExpression lambda)
		{
			EmitContext emitContext = new EmitContext(this, parent, lambda);
			int result = CompilationContext.AddItemToList<EmitContext>(emitContext, this.units);
			emitContext.Emit();
			return result;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000113EC File Offset: 0x0000F5EC
		private void DetectHoistedVariables(LambdaExpression lambda)
		{
			this.hoisted_map = new CompilationContext.HoistedVariableDetector().Process(lambda);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00011400 File Offset: 0x0000F600
		public List<ParameterExpression> GetHoistedLocals(LambdaExpression lambda)
		{
			if (this.hoisted_map == null)
			{
				return null;
			}
			List<ParameterExpression> result;
			this.hoisted_map.TryGetValue(lambda, out result);
			return result;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001142C File Offset: 0x0000F62C
		public object[] CreateHoistedLocals(int unit)
		{
			List<ParameterExpression> hoistedLocals = this.GetHoistedLocals(this.units[unit].Lambda);
			return new object[(hoistedLocals != null) ? hoistedLocals.Count : 0];
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00011468 File Offset: 0x0000F668
		public Expression IsolateExpression(ExecutionScope scope, object[] locals, Expression expression)
		{
			return new CompilationContext.ParameterReplacer(this, scope, locals).Transform(expression);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00011478 File Offset: 0x0000F678
		public Delegate CreateDelegate()
		{
			return this.CreateDelegate(0, new ExecutionScope(this));
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00011488 File Offset: 0x0000F688
		public Delegate CreateDelegate(int unit, ExecutionScope scope)
		{
			return this.units[unit].CreateDelegate(scope);
		}

		// Token: 0x040000BF RID: 191
		private List<object> globals = new List<object>();

		// Token: 0x040000C0 RID: 192
		private List<EmitContext> units = new List<EmitContext>();

		// Token: 0x040000C1 RID: 193
		private Dictionary<LambdaExpression, List<ParameterExpression>> hoisted_map;

		// Token: 0x0200003A RID: 58
		private class ParameterReplacer : ExpressionTransformer
		{
			// Token: 0x060003B5 RID: 949 RVA: 0x0001149C File Offset: 0x0000F69C
			public ParameterReplacer(CompilationContext context, ExecutionScope scope, object[] locals)
			{
				this.context = context;
				this.scope = scope;
				this.locals = locals;
			}

			// Token: 0x060003B6 RID: 950 RVA: 0x000114BC File Offset: 0x0000F6BC
			protected override Expression VisitParameter(ParameterExpression parameter)
			{
				ExecutionScope parent = this.scope;
				object[] array = this.locals;
				while (parent != null)
				{
					int num = this.IndexOfHoistedLocal(parent, parameter);
					if (num != -1)
					{
						return this.ReadHoistedLocalFromArray(array, num);
					}
					array = parent.Locals;
					parent = parent.Parent;
				}
				return parameter;
			}

			// Token: 0x060003B7 RID: 951 RVA: 0x0001150C File Offset: 0x0000F70C
			private Expression ReadHoistedLocalFromArray(object[] locals, int position)
			{
				return Expression.Field(Expression.Convert(Expression.ArrayIndex(Expression.Constant(locals), Expression.Constant(position)), locals[position].GetType()), "Value");
			}

			// Token: 0x060003B8 RID: 952 RVA: 0x00011548 File Offset: 0x0000F748
			private int IndexOfHoistedLocal(ExecutionScope scope, ParameterExpression parameter)
			{
				return this.context.units[scope.compilation_unit].IndexOfHoistedLocal(parameter);
			}

			// Token: 0x040000C2 RID: 194
			private CompilationContext context;

			// Token: 0x040000C3 RID: 195
			private ExecutionScope scope;

			// Token: 0x040000C4 RID: 196
			private object[] locals;
		}

		// Token: 0x0200003B RID: 59
		private class HoistedVariableDetector : ExpressionVisitor
		{
			// Token: 0x060003BA RID: 954 RVA: 0x0001157C File Offset: 0x0000F77C
			public Dictionary<LambdaExpression, List<ParameterExpression>> Process(LambdaExpression lambda)
			{
				this.Visit(lambda);
				return this.hoisted_map;
			}

			// Token: 0x060003BB RID: 955 RVA: 0x0001158C File Offset: 0x0000F78C
			protected override void VisitLambda(LambdaExpression lambda)
			{
				this.lambda = lambda;
				foreach (ParameterExpression key in lambda.Parameters)
				{
					this.parameter_to_lambda[key] = lambda;
				}
				base.VisitLambda(lambda);
			}

			// Token: 0x060003BC RID: 956 RVA: 0x00011604 File Offset: 0x0000F804
			protected override void VisitParameter(ParameterExpression parameter)
			{
				if (this.lambda.Parameters.Contains(parameter))
				{
					return;
				}
				this.Hoist(parameter);
			}

			// Token: 0x060003BD RID: 957 RVA: 0x00011624 File Offset: 0x0000F824
			private void Hoist(ParameterExpression parameter)
			{
				LambdaExpression key;
				if (!this.parameter_to_lambda.TryGetValue(parameter, out key))
				{
					return;
				}
				if (this.hoisted_map == null)
				{
					this.hoisted_map = new Dictionary<LambdaExpression, List<ParameterExpression>>();
				}
				List<ParameterExpression> list;
				if (!this.hoisted_map.TryGetValue(key, out list))
				{
					list = new List<ParameterExpression>();
					this.hoisted_map[key] = list;
				}
				list.Add(parameter);
			}

			// Token: 0x040000C5 RID: 197
			private Dictionary<ParameterExpression, LambdaExpression> parameter_to_lambda = new Dictionary<ParameterExpression, LambdaExpression>();

			// Token: 0x040000C6 RID: 198
			private Dictionary<LambdaExpression, List<ParameterExpression>> hoisted_map;

			// Token: 0x040000C7 RID: 199
			private LambdaExpression lambda;
		}
	}
}
