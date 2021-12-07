using System;
using Core;
using Game;

// Token: 0x020004D8 RID: 1240
internal class SplitFlameMenuItem : FloatDebugMenuItem
{
	// Token: 0x0600218B RID: 8587 RVA: 0x00093078 File Offset: 0x00091278
	public SplitFlameMenuItem(string str)
	{
		this.HelpText = "Press LT \\ RT to add or subtract max number of targets";
		this.Text = str;
		base.Value = (float)Characters.Sein.PlayerAbilities.SplitFlameTargets;
	}

	// Token: 0x0600218C RID: 8588 RVA: 0x000930B4 File Offset: 0x000912B4
	public override void OnSelectedFixedUpdate()
	{
		if (Characters.Sein.Abilities.SpiritFlame == null)
		{
			return;
		}
		if (Input.ChargeJump.OnPressed)
		{
		}
		if (Input.Glide.OnPressed)
		{
		}
		base.Value = (float)Characters.Sein.Abilities.StandardSpiritFlame.MaxTargets;
	}
}
