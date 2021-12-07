using System;

// Token: 0x02000255 RID: 597
public class FuncAction : IAction
{
	// Token: 0x06001427 RID: 5159 RVA: 0x0005BCCC File Offset: 0x00059ECC
	public FuncAction(Action action)
	{
		this.Action = action;
	}

	// Token: 0x06001428 RID: 5160 RVA: 0x0005BCDB File Offset: 0x00059EDB
	public void Perform(IContext context)
	{
		if (this.Action != null)
		{
			this.Action();
		}
	}

	// Token: 0x040011B9 RID: 4537
	public Action Action;
}
