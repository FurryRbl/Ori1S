using System;

// Token: 0x02000265 RID: 613
public class RunActionCondition : ActionMethod
{
	// Token: 0x060014A3 RID: 5283 RVA: 0x0005D2C0 File Offset: 0x0005B4C0
	public override void Perform(IContext context)
	{
		if (this.Condition == null || this.Condition.Validate(context))
		{
			if (this.Action && (!this.TriggerActionOnce || (this.TriggerActionOnce && !this.m_actionTriggered)))
			{
				this.Action.Perform(context);
				this.m_actionTriggered = true;
			}
		}
		else if (this.ElseAction && (!this.TriggerElseActionOnce || (this.TriggerElseActionOnce && !this.m_elseActionTriggered)))
		{
			this.ElseAction.Perform(context);
			this.m_elseActionTriggered = true;
		}
	}

	// Token: 0x060014A4 RID: 5284 RVA: 0x0005D37C File Offset: 0x0005B57C
	public override string GetNiceName()
	{
		if (this.Condition == null)
		{
			return "Run " + ActionHelper.GetName(this.Action) + " action";
		}
		return "Run " + ActionHelper.GetName(this.Action) + " action if " + ActionHelper.GetName(this.Condition);
	}

	// Token: 0x060014A5 RID: 5285 RVA: 0x0005D3DA File Offset: 0x0005B5DA
	public override void Serialize(Archive ar)
	{
		base.Serialize(ar);
		this.m_actionTriggered = ar.Serialize(this.m_actionTriggered);
		this.m_elseActionTriggered = ar.Serialize(this.m_elseActionTriggered);
	}

	// Token: 0x040011F5 RID: 4597
	public ActionMethod Action;

	// Token: 0x040011F6 RID: 4598
	public ActionMethod ElseAction;

	// Token: 0x040011F7 RID: 4599
	public Condition Condition;

	// Token: 0x040011F8 RID: 4600
	public bool TriggerActionOnce;

	// Token: 0x040011F9 RID: 4601
	public bool TriggerElseActionOnce;

	// Token: 0x040011FA RID: 4602
	private bool m_actionTriggered;

	// Token: 0x040011FB RID: 4603
	private bool m_elseActionTriggered;
}
