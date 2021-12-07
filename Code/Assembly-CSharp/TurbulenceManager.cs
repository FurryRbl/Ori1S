using System;
using Game;
using UnityEngine;

// Token: 0x0200065B RID: 1627
[ExecuteInEditMode]
public class TurbulenceManager : MonoBehaviour
{
	// Token: 0x17000651 RID: 1617
	// (get) Token: 0x060027B1 RID: 10161 RVA: 0x000AC901 File Offset: 0x000AAB01
	private float WorkingTime
	{
		get
		{
			return (!Application.isPlaying) ? Time.realtimeSinceStartup : Time.time;
		}
	}

	// Token: 0x17000652 RID: 1618
	// (get) Token: 0x060027B2 RID: 10162 RVA: 0x000AC91C File Offset: 0x000AAB1C
	// (set) Token: 0x060027B3 RID: 10163 RVA: 0x000AC924 File Offset: 0x000AAB24
	public float TimeLineSpeed
	{
		get
		{
			return this.m_timelineSpeed;
		}
		set
		{
			this.m_timelineSpeed = value;
			this.m_isAnimating = true;
		}
	}

	// Token: 0x17000653 RID: 1619
	// (get) Token: 0x060027B4 RID: 10164 RVA: 0x000AC934 File Offset: 0x000AAB34
	// (set) Token: 0x060027B5 RID: 10165 RVA: 0x000AC93C File Offset: 0x000AAB3C
	public float TimeLineStrength
	{
		get
		{
			return this.m_timelineStr;
		}
		set
		{
			this.m_timelineStr = value;
			this.m_isAnimating = true;
		}
	}

	// Token: 0x060027B6 RID: 10166 RVA: 0x000AC94C File Offset: 0x000AAB4C
	private void OnEnable()
	{
		TurbulenceManager.Instance = this;
		this.m_prevTime = Time.realtimeSinceStartup;
		this.m_binder = new TurbulenceManagerBinder();
	}

	// Token: 0x060027B7 RID: 10167 RVA: 0x000AC96C File Offset: 0x000AAB6C
	public float SampleTurbulenceValueAtTime(float strength, float speed, float timeOffset, float time)
	{
		float num = this.DefaultSettings.TurbulenceMagnitudeOverTime.Evaluate(time * this.DefaultSettings.TurbulenceSpeed);
		float num2 = this.DefaultSettings.TurbulenceCurve.Evaluate(time * this.DefaultSettings.TurbulenceSpeed * speed + timeOffset);
		return num2 * num * this.DefaultSettings.TurbulenceMagnitude * strength;
	}

	// Token: 0x060027B8 RID: 10168 RVA: 0x000AC9CC File Offset: 0x000AABCC
	public float GetStrengthMultiplier()
	{
		float t = 1f - Mathf.Clamp01(this.WorkingTime - this.m_lastTimelineSet);
		float num = Mathf.SmoothStep(1f, this.TimeLineStrength, t);
		float from = num;
		float to = num;
		if (this.m_defaultSettingsHelper.FromSettings != null)
		{
			float turbulenceStrengthMult = this.m_defaultSettingsHelper.FromSettings.TurbulenceSettings.TurbulenceStrengthMult;
			from = Mathf.SmoothStep(turbulenceStrengthMult, this.TimeLineStrength, t);
		}
		if (this.m_defaultSettingsHelper.ToSettings != null)
		{
			float turbulenceStrengthMult2 = this.m_defaultSettingsHelper.ToSettings.TurbulenceSettings.TurbulenceStrengthMult;
			to = Mathf.SmoothStep(turbulenceStrengthMult2, this.TimeLineStrength, t);
		}
		return Mathf.SmoothStep(from, to, this.m_defaultSettingsHelper.TweenTime);
	}

