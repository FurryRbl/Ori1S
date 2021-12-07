using System;

// Token: 0x020002FD RID: 765
[Category("Sein")]
public class LockPlayerInputManualAction : ActionMethod
{
	// Token: 0x060016DE RID: 5854 RVA: 0x00063A84 File Offset: 0x00061C84
	public override void Perform(IContext context)
	{
		GameController.Instance.LockInputByAction = this.ShouldLock;
	}

	// Token: 0x060016DF RID: 5855 RVA: 0x00063A96 File Offset: 0x00061C96
	public override string GetNiceName()
	{
		return (!this.ShouldLock) ? "Unlock player input" : "Lock player input";
	}

	// Token: 0x040013AF RID: 5039
	public bool ShouldLock = true;
}
