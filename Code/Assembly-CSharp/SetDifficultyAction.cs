using System;

// Token: 0x020004E9 RID: 1257
public class SetDifficultyAction : ActionMethod
{
	// Token: 0x060021DD RID: 8669 RVA: 0x00093DCF File Offset: 0x00091FCF
	public override void Perform(IContext context)
	{
		SaveSlotsUI.Instance.SetDifficulty(this.Difficulty);
	}

	// Token: 0x060021DE RID: 8670 RVA: 0x00093DE1 File Offset: 0x00091FE1
	public override string GetNiceName()
	{
		return "Set difficulty to " + this.Difficulty;
	}

	// Token: 0x04001C6F RID: 7279
	public DifficultyMode Difficulty;
}
