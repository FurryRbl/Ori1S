using System;

// Token: 0x020009C9 RID: 2505
public class ActivateWindVentAction : ActionMethod
{
	// Token: 0x0600369D RID: 13981 RVA: 0x000E572B File Offset: 0x000E392B
	public override void Perform(IContext context)
	{
		this.WindVent.StartAnticipation();
	}

	// Token: 0x04003181 RID: 12673
	public WindVent WindVent;
}
