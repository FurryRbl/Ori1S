using System;
using Core;
using Game;

// Token: 0x020004B5 RID: 1205
internal class MaxHealthDebugMenuItem : FloatDebugMenuItem
{
	// Token: 0x060020B3 RID: 8371 RVA: 0x0008F3CC File Offset: 0x0008D5CC
	public MaxHealthDebugMenuItem(string str)
	{
		if (Characters.Sein == null)
		{
			this.HelpText = "There is no sein";
			this.Text = str;
			base.Value = 0f;
			return;
		}
		this.HelpText = "Press LT \\ RT to add or remove Max Health";
		this.Text = str;
		if (Characters.Sein.Mortality != null && Characters.Sein.Mortality.Health != null)
		{
			base.Value = (float)Characters.Sein.Mortality.Health.MaxHealth;
		}
	}

	// Token: 0x060020B4 RID: 8372 RVA: 0x0008F464 File Offset: 0x0008D664
	public override void OnSelectedFixedUpdate()
	{
		if (Input.ChargeJump.OnPressed)
		{
			Characters.Sein.Mortality.Health.MaxHealth -= 4;
		}
		if (Input.Glide.OnPressed)
		{
			Characters.Sein.Mortality.Health.MaxHealth += 4;
		}
		if (Characters.Sein.Mortality != null && Characters.Sein.Mortality.Health != null)
		{
			base.Value = (float)Characters.Sein.Mortality.Health.MaxHealth;
		}
	}
}
