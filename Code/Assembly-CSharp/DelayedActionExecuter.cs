using System;

// Token: 0x0200025A RID: 602
public class DelayedActionExecuter : TimedActionExecuter
{
	// Token: 0x17000395 RID: 917
	// (get) Token: 0x06001439 RID: 5177 RVA: 0x0005BE60 File Offset: 0x0005A060
	// (set) Token: 0x06001438 RID: 5176 RVA: 0x0005BE57 File Offset: 0x0005A057
	public override float StartTime
	{
		get
		{
			return this.Delay;
		}
		set
		{
			this.Delay = value;
		}
	}

	// Token: 0x0600143A RID: 5178 RVA: 0x0005BE68 File Offset: 0x0005A068
	public override void ExecuteAction(IContext context)
	{
		if (this.ActionMethod.enabled)
		{
			this.ActionMethod.Perform(context);
		}
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x0005BE94 File Offset: 0x0005A094
	public override void StopAction()
	{
		if (this.ActionWithDuration && this.ActionWithDuration.enabled)
		{
			this.ActionWithDuration.Stop();
		}
	}

	// Token: 0x17000396 RID: 918
	// (get) Token: 0x0600143C RID: 5180 RVA: 0x0005BECC File Offset: 0x0005A0CC
	public override ActionWithDuration ActionWithDuration
	{
		get
		{
			if (this.m_action == null)
			{
				this.m_action = base.GetComponent<ActionWithDuration>();
			}
			return this.m_action as ActionWithDuration;
		}
	}

	// Token: 0x17000397 RID: 919
	// (get) Token: 0x0600143D RID: 5181 RVA: 0x0005BF01 File Offset: 0x0005A101
	public override ActionMethod ActionMethod
	{
		get
		{
			if (this.m_action == null)
			{
				this.m_action = base.GetComponent<ActionMethod>();
			}
			return this.m_action;
		}
	}

	// Token: 0x040011BE RID: 4542
	public float Delay;

	// Token: 0x040011BF RID: 4543
	private ActionMethod m_action;
}
