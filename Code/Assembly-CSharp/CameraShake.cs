using System;
using Game;
using UnityEngine;

// Token: 0x020001FD RID: 509
[ExecuteInEditMode]
public class CameraShake : BaseAnimator
{
	// Token: 0x060011AF RID: 4527 RVA: 0x000519A7 File Offset: 0x0004FBA7
	public override void OnPoolSpawned()
	{
		base.OnPoolSpawned();
		this.m_time = 0f;
	}

	// Token: 0x17000327 RID: 807
	// (get) Token: 0x060011B0 RID: 4528 RVA: 0x000519BC File Offset: 0x0004FBBC
	public Vector3 CurrentOffset
	{
		get
		{
			if (this.Shake == null)
			{
				return Vector3.zero;
			}
			return new Vector3(this.Shake.PositionX.Evaluate(this.m_time), this.Shake.PositionY.Evaluate(this.m_time), this.Shake.PositionZ.Evaluate(this.m_time));
		}
	}

	// Token: 0x17000328 RID: 808
	// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00051A28 File Offset: 0x0004FC28
	public Vector3 CurrentRotation
	{
		get
		{
			if (this.Shake == null)
			{
				return Vector3.zero;
			}
			return new Vector3(this.Shake.RotationX.Evaluate(this.m_time), this.Shake.RotationY.Evaluate(this.m_time), this.Shake.RotationZ.Evaluate(this.m_time));
		}
	}

	// Token: 0x17000329 RID: 809
	// (get) Token: 0x060011B2 RID: 4530 RVA: 0x00051A94 File Offset: 0x0004FC94
	public override bool IsLooping
	{
		get
		{
			return !(this.Shake == null) && this.Shake.PositionX.postWrapMode == WrapMode.Loop;
		}
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x00051AC7 File Offset: 0x0004FCC7
	public new void Awake()
	{
		base.Awake();
	}

	// Token: 0x060011B4 RID: 4532 RVA: 0x00051ACF File Offset: 0x0004FCCF
	public override void CacheOriginals()
	{
	}

	// Token: 0x060011B5 RID: 4533 RVA: 0x00051AD1 File Offset: 0x0004FCD1
	public override void SampleValue(float value, bool forceSample)
	{
		this.m_time = base.TimeToAnimationCurveTime(value);
	}

	// Token: 0x1700032A RID: 810
	// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00051AE0 File Offset: 0x0004FCE0
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

	// Token: 0x060011B7 RID: 4535 RVA: 0x00051B15 File Offset: 0x0004FD15
	public override void RestoreToOriginalState()
	{
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x00051B17 File Offset: 0x0004FD17
	public void PerformTheShake()
	{
		base.Initialize();
		base.AnimatorDriver.Restart();
	}

	// Token: 0x1700032B RID: 811
	// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00051B2C File Offset: 0x0004FD2C
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

	// Token: 0x1700032C RID: 812
	// (get) Token: 0x060011BA RID: 4538 RVA: 0x00051C2B File Offset: 0x0004FE2B
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x00051C38 File Offset: 0x0004FE38
	public void OnEnable()
	{
		CameraShake.All.Add(this);
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x00051C45 File Offset: 0x0004FE45
	public void OnDisable()
	{
		CameraShake.All.Remove(this);
	}

	// Token: 0x04000F3C RID: 3900
	public static AllContainer<CameraShake> All = new AllContainer<CameraShake>();

	// Token: 0x04000F3D RID: 3901
	public bool ShakeOnlyIfVisibleToCamera = true;

	// Token: 0x04000F3E RID: 3902
	public bool AffectedByDistance = true;

	// Token: 0x04000F3F RID: 3903
	public float ShakeObjectSize = 1f;

	// Token: 0x04000F40 RID: 3904
	public float ImpactRadius = 5f;

	// Token: 0x04000F41 RID: 3905
	public float Strength = 1f;

	// Token: 0x04000F42 RID: 3906
	public CameraShakeAsset Shake;

	// Token: 0x04000F43 RID: 3907
	private float m_time;
}
