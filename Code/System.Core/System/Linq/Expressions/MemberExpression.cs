using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x02000047 RID: 71
	public sealed class MemberExpression : Expression
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x00013C3C File Offset: 0x00011E3C
		internal MemberExpression(Expression expression, MemberInfo member, Type type) : base(ExpressionType.MemberAccess, type)
		{
			this.expression = expression;
			this.member = member;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00013C58 File Offset: 0x00011E58
		public Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00013C60 File Offset: 0x00011E60
		public MemberInfo Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00013C68 File Offset: 0x00011E68
		internal override void Emit(EmitContext ec)
		{
			this.member.OnFieldOrProperty(delegate(FieldInfo field)
			{
				this.EmitFieldAccess(ec, field);
			}, delegate(PropertyInfo prop)
			{
				this.EmitPropertyAccess(ec, prop);
			});
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00013CAC File Offset: 0x00011EAC
		private void EmitPropertyAccess(EmitContext ec, PropertyInfo property)
		{
			MethodInfo getMethod = property.GetGetMethod(true);
			if (!getMethod.IsStatic)
			{
				ec.EmitLoadSubject(this.expression);
			}
			ec.EmitCall(getMethod);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00013CE0 File Offset: 0x00011EE0
		private void EmitFieldAccess(EmitContext ec, FieldInfo field)
		{
			if (!field.IsStatic)
			{
				ec.EmitLoadSubject(this.expression);
				ec.ig.Emit(OpCodes.Ldfld, field);
			}
			else
			{
				ec.ig.Emit(OpCodes.Ldsfld, field);
			}
		}

		// Token: 0x0400010B RID: 267
		private Expression expression;

		// Token: 0x0400010C RID: 268
		private MemberInfo member;
	}
}
