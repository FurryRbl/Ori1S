using System;

// Token: 0x020002A0 RID: 672
public class SaveFileExistsCondition : Condition
{
	// Token: 0x0600159B RID: 5531 RVA: 0x0005FF18 File Offset: 0x0005E118
	public override bool Validate(IContext context)
	{
		return GameController.Instance.SaveGameController.SaveFileExists;
	}
}
