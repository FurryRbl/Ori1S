using System;
using Core;
using UnityEngine;

// Token: 0x020004AF RID: 1199
internal class GlobalDebugQuadScaleMenuItem : FloatDebugMenuItem
{
	// Token: 0x060020A6 RID: 8358 RVA: 0x0008EC74 File Offset: 0x0008CE74
	public GlobalDebugQuadScaleMenuItem(string str)
	{
		this.HelpText = "Hold LT \\ RT to change global quad scale";
		this.Text = str;
		base.Value = GlobalDebugQuadScaleMenuItem.GlobalQuadScale;
	}

	// Token: 0x060020A8 RID: 8360 RVA: 0x0008ECB0 File Offset: 0x0008CEB0
	public override void OnSelectedFixedUpdate()
	{
		if (Core.Input.LeftShoulder.IsPressed || Core.Input.ChargeJump.IsPressed)
		{
			base.Value -= 0.01f;
			DebugMenuB.ShouldShowOnlySelectedItem = true;
		}
		if (Core.Input.RightShoulder.IsPressed || Core.Input.Glide.IsPressed)
		{
			base.Value += 0.01f;
			DebugMenuB.ShouldShowOnlySelectedItem = true;
		}
		if (Core.Input.SpiritFlame.IsPressed)
		{
			base.Value = 1f;
		}
		Shader.SetGlobalFloat("_GlobalDebugScale", base.Value - 1f);
		GlobalDebugQuadScaleMenuItem.GlobalQuadScale = base.Value;
	}

	// Token: 0x04001BC2 RID: 7106
	private static float GlobalQuadScale = 1f;
}
