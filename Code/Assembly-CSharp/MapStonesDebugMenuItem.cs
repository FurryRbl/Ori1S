using System;
using Core;
using Game;

// Token: 0x020004B3 RID: 1203
internal class MapStonesDebugMenuItem : FloatDebugMenuItem
{
	// Token: 0x060020AF RID: 8367 RVA: 0x0008F158 File Offset: 0x0008D358
	public MapStonesDebugMenuItem(string str)
	{
		if (Characters.Sein == null)
		{
			this.HelpText = "There is no sein";
			this.Text = str;
			base.Value = 0f;
			return;
		}
		this.HelpText = "Press LT \\ RT to add or remove map stones";
		this.Text = str;
		if (Characters.Sein.Inventory)
		{
			base.Value = (float)Characters.Sein.Inventory.MapStones;
		}
	}

	// Token: 0x060020B0 RID: 8368 RVA: 0x0008F1D8 File Offset: 0x0008D3D8
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
			Characters.Sein.Inventory.SpendMapstone(1);
		}
		if (Input.Glide.OnPressed)
		{
			Characters.Sein.Inventory.CollectMapstone(1);
		}
		base.Value = (float)Characters.Sein.Inventory.MapStones;
	}
}
