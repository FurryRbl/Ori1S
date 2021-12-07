using System;

// Token: 0x0200011C RID: 284
public class CutsceneUnlockedCondition : Condition
{
	// Token: 0x06000B17 RID: 2839 RVA: 0x000303B2 File Offset: 0x0002E5B2
	public override bool Validate(IContext context)
	{
		return GameSettings.Instance && GameSettings.Instance.CutsceneUnlocked(this.Cutscene);
	}

	// Token: 0x04000904 RID: 2308
	public UnlockedCutscenes Cutscene;
}
