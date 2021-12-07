using System;
using UnityEngine;

// Token: 0x02000848 RID: 2120
[ExecuteInEditMode]
public class TurbulenceTimelineAnimator : BaseAnimator
{
	// Token: 0x170007BA RID: 1978
	// (get) Token: 0x0600303A RID: 12346 RVA: 0x000CC424 File Offset: 0x000CA624
	private TurbulenceManager Manager
	{
		get
		{
			if (this.m_manager != null)
			{
				return this.m_manager;
			}
			this.m_manager = Camera.main.GetComponent<TurbulenceManager>();
			if (this.m_manager != null)
			{
				return this.m_manager;
			}
			this.m_manager = UnityEngine.Object.FindObjectOfType<TurbulenceManager>();
			return this.m_manager;
		}
	}

	// Token: 0x170007BB RID: 1979
	// (get) Token: 0x0600303B RID: 12347 RVA: 0x000CC482 File Offset: 0x000CA682
	public override bool IsLooping
	{
		get
		{
			return false;
		}
	}

	// Token: 0x0600303C RID: 12348 RVA: 0x000CC485 File Offset: 0x000CA685
	public override void CacheOriginals()
	{
	}

	// Token: 0x0600303D RID: 12349 RVA: 0x000CC488 File Offset: 0x000CA688
	public override void SampleValue(float value, bool forceSample)
	{
		if (value <= 0f)
		{
			if (TurbulenceManager.Instance != null)
			{
				TurbulenceManager.Instance.StopAnimation();
			}
			return;
		}
		this.Manager.TimeLineSpeed = this.TurbulenceSpeedAnimation.Evaluate(value);
		this.Manager.TimeLineStrength = this.TurbulenceMagnitudeAnimation.Evaluate(value);
	}

	// Token: 0x170007BC RID: 1980
	// (get) Token: 0x0600303E RID: 12350 RVA: 0x000CC4E9 File Offset: 0x000CA6E9
	public override float Duration
	{
		get
		{
			return base.AnimationCurveTimeToTime(Mathf.Max(this.TurbulenceSpeedAnimation.CurveDuration(), this.TurbulenceMagnitudeAnimation.CurveDuration()));
		}
	}

	// Token: 0x0600303F RID: 12351 RVA: 0x000CC50C File Offset: 0x000CA70C
	public override void RestoreToOriginalState()
	{
		this.SampleValue(0f, true);
	}

	// Token: 0x06003040 RID: 12352 RVA: 0x000CC51A File Offset: 0x000CA71A
	private void OnDisable()
	{
		if (TurbulenceManager.Instance != null)
		{
			TurbulenceManager.Instance.StopAnimation();
		}
	}

	// Token: 0x04002B6A RID: 11114
	public AnimationCurve TurbulenceSpeedAnimation = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	// Token: 0x04002B6B RID: 11115
	public AnimationCurve TurbulenceMagnitudeAnimation = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	// Token: 0x04002B6C RID: 11116
	private TurbulenceManager m_manager;
}
