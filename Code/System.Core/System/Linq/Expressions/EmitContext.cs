using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200003C RID: 60
	internal class EmitContext
	{
		// Token: 0x060003BE RID: 958 RVA: 0x00011688 File Offset: 0x0000F888
		public EmitContext(CompilationContext context, EmitContext parent, LambdaExpression lambda)
		{
			this.context = context;
			this.parent = parent;
			this.lambda = lambda;
			this.hoisted = context.GetHoistedLocals(lambda);
			this.method = new DynamicMethod("lambda_method", lambda.GetReturnType(), EmitContext.CreateParameterTypes(lambda.Parameters), typeof(ExecutionScope), true);
			this.ig = this.method.GetILGenerator();
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060003BF RID: 959 RVA: 0x000116FC File Offset: 0x0000F8FC
		public bool HasHoistedLocals
		{
			get
			{
				return this.hoisted != null && this.hoisted.Count > 0;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001171C File Offset: 0x0000F91C
		public LambdaExpression Lambda
		{
			get
			{
				return this.lambda;
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00011724 File Offset: 0x0000F924
		public void Emit()
		{
			if (this.HasHoistedLocals)
			{
				this.EmitStoreHoistedLocals();
			}
			this.lambda.EmitBody(this);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00011744 File Offset: 0x0000F944
		private static Type[] CreateParameterTypes(IList<ParameterExpression> parameters)
		{
			Type[] array = new Type[parameters.Count + 1];
			array[0] = typeof(ExecutionScope);
			for (int i = 0; i < parameters.Count; i++)
			{
				array[i + 1] = parameters[i].Type;
			}
			return array;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00011798 File Offset: 0x0000F998
		public bool IsLocalParameter(ParameterExpression parameter, ref int position)
		{
			position = this.lambda.Parameters.IndexOf(parameter);
			if (position > -1)
			{
				position++;
				return true;
			}
			return false;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000117CC File Offset: 0x0000F9CC
		public Delegate CreateDelegate(ExecutionScope scope)
		{
			return this.method.CreateDelegate(this.lambda.Type, scope);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000117E8 File Offset: 0x0000F9E8
		public void Emit(Expression expression)
		{
			expression.Emit(this);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000117F4 File Offset: 0x0000F9F4
		public LocalBuilder EmitStored(Expression expression)
		{
			LocalBuilder localBuilder = this.ig.DeclareLocal(expression.Type);
			expression.Emit(this);
			this.ig.Emit(OpCodes.Stloc, localBuilder);
			return localBuilder;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001182C File Offset: 0x0000FA2C
		public void EmitLoadAddress(Expression expression)
		{
			this.ig.Emit(OpCodes.Ldloca, this.EmitStored(expression));
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00011848 File Offset: 0x0000FA48
		public void EmitLoadSubject(Expression expression)
		{
			if (expression.Type.IsValueType)
			{
				this.EmitLoadAddress(expression);
				return;
			}
			this.Emit(expression);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00011874 File Offset: 0x0000FA74
		public void EmitLoadSubject(LocalBuilder local)
		{
			if (local.LocalType.IsValueType)
			{
				this.EmitLoadAddress(local);
				return;
			}
			this.EmitLoad(local);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000118A0 File Offset: 0x0000FAA0
		public void EmitLoadAddress(LocalBuilder local)
		{
			this.ig.Emit(OpCodes.Ldloca, local);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000118B4 File Offset: 0x0000FAB4
		public void EmitLoad(LocalBuilder local)
		{
			this.ig.Emit(OpCodes.Ldloc, local);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000118C8 File Offset: 0x0000FAC8
		public void EmitCall(LocalBuilder local, IList<Expression> arguments, MethodInfo method)
		{
			this.EmitLoadSubject(local);
			this.EmitArguments(method, arguments);
			this.EmitCall(method);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000118E0 File Offset: 0x0000FAE0
		public void EmitCall(LocalBuilder local, MethodInfo method)
		{
			this.EmitLoadSubject(local);
			this.EmitCall(method);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000118F0 File Offset: 0x0000FAF0
		public void EmitCall(Expression expression, MethodInfo method)
		{
			if (!method.IsStatic)
			{
				this.EmitLoadSubject(expression);
			}
			this.EmitCall(method);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001190C File Offset: 0x0000FB0C
		public void EmitCall(Expression expression, IList<Expression> arguments, MethodInfo method)
		{
			if (!method.IsStatic)
			{
				this.EmitLoadSubject(expression);
			}
			this.EmitArguments(method, arguments);
			this.EmitCall(method);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001193C File Offset: 0x0000FB3C
		private void EmitArguments(MethodInfo method, IList<Expression> arguments)
		{
			ParameterInfo[] parameters = method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				Expression expression = arguments[i];
				if (parameterInfo.ParameterType.IsByRef)
				{
					this.ig.Emit(OpCodes.Ldloca, this.EmitStored(expression));
				}
				else
				{
					this.Emit(arguments[i]);
				}
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000119AC File Offset: 0x0000FBAC
		public void EmitCall(MethodInfo method)
		{
			this.ig.Emit((!method.IsVirtual) ? OpCodes.Call : OpCodes.Callvirt, method);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000119E0 File Offset: 0x0000FBE0
		public void EmitNullableHasValue(LocalBuilder local)
		{
			this.EmitCall(local, "get_HasValue");
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000119F0 File Offset: 0x0000FBF0
		public void EmitNullableInitialize(LocalBuilder local)
		{
			this.ig.Emit(OpCodes.Ldloca, local);
			this.ig.Emit(OpCodes.Initobj, local.LocalType);
			this.ig.Emit(OpCodes.Ldloc, local);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00011A38 File Offset: 0x0000FC38
		public void EmitNullableGetValue(LocalBuilder local)
		{
			this.EmitCall(local, "get_Value");
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00011A48 File Offset: 0x0000FC48
		public void EmitNullableGetValueOrDefault(LocalBuilder local)
		{
			this.EmitCall(local, "GetValueOrDefault");
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00011A58 File Offset: 0x0000FC58
		private void EmitCall(LocalBuilder local, string method_name)
		{
			this.EmitCall(local, local.LocalType.GetMethod(method_name, Type.EmptyTypes));
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00011A74 File Offset: 0x0000FC74
		public void EmitNullableNew(Type of)
		{
			this.ig.Emit(OpCodes.Newobj, of.GetConstructor(new Type[]
			{
				of.GetFirstGenericArgument()
			}));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00011A9C File Offset: 0x0000FC9C
		public void EmitCollection<T>(IEnumerable<T> collection) where T : Expression
		{
			foreach (T t in collection)
			{
				t.Emit(this);
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00011B04 File Offset: 0x0000FD04
		public void EmitCollection(IEnumerable<ElementInit> initializers, LocalBuilder local)
		{
			foreach (ElementInit elementInit in initializers)
			{
				elementInit.Emit(this, local);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00011B64 File Offset: 0x0000FD64
		public void EmitCollection(IEnumerable<MemberBinding> bindings, LocalBuilder local)
		{
			foreach (MemberBinding memberBinding in bindings)
			{
				memberBinding.Emit(this, local);
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		public void EmitIsInst(Expression expression, Type candidate)
		{
			expression.Emit(this);
			Type type = expression.Type;
			if (type.IsValueType)
			{
				this.ig.Emit(OpCodes.Box, type);
			}
			this.ig.Emit(OpCodes.Isinst, candidate);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00011C0C File Offset: 0x0000FE0C
		public void EmitScope()
		{
			this.ig.Emit(OpCodes.Ldarg_0);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00011C20 File Offset: 0x0000FE20
		public void EmitReadGlobal(object global)
		{
			this.EmitReadGlobal(global, global.GetType());
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00011C30 File Offset: 0x0000FE30
		public void EmitLoadGlobals()
		{
			this.EmitScope();
			this.ig.Emit(OpCodes.Ldfld, typeof(ExecutionScope).GetField("Globals"));
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00011C68 File Offset: 0x0000FE68
		public void EmitReadGlobal(object global, Type type)
		{
			this.EmitLoadGlobals();
			this.ig.Emit(OpCodes.Ldc_I4, this.AddGlobal(global, type));
			this.ig.Emit(OpCodes.Ldelem, typeof(object));
			this.EmitLoadStrongBoxValue(type);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		public void EmitLoadStrongBoxValue(Type type)
		{
			Type type2 = type.MakeStrongBoxType();
			this.ig.Emit(OpCodes.Isinst, type2);
			this.ig.Emit(OpCodes.Ldfld, type2.GetField("Value"));
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00011CF4 File Offset: 0x0000FEF4
		private int AddGlobal(object value, Type type)
		{
			return this.context.AddGlobal(EmitContext.CreateStrongBox(value, type));
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00011D08 File Offset: 0x0000FF08
		public void EmitCreateDelegate(LambdaExpression lambda)
		{
			this.EmitScope();
			this.ig.Emit(OpCodes.Ldc_I4, this.AddChildContext(lambda));
			if (this.hoisted_store != null)
			{
				this.ig.Emit(OpCodes.Ldloc, this.hoisted_store);
			}
			else
			{
				this.ig.Emit(OpCodes.Ldnull);
			}
			this.ig.Emit(OpCodes.Callvirt, typeof(ExecutionScope).GetMethod("CreateDelegate"));
			this.ig.Emit(OpCodes.Castclass, lambda.Type);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00011DA4 File Offset: 0x0000FFA4
		private void EmitStoreHoistedLocals()
		{
			this.EmitHoistedLocalsStore();
			for (int i = 0; i < this.hoisted.Count; i++)
			{
				this.EmitStoreHoistedLocal(i, this.hoisted[i]);
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00011DE8 File Offset: 0x0000FFE8
		private void EmitStoreHoistedLocal(int position, ParameterExpression parameter)
		{
			this.ig.Emit(OpCodes.Ldloc, this.hoisted_store);
			this.ig.Emit(OpCodes.Ldc_I4, position);
			parameter.Emit(this);
			this.EmitCreateStrongBox(parameter.Type);
			this.ig.Emit(OpCodes.Stelem, typeof(object));
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00011E4C File Offset: 0x0001004C
		public void EmitLoadHoistedLocalsStore()
		{
			this.ig.Emit(OpCodes.Ldloc, this.hoisted_store);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00011E64 File Offset: 0x00010064
		private void EmitCreateStrongBox(Type type)
		{
			this.ig.Emit(OpCodes.Newobj, type.MakeStrongBoxType().GetConstructor(new Type[]
			{
				type
			}));
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00011E8C File Offset: 0x0001008C
		private void EmitHoistedLocalsStore()
		{
			this.EmitScope();
			this.hoisted_store = this.ig.DeclareLocal(typeof(object[]));
			this.ig.Emit(OpCodes.Callvirt, typeof(ExecutionScope).GetMethod("CreateHoistedLocals"));
			this.ig.Emit(OpCodes.Stloc, this.hoisted_store);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00011EF4 File Offset: 0x000100F4
		public void EmitLoadLocals()
		{
			this.ig.Emit(OpCodes.Ldfld, typeof(ExecutionScope).GetField("Locals"));
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00011F28 File Offset: 0x00010128
		public void EmitParentScope()
		{
			this.ig.Emit(OpCodes.Ldfld, typeof(ExecutionScope).GetField("Parent"));
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00011F5C File Offset: 0x0001015C
		public void EmitIsolateExpression()
		{
			this.ig.Emit(OpCodes.Callvirt, typeof(ExecutionScope).GetMethod("IsolateExpression"));
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00011F90 File Offset: 0x00010190
		public int IndexOfHoistedLocal(ParameterExpression parameter)
		{
			if (!this.HasHoistedLocals)
			{
				return -1;
			}
			return this.hoisted.IndexOf(parameter);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00011FAC File Offset: 0x000101AC
		public bool IsHoistedLocal(ParameterExpression parameter, ref int level, ref int position)
		{
			if (this.parent == null)
			{
				return false;
			}
			if (this.parent.hoisted != null)
			{
				position = this.parent.hoisted.IndexOf(parameter);
				if (position > -1)
				{
					return true;
				}
			}
			level++;
			return this.parent.IsHoistedLocal(parameter, ref level, ref position);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00012008 File Offset: 0x00010208
		private int AddChildContext(LambdaExpression lambda)
		{
			return this.context.AddCompilationUnit(this, lambda);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00012018 File Offset: 0x00010218
		private static object CreateStrongBox(object value, Type type)
		{
			return Activator.CreateInstance(type.MakeStrongBoxType(), new object[]
			{
				value
			});
		}

		// Token: 0x040000C8 RID: 200
		private CompilationContext context;

		// Token: 0x040000C9 RID: 201
		private EmitContext parent;

		// Token: 0x040000CA RID: 202
		private LambdaExpression lambda;

		// Token: 0x040000CB RID: 203
		private DynamicMethod method;

		// Token: 0x040000CC RID: 204
		private LocalBuilder hoisted_store;

		// Token: 0x040000CD RID: 205
		private List<ParameterExpression> hoisted;

		// Token: 0x040000CE RID: 206
		public readonly ILGenerator ig;
	}
}
