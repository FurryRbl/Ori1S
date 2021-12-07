using System;

// Token: 0x020004E7 RID: 1255
public class DifficultyUnlockedCondition : Condition
{
	// Token: 0x060021D9 RID: 8665 RVA: 0x00093D79 File Offset: 0x00091F79
	public override bool Validate(IContext context)
	{
		return true;
	}

	// Token: 0x04001C6E RID: 7278
	public DifficultyMode Difficulty;
}
