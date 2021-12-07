using System;

// Token: 0x02000294 RID: 660
public class IsUsingAzertyKeyboardCondition : Condition
{
	// Token: 0x06001562 RID: 5474 RVA: 0x0005F0B3 File Offset: 0x0005D2B3
	public override bool Validate(IContext context)
	{
		return GameSettings.Instance.KeyboardLayout == KeyboardLayout.AZERTY;
	}
}
