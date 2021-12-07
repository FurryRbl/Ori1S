using System;
using Game;

// Token: 0x02000896 RID: 2198
public class ShowOptionsAction : ActionMethod
{
	// Token: 0x06003153 RID: 12627 RVA: 0x000D1F1F File Offset: 0x000D011F
	public override void Perform(IContext context)
	{
		UI.Menu.ShowOptions();
	}
}
