using System;

// Token: 0x020002A6 RID: 678
public class WasLastInputKeyboardCondition : Condition
{
	// Token: 0x060015A6 RID: 5542 RVA: 0x00060027 File Offset: 0x0005E227
	public override bool Validate(IContext context)
	{
		return PlayerInput.Instance.WasKeyboardUsedLast;
	}
}
