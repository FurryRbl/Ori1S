using System;
using Game;

// Token: 0x02000897 RID: 2199
public class ShowWorldMapAction : ActionMethod
{
	// Token: 0x06003155 RID: 12629 RVA: 0x000D1F33 File Offset: 0x000D0133
	public override void Perform(IContext context)
	{
		UI.Menu.ShowAreaMap();
	}
}
