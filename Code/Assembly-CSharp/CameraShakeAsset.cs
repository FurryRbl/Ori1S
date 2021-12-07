using System;
using UnityEngine;

// Token: 0x020001FE RID: 510
public class CameraShakeAsset : ScriptableObject
{
	// Token: 0x1700032D RID: 813
	// (get) Token: 0x060011BE RID: 4542 RVA: 0x00051C5C File Offset: 0x0004FE5C
	public float Duration
	{
		get
		{
			if (this.m_duration == 0f)
			{
				this.m_duration = Mathf.Max(new float[]
				{
					this.PositionX.CurveDuration(),
					this.PositionY.CurveDuration(),
					this.PositionZ.CurveDuration(),
					this.RotationX.CurveDuration(),
					this.RotationY.CurveDuration(),
					this.RotationZ.CurveDuration()
				});
			}
			return this.m_duration;
		}
	}

	// Token: 0x04000F44 RID: 3908
	public AnimationCurve PositionX;

	// Token: 0x04000F45 RID: 3909
	public AnimationCurve PositionY;

	// Token: 0x04000F46 RID: 3910
	public AnimationCurve PositionZ;

	// Token: 0x04000F47 RID: 3911
	public AnimationCurve RotationX;

	// Token: 0x04000F48 RID: 3912
	public AnimationCurve RotationY;

	// Token: 0x04000F49 RID: 3913
	public AnimationCurve RotationZ;

	// Token: 0x04000F4A RID: 3914
	private float m_duration;
}
