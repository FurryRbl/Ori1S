using System;

// Token: 0x0200027A RID: 634
public class AnySaveSlotsFilledCondition : Condition
{
	// Token: 0x06001501 RID: 5377 RVA: 0x0005E318 File Offset: 0x0005C518
	public override bool Validate(IContext context)
	{
		return SaveSlotsManager.Instance.AnySaveSlotsExist;
	}
}
