using System;
using System.Collections.Generic;

namespace fsm
{
	// Token: 0x02000564 RID: 1380
	public class AllConditions : ICondition
	{
		// Token: 0x060023E5 RID: 9189 RVA: 0x0009CC76 File Offset: 0x0009AE76
		public AllConditions(params ICondition[] conditions)
		{
			this.m_conditions = conditions;
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x0009CC88 File Offset: 0x0009AE88
		public AllConditions(params Func<bool>[] conditions)
		{
			ICondition[] array = new FuncCondition[conditions.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new FuncCondition(conditions[i]);
			}
			this.m_conditions = array;
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x0009CCCC File Offset: 0x0009AECC
		public bool Validate(IContext context)
		{
			foreach (ICondition condition in this.m_conditions)
			{
				if (!condition.Validate(context))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001E12 RID: 7698
		private readonly IEnumerable<ICondition> m_conditions;
	}
}
