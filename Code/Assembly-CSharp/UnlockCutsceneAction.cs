using System;

// Token: 0x0200013C RID: 316
public class UnlockCutsceneAction : ActionMethod
{
	// Token: 0x06000C76 RID: 3190 RVA: 0x00038E70 File Offset: 0x00037070
	public override void Perform(IContext context)
	{
		if (GameSettings.Instance.UnlockedCutscenes < this.Cutscene)
		{
			GameSettings.Instance.UnlockedCutscenes = this.Cutscene;
			GameSettings.Instance.SaveSettings();
			Telemetry.SendSettings();
		}
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x00038EB1 File Offset: 0x000370B1
	public override string GetNiceName()
	{
		return "Unlock " + this.Cutscene + " cutscene";
	}

	// Token: 0x04000A54 RID: 2644
	public UnlockedCutscenes Cutscene;
}
