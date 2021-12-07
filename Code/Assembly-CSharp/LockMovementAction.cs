using System;
using Game;

// Token: 0x020002FA RID: 762
public class LockMovementAction : ActionMethod
{
	// Token: 0x060016CE RID: 5838 RVA: 0x000638F8 File Offset: 0x00061AF8
	public override void Perform(IContext context)
	{
		Characters.Sein.Controller.LockMovementInput = this.ShouldLock;
	}

	// Token: 0x060016CF RID: 5839 RVA: 0x0006390F File Offset: 0x00061B0F
	public override string GetNiceName()
	{
		return (!this.ShouldLock) ? "Unlock movement input" : "Lock movement input";
	}

	// Token: 0x040013A9 RID: 5033
	public bool ShouldLock = true;
}
