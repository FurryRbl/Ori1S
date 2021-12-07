using System;
using Game;

// Token: 0x02000898 RID: 2200
public class ShowWorldMapObjectiveAction : PerformingAction
{
	// Token: 0x06003157 RID: 12631 RVA: 0x000D1F47 File Offset: 0x000D0147
	public override void Perform(IContext context)
	{
		this.m_isPerforming = true;
		UI.Menu.ShowObjective(this.Objective, new Action(this.OnFinish));
	}

	// Token: 0x06003158 RID: 12632 RVA: 0x000D1F6C File Offset: 0x000D016C
	public override void Stop()
	{
	}

	// Token: 0x170007D9 RID: 2009
	// (get) Token: 0x06003159 RID: 12633 RVA: 0x000D1F6E File Offset: 0x000D016E
	public override bool IsPerforming
	{
		get
		{
			return this.m_isPerforming;
		}
	}

	// Token: 0x0600315A RID: 12634 RVA: 0x000D1F76 File Offset: 0x000D0176
	public void OnFinish()
	{
		this.m_isPerforming = false;
	}

	// Token: 0x04002CA1 RID: 11425
	public Objective Objective;

	// Token: 0x04002CA2 RID: 11426
	private bool m_isPerforming;
}
