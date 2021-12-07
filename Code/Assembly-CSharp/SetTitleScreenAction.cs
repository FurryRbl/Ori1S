using System;

// Token: 0x020007A4 RID: 1956
public class SetTitleScreenAction : ActionMethod
{
	// Token: 0x06002D60 RID: 11616 RVA: 0x000C1F14 File Offset: 0x000C0114
	public override void Perform(IContext context)
	{
		TitleScreenManager.SetScreen(this.Screen);
	}

	// Token: 0x06002D61 RID: 11617 RVA: 0x000C1F21 File Offset: 0x000C0121
	public override string GetNiceName()
	{
		return "Set Title Screen to " + this.Screen.ToString();
	}

	// Token: 0x040028E9 RID: 10473
	public TitleScreenManager.Screen Screen;
}
