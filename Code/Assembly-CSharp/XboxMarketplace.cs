using System;
using System.Diagnostics;
using ManagedSteam.SteamTypes;

// Token: 0x020008B6 RID: 2230
public class XboxMarketplace
{
	// Token: 0x060031B2 RID: 12722 RVA: 0x000D353C File Offset: 0x000D173C
	public static void ShowPurchaseFullGameOffer()
	{
		if (Steamworks.SteamInterface != null && Steamworks.SteamInterface.Utils.IsOverlayEnabled())
		{
			Steamworks.SteamInterface.Friends.ActivateGameOverlayToStore(Steamworks.FullDEAppID, OverlayToStoreFlag.None);
		}
		else
		{
			Process.Start("http://store.steampowered.com/app/424650/");
		}
	}
}
