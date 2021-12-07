using System;
using Game;

// Token: 0x02000340 RID: 832
[Category("Sein")]
public class ShowSeinUIAction : ActionMethod
{
	// Token: 0x060017DF RID: 6111 RVA: 0x000665A9 File Offset: 0x000647A9
	public override void Perform(IContext context)
	{
		if (UI.SeinUI)
		{
			UI.SeinUI.ShowUI = this.ShouldShow;
		}
	}

	// Token: 0x060017E0 RID: 6112 RVA: 0x000665CA File Offset: 0x000647CA
	public override string GetNiceName()
	{
		return (!this.ShouldShow) ? "Hide UI" : "Show UI";
	}

	// Token: 0x04001495 RID: 5269
	public bool ShouldShow = true;
}
