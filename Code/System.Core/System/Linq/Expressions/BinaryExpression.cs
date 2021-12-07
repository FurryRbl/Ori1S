using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x02000035 RID: 53
	public sealed class BinaryExpression : Expression
	{
		// Token: 0x06000374 RID: 884 RVA: 0x0000F980 File Offset: 0x0000DB80
		internal BinaryExpression(ExpressionType node_type, Type type, Expression left, Expression right) : base(node_type, type)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000F99C File Offset: 0x0000DB9C
		internal BinaryExpression(ExpressionType node_type, Type type, Expression left, Expression right, MethodInfo method) : base(node_type, type)
		{
			this.left = left;
			this.right = right;
			this.method = method;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		internal BinaryExpression(ExpressionType node_type, Type type, Expression left, Expression right, bool lift_to_null, bool is_lifted, MethodInfo method, LambdaExpression conversion) : base(node_type, type)
		{
			this.left = left;
			this.right = right;
			this.method = method;
			this.conversion = conversion;
			this.lift_to_null = lift_to_null;
			this.is_lifted = is_lifted;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000F9FC File Offset: 0x0000DBFC
		public Expression Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000FA04 File Offset: 0x0000DC04
		public Expression Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000FA0C File Offset: 0x0000DC0C
		public MethodInfo Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000FA14 File Offset: 0x0000DC14
		public bool IsLifted
		{
			get
			{
				return this.is_lifted;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000FA1C File Offset: 0x0000DC1C
		public bool IsLiftedToNull
		{
			get
			{
				return this.lift_to_null;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000FA24 File Offset: 0x0000DC24
		public LambdaExpression Conversion
		{
			get
			{
				return this.conversion;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		private void EmitArrayAccess(EmitContext ec)
		{
			this.left.Emit(ec);
			this.right.Emit(ec);
			ec.ig.Emit(OpCodes.Ldelem, base.Type);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000FA68 File Offset: 0x0000DC68
		private void EmitLogicalBinary(EmitContext ec)
		{
			ExpressionType nodeType = base.NodeType;
			if (nodeType != ExpressionType.And)
			{
				if (nodeType != ExpressionType.AndAlso)
				{
					if (nodeType == ExpressionType.Or)
					{
						goto IL_2A;
					}
					if (nodeType != ExpressionType.OrElse)
					{
						return;
					}
				}
				if (!this.IsLifted)
				{
					this.EmitLogicalShortCircuit(ec);
				}
				else
				{
					this.EmitLiftedLogicalShortCircuit(ec);
				}
				return;
			}
			IL_2A:
			if (!this.IsLifted)
			{
				this.EmitLogical(ec);
			}
			else if (base.Type == typeof(bool?))
			{
				this.EmitLiftedLogical(ec);
			}
			else
			{
				this.EmitLiftedArithmeticBinary(ec);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000FB08 File Offset: 0x0000DD08
		private void EmitLogical(EmitContext ec)
		{
			this.EmitNonLiftedBinary(ec);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000FB14 File Offset: 0x0000DD14
		private void EmitLiftedLogical(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			bool flag = base.NodeType == ExpressionType.And;
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			LocalBuilder localBuilder2 = ec.EmitStored(this.right);
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			Label label3 = ig.DefineLabel();
			ec.EmitNullableGetValueOrDefault(localBuilder);
			ig.Emit(OpCodes.Brtrue, label);
			ec.EmitNullableGetValueOrDefault(localBuilder2);
			ig.Emit(OpCodes.Brtrue, label2);
			ec.EmitNullableHasValue(localBuilder);
			ig.Emit(OpCodes.Brfalse, label);
			ig.MarkLabel(label2);
			ec.EmitLoad((!flag) ? localBuilder2 : localBuilder);
			ig.Emit(OpCodes.Br, label3);
			ig.MarkLabel(label);
			ec.EmitLoad((!flag) ? localBuilder : localBuilder2);
			ig.MarkLabel(label3);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000FBEC File Offset: 0x0000DDEC
		private void EmitLogicalShortCircuit(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			bool flag = base.NodeType == ExpressionType.AndAlso;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			ec.Emit(this.left);
			ig.Emit((!flag) ? OpCodes.Brtrue : OpCodes.Brfalse, label);
			ec.Emit(this.right);
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			ig.Emit((!flag) ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
			ig.MarkLabel(label2);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000FC84 File Offset: 0x0000DE84
		private MethodInfo GetFalseOperator()
		{
			return Expression.GetFalseOperator(this.left.Type.GetNotNullableType());
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000FC9C File Offset: 0x0000DE9C
		private MethodInfo GetTrueOperator()
		{
			return Expression.GetTrueOperator(this.left.Type.GetNotNullableType());
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000FCB4 File Offset: 0x0000DEB4
		private void EmitUserDefinedLogicalShortCircuit(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			bool flag = base.NodeType == ExpressionType.AndAlso;
			Label label = ig.DefineLabel();
			LocalBuilder local = ec.EmitStored(this.left);
			ec.EmitLoad(local);
			ig.Emit(OpCodes.Dup);
			ec.EmitCall((!flag) ? this.GetTrueOperator() : this.GetFalseOperator());
			ig.Emit(OpCodes.Brtrue, label);
			ec.Emit(this.right);
			ec.EmitCall(this.method);
			ig.MarkLabel(label);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000FD40 File Offset: 0x0000DF40
		private void EmitLiftedLogicalShortCircuit(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			bool flag = base.NodeType == ExpressionType.AndAlso;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			Label label3 = ig.DefineLabel();
			Label label4 = ig.DefineLabel();
			Label label5 = ig.DefineLabel();
			LocalBuilder local = ec.EmitStored(this.left);
			ec.EmitNullableHasValue(local);
			ig.Emit(OpCodes.Brfalse, label);
			ec.EmitNullableGetValueOrDefault(local);
			ig.Emit(OpCodes.Ldc_I4_0);
			ig.Emit(OpCodes.Ceq);
			ig.Emit((!flag) ? OpCodes.Brfalse : OpCodes.Brtrue, label2);
			ig.MarkLabel(label);
			LocalBuilder local2 = ec.EmitStored(this.right);
			ec.EmitNullableHasValue(local2);
			ig.Emit(OpCodes.Brfalse_S, label3);
			ec.EmitNullableGetValueOrDefault(local2);
			ig.Emit(OpCodes.Ldc_I4_0);
			ig.Emit(OpCodes.Ceq);
			ig.Emit((!flag) ? OpCodes.Brfalse : OpCodes.Brtrue, label2);
			ec.EmitNullableHasValue(local);
			ig.Emit(OpCodes.Brfalse, label3);
			ig.Emit((!flag) ? OpCodes.Ldc_I4_0 : OpCodes.Ldc_I4_1);
			ig.Emit(OpCodes.Br_S, label4);
			ig.MarkLabel(label2);
			ig.Emit((!flag) ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
			ig.MarkLabel(label4);
			ec.EmitNullableNew(base.Type);
			ig.Emit(OpCodes.Br, label5);
			ig.MarkLabel(label3);
			LocalBuilder local3 = ig.DeclareLocal(base.Type);
			ec.EmitNullableInitialize(local3);
			ig.MarkLabel(label5);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		private void EmitCoalesce(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			bool flag = localBuilder.LocalType.IsNullable();
			if (flag)
			{
				ec.EmitNullableHasValue(localBuilder);
			}
			else
			{
				ec.EmitLoad(localBuilder);
			}
			ig.Emit(OpCodes.Brfalse, label2);
			if (flag && !base.Type.IsNullable())
			{
				ec.EmitNullableGetValue(localBuilder);
			}
			else
			{
				ec.EmitLoad(localBuilder);
			}
			ig.Emit(OpCodes.Br, label);
			ig.MarkLabel(label2);
			ec.Emit(this.right);
			ig.MarkLabel(label);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000FFA0 File Offset: 0x0000E1A0
		private void EmitConvertedCoalesce(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			if (localBuilder.LocalType.IsNullable())
			{
				ec.EmitNullableHasValue(localBuilder);
			}
			else
			{
				ec.EmitLoad(localBuilder);
			}
			ig.Emit(OpCodes.Brfalse, label2);
			ec.Emit(this.conversion);
			ec.EmitLoad(localBuilder);
			ig.Emit(OpCodes.Callvirt, this.conversion.Type.GetInvokeMethod());
			ig.Emit(OpCodes.Br, label);
			ig.MarkLabel(label2);
			ec.Emit(this.right);
			ig.MarkLabel(label);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00010054 File Offset: 0x0000E254
		private static bool IsInt32OrInt64(Type type)
		{
			return type == typeof(int) || type == typeof(long);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00010084 File Offset: 0x0000E284
		private static bool IsSingleOrDouble(Type type)
		{
			return type == typeof(float) || type == typeof(double);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000100B4 File Offset: 0x0000E2B4
		private void EmitBinaryOperator(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			bool flag = Expression.IsUnsigned(this.left.Type);
			ExpressionType nodeType = base.NodeType;
			switch (nodeType)
			{
			case ExpressionType.Divide:
				ig.Emit((!flag) ? OpCodes.Div : OpCodes.Div_Un);
				break;
			case ExpressionType.Equal:
				ig.Emit(OpCodes.Ceq);
				break;
			case ExpressionType.ExclusiveOr:
				ig.Emit(OpCodes.Xor);
				break;
			case ExpressionType.GreaterThan:
				ig.Emit((!flag) ? OpCodes.Cgt : OpCodes.Cgt_Un);
				break;
			case ExpressionType.GreaterThanOrEqual:
				if (flag || BinaryExpression.IsSingleOrDouble(this.left.Type))
				{
					ig.Emit(OpCodes.Clt_Un);
				}
				else
				{
					ig.Emit(OpCodes.Clt);
				}
				ig.Emit(OpCodes.Ldc_I4_0);
				ig.Emit(OpCodes.Ceq);
				break;
			default:
				switch (nodeType)
				{
				case ExpressionType.Add:
					ig.Emit(OpCodes.Add);
					break;
				case ExpressionType.AddChecked:
					if (BinaryExpression.IsInt32OrInt64(this.left.Type))
					{
						ig.Emit(OpCodes.Add_Ovf);
					}
					else
					{
						ig.Emit((!flag) ? OpCodes.Add : OpCodes.Add_Ovf_Un);
					}
					break;
				case ExpressionType.And:
					ig.Emit(OpCodes.And);
					break;
				default:
					throw new InvalidOperationException(string.Format("Internal error: BinaryExpression contains non-Binary nodetype {0}", base.NodeType));
				}
				break;
			case ExpressionType.LeftShift:
			case ExpressionType.RightShift:
				ig.Emit(OpCodes.Ldc_I4, (this.left.Type != typeof(int)) ? 63 : 31);
				ig.Emit(OpCodes.And);
				if (base.NodeType == ExpressionType.RightShift)
				{
					ig.Emit((!flag) ? OpCodes.Shr : OpCodes.Shr_Un);
				}
				else
				{
					ig.Emit(OpCodes.Shl);
				}
				break;
			case ExpressionType.LessThan:
				ig.Emit((!flag) ? OpCodes.Clt : OpCodes.Clt_Un);
				break;
			case ExpressionType.LessThanOrEqual:
				if (flag || BinaryExpression.IsSingleOrDouble(this.left.Type))
				{
					ig.Emit(OpCodes.Cgt_Un);
				}
				else
				{
					ig.Emit(OpCodes.Cgt);
				}
				ig.Emit(OpCodes.Ldc_I4_0);
				ig.Emit(OpCodes.Ceq);
				break;
			case ExpressionType.Modulo:
				ig.Emit((!flag) ? OpCodes.Rem : OpCodes.Rem_Un);
				break;
			case ExpressionType.Multiply:
				ig.Emit(OpCodes.Mul);
				break;
			case ExpressionType.MultiplyChecked:
				if (BinaryExpression.IsInt32OrInt64(this.left.Type))
				{
					ig.Emit(OpCodes.Mul_Ovf);
				}
				else
				{
					ig.Emit((!flag) ? OpCodes.Mul : OpCodes.Mul_Ovf_Un);
				}
				break;
			case ExpressionType.NotEqual:
				ig.Emit(OpCodes.Ceq);
				ig.Emit(OpCodes.Ldc_I4_0);
				ig.Emit(OpCodes.Ceq);
				break;
			case ExpressionType.Or:
				ig.Emit(OpCodes.Or);
				break;
			case ExpressionType.Power:
				ig.Emit(OpCodes.Call, typeof(Math).GetMethod("Pow"));
				break;
			case ExpressionType.Subtract:
				ig.Emit(OpCodes.Sub);
				break;
			case ExpressionType.SubtractChecked:
				if (BinaryExpression.IsInt32OrInt64(this.left.Type))
				{
					ig.Emit(OpCodes.Sub_Ovf);
				}
				else
				{
					ig.Emit((!flag) ? OpCodes.Sub : OpCodes.Sub_Ovf_Un);
				}
				break;
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000104BC File Offset: 0x0000E6BC
		private bool IsLeftLiftedBinary()
		{
			return this.left.Type.IsNullable() && !this.right.Type.IsNullable();
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000104F4 File Offset: 0x0000E6F4
		private void EmitLeftLiftedToNullBinary(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			LocalBuilder local = ec.EmitStored(this.left);
			ec.EmitNullableHasValue(local);
			ig.Emit(OpCodes.Brfalse, label);
			ec.EmitNullableGetValueOrDefault(local);
			ec.Emit(this.right);
			this.EmitBinaryOperator(ec);
			ec.EmitNullableNew(base.Type);
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			LocalBuilder local2 = ig.DeclareLocal(base.Type);
			ec.EmitNullableInitialize(local2);
			ig.MarkLabel(label2);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001058C File Offset: 0x0000E78C
		private void EmitLiftedArithmeticBinary(EmitContext ec)
		{
			if (this.IsLeftLiftedBinary())
			{
				this.EmitLeftLiftedToNullBinary(ec);
			}
			else
			{
				this.EmitLiftedToNullBinary(ec);
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000105AC File Offset: 0x0000E7AC
		private void EmitLiftedToNullBinary(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			LocalBuilder local = ec.EmitStored(this.left);
			LocalBuilder local2 = ec.EmitStored(this.right);
			LocalBuilder localBuilder = ig.DeclareLocal(base.Type);
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			ec.EmitNullableHasValue(local);
			ec.EmitNullableHasValue(local2);
			ig.Emit(OpCodes.And);
			ig.Emit(OpCodes.Brtrue, label);
			ec.EmitNullableInitialize(localBuilder);
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			ec.EmitNullableGetValueOrDefault(local);
			ec.EmitNullableGetValueOrDefault(local2);
			this.EmitBinaryOperator(ec);
			ec.EmitNullableNew(localBuilder.LocalType);
			ig.MarkLabel(label2);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00010664 File Offset: 0x0000E864
		private void EmitLiftedRelationalBinary(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			LocalBuilder local = ec.EmitStored(this.left);
			LocalBuilder local2 = ec.EmitStored(this.right);
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			ec.EmitNullableGetValueOrDefault(local);
			ec.EmitNullableGetValueOrDefault(local2);
			ExpressionType nodeType = base.NodeType;
			if (nodeType != ExpressionType.Equal && nodeType != ExpressionType.NotEqual)
			{
				this.EmitBinaryOperator(ec);
				ig.Emit(OpCodes.Brfalse, label);
			}
			else
			{
				ig.Emit(OpCodes.Bne_Un, label);
			}
			ec.EmitNullableHasValue(local);
			ec.EmitNullableHasValue(local2);
			nodeType = base.NodeType;
			if (nodeType != ExpressionType.Equal)
			{
				if (nodeType != ExpressionType.NotEqual)
				{
					ig.Emit(OpCodes.And);
				}
				else
				{
					ig.Emit(OpCodes.Ceq);
					ig.Emit(OpCodes.Ldc_I4_0);
					ig.Emit(OpCodes.Ceq);
				}
			}
			else
			{
				ig.Emit(OpCodes.Ceq);
			}
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			ig.Emit((base.NodeType != ExpressionType.NotEqual) ? OpCodes.Ldc_I4_0 : OpCodes.Ldc_I4_1);
			ig.MarkLabel(label2);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000107A8 File Offset: 0x0000E9A8
		private void EmitArithmeticBinary(EmitContext ec)
		{
			if (!this.IsLifted)
			{
				this.EmitNonLiftedBinary(ec);
			}
			else
			{
				this.EmitLiftedArithmeticBinary(ec);
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000107C8 File Offset: 0x0000E9C8
		private void EmitNonLiftedBinary(EmitContext ec)
		{
			ec.Emit(this.left);
			ec.Emit(this.right);
			this.EmitBinaryOperator(ec);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000107EC File Offset: 0x0000E9EC
		private void EmitRelationalBinary(EmitContext ec)
		{
			if (!this.IsLifted)
			{
				this.EmitNonLiftedBinary(ec);
			}
			else if (this.IsLiftedToNull)
			{
				this.EmitLiftedToNullBinary(ec);
			}
			else
			{
				this.EmitLiftedRelationalBinary(ec);
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00010830 File Offset: 0x0000EA30
		private void EmitLiftedUserDefinedOperator(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			Label label3 = ig.DefineLabel();
			LocalBuilder local = ec.EmitStored(this.left);
			LocalBuilder local2 = ec.EmitStored(this.right);
			ec.EmitNullableHasValue(local);
			ec.EmitNullableHasValue(local2);
			ExpressionType nodeType = base.NodeType;
			if (nodeType != ExpressionType.Equal)
			{
				if (nodeType != ExpressionType.NotEqual)
				{
					ig.Emit(OpCodes.And);
					ig.Emit(OpCodes.Brfalse, label2);
				}
				else
				{
					ig.Emit(OpCodes.Bne_Un, label);
					ec.EmitNullableHasValue(local);
					ig.Emit(OpCodes.Brfalse, label2);
				}
			}
			else
			{
				ig.Emit(OpCodes.Bne_Un, label2);
				ec.EmitNullableHasValue(local);
				ig.Emit(OpCodes.Brfalse, label);
			}
			ec.EmitNullableGetValueOrDefault(local);
			ec.EmitNullableGetValueOrDefault(local2);
			ec.EmitCall(this.method);
			ig.Emit(OpCodes.Br, label3);
			ig.MarkLabel(label);
			ig.Emit(OpCodes.Ldc_I4_1);
			ig.Emit(OpCodes.Br, label3);
			ig.MarkLabel(label2);
			ig.Emit(OpCodes.Ldc_I4_0);
			ig.Emit(OpCodes.Br, label3);
			ig.MarkLabel(label3);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00010978 File Offset: 0x0000EB78
		private void EmitLiftedToNullUserDefinedOperator(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			LocalBuilder local = ec.EmitStored(this.left);
			LocalBuilder local2 = ec.EmitStored(this.right);
			ec.EmitNullableHasValue(local);
			ec.EmitNullableHasValue(local2);
			ig.Emit(OpCodes.And);
			ig.Emit(OpCodes.Brfalse, label);
			ec.EmitNullableGetValueOrDefault(local);
			ec.EmitNullableGetValueOrDefault(local2);
			ec.EmitCall(this.method);
			ec.EmitNullableNew(base.Type);
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			LocalBuilder local3 = ig.DeclareLocal(base.Type);
			ec.EmitNullableInitialize(local3);
			ig.MarkLabel(label2);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00010A34 File Offset: 0x0000EC34
		private void EmitUserDefinedLiftedLogicalShortCircuit(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			bool flag = base.NodeType == ExpressionType.AndAlso;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			Label label3 = ig.DefineLabel();
			Label label4 = ig.DefineLabel();
			LocalBuilder local = ec.EmitStored(this.left);
			ec.EmitNullableHasValue(local);
			ig.Emit(OpCodes.Brfalse, (!flag) ? label : label3);
			ec.EmitNullableGetValueOrDefault(local);
			ec.EmitCall((!flag) ? this.GetTrueOperator() : this.GetFalseOperator());
			ig.Emit(OpCodes.Brtrue, label2);
			ig.MarkLabel(label);
			LocalBuilder local2 = ec.EmitStored(this.right);
			ec.EmitNullableHasValue(local2);
			ig.Emit(OpCodes.Brfalse, label3);
			ec.EmitNullableGetValueOrDefault(local);
			ec.EmitNullableGetValueOrDefault(local2);
			ec.EmitCall(this.method);
			ec.EmitNullableNew(base.Type);
			ig.Emit(OpCodes.Br, label4);
			ig.MarkLabel(label2);
			ec.EmitLoad(local);
			ig.Emit(OpCodes.Br, label4);
			ig.MarkLabel(label3);
			LocalBuilder local3 = ig.DeclareLocal(base.Type);
			ec.EmitNullableInitialize(local3);
			ig.MarkLabel(label4);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00010B74 File Offset: 0x0000ED74
		private void EmitUserDefinedOperator(EmitContext ec)
		{
			if (!this.IsLifted)
			{
				ExpressionType nodeType = base.NodeType;
				if (nodeType != ExpressionType.AndAlso && nodeType != ExpressionType.OrElse)
				{
					this.left.Emit(ec);
					this.right.Emit(ec);
					ec.EmitCall(this.method);
				}
				else
				{
					this.EmitUserDefinedLogicalShortCircuit(ec);
				}
			}
			else if (this.IsLiftedToNull)
			{
				ExpressionType nodeType = base.NodeType;
				if (nodeType != ExpressionType.AndAlso && nodeType != ExpressionType.OrElse)
				{
					this.EmitLiftedToNullUserDefinedOperator(ec);
				}
				else
				{
					this.EmitUserDefinedLiftedLogicalShortCircuit(ec);
				}
			}
			else
			{
				this.EmitLiftedUserDefinedOperator(ec);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00010C2C File Offset: 0x0000EE2C
		internal override void Emit(EmitContext ec)
		{
			if (this.method != null)
			{
				this.EmitUserDefinedOperator(ec);
				return;
			}
			switch (base.NodeType)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
			case ExpressionType.Divide:
			case ExpressionType.ExclusiveOr:
			case ExpressionType.LeftShift:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
			case ExpressionType.Power:
			case ExpressionType.RightShift:
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				this.EmitArithmeticBinary(ec);
				return;
			case ExpressionType.And:
			case ExpressionType.AndAlso:
			case ExpressionType.Or:
			case ExpressionType.OrElse:
				this.EmitLogicalBinary(ec);
				return;
			case ExpressionType.ArrayIndex:
				this.EmitArrayAccess(ec);
				return;
			case ExpressionType.Coalesce:
				if (this.conversion != null)
				{
					this.EmitConvertedCoalesce(ec);
				}
				else
				{
					this.EmitCoalesce(ec);
				}
				return;
			case ExpressionType.Equal:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
			case ExpressionType.NotEqual:
				this.EmitRelationalBinary(ec);
				return;
			}
			throw new NotSupportedException(base.NodeType.ToString());
		}

		// Token: 0x040000B3 RID: 179
		private Expression left;

		// Token: 0x040000B4 RID: 180
		private Expression right;

		// Token: 0x040000B5 RID: 181
		private LambdaExpression conversion;

		// Token: 0x040000B6 RID: 182
		private MethodInfo method;

		// Token: 0x040000B7 RID: 183
		private bool lift_to_null;

		// Token: 0x040000B8 RID: 184
		private bool is_lifted;
	}
}
