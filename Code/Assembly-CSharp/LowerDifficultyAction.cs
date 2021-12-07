using System;

// Token: 0x020004E8 RID: 1256
public class LowerDifficultyAction : ActionMethod
{
	// Token: 0x060021DB RID: 8667 RVA: 0x00093D84 File Offset: 0x00091F84
	public override void Perform(IContext context)
	{
		DifficultyMode difficulty = DifficultyController.Instance.Difficulty;
		if (difficulty == DifficultyMode.Normal)
		{
			DifficultyController.Instance.ChangeDifficulty(DifficultyMode.Easy);
			return;
		}
		if (difficulty != DifficultyMode.Hard)
		{
			return;
		}
		DifficultyController.Instance.ChangeDifficulty(DifficultyMode.Normal);
	}
}
