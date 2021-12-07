using System;

// Token: 0x020004E4 RID: 1252
public class DifficultyCondition : Condition
{
	// Token: 0x060021CD RID: 8653 RVA: 0x00093B88 File Offset: 0x00091D88
	public override bool Validate(IContext context)
	{
		switch (DifficultyController.Instance.Difficulty)
		{
		case DifficultyMode.Easy:
			return this.Easy;
		case DifficultyMode.Normal:
			return this.Normal;
		case DifficultyMode.Hard:
			return this.Hard;
		case DifficultyMode.OneLife:
			return this.OneLife;
		default:
			return false;
		}
	}

	// Token: 0x04001C61 RID: 7265
	public bool Easy;

	// Token: 0x04001C62 RID: 7266
	public bool Normal;

	// Token: 0x04001C63 RID: 7267
	public bool Hard;

	// Token: 0x04001C64 RID: 7268
	public bool OneLife;
}
