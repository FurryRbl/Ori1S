using System;

// Token: 0x020002B5 RID: 693
[Category("Laser")]
public class ActivateLaserAction : ActionMethod
{
	// Token: 0x060015CD RID: 5581 RVA: 0x00060507 File Offset: 0x0005E707
	public override void Perform(IContext context)
	{
		this.Laser.Activated = this.ShouldActivate;
	}

	// Token: 0x060015CE RID: 5582 RVA: 0x0006051C File Offset: 0x0005E71C
	public override string GetNiceName()
	{
		return ((!this.ShouldActivate) ? "Deactivate laser " : "Activate laser") + ActionHelper.GetName(this.Laser);
	}

	// Token: 0x040012B3 RID: 4787
	[NotNull]
	public BlockableLaser Laser;

	// Token: 0x040012B4 RID: 4788
	public bool ShouldActivate = true;
}
