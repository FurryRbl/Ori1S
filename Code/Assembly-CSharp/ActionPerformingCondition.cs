using System;

// Token: 0x02000277 RID: 631
public class ActionPerformingCondition : Condition
{
	// Token: 0x060014FC RID: 5372 RVA: 0x0005E174 File Offset: 0x0005C374
	public override bool Validate(IContext context)
	{
		return this.Action.IsPerforming ^ !this.IsPerforming;
	}

	// Token: 0x04001238 RID: 4664
	public PerformingAction Action;

	// Token: 0x04001239 RID: 4665
	public bool IsPerforming;
}
