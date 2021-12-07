using System;
using Game;
using UnityEngine;

// Token: 0x020003D3 RID: 979
[ExecuteInEditMode]
public class ControllerShake : BaseAnimator
{
	// Token: 0x06001AE3 RID: 6883 RVA: 0x000736A8 File Offset: 0x000718A8
	public override void OnPoolSpawned()
	{
		base.OnPoolSpawned();
		this.m_time = 0f;
	}

	// Token: 0x17000471 RID: 1137
	// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x000736BB File Offset: 0x000718BB
	public float CurrentShake
	{
		get
		{
			if (this.Shake == null)
			{
				return 0f;
			}
			return this.Shake.ShakeCurve.Evaluate(this.m_time);
		}
	}

	// Token: 0x17000472 RID: 1138
	// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x000736EA File Offset: 0x000718EA
	public override bool IsLooping
	{
		get
		{
			return this.Shake.ShakeCurve.postWrapMode == WrapMode.Loop;
		}
	}

	// Token: 0x06001AE6 RID: 6886 RVA: 0x000736FF File Offset: 0x000718FF
	public override void CacheOriginals()
	{
	}

	// Token: 0x06001AE7 RID: 6887 RVA: 0x00073701 File Offset: 0x00071901
	public override void SampleValue(float value, bool forceSample)
	{
		this.m_time = base.TimeToAnimationCurveTime(value);
	}

	// Token: 0x17000473 RID: 1139
	// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x00073710 File Offset: 0x00071910
	public override float Duration
	{
		get
		{
			if (this.Shake == null)
			{
				return 0f;
			}
			return base.AnimationCurveTimeToTime(this.Shake.Duration);
		}
	}

	// Token: 0x06001AE9 RID: 6889 RVA: 0x00073745 File Offset: 0x00071945
	public override void RestoreToOriginalState()
	{
	}

	// Token: 0x06001AEA RID: 6890 RVA: 0x00073747 File Offset: 0x00071947
	public void PerformTheShake()
	{
		base.Initialize();
		base.AnimatorDriver.Restart();
	}

	// Token: 0x06001AEB RID: 6891 RVA: 0x0007375A File Offset: 0x0007195A
	public new void Start()
	{
		if (this.PlayAtStart)
		{
			this.PerformTheShake();
		}
	}

	// Token: 0x17000474 RID: 1140
	// (get) Token: 0x06001AEC RID: 6892 RVA: 0x00073770 File Offset: 0x00071970
	public float ModifiedStrength
	{
		get
		{
			if (Mathf.Approximately(this.m_time, 0f))
			{
				return 0f;
			}
			if (Application.isPlaying && this.ShakeOnlyIfVisibleToCamera && !UI.Cameras.Current.CameraBoundingBox.Intersects(new Bounds(base.transform.position, Vector3.one * this.ShakeObjectSize)))
			{
				return 0f;
			}
			float num = this.Strength;
			if (Application.isPlaying)
			{
				float value = Vector3.Distance((Characters.Current != null) ? Characters.Current.Position : UI.Cameras.Current.CameraTarget.TargetPosition, this.Position);
				if (this.AffectedByDistance)
				{
					num *= Mathf.InverseLerp(this.ShakeObjectSize + this.ImpactRadius, this.ShakeObjectSize, value);
					if (Mathf.Approximately(num, 0f))
					{
						return 0f;
					}
				}
			}
			return num;
		}
	}

	// Token: 0x17000475 RID: 1141
	// (get) Token: 0x06001AED RID: 6893 RVA: 0x0007386F File Offset: 0x00071A6F
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x06001AEE RID: 6894 RVA: 0x0007387C File Offset: 0x00071A7C
	public void OnEnable()
	{
		ControllerShake.All.Add(this);
	}

	// Token: 0x06001AEF RID: 6895 RVA: 0x00073889 File Offset: 0x00071A89
	public void OnDisable()
	{
		ControllerShake.All.Remove(this);
	}

	// Token: 0x0400175E RID: 5982
	public static AllContainer<ControllerShake> All = new AllContainer<ControllerShake>();

	// Token: 0x0400175F RID: 5983
	public bool ShakeOnlyIfVisibleToCamera = true;

	// Token: 0x04001760 RID: 5984
	public bool AffectedByDistance = true;

	// Token: 0x04001761 RID: 5985
	public float ShakeObjectSize = 1f;

	// Token: 0x04001762 RID: 5986
	public float ImpactRadius = 5f;

	// Token: 0x04001763 RID: 5987
	public float Strength = 1f;

	// Token: 0x04001764 RID: 5988
	public ControllerShakeAsset Shake;

	// Token: 0x04001765 RID: 5989
	private float m_time;
}
