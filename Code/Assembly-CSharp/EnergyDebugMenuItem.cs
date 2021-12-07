using System;
using Core;
using Game;

// Token: 0x020004B6 RID: 1206
internal class EnergyDebugMenuItem : FloatDebugMenuItem
{
	// Token: 0x060020B5 RID: 8373 RVA: 0x0008F50C File Offset: 0x0008D70C
	public EnergyDebugMenuItem(string str)
	{
		if (Characters.Sein == null)
		{
			this.HelpText = "There is no sein";
			this.Text = str;
			base.Value = 0f;
			return;
		}
		this.HelpText = "Press LT \\ RT to add or remove Energy";
		this.Text = str;
		if (Characters.Sein.Energy)
		{
			base.Value = Characters.Sein.Energy.Current;
		}
	}

	// Token: 0x060020B6 RID: 8374 RVA: 0x0008F588 File Offset: 0x0008D788
	public override void OnSelectedFixedUpdate()
	{
		if (Input.ChargeJump.OnPressed)
		{
			Characters.Sein.Energy.Spend(1f);
		}
		if (Input.Glide.OnPressed)
		{
			Characters.Sein.Energy.Gain(1f);
		}
		if (Characters.Sein.Energy)
		{
			base.Value = Characters.Sein.Energy.Current;
		}
	}
}
