using System;

namespace fsm
{
	// Token: 0x0200055F RID: 1375
	public class FuncCondition : ICondition
	{
		// Token: 0x060023D0 RID: 9168 RVA: 0x0009C9FC File Offset: 0x0009ABFC
		public FuncCondition(Func<bool> function)
		{
			this.m_function = function;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x0009CA0B File Offset: 0x0009AC0B
		public bool Validate(IContext context)
		{
			return this.m_function == null || this.m_function();
		}

		// Token: 0x04001E02 RID: 7682
		private readonly Func<bool> m_function;
	}
}
