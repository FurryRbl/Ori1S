using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x02000044 RID: 68
	public sealed class MemberAssignment : MemberBinding
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x00013A54 File Offset: 0x00011C54
		internal MemberAssignment(MemberInfo member, Expression expression) : base(MemberBindingType.Assignment, member)
		{
			this.expression = expression;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00013A68 File Offset: 0x00011C68
		public Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00013A70 File Offset: 0x00011C70
		internal override void Emit(EmitContext ec, LocalBuilder local)
		{
			base.Member.OnFieldOrProperty(delegate(FieldInfo field)
			{
				this.EmitFieldAssignment(ec, field, local);
			}, delegate(PropertyInfo prop)
			{
				this.EmitPropertyAssignment(ec, prop, local);
			});
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00013ABC File Offset: 0x00011CBC
		private void EmitFieldAssignment(EmitContext ec, FieldInfo field, LocalBuilder local)
		{
			ec.EmitLoadSubject(local);
			this.expression.Emit(ec);
			ec.ig.Emit(OpCodes.Stfld, field);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00013AF0 File Offset: 0x00011CF0
		private void EmitPropertyAssignment(EmitContext ec, PropertyInfo property, LocalBuilder local)
		{
			MethodInfo setMethod = property.GetSetMethod(true);
			if (setMethod == null)
			{
				throw new InvalidOperationException();
			}
			ec.EmitLoadSubject(local);
			this.expression.Emit(ec);
			ec.EmitCall(setMethod);
		}

		// Token: 0x04000104 RID: 260
		private Expression expression;
	}
}
