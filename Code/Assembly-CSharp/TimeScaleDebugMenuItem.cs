using System;
using Core;
using UnityEngine;

// Token: 0x020004AD RID: 1197
internal class TimeScaleDebugMenuItem : FloatDebugMenuItem
{
	// Token: 0x060020A2 RID: 8354 RVA: 0x0008E990 File Offset: 0x0008CB90
	public TimeScaleDebugMenuItem(string str)
	{
		this.HelpText = "Press LT \\ RT to control Time Scale";
		this.Text = str;
		base.Value = Time.timeScale;
	}

	// Token: 0x060020A3 RID: 8355 RVA: 0x0008E9C0 File Offset: 0x0008CBC0
	public override void OnSelectedFixedUpdate()
	{
		if (Core.Input.ChargeJump.OnPressed)
		{
			if (base.Value <= 1f)
			{
				base.Value -= 0.1f;
			}
			else
			{
				base.Value -= 0.5f;
			}
		}
		if (Core.Input.Glide.OnPressed)
		{
			if (base.Value <= 1f)
			{
				base.Value += 0.1f;
			}
			else
			{
				base.Value += 0.5f;
			}
		}
		base.Value = Mathf.Min(Mathf.Max(0.1f, base.Value), 99f);
		Time.timeScale = base.Value;
	}
}
