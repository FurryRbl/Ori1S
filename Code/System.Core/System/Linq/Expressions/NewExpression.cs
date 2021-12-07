using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x0200004D RID: 77
	public sealed class NewExpression : Expression
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x000140B4 File Offset: 0x000122B4
		internal NewExpression(Type type, ReadOnlyCollection<Expression> arguments) : base(ExpressionType.New, type)
		{
			this.arguments = arguments;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000140C8 File Offset: 0x000122C8
		internal NewExpression(ConstructorInfo constructor, ReadOnlyCollection<Expression> arguments, ReadOnlyCollection<MemberInfo> members) : base(ExpressionType.New, constructor.DeclaringType)
		{
			this.constructor = constructor;
			this.arguments = arguments;
			this.members = members;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x000140F0 File Offset: 0x000122F0
		public ConstructorInfo Constructor
		{
			get
			{
				return this.constructor;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000140F8 File Offset: 0x000122F8
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00014100 File Offset: 0x00012300
		public ReadOnlyCollection<MemberInfo> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00014108 File Offset: 0x00012308
		internal override void Emit(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Type type = base.Type;
			LocalBuilder local = null;
			if (type.IsValueType)
			{
				local = ig.DeclareLocal(type);
				ig.Emit(OpCodes.Ldloca, local);
				if (this.constructor == null)
				{
					ig.Emit(OpCodes.Initobj, type);
					ig.Emit(OpCodes.Ldloc, local);
					return;
				}
			}
			ec.EmitCollection<Expression>(this.arguments);
			if (type.IsValueType)
			{
				ig.Emit(OpCodes.Call, this.constructor);
				ig.Emit(OpCodes.Ldloc, local);
			}
			else
			{
				ig.Emit(OpCodes.Newobj, this.constructor ?? NewExpression.GetDefaultConstructor(type));
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000141C0 File Offset: 0x000123C0
		private static ConstructorInfo GetDefaultConstructor(Type type)
		{
			return type.GetConstructor(Type.EmptyTypes);
		}

		// Token: 0x04000115 RID: 277
		private ConstructorInfo constructor;

		// Token: 0x04000116 RID: 278
		private ReadOnlyCollection<Expression> arguments;

		// Token: 0x04000117 RID: 279
		private ReadOnlyCollection<MemberInfo> members;
	}
}
