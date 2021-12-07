using System;
using System.Collections.Generic;

namespace fsm
{
	// Token: 0x02000565 RID: 1381
	public class AnyConditions : ICondition
	{
		// Token: 0x060023E8 RID: 9192 RVA: 0x0009CD34 File Offset: 0x0009AF34
		public AnyConditions(params ICondition[] conditions)
		{
			this.m_conditions = conditions;
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x0009CD44 File Offset: 0x0009AF44
		public bool Validate(IContext context)
		{
			foreach (ICondition condition in this.m_conditions)
			{
				if (condition.Validate(context))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001E13 RID: 7699
		private readonly IEnumerable<ICondition> m_conditions;
	}
}
