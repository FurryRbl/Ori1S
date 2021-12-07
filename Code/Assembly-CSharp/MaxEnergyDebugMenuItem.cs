using System;
using Core;
using Game;

// Token: 0x020004B7 RID: 1207
internal class MaxEnergyDebugMenuItem : FloatDebugMenuItem
{
	// Token: 0x060020B7 RID: 8375 RVA: 0x0008F604 File Offset: 0x0008D804
	public MaxEnergyDebugMenuItem(string str)
	{
		if (Characters.Sein == null)
		{
			this.HelpText = "There is no sein";
			this.Text = str;
			base.Value = 0f;
			return;
		}
		this.HelpText = "Press LT \\ RT to add or remove Max Energy";
		this.Text = str;
		if (Characters.Sein.Energy)
		{
			base.Value = Characters.Sein.Energy.Max;
		}
	}

	// Token: 0x060020B8 RID: 8376 RVA: 0x0008F680 File Offset: 0x0008D880
	public override void OnSelectedFixedUpdate()
	{
		if (Input.ChargeJump.OnPressed)
		{
			Characters.Sein.Energy.Max -= 1f;
			if (Characters.Sein.Energy.Max <= 1f)
			{
				Characters.Sein.Energy.Max = 1f;
			}
		}
		if (Input.Glide.OnPressed)
		{
			Characters.Sein.Energy.Max += 1f;
		}
		if (Characters.Sein.Energy)
		{
			base.Value = Characters.Sein.Energy.Max;
		}
	}
}
