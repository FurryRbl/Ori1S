using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x0200004C RID: 76
	public sealed class NewArrayExpression : Expression
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x00013F28 File Offset: 0x00012128
		internal NewArrayExpression(ExpressionType et, Type type, ReadOnlyCollection<Expression> expressions) : base(et, type)
		{
			this.expressions = expressions;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00013F3C File Offset: 0x0001213C
		public ReadOnlyCollection<Expression> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00013F44 File Offset: 0x00012144
		private void EmitNewArrayInit(EmitContext ec, Type type)
		{
			int count = this.expressions.Count;
			ec.ig.Emit(OpCodes.Ldc_I4, count);
			ec.ig.Emit(OpCodes.Newarr, type);
			for (int i = 0; i < count; i++)
			{
				ec.ig.Emit(OpCodes.Dup);
				ec.ig.Emit(OpCodes.Ldc_I4, i);
				this.expressions[i].Emit(ec);
				ec.ig.Emit(OpCodes.Stelem, type);
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00013FD8 File Offset: 0x000121D8
		private void EmitNewArrayBounds(EmitContext ec, Type type)
		{
			int count = this.expressions.Count;
			ec.EmitCollection<Expression>(this.expressions);
			if (count == 1)
			{
				ec.ig.Emit(OpCodes.Newarr, type);
				return;
			}
			ec.ig.Emit(OpCodes.Newobj, NewArrayExpression.GetArrayConstructor(type, count));
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00014030 File Offset: 0x00012230
		private static ConstructorInfo GetArrayConstructor(Type type, int rank)
		{
			return NewArrayExpression.CreateArray(type, rank).GetConstructor(NewArrayExpression.CreateTypeParameters(rank));
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00014044 File Offset: 0x00012244
		private static Type[] CreateTypeParameters(int rank)
		{
			return Enumerable.Repeat<Type>(typeof(int), rank).ToArray<Type>();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001405C File Offset: 0x0001225C
		private static Type CreateArray(Type type, int rank)
		{
			return type.MakeArrayType(rank);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00014068 File Offset: 0x00012268
		internal override void Emit(EmitContext ec)
		{
			Type elementType = base.Type.GetElementType();
			ExpressionType nodeType = base.NodeType;
			if (nodeType == ExpressionType.NewArrayInit)
			{
				this.EmitNewArrayInit(ec, elementType);
				return;
			}
			if (nodeType != ExpressionType.NewArrayBounds)
			{
				throw new NotSupportedException();
			}
			this.EmitNewArrayBounds(ec, elementType);
		}

		// Token: 0x04000114 RID: 276
		private ReadOnlyCollection<Expression> expressions;
	}
}
