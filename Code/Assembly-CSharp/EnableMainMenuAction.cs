using System;

// Token: 0x020002E0 RID: 736
public class EnableMainMenuAction : ActionMethod
{
	// Token: 0x06001674 RID: 5748 RVA: 0x00062BE1 File Offset: 0x00060DE1
	public override void Perform(IContext context)
	{
		GameController.Instance.MainMenuCanBeOpened = this.Enabled;
	}

	// Token: 0x04001363 RID: 4963
	public bool Enabled = true;
}
