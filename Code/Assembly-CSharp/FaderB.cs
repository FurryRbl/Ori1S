using System;
using Game;
using UnityEngine;

// Token: 0x020000FE RID: 254
[ExecuteInEditMode]
public class FaderB : Suspendable
{
	// Token: 0x060009F6 RID: 2550 RVA: 0x0002B77F File Offset: 0x0002997F
	public new void Awake()
	{
		base.Awake();
		if (UI.Fader == null)
		{
			UI.Fader = this;
		}
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x0002B79D File Offset: 0x0002999D
	public void OnEnable()
	{
		if (UI.Fader == null)
		{
			UI.Fader = this;
		}
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x0002B7B5 File Offset: 0x000299B5
	public void ChangeState(FaderB.State state)
	{
		this.m_stateCurrentTime = 0f;
		this.CurrentState = state;
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x0002B7CC File Offset: 0x000299CC
	public void UpdateState()
	{
		this.m_stateCurrentTime += Time.deltaTime;
		switch (this.CurrentState)
		{
		case FaderB.State.FadeToBlack:
			this.SetOpacity(this.m_stateCurrentTime / this.FadeInTime);
			if (this.m_stateCurrentTime > this.FadeInTime)
			{
				this.OnFadeInFinished();
				this.SetOpacity(1f);
				this.ChangeState(FaderB.State.FadeStay);
			}
			break;
		case FaderB.State.FadeStay:
			this.SetOpacity(1f);
			if (this.m_stateCurrentTime >= this.FadeStayTime)
			{
				this.ChangeState(FaderB.State.FadeFromBlack);
			}
			break;
		case FaderB.State.FadeFromBlack:
			this.SetOpacity(1f - this.m_stateCurrentTime / this.FadeOutTime);
			if (this.m_stateCurrentTime > this.FadeOutTime)
			{
				this.OnFadeOutFinished();
				this.SetOpacity(0f);
				this.ChangeState(FaderB.State.Invisible);
			}
			break;
		}
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x0002B8BC File Offset: 0x00029ABC
	protected void Start()
	{
		switch (this.CurrentState)
		{
		case FaderB.State.FadeToBlack:
			this.SetOpacity(0f);
			break;
		case FaderB.State.FadeStay:
			this.SetOpacity(1f);
			break;
		case FaderB.State.FadeFromBlack:
			this.SetOpacity(1f);
			break;
		case FaderB.State.Invisible:
			this.SetOpacity(0f);
			break;
		}
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x0002B92C File Offset: 0x00029B2C
	public virtual void OnFadeInFinished()
	{
		if (this.OnFadeInEvent != null)
		{
			Action onFadeInEvent = this.OnFadeInEvent;
			this.OnFadeInEvent = null;
			onFadeInEvent();
		}
		UberGCManager.CollectResourcesIfNeeded();
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x0002B960 File Offset: 0x00029B60
	public virtual void OnFadeOutFinished()
	{
		if (this.OnFadeOutEvent != null)
		{
			Action onFadeOutEvent = this.OnFadeOutEvent;
			this.OnFadeOutEvent = null;
			onFadeOutEvent();
		}
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x0002B98C File Offset: 0x00029B8C
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.UpdateState();
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x0002B9A0 File Offset: 0x00029BA0
	private void SetOpacity(float opacity)
	{
		UI.Cameras.Current.CameraPostProcessing.UberPostProcess.FaderBrightnessContrastSettings.Brightness = this.FaderBrightnessCurve.Evaluate(opacity);
		UI.Cameras.Current.CameraPostProcessing.UberPostProcess.FaderBrightnessContrastSettings.Contrast = this.FaderBContrastCurve.Evaluate(opacity);
		UI.Cameras.Current.CameraPostProcessing.UberPostProcess.FaderBrightnessContrastSettings.Weight = this.FaderBWeightCurve.Evaluate(opacity);
	}

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x060009FF RID: 2559 RVA: 0x0002BA1C File Offset: 0x00029C1C
	// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0002BA24 File Offset: 0x00029C24
	public override bool IsSuspended { get; set; }

	// Token: 0x06000A01 RID: 2561 RVA: 0x0002BA30 File Offset: 0x00029C30
	public void Fade(float fadeInDuration, float fadeStayDuration, float fadeOutDuration, Action fadeInComplete, Action fadeOutComplete)
	{
		if (fadeInDuration == 0f)
		{
			this.DoFade(FaderB.State.FadeStay, fadeInDuration, fadeStayDuration, fadeOutDuration);
		}
		else
		{
			this.DoFade(FaderB.State.FadeToBlack, fadeInDuration, fadeStayDuration, fadeOutDuration);
		}
		if (fadeInComplete != null)
		{
			this.OnFadeInEvent = (Action)Delegate.Combine(this.OnFadeInEvent, fadeInComplete);
		}
		if (fadeOutComplete != null)
		{
			this.OnFadeOutEvent = (Action)Delegate.Combine(this.OnFadeOutEvent, fadeOutComplete);
		}
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x0002BA9F File Offset: 0x00029C9F
	private void DoFade(FaderB.State state, float fadeInDuration, float fadeStayDuration, float fadeOutDuration)
	{
		this.FadeInTime = fadeInDuration;
		this.FadeStayTime = fadeStayDuration;
		this.FadeOutTime = fadeOutDuration;
		this.ChangeState(state);
		this.UpdateState();
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x0002BAC4 File Offset: 0x00029CC4
	public void FadeIn(float duration)
	{
		this.DoFade(FaderB.State.FadeToBlack, duration, float.PositiveInfinity, 0f);
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x0002BAD8 File Offset: 0x00029CD8
	public void FadeIn(float duration, float stayDuration)
	{
		this.DoFade(FaderB.State.FadeToBlack, duration, stayDuration, 0f);
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x0002BAE8 File Offset: 0x00029CE8
	public void FadeOut(float duration)
	{
		this.DoFade(FaderB.State.FadeFromBlack, 0f, 0f, duration);
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0002BAFC File Offset: 0x00029CFC
	public void TimelineSample(float value)
	{
		if (this.CurrentState != FaderB.State.Timeline)
		{
			this.ChangeState(FaderB.State.Timeline);
		}
		this.SetOpacity(value);
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x0002BB18 File Offset: 0x00029D18
	public bool IsFadingIn()
	{
		return this.CurrentState == FaderB.State.FadeToBlack;
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x0002BB23 File Offset: 0x00029D23
	public bool IsFadingOut()
	{
		return this.CurrentState == FaderB.State.FadeFromBlack;
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0002BB2E File Offset: 0x00029D2E
	public bool IsFadingInOrStay()
	{
		return this.CurrentState == FaderB.State.FadeToBlack || this.CurrentState == FaderB.State.FadeStay;
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0002BB47 File Offset: 0x00029D47
	public bool IsTimelineFading()
	{
		return this.CurrentState == FaderB.State.Timeline;
	}

	// Token: 0x04000838 RID: 2104
	public float FadeInTime = 1f;

	// Token: 0x04000839 RID: 2105
	public float FadeStayTime;

	// Token: 0x0400083A RID: 2106
	public float FadeOutTime = 1f;

	// Token: 0x0400083B RID: 2107
	public AnimationCurve FaderBrightnessCurve;

	// Token: 0x0400083C RID: 2108
	public AnimationCurve FaderBContrastCurve;

	// Token: 0x0400083D RID: 2109
	public AnimationCurve FaderBWeightCurve;

	// Token: 0x0400083E RID: 2110
	private float m_stateCurrentTime;

	// Token: 0x0400083F RID: 2111
	public FaderB.State CurrentState;

	// Token: 0x04000840 RID: 2112
	public Action OnFadeInEvent = delegate()
	{
	};

	// Token: 0x04000841 RID: 2113
	public Action OnFadeOutEvent = delegate()
	{
	};

	// Token: 0x020002E6 RID: 742
	public enum State
	{
		// Token: 0x04001370 RID: 4976
		FadeToBlack,
		// Token: 0x04001371 RID: 4977
		FadeStay,
		// Token: 0x04001372 RID: 4978
		FadeFromBlack,
		// Token: 0x04001373 RID: 4979
		Invisible,
		// Token: 0x04001374 RID: 4980
		Timeline
	}
}
