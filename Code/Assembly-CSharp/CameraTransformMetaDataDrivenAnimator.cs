using System;
using Game;
using UnityEngine;

// Token: 0x020001FF RID: 511
public class CameraTransformMetaDataDrivenAnimator : LegacyAnimator
{
	// Token: 0x060011C0 RID: 4544 RVA: 0x00051CEC File Offset: 0x0004FEEC
	public int GetFrameAtTime(float index)
	{
		int count = this.AnimationMetaData.CameraData.PositionX.Values.Count;
		if (count == 0)
		{
			return 0;
		}
		return Mathf.FloorToInt(Mathf.Clamp(index, 0f, (float)(count - 1)));
	}

	// Token: 0x060011C1 RID: 4545 RVA: 0x00051D34 File Offset: 0x0004FF34
	protected override void AnimateIt(float value)
	{
		if (base.CurrentTime >= this.AnimationMetaData.Camera.PositionX.Duration)
		{
			base.Stop();
		}
		Vector3 deltaPositionAtTime = this.AnimationMetaData.Camera.GetDeltaPositionAtTime(base.CurrentTime);
		UI.Cameras.Current.OffsetController.AdditiveDefaultOffset += new Vector3(deltaPositionAtTime.x, deltaPositionAtTime.y, deltaPositionAtTime.z);
	}

	// Token: 0x060011C2 RID: 4546 RVA: 0x00051DB2 File Offset: 0x0004FFB2
	public override void RestoreToOriginalState()
	{
	}

	// Token: 0x04000F4B RID: 3915
	public AnimationMetaData AnimationMetaData;
}
