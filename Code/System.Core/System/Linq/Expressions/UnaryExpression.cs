using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x02000050 RID: 80
	public sealed class UnaryExpression : Expression
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x0001434C File Offset: 0x0001254C
		internal UnaryExpression(ExpressionType node_type, Expression operand, Type type) : base(node_type, type)
		{
			this.operand = operand;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00014360 File Offset: 0x00012560
		internal UnaryExpression(ExpressionType node_type, Expression operand, Type type, MethodInfo method, bool is_lifted) : base(node_type, type)
		{
			this.operand = operand;
			this.method = method;
			this.is_lifted = is_lifted;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00014384 File Offset: 0x00012584
		public Expression Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0001438C File Offset: 0x0001258C
		public MethodInfo Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00014394 File Offset: 0x00012594
		public bool IsLifted
		{
			get
			{
				return this.is_lifted;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0001439C File Offset: 0x0001259C
		public bool IsLiftedToNull
		{
			get
			{
				return this.is_lifted && base.Type.IsNullable();
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000143B8 File Offset: 0x000125B8
		private void EmitArrayLength(EmitContext ec)
		{
			this.operand.Emit(ec);
			ec.ig.Emit(OpCodes.Ldlen);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000143D8 File Offset: 0x000125D8
		private void EmitTypeAs(EmitContext ec)
		{
			Type type = base.Type;
			ec.EmitIsInst(this.operand, type);
			if (type.IsNullable())
			{
				ec.ig.Emit(OpCodes.Unbox_Any, type);
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00014418 File Offset: 0x00012618
		private void EmitLiftedUnary(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			LocalBuilder local = ec.EmitStored(this.operand);
			LocalBuilder local2 = ig.DeclareLocal(base.Type);
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			ec.EmitNullableHasValue(local);
			ig.Emit(OpCodes.Brtrue, label);
			ec.EmitNullableInitialize(local2);
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			ec.EmitNullableGetValueOrDefault(local);
			this.EmitUnaryOperator(ec);
			ec.EmitNullableNew(base.Type);
			ig.MarkLabel(label2);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000144A8 File Offset: 0x000126A8
		private void EmitUnaryOperator(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			ExpressionType nodeType = base.NodeType;
			switch (nodeType)
			{
			case ExpressionType.Negate:
				ig.Emit(OpCodes.Neg);
				break;
			default:
				if (nodeType != ExpressionType.Convert && nodeType != ExpressionType.ConvertChecked)
				{
					if (nodeType == ExpressionType.Not)
					{
						if (this.operand.Type.GetNotNullableType() == typeof(bool))
						{
							ig.Emit(OpCodes.Ldc_I4_0);
							ig.Emit(OpCodes.Ceq);
						}
						else
						{
							ig.Emit(OpCodes.Not);
						}
					}
				}
				else
				{
					this.EmitPrimitiveConversion(ec, this.operand.Type.GetNotNullableType(), base.Type.GetNotNullableType());
				}
				break;
			case ExpressionType.NegateChecked:
				ig.Emit(OpCodes.Ldc_I4_M1);
				ig.Emit((!Expression.IsUnsigned(this.operand.Type)) ? OpCodes.Mul_Ovf : OpCodes.Mul_Ovf_Un);
				break;
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000145B0 File Offset: 0x000127B0
		private void EmitConvert(EmitContext ec)
		{
			Type type = this.operand.Type;
			Type type2 = base.Type;
			if (type == type2)
			{
				this.operand.Emit(ec);
			}
			else if (type.IsNullable() && !type2.IsNullable())
			{
				this.EmitConvertFromNullable(ec);
			}
			else if (!type.IsNullable() && type2.IsNullable())
			{
				this.EmitConvertToNullable(ec);
			}
			else if (type.IsNullable() && type2.IsNullable())
			{
				this.EmitConvertFromNullableToNullable(ec);
			}
			else if (Expression.IsReferenceConversion(type, type2))
			{
				this.EmitCast(ec);
			}
			else
			{
				if (!Expression.IsPrimitiveConversion(type, type2))
				{
					throw new NotImplementedException();
				}
				this.EmitPrimitiveConversion(ec);
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00014684 File Offset: 0x00012884
		private void EmitConvertFromNullableToNullable(EmitContext ec)
		{
			this.EmitLiftedUnary(ec);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00014690 File Offset: 0x00012890
		private void EmitConvertToNullable(EmitContext ec)
		{
			ec.Emit(this.operand);
			if (this.IsUnBoxing())
			{
				this.EmitUnbox(ec);
				return;
			}
			if (this.operand.Type != base.Type.GetNotNullableType())
			{
				this.EmitPrimitiveConversion(ec, this.operand.Type, base.Type.GetNotNullableType());
			}
			ec.EmitNullableNew(base.Type);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00014700 File Offset: 0x00012900
		private void EmitConvertFromNullable(EmitContext ec)
		{
			if (this.IsBoxing())
			{
				ec.Emit(this.operand);
				this.EmitBox(ec);
				return;
			}
			ec.EmitCall(this.operand, this.operand.Type.GetMethod("get_Value"));
			if (this.operand.Type.GetNotNullableType() != base.Type)
			{
				this.EmitPrimitiveConversion(ec, this.operand.Type.GetNotNullableType(), base.Type);
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00014788 File Offset: 0x00012988
		private bool IsBoxing()
		{
			return this.operand.Type.IsValueType && !base.Type.IsValueType;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000147BC File Offset: 0x000129BC
		private void EmitBox(EmitContext ec)
		{
			ec.ig.Emit(OpCodes.Box, this.operand.Type);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000147DC File Offset: 0x000129DC
		private bool IsUnBoxing()
		{
			return !this.operand.Type.IsValueType && base.Type.IsValueType;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001480C File Offset: 0x00012A0C
		private void EmitUnbox(EmitContext ec)
		{
			ec.ig.Emit(OpCodes.Unbox_Any, base.Type);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00014824 File Offset: 0x00012A24
		private void EmitCast(EmitContext ec)
		{
			this.operand.Emit(ec);
			if (this.IsBoxing())
			{
				this.EmitBox(ec);
			}
			else if (this.IsUnBoxing())
			{
				this.EmitUnbox(ec);
			}
			else
			{
				ec.ig.Emit(OpCodes.Castclass, base.Type);
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00014884 File Offset: 0x00012A84
		private void EmitPrimitiveConversion(EmitContext ec, bool is_unsigned, OpCode signed, OpCode unsigned, OpCode signed_checked, OpCode unsigned_checked)
		{
			if (base.NodeType != ExpressionType.ConvertChecked)
			{
				ec.ig.Emit((!is_unsigned) ? signed : unsigned);
			}
			else
			{
				ec.ig.Emit((!is_unsigned) ? signed_checked : unsigned_checked);
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000148D8 File Offset: 0x00012AD8
		private void EmitPrimitiveConversion(EmitContext ec)
		{
			this.operand.Emit(ec);
			this.EmitPrimitiveConversion(ec, this.operand.Type, base.Type);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001490C File Offset: 0x00012B0C
		private void EmitPrimitiveConversion(EmitContext ec, Type from, Type to)
		{
			bool flag = Expression.IsUnsigned(from);
			switch (Type.GetTypeCode(to))
			{
			case TypeCode.SByte:
				this.EmitPrimitiveConversion(ec, flag, OpCodes.Conv_I1, OpCodes.Conv_U1, OpCodes.Conv_Ovf_I1, OpCodes.Conv_Ovf_I1_Un);
				return;
			case TypeCode.Byte:
				this.EmitPrimitiveConversion(ec, flag, OpCodes.Conv_I1, OpCodes.Conv_U1, OpCodes.Conv_Ovf_U1, OpCodes.Conv_Ovf_U1_Un);
				return;
			case TypeCode.Int16:
				this.EmitPrimitiveConversion(ec, flag, OpCodes.Conv_I2, OpCodes.Conv_U2, OpCodes.Conv_Ovf_I2, OpCodes.Conv_Ovf_I2_Un);
				return;
			case TypeCode.UInt16:
				this.EmitPrimitiveConversion(ec, flag, OpCodes.Conv_I2, OpCodes.Conv_U2, OpCodes.Conv_Ovf_U2, OpCodes.Conv_Ovf_U2_Un);
				return;
			case TypeCode.Int32:
				this.EmitPrimitiveConversion(ec, flag, OpCodes.Conv_I4, OpCodes.Conv_U4, OpCodes.Conv_Ovf_I4, OpCodes.Conv_Ovf_I4_Un);
				return;
			case TypeCode.UInt32:
				this.EmitPrimitiveConversion(ec, flag, OpCodes.Conv_I4, OpCodes.Conv_U4, OpCodes.Conv_Ovf_U4, OpCodes.Conv_Ovf_U4_Un);
				return;
			case TypeCode.Int64:
				this.EmitPrimitiveConversion(ec, flag, OpCodes.Conv_I8, OpCodes.Conv_U8, OpCodes.Conv_Ovf_I8, OpCodes.Conv_Ovf_I8_Un);
				return;
			case TypeCode.UInt64:
				this.EmitPrimitiveConversion(ec, flag, OpCodes.Conv_I8, OpCodes.Conv_U8, OpCodes.Conv_Ovf_U8, OpCodes.Conv_Ovf_U8_Un);
				return;
			case TypeCode.Single:
				if (flag)
				{
					ec.ig.Emit(OpCodes.Conv_R_Un);
				}
				ec.ig.Emit(OpCodes.Conv_R4);
				return;
			case TypeCode.Double:
				if (flag)
				{
					ec.ig.Emit(OpCodes.Conv_R_Un);
				}
				ec.ig.Emit(OpCodes.Conv_R8);
				return;
			default:
				throw new NotImplementedException(base.Type.ToString());
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00014AA4 File Offset: 0x00012CA4
		private void EmitArithmeticUnary(EmitContext ec)
		{
			if (!this.IsLifted)
			{
				this.operand.Emit(ec);
				this.EmitUnaryOperator(ec);
			}
			else
			{
				this.EmitLiftedUnary(ec);
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00014ADC File Offset: 0x00012CDC
		private void EmitUserDefinedLiftedToNullOperator(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			LocalBuilder local = ec.EmitStored(this.operand);
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			ec.EmitNullableHasValue(local);
			ig.Emit(OpCodes.Brfalse, label);
			ec.EmitNullableGetValueOrDefault(local);
			ec.EmitCall(this.method);
			ec.EmitNullableNew(base.Type);
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			LocalBuilder local2 = ig.DeclareLocal(base.Type);
			ec.EmitNullableInitialize(local2);
			ig.MarkLabel(label2);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00014B70 File Offset: 0x00012D70
		private void EmitUserDefinedLiftedOperator(EmitContext ec)
		{
			LocalBuilder local = ec.EmitStored(this.operand);
			ec.EmitNullableGetValue(local);
			ec.EmitCall(this.method);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00014BA0 File Offset: 0x00012DA0
		private void EmitUserDefinedOperator(EmitContext ec)
		{
			if (!this.IsLifted)
			{
				ec.Emit(this.operand);
				ec.EmitCall(this.method);
			}
			else if (this.IsLiftedToNull)
			{
				this.EmitUserDefinedLiftedToNullOperator(ec);
			}
			else
			{
				this.EmitUserDefinedLiftedOperator(ec);
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00014BF4 File Offset: 0x00012DF4
		private void EmitQuote(EmitContext ec)
		{
			ec.EmitScope();
			ec.EmitReadGlobal(this.operand, typeof(Expression));
			if (ec.HasHoistedLocals)
			{
				ec.EmitLoadHoistedLocalsStore();
			}
			else
			{
				ec.ig.Emit(OpCodes.Ldnull);
			}
			ec.EmitIsolateExpression();
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00014C4C File Offset: 0x00012E4C
		internal override void Emit(EmitContext ec)
		{
			if (this.method != null)
			{
				this.EmitUserDefinedOperator(ec);
				return;
			}
			ExpressionType nodeType = base.NodeType;
			switch (nodeType)
			{
			case ExpressionType.Negate:
			case ExpressionType.UnaryPlus:
			case ExpressionType.NegateChecked:
			case ExpressionType.Not:
				this.EmitArithmeticUnary(ec);
				return;
			default:
				if (nodeType == ExpressionType.Convert || nodeType == ExpressionType.ConvertChecked)
				{
					this.EmitConvert(ec);
					return;
				}
				if (nodeType == ExpressionType.ArrayLength)
				{
					this.EmitArrayLength(ec);
					return;
				}
				if (nodeType == ExpressionType.Quote)
				{
					this.EmitQuote(ec);
					return;
				}
				if (nodeType != ExpressionType.TypeAs)
				{
					throw new NotImplementedException(base.NodeType.ToString());
				}
				this.EmitTypeAs(ec);
				return;
			}
		}

		// Token: 0x0400011B RID: 283
		private Expression operand;

		// Token: 0x0400011C RID: 284
		private MethodInfo method;

		// Token: 0x0400011D RID: 285
		private bool is_lifted;
	}
}
