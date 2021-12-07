using System;
using Core;
using Game;

// Token: 0x020004B4 RID: 1204
internal class HealthDebugMenuItem : FloatDebugMenuItem
{
	// Token: 0x060020B1 RID: 8369 RVA: 0x0008F260 File Offset: 0x0008D460
	public HealthDebugMenuItem(string str)
	{
		if (Characters.Sein == null)
		{
			this.HelpText = "There is no sein";
			this.Text = str;
			base.Value = 0f;
			return;
		}
		this.HelpText = "Press LT \\ RT to add or remove Health";
		this.Text = str;
		if (Characters.Sein.Mortality != null && Characters.Sein.Mortality.Health != null)
		{
			base.Value = Characters.Sein.Mortality.Health.Amount;
		}
	}

	// Token: 0x060020B2 RID: 8370 RVA: 0x0008F2F8 File Offset: 0x0008D4F8
	public override void OnSelectedFixedUpdate()
	{
		if (Characters.Sein == null)
		{
			return;
		}
		if (Characters.Sein.Mortality == null)
		{
			return;
		}
		if (Characters.Sein.Mortality.Health == null)
		{
			return;
		}
		if (Input.ChargeJump.OnPressed)
		{
			Characters.Sein.Mortality.Health.LoseHealth(2);
		}
		if (Input.Glide.OnPressed)
		{
			Characters.Sein.Mortality.Health.GainHealth(2);
		}
		if (Characters.Sein.Mortality != null && Characters.Sein.Mortality.Health != null)
		{
			base.Value = Characters.Sein.Mortality.Health.Amount;
		}
	}
}
