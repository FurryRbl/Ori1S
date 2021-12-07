using System;

// Token: 0x02000325 RID: 805
public class RunActionOnce : ActionMethod
{
	// Token: 0x06001788 RID: 6024 RVA: 0x000652EF File Offset: 0x000634EF
	public override void Perform(IContext context)
	{
		if (!this.m_hasRun)
		{
			this.Action.Perform(context);
			this.m_hasRun = true;
		}
	}

	// Token: 0x06001789 RID: 6025 RVA: 0x0006530F File Offset: 0x0006350F
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_hasRun);
	}

	// Token: 0x0600178A RID: 6026 RVA: 0x00065320 File Offset: 0x00063520
	public override string GetNiceName()
	{
		return "Run " + ((!(this.Action == null)) ? (this.Action.name + " (" + this.Action.GetType().Name + ") action") : "an action");
	}

	// Token: 0x0400142A RID: 5162
	[NotNull]
	public ActionMethod Action;

	// Token: 0x0400142B RID: 5163
	private bool m_hasRun;
}
