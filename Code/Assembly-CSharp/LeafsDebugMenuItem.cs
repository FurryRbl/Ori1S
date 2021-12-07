using System;
using Core;
using Game;

// Token: 0x020004B2 RID: 1202
internal class LeafsDebugMenuItem : FloatDebugMenuItem
{
	// Token: 0x060020AD RID: 8365 RVA: 0x0008F050 File Offset: 0x0008D250
	public LeafsDebugMenuItem(string str)
	{
		if (Characters.Sein == null)
		{
			this.HelpText = "There is no sein";
			this.Text = str;
			base.Value = 0f;
			return;
		}
		this.HelpText = "Press LT \\ RT to add or remove leafs";
		this.Text = str;
		if (Characters.Sein.Inventory)
		{
			base.Value = (float)Characters.Sein.Inventory.Keystones;
		}
	}

	// Token: 0x060020AE RID: 8366 RVA: 0x0008F0D0 File Offset: 0x0008D2D0
	public override void OnSelectedFixedUpdate()
	{
		if (Characters.Sein == null)
		{
			return;
		}
		if (Characters.Sein.Inventory == null)
		{
			return;
		}
		if (Input.ChargeJump.OnPressed)
		{
			Characters.Sein.Inventory.SpendKeystones(1);
		}
		if (Input.Glide.OnPressed)
		{
			Characters.Sein.Inventory.CollectKeystones(1);
		}
		base.Value = (float)Characters.Sein.Inventory.Keystones;
	}
}
