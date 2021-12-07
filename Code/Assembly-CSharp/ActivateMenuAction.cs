using System;

// Token: 0x020002B7 RID: 695
[Category("General")]
public class ActivateMenuAction : ActionMethod
{
	// Token: 0x060015E4 RID: 5604 RVA: 0x00060F69 File Offset: 0x0005F169
	public override void Perform(IContext context)
	{
		this.Target.IsActive = this.Activate;
		this.Target.IsLocked = !this.Activate;
	}

	// Token: 0x040012C6 RID: 4806
	[NotNull]
	public CleverMenuItemSelectionManager Target;

	// Token: 0x040012C7 RID: 4807
	public bool Activate = true;
}