	// Token: 0x060027B9 RID: 10169 RVA: 0x000ACA8C File Offset: 0x000AAC8C
	public float GetSpeedMultiplier()
	{
		float t = 1f - Mathf.Clamp01(this.WorkingTime - this.m_lastTimelineSet);
		float num = Mathf.SmoothStep(1f, this.TimeLineSpeed, t);
		float from = num;
		float to = num;
		if (this.m_defaultSettingsHelper.FromSettings != null)
		{
			float turbulenceSpeedMult = this.m_defaultSettingsHelper.FromSettings.TurbulenceSettings.TurbulenceSpeedMult;
			from = Mathf.SmoothStep(turbulenceSpeedMult, this.TimeLineSpeed, t);
		}
		if (this.m_defaultSettingsHelper.ToSettings != null)
		{
			float turbulenceSpeedMult2 = this.m_defaultSettingsHelper.ToSettings.TurbulenceSettings.TurbulenceSpeedMult;
			to = Mathf.SmoothStep(turbulenceSpeedMult2, this.TimeLineSpeed, t);
		}
		return Mathf.SmoothStep(from, to, this.m_defaultSettingsHelper.TweenTime);
	}

	// Token: 0x060027BA RID: 10170 RVA: 0x000ACB4C File Offset: 0x000AAD4C
	private void OnRenderObject()
	{
		if (Camera.current.gameObject != base.gameObject)
		{
			return;
		}
		if (this.m_isAnimating)
		{
			this.m_lastTimelineSet = this.WorkingTime;
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = (realtimeSinceStartup - this.m_prevTime) * Time.timeScale;
		this.m_prevTime = realtimeSinceStartup;
		this.m_defaultSettingsHelper.Advance(UI.Cameras.Current.TargetHelperPosition, UI.Cameras.Current.TimeDelta * 3f);
		TurbulenceSettings from;
		if (this.m_defaultSettingsHelper.FromSettings != null)
		{
			if (this.m_defaultSettingsHelper.FromSettings.TurbulenceSettings.TurbulenceSettings != null)
			{
				TurbulenceSettings turbulenceSettings = this.m_defaultSettingsHelper.FromSettings.TurbulenceSettings.TurbulenceSettings;
				from = turbulenceSettings;
			}
			else
			{
				from = this.DefaultSettings;
			}
		}
		else
		{
			from = this.DefaultSettings;
		}
		TurbulenceSettings to;
		if (this.m_defaultSettingsHelper.ToSettings != null)
		{
			if (this.m_defaultSettingsHelper.ToSettings.TurbulenceSettings.TurbulenceSettings != null)
			{
				TurbulenceSettings turbulenceSettings2 = this.m_defaultSettingsHelper.ToSettings.TurbulenceSettings.TurbulenceSettings;
				to = turbulenceSettings2;
			}
			else
			{
				to = this.DefaultSettings;
			}
		}
		else
		{
			to = this.DefaultSettings;
		}
		TurbulenceManagerBinder.CurrentShaderSettings currentShaderSettings = this.m_binder.Bind(from, to, this.m_defaultSettingsHelper.TweenTime, this.m_time, this.GetStrengthMultiplier(), this.GetSpeedMultiplier());
		this.m_time += currentShaderSettings.Speed * num * 1.1f;
	}

	// Token: 0x060027BB RID: 10171 RVA: 0x000ACCD8 File Offset: 0x000AAED8
	public void StopAnimation()
	{
		this.m_isAnimating = false;
	}

	// Token: 0x04002250 RID: 8784
	public static TurbulenceManager Instance;

	// Token: 0x04002251 RID: 8785
	public TurbulenceSettings DefaultSettings;

	// Token: 0x04002252 RID: 8786
	private readonly SceneDefaultSettingsHelper m_defaultSettingsHelper = new SceneDefaultSettingsHelper(1f);

	// Token: 0x04002253 RID: 8787
	private TurbulenceManagerBinder m_binder;

	// Token: 0x04002254 RID: 8788
	private float m_lastTimelineSet;

	// Token: 0x04002255 RID: 8789
	private bool m_isAnimating;

	// Token: 0x04002256 RID: 8790
	private float m_prevTime;

	// Token: 0x04002257 RID: 8791
	private float m_timelineSpeed = 1f;

	// Token: 0x04002258 RID: 8792
	private float m_timelineStr = 1f;

	// Token: 0x04002259 RID: 8793
	private float m_time;
}
