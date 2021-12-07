using System;
using Game;

// Token: 0x0200015D RID: 349
public class ShowMainMenuAction : ActionMethod
{
	// Token: 0x06000E18 RID: 3608 RVA: 0x00041980 File Offset: 0x0003FB80
	public override void Perform(IContext context)
	{
		if (this.Show)
		{
			if (this.Immediate)
			{
				UI.Menu.ShowMenuScreen(true);
			}
			else
			{
				UI.Menu.ShowMenuScreen(false);
			}
		}
		else if (this.Immediate)
		{
			UI.Menu.HideMenuScreen(true);
		}
		else
		{
			UI.Menu.HideMenuScreen(false);
		}
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x000419E9 File Offset: 0x0003FBE9
	public override string GetNiceName()
	{
		if (this.Show)
		{
			return "Show main menu";
		}
		return "Hide main menu";
	}

	// Token: 0x04000B67 RID: 2919
	public bool Show;

	// Token: 0x04000B68 RID: 2920
	public bool Immediate;
}
