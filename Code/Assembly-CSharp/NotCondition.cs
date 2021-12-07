using System;

// Token: 0x02000556 RID: 1366
public class NotCondition : Condition
{
	// Token: 0x060023A9 RID: 9129 RVA: 0x0009C29E File Offset: 0x0009A49E
	public override bool Validate(IContext context)
	{
		return !this.Condition.Validate(context);
	}

	// Token: 0x04001DE6 RID: 7654
	public Condition Condition;
}
