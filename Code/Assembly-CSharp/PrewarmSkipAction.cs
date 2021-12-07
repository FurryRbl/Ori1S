using System;
using Game;

// Token: 0x02000317 RID: 791
public class PrewarmSkipAction : ActionMethod
{
	// Token: 0x0600175A RID: 5978 RVA: 0x00064D87 File Offset: 0x00062F87
	public override void Perform(IContext context)
	{
		SkipCutsceneController.Instance.PrewarmSkip();
		UI.Menu.LockClosingMenu = true;
	}
}
