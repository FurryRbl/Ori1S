using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020004AE RID: 1198
internal class ZoomDebugMenuItem : FloatDebugMenuItem
{
	// Token: 0x060020A4 RID: 8356 RVA: 0x0008EA88 File Offset: 0x0008CC88
	public ZoomDebugMenuItem(string str)
	{
		this.HelpText = "Hold LT \\ RT to control Zoom";
		this.Text = str;
		base.Value = UI.Cameras.Current.OffsetController.Offset.z;
	}

	// Token: 0x060020A5 RID: 8357 RVA: 0x0008EACC File Offset: 0x0008CCCC
	public override void OnSelectedFixedUpdate()
	{
		bool flag = false;
		if (Core.Input.ChargeJump.IsPressed || Core.Input.Glide.IsPressed)
		{
			UI.Cameras.Current.OffsetController.AdditiveDefaultOffset = new Vector3(UI.Cameras.Current.OffsetController.AdditiveDefaultOffset.x, UI.Cameras.Current.OffsetController.AdditiveDefaultOffset.y, UI.Cameras.Current.OffsetController.AdditiveDefaultOffset.z + Time.deltaTime * 16f * (float)((!Core.Input.Glide.IsPressed) ? -1 : 1));
			flag = true;
		}
		if (Core.Input.LeftShoulder.OnPressed)
		{
			UI.Cameras.Current.OffsetController.AdditiveDefaultOffset = new Vector3(UI.Cameras.Current.OffsetController.AdditiveDefaultOffset.x, UI.Cameras.Current.OffsetController.AdditiveDefaultOffset.y, (float)Mathf.RoundToInt(UI.Cameras.Current.OffsetController.AdditiveDefaultOffset.z - 1f));
			flag = true;
		}
		if (Core.Input.RightShoulder.OnPressed)
		{
			UI.Cameras.Current.OffsetController.AdditiveDefaultOffset = new Vector3(UI.Cameras.Current.OffsetController.AdditiveDefaultOffset.x, UI.Cameras.Current.OffsetController.AdditiveDefaultOffset.y, (float)Mathf.RoundToInt(UI.Cameras.Current.OffsetController.AdditiveDefaultOffset.z + 1f));
			flag = true;
		}
		base.Value = UI.Cameras.Current.OffsetController.Offset.z;
		if (flag)
		{
			DebugMenuB.ShouldShowOnlySelectedItem = true;
		}
	}
}
