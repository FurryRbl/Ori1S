using System;
using Game;

// Token: 0x02000113 RID: 275
public class CloseOptionScreenAction : ActionMethod
{
	// Token: 0x06000ABF RID: 2751 RVA: 0x0002EC76 File Offset: 0x0002CE76
	public override void Perform(IContext context)
	{
		UI.Menu.HideMenuScreen(false);
	}
}
