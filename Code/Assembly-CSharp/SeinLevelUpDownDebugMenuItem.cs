using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020004B0 RID: 1200
internal class SeinLevelUpDownDebugMenuItem : FloatDebugMenuItem
{
	// Token: 0x060020A9 RID: 8361 RVA: 0x0008ED64 File Offset: 0x0008CF64
	public SeinLevelUpDownDebugMenuItem(string str)
	{
		if (Characters.Sein == null)
		{
			this.HelpText = "There is no sein";
			this.Text = str;
			base.Value = 0f;
			return;
		}
		this.HelpText = "Press LT \\ RT to add or subtract spirit flame level";
		this.Text = str;
		if (Characters.Sein.Abilities.SpiritFlame)
		{
			base.Value = (float)Characters.Sein.Level.Current;
		}
		else
		{
			base.Value = 0f;
		}
	}

	// Token: 0x060020AA RID: 8362 RVA: 0x0008EDF8 File Offset: 0x0008CFF8
	public override void OnSelectedFixedUpdate()
	{
		if (Characters.Sein == null)
		{
			return;
		}
		if (Characters.Sein.Abilities.SpiritFlame == null)
		{
			return;
		}
		if (Core.Input.ChargeJump.OnPressed)
		{
			Characters.Sein.Level.Current--;
			Characters.Sein.Level.Experience = 0;
		}
		if (Core.Input.Glide.OnPressed)
		{
			Characters.Sein.Level.Current++;
			Characters.Sein.Level.Experience = 0;
		}
		Characters.Sein.Level.Current = Mathf.Max(Characters.Sein.Level.Current, 0);
		base.Value = (float)Characters.Sein.Level.Current;
	}
}
