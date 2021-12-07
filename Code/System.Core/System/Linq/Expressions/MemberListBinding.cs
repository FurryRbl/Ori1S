using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	// Token: 0x02000049 RID: 73
	public sealed class MemberListBinding : MemberBinding
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x00013D8C File Offset: 0x00011F8C
		internal MemberListBinding(MemberInfo member, ReadOnlyCollection<ElementInit> initializers) : base(MemberBindingType.ListBinding, member)
		{
			this.initializers = initializers;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00013DA0 File Offset: 0x00011FA0
		public ReadOnlyCollection<ElementInit> Initializers
		{
			get
			{
				return this.initializers;
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00013DA8 File Offset: 0x00011FA8
		internal override void Emit(EmitContext ec, LocalBuilder local)
		{
			LocalBuilder local2 = base.EmitLoadMember(ec, local);
			foreach (ElementInit elementInit in this.initializers)
			{
				elementInit.Emit(ec, local2);
			}
		}

		// Token: 0x0400010F RID: 271
		private ReadOnlyCollection<ElementInit> initializers;
	}
}
