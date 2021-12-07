using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020004B1 RID: 1201
internal class SeinSkillUpDownDebugMenuItem : FloatDebugMenuItem
{
	// Token: 0x060020AB RID: 8363 RVA: 0x0008EED8 File Offset: 0x0008D0D8
	public SeinSkillUpDownDebugMenuItem(string str)
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
		if (Characters.Sein.Level)
		{
			base.Value = (float)Characters.Sein.Level.SkillPoints;
		}
	}

	// Token: 0x060020AC RID: 8364 RVA: 0x0008EF58 File Offset: 0x0008D158
	public override void OnSelectedFixedUpdate()
	{
		if (Characters.Sein == null)
		{
			return;
		}
		if (Characters.Sein.Level == null)
		{
			return;
		}
		if (Characters.Sein.Abilities.SpiritFlame == null)
		{
			return;
		}
		if (Core.Input.ChargeJump.OnPressed)
		{
			Characters.Sein.Level.SkillPoints--;
			Characters.Sein.Level.Experience = 0;
		}
		if (Core.Input.Glide.OnPressed)
		{
			Characters.Sein.Level.SkillPoints++;
			Characters.Sein.Level.Experience = 0;
		}
		Characters.Sein.Level.SkillPoints = Mathf.Max(Characters.Sein.Level.SkillPoints, 0);
		base.Value = (float)Characters.Sein.Level.SkillPoints;
	}
}
