using System;

// Token: 0x020009DD RID: 2525
public class MistyWoodsKuroHideZoneAction : ActionMethod
{
	// Token: 0x060036F4 RID: 14068 RVA: 0x000E691E File Offset: 0x000E4B1E
	public override void Perform(IContext context)
	{
		if (this.HideZone)
		{
			this.HideZone.Active = this.Activate;
		}
	}

	// Token: 0x040031DC RID: 12764
	[NotNull]
	public MistyWoodsKuroGameplayHideZone HideZone;

	// Token: 0x040031DD RID: 12765
	public bool Activate = true;
}
