using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200003F RID: 63
	internal abstract class ExpressionVisitor
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x00012AC4 File Offset: 0x00010CC4
		protected virtual void Visit(Expression expression)
		{
			if (expression == null)
			{
				return;
			}
			switch (expression.NodeType)
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
				this.VisitBinary((BinaryExpression)expression);
				break;
			case ExpressionType.ArrayLength:
			case ExpressionType.Convert:
			case ExpressionType.ConvertChecked:
			case ExpressionType.Negate:
			case ExpressionType.UnaryPlus:
			case ExpressionType.NegateChecked:
			case ExpressionType.Not:
			case ExpressionType.Quote:
			case ExpressionType.TypeAs:
				this.VisitUnary((UnaryExpression)expression);
				break;
			case ExpressionType.Call:
				this.VisitMethodCall((MethodCallExpression)expression);
				break;
			case ExpressionType.Conditional:
				this.VisitConditional((ConditionalExpression)expression);
				break;
			case ExpressionType.Constant:
				this.VisitConstant((ConstantExpression)expression);
				break;
			case ExpressionType.Invoke:
				this.VisitInvocation((InvocationExpression)expression);
				break;
			case ExpressionType.Lambda:
				this.VisitLambda((LambdaExpression)expression);
				break;
			case ExpressionType.ListInit:
				this.VisitListInit((ListInitExpression)expression);
				break;
			case ExpressionType.MemberAccess:
				this.VisitMemberAccess((MemberExpression)expression);
				break;
			case ExpressionType.MemberInit:
				this.VisitMemberInit((MemberInitExpression)expression);
				break;
			case ExpressionType.New:
				this.VisitNew((NewExpression)expression);
				break;
			case ExpressionType.NewArrayInit:
			case ExpressionType.NewArrayBounds:
				this.VisitNewArray((NewArrayExpression)expression);
				break;
			case ExpressionType.Parameter:
				this.VisitParameter((ParameterExpression)expression);
				break;
			case ExpressionType.TypeIs:
				this.VisitTypeIs((TypeBinaryExpression)expression);
				break;
			default:
				throw new ArgumentException(string.Format("Unhandled expression type: '{0}'", expression.NodeType));
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00012CAC File Offset: 0x00010EAC
		protected virtual void VisitBinding(MemberBinding binding)
		{
			switch (binding.BindingType)
			{
			case MemberBindingType.Assignment:
				this.VisitMemberAssignment((MemberAssignment)binding);
				break;
			case MemberBindingType.MemberBinding:
				this.VisitMemberMemberBinding((MemberMemberBinding)binding);
				break;
			case MemberBindingType.ListBinding:
				this.VisitMemberListBinding((MemberListBinding)binding);
				break;
			default:
				throw new ArgumentException(string.Format("Unhandled binding type '{0}'", binding.BindingType));
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00012D28 File Offset: 0x00010F28
		protected virtual void VisitElementInitializer(ElementInit initializer)
		{
			this.VisitExpressionList(initializer.Arguments);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00012D38 File Offset: 0x00010F38
		protected virtual void VisitUnary(UnaryExpression unary)
		{
			this.Visit(unary.Operand);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00012D48 File Offset: 0x00010F48
		protected virtual void VisitBinary(BinaryExpression binary)
		{
			this.Visit(binary.Left);
			this.Visit(binary.Right);
			this.Visit(binary.Conversion);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00012D7C File Offset: 0x00010F7C
		protected virtual void VisitTypeIs(TypeBinaryExpression type)
		{
			this.Visit(type.Expression);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00012D8C File Offset: 0x00010F8C
		protected virtual void VisitConstant(ConstantExpression constant)
		{
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00012D90 File Offset: 0x00010F90
		protected virtual void VisitConditional(ConditionalExpression conditional)
		{
			this.Visit(conditional.Test);
			this.Visit(conditional.IfTrue);
			this.Visit(conditional.IfFalse);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00012DC4 File Offset: 0x00010FC4
		protected virtual void VisitParameter(ParameterExpression parameter)
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00012DC8 File Offset: 0x00010FC8
		protected virtual void VisitMemberAccess(MemberExpression member)
		{
			this.Visit(member.Expression);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00012DD8 File Offset: 0x00010FD8
		protected virtual void VisitMethodCall(MethodCallExpression methodCall)
		{
			this.Visit(methodCall.Object);
			this.VisitExpressionList(methodCall.Arguments);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00012E00 File Offset: 0x00011000
		protected virtual void VisitList<T>(ReadOnlyCollection<T> list, Action<T> visitor)
		{
			foreach (T obj in list)
			{
				visitor(obj);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00012E60 File Offset: 0x00011060
		protected virtual void VisitExpressionList(ReadOnlyCollection<Expression> list)
		{
			this.VisitList<Expression>(list, new Action<Expression>(this.Visit));
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00012E78 File Offset: 0x00011078
		protected virtual void VisitMemberAssignment(MemberAssignment assignment)
		{
			this.Visit(assignment.Expression);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00012E88 File Offset: 0x00011088
		protected virtual void VisitMemberMemberBinding(MemberMemberBinding binding)
		{
			this.VisitBindingList(binding.Bindings);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00012E98 File Offset: 0x00011098
		protected virtual void VisitMemberListBinding(MemberListBinding binding)
		{
			this.VisitElementInitializerList(binding.Initializers);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00012EA8 File Offset: 0x000110A8
		protected virtual void VisitBindingList(ReadOnlyCollection<MemberBinding> list)
		{
			this.VisitList<MemberBinding>(list, new Action<MemberBinding>(this.VisitBinding));
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00012EC0 File Offset: 0x000110C0
		protected virtual void VisitElementInitializerList(ReadOnlyCollection<ElementInit> list)
		{
			this.VisitList<ElementInit>(list, new Action<ElementInit>(this.VisitElementInitializer));
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00012ED8 File Offset: 0x000110D8
		protected virtual void VisitLambda(LambdaExpression lambda)
		{
			this.Visit(lambda.Body);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00012EE8 File Offset: 0x000110E8
		protected virtual void VisitNew(NewExpression nex)
		{
			this.VisitExpressionList(nex.Arguments);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00012EF8 File Offset: 0x000110F8
		protected virtual void VisitMemberInit(MemberInitExpression init)
		{
			this.VisitNew(init.NewExpression);
			this.VisitBindingList(init.Bindings);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00012F20 File Offset: 0x00011120
		protected virtual void VisitListInit(ListInitExpression init)
		{
			this.VisitNew(init.NewExpression);
			this.VisitElementInitializerList(init.Initializers);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00012F48 File Offset: 0x00011148
		protected virtual void VisitNewArray(NewArrayExpression newArray)
		{
			this.VisitExpressionList(newArray.Expressions);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00012F58 File Offset: 0x00011158
		protected virtual void VisitInvocation(InvocationExpression invocation)
		{
			this.VisitExpressionList(invocation.Arguments);
			this.Visit(invocation.Expression);
		}
	}
}
