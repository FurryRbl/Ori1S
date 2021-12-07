using System;

// Token: 0x020004E1 RID: 1249
public class CanChangeDifficultyCondition : Condition
{
	// Token: 0x060021C2 RID: 8642 RVA: 0x000939F2 File Offset: 0x00091BF2
	public override bool Validate(IContext context)
	{
		return DifficultyController.Instance.Difficulty != DifficultyMode.OneLife;
	}
}
