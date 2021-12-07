using System;
using Core;
using UnityEngine;

// Token: 0x020009C8 RID: 2504
public class FallCutsceneCameraOffsetController : MonoBehaviour
{
	// Token: 0x0600369B RID: 13979 RVA: 0x000E565C File Offset: 0x000E385C
	private void FixedUpdate()
	{
		if (Core.Input.Up.IsPressed)
		{
			CameraOffsetZone cameraOffsetZone = this.CameraOffsetZone;
			cameraOffsetZone.Offset.y = cameraOffsetZone.Offset.y - this.PressUpSpeed * Time.fixedDeltaTime;
			this.CameraOffsetZone.Offset.y = Mathf.Max(this.CameraOffsetZone.Offset.y, this.MinOffset);
		}
		if (Core.Input.Down.IsPressed)
		{
			CameraOffsetZone cameraOffsetZone2 = this.CameraOffsetZone;
			cameraOffsetZone2.Offset.y = cameraOffsetZone2.Offset.y + this.PressDownSpeed * Time.fixedDeltaTime;
			this.CameraOffsetZone.Offset.y = Mathf.Min(this.CameraOffsetZone.Offset.y, this.MaxOffset);
		}
	}

	// Token: 0x0400317C RID: 12668
	public float PressUpSpeed = 3f;

	// Token: 0x0400317D RID: 12669
	public float PressDownSpeed = 3f;

	// Token: 0x0400317E RID: 12670
	public float MaxOffset = 10f;

	// Token: 0x0400317F RID: 12671
	public float MinOffset = -10f;

	// Token: 0x04003180 RID: 12672
	public CameraOffsetZone CameraOffsetZone;
}
