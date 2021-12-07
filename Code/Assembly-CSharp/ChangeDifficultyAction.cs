using System;

// Token: 0x020004E2 RID: 1250
public class ChangeDifficultyAction : ActionMethod
{
	// Token: 0x060021C4 RID: 8644 RVA: 0x00093A0F File Offset: 0x00091C0F
	public override void Perform(IContext context)
	{
		ChangeDifficultyScreen.Instance.SetDifficulty(this.Difficulty);
	}

	// Token: 0x060021C5 RID: 8645 RVA: 0x00093A21 File Offset: 0x00091C21
	public override string GetNiceName()
	{
		return "Change difficulty to " + this.Difficulty;
	}

	// Token: 0x04001C58 RID: 7256
	public DifficultyMode Difficulty;
}
