using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000005 RID: 5
	public abstract class Expression
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021FC File Offset: 0x000003FC
		protected Expression(ExpressionType node_type, Type type)
		{
			this.node_type = node_type;
			this.type = type;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002214 File Offset: 0x00000414
		public ExpressionType NodeType
		{
			get
			{
				return this.node_type;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000221C File Offset: 0x0000041C
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002224 File Offset: 0x00000424
		public override string ToString()
		{
			return ExpressionPrinter.ToString(this);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000222C File Offset: 0x0000042C
		private static MethodInfo GetUnaryOperator(string oper_name, Type declaring, Type param)
		{
			return Expression.GetUnaryOperator(oper_name, declaring, param, null);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002238 File Offset: 0x00000438
		private static MethodInfo GetUnaryOperator(string oper_name, Type declaring, Type param, Type ret)
		{
			MethodInfo[] methods = declaring.GetNotNullableType().GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			foreach (MethodInfo methodInfo in methods)
			{
				if (!(methodInfo.Name != oper_name))
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters.Length == 1)
					{
						if (!methodInfo.IsGenericMethod)
						{
							if (Expression.IsAssignableToParameterType(param.GetNotNullableType(), parameters[0]))
							{
								if (ret == null || methodInfo.ReturnType == ret.GetNotNullableType())
								{
									return methodInfo;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022E4 File Offset: 0x000004E4
		internal static MethodInfo GetTrueOperator(Type self)
		{
			return Expression.GetBooleanOperator("op_True", self);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022F4 File Offset: 0x000004F4
		internal static MethodInfo GetFalseOperator(Type self)
		{
			return Expression.GetBooleanOperator("op_False", self);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002304 File Offset: 0x00000504
		private static MethodInfo GetBooleanOperator(string op, Type self)
		{
			return Expression.GetUnaryOperator(op, self, self, typeof(bool));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002318 File Offset: 0x00000518
		private static bool IsAssignableToParameterType(Type type, ParameterInfo param)
		{
			Type type2 = param.ParameterType;
			if (type2.IsByRef)
			{
				type2 = type2.GetElementType();
			}
			return type.GetNotNullableType().IsAssignableTo(type2);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000234C File Offset: 0x0000054C
		private static MethodInfo CheckUnaryMethod(MethodInfo method, Type param)
		{
			if (method.ReturnType == typeof(void))
			{
				throw new ArgumentException("Specified method must return a value", "method");
			}
			if (!method.IsStatic)
			{
				throw new ArgumentException("Method must be static", "method");
			}
			ParameterInfo[] parameters = method.GetParameters();
			if (parameters.Length != 1)
			{
				throw new ArgumentException("Must have only one parameters", "method");
			}
			if (!Expression.IsAssignableToParameterType(param.GetNotNullableType(), parameters[0]))
			{
				throw new InvalidOperationException("left-side argument type does not match expression type");
			}
			return method;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D8 File Offset: 0x000005D8
		private static MethodInfo UnaryCoreCheck(string oper_name, Expression expression, MethodInfo method, Func<Type, bool> validator)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (method != null)
			{
				return Expression.CheckUnaryMethod(method, expression.Type);
			}
			Type notNullableType = expression.Type.GetNotNullableType();
			if (validator(notNullableType))
			{
				return null;
			}
			if (oper_name != null)
			{
				method = Expression.GetUnaryOperator(oper_name, notNullableType, expression.Type);
				if (method != null)
				{
					return method;
				}
			}
			throw new InvalidOperationException(string.Format("Operation {0} not defined for {1}", (oper_name == null) ? "is" : oper_name.Substring(3), expression.Type));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000246C File Offset: 0x0000066C
		private static MethodInfo GetBinaryOperator(string oper_name, Type on_type, Expression left, Expression right)
		{
			MethodInfo[] methods = on_type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			foreach (MethodInfo methodInfo in methods)
			{
				if (!(methodInfo.Name != oper_name))
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters.Length == 2)
					{
						if (!methodInfo.IsGenericMethod)
						{
							if (Expression.IsAssignableToParameterType(left.Type, parameters[0]))
							{
								if (Expression.IsAssignableToParameterType(right.Type, parameters[1]))
								{
									return methodInfo;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002510 File Offset: 0x00000710
		private static MethodInfo BinaryCoreCheck(string oper_name, Expression left, Expression right, MethodInfo method)
		{
			if (left == null)
			{
				throw new ArgumentNullException("left");
			}
			if (right == null)
			{
				throw new ArgumentNullException("right");
			}
			if (method != null)
			{
				if (method.ReturnType == typeof(void))
				{
					throw new ArgumentException("Specified method must return a value", "method");
				}
				if (!method.IsStatic)
				{
					throw new ArgumentException("Method must be static", "method");
				}
				ParameterInfo[] parameters = method.GetParameters();
				if (parameters.Length != 2)
				{
					throw new ArgumentException("Must have only two parameters", "method");
				}
				if (!Expression.IsAssignableToParameterType(left.Type, parameters[0]))
				{
					throw new InvalidOperationException("left-side argument type does not match left expression type");
				}
				if (!Expression.IsAssignableToParameterType(right.Type, parameters[1]))
				{
					throw new InvalidOperationException("right-side argument type does not match right expression type");
				}
				return method;
			}
			else
			{
				Type type = left.Type;
				Type type2 = right.Type;
				Type notNullableType = type.GetNotNullableType();
				Type notNullableType2 = type2.GetNotNullableType();
				if ((oper_name == "op_BitwiseOr" || oper_name == "op_BitwiseAnd") && notNullableType == typeof(bool) && notNullableType == notNullableType2 && type == type2)
				{
					return null;
				}
				if (Expression.IsNumber(notNullableType))
				{
					if (notNullableType == notNullableType2 && type == type2)
					{
						return null;
					}
					if (oper_name != null)
					{
						method = Expression.GetBinaryOperator(oper_name, notNullableType2, left, right);
						if (method != null)
						{
							return method;
						}
					}
				}
				if (oper_name != null)
				{
					method = Expression.GetBinaryOperator(oper_name, notNullableType, left, right);
					if (method != null)
					{
						return method;
					}
				}
				if (oper_name == "op_Equality" || oper_name == "op_Inequality")
				{
					if (!type.IsValueType && !type2.IsValueType)
					{
						return null;
					}
					if (type == type2 && notNullableType.IsEnum)
					{
						return null;
					}
					if (type == type2 && notNullableType == typeof(bool))
					{
						return null;
					}
				}
				if ((oper_name == "op_LeftShift" || oper_name == "op_RightShift") && Expression.IsInt(notNullableType) && notNullableType2 == typeof(int))
				{
					return null;
				}
				throw new InvalidOperationException(string.Format("Operation {0} not defined for {1} and {2}", (oper_name == null) ? "is" : oper_name.Substring(3), type, type2));
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000275C File Offset: 0x0000095C
		private static MethodInfo BinaryBitwiseCoreCheck(string oper_name, Expression left, Expression right, MethodInfo method)
		{
			if (left == null)
			{
				throw new ArgumentNullException("left");
			}
			if (right == null)
			{
				throw new ArgumentNullException("right");
			}
			if (method == null && left.Type == right.Type && Expression.IsIntOrBool(left.Type))
			{
				return null;
			}
			method = Expression.BinaryCoreCheck(oper_name, left, right, method);
			if (method == null && (left.Type == typeof(double) || left.Type == typeof(float)))
			{
				throw new InvalidOperationException("Types not supported");
			}
			return method;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000027FC File Offset: 0x000009FC
		private static BinaryExpression MakeSimpleBinary(ExpressionType et, Expression left, Expression right, MethodInfo method)
		{
			bool flag;
			Type type;
			if (method == null)
			{
				flag = left.Type.IsNullable();
				type = left.Type;
			}
			else
			{
				ParameterInfo[] parameters = method.GetParameters();
				ParameterInfo parameterInfo = parameters[0];
				ParameterInfo parameterInfo2 = parameters[1];
				if (Expression.IsAssignableToOperatorParameter(left, parameterInfo) && Expression.IsAssignableToOperatorParameter(right, parameterInfo2))
				{
					flag = false;
					type = method.ReturnType;
				}
				else
				{
					if (!left.Type.IsNullable() || !right.Type.IsNullable() || left.Type.GetNotNullableType() != parameterInfo.ParameterType || right.Type.GetNotNullableType() != parameterInfo2.ParameterType || method.ReturnType.IsNullable())
					{
						throw new InvalidOperationException();
					}
					flag = true;
					type = method.ReturnType.MakeNullableType();
				}
			}
			return new BinaryExpression(et, type, left, right, flag, flag, method, null);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000028E4 File Offset: 0x00000AE4
		private static bool IsAssignableToOperatorParameter(Expression expression, ParameterInfo parameter)
		{
			return expression.Type == parameter.ParameterType || (!expression.Type.IsNullable() && !parameter.ParameterType.IsNullable() && Expression.IsAssignableToParameterType(expression.Type, parameter));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002938 File Offset: 0x00000B38
		private static UnaryExpression MakeSimpleUnary(ExpressionType et, Expression expression, MethodInfo method)
		{
			Type self;
			bool is_lifted;
			if (method == null)
			{
				self = expression.Type;
				is_lifted = self.IsNullable();
			}
			else
			{
				ParameterInfo parameterInfo = method.GetParameters()[0];
				if (Expression.IsAssignableToOperatorParameter(expression, parameterInfo))
				{
					is_lifted = false;
					self = method.ReturnType;
				}
				else
				{
					if (!expression.Type.IsNullable() || expression.Type.GetNotNullableType() != parameterInfo.ParameterType || method.ReturnType.IsNullable())
					{
						throw new InvalidOperationException();
					}
					is_lifted = true;
					self = method.ReturnType.MakeNullableType();
				}
			}
			return new UnaryExpression(et, expression, self, method, is_lifted);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000029DC File Offset: 0x00000BDC
		private static BinaryExpression MakeBoolBinary(ExpressionType et, Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			bool is_lifted;
			Type type;
			if (method == null)
			{
				if (!left.Type.IsNullable() && !right.Type.IsNullable())
				{
					is_lifted = false;
					liftToNull = false;
					type = typeof(bool);
				}
				else
				{
					if (!left.Type.IsNullable() || !right.Type.IsNullable())
					{
						throw new InvalidOperationException();
					}
					is_lifted = true;
					type = ((!liftToNull) ? typeof(bool) : typeof(bool?));
				}
			}
			else
			{
				ParameterInfo[] parameters = method.GetParameters();
				ParameterInfo parameterInfo = parameters[0];
				ParameterInfo parameterInfo2 = parameters[1];
				if (Expression.IsAssignableToOperatorParameter(left, parameterInfo) && Expression.IsAssignableToOperatorParameter(right, parameterInfo2))
				{
					is_lifted = false;
					liftToNull = false;
					type = method.ReturnType;
				}
				else
				{
					if (!left.Type.IsNullable() || !right.Type.IsNullable() || left.Type.GetNotNullableType() != parameterInfo.ParameterType || right.Type.GetNotNullableType() != parameterInfo2.ParameterType)
					{
						throw new InvalidOperationException();
					}
					is_lifted = true;
					if (method.ReturnType == typeof(bool))
					{
						type = ((!liftToNull) ? typeof(bool) : typeof(bool?));
					}
					else
					{
						if (method.ReturnType.IsNullable())
						{
							throw new InvalidOperationException();
						}
						type = method.ReturnType.MakeNullableType();
					}
				}
			}
			return new BinaryExpression(et, type, left, right, liftToNull, is_lifted, method, null);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B80 File Offset: 0x00000D80
		public static BinaryExpression Add(Expression left, Expression right)
		{
			return Expression.Add(left, right, null);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002B8C File Offset: 0x00000D8C
		public static BinaryExpression Add(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_Addition", left, right, method);
			return Expression.MakeSimpleBinary(ExpressionType.Add, left, right, method);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002BA8 File Offset: 0x00000DA8
		public static BinaryExpression AddChecked(Expression left, Expression right)
		{
			return Expression.AddChecked(left, right, null);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public static BinaryExpression AddChecked(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_Addition", left, right, method);
			if (method == null && (left.Type == typeof(byte) || left.Type == typeof(sbyte)))
			{
				throw new InvalidOperationException(string.Format("AddChecked not defined for {0} and {1}", left.Type, right.Type));
			}
			return Expression.MakeSimpleBinary(ExpressionType.AddChecked, left, right, method);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002C28 File Offset: 0x00000E28
		public static BinaryExpression Subtract(Expression left, Expression right)
		{
			return Expression.Subtract(left, right, null);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C34 File Offset: 0x00000E34
		public static BinaryExpression Subtract(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_Subtraction", left, right, method);
			return Expression.MakeSimpleBinary(ExpressionType.Subtract, left, right, method);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002C50 File Offset: 0x00000E50
		public static BinaryExpression SubtractChecked(Expression left, Expression right)
		{
			return Expression.SubtractChecked(left, right, null);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002C5C File Offset: 0x00000E5C
		public static BinaryExpression SubtractChecked(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_Subtraction", left, right, method);
			if (method == null && (left.Type == typeof(byte) || left.Type == typeof(sbyte)))
			{
				throw new InvalidOperationException(string.Format("SubtractChecked not defined for {0} and {1}", left.Type, right.Type));
			}
			return Expression.MakeSimpleBinary(ExpressionType.SubtractChecked, left, right, method);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public static BinaryExpression Modulo(Expression left, Expression right)
		{
			return Expression.Modulo(left, right, null);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002CDC File Offset: 0x00000EDC
		public static BinaryExpression Modulo(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_Modulus", left, right, method);
			return Expression.MakeSimpleBinary(ExpressionType.Modulo, left, right, method);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public static BinaryExpression Multiply(Expression left, Expression right)
		{
			return Expression.Multiply(left, right, null);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002D04 File Offset: 0x00000F04
		public static BinaryExpression Multiply(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_Multiply", left, right, method);
			return Expression.MakeSimpleBinary(ExpressionType.Multiply, left, right, method);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002D20 File Offset: 0x00000F20
		public static BinaryExpression MultiplyChecked(Expression left, Expression right)
		{
			return Expression.MultiplyChecked(left, right, null);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002D2C File Offset: 0x00000F2C
		public static BinaryExpression MultiplyChecked(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_Multiply", left, right, method);
			return Expression.MakeSimpleBinary(ExpressionType.MultiplyChecked, left, right, method);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002D48 File Offset: 0x00000F48
		public static BinaryExpression Divide(Expression left, Expression right)
		{
			return Expression.Divide(left, right, null);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002D54 File Offset: 0x00000F54
		public static BinaryExpression Divide(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_Division", left, right, method);
			return Expression.MakeSimpleBinary(ExpressionType.Divide, left, right, method);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002D70 File Offset: 0x00000F70
		public static BinaryExpression Power(Expression left, Expression right)
		{
			return Expression.Power(left, right, null);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002D7C File Offset: 0x00000F7C
		public static BinaryExpression Power(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck(null, left, right, method);
			if (left.Type.GetNotNullableType() != typeof(double))
			{
				throw new InvalidOperationException("Power only supports double arguments");
			}
			return Expression.MakeSimpleBinary(ExpressionType.Power, left, right, method);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public static BinaryExpression And(Expression left, Expression right)
		{
			return Expression.And(left, right, null);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public static BinaryExpression And(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryBitwiseCoreCheck("op_BitwiseAnd", left, right, method);
			return Expression.MakeSimpleBinary(ExpressionType.And, left, right, method);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002DEC File Offset: 0x00000FEC
		public static BinaryExpression Or(Expression left, Expression right)
		{
			return Expression.Or(left, right, null);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public static BinaryExpression Or(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryBitwiseCoreCheck("op_BitwiseOr", left, right, method);
			return Expression.MakeSimpleBinary(ExpressionType.Or, left, right, method);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002E14 File Offset: 0x00001014
		public static BinaryExpression ExclusiveOr(Expression left, Expression right)
		{
			return Expression.ExclusiveOr(left, right, null);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002E20 File Offset: 0x00001020
		public static BinaryExpression ExclusiveOr(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryBitwiseCoreCheck("op_ExclusiveOr", left, right, method);
			return Expression.MakeSimpleBinary(ExpressionType.ExclusiveOr, left, right, method);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002E3C File Offset: 0x0000103C
		public static BinaryExpression LeftShift(Expression left, Expression right)
		{
			return Expression.LeftShift(left, right, null);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002E48 File Offset: 0x00001048
		public static BinaryExpression LeftShift(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryBitwiseCoreCheck("op_LeftShift", left, right, method);
			return Expression.MakeSimpleBinary(ExpressionType.LeftShift, left, right, method);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002E64 File Offset: 0x00001064
		public static BinaryExpression RightShift(Expression left, Expression right)
		{
			return Expression.RightShift(left, right, null);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E70 File Offset: 0x00001070
		public static BinaryExpression RightShift(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_RightShift", left, right, method);
			return Expression.MakeSimpleBinary(ExpressionType.RightShift, left, right, method);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002E8C File Offset: 0x0000108C
		public static BinaryExpression AndAlso(Expression left, Expression right)
		{
			return Expression.AndAlso(left, right, null);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E98 File Offset: 0x00001098
		public static BinaryExpression AndAlso(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.ConditionalBinaryCheck("op_BitwiseAnd", left, right, method);
			return Expression.MakeBoolBinary(ExpressionType.AndAlso, left, right, true, method);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002EB4 File Offset: 0x000010B4
		private static MethodInfo ConditionalBinaryCheck(string oper, Expression left, Expression right, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck(oper, left, right, method);
			if (method == null)
			{
				if (left.Type.GetNotNullableType() != typeof(bool))
				{
					throw new InvalidOperationException("Only booleans are allowed");
				}
			}
			else
			{
				Type notNullableType = left.Type.GetNotNullableType();
				if (left.Type != right.Type || method.ReturnType != notNullableType)
				{
					throw new ArgumentException("left, right and return type must match");
				}
				MethodInfo trueOperator = Expression.GetTrueOperator(notNullableType);
				MethodInfo falseOperator = Expression.GetFalseOperator(notNullableType);
				if (trueOperator == null || falseOperator == null)
				{
					throw new ArgumentException("Operators true and false are required but not defined");
				}
			}
			return method;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002F58 File Offset: 0x00001158
		public static BinaryExpression OrElse(Expression left, Expression right)
		{
			return Expression.OrElse(left, right, null);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F64 File Offset: 0x00001164
		public static BinaryExpression OrElse(Expression left, Expression right, MethodInfo method)
		{
			method = Expression.ConditionalBinaryCheck("op_BitwiseOr", left, right, method);
			return Expression.MakeBoolBinary(ExpressionType.OrElse, left, right, true, method);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F80 File Offset: 0x00001180
		public static BinaryExpression Equal(Expression left, Expression right)
		{
			return Expression.Equal(left, right, false, null);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F8C File Offset: 0x0000118C
		public static BinaryExpression Equal(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_Equality", left, right, method);
			return Expression.MakeBoolBinary(ExpressionType.Equal, left, right, liftToNull, method);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002FA8 File Offset: 0x000011A8
		public static BinaryExpression NotEqual(Expression left, Expression right)
		{
			return Expression.NotEqual(left, right, false, null);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002FB4 File Offset: 0x000011B4
		public static BinaryExpression NotEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_Inequality", left, right, method);
			return Expression.MakeBoolBinary(ExpressionType.NotEqual, left, right, liftToNull, method);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002FD0 File Offset: 0x000011D0
		public static BinaryExpression GreaterThan(Expression left, Expression right)
		{
			return Expression.GreaterThan(left, right, false, null);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002FDC File Offset: 0x000011DC
		public static BinaryExpression GreaterThan(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_GreaterThan", left, right, method);
			return Expression.MakeBoolBinary(ExpressionType.GreaterThan, left, right, liftToNull, method);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002FF8 File Offset: 0x000011F8
		public static BinaryExpression GreaterThanOrEqual(Expression left, Expression right)
		{
			return Expression.GreaterThanOrEqual(left, right, false, null);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003004 File Offset: 0x00001204
		public static BinaryExpression GreaterThanOrEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_GreaterThanOrEqual", left, right, method);
			return Expression.MakeBoolBinary(ExpressionType.GreaterThanOrEqual, left, right, liftToNull, method);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003020 File Offset: 0x00001220
		public static BinaryExpression LessThan(Expression left, Expression right)
		{
			return Expression.LessThan(left, right, false, null);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000302C File Offset: 0x0000122C
		public static BinaryExpression LessThan(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_LessThan", left, right, method);
			return Expression.MakeBoolBinary(ExpressionType.LessThan, left, right, liftToNull, method);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003048 File Offset: 0x00001248
		public static BinaryExpression LessThanOrEqual(Expression left, Expression right)
		{
			return Expression.LessThanOrEqual(left, right, false, null);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003054 File Offset: 0x00001254
		public static BinaryExpression LessThanOrEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			method = Expression.BinaryCoreCheck("op_LessThanOrEqual", left, right, method);
			return Expression.MakeBoolBinary(ExpressionType.LessThanOrEqual, left, right, liftToNull, method);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003070 File Offset: 0x00001270
		private static void CheckArray(Expression array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (!array.Type.IsArray)
			{
				throw new ArgumentException("The array argument must be of type array");
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000030AC File Offset: 0x000012AC
		public static BinaryExpression ArrayIndex(Expression array, Expression index)
		{
			Expression.CheckArray(array);
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			if (array.Type.GetArrayRank() != 1)
			{
				throw new ArgumentException("The array argument must be a single dimensional array");
			}
			if (index.Type != typeof(int))
			{
				throw new ArgumentException("The index must be of type int");
			}
			return new BinaryExpression(ExpressionType.ArrayIndex, array.Type.GetElementType(), array, index);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003120 File Offset: 0x00001320
		public static BinaryExpression Coalesce(Expression left, Expression right)
		{
			return Expression.Coalesce(left, right, null);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000312C File Offset: 0x0000132C
		private static BinaryExpression MakeCoalesce(Expression left, Expression right)
		{
			Type type = null;
			if (left.Type.IsNullable())
			{
				Type notNullableType = left.Type.GetNotNullableType();
				if (!right.Type.IsNullable() && right.Type.IsAssignableTo(notNullableType))
				{
					type = notNullableType;
				}
			}
			if (type == null && right.Type.IsAssignableTo(left.Type))
			{
				type = left.Type;
			}
			if (type == null && left.Type.IsNullable() && left.Type.GetNotNullableType().IsAssignableTo(right.Type))
			{
				type = right.Type;
			}
			if (type == null)
			{
				throw new ArgumentException("Incompatible argument types");
			}
			return new BinaryExpression(ExpressionType.Coalesce, type, left, right, false, false, null, null);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000031F4 File Offset: 0x000013F4
		private static BinaryExpression MakeConvertedCoalesce(Expression left, Expression right, LambdaExpression conversion)
		{
			MethodInfo invokeMethod = conversion.Type.GetInvokeMethod();
			Expression.CheckNotVoid(invokeMethod.ReturnType);
			if (invokeMethod.ReturnType != right.Type)
			{
				throw new InvalidOperationException("Conversion return type doesn't march right type");
			}
			ParameterInfo[] parameters = invokeMethod.GetParameters();
			if (parameters.Length != 1)
			{
				throw new ArgumentException("Conversion has wrong number of parameters");
			}
			if (!Expression.IsAssignableToParameterType(left.Type, parameters[0]))
			{
				throw new InvalidOperationException("Conversion argument doesn't marcht left type");
			}
			return new BinaryExpression(ExpressionType.Coalesce, right.Type, left, right, false, false, null, conversion);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003280 File Offset: 0x00001480
		public static BinaryExpression Coalesce(Expression left, Expression right, LambdaExpression conversion)
		{
			if (left == null)
			{
				throw new ArgumentNullException("left");
			}
			if (right == null)
			{
				throw new ArgumentNullException("right");
			}
			if (left.Type.IsValueType && !left.Type.IsNullable())
			{
				throw new InvalidOperationException("Left expression can never be null");
			}
			if (conversion != null)
			{
				return Expression.MakeConvertedCoalesce(left, right, conversion);
			}
			return Expression.MakeCoalesce(left, right);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000032F0 File Offset: 0x000014F0
		public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right)
		{
			return Expression.MakeBinary(binaryType, left, right, false, null);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000032FC File Offset: 0x000014FC
		public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			return Expression.MakeBinary(binaryType, left, right, liftToNull, method, null);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000330C File Offset: 0x0000150C
		public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right, bool liftToNull, MethodInfo method, LambdaExpression conversion)
		{
			switch (binaryType)
			{
			case ExpressionType.Add:
				return Expression.Add(left, right, method);
			case ExpressionType.AddChecked:
				return Expression.AddChecked(left, right, method);
			case ExpressionType.And:
				return Expression.And(left, right, method);
			case ExpressionType.AndAlso:
				return Expression.AndAlso(left, right);
			case ExpressionType.Coalesce:
				return Expression.Coalesce(left, right, conversion);
			case ExpressionType.Divide:
				return Expression.Divide(left, right, method);
			case ExpressionType.Equal:
				return Expression.Equal(left, right, liftToNull, method);
			case ExpressionType.ExclusiveOr:
				return Expression.ExclusiveOr(left, right, method);
			case ExpressionType.GreaterThan:
				return Expression.GreaterThan(left, right, liftToNull, method);
			case ExpressionType.GreaterThanOrEqual:
				return Expression.GreaterThanOrEqual(left, right, liftToNull, method);
			case ExpressionType.LeftShift:
				return Expression.LeftShift(left, right, method);
			case ExpressionType.LessThan:
				return Expression.LessThan(left, right, liftToNull, method);
			case ExpressionType.LessThanOrEqual:
				return Expression.LessThanOrEqual(left, right, liftToNull, method);
			case ExpressionType.Modulo:
				return Expression.Modulo(left, right, method);
			case ExpressionType.Multiply:
				return Expression.Multiply(left, right, method);
			case ExpressionType.MultiplyChecked:
				return Expression.MultiplyChecked(left, right, method);
			case ExpressionType.NotEqual:
				return Expression.NotEqual(left, right, liftToNull, method);
			case ExpressionType.Or:
				return Expression.Or(left, right, method);
			case ExpressionType.OrElse:
				return Expression.OrElse(left, right);
			case ExpressionType.Power:
				return Expression.Power(left, right, method);
			case ExpressionType.RightShift:
				return Expression.RightShift(left, right, method);
			case ExpressionType.Subtract:
				return Expression.Subtract(left, right, method);
			case ExpressionType.SubtractChecked:
				return Expression.SubtractChecked(left, right, method);
			}
			throw new ArgumentException("MakeBinary expect a binary node type");
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000034C8 File Offset: 0x000016C8
		public static MethodCallExpression ArrayIndex(Expression array, params Expression[] indexes)
		{
			return Expression.ArrayIndex(array, indexes);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000034D4 File Offset: 0x000016D4
		public static MethodCallExpression ArrayIndex(Expression array, IEnumerable<Expression> indexes)
		{
			Expression.CheckArray(array);
			if (indexes == null)
			{
				throw new ArgumentNullException("indexes");
			}
			ReadOnlyCollection<Expression> readOnlyCollection = indexes.ToReadOnlyCollection<Expression>();
			if (array.Type.GetArrayRank() != readOnlyCollection.Count)
			{
				throw new ArgumentException("The number of arguments doesn't match the rank of the array");
			}
			foreach (Expression expression in readOnlyCollection)
			{
				if (expression.Type != typeof(int))
				{
					throw new ArgumentException("The index must be of type int");
				}
			}
			return Expression.Call(array, array.Type.GetMethod("Get", BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy), readOnlyCollection);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000035A4 File Offset: 0x000017A4
		public static UnaryExpression ArrayLength(Expression array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (!array.Type.IsArray)
			{
				throw new ArgumentException("The type of the expression must me Array");
			}
			if (array.Type.GetArrayRank() != 1)
			{
				throw new ArgumentException("The array must be a single dimensional array");
			}
			return new UnaryExpression(ExpressionType.ArrayLength, array, typeof(int));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000360C File Offset: 0x0000180C
		public static MemberAssignment Bind(MemberInfo member, Expression expression)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			Type type = null;
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null && propertyInfo.GetSetMethod(true) != null)
			{
				type = propertyInfo.PropertyType;
			}
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				type = fieldInfo.FieldType;
			}
			if (type == null)
			{
				throw new ArgumentException("member");
			}
			if (!expression.Type.IsAssignableTo(type))
			{
				throw new ArgumentException("member");
			}
			return new MemberAssignment(member, expression);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000036A8 File Offset: 0x000018A8
		public static MemberAssignment Bind(MethodInfo propertyAccessor, Expression expression)
		{
			if (propertyAccessor == null)
			{
				throw new ArgumentNullException("propertyAccessor");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			Expression.CheckNonGenericMethod(propertyAccessor);
			PropertyInfo associatedProperty = Expression.GetAssociatedProperty(propertyAccessor);
			if (associatedProperty == null)
			{
				throw new ArgumentException("propertyAccessor");
			}
			if (associatedProperty.GetSetMethod(true) == null)
			{
				throw new ArgumentException("setter");
			}
			if (!expression.Type.IsAssignableTo(associatedProperty.PropertyType))
			{
				throw new ArgumentException("member");
			}
			return new MemberAssignment(associatedProperty, expression);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003738 File Offset: 0x00001938
		public static MethodCallExpression Call(Expression instance, MethodInfo method)
		{
			return Expression.Call(instance, method, null);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003744 File Offset: 0x00001944
		public static MethodCallExpression Call(MethodInfo method, params Expression[] arguments)
		{
			return Expression.Call(null, method, arguments);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003750 File Offset: 0x00001950
		public static MethodCallExpression Call(Expression instance, MethodInfo method, params Expression[] arguments)
		{
			return Expression.Call(instance, method, arguments);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000375C File Offset: 0x0000195C
		public static MethodCallExpression Call(Expression instance, MethodInfo method, IEnumerable<Expression> arguments)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (instance == null && !method.IsStatic)
			{
				throw new ArgumentNullException("instance");
			}
			if (method.IsStatic && instance != null)
			{
				throw new ArgumentException("instance");
			}
			if (!method.IsStatic && !instance.Type.IsAssignableTo(method.DeclaringType))
			{
				throw new ArgumentException("Type is not assignable to the declaring type of the method");
			}
			ReadOnlyCollection<Expression> arguments2 = Expression.CheckMethodArguments(method, arguments);
			return new MethodCallExpression(instance, method, arguments2);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000037F0 File Offset: 0x000019F0
		private static Type[] CollectTypes(IEnumerable<Expression> expressions)
		{
			return (from arg in expressions
			select arg.Type).ToArray<Type>();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003828 File Offset: 0x00001A28
		private static MethodInfo TryMakeGeneric(MethodInfo method, Type[] args)
		{
			if (method == null)
			{
				return null;
			}
			if (!method.IsGenericMethod && (args == null || args.Length == 0))
			{
				return method;
			}
			if (args.Length == method.GetGenericArguments().Length)
			{
				return method.MakeGenericMethod(args);
			}
			return null;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003874 File Offset: 0x00001A74
		public static MethodCallExpression Call(Expression instance, string methodName, Type[] typeArguments, params Expression[] arguments)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (methodName == null)
			{
				throw new ArgumentNullException("methodName");
			}
			MethodInfo method = Expression.TryGetMethod(instance.Type, methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, Expression.CollectTypes(arguments), typeArguments);
			ReadOnlyCollection<Expression> arguments2 = Expression.CheckMethodArguments(method, arguments);
			return new MethodCallExpression(instance, method, arguments2);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000038CC File Offset: 0x00001ACC
		private static bool MethodMatch(MethodInfo method, string name, Type[] parameterTypes, Type[] argumentTypes)
		{
			if (method.Name != name)
			{
				return false;
			}
			ParameterInfo[] parameters = method.GetParameters();
			if (parameters.Length != parameterTypes.Length)
			{
				return false;
			}
			if (method.IsGenericMethod && method.IsGenericMethodDefinition)
			{
				MethodInfo methodInfo = Expression.TryMakeGeneric(method, argumentTypes);
				return methodInfo != null && Expression.MethodMatch(methodInfo, name, parameterTypes, argumentTypes);
			}
			if (!method.IsGenericMethod && argumentTypes != null && argumentTypes.Length > 0)
			{
				return false;
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				Type type = parameterTypes[i];
				ParameterInfo parameterInfo = parameters[i];
				if (!Expression.IsAssignableToParameterType(type, parameterInfo) && !Expression.IsExpressionOfParameter(type, parameterInfo.ParameterType))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000398C File Offset: 0x00001B8C
		private static bool IsExpressionOfParameter(Type type, Type ptype)
		{
			return ptype.IsGenericInstanceOf(typeof(Expression<>)) && ptype.GetFirstGenericArgument() == type;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000039B0 File Offset: 0x00001BB0
		private static MethodInfo TryGetMethod(Type type, string methodName, BindingFlags flags, Type[] parameterTypes, Type[] argumentTypes)
		{
			IEnumerable<MethodInfo> source = from meth in type.GetMethods(flags)
			where Expression.MethodMatch(meth, methodName, parameterTypes, argumentTypes)
			select meth;
			if (source.Count<MethodInfo>() > 1)
			{
				throw new InvalidOperationException("Too many method candidates");
			}
			MethodInfo methodInfo = Expression.TryMakeGeneric(source.FirstOrDefault<MethodInfo>(), argumentTypes);
			if (methodInfo != null)
			{
				return methodInfo;
			}
			throw new InvalidOperationException("No such method");
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003A30 File Offset: 0x00001C30
		public static MethodCallExpression Call(Type type, string methodName, Type[] typeArguments, params Expression[] arguments)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (methodName == null)
			{
				throw new ArgumentNullException("methodName");
			}
			MethodInfo method = Expression.TryGetMethod(type, methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, Expression.CollectTypes(arguments), typeArguments);
			ReadOnlyCollection<Expression> arguments2 = Expression.CheckMethodArguments(method, arguments);
			return new MethodCallExpression(method, arguments2);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003A80 File Offset: 0x00001C80
		public static ConditionalExpression Condition(Expression test, Expression ifTrue, Expression ifFalse)
		{
			if (test == null)
			{
				throw new ArgumentNullException("test");
			}
			if (ifTrue == null)
			{
				throw new ArgumentNullException("ifTrue");
			}
			if (ifFalse == null)
			{
				throw new ArgumentNullException("ifFalse");
			}
			if (test.Type != typeof(bool))
			{
				throw new ArgumentException("Test expression should be of type bool");
			}
			if (ifTrue.Type != ifFalse.Type)
			{
				throw new ArgumentException("The ifTrue and ifFalse type do not match");
			}
			return new ConditionalExpression(test, ifTrue, ifFalse);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003B04 File Offset: 0x00001D04
		public static ConstantExpression Constant(object value)
		{
			if (value == null)
			{
				return new ConstantExpression(null, typeof(object));
			}
			return Expression.Constant(value, value.GetType());
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003B2C File Offset: 0x00001D2C
		public static ConstantExpression Constant(object value, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				if (type.IsValueType && !type.IsNullable())
				{
					throw new ArgumentException();
				}
			}
			else if ((!type.IsValueType || !type.IsNullable()) && !value.GetType().IsAssignableTo(type))
			{
				throw new ArgumentException();
			}
			return new ConstantExpression(value, type);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003BA8 File Offset: 0x00001DA8
		private static bool IsConvertiblePrimitive(Type type)
		{
			Type notNullableType = type.GetNotNullableType();
			return notNullableType != typeof(bool) && (notNullableType.IsEnum || notNullableType.IsPrimitive);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003BE4 File Offset: 0x00001DE4
		internal static bool IsPrimitiveConversion(Type type, Type target)
		{
			return type == target || (type.IsNullable() && target == type.GetNotNullableType()) || (target.IsNullable() && type == target.GetNotNullableType()) || (Expression.IsConvertiblePrimitive(type) && Expression.IsConvertiblePrimitive(target));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003C48 File Offset: 0x00001E48
		internal static bool IsReferenceConversion(Type type, Type target)
		{
			return type == target || (type == typeof(object) || target == typeof(object)) || (type.IsInterface || target.IsInterface) || (!type.IsValueType && !target.IsValueType && (type.IsAssignableTo(target) || target.IsAssignableTo(type)));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003CCC File Offset: 0x00001ECC
		public static UnaryExpression Convert(Expression expression, Type type)
		{
			return Expression.Convert(expression, type, null);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003CD8 File Offset: 0x00001ED8
		private static MethodInfo GetUserConversionMethod(Type type, Type target)
		{
			MethodInfo unaryOperator = Expression.GetUnaryOperator("op_Explicit", type, type, target);
			if (unaryOperator == null)
			{
				unaryOperator = Expression.GetUnaryOperator("op_Implicit", type, type, target);
			}
			if (unaryOperator == null)
			{
				unaryOperator = Expression.GetUnaryOperator("op_Explicit", target, type, target);
			}
			if (unaryOperator == null)
			{
				unaryOperator = Expression.GetUnaryOperator("op_Implicit", target, type, target);
			}
			if (unaryOperator == null)
			{
				throw new InvalidOperationException();
			}
			return unaryOperator;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003D3C File Offset: 0x00001F3C
		public static UnaryExpression Convert(Expression expression, Type type, MethodInfo method)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Type param = expression.Type;
			if (method != null)
			{
				Expression.CheckUnaryMethod(method, param);
			}
			else if (!Expression.IsPrimitiveConversion(param, type) && !Expression.IsReferenceConversion(param, type))
			{
				method = Expression.GetUserConversionMethod(param, type);
			}
			return new UnaryExpression(ExpressionType.Convert, expression, type, method, Expression.IsConvertNodeLifted(method, expression, type));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003DB8 File Offset: 0x00001FB8
		private static bool IsConvertNodeLifted(MethodInfo method, Expression operand, Type target)
		{
			if (method == null)
			{
				return operand.Type.IsNullable() || target.IsNullable();
			}
			return (operand.Type.IsNullable() && !Expression.ParameterMatch(method, operand.Type)) || (target.IsNullable() && !Expression.ReturnTypeMatch(method, target));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003E24 File Offset: 0x00002024
		private static bool ParameterMatch(MethodInfo method, Type type)
		{
			return method.GetParameters()[0].ParameterType == type;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003E38 File Offset: 0x00002038
		private static bool ReturnTypeMatch(MethodInfo method, Type type)
		{
			return method.ReturnType == type;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003E44 File Offset: 0x00002044
		public static UnaryExpression ConvertChecked(Expression expression, Type type)
		{
			return Expression.ConvertChecked(expression, type, null);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003E50 File Offset: 0x00002050
		public static UnaryExpression ConvertChecked(Expression expression, Type type, MethodInfo method)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Type param = expression.Type;
			if (method != null)
			{
				Expression.CheckUnaryMethod(method, param);
			}
			else
			{
				if (Expression.IsReferenceConversion(param, type))
				{
					return Expression.Convert(expression, type, method);
				}
				if (!Expression.IsPrimitiveConversion(param, type))
				{
					method = Expression.GetUserConversionMethod(param, type);
				}
			}
			return new UnaryExpression(ExpressionType.ConvertChecked, expression, type, method, Expression.IsConvertNodeLifted(method, expression, type));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003ED8 File Offset: 0x000020D8
		public static ElementInit ElementInit(MethodInfo addMethod, params Expression[] arguments)
		{
			return Expression.ElementInit(addMethod, arguments);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003EE4 File Offset: 0x000020E4
		public static ElementInit ElementInit(MethodInfo addMethod, IEnumerable<Expression> arguments)
		{
			if (addMethod == null)
			{
				throw new ArgumentNullException("addMethod");
			}
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			if (addMethod.Name.ToLower(CultureInfo.InvariantCulture) != "add")
			{
				throw new ArgumentException("addMethod");
			}
			if (addMethod.IsStatic)
			{
				throw new ArgumentException("addMethod must be an instance method", "addMethod");
			}
			ReadOnlyCollection<Expression> arguments2 = Expression.CheckMethodArguments(addMethod, arguments);
			return new ElementInit(addMethod, arguments2);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003F68 File Offset: 0x00002168
		public static MemberExpression Field(Expression expression, FieldInfo field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			if (!field.IsStatic)
			{
				if (expression == null)
				{
					throw new ArgumentNullException("expression");
				}
				if (!expression.Type.IsAssignableTo(field.DeclaringType))
				{
					throw new ArgumentException("field");
				}
			}
			else if (expression != null)
			{
				throw new ArgumentException("expression");
			}
			return new MemberExpression(expression, field, field.FieldType);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003FE8 File Offset: 0x000021E8
		public static MemberExpression Field(Expression expression, string fieldName)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			FieldInfo field = expression.Type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (field == null)
			{
				throw new ArgumentException(string.Format("No field named {0} on {1}", fieldName, expression.Type));
			}
			return new MemberExpression(expression, field, field.FieldType);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004040 File Offset: 0x00002240
		public static Type GetActionType(params Type[] typeArgs)
		{
			if (typeArgs == null)
			{
				throw new ArgumentNullException("typeArgs");
			}
			if (typeArgs.Length > 4)
			{
				throw new ArgumentException("No Action type of this arity");
			}
			if (typeArgs.Length == 0)
			{
				return typeof(Action);
			}
			Type type = null;
			switch (typeArgs.Length)
			{
			case 1:
				type = typeof(Action<>);
				break;
			case 2:
				type = typeof(Action<, >);
				break;
			case 3:
				type = typeof(Action<, , >);
				break;
			case 4:
				type = typeof(Action<, , , >);
				break;
			}
			return type.MakeGenericType(typeArgs);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000040F0 File Offset: 0x000022F0
		public static Type GetFuncType(params Type[] typeArgs)
		{
			if (typeArgs == null)
			{
				throw new ArgumentNullException("typeArgs");
			}
			if (typeArgs.Length < 1 || typeArgs.Length > 5)
			{
				throw new ArgumentException("No Func type of this arity");
			}
			Type type = null;
			switch (typeArgs.Length)
			{
			case 1:
				type = typeof(Func<>);
				break;
			case 2:
				type = typeof(Func<, >);
				break;
			case 3:
				type = typeof(Func<, , >);
				break;
			case 4:
				type = typeof(Func<, , , >);
				break;
			case 5:
				type = typeof(Func<, , , , >);
				break;
			}
			return type.MakeGenericType(typeArgs);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000041AC File Offset: 0x000023AC
		public static InvocationExpression Invoke(Expression expression, params Expression[] arguments)
		{
			return Expression.Invoke(expression, arguments);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000041B8 File Offset: 0x000023B8
		private static Type GetInvokableType(Type t)
		{
			if (t.IsAssignableTo(typeof(Delegate)))
			{
				return t;
			}
			return Expression.GetGenericType(t, typeof(Expression<>));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000041E4 File Offset: 0x000023E4
		private static Type GetGenericType(Type t, Type def)
		{
			if (t == null)
			{
				return null;
			}
			if (t.IsGenericType && t.GetGenericTypeDefinition() == def)
			{
				return t;
			}
			return Expression.GetGenericType(t.BaseType, def);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004220 File Offset: 0x00002420
		public static InvocationExpression Invoke(Expression expression, IEnumerable<Expression> arguments)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			Type invokableType = Expression.GetInvokableType(expression.Type);
			if (invokableType == null)
			{
				throw new ArgumentException("The type of the expression is not invokable");
			}
			ReadOnlyCollection<Expression> readOnlyCollection = arguments.ToReadOnlyCollection<Expression>();
			Expression.CheckForNull<Expression>(readOnlyCollection, "arguments");
			MethodInfo invokeMethod = invokableType.GetInvokeMethod();
			if (invokeMethod == null)
			{
				throw new ArgumentException("expression");
			}
			if (invokeMethod.GetParameters().Length != readOnlyCollection.Count)
			{
				throw new InvalidOperationException("Arguments count doesn't match parameters length");
			}
			readOnlyCollection = Expression.CheckMethodArguments(invokeMethod, readOnlyCollection);
			return new InvocationExpression(expression, invokeMethod.ReturnType, readOnlyCollection);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000042B8 File Offset: 0x000024B8
		private static bool CanAssign(Type target, Type source)
		{
			return !(target.IsValueType ^ source.IsValueType) && source.IsAssignableTo(target);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000042D8 File Offset: 0x000024D8
		private static Expression CheckLambda(Type delegateType, Expression body, ReadOnlyCollection<ParameterExpression> parameters)
		{
			if (!delegateType.IsSubclassOf(typeof(Delegate)))
			{
				throw new ArgumentException("delegateType");
			}
			MethodInfo invokeMethod = delegateType.GetInvokeMethod();
			if (invokeMethod == null)
			{
				throw new ArgumentException("delegate must contain an Invoke method", "delegateType");
			}
			ParameterInfo[] parameters2 = invokeMethod.GetParameters();
			if (parameters2.Length != parameters.Count)
			{
				throw new ArgumentException(string.Format("Different number of arguments in delegate {0}", delegateType), "delegateType");
			}
			for (int i = 0; i < parameters2.Length; i++)
			{
				ParameterExpression parameterExpression = parameters[i];
				if (parameterExpression == null)
				{
					throw new ArgumentNullException("parameters");
				}
				if (!Expression.CanAssign(parameterExpression.Type, parameters2[i].ParameterType))
				{
					throw new ArgumentException(string.Format("Can not assign a {0} to a {1}", parameters2[i].ParameterType, parameterExpression.Type));
				}
			}
			if (invokeMethod.ReturnType == typeof(void) || Expression.CanAssign(invokeMethod.ReturnType, body.Type))
			{
				return body;
			}
			if (invokeMethod.ReturnType.IsExpression())
			{
				return Expression.Quote(body);
			}
			throw new ArgumentException(string.Format("body type {0} can not be assigned to {1}", body.Type, invokeMethod.ReturnType));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004410 File Offset: 0x00002610
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, params ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, parameters);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000441C File Offset: 0x0000261C
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, IEnumerable<ParameterExpression> parameters)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			ReadOnlyCollection<ParameterExpression> parameters2 = parameters.ToReadOnlyCollection<ParameterExpression>();
			body = Expression.CheckLambda(typeof(TDelegate), body, parameters2);
			return new Expression<TDelegate>(body, parameters2);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000445C File Offset: 0x0000265C
		public static LambdaExpression Lambda(Expression body, params ParameterExpression[] parameters)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parameters.Length > 4)
			{
				throw new ArgumentException("Too many parameters");
			}
			return Expression.Lambda(Expression.GetDelegateType(body.Type, parameters), body, parameters);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000044A4 File Offset: 0x000026A4
		private static Type GetDelegateType(Type return_type, ParameterExpression[] parameters)
		{
			if (parameters == null)
			{
				parameters = new ParameterExpression[0];
			}
			if (return_type == typeof(void))
			{
				return Expression.GetActionType((from p in parameters
				select p.Type).ToArray<Type>());
			}
			Type[] array = new Type[parameters.Length + 1];
			for (int i = 0; i < array.Length - 1; i++)
			{
				array[i] = parameters[i].Type;
			}
			array[array.Length - 1] = return_type;
			return Expression.GetFuncType(array);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004538 File Offset: 0x00002738
		public static LambdaExpression Lambda(Type delegateType, Expression body, params ParameterExpression[] parameters)
		{
			return Expression.Lambda(delegateType, body, parameters);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004544 File Offset: 0x00002744
		private static LambdaExpression CreateExpressionOf(Type type, Expression body, ReadOnlyCollection<ParameterExpression> parameters)
		{
			return (LambdaExpression)typeof(Expression<>).MakeGenericType(new Type[]
			{
				type
			}).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, null, new Type[]
			{
				typeof(Expression),
				typeof(ReadOnlyCollection<ParameterExpression>)
			}, null).Invoke(new object[]
			{
				body,
				parameters
			});
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000045AC File Offset: 0x000027AC
		public static LambdaExpression Lambda(Type delegateType, Expression body, IEnumerable<ParameterExpression> parameters)
		{
			if (delegateType == null)
			{
				throw new ArgumentNullException("delegateType");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			ReadOnlyCollection<ParameterExpression> parameters2 = parameters.ToReadOnlyCollection<ParameterExpression>();
			body = Expression.CheckLambda(delegateType, body, parameters2);
			return Expression.CreateExpressionOf(delegateType, body, parameters2);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000045F4 File Offset: 0x000027F4
		public static MemberListBinding ListBind(MemberInfo member, params ElementInit[] initializers)
		{
			return Expression.ListBind(member, initializers);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004600 File Offset: 0x00002800
		private static void CheckIsAssignableToIEnumerable(Type t)
		{
			if (!t.IsAssignableTo(typeof(IEnumerable)))
			{
				throw new ArgumentException(string.Format("Type {0} doesn't implemen IEnumerable", t));
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004634 File Offset: 0x00002834
		public static MemberListBinding ListBind(MemberInfo member, IEnumerable<ElementInit> initializers)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			if (initializers == null)
			{
				throw new ArgumentNullException("initializers");
			}
			ReadOnlyCollection<ElementInit> readOnlyCollection = initializers.ToReadOnlyCollection<ElementInit>();
			Expression.CheckForNull<ElementInit>(readOnlyCollection, "initializers");
			member.OnFieldOrProperty(delegate(FieldInfo field)
			{
				Expression.CheckIsAssignableToIEnumerable(field.FieldType);
			}, delegate(PropertyInfo prop)
			{
				Expression.CheckIsAssignableToIEnumerable(prop.PropertyType);
			});
			return new MemberListBinding(member, readOnlyCollection);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000046BC File Offset: 0x000028BC
		public static MemberListBinding ListBind(MethodInfo propertyAccessor, params ElementInit[] initializers)
		{
			return Expression.ListBind(propertyAccessor, initializers);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000046C8 File Offset: 0x000028C8
		private static void CheckForNull<T>(ReadOnlyCollection<T> collection, string name) where T : class
		{
			foreach (T t in collection)
			{
				if (t == null)
				{
					throw new ArgumentNullException(name);
				}
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004734 File Offset: 0x00002934
		public static MemberListBinding ListBind(MethodInfo propertyAccessor, IEnumerable<ElementInit> initializers)
		{
			if (propertyAccessor == null)
			{
				throw new ArgumentNullException("propertyAccessor");
			}
			if (initializers == null)
			{
				throw new ArgumentNullException("initializers");
			}
			ReadOnlyCollection<ElementInit> readOnlyCollection = initializers.ToReadOnlyCollection<ElementInit>();
			Expression.CheckForNull<ElementInit>(readOnlyCollection, "initializers");
			PropertyInfo associatedProperty = Expression.GetAssociatedProperty(propertyAccessor);
			if (associatedProperty == null)
			{
				throw new ArgumentException("propertyAccessor");
			}
			Expression.CheckIsAssignableToIEnumerable(associatedProperty.PropertyType);
			return new MemberListBinding(associatedProperty, readOnlyCollection);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000047A0 File Offset: 0x000029A0
		public static ListInitExpression ListInit(NewExpression newExpression, params ElementInit[] initializers)
		{
			return Expression.ListInit(newExpression, initializers);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000047AC File Offset: 0x000029AC
		public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<ElementInit> initializers)
		{
			ReadOnlyCollection<ElementInit> initializers2 = Expression.CheckListInit<ElementInit>(newExpression, initializers);
			return new ListInitExpression(newExpression, initializers2);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000047C8 File Offset: 0x000029C8
		public static ListInitExpression ListInit(NewExpression newExpression, params Expression[] initializers)
		{
			return Expression.ListInit(newExpression, initializers);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000047D4 File Offset: 0x000029D4
		public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<Expression> initializers)
		{
			ReadOnlyCollection<Expression> readOnlyCollection = Expression.CheckListInit<Expression>(newExpression, initializers);
			MethodInfo addMethod = Expression.GetAddMethod(newExpression.Type, readOnlyCollection[0].Type);
			if (addMethod == null)
			{
				throw new InvalidOperationException("No suitable add method found");
			}
			return new ListInitExpression(newExpression, Expression.CreateInitializers(addMethod, readOnlyCollection));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004820 File Offset: 0x00002A20
		private static ReadOnlyCollection<ElementInit> CreateInitializers(MethodInfo add_method, ReadOnlyCollection<Expression> initializers)
		{
			return (from init in initializers
			select Expression.ElementInit(add_method, new Expression[]
			{
				init
			})).ToReadOnlyCollection<ElementInit>();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004854 File Offset: 0x00002A54
		private static MethodInfo GetAddMethod(Type type, Type arg)
		{
			return type.GetMethod("Add", BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy, null, new Type[]
			{
				arg
			}, null);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004870 File Offset: 0x00002A70
		public static ListInitExpression ListInit(NewExpression newExpression, MethodInfo addMethod, params Expression[] initializers)
		{
			return Expression.ListInit(newExpression, addMethod, initializers);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000487C File Offset: 0x00002A7C
		private static ReadOnlyCollection<T> CheckListInit<T>(NewExpression newExpression, IEnumerable<T> initializers) where T : class
		{
			if (newExpression == null)
			{
				throw new ArgumentNullException("newExpression");
			}
			if (initializers == null)
			{
				throw new ArgumentNullException("initializers");
			}
			if (!newExpression.Type.IsAssignableTo(typeof(IEnumerable)))
			{
				throw new InvalidOperationException("The type of the new expression does not implement IEnumerable");
			}
			ReadOnlyCollection<T> readOnlyCollection = initializers.ToReadOnlyCollection<T>();
			if (readOnlyCollection.Count == 0)
			{
				throw new ArgumentException("Empty initializers");
			}
			Expression.CheckForNull<T>(readOnlyCollection, "initializers");
			return readOnlyCollection;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000048FC File Offset: 0x00002AFC
		public static ListInitExpression ListInit(NewExpression newExpression, MethodInfo addMethod, IEnumerable<Expression> initializers)
		{
			ReadOnlyCollection<Expression> readOnlyCollection = Expression.CheckListInit<Expression>(newExpression, initializers);
			if (addMethod != null)
			{
				if (addMethod.Name.ToLower(CultureInfo.InvariantCulture) != "add")
				{
					throw new ArgumentException("addMethod");
				}
				ParameterInfo[] parameters = addMethod.GetParameters();
				if (parameters.Length != 1)
				{
					throw new ArgumentException("addMethod");
				}
				foreach (Expression expression in readOnlyCollection)
				{
					if (!Expression.IsAssignableToParameterType(expression.Type, parameters[0]))
					{
						throw new InvalidOperationException("Initializer not assignable to the add method parameter type");
					}
				}
			}
			if (addMethod == null)
			{
				addMethod = Expression.GetAddMethod(newExpression.Type, readOnlyCollection[0].Type);
			}
			if (addMethod == null)
			{
				throw new InvalidOperationException("No suitable add method found");
			}
			return new ListInitExpression(newExpression, Expression.CreateInitializers(addMethod, readOnlyCollection));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004A04 File Offset: 0x00002C04
		public static MemberExpression MakeMemberAccess(Expression expression, MemberInfo member)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				return Expression.Field(expression, fieldInfo);
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return Expression.Property(expression, propertyInfo);
			}
			throw new ArgumentException("Member should either be a field or a property");
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004A68 File Offset: 0x00002C68
		public static UnaryExpression MakeUnary(ExpressionType unaryType, Expression operand, Type type)
		{
			return Expression.MakeUnary(unaryType, operand, type, null);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004A74 File Offset: 0x00002C74
		public static UnaryExpression MakeUnary(ExpressionType unaryType, Expression operand, Type type, MethodInfo method)
		{
			switch (unaryType)
			{
			case ExpressionType.Negate:
				return Expression.Negate(operand, method);
			case ExpressionType.UnaryPlus:
				return Expression.UnaryPlus(operand, method);
			case ExpressionType.NegateChecked:
				return Expression.NegateChecked(operand, method);
			default:
				if (unaryType == ExpressionType.Convert)
				{
					return Expression.Convert(operand, type, method);
				}
				if (unaryType == ExpressionType.ConvertChecked)
				{
					return Expression.ConvertChecked(operand, type, method);
				}
				if (unaryType == ExpressionType.ArrayLength)
				{
					return Expression.ArrayLength(operand);
				}
				if (unaryType == ExpressionType.Quote)
				{
					return Expression.Quote(operand);
				}
				if (unaryType != ExpressionType.TypeAs)
				{
					throw new ArgumentException("MakeUnary expect an unary operator");
				}
				return Expression.TypeAs(operand, type);
			case ExpressionType.Not:
				return Expression.Not(operand, method);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004B28 File Offset: 0x00002D28
		public static MemberMemberBinding MemberBind(MemberInfo member, params MemberBinding[] bindings)
		{
			return Expression.MemberBind(member, bindings);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004B34 File Offset: 0x00002D34
		public static MemberMemberBinding MemberBind(MemberInfo member, IEnumerable<MemberBinding> bindings)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			Type type = member.OnFieldOrProperty((FieldInfo field) => field.FieldType, (PropertyInfo prop) => prop.PropertyType);
			return new MemberMemberBinding(member, Expression.CheckMemberBindings(type, bindings));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004BA0 File Offset: 0x00002DA0
		public static MemberMemberBinding MemberBind(MethodInfo propertyAccessor, params MemberBinding[] bindings)
		{
			return Expression.MemberBind(propertyAccessor, bindings);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004BAC File Offset: 0x00002DAC
		public static MemberMemberBinding MemberBind(MethodInfo propertyAccessor, IEnumerable<MemberBinding> bindings)
		{
			if (propertyAccessor == null)
			{
				throw new ArgumentNullException("propertyAccessor");
			}
			ReadOnlyCollection<MemberBinding> collection = bindings.ToReadOnlyCollection<MemberBinding>();
			Expression.CheckForNull<MemberBinding>(collection, "bindings");
			PropertyInfo associatedProperty = Expression.GetAssociatedProperty(propertyAccessor);
			if (associatedProperty == null)
			{
				throw new ArgumentException("propertyAccessor");
			}
			return new MemberMemberBinding(associatedProperty, Expression.CheckMemberBindings(associatedProperty.PropertyType, bindings));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004C08 File Offset: 0x00002E08
		private static ReadOnlyCollection<MemberBinding> CheckMemberBindings(Type type, IEnumerable<MemberBinding> bindings)
		{
			if (bindings == null)
			{
				throw new ArgumentNullException("bindings");
			}
			ReadOnlyCollection<MemberBinding> readOnlyCollection = bindings.ToReadOnlyCollection<MemberBinding>();
			Expression.CheckForNull<MemberBinding>(readOnlyCollection, "bindings");
			foreach (MemberBinding memberBinding in readOnlyCollection)
			{
				if (!type.IsAssignableTo(memberBinding.Member.DeclaringType))
				{
					throw new ArgumentException("Type not assignable to member type");
				}
			}
			return readOnlyCollection;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004CA4 File Offset: 0x00002EA4
		public static MemberInitExpression MemberInit(NewExpression newExpression, params MemberBinding[] bindings)
		{
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004CB0 File Offset: 0x00002EB0
		public static MemberInitExpression MemberInit(NewExpression newExpression, IEnumerable<MemberBinding> bindings)
		{
			if (newExpression == null)
			{
				throw new ArgumentNullException("newExpression");
			}
			return new MemberInitExpression(newExpression, Expression.CheckMemberBindings(newExpression.Type, bindings));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004CD8 File Offset: 0x00002ED8
		public static UnaryExpression Negate(Expression expression)
		{
			return Expression.Negate(expression, null);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004CE4 File Offset: 0x00002EE4
		public static UnaryExpression Negate(Expression expression, MethodInfo method)
		{
			method = Expression.UnaryCoreCheck("op_UnaryNegation", expression, method, (Type type) => Expression.IsSignedNumber(type));
			return Expression.MakeSimpleUnary(ExpressionType.Negate, expression, method);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004D28 File Offset: 0x00002F28
		public static UnaryExpression NegateChecked(Expression expression)
		{
			return Expression.NegateChecked(expression, null);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004D34 File Offset: 0x00002F34
		public static UnaryExpression NegateChecked(Expression expression, MethodInfo method)
		{
			method = Expression.UnaryCoreCheck("op_UnaryNegation", expression, method, (Type type) => Expression.IsSignedNumber(type));
			return Expression.MakeSimpleUnary(ExpressionType.NegateChecked, expression, method);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004D78 File Offset: 0x00002F78
		public static NewExpression New(ConstructorInfo constructor)
		{
			if (constructor == null)
			{
				throw new ArgumentNullException("constructor");
			}
			if (constructor.GetParameters().Length > 0)
			{
				throw new ArgumentException("Constructor must be parameter less");
			}
			return new NewExpression(constructor, Enumerable.ToReadOnlyCollection((IEnumerable<TSource>)null), null);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004DB4 File Offset: 0x00002FB4
		public static NewExpression New(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Expression.CheckNotVoid(type);
			ReadOnlyCollection<Expression> arguments = Enumerable.ToReadOnlyCollection((IEnumerable<TSource>)null);
			if (type.IsValueType)
			{
				return new NewExpression(type, arguments);
			}
			ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
			if (constructor == null)
			{
				throw new ArgumentException("Type doesn't have a parameter less constructor");
			}
			return new NewExpression(constructor, arguments, null);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004E18 File Offset: 0x00003018
		public static NewExpression New(ConstructorInfo constructor, params Expression[] arguments)
		{
			return Expression.New(constructor, arguments);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004E24 File Offset: 0x00003024
		public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments)
		{
			if (constructor == null)
			{
				throw new ArgumentNullException("constructor");
			}
			ReadOnlyCollection<Expression> arguments2 = Expression.CheckMethodArguments(constructor, arguments);
			return new NewExpression(constructor, arguments2, null);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004E54 File Offset: 0x00003054
		private static IList<Expression> CreateArgumentList(IEnumerable<Expression> arguments)
		{
			if (arguments == null)
			{
				return arguments.ToReadOnlyCollection<Expression>();
			}
			return arguments.ToList<Expression>();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004E6C File Offset: 0x0000306C
		private static void CheckNonGenericMethod(MethodBase method)
		{
			if (method.IsGenericMethodDefinition || method.ContainsGenericParameters)
			{
				throw new ArgumentException("Can not used open generic methods");
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004E90 File Offset: 0x00003090
		private static ReadOnlyCollection<Expression> CheckMethodArguments(MethodBase method, IEnumerable<Expression> args)
		{
			Expression.CheckNonGenericMethod(method);
			IList<Expression> list = Expression.CreateArgumentList(args);
			ParameterInfo[] parameters = method.GetParameters();
			if (list.Count != parameters.Length)
			{
				throw new ArgumentException("The number of arguments doesn't match the number of parameters");
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				if (list[i] == null)
				{
					throw new ArgumentNullException("arguments");
				}
				if (!Expression.IsAssignableToParameterType(list[i].Type, parameters[i]))
				{
					if (!parameters[i].ParameterType.IsExpression())
					{
						throw new ArgumentException("arguments");
					}
					list[i] = Expression.Quote(list[i]);
				}
			}
			return list.ToReadOnlyCollection<Expression>();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004F44 File Offset: 0x00003144
		public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments, params MemberInfo[] members)
		{
			return Expression.New(constructor, arguments, members);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004F50 File Offset: 0x00003150
		public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments, IEnumerable<MemberInfo> members)
		{
			if (constructor == null)
			{
				throw new ArgumentNullException("constructor");
			}
			ReadOnlyCollection<Expression> readOnlyCollection = arguments.ToReadOnlyCollection<Expression>();
			ReadOnlyCollection<MemberInfo> readOnlyCollection2 = members.ToReadOnlyCollection<MemberInfo>();
			Expression.CheckForNull<Expression>(readOnlyCollection, "arguments");
			Expression.CheckForNull<MemberInfo>(readOnlyCollection2, "members");
			readOnlyCollection = Expression.CheckMethodArguments(constructor, arguments);
			if (readOnlyCollection.Count != readOnlyCollection2.Count)
			{
				throw new ArgumentException("Arguments count does not match members count");
			}
			for (int i = 0; i < readOnlyCollection2.Count; i++)
			{
				MemberInfo memberInfo = readOnlyCollection2[i];
				MemberTypes memberType = memberInfo.MemberType;
				Type type;
				if (memberType != MemberTypes.Field)
				{
					if (memberType != MemberTypes.Method)
					{
						if (memberType != MemberTypes.Property)
						{
							throw new ArgumentException("Member type not allowed");
						}
						PropertyInfo propertyInfo = memberInfo as PropertyInfo;
						if (propertyInfo.GetGetMethod(true) == null)
						{
							throw new ArgumentException("Property must have a getter");
						}
						type = (memberInfo as PropertyInfo).PropertyType;
					}
					else
					{
						type = (memberInfo as MethodInfo).ReturnType;
					}
				}
				else
				{
					type = (memberInfo as FieldInfo).FieldType;
				}
				if (!readOnlyCollection[i].Type.IsAssignableTo(type))
				{
					throw new ArgumentException("Argument type not assignable to member type");
				}
			}
			return new NewExpression(constructor, readOnlyCollection, readOnlyCollection2);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000508C File Offset: 0x0000328C
		public static NewArrayExpression NewArrayBounds(Type type, params Expression[] bounds)
		{
			return Expression.NewArrayBounds(type, bounds);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005098 File Offset: 0x00003298
		public static NewArrayExpression NewArrayBounds(Type type, IEnumerable<Expression> bounds)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (bounds == null)
			{
				throw new ArgumentNullException("bounds");
			}
			Expression.CheckNotVoid(type);
			ReadOnlyCollection<Expression> readOnlyCollection = bounds.ToReadOnlyCollection<Expression>();
			foreach (Expression expression in readOnlyCollection)
			{
				if (!Expression.IsInt(expression.Type))
				{
					throw new ArgumentException("The bounds collection can only contain expression of integers types");
				}
			}
			return new NewArrayExpression(ExpressionType.NewArrayBounds, type.MakeArrayType(readOnlyCollection.Count), readOnlyCollection);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005150 File Offset: 0x00003350
		public static NewArrayExpression NewArrayInit(Type type, params Expression[] initializers)
		{
			return Expression.NewArrayInit(type, initializers);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000515C File Offset: 0x0000335C
		public static NewArrayExpression NewArrayInit(Type type, IEnumerable<Expression> initializers)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (initializers == null)
			{
				throw new ArgumentNullException("initializers");
			}
			Expression.CheckNotVoid(type);
			ReadOnlyCollection<Expression> readOnlyCollection = initializers.ToReadOnlyCollection<Expression>();
			foreach (Expression expression in readOnlyCollection)
			{
				if (expression == null)
				{
					throw new ArgumentNullException("initializers");
				}
				if (!expression.Type.IsAssignableTo(type))
				{
					throw new InvalidOperationException(string.Format("{0} IsAssignableTo {1}, expression [ {2} ] : {3}", new object[]
					{
						expression.Type,
						type,
						expression.NodeType,
						expression
					}));
				}
			}
			return new NewArrayExpression(ExpressionType.NewArrayInit, type.MakeArrayType(), readOnlyCollection);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005248 File Offset: 0x00003448
		public static UnaryExpression Not(Expression expression)
		{
			return Expression.Not(expression, null);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005254 File Offset: 0x00003454
		public static UnaryExpression Not(Expression expression, MethodInfo method)
		{
			Func<Type, bool> validator = (Type type) => Expression.IsIntOrBool(type);
			method = Expression.UnaryCoreCheck("op_LogicalNot", expression, method, validator);
			if (method == null)
			{
				method = Expression.UnaryCoreCheck("op_OnesComplement", expression, method, validator);
			}
			return Expression.MakeSimpleUnary(ExpressionType.Not, expression, method);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000052AC File Offset: 0x000034AC
		private static void CheckNotVoid(Type type)
		{
			if (type == typeof(void))
			{
				throw new ArgumentException("Type can't be void");
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000052CC File Offset: 0x000034CC
		public static ParameterExpression Parameter(Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Expression.CheckNotVoid(type);
			return new ParameterExpression(type, name);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000052EC File Offset: 0x000034EC
		public static MemberExpression Property(Expression expression, MethodInfo propertyAccessor)
		{
			if (propertyAccessor == null)
			{
				throw new ArgumentNullException("propertyAccessor");
			}
			Expression.CheckNonGenericMethod(propertyAccessor);
			if (!propertyAccessor.IsStatic)
			{
				if (expression == null)
				{
					throw new ArgumentNullException("expression");
				}
				if (!expression.Type.IsAssignableTo(propertyAccessor.DeclaringType))
				{
					throw new ArgumentException("expression");
				}
			}
			PropertyInfo associatedProperty = Expression.GetAssociatedProperty(propertyAccessor);
			if (associatedProperty == null)
			{
				throw new ArgumentException(string.Format("Method {0} has no associated property", propertyAccessor));
			}
			return new MemberExpression(expression, associatedProperty, associatedProperty.PropertyType);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005378 File Offset: 0x00003578
		private static PropertyInfo GetAssociatedProperty(MethodInfo method)
		{
			if (method == null)
			{
				return null;
			}
			foreach (PropertyInfo propertyInfo in method.DeclaringType.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
			{
				if (method.Equals(propertyInfo.GetGetMethod(true)))
				{
					return propertyInfo;
				}
				if (method.Equals(propertyInfo.GetSetMethod(true)))
				{
					return propertyInfo;
				}
			}
			return null;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000053DC File Offset: 0x000035DC
		public static MemberExpression Property(Expression expression, PropertyInfo property)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			MethodInfo getMethod = property.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException("getter");
			}
			if (!getMethod.IsStatic)
			{
				if (expression == null)
				{
					throw new ArgumentNullException("expression");
				}
				if (!expression.Type.IsAssignableTo(property.DeclaringType))
				{
					throw new ArgumentException("expression");
				}
			}
			else if (expression != null)
			{
				throw new ArgumentException("expression");
			}
			return new MemberExpression(expression, property, property.PropertyType);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005474 File Offset: 0x00003674
		public static MemberExpression Property(Expression expression, string propertyName)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			PropertyInfo property = expression.Type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (property == null)
			{
				throw new ArgumentException(string.Format("No property named {0} on {1}", propertyName, expression.Type));
			}
			return new MemberExpression(expression, property, property.PropertyType);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000054CC File Offset: 0x000036CC
		public static MemberExpression PropertyOrField(Expression expression, string propertyOrFieldName)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (propertyOrFieldName == null)
			{
				throw new ArgumentNullException("propertyOrFieldName");
			}
			PropertyInfo property = expression.Type.GetProperty(propertyOrFieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (property != null)
			{
				return new MemberExpression(expression, property, property.PropertyType);
			}
			FieldInfo field = expression.Type.GetField(propertyOrFieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (field != null)
			{
				return new MemberExpression(expression, field, field.FieldType);
			}
			throw new ArgumentException(string.Format("No field or property named {0} on {1}", propertyOrFieldName, expression.Type));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005558 File Offset: 0x00003758
		public static UnaryExpression Quote(Expression expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return new UnaryExpression(ExpressionType.Quote, expression, expression.GetType());
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000557C File Offset: 0x0000377C
		public static UnaryExpression TypeAs(Expression expression, Type type)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsValueType && !type.IsNullable())
			{
				throw new ArgumentException("TypeAs expect a reference or a nullable type");
			}
			return new UnaryExpression(ExpressionType.TypeAs, expression, type);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000055D8 File Offset: 0x000037D8
		public static TypeBinaryExpression TypeIs(Expression expression, Type type)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Expression.CheckNotVoid(type);
			return new TypeBinaryExpression(ExpressionType.TypeIs, expression, type, typeof(bool));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005618 File Offset: 0x00003818
		public static UnaryExpression UnaryPlus(Expression expression)
		{
			return Expression.UnaryPlus(expression, null);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005624 File Offset: 0x00003824
		public static UnaryExpression UnaryPlus(Expression expression, MethodInfo method)
		{
			method = Expression.UnaryCoreCheck("op_UnaryPlus", expression, method, (Type type) => Expression.IsNumber(type));
			return Expression.MakeSimpleUnary(ExpressionType.UnaryPlus, expression, method);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005668 File Offset: 0x00003868
		private static bool IsInt(Type t)
		{
			return t == typeof(byte) || t == typeof(sbyte) || t == typeof(short) || t == typeof(ushort) || t == typeof(int) || t == typeof(uint) || t == typeof(long) || t == typeof(ulong);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000056F8 File Offset: 0x000038F8
		private static bool IsIntOrBool(Type t)
		{
			return Expression.IsInt(t) || t == typeof(bool);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005718 File Offset: 0x00003918
		private static bool IsNumber(Type t)
		{
			return Expression.IsInt(t) || t == typeof(float) || t == typeof(double);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005748 File Offset: 0x00003948
		private static bool IsSignedNumber(Type t)
		{
			return Expression.IsNumber(t) && !Expression.IsUnsigned(t);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005764 File Offset: 0x00003964
		internal static bool IsUnsigned(Type t)
		{
			if (t.IsPointer)
			{
				return Expression.IsUnsigned(t.GetElementType());
			}
			return t == typeof(ushort) || t == typeof(uint) || t == typeof(ulong) || t == typeof(byte);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000057C8 File Offset: 0x000039C8
		internal virtual void Emit(EmitContext ec)
		{
			throw new NotImplementedException(string.Format("Emit method is not implemented in expression type {0}", base.GetType()));
		}

		// Token: 0x04000003 RID: 3
		internal const BindingFlags PublicInstance = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;

		// Token: 0x04000004 RID: 4
		internal const BindingFlags NonPublicInstance = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

		// Token: 0x04000005 RID: 5
		internal const BindingFlags PublicStatic = BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;

		// Token: 0x04000006 RID: 6
		internal const BindingFlags AllInstance = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

		// Token: 0x04000007 RID: 7
		internal const BindingFlags AllStatic = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

		// Token: 0x04000008 RID: 8
		internal const BindingFlags All = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

		// Token: 0x04000009 RID: 9
		private ExpressionType node_type;

		// Token: 0x0400000A RID: 10
		private Type type;
	}
}
