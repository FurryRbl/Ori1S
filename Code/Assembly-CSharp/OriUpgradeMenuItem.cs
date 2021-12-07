using System;
using Core;
using Game;

// Token: 0x020004D5 RID: 1237
internal class OriUpgradeMenuItem : FloatDebugMenuItem
{
	// Token: 0x0600217D RID: 8573 RVA: 0x00092F0C File Offset: 0x0009110C
	public OriUpgradeMenuItem(string str)
	{
		this.HelpText = "Press LT \\ RT to add or subtract max number of targets";
		this.Text = str;
		base.Value = (float)(Characters.Sein.PlayerAbilities.OriStrength + 1);
	}

	// Token: 0x0600217E RID: 8574 RVA: 0x00092F4C File Offset: 0x0009114C
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
		base.Value = (float)(Characters.Sein.PlayerAbilities.OriStrength + 1);
	}
}
