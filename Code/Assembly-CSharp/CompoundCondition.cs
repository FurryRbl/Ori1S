using System;
using System.Collections.Generic;

// Token: 0x02000280 RID: 640
public class CompoundCondition : Condition
{
	// Token: 0x06001526 RID: 5414 RVA: 0x0005E4C4 File Offset: 0x0005C6C4
	public override bool Validate(IContext context)
	{
		foreach (CompoundCondition.ConditionInformation conditionInformation in this.Tests)
		{
			bool flag = true;
			if (conditionInformation.Conditions.Count > 0)
			{
				foreach (Condition condition in conditionInformation.Conditions)
				{
					if (!condition.Validate(context))
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0400124F RID: 4687
	public List<CompoundCondition.ConditionInformation> Tests = new List<CompoundCondition.ConditionInformation>();

	// Token: 0x02000281 RID: 641
	[Serializable]
	public class ConditionInformation
	{
		// Token: 0x04001250 RID: 4688
		public List<Condition> Conditions = new List<Condition>();
	}
}
