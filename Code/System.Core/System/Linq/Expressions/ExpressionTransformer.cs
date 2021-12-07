using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000040 RID: 64
	internal abstract class ExpressionTransformer
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x00012F88 File Offset: 0x00011188
		public Expression Transform(Expression expression)
		{
			return this.Visit(expression);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00012F94 File Offset: 0x00011194
		protected virtual Expression Visit(Expression exp)
		{
			if (exp == null)
			{
				return exp;
			}
			switch (exp.NodeType)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
			case ExpressionType.And:
			case ExpressionType.AndAlso:
			case ExpressionType.ArrayIndex:
			case ExpressionType.Coalesce:
			case ExpressionType.Divide:
			case ExpressionType.Equal:
			case ExpressionType.ExclusiveOr:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LeftShift:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
			case ExpressionType.NotEqual:
			case ExpressionType.Or:
			case ExpressionType.OrElse:
			case ExpressionType.Power:
			case ExpressionType.RightShift:
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				return this.VisitBinary((BinaryExpression)exp);
			case ExpressionType.ArrayLength:
			case ExpressionType.Convert:
			case ExpressionType.ConvertChecked:
			case ExpressionType.Negate:
			case ExpressionType.UnaryPlus:
			case ExpressionType.NegateChecked:
			case ExpressionType.Not:
			case ExpressionType.Quote:
			case ExpressionType.TypeAs:
				return this.VisitUnary((UnaryExpression)exp);
			case ExpressionType.Call:
				return this.VisitMethodCall((MethodCallExpression)exp);
			case ExpressionType.Conditional:
				return this.VisitConditional((ConditionalExpression)exp);
			case ExpressionType.Constant:
				return this.VisitConstant((ConstantExpression)exp);
			case ExpressionType.Invoke:
				return this.VisitInvocation((InvocationExpression)exp);
			case ExpressionType.Lambda:
				return this.VisitLambda((LambdaExpression)exp);
			case ExpressionType.ListInit:
				return this.VisitListInit((ListInitExpression)exp);
			case ExpressionType.MemberAccess:
				return this.VisitMemberAccess((MemberExpression)exp);
			case ExpressionType.MemberInit:
				return this.VisitMemberInit((MemberInitExpression)exp);
			case ExpressionType.New:
				return this.VisitNew((NewExpression)exp);
			case ExpressionType.NewArrayInit:
			case ExpressionType.NewArrayBounds:
				return this.VisitNewArray((NewArrayExpression)exp);
			case ExpressionType.Parameter:
				return this.VisitParameter((ParameterExpression)exp);
			case ExpressionType.TypeIs:
				return this.VisitTypeIs((TypeBinaryExpression)exp);
			default:
				throw new Exception(string.Format("Unhandled expression type: '{0}'", exp.NodeType));
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00013144 File Offset: 0x00011344
		protected virtual MemberBinding VisitBinding(MemberBinding binding)
		{
			switch (binding.BindingType)
			{
			case MemberBindingType.Assignment:
				return this.VisitMemberAssignment((MemberAssignment)binding);
			case MemberBindingType.MemberBinding:
				return this.VisitMemberMemberBinding((MemberMemberBinding)binding);
			case MemberBindingType.ListBinding:
				return this.VisitMemberListBinding((MemberListBinding)binding);
			default:
				throw new Exception(string.Format("Unhandled binding type '{0}'", binding.BindingType));
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000131B0 File Offset: 0x000113B0
		protected virtual ElementInit VisitElementInitializer(ElementInit initializer)
		{
			ReadOnlyCollection<Expression> readOnlyCollection = this.VisitExpressionList(initializer.Arguments);
			if (readOnlyCollection != initializer.Arguments)
			{
				return Expression.ElementInit(initializer.AddMethod, readOnlyCollection);
			}
			return initializer;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000131E4 File Offset: 0x000113E4
		protected virtual Expression VisitUnary(UnaryExpression u)
		{
			Expression expression = this.Visit(u.Operand);
			if (expression != u.Operand)
			{
				return Expression.MakeUnary(u.NodeType, expression, u.Type, u.Method);
			}
			return u;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00013224 File Offset: 0x00011424
		protected virtual Expression VisitBinary(BinaryExpression b)
		{
			Expression expression = this.Visit(b.Left);
			Expression expression2 = this.Visit(b.Right);
			Expression expression3 = this.Visit(b.Conversion);
			if (expression == b.Left && expression2 == b.Right && expression3 == b.Conversion)
			{
				return b;
			}
			if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
			{
				return Expression.Coalesce(expression, expression2, expression3 as LambdaExpression);
			}
			return Expression.MakeBinary(b.NodeType, expression, expression2, b.IsLiftedToNull, b.Method);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000132BC File Offset: 0x000114BC
		protected virtual Expression VisitTypeIs(TypeBinaryExpression b)
		{
			Expression expression = this.Visit(b.Expression);
			if (expression != b.Expression)
			{
				return Expression.TypeIs(expression, b.TypeOperand);
			}
			return b;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000132F0 File Offset: 0x000114F0
		protected virtual Expression VisitConstant(ConstantExpression c)
		{
			return c;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000132F4 File Offset: 0x000114F4
		protected virtual Expression VisitConditional(ConditionalExpression c)
		{
			Expression expression = this.Visit(c.Test);
			Expression expression2 = this.Visit(c.IfTrue);
			Expression expression3 = this.Visit(c.IfFalse);
			if (expression != c.Test || expression2 != c.IfTrue || expression3 != c.IfFalse)
			{
				return Expression.Condition(expression, expression2, expression3);
			}
			return c;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00013358 File Offset: 0x00011558
		protected virtual Expression VisitParameter(ParameterExpression p)
		{
			return p;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001335C File Offset: 0x0001155C
		protected virtual Expression VisitMemberAccess(MemberExpression m)
		{
			Expression expression = this.Visit(m.Expression);
			if (expression != m.Expression)
			{
				return Expression.MakeMemberAccess(expression, m.Member);
			}
			return m;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00013390 File Offset: 0x00011590
		protected virtual Expression VisitMethodCall(MethodCallExpression m)
		{
			Expression expression = this.Visit(m.Object);
			IEnumerable<Expression> enumerable = this.VisitExpressionList(m.Arguments);
			if (expression != m.Object || enumerable != m.Arguments)
			{
				return Expression.Call(expression, m.Method, enumerable);
			}
			return m;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000133E0 File Offset: 0x000115E0
		protected virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
		{
			IList<Expression> list = this.VisitList<Expression>(original, new Func<Expression, Expression>(this.Visit));
			if (list == null)
			{
				return original;
			}
			return new ReadOnlyCollection<Expression>(list);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00013410 File Offset: 0x00011610
		protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
		{
			Expression expression = this.Visit(assignment.Expression);
			if (expression != assignment.Expression)
			{
				return Expression.Bind(assignment.Member, expression);
			}
			return assignment;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00013444 File Offset: 0x00011644
		protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
		{
			IEnumerable<MemberBinding> enumerable = this.VisitBindingList(binding.Bindings);
			if (enumerable != binding.Bindings)
			{
				return Expression.MemberBind(binding.Member, enumerable);
			}
			return binding;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00013478 File Offset: 0x00011678
		protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding)
		{
			IEnumerable<ElementInit> enumerable = this.VisitElementInitializerList(binding.Initializers);
			if (enumerable != binding.Initializers)
			{
				return Expression.ListBind(binding.Member, enumerable);
			}
			return binding;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000134AC File Offset: 0x000116AC
		protected virtual IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original)
		{
			return this.VisitList<MemberBinding>(original, new Func<MemberBinding, MemberBinding>(this.VisitBinding));
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000134C4 File Offset: 0x000116C4
		protected virtual IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original)
		{
			return this.VisitList<ElementInit>(original, new Func<ElementInit, ElementInit>(this.VisitElementInitializer));
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000134DC File Offset: 0x000116DC
		private IList<TElement> VisitList<TElement>(ReadOnlyCollection<TElement> original, Func<TElement, TElement> visit)
		{
			List<TElement> list = null;
			int i = 0;
			int count = original.Count;
			while (i < count)
			{
				TElement telement = visit(original[i]);
				if (list != null)
				{
					list.Add(telement);
				}
				else if (!EqualityComparer<TElement>.Default.Equals(telement, original[i]))
				{
					list = new List<TElement>(count);
					for (int j = 0; j < i; j++)
					{
						list.Add(original[j]);
					}
					list.Add(telement);
				}
				i++;
			}
			if (list != null)
			{
				return list;
			}
			return original;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00013578 File Offset: 0x00011778
		protected virtual Expression VisitLambda(LambdaExpression lambda)
		{
			Expression expression = this.Visit(lambda.Body);
			if (expression != lambda.Body)
			{
				return Expression.Lambda(lambda.Type, expression, lambda.Parameters);
			}
			return lambda;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000135B4 File Offset: 0x000117B4
		protected virtual NewExpression VisitNew(NewExpression nex)
		{
			IEnumerable<Expression> enumerable = this.VisitExpressionList(nex.Arguments);
			if (enumerable == nex.Arguments)
			{
				return nex;
			}
			if (nex.Members != null)
			{
				return Expression.New(nex.Constructor, enumerable, nex.Members);
			}
			return Expression.New(nex.Constructor, enumerable);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00013608 File Offset: 0x00011808
		protected virtual Expression VisitMemberInit(MemberInitExpression init)
		{
			NewExpression newExpression = this.VisitNew(init.NewExpression);
			IEnumerable<MemberBinding> enumerable = this.VisitBindingList(init.Bindings);
			if (newExpression != init.NewExpression || enumerable != init.Bindings)
			{
				return Expression.MemberInit(newExpression, enumerable);
			}
			return init;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00013650 File Offset: 0x00011850
		protected virtual Expression VisitListInit(ListInitExpression init)
		{
			NewExpression newExpression = this.VisitNew(init.NewExpression);
			IEnumerable<ElementInit> enumerable = this.VisitElementInitializerList(init.Initializers);
			if (newExpression != init.NewExpression || enumerable != init.Initializers)
			{
				return Expression.ListInit(newExpression, enumerable);
			}
			return init;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00013698 File Offset: 0x00011898
		protected virtual Expression VisitNewArray(NewArrayExpression na)
		{
			IEnumerable<Expression> enumerable = this.VisitExpressionList(na.Expressions);
			if (enumerable == na.Expressions)
			{
				return na;
			}
			if (na.NodeType == ExpressionType.NewArrayInit)
			{
				return Expression.NewArrayInit(na.Type.GetElementType(), enumerable);
			}
			return Expression.NewArrayBounds(na.Type.GetElementType(), enumerable);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000136F0 File Offset: 0x000118F0
		protected virtual Expression VisitInvocation(InvocationExpression iv)
		{
			IEnumerable<Expression> enumerable = this.VisitExpressionList(iv.Arguments);
			Expression expression = this.Visit(iv.Expression);
			if (enumerable != iv.Arguments || expression != iv.Expression)
			{
				return Expression.Invoke(expression, enumerable);
			}
			return iv;
		}
	}
}
