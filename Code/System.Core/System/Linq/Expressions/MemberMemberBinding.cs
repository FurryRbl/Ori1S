using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x0200004A RID: 74
	public sealed class MemberMemberBinding : MemberBinding
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x00013E18 File Offset: 0x00012018
		internal MemberMemberBinding(MemberInfo member, ReadOnlyCollection<MemberBinding> bindings) : base(MemberBindingType.MemberBinding, member)
		{
			this.bindings = bindings;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00013E2C File Offset: 0x0001202C
		public ReadOnlyCollection<MemberBinding> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00013E34 File Offset: 0x00012034
		internal override void Emit(EmitContext ec, LocalBuilder local)
		{
			LocalBuilder local2 = base.EmitLoadMember(ec, local);
			foreach (MemberBinding memberBinding in this.bindings)
			{
				memberBinding.Emit(ec, local2);
			}
		}

		// Token: 0x04000110 RID: 272
		private ReadOnlyCollection<MemberBinding> bindings;
	}
}
