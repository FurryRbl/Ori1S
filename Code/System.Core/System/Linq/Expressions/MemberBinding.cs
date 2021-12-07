using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x02000045 RID: 69
	public abstract class MemberBinding
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x00013B2C File Offset: 0x00011D2C
		protected MemberBinding(MemberBindingType binding_type, MemberInfo member)
		{
			this.binding_type = binding_type;
			this.member = member;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00013B44 File Offset: 0x00011D44
		public MemberBindingType BindingType
		{
			get
			{
				return this.binding_type;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00013B4C File Offset: 0x00011D4C
		public MemberInfo Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00013B54 File Offset: 0x00011D54
		public override string ToString()
		{
			return ExpressionPrinter.ToString(this);
		}

		// Token: 0x06000462 RID: 1122
		internal abstract void Emit(EmitContext ec, LocalBuilder local);

		// Token: 0x06000463 RID: 1123 RVA: 0x00013B5C File Offset: 0x00011D5C
		internal LocalBuilder EmitLoadMember(EmitContext ec, LocalBuilder local)
		{
			ec.EmitLoadSubject(local);
			return this.member.OnFieldOrProperty((FieldInfo field) => this.EmitLoadField(ec, field), (PropertyInfo prop) => this.EmitLoadProperty(ec, prop));
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00013BAC File Offset: 0x00011DAC
		private LocalBuilder EmitLoadProperty(EmitContext ec, PropertyInfo property)
		{
			MethodInfo getMethod = property.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new NotSupportedException();
			}
			LocalBuilder localBuilder = ec.ig.DeclareLocal(property.PropertyType);
			ec.EmitCall(getMethod);
			ec.ig.Emit(OpCodes.Stloc, localBuilder);
			return localBuilder;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00013BF8 File Offset: 0x00011DF8
		private LocalBuilder EmitLoadField(EmitContext ec, FieldInfo field)
		{
			LocalBuilder localBuilder = ec.ig.DeclareLocal(field.FieldType);
			ec.ig.Emit(OpCodes.Ldfld, field);
			ec.ig.Emit(OpCodes.Stloc, localBuilder);
			return localBuilder;
		}

		// Token: 0x04000105 RID: 261
		private MemberBindingType binding_type;

		// Token: 0x04000106 RID: 262
		private MemberInfo member;
	}
}
