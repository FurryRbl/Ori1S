using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x0200049D RID: 1181
	internal class BalancingGroup : CapturingGroup
	{
		// Token: 0x06002A7C RID: 10876 RVA: 0x000926C8 File Offset: 0x000908C8
		public BalancingGroup()
		{
			this.balance = null;
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x000926D8 File Offset: 0x000908D8
		// (set) Token: 0x06002A7E RID: 10878 RVA: 0x000926E0 File Offset: 0x000908E0
		public CapturingGroup Balance
		{
			get
			{
				return this.balance;
			}
			set
			{
				this.balance = value;
			}
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000926EC File Offset: 0x000908EC
		public override void Compile(ICompiler cmp, bool reverse)
		{
			LinkRef linkRef = cmp.NewLink();
			cmp.EmitBalanceStart(base.Index, this.balance.Index, base.IsNamed, linkRef);
			int count = base.Expressions.Count;
			for (int i = 0; i < count; i++)
			{
				Expression expression;
				if (reverse)
				{
					expression = base.Expressions[count - i - 1];
				}
				else
				{
					expression = base.Expressions[i];
				}
				expression.Compile(cmp, reverse);
			}
			cmp.EmitBalance();
			cmp.ResolveLink(linkRef);
		}

		// Token: 0x04001B00 RID: 6912
		private CapturingGroup balance;
	}
}
