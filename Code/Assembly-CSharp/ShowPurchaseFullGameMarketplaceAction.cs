using System;

// Token: 0x020008BD RID: 2237
public class ShowPurchaseFullGameMarketplaceAction : ActionMethod
{
	// Token: 0x060031C0 RID: 12736 RVA: 0x000D361C File Offset: 0x000D181C
	public override void Perform(IContext context)
	{
		XboxMarketplace.ShowPurchaseFullGameOffer();
	}
}
