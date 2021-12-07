using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	// Token: 0x02000023 RID: 35
	internal class QueryableTransformer : ExpressionTransformer
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x0000C180 File Offset: 0x0000A380
		protected override Expression VisitMethodCall(MethodCallExpression methodCall)
		{
			if (QueryableTransformer.IsQueryableExtension(methodCall.Method))
			{
				return this.ReplaceQueryableMethod(methodCall);
			}
			return base.VisitMethodCall(methodCall);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		protected override Expression VisitLambda(LambdaExpression lambda)
		{
			return lambda;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
		protected override Expression VisitConstant(ConstantExpression constant)
		{
			IQueryableEnumerable queryableEnumerable = constant.Value as IQueryableEnumerable;
			if (queryableEnumerable == null)
			{
				return constant;
			}
			return Expression.Constant(queryableEnumerable.GetEnumerable());
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		private static bool IsQueryableExtension(MethodInfo method)
		{
			return QueryableTransformer.HasExtensionAttribute(method) && method.GetParameters()[0].ParameterType.IsAssignableTo(typeof(IQueryable));
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000C214 File Offset: 0x0000A414
		private static bool HasExtensionAttribute(MethodInfo method)
		{
			return method.GetCustomAttributes(typeof(ExtensionAttribute), false).Length > 0;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000C22C File Offset: 0x0000A42C
		private MethodCallExpression ReplaceQueryableMethod(MethodCallExpression old)
		{
			Expression obj = null;
			if (old.Object != null)
			{
				obj = this.Visit(old.Object);
			}
			MethodInfo methodInfo = QueryableTransformer.ReplaceQueryableMethod(old.Method);
			ParameterInfo[] parameters = methodInfo.GetParameters();
			Expression[] array = new Expression[old.Arguments.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = QueryableTransformer.UnquoteIfNeeded(this.Visit(old.Arguments[i]), parameters[i].ParameterType);
			}
			return new MethodCallExpression(obj, methodInfo, array.ToReadOnlyCollection<Expression>());
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000C2C4 File Offset: 0x0000A4C4
		private static Expression UnquoteIfNeeded(Expression expression, Type delegateType)
		{
			if (expression.NodeType != ExpressionType.Quote)
			{
				return expression;
			}
			LambdaExpression lambdaExpression = (LambdaExpression)((UnaryExpression)expression).Operand;
			if (lambdaExpression.Type == delegateType)
			{
				return lambdaExpression;
			}
			return expression;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000C300 File Offset: 0x0000A500
		private static Type GetTargetDeclaringType(MethodInfo method)
		{
			return (method.DeclaringType != typeof(Queryable)) ? method.DeclaringType : typeof(Enumerable);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000C338 File Offset: 0x0000A538
		private static MethodInfo ReplaceQueryableMethod(MethodInfo method)
		{
			MethodInfo matchingMethod = QueryableTransformer.GetMatchingMethod(method, QueryableTransformer.GetTargetDeclaringType(method));
			if (matchingMethod != null)
			{
				return matchingMethod;
			}
			throw new InvalidOperationException(string.Format("There is no method {0} on type {1} that matches the specified arguments", method.Name, method.DeclaringType.FullName));
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000C37C File Offset: 0x0000A57C
		private static MethodInfo GetMatchingMethod(MethodInfo method, Type declaring)
		{
			MethodInfo[] methods = declaring.GetMethods();
			int i = 0;
			while (i < methods.Length)
			{
				MethodInfo methodInfo = methods[i];
				if (!QueryableTransformer.MethodMatch(methodInfo, method))
				{
					i++;
				}
				else
				{
					if (method.IsGenericMethod)
					{
						return methodInfo.MakeGenericMethodFrom(method);
					}
					return methodInfo;
				}
			}
			return null;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000C3D0 File Offset: 0x0000A5D0
		private static bool MethodMatch(MethodInfo candidate, MethodInfo method)
		{
			if (candidate.Name != method.Name)
			{
				return false;
			}
			if (!QueryableTransformer.HasExtensionAttribute(candidate))
			{
				return false;
			}
			Type[] parameterTypes = method.GetParameterTypes();
			if (parameterTypes.Length != candidate.GetParameters().Length)
			{
				return false;
			}
			if (method.IsGenericMethod)
			{
				if (!candidate.IsGenericMethod)
				{
					return false;
				}
				if (candidate.GetGenericArguments().Length != method.GetGenericArguments().Length)
				{
					return false;
				}
				candidate = candidate.MakeGenericMethodFrom(method);
			}
			if (!QueryableTransformer.TypeMatch(candidate.ReturnType, method.ReturnType))
			{
				return false;
			}
			Type[] parameterTypes2 = candidate.GetParameterTypes();
			if (parameterTypes2[0] != QueryableTransformer.GetComparableType(parameterTypes[0]))
			{
				return false;
			}
			for (int i = 1; i < parameterTypes2.Length; i++)
			{
				if (!QueryableTransformer.TypeMatch(parameterTypes2[i], parameterTypes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000C4AC File Offset: 0x0000A6AC
		private static bool TypeMatch(Type candidate, Type type)
		{
			return candidate == type || candidate == QueryableTransformer.GetComparableType(type);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000C4C0 File Offset: 0x0000A6C0
		private static Type GetComparableType(Type type)
		{
			if (type.IsGenericInstanceOf(typeof(IQueryable<>)))
			{
				type = typeof(IEnumerable<>).MakeGenericTypeFrom(type);
			}
			else if (type.IsGenericInstanceOf(typeof(IOrderedQueryable<>)))
			{
				type = typeof(IOrderedEnumerable<>).MakeGenericTypeFrom(type);
			}
			else if (type.IsGenericInstanceOf(typeof(Expression<>)))
			{
				type = type.GetFirstGenericArgument();
			}
			else if (type == typeof(IQueryable))
			{
				type = typeof(IEnumerable);
			}
			return type;
		}
	}
}
