using System;
using UnityEngine;

// Token: 0x020003F1 RID: 1009
public class CameraSettingsZoneAnimator : BaseAnimator
{
	// Token: 0x06001B71 RID: 7025 RVA: 0x00076467 File Offset: 0x00074667
	public override void CacheOriginals()
	{
	}

	// Token: 0x06001B72 RID: 7026 RVA: 0x0007646C File Offset: 0x0007466C
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		this.CameraSettingsZone.AnimatedStrength = this.AnimationCurve.Evaluate(value);
	}

	// Token: 0x06001B73 RID: 7027 RVA: 0x00076499 File Offset: 0x00074699
	public override void RestoreToOriginalState()
	{
		this.CameraSettingsZone.AnimatedStrength = 1f;
	}

	// Token: 0x17000487 RID: 1159
	// (get) Token: 0x06001B74 RID: 7028 RVA: 0x000764AB File Offset: 0x000746AB
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(this.AnimationCurve.CurveDuration());
		}
	}

	// Token: 0x17000488 RID: 1160
	// (get) Token: 0x06001B75 RID: 7029 RVA: 0x000764BE File Offset: 0x000746BE
	public override bool IsLooping
	{
		get
		{
			return this.AnimationCurve.postWrapMode != WrapMode.ClampForever;
		}
	}

	// Token: 0x040017E1 RID: 6113
	public AnimationCurve AnimationCurve = AnimationCurve.Linear(0f, 0f, 5f, 0f);

	// Token: 0x040017E2 RID: 6114
	public CameraSettingsZone CameraSettingsZone;
}
