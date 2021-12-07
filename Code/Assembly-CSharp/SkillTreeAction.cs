using System;
using Game;

// Token: 0x0200092B RID: 2347
public class SkillTreeAction : ActionMethod
{
	// Token: 0x060033F4 RID: 13300 RVA: 0x000DA708 File Offset: 0x000D8908
	public override void Perform(IContext context)
	{
		UI.Menu.ShowMenuScreen(false);
	}
}
